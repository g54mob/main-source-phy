using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Object Pool/MMMultipleObjectPooler")]
	public class MMMultipleObjectPooler : MMObjectPooler
	{
		[Tooltip("the list of objects to pool")]
		public List<MMMultipleObjectPoolerObject> Pool;

		[MMInformation("A MultipleObjectPooler is a reserve of objects, to be used by a Spawner. When asked, it will return an object from the pool (ideally an inactive one) chosen based on the pooling method you've chosen.\n- OriginalOrder will spawn objects in the order you've set them in the inspector (from top to bottom)\n- OriginalOrderSequential will do the same, but will empty each pool before moving to the next object\n- RandomBetweenObjects will pick one object from the pool, at random, but ignoring its pool size, each object has equal chances to get picked\n- PoolSizeBased randomly choses one object from the pool, based on its pool size probability (the larger the pool size, the higher the chances it'll get picked)'...", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("the chosen pooling method")]
		public MMPoolingMethods PoolingMethod;

		[MMInformation("If you set CanPoolSameObjectTwice to false, the Pooler will try to prevent the same object from being pooled twice to avoid repetition. This will only affect random pooling methods, not ordered pooling.", MMInformationAttribute.InformationType.Info, false)]
		[Tooltip("whether or not the same object can be pooled twice in a row. If you set CanPoolSameObjectTwice to false, the Pooler will try to prevent the same object from being pooled twice to avoid repetition. This will only affect random pooling methods, not ordered pooling.")]
		public bool CanPoolSameObjectTwice;

		[Tooltip("a unique name that should match on all MMMultipleObjectPoolers you want to use together")]
		[MMCondition("MutualizeWaitingPools", true)]
		public string MutualizedPoolName;

		[Tooltip("if CanPoolSameObjectTwice is set to false, this determines up to how many times we'll iterate to try and find a different object")]
		[MMCondition("CanPoolSameObjectTwice", true, true)]
		public float OverflowAmount;

		protected GameObject _lastPooledObject;

		protected int _currentIndex;

		protected int _currentIndexCounter;

		protected int _currentCount;

		protected string _tempSearchedName;

		public virtual List<MMMultipleObjectPooler> Owner { get; set; }

		private void OnDestroy()
		{
		}

		protected override string DetermineObjectPoolName()
		{
			return null;
		}

		public override void FillObjectPool()
		{
		}

		protected virtual GameObject AddOneObjectToThePool(GameObject typeOfObject)
		{
			return null;
		}

		public override GameObject GetPooledGameObject()
		{
			return null;
		}

		protected virtual GameObject GetPooledGameObjectOriginalOrder()
		{
			return null;
		}

		protected virtual GameObject GetPooledGameObjectOriginalOrderSequential()
		{
			return null;
		}

		protected virtual void OrderSequentialResetCounter(MMMultipleObjectPoolerObject searchedObject)
		{
		}

		protected virtual GameObject GetPooledGameObjectPoolSizeBased()
		{
			return null;
		}

		protected virtual GameObject GetPooledGameObjectRandomBetweenObjects()
		{
			return null;
		}

		public virtual GameObject GetPooledGamObjectAtIndex(int index)
		{
			return null;
		}

		public virtual GameObject GetPooledGameObjectOfType(string searchedName)
		{
			return null;
		}

		protected virtual GameObject FindInactiveObject(string searchedName, List<GameObject> list)
		{
			return null;
		}

		protected virtual GameObject FindAnyInactiveObject(List<GameObject> list)
		{
			return null;
		}

		protected virtual GameObject FindObject(string searchedName, List<GameObject> list)
		{
			return null;
		}

		protected virtual MMMultipleObjectPoolerObject GetPoolObject(GameObject testedObject)
		{
			return null;
		}

		protected virtual bool PoolObjectEnabled(GameObject testedObject)
		{
			return false;
		}

		public virtual void EnableObjects(string name, bool newStatus)
		{
		}

		public virtual void ResetCurrentIndex()
		{
		}
	}
}
