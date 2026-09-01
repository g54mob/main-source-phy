using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class BounceRocks : MonoBehaviour
	{
		public List<Rigidbody> Rocks;

		public Vector3 MinForce;

		public Vector3 MaxForce;

		public Vector3 MinTorque;

		public Vector3 MaxTorque;

		protected Vector3 _force;

		protected Vector3 _torque;

		public virtual void Bounce()
		{
		}
	}
}
