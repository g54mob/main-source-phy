using System.Runtime.InteropServices;

namespace Sony.PS4.Dialog
{
	public class Common
	{
		public enum SystemMessageType
		{
			TRC_EMPTY_STORE = 0,
			TRC_PSN_CHAT_RESTRICTION = 1,
			TRC_PSN_UGC_RESTRICTION = 2,
			WARNING_SWITCH_TO_SIMULVIEW = 3,
			CAMERA_NOT_CONNECTED = 4,
			WARNING_PROFILE_PICTURE_AND_NAME_NOT_SHARED = 5
		}

		public enum UserMessageType
		{
			OK = 0,
			YESNO = 1,
			NONE = 2,
			OK_CANCEL = 3,
			CANCEL = 4
		}

		public enum CommonDialogResult
		{
			RESULT_BUTTON_NOT_SET = 0,
			RESULT_BUTTON_OK = 1,
			RESULT_BUTTON_CANCEL = 2,
			RESULT_BUTTON_YES = 3,
			RESULT_BUTTON_NO = 4,
			RESULT_BUTTON_1 = 5,
			RESULT_BUTTON_2 = 6,
			RESULT_BUTTON_3 = 7,
			RESULT_CANCELED = 8,
			RESULT_ABORTED = 9,
			RESULT_CLOSED = 10
		}

		public static bool IsDialogOpen => PrxCommonDialogIsDialogOpen();

		public static bool IsErrorDialogOpen => PrxErrorDialogIsDialogOpen();

		public static event Messages.EventHandler OnGotDialogResult;

		[DllImport("CommonDialog")]
		private static extern int PrxCommonDialogInitialise();

		[DllImport("CommonDialog")]
		private static extern void PrxCommonDialogUpdate();

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogIsDialogOpen();

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogErrorMessage(uint errorCode, int userId = 0);

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogSystemMessage(SystemMessageType type, int userId);

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogClose();

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxErrorDialogIsDialogOpen();

		[DllImport("CommonDialog", CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogProgressBar(string str);

		[DllImport("CommonDialog")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogProgressBarSetPercent(int percent);

		[DllImport("CommonDialog", CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogProgressBarSetMessage(string str);

		[DllImport("CommonDialog", CharSet = CharSet.Ansi)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool PrxCommonDialogUserMessage(UserMessageType type, bool infobar, string str, string button1, string button2, string button3);

		[DllImport("CommonDialog")]
		private static extern CommonDialogResult PrxCommonDialogGetResult();

		public static bool ShowErrorMessage(uint errorCode, int userId = 0)
		{
			return PrxCommonDialogErrorMessage(errorCode, userId);
		}

		public static bool ShowSystemMessage(SystemMessageType type, int userId)
		{
			return PrxCommonDialogSystemMessage(type, userId);
		}

		public static bool ShowProgressBar(string message)
		{
			return PrxCommonDialogProgressBar(message);
		}

		public static bool SetProgressBarPercent(int percent)
		{
			return PrxCommonDialogProgressBarSetPercent(percent);
		}

		public static bool SetProgressBarMessage(string message)
		{
			return PrxCommonDialogProgressBarSetMessage(message);
		}

		public static bool ShowUserMessage(UserMessageType type, bool infoBar, string str)
		{
			return PrxCommonDialogUserMessage(type, infoBar, str, "1", "2", "3");
		}

		public static bool Close()
		{
			return PrxCommonDialogClose();
		}

		public static CommonDialogResult GetResult()
		{
			return PrxCommonDialogGetResult();
		}

		public static void ProcessMessage(Messages.PluginMessage msg)
		{
			Messages.MessageType type = msg.type;
			if (type == Messages.MessageType.kDialog_GotDialogResult && Common.OnGotDialogResult != null)
			{
				Common.OnGotDialogResult(msg);
			}
		}

		public static void Initialise()
		{
			PrxCommonDialogInitialise();
		}

		public static void Update()
		{
			PrxCommonDialogUpdate();
		}
	}
}
