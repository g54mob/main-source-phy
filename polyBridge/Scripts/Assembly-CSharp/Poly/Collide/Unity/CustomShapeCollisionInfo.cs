using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poly.Base;
using Poly.Extension;
using Poly.Geometry;
using Poly.Math;
using Poly.Physics;
using Poly.Solver;
using UnityEngine;

namespace Poly.Collide.Unity
{
	public class CustomShapeCollisionInfo : PolyBehaviour
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[HideInInspector]
		internal Poly.Solver.Motion? validate_motion;

		private const float motorStrengthToTroque = 40f;

		private static PhysicsMaterial2D lowFrictionPhysicsMaterial;

		public float minStrengthForDesiredAcceleration { get; private set; }

		public void OnAddedToWorld()
		{
			CustomShape component = GetComponent<CustomShape>();
			bool flag = component.IsDynamic();
			GameObject gameObject = CreateRigidBodyInPhysics();
			if (!gameObject)
			{
				return;
			}
			Poly.Physics.Rigidbody component2 = gameObject.GetComponent<Poly.Physics.Rigidbody>();
			component2._mass = (flag ? Mathf.Max(0.1f, component.m_Mass) : 0f);
			float num = 0f;
			int count = component.m_Anchors.Count;
			if (flag && 0 < count)
			{
				float mass = component2._mass;
				float num2 = 0.1f;
				component2._mass = (1f - num2) * mass;
				num = mass * num2 / (float)count;
				component2.anchorsWithBorrowedMass = new List<Poly.Physics.Rigidbody.AnchorInfo>();
			}
			if (!flag)
			{
				return;
			}
			if (component.m_Pins.Count == 1)
			{
				Poly.Physics.Rigidbody rigidbody = new GameObject("Fixed RB for Joint").AddComponent<Poly.Physics.Rigidbody>();
				rigidbody._mass = 0f;
				WheelJoint wheelJoint = gameObject.AddComponent<WheelJoint>();
				wheelJoint.autoConfigureThisAnchor = true;
				wheelJoint.connectedBody = rigidbody;
				wheelJoint.isCustomShapeJoint = true;
				CustomShapePin customShapePin = component.m_Pins[0];
				wheelJoint.connectedAnchor = customShapePin.transform.position;
				wheelJoint.isCustomShape = true;
				if (component.m_PinMotorStrength != 0f && component.m_Behavior == CustomShapeBehavior.MOTORIZED)
				{
					wheelJoint.enableMotor = true;
					wheelJoint.useSimpleVelocityMotor = true;
					wheelJoint.maxMotorTorque = Mathf.Abs(component.m_PinMotorStrength) * 40f;
					wheelJoint.targetMotorVelocity = 0f - component.m_PinTargetVelocity;
					wheelJoint.desiredAcceleration_ForSimpleMotor = component.GetDesiredAcceleration();
				}
				else
				{
					wheelJoint.applyAngularFriction = true;
				}
			}
			Transform2 inverse_unoptimized = ((Transform2)component2.transform).inverse_unoptimized;
			foreach (CustomShapeAnchor anchor in component.m_Anchors)
			{
				DynamicAnchorJoint dynamicAnchorJoint = gameObject.AddComponent<DynamicAnchorJoint>();
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(anchor.m_BridgeJointGuid);
				if ((bool)bridgeJoint)
				{
					dynamicAnchorJoint.connectedNode = bridgeJoint.m_PhysicsNode;
					dynamicAnchorJoint.connectedNode.define.isKinematic = false;
					dynamicAnchorJoint.connectedNode.handle.SetKinematic(isKinematic: false);
					dynamicAnchorJoint.connectedNode.handle.isSplittableAnchor = true;
					if (num != 0f)
					{
						dynamicAnchorJoint.connectedNode.define.mass = num;
						dynamicAnchorJoint.connectedNode.handle.SetMass(num);
						component2.anchorsWithBorrowedMass.Add(new Poly.Physics.Rigidbody.AnchorInfo
						{
							localPosition = inverse_unoptimized * (Vec2)bridgeJoint.transform.position,
							mass = num
						});
					}
				}
			}
			component.m_PhysicsBodyIfDynamic = component2;
		}

		public PolygonShape[] CreatePolygonShapes_ForBuildMode(bool calculateMinimumStrengthHint)
		{
			List<PolygonShape> list = new List<PolygonShape>();
			GameObject gameObject = CreateRigidBodyInPhysics();
			if ((bool)gameObject)
			{
				PolygonCollider[] componentsInChildren = gameObject.GetComponentsInChildren<PolygonCollider>();
				foreach (PolygonCollider polygonCollider in componentsInChildren)
				{
					list.AddRange(polygonCollider.CreateConvexPolygons(in Transform2.identity));
				}
				if (calculateMinimumStrengthHint)
				{
					CalculateMinimumStrengthHint(gameObject.GetComponent<Poly.Physics.Rigidbody>());
				}
				UnityEngine.Object.Destroy(gameObject);
			}
			return list.ToArray();
		}

		private GameObject CreateRigidBodyInPhysics()
		{
			CustomShape component = GetComponent<CustomShape>();
			Layer layerForCustomShape = GetLayerForCustomShape(component);
			Vector2[] verticesInWorldSpace = component.GetVerticesInWorldSpace();
			return CreateRigidBodyWithShape(verticesInWorldSpace, layerForCustomShape);
		}

		private Layer GetLayerForCustomShape(CustomShape customShape)
		{
			Layer result = Layer.CustomShape;
			bool num = customShape.IsDynamic();
			bool collidesWithNodes = customShape.m_CollidesWithNodes;
			bool collidesWithRoad = customShape.m_CollidesWithRoad;
			bool collidesWithRamps = customShape.m_CollidesWithRamps;
			bool collidesWithVehicles = customShape.m_CollidesWithVehicles;
			if (num)
			{
				if (!collidesWithNodes && !collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape;
				}
				else if (collidesWithNodes && !collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNode;
				}
				else if (!collidesWithNodes && collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRoad;
				}
				else if (collidesWithNodes && collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRoad;
				}
				else if (!collidesWithNodes && !collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRamp;
				}
				else if (collidesWithNodes && !collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRamp;
				}
				else if (!collidesWithNodes && collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRoadAndRamp;
				}
				else if (collidesWithNodes && collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRoadAndRamp;
				}
				else if (!collidesWithNodes && !collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsVehicles;
				}
				else if (collidesWithNodes && !collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndVehicles;
				}
				else if (!collidesWithNodes && collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRoadAndVehicles;
				}
				else if (collidesWithNodes && collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRoadAndVehicles;
				}
				else if (!collidesWithNodes && !collidesWithRoad && collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRampAndVehicles;
				}
				else if (collidesWithNodes && !collidesWithRoad && collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRampAndVehicles;
				}
				else if (!collidesWithNodes && collidesWithRoad && collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsRoadAndRampAndVehicles;
				}
				else if (collidesWithNodes && collidesWithRoad && collidesWithRamps && collidesWithVehicles)
				{
					result = Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles;
				}
			}
			else if (!collidesWithNodes && !collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape;
			}
			else if (collidesWithNodes && !collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNode;
			}
			else if (!collidesWithNodes && collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRoad;
			}
			else if (collidesWithNodes && collidesWithRoad && !collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRoad;
			}
			else if (!collidesWithNodes && !collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRamp;
			}
			else if (collidesWithNodes && !collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRamp;
			}
			else if (!collidesWithNodes && collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRoadAndRamp;
			}
			else if (collidesWithNodes && collidesWithRoad && collidesWithRamps && !collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp;
			}
			else if (!collidesWithNodes && !collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsVehicles;
			}
			else if (collidesWithNodes && !collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndVehicles;
			}
			else if (!collidesWithNodes && collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRoadAndVehicles;
			}
			else if (collidesWithNodes && collidesWithRoad && !collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles;
			}
			else if (!collidesWithNodes && !collidesWithRoad && collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRampAndVehicles;
			}
			else if (collidesWithNodes && !collidesWithRoad && collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles;
			}
			else if (!collidesWithNodes && collidesWithRoad && collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles;
			}
			else if (collidesWithNodes && collidesWithRoad && collidesWithRamps && collidesWithVehicles)
			{
				result = Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles;
			}
			return result;
		}

		private bool VerifyAndFixVerts(Vector2[] pointsInWorld)
		{
			WindingDirection windingDirection = Poly.Geometry.Geometry.GetWindingDirection(pointsInWorld);
			if (windingDirection == WindingDirection.CounterClockWise)
			{
				Array.Reverse(pointsInWorld);
			}
			if (windingDirection == WindingDirection.Invalid)
			{
				return false;
			}
			return true;
		}

		private GameObject CreateRigidBodyWithShape(Vector2[] pointsInWorld, Layer collisionLayer)
		{
			if (!VerifyAndFixVerts(pointsInWorld))
			{
				UnityEngine.Debug.Log("Can't build Custom Shape from given vertices.");
				return null;
			}
			CustomShape component = GetComponent<CustomShape>();
			GameObject gameObject = new GameObject("RB CustomShape");
			Transform obj = gameObject.transform;
			obj.parent = base.transform.parent;
			obj.position = base.transform.position;
			obj.rotation = base.transform.rotation;
			Poly.Physics.Rigidbody rigidbody = gameObject.AddComponent<Poly.Physics.Rigidbody>();
			rigidbody._mass = 0f;
			rigidbody.requestFullRecollision = false;
			rigidbody._mass = (component.IsDynamic() ? Mathf.Max(0.1f, component.m_Mass) : 0f);
			rigidbody.validate_CustomShapeCollisionInfo = this;
			GameObject obj2 = new GameObject("PolyCollider");
			Transform obj3 = obj2.transform;
			obj3.parent = gameObject.transform;
			obj3.SetLocalTransformToIdentity();
			PolygonCollider polygonCollider = obj2.AddComponent<PolygonCollider>();
			polygonCollider.layer = collisionLayer;
			if (component.m_LowFriction)
			{
				if (!lowFrictionPhysicsMaterial)
				{
					lowFrictionPhysicsMaterial = new PhysicsMaterial2D("CustomShape-LowFriction");
					lowFrictionPhysicsMaterial.friction = 0.1f;
					lowFrictionPhysicsMaterial.bounciness = 0f;
				}
				polygonCollider.physicsMaterial = lowFrictionPhysicsMaterial;
			}
			for (int i = 0; i < pointsInWorld.Length; i++)
			{
				Transform obj4 = new GameObject($"Vert {i + 2:00}").transform;
				obj4.parent = polygonCollider.transform;
				obj4.position = pointsInWorld[i];
			}
			return gameObject;
		}

		private void CalculateMinimumStrengthHint(Poly.Physics.Rigidbody body)
		{
			CustomShape component = GetComponent<CustomShape>();
			Vec2 a = (Vec2)component.m_Pins[0].transform.position;
			float desiredAcceleration = component.GetDesiredAcceleration();
			if (0f == desiredAcceleration || float.MaxValue == desiredAcceleration)
			{
				minStrengthForDesiredAcceleration = 0f;
				return;
			}
			World instance = SingletonBehaviour<World>.instance;
			Poly.Solver.Motion value = Poly.Physics.Rigidbody.CalculateFinalMassInertiaAndCom(body, (Vec2)instance.transform.position);
			validate_motion = value;
			float num = Vec2.Distance(in a, in value.com);
			float num2 = 1f / value.invMass;
			float num3 = 1f / value.invInertia + num2 * num * num;
			float num4 = num2 * num * instance.settings.gravity.magnitude;
			float num5 = desiredAcceleration * (MathF.PI / 180f) * num3 + num4;
			minStrengthForDesiredAcceleration = num5 / 40f;
		}
	}
}
