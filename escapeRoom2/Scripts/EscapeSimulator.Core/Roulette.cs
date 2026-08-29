using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
	public List<GameObject> targets = new List<GameObject>();

	public bool targetCanBeActivatedOnlyOnce;

	public int testingTarget = -1;

	public List<int> indexHistory;
}
