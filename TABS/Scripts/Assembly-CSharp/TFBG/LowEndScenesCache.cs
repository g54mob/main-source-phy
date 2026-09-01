using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TFBG
{
	[CreateAssetMenu(fileName = "LowEndScenesCache", menuName = "TABS/LowEndScenesCache")]
	public class LowEndScenesCache : ScriptableObject
	{
		[Serializable]
		private class StandardLowEndScenePair
		{
			public UnityEngine.Object StandardScene;

			public UnityEngine.Object LowEndScene;

			public StandardLowEndScenePair(UnityEngine.Object standardScene, UnityEngine.Object lowEndScene)
			{
				StandardScene = standardScene;
				LowEndScene = lowEndScene;
			}
		}

		private const string LowEndAssetSuffix = "_Optimized";

		[FormerlySerializedAs("StandardLowEndPairs")]
		[SerializeField]
		private List<StandardLowEndScenePair> StandardLowSceneEndPairs = new List<StandardLowEndScenePair>();

		private Dictionary<UnityEngine.Object, UnityEngine.Object> sceneMap = new Dictionary<UnityEngine.Object, UnityEngine.Object>();

		private int count;

		private int total;
	}
}
