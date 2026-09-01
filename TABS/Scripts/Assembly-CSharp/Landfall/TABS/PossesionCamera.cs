using UnityEngine;

namespace Landfall.TABS
{
	public class PossesionCamera : MonoBehaviour
	{
		public Vector3 thirdPersonOffset = new Vector3(0.6f, 0.6f, -1f);

		private Rigidbody rig;

		private Collider collider;

		private void Awake()
		{
			collider = GetComponentInChildren<Collider>(includeInactive: true);
			rig = GetComponentInParent<Rigidbody>();
		}

		public void Enter()
		{
			Vector3 centerOfMass = rig.centerOfMass;
			if ((bool)collider)
			{
				collider.gameObject.SetActive(value: true);
			}
			rig.centerOfMass = centerOfMass;
		}

		public void Leave()
		{
			if ((bool)collider)
			{
				collider.gameObject.SetActive(value: false);
			}
		}
	}
}
