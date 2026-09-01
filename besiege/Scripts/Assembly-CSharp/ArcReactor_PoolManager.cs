using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Arc Reactor Rays/Managers/Pooling manager")]
public class ArcReactor_PoolManager : MonoBehaviour
{
	public Dictionary<GameObject, List<ArcReactor_Arc>> freeEntities;

	public Dictionary<ArcReactor_Arc, GameObject> activeEntities;

	public static ArcReactor_PoolManager Instance { get; private set; }

	protected void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			freeEntities = new Dictionary<GameObject, List<ArcReactor_Arc>>();
			activeEntities = new Dictionary<ArcReactor_Arc, GameObject>();
		}
		else
		{
			Debug.LogError("More than one instance of ArcReactor_PoolManager is active. Disabling additional instance");
			base.enabled = false;
		}
	}

	public GameObject GetFreeEntity(GameObject originalPrefab)
	{
		if (freeEntities.ContainsKey(originalPrefab))
		{
			List<ArcReactor_Arc> list = freeEntities[originalPrefab];
			if (list.Count == 0)
			{
				GameObject gameObject = Object.Instantiate(originalPrefab);
				activeEntities.Add(gameObject.GetComponent<ArcReactor_Arc>(), originalPrefab);
				return gameObject;
			}
			ArcReactor_Arc arcReactor_Arc = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			arcReactor_Arc.EnableArc();
			arcReactor_Arc.currentlyInPool = false;
			arcReactor_Arc.elapsedTime = 0f;
			arcReactor_Arc.playBackward = false;
			arcReactor_Arc.Initialize();
			activeEntities.Add(arcReactor_Arc, originalPrefab);
			return arcReactor_Arc.gameObject;
		}
		GameObject gameObject2 = Object.Instantiate(originalPrefab);
		activeEntities.Add(gameObject2.GetComponent<ArcReactor_Arc>(), originalPrefab);
		freeEntities.Add(originalPrefab, new List<ArcReactor_Arc>());
		return gameObject2;
	}

	public void SetEntityAsFree(ArcReactor_Arc arc)
	{
		if (activeEntities.ContainsKey(arc))
		{
			arc.DisableArc();
			arc.currentlyInPool = true;
			freeEntities[activeEntities[arc]].Add(arc);
			activeEntities.Remove(arc);
		}
		else
		{
			arc.DestroyArc();
		}
	}
}
