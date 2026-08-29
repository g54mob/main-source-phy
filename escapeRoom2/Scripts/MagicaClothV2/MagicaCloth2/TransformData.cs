using System;
using System.Collections.Generic;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	public class TransformData : IDisposable
	{
		[BurstCompile]
		private struct RestoreTransformJob : IJobParallelForTransform
		{
			public int count;

			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			[ReadOnly]
			public NativeArray<float3> localPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> localRotationArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (index < count && transform.isValid)
				{
					transform.localPosition = localPositionArray[index];
					transform.localRotation = localRotationArray[index];
				}
			}
		}

		[BurstCompile]
		private struct ReadTransformJob : IJobParallelForTransform
		{
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			[WriteOnly]
			public NativeArray<float3> positionArray;

			[WriteOnly]
			public NativeArray<quaternion> rotationArray;

			[WriteOnly]
			public NativeArray<float3> scaleList;

			[WriteOnly]
			public NativeArray<float3> localPositionArray;

			[WriteOnly]
			public NativeArray<quaternion> localRotationArray;

			[WriteOnly]
			public NativeArray<quaternion> inverseRotationArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (transform.isValid)
				{
					Vector3 position = transform.position;
					Quaternion rotation = transform.rotation;
					float4x4 b = transform.localToWorldMatrix;
					positionArray[index] = position;
					rotationArray[index] = rotation;
					localPositionArray[index] = transform.localPosition;
					localRotationArray[index] = transform.localRotation;
					float4x4 float4x5 = math.mul(new float4x4(math.inverse(rotation), float3.zero), b);
					float3 value = new float3(float4x5.c0.x, float4x5.c1.y, float4x5.c2.z);
					scaleList[index] = value;
					inverseRotationArray[index] = math.inverse(rotation);
				}
			}
		}

		[Serializable]
		public class ShareSerializationData
		{
			public ExSimpleNativeArray<ExBitFlag8>.SerializationData flagArray;

			public ExSimpleNativeArray<float3>.SerializationData initLocalPositionArray;

			public ExSimpleNativeArray<quaternion>.SerializationData initLocalRotationArray;
		}

		[Serializable]
		public class UniqueSerializationData : ITransform
		{
			public Transform[] transformArray;

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
					if ((bool)transform)
					{
						int instanceID = transform.GetInstanceID();
						if (instanceID != 0 && replaceDict.ContainsKey(instanceID))
						{
							transformArray[i] = replaceDict[instanceID];
						}
					}
				}
			}
		}

		internal List<Transform> transformList;

		internal ExSimpleNativeArray<ExBitFlag8> flagArray;

		internal ExSimpleNativeArray<float3> initLocalPositionArray;

		internal ExSimpleNativeArray<quaternion> initLocalRotationArray;

		internal ExSimpleNativeArray<float3> positionArray;

		internal ExSimpleNativeArray<quaternion> rotationArray;

		internal ExSimpleNativeArray<quaternion> inverseRotationArray;

		internal ExSimpleNativeArray<float3> scaleArray;

		internal ExSimpleNativeArray<float3> localPositionArray;

		internal ExSimpleNativeArray<quaternion> localRotationArray;

		internal ExSimpleNativeArray<int> idArray;

		internal ExSimpleNativeArray<int> parentIdArray;

		internal List<int> rootIdList;

		private bool isDirty;

		internal TransformAccessArray transformAccessArray;

		private Queue<int> emptyStack;

		public int Count => transformList.Count;

		public int RootCount => rootIdList?.Count ?? 0;

		public bool IsDirty => isDirty;

		public bool IsEmpty => transformList == null;

		public TransformData()
		{
		}

		public TransformData(int capacity)
		{
			Init(capacity);
		}

		public void Init(int capacity)
		{
			transformList = new List<Transform>(capacity);
			idArray = new ExSimpleNativeArray<int>(capacity, areaOnly: true);
			parentIdArray = new ExSimpleNativeArray<int>(capacity, areaOnly: true);
			flagArray = new ExSimpleNativeArray<ExBitFlag8>(capacity, areaOnly: true);
			initLocalPositionArray = new ExSimpleNativeArray<float3>(capacity, areaOnly: true);
			initLocalRotationArray = new ExSimpleNativeArray<quaternion>(capacity, areaOnly: true);
			positionArray = new ExSimpleNativeArray<float3>(capacity, areaOnly: true);
			rotationArray = new ExSimpleNativeArray<quaternion>(capacity, areaOnly: true);
			scaleArray = new ExSimpleNativeArray<float3>(capacity, areaOnly: true);
			localPositionArray = new ExSimpleNativeArray<float3>(capacity, areaOnly: true);
			localRotationArray = new ExSimpleNativeArray<quaternion>(capacity, areaOnly: true);
			inverseRotationArray = new ExSimpleNativeArray<quaternion>(capacity, areaOnly: true);
			emptyStack = new Queue<int>(capacity);
			isDirty = true;
		}

		public void Dispose()
		{
			transformList?.Clear();
			idArray?.Dispose();
			parentIdArray?.Dispose();
			flagArray?.Dispose();
			initLocalPositionArray?.Dispose();
			initLocalRotationArray?.Dispose();
			positionArray?.Dispose();
			rotationArray?.Dispose();
			scaleArray?.Dispose();
			localPositionArray?.Dispose();
			localRotationArray?.Dispose();
			inverseRotationArray?.Dispose();
			emptyStack?.Clear();
			if (transformAccessArray.isCreated)
			{
				transformAccessArray.Dispose();
			}
		}

		public int AddTransform(Transform t, int tid = 0, int pid = 0, byte flag = 1, bool checkDuplicate = true)
		{
			int num;
			if (checkDuplicate)
			{
				num = ReferenceIndexOf(transformList, t);
				if (num >= 0)
				{
					return num;
				}
			}
			if (emptyStack.Count > 0)
			{
				num = emptyStack.Dequeue();
				transformList[num] = t;
				if (tid == 0)
				{
					idArray[num] = t.GetInstanceID();
					parentIdArray[num] = t.parent?.GetInstanceID() ?? 0;
					initLocalPositionArray[num] = t.localPosition;
					initLocalRotationArray[num] = t.localRotation;
					positionArray[num] = t.position;
					rotationArray[num] = t.rotation;
					scaleArray[num] = t.lossyScale;
					localPositionArray[num] = t.localPosition;
					localRotationArray[num] = t.localRotation;
					inverseRotationArray[num] = Quaternion.Inverse(t.rotation);
					flagArray[num] = new ExBitFlag8(flag);
				}
				else
				{
					idArray[num] = tid;
					parentIdArray[num] = pid;
					initLocalPositionArray[num] = 0;
					initLocalRotationArray[num] = quaternion.identity;
					positionArray[num] = 0;
					rotationArray[num] = quaternion.identity;
					scaleArray[num] = 1;
					localPositionArray[num] = 0;
					localRotationArray[num] = quaternion.identity;
					inverseRotationArray[num] = quaternion.identity;
					flagArray[num] = new ExBitFlag8(flag);
				}
			}
			else
			{
				num = Count;
				transformList.Add(t);
				if (tid == 0)
				{
					idArray.Add(t.GetInstanceID());
					parentIdArray.Add(t.parent?.GetInstanceID() ?? 0);
					initLocalPositionArray.Add(t.localPosition);
					initLocalRotationArray.Add(t.localRotation);
					positionArray.Add(t.position);
					rotationArray.Add(t.rotation);
					scaleArray.Add(t.lossyScale);
					localPositionArray.Add(t.localPosition);
					localRotationArray.Add(t.localRotation);
					inverseRotationArray.Add(Quaternion.Inverse(t.rotation));
					flagArray.Add(new ExBitFlag8(flag));
				}
				else
				{
					idArray.Add(tid);
					parentIdArray.Add(pid);
					initLocalPositionArray.Add(0);
					initLocalRotationArray.Add(quaternion.identity);
					positionArray.Add(0);
					rotationArray.Add(quaternion.identity);
					scaleArray.Add(1);
					localPositionArray.Add(0);
					localRotationArray.Add(quaternion.identity);
					inverseRotationArray.Add(quaternion.identity);
					flagArray.Add(new ExBitFlag8(flag));
				}
			}
			isDirty = true;
			return num;
		}

		public int AddTransform(TransformRecord record, int pid = 0, byte flag = 1, bool checkDuplicate = true)
		{
			int num;
			if (checkDuplicate)
			{
				num = ReferenceIndexOf(transformList, record.transform);
				if (num >= 0)
				{
					return num;
				}
			}
			if (emptyStack.Count > 0)
			{
				num = emptyStack.Dequeue();
				transformList[num] = record.transform;
				idArray[num] = record.id;
				parentIdArray[num] = pid;
				initLocalPositionArray[num] = record.localPosition;
				initLocalRotationArray[num] = record.localRotation;
				positionArray[num] = record.position;
				rotationArray[num] = record.rotation;
				scaleArray[num] = record.scale;
				localPositionArray[num] = record.localPosition;
				localRotationArray[num] = record.localRotation;
				inverseRotationArray[num] = Quaternion.Inverse(record.rotation);
				flagArray[num] = new ExBitFlag8(flag);
			}
			else
			{
				num = Count;
				transformList.Add(record.transform);
				idArray.Add(record.id);
				parentIdArray.Add(pid);
				initLocalPositionArray.Add(record.localPosition);
				initLocalRotationArray.Add(record.localRotation);
				positionArray.Add(record.position);
				rotationArray.Add(record.rotation);
				scaleArray.Add(record.scale);
				localPositionArray.Add(record.localPosition);
				localRotationArray.Add(record.localRotation);
				inverseRotationArray.Add(Quaternion.Inverse(record.rotation));
				flagArray.Add(new ExBitFlag8(flag));
			}
			isDirty = true;
			return num;
		}

		public int AddTransform(TransformData srcData, int srcIndex, bool checkDuplicate = true)
		{
			Transform transform = srcData.transformList[srcIndex];
			int num;
			if (checkDuplicate)
			{
				num = ReferenceIndexOf(transformList, transform);
				if (num >= 0)
				{
					return num;
				}
			}
			int num2 = srcData.idArray[srcIndex];
			int num3 = srcData.parentIdArray[srcIndex];
			float3 float5 = srcData.initLocalPositionArray[srcIndex];
			quaternion quaternion2 = srcData.initLocalRotationArray[srcIndex];
			float3 float6 = srcData.positionArray[srcIndex];
			quaternion quaternion3 = srcData.rotationArray[srcIndex];
			float3 float7 = srcData.scaleArray[srcIndex];
			float3 float8 = srcData.localPositionArray[srcIndex];
			quaternion quaternion4 = srcData.localRotationArray[srcIndex];
			quaternion quaternion5 = srcData.inverseRotationArray[srcIndex];
			ExBitFlag8 exBitFlag = srcData.flagArray[srcIndex];
			if (emptyStack.Count > 0)
			{
				num = emptyStack.Dequeue();
				transformList[num] = transform;
				idArray[num] = num2;
				parentIdArray[num] = num3;
				initLocalPositionArray[num] = float5;
				initLocalRotationArray[num] = quaternion2;
				positionArray[num] = float6;
				rotationArray[num] = quaternion3;
				scaleArray[num] = float7;
				localPositionArray[num] = float8;
				localRotationArray[num] = quaternion4;
				inverseRotationArray[num] = quaternion5;
				flagArray[num] = exBitFlag;
			}
			else
			{
				num = Count;
				transformList.Add(transform);
				idArray.Add(num2);
				parentIdArray.Add(num3);
				initLocalPositionArray.Add(float5);
				initLocalRotationArray.Add(quaternion2);
				positionArray.Add(float6);
				rotationArray.Add(quaternion3);
				scaleArray.Add(float7);
				localPositionArray.Add(float8);
				localRotationArray.Add(quaternion4);
				inverseRotationArray.Add(quaternion5);
				flagArray.Add(exBitFlag);
			}
			isDirty = true;
			return num;
		}

		public int[] AddTransformRange(List<Transform> tlist, List<int> idList, List<int> pidList, int copyCount = 0)
		{
			int num = ((copyCount > 0) ? copyCount : tlist.Count);
			int count = Count;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				transformList.Add(tlist[i]);
				idArray.Add(idList[i]);
				parentIdArray.Add(pidList[i]);
				array[i] = count + i;
			}
			flagArray.AddRange(num, new ExBitFlag8(1));
			initLocalPositionArray.AddRange(num);
			initLocalRotationArray.AddRange(num);
			positionArray.AddRange(num);
			rotationArray.AddRange(num);
			scaleArray.AddRange(num);
			localPositionArray.AddRange(num);
			localRotationArray.AddRange(num);
			inverseRotationArray.AddRange(num);
			isDirty = true;
			return array;
		}

		public int[] AddTransformRange(TransformData stdata, int copyCount = 0)
		{
			return AddTransformRange(stdata.transformList, new List<int>(stdata.idArray.ToArray()), new List<int>(stdata.parentIdArray.ToArray()), copyCount);
		}

		public int[] AddTransformRange(List<Transform> tlist, List<int> idList, List<int> pidList, List<int> rootIds, NativeArray<float3> localPositions, NativeArray<quaternion> localRotations, NativeArray<float3> positions, NativeArray<quaternion> rotations, NativeArray<float3> scales, NativeArray<quaternion> inverseRotations)
		{
			int count = tlist.Count;
			int count2 = Count;
			int[] array = new int[count];
			transformList.AddRange(tlist);
			for (int i = 0; i < count; i++)
			{
				idArray.Add(idList[i]);
				parentIdArray.Add(pidList[i]);
				array[i] = count2 + i;
			}
			if (rootIds != null && rootIds.Count > 0)
			{
				if (rootIdList == null)
				{
					rootIdList = new List<int>(rootIds);
				}
				else
				{
					rootIdList.AddRange(rootIds);
				}
			}
			flagArray.AddRange(count, new ExBitFlag8(1));
			initLocalPositionArray.AddRange(localPositions);
			initLocalRotationArray.AddRange(localRotations);
			positionArray.AddRange(positions);
			rotationArray.AddRange(rotations);
			scaleArray.AddRange(scales);
			localPositionArray.AddRange(localPositions);
			localRotationArray.AddRange(localRotations);
			inverseRotationArray.AddRange(inverseRotations);
			isDirty = true;
			return array;
		}

		public void RemoveTransformIndex(int index)
		{
			transformList[index] = null;
			flagArray[index] = default(ExBitFlag8);
			emptyStack.Enqueue(index);
		}

		public int ReplaceTransform(int index, Transform t, int tid = 0, int pid = 0, byte flag = 1)
		{
			transformList[index] = t;
			flagArray[index] = new ExBitFlag8(flag);
			if (tid == 0)
			{
				idArray[index] = t.GetInstanceID();
				parentIdArray[index] = t.parent?.GetInstanceID() ?? 0;
				initLocalPositionArray[index] = t.localPosition;
				initLocalRotationArray[index] = t.localRotation;
				positionArray[index] = t.position;
				rotationArray[index] = t.rotation;
				scaleArray[index] = t.lossyScale;
				localPositionArray[index] = t.localPosition;
				localRotationArray[index] = t.localRotation;
			}
			else
			{
				idArray[index] = tid;
				parentIdArray[index] = pid;
				initLocalPositionArray[index] = 0;
				initLocalRotationArray[index] = quaternion.identity;
				positionArray[index] = 0;
				rotationArray[index] = quaternion.identity;
				scaleArray[index] = 1;
				localPositionArray[index] = 0;
				localRotationArray[index] = quaternion.identity;
			}
			isDirty = true;
			return index;
		}

		private int ReferenceIndexOf<T>(List<T> list, T item) where T : class
		{
			if (list == null)
			{
				return -1;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		public void UpdateWorkData()
		{
			if (isDirty)
			{
				if (transformAccessArray.isCreated)
				{
					transformAccessArray.Dispose();
				}
				transformAccessArray = new TransformAccessArray(transformList.ToArray());
				isDirty = false;
			}
		}

		public JobHandle RestoreTransform(int count, JobHandle jobHandle = default(JobHandle))
		{
			UpdateWorkData();
			jobHandle = new RestoreTransformJob
			{
				count = count,
				flagList = flagArray.GetNativeArray(),
				localPositionArray = initLocalPositionArray.GetNativeArray(),
				localRotationArray = initLocalRotationArray.GetNativeArray()
			}.Schedule(transformAccessArray, jobHandle);
			return jobHandle;
		}

		public JobHandle ReadTransform(JobHandle jobHandle = default(JobHandle))
		{
			UpdateWorkData();
			jobHandle = new ReadTransformJob
			{
				flagList = flagArray.GetNativeArray(),
				positionArray = positionArray.GetNativeArray(),
				rotationArray = rotationArray.GetNativeArray(),
				scaleList = scaleArray.GetNativeArray(),
				localPositionArray = localPositionArray.GetNativeArray(),
				localRotationArray = localRotationArray.GetNativeArray(),
				inverseRotationArray = inverseRotationArray.GetNativeArray()
			}.ScheduleReadOnly(transformAccessArray, 16, jobHandle);
			return jobHandle;
		}

		public void ReadTransformRun()
		{
			UpdateWorkData();
			new ReadTransformJob
			{
				flagList = flagArray.GetNativeArray(),
				positionArray = positionArray.GetNativeArray(),
				rotationArray = rotationArray.GetNativeArray(),
				scaleList = scaleArray.GetNativeArray(),
				localPositionArray = localPositionArray.GetNativeArray(),
				localRotationArray = localRotationArray.GetNativeArray(),
				inverseRotationArray = inverseRotationArray.GetNativeArray()
			}.RunReadOnly(transformAccessArray);
		}

		public void OrganizeReductionTransform(VirtualMesh vmesh, ReductionWorkData workData)
		{
			List<int> list = new List<int>(workData.newSkinBoneCount.Value + 2);
			foreach (KeyValue<int, int> item in workData.useSkinBoneMap)
			{
				list.Add(vmesh.skinBoneTransformIndices[item.Key]);
			}
			int count = list.Count;
			list.Add(vmesh.skinRootIndex);
			int count2 = list.Count;
			list.Add(vmesh.centerTransformIndex);
			int count3 = list.Count;
			List<Transform> list2 = new List<Transform>(count3);
			ExSimpleNativeArray<int> exSimpleNativeArray = new ExSimpleNativeArray<int>(count3);
			ExSimpleNativeArray<int> exSimpleNativeArray2 = new ExSimpleNativeArray<int>(count3);
			ExSimpleNativeArray<ExBitFlag8> exSimpleNativeArray3 = new ExSimpleNativeArray<ExBitFlag8>(count3);
			ExSimpleNativeArray<float3> exSimpleNativeArray4 = new ExSimpleNativeArray<float3>(count3);
			ExSimpleNativeArray<quaternion> exSimpleNativeArray5 = new ExSimpleNativeArray<quaternion>(count3);
			ExSimpleNativeArray<float3> exSimpleNativeArray6 = new ExSimpleNativeArray<float3>(count3);
			ExSimpleNativeArray<quaternion> exSimpleNativeArray7 = new ExSimpleNativeArray<quaternion>(count3);
			ExSimpleNativeArray<float3> exSimpleNativeArray8 = new ExSimpleNativeArray<float3>(count3);
			for (int i = 0; i < count3; i++)
			{
				int index = list[i];
				list2.Add(transformList[index]);
				exSimpleNativeArray[i] = idArray[index];
				exSimpleNativeArray2[i] = parentIdArray[index];
				exSimpleNativeArray3[i] = flagArray[index];
				exSimpleNativeArray4[i] = initLocalPositionArray[index];
				exSimpleNativeArray5[i] = initLocalRotationArray[index];
				exSimpleNativeArray6[i] = positionArray[index];
				exSimpleNativeArray7[i] = rotationArray[index];
				exSimpleNativeArray8[i] = scaleArray[index];
			}
			transformList.Clear();
			idArray.Dispose();
			parentIdArray.Dispose();
			flagArray.Dispose();
			initLocalPositionArray.Dispose();
			initLocalRotationArray.Dispose();
			positionArray.Dispose();
			rotationArray.Dispose();
			scaleArray.Dispose();
			transformList = list2;
			idArray = exSimpleNativeArray;
			parentIdArray = exSimpleNativeArray2;
			flagArray = exSimpleNativeArray3;
			initLocalPositionArray = exSimpleNativeArray4;
			initLocalRotationArray = exSimpleNativeArray5;
			positionArray = exSimpleNativeArray6;
			rotationArray = exSimpleNativeArray7;
			scaleArray = exSimpleNativeArray8;
			emptyStack.Clear();
			vmesh.centerTransformIndex = count2;
			vmesh.skinRootIndex = count;
			isDirty = true;
		}

		public Transform GetTransformFromIndex(int index)
		{
			return transformList[index];
		}

		public int GetTransformIndexFormId(int id)
		{
			NativeArray<int> nativeArray = idArray.GetNativeArray();
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				if (nativeArray[i] == id)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetTransformIdFromIndex(int index)
		{
			return idArray[index];
		}

		public int GetParentIdFromIndex(int index)
		{
			return parentIdArray[index];
		}

		public float4x4 GetLocalToWorldMatrix(int index)
		{
			float3 obj = positionArray[index];
			quaternion quaternion2 = rotationArray[index];
			return Matrix4x4.TRS(s: scaleArray[index], pos: obj, q: quaternion2);
		}

		public float4x4 GetWorldToLocalMatrix(int index)
		{
			return math.inverse(GetLocalToWorldMatrix(index));
		}

		public override string ToString()
		{
			int num = transformList?.Count ?? 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("==== TransformData ====");
			stringBuilder.AppendLine($"isDirty:{isDirty}");
			stringBuilder.AppendLine($"transformList:{num}");
			stringBuilder.AppendLine($"flagArray:{flagArray.Length}");
			return stringBuilder.ToString();
		}

		public ShareSerializationData ShareSerialize()
		{
			ShareSerializationData shareSerializationData = new ShareSerializationData();
			try
			{
				shareSerializationData.flagArray = flagArray.Serialize();
				shareSerializationData.initLocalPositionArray = initLocalPositionArray.Serialize();
				shareSerializationData.initLocalRotationArray = initLocalRotationArray.Serialize();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return shareSerializationData;
		}

		public static TransformData ShareDeserialize(ShareSerializationData sdata)
		{
			if (sdata == null)
			{
				return null;
			}
			TransformData transformData = new TransformData();
			try
			{
				transformData.flagArray = new ExSimpleNativeArray<ExBitFlag8>(sdata.flagArray);
				transformData.initLocalPositionArray = new ExSimpleNativeArray<float3>(sdata.initLocalPositionArray);
				transformData.initLocalRotationArray = new ExSimpleNativeArray<quaternion>(sdata.initLocalRotationArray);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return transformData;
		}

		public UniqueSerializationData UniqueSerialize()
		{
			UniqueSerializationData uniqueSerializationData = new UniqueSerializationData();
			try
			{
				uniqueSerializationData.transformArray = transformList.ToArray();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return uniqueSerializationData;
		}
	}
}
