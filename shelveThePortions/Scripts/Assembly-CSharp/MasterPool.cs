using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPool : MonoBehaviour
{
	private readonly string PrefabsPath = "Prefabs";

	private static Transform MasterPoolTransform;

	private static Dictionary<PrefabTypes, GameObject> PrefabsReference = new Dictionary<PrefabTypes, GameObject>();

	private static Dictionary<PrefabTypes, List<GameObject>> PrefabsPools = new Dictionary<PrefabTypes, List<GameObject>>();

	private static Dictionary<PrefabTypes, Transform> PrefabsParents = new Dictionary<PrefabTypes, Transform>();

	private void Awake()
	{
		MasterPoolTransform = base.transform;
		SetPrefabsData();
	}

	private void SetPrefabsData()
	{
		PrefabsReference.Clear();
		PrefabsPools.Clear();
		PrefabsParents.Clear();
		List<GameObject> list = new List<GameObject>(Resources.LoadAll<GameObject>(PrefabsPath));
		PrefabTypes[] array = Enum.GetValues(typeof(PrefabTypes)) as PrefabTypes[];
		for (int i = 0; i < array.Length; i++)
		{
			PrefabTypes key = array[i];
			foreach (GameObject item in list)
			{
				if (string.Equals(key.ToString(), item.name) && !PrefabsReference.ContainsKey(key))
				{
					PrefabsReference.Add(key, item);
					PrefabsPools.Add(key, new List<GameObject>());
				}
			}
		}
	}

	public static GameObject GetGameObjectReference(PrefabTypes type)
	{
		if (!PrefabsReference.ContainsKey(type))
		{
			Debug.LogError("the PrefabType: (" + type.ToString() + ") don't have prefab in MasterPool, check if you have a prefabe with the same name in Resources/Prefabs");
			return null;
		}
		return PrefabsReference[type];
	}

	public static GameObject Get(PrefabTypes type)
	{
		if (!PrefabsPools.ContainsKey(type))
		{
			Debug.LogError("the PrefabType: (" + type.ToString() + ") don't have prefab in MasterPool, check if you have a prefabe with the same name in Resources/Prefabs");
			return null;
		}
		foreach (GameObject item in PrefabsPools[type])
		{
			if (!item.activeSelf)
			{
				item.SetActive(value: true);
				return item;
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(PrefabsReference[type]);
		PrefabsPools[type].Add(gameObject);
		if (!PrefabsParents.ContainsKey(type))
		{
			CreatePrefabParent(type);
		}
		gameObject.transform.SetParent(PrefabsParents[type]);
		return gameObject;
	}

	public static void CreatePrefabParent(PrefabTypes type)
	{
		GameObject gameObject = new GameObject(type.ToString());
		gameObject.transform.SetParent(MasterPoolTransform);
		PrefabsParents.Add(type, gameObject.transform);
	}

	public static GameObject Get(PrefabTypes type, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = Get(type);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = pos;
		gameObject.transform.localRotation = rotation;
		return gameObject;
	}

	public static GameObject Get(PrefabTypes type, Transform parent, bool isUIElement = false)
	{
		GameObject gameObject = Get(type);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.SetParent(parent);
		if (isUIElement)
		{
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public static GameObject Get(PrefabTypes type, Transform parent, Vector3 pos, bool isUIElement = false)
	{
		GameObject gameObject = Get(type, parent);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = pos;
		if (isUIElement)
		{
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public static GameObject Get(PrefabTypes type, Vector3 pos, bool isUIElement = false)
	{
		GameObject gameObject = Get(type);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.position = pos;
		if (isUIElement)
		{
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public static GameObject Get(PrefabTypes type, Vector3 pos, Vector3 angles, bool isUIElement = false)
	{
		GameObject gameObject = Get(type, pos);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.eulerAngles = angles;
		if (isUIElement)
		{
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public static GameObject GetPos(PrefabTypes type, Transform parent, Vector3 position = default(Vector3), Vector3 angle = default(Vector3))
	{
		GameObject gameObject = Get(type, parent);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = position;
		gameObject.transform.localEulerAngles = angle;
		return gameObject;
	}

	public static GameObject GetPos(PrefabTypes type, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
	{
		GameObject gameObject = Get(type);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = position;
		gameObject.transform.localRotation = rotation;
		return gameObject;
	}

	public static GameObject GetPos(PrefabTypes type, Vector3 position = default(Vector3))
	{
		GameObject gameObject = Get(type);
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = position;
		return gameObject;
	}

	public static GameObject CreateNew(PrefabTypes type, Transform parent = null)
	{
		if (!PrefabsReference.ContainsKey(type))
		{
			Debug.LogError("the PrefabType: (" + type.ToString() + ") don't have prefab in MasterPool, check if you have a prefabe with the same name in Resources/Prefabs");
			return null;
		}
		GameObject obj = UnityEngine.Object.Instantiate(PrefabsReference[type]);
		SetToParent(obj, type, parent);
		return obj;
	}

	private static void SetToParent(GameObject NewObj, PrefabTypes type, Transform parent = null)
	{
		if (!PrefabsParents.ContainsKey(type))
		{
			CreatePrefabParent(type);
		}
		if (parent == null)
		{
			NewObj.transform.SetParent(PrefabsParents[type]);
			return;
		}
		NewObj.transform.SetParent(parent);
		NewObj.transform.position = parent.position;
	}

	public static void ReturnToPoolTransform(GameObject usedObj, PrefabTypes type, bool setActive = false)
	{
		if (!(usedObj == null))
		{
			if (!PrefabsParents.ContainsKey(type))
			{
				CreatePrefabParent(type);
			}
			usedObj.transform.SetParent(PrefabsParents[type]);
			usedObj.SetActive(setActive);
		}
	}

	public static List<GameObject> GetAllActivePrefabType(PrefabTypes type)
	{
		List<GameObject> list = new List<GameObject>();
		if (!PrefabsParents.ContainsKey(type))
		{
			return list;
		}
		int childCount = PrefabsParents[type].childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = PrefabsParents[type].GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				list.Add(gameObject);
			}
		}
		return list;
	}

	public static void DisableAllPrefabTypeGameobjects(PrefabTypes type)
	{
		if (PrefabsParents.ContainsKey(type))
		{
			int childCount = PrefabsParents[type].childCount;
			for (int i = 0; i < childCount; i++)
			{
				PrefabsParents[type].GetChild(i).gameObject.SetActive(value: false);
			}
		}
	}

	private IEnumerator UpdateNames()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.25f);
			foreach (PrefabTypes key in PrefabsParents.Keys)
			{
				if (key != PrefabTypes.None)
				{
					UpdateNameAnalysis(key);
				}
			}
		}
	}

	private void UpdateNameAnalysis(PrefabTypes type)
	{
		int num = 0;
		int num2 = 0;
		foreach (GameObject item in PrefabsPools[type])
		{
			if (!(item == null))
			{
				if (item.gameObject.activeSelf)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		PrefabsParents[type].name = type.ToString() + " (A:" + num + " ,D:" + num2 + ")";
	}
}
