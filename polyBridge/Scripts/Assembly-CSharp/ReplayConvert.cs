using System;
using System.IO;
using FFmpeg;

public class ReplayConvert : IFFmpegHandler
{
	private Action<bool> m_Callback;

	public void Webm2Mp4(string inputFullPath, Action<bool> callback)
	{
		FFmpegParser.Handler = this;
		m_Callback = callback;
		FFmpegCommands.DirectInput(" -fflags +genpts -i " + Utils.AddQuotation(inputFullPath) + " -r 30 " + Utils.AddQuotation(Path.ChangeExtension(inputFullPath, ".mp4")));
	}

	public void OnStart()
	{
	}

	public void OnProgress(string msg)
	{
	}

	public void OnFailure(string msg)
	{
		m_Callback?.Invoke(obj: false);
	}

	public void OnSuccess(string msg)
	{
		m_Callback?.Invoke(obj: true);
	}

	public void OnFinish()
	{
	}
}
