using System;
using UnityEngine;

namespace LevelCreator
{
	public class DMEditorComponent : MonoBehaviour
	{
		public enum TeleportMode
		{
			TeleportNone = 0,
			TeleportPosition = 1,
			TeleportRotation = 2,
			TeleportScale = 4,
			TeleportAll = 7
		}

		public Level.Entity entity;

		private float scaleSpeed = 1f;

		private float slopeRatio = 1f;

		public Vector3 pivotOffset;

		public bool CanSimulatePhysics;

		private Rigidbody objectRigidbody;

		public float PhysicsTimer;

		private float physicsMaxDuration = float.PositiveInfinity;

		private float physicsMinDuration = 2f;

		private bool hasDirtyTransformation = true;

		public bool SimulatingPhysics { get; private set; }

		public string ObjectTypeId => entity.objectTypeId;

		public Vector3 Position
		{
			get
			{
				return entity.position;
			}
			set
			{
				entity.position = value;
				hasDirtyTransformation = true;
			}
		}

		public Quaternion Slope
		{
			get
			{
				return entity.slope;
			}
			set
			{
				entity.slope = value;
				hasDirtyTransformation = true;
			}
		}

		public Quaternion AdditionalRotation
		{
			get
			{
				return entity.rotation;
			}
			set
			{
				entity.rotation = value;
				hasDirtyTransformation = true;
			}
		}

		public Vector3 Scale
		{
			get
			{
				return entity.scale;
			}
			set
			{
				entity.scale = value;
				hasDirtyTransformation = true;
			}
		}

		public float HeightOffset
		{
			get
			{
				return entity.heightOffset;
			}
			set
			{
				entity.heightOffset = value;
				hasDirtyTransformation = true;
			}
		}

		public Vector3 CalculateLocalPosition()
		{
			return entity.position + pivotOffset + Vector3.up * entity.heightOffset;
		}

		public Quaternion CalculateFinalSlope(Quaternion slope)
		{
			return Quaternion.Lerp(Quaternion.identity, slope, slopeRatio);
		}

		public Quaternion CalculateLocalSlope()
		{
			return CalculateFinalSlope(entity.slope);
		}

		public Quaternion CalculateLocalRotation()
		{
			return CalculateLocalSlope() * entity.rotation;
		}

		public EntityTransformation GetLocalEntityTransform()
		{
			return new EntityTransformation
			{
				position = CalculateLocalPosition(),
				rotation = CalculateLocalRotation(),
				scale = entity.scale
			};
		}

		public EntityTransformation GetLocalEntityTransformWithoutSlope()
		{
			return new EntityTransformation
			{
				position = CalculateLocalPosition(),
				rotation = entity.rotation,
				scale = entity.scale
			};
		}

		public EntityTransformation GetGlobalEntityTransform()
		{
			EntityTransformation localEntityTransform = GetLocalEntityTransform();
			DMEditorComponent component = base.transform.parent.GetComponent<DMEditorComponent>();
			if (!component)
			{
				return localEntityTransform;
			}
			return component.GetGlobalEntityTransform() * localEntityTransform;
		}

		public EntityTransformation GetGlobalEntityTransformWithoutSlope()
		{
			EntityTransformation localEntityTransformWithoutSlope = GetLocalEntityTransformWithoutSlope();
			DMEditorComponent component = base.transform.parent.GetComponent<DMEditorComponent>();
			if (!component)
			{
				return localEntityTransformWithoutSlope;
			}
			return component.GetGlobalEntityTransformWithoutSlope() * localEntityTransformWithoutSlope;
		}

		public void Teleport(TeleportMode teleportMode)
		{
			if ((teleportMode & TeleportMode.TeleportPosition) != TeleportMode.TeleportNone)
			{
				base.transform.localPosition = CalculateLocalPosition();
			}
			if ((teleportMode & TeleportMode.TeleportRotation) != TeleportMode.TeleportNone)
			{
				base.transform.localRotation = CalculateLocalRotation();
			}
			if ((teleportMode & TeleportMode.TeleportScale) != TeleportMode.TeleportNone)
			{
				base.transform.localScale = entity.scale;
			}
		}

		public void Init(Guid guid, string objectTypeId, float slopeRatio, float scaleSpeed)
		{
			entity.guid = guid;
			entity.objectTypeId = objectTypeId;
			this.slopeRatio = slopeRatio;
			this.scaleSpeed = scaleSpeed;
		}

		public void SetTransform(Vector3 position, Quaternion slope, Quaternion rotation, Vector3 scale)
		{
			entity.position = position;
			entity.slope = slope;
			entity.rotation = rotation;
			entity.scale = scale;
		}

		public void FixedUpdate()
		{
			if (objectRigidbody != null && PhysicsTimer >= physicsMinDuration && (objectRigidbody.velocity.sqrMagnitude < 0.06f || objectRigidbody.IsSleeping()))
			{
				EndPhysicsSimulation();
			}
			if (SimulatingPhysics)
			{
				PhysicsTimer += Time.deltaTime;
			}
			if (PhysicsTimer >= physicsMaxDuration)
			{
				EndPhysicsSimulation();
			}
			if (SimulatingPhysics && base.transform.position.y < -100f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		public void SimulatePhysics(Action action = null, float force = 0f, bool scaleForceByMass = false, float torque = 0f, bool scaleTorqueByMass = false)
		{
			if (!objectRigidbody && CanSimulatePhysics)
			{
				SimulatingPhysics = true;
				objectRigidbody = base.gameObject.AddComponent<Rigidbody>();
				objectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				objectRigidbody.mass = GetComponentInChildren<MeshRenderer>().bounds.size.magnitude;
				objectRigidbody.AddForce(UnityEngine.Random.insideUnitSphere * force * (scaleForceByMass ? objectRigidbody.mass : 1f), ForceMode.Impulse);
				objectRigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * torque * (scaleTorqueByMass ? objectRigidbody.mass : 1f), ForceMode.Impulse);
				PhysicsTimer = 0f;
			}
		}

		public void EndPhysicsSimulation(bool snapInPlace = true)
		{
			if (SimulatingPhysics)
			{
				UnityEngine.Object.DestroyImmediate(objectRigidbody);
				if (snapInPlace)
				{
					Utility.SnapObjectAt(this, base.transform.position, TeleportMode.TeleportNone, Utility.SnapDistance.Short);
				}
				SimulatingPhysics = false;
			}
		}

		private void Update()
		{
			if (hasDirtyTransformation)
			{
				EntityTransformation localEntityTransform = GetLocalEntityTransform();
				float t = 1f - Mathf.Pow(0.001f, Time.deltaTime);
				EntityTransformation a = EntityTransformation.Lerp(new EntityTransformation
				{
					position = base.transform.localPosition,
					rotation = base.transform.localRotation,
					scale = base.transform.localScale
				}, localEntityTransform, t);
				if (EntityTransformation.AlmostSame(a, localEntityTransform))
				{
					a = localEntityTransform;
					hasDirtyTransformation = false;
				}
				base.transform.localPosition = a.position;
				base.transform.localRotation = a.rotation;
				base.transform.localScale = a.scale;
			}
		}
	}
}
