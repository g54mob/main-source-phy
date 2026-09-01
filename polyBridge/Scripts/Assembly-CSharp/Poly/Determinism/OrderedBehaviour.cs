using Poly.Base;
using UnityEngine;

namespace Poly.Determinism
{
	public class OrderedBehaviour : PolyBehaviour
	{
		[SerializeField]
		private int _persistentId = -1;

		public int persistentId => _persistentId;

		public string nameWithId => base.name + "#" + persistentId;

		protected void Awake()
		{
			VerifyOrGetNewId();
		}

		protected void OnValidate()
		{
			VerifyOrGetNewId();
		}

		public void VerifyOrGetNewId()
		{
			_persistentId = Singleton<PersistentIdRegistry<OrderedBehaviour>, int>.instance.VerifyOrGetNewId(this, _persistentId);
		}
	}
}
