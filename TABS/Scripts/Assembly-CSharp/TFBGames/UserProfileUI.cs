using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class UserProfileUI : MonoBehaviour, IUserProfileUI, IService
	{
		[SerializeField]
		protected Image profilePicture;

		[SerializeField]
		protected TextMeshProUGUI userName;

		[SerializeField]
		[Tooltip("Holds the change button and will be hidden when the user cannot change profiles.")]
		protected GameObject changeProfileButtonHolder;

		[SerializeField]
		[Tooltip("Holds the loading indicator.")]
		protected GameObject loadingIndicatorHolder;

		protected int? userId;

		protected ulong? gameCoreUserId;

		protected Texture2D userTexture;

		protected bool didShowOrHide;

		protected PlayerActions playerActions;

		protected AccountManager accountManager;

		public bool IsVisible => base.gameObject.activeInHierarchy;

		public bool CanChangeProfile { get; protected set; }

		public virtual void Show(bool canChangeProfile)
		{
			didShowOrHide = true;
			CanChangeProfile = canChangeProfile;
			base.gameObject.SetActive(value: true);
		}

		public virtual void Hide()
		{
			didShowOrHide = true;
			base.gameObject.SetActive(value: false);
		}

		public virtual void SetCanChangeProfile(bool canChangeProfile)
		{
			CanChangeProfile = canChangeProfile;
		}

		protected virtual void Awake()
		{
			Object.DontDestroyOnLoad(base.gameObject);
			if (!didShowOrHide)
			{
				base.gameObject.SetActive(value: false);
			}
			accountManager = ServiceLocator.GetService<AccountManager>();
		}

		protected void OnLoadedPicture(Texture2D texture)
		{
			ShowLoadingIndicator(visible: false);
			if (!(texture == null))
			{
				CleanupUserTexture();
				userTexture = texture;
				SetProfileSprite(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
				ShowProfilePicture(visible: true);
			}
		}

		protected void ShowProfilePicture(bool visible)
		{
			if (profilePicture != null && profilePicture.gameObject.activeSelf != visible)
			{
				profilePicture.gameObject.SetActive(visible);
			}
		}

		protected void ShowLoadingIndicator(bool visible)
		{
			if (loadingIndicatorHolder != null && loadingIndicatorHolder.activeSelf != visible)
			{
				loadingIndicatorHolder.SetActive(visible);
			}
		}

		protected void ShowChangeProfileButtonHolder(bool visible)
		{
			if (changeProfileButtonHolder != null && changeProfileButtonHolder.activeSelf != visible)
			{
				changeProfileButtonHolder.SetActive(visible);
			}
		}

		protected void SetUserName(string text)
		{
			if (userName != null)
			{
				userName.text = text;
			}
		}

		protected void SetProfileSprite(Sprite sprite)
		{
			if (profilePicture != null)
			{
				profilePicture.sprite = sprite;
			}
		}

		protected void CleanupUserTexture()
		{
			if (!(userTexture == null))
			{
				SetProfileSprite(null);
				Object.Destroy(userTexture);
				userTexture = null;
			}
		}

		public virtual void OnRegister()
		{
		}

		public virtual void OnAwake()
		{
		}

		public virtual void OnStart()
		{
		}

		public virtual void OnFixedUpdate()
		{
		}

		public virtual void OnLateUpdate()
		{
		}

		public virtual void UnRegister()
		{
		}

		public void OnUpdate()
		{
		}
	}
}
