using UnityEngine;

namespace Landfall.TABS
{
	public class SpawnFreezeBody : MonoBehaviour
	{
		public bool freezeY;

		public bool freezeRotation;

		private Rigidbody freezeBody;

		public Rigidbody FreezeBody => freezeBody;

		private void Awake()
		{
			freezeBody = GetComponent<Rigidbody>();
		}
	}
}
