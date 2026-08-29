using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RuntimeTools
	{
		private bool m_isViewing;

		private bool m_autoFocus;

		private bool m_unitSnapping;

		private bool m_isSnapping;

		private SnappingMode m_snappingMode = SnappingMode.Vertex;

		private UnityEngine.Object m_activeTool;

		private LockObject m_lockAxes;

		private RuntimeTool m_current;

		private object m_custom;

		private bool m_isBoxSelectionEnabled = true;

		private RuntimePivotMode m_pivotMode;

		private RuntimePivotRotation m_pivotRotation;

		private SelectionMode m_selectionMode;

		private bool m_showSelectionGizmos;

		private bool m_showGizmos;

		public bool IsViewing
		{
			get
			{
				return m_isViewing;
			}
			set
			{
				if (m_isViewing != value)
				{
					m_isViewing = value;
					if (m_isViewing)
					{
						ActiveTool = null;
					}
					this.IsViewingChanged?.Invoke();
				}
			}
		}

		public bool AutoFocus
		{
			get
			{
				return m_autoFocus;
			}
			set
			{
				if (m_autoFocus != value)
				{
					m_autoFocus = value;
					this.AutoFocusChanged?.Invoke();
				}
			}
		}

		public bool UnitSnapping
		{
			get
			{
				return m_unitSnapping;
			}
			set
			{
				if (m_unitSnapping != value)
				{
					m_unitSnapping = value;
					this.UnitSnappingChanged?.Invoke();
				}
			}
		}

		public bool IsSnapping
		{
			get
			{
				return m_isSnapping;
			}
			set
			{
				if (m_isSnapping != value)
				{
					m_isSnapping = value;
					this.IsSnappingChanged?.Invoke();
				}
			}
		}

		public SnappingMode SnappingMode
		{
			get
			{
				return m_snappingMode;
			}
			set
			{
				if (m_snappingMode != value)
				{
					m_snappingMode = value;
					this.SnappingModeChanged?.Invoke();
				}
			}
		}

		public UnityEngine.Object ActiveTool
		{
			get
			{
				return m_activeTool;
			}
			set
			{
				if (m_activeTool != value)
				{
					m_activeTool = value;
					this.ActiveToolChanged?.Invoke();
				}
			}
		}

		public LockObject LockAxes
		{
			get
			{
				return m_lockAxes;
			}
			set
			{
				if (m_lockAxes != value)
				{
					m_lockAxes = value;
					this.LockAxesChanged?.Invoke();
				}
			}
		}

		public RuntimeTool Current
		{
			get
			{
				return m_current;
			}
			set
			{
				if (m_current != value)
				{
					this.ToolChanging?.Invoke(value, null);
					m_current = value;
					if (m_current != RuntimeTool.Custom)
					{
						m_isBoxSelectionEnabled = true;
					}
					m_custom = null;
					this.ToolChanged?.Invoke();
				}
			}
		}

		public object Custom
		{
			get
			{
				return m_custom;
			}
			set
			{
				if (m_custom != value)
				{
					this.ToolChanging?.Invoke(RuntimeTool.Custom, value);
					m_current = RuntimeTool.Custom;
					m_custom = value;
					this.ToolChanged?.Invoke();
				}
			}
		}

		public bool IsBoxSelectionEnabled
		{
			get
			{
				return m_isBoxSelectionEnabled;
			}
			set
			{
				if (m_isBoxSelectionEnabled != value)
				{
					m_isBoxSelectionEnabled = value;
					this.IsBoxSelectionEnabledChanged?.Invoke();
				}
			}
		}

		public RuntimePivotRotation PivotRotation
		{
			get
			{
				return m_pivotRotation;
			}
			set
			{
				if (m_pivotRotation != value)
				{
					this.PivotRotationChanging?.Invoke();
					m_pivotRotation = value;
					this.PivotRotationChanged?.Invoke();
				}
			}
		}

		public RuntimePivotMode PivotMode
		{
			get
			{
				return m_pivotMode;
			}
			set
			{
				if (m_pivotMode != value)
				{
					this.PivotModeChanging?.Invoke();
					m_pivotMode = value;
					this.PivotModeChanged?.Invoke();
				}
			}
		}

		public Vector3 CustomPivotPosition { get; set; }

		public SelectionMode SelectionMode
		{
			get
			{
				return m_selectionMode;
			}
			set
			{
				if (m_selectionMode != value)
				{
					this.SelectionModeChanging?.Invoke();
					m_selectionMode = value;
					this.SelectionModeChanged?.Invoke();
				}
			}
		}

		[Obsolete]
		public bool ShowSelectionGizmos
		{
			get
			{
				return m_showSelectionGizmos;
			}
			set
			{
				if (m_showSelectionGizmos != value)
				{
					m_showSelectionGizmos = value;
					this.ShowSelectionGizmosChanged?.Invoke();
				}
			}
		}

		[Obsolete]
		public bool ShowGizmos
		{
			get
			{
				return m_showGizmos;
			}
			set
			{
				if (m_showGizmos != value)
				{
					m_showGizmos = value;
					this.ShowGizmosChanged?.Invoke();
				}
			}
		}

		public event RuntimeToolsEvent<RuntimeTool, object> ToolChanging;

		public event RuntimeToolsEvent ToolChanged;

		public event RuntimeToolsEvent PivotRotationChanging;

		public event RuntimeToolsEvent PivotRotationChanged;

		public event RuntimeToolsEvent PivotModeChanging;

		public event RuntimeToolsEvent PivotModeChanged;

		public event RuntimeToolsEvent SelectionModeChanging;

		public event RuntimeToolsEvent SelectionModeChanged;

		public event RuntimeToolsEvent IsViewingChanged;

		public event RuntimeToolsEvent AutoFocusChanged;

		public event RuntimeToolsEvent UnitSnappingChanged;

		public event RuntimeToolsEvent IsSnappingChanged;

		public event RuntimeToolsEvent SnappingModeChanged;

		public event RuntimeToolsEvent LockAxesChanged;

		public event RuntimeToolsEvent ActiveToolChanged;

		public event RuntimeToolsEvent IsBoxSelectionEnabledChanged;

		[Obsolete]
		public event RuntimeToolsEvent ShowSelectionGizmosChanged;

		[Obsolete]
		public event RuntimeToolsEvent ShowGizmosChanged;

		public RuntimeTools()
		{
			Reset();
		}

		public void Reset()
		{
			ActiveTool = null;
			LockAxes = null;
			Custom = null;
			m_isViewing = false;
			m_isSnapping = false;
			m_showSelectionGizmos = true;
			m_showGizmos = true;
			m_unitSnapping = false;
			m_pivotMode = RuntimePivotMode.Center;
		}
	}
}
