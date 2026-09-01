using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InternalModding
{
	public static class ModdingUtil
	{
		public const string CallbackErrorTag = "[Callback Exception] ";

		public static void PerformCallback(MulticastDelegate callback, params object[] args)
		{
			if ((object)callback == null)
			{
				return;
			}
			Delegate[] invocationList = callback.GetInvocationList();
			foreach (Delegate obj in invocationList)
			{
				try
				{
					obj.DynamicInvoke(args);
				}
				catch (Exception ex)
				{
					Debug.LogError("[Callback Exception] " + ex);
				}
			}
		}

		public static bool PerformCallback(Action action)
		{
			if (action == null)
			{
				return false;
			}
			try
			{
				action();
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogError("[Callback Exception] " + ex);
				return false;
			}
		}

		public static bool PerformCallback<TResult>(Func<TResult> func, out TResult result)
		{
			if (func == null)
			{
				result = default(TResult);
				return false;
			}
			try
			{
				result = func();
				return true;
			}
			catch (Exception ex)
			{
				Debug.LogError("[Callback Exception] " + ex);
				result = default(TResult);
				return false;
			}
		}

		public static bool IsInGame()
		{
			Scene activeScene = SceneManager.GetActiveScene();
			if (activeScene.name.Contains("Multiplayer"))
			{
				return BesiegeNetworkManager.Instance.isConnected;
			}
			return !AddPiece.IsMenuScene(activeScene.name) && activeScene.name != "INITIALISER";
		}

		public static bool TryParseGuid(string s, out Guid g)
		{
			try
			{
				g = new Guid(s);
				return true;
			}
			catch (Exception)
			{
				g = Guid.Empty;
				return false;
			}
		}
	}
}
