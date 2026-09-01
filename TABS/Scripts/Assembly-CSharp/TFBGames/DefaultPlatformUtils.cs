using UnityEngine;

namespace TFBGames
{
	public class DefaultPlatformUtils : IPlatformUtils, IService
	{
		public bool IsUIOpenOrLostFocus => !Application.isFocused;

		public bool IsRunningInBackground => false;

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}
	}
}
