namespace TFBGames
{
	public class DummyUserAuthenticator : INetworkUserAuthenticator, IService
	{
		public void AuthenticateUserAsync(string regionCode, AuthenticateUserCallback callback)
		{
			callback?.Invoke(null, null);
		}

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
