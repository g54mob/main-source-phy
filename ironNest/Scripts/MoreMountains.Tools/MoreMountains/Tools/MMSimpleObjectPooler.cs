using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Object Pool/MMSimpleObjectPooler")]
	public class MMSimpleObjectPooler : MMObjectPooler
	{
		public GameObject GameObjectToPool;

		public int PoolSize;

		public bool PoolCanExpand;

		public virtual List<MMSimpleObjectPooler> Owner { get; set; }

		private void OnDestroy()
		{
		}

		public override void FillObjectPool()
		{
		}

		protected override string DetermineObjectPoolName()
		{
			return null;
		}

		public override GameObject GetPooledGameObject()
		{
			return null;
		}

		protected virtual GameObject AddOneObjectToThePool()
		{
			return null;
		}
	}
}
