using System;
using System.IO;
using ModIO;
using ModIO.API;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorAssetUICell : BattleCreatorAssetUICellBase
	{
		[SerializeField]
		private TextMeshProUGUI m_LevelNameText;

		[SerializeField]
		private TextMeshProUGUI m_ExtraDataText;

		[SerializeField]
		private Button m_Button;

		[SerializeField]
		private Button m_DeleteButton;

		[SerializeField]
		private Button m_CogButton;

		[SerializeField]
		private Button m_ThumbsUpButton;

		[SerializeField]
		private Button m_ThumbsDownButton;

		[SerializeField]
		private Image m_MapImage;

		public override void Init(UpdateContentData data)
		{
			base.ContentType = data.filter;
			m_LevelNameText.text = data.levelName;
			m_MapImage.color = Color.black;
			base.LevelAsset = new TABSCampaignLevelAsset();
			base.LevelAsset.SetCustomUnit(data.modProfile.id, data.modProfile);
			base.Description = data.modProfile?.summary;
			base.ContentName = data.levelName;
			AddListeners(data.onClick, data.onRemove, data.onCog, null, null);
			m_ExtraDataText.text = TABSUtils.UnixToDate(data.modProfile.dateUpdated).ToShortDateString();
			InitRating(base.LevelAsset.ModID);
		}

		public override void Init(CampaignLevelData data)
		{
			base.ContentType = data.filter;
			base.LevelAsset = data.level;
			base.Description = base.LevelAsset.ModProfile?.summary;
			m_LevelNameText.text = data.levelName;
			base.FullPath = base.LevelAsset.FilePath;
			base.FolderPath = new FileInfo(base.FullPath).Directory.FullName;
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string path = base.FolderPath + "/Picture.png";
			SetLocalBattleImageSprite(service, path, m_MapImage);
			base.ContentName = data.levelName;
		}

		public override void Init(CampaignData data)
		{
			base.ContentType = data.filter;
			base.CampaignAsset = data.campaign;
			base.Description = base.CampaignAsset.ModProfile?.summary;
			m_LevelNameText.text = data.levelName;
			m_ExtraDataText.text = "Battles: " + data.campaign.LevelsInCampaign.Length;
			m_MapImage.gameObject.SetActive(value: false);
			base.ContentName = data.levelName;
			base.FolderPath = data.campaign.FolderPath;
			base.FullPath = data.campaign.FilePath;
			AddListeners(data.onClick, data.onRemove, data.onCog, data.onUpload, data.onLoad);
			InitRating(base.CampaignAsset.ModID);
		}

		public override void Init(UnitData data)
		{
			base.ContentType = data.filter;
			base.UnitBluePrint = data.unitBlueprint;
			m_LevelNameText.text = data.levelName;
			m_MapImage.gameObject.SetActive(value: false);
			base.ContentName = data.levelName;
			AddListeners(data.onClick, data.onRemove, data.onCog, null, null);
			InitRating(base.UnitBluePrint.ModID);
		}

		private void ClearRatings()
		{
			m_ThumbsDownButton.GetComponent<Image>().color = Color.white;
			m_ThumbsUpButton.GetComponent<Image>().color = Color.white;
		}

		private void RateUp()
		{
			ClearRatings();
			m_ThumbsUpButton.GetComponent<Image>().color = Color.green;
			m_ModRating = ModRatingEnum.Up;
		}

		private void RateDown()
		{
			ClearRatings();
			m_ThumbsDownButton.GetComponent<Image>().color = Color.red;
			m_ModRating = ModRatingEnum.Down;
		}

		private void InitRating(int modID)
		{
			ClearRatings();
			m_ModRating = ModRatingEnum.None;
			base.ModID = modID;
			if (base.ModID == 0)
			{
				m_ThumbsDownButton.gameObject.SetActive(value: false);
				m_ThumbsUpButton.gameObject.SetActive(value: false);
				return;
			}
			m_ThumbsUpButton.onClick.AddListener(OnThumbsUpClicked);
			m_ThumbsDownButton.onClick.AddListener(OnThumbsDownClicked);
			ModRating[] localUserRatings = CustomContentLoaderModIO.LocalUserRatings;
			foreach (ModRating modRating in localUserRatings)
			{
				if (modRating.modId == base.ModID)
				{
					Debug.Log("Found rating: " + modRating.ratingValue);
					if (modRating.ratingValue == ModRatingValue.Negative)
					{
						RateDown();
					}
					else if (modRating.ratingValue == ModRatingValue.Positive)
					{
						RateUp();
					}
				}
			}
		}

		private void OnThumbsDownClicked()
		{
			if (m_ModRating != ModRatingEnum.Down)
			{
				Debug.Log("Pressed Thumbs down!");
				AddModRatingParameters addModRatingParameters = new AddModRatingParameters();
				addModRatingParameters.ratingValue = ModRatingValue.Negative;
				APIClient.AddModRating(base.ModID, addModRatingParameters, OnRatingSuccess, OnRatingFailed);
				RateDown();
			}
		}

		private void OnThumbsUpClicked()
		{
			if (m_ModRating != ModRatingEnum.Up)
			{
				Debug.Log("Pressed Thumbs up!");
				AddModRatingParameters addModRatingParameters = new AddModRatingParameters();
				addModRatingParameters.ratingValue = ModRatingValue.Positive;
				APIClient.AddModRating(base.ModID, addModRatingParameters, OnRatingSuccess, OnRatingFailed);
				RateUp();
			}
		}

		private void OnRatingFailed(WebRequestError obj)
		{
			Debug.Log("On Rating Failed! " + obj.displayMessage);
		}

		private void OnRatingSuccess(APIMessage obj)
		{
			Debug.Log("On Rating Success! " + obj.message);
			ServiceLocator.GetService<CustomContentLoaderModIO>().FetchUserRatings();
		}

		protected override void AddListeners(Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad)
		{
			m_Button.onClick.AddListener(delegate
			{
				onClick(this);
			});
			if (onRemove == null)
			{
				m_DeleteButton.gameObject.SetActive(value: false);
			}
			else
			{
				m_DeleteButton.onClick.AddListener(delegate
				{
					onRemove(this);
				});
			}
			onCog = null;
			if (onCog == null)
			{
				m_CogButton.gameObject.SetActive(value: false);
				return;
			}
			m_CogButton.onClick.AddListener(delegate
			{
				onCog(this);
			});
		}
	}
}
