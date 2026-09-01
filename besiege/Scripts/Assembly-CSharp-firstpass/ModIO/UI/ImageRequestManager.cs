using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ModIO.UI
{
	public class ImageRequestManager : MonoBehaviour, IModSubscriptionsUpdateReceiver
	{
		protected class Callbacks
		{
			public Texture2D fallback;

			public List<Action<Texture2D>> succeeded;

			public List<Action<WebRequestError>> failed;

			public Action<Texture2D> onTextureDownloaded;
		}

		public const string GUEST_AVATAR_URL = ":GUEST_AVATAR:";

		private static ImageRequestManager _instance;

		[Tooltip("Should the downloads made by this object be excluded from logging?")]
		public bool excludeDownloadsFromLogs = true;

		public bool clearCacheOnDisable = true;

		public bool storeIfSubscribed = true;

		public Texture2D guestAvatar;

		private Dictionary<string, Callbacks> m_callbackMap = new Dictionary<string, Callbacks>();

		public static ImageRequestManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = UIUtilities.FindComponentInAllScenes<ImageRequestManager>(true);
					if (_instance == null)
					{
						GameObject gameObject = new GameObject("Image Request Manager");
						_instance = gameObject.AddComponent<ImageRequestManager>();
					}
				}
				return _instance;
			}
		}

		[Obsolete("Use ImageRequestManager.excludeDownloadsFromLogs instead")]
		public bool logDownloads
		{
			get
			{
				return !excludeDownloadsFromLogs;
			}
			set
			{
				excludeDownloadsFromLogs = !value;
			}
		}

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
			}
		}

		public virtual void RequestModLogo(int modId, LogoImageLocator locator, LogoSize size, Action<Texture2D> onLogoReceived, Action<Texture2D> onFallbackFound, Action<WebRequestError> onError)
		{
			if (onLogoReceived == null)
			{
				return;
			}
			if (locator == null)
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingLocator());
				}
				return;
			}
			string url = locator.GetSizeURL(size);
			string fileName = locator.GetFileName();
			if (string.IsNullOrEmpty(url))
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingURL());
				}
			}
			else
			{
				if (TryAddCallbacksToExisting(url, onLogoReceived, onFallbackFound, onError))
				{
					return;
				}
				Callbacks callbacks = CreateCallbacksEntry(url, onLogoReceived, onError);
				if (storeIfSubscribed)
				{
					callbacks.onTextureDownloaded = delegate(Texture2D texture)
					{
						if (LocalUser.SubscribedModIds.Contains(modId))
						{
							CacheClient.SaveModLogo(modId, fileName, size, texture, null);
						}
					};
				}
				CacheClient.LoadModLogo(modId, fileName, size, delegate(Texture2D texture)
				{
					if (!(this == null))
					{
						if (texture != null)
						{
							OnRequestSucceeded(url, texture);
						}
						else
						{
							DownloadImage(url);
						}
					}
				});
			}
		}

		public virtual void RequestModGalleryImage(int modId, GalleryImageLocator locator, ModGalleryImageSize size, Action<Texture2D> onImageReceived, Action<Texture2D> onFallbackFound, Action<WebRequestError> onError)
		{
			if (onImageReceived == null)
			{
				return;
			}
			if (locator == null)
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingLocator());
				}
				return;
			}
			string url = locator.GetSizeURL(size);
			string fileName = locator.GetFileName();
			if (string.IsNullOrEmpty(url))
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingURL());
				}
			}
			else
			{
				if (TryAddCallbacksToExisting(url, onImageReceived, onFallbackFound, onError))
				{
					return;
				}
				Callbacks callbacks = CreateCallbacksEntry(url, onImageReceived, onError);
				if (storeIfSubscribed)
				{
					callbacks.onTextureDownloaded = delegate(Texture2D texture)
					{
						if (LocalUser.SubscribedModIds.Contains(modId))
						{
							CacheClient.SaveModGalleryImage(modId, fileName, size, texture, null);
						}
					};
				}
				CacheClient.LoadModGalleryImage(modId, fileName, size, delegate(Texture2D texture)
				{
					if (!(this == null))
					{
						if (texture != null)
						{
							OnRequestSucceeded(url, texture);
						}
						else
						{
							DownloadImage(url);
						}
					}
				});
			}
		}

		public virtual void RequestUserAvatar(int userId, AvatarImageLocator locator, UserAvatarSize size, Action<Texture2D> onAvatarReceived, Action<Texture2D> onFallbackFound, Action<WebRequestError> onError)
		{
			if (onAvatarReceived == null)
			{
				return;
			}
			if (locator == null)
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingLocator());
				}
				return;
			}
			string url = locator.GetSizeURL(size);
			if (string.IsNullOrEmpty(url))
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingURL());
				}
			}
			else if (url == ":GUEST_AVATAR:")
			{
				if (onAvatarReceived != null)
				{
					onAvatarReceived(guestAvatar);
				}
			}
			else
			{
				if (TryAddCallbacksToExisting(url, onAvatarReceived, onFallbackFound, onError))
				{
					return;
				}
				CreateCallbacksEntry(url, onAvatarReceived, onError);
				CacheClient.LoadUserAvatar(userId, size, delegate(Texture2D texture)
				{
					if (!(this == null))
					{
						if (texture != null)
						{
							OnRequestSucceeded(url, texture);
						}
						else
						{
							DownloadImage(url);
						}
					}
				});
			}
		}

		public virtual void RequestYouTubeThumbnail(int modId, string youTubeId, Action<Texture2D> onThumbnailReceived, Action<WebRequestError> onError)
		{
			if (onThumbnailReceived == null)
			{
				return;
			}
			string url = Utility.GenerateYouTubeThumbnailURL(youTubeId);
			if (string.IsNullOrEmpty(url))
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingURL());
				}
			}
			else
			{
				if (TryAddCallbacksToExisting(url, onThumbnailReceived, null, onError))
				{
					return;
				}
				Callbacks callbacks = CreateCallbacksEntry(url, onThumbnailReceived, onError);
				if (storeIfSubscribed)
				{
					callbacks.onTextureDownloaded = delegate(Texture2D texture)
					{
						if (LocalUser.SubscribedModIds.Contains(modId))
						{
							CacheClient.SaveModYouTubeThumbnail(modId, youTubeId, texture, null);
						}
					};
				}
				CacheClient.LoadModYouTubeThumbnail(modId, youTubeId, delegate(Texture2D texture)
				{
					if (!(this == null))
					{
						if (texture != null)
						{
							OnRequestSucceeded(url, texture);
						}
						else
						{
							DownloadImage(url);
						}
					}
				});
			}
		}

		public virtual void RequestImage(string url, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			if (onSuccess == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(url))
			{
				if (onError != null)
				{
					onError(GenerateErrorForMissingURL());
				}
			}
			else if (!TryAddCallbacksToExisting(url, onSuccess, null, onError))
			{
				CreateCallbacksEntry(url, onSuccess, onError);
				DownloadImage(url);
			}
		}

		protected virtual bool TryAddCallbacksToExisting(string url, Action<Texture2D> onSuccess, Action<Texture2D> onFallbackFound, Action<WebRequestError> onError)
		{
			Callbacks value = null;
			if (m_callbackMap.TryGetValue(url, out value))
			{
				value.succeeded.Add(onSuccess);
				value.failed.Add(onError);
				if (onFallbackFound != null && value.fallback != null)
				{
					onFallbackFound(value.fallback);
				}
				return true;
			}
			return false;
		}

		protected virtual Callbacks CreateCallbacksEntry(string url, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			Callbacks callbacks = new Callbacks();
			callbacks.fallback = null;
			callbacks.succeeded = new List<Action<Texture2D>>();
			callbacks.failed = new List<Action<WebRequestError>>();
			Callbacks callbacks2 = callbacks;
			callbacks2.succeeded.Add(onSuccess);
			if (onError != null)
			{
				callbacks2.failed.Add(onError);
			}
			m_callbackMap[url] = callbacks2;
			return callbacks2;
		}

		protected UnityWebRequestAsyncOperation DownloadImage(string url)
		{
			UnityWebRequest request = UnityWebRequest.Get(url);
			UnityWebRequestAsyncOperation operation = request.SendWebRequest();
			operation.completed += delegate
			{
				OnDownloadCompleted(operation.webRequest, url);
			};
			return operation;
		}

		protected virtual void OnDownloadCompleted(UnityWebRequest webRequest, string imageURL)
		{
			if (this == null)
			{
				return;
			}
			if (webRequest == null)
			{
				OnRequestFailed(imageURL, WebRequestError.GenerateLocal("Error downloading image"));
				return;
			}
			Callbacks value;
			bool flag = m_callbackMap.TryGetValue(imageURL, out value);
			if (value == null)
			{
				Debug.LogWarning("[mod.io] ImageRequestManager completed a download but the callbacks entry for the download was null.\nImageURL = " + imageURL + "\nWebRequest.URL = " + webRequest.url + "\nm_callbackMap.TryGetValue() = " + flag);
				return;
			}
			if (webRequest.isHttpError() || webRequest.isNetworkError())
			{
				WebRequestError error = WebRequestError.GenerateFromWebRequest(webRequest);
				OnRequestFailed(imageURL, error);
				return;
			}
			byte[] data = webRequest.downloadHandler.data;
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(data);
			texture2D.name = imageURL;
			if (value.onTextureDownloaded != null)
			{
				value.onTextureDownloaded(texture2D);
			}
			OnRequestSucceeded(imageURL, texture2D);
		}

		protected virtual void OnRequestFailed(string url, WebRequestError error)
		{
			if (this == null || string.IsNullOrEmpty(url) || !m_callbackMap.ContainsKey(url))
			{
				return;
			}
			foreach (Action<WebRequestError> item in m_callbackMap[url].failed)
			{
				if (item != null)
				{
					item(error);
				}
			}
			m_callbackMap.Remove(url);
		}

		protected virtual void OnRequestSucceeded(string url, Texture2D texture)
		{
			if (this == null || !m_callbackMap.ContainsKey(url))
			{
				return;
			}
			foreach (Action<Texture2D> item in m_callbackMap[url].succeeded)
			{
				if (item != null)
				{
					item(texture);
				}
			}
			m_callbackMap.Remove(url);
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (!storeIfSubscribed || addedSubscriptions.Count <= 0)
			{
				return;
			}
			ModManager.GetModProfiles(addedSubscriptions, delegate(ModProfile[] modProfiles)
			{
				if (!(this == null) && base.isActiveAndEnabled && modProfiles != null)
				{
					IList<int> subscribedModIds = LocalUser.SubscribedModIds;
					foreach (ModProfile modProfile in modProfiles)
					{
						if (modProfile != null && subscribedModIds.Contains(modProfile.id))
						{
							StoreModImages(modProfile);
						}
					}
				}
			}, null);
		}

		protected virtual void StoreModImages(ModProfile profile)
		{
		}

		protected Texture2D[] PullImagesFromCache(IList<string> urlList)
		{
			return new Texture2D[urlList.Count];
		}

		protected WebRequestError GenerateErrorForMissingLocator()
		{
			WebRequestError webRequestError = WebRequestError.GenerateLocal("Locator supplied was null.");
			webRequestError.displayMessage = "There was an error downloading this image. Try again later.";
			return webRequestError;
		}

		protected WebRequestError GenerateErrorForMissingURL()
		{
			WebRequestError webRequestError = WebRequestError.GenerateLocal("No valid URL exists in the locator.");
			webRequestError.displayMessage = "There was an error downloading this image. Try again later.";
			return webRequestError;
		}

		[Obsolete("No longer supported.")]
		public virtual void RequestImageForData(ImageDisplayData data, bool original, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			string imageURL = data.GetImageURL(original);
			Func<Texture2D> loadFromDisk = null;
			Action<Texture2D> saveToDisk = null;
			switch (data.descriptor)
			{
			case ImageDescriptor.ModLogo:
			{
				LogoSize size2 = ((!original) ? ImageDisplayData.logoThumbnailSize : LogoSize.Original);
				loadFromDisk = () => CacheClient.LoadModLogo(data.ownerId, data.imageId, size2);
				if (!storeIfSubscribed)
				{
					break;
				}
				saveToDisk = delegate(Texture2D t)
				{
					if (LocalUser.SubscribedModIds.Contains(data.ownerId))
					{
						CacheClient.SaveModLogo(data.ownerId, data.imageId, size2, t);
					}
				};
				break;
			}
			case ImageDescriptor.ModGalleryImage:
			{
				ModGalleryImageSize size3 = ((!original) ? ImageDisplayData.galleryThumbnailSize : ModGalleryImageSize.Original);
				loadFromDisk = () => CacheClient.LoadModGalleryImage(data.ownerId, data.imageId, size3);
				if (!storeIfSubscribed)
				{
					break;
				}
				saveToDisk = delegate(Texture2D t)
				{
					if (LocalUser.SubscribedModIds.Contains(data.ownerId))
					{
						CacheClient.SaveModGalleryImage(data.ownerId, data.imageId, size3, t);
					}
				};
				break;
			}
			case ImageDescriptor.YouTubeThumbnail:
				loadFromDisk = () => CacheClient.LoadModYouTubeThumbnail(data.ownerId, data.imageId);
				if (!storeIfSubscribed)
				{
					break;
				}
				saveToDisk = delegate(Texture2D t)
				{
					if (LocalUser.SubscribedModIds.Contains(data.ownerId))
					{
						CacheClient.SaveModYouTubeThumbnail(data.ownerId, data.imageId, t);
					}
				};
				break;
			case ImageDescriptor.UserAvatar:
			{
				UserAvatarSize size = ((!original) ? ImageDisplayData.avatarThumbnailSize : UserAvatarSize.Original);
				loadFromDisk = () => CacheClient.LoadUserAvatar(data.ownerId, size);
				break;
			}
			}
			RequestImage_Internal(imageURL, loadFromDisk, saveToDisk, onSuccess, onError);
		}

		[Obsolete("No longer supported.")]
		protected virtual void RequestImage_Internal(string url, Func<Texture2D> loadFromDisk, Action<Texture2D> saveToDisk, Action<Texture2D> onSuccess, Action<WebRequestError> onError)
		{
			if (!TryAddCallbacksToExisting(url, onSuccess, null, onError))
			{
				Callbacks callbacks = CreateCallbacksEntry(url, onSuccess, onError);
				callbacks.onTextureDownloaded = saveToDisk;
				Texture2D texture2D = loadFromDisk();
				if (texture2D != null)
				{
					OnRequestSucceeded(url, texture2D);
				}
				else
				{
					DownloadImage(url);
				}
			}
		}

		[Obsolete("No longer supported.")]
		protected virtual void RequestImage_Internal<E>(IMultiSizeImageLocator<E> locator, E size, Func<Texture2D> loadFromDisk, Action<Texture2D> saveToDisk, Action<Texture2D> onSuccess, Action<Texture2D> onFallback, Action<WebRequestError> onError)
		{
			string sizeURL = locator.GetSizeURL(size);
			if (!TryAddCallbacksToExisting(sizeURL, onSuccess, onFallback, onError))
			{
				Callbacks callbacks = CreateCallbacksEntry(sizeURL, onSuccess, onError);
				callbacks.onTextureDownloaded = saveToDisk;
				Texture2D texture2D = loadFromDisk();
				if (texture2D != null)
				{
					OnRequestSucceeded(sizeURL, texture2D);
				}
				else
				{
					DownloadImage(sizeURL);
				}
			}
		}
	}
}
