using System;
using System.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace MagicaCloth2
{
	public class TransformManager : IManager, IDisposable, IValid
	{
		[BurstCompile]
		private struct EnableTransformJob : IJob
		{
			public DataChunk chunk;

			public bool sw;

			public NativeArray<ExBitFlag8> flagList;

			public void Execute()
			{
				for (int i = 0; i < chunk.dataLength; i++)
				{
					int index = chunk.startIndex + i;
					ExBitFlag8 value = flagList[index];
					if (value.Value != 0)
					{
						value.SetFlag(16, sw);
						flagList[index] = value;
					}
				}
			}
		}

		[BurstCompile]
		private struct RestoreTransformJob : IJobParallelForTransform
		{
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			[ReadOnly]
			public NativeArray<float3> localPositionArray;

			[ReadOnly]
			public NativeArray<quaternion> localRotationArray;

			[ReadOnly]
			public NativeArray<short> teamIdArray;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = flagList[index];
				if (exBitFlag.IsSet(8))
				{
					int index2 = teamIdArray[index];
					TeamManager.TeamData teamData = teamDataArray[index2];
					if ((exBitFlag.IsSet(16) || teamData.flag.IsSet(20)) && (!teamData.IsCameraCullingInvisible || !teamData.IsCameraCullingKeep) && !teamData.IsDistanceCullingInvisible)
					{
						transform.localPosition = localPositionArray[index];
						transform.localRotation = localRotationArray[index];
					}
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
			public NativeArray<float4x4> localToWorldMatrixArray;

			[ReadOnly]
			public NativeArray<short> teamIdArray;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = flagList[index];
				if (exBitFlag.IsSet(16) && exBitFlag.IsSet(1))
				{
					int index2 = teamIdArray[index];
					if (!teamDataArray[index2].IsCullingInvisible)
					{
						Vector3 position = transform.position;
						Quaternion rotation = transform.rotation;
						float4x4 float4x5 = transform.localToWorldMatrix;
						positionArray[index] = position;
						rotationArray[index] = rotation;
						localPositionArray[index] = transform.localPosition;
						localRotationArray[index] = transform.localRotation;
						float4x4 float4x6 = math.mul(new float4x4(math.inverse(rotation), float3.zero), float4x5);
						float3 value = new float3(float4x6.c0.x, float4x6.c1.y, float4x6.c2.z);
						scaleList[index] = value;
						localToWorldMatrixArray[index] = float4x5;
					}
				}
			}
		}

		[BurstCompile]
		private struct WriteTransformJob : IJobParallelForTransform
		{
			[ReadOnly]
			public NativeArray<ExBitFlag8> flagList;

			[ReadOnly]
			public NativeArray<float3> worldPositions;

			[ReadOnly]
			public NativeArray<quaternion> worldRotations;

			[ReadOnly]
			public NativeArray<float3> localPositions;

			[ReadOnly]
			public NativeArray<quaternion> localRotations;

			[ReadOnly]
			public NativeArray<short> teamIdArray;

			[ReadOnly]
			public NativeArray<TeamManager.TeamData> teamDataArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (!transform.isValid)
				{
					return;
				}
				ExBitFlag8 exBitFlag = flagList[index];
				if (!exBitFlag.IsSet(16))
				{
					return;
				}
				int index2 = teamIdArray[index];
				TeamManager.TeamData teamData = teamDataArray[index2];
				if (teamData.IsCullingInvisible || teamData.flag.IsSet(14))
				{
					return;
				}
				if (exBitFlag.IsSet(2))
				{
					transform.rotation = worldRotations[index];
					if (teamData.IsSpring)
					{
						transform.position = worldPositions[index];
					}
				}
				else if (exBitFlag.IsSet(4))
				{
					transform.localPosition = localPositions[index];
					transform.localRotation = localRotations[index];
				}
			}
		}

		[BurstCompile]
		private struct ReadComponentTransformJob : IJobParallelForTransform
		{
			[WriteOnly]
			public NativeArray<float3> positionArray;

			public void Execute(int index, TransformAccess transform)
			{
				if (transform.isValid)
				{
					positionArray[index] = transform.position;
				}
			}
		}

		internal const byte Flag_Read = 1;

		internal const byte Flag_WorldRotWrite = 2;

		internal const byte Flag_LocalPosRotWrite = 4;

		internal const byte Flag_Restore = 8;

		internal const byte Flag_Enable = 16;

		internal ExNativeArray<ExBitFlag8> flagArray;

		internal ExNativeArray<float3> initLocalPositionArray;

		internal ExNativeArray<quaternion> initLocalRotationArray;

		internal ExNativeArray<float3> positionArray;

		internal ExNativeArray<quaternion> rotationArray;

		internal ExNativeArray<float3> scaleArray;

		internal ExNativeArray<float3> localPositionArray;

		internal ExNativeArray<quaternion> localRotationArray;

		internal ExNativeArray<float4x4> localToWorldMatrixArray;

		internal ExNativeArray<short> teamIdArray;

		internal TransformAccessArray transformAccessArray;

		internal ExNativeArray<float3> componentPositionArray;

		internal TransformAccessArray componentTransformAccessArray;

		private bool isValid;

		internal int Count => flagArray?.Count ?? 0;

		public void Dispose()
		{
			isValid = false;
			flagArray?.Dispose();
			initLocalPositionArray?.Dispose();
			initLocalRotationArray?.Dispose();
			positionArray?.Dispose();
			rotationArray?.Dispose();
			scaleArray?.Dispose();
			localPositionArray?.Dispose();
			localRotationArray?.Dispose();
			localToWorldMatrixArray?.Dispose();
			teamIdArray?.Dispose();
			flagArray = null;
			initLocalPositionArray = null;
			initLocalRotationArray = null;
			positionArray = null;
			rotationArray = null;
			scaleArray = null;
			localPositionArray = null;
			localRotationArray = null;
			localToWorldMatrixArray = null;
			teamIdArray = null;
			if (transformAccessArray.isCreated)
			{
				transformAccessArray.Dispose();
			}
			componentPositionArray?.Dispose();
			if (componentTransformAccessArray.isCreated)
			{
				componentTransformAccessArray.Dispose();
			}
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			flagArray = new ExNativeArray<ExBitFlag8>(256);
			initLocalPositionArray = new ExNativeArray<float3>(256);
			initLocalRotationArray = new ExNativeArray<quaternion>(256);
			positionArray = new ExNativeArray<float3>(256);
			rotationArray = new ExNativeArray<quaternion>(256);
			scaleArray = new ExNativeArray<float3>(256);
			localPositionArray = new ExNativeArray<float3>(256);
			localRotationArray = new ExNativeArray<quaternion>(256);
			localToWorldMatrixArray = new ExNativeArray<float4x4>(256);
			teamIdArray = new ExNativeArray<short>(256);
			transformAccessArray = new TransformAccessArray(256);
			componentPositionArray = new ExNativeArray<float3>(256);
			componentTransformAccessArray = new TransformAccessArray(256);
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		internal DataChunk AddTransform(VirtualMeshContainer cmesh, int teamId)
		{
			if (!isValid)
			{
				return default(DataChunk);
			}
			int transformCount = cmesh.GetTransformCount();
			DataChunk result = flagArray.AddRange(cmesh.shareVirtualMesh.transformData.flagArray);
			initLocalPositionArray.AddRange(cmesh.shareVirtualMesh.transformData.initLocalPositionArray);
			initLocalRotationArray.AddRange(cmesh.shareVirtualMesh.transformData.initLocalRotationArray);
			positionArray.AddRange(transformCount);
			rotationArray.AddRange(transformCount);
			scaleArray.AddRange(transformCount);
			localPositionArray.AddRange(transformCount);
			localRotationArray.AddRange(transformCount);
			localToWorldMatrixArray.AddRange(transformCount);
			teamIdArray.AddRange(transformCount, (short)teamId);
			int i = transformAccessArray.length;
			for (int startIndex = result.startIndex; i < startIndex; i++)
			{
				transformAccessArray.Add(null);
			}
			for (int j = 0; j < transformCount; j++)
			{
				Transform transformFromIndex = cmesh.GetTransformFromIndex(j);
				int num = result.startIndex + j;
				if (num < i)
				{
					transformAccessArray[num] = transformFromIndex;
				}
				else
				{
					transformAccessArray.Add(transformFromIndex);
				}
			}
			return result;
		}

		internal DataChunk AddTransform(int count, int teamId)
		{
			if (!isValid)
			{
				return default(DataChunk);
			}
			DataChunk result = flagArray.AddRange(count);
			initLocalPositionArray.AddRange(count);
			initLocalRotationArray.AddRange(count);
			positionArray.AddRange(count);
			rotationArray.AddRange(count);
			scaleArray.AddRange(count);
			localPositionArray.AddRange(count);
			localRotationArray.AddRange(count);
			localToWorldMatrixArray.AddRange(count);
			teamIdArray.AddRange(count, (short)teamId);
			int i = transformAccessArray.length;
			for (int startIndex = result.startIndex; i < startIndex; i++)
			{
				transformAccessArray.Add(null);
			}
			for (int j = 0; j < count; j++)
			{
				Transform transform = null;
				int num = result.startIndex + j;
				if (num < i)
				{
					transformAccessArray[num] = transform;
				}
				else
				{
					transformAccessArray.Add(transform);
				}
			}
			return result;
		}

		internal DataChunk AddTransform(Transform t, ExBitFlag8 flag, int teamId)
		{
			if (!isValid)
			{
				return default(DataChunk);
			}
			DataChunk result = flagArray.Add(flag);
			initLocalPositionArray.Add(t.localPosition);
			initLocalRotationArray.Add(t.localRotation);
			positionArray.Add(t.position);
			rotationArray.Add(t.rotation);
			scaleArray.Add(t.lossyScale);
			localPositionArray.Add(t.localPosition);
			localRotationArray.Add(t.localRotation);
			localToWorldMatrixArray.Add(float4x4.identity);
			teamIdArray.Add((short)teamId);
			int length = transformAccessArray.length;
			int startIndex = result.startIndex;
			if (startIndex < length)
			{
				transformAccessArray[startIndex] = t;
				return result;
			}
			transformAccessArray.Add(t);
			return result;
		}

		internal void SetTransform(Transform t, ExBitFlag8 flag, int index, int teamId)
		{
			if (isValid)
			{
				if (t != null)
				{
					flagArray[index] = flag;
					initLocalPositionArray[index] = t.localPosition;
					initLocalRotationArray[index] = t.localRotation;
					positionArray[index] = t.position;
					rotationArray[index] = t.rotation;
					scaleArray[index] = t.lossyScale;
					localPositionArray[index] = t.localPosition;
					localRotationArray[index] = t.localRotation;
					teamIdArray[index] = (short)teamId;
					transformAccessArray[index] = t;
				}
				else
				{
					flagArray[index] = default(ExBitFlag8);
					transformAccessArray[index] = null;
					teamIdArray[index] = 0;
				}
			}
		}

		internal void CopyTransform(int fromIndex, int toIndex)
		{
			if (isValid)
			{
				flagArray[toIndex] = flagArray[fromIndex];
				initLocalPositionArray[toIndex] = initLocalPositionArray[fromIndex];
				initLocalRotationArray[toIndex] = initLocalRotationArray[fromIndex];
				positionArray[toIndex] = positionArray[fromIndex];
				rotationArray[toIndex] = rotationArray[fromIndex];
				scaleArray[toIndex] = scaleArray[fromIndex];
				localPositionArray[toIndex] = localPositionArray[fromIndex];
				localRotationArray[toIndex] = localRotationArray[fromIndex];
				transformAccessArray[toIndex] = transformAccessArray[fromIndex];
				teamIdArray[toIndex] = teamIdArray[fromIndex];
			}
		}

		internal void RemoveTransform(DataChunk c)
		{
			if (isValid && c.IsValid)
			{
				flagArray.RemoveAndFill(c);
				initLocalPositionArray.Remove(c);
				initLocalRotationArray.Remove(c);
				positionArray.Remove(c);
				rotationArray.Remove(c);
				scaleArray.Remove(c);
				localPositionArray.Remove(c);
				localRotationArray.Remove(c);
				localToWorldMatrixArray.Remove(c);
				teamIdArray.RemoveAndFill(c, 0);
				for (int i = 0; i < c.dataLength; i++)
				{
					int index = c.startIndex + i;
					transformAccessArray[index] = null;
				}
			}
		}

		internal void EnableTransform(DataChunk c, bool sw)
		{
			if (isValid && c.IsValid)
			{
				new EnableTransformJob
				{
					chunk = c,
					sw = sw,
					flagList = flagArray.GetNativeArray()
				}.Run();
			}
		}

		internal void EnableTransform(int index, bool sw)
		{
			if (isValid && index >= 0)
			{
				ExBitFlag8 value = flagArray[index];
				if (value.Value != 0)
				{
					value.SetFlag(16, sw);
					flagArray[index] = value;
				}
			}
		}

		internal DataChunk Expand(DataChunk c, int newLength)
		{
			if (!isValid)
			{
				return default(DataChunk);
			}
			DataChunk result = flagArray.Expand(c, newLength);
			initLocalPositionArray.Expand(c, newLength);
			initLocalRotationArray.Expand(c, newLength);
			positionArray.Expand(c, newLength);
			rotationArray.Expand(c, newLength);
			scaleArray.Expand(c, newLength);
			localPositionArray.Expand(c, newLength);
			localRotationArray.Expand(c, newLength);
			localToWorldMatrixArray.Expand(c, newLength);
			teamIdArray.Expand(c, newLength);
			if (c.startIndex != result.startIndex)
			{
				while (transformAccessArray.length < result.startIndex + result.dataLength)
				{
					transformAccessArray.Add(null);
				}
				for (int i = 0; i < c.dataLength; i++)
				{
					Transform value = transformAccessArray[c.startIndex + i];
					transformAccessArray[result.startIndex + i] = value;
					transformAccessArray[c.startIndex + i] = null;
				}
			}
			return result;
		}

		public JobHandle RestoreTransform(JobHandle jobHandle)
		{
			if (Count > 0)
			{
				jobHandle = new RestoreTransformJob
				{
					flagList = flagArray.GetNativeArray(),
					localPositionArray = initLocalPositionArray.GetNativeArray(),
					localRotationArray = initLocalRotationArray.GetNativeArray(),
					teamIdArray = teamIdArray.GetNativeArray(),
					teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray()
				}.Schedule(transformAccessArray, jobHandle);
			}
			return jobHandle;
		}

		public JobHandle ReadTransformSchedule(JobHandle jobHandle)
		{
			if (Count > 0)
			{
				jobHandle = new ReadTransformJob
				{
					flagList = flagArray.GetNativeArray(),
					positionArray = positionArray.GetNativeArray(),
					rotationArray = rotationArray.GetNativeArray(),
					scaleList = scaleArray.GetNativeArray(),
					localPositionArray = localPositionArray.GetNativeArray(),
					localRotationArray = localRotationArray.GetNativeArray(),
					localToWorldMatrixArray = localToWorldMatrixArray.GetNativeArray(),
					teamIdArray = teamIdArray.GetNativeArray(),
					teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray()
				}.ScheduleReadOnly(transformAccessArray, 8, jobHandle);
			}
			return jobHandle;
		}

		public JobHandle WriteTransformSchedule(JobHandle jobHandle)
		{
			jobHandle = new WriteTransformJob
			{
				flagList = flagArray.GetNativeArray(),
				worldPositions = positionArray.GetNativeArray(),
				worldRotations = rotationArray.GetNativeArray(),
				localPositions = localPositionArray.GetNativeArray(),
				localRotations = localRotationArray.GetNativeArray(),
				teamIdArray = teamIdArray.GetNativeArray(),
				teamDataArray = MagicaManager.Team.teamDataArray.GetNativeArray()
			}.Schedule(transformAccessArray, jobHandle);
			return jobHandle;
		}

		internal int AddComponentTransform(Transform t)
		{
			if (!isValid)
			{
				return -1;
			}
			int startIndex = componentPositionArray.Add(float3.zero).startIndex;
			int length = componentTransformAccessArray.length;
			if (startIndex < length)
			{
				componentTransformAccessArray[startIndex] = t;
			}
			else
			{
				componentTransformAccessArray.Add(t);
			}
			return startIndex;
		}

		internal void RemoveComponentTransform(int index)
		{
			if (isValid && index >= 0)
			{
				componentPositionArray.Remove(index);
				componentTransformAccessArray[index] = null;
			}
		}

		internal JobHandle ReadComponentTransform(JobHandle jobHandle)
		{
			if (componentPositionArray.Count > 0)
			{
				jobHandle = new ReadComponentTransformJob
				{
					positionArray = componentPositionArray.GetNativeArray()
				}.ScheduleReadOnly(componentTransformAccessArray, 16, jobHandle);
			}
			return jobHandle;
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Transform Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Transform Manager. Invalid.");
			}
			else
			{
				int num = (transformAccessArray.isCreated ? transformAccessArray.length : 0);
				stringBuilder.AppendLine($"Transform Manager. Length:{num}");
				stringBuilder.AppendLine("  -flagArray:" + flagArray.ToSummary());
				stringBuilder.AppendLine("  -initLocalPositionArray:" + initLocalPositionArray.ToSummary());
				stringBuilder.AppendLine("  -initLocalRotationArray:" + initLocalRotationArray.ToSummary());
				stringBuilder.AppendLine("  -positionArray:" + positionArray.ToSummary());
				stringBuilder.AppendLine("  -rotationArray:" + rotationArray.ToSummary());
				stringBuilder.AppendLine("  -scaleArray:" + scaleArray.ToSummary());
				stringBuilder.AppendLine("  -localPositionArray:" + localPositionArray.ToSummary());
				stringBuilder.AppendLine("  -localRotationArray:" + localRotationArray.ToSummary());
				stringBuilder.AppendLine("  -localToWorldMatirxArray:" + localToWorldMatrixArray.ToSummary());
				stringBuilder.AppendLine("  -teamIdArray:" + teamIdArray.ToSummary());
				if (transformAccessArray.isCreated)
				{
					for (int i = 0; i < num; i++)
					{
						Transform transform = transformAccessArray[i];
						ExBitFlag8 exBitFlag = flagArray[i];
						short num2 = teamIdArray[i];
						stringBuilder.Append($"  [{i}] team:{num2} (");
						stringBuilder.Append(exBitFlag.IsSet(16) ? "E" : "");
						stringBuilder.Append(exBitFlag.IsSet(8) ? "R" : "");
						stringBuilder.Append(exBitFlag.IsSet(1) ? "r" : "");
						stringBuilder.Append(exBitFlag.IsSet(2) ? "W" : "");
						stringBuilder.Append(exBitFlag.IsSet(4) ? "w" : "");
						stringBuilder.AppendLine(") " + (transform?.name ?? "(null)"));
					}
				}
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
