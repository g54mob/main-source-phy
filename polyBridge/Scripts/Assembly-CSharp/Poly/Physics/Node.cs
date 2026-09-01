using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Poly.Base;
using Poly.Collide;
using Poly.Determinism;
using Poly.Extension;
using Poly.Physics.Unity;
using UnityEngine;

namespace Poly.Physics
{
	[DebuggerDisplay("#{persistentId} {name}")]
	public class Node : WorldObject, IComparable
	{
		public NodeDefinition define;

		public NodeShapeDefinition shapeDefine;

		[Header("Hydraulic Properties")]
		public bool willSplit;

		[NonSerialized]
		public NodeHandle handle;

		[NonSerialized]
		[Header("Debug View")]
		public NodeHandle handleImage;

		[NonSerialized]
		public Shape shapeImage;

		internal HashSet<Edge> edges = new HashSet<Edge>();

		[NonSerialized]
		public Node[] newSplitParts_forHydraulicsOnly = new Node[0];

		[NonSerialized]
		public bool is3WaySplit;

		[NonSerialized]
		public bool isInitialized;

		internal bool doNotDestroy;

		private bool initOnceDone;

		public static NodeHandle NodeComponent_ConstructionHandle;

		public Vec2 pos
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return handle.solverNode.pos;
			}
		}

		public Vec2 smoothPos
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vec2.LerpUnclamped(in handle.oldPos, pos, handle.world.currentFractionOfFixedFrame);
			}
		}

		public Vec2 cachedSmoothPos { get; set; }

		public bool isAddedToWorld
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if ((bool)handle)
				{
					return LoggingBehaviour.Exists(handle.world);
				}
				return false;
			}
		}

		private new void Awake()
		{
			base.Awake();
			shapeDefine.collisionRadius = CalcEffectiveRadius();
			base.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
			UpdateRendererMaterial();
			NodeHandle obj = NodeComponent_ConstructionHandle ?? NodeHandle.Create(define, shapeDefine, (Vec2)base.transform.position);
			NodeComponent_ConstructionHandle = null;
			WorldSyncUtil.BindNode(obj, this);
			InitOnce();
		}

		public void InitOnce()
		{
			if (!initOnceDone)
			{
				initOnceDone = true;
			}
		}

		public void ReInit()
		{
			WorldSyncUtil.UnbindNode(handle, this);
			WorldSyncUtil.BindNode(NodeHandle.Create(define, shapeDefine, (Vec2)base.transform.position), this);
			UpdateRendererMaterial();
		}

		public void ReInitPosition()
		{
			handle.solverNode.pos = (Vec2)base.transform.position;
			handle.oldPos = handle.solverNode.pos;
			handle.solverNode.vel = Vec2.zero;
		}

		private new void OnDestroy()
		{
			base.OnDestroy();
			edges.ToList().ForEach(delegate(Edge e)
			{
				if ((bool)e)
				{
					UnityEngine.Object.Destroy(e.gameObject);
				}
			});
			if ((bool)handle)
			{
				if (isAddedToWorld)
				{
					handle.world.RemoveNode(handle);
				}
				if ((bool)handle)
				{
					NodeHandle.DestroyNode(handle);
					WorldSyncUtil.UnbindNode(handle, this);
				}
			}
		}

		private new void OnEnable()
		{
			base.OnEnable();
			Registry<Node>.Add(this);
		}

		private new void OnDisable()
		{
			base.OnDisable();
			Registry<Node>.Remove(this);
		}

		private new void OnValidate()
		{
			base.OnValidate();
			define.mass = Mathf.Max(1E-05f, define.mass);
			if ((bool)handle)
			{
				handle.SetKinematic(define.isKinematic);
				handle.SetMass(define.mass);
			}
			if (SingletonBehaviour<VerletEditor>.instanceExists)
			{
				UpdateRendererMaterial();
			}
		}

		private float CalcEffectiveRadius()
		{
			if (shapeDefine.collisionRadius > 0f)
			{
				return shapeDefine.collisionRadius;
			}
			Vector3 lossyScale = base.transform.lossyScale;
			lossyScale = Vector3.Max(lossyScale, -lossyScale);
			return 0.5f * Mathf.Max(Mathf.Max(lossyScale.x, lossyScale.y), lossyScale.z);
		}

		public void UpdateRendererMaterial()
		{
			Material material = null;
			bool flag = (handle ? handle.isKinematic : define.isKinematic);
			material = (willSplit ? (flag ? SingletonBehaviour<VerletEditor>.instance.splitFixedNodeMaterial : SingletonBehaviour<VerletEditor>.instance.splitDynamicNodeMaterial) : (flag ? SingletonBehaviour<VerletEditor>.instance.fixedNodeMaterial : SingletonBehaviour<VerletEditor>.instance.dynamicNodeMaterial));
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sharedMaterial = material;
			}
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			Node node = obj as Node;
			if ((bool)this && (bool)node)
			{
				return Vector2Extension.CompareTo((Vector2)base.transform.position, (Vector2)node.transform.position);
			}
			return 0;
		}
	}
}
