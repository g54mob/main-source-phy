using System;
using Poly.Base;
using Poly.UI;
using UnityEngine;

namespace Poly.Physics
{
	[CreateAssetMenu(fileName = "EdgeMaterial", menuName = "BridgePhysics/EdgeMaterial", order = 1)]
	public class EdgeMaterial : ScriptableObject
	{
		[Header("Strength")]
		[Tooltip("Maximum longitudinal force this material can bear before breaking.")]
		public float strength;

		[NonSerialized]
		public float tensionStrengthFactor = 1f;

		[Tooltip("Ropes only apply force when nominal length is exceeded, but have no effect when length is less then nominal.")]
		public bool isRope;

		[Header("Mass")]
		[Tooltip("Base mass component, independent of the edge's length.")]
		public float baseMass;

		[Tooltip("Additional mass component, scaled by edge's length.")]
		public float massPerMeter;

		[Header("Collision")]
		[Tooltip("Does it collide with spheres and rigid bodies.")]
		public bool enableCollision;

		[Tooltip("Radius or half-width of this edge.")]
		[ShowIf("enableCollision", false, false, "")]
		public float collisionRadius;

		[Tooltip("Bounciness and restitution of this material.")]
		[ShowIf("enableCollision", false, false, "")]
		public PhysicsMaterial2D physicsMaterial;

		[Header("Solver Stiffness")]
		public bool overrideSolverStiffness;

		[ShowIf("overrideSolverStiffness", false, false, "")]
		public float realativeStiffness = 1f;

		[Header("Debug View")]
		[Tooltip("Display color in physics debug view.")]
		public Color color = new Color(0f, 0f, 0f, 1f);

		public Material debugDisplayMaterial;

		public bool isSpring;

		[ShowIf("isSpring", false, false, "")]
		public float springConstant = 10f;

		[ShowIf("isSpring", false, false, "")]
		public float dampingConstant = 0.5f;

		public bool isDebris;

		public bool isPin;

		public float temp_old_baseMass = -1f;

		public float temp_old_massPerMeter = -1f;

		public void OnValidate()
		{
			if (Application.isPlaying && SingletonBehaviour<World>.instanceExists)
			{
				SingletonBehaviour<World>.instance.OnValidate();
			}
			if (temp_old_baseMass == -1f)
			{
				temp_old_baseMass = baseMass;
			}
			if (temp_old_massPerMeter == -1f)
			{
				temp_old_massPerMeter = massPerMeter;
			}
		}
	}
}
