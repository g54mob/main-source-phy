using InternalModding.Mods;
using Localisation;
using UnityEngine;

namespace InternalModding.UI
{
	public class ModListEntry : MonoBehaviour, ILocalisationAware
	{
		public DynamicText NameMesh;

		public DynamicText AuthorMesh;

		public DynamicText DescriptionMesh;

		public MeshRenderer IconRenderer;

		public GameObject MPWarning;

		public UIButton ToggleButton;

		public GameObject ActiveBg;

		public GameObject WarningBg;

		public Tooltip DisableWarningTooltip;

		public SimpleUIButton WorkshopButton;

		public GameObject WorkshopButtonEnabled;

		public GameObject WorkshopButtonDisabled;

		public Transform ToggleOnText;

		public Transform ToggleOffText;

		public int MaxCharactersPerLine = 90;

		public ModListUI List;

		public ModContainer Mod;

		private bool toggleState = true;

		public void OnLocalisationChange()
		{
			if (Mod != null)
			{
				SetMod(Mod);
			}
		}

		public void Awake()
		{
			ToggleButton.Click += delegate
			{
				SetToggleState(!toggleState);
			};
			WorkshopButton.Click += delegate
			{
				List.OpenWorkshopInterface(Mod);
			};
		}

		private void SetToggleState(bool state, bool uiOnly = false)
		{
			ToggleOnText.gameObject.SetActive(state);
			ToggleOffText.gameObject.SetActive(!state);
			if (Mod != null)
			{
				if (!uiOnly && toggleState != state)
				{
					if (state)
					{
						ModStatus.EnableMod(Mod);
					}
					else
					{
						ModStatus.DisableMod(Mod);
					}
					List.RefreshMods();
				}
				else if (Mod.IsActive && !state)
				{
					ActiveBg.SetActive(false);
					DisableWarningTooltip.enabled = true;
					WarningBg.SetActive(true);
				}
				else
				{
					ActiveBg.SetActive(state);
					DisableWarningTooltip.enabled = false;
					WarningBg.SetActive(false);
				}
			}
			toggleState = state;
		}

		public void SetMod(ModContainer mod)
		{
			base.name = "Entry (" + mod.Info.Name + ")";
			Mod = mod;
			SetNameMesh(mod.Info.Name, mod.Info.Author, !mod.Info.MultiplayerCompatible);
			if (mod.HadLoadOrActivateErrors)
			{
				SetDescriptionMesh(LocalisationManager.GetTranslation(3579));
			}
			else
			{
				WorkshopManager.VerifyString(mod.Info.Description, delegate(WorkshopManager.VerifyStringResult res, string str)
				{
					SetDescriptionMesh(str);
				});
			}
			if (mod.Info.Icon != null)
			{
				mod.Info.Icon.OnLoad += delegate
				{
					if (!(IconRenderer == null))
					{
						IconRenderer.material.mainTexture = (Texture2D)mod.Info.Icon;
					}
				};
			}
			SetToggleState(mod.IsEnabled, true);
			if (mod.Info.FromWorkshop || !ReferenceMaster.IsPlatformReady())
			{
				Object.Destroy(WorkshopButtonEnabled);
				Object.Destroy(WorkshopButtonDisabled);
			}
			else
			{
				WorkshopButtonEnabled.SetActive(!mod.Info.DebugEnabled);
				WorkshopButtonDisabled.SetActive(mod.Info.DebugEnabled);
			}
		}

		public void SetNameMesh(string name, string author, bool mpWarning)
		{
			MPWarning.SetActive(mpWarning);
			WorkshopManager.VerifyString(ReferenceMaster.CamelCaseToSpaces(name).ToUpper(), delegate(WorkshopManager.VerifyStringResult res, string str)
			{
				if (NameMesh != null)
				{
					ReferenceMaster.SetDynamicText(NameMesh, str);
					ReferenceMaster.SetDynamicText(AuthorMesh, LocalisationManager.GetTranslation(3578) + " " + ReferenceMaster.CamelCaseToSpaces(author).ToUpper());
					float num = NameMesh.bounds.max.x * NameMesh.transform.lossyScale.x + NameMesh.transform.position.x;
					AuthorMesh.transform.position = new Vector3(num + 0.06f, AuthorMesh.transform.position.y, AuthorMesh.transform.position.z);
				}
			});
		}

		public void SetDescriptionMesh(string text)
		{
			if (DescriptionMesh == null)
			{
				return;
			}
			string[] array = text.Split('\n');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length >= MaxCharactersPerLine)
				{
					array[i] = array[i].Substring(0, MaxCharactersPerLine) + "...";
				}
			}
			string text2 = ((array.Length == 0) ? string.Empty : ((array.Length != 1) ? (array[0] + "\n" + array[1]) : array[0]));
			if (array.Length > 2 && !text2.EndsWith("..."))
			{
				text2 += "...";
			}
			ReferenceMaster.SetDynamicText(DescriptionMesh, text2);
		}
	}
}
