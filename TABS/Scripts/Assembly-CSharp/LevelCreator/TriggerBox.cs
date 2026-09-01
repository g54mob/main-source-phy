using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameState;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelCreator
{
	public class TriggerBox : MonoBehaviour
	{
		private List<DMEditorComponent> m_connections = new List<DMEditorComponent>();

		public List<GameObject> m_playConnections = new List<GameObject>();

		[SerializeField]
		[FormerlySerializedAs("selectableCollider")]
		private Collider m_selectableCollider;

		private List<Unit> m_collidedUnits = new List<Unit>();

		private GameStateManager m_gameStateManager;

		[FormerlySerializedAs("invalidObjectMessage")]
		public string m_invalidObjectMessage;

		private void Awake()
		{
			m_selectableCollider.enabled = false;
			if (DMEditor.Instance != null)
			{
				m_selectableCollider.enabled = true;
				return;
			}
			m_selectableCollider.enabled = false;
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
			Canvas[] componentsInChildren2 = GetComponentsInChildren<Canvas>();
			if (componentsInChildren2 != null)
			{
				Canvas[] array = componentsInChildren2;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].enabled = false;
				}
			}
			ParticleSystem[] componentsInChildren3 = GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].gameObject.SetActive(value: false);
			}
			m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		}

		protected bool CanTrigger(Collider other)
		{
			if (other == null)
			{
				return false;
			}
			if (m_gameStateManager == null || m_gameStateManager.GameState != GameState.BattleState)
			{
				return false;
			}
			Transform parent = other.transform.parent;
			Unit unit = null;
			while (parent != null)
			{
				unit = parent.GetComponent<Unit>();
				if (unit != null)
				{
					break;
				}
				parent = parent.parent;
			}
			if (other == null || unit == null || m_collidedUnits.Contains(unit))
			{
				return false;
			}
			m_collidedUnits.Add(unit);
			return true;
		}

		public virtual void Trigger(Collider other)
		{
			if (!CanTrigger(other))
			{
				return;
			}
			m_playConnections.ForEach(delegate(GameObject triggerable)
			{
				if (triggerable != null)
				{
					triggerable.GetComponent<ITriggerable>()?.Trigger();
				}
			});
		}

		public virtual DMEditorComponent ValidateHighlightedObject(DMEditorComponent obj)
		{
			if (obj == null || obj.GetComponent<ITriggerable>() == null)
			{
				return null;
			}
			return obj;
		}

		public void AddConnection(DMEditorComponent newConnection)
		{
			m_connections.Add(newConnection);
			UpdateCustomData();
		}

		public void RemoveConnection(DMEditorComponent connection)
		{
			if (m_connections.Contains(connection))
			{
				m_connections.Remove(connection);
				UpdateCustomData();
			}
		}

		public void ClearConnections()
		{
			m_connections.Clear();
			UpdateCustomData();
		}

		public void SetConnections(List<DMEditorComponent> newConnections)
		{
			m_connections = newConnections;
			UpdateCustomData();
		}

		public List<DMEditorComponent> GetConnectionsCopy()
		{
			List<DMEditorComponent> list = new List<DMEditorComponent>();
			list.AddRange(m_connections);
			return list;
		}

		public int GetConnectionCount()
		{
			return m_connections.Count;
		}

		public void ForEachConnection(Action<DMEditorComponent> connection)
		{
			m_connections.ForEach(connection);
		}

		public bool ConnectionsContains(DMEditorComponent connection)
		{
			return m_connections.Contains(connection);
		}

		public void UpdateCustomData()
		{
			DMEditorComponent component = GetComponent<DMEditorComponent>();
			if (component == null)
			{
				return;
			}
			Dictionary<string, string> customData = component.entity.customData;
			component.entity.customData = new Dictionary<string, string>();
			if (customData != null)
			{
				foreach (KeyValuePair<string, string> item in customData)
				{
					if (item.Key != "triggerBox")
					{
						component.entity.customData.Add(item.Key, item.Value);
					}
				}
			}
			string text = "";
			foreach (DMEditorComponent connection in m_connections)
			{
				text = text + ((text == "") ? "" : ", ") + connection.entity.guid;
			}
			if (!(text == string.Empty))
			{
				component.entity.customData.Add("triggerBox", text);
			}
		}
	}
}
