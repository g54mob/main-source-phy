using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Rendering;

namespace MagicaCloth2
{
	public class RenderSetupData : IDisposable, ITransform
	{
		public enum SetupType
		{
			MeshCloth = 0,
			BoneCloth = 1,
			BoneSpring = 2
		}

		public enum BoneConnectionMode
		{
			Line = 0,
			AutomaticMesh = 1,
			SequentialLoopMesh = 2,
			SequentialNonLoopMesh = 3
		}

		[BurstCompile]
		private struct CalcInverseRotationJob : IJobParallelFor
		{
			[ReadOnly]
			public NativeArray<quaternion> rotations;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> inverseRotations;

			public void Execute(int index)
			{
				quaternion q = rotations[index];
				if (math.any(q.value))
				{
					quaternion value = math.inverse(q);
					inverseRotations[index] = value;
				}
			}
		}

		[BurstCompile]
		private struct ReadTransformJob : IJobParallelForTransform
		{
			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> positions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> rotations;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> scales;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<float3> localPositions;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> localRotations;

			[NativeDisableParallelForRestriction]
			[WriteOnly]
			public NativeArray<quaternion> inverseRotations;

			public void Execute(int index, TransformAccess transform)
			{
				if (transform.isValid)
				{
					Vector3 position = transform.position;
					Quaternion rotation = transform.rotation;
					float4x4 b = transform.localToWorldMatrix;
					positions[index] = position;
					rotations[index] = rotation;
					localPositions[index] = transform.localPosition;
					localRotations[index] = transform.localRotation;
					quaternion quaternion2 = math.inverse(rotation);
					float4x4 float4x5 = math.mul(new float4x4(quaternion2, float3.zero), b);
					float3 value = new float3(float4x5.c0.x, float4x5.c1.y, float4x5.c2.z);
					scales[index] = value;
					inverseRotations[index] = quaternion2;
				}
			}
		}

		[BurstCompile]
		private struct GetBoneWeightJos : IJob
		{
			public int vcnt;

			[ReadOnly]
			public NativeArray<byte> bonesPerVertexArray;

			[ReadOnly]
			public NativeArray<BoneWeight1> boneWeightArray;

			[WriteOnly]
			public NativeArray<BoneWeight> boneWeights;

			public void Execute()
			{
				int num = 0;
				for (int i = 0; i < vcnt; i++)
				{
					BoneWeight value = default(BoneWeight);
					byte b = bonesPerVertexArray[i];
					for (int j = 0; j < b; j++)
					{
						BoneWeight1 boneWeight = boneWeightArray[num];
						num++;
						switch (j)
						{
						case 0:
							value.weight0 = boneWeight.weight;
							value.boneIndex0 = boneWeight.boneIndex;
							break;
						case 1:
							value.weight1 = boneWeight.weight;
							value.boneIndex1 = boneWeight.boneIndex;
							break;
						case 2:
							value.weight2 = boneWeight.weight;
							value.boneIndex2 = boneWeight.boneIndex;
							break;
						case 3:
							value.weight3 = boneWeight.weight;
							value.boneIndex3 = boneWeight.boneIndex;
							break;
						}
					}
					boneWeights[i] = value;
				}
			}
		}

		[Serializable]
		public class ShareSerializationData
		{
			public ResultCode result;

			public string name;

			public SetupType setupType;

			public Mesh originalMesh;

			public int vertexCount;

			public bool hasSkinnedMesh;

			public bool hasBoneWeight;

			public int skinRootBoneIndex;

			public int skinBoneCount;

			public List<Matrix4x4> bindPoseList;

			public byte[] bonesPerVertexArray;

			public byte[] boneWeightArray;

			public Vector3[] localPositions;

			public Vector3[] localNormals;

			public Vector4[] localTangents;

			public BoneConnectionMode boneConnectionMode;

			public int renderTransformIndex;

			public bool HasTangent
			{
				get
				{
					Vector4[] array = localTangents;
					if (array == null)
					{
						return false;
					}
					return array.Length != 0;
				}
			}
		}

		[Serializable]
		public class UniqueSerializationData : ITransform
		{
			public ResultCode result;

			public Renderer renderer;

			public SkinnedMeshRenderer skinRenderer;

			public MeshFilter meshFilter;

			public Mesh originalMesh;

			public List<Transform> transformList;

			public void GetUsedTransform(HashSet<Transform> transformSet)
			{
				transformList?.ForEach(delegate(Transform x)
				{
					if ((bool)x)
					{
						transformSet.Add(x);
					}
				});
			}

			public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
			{
				if (transformList == null)
				{
					return;
				}
				for (int i = 0; i < transformList.Count; i++)
				{
					Transform transform = transformList[i];
					if ((bool)transform)
					{
						int instanceID = transform.GetInstanceID();
						if (instanceID != 0 && replaceDict.ContainsKey(instanceID))
						{
							transformList[i] = replaceDict[instanceID];
						}
					}
				}
			}
		}

		public ResultCode result;

		public string name = string.Empty;

		public bool isManaged;

		public SetupType setupType;

		public Renderer renderer;

		public SkinnedMeshRenderer skinRenderer;

		public MeshFilter meshFilter;

		public Mesh originalMesh;

		public int vertexCount;

		public bool hasSkinnedMesh;

		public bool hasBoneWeight;

		public Mesh.MeshDataArray meshDataArray;

		public int skinRootBoneIndex;

		public int skinBoneCount;

		public List<Matrix4x4> bindPoseList;

		public NativeArray<byte> bonesPerVertexArray;

		public NativeArray<BoneWeight1> boneWeightArray;

		public NativeArray<Vector3> localPositions;

		public NativeArray<Vector3> localNormals;

		public NativeArray<Vector4> localTangents;

		public List<int> rootTransformIdList;

		public BoneConnectionMode boneConnectionMode;

		public List<int> collisionBoneIndexList;

		public List<Transform> transformList;

		public List<int> transformIdList;

		public List<int> transformParentIdList;

		public List<FixedList512Bytes<int>> transformChildIdList;

		public NativeArray<float3> transformPositions;

		public NativeArray<quaternion> transformRotations;

		public NativeArray<float3> transformLocalPositions;

		public NativeArray<quaternion> transformLocalRotations;

		public NativeArray<float3> transformScales;

		public NativeArray<quaternion> transformInverseRotations;

		public int renderTransformIndex;

		public float4x4 initRenderLocalToWorld;

		public float4x4 initRenderWorldtoLocal;

		public quaternion initRenderRotation;

		public float3 initRenderScale;

		private static readonly ProfilerMarker readTransformProfiler = new ProfilerMarker("readTransform");

		public int TransformCount => transformList?.Count ?? 0;

		public bool HasMeshDataArray => meshDataArray.Length > 0;

		public bool HasLocalPositions => localPositions.IsCreated;

		public bool HasTangent
		{
			get
			{
				if (localTangents.IsCreated)
				{
					return localTangents.Length > 0;
				}
				return false;
			}
		}

		public bool IsSuccess()
		{
			return result.IsSuccess();
		}

		public bool IsFaild()
		{
			return result.IsFaild();
		}

		public RenderSetupData()
		{
		}

		public RenderSetupData(RenderSetupSerializeData referenceInitSetupData, Renderer ren)
		{
			result.Clear();
			setupType = SetupType.MeshCloth;
			if (ren == null)
			{
				Develop.LogWarning((object)"Renderer is null!");
				result.SetError(Define.Result.RenderSetup_InvalidSource);
				return;
			}
			bool num = referenceInitSetupData != null;
			name = ren.name;
			SkinnedMeshRenderer skinnedMeshRenderer = ren as SkinnedMeshRenderer;
			hasSkinnedMesh = (skinnedMeshRenderer ? true : false);
			hasBoneWeight = false;
			skinRenderer = skinnedMeshRenderer;
			Transform transform = ren.transform;
			if (num)
			{
				skinBoneCount = referenceInitSetupData.skinBoneCount;
				skinRootBoneIndex = referenceInitSetupData.skinRootBoneIndex;
				renderTransformIndex = referenceInitSetupData.renderTransformIndex;
				hasBoneWeight = referenceInitSetupData.hasBoneWeight;
				transformList = new List<Transform>(new Transform[referenceInitSetupData.transformCount]);
				int useTransformCount = referenceInitSetupData.useTransformCount;
				for (int i = 0; i < useTransformCount; i++)
				{
					int index = referenceInitSetupData.useTransformIndexArray[i];
					transformList[index] = referenceInitSetupData.transformArray[i];
				}
				if ((bool)skinnedMeshRenderer && (bool)referenceInitSetupData.originalMesh && skinnedMeshRenderer.sharedMesh != referenceInitSetupData.originalMesh && skinBoneCount > 0)
				{
					skinnedMeshRenderer.sharedMesh = referenceInitSetupData.originalMesh;
					Transform[] array = new Transform[skinBoneCount];
					transformList.CopyTo(0, array, 0, skinBoneCount);
					skinnedMeshRenderer.bones = array;
					skinnedMeshRenderer.rootBone = transformList[skinRootBoneIndex];
				}
			}
			else if ((bool)skinnedMeshRenderer)
			{
				Transform[] bones = skinnedMeshRenderer.bones;
				if (bones == null || bones.Length == 0)
				{
					skinBoneCount = 1;
					transformList = new List<Transform>(skinBoneCount);
					transformList.Add(transform);
					skinRootBoneIndex = 0;
					renderTransformIndex = 0;
				}
				else
				{
					skinBoneCount = bones.Length;
					transformList = new List<Transform>(skinBoneCount + 2);
					transformList.AddRange(bones);
					Transform item = (skinnedMeshRenderer.rootBone ? skinnedMeshRenderer.rootBone : transform);
					skinRootBoneIndex = transformList.Count;
					transformList.Add(item);
					renderTransformIndex = transformList.Count;
					transformList.Add(transform);
					hasBoneWeight = true;
				}
			}
			else
			{
				skinBoneCount = 1;
				transformList = new List<Transform>(skinBoneCount);
				transformList.Add(transform);
				skinRootBoneIndex = 0;
				renderTransformIndex = 0;
			}
			ReadTransformInformation(includeChilds: false, referenceInitSetupData);
			Mesh sharedMesh;
			if ((bool)skinnedMeshRenderer)
			{
				sharedMesh = skinnedMeshRenderer.sharedMesh;
				if (sharedMesh == null)
				{
					Develop.LogWarning((object)"SkinnedMeshRenderer.sharedMesh is null!");
					result.SetError(Define.Result.RenderSetup_NoMeshOnRenderer);
					return;
				}
				if (hasBoneWeight)
				{
					bindPoseList = new List<Matrix4x4>(sharedMesh.bindposes);
					using NativeArray<BoneWeight1> array2 = sharedMesh.GetAllBoneWeights();
					using NativeArray<byte> array3 = sharedMesh.GetBonesPerVertex();
					boneWeightArray = new NativeArray<BoneWeight1>(array2, Allocator.Persistent);
					bonesPerVertexArray = new NativeArray<byte>(array3, Allocator.Persistent);
				}
				else
				{
					bindPoseList = new List<Matrix4x4>(1);
					bindPoseList.Add(Matrix4x4.identity);
				}
			}
			else
			{
				MeshFilter component = ren.GetComponent<MeshFilter>();
				if (component == null)
				{
					result.SetError(Define.Result.RenderSetup_InvalidSource);
					return;
				}
				sharedMesh = component.sharedMesh;
				if (sharedMesh == null)
				{
					Develop.LogWarning((object)"MeshFilter.sharedMesh is null!");
					result.SetError(Define.Result.RenderSetup_NoMeshOnRenderer);
					return;
				}
				bindPoseList = new List<Matrix4x4>(1);
				bindPoseList.Add(Matrix4x4.identity);
				meshFilter = component;
			}
			if (!sharedMesh.isReadable)
			{
				result.SetError(Define.Result.RenderSetup_Unreadable);
				return;
			}
			if (sharedMesh.vertexCount > 65535)
			{
				result.SetError(Define.Result.RenderSetup_Over65535vertices);
				return;
			}
			meshDataArray = Mesh.AcquireReadOnlyMeshData(sharedMesh);
			renderer = ren;
			originalMesh = sharedMesh;
			vertexCount = sharedMesh.vertexCount;
			result.SetSuccess();
		}

		public RenderSetupData(RenderSetupSerializeData referenceInitSetupData, SetupType setType, Transform renderTransform, List<Transform> rootTransforms, List<Transform> collisionBones, BoneConnectionMode connectionMode = BoneConnectionMode.Line, string name = "(no name)")
		{
			result.Clear();
			try
			{
				bool flag = referenceInitSetupData != null;
				setupType = setType;
				boneConnectionMode = connectionMode;
				if (renderTransform == null)
				{
					result.SetError(Define.Result.RenderSetup_InvalidSource);
					return;
				}
				if (rootTransforms == null || rootTransforms.Count == 0)
				{
					result.SetError(Define.Result.RenderSetup_InvalidSource);
					return;
				}
				this.name = name;
				if (flag)
				{
					transformList = new List<Transform>(referenceInitSetupData.transformArray);
					skinBoneCount = referenceInitSetupData.skinBoneCount;
					renderTransformIndex = referenceInitSetupData.renderTransformIndex;
				}
				else
				{
					Dictionary<Transform, int> dictionary = new Dictionary<Transform, int>(256);
					transformList = new List<Transform>(256);
					Stack<Transform> stack = new Stack<Transform>(256);
					foreach (Transform rootTransform in rootTransforms)
					{
						stack.Push(rootTransform);
					}
					while (stack.Count > 0)
					{
						Transform transform = stack.Pop();
						if (!dictionary.ContainsKey(transform))
						{
							int count = transformList.Count;
							transformList.Add(transform);
							dictionary.Add(transform, count);
							int childCount = transform.childCount;
							for (int i = 0; i < childCount; i++)
							{
								stack.Push(transform.GetChild(i));
							}
						}
					}
					skinBoneCount = transformList.Count;
					renderTransformIndex = transformList.Count;
					transformList.Add(renderTransform);
				}
				rootTransformIdList = new List<int>(rootTransforms.Count);
				foreach (Transform rootTransform2 in rootTransforms)
				{
					rootTransformIdList.Add(rootTransform2.GetInstanceID());
				}
				if (collisionBones != null)
				{
					collisionBoneIndexList = new List<int>(collisionBones.Count);
					foreach (Transform collisionBone in collisionBones)
					{
						if ((bool)collisionBone)
						{
							int item = transformList.IndexOf(collisionBone);
							collisionBoneIndexList.Add(item);
						}
					}
				}
				ReadTransformInformation(includeChilds: true, referenceInitSetupData);
				result.SetSuccess();
			}
			catch (MagicaClothProcessingException)
			{
				if (!result.IsError())
				{
					result.SetError(Define.Result.RenderSetup_UnknownError);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				result.SetError(Define.Result.RenderSetup_Exception);
			}
		}

		private void ReadTransformInformation(bool includeChilds, RenderSetupSerializeData referenceInitSetupData)
		{
			int count = transformList.Count;
			bool num = referenceInitSetupData != null;
			transformPositions = new NativeArray<float3>(count, Allocator.Persistent);
			transformRotations = new NativeArray<quaternion>(count, Allocator.Persistent);
			transformLocalPositions = new NativeArray<float3>(count, Allocator.Persistent);
			transformLocalRotations = new NativeArray<quaternion>(count, Allocator.Persistent);
			transformScales = new NativeArray<float3>(count, Allocator.Persistent);
			transformInverseRotations = new NativeArray<quaternion>(count, Allocator.Persistent);
			if (num)
			{
				int useTransformCount = referenceInitSetupData.useTransformCount;
				if (useTransformCount == count)
				{
					NativeArray<float3>.Copy(referenceInitSetupData.transformPositions, 0, transformPositions, 0, useTransformCount);
					NativeArray<quaternion>.Copy(referenceInitSetupData.transformRotations, 0, transformRotations, 0, useTransformCount);
					NativeArray<float3>.Copy(referenceInitSetupData.transformLocalPositions, 0, transformLocalPositions, 0, useTransformCount);
					NativeArray<quaternion>.Copy(referenceInitSetupData.transformLocalRotations, 0, transformLocalRotations, 0, useTransformCount);
					NativeArray<float3>.Copy(referenceInitSetupData.transformScales, 0, transformScales, 0, useTransformCount);
				}
				else
				{
					for (int i = 0; i < useTransformCount; i++)
					{
						int index = referenceInitSetupData.useTransformIndexArray[i];
						transformPositions[index] = referenceInitSetupData.transformPositions[i];
						transformRotations[index] = referenceInitSetupData.transformRotations[i];
						transformLocalPositions[index] = referenceInitSetupData.transformLocalPositions[i];
						transformLocalRotations[index] = referenceInitSetupData.transformLocalRotations[i];
						transformScales[index] = referenceInitSetupData.transformScales[i];
					}
				}
				IJobParallelForExtensions.Run(new CalcInverseRotationJob
				{
					rotations = transformRotations,
					inverseRotations = transformInverseRotations
				}, count);
				initRenderLocalToWorld = referenceInitSetupData.initRenderLocalToWorld;
				initRenderWorldtoLocal = referenceInitSetupData.initRenderWorldtoLocal;
				initRenderRotation = referenceInitSetupData.initRenderRotation;
				initRenderScale = referenceInitSetupData.initRenderScale;
			}
			else
			{
				using TransformAccessArray transforms = new TransformAccessArray(transformList.ToArray());
				new ReadTransformJob
				{
					positions = transformPositions,
					rotations = transformRotations,
					scales = transformScales,
					localPositions = transformLocalPositions,
					localRotations = transformLocalRotations,
					inverseRotations = transformInverseRotations
				}.RunReadOnly(transforms);
				initRenderLocalToWorld = GetRendeerLocalToWorldMatrix();
				initRenderWorldtoLocal = math.inverse(initRenderLocalToWorld);
				initRenderRotation = transformRotations[renderTransformIndex];
				initRenderScale = transformScales[renderTransformIndex];
			}
			transformIdList = new List<int>(count);
			transformParentIdList = new List<int>(count);
			for (int j = 0; j < count; j++)
			{
				int item = 0;
				int item2 = 0;
				Transform transform = transformList[j];
				if ((bool)transform)
				{
					item = transform.GetInstanceID();
					if (includeChilds && (bool)transform.parent)
					{
						item2 = transform.parent.GetInstanceID();
					}
				}
				transformIdList.Add(item);
				transformParentIdList.Add(item2);
			}
			if (!includeChilds)
			{
				return;
			}
			transformChildIdList = new List<FixedList512Bytes<int>>(count);
			for (int k = 0; k < count; k++)
			{
				Transform transform2 = transformList[k];
				FixedList512Bytes<int> item3 = default(FixedList512Bytes<int>);
				if ((bool)transform2 && transform2.childCount > 0)
				{
					for (int l = 0; l < transform2.childCount; l++)
					{
						Transform child = transform2.GetChild(l);
						item3.Add(child.GetInstanceID());
					}
				}
				transformChildIdList.Add(item3);
			}
		}

		public void Dispose()
		{
			if (!isManaged)
			{
				NativeArrayExtensions.MC2DisposeSafe(ref bonesPerVertexArray);
				NativeArrayExtensions.MC2DisposeSafe(ref boneWeightArray);
				NativeArrayExtensions.MC2DisposeSafe(ref localPositions);
				NativeArrayExtensions.MC2DisposeSafe(ref localNormals);
				NativeArrayExtensions.MC2DisposeSafe(ref localTangents);
				NativeArrayExtensions.MC2DisposeSafe(ref transformPositions);
				NativeArrayExtensions.MC2DisposeSafe(ref transformRotations);
				NativeArrayExtensions.MC2DisposeSafe(ref transformLocalPositions);
				NativeArrayExtensions.MC2DisposeSafe(ref transformLocalRotations);
				NativeArrayExtensions.MC2DisposeSafe(ref transformScales);
				NativeArrayExtensions.MC2DisposeSafe(ref transformInverseRotations);
				if (setupType == SetupType.MeshCloth && meshDataArray.Length > 0)
				{
					meshDataArray.Dispose();
				}
			}
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			if (transformList == null)
			{
				return;
			}
			foreach (Transform transform in transformList)
			{
				if ((bool)transform)
				{
					transformSet.Add(transform);
				}
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (rootTransformIdList != null)
			{
				for (int i = 0; i < rootTransformIdList.Count; i++)
				{
					int key = rootTransformIdList[i];
					if (replaceDict.ContainsKey(key))
					{
						rootTransformIdList[i] = replaceDict[key].GetInstanceID();
					}
				}
			}
			for (int j = 0; j < TransformCount; j++)
			{
				int key2 = transformIdList[j];
				if (replaceDict.ContainsKey(key2))
				{
					Transform transform = replaceDict[key2];
					transformIdList[j] = transform.GetInstanceID();
					transformList[j] = transform;
				}
				int key3 = transformParentIdList[j];
				if (replaceDict.ContainsKey(key3))
				{
					Transform transform2 = replaceDict[key3];
					transformParentIdList[j] = transform2.GetInstanceID();
				}
				if (transformChildIdList == null)
				{
					continue;
				}
				FixedList512Bytes<int> value = transformChildIdList[j];
				for (int k = 0; k < value.Length; k++)
				{
					int key4 = value[k];
					if (replaceDict.ContainsKey(key4))
					{
						Transform transform3 = replaceDict[key4];
						value[k] = transform3.GetInstanceID();
					}
				}
				transformChildIdList[j] = value;
			}
		}

		public Transform GetRendeerTransform()
		{
			return transformList[renderTransformIndex];
		}

		public int GetRenderTransformId()
		{
			return transformIdList[renderTransformIndex];
		}

		public float4x4 GetRendeerLocalToWorldMatrix()
		{
			int index = renderTransformIndex;
			float3 translation = transformPositions[index];
			quaternion rotation = transformRotations[index];
			float3 scale = transformScales[index];
			return float4x4.TRS(translation, rotation, scale);
		}

		public Transform GetSkinRootTransform()
		{
			return transformList[skinRootBoneIndex];
		}

		public int GetSkinRootTransformId()
		{
			return transformIdList[skinRootBoneIndex];
		}

		public int GetTransformIndexFromId(int id)
		{
			return transformIdList.IndexOf(id);
		}

		public int GetParentTransformIndex(int index, bool centerExcluded)
		{
			int item = transformParentIdList[index];
			int num = transformIdList.IndexOf(item);
			if (centerExcluded && num == renderTransformIndex)
			{
				num = -1;
			}
			return num;
		}

		public void GetBoneWeightsRun(NativeArray<BoneWeight> weights)
		{
			new GetBoneWeightJos
			{
				vcnt = vertexCount,
				bonesPerVertexArray = bonesPerVertexArray,
				boneWeightArray = boneWeightArray,
				boneWeights = weights
			}.Run();
		}

		public ShareSerializationData ShareSerialize()
		{
			ShareSerializationData shareSerializationData = new ShareSerializationData();
			try
			{
				shareSerializationData.result = result;
				shareSerializationData.name = name;
				shareSerializationData.setupType = setupType;
				shareSerializationData.originalMesh = originalMesh;
				shareSerializationData.vertexCount = vertexCount;
				shareSerializationData.hasSkinnedMesh = hasSkinnedMesh;
				shareSerializationData.hasBoneWeight = hasBoneWeight;
				shareSerializationData.skinRootBoneIndex = skinRootBoneIndex;
				shareSerializationData.skinBoneCount = skinBoneCount;
				shareSerializationData.bindPoseList = new List<Matrix4x4>(bindPoseList);
				shareSerializationData.bonesPerVertexArray = NativeArrayExtensions.MC2ToRawBytes(ref bonesPerVertexArray);
				shareSerializationData.boneWeightArray = NativeArrayExtensions.MC2ToRawBytes(ref boneWeightArray);
				shareSerializationData.localPositions = originalMesh.vertices;
				shareSerializationData.localNormals = originalMesh.normals;
				if (originalMesh.HasVertexAttribute(UnityEngine.Rendering.VertexAttribute.Tangent))
				{
					shareSerializationData.localTangents = originalMesh.tangents;
				}
				shareSerializationData.boneConnectionMode = boneConnectionMode;
				shareSerializationData.renderTransformIndex = renderTransformIndex;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return shareSerializationData;
		}

		public static RenderSetupData ShareDeserialize(ShareSerializationData sdata)
		{
			RenderSetupData renderSetupData = new RenderSetupData();
			renderSetupData.isManaged = true;
			try
			{
				renderSetupData.name = sdata.name;
				renderSetupData.setupType = sdata.setupType;
				renderSetupData.originalMesh = sdata.originalMesh;
				renderSetupData.vertexCount = sdata.vertexCount;
				renderSetupData.hasSkinnedMesh = sdata.hasSkinnedMesh;
				renderSetupData.hasBoneWeight = sdata.hasBoneWeight;
				renderSetupData.skinRootBoneIndex = sdata.skinRootBoneIndex;
				renderSetupData.skinBoneCount = sdata.skinBoneCount;
				renderSetupData.bindPoseList = new List<Matrix4x4>(sdata.bindPoseList);
				renderSetupData.bonesPerVertexArray = NativeArrayExtensions.MC2FromRawBytes<byte>(sdata.bonesPerVertexArray);
				renderSetupData.boneWeightArray = NativeArrayExtensions.MC2FromRawBytes<BoneWeight1>(sdata.boneWeightArray);
				renderSetupData.localPositions = new NativeArray<Vector3>(sdata.localPositions, Allocator.Persistent);
				renderSetupData.localNormals = new NativeArray<Vector3>(sdata.localNormals, Allocator.Persistent);
				if (sdata.HasTangent)
				{
					renderSetupData.localTangents = new NativeArray<Vector4>(sdata.localTangents, Allocator.Persistent);
				}
				renderSetupData.boneConnectionMode = sdata.boneConnectionMode;
				renderSetupData.renderTransformIndex = sdata.renderTransformIndex;
				renderSetupData.result.SetSuccess();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				renderSetupData.result.SetError(Define.Result.PreBuild_InvalidRenderSetupData);
			}
			return renderSetupData;
		}

		public UniqueSerializationData UniqueSerialize()
		{
			UniqueSerializationData uniqueSerializationData = new UniqueSerializationData();
			try
			{
				uniqueSerializationData.result = result;
				uniqueSerializationData.renderer = renderer;
				uniqueSerializationData.skinRenderer = skinRenderer;
				uniqueSerializationData.meshFilter = meshFilter;
				uniqueSerializationData.originalMesh = originalMesh;
				uniqueSerializationData.transformList = new List<Transform>(transformList);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return uniqueSerializationData;
		}
	}
}
