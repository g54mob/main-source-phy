using System.Collections.Generic;
using UnityEngine;

public class MapReconClearHandle : MonoBehaviour
{
	[Tooltip("Children of this impact that are reparented into other map parents at runtime (e.g. Crater, Arrow Parent, Text Parent, Scouting Strip). Drag each split child's root GameObject here so they are destroyed together with this handle. You can also leave this empty and rely on MapReconClearChild auto-registration via Awake().")]
	[SerializeField]
	private List<GameObject> _prelinkedChildren;

	private readonly List<GameObject> _allChildren;

	private MapReconClearer _clearer;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void RegisterChild(GameObject child)
	{
	}

	public void DestroyAll()
	{
	}
}
