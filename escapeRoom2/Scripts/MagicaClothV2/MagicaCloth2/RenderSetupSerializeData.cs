using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class RenderSetupSerializeData : ITransform
	{
		[BurstCompile]
		private struct CalcUseBoneArrayJob2 : IJob
		{
			public int boneCount;

			[ReadOnly]
			public NativeArray<BoneWeight1> boneWeightArray;

			public NativeList<int> useBoneIndexList;

			public void Execute()
			{
				NativeArray<byte> nativeArray = new NativeArray<byte>(boneCount, Allocator.Temp);
				int length = boneWeightArray.Length;
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					BoneWeight1 boneWeight = boneWeightArray[i];
					if (boneWeight.weight > 0f)
					{
						if (nativeArray[boneWeight.boneIndex] == 0)
						{
							num++;
						}
						nativeArray[boneWeight.boneIndex] = 1;
					}
				}
				for (int j = 0; j < boneCount; j++)
				{
					if (nativeArray[j] != 0)
					{
						useBoneIndexList.Add(in j);
					}
				}
				nativeArray.Dispose();
			}
		}

		public RenderSetupData.SetupType setupType;

		public int vertexCount;

		public bool hasSkinnedMesh;

		public bool hasBoneWeight;

		public int skinRootBoneIndex;

		public int renderTransformIndex;

		public int skinBoneCount;

		public int transformCount;

		public int useTransformCount;

		public int[] useTransformIndexArray;

		public Transform[] transformArray;

		public float3[] transformPositions;

		public quaternion[] transformRotations;

		public float3[] transformLocalPositions;

		public quaternion[] transformLocalRotations;

		public float3[] transformScales;

		public float4x4 initRenderLocalToWorld;

		public float4x4 initRenderWorldtoLocal;

		public quaternion initRenderRotation;

		public float3 initRenderScale;

		public Mesh originalMesh;

		public bool DataValidateMeshCloth(Renderer ren)
		{
			if (setupType != RenderSetupData.SetupType.MeshCloth)
			{
				return false;
			}
			if (ren == null)
			{
				return false;
			}
			if (ren is SkinnedMeshRenderer)
			{
				if (!hasSkinnedMesh)
				{
					return false;
				}
				if ((ren as SkinnedMeshRenderer).sharedMesh == null)
				{
					return false;
				}
			}
			else
			{
				if (hasSkinnedMesh)
				{
					return false;
				}
				MeshFilter component = ren.GetComponent<MeshFilter>();
				if (component == null)
				{
					return false;
				}
				if (component.sharedMesh == null)
				{
					return false;
				}
			}
			if (!DataValidateTransform())
			{
				return false;
			}
			return true;
		}

		public bool DataValidateBoneCloth(ClothSerializeData sdata, RenderSetupData.SetupType clothType)
		{
			if (setupType != clothType)
			{
				return false;
			}
			if (hasSkinnedMesh)
			{
				return false;
			}
			if (hasBoneWeight)
			{
				return false;
			}
			if (!DataValidateTransform())
			{
				return false;
			}
			return true;
		}

		public bool DataValidateTransform()
		{
			if (transformCount == 0)
			{
				return false;
			}
			if (useTransformCount == 0)
			{
				return false;
			}
			int num = useTransformCount;
			int[] array = useTransformIndexArray;
			if (num != ((array != null) ? array.Length : 0))
			{
				return false;
			}
			Transform[] array2 = transformArray;
			if (num != ((array2 != null) ? array2.Length : 0))
			{
				return false;
			}
			float3[] array3 = transformPositions;
			if (num != ((array3 != null) ? array3.Length : 0))
			{
				return false;
			}
			quaternion[] array4 = transformRotations;
			if (num != ((array4 != null) ? array4.Length : 0))
			{
				return false;
			}
			float3[] array5 = transformLocalPositions;
			if (num != ((array5 != null) ? array5.Length : 0))
			{
				return false;
			}
			quaternion[] array6 = transformLocalRotations;
			if (num != ((array6 != null) ? array6.Length : 0))
			{
				return false;
			}
			float3[] array7 = transformScales;
			if (num != ((array7 != null) ? array7.Length : 0))
			{
				return false;
			}
			return true;
		}

		public bool Serialize(RenderSetupData sd)
		{
			setupType = sd.setupType;
			vertexCount = sd.vertexCount;
			hasSkinnedMesh = sd.hasSkinnedMesh;
			hasBoneWeight = sd.hasBoneWeight;
			skinRootBoneIndex = sd.skinRootBoneIndex;
			renderTransformIndex = sd.renderTransformIndex;
			skinBoneCount = sd.skinBoneCount;
			transformCount = sd.TransformCount;
			useTransformCount = 0;
			originalMesh = null;
			if (sd.TransformCount > 0)
			{
				int num = sd.TransformCount;
				using NativeList<int> useBoneIndexList = new NativeList<int>(num, Allocator.TempJob);
				if (setupType == RenderSetupData.SetupType.MeshCloth)
				{
					if (hasBoneWeight)
					{
						new CalcUseBoneArrayJob2
						{
							boneCount = skinBoneCount,
							boneWeightArray = sd.boneWeightArray,
							useBoneIndexList = useBoneIndexList
						}.Run();
						if (skinRootBoneIndex >= skinBoneCount)
						{
							useBoneIndexList.Add(in skinRootBoneIndex);
						}
						useBoneIndexList.Add(in renderTransformIndex);
					}
					else
					{
						for (int i = 0; i < num; i++)
						{
							useBoneIndexList.Add(in i);
						}
					}
					if ((bool)sd.originalMesh && !sd.originalMesh.name.Contains("(Clone)"))
					{
						originalMesh = sd.originalMesh;
					}
				}
				else
				{
					for (int j = 0; j < num; j++)
					{
						useBoneIndexList.Add(in j);
					}
				}
				using NativeArray<int> nativeArray = useBoneIndexList.ToArray(Allocator.TempJob);
				useTransformIndexArray = nativeArray.ToArray();
				int num2 = (useTransformCount = useTransformIndexArray.Length);
				transformArray = new Transform[num2];
				transformPositions = new float3[num2];
				transformRotations = new quaternion[num2];
				transformLocalPositions = new float3[num2];
				transformLocalRotations = new quaternion[num2];
				transformScales = new float3[num2];
				for (int k = 0; k < num2; k++)
				{
					int index = useTransformIndexArray[k];
					transformArray[k] = sd.transformList[index];
					transformPositions[k] = sd.transformPositions[index];
					transformRotations[k] = sd.transformRotations[index];
					transformLocalPositions[k] = sd.transformLocalPositions[index];
					transformLocalRotations[k] = sd.transformLocalRotations[index];
					transformScales[k] = sd.transformScales[index];
				}
			}
			initRenderLocalToWorld = sd.initRenderLocalToWorld;
			initRenderWorldtoLocal = sd.initRenderWorldtoLocal;
			initRenderRotation = sd.initRenderRotation;
			initRenderScale = sd.initRenderScale;
			return true;
		}

		public int GetLocalHash()
		{
			int num = 0;
			num += (int)setupType * 100;
			num += vertexCount;
			num += (hasSkinnedMesh ? 1 : 0);
			num += (hasBoneWeight ? 1 : 0);
			num += skinRootBoneIndex;
			num += renderTransformIndex;
			num += skinBoneCount;
			num += transformCount;
			num += useTransformCount;
			int num2 = num;
			int[] array = useTransformIndexArray;
			num = num2 + ((array != null) ? array.Length : 0);
			int num3 = num;
			Transform[] array2 = transformArray;
			num = num3 + ((array2 != null) ? array2.Length : 0);
			int num4 = num;
			float3[] array3 = transformPositions;
			num = num4 + ((array3 != null) ? array3.Length : 0);
			int num5 = num;
			quaternion[] array4 = transformRotations;
			num = num5 + ((array4 != null) ? array4.Length : 0);
			int num6 = num;
			float3[] array5 = transformLocalPositions;
			num = num6 + ((array5 != null) ? array5.Length : 0);
			int num7 = num;
			quaternion[] array6 = transformLocalRotations;
			num = num7 + ((array6 != null) ? array6.Length : 0);
			int num8 = num;
			float3[] array7 = transformScales;
			num = num8 + ((array7 != null) ? array7.Length : 0);
			if (transformArray != null)
			{
				Transform[] array8 = transformArray;
				foreach (Transform transform in array8)
				{
					num += ((transform != null) ? (456 + transform.childCount * 789) : 0);
				}
			}
			if (originalMesh != null)
			{
				num += originalMesh.vertexCount;
			}
			return num;
		}

		public int GetGlobalHash()
		{
			int num = 0;
			if (transformArray != null)
			{
				Transform[] array = transformArray;
				foreach (Transform transform in array)
				{
					if ((bool)transform)
					{
						num += transform.localPosition.GetHashCode();
						num += transform.localRotation.GetHashCode();
						num += transform.localScale.GetHashCode();
					}
				}
			}
			return num + ((int3)math.round(initRenderScale * 1000f)).GetHashCode();
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			if (transformArray == null)
			{
				return;
			}
			Transform[] array = transformArray;
			foreach (Transform transform in array)
			{
				if ((bool)transform)
				{
					transformSet.Add(transform);
				}
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (transformArray == null)
			{
				return;
			}
			for (int i = 0; i < transformArray.Length; i++)
			{
				Transform transform = transformArray[i];
				if ((bool)transform && replaceDict.ContainsKey(transform.GetInstanceID()))
				{
					transformArray[i] = replaceDict[transform.GetInstanceID()];
				}
			}
		}
	}
}
