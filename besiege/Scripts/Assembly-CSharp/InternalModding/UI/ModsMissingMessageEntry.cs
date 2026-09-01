using InternalModding.Loading;
using InternalModding.Mods;
using Steamworks;
using UnityEngine;

namespace InternalModding.UI
{
	public class ModsMissingMessageEntry : ClickBehaviour
	{
		public MeshRenderer Icon;

		public TextMesh Text;

		public Tooltip Tooltip;

		protected ModList.Mod mod;

		public void Setup(ModList.Mod mod)
		{
			this.mod = mod;
			base.gameObject.name = "ModsMissingEntry: " + mod.Name;
			Text.text = mod.Name;
			CenterText();
			Tooltip.enabled = ModIds.GetModById(mod.Id, true) != null;
		}

		private void OpenSteamWorkshop()
		{
			if (mod.Workshop)
			{
				string text = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + mod.WorkshopId;
				if (SteamManager.Initialized)
				{
					SteamFriends.ActivateGameOverlayToWebPage(text);
				}
				else
				{
					Application.OpenURL(text);
				}
			}
			else
			{
				string text2 = "https://steamcommunity.com/workshop/browse/?appid=346010&searchtext=" + mod.Name.Replace(" ", "+") + "&childpublishedfileid=0&browsesort=trend&section=readytouseitems&requiredtags%5B%5B=Mods";
				if (SteamManager.Initialized)
				{
					SteamFriends.ActivateGameOverlayToWebPage(text2);
				}
				else
				{
					Application.OpenURL(text2);
				}
			}
		}

		public override void OnClicked()
		{
			OpenSteamWorkshop();
		}

		private void CenterText()
		{
			MeshRenderer component = Text.GetComponent<MeshRenderer>();
			float x = component.bounds.min.x;
			float x2 = Icon.bounds.max.x;
			float num = (x + x2) / 2f;
			float num2 = component.transform.parent.position.x - num;
			component.transform.position += Vector3.right * num2;
			Icon.transform.position += Vector3.right * num2;
		}
	}
}
