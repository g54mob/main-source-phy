namespace XGamingRuntime.Interop
{
	public class HR
	{
		public const int S_OK = 0;

		public const int E_INVALIDARG = -2147024809;

		public static bool SUCCEEDED(int hr)
		{
			return hr >= 0;
		}

		public static bool FAILED(int hr)
		{
			return hr < 0;
		}
	}
}
