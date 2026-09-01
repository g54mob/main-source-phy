using UnityEngine;

namespace FFmpeg
{
	public static class FFmpegParser
	{
		public const string COMMAND_CODE = "FFmpeg COMMAND: ";

		public const string ERROR_CODE = "FFmpeg EXCEPTION: ";

		public const string START_CODE = "onStart";

		public const string PROGRESS_CODE = "onProgress: ";

		public const string FAILURE_CODE = "onFailure: ";

		public const string SUCCESS_CODE = "onSuccess: ";

		public const string FINISH_CODE = "onFinish";

		public static IFFmpegHandler Handler { get; set; }

		public static void Handle(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				Debug.LogWarning("FFmpeg callback is null.");
			}
			else if (Handler != null && IsCode(ref message, "FFmpeg COMMAND: "))
			{
				if (IsCode(message, "onStart"))
				{
					Handler.OnStart();
				}
				else if (IsCode(ref message, "onProgress: "))
				{
					Handler.OnProgress(message);
				}
				else if (IsCode(ref message, "onFailure: "))
				{
					Handler.OnFailure(message);
				}
				else if (IsCode(ref message, "onSuccess: "))
				{
					Handler.OnSuccess(message);
				}
				else if (IsCode(message, "onFinish"))
				{
					Handler.OnFinish();
				}
			}
			else if (IsCode(message, "FFmpeg EXCEPTION: "))
			{
				Debug.LogError(message);
			}
		}

		private static bool IsCode(ref string message, string CODE)
		{
			if (string.IsNullOrEmpty(message))
			{
				Debug.LogWarning("FFmpegParser: Empty callback message.");
			}
			else if (message.Contains(CODE))
			{
				message = message.Remove(0, CODE.Length);
				return true;
			}
			return false;
		}

		private static bool IsCode(string message, string CODE)
		{
			if (string.IsNullOrEmpty(message))
			{
				Debug.LogWarning("FFmpegParser: Empty callback message.");
				return false;
			}
			return message.Contains(CODE);
		}
	}
}
