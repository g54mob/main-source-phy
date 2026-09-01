using System;
using System.Reflection;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.UI.UIGroups.Attributes;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Landfall.TABS_Input;
using TFBGames;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.UI.Widgets.Fields
{
	public class UIReferenceField : UIPropertyField, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, ISubmitHandler, IDeselectHandler, IPointerClickHandler
	{
		[SerializeField]
		private LocalizeText m_referenceLabel;

		[SerializeField]
		private Image m_UnitIcon;

		private const int INPUT_BUFFER = 5;

		private Type m_referenceType;

		private TeamLock m_teamLock = TeamLock.Allow_All;

		private Team m_owningTeam;

		private UIPointerEvents m_pickReferenceButton;

		private Type m_originalBrushType;

		private RuntimeReference m_pickedRuntimeReference;

		private TeamSystem m_teamSystem;

		private bool m_isInPickMode;

		private ReferenceRenderer m_referenceRenderer;

		private GameStateManager m_gameStateService;

		private WinConditionsComponent m_WinConditionsComponent;

		private Button m_EditButton;

		private Button m_BackButton;

		private PlayerActions m_PlayerActions;

		private UnitPlacementBrush m_PlacementBrush;

		private bool m_BlockGamepadInput;

		private int m_BlockTriggeredOnFrame;

		private Vector3 m_OriginalCursorObjectScale;

		private Vector3 m_OriginalCursorFillRectScale;

		protected override void Awake()
		{
			m_pickReferenceButton = base.transform.Find("ValueArea").GetComponent<UIPointerEvents>();
			m_referenceLabel.LocaleID = "LABEL_EMPTY";
			m_WinConditionsComponent = UnityEngine.Object.FindObjectOfType<WinConditionsComponent>();
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			m_referenceRenderer = UnityEngine.Object.FindObjectOfType<ReferenceRenderer>();
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			currentGameMode.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Combine(currentGameMode.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnUnitRemoved));
			m_gameStateService = ServiceLocator.GetService<GameStateManager>();
			m_PlayerActions = PlayerActions.Instance;
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			if (service != null)
			{
				m_PlacementBrush = service.CurrentGameMode?.Brush;
			}
			UpdateUI();
		}

		public void SetButtonReference(Button edit, Button back)
		{
			m_EditButton = edit;
			if (m_EditButton != null)
			{
				m_EditButton.onClick.RemoveAllListeners();
				m_EditButton.onClick.AddListener(OnEditButtonClicked);
			}
			m_BackButton = back;
			if (m_BackButton != null)
			{
				m_BackButton.onClick.RemoveAllListeners();
				m_BackButton.onClick.AddListener(OnBackButtonClicked);
			}
		}

		private void OnUnitRemoved(Unit unit)
		{
			if (unit.RuntimeReference == null)
			{
				return;
			}
			_ = unit.RuntimeReference.Guid;
			if (!(unit.RuntimeReference.Guid == Guid.Empty) && m_pickedRuntimeReference != null)
			{
				_ = m_pickedRuntimeReference.Guid;
				if (!(m_pickedRuntimeReference.Guid == Guid.Empty) && unit.RuntimeReference.Guid == m_pickedRuntimeReference.Guid)
				{
					m_pickedRuntimeReference = null;
					base.PropertyField.SetValue(base.PropertyOwner, null);
					UpdateUI();
				}
			}
		}

		public void OnDestroy()
		{
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			if (!(service == null))
			{
				BaseGameMode currentGameMode = service.CurrentGameMode;
				currentGameMode.OnUnitRemovedCallback = (BaseGameMode.OnUnitRemovedDelegate)Delegate.Remove(currentGameMode.OnUnitRemovedCallback, new BaseGameMode.OnUnitRemovedDelegate(OnUnitRemoved));
			}
		}

		public void Update()
		{
			if (m_gameStateService.GameState == Landfall.TABS.GameState.GameState.BattleState && m_isInPickMode)
			{
				RestoreBrush();
				m_isInPickMode = false;
			}
			if (m_BlockGamepadInput)
			{
				if (Time.frameCount >= m_BlockTriggeredOnFrame + 5)
				{
					m_BlockGamepadInput = false;
				}
			}
			else
			{
				HandleGamepadInput();
			}
		}

		private void HandleGamepadInput()
		{
			if ((m_PlayerActions.m_removeUnit.WasPressed || m_PlayerActions.m_back.WasPressed) && m_isInPickMode)
			{
				OnBackButtonClicked();
			}
			if (m_PlayerActions.m_EditVictoryConditions.WasPressed && !m_isInPickMode && m_WinConditionsComponent.VictoryConditionsPanelIsOpen)
			{
				OnEditButtonClicked();
			}
		}

		private void OnEditButtonClicked()
		{
			if (!m_isInPickMode || m_WinConditionsComponent.VictoryConditionsPanelIsOpen)
			{
				m_WinConditionsComponent.UpdateBackButtons();
				OnClickedReferenceButton();
				TriggerBlockInput();
			}
		}

		private void OnBackButtonClicked()
		{
			if (m_isInPickMode)
			{
				ReferencePickFinished(null);
				TriggerBlockInput();
			}
		}

		private void TriggerBlockInput()
		{
			m_BlockGamepadInput = true;
			m_BlockTriggeredOnFrame = Time.frameCount;
		}

		public void OnHoverEnter()
		{
			if (m_pickedRuntimeReference != null && !(m_pickedRuntimeReference.Guid == Guid.Empty) && m_referenceType == typeof(Unit))
			{
				StartHighlightUnit();
			}
		}

		public void OnHoverExit()
		{
			if (m_pickedRuntimeReference != null && !(m_pickedRuntimeReference.Guid == Guid.Empty) && m_referenceType == typeof(Unit))
			{
				EndHighlightUnit();
			}
		}

		private void StartHighlightUnit()
		{
			foreach (Unit allUnit in m_teamSystem.GetAllUnits())
			{
				if (allUnit.RuntimeReference != null)
				{
					_ = allUnit.RuntimeReference.Guid;
					if (allUnit.RuntimeReference.Guid == m_pickedRuntimeReference.Guid)
					{
						allUnit.SetHighlight(Color.white);
					}
				}
			}
		}

		private void EndHighlightUnit()
		{
			foreach (Unit allUnit in m_teamSystem.GetAllUnits())
			{
				if (allUnit.RuntimeReference != null)
				{
					_ = allUnit.RuntimeReference.Guid;
					if (allUnit.RuntimeReference.Guid == m_pickedRuntimeReference.Guid)
					{
						allUnit.RemoveHighlight();
					}
				}
			}
		}

		public void OnClickedReferenceButton()
		{
			SwitchToReferenceBrush();
			if (m_WinConditionsComponent != null)
			{
				m_WinConditionsComponent.HideForUnitSelect();
			}
		}

		private void SwitchToReferenceBrush()
		{
			m_PlacementBrush.ShouldUpdate(update: true);
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			m_originalBrushType = service.CurrentGameMode.Brush.BrushBehaviour.GetType();
			BrushBehaviourReferencePicker obj = (BrushBehaviourReferencePicker)m_PlacementBrush.InitializeBrushWithType<BrushBehaviourReferencePicker>();
			m_OriginalCursorObjectScale = m_PlacementBrush.Cursor.CursorObjectScale;
			m_OriginalCursorFillRectScale = m_PlacementBrush.Cursor.FillCircleRendererScale;
			m_PlacementBrush.Cursor.ResetCursorScale();
			obj.InitiatedFromField = this;
			obj.LockToTeam(m_owningTeam, m_teamLock);
			m_isInPickMode = true;
			m_PlacementBrush.BlockBrushRemove(value: true);
			m_PlacementBrush.PreventPlacementBuffer();
		}

		private void RestoreBrush()
		{
			m_PlacementBrush.InitializeBrushWithType(m_originalBrushType);
			m_PlacementBrush.BlockBrushRemove(value: true);
			m_PlacementBrush.ShouldUpdate(update: false);
			m_PlacementBrush.Cursor.ReApplyCursorScale(m_OriginalCursorObjectScale, m_OriginalCursorFillRectScale);
		}

		public void ReferencePickFinished(RuntimeReference reference)
		{
			RestoreBrush();
			if (m_WinConditionsComponent != null)
			{
				m_WinConditionsComponent.ShowAfterUnitSelected();
			}
			if (reference == null)
			{
				m_isInPickMode = false;
				return;
			}
			ReferenceRequest<Unit> referenceRequest = new ReferenceRequest<Unit>(reference.Guid.ToString());
			if (m_pickedRuntimeReference != null)
			{
				ReferenceRequest<Unit> referenceRequest2 = (ReferenceRequest<Unit>)m_pickedRuntimeReference;
				if (m_referenceRenderer != null)
				{
					m_referenceRenderer.RemoveIconedUnit(referenceRequest2);
				}
				referenceRequest2.Release();
			}
			m_pickedRuntimeReference = referenceRequest;
			UpdateUI();
			base.PropertyField.SetValue(base.PropertyOwner, referenceRequest);
			m_isInPickMode = false;
			if (m_referenceRenderer != null)
			{
				m_referenceRenderer.AddIconedUnit(referenceRequest);
			}
		}

		private void UpdateUI()
		{
			_ = ServiceLocator.GetService<GameModeService>().CurrentGameMode.TeamLayouts;
			if (m_pickedRuntimeReference == null)
			{
				m_referenceLabel.LocaleID = "LABEL_EMPTY";
				return;
			}
			Unit referenceTarget = ServiceLocator.GetService<RuntimeReferenceService>().GetReferenceTarget<Unit>(m_pickedRuntimeReference);
			if (referenceTarget == null)
			{
				return;
			}
			m_referenceLabel.LocaleID = referenceTarget.unitBlueprint.Name;
			if (!(m_UnitIcon != null))
			{
				return;
			}
			referenceTarget.unitBlueprint.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
			{
				if (sprite != null && m_UnitIcon != null)
				{
					m_UnitIcon.sprite = sprite;
				}
			});
		}

		public void SetReferenceType<T>()
		{
			SetReferenceType(typeof(T));
		}

		public void LockToTeam(Team owningTeam, TeamLock teamLock)
		{
			m_teamLock = teamLock;
			m_owningTeam = owningTeam;
		}

		public void SetReferenceType(Type type)
		{
			m_referenceType = type;
		}

		public override void BindObject(object propertyOwner, FieldInfo propertyField)
		{
			base.PropertyOwner = propertyOwner;
			base.PropertyField = propertyField;
			object value = base.PropertyField.GetValue(base.PropertyOwner);
			m_pickedRuntimeReference = (RuntimeReference)value;
			UpdateUI();
		}

		public override void SetValue(string value)
		{
			m_referenceLabel.LocaleID = value;
		}

		public override void SetCallback(UnityAction<string> call)
		{
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			OnHoverEnter();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			OnHoverExit();
		}

		public void OnSelect(BaseEventData eventData)
		{
			OnHoverEnter();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			OnClickedReferenceButton();
		}

		public void OnDeselect(BaseEventData eventData)
		{
			OnHoverExit();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnClickedReferenceButton();
		}
	}
}
