﻿using Infrastructure.Categories;
using Infrastructure.Commons;
using Infrastructure.Entities;
using Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Bills
{
    public interface IBillService
    {
        Task<ResponseResult> CreateBill(BillCreateViewModel model);
        List<BillCreateViewModel> GetBills();
    }
    public class BillService : IBillService
    {
        private readonly TechShopDbContext _context;

        public BillService(TechShopDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> CreateBill(BillCreateViewModel model)
        {
            using var transcation = await _context.Database.BeginTransactionAsync();
            try
            {
                var bill = new Bill
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Email = model.Email,
                    Telephone = model.PhoneNumber,
                    TotalAmount = model.BillDetails.Sum(s => s.Quantity * s.Price),
                    PaymentMethod = model.PaymentMethod,
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Status = Enums.EntityStatus.Active,

                };
                _context.Bills.Add(bill);
                await _context.SaveChangesAsync();
                foreach (var item in model.BillDetails)
                {
                    var billDetail = new BillDetail
                    {
                        Id = Guid.NewGuid(),
                        BillId = bill.Id,
                        CreatedDate = bill.CreatedDate,
                        Status = Enums.EntityStatus.Active,
                        ProductName = item.ProductName,
                        UnitPrice = item.Price,
                        Quantity = item.Quantity,

                    };
                    _context.BillDetails.Add(billDetail);
                    await _context.SaveChangesAsync();
                }
                await transcation.CommitAsync();
                return new ResponseResult(200, "Place order success");
            }
            catch (Exception)
            {
                await transcation.RollbackAsync();
                return new ResponseResult(400, "Some thing went wrong");
            }
        }

        public List<BillCreateViewModel> GetBills()
        {
            var billCreateViewModels = new List<BillCreateViewModel>();
            var bills = _context.Bills.AsQueryable();
            //var billDetails = _context.BillDetails.AsQueryable();

            var query = (from bill in bills
                        select new BillCreateViewModel
                        {
                            FirstName = bill.FirstName,
                            LastName = bill.LastName,
                            Address = bill.Address,
                            PhoneNumber = bill.Telephone,
                            PaymentMethod = bill.PaymentMethod,
                            TotalAmount = bill.TotalAmount,
						}).AsEnumerable();

            foreach (var item in query)
            {
               billCreateViewModels.Add(item);
            }
            return billCreateViewModels;
        }


    }
}
