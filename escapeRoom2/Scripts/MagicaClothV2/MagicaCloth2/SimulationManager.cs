using System;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class SimulationManager : IManager, IDisposable, IValid
	{
		[BurstCompile]
		private struct SimulationNormalJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeList<int> batchNormalTeamList;

			public float4 simulationPower;

			public float simulationDeltaTime;

			public int mappingCount;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamWindData> teamWindArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			public int windZoneCount;

			[ReadOnly]
			public NativeArray<WindManager.WindData> windDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> transformPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			[ReadOnly]
			public NativeArray<float4x4> transformLocalToWorldMatrixArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> transformLocalPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> transformLocalRotationArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<int> skinBoneTransformIndices;

			[ReadOnly]
			public NativeArray<float4x4> skinBoneBindPoses;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<quaternion> vertexBindPoseRotations;

			[ReadOnly]
			public NativeArray<float> vertexDepths;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			[ReadOnly]
			public NativeArray<float3> vertexLocalPositions;

			[ReadOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			[ReadOnly]
			public NativeArray<uint> vertexChildIndexArray;

			[ReadOnly]
			public NativeArray<ushort> vertexChildDataArray;

			[ReadOnly]
			public NativeArray<ExBitFlag8> baseLineFlags;

			[ReadOnly]
			public NativeArray<int3> triangles;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> triangleNormals;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> triangleTangents;

			[ReadOnly]
			public NativeArray<float2> uvs;

			[ReadOnly]
			public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

			[ReadOnly]
			public NativeArray<quaternion> normalAdjustmentRotations;

			[ReadOnly]
			public NativeArray<quaternion> vertexToTransformRotations;

			[ReadOnly]
			public NativeArray<int2> edges;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> oldRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> basePosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> baseRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> oldRotationArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> dispPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> realVelocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> staticFrictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> collisionNormalArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			[ReadOnly]
			public NativeArray<float3> colliderCenterArray;

			[ReadOnly]
			public NativeArray<float3> colliderSizeArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderFramePositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderFrameScales;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldFramePositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderNowPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderNowRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;

			[ReadOnly]
			public NativeArray<ushort> fixedArray;

			[ReadOnly]
			public NativeArray<uint> distanceIndexArray;

			[ReadOnly]
			public NativeArray<ushort> distanceDataArray;

			[ReadOnly]
			public NativeArray<float> distanceDistanceArray;

			[ReadOnly]
			public NativeArray<ulong> bendingTrianglePairArray;

			[ReadOnly]
			public NativeArray<float> bendingRestAngleOrVolumeArray;

			[ReadOnly]
			public NativeArray<sbyte> bendingSignOrVolumeArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> stepBasicPositionBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> stepBasicRotationBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> tempFloatBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> tempRotationBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> tempRotationBufferB;

			public unsafe void Execute(int localIndex)
			{
				int num = batchNormalTeamList[localIndex];
				TeamManager.TeamData* unsafePtr = (TeamManager.TeamData*)teamDataArray.GetUnsafePtr();
				ClothParameters* unsafeReadOnlyPtr = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafePtr2 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafePtr();
				TeamWindData* unsafePtr3 = (TeamWindData*)teamWindArray.GetUnsafePtr();
				ref TeamManager.TeamData reference = ref unsafePtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafePtr2[num];
				ref ClothParameters reference2 = ref unsafeReadOnlyPtr[num];
				ref TeamWindData reference3 = ref unsafePtr3[num];
				if (!reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				VirtualMeshManager.SimulationPreProxyMeshUpdate(new DataChunk(0, reference.proxyCommonChunk.dataLength), num, ref reference, in attributes, in localPositions, in localNormals, in localTangents, in boneWeights, in skinBoneTransformIndices, in skinBoneBindPoses, ref positions, ref rotations, in transformLocalToWorldMatrixArray);
				TeamManager.SimulationCalcCenterAndInertiaAndWind(simulationDeltaTime, num, ref reference, ref cdata, ref reference3, ref reference2, in positions, in rotations, in vertexBindPoseRotations, in fixedArray, in transformPositionArray, in transformRotationArray, in transformScaleArray, windZoneCount, in windDataArray);
				SimulationPreTeamUpdate(new DataChunk(0, reference.particleChunk.dataLength), ref reference, in reference2, in cdata, in positions, in rotations, in vertexDepths, ref nextPosArray, ref oldPosArray, ref oldRotArray, ref basePosArray, ref baseRotArray, ref oldPositionArray, ref oldRotationArray, ref velocityPosArray, ref dispPosArray, ref velocityArray, ref realVelocityArray, ref frictionArray, ref staticFrictionArray, ref collisionNormalArray);
				if (reference.colliderCount > 0)
				{
					ColliderManager.SimulationPreUpdate(new DataChunk(0, reference.colliderChunk.dataLength), ref reference, ref cdata, ref colliderFlagArray, ref colliderCenterArray, ref colliderFramePositions, ref colliderFrameRotations, ref colliderFrameScales, ref colliderOldFramePositions, ref colliderOldFrameRotations, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations, ref transformPositionArray, ref transformRotationArray, ref transformScaleArray);
				}
				for (int i = 0; i < reference.updateCount; i++)
				{
					TeamManager.SimulationStepTeamUpdate(i, simulationDeltaTime, num, ref reference, ref reference2, ref cdata, ref reference3);
					if (reference.colliderCount > 0)
					{
						ColliderManager.SimulationStartStep(ref reference, ref cdata, ref colliderFlagArray, ref colliderSizeArray, ref colliderFramePositions, ref colliderFrameRotations, ref colliderFrameScales, ref colliderOldFramePositions, ref colliderOldFrameRotations, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations, ref colliderWorkDataArray);
					}
					SimulationStepUpdateParticles(new DataChunk(0, reference.particleChunk.dataLength), simulationPower, simulationDeltaTime, num, ref reference, ref cdata, ref reference2, ref reference3, ref windDataArray, ref attributes, ref depthArray, ref positions, ref rotations, ref vertexRootIndices, ref nextPosArray, ref oldPosArray, ref basePosArray, ref baseRotArray, ref oldPositionArray, ref oldRotationArray, ref velocityPosArray, ref velocityArray, ref frictionArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer);
					SimulationStepUpdateBaseLinePose(new DataChunk(0, reference.baseLineChunk.dataLength), ref reference, ref attributes, ref vertexParentIndices, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData, ref vertexLocalPositions, ref vertexLocalRotations, ref basePosArray, ref baseRotArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer);
					TetherConstraint.SolverConstraint(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref reference2, ref cdata, ref attributes, ref depthArray, ref vertexRootIndices, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref stepBasicPositionBuffer);
					DistanceConstraint.SolverConstraint(new DataChunk(0, reference.particleChunk.dataLength), simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref basePosArray, ref velocityPosArray, ref frictionArray, ref distanceIndexArray, ref distanceDataArray, ref distanceDistanceArray);
					AngleConstraint.SolverConstraint(new DataChunk(0, reference.baseLineChunk.dataLength), in simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref vertexParentIndices, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer, ref tempFloatBufferA, ref tempVectorBufferA, ref tempRotationBufferA, ref tempRotationBufferB, ref tempVectorBufferB);
					SimulationClearTempBuffer(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
					TriangleBendingConstraint.SolverConstraint(new DataChunk(0, reference.bendingPairChunk.dataLength), in simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref frictionArray, ref bendingTrianglePairArray, ref bendingRestAngleOrVolumeArray, ref bendingSignOrVolumeArray, ref tempVectorBufferA, ref tempCountBuffer);
					TriangleBendingConstraint.SumConstraint(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref reference2, ref attributes, ref nextPosArray, ref tempVectorBufferA, ref tempCountBuffer);
					if (reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Point)
					{
						ColliderCollisionConstraint.SolverPointConstraint(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref frictionArray, ref collisionNormalArray, ref velocityPosArray, ref basePosArray, ref colliderFlagArray, ref colliderWorkDataArray);
					}
					else if (reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
					{
						ColliderCollisionConstraint.SolverEdgeConstraint(new DataChunk(0, reference.proxyEdgeChunk.dataLength), ref reference, ref reference2, ref attributes, ref depthArray, ref edges, ref nextPosArray, ref colliderFlagArray, ref colliderWorkDataArray, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
						ColliderCollisionConstraint.SumEdgeConstraint(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref reference2, ref nextPosArray, ref frictionArray, ref collisionNormalArray, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
					}
					DistanceConstraint.SolverConstraint(new DataChunk(0, reference.particleChunk.dataLength), simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref basePosArray, ref velocityPosArray, ref frictionArray, ref distanceIndexArray, ref distanceDataArray, ref distanceDistanceArray);
					MotionConstraint.SolverConstraint(new DataChunk(0, reference.particleChunk.dataLength), ref reference, ref reference2, ref attributes, ref depthArray, ref basePosArray, ref baseRotArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref collisionNormalArray);
					SimulationStepPostTeam(new DataChunk(0, reference.particleChunk.dataLength), simulationDeltaTime, num, ref reference, ref cdata, ref reference2, ref attributes, ref depthArray, ref oldPosArray, ref velocityArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref staticFrictionArray, ref collisionNormalArray, ref realVelocityArray);
					ColliderManager.SimulationEndStep(new DataChunk(0, reference.colliderChunk.dataLength), ref reference, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations);
				}
				SimulationCalcDisplayPosition(new DataChunk(0, reference.particleChunk.dataLength), simulationDeltaTime, ref reference, ref oldPosArray, ref realVelocityArray, ref oldPositionArray, ref oldRotationArray, ref dispPosArray, ref attributes, ref positions, ref rotations, ref vertexRootIndices);
				VirtualMeshManager.SimulationPostProxyMeshUpdateLine(new DataChunk(0, reference.baseLineChunk.dataLength), ref reference, ref reference2, ref attributes, ref positions, ref rotations, ref vertexLocalPositions, ref vertexLocalRotations, ref vertexChildIndexArray, ref vertexChildDataArray, ref baseLineFlags, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData);
				VirtualMeshManager.SimulationPostProxyMeshUpdateTriangle(new DataChunk(0, reference.proxyTriangleChunk.dataLength), ref reference, ref positions, ref triangles, ref triangleNormals, ref triangleTangents, ref uvs);
				VirtualMeshManager.SimulationPostProxyMeshUpdateTriangleSum(new DataChunk(0, reference.proxyCommonChunk.dataLength), ref reference, ref rotations, ref triangleNormals, ref triangleTangents, ref vertexToTriangles, ref normalAdjustmentRotations);
				VirtualMeshManager.SimulationPostProxyMeshUpdateTransform(new DataChunk(0, reference.proxyCommonChunk.dataLength), ref reference, ref attributes, ref positions, ref rotations, ref vertexParentIndices, ref vertexToTransformRotations, ref transformPositionArray, ref transformRotationArray, ref transformScaleArray, ref transformLocalPositionArray, ref transformLocalRotationArray);
				ColliderManager.SimulationPostUpdate(ref reference, ref colliderFramePositions, ref colliderFrameRotations, ref colliderOldFramePositions, ref colliderOldFrameRotations);
				TeamManager.SimulationPostTeamUpdate(ref reference, ref cdata);
			}
		}

		[BurstCompile]
		private struct SplitPre_A_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<float4x4> transformLocalToWorldMatrixArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<float3> localNormals;

			[ReadOnly]
			public NativeArray<float3> localTangents;

			[ReadOnly]
			public NativeArray<VirtualMeshBoneWeight> boneWeights;

			[ReadOnly]
			public NativeArray<int> skinBoneTransformIndices;

			[ReadOnly]
			public NativeArray<float4x4> skinBoneBindPoses;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> rotations;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.proxyCommonChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						VirtualMeshManager.SimulationPreProxyMeshUpdate(workerChunk, num, ref reference, in attributes, in localPositions, in localNormals, in localTangents, in boneWeights, in skinBoneTransformIndices, in skinBoneBindPoses, ref positions, ref rotations, in transformLocalToWorldMatrixArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitPre_B_Job : IJobParallelFor
		{
			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamWindData> teamWindArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			public int windZoneCount;

			[ReadOnly]
			public NativeArray<WindManager.WindData> windDataArray;

			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<quaternion> vertexBindPoseRotations;

			[ReadOnly]
			public NativeArray<ushort> fixedArray;

			public unsafe void Execute(int localIndex)
			{
				TeamManager.TeamData* unsafePtr = (TeamManager.TeamData*)teamDataArray.GetUnsafePtr();
				ClothParameters* unsafeReadOnlyPtr = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafePtr2 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafePtr();
				TeamWindData* unsafePtr3 = (TeamWindData*)teamWindArray.GetUnsafePtr();
				int num = batchSelfTeamList[localIndex];
				ref TeamManager.TeamData reference = ref unsafePtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafePtr2[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr[num];
				ref TeamWindData windData = ref unsafePtr3[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					TeamManager.SimulationCalcCenterAndInertiaAndWind(simulationDeltaTime, num, ref reference, ref cdata, ref windData, ref param, in positions, in rotations, in vertexBindPoseRotations, in fixedArray, in transformPositionArray, in transformRotationArray, in transformScaleArray, windZoneCount, in windDataArray);
				}
			}
		}

		[BurstCompile]
		private struct SplitPre_C_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<float3> transformPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<float> vertexDepths;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> oldRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> basePosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> baseRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> oldRotationArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> dispPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> realVelocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> staticFrictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> collisionNormalArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			[ReadOnly]
			public NativeArray<float3> colliderCenterArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderFramePositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderFrameScales;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldFramePositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderNowPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderNowRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldRotations;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafeReadOnlyPtr3 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafeReadOnlyPtr3[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (!reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
				if (workerChunk.IsValid)
				{
					SimulationPreTeamUpdate(workerChunk, ref reference, in param, in cdata, in positions, in rotations, in vertexDepths, ref nextPosArray, ref oldPosArray, ref oldRotArray, ref basePosArray, ref baseRotArray, ref oldPositionArray, ref oldRotationArray, ref velocityPosArray, ref dispPosArray, ref velocityArray, ref realVelocityArray, ref frictionArray, ref staticFrictionArray, ref collisionNormalArray);
				}
				if (reference.colliderCount > 0)
				{
					DataChunk workerChunk2 = MathUtility.GetWorkerChunk(reference.colliderChunk.dataLength, workerCount, workerIndex);
					if (workerChunk2.IsValid)
					{
						ColliderManager.SimulationPreUpdate(workerChunk2, ref reference, ref cdata, ref colliderFlagArray, ref colliderCenterArray, ref colliderFramePositions, ref colliderFrameRotations, ref colliderFrameScales, ref colliderOldFramePositions, ref colliderOldFrameRotations, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations, ref transformPositionArray, ref transformRotationArray, ref transformScaleArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_A_Job : IJobParallelFor
		{
			public int updateIndex;

			public float4 simulationPower;

			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamWindData> teamWindArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			[ReadOnly]
			public NativeArray<float3> colliderSizeArray;

			[ReadOnly]
			public NativeArray<float3> colliderFramePositions;

			[ReadOnly]
			public NativeArray<quaternion> colliderFrameRotations;

			[ReadOnly]
			public NativeArray<float3> colliderFrameScales;

			[ReadOnly]
			public NativeArray<float3> colliderOldFramePositions;

			[ReadOnly]
			public NativeArray<quaternion> colliderOldFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderNowPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderNowRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;

			public unsafe void Execute(int localIndex)
			{
				TeamManager.TeamData* unsafePtr = (TeamManager.TeamData*)teamDataArray.GetUnsafePtr();
				ClothParameters* unsafeReadOnlyPtr = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafePtr2 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafePtr();
				TeamWindData* unsafePtr3 = (TeamWindData*)teamWindArray.GetUnsafePtr();
				int num = batchSelfTeamList[localIndex];
				ref TeamManager.TeamData reference = ref unsafePtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafePtr2[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr[num];
				ref TeamWindData wdata = ref unsafePtr3[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					TeamManager.SimulationStepTeamUpdate(updateIndex, simulationDeltaTime, num, ref reference, ref param, ref cdata, ref wdata);
					if (reference.colliderCount > 0)
					{
						ColliderManager.SimulationStartStep(ref reference, ref cdata, ref colliderFlagArray, ref colliderSizeArray, ref colliderFramePositions, ref colliderFrameRotations, ref colliderFrameScales, ref colliderOldFramePositions, ref colliderOldFrameRotations, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations, ref colliderWorkDataArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_B_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<TeamWindData> teamWindArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			public int windZoneCount;

			[ReadOnly]
			public NativeArray<WindManager.WindData> windDataArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> basePosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> baseRotArray;

			[ReadOnly]
			public NativeArray<float3> oldPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> oldRotationArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[ReadOnly]
			public NativeArray<float3> velocityArray;

			[ReadOnly]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> stepBasicPositionBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> stepBasicRotationBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafeReadOnlyPtr3 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafeReadOnlyPtr();
				TeamWindData* unsafeReadOnlyPtr4 = (TeamWindData*)teamWindArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafeReadOnlyPtr3[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				ref TeamWindData wdata = ref unsafeReadOnlyPtr4[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						SimulationStepUpdateParticles(workerChunk, simulationPower, simulationDeltaTime, num, ref reference, ref cdata, ref param, ref wdata, ref windDataArray, ref attributes, ref depthArray, ref positions, ref rotations, ref vertexRootIndices, ref nextPosArray, ref oldPosArray, ref basePosArray, ref baseRotArray, ref oldPositionArray, ref oldRotationArray, ref velocityPosArray, ref velocityArray, ref frictionArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_C_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			[ReadOnly]
			public NativeArray<float3> vertexLocalPositions;

			[ReadOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			[ReadOnly]
			public NativeArray<float3> basePosArray;

			[ReadOnly]
			public NativeArray<quaternion> baseRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> stepBasicPositionBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> stepBasicRotationBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.baseLineChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						SimulationStepUpdateBaseLinePose(workerChunk, ref reference, ref attributes, ref vertexParentIndices, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData, ref vertexLocalPositions, ref vertexLocalRotations, ref basePosArray, ref baseRotArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_D_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> basePosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[ReadOnly]
			public NativeArray<float> frictionArray;

			[ReadOnly]
			public NativeArray<uint> distanceIndexArray;

			[ReadOnly]
			public NativeArray<ushort> distanceDataArray;

			[ReadOnly]
			public NativeArray<float> distanceDistanceArray;

			[ReadOnly]
			public NativeArray<float3> stepBasicPositionBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafeReadOnlyPtr3 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafeReadOnlyPtr3[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						TetherConstraint.SolverConstraint(workerChunk, ref reference, ref param, ref cdata, ref attributes, ref depthArray, ref vertexRootIndices, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref stepBasicPositionBuffer);
						DistanceConstraint.SolverConstraint(workerChunk, simulationPower, ref reference, ref param, ref attributes, ref depthArray, ref nextPosArray, ref basePosArray, ref velocityPosArray, ref frictionArray, ref distanceIndexArray, ref distanceDataArray, ref distanceDistanceArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_Angle_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[ReadOnly]
			public NativeArray<float> frictionArray;

			[ReadOnly]
			public NativeArray<uint> distanceIndexArray;

			[ReadOnly]
			public NativeArray<ushort> distanceDataArray;

			[ReadOnly]
			public NativeArray<float> distanceDistanceArray;

			[ReadOnly]
			public NativeArray<float3> stepBasicPositionBuffer;

			[ReadOnly]
			public NativeArray<quaternion> stepBasicRotationBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> tempFloatBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> tempRotationBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> tempRotationBufferB;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.baseLineChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						AngleConstraint.SolverConstraint(workerChunk, in simulationPower, ref reference, ref param, ref attributes, ref depthArray, ref vertexParentIndices, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref stepBasicPositionBuffer, ref stepBasicRotationBuffer, ref tempFloatBufferA, ref tempVectorBufferA, ref tempRotationBufferA, ref tempRotationBufferB, ref tempVectorBufferB);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_Triangle_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float> frictionArray;

			[ReadOnly]
			public NativeArray<ulong> bendingTrianglePairArray;

			[ReadOnly]
			public NativeArray<float> bendingRestAngleOrVolumeArray;

			[ReadOnly]
			public NativeArray<sbyte> bendingSignOrVolumeArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.bendingPairChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						TriangleBendingConstraint.SolverConstraint(workerChunk, in simulationPower, ref reference, ref param, ref attributes, ref depthArray, ref nextPosArray, ref frictionArray, ref bendingTrianglePairArray, ref bendingRestAngleOrVolumeArray, ref bendingSignOrVolumeArray, ref tempVectorBufferA, ref tempCountBuffer);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_E_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> basePosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> collisionNormalArray;

			[ReadOnly]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			[ReadOnly]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters reference2 = ref unsafeReadOnlyPtr2[num];
				if (updateIndex >= reference.updateCount || !reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
				if (workerChunk.IsValid)
				{
					TriangleBendingConstraint.SumConstraint(workerChunk, ref reference, ref reference2, ref attributes, ref nextPosArray, ref tempVectorBufferA, ref tempCountBuffer);
					if (reference.ColliderCount > 0 && reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Point)
					{
						ColliderCollisionConstraint.SolverPointConstraint(workerChunk, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref frictionArray, ref collisionNormalArray, ref velocityPosArray, ref basePosArray, ref colliderFlagArray, ref colliderWorkDataArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_Edge_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<int2> edges;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<ExBitFlag8> colliderFlagArray;

			[ReadOnly]
			public NativeArray<ColliderManager.WorkData> colliderWorkDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> tempFloatBufferA;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters reference2 = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0 && reference.ColliderCount > 0 && reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.proxyEdgeChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						ColliderCollisionConstraint.SolverEdgeConstraint(workerChunk, ref reference, ref reference2, ref attributes, ref depthArray, ref edges, ref nextPosArray, ref colliderFlagArray, ref colliderWorkDataArray, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_F_Self_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[ReadOnly]
			public NativeArray<float3> basePosArray;

			[ReadOnly]
			public NativeArray<quaternion> baseRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> collisionNormalArray;

			[ReadOnly]
			public NativeArray<uint> distanceIndexArray;

			[ReadOnly]
			public NativeArray<ushort> distanceDataArray;

			[ReadOnly]
			public NativeArray<float> distanceDistanceArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> tempFloatBufferA;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters reference2 = ref unsafeReadOnlyPtr2[num];
				if (updateIndex >= reference.updateCount || !reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
				if (workerChunk.IsValid)
				{
					if (reference.ColliderCount > 0 && reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
					{
						ColliderCollisionConstraint.SumEdgeConstraint(workerChunk, ref reference, ref reference2, ref nextPosArray, ref frictionArray, ref collisionNormalArray, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
					}
					DistanceConstraint.SolverConstraint(workerChunk, simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref basePosArray, ref velocityPosArray, ref frictionArray, ref distanceIndexArray, ref distanceDataArray, ref distanceDistanceArray);
					MotionConstraint.SolverConstraint(workerChunk, ref reference, ref reference2, ref attributes, ref depthArray, ref basePosArray, ref baseRotArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref collisionNormalArray);
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_G_Self_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[ReadOnly]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPosArray;

			[ReadOnly]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> realVelocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> staticFrictionArray;

			[ReadOnly]
			public NativeArray<float3> collisionNormalArray;

			[ReadOnly]
			public NativeArray<float3> colliderNowPositions;

			[ReadOnly]
			public NativeArray<quaternion> colliderNowRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldRotations;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafeReadOnlyPtr3 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafeReadOnlyPtr3[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (updateIndex < reference.updateCount && reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						SimulationStepPostTeam(workerChunk, simulationDeltaTime, num, ref reference, ref cdata, ref param, ref attributes, ref depthArray, ref oldPosArray, ref velocityArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref staticFrictionArray, ref collisionNormalArray, ref realVelocityArray);
					}
					DataChunk workerChunk2 = MathUtility.GetWorkerChunk(reference.colliderChunk.dataLength, workerCount, workerIndex);
					if (workerChunk2.IsValid)
					{
						ColliderManager.SimulationEndStep(workerChunk2, ref reference, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitStep_FG_NoSelf_Job : IJobParallelFor
		{
			public int workerCount;

			public int updateIndex;

			public float4 simulationPower;

			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float> depthArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> nextPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPosArray;

			[ReadOnly]
			public NativeArray<float3> basePosArray;

			[ReadOnly]
			public NativeArray<quaternion> baseRotArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> velocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> realVelocityArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> frictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> staticFrictionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> collisionNormalArray;

			[ReadOnly]
			public NativeArray<float3> colliderNowPositions;

			[ReadOnly]
			public NativeArray<quaternion> colliderNowRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldPositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldRotations;

			[ReadOnly]
			public NativeArray<uint> distanceIndexArray;

			[ReadOnly]
			public NativeArray<ushort> distanceDataArray;

			[ReadOnly]
			public NativeArray<float> distanceDistanceArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferA;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> tempVectorBufferB;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<int> tempCountBuffer;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float> tempFloatBufferA;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				InertiaConstraint.CenterData* unsafeReadOnlyPtr3 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafeReadOnlyPtr3[num];
				ref ClothParameters reference2 = ref unsafeReadOnlyPtr2[num];
				if (updateIndex >= reference.updateCount || !reference.IsProcess || reference.ParticleCount == 0)
				{
					return;
				}
				DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
				if (workerChunk.IsValid)
				{
					if (reference.ColliderCount > 0 && reference2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
					{
						ColliderCollisionConstraint.SumEdgeConstraint(workerChunk, ref reference, ref reference2, ref nextPosArray, ref frictionArray, ref collisionNormalArray, ref tempVectorBufferA, ref tempVectorBufferB, ref tempCountBuffer, ref tempFloatBufferA);
					}
					DistanceConstraint.SolverConstraint(workerChunk, simulationPower, ref reference, ref reference2, ref attributes, ref depthArray, ref nextPosArray, ref basePosArray, ref velocityPosArray, ref frictionArray, ref distanceIndexArray, ref distanceDataArray, ref distanceDistanceArray);
					MotionConstraint.SolverConstraint(workerChunk, ref reference, ref reference2, ref attributes, ref depthArray, ref basePosArray, ref baseRotArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref collisionNormalArray);
					SimulationStepPostTeam(workerChunk, simulationDeltaTime, num, ref reference, ref cdata, ref reference2, ref attributes, ref depthArray, ref oldPosArray, ref velocityArray, ref nextPosArray, ref velocityPosArray, ref frictionArray, ref staticFrictionArray, ref collisionNormalArray, ref realVelocityArray);
				}
				DataChunk workerChunk2 = MathUtility.GetWorkerChunk(reference.colliderChunk.dataLength, workerCount, workerIndex);
				if (workerChunk2.IsValid)
				{
					ColliderManager.SimulationEndStep(workerChunk2, ref reference, ref colliderNowPositions, ref colliderNowRotations, ref colliderOldPositions, ref colliderOldRotations);
				}
			}
		}

		[BurstCompile]
		private struct SplitPost_DisplayPos_Job : IJobParallelFor
		{
			public int workerCount;

			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<int> vertexRootIndices;

			[ReadOnly]
			public NativeArray<float3> oldPosArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> oldPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> oldRotationArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> dispPosArray;

			[ReadOnly]
			public NativeArray<float3> realVelocityArray;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.particleChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						SimulationCalcDisplayPosition(workerChunk, simulationDeltaTime, ref reference, ref oldPosArray, ref realVelocityArray, ref oldPositionArray, ref oldRotationArray, ref dispPosArray, ref attributes, ref positions, ref rotations, ref vertexRootIndices);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitPost_CalcProxy_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<ushort> baseLineStartDataIndices;

			[ReadOnly]
			public NativeArray<ushort> baseLineDataCounts;

			[ReadOnly]
			public NativeArray<ushort> baseLineData;

			[ReadOnly]
			public NativeArray<float3> vertexLocalPositions;

			[ReadOnly]
			public NativeArray<quaternion> vertexLocalRotations;

			[ReadOnly]
			public NativeArray<uint> vertexChildIndexArray;

			[ReadOnly]
			public NativeArray<ushort> vertexChildDataArray;

			[ReadOnly]
			public NativeArray<ExBitFlag8> baseLineFlags;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				ClothParameters* unsafeReadOnlyPtr2 = (ClothParameters*)parameterArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				ref ClothParameters param = ref unsafeReadOnlyPtr2[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.baseLineChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						VirtualMeshManager.SimulationPostProxyMeshUpdateLine(workerChunk, ref reference, ref param, ref attributes, ref positions, ref rotations, ref vertexLocalPositions, ref vertexLocalRotations, ref vertexChildIndexArray, ref vertexChildDataArray, ref baseLineFlags, ref baseLineStartDataIndices, ref baseLineDataCounts, ref baseLineData);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitPost_CalcProxyTriangle_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeArray<int3> triangles;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> triangleNormals;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> triangleTangents;

			[ReadOnly]
			public NativeArray<float2> uvs;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.proxyTriangleChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						VirtualMeshManager.SimulationPostProxyMeshUpdateTriangle(workerChunk, ref reference, ref positions, ref triangles, ref triangleNormals, ref triangleTangents, ref uvs);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitPost_SumProxyTriangleAndTransform_Job : IJobParallelFor
		{
			public int workerCount;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> transformPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> transformRotationArray;

			[ReadOnly]
			public NativeArray<float3> transformScaleArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> transformLocalPositionArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> transformLocalRotationArray;

			[ReadOnly]
			public NativeArray<VertexAttribute> attributes;

			[ReadOnly]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> rotations;

			[ReadOnly]
			public NativeArray<int> vertexParentIndices;

			[ReadOnly]
			public NativeArray<float3> triangleNormals;

			[ReadOnly]
			public NativeArray<float3> triangleTangents;

			[ReadOnly]
			public NativeArray<FixedList32Bytes<uint>> vertexToTriangles;

			[ReadOnly]
			public NativeArray<quaternion> normalAdjustmentRotations;

			[ReadOnly]
			public NativeArray<quaternion> vertexToTransformRotations;

			public unsafe void Execute(int index)
			{
				int index2 = index / workerCount;
				int workerIndex = index % workerCount;
				TeamManager.TeamData* unsafeReadOnlyPtr = (TeamManager.TeamData*)teamDataArray.GetUnsafeReadOnlyPtr();
				int num = batchSelfTeamList[index2];
				ref TeamManager.TeamData reference = ref unsafeReadOnlyPtr[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					DataChunk workerChunk = MathUtility.GetWorkerChunk(reference.proxyCommonChunk.dataLength, workerCount, workerIndex);
					if (workerChunk.IsValid)
					{
						VirtualMeshManager.SimulationPostProxyMeshUpdateTriangleSum(workerChunk, ref reference, ref rotations, ref triangleNormals, ref triangleTangents, ref vertexToTriangles, ref normalAdjustmentRotations);
						VirtualMeshManager.SimulationPostProxyMeshUpdateTransform(workerChunk, ref reference, ref attributes, ref positions, ref rotations, ref vertexParentIndices, ref vertexToTransformRotations, ref transformPositionArray, ref transformRotationArray, ref transformScaleArray, ref transformLocalPositionArray, ref transformLocalRotationArray);
					}
				}
			}
		}

		[BurstCompile]
		private struct SplitPost_TeamCollider_Job : IJobParallelFor
		{
			public float simulationDeltaTime;

			[ReadOnly]
			public NativeList<int> batchSelfTeamList;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			[ReadOnly]
			public NativeArray<float3> colliderFramePositions;

			[ReadOnly]
			public NativeArray<quaternion> colliderFrameRotations;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<float3> colliderOldFramePositions;

			[NativeDisableParallelForRestriction]
			[NativeDisableContainerSafetyRestriction]
			public NativeArray<quaternion> colliderOldFrameRotations;

			public unsafe void Execute(int localIndex)
			{
				TeamManager.TeamData* unsafePtr = (TeamManager.TeamData*)teamDataArray.GetUnsafePtr();
				InertiaConstraint.CenterData* unsafePtr2 = (InertiaConstraint.CenterData*)centerDataArray.GetUnsafePtr();
				int num = batchSelfTeamList[localIndex];
				ref TeamManager.TeamData reference = ref unsafePtr[num];
				ref InertiaConstraint.CenterData cdata = ref unsafePtr2[num];
				if (reference.IsProcess && reference.ParticleCount != 0)
				{
					ColliderManager.SimulationPostUpdate(ref reference, ref colliderFramePositions, ref colliderFrameRotations, ref colliderOldFramePositions, ref colliderOldFrameRotations);
					TeamManager.SimulationPostTeamUpdate(ref reference, ref cdata);
				}
			}
		}

		public ExNativeArray<short> teamIdArray;

		public ExNativeArray<float3> nextPosArray;

		public ExNativeArray<float3> oldPosArray;

		public ExNativeArray<quaternion> oldRotArray;

		public ExNativeArray<float3> basePosArray;

		public ExNativeArray<quaternion> baseRotArray;

		public ExNativeArray<float3> oldPositionArray;

		public ExNativeArray<quaternion> oldRotationArray;

		public ExNativeArray<float3> velocityPosArray;

		public ExNativeArray<float3> dispPosArray;

		public ExNativeArray<float3> velocityArray;

		public ExNativeArray<float3> realVelocityArray;

		public ExNativeArray<float> frictionArray;

		public ExNativeArray<float> staticFrictionArray;

		public ExNativeArray<float3> collisionNormalArray;

		public DistanceConstraint distanceConstraint;

		public TriangleBendingConstraint bendingConstraint;

		public TetherConstraint tetherConstraint;

		public AngleConstraint angleConstraint;

		public InertiaConstraint inertiaConstraint;

		public ColliderCollisionConstraint colliderCollisionConstraint;

		public MotionConstraint motionConstraint;

		public SelfCollisionConstraint selfCollisionConstraint;

		public NativeArray<float3> stepBasicPositionBuffer;

		public NativeArray<quaternion> stepBasicRotationBuffer;

		internal NativeArray<float3> tempVectorBufferA;

		internal NativeArray<float3> tempVectorBufferB;

		internal NativeArray<int> tempCountBuffer;

		internal NativeArray<float> tempFloatBufferA;

		internal NativeArray<quaternion> tempRotationBufferA;

		internal NativeArray<quaternion> tempRotationBufferB;

		internal int splitProxyMeshVertexCount = 300;

		private bool isValid;

		public int ParticleCount => nextPosArray?.Count ?? 0;

		internal int SimulationStepCount { get; private set; }

		internal int WorkerCount => JobsUtility.JobWorkerCount;

		public void Dispose()
		{
			isValid = false;
			teamIdArray?.Dispose();
			nextPosArray?.Dispose();
			oldPosArray?.Dispose();
			oldRotArray?.Dispose();
			basePosArray?.Dispose();
			baseRotArray?.Dispose();
			oldPositionArray?.Dispose();
			oldRotationArray?.Dispose();
			velocityPosArray?.Dispose();
			dispPosArray?.Dispose();
			velocityArray?.Dispose();
			realVelocityArray?.Dispose();
			frictionArray?.Dispose();
			staticFrictionArray?.Dispose();
			collisionNormalArray?.Dispose();
			teamIdArray = null;
			nextPosArray = null;
			oldPosArray = null;
			oldRotArray = null;
			basePosArray = null;
			baseRotArray = null;
			oldPositionArray = null;
			oldRotationArray = null;
			velocityPosArray = null;
			dispPosArray = null;
			velocityArray = null;
			realVelocityArray = null;
			frictionArray = null;
			staticFrictionArray = null;
			collisionNormalArray = null;
			if (stepBasicPositionBuffer.IsCreated)
			{
				stepBasicPositionBuffer.Dispose();
			}
			if (stepBasicRotationBuffer.IsCreated)
			{
				stepBasicRotationBuffer.Dispose();
			}
			if (tempVectorBufferA.IsCreated)
			{
				tempVectorBufferA.Dispose();
			}
			if (tempVectorBufferB.IsCreated)
			{
				tempVectorBufferB.Dispose();
			}
			if (tempCountBuffer.IsCreated)
			{
				tempCountBuffer.Dispose();
			}
			if (tempFloatBufferA.IsCreated)
			{
				tempFloatBufferA.Dispose();
			}
			if (tempRotationBufferA.IsCreated)
			{
				tempRotationBufferA.Dispose();
			}
			if (tempRotationBufferB.IsCreated)
			{
				tempRotationBufferB.Dispose();
			}
			distanceConstraint?.Dispose();
			bendingConstraint?.Dispose();
			tetherConstraint?.Dispose();
			angleConstraint?.Dispose();
			inertiaConstraint?.Dispose();
			colliderCollisionConstraint?.Dispose();
			motionConstraint?.Dispose();
			selfCollisionConstraint?.Dispose();
			distanceConstraint = null;
			bendingConstraint = null;
			tetherConstraint = null;
			angleConstraint = null;
			inertiaConstraint = null;
			colliderCollisionConstraint = null;
			motionConstraint = null;
			selfCollisionConstraint = null;
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			teamIdArray = new ExNativeArray<short>(0);
			nextPosArray = new ExNativeArray<float3>(0);
			oldPosArray = new ExNativeArray<float3>(0);
			oldRotArray = new ExNativeArray<quaternion>(0);
			basePosArray = new ExNativeArray<float3>(0);
			baseRotArray = new ExNativeArray<quaternion>(0);
			oldPositionArray = new ExNativeArray<float3>(0);
			oldRotationArray = new ExNativeArray<quaternion>(0);
			velocityPosArray = new ExNativeArray<float3>(0);
			dispPosArray = new ExNativeArray<float3>(0);
			velocityArray = new ExNativeArray<float3>(0);
			realVelocityArray = new ExNativeArray<float3>(0);
			frictionArray = new ExNativeArray<float>(0);
			staticFrictionArray = new ExNativeArray<float>(0);
			collisionNormalArray = new ExNativeArray<float3>(0);
			distanceConstraint = new DistanceConstraint();
			bendingConstraint = new TriangleBendingConstraint();
			tetherConstraint = new TetherConstraint();
			angleConstraint = new AngleConstraint();
			inertiaConstraint = new InertiaConstraint();
			colliderCollisionConstraint = new ColliderCollisionConstraint();
			motionConstraint = new MotionConstraint();
			selfCollisionConstraint = new SelfCollisionConstraint();
			SimulationStepCount = 0;
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		internal void RegisterProxyMesh(ClothProcess cprocess)
		{
			if (isValid)
			{
				int teamId = cprocess.TeamId;
				VirtualMesh shareVirtualMesh = cprocess.ProxyMeshContainer.shareVirtualMesh;
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
				int vertexCount = shareVirtualMesh.VertexCount;
				teamDataRef.particleChunk = teamIdArray.AddRange(vertexCount, (short)teamId);
				nextPosArray.AddRange(vertexCount);
				oldPosArray.AddRange(vertexCount);
				oldRotArray.AddRange(vertexCount);
				basePosArray.AddRange(vertexCount);
				baseRotArray.AddRange(vertexCount);
				oldPositionArray.AddRange(vertexCount);
				oldRotationArray.AddRange(vertexCount);
				velocityPosArray.AddRange(vertexCount);
				dispPosArray.AddRange(vertexCount);
				velocityArray.AddRange(vertexCount);
				realVelocityArray.AddRange(vertexCount);
				frictionArray.AddRange(vertexCount);
				staticFrictionArray.AddRange(vertexCount);
				collisionNormalArray.AddRange(vertexCount);
			}
		}

		internal void RegisterConstraint(ClothProcess cprocess)
		{
			if (isValid)
			{
				int teamId = cprocess.TeamId;
				MagicaManager.Team.centerDataArray[teamId] = cprocess.inertiaConstraintData.centerData;
				distanceConstraint.Register(cprocess);
				bendingConstraint.Register(cprocess);
				inertiaConstraint.Register(cprocess);
				selfCollisionConstraint.Register(cprocess);
			}
		}

		internal void ExitProxyMesh(ClothProcess cprocess)
		{
			if (isValid)
			{
				int teamId = cprocess.TeamId;
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
				teamDataRef.flag.SetBits(8, value: true);
				DataChunk particleChunk = teamDataRef.particleChunk;
				teamIdArray.RemoveAndFill(particleChunk, 0);
				nextPosArray.Remove(particleChunk);
				oldPosArray.Remove(particleChunk);
				oldRotArray.Remove(particleChunk);
				basePosArray.Remove(particleChunk);
				baseRotArray.Remove(particleChunk);
				oldPositionArray.Remove(particleChunk);
				oldRotationArray.Remove(particleChunk);
				velocityPosArray.Remove(particleChunk);
				dispPosArray.Remove(particleChunk);
				velocityArray.Remove(particleChunk);
				realVelocityArray.Remove(particleChunk);
				frictionArray.Remove(particleChunk);
				staticFrictionArray.Remove(particleChunk);
				collisionNormalArray.Remove(particleChunk);
				teamDataRef.particleChunk.Clear();
				distanceConstraint.Exit(cprocess);
				bendingConstraint.Exit(cprocess);
				inertiaConstraint.Exit(cprocess);
				selfCollisionConstraint.Exit(cprocess);
			}
		}

		internal void WorkBufferUpdate()
		{
			int particleCount = ParticleCount;
			NativeArrayExtensions.MC2Resize(ref stepBasicPositionBuffer, particleCount);
			NativeArrayExtensions.MC2Resize(ref stepBasicRotationBuffer, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempVectorBufferA, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempVectorBufferB, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempCountBuffer, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempFloatBufferA, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempRotationBufferA, particleCount);
			NativeArrayExtensions.MC2Resize(ref tempRotationBufferB, particleCount);
			selfCollisionConstraint.WorkBufferUpdate();
		}

		internal JobHandle ClothSimulationSchedule(JobHandle jobHandle)
		{
			TeamManager team = MagicaManager.Team;
			TransformManager bone = MagicaManager.Bone;
			VirtualMeshManager vMesh = MagicaManager.VMesh;
			WindManager wind = MagicaManager.Wind;
			ColliderManager collider = MagicaManager.Collider;
			TimeManager time = MagicaManager.Time;
			int length = team.batchNormalClothTeamList.Length;
			int length2 = team.batchSplitClothTeamList.Length;
			bool flag = length > 0;
			bool flag2 = length2 > 0;
			if (!flag && !flag2)
			{
				return jobHandle;
			}
			int teamMaxUpdateCount = team.TeamMaxUpdateCount;
			int num = math.max(WorkerCount, 1);
			num *= 5;
			JobHandle job = default(JobHandle);
			JobHandle job2 = default(JobHandle);
			JobHandle job3 = default(JobHandle);
			JobHandle job4 = default(JobHandle);
			if (flag2)
			{
				selfCollisionConstraint.contactQueue.Clear();
				selfCollisionConstraint.contactList.Clear();
				selfCollisionConstraint.intersectQueue.Clear();
				selfCollisionConstraint.intersectList.Clear();
				bool flag3 = team.teamStatus.Value.z > 0;
				bool flag4 = team.teamStatus.Value.w > 0;
				job2 = IJobParallelForExtensions.Schedule(new SplitPre_A_Job
				{
					workerCount = num,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					transformLocalToWorldMatrixArray = bone.localToWorldMatrixArray.GetNativeArray(),
					attributes = vMesh.attributes.GetNativeArray(),
					localPositions = vMesh.localPositions.GetNativeArray(),
					localNormals = vMesh.localNormals.GetNativeArray(),
					localTangents = vMesh.localTangents.GetNativeArray(),
					boneWeights = vMesh.boneWeights.GetNativeArray(),
					skinBoneTransformIndices = vMesh.skinBoneTransformIndices.GetNativeArray(),
					skinBoneBindPoses = vMesh.skinBoneBindPoses.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray()
				}, length2 * num, 1, jobHandle);
				job2 = IJobParallelForExtensions.Schedule(new SplitPre_B_Job
				{
					simulationDeltaTime = MagicaManager.Time.SimulationDeltaTime,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					centerDataArray = team.centerDataArray.GetNativeArray(),
					teamWindArray = team.teamWindArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					windZoneCount = wind.WindCount,
					windDataArray = wind.windDataArray.GetNativeArray(),
					transformPositionArray = bone.positionArray.GetNativeArray(),
					transformRotationArray = bone.rotationArray.GetNativeArray(),
					transformScaleArray = bone.scaleArray.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					vertexBindPoseRotations = vMesh.vertexBindPoseRotations.GetNativeArray(),
					fixedArray = inertiaConstraint.fixedArray.GetNativeArray()
				}, length2, 1, job2);
				job2 = IJobParallelForExtensions.Schedule(new SplitPre_C_Job
				{
					workerCount = num,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					centerDataArray = team.centerDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					transformPositionArray = bone.positionArray.GetNativeArray(),
					transformRotationArray = bone.rotationArray.GetNativeArray(),
					transformScaleArray = bone.scaleArray.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					vertexDepths = vMesh.vertexDepths.GetNativeArray(),
					nextPosArray = nextPosArray.GetNativeArray(),
					oldPosArray = oldPosArray.GetNativeArray(),
					oldRotArray = oldRotArray.GetNativeArray(),
					basePosArray = basePosArray.GetNativeArray(),
					baseRotArray = baseRotArray.GetNativeArray(),
					oldPositionArray = oldPositionArray.GetNativeArray(),
					oldRotationArray = oldRotationArray.GetNativeArray(),
					velocityPosArray = velocityPosArray.GetNativeArray(),
					dispPosArray = dispPosArray.GetNativeArray(),
					velocityArray = velocityArray.GetNativeArray(),
					realVelocityArray = realVelocityArray.GetNativeArray(),
					frictionArray = frictionArray.GetNativeArray(),
					staticFrictionArray = staticFrictionArray.GetNativeArray(),
					collisionNormalArray = collisionNormalArray.GetNativeArray(),
					colliderFlagArray = collider.flagArray.GetNativeArray(),
					colliderCenterArray = collider.centerArray.GetNativeArray(),
					colliderFramePositions = collider.framePositions.GetNativeArray(),
					colliderFrameRotations = collider.frameRotations.GetNativeArray(),
					colliderFrameScales = collider.frameScales.GetNativeArray(),
					colliderOldFramePositions = collider.oldFramePositions.GetNativeArray(),
					colliderOldFrameRotations = collider.oldFrameRotations.GetNativeArray(),
					colliderNowPositions = collider.nowPositions.GetNativeArray(),
					colliderNowRotations = collider.nowRotations.GetNativeArray(),
					colliderOldPositions = collider.oldPositions.GetNativeArray(),
					colliderOldRotations = collider.oldRotations.GetNativeArray()
				}, length2 * num, 1, job2);
				bool flag5 = false;
				if (flag4 && teamMaxUpdateCount > 0)
				{
					job3 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfDetectionIntersectJob
					{
						workerCount = num,
						frameIndex = Time.frameCount % 2,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray(),
						uniformGridStartCountBuffer = selfCollisionConstraint.uniformGridStartCountBuffer.GetNativeArray(),
						intersectQueue = selfCollisionConstraint.intersectQueue.AsParallelWriter()
					}, length2 * num, 1, jobHandle);
					job3 = new SelfCollisionConstraint.SelfConvertIntersectListJob
					{
						intersectQueue = selfCollisionConstraint.intersectQueue,
						intersectList = selfCollisionConstraint.intersectList
					}.Schedule(job3);
					flag5 = true;
				}
				for (int i = 0; i < teamMaxUpdateCount; i++)
				{
					bool flag6 = i == 0;
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_A_Job
					{
						updateIndex = i,
						simulationPower = time.SimulationPower,
						simulationDeltaTime = time.SimulationDeltaTime,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						centerDataArray = team.centerDataArray.GetNativeArray(),
						teamWindArray = team.teamWindArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						colliderFlagArray = collider.flagArray.GetNativeArray(),
						colliderSizeArray = collider.sizeArray.GetNativeArray(),
						colliderFramePositions = collider.framePositions.GetNativeArray(),
						colliderFrameRotations = collider.frameRotations.GetNativeArray(),
						colliderFrameScales = collider.frameScales.GetNativeArray(),
						colliderOldFramePositions = collider.oldFramePositions.GetNativeArray(),
						colliderOldFrameRotations = collider.oldFrameRotations.GetNativeArray(),
						colliderNowPositions = collider.nowPositions.GetNativeArray(),
						colliderNowRotations = collider.nowRotations.GetNativeArray(),
						colliderOldPositions = collider.oldPositions.GetNativeArray(),
						colliderOldRotations = collider.oldRotations.GetNativeArray(),
						colliderWorkDataArray = collider.workDataArray.GetNativeArray()
					}, length2, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_B_Job
					{
						workerCount = num,
						updateIndex = i,
						simulationPower = time.SimulationPower,
						simulationDeltaTime = time.SimulationDeltaTime,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						centerDataArray = team.centerDataArray.GetNativeArray(),
						teamWindArray = team.teamWindArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						windZoneCount = wind.WindCount,
						windDataArray = wind.windDataArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						depthArray = vMesh.vertexDepths.GetNativeArray(),
						positions = vMesh.positions.GetNativeArray(),
						rotations = vMesh.rotations.GetNativeArray(),
						vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
						nextPosArray = nextPosArray.GetNativeArray(),
						oldPosArray = oldPosArray.GetNativeArray(),
						basePosArray = basePosArray.GetNativeArray(),
						baseRotArray = baseRotArray.GetNativeArray(),
						oldPositionArray = oldPositionArray.GetNativeArray(),
						oldRotationArray = oldRotationArray.GetNativeArray(),
						velocityPosArray = velocityPosArray.GetNativeArray(),
						velocityArray = velocityArray.GetNativeArray(),
						frictionArray = frictionArray.GetNativeArray(),
						stepBasicPositionBuffer = stepBasicPositionBuffer,
						stepBasicRotationBuffer = stepBasicRotationBuffer
					}, length2 * num, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_C_Job
					{
						workerCount = num,
						updateIndex = i,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
						vertexParentIndices = vMesh.vertexParentIndices.GetNativeArray(),
						baseLineStartDataIndices = vMesh.baseLineStartDataIndices.GetNativeArray(),
						baseLineDataCounts = vMesh.baseLineDataCounts.GetNativeArray(),
						baseLineData = vMesh.baseLineData.GetNativeArray(),
						vertexLocalPositions = vMesh.vertexLocalPositions.GetNativeArray(),
						vertexLocalRotations = vMesh.vertexLocalRotations.GetNativeArray(),
						basePosArray = basePosArray.GetNativeArray(),
						baseRotArray = baseRotArray.GetNativeArray(),
						stepBasicPositionBuffer = stepBasicPositionBuffer,
						stepBasicRotationBuffer = stepBasicRotationBuffer
					}, length2 * num, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_D_Job
					{
						workerCount = num,
						updateIndex = i,
						simulationPower = time.SimulationPower,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						centerDataArray = team.centerDataArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						depthArray = vMesh.vertexDepths.GetNativeArray(),
						vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
						nextPosArray = nextPosArray.GetNativeArray(),
						basePosArray = basePosArray.GetNativeArray(),
						velocityPosArray = velocityPosArray.GetNativeArray(),
						frictionArray = frictionArray.GetNativeArray(),
						distanceIndexArray = distanceConstraint.indexArray.GetNativeArray(),
						distanceDataArray = distanceConstraint.dataArray.GetNativeArray(),
						distanceDistanceArray = distanceConstraint.distanceArray.GetNativeArray(),
						stepBasicPositionBuffer = stepBasicPositionBuffer
					}, length2 * num, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_Angle_Job
					{
						workerCount = num,
						updateIndex = i,
						simulationPower = time.SimulationPower,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						depthArray = vMesh.vertexDepths.GetNativeArray(),
						vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
						vertexParentIndices = vMesh.vertexParentIndices.GetNativeArray(),
						baseLineStartDataIndices = vMesh.baseLineStartDataIndices.GetNativeArray(),
						baseLineDataCounts = vMesh.baseLineDataCounts.GetNativeArray(),
						baseLineData = vMesh.baseLineData.GetNativeArray(),
						nextPosArray = nextPosArray.GetNativeArray(),
						velocityPosArray = velocityPosArray.GetNativeArray(),
						frictionArray = frictionArray.GetNativeArray(),
						distanceIndexArray = distanceConstraint.indexArray.GetNativeArray(),
						distanceDataArray = distanceConstraint.dataArray.GetNativeArray(),
						distanceDistanceArray = distanceConstraint.distanceArray.GetNativeArray(),
						stepBasicPositionBuffer = stepBasicPositionBuffer,
						stepBasicRotationBuffer = stepBasicRotationBuffer,
						tempVectorBufferA = tempVectorBufferA,
						tempVectorBufferB = tempVectorBufferB,
						tempFloatBufferA = tempFloatBufferA,
						tempRotationBufferA = tempRotationBufferA,
						tempRotationBufferB = tempRotationBufferB
					}, length2 * num, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_Triangle_Job
					{
						workerCount = num,
						updateIndex = i,
						simulationPower = time.SimulationPower,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						depthArray = vMesh.vertexDepths.GetNativeArray(),
						nextPosArray = nextPosArray.GetNativeArray(),
						frictionArray = frictionArray.GetNativeArray(),
						bendingTrianglePairArray = bendingConstraint.trianglePairArray.GetNativeArray(),
						bendingRestAngleOrVolumeArray = bendingConstraint.restAngleOrVolumeArray.GetNativeArray(),
						bendingSignOrVolumeArray = bendingConstraint.signOrVolumeArray.GetNativeArray(),
						tempVectorBufferA = tempVectorBufferA,
						tempCountBuffer = tempCountBuffer
					}, length2 * num, 1, job2);
					job2 = IJobParallelForExtensions.Schedule(new SplitStep_E_Job
					{
						workerCount = num,
						updateIndex = i,
						simulationPower = time.SimulationPower,
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						parameterArray = team.parameterArray.GetNativeArray(),
						attributes = vMesh.attributes.GetNativeArray(),
						depthArray = vMesh.vertexDepths.GetNativeArray(),
						nextPosArray = nextPosArray.GetNativeArray(),
						basePosArray = basePosArray.GetNativeArray(),
						velocityPosArray = velocityPosArray.GetNativeArray(),
						frictionArray = frictionArray.GetNativeArray(),
						collisionNormalArray = collisionNormalArray.GetNativeArray(),
						colliderFlagArray = collider.flagArray.GetNativeArray(),
						colliderWorkDataArray = collider.workDataArray.GetNativeArray(),
						tempVectorBufferA = tempVectorBufferA,
						tempCountBuffer = tempCountBuffer
					}, length2 * num, 1, job2);
					if (flag3)
					{
						job2 = IJobParallelForExtensions.Schedule(new SplitStep_Edge_Job
						{
							workerCount = num,
							updateIndex = i,
							simulationPower = time.SimulationPower,
							batchSelfTeamList = team.batchSplitClothTeamList,
							teamDataArray = team.teamDataArray.GetNativeArray(),
							parameterArray = team.parameterArray.GetNativeArray(),
							attributes = vMesh.attributes.GetNativeArray(),
							depthArray = vMesh.vertexDepths.GetNativeArray(),
							edges = vMesh.edges.GetNativeArray(),
							nextPosArray = nextPosArray.GetNativeArray(),
							colliderFlagArray = collider.flagArray.GetNativeArray(),
							colliderWorkDataArray = collider.workDataArray.GetNativeArray(),
							tempVectorBufferA = tempVectorBufferA,
							tempVectorBufferB = tempVectorBufferB,
							tempCountBuffer = tempCountBuffer,
							tempFloatBufferA = tempFloatBufferA
						}, length2 * num, 1, job2);
					}
					if (flag4)
					{
						job2 = IJobParallelForExtensions.Schedule(new SplitStep_F_Self_Job
						{
							workerCount = num,
							updateIndex = i,
							simulationPower = time.SimulationPower,
							batchSelfTeamList = team.batchSplitClothTeamList,
							teamDataArray = team.teamDataArray.GetNativeArray(),
							parameterArray = team.parameterArray.GetNativeArray(),
							attributes = vMesh.attributes.GetNativeArray(),
							depthArray = vMesh.vertexDepths.GetNativeArray(),
							nextPosArray = nextPosArray.GetNativeArray(),
							basePosArray = basePosArray.GetNativeArray(),
							baseRotArray = baseRotArray.GetNativeArray(),
							velocityPosArray = velocityPosArray.GetNativeArray(),
							frictionArray = frictionArray.GetNativeArray(),
							collisionNormalArray = collisionNormalArray.GetNativeArray(),
							distanceIndexArray = distanceConstraint.indexArray.GetNativeArray(),
							distanceDataArray = distanceConstraint.dataArray.GetNativeArray(),
							distanceDistanceArray = distanceConstraint.distanceArray.GetNativeArray(),
							tempVectorBufferA = tempVectorBufferA,
							tempVectorBufferB = tempVectorBufferB,
							tempCountBuffer = tempCountBuffer,
							tempFloatBufferA = tempFloatBufferA
						}, length2 * num, 1, job2);
						if (flag6 && flag5)
						{
							job2 = JobHandle.CombineDependencies(job3, job2);
						}
						if (flag6)
						{
							job2 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfStep_UpdatePrimitiveJob
							{
								workerCount = 3,
								updateIndex = i,
								simulationPower = time.SimulationPower,
								batchSelfTeamList = team.batchSplitClothTeamList,
								teamDataArray = team.teamDataArray.GetNativeArray(),
								parameterArray = team.parameterArray.GetNativeArray(),
								nextPosArray = nextPosArray.GetNativeArray(),
								oldPosArray = oldPosArray.GetNativeArray(),
								frictionArray = frictionArray.GetNativeArray(),
								primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray(),
								intersectFlagArray = selfCollisionConstraint.intersectFlagArray
							}, length2 * 3, 1, job2);
							job2 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfStep_UpdateGridJob
							{
								kindCount = 3,
								updateIndex = i,
								simulationPower = time.SimulationPower,
								batchSelfTeamList = team.batchSplitClothTeamList,
								teamDataArray = team.teamDataArray.GetNativeArray(),
								primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray(),
								uniformGridStartCountBuffer = selfCollisionConstraint.uniformGridStartCountBuffer.GetNativeArray()
							}, length2 * 3, 1, job2);
							job2 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfStep_DetectionContactJob
							{
								updateIndex = i,
								workerCount = num,
								teamCount = length2,
								batchSelfTeamList = team.batchSplitClothTeamList,
								teamDataArray = team.teamDataArray.GetNativeArray(),
								nextPosArray = nextPosArray.GetNativeArray(),
								oldPosArray = oldPosArray.GetNativeArray(),
								primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray(),
								uniformGridStartCountBuffer = selfCollisionConstraint.uniformGridStartCountBuffer.GetNativeArray(),
								contactQueue = selfCollisionConstraint.contactQueue.AsParallelWriter()
							}, length2 * 6 * num, 1, job2);
							job2 = new SelfCollisionConstraint.SelfStep_ConvertContactListJob
							{
								contactQueue = selfCollisionConstraint.contactQueue,
								contactList = selfCollisionConstraint.contactList
							}.Schedule(job2);
						}
						else
						{
							job2 = new SelfCollisionConstraint.SelfStep_UpdateContactJob
							{
								first = (i == 0),
								contactList = selfCollisionConstraint.contactList,
								nextPosArray = nextPosArray.GetNativeArray(),
								oldPosArray = oldPosArray.GetNativeArray(),
								primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray()
							}.Schedule(selfCollisionConstraint.contactList, 256, job2);
						}
						for (int j = 0; j < 4; j++)
						{
							job2 = new SelfCollisionConstraint.SelfStep_SolverContactJob
							{
								nextPosArray = nextPosArray.GetNativeArray(),
								primitiveArrayB = selfCollisionConstraint.primitiveArrayB.GetNativeArray(),
								contactList = selfCollisionConstraint.contactList,
								tempVectorBufferA = tempVectorBufferA,
								tempCountBuffer = tempCountBuffer
							}.Schedule(selfCollisionConstraint.contactList, 128, job2);
							job2 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfStep_SumContactJob
							{
								updateIndex = i,
								batchSelfTeamList = team.batchSplitClothTeamList,
								teamDataArray = team.teamDataArray.GetNativeArray(),
								nextPosArray = nextPosArray.GetNativeArray(),
								tempVectorBufferA = tempVectorBufferA,
								tempCountBuffer = tempCountBuffer
							}, length2, 1, job2);
						}
						job2 = IJobParallelForExtensions.Schedule(new SplitStep_G_Self_Job
						{
							workerCount = num,
							updateIndex = i,
							simulationDeltaTime = time.SimulationDeltaTime,
							batchSelfTeamList = team.batchSplitClothTeamList,
							teamDataArray = team.teamDataArray.GetNativeArray(),
							centerDataArray = team.centerDataArray.GetNativeArray(),
							parameterArray = team.parameterArray.GetNativeArray(),
							attributes = vMesh.attributes.GetNativeArray(),
							depthArray = vMesh.vertexDepths.GetNativeArray(),
							nextPosArray = nextPosArray.GetNativeArray(),
							oldPosArray = oldPosArray.GetNativeArray(),
							velocityPosArray = velocityPosArray.GetNativeArray(),
							velocityArray = velocityArray.GetNativeArray(),
							realVelocityArray = realVelocityArray.GetNativeArray(),
							frictionArray = frictionArray.GetNativeArray(),
							staticFrictionArray = staticFrictionArray.GetNativeArray(),
							collisionNormalArray = collisionNormalArray.GetNativeArray(),
							colliderNowPositions = collider.nowPositions.GetNativeArray(),
							colliderNowRotations = collider.nowRotations.GetNativeArray(),
							colliderOldPositions = collider.oldPositions.GetNativeArray(),
							colliderOldRotations = collider.oldRotations.GetNativeArray()
						}, length2 * num, 1, job2);
					}
					else
					{
						job2 = IJobParallelForExtensions.Schedule(new SplitStep_FG_NoSelf_Job
						{
							workerCount = num,
							updateIndex = i,
							simulationPower = time.SimulationPower,
							simulationDeltaTime = time.SimulationDeltaTime,
							batchSelfTeamList = team.batchSplitClothTeamList,
							teamDataArray = team.teamDataArray.GetNativeArray(),
							centerDataArray = team.centerDataArray.GetNativeArray(),
							parameterArray = team.parameterArray.GetNativeArray(),
							attributes = vMesh.attributes.GetNativeArray(),
							depthArray = vMesh.vertexDepths.GetNativeArray(),
							nextPosArray = nextPosArray.GetNativeArray(),
							oldPosArray = oldPosArray.GetNativeArray(),
							basePosArray = basePosArray.GetNativeArray(),
							baseRotArray = baseRotArray.GetNativeArray(),
							velocityPosArray = velocityPosArray.GetNativeArray(),
							velocityArray = velocityArray.GetNativeArray(),
							realVelocityArray = realVelocityArray.GetNativeArray(),
							frictionArray = frictionArray.GetNativeArray(),
							staticFrictionArray = staticFrictionArray.GetNativeArray(),
							collisionNormalArray = collisionNormalArray.GetNativeArray(),
							colliderNowPositions = collider.nowPositions.GetNativeArray(),
							colliderNowRotations = collider.nowRotations.GetNativeArray(),
							colliderOldPositions = collider.oldPositions.GetNativeArray(),
							colliderOldRotations = collider.oldRotations.GetNativeArray(),
							distanceIndexArray = distanceConstraint.indexArray.GetNativeArray(),
							distanceDataArray = distanceConstraint.dataArray.GetNativeArray(),
							distanceDistanceArray = distanceConstraint.distanceArray.GetNativeArray(),
							tempVectorBufferA = tempVectorBufferA,
							tempVectorBufferB = tempVectorBufferB,
							tempCountBuffer = tempCountBuffer,
							tempFloatBufferA = tempFloatBufferA
						}, length2 * num, 1, job2);
					}
				}
				job2 = IJobParallelForExtensions.Schedule(new SplitPost_DisplayPos_Job
				{
					workerCount = num,
					simulationDeltaTime = MagicaManager.Time.SimulationDeltaTime,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					attributes = vMesh.attributes.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
					oldPosArray = oldPosArray.GetNativeArray(),
					oldPositionArray = oldPositionArray.GetNativeArray(),
					oldRotationArray = oldRotationArray.GetNativeArray(),
					dispPosArray = dispPosArray.GetNativeArray(),
					realVelocityArray = realVelocityArray.GetNativeArray()
				}, length2 * num, 1, job2);
				if (flag5)
				{
					job4 = IJobParallelForExtensions.Schedule(new SelfCollisionConstraint.SelfClearIntersectJob
					{
						batchSelfTeamList = team.batchSplitClothTeamList,
						teamDataArray = team.teamDataArray.GetNativeArray(),
						intersectFlagArray = selfCollisionConstraint.intersectFlagArray
					}, length2, 1, job2);
					job4 = new SelfCollisionConstraint.SelfSolverIntersectJob
					{
						nextPosArray = nextPosArray.GetNativeArray(),
						intersectList = selfCollisionConstraint.intersectList,
						intersectFlagArray = selfCollisionConstraint.intersectFlagArray
					}.Schedule(selfCollisionConstraint.intersectList, 128, job4);
				}
				job2 = IJobParallelForExtensions.Schedule(new SplitPost_CalcProxy_Job
				{
					workerCount = num,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					attributes = vMesh.attributes.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					baseLineStartDataIndices = vMesh.baseLineStartDataIndices.GetNativeArray(),
					baseLineDataCounts = vMesh.baseLineDataCounts.GetNativeArray(),
					baseLineData = vMesh.baseLineData.GetNativeArray(),
					vertexLocalPositions = vMesh.vertexLocalPositions.GetNativeArray(),
					vertexLocalRotations = vMesh.vertexLocalRotations.GetNativeArray(),
					vertexChildIndexArray = vMesh.vertexChildIndexArray.GetNativeArray(),
					vertexChildDataArray = vMesh.vertexChildDataArray.GetNativeArray(),
					baseLineFlags = vMesh.baseLineFlags.GetNativeArray()
				}, length2 * num, 1, job2);
				job2 = IJobParallelForExtensions.Schedule(new SplitPost_CalcProxyTriangle_Job
				{
					workerCount = num,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					triangles = vMesh.triangles.GetNativeArray(),
					triangleNormals = vMesh.triangleNormals.GetNativeArray(),
					triangleTangents = vMesh.triangleTangents.GetNativeArray(),
					uvs = vMesh.uv.GetNativeArray()
				}, length2 * num, 1, job2);
				job2 = IJobParallelForExtensions.Schedule(new SplitPost_SumProxyTriangleAndTransform_Job
				{
					workerCount = num,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					transformPositionArray = bone.positionArray.GetNativeArray(),
					transformRotationArray = bone.rotationArray.GetNativeArray(),
					transformScaleArray = bone.scaleArray.GetNativeArray(),
					transformLocalPositionArray = bone.localPositionArray.GetNativeArray(),
					transformLocalRotationArray = bone.localRotationArray.GetNativeArray(),
					attributes = vMesh.attributes.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					vertexParentIndices = vMesh.vertexParentIndices.GetNativeArray(),
					triangleNormals = vMesh.triangleNormals.GetNativeArray(),
					triangleTangents = vMesh.triangleTangents.GetNativeArray(),
					vertexToTriangles = vMesh.vertexToTriangles.GetNativeArray(),
					normalAdjustmentRotations = vMesh.normalAdjustmentRotations.GetNativeArray(),
					vertexToTransformRotations = vMesh.vertexToTransformRotations.GetNativeArray()
				}, length2 * num, 1, job2);
				job2 = IJobParallelForExtensions.Schedule(new SplitPost_TeamCollider_Job
				{
					simulationDeltaTime = MagicaManager.Time.SimulationDeltaTime,
					batchSelfTeamList = team.batchSplitClothTeamList,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					centerDataArray = team.centerDataArray.GetNativeArray(),
					colliderFramePositions = collider.framePositions.GetNativeArray(),
					colliderFrameRotations = collider.frameRotations.GetNativeArray(),
					colliderOldFramePositions = collider.oldFramePositions.GetNativeArray(),
					colliderOldFrameRotations = collider.oldFrameRotations.GetNativeArray()
				}, length2, 1, job2);
				if (flag5)
				{
					job2 = JobHandle.CombineDependencies(job2, job4);
				}
			}
			if (flag)
			{
				job = IJobParallelForExtensions.Schedule(new SimulationNormalJob
				{
					batchNormalTeamList = team.batchNormalClothTeamList,
					simulationPower = time.SimulationPower,
					simulationDeltaTime = time.SimulationDeltaTime,
					mappingCount = team.MappingCount,
					teamDataArray = team.teamDataArray.GetNativeArray(),
					centerDataArray = team.centerDataArray.GetNativeArray(),
					teamWindArray = team.teamWindArray.GetNativeArray(),
					parameterArray = team.parameterArray.GetNativeArray(),
					windZoneCount = wind.WindCount,
					windDataArray = wind.windDataArray.GetNativeArray(),
					transformPositionArray = bone.positionArray.GetNativeArray(),
					transformRotationArray = bone.rotationArray.GetNativeArray(),
					transformScaleArray = bone.scaleArray.GetNativeArray(),
					transformLocalToWorldMatrixArray = bone.localToWorldMatrixArray.GetNativeArray(),
					transformLocalPositionArray = bone.localPositionArray.GetNativeArray(),
					transformLocalRotationArray = bone.localRotationArray.GetNativeArray(),
					attributes = vMesh.attributes.GetNativeArray(),
					depthArray = vMesh.vertexDepths.GetNativeArray(),
					localPositions = vMesh.localPositions.GetNativeArray(),
					localNormals = vMesh.localNormals.GetNativeArray(),
					localTangents = vMesh.localTangents.GetNativeArray(),
					boneWeights = vMesh.boneWeights.GetNativeArray(),
					skinBoneTransformIndices = vMesh.skinBoneTransformIndices.GetNativeArray(),
					skinBoneBindPoses = vMesh.skinBoneBindPoses.GetNativeArray(),
					positions = vMesh.positions.GetNativeArray(),
					rotations = vMesh.rotations.GetNativeArray(),
					vertexBindPoseRotations = vMesh.vertexBindPoseRotations.GetNativeArray(),
					vertexDepths = vMesh.vertexDepths.GetNativeArray(),
					vertexRootIndices = vMesh.vertexRootIndices.GetNativeArray(),
					vertexParentIndices = vMesh.vertexParentIndices.GetNativeArray(),
					baseLineStartDataIndices = vMesh.baseLineStartDataIndices.GetNativeArray(),
					baseLineDataCounts = vMesh.baseLineDataCounts.GetNativeArray(),
					baseLineData = vMesh.baseLineData.GetNativeArray(),
					vertexLocalPositions = vMesh.vertexLocalPositions.GetNativeArray(),
					vertexLocalRotations = vMesh.vertexLocalRotations.GetNativeArray(),
					vertexChildIndexArray = vMesh.vertexChildIndexArray.GetNativeArray(),
					vertexChildDataArray = vMesh.vertexChildDataArray.GetNativeArray(),
					baseLineFlags = vMesh.baseLineFlags.GetNativeArray(),
					triangles = vMesh.triangles.GetNativeArray(),
					triangleNormals = vMesh.triangleNormals.GetNativeArray(),
					triangleTangents = vMesh.triangleTangents.GetNativeArray(),
					uvs = vMesh.uv.GetNativeArray(),
					vertexToTriangles = vMesh.vertexToTriangles.GetNativeArray(),
					normalAdjustmentRotations = vMesh.normalAdjustmentRotations.GetNativeArray(),
					vertexToTransformRotations = vMesh.vertexToTransformRotations.GetNativeArray(),
					edges = vMesh.edges.GetNativeArray(),
					nextPosArray = nextPosArray.GetNativeArray(),
					oldPosArray = oldPosArray.GetNativeArray(),
					oldRotArray = oldRotArray.GetNativeArray(),
					basePosArray = basePosArray.GetNativeArray(),
					baseRotArray = baseRotArray.GetNativeArray(),
					oldPositionArray = oldPositionArray.GetNativeArray(),
					oldRotationArray = oldRotationArray.GetNativeArray(),
					velocityPosArray = velocityPosArray.GetNativeArray(),
					dispPosArray = dispPosArray.GetNativeArray(),
					velocityArray = velocityArray.GetNativeArray(),
					realVelocityArray = realVelocityArray.GetNativeArray(),
					frictionArray = frictionArray.GetNativeArray(),
					staticFrictionArray = staticFrictionArray.GetNativeArray(),
					collisionNormalArray = collisionNormalArray.GetNativeArray(),
					colliderFlagArray = collider.flagArray.GetNativeArray(),
					colliderCenterArray = collider.centerArray.GetNativeArray(),
					colliderSizeArray = collider.sizeArray.GetNativeArray(),
					colliderFramePositions = collider.framePositions.GetNativeArray(),
					colliderFrameRotations = collider.frameRotations.GetNativeArray(),
					colliderFrameScales = collider.frameScales.GetNativeArray(),
					colliderOldFramePositions = collider.oldFramePositions.GetNativeArray(),
					colliderOldFrameRotations = collider.oldFrameRotations.GetNativeArray(),
					colliderNowPositions = collider.nowPositions.GetNativeArray(),
					colliderNowRotations = collider.nowRotations.GetNativeArray(),
					colliderOldPositions = collider.oldPositions.GetNativeArray(),
					colliderOldRotations = collider.oldRotations.GetNativeArray(),
					colliderWorkDataArray = collider.workDataArray.GetNativeArray(),
					fixedArray = inertiaConstraint.fixedArray.GetNativeArray(),
					distanceIndexArray = distanceConstraint.indexArray.GetNativeArray(),
					distanceDataArray = distanceConstraint.dataArray.GetNativeArray(),
					distanceDistanceArray = distanceConstraint.distanceArray.GetNativeArray(),
					bendingTrianglePairArray = bendingConstraint.trianglePairArray.GetNativeArray(),
					bendingRestAngleOrVolumeArray = bendingConstraint.restAngleOrVolumeArray.GetNativeArray(),
					bendingSignOrVolumeArray = bendingConstraint.signOrVolumeArray.GetNativeArray(),
					stepBasicPositionBuffer = stepBasicPositionBuffer,
					stepBasicRotationBuffer = stepBasicRotationBuffer,
					tempVectorBufferA = tempVectorBufferA,
					tempVectorBufferB = tempVectorBufferB,
					tempCountBuffer = tempCountBuffer,
					tempFloatBufferA = tempFloatBufferA,
					tempRotationBufferA = tempRotationBufferA,
					tempRotationBufferB = tempRotationBufferB
				}, length, 1, jobHandle);
			}
			jobHandle = JobHandle.CombineDependencies(job2, job);
			if (MagicaManager.Team.MappingCount > 0)
			{
				JobHandle job5 = vMesh.PostMappingMeshUpdateBatchSchedule(jobHandle, num);
				JobHandle job6 = bone.WriteTransformSchedule(jobHandle);
				jobHandle = JobHandle.CombineDependencies(job5, job6);
			}
			else
			{
				jobHandle = bone.WriteTransformSchedule(jobHandle);
			}
			return jobHandle;
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Simulation Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Simulation Manager. Invalid");
			}
			else
			{
				stringBuilder.AppendLine($"Simulation Manager. Particle:{ParticleCount}");
				stringBuilder.AppendLine("  -teamIdArray:" + teamIdArray.ToSummary());
				stringBuilder.AppendLine("  -nextPosArray:" + nextPosArray.ToSummary());
				stringBuilder.AppendLine("  -oldPosArray:" + oldPosArray.ToSummary());
				stringBuilder.AppendLine("  -oldRotArray:" + oldRotArray.ToSummary());
				stringBuilder.AppendLine("  -basePosArray:" + basePosArray.ToSummary());
				stringBuilder.AppendLine("  -baseRotArray:" + baseRotArray.ToSummary());
				stringBuilder.AppendLine("  -oldPositionArray:" + oldPositionArray.ToSummary());
				stringBuilder.AppendLine("  -oldRotationArray:" + oldRotationArray.ToSummary());
				stringBuilder.AppendLine("  -velocityPosArray:" + velocityPosArray.ToSummary());
				stringBuilder.AppendLine("  -dispPosArray:" + dispPosArray.ToSummary());
				stringBuilder.AppendLine("  -velocityArray:" + velocityArray.ToSummary());
				stringBuilder.AppendLine("  -realVelocityArray:" + realVelocityArray.ToSummary());
				stringBuilder.AppendLine("  -frictionArray:" + frictionArray.ToSummary());
				stringBuilder.AppendLine("  -staticFrictionArray:" + staticFrictionArray.ToSummary());
				stringBuilder.AppendLine("  -collisionNormalArray:" + collisionNormalArray.ToSummary());
				stringBuilder.Append(distanceConstraint.ToString());
				stringBuilder.Append(bendingConstraint.ToString());
				stringBuilder.Append(angleConstraint.ToString());
				stringBuilder.Append(inertiaConstraint.ToString());
				stringBuilder.Append(colliderCollisionConstraint.ToString());
				stringBuilder.Append(selfCollisionConstraint.ToString());
				stringBuilder.AppendLine("[Buffer]");
				stringBuilder.AppendLine($"  -stepBasicPositionBuffer:{(stepBasicPositionBuffer.IsCreated ? stepBasicPositionBuffer.Length : 0)}");
				stringBuilder.AppendLine($"  -stepBasicRotationBuffer:{(stepBasicRotationBuffer.IsCreated ? stepBasicRotationBuffer.Length : 0)}");
				stringBuilder.AppendLine($"  -tempVectorBufferA:{(tempVectorBufferA.IsCreated ? tempVectorBufferA.Length : 0)}");
				stringBuilder.AppendLine($"  -tempVectorBufferB:{(tempVectorBufferB.IsCreated ? tempVectorBufferB.Length : 0)}");
				stringBuilder.AppendLine($"  -tempCountBuffer:{(tempCountBuffer.IsCreated ? tempCountBuffer.Length : 0)}");
				stringBuilder.AppendLine($"  -tempFloatBufferA:{(tempFloatBufferA.IsCreated ? tempFloatBufferA.Length : 0)}");
				stringBuilder.AppendLine($"  -tempRotationBufferA:{(tempRotationBufferA.IsCreated ? tempRotationBufferA.Length : 0)}");
				stringBuilder.AppendLine($"  -tempRotationBufferB:{(tempRotationBufferB.IsCreated ? tempRotationBufferB.Length : 0)}");
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}

		private static void SimulationPreTeamUpdate(DataChunk chunk, ref TeamManager.TeamData tdata, in ClothParameters param, in InertiaConstraint.CenterData cdata, in NativeArray<float3> positions, in NativeArray<quaternion> rotations, in NativeArray<float> vertexDepths, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> oldPosArray, ref NativeArray<quaternion> oldRotArray, ref NativeArray<float3> basePosArray, ref NativeArray<quaternion> baseRotArray, ref NativeArray<float3> oldPositionArray, ref NativeArray<quaternion> oldRotationArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float3> dispPosArray, ref NativeArray<float3> velocityArray, ref NativeArray<float3> realVelocityArray, ref NativeArray<float> frictionArray, ref NativeArray<float> staticFrictionArray, ref NativeArray<float3> collisionNormalArray)
		{
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			if (tdata.IsReset)
			{
				int num3 = 0;
				while (num3 < chunk.dataLength)
				{
					float3 value = positions[num2];
					quaternion value2 = rotations[num2];
					nextPosArray[num] = value;
					oldPosArray[num] = value;
					oldRotArray[num] = value2;
					basePosArray[num] = value;
					baseRotArray[num] = value2;
					oldPositionArray[num] = value;
					oldRotationArray[num] = value2;
					velocityPosArray[num] = value;
					dispPosArray[num] = value;
					velocityArray[num] = 0;
					realVelocityArray[num] = 0;
					frictionArray[num] = 0f;
					staticFrictionArray[num] = 0f;
					collisionNormalArray[num] = 0;
					num3++;
					num++;
					num2++;
				}
			}
			else
			{
				if (!tdata.IsInertiaShift && !tdata.IsNegativeScaleTeleport)
				{
					return;
				}
				int num4 = 0;
				while (num4 < chunk.dataLength)
				{
					float3 pos = oldPosArray[num];
					quaternion rot = oldRotArray[num];
					float3 pos2 = oldPositionArray[num];
					quaternion rot2 = oldRotationArray[num];
					float3 pos3 = dispPosArray[num];
					float3 vec = velocityArray[num];
					float3 vec2 = realVelocityArray[num];
					if (tdata.IsNegativeScaleTeleport)
					{
						float4x4 m = cdata.negativeScaleMatrix;
						pos = MathUtility.TransformPoint(in pos, in m);
						rot = MathUtility.TransformRotation(in rot, in m, (float3)1);
						pos2 = MathUtility.TransformPoint(in pos2, in m);
						rot2 = MathUtility.TransformRotation(in rot2, in m, (float3)1);
						pos3 = MathUtility.TransformPoint(in pos3, in m);
						vec = MathUtility.TransformVector(in vec, in m);
						vec2 = MathUtility.TransformVector(in vec2, in m);
					}
					if (tdata.IsInertiaShift)
					{
						pos = MathUtility.ShiftPosition(in pos, in cdata.oldComponentWorldPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
						rot = math.mul(cdata.frameComponentShiftRotation, rot);
						pos2 = MathUtility.ShiftPosition(in pos2, in cdata.oldComponentWorldPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
						rot2 = math.mul(cdata.frameComponentShiftRotation, rot2);
						pos3 = MathUtility.ShiftPosition(in pos3, in cdata.oldComponentWorldPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
						vec = math.mul(cdata.frameComponentShiftRotation, vec);
						vec2 = math.mul(cdata.frameComponentShiftRotation, vec2);
					}
					oldPosArray[num] = pos;
					oldRotArray[num] = rot;
					oldPositionArray[num] = pos2;
					oldRotationArray[num] = rot2;
					dispPosArray[num] = pos3;
					velocityArray[num] = vec;
					realVelocityArray[num] = vec2;
					num4++;
					num++;
					num2++;
				}
			}
		}

		private static float3 WindBatchJob(int teamId, in WindParams windParams, int vindex, int pindex, float depth, ref NativeArray<int> vertexRootIndices, ref TeamWindData teamWindData, ref NativeArray<WindManager.WindData> windDataArray, ref NativeArray<float> frictionArray)
		{
			float3 float5 = 0;
			int num = vertexRootIndices[vindex];
			float3 windPos = (float)(teamId + 1) * 4.1923065f + (float)num * 0.0023963f * (1f - windParams.synchronization) * 100f;
			int zoneCount = teamWindData.ZoneCount;
			for (int i = 0; i < zoneCount; i++)
			{
				TeamWindInfo windInfo = teamWindData.windZoneList[i];
				float5 += WindForceBlendBatchJob(in windInfo, in windParams, in windPos, windDataArray[windInfo.windId].turbulence);
			}
			if (windParams.movingWind > 0.01f)
			{
				float5 += WindForceBlendBatchJob(in teamWindData.movingWind, in windParams, in windPos, 1f);
			}
			float influence = windParams.influence;
			float num2 = frictionArray[pindex];
			influence *= 1f - num2;
			float end = depth * depth;
			influence *= math.lerp(1f, end, windParams.depthWeight);
			return float5 * influence;
		}

		private static void SimulationStepUpdateParticles(DataChunk chunk, float4 simulationPower, float simulationDeltaTime, int teamId, ref TeamManager.TeamData tdata, ref InertiaConstraint.CenterData cdata, ref ClothParameters param, ref TeamWindData wdata, ref NativeArray<WindManager.WindData> windDataArray, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> depthArray, ref NativeArray<float3> positions, ref NativeArray<quaternion> rotations, ref NativeArray<int> vertexRootIndices, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> oldPosArray, ref NativeArray<float3> basePosArray, ref NativeArray<quaternion> baseRotArray, ref NativeArray<float3> oldPositionArray, ref NativeArray<quaternion> oldRotationArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float3> velocityArray, ref NativeArray<float> frictionArray, ref NativeArray<float3> stepBasicPositionBuffer, ref NativeArray<quaternion> stepBasicRotationBuffer)
		{
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				VertexAttribute vertexAttribute = attributes[num2];
				float num4 = depthArray[num2];
				float3 float5 = oldPosArray[num];
				float3 float6 = float5;
				float3 value = float5;
				float3 start = oldPositionArray[num];
				quaternion q = oldRotationArray[num];
				float3 end = positions[num2];
				quaternion q2 = rotations[num2];
				float3 basePos = math.lerp(start, end, tdata.frameInterpolation);
				quaternion q3 = math.slerp(q, q2, tdata.frameInterpolation);
				q3 = math.normalize(q3);
				basePosArray[num] = basePos;
				baseRotArray[num] = q3;
				stepBasicPositionBuffer[num] = basePos;
				stepBasicRotationBuffer[num] = q3;
				if (vertexAttribute.IsMove() || tdata.IsSpring)
				{
					float3 v = velocityArray[num];
					float3 inertiaVector = cdata.inertiaVector;
					quaternion inertiaRotation = cdata.inertiaRotation;
					float t = param.inertiaConstraint.depthInertia * (1f - num4 * num4);
					inertiaVector = math.lerp(inertiaVector, cdata.stepVector, t);
					quaternion q4 = math.slerp(inertiaRotation, cdata.stepRotation, t);
					float3 v2 = float5 - cdata.oldWorldPosition;
					v2 = math.mul(q4, v2);
					v2 += inertiaVector;
					float3 obj = cdata.oldWorldPosition + v2;
					float3 float7 = obj - float6;
					float6 = obj;
					value += float7;
					v = math.mul(q4, v);
					v *= tdata.velocityWeight;
					float num5 = param.dampingCurveData.MC2EvaluateCurveClamp01(num4);
					v *= math.saturate(1f - num5 * simulationPower.z);
					float3 float8 = 0;
					float3 float9 = param.worldGravityDirection * (param.gravity * tdata.gravityRatio);
					float8 += float9;
					float3 float10 = 0;
					float num6 = MathUtility.CalcMass(num4);
					switch (tdata.forceMode)
					{
					case ClothForceMode.VelocityAdd:
						float10 = tdata.impactForce / num6;
						break;
					case ClothForceMode.VelocityAddWithoutDepth:
						float10 = tdata.impactForce;
						break;
					case ClothForceMode.VelocityChange:
						float10 = tdata.impactForce / num6;
						v = 0;
						break;
					case ClothForceMode.VelocityChangeWithoutDepth:
						float10 = tdata.impactForce;
						v = 0;
						break;
					}
					float8 += float10;
					float8 += WindBatchJob(teamId, in param.wind, num2, num, num4, ref vertexRootIndices, ref wdata, ref windDataArray, ref frictionArray);
					float8 *= tdata.scaleRatio;
					v += float8 * simulationDeltaTime;
					float6 += v * simulationDeltaTime;
				}
				else
				{
					float6 = basePos;
					value = basePos;
				}
				if (tdata.IsSpring && vertexAttribute.IsFixed())
				{
					SpringBatchJob(in param.springConstraint, param.normalAxis, ref float6, in basePos, in q3, (tdata.time + (float)num * 49.6198f) * 2.4512f + math.csum(float6), tdata.scaleRatio);
				}
				velocityPosArray[num] = value;
				nextPosArray[num] = float6;
				num3++;
				num++;
				num2++;
			}
		}

		private static void SpringBatchJob(in SpringConstraint.SpringConstraintParams springParams, ClothNormalAxis normalAxis, ref float3 nextPos, in float3 basePos, in quaternion baseRot, float noiseTime, float scaleRatio)
		{
			float3 float5 = nextPos - basePos;
			float3 v = math.up();
			switch (normalAxis)
			{
			case ClothNormalAxis.Right:
				v = math.right();
				break;
			case ClothNormalAxis.Up:
				v = math.up();
				break;
			case ClothNormalAxis.Forward:
				v = math.forward();
				break;
			case ClothNormalAxis.InverseRight:
				v = -math.right();
				break;
			case ClothNormalAxis.InverseUp:
				v = -math.up();
				break;
			case ClothNormalAxis.InverseForward:
				v = -math.forward();
				break;
			}
			v = math.mul(baseRot, v);
			float num = springParams.limitDistance * scaleRatio;
			if (num > 1E-08f)
			{
				float num2 = math.length(float5);
				if (num2 > num)
				{
					float5 *= num / num2;
				}
				if (springParams.normalLimitRatio < 1f)
				{
					float num3 = math.dot(v, float5);
					float num4 = math.cos(math.asin(math.length(float5 - v * num3) / num));
					num4 *= num * springParams.normalLimitRatio;
					if (math.abs(num3) > num4)
					{
						float5 -= v * (math.abs(num3) - num4) * math.sign(num3);
					}
				}
			}
			else
			{
				float5 = float3.zero;
			}
			float num5 = springParams.springPower;
			if (springParams.springNoise > 0f)
			{
				float num6 = math.sin(noiseTime);
				num6 *= springParams.springNoise * 0.6f;
				num5 = math.max(num5 + num5 * num6, 0f);
			}
			float5 -= float5 * num5;
			nextPos = basePos + float5;
		}

		private static void SimulationStepUpdateBaseLinePose(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<VertexAttribute> attributes, ref NativeArray<int> vertexParentIndices, ref NativeArray<ushort> baseLineStartDataIndices, ref NativeArray<ushort> baseLineDataCounts, ref NativeArray<ushort> baseLineData, ref NativeArray<float3> vertexLocalPositions, ref NativeArray<quaternion> vertexLocalRotations, ref NativeArray<float3> basePosArray, ref NativeArray<quaternion> baseRotArray, ref NativeArray<float3> stepBasicPositionBuffer, ref NativeArray<quaternion> stepBasicRotationBuffer)
		{
			float animationPoseRatio = tdata.animationPoseRatio;
			if (!(animationPoseRatio <= 0.99f))
			{
				return;
			}
			int startIndex = tdata.baseLineDataChunk.startIndex;
			int startIndex2 = tdata.particleChunk.startIndex;
			int startIndex3 = tdata.proxyCommonChunk.startIndex;
			float3 float5 = tdata.initScale * tdata.scaleRatio;
			int num = tdata.baseLineChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				int num3 = baseLineStartDataIndices[num];
				int num4 = baseLineDataCounts[num];
				int num5 = num3 + startIndex;
				int num6 = 0;
				while (num6 < num4)
				{
					int num7 = baseLineData[num5];
					int index = startIndex2 + num7;
					int index2 = startIndex3 + num7;
					int num8 = vertexParentIndices[index2];
					int index3 = num8 + startIndex2;
					if (attributes[index2].IsMove() && num8 >= 0)
					{
						float3 float6 = vertexLocalPositions[index2];
						quaternion quaternion2 = vertexLocalRotations[index2];
						float3 float7 = stepBasicPositionBuffer[index3];
						quaternion quaternion3 = stepBasicRotationBuffer[index3];
						float6 *= tdata.negativeScaleDirection;
						quaternion2 = quaternion2.value * tdata.negativeScaleQuaternionValue;
						stepBasicPositionBuffer[index] = math.mul(quaternion3, float6 * float5) + float7;
						stepBasicRotationBuffer[index] = math.mul(quaternion3, quaternion2);
					}
					else
					{
						quaternion rotation = stepBasicRotationBuffer[index];
						float4x4 float4x5 = float4x4.TRS(0, rotation, tdata.negativeScaleDirection);
						quaternion value = MathUtility.ToRotation(float4x5.c1.xyz, float4x5.c2.xyz);
						stepBasicRotationBuffer[index] = value;
					}
					num6++;
					num5++;
				}
				if (animationPoseRatio > 1E-08f)
				{
					num5 = num3 + startIndex;
					int num9 = 0;
					while (num9 < num4)
					{
						int num10 = baseLineData[num5];
						int index4 = startIndex2 + num10;
						float3 end = basePosArray[index4];
						quaternion q = baseRotArray[index4];
						stepBasicPositionBuffer[index4] = math.lerp(stepBasicPositionBuffer[index4], end, animationPoseRatio);
						stepBasicRotationBuffer[index4] = math.slerp(stepBasicRotationBuffer[index4], q, animationPoseRatio);
						num9++;
						num5++;
					}
				}
				num2++;
				num++;
			}
		}

		private static float3 WindForceBlendBatchJob(in TeamWindInfo windInfo, in WindParams windParams, in float3 windPos, float windTurbulence)
		{
			float main = windInfo.main;
			if (main < 0.01f)
			{
				return 0;
			}
			float num = main / 7.5f;
			float2 start = math.sin((windPos + windInfo.time * 10f).xy);
			float3 float5 = windPos + windInfo.time * 2.3132f;
			float2 end = new float2(noise.cnoise(float5.xy), noise.cnoise(float5.yx));
			end *= 2.3f;
			float2 float6 = math.lerp(start, end, windParams.blend);
			windTurbulence *= windParams.turbulence;
			float2 float7 = math.radians(float6 * 45f);
			float7.y *= math.lerp(0.1f, 0.5f, windParams.blend);
			float7 *= windTurbulence;
			quaternion b = quaternion.Euler(float7.x, float7.y, 0f);
			float3 obj = math.forward(math.mul(MathUtility.AxisQuaternion(windInfo.direction), b));
			float num2 = math.saturate(1f - num * 1f);
			float num3 = math.unlerp(-1f, 1f, float6.x);
			num3 *= num2 * windTurbulence;
			main -= main * num3;
			return obj * main;
		}

		private static void SimulationStepPostTeam(DataChunk chunk, float simulationDeltaTime, int teamId, ref TeamManager.TeamData tdata, ref InertiaConstraint.CenterData cdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> depthArray, ref NativeArray<float3> oldPosArray, ref NativeArray<float3> velocityArray, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> velocityPosArray, ref NativeArray<float> frictionArray, ref NativeArray<float> staticFrictionArray, ref NativeArray<float3> collisionNormalArray, ref NativeArray<float3> realVelocityArray)
		{
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = tdata.proxyCommonChunk.startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				VertexAttribute vertexAttribute = attributes[num2];
				float num4 = depthArray[num2];
				float3 float5 = nextPosArray[num];
				float3 float6 = oldPosArray[num];
				if (vertexAttribute.IsMove() || tdata.IsSpring)
				{
					float3 float7 = velocityPosArray[num];
					float num5 = frictionArray[num];
					float3 n = collisionNormalArray[num];
					bool flag = math.lengthsq(n) > 1E-08f;
					float num6 = param.colliderCollisionConstraint.staticFriction * tdata.scaleRatio;
					float dynamicFriction = param.colliderCollisionConstraint.dynamicFriction;
					float num7 = staticFrictionArray[num];
					if (flag && num5 > 0f && num6 > 0f)
					{
						float3 v = float5 - float6;
						float3 float8 = v - MathUtility.Project(in v, in n);
						float num8 = math.length(float8) / simulationDeltaTime;
						if (num8 < num6)
						{
							num7 = math.saturate(num7 + 0.04f);
						}
						else
						{
							float num9 = math.max((num8 - num6) / 0.2f, 0.05f);
							num7 = math.saturate(num7 - num9);
						}
						float8 *= num7;
						float5 -= float8;
						float7 -= float8;
					}
					else
					{
						num7 = math.saturate(num7 - 0.05f);
					}
					staticFrictionArray[num] = num7;
					float3 float9 = (float5 - float7) / simulationDeltaTime;
					float num10 = math.lengthsq(float9);
					float3 float10 = ((num10 > 1E-08f) ? math.normalize(float9) : ((float3)0));
					if (num5 > 1E-08f && flag && dynamicFriction > 0f && num10 >= 1E-08f)
					{
						float num11 = math.dot(n, float10);
						num11 = 0.5f + 0.5f * num11;
						num11 *= num11;
						num11 = 1f - num11;
						float9 -= float9 * (num11 * math.saturate(num5 * dynamicFriction));
					}
					num5 *= 0.6f;
					frictionArray[num] = num5;
					if (param.inertiaConstraint.particleSpeedLimit >= 0f)
					{
						float9 = MathUtility.ClampVector(float9, param.inertiaConstraint.particleSpeedLimit * tdata.scaleRatio);
					}
					if (cdata.angularVelocity > 1E-08f && param.inertiaConstraint.centrifualAcceleration > 1E-08f && num10 >= 1E-08f)
					{
						float3 float11 = MathUtility.ProjectOnPlane(float5 - cdata.nowWorldPosition, in cdata.rotationAxis);
						float num12 = math.length(float11);
						if (num12 > 1E-08f)
						{
							float3 float12 = float11 / num12;
							float angularVelocity = cdata.angularVelocity;
							float num13 = (1f + (1f - num4)) * angularVelocity * angularVelocity * num12;
							float3 y = math.normalize(math.cross(cdata.rotationAxis, float12));
							num13 *= math.saturate(math.dot(float10, y));
							float9 += float12 * (num13 * param.inertiaConstraint.centrifualAcceleration * 0.02f);
						}
					}
					float9 *= tdata.velocityWeight;
					velocityArray[num] = float9;
				}
				float3 value = (float5 - float6) / simulationDeltaTime;
				realVelocityArray[num] = value;
				oldPosArray[num] = float5;
				num3++;
				num++;
				num2++;
			}
		}

		private static void SimulationCalcDisplayPosition(DataChunk chunk, float simulationDeltaTime, ref TeamManager.TeamData tdata, ref NativeArray<float3> oldPosArray, ref NativeArray<float3> realVelocityArray, ref NativeArray<float3> oldPositionArray, ref NativeArray<quaternion> oldRotationArray, ref NativeArray<float3> dispPosArray, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float3> positions, ref NativeArray<quaternion> rotations, ref NativeArray<int> vertexRootIndices)
		{
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int startIndex = tdata.proxyCommonChunk.startIndex;
			int num2 = startIndex + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				VertexAttribute vertexAttribute = attributes[num2];
				float3 float5 = positions[num2];
				quaternion quaternion2 = rotations[num2];
				if (vertexAttribute.IsMove() || tdata.IsSpring)
				{
					float3 obj = oldPosArray[num];
					float3 float6 = realVelocityArray[num] * simulationDeltaTime;
					float3 end = obj + float6;
					float num4 = tdata.nowUpdateTime + simulationDeltaTime - tdata.oldTime;
					float t = ((num4 > 0f) ? ((tdata.time - tdata.oldTime) / num4) : 0f);
					end = math.lerp(dispPosArray[num], end, t);
					int num5 = vertexRootIndices[num2];
					if (num5 >= 0)
					{
						float3 float7 = positions[startIndex + num5];
						float maxlength = math.distance(float7, float5) * 1.3f;
						float3 v = end - float7;
						v = MathUtility.ClampVector(v, maxlength);
						end = float7 + v;
					}
					float3 value = math.lerp(end: dispPosArray[num] = end, start: positions[num2], t: tdata.blendWeight);
					positions[num2] = value;
				}
				else
				{
					float3 value2 = positions[num2];
					dispPosArray[num] = value2;
				}
				if (tdata.IsRunning)
				{
					oldPositionArray[num] = float5;
					oldRotationArray[num] = quaternion2;
				}
				if (tdata.IsNegativeScale)
				{
					float4x4 float4x5 = float4x4.TRS(0, quaternion2, tdata.negativeScaleDirection);
					quaternion2 = MathUtility.ToRotation(float4x5.c1.xyz, float4x5.c2.xyz);
					rotations[num2] = quaternion2;
				}
				num3++;
				num++;
				num2++;
			}
		}

		private static void SimulationClearTempBuffer(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<float3> tempVectorBufferA, ref NativeArray<float3> tempVectorBufferB, ref NativeArray<int> tempCountBuffer, ref NativeArray<float> tempFloatBufferA)
		{
			int num = tdata.particleChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				tempVectorBufferA[num] = 0;
				tempVectorBufferB[num] = 0;
				tempCountBuffer[num] = 0;
				tempFloatBufferA[num] = 0f;
				num2++;
				num++;
			}
		}
	}
}
