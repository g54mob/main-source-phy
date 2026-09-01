namespace FFmpeg
{
	public interface IFFmpegHandler
	{
		void OnStart();

		void OnProgress(string msg);

		void OnFailure(string msg);

		void OnSuccess(string msg);

		void OnFinish();
	}
}
