using System;
using System.Collections.Generic;
using Poly.Base;
using UnityEngine;

namespace Poly.Physics.Unity
{
	public static class WorldSyncUtil
	{
		public static Node CreateAndBindNodeObject(NodeHandle handle, Node nodePrefab, Transform parent = null)
		{
			return CreateAndBindNodeObject(handle.pos, nodePrefab, parent, "Node_Handle", handle);
		}

		public static Node CreateAndBindNodeObject(Vector2 position, Node nodePrefab, Transform parent = null, string name = "Node_Handle", NodeHandle handle = null)
		{
			Node.NodeComponent_ConstructionHandle = handle;
			Node node = UnityEngine.Object.Instantiate(nodePrefab);
			node.transform.parent = parent;
			node.gameObject.name = name;
			node.transform.position = SingletonBehaviour<World>.instance.transform.TransformPoint(position);
			node.transform.hasChanged = false;
			return node;
		}

		public static Edge _CreateEdge(Node a, Node b, EdgeMaterial material, Edge edgePrefab, Transform parent = null, string name = "Pin-Edge")
		{
			Edge edge = UnityEngine.Object.Instantiate(edgePrefab);
			edge.transform.parent = parent ?? a.transform.parent;
			edge.gameObject.name = name;
			if ((bool)material)
			{
				edge.material = material;
			}
			edge.InitBeforeStart(a, b);
			return edge;
		}

		public static Edge CreateAndBindGameObject(EdgeHandle edgeHandle, Edge edgePrefab, Transform container)
		{
			Edge edge = _CreateEdge(edgeHandle.node0.unityNodeComponent, edgeHandle.node1.unityNodeComponent, edgeHandle.material, edgePrefab, container, "Edge_Handle");
			Bind(edgeHandle, edge);
			return edge;
		}

		public static void TryDestroyGameObject(EdgeHandle edge)
		{
			Edge unityEdgeComponent = edge.unityEdgeComponent;
			if ((bool)unityEdgeComponent)
			{
				Unbind(edge, unityEdgeComponent);
				UnityEngine.Object.Destroy(unityEdgeComponent.gameObject);
			}
		}

		public static void BindNode(NodeHandle handle, Node node)
		{
			handle.unityNodeComponent = node;
			node.handle = handle;
		}

		public static void UnbindNode(NodeHandle handle, Node node)
		{
			handle.unityNodeComponent = null;
			node.handle = null;
		}

		public static void Bind(EdgeHandle handle, Edge edge)
		{
			handle.unityEdgeComponent = edge;
			edge.handle = handle;
		}

		public static void Unbind(EdgeHandle handle, Edge edge)
		{
			handle.unityEdgeComponent = null;
			edge.handle = null;
		}

		public static void NonFixed_UpdateUnityNodes(List<NodeHandle> nodeHandles, Vector3 visualOffset)
		{
			foreach (NodeHandle nodeHandle in nodeHandles)
			{
				if ((bool)nodeHandle && (bool)nodeHandle.unityNodeComponent)
				{
					Transform transform = nodeHandle.unityNodeComponent.transform;
					transform.position = nodeHandle.solverNode.pos + visualOffset;
					transform.hasChanged = false;
					nodeHandle.unityNodeComponent.handleImage = nodeHandle.unityNodeComponent.handle;
					if (nodeHandle.shapeHandleIndex.isValid)
					{
						nodeHandle.unityNodeComponent.shapeImage = nodeHandle.shapeHandleIndex.Get().shape;
					}
					nodeHandle.unityNodeComponent.define.isKinematic = nodeHandle.isKinematic;
					nodeHandle.unityNodeComponent.define.mass = nodeHandle.mass;
					if (nodeHandle.shapeHandleIndex.isValid)
					{
						nodeHandle.unityNodeComponent.shapeDefine.enableCollision = true;
						nodeHandle.unityNodeComponent.shapeDefine.layer = nodeHandle.shapeHandleIndex.Get().layer;
						nodeHandle.unityNodeComponent.shapeDefine.collisionGroup = (CollisionGroup)nodeHandle.shapeHandleIndex.Get().collisionGroup;
						nodeHandle.unityNodeComponent.shapeDefine.collisionRadius = nodeHandle.shapeHandleIndex.Get().shape.radius;
					}
					else
					{
						nodeHandle.unityNodeComponent.shapeDefine.enableCollision = false;
					}
				}
			}
		}

		public static void LateUpdateUnityEdges(List<EdgeHandle> edgeHandles, bool updateDataImagesInUnityComponents)
		{
			Camera main = Camera.main;
			bool showEdges = SingletonBehaviour<World>.instance.showEdges;
			int count = edgeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				EdgeHandle edgeHandle = edgeHandles[i];
				if (!edgeHandle.unityEdgeComponent)
				{
					continue;
				}
				edgeHandle.unityEdgeComponent.LateUpdate_Manual(showEdges, main);
				Edge unityEdgeComponent = edgeHandle.unityEdgeComponent;
				if (updateDataImagesInUnityComponents)
				{
					unityEdgeComponent.handleImage = unityEdgeComponent.handle;
					if (edgeHandle.shapeHandleIndex.isValid)
					{
						unityEdgeComponent.shapeImage = edgeHandle.shapeHandleIndex.Get().shape;
					}
				}
			}
		}

		public static void FixedUpdateUnityEdges(List<EdgeHandle> edgeHandles)
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			int count = edgeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				Edge unityEdgeComponent = edgeHandles[i].unityEdgeComponent;
				if ((bool)unityEdgeComponent)
				{
					unityEdgeComponent.PostFixedUpdate_Manual(fixedDeltaTime);
				}
			}
		}

		public static void FixedUpdateFromUnityNodes(List<NodeHandle> nodeHandles, Vector3 visualOffset)
		{
			int count = nodeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				NodeHandle nodeHandle = nodeHandles[i];
				bool flag = false;
				if (!nodeHandle.unityNodeComponent || !(nodeHandle.unityNodeComponent.transform.hasChanged || flag))
				{
					continue;
				}
				Transform transform = nodeHandle.unityNodeComponent.transform;
				if (transform.hasChanged)
				{
					Debug.Log("Updating node position from editor or external scripts. Slow");
					transform.hasChanged = false;
					_ = nodeHandle.solverNode.vel != Vec2.zero;
					nodeHandle.solverNode.pos = (Vec2)(transform.position - visualOffset);
					nodeHandle.CacheTransform2InShapeHandles_Util();
					foreach (EdgeHandle edge in nodeHandle.edges)
					{
						edge.CacheTransform2InShapeHandles_Util();
					}
				}
				nodeHandle.solverNode.vel = Vec2.zero;
			}
		}

		public static void FixedUpdateFromUnityBodies(List<Rigidbody> bodies, Vector3 visualOffset)
		{
			int count = bodies.Count;
			for (int i = 0; i < count; i++)
			{
				Rigidbody rigidbody = bodies[i];
				bool flag = false;
				if ((bool)rigidbody && (rigidbody.transform.hasChanged || flag))
				{
					Transform transform = rigidbody.transform;
					if (transform.hasChanged)
					{
						Debug.Log("Updating body position from editor or external scripts.");
						transform.hasChanged = false;
						_ = rigidbody.motion.linVel != Vec2.zero;
						rigidbody.motion.com = (Vec2)(transform.position - visualOffset) - rigidbody.engine_comTbody;
						rigidbody.motion.angle = transform.eulerAngles.z * (MathF.PI / 180f);
						rigidbody.CacheTransform2();
						rigidbody.CacheTransform2InShapeHandles_Util();
					}
					rigidbody.motion.linVel = Vec2.zero;
					rigidbody.motion.angVel = 0f;
				}
			}
		}
	}
}
