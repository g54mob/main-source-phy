using Poly.Base;
using Poly.Solver;
using Poly.UI;
using UnityEngine;

namespace Poly.Physics
{
	public abstract class Joint : WorldObject
	{
		internal short worldIdx;

		public Rigidbody connectedBody;

		public bool autoConfigureThisAnchor;

		[ShowIf("autoConfigureThisAnchor", false, true, "")]
		public bool autoConfigureConnectedAnchor = true;

		[ShowIf("autoConfigureThisAnchor", false, true, "")]
		public Vector2 anchor;

		[ShowIf("", false, false, "", runProperty = "showConnectedAnchor")]
		public Vector2 connectedAnchor;

		public InspectorButton anchorButton;

		internal Vec2 pivot;

		internal Vec2 connectedPivot;

		public World world { get; private set; }

		public bool isAddedToWorld => worldIdx >= 0;

		public bool isCustomShapeJoint { get; set; }

		public Rigidbody body { get; set; }

		internal Rigidbody body0 => body;

		internal Rigidbody body1 => connectedBody;

		public virtual void SetWorldAndIndex(World world, int index)
		{
			this.world = world;
			worldIdx = (short)index;
		}

		public Joint()
		{
			anchorButton = new InspectorButton("Init this anchor", InitThisAnchor);
		}

		protected new void Awake()
		{
			base.Awake();
			worldIdx = -1;
			body = GetComponent<Rigidbody>();
		}

		protected new void OnValidate()
		{
			base.OnValidate();
		}

		protected new void OnDestroy()
		{
			base.OnDestroy();
		}

		protected new void OnEnable()
		{
			base.OnEnable();
			Registry<Joint>.Add(this);
		}

		protected new void OnDisable()
		{
			base.OnDisable();
			Registry<Joint>.Remove(this);
		}

		public abstract void PrepForSolving(SolverSettings settings);

		public abstract void Solve(SolverSettings settings, Poly.Solver.Motion[] solverMotions);

		internal void CalcConnectedAnchor(bool reverse)
		{
			anchor *= (Vector2)body.transform.lossyScale;
			connectedAnchor *= (Vector2)connectedBody.transform.lossyScale;
			body.CacheTransform2();
			if ((bool)connectedBody)
			{
				connectedBody.CacheTransform2();
			}
			if (!reverse)
			{
				Vec2 vec = body.t2 * anchor;
				if (!connectedBody)
				{
					connectedAnchor = vec;
				}
				else
				{
					connectedAnchor = connectedBody.t2.InvMul(vec);
				}
			}
			else if ((bool)connectedBody)
			{
				anchor = body.t2.InvMul(connectedBody.t2 * connectedAnchor);
			}
			else
			{
				anchor = body.t2.InvMul(connectedAnchor);
			}
		}

		internal void CalcPivots()
		{
			body.CacheTransform2();
			if ((bool)connectedBody)
			{
				connectedBody.CacheTransform2();
			}
			pivot = body.motion.InverseTransformPoint_Slow(body.t2 * anchor);
			if ((bool)connectedBody)
			{
				connectedPivot = connectedBody.motion.InverseTransformPoint_Slow(connectedBody.t2 * connectedAnchor);
			}
			else
			{
				connectedPivot = connectedAnchor;
			}
		}

		private void InitThisAnchor()
		{
			Vector2 vector = connectedBody.transform.TransformPoint(connectedAnchor);
			anchor = base.transform.InverseTransformPoint(vector);
		}
	}
}
