using System.Collections;
using Landfall.TABS.Workshop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionButton : SimpleButton, IUILayoutElement, IPointerClickHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler
	{
		public class FactionButtonData
		{
			public Faction faction;

			public IPlacementUI placementUI;

			public bool unlocked;

			public int index;

			public bool expandButton;

			public Transform followTransform;

			public FactionButtonData(Faction faction, IPlacementUI placementUI, bool unlocked, int index, Transform followTransform)
			{
				this.faction = faction;
				this.placementUI = placementUI;
				this.unlocked = unlocked;
				this.index = index;
				expandButton = false;
			}

			public FactionButtonData(Transform followTransform)
			{
				expandButton = true;
				this.followTransform = followTransform;
			}
		}

		public Sprite plusSprite;

		public Image m_Image;

		public Image m_Outline;

		public Image m_Color;

		public Faction faction;

		private IPlacementUI m_placementUI;

		public int index;

		[SerializeField]
		private LocalizeText m_textTooltip;

		public FactionButtonData m_factionButtonData;

		private CodeAnimation m_animation;

		private FactionButtonMover factionButtonMover;

		private bool m_WasAlreadySelected;

		private bool m_unlocked;

		private FactionButtonPhlerp pherlp;

		private Transform lerpTargetTransform;

		private Transform initialPositionTransform;

		private ExpandedFactionUI expandedFactionUI;

		private bool isDragging;

		public DatabaseID m_factionID
		{
			get
			{
				if (faction == null || faction.Entity == null)
				{
					return new DatabaseID(0);
				}
				return faction.Entity.GUID;
			}
		}

		public LocalizeText Tooltip => m_textTooltip;

		public override bool Unlocked => m_unlocked;

		public bool WasAlreadySelected => m_WasAlreadySelected;

		public void SetUnlocked(bool unlocked)
		{
			Color color = (unlocked ? Color.white : lockedColor);
			m_Image.color = color;
			m_textTooltip.Text.color = color;
			m_highlight.color = lockedColor;
			m_unlocked = unlocked;
		}

		private void Awake()
		{
			m_animation = GetComponentInChildren<CodeAnimation>();
			factionButtonMover = GetComponent<FactionButtonMover>();
		}

		protected override void Start()
		{
			base.Start();
			if (m_placementUI is UnitWhitelistUI && m_factionButtonData?.faction != null)
			{
				SetUnlocked(unlocked: true);
				if (m_placementUI.WasLastSelectedFactionIndex(m_factionID))
				{
					PopFactionTabUp();
				}
			}
		}

		public void AddForce(Vector3 force)
		{
			if ((bool)pherlp)
			{
				pherlp.AddForce(force);
			}
		}

		public void HighlightOutline(Color highlightColor)
		{
			m_Outline.color = highlightColor;
		}

		public void SetPoistionDelayed(Transform target)
		{
			if (target.gameObject.activeInHierarchy && base.gameObject.activeInHierarchy)
			{
				StartCoroutine(SetPoistionDelayedInternal(target));
			}
		}

		private IEnumerator SetPoistionDelayedInternal(Transform target)
		{
			yield return new WaitForEndOfFrame();
			base.transform.position = target.position;
		}

		public void SetFollowTransformLast()
		{
			if (lerpTargetTransform != null)
			{
				lerpTargetTransform.SetAsLastSibling();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}

		public void ToggleWhitelist()
		{
			if (m_placementUI != null)
			{
				m_WasAlreadySelected = m_placementUI.WasLastSelectedFactionIndex(m_factionID);
				m_placementUI.SelectFaction(m_factionID, this);
				InstancedHandler<SandboxUIManager>.Instance.DoOnWhitelistFactionClickAction(m_factionID);
			}
		}

		public void PointerSelect()
		{
			if (m_placementUI != null)
			{
				m_WasAlreadySelected = m_placementUI.WasLastSelectedFactionIndex(m_factionID);
				m_placementUI.SelectFaction(m_factionID, this);
			}
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (!disabled && base.isActiveAndEnabled)
			{
				base.OnPointerEnter(eventData);
				m_textTooltip.enabled = true;
				m_animation.PlayIn();
			}
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			if (!disabled && base.isActiveAndEnabled)
			{
				base.OnPointerExit(eventData);
				m_animation.PlayOut();
			}
		}

		public void StopDragMode()
		{
			disabled = false;
			factionButtonMover.enabled = true;
		}

		public void SetDraggingMode()
		{
			disabled = true;
			factionButtonMover.enabled = false;
		}

		public void PopFactionTab()
		{
			StopCoroutine(PopFactionTabCoroutine());
			StartCoroutine(PopFactionTabCoroutine());
		}

		private IEnumerator PopFactionTabCoroutine()
		{
			m_animation.PlayIn();
			factionButtonMover.MoveUp();
			yield return new WaitForSeconds(0.7f);
			factionButtonMover.MoveDown();
			m_animation.PlayOut();
		}

		public void PopFactionTabUp()
		{
			if (!disabled)
			{
				m_animation.PlayIn();
				factionButtonMover.MoveUp();
			}
		}

		public void PopFactionTabDown()
		{
			if (!disabled)
			{
				factionButtonMover.MoveDown();
				m_animation.PlayOut();
			}
		}

		public void SetupFaction(FactionButtonData factionButtonData, ExpandedFactionUI expandedFactionUI)
		{
			this.expandedFactionUI = expandedFactionUI;
			Sprite icon = plusSprite;
			m_factionButtonData = factionButtonData;
			m_placementUI = factionButtonData.placementUI;
			lerpTargetTransform = factionButtonData.followTransform;
			if (!factionButtonData.expandButton)
			{
				m_textTooltip.Localized = !m_factionButtonData.faction.IsCustom;
				m_textTooltip.LocaleID = m_factionButtonData.faction.Entity.Name;
				faction = factionButtonData.faction;
				factionButtonData.faction.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
				{
					if (sprite != null && icon != null)
					{
						icon = sprite;
					}
				});
				SetUnlocked(factionButtonData.unlocked);
				index = factionButtonData.index;
			}
			else
			{
				m_textTooltip.LocaleID = "BUTTON_MORE_FACTIONS";
				SetUnlocked(unlocked: true);
			}
			if (icon == null)
			{
				m_Image.enabled = false;
			}
			else
			{
				m_Image.sprite = icon;
			}
			if (!factionButtonData.expandButton && factionButtonData.faction.m_FactionColor.a > 0.01f)
			{
				m_Color.color = factionButtonData.faction.m_FactionColor;
			}
			else
			{
				m_Color.enabled = false;
			}
		}

		public void Deselect()
		{
			overrideSelection = false;
			PopFactionTabDown();
			HighlightAndScale(m_normalColor, scaleUp: false);
		}

		public void SetupUnit(UnitButton.UnitButtonData unitButtonData)
		{
			Debug.LogError("UNIT DATA IN THE FACTION BUTTON? WAA");
		}

		public Faction GetFaction()
		{
			return faction;
		}

		public void StopPhlerp()
		{
			pherlp = GetComponent<FactionButtonPhlerp>();
			if ((bool)pherlp)
			{
				pherlp.enabled = false;
			}
		}

		public Transform GetFollowTransform()
		{
			if (lerpTargetTransform != null)
			{
				return lerpTargetTransform;
			}
			return initialPositionTransform;
		}

		public void SetupPhlerp()
		{
			pherlp = GetComponent<FactionButtonPhlerp>();
			if (pherlp == null)
			{
				pherlp = base.gameObject.AddComponent<FactionButtonPhlerp>();
			}
			pherlp.SetTarget(lerpTargetTransform);
			pherlp.Drag = 15f;
			pherlp.Spring = 15f;
			pherlp.enabled = true;
		}

		public void SetFollowTransform(Transform target)
		{
			if (base.isActiveAndEnabled && target.gameObject.activeSelf)
			{
				if (initialPositionTransform != null)
				{
					Object.Destroy(initialPositionTransform.gameObject);
					initialPositionTransform = null;
				}
				initialPositionTransform = target;
				StartCoroutine(SetFollowDelayed(target));
			}
		}

		private IEnumerator SetFollowDelayed(Transform target)
		{
			yield return new WaitForEndOfFrame();
			lerpTargetTransform = target;
			SetupPhlerp();
		}

		public void DestoryTarget()
		{
			Object.Destroy(lerpTargetTransform.gameObject);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && m_factionButtonData.expandButton)
			{
				Object.FindObjectOfType<PlacementUI>().ToggleExpandFaction();
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && !isDragging)
			{
				PointerSelect();
			}
			isDragging = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && !isDragging)
			{
				isDragging = true;
				expandedFactionUI.AttemtFactionShardDrag(this);
			}
		}
	}
}
