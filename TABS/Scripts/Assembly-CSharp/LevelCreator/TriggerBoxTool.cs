using System.Collections.Generic;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;

namespace LevelCreator
{
	public class TriggerBoxTool : Tool
	{
		public class State
		{
			public virtual void TearDown()
			{
			}

			public virtual void OnStartLine()
			{
			}

			public virtual void OnFinishLine()
			{
			}

			public virtual void OnCancelLine()
			{
			}

			public virtual void OnRemoveConnection()
			{
			}

			public virtual void OnUpdate()
			{
			}

			public virtual void OnExitTriggerBoxTool()
			{
			}
		}

		private class NoActionState : State
		{
			private TriggerBoxTool m_triggerBoxTool;

			private DMEditorComponent m_targetObject;

			private DMEditorComponent m_previousTargetObject;

			private TriggerBox m_hoveredTriggerBox;

			private List<LineRenderer> m_lineRenderers = new List<LineRenderer>();

			public NoActionState(TriggerBoxTool triggerBoxTool)
			{
				m_triggerBoxTool = triggerBoxTool;
				triggerBoxTool.SetInputState(triggerBoxTool.m_noActionInputState);
			}

			public override void TearDown()
			{
				m_lineRenderers.ForEach(delegate(LineRenderer x)
				{
					m_triggerBoxTool.DestroyLineRenderer(x);
				});
				m_lineRenderers.Clear();
				m_triggerBoxTool.DestroyLineIndicies();
				if ((bool)m_targetObject)
				{
					Utility.SetHighlightObject(m_targetObject.gameObject, highlight: false);
				}
				if (!m_hoveredTriggerBox)
				{
					return;
				}
				m_hoveredTriggerBox.ForEachConnection(delegate(DMEditorComponent x)
				{
					if (x != null)
					{
						Utility.SetHighlightObject(x.gameObject, highlight: false);
					}
				});
			}

			public override void OnUpdate()
			{
				m_targetObject = m_triggerBoxTool.GetObject();
				TriggerBox result;
				if (m_targetObject != null)
				{
					m_hoveredTriggerBox = m_targetObject.GetComponentInChildren<TriggerBox>();
					if (m_hoveredTriggerBox != null && (m_previousTargetObject == null || m_previousTargetObject != m_targetObject))
					{
						if ((bool)m_previousTargetObject)
						{
							Utility.SetHighlightObject(m_previousTargetObject.gameObject, highlight: false);
							m_triggerBoxTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
						}
						Utility.SetHighlightObject(m_hoveredTriggerBox.gameObject, highlight: true);
						m_triggerBoxTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_triggerBoxTool.m_startConnectionIcon);
						if (m_hoveredTriggerBox.GetConnectionCount() > 0)
						{
							m_triggerBoxTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSecondary, m_triggerBoxTool.m_removeConnectionIcon);
						}
						int lineIndex = 0;
						m_hoveredTriggerBox.ForEachConnection(delegate(DMEditorComponent connection)
						{
							if (!(connection == null))
							{
								LineRenderer lineRenderer2 = Object.Instantiate(m_triggerBoxTool.m_lineRendererPrefab, null);
								lineRenderer2.SetPosition(0, m_hoveredTriggerBox.transform.position + m_triggerBoxTool.lineHeightOffset);
								lineRenderer2.SetPosition(1, connection.transform.position + m_triggerBoxTool.lineHeightOffset);
								m_lineRenderers.Add(lineRenderer2);
								m_triggerBoxTool.AddLineIndex(lineRenderer2, lineIndex);
								lineIndex++;
								Utility.SetHighlightObject(connection.gameObject, highlight: true);
							}
						});
					}
					if (m_hoveredTriggerBox == null && (m_previousTargetObject == null || m_previousTargetObject != m_targetObject))
					{
						Utility.SetHighlightObject(m_targetObject.gameObject, highlight: true);
						bool flag = false;
						TriggerBox[] triggerBoxes = m_triggerBoxes;
						foreach (TriggerBox triggerBox in triggerBoxes)
						{
							if (triggerBox != null && triggerBox.ConnectionsContains(m_targetObject))
							{
								flag = true;
								LineRenderer lineRenderer = Object.Instantiate(m_triggerBoxTool.m_lineRendererPrefab, null);
								lineRenderer.SetPosition(0, triggerBox.transform.position + m_triggerBoxTool.lineHeightOffset);
								lineRenderer.SetPosition(1, m_targetObject.transform.position + m_triggerBoxTool.lineHeightOffset);
								m_lineRenderers.Add(lineRenderer);
							}
						}
						if (flag)
						{
							m_triggerBoxTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolSecondary, m_triggerBoxTool.m_removeConnectionIcon);
						}
					}
				}
				else if (m_previousTargetObject != null && m_previousTargetObject.TryGetComponentInChildren<TriggerBox>(out result))
				{
					TriggerBox triggerBox2 = result;
					Utility.SetHighlightObject(triggerBox2.gameObject, highlight: false);
					triggerBox2.ForEachConnection(delegate(DMEditorComponent x)
					{
						if (x != null)
						{
							Utility.SetHighlightObject(x.gameObject, highlight: false);
						}
					});
					m_lineRenderers.ForEach(delegate(LineRenderer x)
					{
						m_triggerBoxTool.DestroyLineRenderer(x);
					});
					m_lineRenderers.Clear();
					m_triggerBoxTool.DestroyLineIndicies();
					m_triggerBoxTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
					m_hoveredTriggerBox = null;
				}
				else if (m_previousTargetObject != null)
				{
					Utility.SetHighlightObject(m_previousTargetObject.gameObject, highlight: false);
					m_lineRenderers.ForEach(delegate(LineRenderer x)
					{
						m_triggerBoxTool.DestroyLineRenderer(x);
					});
					m_lineRenderers.Clear();
					m_triggerBoxTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
				}
				m_previousTargetObject = m_targetObject;
				m_triggerBoxTool.UpdateLineIndexPositions(m_lineRenderers);
			}

			public override void OnStartLine()
			{
				if (m_targetObject == null || m_hoveredTriggerBox == null)
				{
					MessageDisplay.DisplayMessage("LC_SELECT_A_TRIGGERBOX");
					return;
				}
				TearDown();
				m_triggerBoxTool.m_currentState = new DrawingConnectionState(m_triggerBoxTool, m_hoveredTriggerBox);
			}

			public override void OnRemoveConnection()
			{
				if (m_hoveredTriggerBox != null)
				{
					m_hoveredTriggerBox.ForEachConnection(delegate(DMEditorComponent x)
					{
						if (x != null)
						{
							Utility.SetHighlightObject(x.gameObject, highlight: false);
						}
					});
					m_hoveredTriggerBox.ClearConnections();
					m_lineRenderers.ForEach(delegate(LineRenderer x)
					{
						m_triggerBoxTool.DestroyLineRenderer(x);
					});
					m_lineRenderers.Clear();
					m_triggerBoxTool.DestroyLineIndicies();
					m_triggerBoxTool.PlayRemoveSound();
				}
				else
				{
					if (!(m_targetObject != null))
					{
						return;
					}
					Utility.SetHighlightObject(m_targetObject.gameObject, highlight: false);
					TriggerBox[] triggerBoxes = m_triggerBoxes;
					foreach (TriggerBox triggerBox in triggerBoxes)
					{
						if (m_targetObject != null)
						{
							triggerBox.RemoveConnection(m_targetObject);
						}
					}
					m_lineRenderers.ForEach(delegate(LineRenderer x)
					{
						m_triggerBoxTool.DestroyLineRenderer(x);
					});
					m_lineRenderers.Clear();
					m_triggerBoxTool.PlayRemoveSound();
				}
			}

			public override void OnExitTriggerBoxTool()
			{
				DMEditor.Instance.toolBar.SwitchHotbar(0);
			}
		}

		private class DrawingConnectionState : State
		{
			private TriggerBoxTool m_triggerBoxTool;

			private DMEditorComponent m_targetObject;

			private DMEditorComponent m_previousTargetObject;

			private TriggerBox m_targetTriggerBox;

			private Vector3 m_targetPosition;

			private LineRenderer m_drawingLineRenderer;

			private List<LineRenderer> m_existingLines = new List<LineRenderer>();

			public DrawingConnectionState(TriggerBoxTool triggerBoxTool, TriggerBox targetTriggerBox)
			{
				DrawingConnectionState drawingConnectionState = this;
				m_triggerBoxTool = triggerBoxTool;
				m_targetTriggerBox = targetTriggerBox;
				m_drawingLineRenderer = Object.Instantiate(triggerBoxTool.m_lineRendererPrefab, null);
				triggerBoxTool.PlayPlaceSound();
				int lineIndex = 0;
				targetTriggerBox.ForEachConnection(delegate(DMEditorComponent connection)
				{
					if (!(connection == null))
					{
						LineRenderer lineRenderer = Object.Instantiate(triggerBoxTool.m_lineRendererPrefab);
						lineRenderer.SetPosition(0, targetTriggerBox.transform.position + triggerBoxTool.lineHeightOffset);
						lineRenderer.SetPosition(1, connection.transform.position + triggerBoxTool.lineHeightOffset);
						drawingConnectionState.m_existingLines.Add(lineRenderer);
						triggerBoxTool.AddLineIndex(lineRenderer, lineIndex);
						lineIndex++;
					}
				});
				triggerBoxTool.SetInputState(triggerBoxTool.m_drawingConnectionInputState);
			}

			public override void TearDown()
			{
				if ((bool)m_targetObject && (bool)m_targetObject.gameObject)
				{
					Utility.SetHighlightObject(m_targetObject.gameObject, highlight: false);
				}
				if ((bool)m_targetTriggerBox && (bool)m_targetTriggerBox.gameObject)
				{
					Utility.SetHighlightObject(m_targetTriggerBox.gameObject, highlight: false);
				}
				if ((bool)m_triggerBoxTool)
				{
					m_triggerBoxTool.DestroyLineRenderer(m_drawingLineRenderer);
				}
				foreach (LineRenderer existingLine in m_existingLines)
				{
					m_triggerBoxTool.DestroyLineRenderer(existingLine);
				}
				if ((bool)m_triggerBoxTool)
				{
					m_triggerBoxTool.DestroyLineIndicies();
				}
				if ((bool)m_triggerBoxTool)
				{
					m_triggerBoxTool.m_currentState = new NoActionState(m_triggerBoxTool);
				}
			}

			public override void OnUpdate()
			{
				m_targetPosition = Utility.GetTargetPosition(m_triggerBoxTool.m_dmEditor.playerCamera.transform.position, m_triggerBoxTool.m_dmEditor.playerCamera.transform.forward, m_triggerBoxTool.m_dmEditor.rayDistance);
				m_drawingLineRenderer.SetPosition(0, m_targetTriggerBox.transform.position + m_triggerBoxTool.lineHeightOffset);
				m_drawingLineRenderer.SetPosition(1, m_targetPosition);
				m_triggerBoxTool.UpdateLineIndexPositions(m_existingLines);
				m_targetObject = m_targetTriggerBox.ValidateHighlightedObject(m_triggerBoxTool.GetObject());
				if ((bool)m_targetObject)
				{
					Utility.SetHighlightObject(m_targetObject.gameObject, highlight: true);
					if (m_targetObject != m_previousTargetObject)
					{
						if (m_previousTargetObject != null)
						{
							Utility.SetHighlightObject(m_previousTargetObject.gameObject, highlight: false);
						}
						m_triggerBoxTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
						m_triggerBoxTool.m_dmEditor.contextInfoMenu.AddContextKey(PlayerActions.Instance.m_toolPrimary, m_triggerBoxTool.m_finishConnectionIcon);
					}
				}
				else if ((bool)m_previousTargetObject)
				{
					Utility.SetHighlightObject(m_previousTargetObject.gameObject, highlight: false);
					if (m_targetObject != m_previousTargetObject)
					{
						m_triggerBoxTool.m_dmEditor.contextInfoMenu.ReplaceContextKeys();
					}
				}
				m_previousTargetObject = m_targetObject;
			}

			public override void OnFinishLine()
			{
				if (m_targetObject == null)
				{
					MessageDisplay.DisplayMessage(m_targetTriggerBox.m_invalidObjectMessage);
					return;
				}
				m_triggerBoxTool.PlayPlaceSound();
				if (!m_targetTriggerBox.ConnectionsContains(m_targetObject) && m_targetTriggerBox.gameObject != m_targetObject.gameObject)
				{
					m_targetTriggerBox.AddConnection(m_targetObject);
					m_triggerBoxTool.m_dmEditor.ScheduleTakeLevelSnapshot();
					if (InputManager.ShiftIsPressed)
					{
						LineRenderer lineRenderer = Object.Instantiate(m_drawingLineRenderer);
						lineRenderer.SetPosition(1, m_targetObject.transform.position + m_triggerBoxTool.lineHeightOffset);
						m_existingLines.Add(lineRenderer);
						m_triggerBoxTool.AddLineIndex(lineRenderer, m_existingLines.Count - 1);
					}
				}
				if (!InputManager.ShiftIsPressed)
				{
					TearDown();
				}
			}

			public override void OnCancelLine()
			{
				m_triggerBoxTool.PlayRemoveSound();
				TearDown();
			}

			public override void OnExitTriggerBoxTool()
			{
				DMEditor.Instance.toolBar.SwitchHotbar(0);
			}
		}

		private InputState m_noActionInputState = new InputState("TriggerBoxTool.NoActionInputState");

		private InputState m_drawingConnectionInputState = new InputState("TriggerBoxTool.DrawingConnectionInputState");

		private InputState m_currentInputState;

		private State m_currentState;

		private DMEditor m_dmEditor;

		private Vector3 lineHeightOffset = new Vector3(0f, 0.75f, 0f);

		[SerializeField]
		private LineRenderer m_lineRendererPrefab;

		[SerializeField]
		private GameObject m_lineIndexPrefab;

		[SerializeField]
		private Sprite m_cancelIcon;

		[SerializeField]
		private Sprite m_finishConnectionIcon;

		[SerializeField]
		private Sprite m_startConnectionIcon;

		[SerializeField]
		private Sprite m_removeConnectionIcon;

		[SerializeField]
		private Sprite m_exitTriggerboxToolIcon;

		private static TriggerBox[] m_triggerBoxes = new TriggerBox[0];

		private List<GameObject> lineIndiciesObjects = new List<GameObject>();

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

		protected override void Start()
		{
			base.Start();
			m_dmEditor = DMEditor.Instance;
			m_currentState = new NoActionState(this);
			m_triggerBoxes = Object.FindObjectsOfType<TriggerBox>();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				m_currentState.OnStartLine();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				m_currentState.OnRemoveConnection();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_toolConnectTriggers, delegate
			{
				m_currentState.OnExitTriggerBoxTool();
			}, m_exitTriggerboxToolIcon);
			m_noActionInputState.AddOnKeyDownListener(actions.m_back, delegate
			{
				m_currentState.OnExitTriggerBoxTool();
			});
			m_noActionInputState.AddOnKeyDownListener(actions.m_enterExitBattle, delegate
			{
				m_currentState.OnExitTriggerBoxTool();
			});
			m_drawingConnectionInputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				m_currentState.OnFinishLine();
			});
			m_drawingConnectionInputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				m_currentState.OnCancelLine();
			}, m_cancelIcon);
		}

		private void Update()
		{
			m_currentState.OnUpdate();
		}

		private void DestroyLineRenderer(LineRenderer lineRenderer)
		{
			if (!(lineRenderer == null))
			{
				Object.Destroy(lineRenderer.gameObject, 0.7f);
				LeanTween.value(lineRenderer.gameObject, 0.6f, 0f, 0.6f).setOnUpdate(delegate(float v)
				{
					lineRenderer.widthMultiplier = v;
				}).setEaseInExpo();
			}
		}

		private void AddLineIndex(LineRenderer line, int index)
		{
			GameObject gameObject = Object.Instantiate(m_lineIndexPrefab, m_dmEditor.playerCanvasRenderer.transform);
			gameObject.transform.position = m_dmEditor.playerCamera.WorldToScreenPoint(line.GetPosition(1));
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = (index + 1).ToString();
			lineIndiciesObjects.Add(gameObject);
		}

		private void UpdateLineIndexPositions(List<LineRenderer> lines)
		{
			if (lineIndiciesObjects.Count != lines.Count)
			{
				return;
			}
			Camera playerCamera = m_dmEditor.playerCamera;
			for (int i = 0; i < lines.Count; i++)
			{
				if (!(lineIndiciesObjects[i] == null) && !(lines[i] == null))
				{
					lineIndiciesObjects[i].transform.position = playerCamera.WorldToScreenPoint(lines[i].GetPosition(1));
				}
			}
		}

		private void DestroyLineIndicies()
		{
			lineIndiciesObjects.ForEach(delegate(GameObject x)
			{
				Object.Destroy(x.gameObject);
			});
			lineIndiciesObjects.Clear();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (m_currentState is DrawingConnectionState)
			{
				m_currentState.TearDown();
				m_currentState = new NoActionState(this);
			}
			m_currentState.TearDown();
			InputManager.RemoveState(m_noActionInputState);
			if (DMEditor.Instance != null && DMEditor.Instance.toolBar != null)
			{
				DMEditor.Instance.toolBar.Show();
			}
		}

		private DMEditorComponent GetObject()
		{
			return Utility.GetObjectInLine(m_dmEditor.playerCamera.transform.position, m_dmEditor.playerCamera.transform.forward, m_dmEditor.rayDistance);
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
