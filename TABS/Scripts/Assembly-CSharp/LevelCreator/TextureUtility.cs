using System;
using System.Collections;
using TFBGames;
using UnityEngine;
using UnityEngine.Networking;

namespace LevelCreator
{
	public static class TextureUtility
	{
		private static void LoadTexture(string path, Action<Texture> onFinish)
		{
			DMIOWrapper.File.ReadAllBytes(path, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(byte[] rawByteData, Exception e)
			{
				if (rawByteData != null)
				{
					Texture2D texture2D = new Texture2D(2, 2);
					texture2D.LoadImage(rawByteData);
					onFinish?.Invoke(texture2D);
				}
				else
				{
					onFinish?.Invoke(null);
				}
			});
		}

		private static IEnumerator LoadTex(string path, Action<Texture> onFinish)
		{
			yield return LoadTextureViaHttp(path, onFinish);
		}

		private static IEnumerator LoadTextureViaHttp(string path, Action<Texture> onFinish)
		{
			Texture texture = null;
			using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
			{
				yield return uwr.SendWebRequest();
				if (uwr.isNetworkError || uwr.isHttpError)
				{
					Debug.LogError("Failed to download texture:\n" + path + "\n" + uwr.error);
				}
				else
				{
					DownloadHandlerTexture downloadHandlerTexture = (DownloadHandlerTexture)uwr.downloadHandler;
					while (!downloadHandlerTexture.isDone)
					{
						yield return new WaitForSeconds(0.1f);
					}
					texture = downloadHandlerTexture.texture;
				}
			}
			onFinish(texture);
		}

		private static IEnumerator LoadTextureViaFileIO(string path, Action<Texture> onFinish)
		{
			bool isBusy = true;
			Texture2D texture = null;
			DMIOWrapper.File.ReadAllBytes(path, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(byte[] bytes, Exception exception)
			{
				if (bytes != null && bytes.Length != 0)
				{
					texture = new Texture2D(2, 2);
					texture.LoadImage(bytes);
				}
				isBusy = false;
			});
			while (isBusy)
			{
				yield return null;
			}
			onFinish(texture);
		}

		public static void LoadTextureAsync(MonoBehaviour owner, string path, Action<Texture> onFinish)
		{
			DMIOWrapper.File.Exists(path, FileHandlingFileType.StreamingAssetsOrReadOnlyFile, delegate(bool exists)
			{
				if (exists)
				{
					if (owner != null && owner.gameObject.activeInHierarchy)
					{
						owner.StartCoroutine(LoadTex(path, onFinish));
					}
					else
					{
						onFinish?.Invoke(null);
					}
				}
				else
				{
					Debug.LogError("Invalid Texture path:" + path);
					onFinish?.Invoke(null);
				}
			});
		}

		public static void LoadTexturesAsyncTimeDelay(MonoBehaviour owner, string[] paths, Action<Texture>[] onEachFinished, float loadDelay)
		{
			owner.StartCoroutine(StartLoadTextureAsync());
			IEnumerator StartLoadTextureAsync()
			{
				for (int i = 0; i < paths.Length; i++)
				{
					LoadTextureAsync(owner, paths[i], onEachFinished[i]);
					yield return new WaitForSecondsRealtime(loadDelay);
				}
			}
		}

		public static void LoadTexturesAsyncSequential(MonoBehaviour owner, string[] paths, Action<Texture>[] onEachFinished, System.Action onAllFinished)
		{
			owner.StartCoroutine(StartSequentialAsyncLoad());
			IEnumerator StartSequentialAsyncLoad()
			{
				for (int i = 0; i < paths.Length; i++)
				{
					yield return owner.StartCoroutine(LoadTex(paths[i], onEachFinished[i]));
				}
				onAllFinished?.Invoke();
			}
		}
	}
}
