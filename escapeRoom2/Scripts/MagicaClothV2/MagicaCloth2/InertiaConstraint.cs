using System;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace MagicaCloth2
{
	public class InertiaConstraint : IDisposable
	{
		public enum TeleportMode
		{
			None = 0,
			Reset = 1,
			Keep = 2
		}

		[Serializable]
		public class SerializeData : IDataValidate
		{
			public Transform anchor;

			[Range(0f, 1f)]
			public float anchorInertia;

			[FormerlySerializedAs("movementInertia")]
			[Range(0f, 1f)]
			public float worldInertia;

			[Range(0f, 1f)]
			public float movementInertiaSmoothing;

			public CheckSliderSerializeData movementSpeedLimit;

			public CheckSliderSerializeData rotationSpeedLimit;

			[Range(0f, 1f)]
			public float localInertia;

			public CheckSliderSerializeData localMovementSpeedLimit;

			public CheckSliderSerializeData localRotationSpeedLimit;

			[Range(0f, 1f)]
			public float depthInertia;

			[Range(0f, 1f)]
			public float centrifualAcceleration;

			public CheckSliderSerializeData particleSpeedLimit;

			public TeleportMode teleportMode;

			public float teleportDistance;

			public float teleportRotation;

			public SerializeData()
			{
				anchor = null;
				anchorInertia = 0f;
				worldInertia = 1f;
				movementInertiaSmoothing = 0.4f;
				movementSpeedLimit = new CheckSliderSerializeData(use: true, 5f);
				rotationSpeedLimit = new CheckSliderSerializeData(use: true, 720f);
				localInertia = 1f;
				localMovementSpeedLimit = new CheckSliderSerializeData(use: false, 5f);
				localRotationSpeedLimit = new CheckSliderSerializeData(use: false, 720f);
				depthInertia = 0f;
				centrifualAcceleration = 0f;
				particleSpeedLimit = new CheckSliderSerializeData(use: true, 4f);
				teleportMode = TeleportMode.None;
				teleportDistance = 0.5f;
				teleportRotation = 90f;
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					anchor = anchor,
					anchorInertia = anchorInertia,
					worldInertia = worldInertia,
					movementInertiaSmoothing = movementInertiaSmoothing,
					movementSpeedLimit = movementSpeedLimit.Clone(),
					rotationSpeedLimit = rotationSpeedLimit.Clone(),
					localInertia = localInertia,
					localMovementSpeedLimit = localMovementSpeedLimit.Clone(),
					localRotationSpeedLimit = localRotationSpeedLimit.Clone(),
					depthInertia = depthInertia,
					centrifualAcceleration = centrifualAcceleration,
					particleSpeedLimit = particleSpeedLimit.Clone(),
					teleportMode = teleportMode,
					teleportDistance = teleportDistance,
					teleportRotation = teleportRotation
				};
			}

			public void DataValidate()
			{
				anchorInertia = Mathf.Clamp01(anchorInertia);
				worldInertia = Mathf.Clamp01(worldInertia);
				movementInertiaSmoothing = Mathf.Clamp01(movementInertiaSmoothing);
				movementSpeedLimit.DataValidate(0f, 10f);
				rotationSpeedLimit.DataValidate(0f, 1440f);
				localInertia = Mathf.Clamp01(localInertia);
				localMovementSpeedLimit.DataValidate(0f, 10f);
				localRotationSpeedLimit.DataValidate(0f, 1440f);
				centrifualAcceleration = Mathf.Clamp01(centrifualAcceleration);
				depthInertia = Mathf.Clamp01(depthInertia);
				particleSpeedLimit.DataValidate(0f, 10f);
				teleportDistance = Mathf.Max(teleportDistance, 0f);
				teleportRotation = Mathf.Max(teleportRotation, 0f);
			}
		}

		public struct InertiaConstraintParams
		{
			public float anchorInertia;

			public float worldInertia;

			public float movementInertiaSmoothing;

			public float movementSpeedLimit;

			public float rotationSpeedLimit;

			public float localInertia;

			public float localMovementSpeedLimit;

			public float localRotationSpeedLimit;

			public float depthInertia;

			public float centrifualAcceleration;

			public float particleSpeedLimit;

			public TeleportMode teleportMode;

			public float teleportDistance;

			public float teleportRotation;

			public void Convert(SerializeData sdata)
			{
				anchorInertia = sdata.anchorInertia;
				worldInertia = sdata.worldInertia;
				movementInertiaSmoothing = sdata.movementInertiaSmoothing;
				movementSpeedLimit = sdata.movementSpeedLimit.GetValue(-1f);
				rotationSpeedLimit = sdata.rotationSpeedLimit.GetValue(-1f);
				localInertia = sdata.localInertia;
				localMovementSpeedLimit = sdata.localMovementSpeedLimit.GetValue(-1f);
				localRotationSpeedLimit = sdata.localRotationSpeedLimit.GetValue(-1f);
				depthInertia = sdata.depthInertia;
				centrifualAcceleration = sdata.centrifualAcceleration;
				particleSpeedLimit = sdata.particleSpeedLimit.GetValue(-1f);
				teleportMode = sdata.teleportMode;
				teleportDistance = sdata.teleportDistance;
				teleportRotation = sdata.teleportRotation;
			}
		}

		[Serializable]
		public struct CenterData
		{
			public float3 anchorPosition;

			public quaternion anchorRotation;

			public float3 oldAnchorPosition;

			public quaternion oldAnchorRotation;

			public float3 anchorComponentLocalPosition;

			public int centerTransformIndex;

			public float3 componentWorldPosition;

			public quaternion componentWorldRotation;

			public float3 componentWorldScale;

			public float3 oldComponentWorldPosition;

			public quaternion oldComponentWorldRotation;

			public float3 oldComponentWorldScale;

			public float3 frameComponentShiftVector;

			public quaternion frameComponentShiftRotation;

			public float frameMovingSpeed;

			public float3 frameMovingDirection;

			public float3 frameWorldPosition;

			public quaternion frameWorldRotation;

			public float3 frameWorldScale;

			public float3 frameLocalPosition;

			public float3 oldFrameWorldPosition;

			public quaternion oldFrameWorldRotation;

			public float3 oldFrameWorldScale;

			public float3 nowWorldPosition;

			public quaternion nowWorldRotation;

			public float3 oldWorldPosition;

			public quaternion oldWorldRotation;

			public float stepMoveInertiaRatio;

			public float stepRotationInertiaRatio;

			public float3 stepVector;

			public quaternion stepRotation;

			public float3 inertiaVector;

			public quaternion inertiaRotation;

			public float stepMovingSpeed;

			public float3 stepMovingDirection;

			public float angularVelocity;

			public float3 rotationAxis;

			public float3 initLocalGravityDirection;

			public float3 smoothingVelocity;

			public float4x4 negativeScaleMatrix;

			internal void Initialize()
			{
				anchorRotation = quaternion.identity;
				oldAnchorRotation = quaternion.identity;
				componentWorldRotation = quaternion.identity;
				componentWorldScale = 1;
				oldComponentWorldRotation = quaternion.identity;
				oldComponentWorldScale = 1;
				frameComponentShiftRotation = quaternion.identity;
				frameWorldRotation = quaternion.identity;
				oldFrameWorldRotation = quaternion.identity;
				nowWorldRotation = quaternion.identity;
				oldWorldRotation = quaternion.identity;
				stepRotation = quaternion.identity;
			}
		}

		[Serializable]
		public class ConstraintData
		{
			public ResultCode result;

			public CenterData centerData;

			public float3 initLocalGravityDirection;
		}

		internal ExNativeArray<ushort> fixedArray;

		public InertiaConstraint()
		{
			fixedArray = new ExNativeArray<ushort>(0, create: true);
		}

		public void Dispose()
		{
			fixedArray?.Dispose();
			fixedArray = null;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[InertiaConstraint]");
			stringBuilder.AppendLine("  -fixedArray:" + fixedArray.ToSummary());
			return stringBuilder.ToString();
		}

		public static ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			ConstraintData constraintData = new ConstraintData();
			try
			{
				CenterData centerData = default(CenterData);
				centerData.Initialize();
				centerData.centerTransformIndex = proxyMesh.centerTransformIndex;
				constraintData.centerData = centerData;
				float3 x = 0;
				float3 x2 = 0;
				int centerFixedPointCount = proxyMesh.CenterFixedPointCount;
				if (centerFixedPointCount > 0)
				{
					for (int i = 0; i < centerFixedPointCount; i++)
					{
						int index = proxyMesh.centerFixedList[i];
						quaternion a = MathUtility.ToRotation(proxyMesh.localNormals[index], proxyMesh.localTangents[index]);
						quaternion b = proxyMesh.vertexBindPoseRotations[index];
						quaternion rot = math.mul(a, b);
						x += MathUtility.ToNormal(in rot);
						x2 += MathUtility.ToTangent(in rot);
					}
				}
				float3 initLocalGravityDirection = new float3(0f, -1f, 0f);
				if (centerFixedPointCount > 0)
				{
					initLocalGravityDirection = math.mul(math.inverse(MathUtility.ToRotation(math.normalize(x), math.normalize(x2))), parameters.worldGravityDirection);
				}
				constraintData.initLocalGravityDirection = initLocalGravityDirection;
				constraintData.result.SetSuccess();
				return constraintData;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateInertiaException);
				throw;
			}
		}

		internal void Register(ClothProcess cprocess)
		{
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
			ref CenterData reference = ref MagicaManager.Team.centerDataArray.GetRef(cprocess.TeamId);
			reference.centerTransformIndex = teamDataRef.centerTransformIndex;
			reference.initLocalGravityDirection = cprocess.inertiaConstraintData.initLocalGravityDirection;
			DataChunk fixedDataChunk = default(DataChunk);
			if (cprocess.ProxyMeshContainer.shareVirtualMesh.CenterFixedPointCount > 0)
			{
				fixedDataChunk = fixedArray.AddRange(cprocess.ProxyMeshContainer.shareVirtualMesh.centerFixedList);
			}
			teamDataRef.fixedDataChunk = fixedDataChunk;
		}

		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				fixedArray.Remove(teamDataRef.fixedDataChunk);
				teamDataRef.fixedDataChunk.Clear();
			}
		}
	}
}
