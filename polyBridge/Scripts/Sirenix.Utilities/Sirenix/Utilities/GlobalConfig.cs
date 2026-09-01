using UnityEngine;

namespace Sirenix.Utilities
{
	public abstract class GlobalConfig<T> : ScriptableObject where T : GlobalConfig<T>, new()
	{
		private static GlobalConfigAttribute configAttribute;

		private static T instance;

		private static GlobalConfigAttribute ConfigAttribute
		{
			get
			{
				if (configAttribute == null)
				{
					configAttribute = typeof(T).GetCustomAttribute<GlobalConfigAttribute>();
					if (configAttribute == null)
					{
						configAttribute = new GlobalConfigAttribute(typeof(T).GetNiceName());
					}
				}
				return configAttribute;
			}
		}

		public static bool HasInstanceLoaded => instance != null;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					LoadInstanceIfAssetExists();
					T val = instance;
					if (val == null)
					{
						val = ScriptableObject.CreateInstance<T>();
					}
					instance = val;
				}
				return instance;
			}
		}

		public static void LoadInstanceIfAssetExists()
		{
			if (ConfigAttribute.IsInResourcesFolder)
			{
				string niceName = typeof(T).GetNiceName();
				instance = Resources.Load<T>(ConfigAttribute.ResourcesPath + niceName);
			}
		}

		public void OpenInEditor()
		{
			Debug.Log("Downloading, installing and launching the Unity Editor so we can open this config window in the editor, please stand by until pigs can fly and hell has frozen over...");
		}

		protected virtual void OnConfigAutoCreated()
		{
		}
	}
}
