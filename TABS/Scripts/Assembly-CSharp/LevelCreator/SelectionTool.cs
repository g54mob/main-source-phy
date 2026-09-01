using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class SelectionTool : Tool
	{
		private class State
		{
			public virtual void TearDown()
			{
			}

			public virtual void OnUpdate()
			{
			}
		}

		private class NoActionState : State
		{
			private SelectionTool mSelectionTool;

			private DMEditorComponent mHoveredObject;

			private List<DMEditorComponent> mHoveredObjects;

			public NoActionState(SelectionTool selectionTool, List<DMEditorComponent> hoveredObjects = null)
			{
				mSelectionTool = selectionTool;
				mHoveredObjects = hoveredObjects;
				selectionTool.SetInputState(selectionTool.m_inputState);
				if (mHoveredObjects != null)
				{
					foreach (DMEditorComponent mHoveredObject in mHoveredObjects)
					{
						mSelectionTool.EnableHighlight(mHoveredObject.gameObject, enabled: true);
					}
				}
				DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
			}

			public override void TearDown()
			{
				if (mHoveredObjects != null)
				{
					foreach (DMEditorComponent mHoveredObject in mHoveredObjects)
					{
						mSelectionTool.EnableHighlight(mHoveredObject.gameObject, enabled: false);
					}
				}
				if ((bool)this.mHoveredObject)
				{
					Utility.SetHighlightObject(this.mHoveredObject.gameObject, highlight: false);
					DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
				}
				DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.None);
			}

			public override void OnUpdate()
			{
				DMEditorComponent hoveredObject = GetHoveredObject();
				if (hoveredObject != mHoveredObject)
				{
					if ((bool)mHoveredObject)
					{
						Utility.SetHighlightObject(mHoveredObject.gameObject, highlight: false);
						DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
						DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
					}
					mHoveredObject = hoveredObject;
					if ((bool)mHoveredObject)
					{
						Utility.SetHighlightObject(mHoveredObject.gameObject, highlight: true);
						DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Hand);
						DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, "Grab or Move (+Shift)");
						DMEditor.Instance.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSecondary, "Remove");
					}
				}
			}

			public void PickupObject()
			{
				if (!(mHoveredObject == null))
				{
					TearDown();
					if (InputManager.AltIsPressed)
					{
						Utility.SetHighlightObject(mHoveredObject.gameObject, highlight: false);
						DMEditorComponent dMEditorComponent = DMEditor.Instance.InstantiateEditorObject(LevelUtil.BuildEntityTree(mHoveredObject), DMEditor.Instance.LevelRootObject, animatedSpawn: false, null, null);
						Utility.SetHighlightObject(dMEditorComponent.gameObject, highlight: true);
						DMEditor.Instance.MoveToPreview(dMEditorComponent);
						mSelectionTool.mCurrentState = new PlaceObjectState(mSelectionTool, dMEditorComponent, objectWasPickedUp: false);
					}
					else
					{
						mSelectionTool.mCurrentState = new PlaceObjectState(mSelectionTool, mHoveredObject, objectWasPickedUp: true);
					}
				}
			}

			public void DeleteObject()
			{
				if (!(mHoveredObject == null))
				{
					mHoveredObject.gameObject.SetActive(value: false);
					UnityEngine.Object.Destroy(mHoveredObject.gameObject);
					mHoveredObject = null;
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
					DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
					DMEditor.Instance.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
					mSelectionTool.PlayRemoveSound();
				}
			}

			public void AdjustObject()
			{
				if (!(mHoveredObject == null))
				{
					TearDown();
					mSelectionTool.mCurrentState = new AdjustObjectState(mSelectionTool, mHoveredObject);
				}
			}

			public void StartMultiselect()
			{
				TearDown();
				mSelectionTool.mCurrentState = new MultiSelectState(mSelectionTool);
			}
		}

		private class PlaceObjectState : State
		{
			private SelectionTool mSelectionTool;

			private Quaternion mParentRotation = Quaternion.identity;

			private Quaternion mEntitySlope;

			private EntityTransformation mEntityTransformation;

			private DMEditorComponent mPreviewObject;

			private DMEditorComponent mHologramObject;

			private readonly UnityAction mUndoAction;

			private readonly bool mObjectWasPickedUp;

			private bool mHasValidPosition;

			public PlaceObjectState(SelectionTool selectionTool, DMEditorComponent previewObject, bool objectWasPickedUp)
			{
				mSelectionTool = selectionTool;
				mObjectWasPickedUp = objectWasPickedUp;
				mPreviewObject = previewObject;
				DMEditor.Instance.MoveToPreview(previewObject);
				Utility.SetHighlightObject(previewObject.gameObject, highlight: true);
				mEntityTransformation = new EntityTransformation
				{
					position = previewObject.Position,
					rotation = previewObject.AdditionalRotation,
					scale = previewObject.Scale
				};
				mHologramObject = mSelectionTool.InstantiateHologram(previewObject);
				mSelectionTool.SetInputState(mSelectionTool.mPlaceObjectInputState);
				mUndoAction = delegate
				{
					Cancel();
				};
				DMEditor.Instance.undo.AddListener(mUndoAction);
				mSelectionTool.PlayPlaceSound();
			}

			public override void TearDown()
			{
				UnityEngine.Object.Destroy(mPreviewObject.gameObject);
				mPreviewObject = null;
				UnityEngine.Object.Destroy(mHologramObject.gameObject);
				mHologramObject = null;
				DMEditor.Instance.undo.RemoveListener(mUndoAction);
			}

			public override void OnUpdate()
			{
				TargetInfo targetInfo = mSelectionTool.GetTargetInfo();
				mHasValidPosition = targetInfo.hit;
				UpdatePosition(targetInfo);
				UpdatePreviewObjects();
			}

			private void UpdatePosition(TargetInfo targetInfo)
			{
				if (!mSelectionTool.mAdjustHeight)
				{
					DMEditorComponent dMEditorComponent = null;
					if (targetInfo.gameObject != null)
					{
						dMEditorComponent = targetInfo.gameObject.GetComponentInParent<DMEditorComponent>();
					}
					mParentRotation = ((dMEditorComponent != null) ? dMEditorComponent.GetGlobalEntityTransform().rotation : Quaternion.identity);
					mEntityTransformation.position = targetInfo.position;
					mEntitySlope = Quaternion.FromToRotation(Vector3.up, Quaternion.Inverse(mParentRotation) * targetInfo.normal);
				}
				else
				{
					Plane plane = new Plane(Vector3.up, mEntityTransformation.position);
					Vector3 b = plane.ClosestPointOnPlane(targetInfo.position);
					Vector3 b2 = plane.ClosestPointOnPlane(mEntityTransformation.position);
					Vector3 a = plane.ClosestPointOnPlane(DMEditor.Instance.playerCamera.transform.position);
					float num = Vector3.Distance(a, b2);
					float num2 = Vector3.Distance(a, b) - num;
					Bounds bounds = Utility.GetBounds(mPreviewObject.transform);
					float num3 = Mathf.Lerp(0f - bounds.extents.y, bounds.extents.y, num2 / 10f);
					mEntityTransformation.position = new Vector3(mEntityTransformation.position.x, targetInfo.position.y + num3, mEntityTransformation.position.z);
					mEntitySlope = mPreviewObject.entity.slope;
				}
			}

			private Quaternion GetFinalLocalRotation()
			{
				Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, mParentRotation * mPreviewObject.CalculateFinalSlope(mEntitySlope) * Vector3.up);
				return Quaternion.Inverse(mParentRotation * mPreviewObject.CalculateFinalSlope(mEntitySlope)) * quaternion * mEntityTransformation.rotation;
			}

			private void UpdatePreviewObjects()
			{
				Quaternion identity = Quaternion.identity;
				Quaternion quaternion = mParentRotation * mPreviewObject.CalculateFinalSlope(mEntitySlope);
				EntityTransformation previewTransformation = new EntityTransformation
				{
					position = mEntityTransformation.position,
					rotation = quaternion * GetFinalLocalRotation(),
					scale = mEntityTransformation.scale
				};
				mSelectionTool.SetTransformation(mPreviewObject, null, identity, previewTransformation);
				mSelectionTool.SetTransformation(mHologramObject, null, identity, previewTransformation);
				mSelectionTool.SendDistanceToPreviewShader(mPreviewObject, mHologramObject, mHasValidPosition);
			}

			public void Cancel()
			{
			}

			public void DropObject()
			{
				TargetInfo targetInfo = mSelectionTool.GetTargetInfo();
				if (targetInfo.hit)
				{
					UpdatePosition(targetInfo);
					EntityTransformation previewTransformation = new EntityTransformation
					{
						position = mEntityTransformation.position,
						rotation = mParentRotation * mPreviewObject.CalculateFinalSlope(mEntitySlope) * GetFinalLocalRotation(),
						scale = mEntityTransformation.scale
					};
					DMEditorComponent entityObject = DMEditor.Instance.InstantiateEditorObject(LevelUtil.BuildEntityTree(mPreviewObject), DMEditor.Instance.LevelRootObject, animatedSpawn: false, null, null);
					mSelectionTool.SetTransformation(entityObject, targetInfo.gameObject, mEntitySlope, previewTransformation);
					if (!InputManager.AltIsPressed)
					{
						UnityEngine.Object.Destroy(mHologramObject.gameObject);
						mHologramObject = null;
						UnityEngine.Object.Destroy(mPreviewObject.gameObject);
						mPreviewObject = null;
						mSelectionTool.mCurrentState = new NoActionState(mSelectionTool);
						DMEditor.Instance.undo.RemoveListener(mUndoAction);
					}
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
					mSelectionTool.PlayPlaceSound();
				}
			}

			public void DeleteObject()
			{
				TearDown();
				mSelectionTool.mCurrentState = new NoActionState(mSelectionTool);
				mSelectionTool.PlayRemoveSound();
			}

			public void Scale(float scrollDelta)
			{
				mEntityTransformation.scale = Vector3.Max(mEntityTransformation.scale + Vector3.one * scrollDelta * 0.01f, Vector3.one * 0.1f);
				UpdatePreviewObjects();
			}

			public void Rotate(float scrollDelta)
			{
				if (InputManager.ShiftIsPressed)
				{
					Scale(scrollDelta * 10f);
					return;
				}
				mEntityTransformation.rotation *= Quaternion.Euler(0f, scrollDelta * 10f, 0f);
				UpdatePreviewObjects();
			}
		}

		private class AdjustObjectState : State
		{
			private SelectionTool mSelectionTool;

			private Plane mMovementPlane;

			private DMEditorComponent mPreviewObject;

			private Vector3 mOffset;

			private Quaternion mStartSlope;

			private EntityTransformation mStartTransformation;

			private bool mHasValidPosition = true;

			private DMEditorComponent mHologramObject;

			private readonly UnityAction mUndoAction;

			public AdjustObjectState(SelectionTool selectionTool, DMEditorComponent requestedPreviewObject)
			{
				mSelectionTool = selectionTool;
				Vector3 targetPosition = selectionTool.GetTargetPosition();
				mMovementPlane = new Plane(Vector3.up, targetPosition);
				DMEditorComponent rootEditorObject = Utility.GetRootEditorObject(requestedPreviewObject);
				mPreviewObject = rootEditorObject;
				mOffset = mPreviewObject.Position - targetPosition;
				mStartSlope = mPreviewObject.Slope;
				mStartTransformation = mPreviewObject.GetGlobalEntityTransform();
				DMEditor.Instance.MoveToPreview(mPreviewObject);
				mHologramObject = mSelectionTool.InstantiateHologram(mPreviewObject);
				Utility.SetHighlightObject(mPreviewObject.gameObject, highlight: true);
				mSelectionTool.SetInputState(mSelectionTool.mAdjustObjectInputState);
				mUndoAction = delegate
				{
					Cancel();
				};
				DMEditor.Instance.undo.AddListener(mUndoAction);
				mSelectionTool.PlayPlaceSound();
			}

			public override void TearDown()
			{
				if ((bool)mPreviewObject)
				{
					UnityEngine.Object.Destroy(mPreviewObject.gameObject);
					mPreviewObject = null;
				}
				if ((bool)mHologramObject)
				{
					UnityEngine.Object.Destroy(mHologramObject.gameObject);
					mHologramObject = null;
				}
				DMEditor.Instance.undo.RemoveListener(mUndoAction);
			}

			public override void OnUpdate()
			{
				Ray ray = new Ray(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward);
				if (mMovementPlane.Raycast(ray, out var enter))
				{
					Vector3 vector = ray.GetPoint(enter) + mOffset;
					if (Utility.SnapObjectAt(mPreviewObject, vector, DMEditorComponent.TeleportMode.TeleportAll, Utility.SnapDistance.Unlimited))
					{
						mHasValidPosition = true;
						mPreviewObject.gameObject.SetActive(value: true);
					}
					else
					{
						mHasValidPosition = false;
						mPreviewObject.gameObject.SetActive(value: false);
					}
					Vector3 position = ((Vector3.Distance(mPreviewObject.Position, vector) < 0.2f) ? mPreviewObject.Position : vector);
					mHologramObject.Position = position;
					mHologramObject.Slope = mPreviewObject.Slope;
					mHologramObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				}
				mSelectionTool.SendDistanceToPreviewShader(mPreviewObject, mHologramObject, mHasValidPosition);
			}

			public void Cancel()
			{
				mPreviewObject.Slope = mStartSlope;
				mPreviewObject.Position = mStartTransformation.position;
				mPreviewObject.AdditionalRotation = mStartTransformation.rotation;
				mPreviewObject.Scale = mStartTransformation.scale;
				mPreviewObject.Teleport(DMEditorComponent.TeleportMode.TeleportAll);
				mPreviewObject.gameObject.SetActive(value: true);
				DMEditor.Instance.MoveToLevel(mPreviewObject);
				Utility.SetHighlightObject(mPreviewObject.gameObject, highlight: false);
				mPreviewObject = null;
				mSelectionTool.PlayRemoveSound();
				TearDown();
				mSelectionTool.mCurrentState = new NoActionState(mSelectionTool);
			}

			public void DeleteObject()
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
				mSelectionTool.PlayRemoveSound();
				TearDown();
				mSelectionTool.mCurrentState = new NoActionState(mSelectionTool);
			}

			public void FinishAdjustObject()
			{
				if (mHasValidPosition)
				{
					Utility.SetHighlightObject(mPreviewObject.gameObject, highlight: false);
					DMEditor.Instance.MoveToLevel(mPreviewObject);
					mPreviewObject = null;
				}
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
				mSelectionTool.PlayPlaceSound();
				TearDown();
				mSelectionTool.mCurrentState = new NoActionState(mSelectionTool);
			}
		}

		private class MultiSelectState : State
		{
			private SelectionTool mSelectionTool;

			private List<DMEditorComponent> mHoveredObjects = new List<DMEditorComponent>();

			private Vector2 mStartPosition;

			private Vector2 mCurrentPosition;

			public MultiSelectState(SelectionTool selectionTool)
			{
				mSelectionTool = selectionTool;
				mStartPosition = Input.mousePosition;
				mCurrentPosition = mStartPosition;
				mSelectionTool.SelectionBox.gameObject.SetActive(value: true);
				DMEditor.Instance.DisableFirstPersonMovement();
			}

			public override void TearDown()
			{
				foreach (DMEditorComponent mHoveredObject in mHoveredObjects)
				{
					mSelectionTool.EnableHighlight(mHoveredObject.gameObject, enabled: false);
				}
				mSelectionTool.SelectionBox.gameObject.SetActive(value: false);
				DMEditor.Instance.EnableFirstPersonMovement();
			}

			public override void OnUpdate()
			{
				UpdateSelectionBox();
				foreach (DMEditorComponent mHoveredObject in mHoveredObjects)
				{
					mSelectionTool.EnableHighlight(mHoveredObject.gameObject, enabled: false);
				}
				mHoveredObjects.Clear();
				Vector2 vector = mSelectionTool.SelectionBox.anchoredPosition - mSelectionTool.SelectionBox.sizeDelta / 2f;
				Vector2 vector2 = mSelectionTool.SelectionBox.anchoredPosition + mSelectionTool.SelectionBox.sizeDelta / 2f;
				Camera playerCamera = DMEditor.Instance.playerCamera;
				foreach (Transform item in DMEditor.Instance.LevelRootObject.transform)
				{
					DMEditorComponent component = item.GetComponent<DMEditorComponent>();
					if (component != null)
					{
						Vector3 vector3 = playerCamera.WorldToScreenPoint(item.position);
						if (vector3.x >= vector.x && vector3.x <= vector2.x && vector3.y >= vector.y && vector3.y <= vector2.y)
						{
							mHoveredObjects.Add(component);
						}
					}
				}
				foreach (DMEditorComponent mHoveredObject2 in mHoveredObjects)
				{
					mSelectionTool.EnableHighlight(mHoveredObject2.gameObject, enabled: true);
				}
			}

			private void UpdateSelectionBox()
			{
				PlayerActions instance = PlayerActions.Instance;
				mCurrentPosition += new Vector2(instance.m_aim.X, instance.m_aim.Y) * 10f;
				Vector2 vector = mCurrentPosition - mStartPosition;
				mSelectionTool.SelectionBox.sizeDelta = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
				mSelectionTool.SelectionBox.anchoredPosition = mStartPosition + new Vector2(vector.x / 2f, vector.y / 2f);
			}

			public void StopMultiselect()
			{
				TearDown();
				mSelectionTool.mCurrentState = new NoActionState(mSelectionTool, mHoveredObjects);
			}
		}

		public Color SelectedColor;

		public Canvas Canvas;

		public RectTransform SelectionBox;

		public Material mTransparentObjectPreviewMaterial;

		private bool mAdjustHeight;

		private InputState mPlaceObjectInputState = new InputState("SelectionTool.PlaceObject");

		private InputState mAdjustObjectInputState = new InputState("SelectionTool.AdjustObject");

		private InputState mMultiSelectState = new InputState("SelectionTool.MultiSelect");

		private InputState mCurrentInputState;

		private static readonly State mEmptyState = new State();

		private State mCurrentState = mEmptyState;

		protected override void Start()
		{
			base.Start();
			mCurrentState = new NoActionState(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			mCurrentState.TearDown();
			SetInputState(null);
		}

		private void Update()
		{
			mCurrentState.OnUpdate();
		}

		private void OnLastInputTypeChanged(BindingSourceType obj, GameObject keyHint, GameObject controllerHint)
		{
			keyHint.GetComponentInChildren<TextMeshProUGUI>().text = ServiceLocator.GetService<GlyphService>().GetActionGlyph(PlayerActions.Instance.m_invokeHotbar, InputType.Keyboard);
			controllerHint.GetComponentInChildren<TextMeshProUGUI>().text = ServiceLocator.GetService<GlyphService>().GetActionGlyph(PlayerActions.Instance.m_invokeHotbar, InputType.Controller);
		}

		private void DeleteObject()
		{
			State state = mCurrentState;
			if (state != null)
			{
				if (state is NoActionState noActionState)
				{
					noActionState.DeleteObject();
					return;
				}
				if (state is PlaceObjectState placeObjectState)
				{
					placeObjectState.DeleteObject();
					return;
				}
				if (state is AdjustObjectState adjustObjectState)
				{
					adjustObjectState.DeleteObject();
					return;
				}
			}
			throw new Exception("DeleteObject with bad SelectionState");
		}

		private void PickupObject()
		{
			State state = mCurrentState;
			if (state != null && state is NoActionState noActionState)
			{
				noActionState.PickupObject();
				return;
			}
			throw new Exception("PickupObject with bad SelectionState");
		}

		private void DropObject()
		{
			State state = mCurrentState;
			if (state != null && state is PlaceObjectState placeObjectState)
			{
				placeObjectState.DropObject();
				return;
			}
			throw new Exception("DropObject with bad SelectionState");
		}

		private void Rotate(float scrollDelta)
		{
			State state = mCurrentState;
			if (state != null && state is PlaceObjectState placeObjectState)
			{
				placeObjectState.Rotate(scrollDelta);
				return;
			}
			throw new Exception("Rotate with bad SelectionState");
		}

		private void Scale(float scrollDelta)
		{
			State state = mCurrentState;
			if (state != null && state is PlaceObjectState placeObjectState)
			{
				placeObjectState.Scale(scrollDelta);
				return;
			}
			throw new Exception("Scale with bad SelectionState");
		}

		private void FinishAdjustObject()
		{
			State state = mCurrentState;
			if (state != null && state is AdjustObjectState adjustObjectState)
			{
				adjustObjectState.FinishAdjustObject();
				return;
			}
			throw new Exception("FinishAdjustObject with bad SelectionState");
		}

		private void AdjustObject()
		{
			State state = mCurrentState;
			if (state != null && state is NoActionState noActionState)
			{
				noActionState.AdjustObject();
				return;
			}
			throw new Exception("AdjustObject with bad SelectionState");
		}

		private void StartMultiselect()
		{
			State state = mCurrentState;
			if (state != null && state is NoActionState noActionState)
			{
				noActionState.StartMultiselect();
				return;
			}
			throw new Exception("StartMultiselect with bad SelectionState");
		}

		private void StopMultiselect()
		{
			State state = mCurrentState;
			if (state != null && state is MultiSelectState multiSelectState)
			{
				multiSelectState.StopMultiselect();
				return;
			}
			throw new Exception("StartMultiselect with bad SelectionState");
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_invokeHotbar, delegate
			{
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				if (InputManager.ShiftIsPressed)
				{
					AdjustObject();
				}
				else
				{
					PickupObject();
				}
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				DeleteObject();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial2, delegate
			{
				StartMultiselect();
			});
			mPlaceObjectInputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				DropObject();
			}, "Drop");
			mPlaceObjectInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				DeleteObject();
			}, "Remove");
			mPlaceObjectInputState.AddOnKeyDownListener(actions.m_toolRotateLeft, delegate
			{
				Rotate(actions.m_toolRotateLeft.Value * InputManager.ScrollSensitivity);
			}, "Rotate or Scale (+Shift)");
			mPlaceObjectInputState.AddOnKeyDownListener(actions.m_toolRotateRight, delegate
			{
				Rotate((0f - actions.m_toolRotateRight.Value) * InputManager.ScrollSensitivity);
			});
			mAdjustObjectInputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				FinishAdjustObject();
			}, "Drop");
			mAdjustObjectInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				DeleteObject();
			}, "Remove");
			mAdjustObjectInputState.AddOnKeyDownListener(actions.m_toolSpecial2, delegate
			{
				Rotate(actions.m_toolSpecial2.Value * InputManager.ScrollSensitivity);
			});
			mAdjustObjectInputState.AddOnKeyDownListener(actions.m_toolSpecial1, delegate
			{
				Rotate((0f - actions.m_toolSpecial1.Value) * InputManager.ScrollSensitivity);
			});
			mAdjustObjectInputState.AddOnKeyDownListener(actions.m_toolSpecial3, delegate
			{
				Scale(actions.m_toolSpecial3.Value * InputManager.ScrollSensitivity);
			});
			mAdjustObjectInputState.AddOnKeyDownListener(actions.m_toolSpecial4, delegate
			{
				Scale((0f - actions.m_toolSpecial4.Value) * InputManager.ScrollSensitivity);
			});
			m_inputState.AddOnKeyUpListener(actions.m_toolSpecial2, delegate
			{
				StopMultiselect();
			});
		}

		public void EnableHeightAdjust(bool enabled)
		{
			mAdjustHeight = enabled;
		}

		private void SetInputState(InputState newInputState)
		{
			if (mCurrentInputState != newInputState)
			{
				if (mCurrentInputState != null)
				{
					InputManager.RemoveState(mCurrentInputState);
				}
				mCurrentInputState = newInputState;
				if (mCurrentInputState != null)
				{
					InputManager.PushState(mCurrentInputState);
				}
			}
		}

		private TargetInfo GetTargetInfo()
		{
			return Utility.GetTargetInfo(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
		}

		private Vector3 GetTargetPosition()
		{
			return Utility.GetTargetPosition(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
		}

		private static DMEditorComponent GetHoveredObject()
		{
			return Utility.GetObjectInLine(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance);
		}

		private DMEditorComponent InstantiateHologram(DMEditorComponent editorObject)
		{
			DMEditorComponent dMEditorComponent = DMEditor.Instance.InstantiateEditorObject(editorObject.ObjectTypeId, editorObject, DMEditor.Instance.Preview, animatedSpawn: false);
			UnityEngine.Object.Destroy(dMEditorComponent.GetComponentInChildren<Collider>());
			DMEditor.Instance.MoveToPreview(dMEditorComponent);
			Renderer[] componentsInChildren = dMEditorComponent.GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				Material[] sharedMaterials = renderer.sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					sharedMaterials[j] = mTransparentObjectPreviewMaterial;
				}
				renderer.sharedMaterials = sharedMaterials;
			}
			return dMEditorComponent;
		}

		private void SetTransformation(DMEditorComponent entityObject, GameObject parentGameObject, Quaternion previewSlope, EntityTransformation previewTransformation)
		{
			entityObject.Slope = previewSlope;
			entityObject.AdditionalRotation = previewTransformation.rotation;
			entityObject.Position = previewTransformation.position;
			entityObject.Scale = previewTransformation.scale;
			if ((bool)parentGameObject)
			{
				DMEditor.Instance.SetParent(entityObject, parentGameObject);
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
			hologramObject.GetComponentInChildren<MeshRenderer>().material.SetFloat("_Distance", hasValidPosition ? num : 10f);
		}

		private void PlayPlaceSound()
		{
			Utility.PlaySound("UI/Unit Placed", 1f, base.transform.position);
		}

		private void PlayRemoveSound()
		{
			Utility.PlaySound("UI/Unit Removed", 1f, base.transform.position);
		}

		private void EnableHighlight(GameObject gameObject, bool enabled, Color? color = null)
		{
			Utility.SetHighlightObject(gameObject, enabled, color ?? SelectedColor);
		}
	}
}
