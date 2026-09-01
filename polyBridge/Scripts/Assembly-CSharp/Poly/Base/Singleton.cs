namespace Poly.Base
{
	public class Singleton<TClass, TContext> where TClass : Singleton<TClass>, new()
	{
		public static TClass instance { get; private set; }

		static Singleton()
		{
			Init();
			RuntimeInitializer.AddReinitAction(Init);
		}

		private static void Init()
		{
			instance = new TClass();
		}

		public static implicit operator bool(Singleton<TClass, TContext> instance)
		{
			return instance != null;
		}
	}
	public class Singleton<TClass> : Singleton<TClass, int> where TClass : Singleton<TClass>, new()
	{
	}
}
