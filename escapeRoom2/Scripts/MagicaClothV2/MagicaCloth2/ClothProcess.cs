using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace MagicaCloth2
{
	public class ClothProcess : IDisposable, IValid, ITransform
	{
		[BurstCompile]
		private struct GenerateSelectionJob : IJobParallelFor
		{
			public int offset;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> positionList;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<VertexAttribute> attributeList;

			public int attributeMapWidth;

			public float4x4 toM;

			public int2 xySize;

			public ExBitFlag8 attributeReadFlag;

			[ReadOnly]
			public NativeArray<Color32> attributeMapData;

			[ReadOnly]
			public NativeArray<float2> uvs;

			[ReadOnly]
			public NativeArray<float3> vertexs;

			public void Execute(int vindex)
			{
				int2 int5 = (int2)((uvs[vindex] % 1f + 1f) % 1f * xySize);
				Color32 color = attributeMapData[int5.y * attributeMapWidth + int5.x];
				VertexAttribute value = default(VertexAttribute);
				if (attributeReadFlag.IsSet(2) && color.g > 32)
				{
					value.SetFlag(2, sw: true);
				}
				else if (attributeReadFlag.IsSet(1) && color.r > 32)
				{
					value.SetFlag(1, sw: true);
				}
				if (attributeReadFlag.IsSet(4) && color.b <= 32)
				{
					value.SetFlag(8, sw: true);
				}
				float3 value2 = math.transform(toM, vertexs[vindex]);
				positionList[offset + vindex] = value2;
				attributeList[offset + vindex] = value;
			}
		}

		public class RenderMeshInfo
		{
			public int renderHandle;

			public VirtualMeshContainer renderMeshContainer;

			public DataChunk mappingChunk;

			public int renderDataWorkIndex;
		}

		public class PaintMapData
		{
			public const byte ReadFlag_Fixed = 1;

			public const byte ReadFlag_Move = 2;

			public const byte ReadFlag_Limit = 4;

			public Color32[] paintData;

			public int paintMapWidth;

			public int paintMapHeight;

			public ExBitFlag8 paintReadFlag;
		}

		public enum ClothType
		{
			MeshCloth = 0,
			BoneCloth = 1,
			BoneSpring = 10
		}

		private static readonly ProfilerMarker initClothProfiler = new ProfilerMarker("InitCloth");

		private static readonly ProfilerMarker preBuildProfiler = new ProfilerMarker("ClothProcess.PreBuild");

		private static readonly ProfilerMarker preBuildDeserializationProfiler = new ProfilerMarker("ClothProcess.PreBuild.Deserialization");

		private static readonly ProfilerMarker preBuildRegistrationProfiler = new ProfilerMarker("ClothProcess.PreBuild.Registration");

		public const int State_Valid = 0;

		public const int State_Enable = 1;

		public const int State_InitSuccess = 3;

		public const int State_InitComplete = 4;

		public const int State_Build = 5;

		public const int State_Running = 6;

		public const int State_DisableAutoBuild = 7;

		public const int State_CameraCullingInvisible = 8;

		public const int State_CameraCullingKeep = 9;

		public const int State_SkipWriting = 10;

		public const int State_UsePreBuild = 12;

		public const int State_DistanceCullingInvisible = 13;

		public const int State_UpdateTangent = 14;

		public const int State_Component = 15;

		public const int State_Verification = 16;

		internal BitField32 stateFlag;

		internal List<int> renderHandleList = new List<int>();

		internal RenderSetupData boneClothSetupData;

		internal List<RenderMeshInfo> renderMeshInfoList = new List<RenderMeshInfo>();

		internal List<TransformRecord> customSkinningBoneRecords = new List<TransformRecord>();

		internal ResultCode result;

		private ReductionSettings reductionSettings;

		internal List<ColliderComponent> colliderList = new List<ColliderComponent>();

		internal InertiaConstraint.ConstraintData inertiaConstraintData;

		internal DistanceConstraint.ConstraintData distanceConstraintData;

		internal TriangleBendingConstraint.ConstraintData bendingConstraintData;

		internal Animator interlockingAnimator;

		internal List<Renderer> interlockingAnimatorRenderers = new List<Renderer>();

		internal int anchorTransformId;

		internal int distanceReferenceObjectId;

		internal Animator cameraCullingAnimator;

		internal List<Renderer> cameraCullingRenderers;

		internal CullingSettings.CameraCullingMode cameraCullingMode;

		internal bool cameraCullingOldInvisible;

		private CancellationTokenSource cts = new CancellationTokenSource();

		private volatile object lockObject = new object();

		private volatile bool isDestory;

		private volatile bool isDestoryInternal;

		private volatile bool isBuild;

		public MagicaCloth cloth { get; internal set; }

		public MagicaCloth SyncTopCloth { get; internal set; }

		internal TransformRecord clothTransformRecord { get; private set; }

		internal TransformRecord normalAdjustmentTransformRecord { get; private set; }

		public ResultCode Result => result;

		public ResultCode InitDataResult { get; internal set; }

		internal ClothType clothType { get; private set; }

		public ClothParameters parameters { get; private set; }

		public VirtualMeshContainer ProxyMeshContainer { get; private set; }

		internal int ColliderCapacity => colliderList.Count;

		public int TeamId { get; private set; }

		public bool IsEnable
		{
			get
			{
				if (!IsValid() || TeamId == 0)
				{
					return false;
				}
				return MagicaManager.Team.IsEnable(TeamId);
			}
		}

		public bool HasProxyMesh
		{
			get
			{
				if (!IsValid() || TeamId == 0)
				{
					return false;
				}
				return ProxyMeshContainer?.shareVirtualMesh?.IsSuccess == true;
			}
		}

		public string Name
		{
			get
			{
				if (!(cloth != null))
				{
					return "(none)";
				}
				return cloth.name;
			}
		}

		internal void Init()
		{
			result.SetSuccess();
			InitDataResult.Clear();
			try
			{
				if (isDestory)
				{
					Develop.LogError((object)"Already destroyed components cannot be reinitialized.");
					throw new OperationCanceledException();
				}
				if (IsState(4))
				{
					throw new OperationCanceledException();
				}
				ClothSerializeData serializeData = cloth.SerializeData;
				ClothSerializeData2 serializeData2 = cloth.GetSerializeData2();
				SetState(0, sw: false);
				cloth.InitAnimationProperty();
				if (!serializeData.IsValid())
				{
					result.SetResult(serializeData.VerificationResult);
					throw new OperationCanceledException();
				}
				ResultCode src = GenerateStatusCheck();
				result.Merge(src);
				if (src.IsError())
				{
					throw new MagicaClothProcessingException();
				}
				SetState(4, sw: true);
				bool flag = serializeData2.preBuildData.UsePreBuild();
				SharePreBuildData sharePreBuildData = null;
				if (flag)
				{
					SetState(12, sw: true);
					ResultCode src2 = serializeData2.preBuildData.DataValidate();
					if (src2.IsFaild())
					{
						result.Merge(src2);
						throw new OperationCanceledException();
					}
					sharePreBuildData = serializeData2.preBuildData.GetSharePreBuildData();
				}
				bool flag2 = false;
				if (!flag && serializeData2.initData != null && serializeData2.initData.HasData())
				{
					InitDataResult = serializeData2.initData.DataValidate(this);
					flag2 = InitDataResult.IsSuccess();
				}
				clothType = serializeData.clothType;
				reductionSettings = serializeData.reductionSetting;
				parameters = serializeData.GetClothParameters();
				clothTransformRecord = new TransformRecord(cloth.ClothTransform, !flag2);
				if (flag)
				{
					clothTransformRecord.scale = sharePreBuildData.buildScale;
				}
				if (flag2)
				{
					serializeData2.initData.clothTransformRecord.Deserialize(clothTransformRecord);
				}
				normalAdjustmentTransformRecord = new TransformRecord(serializeData.normalAlignmentSetting.adjustmentTransform ? serializeData.normalAlignmentSetting.adjustmentTransform : cloth.ClothTransform, !flag2);
				if (flag2)
				{
					serializeData2.initData.normalAdjustmentTransformRecord.Deserialize(normalAdjustmentTransformRecord);
				}
				PreBuildManager.ShareDeserializationData shareDeserializationData = (flag ? MagicaManager.PreBuild.RegisterPreBuildData(sharePreBuildData, referenceIncrement: true) : null);
				UniquePreBuildData uniquePreBuildData = (flag ? serializeData2.preBuildData.uniquePreBuildData : null);
				if (clothType == ClothType.MeshCloth)
				{
					for (int i = 0; i < serializeData.sourceRenderers.Count; i++)
					{
						Renderer renderer = serializeData.sourceRenderers[i];
						if (!renderer)
						{
							continue;
						}
						RenderSetupData renderSetupData = null;
						RenderSetupData.UniqueSerializationData referenceUniqueSetupData = null;
						if (flag)
						{
							renderSetupData = shareDeserializationData.renderSetupDataList[i];
							referenceUniqueSetupData = uniquePreBuildData.renderSetupDataList[i];
							if (renderSetupData.result.IsFaild())
							{
								renderSetupData.Dispose();
								result.SetError(Define.Result.PreBuild_SetupDeserializationError);
								throw new OperationCanceledException();
							}
						}
						RenderSetupSerializeData referenceInitSetupData = (flag2 ? serializeData2.initData.clothSetupDataList[i] : null);
						int num = AddRenderer(renderer, renderSetupData, referenceUniqueSetupData, referenceInitSetupData);
						if (num == 0)
						{
							result.SetError(Define.Result.ClothInit_FailedAddRenderer);
							throw new OperationCanceledException();
						}
						RenderData rendererData = MagicaManager.Render.GetRendererData(num);
						result.Merge(rendererData.Result);
						if (rendererData.Result.IsFaild())
						{
							throw new OperationCanceledException();
						}
					}
				}
				else if (clothType == ClothType.BoneCloth && !flag)
				{
					CreateBoneRenderSetupData(flag2 ? serializeData2.initData : null, clothType, serializeData.rootBones, null, serializeData.connectionMode);
				}
				else if (clothType == ClothType.BoneSpring && !flag)
				{
					CreateBoneRenderSetupData(flag2 ? serializeData2.initData : null, clothType, serializeData.rootBones, serializeData.colliderCollisionConstraint.collisionBones, RenderSetupData.BoneConnectionMode.Line);
				}
				int count = serializeData.customSkinningSetting.skinningBones.Count;
				for (int j = 0; j < count; j++)
				{
					TransformRecord transformRecord = new TransformRecord(serializeData.customSkinningSetting.skinningBones[j], !flag2);
					if (flag2)
					{
						serializeData2.initData.customSkinningBoneRecords[j].Deserialize(transformRecord);
					}
					customSkinningBoneRecords.Add(transformRecord);
				}
				MagicaCloth syncPartnerCloth = cloth.SyncPartnerCloth;
				if ((bool)syncPartnerCloth)
				{
					MagicaManager.Team.comp2SyncPartnerCompMap.Add(cloth.GetInstanceID(), syncPartnerCloth.GetInstanceID());
					MagicaCloth magicaCloth = syncPartnerCloth;
					while ((bool)magicaCloth)
					{
						if (magicaCloth == cloth)
						{
							magicaCloth = null;
							continue;
						}
						if (!magicaCloth.SyncPartnerCloth)
						{
							break;
						}
						magicaCloth = magicaCloth.SyncPartnerCloth;
					}
					SyncTopCloth = magicaCloth;
					MagicaManager.Team.comp2SyncTopCompMap.Add(cloth.GetInstanceID(), SyncTopCloth.GetInstanceID());
				}
				result.SetSuccess();
				SetState(0, sw: true);
				SetState(3, sw: true);
				SetState(16, sw: true);
				if (!cloth.isActiveAndEnabled)
				{
					MagicaManager.Team.AddMonitoringProcess(this);
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (MagicaClothProcessingException)
			{
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.ClothProcess_Exception);
			}
			result.IsSuccess();
		}

		private int AddRenderer(Renderer ren, RenderSetupData referenceSetupData, RenderSetupData.UniqueSerializationData referenceUniqueSetupData, RenderSetupSerializeData referenceInitSetupData)
		{
			if (ren == null)
			{
				return 0;
			}
			if (renderHandleList == null)
			{
				return 0;
			}
			int num = ren.GetInstanceID();
			if (!renderHandleList.Contains(num))
			{
				num = MagicaManager.Render.AddRenderer(ren, referenceSetupData, referenceUniqueSetupData, referenceInitSetupData);
				if (num != 0)
				{
					lock (lockObject)
					{
						if (!renderHandleList.Contains(num))
						{
							renderHandleList.Add(num);
						}
					}
				}
			}
			return num;
		}

		private void CreateBoneRenderSetupData(ClothInitSerializeData initData, ClothType ctype, List<Transform> rootTransforms, List<Transform> collisionBones, RenderSetupData.BoneConnectionMode connectionMode)
		{
			boneClothSetupData = new RenderSetupData(initData?.clothSetupDataList[0], (ctype != ClothType.BoneSpring) ? RenderSetupData.SetupType.BoneCloth : RenderSetupData.SetupType.BoneSpring, clothTransformRecord.transform, rootTransforms, collisionBones, connectionMode, cloth.name);
		}

		internal void StartUse()
		{
			if (MagicaManager.IsPlaying())
			{
				SetState(15, sw: true);
				UpdateUse();
			}
		}

		internal void EndUse()
		{
			if (MagicaManager.IsPlaying())
			{
				SetState(15, sw: false);
				UpdateUse();
			}
		}

		internal void UpdateUse()
		{
			bool flag = IsState(15) && IsState(16);
			SetState(1, flag);
			MagicaManager.Team.SetEnable(TeamId, flag);
			if (renderHandleList == null)
			{
				return;
			}
			foreach (int renderHandle in renderHandleList)
			{
				if (flag)
				{
					MagicaManager.Render.StartUse(this, renderHandle);
				}
				else
				{
					MagicaManager.Render.EndUse(this, renderHandle);
				}
			}
		}

		internal void DataUpdate()
		{
			cloth.SerializeData.DataValidate();
			cloth.serializeData2.DataValidate();
			if (Application.isPlaying)
			{
				MagicaManager.Team.parameterDirtyList.Add(this);
			}
		}

		internal bool StartRuntimeBuild()
		{
			if (IsValid() && IsState(3) && !IsState(5) && !IsState(12))
			{
				result.SetProcess();
				SetState(5, sw: true);
				RuntimeBuildAsync(cts.Token);
				return true;
			}
			if (!result.IsError())
			{
				result.SetError(Define.Result.CreateCloth_CanNotStart);
			}
			Develop.LogError((object)("Cloth runtime build failure! [" + cloth.name + "] : " + result.GetResultString()));
			return false;
		}

		internal bool AutoBuild()
		{
			bool flag = true;
			bool flag2;
			if (IsState(7))
			{
				flag2 = false;
			}
			else if (IsState(12))
			{
				flag2 = PreBuildDataConstruction();
			}
			else
			{
				flag2 = StartRuntimeBuild();
				if (flag2)
				{
					flag = false;
				}
			}
			if (flag)
			{
				cloth?.OnBuildComplete?.Invoke(cloth, flag2);
			}
			return flag2;
		}

		private async Task RuntimeBuildAsync(CancellationToken ct)
		{
			isBuild = true;
			result.SetProcess();
			List<RenderMeshInfo> renderMeshInfos = new List<RenderMeshInfo>();
			VirtualMesh proxyMesh = null;
			try
			{
				ClothSerializeData sdata = cloth.SerializeData;
				ClothSerializeData2 sdata2 = cloth.GetSerializeData2();
				await Task.Delay(5);
				ct.ThrowIfCancellationRequested();
				if ((bool)cloth.SyncPartnerCloth)
				{
					MagicaCloth syncPartnerCloth = cloth.SyncPartnerCloth;
					while (syncPartnerCloth != cloth && syncPartnerCloth != null)
					{
						syncPartnerCloth.Process.IncrementSuspendCounter();
						syncPartnerCloth = syncPartnerCloth.SyncPartnerCloth;
					}
				}
				bool useManualVertexAttribute = false;
				if (sdata.clothType == ClothType.MeshCloth && sdata2.vertexAttributeList != null && sdata2.vertexAttributeList.Count > 0)
				{
					if (sdata2.vertexAttributeList.Count != renderHandleList.Count)
					{
						result.SetError(Define.Result.CreateCloth_VertexAttributeListCountMismatch);
						throw new MagicaClothProcessingException();
					}
					for (int i = 0; i < renderHandleList.Count; i++)
					{
						VertexAttribute[] array = sdata2.vertexAttributeList[i];
						if (array == null)
						{
							result.SetError(Define.Result.CreateCloth_VertexAttributeListIsNull);
							throw new MagicaClothProcessingException();
						}
						int handle = renderHandleList[i];
						if (MagicaManager.Render.GetRendererData(handle).setupData.vertexCount != array.Length)
						{
							result.SetError(Define.Result.CreateCloth_VertexAttributeListDataMismatch);
							throw new MagicaClothProcessingException();
						}
					}
					useManualVertexAttribute = true;
				}
				bool usePaintMap = false;
				List<PaintMapData> paintMapDataList = new List<PaintMapData>();
				if (sdata.clothType == ClothType.MeshCloth && sdata.paintMode != ClothSerializeData.PaintMode.Manual && !useManualVertexAttribute)
				{
					ResultCode src = GeneratePaintMapDataList(paintMapDataList);
					if (src.IsError())
					{
						result.Merge(src);
						throw new MagicaClothProcessingException();
					}
					if (paintMapDataList.Count != renderHandleList.Count)
					{
						result.SetError(Define.Result.CreateCloth_PaintMapCountMismatch);
						throw new MagicaClothProcessingException();
					}
					usePaintMap = true;
				}
				SelectionData selectionData = ((usePaintMap || useManualVertexAttribute) ? new SelectionData() : sdata2.selectionData.Clone());
				Dictionary<int, VertexAttribute> boneAttributeDict = null;
				if (sdata2.boneAttributeDict.Count > 0)
				{
					boneAttributeDict = new Dictionary<int, VertexAttribute>(sdata2.boneAttributeDict.Count);
					foreach (KeyValuePair<Transform, VertexAttribute> item2 in sdata2.boneAttributeDict)
					{
						if ((bool)item2.Key)
						{
							boneAttributeDict.Add(item2.Key.GetInstanceID(), item2.Value);
						}
					}
				}
				await Task.Run(delegate
				{
					VirtualMesh virtualMesh = null;
					try
					{
						ct.ThrowIfCancellationRequested();
						proxyMesh = new VirtualMesh("Proxy");
						proxyMesh.result.SetProcess();
						List<int> list = null;
						if (clothType == ClothType.MeshCloth)
						{
							proxyMesh.SetTransform(clothTransformRecord);
							lock (lockObject)
							{
								list = new List<int>(renderHandleList);
							}
						}
						bool flag = selectionData?.IsValid() ?? false;
						if (clothType == ClothType.MeshCloth)
						{
							if (renderHandleList.Count == 0)
							{
								result.SetError(Define.Result.ClothProcess_InvalidRenderHandleList);
								throw new MagicaClothProcessingException();
							}
							for (int j = 0; j < list.Count; j++)
							{
								ct.ThrowIfCancellationRequested();
								int num = list[j];
								RenderData rendererData = MagicaManager.Render.GetRendererData(num);
								virtualMesh = new VirtualMesh("[" + rendererData.Name + "]");
								virtualMesh.result.SetProcess();
								virtualMesh.ImportFrom(rendererData);
								if (virtualMesh.IsError)
								{
									result.Merge(virtualMesh.result);
									throw new MagicaClothProcessingException();
								}
								SelectionData selectionData2 = selectionData;
								if (useManualVertexAttribute)
								{
									ResultCode src2 = GenerateSelectionDataFromVertexAttributeData(clothTransformRecord, virtualMesh, sdata2.vertexAttributeList[j], out selectionData2);
									if (src2.IsError())
									{
										result.Merge(src2);
										throw new MagicaClothProcessingException();
									}
									selectionData.Merge(selectionData2);
								}
								if (usePaintMap)
								{
									ResultCode src3 = GenerateSelectionDataFromPaintMap(clothTransformRecord, virtualMesh, paintMapDataList[j], out selectionData2);
									if (src3.IsError())
									{
										result.Merge(src3);
										throw new MagicaClothProcessingException();
									}
									selectionData.Merge(selectionData2);
								}
								flag = selectionData?.IsValid() ?? false;
								ct.ThrowIfCancellationRequested();
								if (selectionData2 != null && selectionData2.IsValid())
								{
									float x = virtualMesh.CalcSelectionMergin(reductionSettings);
									x = math.max(x, 1E-05f);
									virtualMesh.SelectionMesh(selectionData2, clothTransformRecord.localToWorldMatrix, x);
									if (virtualMesh.IsError)
									{
										result.Merge(virtualMesh.result);
										throw new MagicaClothProcessingException();
									}
								}
								ct.ThrowIfCancellationRequested();
								virtualMesh.result.SetSuccess();
								proxyMesh.AddMesh(virtualMesh);
								RenderMeshInfo item = new RenderMeshInfo
								{
									renderHandle = num,
									renderMeshContainer = new VirtualMeshContainer(virtualMesh),
									renderDataWorkIndex = rendererData.renderDataWorkIndex
								};
								virtualMesh = null;
								renderMeshInfos.Add(item);
							}
						}
						else if (clothType == ClothType.BoneCloth || clothType == ClothType.BoneSpring)
						{
							proxyMesh.ImportFrom(boneClothSetupData);
							if (proxyMesh.IsError)
							{
								result.Merge(proxyMesh.result);
								throw new MagicaClothProcessingException();
							}
							if (!flag)
							{
								selectionData = new SelectionData(proxyMesh, float4x4.identity);
								if (selectionData.Count > 0)
								{
									selectionData.Fill(VertexAttribute.Move);
									foreach (int rootTransformId in boneClothSetupData.rootTransformIdList)
									{
										int transformIndexFromId = boneClothSetupData.GetTransformIndexFromId(rootTransformId);
										selectionData.attributes[transformIndexFromId] = VertexAttribute.Fixed;
									}
									flag = selectionData.IsValid();
								}
							}
							if (boneAttributeDict != null)
							{
								foreach (KeyValuePair<int, VertexAttribute> item3 in boneAttributeDict)
								{
									int transformIndexFromId2 = boneClothSetupData.GetTransformIndexFromId(item3.Key);
									if (transformIndexFromId2 >= 0)
									{
										selectionData.attributes[transformIndexFromId2] = item3.Value;
									}
								}
							}
						}
						if (clothType == ClothType.MeshCloth && proxyMesh.VertexCount > 1)
						{
							ct.ThrowIfCancellationRequested();
							if (reductionSettings.IsEnabled)
							{
								proxyMesh.Reduction(reductionSettings, ct);
								if (proxyMesh.IsError)
								{
									result.Merge(proxyMesh.result);
									throw new MagicaClothProcessingException();
								}
							}
						}
						if (!proxyMesh.joinIndices.IsCreated)
						{
							ct.ThrowIfCancellationRequested();
							proxyMesh.joinIndices = new NativeArray<int>(proxyMesh.VertexCount, Allocator.Persistent);
							JobUtility.SerialNumberRun(proxyMesh.joinIndices, proxyMesh.VertexCount);
						}
						ct.ThrowIfCancellationRequested();
						proxyMesh.Optimization();
						if (proxyMesh.IsError)
						{
							result.Merge(proxyMesh.result);
							throw new MagicaClothProcessingException();
						}
						if (flag)
						{
							proxyMesh.ApplySelectionAttribute(selectionData);
							if (proxyMesh.IsError)
							{
								result.Merge(proxyMesh.result);
								throw new MagicaClothProcessingException();
							}
						}
						ct.ThrowIfCancellationRequested();
						proxyMesh.ConvertProxyMesh(sdata, clothTransformRecord, customSkinningBoneRecords, normalAdjustmentTransformRecord);
						if (proxyMesh.IsError)
						{
							result.Merge(proxyMesh.result);
							throw new MagicaClothProcessingException();
						}
						if (proxyMesh.VertexCount > 32767)
						{
							result.SetError(Define.Result.ProxyMesh_Over32767Vertices);
							throw new MagicaClothProcessingException();
						}
						if (proxyMesh.EdgeCount > 32767)
						{
							result.SetError(Define.Result.ProxyMesh_Over32767Edges);
							throw new MagicaClothProcessingException();
						}
						if (proxyMesh.TriangleCount > 32767)
						{
							result.SetError(Define.Result.ProxyMesh_Over32767Triangles);
							throw new MagicaClothProcessingException();
						}
						ct.ThrowIfCancellationRequested();
						if (proxyMesh.IsError)
						{
							result.Merge(proxyMesh.result);
							throw new MagicaClothProcessingException();
						}
						proxyMesh.result.SetSuccess();
						if (clothType == ClothType.MeshCloth)
						{
							foreach (RenderMeshInfo item4 in renderMeshInfos)
							{
								ct.ThrowIfCancellationRequested();
								VirtualMesh shareVirtualMesh2 = item4.renderMeshContainer.shareVirtualMesh;
								shareVirtualMesh2.Mapping(proxyMesh);
								if (shareVirtualMesh2.IsError)
								{
									result.Merge(shareVirtualMesh2.result);
									throw new MagicaClothProcessingException();
								}
							}
							return;
						}
					}
					catch (MagicaClothProcessingException)
					{
						throw;
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						result.SetError(Define.Result.ClothProcess_Exception);
						throw;
					}
					finally
					{
						virtualMesh?.Dispose();
					}
				}, ct);
				ct.ThrowIfCancellationRequested();
				if (cloth == null)
				{
					throw new OperationCanceledException();
				}
				MagicaCloth syncCloth = cloth.SyncPartnerCloth;
				if (syncCloth != null)
				{
					int timeOutCount = 100;
					while (syncCloth != null && !syncCloth.Process.IsEnable && timeOutCount >= 0)
					{
						await Task.Delay(20);
						ct.ThrowIfCancellationRequested();
						timeOutCount--;
					}
					if (syncCloth == null || !syncCloth.Process.IsEnable)
					{
						Develop.LogWarning((object)"Sync timeout! Is there a deadlock between synchronous cloths?");
					}
				}
				ct.ThrowIfCancellationRequested();
				if (cloth == null)
				{
					throw new OperationCanceledException();
				}
				if (!IsValid())
				{
					result.SetError(Define.Result.ClothProcess_Invalid);
					throw new MagicaClothProcessingException();
				}
				if (!MagicaManager.IsPlaying())
				{
					result.SetError(Define.Result.ClothProcess_Invalid);
					throw new MagicaClothProcessingException();
				}
				ct.ThrowIfCancellationRequested();
				await Task.Run(delegate
				{
					try
					{
						ct.ThrowIfCancellationRequested();
						distanceConstraintData = DistanceConstraint.CreateData(proxyMesh, parameters);
						if (distanceConstraintData != null && distanceConstraintData.result.IsError())
						{
							result.Merge(distanceConstraintData.result);
							throw new MagicaClothProcessingException();
						}
						ct.ThrowIfCancellationRequested();
						bendingConstraintData = TriangleBendingConstraint.CreateData(proxyMesh, parameters);
						if (bendingConstraintData != null && bendingConstraintData.result.IsError())
						{
							result.Merge(bendingConstraintData.result);
							throw new MagicaClothProcessingException();
						}
						ct.ThrowIfCancellationRequested();
						inertiaConstraintData = InertiaConstraint.CreateData(proxyMesh, parameters);
						if (inertiaConstraintData != null && inertiaConstraintData.result.IsError())
						{
							result.Merge(inertiaConstraintData.result);
							throw new MagicaClothProcessingException();
						}
						if (result.IsError())
						{
							throw new MagicaClothProcessingException();
						}
					}
					catch (MagicaClothProcessingException)
					{
						throw;
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						result.SetError(Define.Result.Constraint_Exception);
						throw;
					}
				}, ct);
				ct.ThrowIfCancellationRequested();
				if (cloth == null)
				{
					throw new OperationCanceledException();
				}
				lock (lockObject)
				{
					ProxyMeshContainer = new VirtualMeshContainer(proxyMesh);
					proxyMesh = null;
					TeamId = MagicaManager.Cloth.AddCloth(this, parameters);
					if (TeamId <= 0)
					{
						result.SetError(Define.Result.ClothProcess_OverflowTeamCount4096);
						throw new MagicaClothProcessingException();
					}
					MagicaManager.Team.parameterDirtyList.Add(this);
					MagicaManager.VMesh.RegisterProxyMesh(TeamId, ProxyMeshContainer);
					MagicaManager.Simulation.RegisterProxyMesh(this);
					MagicaManager.Collider.Register(this);
				}
				ct.ThrowIfCancellationRequested();
				MagicaManager.Simulation.RegisterConstraint(this);
				lock (lockObject)
				{
					if (clothType == ClothType.MeshCloth)
					{
						foreach (RenderMeshInfo item5 in renderMeshInfos)
						{
							VirtualMesh shareVirtualMesh = item5.renderMeshContainer.shareVirtualMesh;
							if (!shareVirtualMesh.IsError && shareVirtualMesh.IsMapping && shareVirtualMesh.IsValid())
							{
								item5.mappingChunk = MagicaManager.VMesh.RegisterMappingMesh(TeamId, item5.renderMeshContainer, item5.renderDataWorkIndex);
							}
						}
					}
					foreach (RenderMeshInfo item6 in renderMeshInfos)
					{
						renderMeshInfoList.Add(item6);
					}
					renderMeshInfos.Clear();
				}
				ct.ThrowIfCancellationRequested();
				UpdateUse();
				result.SetSuccess();
				SetState(6, sw: true);
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.ClothProcess_UnknownError);
				}
			}
			catch (OperationCanceledException)
			{
				result.SetCancel();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.ClothProcess_Exception);
			}
			finally
			{
				foreach (RenderMeshInfo item7 in renderMeshInfos)
				{
					item7?.renderMeshContainer?.Dispose();
				}
				proxyMesh?.Dispose();
				if (cloth != null && (bool)cloth.SyncPartnerCloth)
				{
					MagicaCloth syncPartnerCloth2 = cloth.SyncPartnerCloth;
					while (syncPartnerCloth2 != cloth && syncPartnerCloth2 != null)
					{
						syncPartnerCloth2.Process.DecrementSuspendCounter();
						syncPartnerCloth2 = syncPartnerCloth2.SyncPartnerCloth;
					}
				}
				isBuild = false;
				if (isDestory)
				{
					DisposeInternal();
				}
				else if (cloth != null)
				{
					if (result.IsFaild())
					{
						Develop.LogError((object)("Cloth runtime build failure! [" + cloth.name + "] : " + result.GetResultString()));
					}
					cloth.OnBuildComplete?.Invoke(cloth, result.IsSuccess());
				}
			}
		}

		public ResultCode GenerateSelectionDataFromPaintMap(TransformRecord clothTransformRecord, VirtualMesh renderMesh, PaintMapData paintMapData, out SelectionData selectionData)
		{
			ResultCode resultCode = default(ResultCode);
			resultCode.SetProcess();
			selectionData = new SelectionData();
			try
			{
				if (paintMapData == null)
				{
					resultCode.SetError(Define.Result.CreateCloth_PaintMapCountMismatch);
					throw new MagicaClothProcessingException();
				}
				int vertexCount = renderMesh.VertexCount;
				using NativeArray<float3> positionList = new NativeArray<float3>(vertexCount, Allocator.TempJob);
				using NativeArray<VertexAttribute> attributeList = new NativeArray<VertexAttribute>(vertexCount, Allocator.TempJob);
				float4x4 localToWorldMatrix = MathUtility.Transform(in renderMesh.initLocalToWorld, (float4x4)clothTransformRecord.worldToLocalMatrix);
				int2 xySize = new int2(paintMapData.paintMapWidth, paintMapData.paintMapHeight);
				using NativeArray<Color32> attributeMapData = new NativeArray<Color32>(paintMapData.paintData, Allocator.TempJob);
				IJobParallelForExtensions.Run(new GenerateSelectionJob
				{
					offset = 0,
					positionList = positionList,
					attributeList = attributeList,
					attributeMapWidth = paintMapData.paintMapWidth,
					toM = localToWorldMatrix,
					xySize = xySize,
					attributeReadFlag = paintMapData.paintReadFlag,
					attributeMapData = attributeMapData,
					uvs = renderMesh.uv.GetNativeArray(),
					vertexs = renderMesh.localPositions.GetNativeArray()
				}, vertexCount);
				selectionData.positions = positionList.ToArray();
				selectionData.attributes = attributeList.ToArray();
				selectionData.maxConnectionDistance = MathUtility.TransformDistance(renderMesh.maxVertexDistance.Value, in localToWorldMatrix);
				selectionData.userEdit = true;
				resultCode.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
				if (resultCode.IsNone())
				{
					resultCode.SetError(Define.Result.CreateCloth_InvalidPaintMap);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				resultCode.SetError(Define.Result.CreateCloth_InvalidPaintMap);
			}
			return resultCode;
		}

		public ResultCode GeneratePaintMapDataList(List<PaintMapData> dataList)
		{
			ResultCode resultCode = default(ResultCode);
			resultCode.SetProcess();
			try
			{
				int count = cloth.SerializeData.paintMaps.Count;
				ExBitFlag8 paintReadFlag = new ExBitFlag8(3);
				if (cloth.SerializeData.paintMode == ClothSerializeData.PaintMode.Texture_Fixed_Move_Limit)
				{
					paintReadFlag.SetFlag(4, sw: true);
				}
				for (int i = 0; i < count; i++)
				{
					Texture2D texture2D = cloth.SerializeData.paintMaps[i];
					if (texture2D == null)
					{
						resultCode.SetError(Define.Result.Init_InvalidPaintMap);
						throw new MagicaClothProcessingException();
					}
					if (!texture2D.isReadable)
					{
						resultCode.SetError(Define.Result.Init_PaintMapNotReadable);
						throw new MagicaClothProcessingException();
					}
					int num = texture2D.width;
					int num2 = texture2D.height;
					int num3 = num * num2;
					int num4 = 1;
					while (num4 < texture2D.mipmapCount && num3 > 16384)
					{
						num4++;
						num3 /= 4;
						num /= 2;
						num2 /= 2;
					}
					PaintMapData paintMapData = new PaintMapData();
					paintMapData.paintData = texture2D.GetPixels32(num4 - 1);
					paintMapData.paintMapWidth = num;
					paintMapData.paintMapHeight = num2;
					paintMapData.paintReadFlag = paintReadFlag;
					dataList.Add(paintMapData);
				}
				resultCode.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				resultCode.SetError(Define.Result.Init_InvalidPaintMap);
			}
			return resultCode;
		}

		public ResultCode GenerateSelectionDataFromVertexAttributeData(TransformRecord clothTransformRecord, VirtualMesh renderMesh, VertexAttribute[] vertexAttributeArray, out SelectionData selectionData)
		{
			ResultCode resultCode = default(ResultCode);
			resultCode.SetProcess();
			selectionData = new SelectionData();
			try
			{
				int vertexCount = renderMesh.VertexCount;
				using NativeArray<float3> dstPositions = new NativeArray<float3>(vertexCount, Allocator.TempJob);
				float4x4 toM = MathUtility.Transform(in renderMesh.initLocalToWorld, (float4x4)clothTransformRecord.worldToLocalMatrix);
				JobUtility.TransformPositionRun(renderMesh.localPositions.GetNativeArray(), dstPositions, vertexCount, in toM);
				selectionData.positions = dstPositions.ToArray();
				selectionData.attributes = vertexAttributeArray;
				selectionData.maxConnectionDistance = MathUtility.TransformDistance(renderMesh.maxVertexDistance.Value, in toM);
				selectionData.userEdit = true;
				resultCode.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
				if (resultCode.IsNone())
				{
					resultCode.SetError(Define.Result.CreateCloth_InvalidVertexAttributeData);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				resultCode.SetError(Define.Result.CreateCloth_InvalidVertexAttributeData);
			}
			return resultCode;
		}

		internal bool PreBuildDataConstruction()
		{
			if (!IsState(12))
			{
				return false;
			}
			if (!IsState(3))
			{
				return false;
			}
			result.SetProcess();
			_ = cloth.SerializeData;
			ClothSerializeData2 serializeData = cloth.GetSerializeData2();
			VirtualMeshContainer virtualMeshContainer = null;
			List<VirtualMeshContainer> list = new List<VirtualMeshContainer>();
			try
			{
				UniquePreBuildData uniquePreBuildData = serializeData.preBuildData.uniquePreBuildData;
				PreBuildManager.ShareDeserializationData preBuildData = MagicaManager.PreBuild.GetPreBuildData(serializeData.preBuildData.GetSharePreBuildData());
				try
				{
					virtualMeshContainer = new VirtualMeshContainer(preBuildData.proxyMesh);
					if (virtualMeshContainer.shareVirtualMesh.IsError)
					{
						result.Merge(virtualMeshContainer.shareVirtualMesh.result);
						throw new MagicaClothProcessingException();
					}
					virtualMeshContainer.uniqueData = uniquePreBuildData.proxyMesh;
					for (int i = 0; i < preBuildData.renderMeshList.Count; i++)
					{
						VirtualMeshContainer virtualMeshContainer2 = new VirtualMeshContainer(preBuildData.renderMeshList[i]);
						list.Add(virtualMeshContainer2);
						if (virtualMeshContainer2.shareVirtualMesh.IsError)
						{
							result.Merge(virtualMeshContainer2.shareVirtualMesh.result);
							throw new MagicaClothProcessingException();
						}
						virtualMeshContainer2.uniqueData = uniquePreBuildData.renderMeshList[i];
					}
					inertiaConstraintData = preBuildData.inertiaConstraintData;
					distanceConstraintData = preBuildData.distanceConstraintData;
					bendingConstraintData = preBuildData.bendingConstraintData;
				}
				catch
				{
					throw;
				}
				finally
				{
				}
				try
				{
					ProxyMeshContainer = virtualMeshContainer;
					virtualMeshContainer = null;
					TeamId = MagicaManager.Cloth.AddCloth(this, parameters);
					if (TeamId <= 0)
					{
						result.SetError(Define.Result.ClothProcess_OverflowTeamCount4096);
						throw new MagicaClothProcessingException();
					}
					MagicaManager.Team.parameterDirtyList.Add(this);
					MagicaManager.VMesh.RegisterProxyMesh(TeamId, ProxyMeshContainer);
					MagicaManager.Simulation.RegisterProxyMesh(this);
					MagicaManager.Collider.Register(this);
					MagicaManager.Simulation.RegisterConstraint(this);
					for (int j = 0; j < list.Count; j++)
					{
						VirtualMeshContainer virtualMeshContainer3 = list[j];
						VirtualMesh shareVirtualMesh = virtualMeshContainer3.shareVirtualMesh;
						if (!shareVirtualMesh.IsError && shareVirtualMesh.IsMapping && shareVirtualMesh.IsValid())
						{
							list[j] = null;
							int num = renderHandleList[j];
							RenderData rendererData = MagicaManager.Render.GetRendererData(num);
							DataChunk mappingChunk = MagicaManager.VMesh.RegisterMappingMesh(TeamId, virtualMeshContainer3, rendererData.renderDataWorkIndex);
							shareVirtualMesh.result.SetSuccess();
							RenderMeshInfo item = new RenderMeshInfo
							{
								renderHandle = num,
								renderMeshContainer = virtualMeshContainer3,
								mappingChunk = mappingChunk,
								renderDataWorkIndex = rendererData.renderDataWorkIndex
							};
							renderMeshInfoList.Add(item);
						}
					}
					UpdateUse();
				}
				catch
				{
					throw;
				}
				finally
				{
				}
				result.SetSuccess();
				SetState(6, sw: true);
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.PreBuild_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.PreBuild_Exception);
			}
			finally
			{
				list.ForEach(delegate(VirtualMeshContainer x)
				{
					x?.Dispose();
				});
				list.Clear();
				virtualMeshContainer?.Dispose();
				if (result.IsFaild())
				{
					Develop.LogError((object)("Cloth Pre-Build construction failure! [" + cloth.name + "] : " + result.GetResultString()));
				}
			}
			return result.IsSuccess();
		}

		internal int GetColliderIndex(ColliderComponent col)
		{
			return colliderList.IndexOf(col);
		}

		internal void UpdateCullingAnimatorAndRenderers()
		{
			CullingSettings cullingSettings = cloth.SerializeData.cullingSettings;
			if (cullingSettings.cameraCullingMode == CullingSettings.CameraCullingMode.AnimatorLinkage || cullingSettings.cameraCullingMethod == CullingSettings.CameraCullingMethod.AutomaticRenderer || cloth.SerializeData.updateMode == ClothUpdateMode.AnimatorLinkage)
			{
				interlockingAnimator = cloth.GetComponentInParent<Animator>();
			}
			if (cullingSettings.cameraCullingMethod == CullingSettings.CameraCullingMethod.AutomaticRenderer && (bool)interlockingAnimator)
			{
				interlockingAnimatorRenderers.Clear();
				interlockingAnimator.GetComponentsInChildren(interlockingAnimatorRenderers);
			}
		}

		internal void UpdateRendererUse()
		{
			renderHandleList.ForEach(delegate(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).UpdateUse(this, 0);
			});
		}

		public BitField32 GetStateFlag()
		{
			return stateFlag;
		}

		public bool IsState(int state)
		{
			return stateFlag.IsSet(state);
		}

		public void SetState(int state, bool sw)
		{
			stateFlag.SetBits(state, sw);
		}

		public bool IsValid()
		{
			return IsState(0);
		}

		public bool IsRunning()
		{
			return IsState(6);
		}

		public bool IsCameraCullingInvisible()
		{
			return IsState(8);
		}

		public bool IsCameraCullingKeep()
		{
			return IsState(9);
		}

		public bool IsDistanceCullingInvisible()
		{
			return IsState(13);
		}

		public bool IsSkipWriting()
		{
			return IsState(10);
		}

		public bool IsUpdateTangent()
		{
			return IsState(14);
		}

		public ClothProcess()
		{
			result = ResultCode.Empty;
		}

		public void Dispose()
		{
			lock (lockObject)
			{
				isDestory = true;
				SetState(0, sw: false);
				result.Clear();
				cts.Cancel();
			}
			DisposeInternal();
		}

		private void DisposeInternal()
		{
			lock (lockObject)
			{
				if (isDestoryInternal || isBuild)
				{
					return;
				}
				MagicaManager.Simulation?.ExitProxyMesh(this);
				MagicaManager.VMesh?.ExitProxyMesh(TeamId);
				MagicaManager.Collider?.Exit(this);
				MagicaManager.Cloth?.RemoveCloth(this);
				foreach (RenderMeshInfo renderMeshInfo in renderMeshInfoList)
				{
					renderMeshInfo?.renderMeshContainer?.Dispose();
				}
				renderMeshInfoList.Clear();
				renderMeshInfoList = null;
				foreach (int renderHandle in renderHandleList)
				{
					MagicaManager.Render?.RemoveRenderer(renderHandle);
				}
				renderHandleList.Clear();
				renderHandleList = null;
				boneClothSetupData?.Dispose();
				boneClothSetupData = null;
				ProxyMeshContainer?.Dispose();
				ProxyMeshContainer = null;
				colliderList.Clear();
				interlockingAnimator = null;
				interlockingAnimatorRenderers.Clear();
				MagicaManager.PreBuild?.UnregisterPreBuildData(cloth?.GetSerializeData2()?.preBuildData.GetSharePreBuildData());
				SyncTopCloth = null;
				int instanceID = cloth.GetInstanceID();
				MagicaManager.Team.comp2SuspendCounterMap.Remove(instanceID);
				MagicaManager.Team.comp2TeamIdMap.Remove(instanceID);
				MagicaManager.Team.comp2SyncPartnerCompMap.Remove(instanceID);
				MagicaManager.Team.comp2SyncTopCompMap.Remove(instanceID);
				isDestoryInternal = true;
			}
			MagicaManager.Team.RemoveMonitoringProcess(this);
		}

		internal void IncrementSuspendCounter()
		{
			TeamManager team = MagicaManager.Team;
			int instanceID = cloth.GetInstanceID();
			if (team.comp2SuspendCounterMap.TryGetValue(instanceID, out var item))
			{
				item++;
				team.comp2SuspendCounterMap[instanceID] = item;
			}
			else
			{
				team.comp2SuspendCounterMap.Add(instanceID, 1);
			}
		}

		internal void DecrementSuspendCounter()
		{
			TeamManager team = MagicaManager.Team;
			int instanceID = cloth.GetInstanceID();
			if (team.comp2SuspendCounterMap.TryGetValue(instanceID, out var item))
			{
				item--;
				if (item > 0)
				{
					team.comp2SuspendCounterMap[instanceID] = item;
				}
				else
				{
					team.comp2SuspendCounterMap.Remove(instanceID);
				}
			}
		}

		internal int GetSuspendCounter()
		{
			TeamManager team = MagicaManager.Team;
			int instanceID = cloth.GetInstanceID();
			if (team.comp2SuspendCounterMap.TryGetValue(instanceID, out var item))
			{
				return item;
			}
			return 0;
		}

		public RenderMeshInfo GetRenderMeshInfo(int index)
		{
			if (index >= 0 && index < renderMeshInfoList.Count)
			{
				return renderMeshInfoList[index];
			}
			return null;
		}

		internal void SyncParameters()
		{
			parameters = cloth.SerializeData.GetClothParameters();
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			cloth.SerializeData.GetUsedTransform(transformSet);
			cloth.serializeData2.GetUsedTransform(transformSet);
			clothTransformRecord?.GetUsedTransform(transformSet);
			boneClothSetupData?.GetUsedTransform(transformSet);
			renderHandleList.ForEach(delegate(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).GetUsedTransform(transformSet);
			});
			customSkinningBoneRecords.ForEach(delegate(TransformRecord rd)
			{
				rd.GetUsedTransform(transformSet);
			});
			normalAdjustmentTransformRecord?.GetUsedTransform(transformSet);
			if (transformSet.Contains(null))
			{
				transformSet.Remove(null);
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			cloth.SerializeData.ReplaceTransform(replaceDict);
			cloth.serializeData2.ReplaceTransform(replaceDict);
			clothTransformRecord?.ReplaceTransform(replaceDict);
			boneClothSetupData?.ReplaceTransform(replaceDict);
			renderHandleList.ForEach(delegate(int handle)
			{
				MagicaManager.Render.GetRendererData(handle).ReplaceTransform(replaceDict);
			});
			customSkinningBoneRecords.ForEach(delegate(TransformRecord rd)
			{
				rd.ReplaceTransform(replaceDict);
			});
			normalAdjustmentTransformRecord?.ReplaceTransform(replaceDict);
		}

		internal void SetSkipWriting(bool sw)
		{
			SetState(10, sw);
			MagicaManager.Team.skipWritingDirtyList.Add(this);
		}

		internal ClothUpdateMode GetClothUpdateMode()
		{
			switch (cloth.SerializeData.updateMode)
			{
			case ClothUpdateMode.Normal:
			case ClothUpdateMode.UnityPhysics:
			case ClothUpdateMode.Unscaled:
				return cloth.SerializeData.updateMode;
			case ClothUpdateMode.AnimatorLinkage:
				if ((bool)interlockingAnimator)
				{
					switch (interlockingAnimator.updateMode)
					{
					case AnimatorUpdateMode.Normal:
						return ClothUpdateMode.Normal;
					case AnimatorUpdateMode.Fixed:
						return ClothUpdateMode.UnityPhysics;
					case AnimatorUpdateMode.UnscaledTime:
						return ClothUpdateMode.Unscaled;
					}
				}
				return ClothUpdateMode.Normal;
			default:
				Develop.LogError((object)$"[{cloth.name}] Unknown Cloth Update Mode:{cloth.SerializeData.updateMode}");
				return ClothUpdateMode.Normal;
			}
		}

		public ResultCode GenerateStatusCheck()
		{
			ResultCode resultCode = default(ResultCode);
			Vector3 lossyScale = cloth.transform.lossyScale;
			if (Mathf.Approximately(lossyScale.x, 0f) || Mathf.Approximately(lossyScale.y, 0f) || Mathf.Approximately(lossyScale.z, 0f))
			{
				resultCode.SetError(Define.Result.Init_ScaleIsZero);
			}
			else if (lossyScale.x < 0f || lossyScale.y < 0f || lossyScale.z < 0f)
			{
				ClothSerializeData2 serializeData = cloth.GetSerializeData2();
				if (!serializeData.preBuildData.UsePreBuild())
				{
					ClothInitSerializeData initData = serializeData.initData;
					if (initData == null || !initData.HasData())
					{
						resultCode.SetError(Define.Result.Init_NegativeScale);
						goto IL_0157;
					}
				}
				if (((lossyScale.x < 0f) ? 1 : 0) + ((lossyScale.y < 0f) ? 1 : 0) + ((lossyScale.z < 0f) ? 1 : 0) != 1)
				{
					resultCode.SetError(Define.Result.Init_NegativeScale);
				}
			}
			else
			{
				float num = Mathf.Abs(1f - lossyScale.x / lossyScale.y);
				float num2 = Mathf.Abs(1f - lossyScale.x / lossyScale.z);
				if (num > 0.01f || num2 > 0.01f)
				{
					resultCode.SetWarning(Define.Result.Init_NonUniformScale);
				}
			}
			goto IL_0157;
			IL_0157:
			return resultCode;
		}

		internal bool GenerateInitialization()
		{
			result.SetProcess();
			if (!cloth.SerializeData.IsValid())
			{
				if (cloth.SerializeData.VerificationResult == Define.Result.Empty)
				{
					result.SetError(Define.Result.CreateCloth_InvalidSerializeData);
				}
				else
				{
					result.SetError(cloth.SerializeData.VerificationResult);
				}
				return false;
			}
			cloth.SerializeData.DataValidate();
			cloth.serializeData2.DataValidate();
			Init();
			if (result.IsError())
			{
				return false;
			}
			return true;
		}

		internal bool GenerateBoneClothSelection()
		{
			RenderSetupData renderSetupData = boneClothSetupData;
			int skinBoneCount = renderSetupData.skinBoneCount;
			SelectionData selectionData = new SelectionData(skinBoneCount);
			for (int i = 0; i < skinBoneCount; i++)
			{
				float3 float5 = math.transform(renderSetupData.initRenderWorldtoLocal, renderSetupData.transformPositions[i]);
				selectionData.positions[i] = float5;
				selectionData.attributes[i] = VertexAttribute.Move;
			}
			float num = 0f;
			for (int j = 0; j < skinBoneCount; j++)
			{
				int parentTransformIndex = renderSetupData.GetParentTransformIndex(j, centerExcluded: true);
				if (parentTransformIndex >= 0)
				{
					float y = math.distance(selectionData.positions[j], selectionData.positions[parentTransformIndex]);
					num = math.max(num, y);
				}
			}
			selectionData.maxConnectionDistance = num;
			foreach (Transform rootBone in cloth.SerializeData.rootBones)
			{
				if ((bool)rootBone)
				{
					int transformIndexFromId = renderSetupData.GetTransformIndexFromId(rootBone.GetInstanceID());
					selectionData.attributes[transformIndexFromId] = VertexAttribute.Fixed;
				}
			}
			selectionData.userEdit = true;
			cloth.GetSerializeData2().selectionData = selectionData;
			return true;
		}
	}
}
