using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMLootTable<T, V> where T : MMLoot<V>
	{
		[SerializeField]
		public List<T> ObjectsToLoot;

		[Header("Debug")]
		[MMReadOnly]
		public float WeightsTotal;

		protected float _maximumWeightSoFar;

		protected bool _weightsComputed;

		public virtual void ComputeWeights()
		{
		}

		public virtual T GetLoot()
		{
			return null;
		}
	}
}
