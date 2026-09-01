using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class MultiSelectTool : Tool
	{
		private class State
		{
		}

		private class SelectionState : State
		{
			public List<DMEditorComponent> hoveredObjects;

			public bool selectObjects;

			public bool deselectObjects;

			public SelectionState(MultiSelectTool multiSelectTool)
			{
				multiSelectTool.UpdateContextInfo();
			}

			public void TearDown(MultiSelectTool multiSelectTool)
			{
				foreach (DMEditorComponent hoveredObject in multiSelectTool.GetHoveredObjects(selectionRadius))
				{
					if (!multiSelectTool.m_selectedObjects.Contains(hoveredObject))
					{
						multiSelectTool.EnableHighlight(hoveredObject.gameObject, enabled: false);
					}
				}
				DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.None);
			}

			private void LoadTemplate(MultiSelectTool multiSelectTool, string filePath)
			{
				DMIOWrapper.File.ReadAllBytes(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception e)
				{
					Level level = LevelSerializer.Deserialize(Utility.Unzip(bytes));
					Vector3 targetPosition = multiSelectTool.GetTargetPosition();
					foreach (EntityTreeNode item in LevelUtil.BuildEntityTrees(level.scene.flatEntities))
					{
						Utility.SnapTransform? snapTransform = Utility.GetSnapTransform(item.entity.position + targetPosition, Utility.SnapDistance.Unlimited);
						if (snapTransform.HasValue)
						{
							DMEditor.Instance.InstantiateEditorObject(item, DMEditor.Instance.LevelRootObject, animatedSpawn: true, snapTransform.Value.position, snapTransform.Value.slope);
						}
					}
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
				});
			}

			private void SaveTemplate(MultiSelectTool multiSelectTool, string filePath)
			{
				if (multiSelectTool.m_selectedObjects.Count == 0)
				{
					Debug.LogWarning("Nothing to save in template.");
				}
				List<Level.FlatEntity> list = new List<Level.FlatEntity>();
				Vector3 zero = Vector3.zero;
				if (multiSelectTool.m_selectedObjects.Count > 0)
				{
					foreach (DMEditorComponent selectedObject in multiSelectTool.m_selectedObjects)
					{
						zero += selectedObject.Position;
					}
					zero /= (float)multiSelectTool.m_selectedObjects.Count;
					foreach (DMEditorComponent selectedObject2 in multiSelectTool.m_selectedObjects)
					{
						Level.FlatEntity item = new Level.FlatEntity
						{
							entity = selectedObject2.entity.Clone(),
							parentGuid = Guid.Empty
						};
						item.entity.position -= zero;
						list.Add(item);
						LevelUtil.AddChildEntities(list, selectedObject2.entity.guid, selectedObject2.gameObject);
					}
				}
				DMIOWrapper.File.WriteAllBytes(filePath, Utility.Zip(LevelSerializer.Serialize(new Level
				{
					settings = null,
					scene = new Level.Scene
					{
						flatEntities = list
					},
					volume = null
				})), FileHandlingFileType.CustomContentOrLocalStorageFile, delegate
				{
					Debug.Log("Saved template to " + filePath);
				});
			}

			public void OnUpdate(MultiSelectTool multiSelectTool)
			{
				List<DMEditorComponent> list = multiSelectTool.GetHoveredObjects(selectionRadius);
				if (list != hoveredObjects)
				{
					if (hoveredObjects != null)
					{
						foreach (DMEditorComponent hoveredObject in hoveredObjects)
						{
							if ((bool)hoveredObject && !multiSelectTool.m_selectedObjects.Contains(hoveredObject) && !list.Contains(hoveredObject))
							{
								multiSelectTool.EnableHighlight(hoveredObject.gameObject, enabled: false);
							}
						}
					}
					hoveredObjects = list;
					if (hoveredObjects != null)
					{
						foreach (DMEditorComponent hoveredObject2 in hoveredObjects)
						{
							if ((bool)hoveredObject2 && !multiSelectTool.m_selectedObjects.Contains(hoveredObject2))
							{
								multiSelectTool.EnableHighlight(hoveredObject2.gameObject, enabled: true);
							}
						}
					}
				}
				if (selectObjects)
				{
					DMEditor.Instance.EnableSphereEmission(enabled: true);
					SelectObjects(multiSelectTool);
				}
				else if (deselectObjects)
				{
					DMEditor.Instance.EnableSphereEmission(enabled: true);
					DeselectObjects(multiSelectTool);
				}
				else
				{
					DMEditor.Instance.EnableSphereEmission(enabled: false);
				}
			}

			private void SelectObjects(MultiSelectTool multiSelectTool)
			{
				if (hoveredObjects == null)
				{
					return;
				}
				foreach (DMEditorComponent hoveredObject in hoveredObjects)
				{
					if ((bool)hoveredObject)
					{
						if (!multiSelectTool.m_selectedObjects.Contains(hoveredObject))
						{
							multiSelectTool.m_selectedObjects.Add(hoveredObject);
						}
						multiSelectTool.EnableHighlight(hoveredObject.gameObject, enabled: true);
					}
				}
			}

			private void DeselectObjects(MultiSelectTool multiSelectTool)
			{
				if (hoveredObjects == null)
				{
					return;
				}
				foreach (DMEditorComponent hoveredObject in hoveredObjects)
				{
					if ((bool)hoveredObject && multiSelectTool.m_selectedObjects.Contains(hoveredObject))
					{
						multiSelectTool.m_selectedObjects.Remove(hoveredObject);
						multiSelectTool.EnableHighlight(hoveredObject.gameObject, enabled: false, Color.white);
					}
				}
			}
		}

		private class AdjustObjectsAwaitingState : State
		{
			public void DuplicateSelectedObjects(MultiSelectTool multiSelectTool)
			{
				for (int i = 0; i < multiSelectTool.m_selectedObjects.Count; i++)
				{
					DMEditorComponent dMEditorComponent = multiSelectTool.m_selectedObjects[i];
					if (!(dMEditorComponent == null))
					{
						multiSelectTool.EnableHighlight(dMEditorComponent.gameObject, enabled: false);
						DMEditorComponent dMEditorComponent2 = DMEditor.Instance.InstantiateEditorObject(LevelUtil.BuildEntityTree(dMEditorComponent), DMEditor.Instance.LevelRootObject, animatedSpawn: false, null, null);
						multiSelectTool.EnableHighlight(dMEditorComponent2.gameObject, enabled: true);
						multiSelectTool.m_selectedObjects[i] = dMEditorComponent2;
					}
				}
			}
		}

		private class AdjustObjectsState : State
		{
			public struct SelectedObject
			{
				public DMEditorComponent previewObject;

				public Vector3 offset;

				public Quaternion startSlope;

				public EntityTransformation startTransformation;

				public bool hasValidPosition;
			}

			private Plane m_movementPlane;

			private float m_yawRotation;

			public List<SelectedObject> selectedObjects = new List<SelectedObject>();

			private readonly UnityAction undoAction;

			private InputState m_inputState;

			public AdjustObjectsState(MultiSelectTool multiSelectTool, List<DMEditorComponent> selectedObjects)
			{
				AdjustObjectsState adjustObjectsState = this;
				Vector3 targetPosition = multiSelectTool.GetTargetPosition();
				m_movementPlane = new Plane(Vector3.up, targetPosition);
				foreach (DMEditorComponent selectedObject in selectedObjects)
				{
					if (selectedObject == null)
					{
						Debug.LogWarning("Found null in selected object list");
						continue;
					}
					DMEditor.Instance.MoveToPreview(selectedObject);
					this.selectedObjects.Add(new SelectedObject
					{
						previewObject = selectedObject,
						offset = selectedObject.Position - targetPosition,
						startSlope = selectedObject.Slope,
						startTransformation = selectedObject.GetGlobalEntityTransform(),
						hasValidPosition = true
					});
				}
				undoAction = delegate
				{
					adjustObjectsState.OnCancel(multiSelectTool);
				};
				if (DMEditor.Instance != null)
				{
					DMEditor.Instance.undo.AddListener(undoAction);
				}
				else
				{
					Debug.LogError("DMEditor.Instance should not be null!");
				}
				multiSelectTool.PlayPlaceSound();
				m_inputState = new InputState("MultiSelectTool.AdjustObjectState");
				m_inputState.AddOnKeyDownListener(PlayerActions.Instance.m_toolRotateLeft, delegate
				{
					multiSelectTool.OnRotateLeft();
				});
				m_inputState.AddOnKeyDownListener(PlayerActions.Instance.m_toolRotateRight, delegate
				{
					multiSelectTool.OnRotateRight();
				});
				m_inputState.AddOnKeyDownListener(PlayerActions.Instance.m_enterExitBattle, delegate
				{
				});
				InputManager.PushState(m_inputState);
				multiSelectTool.UpdateContextInfo();
			}

			public void TearDown(MultiSelectTool multiSelectTool)
			{
				foreach (SelectedObject selectedObject in selectedObjects)
				{
					if (!(selectedObject.previewObject == null))
					{
						DMEditorComponent previewObject = selectedObject.previewObject;
						previewObject.Slope = selectedObject.startSlope;
						previewObject.Position = selectedObject.startTransformation.position;
						previewObject.AdditionalRotation = selectedObject.startTransformation.rotation;
						previewObject.Scale = selectedObject.startTransformation.scale;
						previewObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
						previewObject.gameObject.SetActive(value: true);
						if (DMEditor.Instance != null)
						{
							DMEditor.Instance.MoveToLevel(previewObject);
						}
						else
						{
							Debug.LogError("DMEditor.Instance should not be null!");
						}
					}
				}
				if (DMEditor.Instance != null)
				{
					DMEditor.Instance.undo.RemoveListener(undoAction);
				}
				else
				{
					Debug.LogError("DMEditor.Instance should not be null!");
				}
				if (multiSelectTool != null)
				{
					multiSelectTool.PlayPlaceSound();
				}
				InputManager.RemoveState(m_inputState);
			}

			public void OnUpdate(MultiSelectTool multiSelectTool)
			{
				InputHoldUpdate();
				Quaternion quaternion = Quaternion.Euler(0f, m_yawRotation, 0f);
				for (int i = 0; i < selectedObjects.Count; i++)
				{
					Ray ray = new Ray(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward);
					if (m_movementPlane.Raycast(ray, out var enter))
					{
						Vector3 position = ray.GetPoint(enter) + quaternion * selectedObjects[i].offset;
						selectedObjects[i].previewObject.Position = position;
						selectedObjects[i].previewObject.AdditionalRotation = quaternion * selectedObjects[i].startTransformation.rotation;
						selectedObjects[i].previewObject.Slope = Quaternion.identity;
						bool flag = Utility.SnapObjectAt(selectedObjects[i].previewObject, selectedObjects[i].previewObject.Position, DMEditorComponent.TeleportMode.TeleportAll, Utility.SnapDistance.Unlimited);
						if (flag != selectedObjects[i].hasValidPosition)
						{
							selectedObjects[i] = new SelectedObject
							{
								previewObject = selectedObjects[i].previewObject,
								offset = selectedObjects[i].offset,
								startSlope = selectedObjects[i].startSlope,
								startTransformation = selectedObjects[i].startTransformation,
								hasValidPosition = flag
							};
							selectedObjects[i].previewObject.gameObject.SetActive(flag);
						}
					}
				}
			}

			private void InputHoldUpdate()
			{
				PlayerActions instance = PlayerActions.Instance;
				bool flag = InputManager.ShouldPollInvokePlayerAction(instance.m_toolRotateLeft);
				bool num = InputManager.ShouldPollInvokePlayerAction(instance.m_toolRotateRight);
				if (flag)
				{
					Rotate(-60f * Time.deltaTime);
				}
				if (num)
				{
					Rotate(60f * Time.deltaTime);
				}
			}

			public void OnCancel(MultiSelectTool multiSelectTool)
			{
				TearDown(multiSelectTool);
				multiSelectTool.m_currentState = new AdjustObjectsAwaitingState();
			}

			public void Rotate(float delta)
			{
				m_yawRotation += delta;
			}
		}

		[SerializeField]
		private Color m_selectedColor;

		[SerializeField]
		private Sprite m_selectIcon;

		[SerializeField]
		private Sprite m_deselectIcon;

		[SerializeField]
		private Sprite m_moveIcon;

		[SerializeField]
		private Sprite m_placeIcon;

		[SerializeField]
		private Sprite m_duplicateIcon;

		[SerializeField]
		private Sprite m_rotatePositiveIcon;

		[SerializeField]
		private Sprite m_rotateNegativeIcon;

		private State m_currentState;

		private static float selectionRadius = 4f;

		private List<DMEditorComponent> m_selectedObjects = new List<DMEditorComponent>();

		protected override void Start()
		{
			base.Start();
			DMEditor.Instance.SetVisualObjectSphereRadius(selectionRadius);
			m_currentState = new SelectionState(this);
			UpdateContextInfo();
		}

		private void Update()
		{
			State currentState = m_currentState;
			if (currentState == null)
			{
				return;
			}
			if (!(currentState is SelectionState selectionState))
			{
				if (currentState is AdjustObjectsState adjustObjectsState)
				{
					adjustObjectsState.OnUpdate(this);
				}
			}
			else
			{
				selectionState.OnUpdate(this);
			}
		}

		private void SwitchToSelectionState()
		{
			m_currentState = new SelectionState(this);
		}

		private void SwitchToAwaitingState(SelectionState s)
		{
			if (m_selectedObjects != null)
			{
				s.TearDown(this);
				m_currentState = new AdjustObjectsAwaitingState();
				UpdateContextInfo();
			}
		}

		private void AdjustObject(AdjustObjectsAwaitingState s)
		{
			if (m_selectedObjects != null)
			{
				if (InputManager.ShiftIsPressed)
				{
					s.DuplicateSelectedObjects(this);
				}
				m_currentState = new AdjustObjectsState(this, m_selectedObjects);
				UpdateContextInfo();
			}
		}

		private void EndAdjustObject(AdjustObjectsState s)
		{
			foreach (AdjustObjectsState.SelectedObject selectedObject in s.selectedObjects)
			{
				if (selectedObject.hasValidPosition)
				{
					DMEditor.Instance.MoveToLevel(selectedObject.previewObject);
				}
				else
				{
					UnityEngine.Object.Destroy(selectedObject.previewObject);
				}
			}
			s.selectedObjects.Clear();
			DMEditor.Instance.ScheduleTakeLevelSnapshot();
			s.TearDown(this);
			m_currentState = new AdjustObjectsAwaitingState();
			UpdateContextInfo();
		}

		public void SetRadius(float radius)
		{
			selectionRadius = radius;
			DMEditor.Instance.SetVisualObjectSphereRadius(selectionRadius);
		}

		public void SetInteractionState(bool goToAdjust)
		{
			State currentState = m_currentState;
			if (currentState != null)
			{
				if (!(currentState is SelectionState selectionState))
				{
					if (!(currentState is AdjustObjectsAwaitingState))
					{
						if (currentState is AdjustObjectsState && !goToAdjust)
						{
							SwitchToSelectionState();
						}
					}
					else if (!goToAdjust)
					{
						SwitchToSelectionState();
					}
				}
				else
				{
					SelectionState s = selectionState;
					if (goToAdjust)
					{
						SwitchToAwaitingState(s);
					}
				}
			}
			UpdateContextInfo();
		}

		private void TearDownCurrentState()
		{
			State currentState = m_currentState;
			if (currentState == null)
			{
				return;
			}
			if (!(currentState is SelectionState selectionState))
			{
				if (currentState is AdjustObjectsState adjustObjectsState)
				{
					adjustObjectsState.TearDown(this);
				}
			}
			else
			{
				selectionState.TearDown(this);
			}
		}

		private void OnToolPrimary()
		{
			State currentState = m_currentState;
			if (currentState == null)
			{
				return;
			}
			if (!(currentState is SelectionState selectionState))
			{
				if (!(currentState is AdjustObjectsAwaitingState adjustObjectsAwaitingState))
				{
					if (currentState is AdjustObjectsState adjustObjectsState)
					{
						AdjustObjectsState s = adjustObjectsState;
						EndAdjustObject(s);
					}
				}
				else
				{
					AdjustObjectsAwaitingState s2 = adjustObjectsAwaitingState;
					AdjustObject(s2);
				}
			}
			else
			{
				selectionState.selectObjects = true;
			}
		}

		private void OnToolPrimaryEnd()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is SelectionState selectionState)
			{
				selectionState.selectObjects = false;
			}
		}

		private void OnToolSecondary()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is SelectionState selectionState)
			{
				selectionState.deselectObjects = true;
			}
		}

		private void OnToolSecondaryEnd()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is SelectionState selectionState)
			{
				selectionState.deselectObjects = false;
			}
		}

		private void OnToolSpecial1()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is AdjustObjectsAwaitingState adjustObjectsAwaitingState)
			{
				AdjustObjectsAwaitingState adjustObjectsAwaitingState2 = adjustObjectsAwaitingState;
				adjustObjectsAwaitingState2.DuplicateSelectedObjects(this);
				AdjustObject(adjustObjectsAwaitingState2);
			}
		}

		private void OnRotateLeft()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is AdjustObjectsState adjustObjectsState)
			{
				adjustObjectsState.Rotate(-10f);
			}
		}

		private void OnRotateRight()
		{
			State currentState = m_currentState;
			if (currentState != null && currentState is AdjustObjectsState adjustObjectsState)
			{
				adjustObjectsState.Rotate(10f);
			}
		}

		private void UpdateContextInfo()
		{
			State currentState = m_currentState;
			if (currentState == null)
			{
				return;
			}
			if (!(currentState is SelectionState))
			{
				if (!(currentState is AdjustObjectsAwaitingState))
				{
					if (currentState is AdjustObjectsState)
					{
						DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.HandClosed);
						DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
						DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_placeIcon);
						DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolRotateLeft, m_rotateNegativeIcon);
						DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolRotateRight, m_rotatePositiveIcon);
					}
				}
				else
				{
					DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Hand);
					DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
					DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_moveIcon);
					DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSpecial1, m_duplicateIcon);
				}
			}
			else
			{
				DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Sphere);
				DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
				DMEditor.Instance.SetVisualObjectSphereRadius(selectionRadius);
				DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_selectIcon);
				DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSecondary, m_deselectIcon);
			}
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				OnToolPrimary();
			});
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				OnToolPrimaryEnd();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				OnToolSecondary();
			});
			m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
			{
				OnToolSecondaryEnd();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial1, delegate
			{
				OnToolSpecial1();
			});
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			TearDownCurrentState();
			foreach (DMEditorComponent selectedObject in m_selectedObjects)
			{
				if (selectedObject != null)
				{
					EnableHighlight(selectedObject.gameObject, enabled: false, Color.white);
				}
			}
		}

		public Vector3 GetTargetPosition()
		{
			DMEditor instance = DMEditor.Instance;
			return Utility.GetTargetPositionOnVolume(instance.playerCamera.transform.position, instance.playerCamera.transform.forward, instance.rayDistance);
		}

		private List<DMEditorComponent> GetHoveredObjects(float radius = 0.1f)
		{
			return DMEditor.Instance.GetRootObjectsInSphere(GetTargetPosition(), radius);
		}

		private void EnableHighlight(GameObject gameObject, bool enabled, Color? constantColor = null)
		{
			Utility.SetHighlightObject(gameObject, enabled, Color.white, constantColor ?? m_selectedColor);
		}

		private void PlayPlaceSound()
		{
			Utility.PlaySound("UI/Unit Placed", 1f, base.transform.position);
		}
	}
}
