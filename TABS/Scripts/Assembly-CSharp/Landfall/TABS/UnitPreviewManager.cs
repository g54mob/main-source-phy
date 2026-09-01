using System;
using System.Collections.Generic;
using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABS
{
	public class UnitPreviewManager : GameStateListener
	{
		public class CurrentUnit
		{
			public DatabaseID m_unitType;

			public GameObject[] m_spawnedObjects;
		}

		private static UnitPreviewManager instance;

		public Transform m_CameraPivot;

		public GameObject m_Plane;

		public Camera m_Camera;

		private CurrentUnit[] m_SpawnedUnits;

		private DatabaseID[] m_LastSpawnedUnits;

		private DatabaseID m_lastSelectedUnit;

		private void Start()
		{
			instance = this;
			m_SpawnedUnits = new CurrentUnit[9];
		}

		public void PrepareUnits(UnitBlueprint[] units)
		{
			List<UnitBlueprint> list = new List<UnitBlueprint>();
			for (int i = 0; i < units.Length; i++)
			{
				list.Add(units[i]);
			}
			bool[] array = new bool[m_SpawnedUnits.Length];
			array = new bool[m_SpawnedUnits.Length];
			for (int j = 0; j < m_SpawnedUnits.Length; j++)
			{
				if (m_SpawnedUnits[j] == null)
				{
					continue;
				}
				for (int k = 0; k < units.Length; k++)
				{
					if (units[k].Entity.GUID == m_SpawnedUnits[j].m_unitType)
					{
						array[j] = true;
						break;
					}
				}
				if (!array[j])
				{
					for (int l = 0; l < m_SpawnedUnits[j].m_spawnedObjects.Length; l++)
					{
						UnityEngine.Object.Destroy(m_SpawnedUnits[j].m_spawnedObjects[l]);
					}
					m_SpawnedUnits[j] = null;
					continue;
				}
				for (int m = 0; m < units.Length; m++)
				{
					if (m_SpawnedUnits[j].m_unitType == units[m].Entity.GUID)
					{
						list.Remove(units[m]);
						break;
					}
				}
			}
			Debug.Log("Units to spawm:" + list.Count);
			for (int n = 0; n < list.Count; n++)
			{
				for (int num = 0; num < m_SpawnedUnits.Length; num++)
				{
					if (m_SpawnedUnits[num] == null)
					{
						m_SpawnedUnits[num] = SpawnUnit(list[n], num);
						break;
					}
				}
			}
		}

		private CurrentUnit SpawnUnit(UnitBlueprint unit, int index)
		{
			CurrentUnit currentUnit = new CurrentUnit();
			Vector3 vector = base.transform.position + Vector3.right * 60f * index;
			List<GameObject> list = new List<GameObject>();
			list.Add(UnityEngine.Object.Instantiate(m_Plane, vector, Quaternion.identity));
			list[0].SetActive(value: true);
			GameObject[] array = unit.Spawn(vector, Quaternion.identity, Team.Red);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			currentUnit.m_spawnedObjects = list.ToArray();
			currentUnit.m_unitType = unit.Entity.GUID;
			return currentUnit;
		}

		public static void SetUnselectEvent(DatabaseID unit)
		{
		}

		private void UnselectedEvent(DatabaseID unit)
		{
			if (m_lastSelectedUnit == unit)
			{
				m_Camera.enabled = false;
				PreviewRenderer.Hide();
			}
		}

		public static void SetSelectedUnit(DatabaseID unit, Vector3 buttonPos)
		{
		}

		private void SetCamera(CurrentUnit unit)
		{
			m_CameraPivot.gameObject.SetActive(value: true);
			GameObject gameObject = unit.m_spawnedObjects[1];
			BoxCollider component = gameObject.GetComponent<BoxCollider>();
			m_CameraPivot.transform.position = gameObject.transform.TransformPoint(component.center);
			Vector3 vector = component.size * gameObject.GetComponent<Unit>().unitBlueprint.sizeMultiplier;
			float num = vector.y * gameObject.transform.localScale.y;
			if (num < vector.x * gameObject.transform.localScale.x)
			{
				num = vector.x * gameObject.transform.localScale.x;
			}
			if (num < vector.z * gameObject.transform.localScale.z)
			{
				num = vector.z * gameObject.transform.localScale.z;
			}
			float num2 = num;
			Debug.Log("frustrumHeight: " + num2);
			float num3 = num2 * 0.5f / Mathf.Tan(m_Camera.fieldOfView * 0.5f * ((float)Math.PI / 180f));
			m_Camera.transform.localPosition = new Vector3(0f, 0f, 0f - num3);
		}

		private CurrentUnit GetCurrentUnit(DatabaseID unitType)
		{
			for (int i = 0; i < m_SpawnedUnits.Length; i++)
			{
				if (m_SpawnedUnits[i].m_unitType == unitType)
				{
					return m_SpawnedUnits[i];
				}
			}
			return null;
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnEnterBattleState()
		{
			Debug.Log("PREPARING TO KILL PREVIEW UNITS!!");
			for (int i = 0; i < m_SpawnedUnits.Length; i++)
			{
				if (m_SpawnedUnits[i] != null)
				{
					for (int j = 0; j < m_SpawnedUnits[i].m_spawnedObjects.Length; j++)
					{
						UnityEngine.Object.Destroy(m_SpawnedUnits[i].m_spawnedObjects[j]);
					}
					m_SpawnedUnits[i] = null;
				}
			}
		}
	}
}
