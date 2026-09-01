using System.Collections;
using DM;
using Landfall.TABS.Workshop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class UnitButton : SimpleButton, IPointerClickHandler, IEventSystemHandler, IUILayoutElement
	{
		public class UnitButtonData
		{
			public UnitBlueprint unit;

			public IPlacementUI placementUI;

			public bool unlocked;

			public bool secretLocked;

			public UnitButtonData(UnitBlueprint unit, IPlacementUI placementUI, bool unlocked, bool secretLocked = false)
			{
				this.unit = unit;
				this.placementUI = placementUI;
				this.unlocked = unlocked;
				this.secretLocked = secretLocked;
			}
		}

		[SerializeField]
		private Image m_icon;

		[SerializeField]
		private Image m_customIcon;

		[SerializeField]
		private TextMeshProUGUI m_cost;

		[SerializeField]
		private LocalizeText m_unitName;

		[SerializeField]
		private TextMeshProUGUI m_customUnitCost;

		[SerializeField]
		private GameObject normalParent;

		[SerializeField]
		private GameObject customParent;

		[SerializeField]
		[Tooltip("Graphic Object to display if this is locked. (optional)")]
		private Image disabledGraphic;

		private DatabaseID m_unitID;

		private IPlacementUI m_placementUI;

		private bool m_unlocked = true;

		private SoundPlayer soundPlayer;

		private static DatabaseID _lastSelectedUnit;

		public DatabaseID UnitID => m_unitID;

		public override bool Unlocked => m_unlocked;

		protected override void Start()
		{
			base.Start();
			soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		}

		public void SetUnlocked(bool unlocked, bool secretLocked)
		{
			Color color = (unlocked ? Color.white : lockedColor);
			m_icon.color = color;
			m_cost.color = color;
			m_unitName.Text.color = color;
			m_highlight.color = lockedColor;
			if (disabledGraphic != null)
			{
				disabledGraphic.gameObject.SetActive(!unlocked);
			}
			m_unlocked = unlocked;
			if (!unlocked && secretLocked)
			{
				m_unitName.gameObject.SetActive(value: false);
				disabled = true;
			}
			else
			{
				m_unitName.gameObject.SetActive(value: true);
				disabled = false;
			}
		}

		public void SetHighlightColor(Color col)
		{
			m_highlight.color = col;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (m_placementUI is PlacementUI)
			{
				if (m_unlocked && eventData.button == PointerEventData.InputButton.Left && Unlocked)
				{
					PointerSelect();
				}
			}
			else if (m_placementUI is UnitWhitelistUI && (eventData.button == PointerEventData.InputButton.Right || eventData.button == PointerEventData.InputButton.Left))
			{
				ToggleWhitelist();
			}
		}

		public void ToggleWhitelist()
		{
			if (m_placementUI != null)
			{
				m_placementUI.SelectUnit(m_unitID, this);
				InstancedHandler<SandboxUIManager>.Instance.DoOnWhitelistClickAction(m_unitID);
			}
		}

		public void PointerSelect()
		{
			SelectUnit(makeSound: true);
		}

		public void SetupUnit(Sprite icon, DatabaseID id, IPlacementUI placementUI, bool Unlocked)
		{
			m_icon.sprite = icon;
			m_unitID = id;
			m_placementUI = placementUI;
			m_unitName.Text.color = UIStyleManager.GetStyle().m_TextColor;
		}

		public void SelectUnit(bool makeSound = false)
		{
			if (m_unlocked)
			{
				if (makeSound)
				{
					UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(m_unitID);
					soundPlayer.PlaySoundEffectNonAlloc(unitBlueprint.VocalPathData, 1f, m_mainCamTransform.position + m_mainCamTransform.forward * 25f, SoundEffectVariations.MaterialType.Default, null, unitBlueprint.VoicePitch);
				}
				if (m_placementUI is PlacementUI && m_placementUI.SelectUnit(m_unitID, this))
				{
					_lastSelectedUnit = m_unitID;
				}
			}
		}

		public void Deselect()
		{
			overrideSelection = false;
			OnExit();
		}

		public override void OnEnter()
		{
			base.OnEnter();
			ShowCost(show: true);
		}

		public override void OnExit()
		{
			base.OnExit();
			ShowCost(show: false);
			UnitPreviewManager.SetUnselectEvent(m_unitID);
		}

		private void ShowCost(bool show)
		{
			if (!disabled && SandboxUIManager.UIMode == SandboxUIManager.PlacementUIMode.PlacementMode)
			{
				m_customUnitCost.enabled = show;
				m_cost.enabled = show;
				m_icon.enabled = !show;
			}
		}

		public void ShowSelection()
		{
			if (!overrideSelection)
			{
				m_targetColor = m_highlightedColor;
				StopAllCoroutines();
				StartCoroutine(ShowSelectionCoroutine());
			}
		}

		private IEnumerator ShowSelectionCoroutine()
		{
			ShowCost(show: true);
			yield return StartCoroutine(LerpColors(enter: true));
			yield return new WaitForSeconds(0.7f);
			ShowCost(show: false);
		}

		public void SetupUnit(UnitButtonData unitButtonData)
		{
			if (unitButtonData.unit.IsCustomUnit)
			{
				normalParent.SetActive(value: false);
				customParent.SetActive(value: true);
			}
			else
			{
				normalParent.SetActive(value: true);
				customParent.SetActive(value: false);
			}
			m_unitName.Localized = !unitButtonData.unit.IsCustomUnit;
			base.gameObject.name = unitButtonData.unit.Entity.Name;
			unitButtonData.unit.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && m_icon != null)
				{
					m_icon.sprite = sprite;
					m_customIcon.sprite = sprite;
				}
			});
			m_unitID = unitButtonData.unit.Entity.GUID;
			m_placementUI = unitButtonData.placementUI;
			m_unitName.LocaleID = unitButtonData.unit.Name;
			if (unitButtonData.unit != null && m_cost != null)
			{
				m_cost.text = unitButtonData.unit.GetUnitCost().ToString();
				m_customUnitCost.text = m_cost.text;
			}
			SetUnlocked(unitButtonData.unlocked, unitButtonData.secretLocked);
			if (_lastSelectedUnit == unitButtonData.unit.Entity.GUID)
			{
				StartCoroutine(DelayedSelect());
			}
		}

		private IEnumerator DelayedSelect()
		{
			yield return 1;
			base.OnEnter();
			SelectUnit();
		}

		public void SetWhitelistActive(bool active = true)
		{
			m_icon.color = (active ? Color.white : Color.grey);
		}

		public void SetupFaction(FactionButton.FactionButtonData factionButtonData, ExpandedFactionUI expandedFactionUI)
		{
			Debug.LogError("FACTION DATA IN THE UNIT BUTTON? WAA");
		}
	}
}
