using System;
using System.Collections.Generic;
using UnityEngine;

namespace JigglePhysics
{
	public class JiggleRigBuilder : MonoBehaviour
	{
		[Serializable]
		public class JiggleRig
		{
			[SerializeField]
			[Tooltip("The root bone from which an individual JiggleRig will be constructed. The JiggleRig encompasses all children of the specified root.")]
			private Transform rootTransform;

			[Tooltip("The settings that the rig should update with, create them using the Create->JigglePhysics->Settings menu option.")]
			public JiggleSettingsBase jiggleSettings;

			[SerializeField]
			[Tooltip("The list of transforms to ignore during the jiggle. Each bone listed will also ignore all the children of the specified bone.")]
			private List<Transform> ignoredTransforms;

			[SerializeField]
			private List<Collider> colliders;

			private bool initialized;

			[HideInInspector]
			private List<JiggleBone> simulatedPoints;

			public Transform GetRootTransform()
			{
				return rootTransform;
			}

			public JiggleRig(Transform rootTransform, JiggleSettingsBase jiggleSettings, ICollection<Transform> ignoredTransforms)
			{
				this.rootTransform = rootTransform;
				this.jiggleSettings = jiggleSettings;
				this.ignoredTransforms = new List<Transform>(ignoredTransforms);
				Initialize();
			}

			public void PrepareBone()
			{
				if (!initialized)
				{
					throw new UnityException("JiggleRig was never initialized. Please call JiggleRig.Initialize() if you're going to manually timestep.");
				}
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.PrepareBone();
				}
			}

			public void Simulate(Vector3 wind, double time)
			{
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.Simulate(jiggleSettings, wind, time, colliders);
				}
			}

			public void Initialize()
			{
				simulatedPoints = new List<JiggleBone>();
				CreateSimulatedPoints(simulatedPoints, ignoredTransforms, rootTransform, null);
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.CalculateNormalizedIndex();
				}
				initialized = true;
			}

			public void DeriveFinalSolve()
			{
				Vector3 vector = simulatedPoints[0].DeriveFinalSolvePosition(Vector3.zero, 1f);
				Vector3 offset = simulatedPoints[0].transform.position - vector;
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.DeriveFinalSolvePosition(offset, 1f);
				}
			}

			public void Pose(bool debugDraw)
			{
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.PoseBone(jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Blend));
					if (debugDraw)
					{
						simulatedPoint.DebugDraw(Color.red, Color.blue, interpolated: true);
					}
				}
			}

			public void PrepareTeleport()
			{
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.PrepareTeleport();
				}
			}

			public void FinishTeleport()
			{
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.FinishTeleport();
				}
			}

			public void OnDrawGizmos()
			{
				if (!initialized || simulatedPoints == null)
				{
					Initialize();
				}
				foreach (JiggleBone simulatedPoint in simulatedPoints)
				{
					simulatedPoint.OnDrawGizmos(jiggleSettings);
				}
			}

			private static void CreateSimulatedPoints(ICollection<JiggleBone> outputPoints, ICollection<Transform> ignoredTransforms, Transform currentTransform, JiggleBone parentJiggleBone)
			{
				JiggleBone jiggleBone = new JiggleBone(currentTransform, parentJiggleBone, currentTransform.position);
				outputPoints.Add(jiggleBone);
				if (currentTransform.childCount == 0)
				{
					if (jiggleBone.parent == null)
					{
						if (jiggleBone.transform.parent == null)
						{
							throw new UnityException("Can't have a singular jiggle bone with no parents. That doesn't even make sense!");
						}
						float num = Vector3.Distance(currentTransform.position, jiggleBone.transform.parent.position);
						Vector3 normalized = (currentTransform.position - jiggleBone.transform.parent.position).normalized;
						outputPoints.Add(new JiggleBone(null, jiggleBone, currentTransform.position + normalized * num));
					}
					else
					{
						Vector3 normalized2 = (currentTransform.position - parentJiggleBone.transform.position).normalized;
						float num2 = 0.1f;
						if (parentJiggleBone.parent != null)
						{
							num2 = Vector3.Distance(parentJiggleBone.transform.position, parentJiggleBone.parent.transform.position);
						}
						outputPoints.Add(new JiggleBone(null, jiggleBone, currentTransform.position + normalized2 * num2));
					}
					return;
				}
				for (int i = 0; i < currentTransform.childCount; i++)
				{
					if (!ignoredTransforms.Contains(currentTransform.GetChild(i)))
					{
						CreateSimulatedPoints(outputPoints, ignoredTransforms, currentTransform.GetChild(i), jiggleBone);
					}
				}
			}
		}

		[Tooltip("Enables interpolation for the simulation, this should be enabled unless you *really* need the simulation to only update on FixedUpdate.")]
		public bool interpolate = true;

		public List<JiggleRig> jiggleRigs;

		[Tooltip("An air force that is applied to the entire rig, this is useful to plug in some wind volumes from external sources.")]
		public Vector3 wind;

		[Tooltip("Draws some simple lines to show what the simulation is doing. Generally this should be disabled.")]
		[SerializeField]
		private bool debugDraw;

		private const float smoothing = 1f;

		private double accumulation;

		private void Awake()
		{
			Initialize();
		}

		public void Initialize()
		{
			accumulation = 0.0;
			if (jiggleRigs == null)
			{
				jiggleRigs = new List<JiggleRig>();
			}
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				jiggleRig.Initialize();
			}
		}

		public void Advance(float deltaTime)
		{
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				jiggleRig.PrepareBone();
			}
			accumulation = Math.Min(accumulation + (double)deltaTime, Time.fixedDeltaTime * 4f);
			while (accumulation > (double)Time.fixedDeltaTime)
			{
				accumulation -= Time.fixedDeltaTime;
				double time = (double)Time.time - accumulation;
				foreach (JiggleRig jiggleRig2 in jiggleRigs)
				{
					jiggleRig2.Simulate(wind, time);
				}
			}
			foreach (JiggleRig jiggleRig3 in jiggleRigs)
			{
				jiggleRig3.DeriveFinalSolve();
			}
			foreach (JiggleRig jiggleRig4 in jiggleRigs)
			{
				jiggleRig4.Pose(debugDraw);
			}
		}

		public JiggleRig GetJiggleRig(Transform rootTransform)
		{
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				if (jiggleRig.GetRootTransform() == rootTransform)
				{
					return jiggleRig;
				}
			}
			return null;
		}

		private void LateUpdate()
		{
			if (interpolate)
			{
				Advance(Time.deltaTime);
			}
		}

		private void FixedUpdate()
		{
			if (!interpolate)
			{
				Advance(Time.deltaTime);
			}
		}

		public void PrepareTeleport()
		{
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				jiggleRig.PrepareTeleport();
			}
		}

		public void FinishTeleport()
		{
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				jiggleRig.FinishTeleport();
			}
		}

		private void OnDrawGizmos()
		{
			foreach (JiggleRig jiggleRig in jiggleRigs)
			{
				jiggleRig.OnDrawGizmos();
			}
		}
	}
}
