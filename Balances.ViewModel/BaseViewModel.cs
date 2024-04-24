using System;
namespace Balances.ViewModel
{
	public class BaseViewModel<T,U> where T:class where U :class
	{
		public T ToRegistry { get; set; }

		public U OtherRegistry { get; set; }

		public List<T> List { get; set; }

		public string Message { get; set; }

		public BaseViewModel()
		{
            ToRegistry = Activator.CreateInstance<T>();
            OtherRegistry = Activator.CreateInstance<U>();

            List = new List<T>();
		}
	}
}

