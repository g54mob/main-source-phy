using InternalModding.Blocks;
using InternalModding.Mods;
using Localisation;
using UnityEngine;

namespace InternalModding.UI
{
	public class BlockListEntry : MonoBehaviour, ILocalisationAware
	{
		public DynamicText NameMesh;

		public DynamicText ModMesh;

		public UIButton EyeButton;

		public MeshRenderer EyeIcon;

		public GameObject EyeActiveBG;

		public int DisabledTextSize = 30;

		public UIButton ModTextButton;

		public GameObject IconPivot;

		private ModdedBlock block;

		private ModListUI list;

		private bool hideState = true;

		public void OnLocalisationChange()
		{
			if (list != null && block != null)
			{
				SetBlock(list, block);
			}
		}

		public void Awake()
		{
			ModTextButton.Click += delegate
			{
				list.MoveTo(block.Info.Mod);
			};
			EyeButton.Click += delegate
			{
				SetHideState(!hideState);
			};
		}

		private void SetHideState(bool state, bool uiOnly = false)
		{
			EyeActiveBG.SetActive((block == null || block.Info.Mod.IsEnabled) && state);
			if (hideState != state && (block == null || block.Info.Mod.IsEnabled))
			{
				hideState = state;
				if (!uiOnly && block != null)
				{
					ModStatus.SetBlockHidden(block, !state);
				}
			}
		}

		public void SetBlock(ModListUI list, ModdedBlock block)
		{
			this.list = list;
			this.block = block;
			bool isEnabled = block.Info.Mod.IsEnabled;
			EyeIcon.material.SetColor("_TintColor", (!isEnabled) ? (Color.white * 0.4f) : Color.white);
			WorkshopManager.VerifyString(block.Name.ToUpper(), delegate(WorkshopManager.VerifyStringResult res, string str)
			{
				if (NameMesh != null)
				{
					ReferenceMaster.SetDynamicText(NameMesh, str);
				}
			});
			ReferenceMaster.SetDynamicText(ModMesh, LocalisationManager.GetTranslation(3583) + " " + block.Info.Mod.Info.Name + ((!isEnabled) ? (" (" + LocalisationManager.GetTranslation(3584) + ")") : string.Empty));
			SetHideState(!block.HideInUI, true);
			MeshRenderer icon = IconPivot.transform.FindChild("Vis").GetComponent<MeshRenderer>();
			Material origMaterial = icon.material;
			icon.material = SingleInstanceFindOnly<BlockLoader>.Instance.LoadingMaterial;
			if (block.Mesh != null)
			{
				block.Mesh.OnLoad += delegate
				{
					if (!(icon == null))
					{
						icon.gameObject.GetComponent<MeshFilter>().sharedMesh = block.Mesh;
						block.Icon.SetOnTransform(icon.transform);
					}
				};
			}
			if (block.Texture == null)
			{
				return;
			}
			block.Texture.OnLoad += delegate
			{
				if (!(icon == null))
				{
					icon.material = origMaterial;
					icon.material.mainTexture = (Texture2D)block.Texture;
				}
			};
		}
	}
}
