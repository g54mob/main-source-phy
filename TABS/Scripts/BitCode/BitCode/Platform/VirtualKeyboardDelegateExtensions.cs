using System;

namespace BitCode.Platform
{
	public static class VirtualKeyboardDelegateExtensions
	{
		public static void SafelyInvoke(this KeyboardClosedEventHandler self, string text, bool accepted)
		{
			try
			{
				self(text, accepted);
			}
			catch (Exception)
			{
			}
		}
	}
}
