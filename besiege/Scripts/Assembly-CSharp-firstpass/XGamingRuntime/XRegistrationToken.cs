using System.Runtime.InteropServices;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XRegistrationToken
	{
		internal GCHandle CallbackHandle { get; private set; }

		internal XTaskQueueRegistrationToken Token { get; private set; }

		internal XRegistrationToken(GCHandle callbackHandle, XTaskQueueRegistrationToken token)
		{
			CallbackHandle = callbackHandle;
			Token = token;
		}
	}
}
