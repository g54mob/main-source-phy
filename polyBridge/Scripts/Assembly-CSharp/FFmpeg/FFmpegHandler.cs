using UnityEngine;

namespace FFmpeg
{
	public class FFmpegHandler : IFFmpegHandler
	{
		public void OnStart()
		{
			Debug.Log("FFmpegHandler.Start");
		}

		public void OnProgress(string msg)
		{
			Debug.Log("FFmpegHandler.Progress: " + msg);
		}

		public void OnFailure(string msg)
		{
			Debug.Log("FFmpegHandler.Failure: " + msg);
		}

		public void OnSuccess(string msg)
		{
			Debug.Log("FFmpegHandler.Success: " + msg);
		}

		public void OnFinish()
		{
			Debug.Log("FFmpegHandler.Finish");
		}
	}
}
