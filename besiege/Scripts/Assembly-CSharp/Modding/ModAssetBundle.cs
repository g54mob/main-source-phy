using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using InternalModding;
using UnityEngine;

namespace Modding
{
	public class ModAssetBundle : ModResource
	{
		private static Dictionary<long, AssetBundle> bundlesLoaded = new Dictionary<long, AssetBundle>();

		private bool isDone;

		private bool hasError;

		private string error;

		private static MD5 md5 = MD5.Create();

		public override bool HasError
		{
			get
			{
				return isDone && hasError;
			}
		}

		public override string Error
		{
			get
			{
				return (!HasError) ? string.Empty : error;
			}
		}

		public override bool Loaded
		{
			get
			{
				return isDone;
			}
		}

		public AssetBundle AssetBundle { get; private set; }

		public override event Action OnLoad
		{
			add
			{
				if (Loaded && AssetBundle != null && AssetBundle.LoadAllAssets().Length == 0)
				{
					Reload();
				}
				base.OnLoad += value;
			}
			remove
			{
				base.OnLoad -= value;
			}
		}

		internal ModAssetBundle()
		{
			isDone = false;
			hasError = false;
			error = string.Empty;
		}

		public T LoadAsset<T>(string name) where T : UnityEngine.Object
		{
			if (AssetBundle == null)
			{
				return (T)null;
			}
			if (AssetBundle.LoadAllAssets().Length == 0)
			{
				ReloadSync();
			}
			return AssetBundle.LoadAsset<T>(name);
		}

		public T LoadAsset<T>(int index) where T : UnityEngine.Object
		{
			if (AssetBundle == null)
			{
				return (T)null;
			}
			if (AssetBundle.LoadAllAssets().Length == 0)
			{
				ReloadSync();
			}
			return (T)AssetBundle.LoadAllAssets()[index];
		}

		private void Reload()
		{
			isDone = false;
			hasError = false;
			error = string.Empty;
			if (AssetBundle != null)
			{
				AssetBundle.Unload(false);
			}
			SingleInstanceFindOnly<ModManager>.Instance.StartCoroutine(Load());
		}

		private void ReloadSync()
		{
			isDone = false;
			hasError = false;
			error = string.Empty;
			if (AssetBundle != null)
			{
				AssetBundle.Unload(false);
			}
			try
			{
				byte[] binary = File.ReadAllBytes(base.Info.Path);
				AssetBundle = AssetBundle.LoadFromMemory(binary);
			}
			catch (Exception ex)
			{
				hasError = true;
				error = ex.Message;
			}
			isDone = true;
		}

		internal override IEnumerator Load()
		{
			long p = GetFileHash(base.Info.Path);
			if (bundlesLoaded.ContainsKey(p))
			{
				Debug.LogError("[ModAssetBundle]: Tried to load already loaded AssetBundle: " + base.Info.Path + ", do you have duplicate mods?");
				while (bundlesLoaded.ContainsKey(p) && !isDone)
				{
					AssetBundle = bundlesLoaded[p];
					isDone = AssetBundle != null;
					yield return null;
				}
				if (isDone)
				{
					isDone = true;
					TriggerOnLoad();
				}
				else
				{
					error = "[ModAssetBundle]: Failed after trying to use original AssetBundle " + base.Info.Path;
					hasError = true;
					isDone = true;
				}
				yield break;
			}
			bundlesLoaded.Add(p, null);
			WWW www = new WWW("file:///" + base.Info.Path);
			yield return www;
			if (string.IsNullOrEmpty(www.error))
			{
				AssetBundle = www.assetBundle;
				bundlesLoaded[p] = AssetBundle;
			}
			else
			{
				AssetBundle = null;
				bundlesLoaded.Remove(p);
			}
			error = www.error;
			hasError = !string.IsNullOrEmpty(error);
			isDone = true;
			www.Dispose();
			TriggerOnLoad();
		}

		internal override void ApplyToObject(GameObject go)
		{
		}

		private static long GetFileHash(string filePath)
		{
			md5.Initialize();
			using (FileStream inputStream = File.OpenRead(filePath))
			{
				byte[] value = md5.ComputeHash(inputStream);
				return BitConverter.ToInt64(value, 0);
			}
		}

		public static implicit operator AssetBundle(ModAssetBundle bundle)
		{
			if (bundle == null)
			{
				return null;
			}
			return bundle.AssetBundle;
		}
	}
}
