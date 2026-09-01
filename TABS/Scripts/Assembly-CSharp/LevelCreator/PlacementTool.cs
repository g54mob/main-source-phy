using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace LevelCreator
{
	public class PlacementTool : Tool
	{
		public class State
		{
			public virtual void TearDown()
			{
			}

			public virtual void OnUpdate()
			{
			}

			public virtual void OnCancel()
			{
			}

			public virtual void OnRadialMenu()
			{
			}

			public virtual void OnPickupObject()
			{
			}

			public virtual void OnDropObject()
			{
			}

			public virtual void OnDeleteObject()
			{
			}

			public virtual void OnScroll(float scrollDelta)
			{
			}

			public virtual void OnScale(float scrollDelta)
			{
			}

			public virtual void OnRotate(float scrollDelta)
			{
			}

			public virtual void OnAdjustObject()
			{
			}

			public virtual void OnFinishAdjustObject()
			{
			}

			public virtual void OnInputModifierChanged(bool pressed)
			{
			}
		}

		private class NoActionState : State
		{
			private PlacementTool m_placementTool;

			private DMEditorComponent m_hoveredObject;

			public NoActionState(PlacementTool placementTool)
			{
				m_placementTool = placementTool;
				placementTool.SetInputState(placementTool.m_noActionInputState);
				placementTool.m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
				if (placementGridManager == null)
				{
					placementGridManager = Object.Instantiate(placementTool.m_gridPrefab, placementTool.m_dmEditor.gridCanvas);
					List<GridItem> gridItems = new List<GridItem>();
					placementTool.m_dmEditor.editorObjectTable.ForEachRow(delegate(string key, DMEditorObjectRow editorObj)
					{
						editorObj.Key = key;
						gridItems.Add(new GridItem
						{
							Id = key,
							Path = editorObj.RadialMenuPath,
							DisplayName = placementTool.m_dmEditor.editorObjectTable.GetDisplayName(key),
							Tooltip = "",
							Icon = editorObj.Thumbnail,
							Tint = Color.white,
							NormalizedSize = editorObj.NormalizedSize
						});
					});
					placementGridManager.SetGridData(gridItems, "LC_ITEMGRID_PROPS_AND_DECORATIONS");
					DMUIManager.Instance.BindPanel(placementGridManager, DMUIManager.UIPanels.ItemBrowser);
				}
				placementTool.m_onItemSelected = delegate(string id)
				{
					if (!(placementTool == null))
					{
						placementTool.SetPrefab(id);
						placementTool.UpdateHotbar(id);
					}
				};
				placementGridManager.onItemSelected.AddListener(placementTool.m_onItemSelected);
				if (hotbar != null)
				{
					hotbar.EnableHotbar(m_placementTool.m_noActionInputState);
				}
			}

			public override void TearDown()
			{
				if (m_hoveredObject != null)
				{
					Utility.SetHighlightObject(m_hoveredObject.gameObject, highlight: false);
					m_placementTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
				}
				m_placementTool.m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.None);
				placementGridManager.onItemSelected.RemoveListener(m_placementTool.m_onItemSelected);
			}

			public override void OnUpdate()
			{
				DMEditorComponent hoveredObject = m_placementTool.GetHoveredObject();
				if (!(hoveredObject != m_hoveredObject))
				{
					return;
				}
				if ((bool)m_hoveredObject)
				{
					Utility.SetHighlightObject(m_hoveredObject.gameObject, highlight: false);
					m_placementTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
					m_placementTool.m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
				}
				m_hoveredObject = hoveredObject;
				if ((bool)m_hoveredObject)
				{
					Utility.SetHighlightObject(m_hoveredObject.gameObject, highlight: true);
					m_placementTool.m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Hand);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_placementTool.m_grabItemIcon);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSecondary, m_placementTool.m_removeItemIcon);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSpecial2, m_placementTool.m_moveItemIcon);
					if (m_hoveredObject.GetComponent<TriggerBox>() != null || m_hoveredObject.GetComponent<ITriggerable>() != null)
					{
						m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolConnectTriggers, m_placementTool.m_connectTriggersIcon);
					}
				}
			}

			public override void OnRadialMenu()
			{
				if (!(DMUIManager.Instance.currentPanel != null) && !placementGridManager.Building)
				{
					m_placementTool.currentState = new RadialMenuState(m_placementTool, this);
				}
			}

			public override void OnPickupObject()
			{
				if (m_hoveredObject == null)
				{
					MessageDisplay.DisplayMessage("LC_GRAB_AN_OBJECT");
					return;
				}
				TearDown();
				if (m_placementTool.m_modifierPressed)
				{
					Utility.SetHighlightObject(m_hoveredObject.gameObject, highlight: false);
					DMEditorComponent dMEditorComponent = m_placementTool.m_dmEditor.InstantiateEditorObject(LevelUtil.BuildEntityTree(m_hoveredObject), m_placementTool.m_dmEditor.LevelRootObject, animatedSpawn: false, null, null);
					Utility.SetHighlightObject(dMEditorComponent.gameObject, highlight: true);
					m_placementTool.m_dmEditor.MoveToPreview(dMEditorComponent);
					m_placementTool.currentState = new PlaceObjectState(m_placementTool, dMEditorComponent, objectWasPickedUp: false);
				}
				else
				{
					m_placementTool.currentState = new PlaceObjectState(m_placementTool, m_hoveredObject, objectWasPickedUp: true);
				}
			}

			public override void OnDeleteObject()
			{
				if (!(m_hoveredObject == null))
				{
					m_hoveredObject.gameObject.SetActive(value: false);
					Object.Destroy(m_hoveredObject.gameObject);
					m_hoveredObject = null;
					m_placementTool.m_dmEditor.ScheduleTakeLevelSnapshot();
					m_placementTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
					m_placementTool.m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
					m_placementTool.PlayRemoveSound();
				}
			}

			public override void OnAdjustObject()
			{
				if (!(m_hoveredObject == null))
				{
					TearDown();
					m_placementTool.currentState = new AdjustObjectState(m_placementTool, m_hoveredObject);
				}
			}
		}

		private class PlaceObjectState : State
		{
			private PlacementTool m_placementTool;

			private Quaternion m_parentRotation = Quaternion.identity;

			private Quaternion m_entitySlope;

			private EntityTransformation m_entityTransformation;

			private Vector3 m_initialPosition;

			private DMEditorComponent m_previewObject;

			private DMEditorComponent m_hologramObject;

			private readonly UnityAction m_undoAction;

			private readonly bool m_objectWasPickedUp;

			private bool m_hasValidPosition;

			private void UpdatePosition(TargetInfo targetInfo)
			{
				if (!m_placementTool.m_adjustHeight)
				{
					DMEditorComponent dMEditorComponent = null;
					if (targetInfo.gameObject != null)
					{
						dMEditorComponent = targetInfo.gameObject.GetComponentInParent<DMEditorComponent>();
					}
					m_parentRotation = ((dMEditorComponent != null) ? dMEditorComponent.GetGlobalEntityTransform().rotation : Quaternion.identity);
					m_entityTransformation.position = targetInfo.position;
					m_entitySlope = Quaternion.FromToRotation(Vector3.up, Quaternion.Inverse(m_parentRotation) * targetInfo.normal);
					return;
				}
				Plane plane = new Plane(Vector3.up, m_initialPosition);
				Ray ray = new Ray(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward);
				if (plane.Raycast(ray, out var enter))
				{
					Vector3 point = ray.GetPoint(enter);
					Vector3 b = plane.ClosestPointOnPlane(m_entityTransformation.position);
					Vector3 a = plane.ClosestPointOnPlane(DMEditor.Instance.playerCamera.transform.position);
					float num = Vector3.Distance(a, b);
					float num2 = Vector3.Distance(a, point) - num;
					Bounds bounds = Utility.GetBounds(m_previewObject.transform);
					float heightOffset = Mathf.Lerp(0f - bounds.extents.y, bounds.extents.y, num2 / 10f);
					m_entityTransformation.heightOffset = heightOffset;
					m_entitySlope = m_previewObject.entity.slope;
				}
			}

			private Quaternion GetFinalLocalRotation()
			{
				Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, m_parentRotation * m_previewObject.CalculateFinalSlope(m_entitySlope) * Vector3.up);
				return Quaternion.Inverse(m_parentRotation * m_previewObject.CalculateFinalSlope(m_entitySlope)) * quaternion * m_entityTransformation.rotation;
			}

			private void UpdatePreviewObjects()
			{
				Quaternion identity = Quaternion.identity;
				Quaternion quaternion = m_parentRotation * m_previewObject.CalculateFinalSlope(m_entitySlope);
				EntityTransformation previewTransformation = new EntityTransformation
				{
					position = m_entityTransformation.position,
					rotation = quaternion * GetFinalLocalRotation(),
					scale = m_entityTransformation.scale,
					heightOffset = m_entityTransformation.heightOffset
				};
				m_placementTool.SetTransformation(m_previewObject, null, identity, previewTransformation);
				m_placementTool.SetTransformation(m_hologramObject, null, identity, previewTransformation);
				m_placementTool.SendDistanceToPreviewShader(m_previewObject, m_hologramObject, m_hasValidPosition);
			}

			public PlaceObjectState(PlacementTool placementTool, DMEditorComponent previewObject, bool objectWasPickedUp)
			{
				m_placementTool = placementTool;
				m_objectWasPickedUp = objectWasPickedUp;
				m_previewObject = previewObject;
				placementTool.m_dmEditor.MoveToPreview(previewObject);
				Utility.SetHighlightObject(previewObject.gameObject, highlight: true);
				m_entityTransformation = new EntityTransformation
				{
					position = previewObject.Position,
					rotation = previewObject.AdditionalRotation,
					scale = previewObject.Scale,
					heightOffset = previewObject.HeightOffset
				};
				m_initialPosition = previewObject.Position;
				m_hologramObject = placementTool.InstantiateHologram(previewObject);
				placementTool.SetInputState(placementTool.m_placeObjectInputState);
				m_undoAction = delegate
				{
					OnCancel();
				};
				placementTool.m_dmEditor.undo.AddListener(m_undoAction);
				placementTool.PlayPlaceSound();
				OnInputModifierChanged(pressed: false);
				hotbar.AssignInput(m_placementTool.m_placeObjectInputState);
				m_placementTool.BuildToolUI();
			}

			public override void TearDown()
			{
				if (m_previewObject != null)
				{
					Object.Destroy(m_previewObject.gameObject);
					m_previewObject = null;
				}
				if (m_hologramObject != null)
				{
					Object.Destroy(m_hologramObject.gameObject);
					m_hologramObject = null;
				}
				if (m_placementTool != null && m_placementTool.m_dmEditor != null && m_placementTool.m_dmEditor.undo != null)
				{
					m_placementTool.m_dmEditor.undo.RemoveListener(m_undoAction);
				}
				if (DMEditor.Instance != null && DMEditor.Instance.toolControlsBuilder != null)
				{
					DMEditor.Instance.toolControlsBuilder.ClearUI();
				}
			}

			public override void OnUpdate()
			{
				TargetInfo targetInfo = m_placementTool.GetTargetInfo();
				m_hasValidPosition = targetInfo.hit;
				UpdatePosition(targetInfo);
				UpdatePreviewObjects();
				InputHoldUpdate();
			}

			private void InputHoldUpdate()
			{
				PlayerActions instance = PlayerActions.Instance;
				bool flag = InputManager.ShouldPollInvokePlayerAction(instance.m_toolRotateLeft);
				bool flag2 = InputManager.ShouldPollInvokePlayerAction(instance.m_toolRotateRight);
				bool flag3 = InputManager.ShouldPollInvokePlayerAction(instance.m_toolScaleUp);
				bool num = InputManager.ShouldPollInvokePlayerAction(instance.m_toolScaleDown);
				if (flag)
				{
					OnRotate(-5f * Time.deltaTime);
				}
				if (flag2)
				{
					OnRotate(5f * Time.deltaTime);
				}
				if (flag3)
				{
					OnScale(-0.1f * Time.deltaTime);
				}
				if (num)
				{
					OnScale(0.1f * Time.deltaTime);
				}
			}

			public override void OnInputModifierChanged(bool pressed)
			{
				m_placementTool.m_modifierPressed = pressed;
				PlayerActions instance = PlayerActions.Instance;
				if (pressed)
				{
					m_placementTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys(displayInputStateActions: false);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(instance.m_toolPrimary, m_placementTool.m_newItemIcon);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(instance.m_toolScaleUp, m_placementTool.m_scaleNegativeIcon);
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(instance.m_toolScaleDown, m_placementTool.m_scalePositiveIcon);
				}
				else
				{
					m_placementTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
					m_placementTool.m_dmEditor.contextInfoMenu.AddContextKey(instance.m_scaleModifier, m_placementTool.m_moreActionsIcon);
				}
			}

			public override void OnDropObject()
			{
				TargetInfo targetInfo = m_placementTool.GetTargetInfo();
				if (targetInfo.hit)
				{
					UpdatePosition(targetInfo);
					EntityTransformation previewTransformation = new EntityTransformation
					{
						position = m_entityTransformation.position,
						rotation = m_parentRotation * m_previewObject.CalculateFinalSlope(m_entitySlope) * GetFinalLocalRotation(),
						scale = m_entityTransformation.scale,
						heightOffset = m_entityTransformation.heightOffset
					};
					DMEditorComponent dMEditorComponent = m_placementTool.m_dmEditor.InstantiateEditorObject(LevelUtil.BuildEntityTree(m_previewObject), m_placementTool.m_dmEditor.LevelRootObject, animatedSpawn: false, null, null);
					m_placementTool.SetTransformation(dMEditorComponent, targetInfo.gameObject, m_entitySlope, previewTransformation);
					CopyTriggerConnections(m_previewObject, dMEditorComponent);
					if (m_objectWasPickedUp && !m_placementTool.m_modifierPressed)
					{
						Object.Destroy(m_hologramObject.gameObject);
						m_hologramObject = null;
						Object.Destroy(m_previewObject.gameObject);
						m_previewObject = null;
						DMEditor.Instance.toolControlsBuilder.ClearUI();
						m_placementTool.currentState = new NoActionState(m_placementTool);
						m_placementTool.m_dmEditor.undo.RemoveListener(m_undoAction);
					}
					m_placementTool.m_dmEditor.ScheduleTakeLevelSnapshot();
					m_placementTool.PlayPlaceSound();
				}
			}

			private void CopyTriggerConnections(DMEditorComponent original, DMEditorComponent copy)
			{
				TriggerBox component = original.GetComponent<TriggerBox>();
				if (component != null)
				{
					copy.GetComponent<TriggerBox>().SetConnections(component.GetConnectionsCopy());
				}
				if (original.GetComponent<ITriggerable>() == null)
				{
					return;
				}
				TriggerBox[] array = Object.FindObjectsOfType<TriggerBox>();
				foreach (TriggerBox obj in array)
				{
					List<DMEditorComponent> newConnections = new List<DMEditorComponent>();
					obj.ForEachConnection(delegate(DMEditorComponent c)
					{
						if (c == original)
						{
							newConnections.Add(copy);
						}
						else
						{
							newConnections.Add(c);
						}
					});
					obj.SetConnections(newConnections);
				}
			}

			public override void OnDeleteObject()
			{
				TearDown();
				if (m_placementTool != null)
				{
					m_placementTool.currentState = new NoActionState(m_placementTool);
					m_placementTool.PlayRemoveSound();
				}
			}

			public override void OnScale(float delta)
			{
				float num = 0.15f * Mathf.Abs(delta);
				float num2 = 0.101f;
				float num3 = 39.6f;
				m_entityTransformation.scale = Vector3.Min(Vector3.Max(m_entityTransformation.scale * ((Mathf.Sign(delta) < 0f) ? (1f - num) : (1f + num)), new Vector3(num2, num2, num2)), new Vector3(num3, num3, num3));
				UpdatePreviewObjects();
			}

			public override void OnRotate(float delta)
			{
				if (m_placementTool.m_modifierPressed)
				{
					OnScale(delta);
					return;
				}
				m_entityTransformation.rotation *= Quaternion.Euler(0f, delta * 15f, 0f);
				UpdatePreviewObjects();
			}

			public override void OnRadialMenu()
			{
				if (!placementGridManager.Building)
				{
					TearDown();
					m_placementTool.currentState = new NoActionState(m_placementTool);
					m_placementTool.currentState.OnRadialMenu();
				}
			}
		}

		private class RadialMenuState : State
		{
			private PlacementTool m_placementTool;

			private State m_previousState;

			private readonly InputState m_previousInputState;

			public RadialMenuState(PlacementTool placementTool, State previousState)
			{
				m_placementTool = placementTool;
				m_previousState = previousState;
				m_previousInputState = placementTool.m_currentInputState;
				EnableRadialState();
			}

			public override void OnUpdate()
			{
				if (!placementGridManager.Showing && m_placementTool.m_currentInputState == m_placementTool.m_radialMenuInputState)
				{
					m_placementTool.currentState = new NoActionState(m_placementTool);
				}
			}

			public override void TearDown()
			{
				DisableRadialState(isTearDown: true);
				if (m_previousState != null)
				{
					m_previousState.TearDown();
				}
			}

			public override void OnRadialMenu()
			{
				DisableRadialState(isTearDown: false);
				m_placementTool.currentState = m_previousState;
			}

			private void EnableRadialState()
			{
				m_placementTool.SetInputState(m_placementTool.m_radialMenuInputState);
				DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.ItemBrowser);
			}

			private void DisableRadialState(bool isTearDown)
			{
				if (!isTearDown)
				{
					DMUIManager.Instance.PopPanel();
				}
				m_placementTool.SetInputState(m_previousInputState);
			}
		}

		private class AdjustObjectState : State
		{
			private PlacementTool placementTool;

			private Plane movementPlane;

			private DMEditorComponent previewObject;

			private Vector3 offset;

			private Quaternion startSlope;

			private EntityTransformation startTransformation;

			private bool hasValidPosition = true;

			private DMEditorComponent hologramObject;

			private readonly UnityAction undoAction;

			public AdjustObjectState(PlacementTool placementTool, DMEditorComponent requestedPreviewObject)
			{
				this.placementTool = placementTool;
				Vector3 targetPosition = placementTool.GetTargetPosition();
				movementPlane = new Plane(Vector3.up, targetPosition);
				DMEditorComponent rootEditorObject = Utility.GetRootEditorObject(requestedPreviewObject);
				previewObject = rootEditorObject;
				offset = previewObject.Position - targetPosition;
				startSlope = previewObject.Slope;
				startTransformation = previewObject.GetGlobalEntityTransform();
				placementTool.m_dmEditor.MoveToPreview(previewObject);
				hologramObject = placementTool.InstantiateHologram(previewObject);
				Utility.SetHighlightObject(previewObject.gameObject, highlight: true);
				placementTool.SetInputState(placementTool.m_adjustObjectInputState);
				undoAction = delegate
				{
					OnCancel();
				};
				placementTool.m_dmEditor.undo.AddListener(undoAction);
				placementTool.PlayPlaceSound();
			}

			public override void TearDown()
			{
				if ((bool)previewObject)
				{
					Object.Destroy(previewObject.gameObject);
					previewObject = null;
				}
				if ((bool)hologramObject)
				{
					Object.Destroy(hologramObject.gameObject);
					hologramObject = null;
				}
				placementTool.m_dmEditor.undo.RemoveListener(undoAction);
			}

			public override void OnCancel()
			{
				previewObject.Slope = startSlope;
				previewObject.Position = startTransformation.position;
				previewObject.AdditionalRotation = startTransformation.rotation;
				previewObject.Scale = startTransformation.scale;
				previewObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				previewObject.gameObject.SetActive(value: true);
				placementTool.m_dmEditor.MoveToLevel(previewObject);
				Utility.SetHighlightObject(previewObject.gameObject, highlight: false);
				previewObject = null;
				placementTool.PlayRemoveSound();
				TearDown();
				placementTool.currentState = new NoActionState(placementTool);
			}

			public override void OnDeleteObject()
			{
				placementTool.m_dmEditor.ScheduleTakeLevelSnapshot();
				placementTool.PlayRemoveSound();
				TearDown();
				placementTool.currentState = new NoActionState(placementTool);
			}

			public override void OnUpdate()
			{
				Ray ray = new Ray(placementTool.m_dmEditor.playerCamera.transform.position, placementTool.m_dmEditor.playerCamera.transform.forward);
				if (movementPlane.Raycast(ray, out var enter))
				{
					Vector3 vector = ray.GetPoint(enter) + offset;
					if (Utility.SnapObjectAt(previewObject, vector, DMEditorComponent.TeleportMode.TeleportAll, Utility.SnapDistance.Unlimited))
					{
						hasValidPosition = true;
						previewObject.gameObject.SetActive(value: true);
					}
					else
					{
						hasValidPosition = false;
						previewObject.gameObject.SetActive(value: false);
					}
					Vector3 position = ((Vector3.Distance(previewObject.Position, vector) < 0.2f) ? previewObject.Position : vector);
					hologramObject.Position = position;
					hologramObject.Slope = previewObject.Slope;
					hologramObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				}
				placementTool.SendDistanceToPreviewShader(previewObject, hologramObject, hasValidPosition);
			}

			public override void OnScroll(float scrollDelta)
			{
				if (placementTool.m_modifierPressed)
				{
					placementTool.Scale(previewObject, hologramObject, scrollDelta * 0.05f);
				}
				else
				{
					placementTool.Rotate(previewObject, hologramObject, Quaternion.Euler(0f, scrollDelta * 4f, 0f));
				}
			}

			public override void OnFinishAdjustObject()
			{
				if (hasValidPosition)
				{
					Utility.SetHighlightObject(previewObject.gameObject, highlight: false);
					placementTool.m_dmEditor.MoveToLevel(previewObject);
					previewObject = null;
				}
				placementTool.PlayPlaceSound();
				TearDown();
				placementTool.currentState = new NoActionState(placementTool);
				placementTool.m_dmEditor.ScheduleTakeLevelSnapshot();
			}
		}

		private InputState m_noActionInputState = new InputState("PlacementTool.NoAction");

		private InputState m_placeObjectInputState = new InputState("PlacementTool.PlaceObject");

		private InputState m_adjustObjectInputState = new InputState("PlacementTool.AdjustObject");

		private InputState m_radialMenuInputState = new InputState("PlacementTool.RadialMenu");

		private InputState m_currentInputState;

		private static readonly State emptyState = new State();

		private DMEditor m_dmEditor;

		public State currentState = emptyState;

		[SerializeField]
		private Grid m_gridPrefab;

		private static Grid placementGridManager;

		private UnityAction<string> m_onItemSelected;

		[SerializeField]
		private Hotbar m_hotbarPrefab;

		private static Hotbar hotbar;

		[SerializeField]
		private Material m_transparentObjectPreviewMaterial;

		[SerializeField]
		private GameObject keyhintPrefab;

		[SerializeField]
		private GameObject m_keyhintPrefab;

		[SerializeField]
		private Sprite m_newItemIcon;

		[SerializeField]
		private Sprite m_placeItemIcon;

		[SerializeField]
		private Sprite m_grabItemIcon;

		[SerializeField]
		private Sprite m_moveItemIcon;

		[SerializeField]
		private Sprite m_removeItemIcon;

		[SerializeField]
		private Sprite m_rotateNegativeIcon;

		[SerializeField]
		private Sprite m_rotatePositiveIcon;

		[SerializeField]
		private Sprite m_scaleNegativeIcon;

		[SerializeField]
		private Sprite m_scalePositiveIcon;

		[SerializeField]
		private Sprite m_connectTriggersIcon;

		[SerializeField]
		private Sprite m_moreActionsIcon;

		private bool m_adjustHeight;

		private bool m_modifierPressed;

		protected override void Start()
		{
			base.Start();
			m_dmEditor = DMEditor.Instance;
			currentState = new NoActionState(this);
			if (!(hotbar == null))
			{
				return;
			}
			hotbar = Object.Instantiate(m_hotbarPrefab, m_dmEditor.toolBar.transform);
			hotbar.gameObject.name = "Hotbar_PlacementObjects";
			GameObject keyHint = Object.Instantiate(keyhintPrefab, hotbar.transform);
			keyHint.AddComponent<EnabledByInputMode>().inputType = InputType.Keyboard;
			keyHint.transform.position += Vector3.down * 0.114f * Screen.height;
			GameObject controllerHint = Object.Instantiate(keyhintPrefab, hotbar.transform);
			controllerHint.AddComponent<EnabledByInputMode>().inputType = InputType.Controller;
			controllerHint.transform.position += Vector3.down * 0.114f * Screen.height;
			PlayerActions.Instance.OnLastInputTypeChanged += delegate(BindingSourceType type)
			{
				OnLastInputTypeChanged(type, keyHint, controllerHint);
			};
			SceneManager.sceneLoaded += delegate
			{
				PlayerActions.Instance.OnLastInputTypeChanged -= delegate(BindingSourceType type)
				{
					OnLastInputTypeChanged(type, keyHint, controllerHint);
				};
			};
			OnLastInputTypeChanged(BindingSourceType.None, keyHint, controllerHint);
			GridCategory currentCategory = placementGridManager.GetCurrentCategory();
			List<HotbarItem> list = new List<HotbarItem>();
			foreach (KeyValuePair<string, GridGroup> group in currentCategory.Groups)
			{
				foreach (KeyValuePair<string, GridItem> item in group.Value.Items)
				{
					list.Add(new HotbarItem
					{
						icon = item.Value.Icon,
						callback = delegate
						{
							SetPrefab(item.Value.Id);
						},
						normalizedSize = item.Value.NormalizedSize,
						temp_id = item.Value.Id
					});
				}
			}
			hotbar.SetData(list);
			hotbar.EnableHotbar(m_noActionInputState);
		}

		private void OnLastInputTypeChanged(BindingSourceType obj, GameObject keyHint, GameObject controllerHint)
		{
			if (keyHint != null)
			{
				keyHint.GetComponentInChildren<TextMeshProUGUI>().text = ServiceLocator.GetService<GlyphService>().GetActionGlyph(PlayerActions.Instance.m_invokeHotbar, InputType.Keyboard);
			}
			if (controllerHint != null)
			{
				controllerHint.GetComponentInChildren<TextMeshProUGUI>().text = ServiceLocator.GetService<GlyphService>().GetActionGlyph(PlayerActions.Instance.m_invokeHotbar, InputType.Controller);
			}
		}

		private void Update()
		{
			currentState.OnUpdate();
		}

		private void SetHotbarData(List<DMEditorObjectRow> category, string objectTypeId)
		{
			List<HotbarItem> list = new List<HotbarItem>();
			int index = 0;
			for (int i = 0; i < category.Count; i++)
			{
				DMEditorObjectRow item = category[i];
				list.Add(new HotbarItem
				{
					icon = item.Thumbnail,
					callback = delegate
					{
						m_onItemSelected(item.Key);
					},
					normalizedSize = item.NormalizedSize,
					temp_id = item.Key
				});
				if (item.Key == objectTypeId)
				{
					index = i;
				}
			}
			hotbar.SetData(list, index);
		}

		public void SetPrefab(string objectTypeId)
		{
			currentState.TearDown();
			currentState = new PlaceObjectState(this, m_dmEditor.InstantiateEditorObject(objectTypeId, GetTargetPosition(), Quaternion.identity, Quaternion.identity, base.gameObject, animatedSpawn: true), objectWasPickedUp: false);
		}

		private void UpdateHotbar(string objectTypeId)
		{
			List<DMEditorObjectRow> categoryObjectsRows = m_dmEditor.editorObjectTable.GetCategoryObjectsRows(objectTypeId, excludeInputObjectID: false);
			SetHotbarData(categoryObjectsRows, objectTypeId);
		}

		private void OpenTriggerBoxTool()
		{
			TargetInfo targetInfo = GetTargetInfo();
			if (targetInfo.gameObject != null && (targetInfo.gameObject.GetComponentInParent<TriggerBox>() != null || targetInfo.gameObject.GetComponentInParent<ITriggerable>() != null))
			{
				ToolTableRow rowValue = DMEditor.Instance.toolTable.GetRowValue("e94da58f-2481-44c6-be13-8e6fad5c0c5d");
				DMEditor.Instance.SwitchAction(rowValue);
				DMEditor.Instance.toolBar.Hide();
			}
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_noActionInputState.AddOnKeyDownListener(actions.m_openGrid, delegate
			{
				currentState.OnRadialMenu();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				currentState.OnPickupObject();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				currentState.OnDeleteObject();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolSpecial1, delegate
			{
				SetPrefab(hotbar.CurrentTempId());
			}, m_newItemIcon);
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolSpecial2, delegate
			{
				currentState.OnAdjustObject();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolConnectTriggers, delegate
			{
				OpenTriggerBoxTool();
			});
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_openGrid, delegate
			{
				currentState.OnRadialMenu();
			});
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				currentState.OnDropObject();
			}, m_placeItemIcon);
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				currentState.OnDeleteObject();
			}, m_removeItemIcon);
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_cycleHotbarCategoryLeft, delegate
			{
				currentState.OnRotate(-1f);
			}, m_rotatePositiveIcon);
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_cycleHotbarCategoryRight, delegate
			{
				currentState.OnRotate(1f);
			}, m_rotateNegativeIcon);
			m_placeObjectInputState.AddOnKeyDownListener(actions.m_scaleModifier, delegate
			{
				currentState.OnInputModifierChanged(pressed: true);
			});
			m_placeObjectInputState.AddOnKeyUpListener(actions.m_scaleModifier, delegate
			{
				currentState.OnInputModifierChanged(pressed: false);
			});
			m_placeObjectInputState.AddOnKeyUpListener(actions.m_cycleHotbarLeft, delegate
			{
				SetPrefab(hotbar.CurrentTempId());
			});
			m_placeObjectInputState.AddOnKeyUpListener(actions.m_cycleHotbarRight, delegate
			{
				SetPrefab(hotbar.CurrentTempId());
			});
			m_radialMenuInputState.AddOnKeyDownListener(actions.m_openGrid, delegate
			{
				currentState.OnRadialMenu();
			});
			m_adjustObjectInputState.AddOnKeyUpListener(actions.m_toolSpecial2, delegate
			{
				currentState.OnFinishAdjustObject();
			}, m_placeItemIcon);
			m_adjustObjectInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				currentState.OnDeleteObject();
			}, m_removeItemIcon);
		}

		private void SetInputState(InputState newInputState)
		{
			if (m_currentInputState != newInputState)
			{
				if (m_currentInputState != null)
				{
					InputManager.RemoveState(m_currentInputState);
				}
				m_currentInputState = newInputState;
				if (m_currentInputState != null)
				{
					InputManager.PushState(m_currentInputState);
				}
				m_dmEditor.contextInfoMenu.ReplaceContextKeys();
			}
		}

		public void EnableHeightLock(bool enable)
		{
			m_adjustHeight = enable;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (currentState != null)
			{
				currentState.TearDown();
			}
			if (hotbar != null)
			{
				hotbar.DisableHotbar();
			}
			SetInputState(null);
		}

		public TargetInfo GetTargetInfo()
		{
			return Utility.GetTargetInfo(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
		}

		public Vector3 GetTargetPosition()
		{
			return Utility.GetTargetPosition(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
		}

		private DMEditorComponent GetHoveredObject()
		{
			return Utility.GetObjectInLine(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
		}

		private DMEditorComponent InstantiateHologram(DMEditorComponent editorObject)
		{
			DMEditorComponent dMEditorComponent = m_dmEditor.InstantiateEditorObject(editorObject.ObjectTypeId, editorObject, m_dmEditor.Preview, animatedSpawn: false);
			Object.Destroy(dMEditorComponent.GetComponentInChildren<Collider>());
			m_dmEditor.MoveToPreview(dMEditorComponent);
			Renderer[] componentsInChildren = dMEditorComponent.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer is ParticleSystemRenderer))
				{
					Material[] sharedMaterials = renderer.sharedMaterials;
					for (int j = 0; j < sharedMaterials.Length; j++)
					{
						sharedMaterials[j] = m_transparentObjectPreviewMaterial;
					}
					renderer.sharedMaterials = sharedMaterials;
				}
			}
			return dMEditorComponent;
		}

		private void SetTransformation(DMEditorComponent entityObject, GameObject parentGameObject, Quaternion previewSlope, EntityTransformation previewTransformation)
		{
			entityObject.Slope = previewSlope;
			entityObject.AdditionalRotation = previewTransformation.rotation;
			entityObject.Position = previewTransformation.position;
			entityObject.Scale = previewTransformation.scale;
			entityObject.HeightOffset = previewTransformation.heightOffset;
			if ((bool)parentGameObject)
			{
				m_dmEditor.SetParent(entityObject, parentGameObject);
			}
			entityObject.entity.rotation = Quaternion.Inverse(entityObject.CalculateLocalSlope()) * entityObject.entity.rotation;
			entityObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
		}

		private void Rotate(DMEditorComponent previewObject, DMEditorComponent hologramObject, Quaternion rotation)
		{
			previewObject.AdditionalRotation *= rotation;
			hologramObject.AdditionalRotation = previewObject.AdditionalRotation;
		}

		private void Scale(DMEditorComponent previewObject, DMEditorComponent hologramObject, float scaleMultiplier)
		{
			previewObject.Scale = Vector3.Max(previewObject.Scale + Vector3.one * scaleMultiplier, Vector3.one * 0.1f);
			hologramObject.Scale = previewObject.Scale;
		}

		private void SendDistanceToPreviewShader(DMEditorComponent previewObject, DMEditorComponent hologramObject, bool hasValidPosition)
		{
			float num = Vector3.Distance(previewObject.transform.position, hologramObject.transform.position);
			MeshRenderer componentInChildren = hologramObject.GetComponentInChildren<MeshRenderer>();
			if (componentInChildren != null && componentInChildren.material != null)
			{
				componentInChildren.material.SetFloat("_Distance", hasValidPosition ? num : 10f);
			}
		}

		private void PlayPlaceSound()
		{
			Utility.PlaySound("UI/Unit Placed", 1f, base.transform.position);
		}

		private void PlayRemoveSound()
		{
			Utility.PlaySound("UI/Unit Removed", 1f, base.transform.position);
		}
	}
}
