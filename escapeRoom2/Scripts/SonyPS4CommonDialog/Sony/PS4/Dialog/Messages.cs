using System;
using System.Runtime.InteropServices;

namespace Sony.PS4.Dialog
{
	public class Messages
	{
		public enum MessageType
		{
			kDialog_NotSet = 0,
			kDialog_Log = 1,
			kDialog_LogWarning = 2,
			kDialog_LogError = 3,
			kDialog_GotDialogResult = 4,
			kDialog_GotIMEDialogResult = 5,
			kDialog_GotSigninDialogResult = 6
		}

		public delegate void EventHandler(PluginMessage msg);

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PluginMessage
		{
			public MessageType type;

			public int dataSize;

			public IntPtr data;

			public string Text
			{
				get
				{
					switch (type)
					{
					case MessageType.kDialog_Log:
					case MessageType.kDialog_LogWarning:
					case MessageType.kDialog_LogError:
						return Marshal.PtrToStringAnsi(data);
					default:
						return "no text";
					}
				}
			}

			public int Int => type switch
			{
				MessageType.kDialog_GotDialogResult => (int)data, 
				MessageType.kDialog_GotIMEDialogResult => (int)data, 
				MessageType.kDialog_GotSigninDialogResult => (int)data, 
				_ => 0, 
			};
		}

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogHasMessage();

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogGetFirstMessage(out PluginMessage msg);

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogRemoveFirstMessage();

		public static bool HasMessage()
		{
			return PrxCommonDialogHasMessage();
		}

		public static void RemoveFirstMessage()
		{
			PrxCommonDialogRemoveFirstMessage();
		}

		public static void GetFirstMessage(out PluginMessage msg)
		{
			PrxCommonDialogGetFirstMessage(out msg);
		}
	}
}
