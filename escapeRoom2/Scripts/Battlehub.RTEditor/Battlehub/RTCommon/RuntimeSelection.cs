using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class RuntimeSelection : IRuntimeSelectionInternal, IRuntimeSelection, IEnumerable
	{
		private bool m_isEnabled = true;

		private bool m_enableUndo = true;

		protected Object m_activeObject;

		protected Object[] m_objects;

		private HashSet<Object> m_selectionHS;

		private IRTE m_editor;

		private Object[] m_empty = new Object[0];

		public Object INTERNAL_activeObjectProperty
		{
			get
			{
				return m_activeObject;
			}
			set
			{
				m_activeObject = value;
			}
		}

		public Object[] INTERNAL_objectsProperty
		{
			get
			{
				return m_objects;
			}
			set
			{
				SetObjects(value);
			}
		}

		public bool Enabled
		{
			get
			{
				return m_isEnabled;
			}
			set
			{
				m_isEnabled = value;
				if (!m_isEnabled)
				{
					objects = null;
				}
			}
		}

		public bool EnableUndo
		{
			get
			{
				return m_enableUndo;
			}
			set
			{
				m_enableUndo = value;
			}
		}

		public GameObject activeGameObject
		{
			get
			{
				return activeObject as GameObject;
			}
			set
			{
				activeObject = value;
			}
		}

		public Object activeObject
		{
			get
			{
				return m_activeObject;
			}
			set
			{
				if (value == null)
				{
					objects = null;
					return;
				}
				objects = new Object[1] { value };
			}
		}

		public Object[] objects
		{
			get
			{
				return m_objects;
			}
			set
			{
				if (m_isEnabled && IsSelectionChanged(value))
				{
					if (m_editor != null && m_editor.Undo.Enabled && EnableUndo)
					{
						m_editor.Undo.Select(this, value, null);
					}
					else
					{
						SetObjects(value);
					}
				}
			}
		}

		public int Length
		{
			get
			{
				if (m_objects == null)
				{
					return 0;
				}
				return m_objects.Length;
			}
		}

		public GameObject[] gameObjects
		{
			get
			{
				if (m_objects == null)
				{
					return null;
				}
				return m_objects.OfType<GameObject>().ToArray();
			}
		}

		public Transform activeTransform
		{
			get
			{
				if (m_activeObject == null)
				{
					return null;
				}
				if (m_activeObject is GameObject)
				{
					return ((GameObject)m_activeObject).transform;
				}
				return null;
			}
		}

		public event RuntimeSelectionChanged SelectionChanged;

		protected void RaiseSelectionChanged(Object[] unselectedObjects)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(unselectedObjects);
			}
		}

		public RuntimeSelection(IRTE rte)
		{
			m_editor = rte;
		}

		public RuntimeSelection()
		{
		}

		public bool IsSelected(Object obj)
		{
			if (m_selectionHS == null)
			{
				return false;
			}
			return m_selectionHS.Contains(obj);
		}

		private void UpdateHS()
		{
			if (m_objects != null)
			{
				m_selectionHS = new HashSet<Object>(m_objects);
			}
			else
			{
				m_selectionHS = null;
			}
		}

		private bool IsSelectionChanged(Object[] value)
		{
			if (m_objects == value)
			{
				return false;
			}
			if (m_objects == null)
			{
				return value.Length != 0;
			}
			if (value == null)
			{
				return m_objects.Length != 0;
			}
			if (m_objects.Length != value.Length)
			{
				return true;
			}
			for (int i = 0; i < m_objects.Length; i++)
			{
				if (m_objects[i] != value[i])
				{
					return true;
				}
			}
			return false;
		}

		protected void SetObjects(Object[] value)
		{
			if (!IsSelectionChanged(value))
			{
				return;
			}
			Object[] unselectedObjects = ((m_objects != null) ? m_objects.Where((Object obj) => obj != null).ToArray() : m_objects);
			if (value == null)
			{
				m_objects = null;
				m_activeObject = null;
			}
			else
			{
				m_objects = value.Where((Object v) => v != null).ToArray();
				if (m_activeObject == null || !m_objects.Contains(m_activeObject))
				{
					m_activeObject = m_objects.OfType<Object>().FirstOrDefault();
				}
			}
			UpdateHS();
			RaiseSelectionChanged(unselectedObjects);
		}

		public void Select(Object activeObject, Object[] selection)
		{
			if (IsSelectionChanged(selection))
			{
				m_activeObject = activeObject;
				SetObjects(selection);
			}
		}

		public IEnumerator GetEnumerator()
		{
			if (m_objects != null)
			{
				return m_objects.GetEnumerator();
			}
			return m_empty.GetEnumerator();
		}
	}
}
