using System;
using System.Collections.Generic;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	public class NodeMergeConfig
	{
		public delegate void MergeCallback(NodeHandle a, NodeHandle b, float distOverride, HashSet<NodeHandle> additionalMergedNodes);

		public bool enableJustInTimeMerging = true;

		public float progressFractionToStartMerging = 0.9f;

		[HideInInspector]
		public float maxMergeDistance = 0.25f;

		public bool usePrevClosestDistance_experimental;

		public MergeCallback mergeCallback;
	}
}
