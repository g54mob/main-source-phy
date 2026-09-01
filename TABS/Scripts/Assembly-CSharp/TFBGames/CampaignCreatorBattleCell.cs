using System;
using System.Collections.Generic;
using System.IO;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class CampaignCreatorBattleCell : BattleCreatorAssetUICellBase, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IFilterableItem
	{
		[SerializeField]
		private TextMeshProUGUI battleName;

		[SerializeField]
		public Image mapImage;

		[Space]
		[SerializeField]
		private ScaleJiggle scaleJiggle;

		[SerializeField]
		private float selectedJiggleScale;

		[Space]
		[SerializeField]
		private GameObject arrows;

		[Space]
		[SerializeField]
		private GameObject visibleIndex;

		private Transform battleList;

		private Transform newCampaignList;

		public List<CampaignCreatorBattleCell> campaignList;

		public string FilteringName => base.LevelAsset.Entity.Name.ToLower();

		public GameObject ItemCellGameObject => base.gameObject;

		public event Action MovedToOtherList;

		public event Action EditedList;

		public void ShowArrows(bool show)
		{
			arrows.SetActive(show && base.transform.parent == newCampaignList);
		}

		public void UpdateAllIndicies()
		{
			for (int i = 0; i < campaignList.Count; i++)
			{
				campaignList[i].UpdateIndex();
			}
		}

		public void UpdateIndex()
		{
			for (int i = 0; i < campaignList.Count; i++)
			{
				if (campaignList[i].LevelAsset == base.LevelAsset)
				{
					SetIndex(i + 1);
					return;
				}
			}
			HideIndex();
		}

		public void SetIndex(int index)
		{
			visibleIndex.GetComponentInChildren<TMP_Text>().text = index.ToString();
			visibleIndex.gameObject.SetActive(value: true);
		}

		public void HideIndex()
		{
			visibleIndex.gameObject.SetActive(value: false);
		}

		public void ShowSelection()
		{
			scaleJiggle.targetScale = selectedJiggleScale;
		}

		public void HideSelection()
		{
			scaleJiggle.targetScale = 1f;
		}

		public void OnSelect(BaseEventData eventData)
		{
			ShowSelection();
			ShowArrows(show: true);
		}

		public void OnDeselect(BaseEventData eventData)
		{
			HideSelection();
			ShowArrows(show: false);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			ToggleFromList();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnSelect(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnDeselect(eventData);
		}

		public void Init(Transform battleList, Transform newCampaignList, List<CampaignCreatorBattleCell> campaignList)
		{
			this.battleList = battleList;
			this.newCampaignList = newCampaignList;
			this.campaignList = campaignList;
		}

		public void ChangeSiblingIndex(int deltaChange)
		{
			int siblingIndex = MathHelpers.Wrap(base.transform.GetSiblingIndex() + deltaChange, 0, base.transform.parent.childCount - 1);
			base.transform.SetSiblingIndex(siblingIndex);
		}

		public void MoveToOtherList()
		{
			base.transform.SetParent((base.transform.parent == battleList) ? newCampaignList : battleList);
			this.MovedToOtherList?.Invoke();
			ShowArrows(show: false);
		}

		public void ToggleFromList()
		{
			if (campaignList.Contains(this))
			{
				RemoveFromList();
			}
			else
			{
				AddToList();
			}
		}

		public void AddToList()
		{
			if (!campaignList.Contains(this))
			{
				campaignList.Add(this);
				UpdateAllIndicies();
				this.EditedList?.Invoke();
			}
		}

		public void RemoveFromList()
		{
			campaignList.Remove(this);
			HideIndex();
			UpdateAllIndicies();
			this.EditedList?.Invoke();
		}

		public override void Init(UpdateContentData data)
		{
			throw new NotImplementedException();
		}

		public override void Init(CampaignLevelData data)
		{
			base.ContentType = data.filter;
			base.LevelAsset = data.level;
			base.Description = base.LevelAsset.ModProfile?.summary;
			battleName.text = data.levelName;
			base.FullPath = base.LevelAsset.FilePath;
			base.FolderPath = new FileInfo(base.FullPath).Directory.FullName;
			FileIOWrapper service = ServiceLocator.GetService<FileIOWrapper>();
			string path = base.FolderPath + "/Picture.png";
			SetLocalBattleImageSprite(service, path, mapImage);
			base.ContentName = data.levelName;
		}

		public override void Init(CampaignData data)
		{
			throw new NotImplementedException();
		}

		public override void Init(UnitData data)
		{
			throw new NotImplementedException();
		}

		protected override void AddListeners(Action<BattleCreatorAssetUICellBase> onClick, Action<BattleCreatorAssetUICellBase> onRemove, Action<BattleCreatorAssetUICellBase> onCog, Action<BattleCreatorAssetUICellBase> onUpload, Action<BattleCreatorAssetUICellBase> onLoad)
		{
			throw new NotImplementedException();
		}
	}
}
