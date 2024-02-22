namespace Practice.DI
{
	public interface IServiceA
	{
		string GetId();
	}
	public class ServiceA: IServiceA
	{
		private Guid _id;
        public ServiceA()
        {
            _id = Guid.NewGuid();
        }

		public string GetId()
		{
			return _id.ToString();
		}
	}

	public class ServiceAVer2 : IServiceA
	{
		public string GetId()
		{
			return "ServiceA ID";
		}
	}
}
