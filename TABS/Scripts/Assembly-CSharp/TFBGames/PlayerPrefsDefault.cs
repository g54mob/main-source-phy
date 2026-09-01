using System;
using UnityEngine;

namespace TFBGames
{
	public class PlayerPrefsDefault : IPlayerPrefsPlatform, IService
	{
		public bool IsReady => true;

		public event Action<bool> Saved;

		public void OnInitializationTimedOut()
		{
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

		public void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		public void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		public float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key);
		}

		public float GetFloat(string key, float defaultValue)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		public int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		public int GetInt(string key, int defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		public string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		public string GetString(string key, string defaultValue)
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}

		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public void Save()
		{
			PlayerPrefs.Save();
			this.Saved?.Invoke(obj: true);
		}

		public void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}

		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}
}
