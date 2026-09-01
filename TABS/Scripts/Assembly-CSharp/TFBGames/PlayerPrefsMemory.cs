using System;

namespace TFBGames
{
	public class PlayerPrefsMemory : IPlayerPrefsPlatform, IService
	{
		private readonly PlayerPrefsData data = new PlayerPrefsData();

		public bool IsReady => true;

		public event Action<bool> Saved;

		public void OnInitializationTimedOut()
		{
		}

		public void DeleteAll()
		{
			if (data != null)
			{
				data.IntPrefs?.Clear();
				data.FloatPrefs?.Clear();
				data.StringPrefs?.Clear();
			}
		}

		public void DeleteKey(string key)
		{
			if (data != null)
			{
				if (data.IntPrefs != null && data.IntPrefs.ContainsKey(key))
				{
					data.IntPrefs.Remove(key);
				}
				if (data.FloatPrefs != null && data.FloatPrefs.ContainsKey(key))
				{
					data.FloatPrefs.Remove(key);
				}
				if (data.StringPrefs != null && data.StringPrefs.ContainsKey(key))
				{
					data.StringPrefs.Remove(key);
				}
			}
		}

		public float GetFloat(string key)
		{
			if (data?.FloatPrefs != null && data.FloatPrefs.ContainsKey(key))
			{
				return data.FloatPrefs[key];
			}
			return 0f;
		}

		public float GetFloat(string key, float defaultValue)
		{
			if (data?.FloatPrefs != null && data.FloatPrefs.ContainsKey(key))
			{
				return data.FloatPrefs[key];
			}
			return defaultValue;
		}

		public int GetInt(string key)
		{
			if (data?.IntPrefs != null && data.IntPrefs.ContainsKey(key))
			{
				return data.IntPrefs[key];
			}
			return 0;
		}

		public int GetInt(string key, int defaultValue)
		{
			if (data?.IntPrefs != null && data.IntPrefs.ContainsKey(key))
			{
				return data.IntPrefs[key];
			}
			return defaultValue;
		}

		public string GetString(string key)
		{
			if (data?.StringPrefs != null && data.StringPrefs.ContainsKey(key))
			{
				return data.StringPrefs[key];
			}
			return string.Empty;
		}

		public string GetString(string key, string defaultValue)
		{
			if (data?.StringPrefs != null && data.StringPrefs.ContainsKey(key))
			{
				return data.StringPrefs[key];
			}
			return defaultValue;
		}

		public bool HasKey(string key)
		{
			if (data == null)
			{
				return false;
			}
			if ((data.IntPrefs == null || !data.IntPrefs.ContainsKey(key)) && (data.FloatPrefs == null || !data.FloatPrefs.ContainsKey(key)))
			{
				if (data.StringPrefs != null)
				{
					return data.StringPrefs.ContainsKey(key);
				}
				return false;
			}
			return true;
		}

		public void Save()
		{
			this.Saved?.Invoke(obj: true);
		}

		public void SetFloat(string key, float value)
		{
			if (data?.FloatPrefs != null)
			{
				data.FloatPrefs[key] = value;
			}
		}

		public void SetInt(string key, int value)
		{
			if (data?.IntPrefs != null)
			{
				data.IntPrefs[key] = value;
			}
		}

		public void SetString(string key, string value)
		{
			if (data?.StringPrefs != null)
			{
				data.StringPrefs[key] = value;
			}
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
