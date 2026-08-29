using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public struct VirtualMeshTransform
	{
		public FixedString32Bytes name;

		public int index;

		public float4x4 localToWorldMatrix;

		public float4x4 worldToLocalMatrix;

		public int parentIndex;

		public static VirtualMeshTransform Origin => new VirtualMeshTransform
		{
			name = "VirtualMesh Origin",
			localToWorldMatrix = float4x4.identity,
			worldToLocalMatrix = float4x4.identity,
			parentIndex = -1
		};

		public VirtualMeshTransform(Transform t)
		{
			name = t.name.Substring(0, math.min(t.name.Length, 29));
			index = -1;
			localToWorldMatrix = t.localToWorldMatrix;
			worldToLocalMatrix = t.worldToLocalMatrix;
			parentIndex = -1;
		}

		public VirtualMeshTransform(Transform t, int index)
			: this(t)
		{
			this.index = index;
		}

		public VirtualMeshTransform Clone()
		{
			return new VirtualMeshTransform
			{
				name = name,
				index = index,
				localToWorldMatrix = localToWorldMatrix,
				worldToLocalMatrix = worldToLocalMatrix,
				parentIndex = parentIndex
			};
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public void Update(Transform t)
		{
			localToWorldMatrix = t.localToWorldMatrix;
			worldToLocalMatrix = t.worldToLocalMatrix;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformPoint(float3 pos)
		{
			return math.transform(localToWorldMatrix, pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformVector(float3 vec)
		{
			return math.mul(localToWorldMatrix, new float4(vec, 0f)).xyz;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 TransformDirection(float3 dir)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(TransformVector(dir)) * num;
			}
			return dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformPoint(float3 pos)
		{
			return math.transform(worldToLocalMatrix, pos);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformVector(float3 vec)
		{
			return math.mul(worldToLocalMatrix, new float4(vec, 0f)).xyz;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 InverseTransformDirection(float3 dir)
		{
			float num = math.length(dir);
			if (num > 0f)
			{
				return math.normalize(InverseTransformVector(dir)) * num;
			}
			return dir;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion InverseTransformRotation(quaternion rot)
		{
			return math.mul(new quaternion(worldToLocalMatrix), rot);
		}

		public VirtualMeshTransform Transform(in VirtualMeshTransform to)
		{
			return new VirtualMeshTransform
			{
				name = "__(temporary)__",
				index = -1,
				localToWorldMatrix = math.mul(to.worldToLocalMatrix, localToWorldMatrix),
				worldToLocalMatrix = math.mul(worldToLocalMatrix, to.localToWorldMatrix),
				parentIndex = -1
			};
		}
	}
}
