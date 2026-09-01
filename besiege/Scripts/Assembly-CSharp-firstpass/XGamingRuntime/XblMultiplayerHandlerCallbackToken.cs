using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public struct XblMultiplayerHandlerCallbackToken
	{
		public const int InvalidHandlerId = 0;

		public XblFunctionContext FunctionContext;

		public void Reset()
		{
			FunctionContext = default(XblFunctionContext);
		}

		public bool IsValid()
		{
			return IsValid(FunctionContext.context);
		}

		public static bool IsValid(int interopHandlerId)
		{
			return interopHandlerId > 0;
		}
	}
}
