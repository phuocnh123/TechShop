using Microsoft.AspNetCore.Mvc;
using Practice.DI;
using Practice.Models;
using System.Diagnostics;
using System.Net.WebSockets;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Student> _students;

        private readonly IServiceA _serviceA;
        private readonly IServiceA _serviceA1;

        public HomeController(ILogger<HomeController> logger, IServiceA serviceA, IServiceA serviceA1)
        {
           
            _logger = logger;
            _students = new List<Student>()
            {
                new Student(1, "messis", 15),
                new Student(2, "ronaldo", 16),
                new Student(3, "foden", 19),
            };    
            _serviceA = serviceA;
            _serviceA1 = serviceA1;
            
        }

        public IActionResult Index()
        {
            var a = "hello".AddThreeDots();
            return View(_students);
        }

        public IActionResult Privacy()
        {
            return Json(_students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LearnDi()
        {
            ViewBag.Id = _serviceA.GetId();
            ViewBag.Id1 = _serviceA1.GetId();
            return View();
        }
		public IActionResult LearnGroupBy()
		{
            var students = new List<StudentDemo>()
            {
                new StudentDemo("nam", 15),
                new StudentDemo("lan", 16),
                new StudentDemo("hoa", 15),
                new StudentDemo("tuan", 17),
                new StudentDemo("nam", 15),
                new StudentDemo("quy", 17),
                new StudentDemo("huong", 16),
            };

            var countResult = new List<StudentCount>();
            var result = students.GroupBy(s=> s.Age).ToList();
            foreach (var item in result)
            {
                var studentCount = new StudentCount
                {
                    Age = item.Key,
                    Count = item.ToList().Count
                };
				countResult.Add(studentCount);
			}
            
			return Json(countResult);
		}

        public class StudentDemo
        {
            public StudentDemo(string name, int age)
            {
                Name = name;
                Age = age;
            }
            public int Age { get; set; }
            public string Name { get; set; }
        }

        public class StudentCount
        {
            public int Count { get; set; }
            public int Age { get; set; }
        }
	}
}