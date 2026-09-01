using System;
using System.Collections;
using System.Collections.Generic;
using DM;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	[RequireComponent(typeof(HorizontalLayoutGroup))]
	public class UILayoutGroup : MonoBehaviour
	{
		public struct ElementDataWrapper
		{
			public FactionButton.FactionButtonData factionData;

			public UnitButton.UnitButtonData unitData;

			public ElementDataWrapper(FactionButton.FactionButtonData data)
			{
				factionData = data;
				unitData = null;
			}

			public ElementDataWrapper(UnitButton.UnitButtonData data)
			{
				factionData = null;
				unitData = data;
			}
		}

		public class SpawnedElementWrapper
		{
			public GameObject gameObject;

			public int dataIndex;

			public SpawnedElementWrapper(GameObject go, int dataIndex)
			{
				gameObject = go;
				this.dataIndex = dataIndex;
			}
		}

		public GameObject m_Element;

		public float spacing = 10f;

		public float repeatDelayOut = 0.05f;

		public float repeatSpawnOffset = 100f;

		private List<ElementDataWrapper> m_elements;

		private List<SpawnedElementWrapper> m_spawnedElements;

		private bool m_dirty;

		private float m_elementXSize;

		private float m_xMinSide;

		private HorizontalLayoutGroup m_layoutGroup;

		private Vector3[] m_posistions = new Vector3[0];

		private int m_farLeftIndex;

		private bool m_shouldMoveToTargetPos;

		public bool m_isUnits;

		public bool m_isFactions;

		public int elementsShown => (int)(GetComponent<RectTransform>().rect.width / (m_elementXSize + spacing)) - 2;

		public event Action onUpdatedListPos;

		public event Action<UnitButton> OnButtonSpawned;

		public event Action RedrawAction;

		private void Awake()
		{
			m_elements = new List<ElementDataWrapper>();
			m_elementXSize = ((RectTransform)m_Element.transform).rect.xMax - ((RectTransform)m_Element.transform).rect.xMin;
			m_xMinSide = ((RectTransform)base.transform).sizeDelta.x / 2f;
			m_layoutGroup = GetComponent<HorizontalLayoutGroup>();
			m_layoutGroup.spacing = spacing;
		}

		public void AddElement(UnitButton.UnitButtonData unitButtonData)
		{
			m_elements.Add(new ElementDataWrapper(unitButtonData));
			m_dirty = true;
		}

		public void AddElement(FactionButton.FactionButtonData factionButtonData)
		{
			m_elements.Add(new ElementDataWrapper(factionButtonData));
			m_dirty = true;
		}

		public FactionButton GetFactionButton(int index)
		{
			if (m_spawnedElements == null)
			{
				return null;
			}
			index = Mathf.Clamp(index, 0, m_spawnedElements.Count - 1);
			return m_spawnedElements[index].gameObject.transform.GetComponentInChildren<FactionButton>();
		}

		public UnitButton GetUnitButton(DatabaseID ID)
		{
			if (m_spawnedElements == null)
			{
				return null;
			}
			for (int i = 0; i < m_spawnedElements.Count; i++)
			{
				UnitButton component = m_spawnedElements[i].gameObject.GetComponent<UnitButton>();
				if (component != null && component.UnitID == ID)
				{
					return component;
				}
			}
			return null;
		}

		public void RemoveOnRedrawAction(Action a)
		{
			RedrawAction -= a;
		}

		private void LateUpdate()
		{
			if (m_dirty)
			{
				Clear();
				SpawnObjects();
				StartCoroutine(RecalculatePosistions());
				m_dirty = false;
			}
			if (!m_shouldMoveToTargetPos || m_spawnedElements == null)
			{
				return;
			}
			for (int i = 0; i < m_spawnedElements.Count; i++)
			{
				if (m_spawnedElements[i] != null)
				{
					m_spawnedElements[i].gameObject.transform.localPosition = Vector3.Lerp(m_spawnedElements[i].gameObject.transform.localPosition, m_posistions[i], Time.unscaledDeltaTime * 7f);
				}
			}
		}

		public void SelectFirst()
		{
			if (m_spawnedElements != null)
			{
				FactionButton component = m_spawnedElements[0].gameObject.GetComponent<FactionButton>();
				if ((bool)component)
				{
					component.OnPointerEnter(null);
					component.PointerSelect();
				}
				UnitButton component2 = m_spawnedElements[0].gameObject.GetComponent<UnitButton>();
				if ((bool)component2)
				{
					component2.OnPointerEnter(null);
					component2.PointerSelect();
				}
			}
		}

		public void SetFirstFactionWhiteList()
		{
			if (m_spawnedElements != null)
			{
				FactionButton component = m_spawnedElements[0].gameObject.GetComponent<FactionButton>();
				if ((bool)component)
				{
					component.OnPointerEnter(null);
				}
			}
		}

		public void SetFirstUnitWhiteList()
		{
			UnitButton component = m_spawnedElements[0].gameObject.GetComponent<UnitButton>();
			if ((bool)component)
			{
				component.OnPointerEnter(null);
			}
		}

		public void RefreshUnitWhitelist(DatabaseID[] ids)
		{
			if (m_spawnedElements == null)
			{
				return;
			}
			for (int i = 0; i < m_spawnedElements.Count; i++)
			{
				UnitButton component = m_spawnedElements[i].gameObject.GetComponent<UnitButton>();
				bool unlocked = ExistsInWhitelist(ids, component.UnitID);
				if (ids.Length == 0)
				{
					unlocked = true;
				}
				component.SetUnlocked(unlocked, secretLocked: false);
				component.SetHighlightColor(Color.clear);
			}
		}

		public void RefreshFactionsWhitelist(DatabaseID[] ids)
		{
			if (m_spawnedElements == null)
			{
				return;
			}
			for (int i = 0; i < m_spawnedElements.Count; i++)
			{
				FactionButton component = m_spawnedElements[i].gameObject.GetComponent<FactionButton>();
				bool unlocked = ShouldShowFaction(component.m_factionID, ids);
				if (ids.Length == 0)
				{
					unlocked = true;
				}
				component.SetUnlocked(unlocked);
			}
		}

		private bool ShouldShowFaction(DatabaseID factionID, DatabaseID[] ids)
		{
			Faction faction = ContentDatabase.Instance().GetFaction(factionID);
			for (int i = 0; i < ids.Length; i++)
			{
				if (faction.DoesIncludeUnit(ids[i]))
				{
					return true;
				}
			}
			return false;
		}

		private bool ExistsInWhitelist(DatabaseID[] ids, DatabaseID id)
		{
			bool result = false;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == id)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void UpdatedListPos()
		{
			if (m_isUnits)
			{
				List<DatabaseID> list = new List<DatabaseID>();
				if (m_spawnedElements != null)
				{
					for (int i = 0; i < m_spawnedElements.Count; i++)
					{
						list.Add(m_elements[m_spawnedElements[i].dataIndex].unitData.unit.Entity.GUID);
					}
					if (m_farLeftIndex > 0)
					{
						list.Add(m_elements[m_spawnedElements[0].dataIndex - 1].unitData.unit.Entity.GUID);
					}
					if (m_farLeftIndex + m_spawnedElements.Count < m_elements.Count)
					{
						list.Add(m_elements[m_spawnedElements[0].dataIndex + 1].unitData.unit.Entity.GUID);
					}
				}
			}
			this.onUpdatedListPos?.Invoke();
		}

		private void SpawnObjects()
		{
			m_spawnedElements = new List<SpawnedElementWrapper>();
			int num = Mathf.Clamp(m_elements.Count, 0, Mathf.Min(m_elements.Count, elementsShown));
			for (int i = 0; i < num; i++)
			{
				SpawnedElementWrapper item = new SpawnedElementWrapper(SpawnObject(i, Vector3.zero), i);
				m_spawnedElements?.Add(item);
			}
			this.RedrawAction?.Invoke();
		}

		private GameObject SpawnObject(int index, Vector3 pos)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_Element, base.transform);
			IUILayoutElement component = gameObject.GetComponent<IUILayoutElement>();
			gameObject.transform.localPosition = pos;
			if (m_isUnits)
			{
				gameObject.name = m_elements[index].unitData.unit.Entity.Name;
				component.SetupUnit(m_elements[index].unitData);
			}
			if (m_isUnits && this.OnButtonSpawned != null)
			{
				this.OnButtonSpawned(gameObject.GetComponent<UnitButton>());
			}
			return gameObject;
		}

		private IEnumerator RecalculatePosistions()
		{
			m_layoutGroup.enabled = true;
			yield return null;
			m_layoutGroup.enabled = false;
			_ = Mathf.Clamp(m_elements.Count, 0, Mathf.Min(m_elements.Count, elementsShown)) % 2;
			List<Vector3> list = new List<Vector3>();
			if (m_spawnedElements != null)
			{
				for (int i = 0; i < m_spawnedElements.Count; i++)
				{
					list.Add(m_spawnedElements[i].gameObject.transform.localPosition);
				}
			}
			list?.Sort((Vector3 v1, Vector3 v2) => v1.x.CompareTo(v2.x));
			m_posistions = list.ToArray();
			UpdatedListPos();
		}

		public bool CanMoveListMore(bool increament)
		{
			if (m_spawnedElements == null)
			{
				return false;
			}
			if (increament && m_spawnedElements.Count + m_farLeftIndex == m_elements.Count)
			{
				return false;
			}
			if (!increament && m_farLeftIndex == 0)
			{
				return false;
			}
			return true;
		}

		public bool IsExpandedFaction()
		{
			if (!CanMoveListMore(increament: true))
			{
				return CanMoveListMore(increament: false);
			}
			return true;
		}

		public void MoveList(bool increament = true, int amount = 1)
		{
			if (m_spawnedElements == null)
			{
				return;
			}
			for (int i = 0; i < amount; i++)
			{
				if (!CanMoveListMore(increament))
				{
					return;
				}
				int num = 1;
				if (!increament)
				{
					num = -1;
				}
				m_farLeftIndex += num;
				Vector3 posistion = new Vector3(increament ? (-2000f) : 2000f, 0f, 0f);
				if (increament)
				{
					StartCoroutine(GoAndDie(m_spawnedElements[0].gameObject, posistion, (float)i * repeatDelayOut));
				}
				else
				{
					StartCoroutine(GoAndDie(m_spawnedElements[m_spawnedElements.Count - 1].gameObject, posistion, (float)i * repeatDelayOut));
				}
				if (increament)
				{
					for (int j = 0; j < m_spawnedElements.Count; j++)
					{
						if (j < m_spawnedElements.Count - 1)
						{
							m_spawnedElements[j] = m_spawnedElements[j + 1];
						}
						else
						{
							m_spawnedElements[j] = new SpawnedElementWrapper(SpawnObject(j + m_farLeftIndex, new Vector3(2000f + m_elementXSize * (float)i + spacing * (float)i + repeatSpawnOffset * (float)i, 0f, 0f)), j + m_farLeftIndex);
						}
					}
				}
				else
				{
					for (int num2 = m_spawnedElements.Count - 1; num2 > -1; num2--)
					{
						if (num2 > 0)
						{
							m_spawnedElements[num2] = m_spawnedElements[num2 - 1];
						}
						else
						{
							m_spawnedElements[0] = new SpawnedElementWrapper(SpawnObject(num2 + m_farLeftIndex, new Vector3(-2000f - m_elementXSize * (float)i - spacing * (float)i - repeatSpawnOffset * (float)i, 0f, 0f)), num2 + m_farLeftIndex);
						}
					}
				}
				m_shouldMoveToTargetPos = true;
			}
			this.RedrawAction?.Invoke();
			this.onUpdatedListPos?.Invoke();
		}

		private IEnumerator GoAndDie(GameObject go, Vector3 posistion, float delay = 0f)
		{
			Transform parent = go.transform.parent;
			posistion = parent.TransformPoint(posistion);
			go.transform.SetParent(go.transform.parent.parent);
			if (delay > 0f)
			{
				yield return new WaitForSeconds(delay);
			}
			float time = 1f;
			float counter = 0f;
			SimpleButton component = go.GetComponent<SimpleButton>();
			if ((bool)component)
			{
				component.DisableButton();
			}
			while (counter < time)
			{
				go.transform.position = Vector3.Lerp(go.transform.position, posistion, Time.unscaledDeltaTime * 7f);
				counter += Time.deltaTime;
				yield return null;
			}
			UnityEngine.Object.Destroy(go);
		}

		public void RemoveFactionElement(Faction f)
		{
			for (int i = 0; i < m_elements.Count; i++)
			{
				if (!m_elements[i].factionData.expandButton && m_elements[i].factionData.faction == f)
				{
					m_elements.RemoveAt(i);
					i--;
					m_dirty = true;
				}
			}
		}

		public void ClearElements()
		{
			Clear();
			m_elements.Clear();
			m_farLeftIndex = 0;
		}

		public void Clear()
		{
			if (m_spawnedElements != null)
			{
				for (int i = 0; i < m_spawnedElements.Count; i++)
				{
					UnityEngine.Object.Destroy(m_spawnedElements[i].gameObject);
				}
				m_spawnedElements.Clear();
				m_spawnedElements = null;
			}
			m_posistions = null;
			m_shouldMoveToTargetPos = false;
		}

		public RectTransform[] GetAllButtonRectTransforms()
		{
			if (m_spawnedElements != null && m_spawnedElements.Count > 0)
			{
				RectTransform[] array = new RectTransform[m_spawnedElements.Count];
				for (int i = 0; i < m_spawnedElements.Count; i++)
				{
					array[i] = m_spawnedElements[i].gameObject.transform as RectTransform;
				}
				return array;
			}
			return null;
		}
	}
}
