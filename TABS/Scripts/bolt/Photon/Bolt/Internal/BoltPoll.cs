using Photon.Bolt.Utils;
using UnityEngine;

namespace Photon.Bolt.Internal
{
	public class BoltPoll : MonoBehaviour
	{
		public bool AllowImmediateShutdown = true;

		protected void Awake()
		{
			Application.runInBackground = true;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		protected void Update()
		{
			try
			{
				if (Time.timeScale != 1f && BoltRuntimeSettings.instance.overrideTimeScale)
				{
					Time.timeScale = 1f;
				}
			}
			finally
			{
				BoltCore.Update();
			}
		}

		protected void FixedUpdate()
		{
			BoltCore._timer.Stop();
			BoltCore._timer.Reset();
			BoltCore._timer.Start();
			BoltCore.Poll();
			BoltCore._timer.Stop();
			DebugInfo.PollTime = DebugInfo.GetStopWatchElapsedMilliseconds(BoltCore._timer);
		}

		protected void OnDisable()
		{
			if (Application.isEditor && AllowImmediateShutdown)
			{
				BoltCore.ShutdownImmediate();
			}
		}

		protected void OnDestroy()
		{
			if (Application.isEditor && AllowImmediateShutdown)
			{
				BoltCore.ShutdownImmediate();
			}
		}

		protected void OnApplicationQuit()
		{
			BoltCore.Quit();
			if (AllowImmediateShutdown)
			{
				BoltCore.ShutdownImmediate();
			}
		}
	}
}
