using System.Collections.Generic;
using UnityEngine;

public class LevelGhostManager : MonoBehaviour
{
	private Dictionary<ushort, LevelGhost> ghosts;

	private Material entityMat;

	private bool isInitialized;

	protected void Awake()
	{
		ghosts = new Dictionary<ushort, LevelGhost>();
	}

	public void Clear()
	{
		if (ghosts == null || ghosts.Count == 0)
		{
			return;
		}
		foreach (ushort item in new List<ushort>(ghosts.Keys))
		{
			RemoveGhost(item);
		}
		ghosts.Clear();
	}

	public void Init(Material ghostMat)
	{
		if (!isInitialized)
		{
			entityMat = ghostMat;
			isInitialized = true;
		}
	}

	public void CreateGhost(ushort id, bool isLocalGhost)
	{
		if (!ghosts.ContainsKey(id))
		{
			GameObject gameObject = new GameObject("Ghost" + id);
			LevelGhost levelGhost = gameObject.AddComponent<LevelGhost>();
			gameObject.SetActive(false);
			levelGhost.Init(id, isLocalGhost, entityMat);
			ghosts.Add(id, levelGhost);
			gameObject.transform.SetParent(base.transform, false);
		}
	}

	public LevelPrefab GetPrefab(ushort id)
	{
		LevelGhost value;
		if (!ghosts.TryGetValue(id, out value))
		{
			Debug.Log("Can't get prefab for ghost " + id + ", doesn't exist!");
			return null;
		}
		return value.GetPrefab();
	}

	public void SetPrefab(ushort id, LevelPrefab prefab)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			value.SetPrefab(prefab);
		}
	}

	public void UpdateGhost(ushort id, byte[] data, int offset)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			value.UpdateTransform(data, offset);
		}
	}

	public void MoveGhost(ushort id, Vector3 pos, Vector3 rot, Vector3 scale)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			value.UpdateTransform(pos, rot, scale);
		}
	}

	public void Toggle(ushort id, byte[] data)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			value.Toggle(data);
		}
	}

	public void Toggle(ushort id, bool toggle, Vector3 pos)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			value.Toggle(toggle, pos);
		}
	}

	public void RemoveGhost(ushort id)
	{
		LevelGhost value;
		if (ghosts.TryGetValue(id, out value))
		{
			ghosts.Remove(id);
			if ((bool)value.gameObject)
			{
				Object.Destroy(value.gameObject);
			}
		}
	}
}
