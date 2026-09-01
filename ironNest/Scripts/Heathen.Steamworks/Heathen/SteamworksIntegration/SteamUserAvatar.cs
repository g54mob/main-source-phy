using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Avatars", "image")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserAvatar : MonoBehaviour
	{
		public RawImage image;

		private SteamUserData _inspector;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void HandleSteamInitialized()
		{
		}

		private void HandlePersonaStateChange(UserData user, EPersonaChange flag)
		{
		}

		public void LoadAvatar(UserData user)
		{
		}

		public void LoadAvatar(CSteamID user)
		{
		}

		public void LoadAvatar(ulong user)
		{
		}

		private void AvatarLoaded(Texture2D texture)
		{
		}
	}
}
