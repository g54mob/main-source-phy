using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFBG
{
	[CreateAssetMenu(fileName = "LowEndCache", menuName = "TABS/LowEndCache")]
	public class LowEndCache : ScriptableObject
	{
		[Serializable]
		private class StandardLowEndPair
		{
			public GameObject StandardPrefab;

			public GameObject LowEndPrefab;

			public StandardLowEndPair(GameObject standardPrefab, GameObject lowEndPrefab)
			{
				StandardPrefab = standardPrefab;
				LowEndPrefab = lowEndPrefab;
			}
		}

		private const string LowEndAssetSuffix = "_LE";

		[SerializeField]
		private List<StandardLowEndPair> StandardLowEndPairs = new List<StandardLowEndPair>();

		private Dictionary<GameObject, GameObject> prefabMap = new Dictionary<GameObject, GameObject>();

		private int count;

		private int total;
	}
}
