using System;
using System.Collections.Generic;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace MagicaCloth2
{
	public class TeamManager : IManager, IDisposable, IValid
	{
		public struct TeamData
		{
			public BitField64 flag;

			public ClothUpdateMode originalUpdateMode;

			public ClothUpdateMode updateMode;

			public float frameDeltaTime;

			public float time;

			public float oldTime;

			public float nowUpdateTime;

			public float oldUpdateTime;

			public float frameUpdateTime;

			public float frameOldTime;

			public float timeScale;

			public float nowTimeScale;

			public int updateCount;

			public int skipCount;

			public float frameInterpolation;

			public float gravityRatio;

			public float gravityDot;

			public int centerTransformIndex;

			public int distanceReferenceObjectId;

			public int componentTransformIndex;

			public float3 initScale;

			public float scaleRatio;

			public float negativeScaleSign;

			public float3 negativeScaleDirection;

			public float3 negativeScaleChange;

			public float2 negativeScaleTriangleSign;

			public float4 negativeScaleQuaternionValue;

			public int componentId;

			public int syncTeamId;

			public FixedList32Bytes<int> syncParentTeamId;

			public int syncCenterTransformIndex;

			public int interlockingAnimatorId;

			public float animationPoseRatio;

			public float velocityWeight;

			public float distanceWeight;

			public float blendWeight;

			public ClothForceMode forceMode;

			public float3 impactForce;

			public VirtualMesh.MeshType proxyMeshType;

			public DataChunk proxyTransformChunk;

			public DataChunk proxyCommonChunk;

			public DataChunk proxyVertexChildDataChunk;

			public DataChunk proxyTriangleChunk;

			public DataChunk proxyEdgeChunk;

			public DataChunk proxyMeshChunk;

			public DataChunk proxyBoneChunk;

			public DataChunk proxySkinBoneChunk;

			public DataChunk baseLineChunk;

			public DataChunk baseLineDataChunk;

			public DataChunk fixedDataChunk;

			public DataChunk particleChunk;

			public DataChunk colliderChunk;

			public DataChunk colliderTransformChunk;

			public int colliderCount;

			public DataChunk distanceStartChunk;

			public DataChunk distanceDataChunk;

			public DataChunk bendingPairChunk;

			public DataChunk selfPointChunk;

			public DataChunk selfEdgeChunk;

			public DataChunk selfTriangleChunk;

			public float selfGridSize;

			public int selfPointGridCount;

			public int selfEdgeGridCount;

			public int selfTriangleGridCount;

			public float selfMaxPrimitiveSize;

			public bool IsFixedUpdate => updateMode == ClothUpdateMode.UnityPhysics;

			public bool IsUnscaled => updateMode == ClothUpdateMode.Unscaled;

			public bool IsValid => flag.IsSet(0);

			public bool IsEnable => flag.IsSet(1);

			public bool IsProcess
			{
				get
				{
					if (IsEnable && !flag.IsSet(4))
					{
						return !IsCullingInvisible;
					}
					return false;
				}
			}

			public bool IsReset => flag.IsSet(2);

			public bool IsKeepReset => flag.IsSet(9);

			public bool IsInertiaShift => flag.IsSet(10);

			public bool IsRunning => flag.IsSet(5);

			public bool IsStepRunning => flag.IsSet(7);

			public bool IsCameraCullingInvisible => flag.IsSet(11);

			public bool IsCameraCullingKeep => flag.IsSet(12);

			public bool IsDistanceCullingInvisible => flag.IsSet(19);

			public bool IsCullingInvisible
			{
				get
				{
					if (!IsCameraCullingInvisible)
					{
						return IsDistanceCullingInvisible;
					}
					return true;
				}
			}

			public bool IsSpring => flag.IsSet(13);

			public bool IsNegativeScale => flag.IsSet(17);

			public bool IsNegativeScaleTeleport => flag.IsSet(18);

			public bool IsTangent => flag.IsSet(21);

			public int ParticleCount => particleChunk.dataLength;

			public int ColliderCount => colliderCount;

			public int BaseLineCount => baseLineChunk.dataLength;

			public int TriangleCount => proxyTriangleChunk.dataLength;

			public int EdgeCount => proxyEdgeChunk.dataLength;

			public float InitScale => initScale.x;
		}

		public struct MappingData : IValid
		{
			public int teamId;

			public BitField32 flag;

			public int centerTransformIndex;

			public DataChunk mappingCommonChunk;

			public float4x4 toProxyMatrix;

			public quaternion toProxyRotation;

			public bool sameSpace;

			public float4x4 toMappingMatrix;

			public quaternion toMappingRotation;

			public float scaleRatio;

			public int renderDataWorkIndex;

			public int VertexCount => mappingCommonChunk.dataLength;

			public bool IsValid()
			{
				return teamId > 0;
			}
		}

		[BurstCompile]
		private struct AlwaysTeamUpdatePreJob : IJob
		{
			public NativeArray<TeamData> teamDataArray;

			public NativeArray<ClothParameters> parameterArray;

			public NativeParallelHashMap<int, int> comp2SuspendCounterMap;

			public NativeParallelHashMap<int, int> comp2TeamIdMap;

			public NativeParallelHashMap<int, int> comp2SyncPartnerCompMap;

			public NativeParallelHashMap<int, int> comp2SyncTopCompMap;

			public NativeParallelHashSet<int> selfCollisionUpdateSet;

			public NativeReference<int> edgeColliderCollisionCountBuff;

			public unsafe void Execute()
			{
				int num = 0;
				TeamData* unsafePtr = (TeamData*)teamDataArray.GetUnsafePtr();
				foreach (KeyValue<int, int> item2 in comp2TeamIdMap)
				{
					int item = item2.Value;
					if (item == 0)
					{
						continue;
					}
					int key = item2.Key;
					ref TeamData reference = ref unsafePtr[item];
					reference.flag.SetBits(20, value: false);
					if (!reference.flag.IsSet(1))
					{
						continue;
					}
					bool value = true;
					if (!comp2SuspendCounterMap.ContainsKey(key) || comp2SuspendCounterMap[key] == 0)
					{
						value = false;
					}
					if (comp2SyncPartnerCompMap.ContainsKey(key))
					{
						int key2 = comp2SyncPartnerCompMap[key];
						if (comp2TeamIdMap.ContainsKey(key2))
						{
							int num2 = comp2TeamIdMap[key2];
							if (num2 > 0)
							{
								TeamData* num3 = unsafePtr + num2;
								int num4 = (comp2SuspendCounterMap.ContainsKey(key2) ? comp2SuspendCounterMap[key2] : 0);
								if (num3->IsEnable && num4 == 0)
								{
									value = false;
								}
							}
						}
					}
					reference.flag.SetBits(4, value);
					if (reference.flag.IsSet(4))
					{
						continue;
					}
					int syncTeamId = reference.syncTeamId;
					reference.syncTeamId = 0;
					if (comp2SyncPartnerCompMap.ContainsKey(key))
					{
						int key3 = comp2SyncPartnerCompMap[key];
						if (comp2TeamIdMap.ContainsKey(key3))
						{
							int syncTeamId2 = comp2TeamIdMap[key3];
							reference.syncTeamId = syncTeamId2;
						}
					}
					reference.flag.SetBits(6, reference.syncTeamId != 0);
					reference.syncCenterTransformIndex = 0;
					if (syncTeamId != reference.syncTeamId)
					{
						if (syncTeamId > 0)
						{
							unsafePtr[syncTeamId].syncParentTeamId.MC2RemoveItemAtSwapBack(item);
						}
						if (reference.syncTeamId != 0)
						{
							ref TeamData reference2 = ref unsafePtr[reference.syncTeamId];
							if (reference2.syncParentTeamId.Length != reference2.syncParentTeamId.Capacity)
							{
								reference2.syncParentTeamId.Add(in item);
							}
							reference.flag.SetBits(3, value: false);
						}
						selfCollisionUpdateSet.Add(item);
					}
					ClothParameters value2 = parameterArray[item];
					int num5 = 0;
					if (comp2SyncTopCompMap.ContainsKey(key))
					{
						int key4 = comp2SyncTopCompMap[key];
						if (comp2TeamIdMap.ContainsKey(key4))
						{
							num5 = comp2TeamIdMap[key4];
							ref TeamData reference3 = ref unsafePtr[num5];
							if (reference3.IsValid)
							{
								reference.originalUpdateMode = reference3.originalUpdateMode;
								reference.updateMode = reference3.updateMode;
								reference.time = reference3.time;
								reference.oldTime = reference3.oldTime;
								reference.nowUpdateTime = reference3.nowUpdateTime;
								reference.oldUpdateTime = reference3.oldUpdateTime;
								reference.frameUpdateTime = reference3.frameUpdateTime;
								reference.frameOldTime = reference3.frameOldTime;
								reference.timeScale = reference3.timeScale;
								reference.updateCount = reference3.updateCount;
								reference.frameInterpolation = reference3.frameInterpolation;
								reference.skipCount = reference3.skipCount;
							}
							ClothParameters clothParameters = parameterArray[num5];
							value2.inertiaConstraint.anchorInertia = clothParameters.inertiaConstraint.anchorInertia;
							value2.inertiaConstraint.worldInertia = clothParameters.inertiaConstraint.worldInertia;
							value2.inertiaConstraint.movementInertiaSmoothing = clothParameters.inertiaConstraint.movementInertiaSmoothing;
							value2.inertiaConstraint.movementSpeedLimit = clothParameters.inertiaConstraint.movementSpeedLimit;
							value2.inertiaConstraint.rotationSpeedLimit = clothParameters.inertiaConstraint.rotationSpeedLimit;
							value2.inertiaConstraint.teleportMode = clothParameters.inertiaConstraint.teleportMode;
							value2.inertiaConstraint.teleportDistance = clothParameters.inertiaConstraint.teleportDistance;
							value2.inertiaConstraint.teleportRotation = clothParameters.inertiaConstraint.teleportRotation;
							parameterArray[item] = value2;
							reference.syncCenterTransformIndex = reference3.centerTransformIndex;
						}
					}
					if (value2.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
					{
						num += reference.EdgeCount;
					}
				}
				edgeColliderCollisionCountBuff.Value = num;
			}
		}

		[BurstCompile]
		private struct AlwaysTeamUpdatePostJob : IJob
		{
			public int teamCount;

			public float unityFrameDeltaTime;

			public float unityFrameFixedDeltaTime;

			public float unityFrameUnscaledDeltaTime;

			public float globalTimeScale;

			public float simulationDeltaTime;

			public int maxSimmulationCountPerFrame;

			public int splitProxyMeshVertexCount;

			public NativeReference<int4> teamStatus;

			public NativeArray<TeamData> teamDataArray;

			[ReadOnly]
			public NativeArray<ClothParameters> parameterArray;

			public NativeArray<InertiaConstraint.CenterData> centerDataArray;

			public NativeArray<float3> componentPositionArray;

			public bool hasMainCamera;

			public NativeParallelHashMap<int, int> comp2TeamIdMap;

			public NativeParallelHashMap<int, int> comp2SyncTopCompMap;

			public NativeParallelHashMap<int, int> animatorUpdateModeMap;

			public NativeArray<int> teamAnchorTransformIndexArray;

			public NativeArray<int> teamDistanceTransformIndexArray;

			public NativeParallelHashMap<int, float3> transformPositionMap;

			public NativeParallelHashMap<int, quaternion> transformRotationMap;

			public NativeList<int> cullingDirtyList;

			public NativeList<int> batchNormalClothTeamList;

			public NativeList<int> batchSplitClothTeamList;

			public void Execute()
			{
				int x = 0;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				for (int i = 1; i < teamCount; i++)
				{
					TeamData tdata = teamDataArray[i];
					int componentId = tdata.componentId;
					if (!tdata.IsEnable || tdata.flag.IsSet(4))
					{
						continue;
					}
					ClothParameters param = parameterArray[i];
					int num4 = teamAnchorTransformIndexArray[i];
					bool flag = tdata.flag.IsSet(15);
					bool flag2 = num4 != 0;
					tdata.flag.SetBits(15, flag2);
					tdata.flag.SetBits(16, flag != flag2);
					InertiaConstraint.CenterData value = centerDataArray[i];
					value.anchorPosition = ((num4 != 0) ? transformPositionMap[num4] : float3.zero);
					value.anchorRotation = ((num4 != 0) ? transformRotationMap[num4] : quaternion.identity);
					centerDataArray[i] = value;
					DistanceCullingUpdate(i, ref tdata, ref param);
					if (tdata.IsCullingInvisible)
					{
						teamDataArray[i] = tdata;
						continue;
					}
					int num5 = 0;
					if (comp2SyncTopCompMap.ContainsKey(componentId))
					{
						int key = comp2SyncTopCompMap[componentId];
						if (comp2TeamIdMap.ContainsKey(key))
						{
							num5 = comp2TeamIdMap[key];
						}
					}
					if (tdata.originalUpdateMode == ClothUpdateMode.AnimatorLinkage || num5 > 0)
					{
						ClothUpdateMode originalUpdateMode = tdata.originalUpdateMode;
						int interlockingAnimatorId = tdata.interlockingAnimatorId;
						if (num5 > 0)
						{
							TeamData teamData = teamDataArray[num5];
							originalUpdateMode = teamData.originalUpdateMode;
							interlockingAnimatorId = teamData.interlockingAnimatorId;
						}
						switch (originalUpdateMode)
						{
						case ClothUpdateMode.Normal:
						case ClothUpdateMode.UnityPhysics:
						case ClothUpdateMode.Unscaled:
							tdata.updateMode = originalUpdateMode;
							break;
						case ClothUpdateMode.AnimatorLinkage:
							if (animatorUpdateModeMap.ContainsKey(interlockingAnimatorId))
							{
								switch ((AnimatorUpdateMode)animatorUpdateModeMap[interlockingAnimatorId])
								{
								case AnimatorUpdateMode.Normal:
									tdata.updateMode = ClothUpdateMode.Normal;
									break;
								case AnimatorUpdateMode.Fixed:
									tdata.updateMode = ClothUpdateMode.UnityPhysics;
									break;
								case AnimatorUpdateMode.UnscaledTime:
									tdata.updateMode = ClothUpdateMode.Unscaled;
									break;
								default:
									tdata.updateMode = ClothUpdateMode.Normal;
									break;
								}
							}
							else
							{
								tdata.updateMode = ClothUpdateMode.Normal;
							}
							break;
						default:
							tdata.updateMode = ClothUpdateMode.Normal;
							break;
						}
					}
					if (tdata.flag.IsSet(3))
					{
						tdata.time = 0f;
						tdata.oldTime = 0f;
						tdata.nowUpdateTime = 0f;
						tdata.oldUpdateTime = 0f;
						tdata.frameUpdateTime = 0f;
						tdata.frameOldTime = 0f;
					}
					float num6 = (tdata.frameDeltaTime = (tdata.IsFixedUpdate ? unityFrameFixedDeltaTime : (tdata.IsUnscaled ? unityFrameUnscaledDeltaTime : unityFrameDeltaTime)));
					float num7 = tdata.timeScale * (tdata.IsUnscaled ? 1f : globalTimeScale);
					float num8 = num6 * (tdata.nowTimeScale = (tdata.flag.IsSet(4) ? 0f : num7));
					float num9 = tdata.time + num8;
					int num10 = (int)((num9 - tdata.nowUpdateTime) / simulationDeltaTime);
					tdata.updateCount = math.min(num10, maxSimmulationCountPerFrame);
					tdata.skipCount = num10 - tdata.updateCount;
					if (tdata.skipCount > 0)
					{
						num9 -= simulationDeltaTime * (float)tdata.skipCount;
					}
					if (tdata.updateCount > 0 && num8 == 0f)
					{
						tdata.updateCount = 0;
						tdata.skipCount = 0;
						tdata.nowUpdateTime = num9 - simulationDeltaTime + 0.0001f;
					}
					if (tdata.updateCount > 0)
					{
						tdata.frameOldTime = tdata.frameUpdateTime;
						tdata.frameUpdateTime = num9;
						tdata.oldUpdateTime = tdata.nowUpdateTime;
					}
					tdata.oldTime = tdata.time;
					tdata.time = num9;
					tdata.flag.SetBits(5, tdata.updateCount > 0);
					teamDataArray[i] = tdata;
					x = math.max(x, tdata.updateCount);
					bool flag3 = false;
					if (tdata.flag.IsSet(32) || tdata.flag.IsSet(33) || tdata.flag.IsSet(34))
					{
						batchSplitClothTeamList.Add(in i);
						flag3 = true;
						num3++;
					}
					else if (tdata.ParticleCount >= splitProxyMeshVertexCount)
					{
						batchSplitClothTeamList.Add(in i);
						flag3 = true;
					}
					else
					{
						batchNormalClothTeamList.Add(in i);
					}
					if (flag3 && tdata.ColliderCount > 0)
					{
						if (param.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Point)
						{
							num++;
						}
						if (param.colliderCollisionConstraint.mode == ColliderCollisionConstraint.Mode.Edge)
						{
							num2++;
						}
					}
				}
				teamStatus.Value = new int4(x, num, num2, num3);
			}

			private void DistanceCullingUpdate(int teamId, ref TeamData tdata, ref ClothParameters param)
			{
				bool isDistanceCullingInvisible = tdata.IsDistanceCullingInvisible;
				bool flag;
				if (param.culling.useDistanceCulling)
				{
					int key = teamDistanceTransformIndexArray[teamId];
					if (transformPositionMap.ContainsKey(key))
					{
						float3 x = componentPositionArray[tdata.componentTransformIndex];
						float3 y = transformPositionMap[key];
						float num = math.distance(x, y);
						float num2 = param.culling.distanceCullingLength;
						if (!hasMainCamera && tdata.componentTransformIndex == 0)
						{
							num2 = 1000000f;
						}
						flag = num >= num2;
						float num3 = math.saturate(param.culling.distanceCullingFadeRatio) * num2;
						tdata.distanceWeight = 1f - math.saturate(math.unlerp(num2 - num3, num2, num));
					}
					else
					{
						flag = false;
						tdata.distanceWeight = 1f;
					}
				}
				else
				{
					flag = false;
					tdata.distanceWeight = 1f;
				}
				if (isDistanceCullingInvisible != flag)
				{
					tdata.flag.SetBits(19, flag);
					tdata.flag.SetBits(2, value: true);
					tdata.flag.SetBits(12, value: false);
					cullingDirtyList.Add(in teamId);
				}
			}
		}

		public const int Flag_Valid = 0;

		public const int Flag_Enable = 1;

		public const int Flag_Reset = 2;

		public const int Flag_TimeReset = 3;

		public const int Flag_SyncSuspend = 4;

		public const int Flag_Running = 5;

		public const int Flag_Synchronization = 6;

		public const int Flag_StepRunning = 7;

		public const int Flag_Exit = 8;

		public const int Flag_KeepTeleport = 9;

		public const int Flag_InertiaShift = 10;

		public const int Flag_CameraCullingInvisible = 11;

		public const int Flag_CameraCullingKeep = 12;

		public const int Flag_Spring = 13;

		public const int Flag_SkipWriting = 14;

		public const int Flag_Anchor = 15;

		public const int Flag_AnchorReset = 16;

		public const int Flag_NegativeScale = 17;

		public const int Flag_NegativeScaleTeleport = 18;

		public const int Flag_DistanceCullingInvisible = 19;

		public const int Flag_RestoreTransformOnlyOnec = 20;

		public const int Flag_Tangent = 21;

		public const int Flag_Self_PointPrimitive = 32;

		public const int Flag_Self_EdgePrimitive = 33;

		public const int Flag_Self_TrianglePrimitive = 34;

		public const int Flag_Self_EdgeEdge = 35;

		public const int Flag_Sync_EdgeEdge = 36;

		public const int Flag_PSync_EdgeEdge = 37;

		public const int Flag_Self_PointTriangle = 38;

		public const int Flag_Sync_PointTriangle = 39;

		public const int Flag_PSync_PointTriangle = 40;

		public const int Flag_Self_TrianglePoint = 41;

		public const int Flag_Sync_TrianglePoint = 42;

		public const int Flag_PSync_TrianglePoint = 43;

		public const int Flag_Self_EdgeTriangleIntersect = 44;

		public const int Flag_Sync_EdgeTriangleIntersect = 45;

		public const int Flag_PSync_EdgeTriangleIntersect = 46;

		public const int Flag_Self_TriangleEdgeIntersect = 47;

		public const int Flag_Sync_TriangleEdgeIntersect = 48;

		public const int Flag_PSync_TriangleEdgeIntersect = 49;

		public ExNativeArray<TeamData> teamDataArray;

		public ExNativeArray<TeamWindData> teamWindArray;

		public const int MappingDataFlag_ChangePositionNormal = 0;

		public const int MappingDataFlag_ChangeTangent = 1;

		public const int MappingDataFlag_ChangeBoneWeight = 2;

		public const int MappingDataFlag_ModifyBoneWeight = 3;

		public ExNativeArray<MappingData> mappingDataArray;

		public ExNativeArray<FixedList64Bytes<short>> teamMappingIndexArray;

		public NativeReference<int4> teamStatus;

		public ExNativeArray<ClothParameters> parameterArray;

		public ExNativeArray<InertiaConstraint.CenterData> centerDataArray;

		private HashSet<int> enableTeamSet = new HashSet<int>();

		private Dictionary<int, ClothProcess> clothProcessDict = new Dictionary<int, ClothProcess>();

		private bool isValid;

		internal int edgeColliderCollisionCount;

		internal NativeReference<int> edgeColliderCollisionCountBuff;

		internal NativeParallelHashMap<int, int> comp2SuspendCounterMap;

		internal NativeParallelHashMap<int, int> comp2TeamIdMap;

		internal NativeParallelHashMap<int, int> comp2SyncPartnerCompMap;

		internal NativeParallelHashMap<int, int> comp2SyncTopCompMap;

		internal NativeList<int> batchNormalClothTeamList;

		internal NativeList<int> batchSplitClothTeamList;

		internal List<ClothProcess> parameterDirtyList;

		internal List<ClothProcess> skipWritingDirtyList;

		internal NativeList<int> cullingDirtyList;

		internal NativeParallelHashSet<int> selfCollisionUpdateSet;

		internal NativeParallelHashMap<int, int> animatorUpdateModeMap;

		internal ExSimpleNativeArray<int> teamAnchorTransformIndexArray;

		internal ExSimpleNativeArray<int> teamDistanceTransformIndexArray;

		internal NativeParallelHashMap<int, float3> transformPositionMap;

		internal NativeParallelHashMap<int, quaternion> transformRotationMap;

		internal HashSet<MagicaCloth> cameraCullingClothSet = new HashSet<MagicaCloth>(256);

		private static readonly ProfilerMarker teamCameraCullingPreProfiler = new ProfilerMarker("CameraCullingPre");

		private static readonly ProfilerMarker teamCameraCullingProfiler = new ProfilerMarker("CameraCullingPost");

		private static readonly ProfilerMarker startClothUpdateComponentProfiler = new ProfilerMarker("StartClothUpdate.Component");

		private HashSet<ClothProcess> monitoringProcessSet = new HashSet<ClothProcess>();

		private List<ClothProcess> disposeProcessList = new List<ClothProcess>();

		public int MappingCount => mappingDataArray?.Count ?? 0;

		public int TeamCount => teamDataArray?.Count ?? 0;

		public int TrueTeamCount => clothProcessDict.Count;

		public int ActiveTeamCount => enableTeamSet.Count;

		public int TeamMaxUpdateCount => teamStatus.Value.x;

		public void Dispose()
		{
			MonitoringProcess(force: true);
			isValid = false;
			teamDataArray?.Dispose();
			teamWindArray?.Dispose();
			mappingDataArray?.Dispose();
			teamMappingIndexArray?.Dispose();
			parameterArray?.Dispose();
			centerDataArray?.Dispose();
			teamDataArray = null;
			teamWindArray = null;
			mappingDataArray = null;
			teamMappingIndexArray = null;
			parameterArray = null;
			centerDataArray = null;
			if (teamStatus.IsCreated)
			{
				teamStatus.Dispose();
			}
			enableTeamSet.Clear();
			clothProcessDict.Clear();
			if (edgeColliderCollisionCountBuff.IsCreated)
			{
				edgeColliderCollisionCountBuff.Dispose();
			}
			NativeParallelHashMap.MC2DisposeSafe(ref comp2SuspendCounterMap);
			NativeParallelHashMap.MC2DisposeSafe(ref comp2TeamIdMap);
			NativeParallelHashMap.MC2DisposeSafe(ref comp2SyncPartnerCompMap);
			NativeParallelHashMap.MC2DisposeSafe(ref comp2SyncTopCompMap);
			if (batchNormalClothTeamList.IsCreated)
			{
				batchNormalClothTeamList.Dispose();
			}
			if (batchSplitClothTeamList.IsCreated)
			{
				batchSplitClothTeamList.Dispose();
			}
			parameterDirtyList?.Clear();
			skipWritingDirtyList?.Clear();
			if (cullingDirtyList.IsCreated)
			{
				cullingDirtyList.Dispose();
			}
			if (selfCollisionUpdateSet.IsCreated)
			{
				selfCollisionUpdateSet.Dispose();
			}
			NativeParallelHashMap.MC2DisposeSafe(ref animatorUpdateModeMap);
			teamAnchorTransformIndexArray?.Dispose();
			teamDistanceTransformIndexArray?.Dispose();
			NativeParallelHashMap.MC2DisposeSafe(ref transformPositionMap);
			NativeParallelHashMap.MC2DisposeSafe(ref transformRotationMap);
			cameraCullingClothSet.Clear();
			MagicaManager.afterUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterUpdateDelegate, new MagicaManager.UpdateMethod(MonitoringProcessUpdate));
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			teamDataArray = new ExNativeArray<TeamData>(32);
			teamWindArray = new ExNativeArray<TeamWindData>(32);
			mappingDataArray = new ExNativeArray<MappingData>(32);
			teamMappingIndexArray = new ExNativeArray<FixedList64Bytes<short>>(32);
			parameterArray = new ExNativeArray<ClothParameters>(32);
			centerDataArray = new ExNativeArray<InertiaConstraint.CenterData>(32);
			teamDataArray.Add(default(TeamData));
			teamWindArray.Add(default(TeamWindData));
			teamMappingIndexArray.Add(default(FixedList64Bytes<short>));
			parameterArray.Add(default(ClothParameters));
			centerDataArray.Add(default(InertiaConstraint.CenterData));
			teamStatus = new NativeReference<int4>(Allocator.Persistent);
			edgeColliderCollisionCountBuff = new NativeReference<int>(Allocator.Persistent);
			comp2SuspendCounterMap = new NativeParallelHashMap<int, int>(256, Allocator.Persistent);
			comp2TeamIdMap = new NativeParallelHashMap<int, int>(256, Allocator.Persistent);
			comp2SyncPartnerCompMap = new NativeParallelHashMap<int, int>(256, Allocator.Persistent);
			comp2SyncTopCompMap = new NativeParallelHashMap<int, int>(256, Allocator.Persistent);
			batchNormalClothTeamList = new NativeList<int>(Allocator.Persistent);
			batchSplitClothTeamList = new NativeList<int>(Allocator.Persistent);
			parameterDirtyList = new List<ClothProcess>(128);
			skipWritingDirtyList = new List<ClothProcess>(128);
			cullingDirtyList = new NativeList<int>(128, Allocator.Persistent);
			selfCollisionUpdateSet = new NativeParallelHashSet<int>(256, Allocator.Persistent);
			animatorUpdateModeMap = new NativeParallelHashMap<int, int>(128, Allocator.Persistent);
			teamAnchorTransformIndexArray = new ExSimpleNativeArray<int>(256, areaOnly: true);
			teamDistanceTransformIndexArray = new ExSimpleNativeArray<int>(256, areaOnly: true);
			transformPositionMap = new NativeParallelHashMap<int, float3>(32, Allocator.Persistent);
			transformRotationMap = new NativeParallelHashMap<int, quaternion>(32, Allocator.Persistent);
			MagicaManager.afterUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterUpdateDelegate, new MagicaManager.UpdateMethod(MonitoringProcessUpdate));
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		internal int AddTeam(ClothProcess cprocess, ClothParameters clothParams)
		{
			if (!isValid)
			{
				return 0;
			}
			TeamData data = default(TeamData);
			data.componentId = cprocess.cloth.GetInstanceID();
			data.flag.SetBits(0, value: true);
			data.flag.SetBits(2, value: true);
			data.flag.SetBits(3, value: true);
			data.originalUpdateMode = cprocess.cloth.SerializeData.updateMode;
			data.updateMode = cprocess.cloth.SerializeData.updateMode;
			data.timeScale = 1f;
			data.initScale = cprocess.clothTransformRecord.scale;
			data.scaleRatio = 1f;
			data.negativeScaleSign = 1f;
			data.negativeScaleDirection = 1;
			data.negativeScaleChange = 1;
			data.negativeScaleQuaternionValue = 1;
			data.negativeScaleTriangleSign = 1;
			data.animationPoseRatio = cprocess.cloth.SerializeData.animationPoseRatio;
			data.distanceWeight = 1f;
			data.componentTransformIndex = MagicaManager.Bone.AddComponentTransform(cprocess.cloth.transform);
			DataChunk chunk = teamDataArray.Add(data);
			int startIndex = chunk.startIndex;
			if (startIndex >= 4096)
			{
				Develop.LogError((object)$"Cannot create more than {4096} teams.");
				teamDataArray.Remove(chunk);
				return 0;
			}
			TeamWindData data2 = new TeamWindData
			{
				movingWind = 
				{
					time = -10000f
				}
			};
			teamWindArray.Add(data2);
			teamMappingIndexArray.Add(default(FixedList64Bytes<short>));
			parameterArray.Add(clothParams);
			InertiaConstraint.CenterData data3 = new InertiaConstraint.CenterData
			{
				frameLocalPosition = cprocess.ProxyMeshContainer.shareVirtualMesh.localCenterPosition.Value
			};
			centerDataArray.Add(data3);
			clothProcessDict.Add(startIndex, cprocess);
			return startIndex;
		}

		internal void RemoveTeam(int teamId)
		{
			if (isValid && teamId != 0)
			{
				ref TeamData teamDataRef = ref GetTeamDataRef(teamId);
				if (teamDataRef.syncTeamId > 0 && ContainsTeamData(teamDataRef.syncTeamId))
				{
					RemoveSyncParent(ref GetTeamDataRef(teamDataRef.syncTeamId), teamId);
				}
				MagicaManager.Bone.RemoveComponentTransform(teamDataRef.componentTransformIndex);
				DataChunk chunk = new DataChunk(teamId);
				teamDataArray.RemoveAndFill(chunk);
				teamWindArray.RemoveAndFill(chunk);
				teamMappingIndexArray.RemoveAndFill(chunk);
				parameterArray.Remove(chunk);
				centerDataArray.Remove(chunk);
				teamAnchorTransformIndexArray[teamId] = 0;
				teamDistanceTransformIndexArray[teamId] = 0;
				clothProcessDict.Remove(teamId);
			}
		}

		public void SetEnable(int teamId, bool sw)
		{
			if (isValid && teamId != 0)
			{
				ref TeamData reference = ref teamDataArray.GetRef(teamId);
				reference.flag.SetBits(1, sw);
				reference.flag.SetBits(2, sw);
				if (sw)
				{
					enableTeamSet.Add(teamId);
				}
				else
				{
					enableTeamSet.Remove(teamId);
				}
				if (!sw)
				{
					reference.flag.SetBits(20, value: true);
				}
				MagicaManager.Collider.EnableTeamCollider(teamId);
				MagicaManager.Bone.EnableTransform(reference.centerTransformIndex, sw);
				MagicaManager.Bone.EnableTransform(reference.proxyTransformChunk, sw);
			}
		}

		public bool IsEnable(int teamId)
		{
			return enableTeamSet.Contains(teamId);
		}

		internal void SetSkipWriting(int teamId, bool sw)
		{
			if (isValid && teamId != 0)
			{
				teamDataArray.GetRef(teamId).flag.SetBits(14, sw);
			}
		}

		public bool ContainsTeamData(int teamId)
		{
			if (teamId >= 0)
			{
				return clothProcessDict.ContainsKey(teamId);
			}
			return false;
		}

		public ref TeamData GetTeamDataRef(int teamId)
		{
			return ref teamDataArray.GetRef(teamId);
		}

		public ref FixedList64Bytes<short> GetTeamMappingRef(int teamId)
		{
			return ref teamMappingIndexArray.GetRef(teamId);
		}

		public ref ClothParameters GetParametersRef(int teamId)
		{
			return ref parameterArray.GetRef(teamId);
		}

		internal ref InertiaConstraint.CenterData GetCenterDataRef(int teamId)
		{
			return ref centerDataArray.GetRef(teamId);
		}

		internal ref MappingData GetMappingDataRef(int mindex)
		{
			return ref mappingDataArray.GetRef(mindex);
		}

		public ClothProcess GetClothProcess(int teamId)
		{
			if (clothProcessDict.ContainsKey(teamId))
			{
				return clothProcessDict[teamId];
			}
			return null;
		}

		internal void CameraCullingPreProcess()
		{
			ClothManager cloth = MagicaManager.Cloth;
			cameraCullingClothSet.Clear();
			foreach (ClothProcess item in cloth.clothSet)
			{
				if (!item.IsEnable || !item.IsRunning())
				{
					continue;
				}
				MagicaCloth magicaCloth = ((item.SyncTopCloth != null) ? item.SyncTopCloth : item.cloth);
				CullingSettings cullingSettings = magicaCloth.SerializeData.cullingSettings;
				ClothProcess process = magicaCloth.Process;
				CullingSettings.CameraCullingMode cameraCullingMode = cullingSettings.cameraCullingMode;
				if (cameraCullingMode == CullingSettings.CameraCullingMode.AnimatorLinkage)
				{
					if ((bool)process.interlockingAnimator)
					{
						switch (process.interlockingAnimator.cullingMode)
						{
						case AnimatorCullingMode.AlwaysAnimate:
							cameraCullingMode = CullingSettings.CameraCullingMode.Off;
							break;
						case AnimatorCullingMode.CullCompletely:
							cameraCullingMode = CullingSettings.CameraCullingMode.Keep;
							break;
						case AnimatorCullingMode.CullUpdateTransforms:
							cameraCullingMode = CullingSettings.CameraCullingMode.Reset;
							break;
						}
					}
					else
					{
						cameraCullingMode = CullingSettings.CameraCullingMode.Off;
					}
				}
				if (magicaCloth == null || cameraCullingMode == CullingSettings.CameraCullingMode.Off)
				{
					item.cameraCullingAnimator = null;
					item.cameraCullingRenderers = null;
				}
				else
				{
					Animator cameraCullingAnimator = null;
					List<Renderer> cameraCullingRenderers = cullingSettings.cameraCullingRenderers;
					if (cullingSettings.cameraCullingMethod == CullingSettings.CameraCullingMethod.AutomaticRenderer)
					{
						cameraCullingAnimator = magicaCloth.Process.interlockingAnimator;
						cameraCullingRenderers = magicaCloth.Process.interlockingAnimatorRenderers;
					}
					item.cameraCullingAnimator = cameraCullingAnimator;
					item.cameraCullingRenderers = cameraCullingRenderers;
				}
				item.cameraCullingMode = cameraCullingMode;
				cameraCullingClothSet.Add(item.cloth);
			}
		}

		internal void CameraCullingPostProcess()
		{
			ClothManager cloth = MagicaManager.Cloth;
			cloth.ClearVisibleDict();
			foreach (MagicaCloth item in cameraCullingClothSet)
			{
				if (item == null)
				{
					continue;
				}
				ClothProcess process = item.Process;
				bool cameraCullingOldInvisible = process.cameraCullingOldInvisible;
				bool flag = (!(process.cameraCullingAnimator == null) || process.cameraCullingRenderers != null) && !cloth.CheckVisible(process.cameraCullingAnimator, process.cameraCullingRenderers);
				if (cameraCullingOldInvisible == flag)
				{
					continue;
				}
				int teamId = process.TeamId;
				ref TeamData teamDataRef = ref GetTeamDataRef(teamId);
				teamDataRef.flag.SetBits(11, flag);
				teamDataRef.flag.SetBits(12, value: false);
				process.SetState(8, flag);
				process.SetState(9, sw: false);
				process.cameraCullingOldInvisible = flag;
				if (flag)
				{
					switch (process.cameraCullingMode)
					{
					case CullingSettings.CameraCullingMode.Off:
					case CullingSettings.CameraCullingMode.Reset:
						teamDataRef.flag.SetBits(2, value: true);
						break;
					case CullingSettings.CameraCullingMode.Keep:
						teamDataRef.flag.SetBits(12, value: true);
						process.SetState(9, sw: true);
						break;
					}
				}
				process.UpdateRendererUse();
			}
			cameraCullingClothSet.Clear();
		}

		internal void AlwaysTeamUpdate()
		{
			ClothManager cloth = MagicaManager.Cloth;
			TimeManager time = MagicaManager.Time;
			RenderManager render = MagicaManager.Render;
			TransformManager bone = MagicaManager.Bone;
			SimulationManager simulation = MagicaManager.Simulation;
			edgeColliderCollisionCount = 0;
			edgeColliderCollisionCountBuff.Value = 0;
			cloth.ClearVisibleDict();
			selfCollisionUpdateSet.Clear();
			teamAnchorTransformIndexArray.SetLength(TeamCount);
			teamDistanceTransformIndexArray.SetLength(TeamCount);
			transformPositionMap.Clear();
			transformRotationMap.Clear();
			cullingDirtyList.Clear();
			batchNormalClothTeamList.Clear();
			batchSplitClothTeamList.Clear();
			int num = 0;
			while (num < parameterDirtyList.Count)
			{
				ClothProcess clothProcess = parameterDirtyList[num];
				if (clothProcess == null)
				{
					parameterDirtyList.RemoveAt(num);
					continue;
				}
				if (!clothProcess.IsEnable)
				{
					num++;
					continue;
				}
				MagicaManager.Collider.UpdateColliders(clothProcess);
				clothProcess.UpdateCullingAnimatorAndRenderers();
				int teamId = clothProcess.TeamId;
				ref TeamData teamDataRef = ref GetTeamDataRef(teamId);
				MagicaCloth cloth2 = clothProcess.cloth;
				teamDataRef.interlockingAnimatorId = ((clothProcess.interlockingAnimator != null) ? clothProcess.interlockingAnimator.GetInstanceID() : 0);
				clothProcess.SyncParameters();
				parameterArray[teamId] = clothProcess.parameters;
				teamDataRef.originalUpdateMode = cloth2.SerializeData.updateMode;
				teamDataRef.updateMode = cloth2.SerializeData.updateMode;
				teamDataRef.animationPoseRatio = cloth2.SerializeData.animationPoseRatio;
				teamDataRef.flag.SetBits(13, clothProcess.clothType == ClothProcess.ClothType.BoneSpring && clothProcess.parameters.springConstraint.springPower > 0f);
				selfCollisionUpdateSet.Add(teamId);
				teamDataRef.flag.SetBits(21, cloth2.SerializeData.meshWriteMode == ClothMeshWriteMode.PositionAndNormalTangent);
				clothProcess.SetState(14, teamDataRef.flag.IsSet(21));
				parameterDirtyList.RemoveAt(num);
			}
			int num2 = 0;
			while (num2 < skipWritingDirtyList.Count)
			{
				ClothProcess clothProcess2 = skipWritingDirtyList[num2];
				if (clothProcess2 == null)
				{
					skipWritingDirtyList.RemoveAt(num2);
					continue;
				}
				if (!clothProcess2.IsEnable)
				{
					num2++;
					continue;
				}
				bool value = clothProcess2.IsState(10);
				int teamId2 = clothProcess2.TeamId;
				GetTeamDataRef(teamId2).flag.SetBits(14, value);
				foreach (ClothProcess.RenderMeshInfo renderMeshInfo in clothProcess2.renderMeshInfoList)
				{
					render.GetRendererData(renderMeshInfo.renderHandle).UpdateSkipWriting();
				}
				skipWritingDirtyList.RemoveAt(num2);
			}
			JobHandle jobHandle = new AlwaysTeamUpdatePreJob
			{
				teamDataArray = teamDataArray.GetNativeArray(),
				parameterArray = parameterArray.GetNativeArray(),
				comp2SuspendCounterMap = comp2SuspendCounterMap,
				comp2TeamIdMap = comp2TeamIdMap,
				comp2SyncPartnerCompMap = comp2SyncPartnerCompMap,
				comp2SyncTopCompMap = comp2SyncTopCompMap,
				selfCollisionUpdateSet = selfCollisionUpdateSet,
				edgeColliderCollisionCountBuff = edgeColliderCollisionCountBuff
			}.Schedule();
			JobHandle jobHandle2 = bone.ReadComponentTransform(default(JobHandle));
			JobHandle.ScheduleBatchedJobs();
			animatorUpdateModeMap.Clear();
			foreach (ClothProcess item2 in cloth.clothSet)
			{
				if (item2.TeamId == 0)
				{
					continue;
				}
				if ((bool)item2.interlockingAnimator)
				{
					int instanceID = item2.interlockingAnimator.GetInstanceID();
					if (!animatorUpdateModeMap.ContainsKey(instanceID))
					{
						animatorUpdateModeMap.Add(instanceID, (int)item2.interlockingAnimator.updateMode);
					}
				}
				ClothSerializeData serializeData = ((item2.SyncTopCloth != null) ? item2.SyncTopCloth : item2.cloth).SerializeData;
				Transform anchor = serializeData.inertiaConstraint.anchor;
				int num3 = ((anchor != null) ? anchor.GetInstanceID() : 0);
				if (num3 != 0 && !transformPositionMap.ContainsKey(num3))
				{
					transformPositionMap.Add(num3, anchor.position);
					transformRotationMap.Add(num3, anchor.rotation);
				}
				if (item2.anchorTransformId != num3)
				{
					item2.anchorTransformId = num3;
					teamAnchorTransformIndexArray[item2.TeamId] = num3;
				}
				int num4 = ((serializeData.cullingSettings.distanceCullingReferenceObject != null) ? serializeData.cullingSettings.distanceCullingReferenceObject.GetInstanceID() : 0);
				if (num4 != 0 && !transformPositionMap.ContainsKey(num4))
				{
					transformPositionMap.Add(num4, serializeData.cullingSettings.distanceCullingReferenceObject.transform.position);
				}
				if (item2.distanceReferenceObjectId != num4)
				{
					item2.distanceReferenceObjectId = num4;
					teamDistanceTransformIndexArray[item2.TeamId] = num4;
				}
			}
			bool hasMainCamera = Camera.main != null;
			float3 item = (Camera.main ? ((float3)Camera.main.transform.position) : ((float3)0));
			transformPositionMap.Add(0, item);
			jobHandle.Complete();
			jobHandle2.Complete();
			edgeColliderCollisionCount = edgeColliderCollisionCountBuff.Value;
			if (selfCollisionUpdateSet.Count() > 0)
			{
				foreach (int item3 in selfCollisionUpdateSet)
				{
					simulation.selfCollisionConstraint.UpdateTeam(item3);
				}
				selfCollisionUpdateSet.Clear();
			}
			if (ActiveTeamCount <= 0)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			float unityFrameFixedDeltaTime = (float)time.FixedUpdateCount * Time.fixedDeltaTime;
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			new AlwaysTeamUpdatePostJob
			{
				teamCount = TeamCount,
				unityFrameDeltaTime = deltaTime,
				unityFrameFixedDeltaTime = unityFrameFixedDeltaTime,
				unityFrameUnscaledDeltaTime = unscaledDeltaTime,
				globalTimeScale = time.GlobalTimeScale,
				simulationDeltaTime = time.SimulationDeltaTime,
				maxSimmulationCountPerFrame = time.maxSimulationCountPerFrame,
				splitProxyMeshVertexCount = simulation.splitProxyMeshVertexCount,
				teamStatus = teamStatus,
				teamDataArray = teamDataArray.GetNativeArray(),
				parameterArray = parameterArray.GetNativeArray(),
				centerDataArray = centerDataArray.GetNativeArray(),
				componentPositionArray = bone.componentPositionArray.GetNativeArray(),
				hasMainCamera = hasMainCamera,
				comp2TeamIdMap = comp2TeamIdMap,
				comp2SyncTopCompMap = comp2SyncTopCompMap,
				animatorUpdateModeMap = animatorUpdateModeMap,
				teamAnchorTransformIndexArray = teamAnchorTransformIndexArray.GetNativeArray(),
				teamDistanceTransformIndexArray = teamDistanceTransformIndexArray.GetNativeArray(),
				transformPositionMap = transformPositionMap,
				transformRotationMap = transformRotationMap,
				cullingDirtyList = cullingDirtyList,
				batchNormalClothTeamList = batchNormalClothTeamList,
				batchSplitClothTeamList = batchSplitClothTeamList
			}.Run();
			if (cullingDirtyList.Length <= 0)
			{
				return;
			}
			foreach (int cullingDirty in cullingDirtyList)
			{
				ref TeamData teamDataRef2 = ref GetTeamDataRef(cullingDirty);
				ClothProcess clothProcess3 = GetClothProcess(cullingDirty);
				bool isDistanceCullingInvisible = teamDataRef2.IsDistanceCullingInvisible;
				clothProcess3.SetState(13, isDistanceCullingInvisible);
				clothProcess3.SetState(9, sw: false);
				clothProcess3.UpdateRendererUse();
			}
		}

		private void RemoveSyncParent(ref TeamData tdata, int parentTeamId)
		{
			tdata.syncParentTeamId.MC2RemoveItemAtSwapBack(parentTeamId);
		}

		internal void AddMonitoringProcess(ClothProcess cprocess)
		{
			monitoringProcessSet.Add(cprocess);
		}

		internal void RemoveMonitoringProcess(ClothProcess cprocess)
		{
			if (monitoringProcessSet.Contains(cprocess))
			{
				monitoringProcessSet.Remove(cprocess);
			}
		}

		private void MonitoringProcess(bool force)
		{
			disposeProcessList.Clear();
			foreach (ClothProcess item in monitoringProcessSet)
			{
				if (item.cloth == null || force)
				{
					disposeProcessList.Add(item);
				}
			}
			if (disposeProcessList.Count > 0)
			{
				disposeProcessList.ForEach(delegate(ClothProcess cprocess)
				{
					cprocess.Dispose();
				});
				disposeProcessList.Clear();
			}
			if (force)
			{
				monitoringProcessSet.Clear();
			}
		}

		private void MonitoringProcessUpdate()
		{
			MonitoringProcess(force: false);
		}

		internal static void SimulationCalcCenterAndInertiaAndWind(float simulationDeltaTime, int teamId, ref TeamData tdata, ref InertiaConstraint.CenterData cdata, ref TeamWindData windData, ref ClothParameters param, in NativeArray<float3> positions, in NativeArray<quaternion> rotations, in NativeArray<quaternion> vertexBindPoseRotations, in NativeArray<ushort> fixedArray, in NativeArray<float3> transformPositionArray, in NativeArray<quaternion> transformRotationArray, in NativeArray<float3> transformScaleArray, int windZoneCount, in NativeArray<WindManager.WindData> windDataArray)
		{
			int index = ((tdata.syncTeamId != 0 && tdata.flag.IsSet(6)) ? tdata.syncCenterTransformIndex : cdata.centerTransformIndex);
			float3 pos = transformPositionArray[index];
			quaternion b = transformRotationArray[index];
			float3 wscl = transformScaleArray[cdata.centerTransformIndex];
			cdata.componentWorldPosition = pos;
			cdata.componentWorldRotation = b;
			cdata.componentWorldScale = wscl;
			float num = math.length(wscl) / math.length(tdata.initScale);
			float3 negativeScaleDirection = tdata.negativeScaleDirection;
			tdata.negativeScaleDirection = math.sign(wscl);
			tdata.negativeScaleChange = negativeScaleDirection * tdata.negativeScaleDirection;
			if (wscl.x < 0f || wscl.y < 0f || wscl.z < 0f)
			{
				tdata.negativeScaleSign = -1f;
				tdata.negativeScaleQuaternionValue = new float4(-math.sign(wscl), 1f);
				tdata.negativeScaleTriangleSign.x = ((!(wscl.x < 0f) && !(wscl.z < 0f)) ? 1 : (-1));
				tdata.negativeScaleTriangleSign.y = ((!(wscl.x < 0f)) ? 1 : (-1));
				tdata.flag.SetBits(17, value: true);
			}
			else
			{
				tdata.negativeScaleSign = 1f;
				tdata.negativeScaleQuaternionValue = 1;
				tdata.negativeScaleTriangleSign = 1;
				tdata.flag.SetBits(17, value: false);
			}
			if (!negativeScaleDirection.Equals(tdata.negativeScaleDirection))
			{
				tdata.flag.SetBits(18, value: true);
				float4x4 a = float4x4.TRS(pos, b, wscl);
				float4x4 m = float4x4.TRS(cdata.oldComponentWorldPosition, cdata.oldComponentWorldRotation, cdata.oldComponentWorldScale);
				float4x4 m2 = math.mul(a, math.inverse(m));
				cdata.oldComponentWorldPosition = MathUtility.TransformPoint(in cdata.oldComponentWorldPosition, in m2);
				cdata.oldComponentWorldScale = wscl;
				cdata.oldAnchorPosition = MathUtility.TransformPoint(in cdata.oldAnchorPosition, in m2);
				cdata.smoothingVelocity = MathUtility.TransformVector(in cdata.smoothingVelocity, in m2);
			}
			float3 float5 = cdata.oldComponentWorldPosition;
			quaternion a2 = cdata.oldComponentWorldRotation;
			float3 wpos = pos;
			quaternion wrot = b;
			if (tdata.fixedDataChunk.IsValid)
			{
				float3 float6 = 0;
				float3 x = 0;
				float3 x2 = 0;
				int startIndex = tdata.proxyCommonChunk.startIndex;
				int dataLength = tdata.fixedDataChunk.dataLength;
				int startIndex2 = tdata.fixedDataChunk.startIndex;
				for (int i = 0; i < dataLength; i++)
				{
					int index2 = fixedArray[startIndex2 + i] + startIndex;
					float6 += positions[index2];
					quaternion rot = rotations[index2];
					if (tdata.negativeScaleSign < 0f)
					{
						MathUtility.ToNormalTangent(in rot, out var nor, out var tan);
						rot = MathUtility.ToRotation(-nor, -tan);
					}
					rot = math.mul(rot, vertexBindPoseRotations[index2]);
					x += MathUtility.ToNormal(in rot);
					x2 += MathUtility.ToTangent(in rot);
				}
				x *= (float)((!(tdata.negativeScaleDirection.x < 0f) && !(tdata.negativeScaleDirection.z < 0f)) ? 1 : (-1));
				x2 *= (float)((!(tdata.negativeScaleDirection.x < 0f) && !(tdata.negativeScaleDirection.y < 0f)) ? 1 : (-1));
				wpos = float6 / dataLength;
				wrot = MathUtility.ToRotation(math.normalize(x), math.normalize(x2));
			}
			float4x4 worldToLocalMatrix = MathUtility.WorldToLocalMatrix(in wpos, in wrot, in wscl);
			if (tdata.IsNegativeScaleTeleport)
			{
				float4x4 a3 = float4x4.TRS(wpos, wrot, wscl);
				float4x4 m3 = float4x4.TRS(cdata.oldFrameWorldPosition, cdata.oldFrameWorldRotation, cdata.oldFrameWorldScale);
				float4x4 negativeScaleMatrix = math.mul(a3, math.inverse(m3));
				cdata.negativeScaleMatrix = negativeScaleMatrix;
			}
			float3 float7 = 0;
			quaternion a4 = quaternion.identity;
			if (tdata.flag.IsSet(16) || tdata.IsReset)
			{
				cdata.oldAnchorPosition = cdata.anchorPosition;
				cdata.oldAnchorRotation = cdata.anchorRotation;
				cdata.anchorComponentLocalPosition = MathUtility.InverseTransformPoint(in pos, in cdata.anchorPosition, in cdata.anchorRotation, (float3)1);
			}
			if (tdata.flag.IsSet(15))
			{
				float7 = MathUtility.TransformPoint(in cdata.anchorComponentLocalPosition, in cdata.anchorPosition, in cdata.anchorRotation, (float3)1) - float5;
				a4 = MathUtility.FromToRotation(in cdata.oldAnchorRotation, in cdata.anchorRotation);
				float t = 1f - param.inertiaConstraint.anchorInertia;
				float7 = math.lerp(float3.zero, float7, t);
				a4 = math.slerp(quaternion.identity, a4, t);
				float5 += float7;
				a2 = math.mul(a4, a2);
				tdata.flag.SetBits(10, value: true);
			}
			float3 float8 = pos - float5;
			float x3 = MathUtility.Angle(in a2, in b);
			if (param.inertiaConstraint.teleportMode != InertiaConstraint.TeleportMode.None && !tdata.IsReset)
			{
				bool flag = false;
				flag = math.length(float8) >= param.inertiaConstraint.teleportDistance * num || flag;
				if (math.degrees(x3) >= param.inertiaConstraint.teleportRotation || flag)
				{
					switch (param.inertiaConstraint.teleportMode)
					{
					case InertiaConstraint.TeleportMode.Reset:
						tdata.flag.SetBits(2, value: true);
						break;
					case InertiaConstraint.TeleportMode.Keep:
						tdata.flag.SetBits(9, value: true);
						break;
					}
				}
			}
			float3 float9 = 0;
			if (param.inertiaConstraint.movementInertiaSmoothing >= 1E-06f)
			{
				if (tdata.IsRunning)
				{
					float3 float10 = ((tdata.frameDeltaTime > 0f) ? (float8 / tdata.frameDeltaTime) : ((float3)0));
					float num2 = param.inertiaConstraint.movementSpeedLimit * num;
					if (num2 >= 0f)
					{
						float10 = MathUtility.ClampVector(float10, num2);
					}
					float t2 = math.saturate(math.pow(1f - param.inertiaConstraint.movementInertiaSmoothing, 3f) * 0.99f + 0.01f);
					cdata.smoothingVelocity = math.lerp(cdata.smoothingVelocity, float10, t2);
				}
				float3 obj = pos - cdata.smoothingVelocity * tdata.frameDeltaTime;
				float9 = obj - float5;
				float5 = obj;
				tdata.flag.SetBits(10, value: true);
			}
			cdata.frameWorldPosition = wpos;
			cdata.frameWorldRotation = wrot;
			cdata.frameWorldScale = wscl;
			if (tdata.IsReset)
			{
				cdata.oldComponentWorldPosition = pos;
				cdata.oldComponentWorldRotation = b;
				cdata.oldComponentWorldScale = wscl;
				float5 = pos;
				a2 = b;
				cdata.oldFrameWorldPosition = wpos;
				cdata.oldFrameWorldRotation = wrot;
				cdata.oldFrameWorldScale = wscl;
				cdata.nowWorldPosition = wpos;
				cdata.nowWorldRotation = wrot;
				cdata.oldWorldPosition = wpos;
				cdata.oldWorldRotation = wrot;
			}
			else if (tdata.IsNegativeScaleTeleport)
			{
				cdata.oldFrameWorldPosition = wpos;
				cdata.oldFrameWorldRotation = wrot;
				cdata.oldFrameWorldScale = wscl;
				cdata.nowWorldPosition = wpos;
				cdata.nowWorldRotation = wrot;
				cdata.oldWorldPosition = wpos;
				cdata.oldWorldRotation = wrot;
			}
			float3 float11 = float5;
			quaternion a5 = a2;
			if (tdata.IsReset)
			{
				cdata.frameComponentShiftVector = 0;
				cdata.frameComponentShiftRotation = quaternion.identity;
				cdata.smoothingVelocity = 0;
				float9 = 0;
				goto IL_0b51;
			}
			cdata.frameComponentShiftVector = pos - float5;
			cdata.frameComponentShiftRotation = MathUtility.FromToRotation(in a2, in b);
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 1f - param.inertiaConstraint.worldInertia;
			float num6 = 1f - param.inertiaConstraint.worldInertia;
			int num7;
			float num8;
			if (!tdata.IsKeepReset)
			{
				num7 = (tdata.IsCullingInvisible ? 1 : 0);
				if (num7 == 0)
				{
					num8 = num5;
					goto IL_0819;
				}
			}
			else
			{
				num7 = 1;
			}
			num8 = 1f;
			goto IL_0819;
			IL_0b51:
			float3 float12 = pos - float11;
			float num9 = math.length(float12);
			cdata.frameMovingSpeed = ((tdata.frameDeltaTime > 0f) ? (num9 / tdata.frameDeltaTime) : 0f);
			cdata.frameMovingSpeed *= ((tdata.nowTimeScale > 1E-06f) ? (1f / tdata.nowTimeScale) : 0f);
			cdata.frameMovingDirection = ((num9 > 1E-06f) ? (float12 / num9) : ((float3)0));
			float3 frameLocalPosition = MathUtility.InverseTransformPoint(in wpos, in worldToLocalMatrix);
			cdata.frameLocalPosition = frameLocalPosition;
			if (tdata.flag.IsSet(2) || tdata.flag.IsSet(3))
			{
				tdata.velocityWeight = ((param.stablizationTimeAfterReset > 1E-06f) ? 0f : 1f);
				tdata.blendWeight = tdata.velocityWeight;
			}
			TeamWindData wdata = default(TeamWindData);
			if (windZoneCount > 0 && param.wind.IsValid())
			{
				float num10 = float.MaxValue;
				int num11 = 0;
				int windId = -1;
				for (int j = 0; j < windZoneCount; j++)
				{
					WindManager.WindData windData2 = windDataArray[j];
					if (!windData2.IsValid() || !windData2.IsEnable())
					{
						continue;
					}
					bool flag2 = windData2.IsAddition();
					if (flag2 && num11 >= 3)
					{
						continue;
					}
					float3 x4 = math.transform(windData2.worldToLocalMatrix, wpos);
					float num12 = math.length(x4);
					switch (windData2.mode)
					{
					case MagicaWindZone.Mode.BoxDirection:
					{
						float3 float13 = math.abs(x4) * 2f;
						if (float13.x > windData2.size.x || float13.y > windData2.size.y || float13.z > windData2.size.z)
						{
							continue;
						}
						break;
					}
					case MagicaWindZone.Mode.SphereDirection:
					case MagicaWindZone.Mode.SphereRadial:
						if (num12 > windData2.size.x)
						{
							continue;
						}
						break;
					}
					if (!flag2 && windData2.zoneVolume > num10)
					{
						continue;
					}
					float3 direction = windData2.worldWindDirection;
					if (windData2.mode == MagicaWindZone.Mode.SphereRadial)
					{
						if (num12 <= 1E-06f)
						{
							continue;
						}
						direction = math.normalize(wpos - windData2.worldPositin);
					}
					float num13 = windData2.main;
					if (windData2.mode == MagicaWindZone.Mode.SphereRadial)
					{
						if (num12 <= 1E-06f)
						{
							continue;
						}
						float time = math.saturate(num12 / windData2.size.x);
						float num14 = windData2.attenuation.MC2EvaluateCurveClamp01(time);
						num13 *= num14;
					}
					TeamWindInfo windInfo = new TeamWindInfo
					{
						windId = j,
						time = -10000f,
						main = num13,
						direction = direction
					};
					if (flag2)
					{
						wdata.AddOrReplaceWindZone(windInfo, in windData);
						num11++;
						continue;
					}
					wdata.RemoveWindZone(windId);
					wdata.AddOrReplaceWindZone(windInfo, in windData);
					num10 = windData2.zoneVolume;
					windId = j;
				}
			}
			wdata.movingWind = windData.movingWind;
			windData.CopyFrom(in wdata);
			return;
			IL_0819:
			num5 = num8;
			num6 = ((num7 != 0) ? 1f : num6);
			if (num5 > 1E-08f || num6 > 1E-08f)
			{
				tdata.flag.SetBits(10, value: true);
				num3 = num5;
				num4 = num6;
				float11 = math.lerp(float11, pos, num5);
				a5 = math.slerp(a5, b, num6);
			}
			float num15 = param.inertiaConstraint.movementSpeedLimit * num;
			float rotationSpeedLimit = param.inertiaConstraint.rotationSpeedLimit;
			float3 x5 = pos - float11;
			float x6 = MathUtility.Angle(in a5, in b);
			float num16 = ((tdata.frameDeltaTime > 0f) ? (math.length(x5) / tdata.frameDeltaTime) : 0f);
			float num17 = ((tdata.frameDeltaTime > 0f) ? (math.degrees(x6) / tdata.frameDeltaTime) : 0f);
			if (num16 > num15 && num15 >= 0f)
			{
				tdata.flag.SetBits(10, value: true);
				float t3 = math.saturate(math.max(num16 - num15, 0f) / num16);
				num3 = math.lerp(num3, 1f, t3);
				float11 = math.lerp(float11, pos, t3);
			}
			if (num17 > rotationSpeedLimit && rotationSpeedLimit >= 0f)
			{
				tdata.flag.SetBits(10, value: true);
				float t4 = math.saturate(math.max(num17 - rotationSpeedLimit, 0f) / num17);
				num4 = math.lerp(num4, 1f, t4);
				a5 = math.slerp(a5, b, t4);
			}
			float num18 = 0f;
			if (tdata.skipCount > 0)
			{
				num18 = math.lerp(num18, 1f, math.saturate((float)tdata.skipCount * simulationDeltaTime / (tdata.frameDeltaTime * tdata.nowTimeScale)));
			}
			if (tdata.velocityWeight < 1f)
			{
				num18 = math.lerp(num18, 1f, 1f - tdata.velocityWeight);
			}
			if (tdata.nowTimeScale < 1f)
			{
				num18 = math.lerp(num18, 1f, 1f - tdata.nowTimeScale);
			}
			if (num18 > 0f)
			{
				tdata.flag.SetBits(10, value: true);
				num3 = math.lerp(num3, 1f, num18);
				float11 = math.lerp(float11, pos, num18);
				num4 = math.lerp(num4, 1f, num18);
				a5 = math.slerp(a5, b, num18);
			}
			if (tdata.IsInertiaShift)
			{
				cdata.frameComponentShiftVector *= num3;
				cdata.frameComponentShiftRotation = math.slerp(quaternion.identity, cdata.frameComponentShiftRotation, num4);
				cdata.frameComponentShiftVector += float7;
				cdata.frameComponentShiftRotation = math.mul(a4, cdata.frameComponentShiftRotation);
				cdata.frameComponentShiftVector += float9;
				cdata.oldFrameWorldPosition = MathUtility.ShiftPosition(in cdata.oldFrameWorldPosition, in cdata.oldComponentWorldPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
				cdata.oldFrameWorldRotation = math.mul(cdata.frameComponentShiftRotation, cdata.oldFrameWorldRotation);
				cdata.nowWorldPosition = MathUtility.ShiftPosition(in cdata.nowWorldPosition, in cdata.oldComponentWorldPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
				cdata.nowWorldRotation = math.mul(cdata.frameComponentShiftRotation, cdata.nowWorldRotation);
			}
			goto IL_0b51;
		}

		internal static void SimulationStepTeamUpdate(int updateIndex, float simulationDeltaTime, int teamId, ref TeamData tdata, ref ClothParameters param, ref InertiaConstraint.CenterData cdata, ref TeamWindData wdata)
		{
			bool value = updateIndex < tdata.updateCount;
			tdata.flag.SetBits(7, value);
			tdata.nowUpdateTime += simulationDeltaTime;
			float num = tdata.time - tdata.frameOldTime;
			tdata.frameInterpolation = ((num > 0f) ? math.saturate((tdata.nowUpdateTime - tdata.frameOldTime) / num) : 1f);
			cdata.oldWorldPosition = cdata.nowWorldPosition;
			cdata.oldWorldRotation = cdata.nowWorldRotation;
			cdata.nowWorldPosition = math.lerp(cdata.oldFrameWorldPosition, cdata.frameWorldPosition, tdata.frameInterpolation);
			cdata.nowWorldRotation = math.slerp(cdata.oldFrameWorldRotation, cdata.frameWorldRotation, tdata.frameInterpolation);
			cdata.nowWorldRotation = math.normalize(cdata.nowWorldRotation);
			float3 x = math.lerp(cdata.oldFrameWorldScale, cdata.frameWorldScale, tdata.frameInterpolation);
			cdata.stepVector = cdata.nowWorldPosition - cdata.oldWorldPosition;
			cdata.stepRotation = MathUtility.FromToRotation(in cdata.oldWorldRotation, in cdata.nowWorldRotation);
			float num2 = MathUtility.Angle(in cdata.oldWorldRotation, in cdata.nowWorldRotation);
			float num3 = 1f - param.inertiaConstraint.localInertia;
			float num4 = 1f - param.inertiaConstraint.localInertia;
			float num5 = math.length(cdata.stepVector * (1f - num3)) / simulationDeltaTime;
			if (num5 > param.inertiaConstraint.localMovementSpeedLimit && param.inertiaConstraint.localMovementSpeedLimit >= 0f)
			{
				float t = param.inertiaConstraint.localMovementSpeedLimit / num5;
				num3 = math.lerp(1f, num3, t);
			}
			float num6 = math.degrees(num2 * (1f - num4) / simulationDeltaTime);
			if (num6 > param.inertiaConstraint.localRotationSpeedLimit && param.inertiaConstraint.localRotationSpeedLimit >= 0f)
			{
				float t2 = param.inertiaConstraint.localRotationSpeedLimit / num6;
				num4 = math.lerp(1f, num4, t2);
			}
			cdata.stepMoveInertiaRatio = num3;
			cdata.stepRotationInertiaRatio = num4;
			cdata.inertiaVector = math.lerp(float3.zero, cdata.stepVector, num3);
			cdata.inertiaRotation = math.slerp(quaternion.identity, cdata.stepRotation, num4);
			cdata.angularVelocity = num2 / simulationDeltaTime;
			if (cdata.angularVelocity > 1E-08f)
			{
				MathUtility.ToAngleAxis(in cdata.stepRotation, out var _, out cdata.rotationAxis);
			}
			else
			{
				cdata.rotationAxis = 0;
			}
			tdata.scaleRatio = math.max(math.length(x) / math.length(tdata.initScale), 1E-06f);
			float num7 = 1f;
			if (math.lengthsq(param.worldGravityDirection) > 1E-08f)
			{
				float3 initLocalGravityDirection = cdata.initLocalGravityDirection;
				initLocalGravityDirection.y *= tdata.negativeScaleDirection.y;
				num7 = math.dot(math.mul(cdata.nowWorldRotation, initLocalGravityDirection), param.worldGravityDirection);
				num7 = math.saturate(num7 * 0.5f + 0.5f);
			}
			tdata.gravityDot = num7;
			float gravityRatio = 1f;
			if (param.gravity > 1E-06f && param.gravityFalloff > 1E-06f)
			{
				gravityRatio = math.lerp(math.saturate(1f - param.gravityFalloff), 1f, math.saturate(1f - num7));
			}
			tdata.gravityRatio = gravityRatio;
			if (tdata.velocityWeight < 1f)
			{
				float num8 = ((param.stablizationTimeAfterReset > 1E-06f) ? (simulationDeltaTime / param.stablizationTimeAfterReset) : 1f);
				tdata.velocityWeight = math.saturate(tdata.velocityWeight + num8);
			}
			tdata.blendWeight = math.saturate(tdata.velocityWeight * param.blendWeight * tdata.distanceWeight);
			UpdateWind(simulationDeltaTime, teamId, in tdata, in param.wind, in cdata, ref wdata);
		}

		private static void UpdateWind(float simulationDeltaTime, int teamId, in TeamData tdata, in WindParams windParams, in InertiaConstraint.CenterData cdata, ref TeamWindData teamWindData)
		{
			if (windParams.IsValid())
			{
				int zoneCount = teamWindData.ZoneCount;
				for (int i = 0; i < zoneCount; i++)
				{
					TeamWindInfo windInfo = teamWindData.windZoneList[i];
					UpdateWindTime(ref windInfo, windParams.frequency, simulationDeltaTime);
					teamWindData.windZoneList[i] = windInfo;
				}
				TeamWindInfo windInfo2 = teamWindData.movingWind;
				windInfo2.main = 0f;
				if (windParams.movingWind > 0.01f)
				{
					windInfo2.main = cdata.frameMovingSpeed * windParams.movingWind / tdata.scaleRatio;
					windInfo2.direction = -cdata.frameMovingDirection;
					UpdateWindTime(ref windInfo2, windParams.frequency, simulationDeltaTime);
				}
				teamWindData.movingWind = windInfo2;
			}
		}

		private static void UpdateWindTime(ref TeamWindInfo windInfo, float frequency, float simulationDeltaTime)
		{
			float num = windInfo.main / 7.5f;
			float num2 = 0.2f + num * 0.5f;
			num2 *= frequency;
			num2 = math.min(num2, 1.5f);
			num2 *= simulationDeltaTime;
			windInfo.time += num2;
			if (windInfo.time > 10000f)
			{
				windInfo.time -= 20000f;
			}
		}

		internal static void SimulationPostTeamUpdate(ref TeamData tdata, ref InertiaConstraint.CenterData cdata)
		{
			cdata.oldComponentWorldPosition = cdata.componentWorldPosition;
			cdata.oldComponentWorldRotation = cdata.componentWorldRotation;
			cdata.oldComponentWorldScale = cdata.componentWorldScale;
			if (tdata.IsRunning)
			{
				cdata.oldFrameWorldPosition = cdata.frameWorldPosition;
				cdata.oldFrameWorldRotation = cdata.frameWorldRotation;
				cdata.oldFrameWorldScale = cdata.frameWorldScale;
				tdata.forceMode = ClothForceMode.None;
				tdata.impactForce = 0;
				tdata.skipCount = 0;
			}
			cdata.oldAnchorPosition = cdata.anchorPosition;
			cdata.oldAnchorRotation = cdata.anchorRotation;
			cdata.anchorComponentLocalPosition = MathUtility.InverseTransformPoint(in cdata.componentWorldPosition, in cdata.anchorPosition, in cdata.anchorRotation, (float3)1);
			tdata.flag.SetBits(2, value: false);
			tdata.flag.SetBits(3, value: false);
			tdata.flag.SetBits(5, value: false);
			tdata.flag.SetBits(7, value: false);
			tdata.flag.SetBits(9, value: false);
			tdata.flag.SetBits(10, value: false);
			tdata.flag.SetBits(18, value: false);
			if (tdata.time > 7200f)
			{
				tdata.time -= 3600f;
				tdata.oldTime -= 3600f;
				tdata.nowUpdateTime -= 3600f;
				tdata.oldUpdateTime -= 3600f;
				tdata.frameUpdateTime -= 3600f;
				tdata.frameOldTime -= 3600f;
			}
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Team Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Team Manager. Invalid.");
				stringBuilder.AppendLine();
				Debug.Log(stringBuilder.ToString());
				allsb.Append(stringBuilder);
				return;
			}
			stringBuilder.AppendLine($"Team Manager. Team:{TeamCount}, Mapping:{MappingCount}, Monitoring:{monitoringProcessSet.Count}");
			stringBuilder.AppendLine("  -teamDataArray:" + teamDataArray.ToSummary());
			stringBuilder.AppendLine("  -teamWindArray:" + teamWindArray.ToSummary());
			stringBuilder.AppendLine("  -mappingDataArray:" + mappingDataArray.ToSummary());
			stringBuilder.AppendLine("  -parameterArray:" + parameterArray.ToSummary());
			stringBuilder.AppendLine("  -centerDataArray:" + centerDataArray.ToSummary());
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
			for (int i = 1; i < TeamCount; i++)
			{
				TeamData teamData = teamDataArray[i];
				if (!teamData.IsValid)
				{
					continue;
				}
				stringBuilder.Clear();
				FixedList64Bytes<short> fixedList64Bytes = teamMappingIndexArray[i];
				ClothProcess clothProcess = GetClothProcess(i);
				if (clothProcess == null)
				{
					stringBuilder.AppendLine($"ID:{i} cprocess is null!");
					Debug.LogWarning(stringBuilder.ToString());
					allsb.Append(stringBuilder);
					continue;
				}
				MagicaCloth cloth = clothProcess.cloth;
				if (cloth == null)
				{
					stringBuilder.AppendLine($"ID:{i} cloth is null!");
					Debug.LogWarning(stringBuilder.ToString());
					allsb.Append(stringBuilder);
					continue;
				}
				stringBuilder.AppendLine($"ID:{i} [{clothProcess.Name}] state:0x{clothProcess.GetStateFlag().Value:X}, Flag:0x{teamData.flag.Value:X}, Particle:{teamData.ParticleCount}, Collider:{clothProcess.ColliderCapacity} Proxy:{teamData.proxyMeshType}, Mapping:{fixedList64Bytes.Length}");
				stringBuilder.AppendLine($"  -centerTransformIndex {teamData.centerTransformIndex}");
				stringBuilder.AppendLine($"  -initScale {teamData.initScale}");
				stringBuilder.AppendLine($"  -scaleRatio {teamData.scaleRatio}");
				stringBuilder.AppendLine($"  -animationPoseRatio {teamData.animationPoseRatio}");
				stringBuilder.AppendLine($"  -blendWeight {teamData.blendWeight}");
				stringBuilder.AppendLine($"  Sync:{cloth.SyncPartnerCloth}, SyncParentCount:{teamData.syncParentTeamId.Length}");
				stringBuilder.AppendLine($"  -ProxyTransformChunk {teamData.proxyTransformChunk}");
				stringBuilder.AppendLine($"  -ProxyCommonChunk {teamData.proxyCommonChunk}");
				stringBuilder.AppendLine($"  -ProxyMeshChunk {teamData.proxyMeshChunk}");
				stringBuilder.AppendLine($"  -ProxyBoneChunk {teamData.proxyBoneChunk}");
				stringBuilder.AppendLine($"  -ProxySkinBoneChunk {teamData.proxySkinBoneChunk}");
				stringBuilder.AppendLine($"  -ProxyTriangleChunk {teamData.proxyTriangleChunk}");
				stringBuilder.AppendLine($"  -ProxyEdgeChunk {teamData.proxyEdgeChunk}");
				stringBuilder.AppendLine($"  -BaseLineChunk {teamData.baseLineChunk}");
				stringBuilder.AppendLine($"  -BaseLineDataChunk {teamData.baseLineDataChunk}");
				stringBuilder.AppendLine($"  -ParticleChunk {teamData.particleChunk}");
				stringBuilder.AppendLine($"  -ColliderChunk {teamData.colliderChunk}");
				stringBuilder.AppendLine($"  -ColliderTrnasformChunk {teamData.colliderTransformChunk}");
				stringBuilder.AppendLine($"  -colliderCount {teamData.colliderCount}");
				stringBuilder.AppendLine($"  *Mapping Count {fixedList64Bytes.Length}");
				if (fixedList64Bytes.Length > 0)
				{
					for (int j = 0; j < fixedList64Bytes.Length; j++)
					{
						int num = fixedList64Bytes[j];
						MappingData mappingData = mappingDataArray[num];
						stringBuilder.AppendLine($"  *Mapping Mid:{num}, Vertex:{mappingData.VertexCount}");
						stringBuilder.AppendLine($"    -teamId:{mappingData.teamId}");
						stringBuilder.AppendLine($"    -centerTransformIndex:{mappingData.centerTransformIndex}");
						stringBuilder.AppendLine($"    -mappingCommonChunk:{mappingData.mappingCommonChunk}");
						stringBuilder.AppendLine($"    -toProxyMatrix:{mappingData.toProxyMatrix}");
						stringBuilder.AppendLine($"    -toProxyRotation:{mappingData.toProxyRotation}");
						stringBuilder.AppendLine($"    -sameSpace:{mappingData.sameSpace}");
						stringBuilder.AppendLine($"    -toMappingMatrix:{mappingData.toMappingMatrix}");
						stringBuilder.AppendLine($"    -scaleRatio:{mappingData.scaleRatio}");
						stringBuilder.AppendLine($"    -renderDataWorkIndex:{mappingData.renderDataWorkIndex}");
					}
				}
				stringBuilder.AppendLine($"  +DistanceStartChunk {teamData.distanceStartChunk}");
				stringBuilder.AppendLine($"  +DistanceDataChunk {teamData.distanceDataChunk}");
				stringBuilder.AppendLine($"  +BendingPairChunk {teamData.bendingPairChunk}");
				stringBuilder.AppendLine($"  +selfPointChunk {teamData.selfPointChunk}");
				stringBuilder.AppendLine($"  +selfEdgeChunk {teamData.selfEdgeChunk}");
				stringBuilder.AppendLine($"  +selfTriangleChunk {teamData.selfTriangleChunk}");
				TeamWindData teamWindData = teamWindArray[i];
				stringBuilder.AppendLine($"  #Wind ZoneCount:{teamWindData.ZoneCount}");
				for (int k = 0; k < teamWindData.ZoneCount; k++)
				{
					stringBuilder.AppendLine($"    [{k}] {teamWindData.windZoneList[k].ToString()}");
				}
				stringBuilder.AppendLine("    [Move] " + teamWindData.movingWind.ToString());
				Debug.Log(stringBuilder.ToString());
				allsb.Append(stringBuilder);
			}
			allsb.AppendLine();
			stringBuilder.Clear();
			int count = mappingDataArray.Count;
			stringBuilder.AppendLine($"#MappingData Count:{count}");
			for (int l = 0; l < count; l++)
			{
				MappingData mappingData2 = mappingDataArray[l];
				if (mappingData2.IsValid())
				{
					stringBuilder.AppendLine($"[{l}] teamId:{mappingData2.teamId}, renderDataWorkIndex:{mappingData2.renderDataWorkIndex}");
				}
			}
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
