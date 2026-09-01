using System;
using UnityEngine;

namespace Poly.Physics
{
	[Serializable]
	public class NodeShapeDefinition
	{
		public bool enableCollision;

		public Layer layer;

		[Tooltip("If left at zero, radius is taken from Node lossyScale")]
		[SerializeField]
		public float collisionRadius;

		public PhysicsMaterial2D physicsMaterial;

		public float surfaceVelocity;

		public CollisionGroup collisionGroup = CollisionGroup.Bridge;
	}
}
