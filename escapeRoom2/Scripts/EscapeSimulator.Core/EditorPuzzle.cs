using System.Collections.Generic;
using UnityEngine;

public class EditorPuzzle : MonoBehaviour
{
	public string puzzleName;

	public int[] hints;

	public List<GameObject> conditions;

	public bool muteSound;

	public bool done;
}
