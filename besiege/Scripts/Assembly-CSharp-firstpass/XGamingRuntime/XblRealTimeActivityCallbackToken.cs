namespace XGamingRuntime
{
	public struct XblRealTimeActivityCallbackToken
	{
		public const int InvalidHandlerId = 0;

		public int InteropHandlerId;

		public void Reset()
		{
			InteropHandlerId = 0;
		}

		public bool IsValid()
		{
			return IsValid(InteropHandlerId);
		}

		public static bool IsValid(int interopHandlerId)
		{
			return interopHandlerId > 0;
		}
	}
}
