using System;
using System.Collections;
using System.Linq;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using UIStateManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class ExpandedFactionUI : UIComponent
	{
		private enum DraggingMode
		{
			FactionButton = 0,
			FactionShard = 1,
			None = 2
		}

		[SerializeField]
		protected InterfaceStateManager interfaceStateManager;

		public SimpleStateAnimation expandFactionAnimation;

		public ExpandedLandfallFactionsGrid LandfallFactionsGrid;

		public ExpandedCustomFactionGrid m_CustomFactionGrid;

		public Image background;

		public AnimationCurve dropKillCurve;

		public AnimationCurve resurfaceCurve;

		public AnimationCurve shrinkCurve;

		public PlacementFactionBar factionBar;

		public GameObject unitsTriggerGlyphs;

		public UnitWhitelistUI unitWhitelistUI;

		public Transform factionButtonDragParent;

		private RectTransform draggingExpandedFaction;

		private RectTransform draggingFactionShard;

		private DraggingMode draggingMode = DraggingMode.None;

		private GameObject ogDraggingButton;

		private GameObject fillerObject;

		private Faction draggingFaction;

		public bool open;

		public Component placementUI;

		private RectTransform factionBarRect;

		private GameStateManager gamestateManager;

		private GlobalSettingsHandler settingsHandler;

		private bool useExpandedFactionUI;

		private GameModeService gameModeService;

		protected override void Awake()
		{
			base.Awake();
			gamestateManager = ServiceLocator.GetService<GameStateManager>();
			gameModeService = ServiceLocator.GetService<GameModeService>();
		}

		protected override void Start()
		{
			base.Start();
			factionBarRect = factionBar.GetComponent<RectTransform>();
			PlacementFactionBar placementFactionBar = factionBar;
			placementFactionBar.OnFactionBarChanged = (Action)Delegate.Combine(placementFactionBar.OnFactionBarChanged, new Action(UpdateExpandedFactionButtons));
			PopulateFactions();
			Invoke("UpdateExpandedFactionButtons", 0.2f);
			settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
			SettingsInstance settingsInstance = settingsHandler.GetSettingsInstance("UI_INPUT_MODE");
			if (settingsInstance != null)
			{
				useExpandedFactionUI = settingsInstance.currentValue == 1;
			}
			else
			{
				Debug.LogError("UI_INPUT_MODE Settings Instance can not be found!");
			}
		}

		private void PopulateFactions()
		{
			bool flag = false;
			Faction[] array = ContentDatabase.Instance().GetAllFactions().ToArray();
			Faction[] array2 = ContentDatabase.Instance().GetUserFactions().ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsCustom)
				{
					SpawnLandfallFaction(array[i]);
				}
			}
			if (gameModeService != null && gameModeService.CurrentGameMode is OnlineMultiplayerGameMode)
			{
				flag = true;
			}
			if (array2 == null || array2.Length == 0 || flag)
			{
				m_CustomFactionGrid.gameObject.SetActive(value: false);
			}
			else
			{
				m_CustomFactionGrid.gameObject.SetActive(value: true);
				m_CustomFactionGrid.SetupFactions(array2);
			}
			StartCoroutine(SelectFirst());
		}

		private GameObject SpawnLandfallFaction(Faction faction)
		{
			if (!faction.m_displayFaction)
			{
				return null;
			}
			return LandfallFactionsGrid.SpawnFaction(faction, this);
		}

		public void AddFactionShard(Faction faction)
		{
			if (factionBar.IsRoomForMoreShards())
			{
				factionBar.AddFactionShard(faction);
				UpdateExpandedFactionButtons();
			}
		}

		public void RemoveFactionShard(Faction faction)
		{
			factionBar.RemoveFactionShard(faction);
			UpdateExpandedFactionButtons();
		}

		private void UpdateExpandedFactionButtons()
		{
			Faction[] factionsOnBar = factionBar.GetFactionsOnBar();
			if (factionsOnBar != null)
			{
				LandfallFactionsGrid.SetFactionAvailability(factionsOnBar);
				m_CustomFactionGrid.SetFactionAvailability(factionsOnBar);
			}
		}

		public void ExpandedButtonClicked(Faction faction)
		{
			if (factionBar.IsFactionOnBar(faction))
			{
				RemoveFactionShard(faction);
			}
			else
			{
				AddFactionShard(faction);
			}
		}

		public bool BlockExpandedFactionUIOpen()
		{
			bool flag = gamestateManager.GameState == Landfall.TABS.GameState.GameState.PlacementState;
			bool isInFreeLook = gameModeService.CurrentGameMode.IsInFreeLook;
			if (unitWhitelistUI != null)
			{
				return false;
			}
			return !flag || !useExpandedFactionUI || isInFreeLook;
		}

		public void ToggleUI()
		{
			if (!BlockExpandedFactionUIOpen())
			{
				if (!open)
				{
					OpenUI();
				}
				else
				{
					CloseUI();
				}
			}
		}

		private void OpenUI()
		{
			if (!BlockExpandedFactionUIOpen())
			{
				stateManager.OpenUIComponent(this);
			}
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			expandFactionAnimation.SetState(SimpleStateAnimation.State.State01);
			open = true;
			if ((bool)background)
			{
				background.raycastTarget = true;
			}
			StartCoroutine(SelectFirst());
			unitsTriggerGlyphs.SetActive(value: false);
		}

		private IEnumerator SelectFirst()
		{
			if (!open)
			{
				yield break;
			}
			yield return null;
			Selectable[] componentsInChildren = LandfallFactionsGrid.GetComponentsInChildren<Selectable>();
			foreach (Selectable selectable in componentsInChildren)
			{
				if (selectable != null && selectable.gameObject.activeInHierarchy)
				{
					selectable.Select();
					EventSystem.current.SetSelectedGameObject(selectable.gameObject);
					break;
				}
			}
		}

		public void ForceOpenState(bool state)
		{
			if (!BlockExpandedFactionUIOpen())
			{
				open = state;
			}
		}

		private void CloseUI()
		{
			PlacementUI placementUI = this.placementUI as PlacementUI;
			if ((bool)placementUI)
			{
				stateManager.OpenUIComponent(placementUI);
			}
		}

		protected override void OnClose()
		{
			base.OnOpen();
			expandFactionAnimation.SetState(SimpleStateAnimation.State.State02);
			open = false;
			background.raycastTarget = false;
			EventSystem.current.SetSelectedGameObject(null);
			unitsTriggerGlyphs.SetActive(value: true);
		}

		protected override void Update()
		{
			base.Update();
			if ((bool)background)
			{
				float b = (open ? 0.87f : 0f);
				Color color = background.color;
				color.a = Mathf.Lerp(color.a, b, Time.unscaledDeltaTime * 10f);
				background.color = color;
			}
			if (draggingMode == DraggingMode.FactionButton)
			{
				if ((bool)draggingExpandedFaction)
				{
					if (factionBarRect.rect.Contains(factionBarRect.InverseTransformPoint(Input.mousePosition)) && factionBar.IsRoomForMoreShards())
					{
						TransformIntoFactionShard();
						return;
					}
					draggingExpandedFaction.position = Vector2.Lerp(draggingExpandedFaction.position, Input.mousePosition, Time.unscaledDeltaTime * 30f);
					if (Input.GetMouseButtonUp(0))
					{
						DropExpanded();
					}
				}
			}
			else if (draggingMode == DraggingMode.FactionShard && (bool)draggingFactionShard)
			{
				Vector2 vector = Input.mousePosition;
				vector.y = factionBar.GetFactionButtonY();
				Vector2 vector2 = Vector2.Lerp(draggingFactionShard.position, vector, Time.unscaledDeltaTime * 20f);
				if (!factionBarRect.rect.Contains(factionBarRect.InverseTransformPoint(Input.mousePosition)) && Vector2.Distance(vector2, Input.mousePosition) > 45f)
				{
					Debug.Log("TRANSFORM FROM SHARD TO BUTTON");
					TransformIntoFactionButton();
				}
				draggingFactionShard.position = vector2;
				factionBar.AddForce(vector, 1f);
				if (Input.GetMouseButtonUp(0))
				{
					DropExpanded();
					PlaceNewShard();
				}
			}
			if (gamestateManager.GameState != Landfall.TABS.GameState.GameState.PlacementState)
			{
				return;
			}
			PlayerActions instance = PlayerActions.Instance;
			if (open && (unitWhitelistUI == null || !unitWhitelistUI.canCycleUnits))
			{
				if (instance.m_pageRight.WasPressed)
				{
					m_CustomFactionGrid.IncreasePage(1);
					StartCoroutine(SelectFirst());
				}
				if (instance.m_pageLeft.WasPressed)
				{
					m_CustomFactionGrid.IncreasePage(-1);
					StartCoroutine(SelectFirst());
				}
			}
		}

		private void PlaceNewShard()
		{
			StartCoroutine(DelayedPlaceShard());
		}

		private IEnumerator DelayedPlaceShard()
		{
			Transform followTransform = factionBar.GetNewFollowTransform();
			int indexForPos = factionBar.GetIndexForPos(draggingFactionShard.position);
			followTransform.SetSiblingIndex(indexForPos);
			yield return null;
			FactionButton component = draggingFactionShard.GetComponent<FactionButton>();
			component.SetFollowTransform(followTransform);
			component.SetupPhlerp();
			component.StopDragMode();
			factionBar.RegisterFactionButton(component);
		}

		private void TransformIntoFactionShard()
		{
			draggingMode = DraggingMode.FactionShard;
			draggingExpandedFaction.gameObject.SetActive(value: false);
			IPlacementUI placementUI = (IPlacementUI)this.placementUI;
			FactionButton component = UnityEngine.Object.Instantiate(factionBar.prefab, factionBar.RealObjectParent).GetComponent<FactionButton>();
			component.SetupFaction(new FactionButton.FactionButtonData(draggingFaction, placementUI, unlocked: true, 0, null), this);
			component.SetDraggingMode();
			draggingFactionShard = component.GetComponent<RectTransform>();
			draggingFactionShard.position = draggingExpandedFaction.position;
		}

		private void TransformIntoFactionButton()
		{
			draggingMode = DraggingMode.FactionButton;
			if (draggingExpandedFaction == null)
			{
				GameObject gameObject = SpawnLandfallFaction(draggingFaction);
				gameObject.transform.parent = factionButtonDragParent;
				gameObject.GetComponent<ExpandedFactionButton>().SetupDragClone();
				gameObject.transform.localScale = Vector3.one * shrinkCurve.keys[shrinkCurve.keys.Length - 1].value;
				draggingExpandedFaction = gameObject.GetComponent<RectTransform>();
			}
			draggingExpandedFaction.gameObject.SetActive(value: true);
			draggingExpandedFaction.transform.position = draggingFactionShard.position;
			draggingFactionShard.gameObject.SetActive(value: false);
		}

		private void DropExpanded()
		{
			if ((bool)draggingExpandedFaction)
			{
				DropKill(draggingExpandedFaction.gameObject);
				UnityEngine.Object.Destroy(fillerObject);
				if ((bool)ogDraggingButton)
				{
					ogDraggingButton.SetActive(value: true);
					StartCoroutine(ResurfaceAnimation(ogDraggingButton));
				}
			}
			draggingFaction = null;
			draggingExpandedFaction = null;
			draggingMode = DraggingMode.None;
		}

		private void DropShard()
		{
			if ((bool)draggingFactionShard)
			{
				UnityEngine.Object.Destroy(draggingFactionShard.gameObject);
			}
			draggingFactionShard = null;
		}

		public void BeginDragging(ExpandedFactionButton button)
		{
			draggingFaction = button.GetFaction();
			draggingMode = DraggingMode.FactionButton;
			GameObject gameObject = new GameObject("filler");
			gameObject.transform.parent = button.transform.parent;
			gameObject.transform.SetSiblingIndex(button.transform.GetSiblingIndex());
			Image image = gameObject.AddComponent<Image>();
			if ((bool)image)
			{
				image.enabled = false;
			}
			else
			{
				gameObject.GetComponent<Image>().enabled = false;
			}
			MonoBehaviour monoBehaviour = GetComponentInParent<IPlacementUI>() as MonoBehaviour;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(button.gameObject, monoBehaviour.transform);
			gameObject2.GetComponentInChildren<LocalizeText>().Localized = !draggingFaction.IsCustom;
			UnityEngine.Object.Destroy(gameObject2.GetComponent<ExpandedFactionButton>().SetupDragClone());
			RectTransform component = gameObject2.GetComponent<RectTransform>();
			component.sizeDelta = Vector2.one * button.size;
			component.position = button.transform.position;
			draggingExpandedFaction = component;
			button.gameObject.SetActive(value: false);
			fillerObject = gameObject;
			ogDraggingButton = button.gameObject;
			StartCoroutine(ShrinkAnimation(gameObject2));
		}

		public void BeginDraggingShard(FactionButton factionButton)
		{
			draggingFaction = factionButton.GetFaction();
			draggingMode = DraggingMode.FactionShard;
			factionButton.StopPhlerp();
			factionButton.SetDraggingMode();
			factionBar.UnregisterButton(factionButton);
			factionButton.transform.SetAsLastSibling();
			factionButton.DestoryTarget();
			draggingFactionShard = factionButton.GetComponent<RectTransform>();
		}

		internal void DropKill(GameObject objectToKill)
		{
			StartCoroutine(DropKillAnimation(objectToKill));
		}

		private IEnumerator DropKillAnimation(GameObject objectToKill)
		{
			float startScale = base.transform.localScale.x;
			float length = dropKillCurve.keys[dropKillCurve.length - 1].time;
			float timer = 0f;
			while (timer < length)
			{
				yield return null;
				timer += Time.unscaledDeltaTime;
				float num = dropKillCurve.Evaluate(timer);
				objectToKill.transform.localScale = num * startScale * Vector3.one;
			}
			UnityEngine.Object.Destroy(objectToKill);
		}

		private IEnumerator ResurfaceAnimation(GameObject objectToResurface)
		{
			objectToResurface.transform.localScale = Vector3.zero;
			float length = resurfaceCurve.keys[resurfaceCurve.length - 1].time;
			float timer = 0f;
			while (timer < length)
			{
				yield return null;
				timer += Time.unscaledDeltaTime;
				float num = resurfaceCurve.Evaluate(timer);
				objectToResurface.transform.localScale = Vector3.one * num;
			}
		}

		private IEnumerator ShrinkAnimation(GameObject objectToShrink)
		{
			float startScale = base.transform.localScale.x;
			float length = shrinkCurve.keys[shrinkCurve.length - 1].time;
			float timer = 0f;
			while (timer < length)
			{
				yield return null;
				timer += Time.unscaledDeltaTime;
				float num = shrinkCurve.Evaluate(timer);
				objectToShrink.transform.localScale = num * startScale * Vector3.one;
			}
		}

		public void AttemtFactionShardDrag(FactionButton button)
		{
			if (open && !button.m_factionButtonData.expandButton)
			{
				BeginDraggingShard(button);
			}
		}

		public void RegisterAddFactionButtonCallback(Action<FactionButton> action)
		{
			factionBar.RegisterAddFactionButtonCallback(action);
		}
	}
}
