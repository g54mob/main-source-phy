using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;

namespace LevelCreator
{
	public class LineTool : Tool
	{
		[SerializeField]
		private GameObject m_linePreviewPrefab;

		[SerializeField]
		private Material m_previewObjectMaterial;

		private LineRenderer m_linePreview;

		private DMEditor m_dmEditor;

		private static string currentObjectID;

		private Vector3 m_targetPosition;

		private Vector3 m_lineStartPosition;

		private float m_objectSpacingOffset = 1f;

		private Vector3 m_objectScaling;

		private const float m_spacingSensitivity = 0.15f;

		private float m_rotationSensitivity = 10f;

		private float m_yRotation;

		private bool m_drawingLine;

		private bool m_snapToAxis;

		private bool m_modifierPressed;

		private static float objectSizeX;

		private List<DMEditorComponent> m_previewObjects = new List<DMEditorComponent>();

		private InputState m_inputDrawState = new InputState("LineToolDrawInputState");

		private DMEditorComponent hoveredObject;

		private DMEditorComponent startObject;

		protected override void Start()
		{
			base.Start();
			m_dmEditor = DMEditor.Instance;
			m_dmEditor.HideCursor();
			m_dmEditor.undo.AddListener(CancelLine);
			_ = m_dmEditor.editorObjectTable;
			m_linePreview = UnityEngine.Object.Instantiate(m_linePreviewPrefab).GetComponent<LineRenderer>();
			m_linePreview.enabled = false;
			RebuildContextMenu();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				StartLine();
			});
			m_inputDrawState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				FinishLine();
			}, m_contextIcons.m_primaryIcon);
			m_inputDrawState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				CancelLine();
				PlayRemoveSound();
			}, m_contextIcons.m_secondaryIcon);
			m_inputDrawState.AddOnKeyDownListener(actions.m_toolScaleUp, delegate
			{
				if (!m_modifierPressed)
				{
					ChangeSpacing(-0.15f);
					Utility.PlaySound("UI/SmallClick", 0.5f, base.transform.position);
				}
				else
				{
					Rotate(m_rotationSensitivity);
				}
			}, m_contextIcons.m_special1Icon);
			m_inputDrawState.AddOnKeyDownListener(actions.m_toolScaleDown, delegate
			{
				if (!m_modifierPressed)
				{
					ChangeSpacing(0.15f);
					Utility.PlaySound("UI/SmallClick", 0.5f, base.transform.position);
				}
				else
				{
					Rotate(0f - m_rotationSensitivity);
				}
			});
			m_inputDrawState.AddOnKeyDownListener(actions.m_scaleModifier, delegate
			{
				m_modifierPressed = true;
				RebuildContextMenu();
			});
			m_inputDrawState.AddOnKeyUpListener(actions.m_scaleModifier, delegate
			{
				m_modifierPressed = false;
				RebuildContextMenu();
			});
			m_inputDrawState.AddOnKeyDownListener(actions.m_enterExitBattle, delegate
			{
			});
		}

		private void ChangeSpacing(float delta)
		{
			m_objectSpacingOffset = Mathf.Max(0f, m_objectSpacingOffset * (1f + delta));
		}

		private void Rotate(float delta)
		{
			m_yRotation += delta;
		}

		public void SetSnapToAxis(bool enabled)
		{
			m_snapToAxis = enabled;
		}

		private void Update()
		{
			if (this == null || m_dmEditor == null || m_dmEditor.playerCamera == null)
			{
				return;
			}
			InputHoldUpdate();
			m_targetPosition = Utility.GetTargetPositionOnVolume(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
			DMEditorComponent objectInLine = Utility.GetObjectInLine(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
			if (objectInLine != hoveredObject)
			{
				if (hoveredObject != null)
				{
					Utility.SetHighlightObject(hoveredObject.gameObject, highlight: false);
					m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Crosshair);
				}
				hoveredObject = objectInLine;
				if (hoveredObject != null)
				{
					m_dmEditor.SetVisualTargetMode(DMEditor.VisualTargetMode.Hand);
					Utility.SetHighlightObject(hoveredObject.gameObject, highlight: true);
				}
			}
			if (m_drawingLine)
			{
				if (m_linePreview.positionCount > 1)
				{
					m_linePreview.SetPosition(m_linePreview.positionCount - 1, m_targetPosition + Vector3.up * 0.2f);
				}
				UpdatePreviews();
			}
		}

		private void InputHoldUpdate()
		{
			PlayerActions instance = PlayerActions.Instance;
			bool flag = InputManager.ShouldPollInvokePlayerAction(instance.m_toolScaleDown);
			bool flag2 = InputManager.ShouldPollInvokePlayerAction(instance.m_toolScaleUp);
			bool num = m_modifierPressed && flag;
			bool flag3 = m_modifierPressed && flag2;
			if (num)
			{
				Rotate(-60f * Time.deltaTime);
			}
			else if (flag)
			{
				ChangeSpacing(1f * Time.deltaTime);
			}
			if (flag3)
			{
				Rotate(60f * Time.deltaTime);
			}
			else if (flag2)
			{
				ChangeSpacing(-1f * Time.deltaTime);
			}
		}

		private void RebuildContextMenu()
		{
			PlayerActions instance = PlayerActions.Instance;
			if (m_drawingLine)
			{
				if (m_modifierPressed)
				{
					DMEditor.Instance.contextInfoMenu.ReplaceContextKeys(displayInputStateActions: false);
					DMEditor.Instance.contextInfoMenu.AddContextKey(instance.m_toolScaleUp, m_contextIcons.m_special3Icon);
					DMEditor.Instance.contextInfoMenu.AddContextKey(instance.m_toolScaleDown, m_contextIcons.m_special4Icon);
				}
				else
				{
					DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
					DMEditor.Instance.contextInfoMenu.AddContextKey(instance.m_toolRotateRight, m_contextIcons.m_special1Icon);
					DMEditor.Instance.contextInfoMenu.AddContextKey(instance.m_scaleModifier, m_contextIcons.m_special2Icon);
				}
			}
			else
			{
				DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
				DMEditor.Instance.contextInfoMenu.AddContextKey(instance.m_toolPrimary, m_contextIcons.m_special5Icon);
			}
		}

		private void UpdatePreviews()
		{
			Vector3.Distance(m_targetPosition, m_lineStartPosition);
			float num = Mathf.Max(objectSizeX, objectSizeX + m_objectSpacingOffset);
			Vector3 vector = (m_targetPosition - m_lineStartPosition).normalized;
			int num2 = 0;
			for (int i = 0; i < 100; i++)
			{
				Vector3 vector2 = m_lineStartPosition + (float)(i + 1) * num * vector;
				if (Vector3.Distance(m_lineStartPosition, m_targetPosition) < Vector3.Distance(m_lineStartPosition, vector2) || !Utility.GetSnapTransform(vector2, Utility.SnapDistance.Unlimited).HasValue)
				{
					break;
				}
				num2++;
			}
			m_linePreview.positionCount = num2 + 2;
			if (m_snapToAxis)
			{
				Transform transform = startObject.transform;
				float num3 = Vector3.Dot(vector, transform.right);
				float num4 = Vector3.Dot(vector, transform.up);
				float num5 = Vector3.Dot(vector, transform.forward);
				float num6 = Vector3.Dot(vector, -transform.right);
				float num7 = Vector3.Dot(vector, -transform.up);
				float num8 = Vector3.Dot(vector, -transform.forward);
				float a = Mathf.Max(num3, Mathf.Max(num4, num5));
				float b = Mathf.Max(num6, Mathf.Max(num7, num8));
				float num9 = Mathf.Max(a, b);
				if (num9 == num3)
				{
					vector = transform.right;
				}
				else if (num9 == num4)
				{
					vector = transform.up;
				}
				else if (num9 == num5)
				{
					vector = transform.forward;
				}
				else if (num9 == num6)
				{
					vector = -transform.right;
				}
				else if (num9 == num7)
				{
					vector = -transform.up;
				}
				else if (num9 == num8)
				{
					vector = -transform.forward;
				}
			}
			while (m_previewObjects.Count < num2)
			{
				DMEditorComponent dMEditorComponent = InstantiateEntityWithChildren();
				Array.ForEach(dMEditorComponent.GetComponentsInChildren<MeshRenderer>(), delegate(MeshRenderer x)
				{
					Material[] array = new Material[x.materials.Length];
					for (int j = 0; j < x.materials.Length; j++)
					{
						array[j] = m_previewObjectMaterial;
					}
					x.materials = array;
				});
				m_previewObjects.Add(dMEditorComponent);
			}
			while (m_previewObjects.Count > num2)
			{
				DMEditorComponent dMEditorComponent2 = m_previewObjects[m_previewObjects.Count - 1];
				m_previewObjects.Remove(dMEditorComponent2);
				UnityEngine.Object.Destroy(dMEditorComponent2.gameObject);
			}
			for (int num10 = 0; num10 < m_previewObjects.Count; num10++)
			{
				Vector3 newPosition = m_lineStartPosition + (float)(num10 + 1) * num * vector;
				DMEditorComponent dMEditorComponent3 = m_previewObjects[num10];
				dMEditorComponent3.Scale = m_objectScaling;
				dMEditorComponent3.AdditionalRotation = startObject.transform.rotation * Quaternion.Euler(0f, 0f - m_yRotation, 0f);
				Utility.SnapObjectAt(dMEditorComponent3, newPosition, DMEditorComponent.TeleportMode.TeleportNone, Utility.SnapDistance.Unlimited);
				if (m_linePreview.positionCount > 2)
				{
					m_linePreview.SetPosition(Mathf.Min(num10 + 1, m_linePreview.positionCount - 2), dMEditorComponent3.Position);
				}
			}
		}

		private void StartLine()
		{
			if (hoveredObject != null)
			{
				currentObjectID = hoveredObject.ObjectTypeId;
				startObject = hoveredObject;
			}
			else
			{
				currentObjectID = null;
			}
			if (string.IsNullOrEmpty(currentObjectID))
			{
				MessageDisplay.DisplayMessage("LC_SELECT_AN_OBJECT");
				return;
			}
			objectSizeX = 1f;
			MeshRenderer componentInChildren = m_dmEditor.editorObjectTable.GetRowValue(currentObjectID).EditorObject.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				objectSizeX = componentInChildren.bounds.size.x * 0.5f;
			}
			m_objectScaling = hoveredObject.Scale;
			m_drawingLine = true;
			m_linePreview.enabled = true;
			m_lineStartPosition = hoveredObject.GetGlobalEntityTransform().position;
			m_linePreview.SetPosition(0, m_lineStartPosition + Vector3.up * 0.2f);
			InputManager.PushState(m_inputDrawState);
			RebuildContextMenu();
			PlayPlaceSound();
		}

		private void FinishLine()
		{
			for (int i = 0; i < m_previewObjects.Count; i++)
			{
				DMEditorComponent dMEditorComponent = InstantiateEntityWithChildren();
				dMEditorComponent.Position = m_previewObjects[i].Position;
				dMEditorComponent.AdditionalRotation = m_previewObjects[i].AdditionalRotation;
				dMEditorComponent.Scale = m_previewObjects[i].Scale;
				dMEditorComponent.HeightOffset = m_previewObjects[i].HeightOffset;
				m_dmEditor.MoveToLevel(dMEditorComponent);
			}
			StartCoroutine(PlaySoundWithInterval("UI/Unit Placed", base.transform.position, m_previewObjects.Count, 0.035f));
			m_drawingLine = false;
			m_linePreview.enabled = false;
			currentObjectID = null;
			m_dmEditor.ScheduleTakeLevelSnapshot();
			DestroyPreviews();
			InputManager.RemoveState(m_inputDrawState);
			RebuildContextMenu();
		}

		private void DestroyPreviews()
		{
			for (int i = 0; i < m_previewObjects.Count; i++)
			{
				if (m_previewObjects[i] != null)
				{
					UnityEngine.Object.Destroy(m_previewObjects[i].gameObject);
				}
			}
			m_previewObjects.Clear();
		}

		private void CancelLine()
		{
			m_drawingLine = false;
			if ((bool)m_linePreview)
			{
				m_linePreview.enabled = false;
			}
			currentObjectID = null;
			DestroyPreviews();
			InputManager.RemoveState(m_inputDrawState);
			RebuildContextMenu();
		}

		private DMEditorComponent InstantiateEntityWithChildren()
		{
			return m_dmEditor.InstantiateEditorObject(LevelUtil.BuildEntityTree(startObject), m_dmEditor.LevelRootObject, animatedSpawn: false, null, null);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_dmEditor.undo.RemoveListener(CancelLine);
			CancelLine();
			if (m_linePreview != null)
			{
				UnityEngine.Object.Destroy(m_linePreview.gameObject);
			}
			DestroyPreviews();
			if (hoveredObject != null)
			{
				Utility.SetHighlightObject(hoveredObject.gameObject, highlight: false);
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

		private IEnumerator PlaySoundWithInterval(string soundRef, Vector3 position, int count, float interval)
		{
			for (int i = 0; i < count; i++)
			{
				Utility.PlaySound(soundRef, 1f, position);
				yield return new WaitForSeconds(interval);
			}
		}
	}
}
