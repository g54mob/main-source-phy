using System.Collections.Generic;
using UnityEngine;

public class EditorSetup : MonoBehaviour
{
	public enum SetupDifficulty
	{
		Any = 0,
		Normal = 1,
		Hard = 2
	}

	public List<GameObject> targets = new List<GameObject>();

	public SetupDifficulty difficulty;
}
