using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace MagicaCloth2
{
	[Serializable]
	public class SelectionData : IValid
	{
		[BurstCompile]
		private struct TransformPositionJob : IJobParallelFor
		{
			public float4x4 transformMatrix;

			public NativeArray<float3> localPositions;

			public void Execute(int index)
			{
				float3 b = localPositions[index];
				b = math.transform(transformMatrix, b);
				localPositions[index] = b;
			}
		}

		[BurstCompile]
		private struct CreateGridMapJob : IJob
		{
			public bool move;

			public bool fix;

			public bool ignore;

			public bool invalid;

			public NativeParallelMultiHashMap<int3, int> gridMap;

			public float gridSize;

			[ReadOnly]
			public NativeArray<float3> positions;

			[ReadOnly]
			public NativeArray<VertexAttribute> attribute;

			public void Execute()
			{
				int length = positions.Length;
				for (int i = 0; i < length; i++)
				{
					VertexAttribute vertexAttribute = attribute[i];
					if ((move || !vertexAttribute.IsMove()) && (fix || !vertexAttribute.IsFixed()) && (invalid || !vertexAttribute.IsInvalid()))
					{
						GridMap<int>.AddGrid(positions[i], i, gridMap, gridSize);
					}
				}
			}
		}

		[BurstCompile]
		private struct ConvertSelectionJob : IJobParallelFor
		{
			public float gridSize;

			public float radius;

			[ReadOnly]
			public NativeArray<float3> toPositions;

			[WriteOnly]
			public NativeArray<VertexAttribute> toAttributes;

			[ReadOnly]
			public NativeParallelMultiHashMap<int3, int> gridMap;

			[ReadOnly]
			public NativeArray<float3> fromPositions;

			[ReadOnly]
			public NativeArray<VertexAttribute> fromAttributes;

			public void Execute(int vindex)
			{
				float3 float5 = toPositions[vindex];
				VertexAttribute value = VertexAttribute.Invalid;
				float num = float.MaxValue;
				foreach (int3 item in GridMap<int>.GetArea(float5, radius, gridMap, gridSize))
				{
					if (!gridMap.ContainsKey(item))
					{
						continue;
					}
					foreach (int item2 in gridMap.GetValuesForKey(item))
					{
						float3 y = fromPositions[item2];
						float num2 = math.distance(float5, y);
						if (!(num2 > radius) && !(num2 > num))
						{
							num = num2;
							value = fromAttributes[item2];
						}
					}
				}
				toAttributes[vindex] = value;
			}
		}

		public float3[] positions;

		public VertexAttribute[] attributes;

		public float maxConnectionDistance;

		public bool userEdit;

		public int Count
		{
			get
			{
				float3[] array = positions;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		public SelectionData()
		{
		}

		public SelectionData(int cnt)
		{
			positions = new float3[cnt];
			attributes = new VertexAttribute[cnt];
		}

		public SelectionData(VirtualMesh vmesh, float4x4 transformMatrix)
		{
			if (vmesh != null && vmesh.VertexCount > 0)
			{
				using (NativeArray<float3> localPositions = new NativeArray<float3>(vmesh.localPositions.GetNativeArray(), Allocator.TempJob))
				{
					IJobParallelForExtensions.Run(new TransformPositionJob
					{
						transformMatrix = transformMatrix,
						localPositions = localPositions
					}, vmesh.VertexCount);
					positions = localPositions.ToArray();
					attributes = vmesh.attributes.ToArray();
					maxConnectionDistance = vmesh.maxVertexDistance.Value;
				}
			}
		}

		public bool IsValid()
		{
			if (positions == null || positions.Length == 0)
			{
				return false;
			}
			if (attributes == null || attributes.Length == 0)
			{
				return false;
			}
			if (positions.Length != attributes.Length)
			{
				return false;
			}
			return true;
		}

		public bool IsUserEdit()
		{
			return userEdit;
		}

		public SelectionData Clone()
		{
			return new SelectionData
			{
				positions = (positions?.Clone() as float3[]),
				attributes = (attributes?.Clone() as VertexAttribute[]),
				maxConnectionDistance = maxConnectionDistance,
				userEdit = userEdit
			};
		}

		public bool Compare(SelectionData sdata)
		{
			if (positions?.Length != sdata.positions?.Length)
			{
				return false;
			}
			if (attributes?.Length != sdata.attributes?.Length)
			{
				return false;
			}
			if (userEdit != sdata.userEdit)
			{
				return false;
			}
			int num = attributes.Length;
			for (int i = 0; i < num; i++)
			{
				if (attributes[i] != sdata.attributes[i])
				{
					return false;
				}
				if (!positions[i].Equals(sdata.positions[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void AddRange(float3[] addPositions, VertexAttribute[] addAttributes = null)
		{
			if (Count == 0)
			{
				positions = addPositions;
				attributes = ((addAttributes != null) ? addAttributes : new VertexAttribute[addPositions.Length]);
				return;
			}
			int count = Count;
			int num = addPositions.Length;
			float3[] destinationArray = new float3[count + num];
			VertexAttribute[] destinationArray2 = new VertexAttribute[count + num];
			Array.Copy(positions, 0, destinationArray, 0, count);
			Array.Copy(addPositions, 0, destinationArray, count, num);
			Array.Copy(attributes, 0, destinationArray2, 0, count);
			if (addAttributes != null)
			{
				Array.Copy(addAttributes, 0, destinationArray2, count, num);
			}
			positions = destinationArray;
			attributes = destinationArray2;
		}

		public void Fill(VertexAttribute attr)
		{
			Array.Fill(attributes, attr);
		}

		public NativeArray<float3> GetPositionNativeArray()
		{
			return new NativeArray<float3>(positions, Allocator.Persistent);
		}

		public NativeArray<float3> GetPositionNativeArray(float4x4 transformMatrix)
		{
			NativeArray<float3> positionNativeArray = GetPositionNativeArray();
			IJobParallelForExtensions.Run(new TransformPositionJob
			{
				transformMatrix = transformMatrix,
				localPositions = positionNativeArray
			}, Count);
			return positionNativeArray;
		}

		public NativeArray<VertexAttribute> GetAttributeNativeArray()
		{
			return new NativeArray<VertexAttribute>(attributes, Allocator.Persistent);
		}

		public static GridMap<int> CreateGridMapRun(float gridSize, in NativeArray<float3> positions, in NativeArray<VertexAttribute> attributes, bool move = true, bool fix = true, bool ignore = true, bool invalid = true)
		{
			GridMap<int> gridMap = new GridMap<int>(positions.Length);
			new CreateGridMapJob
			{
				move = move,
				fix = fix,
				ignore = ignore,
				invalid = invalid,
				gridMap = gridMap.GetMultiHashMap(),
				gridSize = gridSize,
				positions = positions,
				attribute = attributes
			}.Run();
			return gridMap;
		}

		public void Merge(SelectionData from)
		{
			if (from.Count != 0)
			{
				int capacity = Count + from.Count;
				List<float3> list = new List<float3>(capacity);
				List<VertexAttribute> list2 = new List<VertexAttribute>(capacity);
				if (positions != null)
				{
					list.AddRange(positions);
				}
				if (attributes != null)
				{
					list2.AddRange(attributes);
				}
				list.AddRange(from.positions);
				list2.AddRange(from.attributes);
				positions = list.ToArray();
				attributes = list2.ToArray();
				maxConnectionDistance = math.max(maxConnectionDistance, from.maxConnectionDistance);
				userEdit = userEdit || from.userEdit;
			}
		}

		public void ConvertFrom(SelectionData from)
		{
			if (from.Count == 0 || Count == 0)
			{
				return;
			}
			using NativeArray<float3> toPositions = GetPositionNativeArray();
			using NativeArray<VertexAttribute> toAttributes = GetAttributeNativeArray();
			using NativeArray<float3> fromPositions = from.GetPositionNativeArray();
			using NativeArray<VertexAttribute> fromAttributes = from.GetAttributeNativeArray();
			using NativeReference<AABB> outAABB = new NativeReference<AABB>(Allocator.TempJob);
			JobUtility.CalcAABBRun(toPositions, Count, outAABB);
			float x = outAABB.Value.MaxSideLength * 0.2f;
			x = math.max(x, 1E-05f);
			float gridSize = x * 0.5f;
			using GridMap<int> gridMap = CreateGridMapRun(gridSize, in fromPositions, in fromAttributes);
			IJobParallelForExtensions.Run(new ConvertSelectionJob
			{
				gridSize = gridSize,
				radius = x,
				toPositions = toPositions,
				toAttributes = toAttributes,
				gridMap = gridMap.GetMultiHashMap(),
				fromPositions = fromPositions,
				fromAttributes = fromAttributes
			}, Count);
			positions = toPositions.ToArray();
			attributes = toAttributes.ToArray();
		}
	}
}
