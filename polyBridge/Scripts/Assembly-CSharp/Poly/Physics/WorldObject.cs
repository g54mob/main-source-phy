using Poly.Determinism;
using UnityEngine;

namespace Poly.Physics
{
	public class WorldObject : LoggingBehaviour
	{
		[HideInInspector]
		public object userData;

		public bool isRegistered { get; set; }
	}
}
