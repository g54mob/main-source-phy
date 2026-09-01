using System;
using ModIO;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public abstract class BattleCreatorAssetUICellBase : MonoBehaviour
	{
		protected enum ModRatingEnum : byte
		{
			None = 0,
			Up = 1,
			Down = 2
		}

		public enum CellType
		{
			UpdateContent = 0,
			LevelContent = 1,
			CampaignContent = 2,
			UnitContent = 3
		}

		public struct UpdateContentData
		{
			public string levelName;

			public ModProfile modProfile;

			public Action<BattleCreatorAssetUICellBase> onClick;

			public Action<BattleCreatorAssetUICellBase> onRemove;

			public Action<BattleCreatorAssetUICellBase> onCog;

			public ContentTypeFilter filter;

			public BattleCreatorState battleState;

			public UpdateContentData(string levelName, ModProfile modProfile, Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, ContentTypeFilter filter, BattleCreatorState battleState)
			{
				this.levelName = levelName;
				this.modProfile = modProfile;
				this.onClick = onClick;
				this.onRemove = onRemove;
				this.onCog = onCog;
				this.filter = filter;
				this.battleState = battleState;
			}
		}

		public struct CampaignLevelData
		{
			public string levelName;

			public TABSCampaignLevelAsset level;

			public Action<BattleCreatorAssetUICellBase> onClick;

			public Action<BattleCreatorAssetUICellBase> onRemove;

			public Action<BattleCreatorAssetUICellBase> onCog;

			public Action<BattleCreatorAssetUICellBase> onUpload;

			public Action<BattleCreatorAssetUICellBase> onLoad;

			public ContentTypeFilter filter;

			public BattleCreatorState battleState;

			public CampaignLevelData(string levelName, TABSCampaignLevelAsset level, Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad, ContentTypeFilter filter, BattleCreatorState battleState)
			{
				this.levelName = levelName;
				this.level = level;
				this.onClick = onClick;
				this.onRemove = onRemove;
				this.onCog = onCog;
				this.onUpload = onUpload;
				this.onLoad = onLoad;
				this.filter = filter;
				this.battleState = battleState;
			}
		}

		public struct CampaignData
		{
			public string levelName;

			public TABSCampaignAsset campaign;

			public Action<BattleCreatorAssetUICellBase> onClick;

			public Action<BattleCreatorAssetUICellBase> onRemove;

			public Action<BattleCreatorAssetUICellBase> onCog;

			public Action<BattleCreatorAssetUICellBase> onUpload;

			public Action<BattleCreatorAssetUICellBase> onLoad;

			public ContentTypeFilter filter;

			public BattleCreatorState battleState;

			public CampaignData(string levelName, TABSCampaignAsset campaign, Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad, ContentTypeFilter filter, BattleCreatorState battleState)
			{
				this.levelName = levelName;
				this.campaign = campaign;
				this.onClick = onClick;
				this.onRemove = onRemove;
				this.onCog = onCog;
				this.onUpload = onUpload;
				this.onLoad = onLoad;
				this.filter = filter;
				this.battleState = battleState;
			}
		}

		public struct UnitData
		{
			public string levelName;

			public UnitBlueprint unitBlueprint;

			public Action<BattleCreatorAssetUICellBase> onClick;

			public Action<BattleCreatorAssetUICellBase> onRemove;

			public Action<BattleCreatorAssetUICellBase> onCog;

			public ContentTypeFilter filter;

			public BattleCreatorState battleState;

			public UnitData(string levelName, UnitBlueprint unitBlueprint, Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, ContentTypeFilter filter, BattleCreatorState battleState)
			{
				this.levelName = levelName;
				this.unitBlueprint = unitBlueprint;
				this.onClick = onClick;
				this.onRemove = onRemove;
				this.onCog = onCog;
				this.filter = filter;
				this.battleState = battleState;
			}
		}

		protected ModRatingEnum m_ModRating;

		public ContentTypeFilter ContentType { get; protected set; }

		public UnitBlueprint UnitBluePrint { get; protected set; }

		public TABSCampaignAsset CampaignAsset { get; protected set; }

		public TABSCampaignLevelAsset LevelAsset { get; protected set; }

		public string FullPath { get; protected set; }

		public string ContentName { get; protected set; }

		public string FolderPath { get; protected set; }

		public int ModID { get; protected set; }

		public string Description { get; protected set; }

		public ModProfile ModProfile { get; protected set; }

		public abstract void Init(UpdateContentData data);

		public abstract void Init(CampaignLevelData data);

		public abstract void Init(CampaignData data);

		public abstract void Init(UnitData data);

		protected abstract void AddListeners(Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad);

		protected void SetLocalBattleImageSprite(FileIOWrapper fileIO, string path, Image image)
		{
			fileIO.FileExists(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
			{
				if (exists)
				{
					fileIO.ReadAllBytes(path, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] byteArray, Exception exception)
					{
						if (byteArray != null && byteArray.Length != 0)
						{
							Texture2D texture2D = new Texture2D(2, 2);
							texture2D.LoadImage(byteArray);
							Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero);
							image.sprite = sprite;
						}
					});
				}
			});
		}
	}
}
