using System;
using Poly.Collide;
using Poly.Math;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics
{
	public struct ShapeHandle
	{
		public WorldObjectImpl entityHandle;

		public Shape shape;

		public Transform2 t2;

		public Vec2 fastLinearVel;

		public short nodeIdx;

		public short motionIdx;

		public short collisionGroup;

		public Layer layer;

		public bool isTrigger;

		public RecollisionType recollisionType;

		public const short InvalidIdx = short.MinValue;

		public IEntity entity { get; set; }

		public static ShapeHandle Create()
		{
			return new ShapeHandle
			{
				t2 = Transform2.identity,
				nodeIdx = short.MinValue,
				motionIdx = short.MinValue
			};
		}

		public Aabb GetAabb_unused(float padding)
		{
			return shape.GetAabb(ref t2, padding);
		}

		public void CacheTransform2(float nodeToMotionVelocityMultiplier)
		{
			if (entityHandle is NodeHandle)
			{
				NodeHandle nodeHandle = (NodeHandle)entityHandle;
				t2.position = nodeHandle.solverNode.pos;
				fastLinearVel = nodeHandle.solverNode.vel * nodeToMotionVelocityMultiplier;
			}
			else if (entityHandle is EdgeHandle)
			{
				EdgeHandle edgeHandle = (EdgeHandle)entityHandle;
				NodeHandle node = edgeHandle.node0;
				NodeHandle node2 = edgeHandle.node1;
				t2.position = 0.5f * (node.solverNode.pos + node2.solverNode.pos);
				fastLinearVel = 0.5f * (node.solverNode.vel + node2.solverNode.vel) * nodeToMotionVelocityMultiplier;
				Vec2 vec = node2.solverNode.pos - node.solverNode.pos;
				float magnitude = vec.magnitude;
				t2.rotation.basisX = vec / (magnitude + 5.877472E-39f);
				((Segment)shape).halfLengthX = 0.5f * magnitude;
				if ((short)edgeHandle.shapeHandleIndex >= 0)
				{
					float comT = edgeHandle.optional_motion.segment.comT;
					Vec2 rotated = vec.rotated90;
					edgeHandle.optional_motion.segment.angleToNode0 = (0f - comT) * rotated;
					edgeHandle.optional_motion.segment.angleToNode1 = (1f - comT) * rotated;
					edgeHandle.optional_motion.segment.currentStretchedLength = magnitude;
					Poly.Solver.Motion.ConvertNodesToMotion_OutsideSolver(ref node.solverNode, ref node2.solverNode, ref edgeHandle.optional_motion, nodeToMotionVelocityMultiplier);
				}
			}
			else if (entity != null)
			{
				Rigidbody rigidbody = (Rigidbody)entity;
				rigidbody.CacheTransform2();
				t2 = rigidbody.t2;
				fastLinearVel = rigidbody.motion.linVel;
			}
		}

		internal Vec2 GetComTBody_InCollide()
		{
			Vec2 result = Vec2.zero;
			if (!(entityHandle is NodeHandle))
			{
				if (entityHandle is EdgeHandle)
				{
					EdgeHandle edgeHandle = (EdgeHandle)entityHandle;
					result.x = (0f - (edgeHandle.optional_motion.segment.comT - 0.5f)) * edgeHandle.optional_motion.segment.currentStretchedLength;
				}
				else if (entity != null)
				{
					result = ((Rigidbody)entity).engine_comTbody;
				}
			}
			return result;
		}

		[Obsolete]
		public void CacheTransform2_InRecalculateFull(SolverNode[] nodesPtr, Poly.Solver.Motion[] motionsPtr, bool warpBodyMotionForward, out Vec2 displacement)
		{
			displacement = Vec2.zero;
			if (entityHandle is NodeHandle)
			{
				t2.position = nodesPtr[((NodeHandle)entityHandle).worldIdx].pos;
			}
			else if (entityHandle is EdgeHandle)
			{
				EdgeHandle obj = (EdgeHandle)entityHandle;
				NodeHandle node = obj.node0;
				NodeHandle node2 = obj.node1;
				Vec2 pos = nodesPtr[node.worldIdx].pos;
				Vec2 pos2 = nodesPtr[node2.worldIdx].pos;
				t2.position = 0.5f * (pos + pos2);
				Vec2 vec = pos2 - pos;
				t2.rotation.basisX = vec.normalized;
			}
			else if (entity != null)
			{
				Rigidbody rigidbody = (Rigidbody)entity;
				rigidbody.CacheTransform2_InSolver(in motionsPtr[rigidbody.worldIdx]);
				t2 = rigidbody.t2;
				if (warpBodyMotionForward)
				{
					displacement = motionsPtr[rigidbody.worldIdx].linVel;
					t2.position += motionsPtr[rigidbody.worldIdx].linVel;
				}
			}
		}

		public void Dispose()
		{
			entityHandle = null;
			entity = null;
			shape = null;
		}

		public Component GetUnityComponent()
		{
			Component result = null;
			if (entityHandle is NodeHandle)
			{
				result = ((NodeHandle)entityHandle).unityNodeComponent;
			}
			else if (entityHandle is EdgeHandle)
			{
				result = ((EdgeHandle)entityHandle).unityEdgeComponent;
			}
			else if (entity != null)
			{
				result = (Rigidbody)entity;
			}
			return result;
		}
	}
}
