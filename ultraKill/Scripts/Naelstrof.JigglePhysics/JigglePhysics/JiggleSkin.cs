using System;
using System.Collections.Generic;
using UnityEngine;

namespace JigglePhysics
{
	public class JiggleSkin : MonoBehaviour
	{
		[Serializable]
		public class JiggleZone
		{
			[SerializeField]
			[Tooltip("The transform from which the zone effects, this is used as the 'center'.")]
			private Transform target;

			[Tooltip("How large of a radius the zone should effect, in target-space meters. (Scaling the target will effect the radius.)")]
			public float radius;

			[Tooltip("The settings that the skin should update with, create them using the Create->JigglePhysics->Settings menu option.")]
			public JiggleSettingsBase jiggleSettings;

			[HideInInspector]
			private JigglePoint simulatedPoint;

			private bool initialized;

			public void PrepareSimulate()
			{
				if (!initialized)
				{
					throw new UnityException("JiggleZone wasn't initialized, please call JiggleSkin.Initialize() or JiggleZone.Awake() before manually timestepping.");
				}
				simulatedPoint.PrepareSimulate();
			}

			public void Initialize()
			{
				simulatedPoint = new JigglePoint(target);
				initialized = true;
			}

			public Transform GetTargetBone()
			{
				return target;
			}

			public void Simulate(Vector3 wind, double time)
			{
				simulatedPoint.Simulate(jiggleSettings, wind, time);
			}

			public void DeriveFinalSolve(float smoothing)
			{
				simulatedPoint.DeriveFinalSolvePosition(smoothing);
			}

			public void DebugDraw()
			{
				simulatedPoint.DebugDraw(Color.red);
			}

			public float GetLossyScale()
			{
				return target.lossyScale.x;
			}

			public Vector3 GetPosition()
			{
				return target.position;
			}

			public Vector3 GetSolve()
			{
				return simulatedPoint.extrapolatedPosition;
			}

			public void OnDrawGizmosSelected()
			{
				if (!(target == null))
				{
					Gizmos.color = new Color(0.1f, 0.1f, 0.8f, 0.5f);
					Gizmos.DrawWireSphere(target.position, radius * target.lossyScale.x);
				}
			}
		}

		[Tooltip("Enables interpolation for the simulation, this should be enabled unless you *really* need the simulation to only update on FixedUpdate.")]
		public bool interpolate = true;

		public List<JiggleZone> jiggleZones;

		[SerializeField]
		[Tooltip("The list of skins to send the deformation data too, they should have JiggleSkin-compatible materials!")]
		public List<SkinnedMeshRenderer> targetSkins;

		[Tooltip("An air force that is applied to the entire rig, this is useful to plug in some wind volumes from external sources.")]
		public Vector3 wind;

		[SerializeField]
		[Tooltip("Draws some simple lines to show what the simulation is doing. Generally this should be disabled.")]
		private bool debugDraw;

		private double accumulation;

		private List<Material> targetMaterials;

		private List<Vector4> packedVectors;

		private int jiggleInfoNameID;

		private const float smoothing = 1f;

		private void Awake()
		{
			Initialize();
		}

		public void Initialize()
		{
			accumulation = 0.0;
			if (jiggleZones == null)
			{
				jiggleZones = new List<JiggleZone>();
			}
			foreach (JiggleZone jiggleZone in jiggleZones)
			{
				jiggleZone.Initialize();
			}
			targetMaterials = new List<Material>();
			jiggleInfoNameID = Shader.PropertyToID("_JiggleInfos");
			packedVectors = new List<Vector4>();
		}

		public JiggleZone GetJiggleZone(Transform target)
		{
			foreach (JiggleZone jiggleZone in jiggleZones)
			{
				if (jiggleZone.GetTargetBone() == target)
				{
					return jiggleZone;
				}
			}
			return null;
		}

		public void Advance(float deltaTime)
		{
			foreach (JiggleZone jiggleZone in jiggleZones)
			{
				jiggleZone.PrepareSimulate();
			}
			accumulation = Math.Min(accumulation + (double)deltaTime, Time.fixedDeltaTime * 4f);
			while (accumulation > (double)Time.fixedDeltaTime)
			{
				accumulation -= Time.fixedDeltaTime;
				double time = (double)Time.time - accumulation;
				foreach (JiggleZone jiggleZone2 in jiggleZones)
				{
					jiggleZone2.Simulate(wind, time);
				}
			}
			foreach (JiggleZone jiggleZone3 in jiggleZones)
			{
				jiggleZone3.DeriveFinalSolve(1f);
			}
			UpdateMesh();
			if (!debugDraw)
			{
				return;
			}
			foreach (JiggleZone jiggleZone4 in jiggleZones)
			{
				jiggleZone4.DebugDraw();
			}
		}

		private void LateUpdate()
		{
			if (interpolate)
			{
				Advance(Time.deltaTime);
			}
		}

		private void UpdateMesh()
		{
			packedVectors.Clear();
			foreach (SkinnedMeshRenderer targetSkin in targetSkins)
			{
				foreach (JiggleZone jiggleZone in jiggleZones)
				{
					Vector3 vector = targetSkin.rootBone.InverseTransformPoint(jiggleZone.GetPosition());
					Vector3 vector2 = targetSkin.rootBone.InverseTransformPoint(jiggleZone.GetSolve());
					packedVectors.Add(new Vector4(vector.x, vector.y, vector.z, jiggleZone.radius * jiggleZone.GetLossyScale()));
					packedVectors.Add(new Vector4(vector2.x, vector2.y, vector2.z, jiggleZone.jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Blend)));
				}
			}
			for (int i = packedVectors.Count; i < 16; i++)
			{
				packedVectors.Add(Vector4.zero);
			}
			foreach (SkinnedMeshRenderer targetSkin2 in targetSkins)
			{
				targetSkin2.GetMaterials(targetMaterials);
				foreach (Material targetMaterial in targetMaterials)
				{
					targetMaterial.SetVectorArray(jiggleInfoNameID, packedVectors);
				}
			}
		}

		private void FixedUpdate()
		{
			if (!interpolate)
			{
				Advance(Time.deltaTime);
			}
		}

		private void OnValidate()
		{
			if (jiggleZones != null)
			{
				for (int num = jiggleZones.Count - 1; num > 8; num--)
				{
					jiggleZones.RemoveAt(num);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (jiggleZones == null)
			{
				return;
			}
			Gizmos.color = new Color(0.1f, 0.1f, 0.8f, 0.5f);
			foreach (JiggleZone jiggleZone in jiggleZones)
			{
				jiggleZone.OnDrawGizmosSelected();
			}
		}

		public Vector3 ApplyJiggle(Vector3 toPoint, float blend)
		{
			Vector3 result = toPoint;
			foreach (JiggleZone jiggleZone in jiggleZones)
			{
				jiggleZone.DeriveFinalSolve(1f);
				Vector3 vector = targetSkins[0].rootBone.InverseTransformPoint(jiggleZone.GetPosition());
				Vector3 vector2 = targetSkins[0].rootBone.InverseTransformPoint(jiggleZone.GetSolve()) - vector;
				float t = Vector3.Distance(vector, targetSkins[0].rootBone.InverseTransformPoint(toPoint));
				Mathf.SmoothStep(0f, jiggleZone.radius * jiggleZone.GetLossyScale(), t);
				result += targetSkins[0].rootBone.TransformVector(vector2) * jiggleZone.jiggleSettings.GetParameter(JiggleSettingsBase.JiggleSettingParameter.Blend) * blend;
			}
			return result;
		}
	}
}
