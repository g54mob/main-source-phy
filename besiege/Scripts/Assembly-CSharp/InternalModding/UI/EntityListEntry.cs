using InternalModding.LevelEntities;
using InternalModding.Mods;
using Localisation;
using UnityEngine;

namespace InternalModding.UI
{
	public class EntityListEntry : MonoBehaviour, ILocalisationAware
	{
		public DynamicText NameMesh;

		public DynamicText ModMesh;

		public UIButton EyeButton;

		public MeshRenderer EyeIcon;

		public GameObject EyeActiveBG;

		public int DisabledTextSize = 30;

		public UIButton ModTextButton;

		public MeshRenderer ThumbnailRenderer;

		private ModListUI list;

		private ModdedEntity entity;

		private bool hideState = true;

		public void OnLocalisationChange()
		{
			if (list != null && entity != null)
			{
				SetEntity(list, entity);
			}
		}

		public void Awake()
		{
			ModTextButton.Click += delegate
			{
				list.MoveTo(entity.Info.Mod);
			};
			EyeButton.Click += delegate
			{
				SetHideState(!hideState);
			};
		}

		private void SetHideState(bool state, bool uiOnly = false)
		{
			EyeActiveBG.SetActive((entity == null || entity.Info.Mod.IsEnabled) && state);
			if (hideState != state && (entity == null || entity.Info.Mod.IsEnabled))
			{
				hideState = state;
				if (!uiOnly && entity != null)
				{
					ModStatus.SetEntityHidden(entity, !state);
				}
			}
		}

		public void SetEntity(ModListUI list, ModdedEntity entity)
		{
			this.list = list;
			this.entity = entity;
			bool isEnabled = entity.Info.Mod.IsEnabled;
			EyeIcon.material.SetColor("_TintColor", (!isEnabled) ? (Color.white * 0.4f) : Color.white);
			WorkshopManager.VerifyString(entity.Name.ToUpper(), delegate(WorkshopManager.VerifyStringResult res, string str)
			{
				if (NameMesh != null)
				{
					ReferenceMaster.SetDynamicText(NameMesh, str);
				}
			});
			ReferenceMaster.SetDynamicText(ModMesh, LocalisationManager.GetTranslation(3583) + " " + entity.Info.Mod.Info.Name + ((!isEnabled) ? (" (" + LocalisationManager.GetTranslation(3584) + ")") : string.Empty));
			SetHideState(!entity.HideInUI, true);
			if (entity.Icon == null)
			{
				return;
			}
			entity.Icon.OnLoad += delegate
			{
				if (!(ThumbnailRenderer == null))
				{
					ThumbnailRenderer.material.mainTexture = (Texture2D)entity.Icon;
				}
			};
		}
	}
}
