using System.Collections.Generic;
using UnityEngine;

namespace FFmpeg
{
	public class FFmpegWrapper : MonoBehaviour
	{
		private static Queue<string> callbackMSGs = new Queue<string>();

		private static void StandaloneCallback(string message)
		{
			callbackMSGs.Enqueue(message);
		}

		private void Start()
		{
			StandaloneProxy.Begin(StandaloneCallback);
		}

		internal void Abort()
		{
			StandaloneProxy.Abort();
		}

		internal void Execute(string[] cmd)
		{
			StandaloneProxy.Execute(string.Join(" ", cmd));
		}

		private void Update()
		{
			if (callbackMSGs.Count > 0)
			{
				FFmpegParser.Handle(callbackMSGs.Dequeue());
			}
		}
	}
}
