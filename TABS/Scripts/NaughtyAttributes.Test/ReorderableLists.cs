using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ReorderableLists : MonoBehaviour
{
	[BoxGroup("Reorderable Lists")]
	[ReorderableList]
	public int[] intArray;

	[BoxGroup("Reorderable Lists")]
	[ReorderableList]
	public List<Vector3> vectorList;

	[BoxGroup("Reorderable Lists")]
	[ReorderableList]
	public List<SomeStruct> structList;
}
