using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class TriangleBendingConstraint : IDisposable
	{
		public enum Method
		{
			None = 0,
			DihedralAngle = 1,
			DirectionDihedralAngle = 2
		}

		[Serializable]
		public class SerializeData : IDataValidate
		{
			[Range(0f, 1f)]
			public float stiffness;

			public SerializeData()
			{
				stiffness = 1f;
			}

			public void DataValidate()
			{
				stiffness = Mathf.Clamp01(stiffness);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					stiffness = stiffness
				};
			}
		}

		public struct TriangleBendingConstraintParams
		{
			public Method method;

			public float stiffness;

			public void Convert(SerializeData sdata)
			{
				method = ((sdata.stiffness > 1E-08f) ? Method.DirectionDihedralAngle : Method.None);
				stiffness = sdata.stiffness;
			}
		}

		[Serializable]
		public class ConstraintData : IValid
		{
			public ResultCode result;

			public ulong[] trianglePairArray;

			public float[] restAngleOrVolumeArray;

			public sbyte[] signOrVolumeArray;

			public int writeBufferCount;

			public uint[] writeDataArray;

			public uint[] writeIndexArray;

			public bool IsValid()
			{
				if (trianglePairArray != null)
				{
					return trianglePairArray.Length != 0;
				}
				return false;
			}
		}

		private const sbyte VOLUME_SIGN = 100;

		public ExNativeArray<ulong> trianglePairArray;

		public ExNativeArray<float> restAngleOrVolumeArray;

		public ExNativeArray<sbyte> signOrVolumeArray;

		private const float VolumeScale = 1000f;

		public TriangleBendingConstraint()
		{
			trianglePairArray = new ExNativeArray<ulong>(0, create: true);
			restAngleOrVolumeArray = new ExNativeArray<float>(0, create: true);
			signOrVolumeArray = new ExNativeArray<sbyte>(0, create: true);
		}

		public void Dispose()
		{
			trianglePairArray?.Dispose();
			restAngleOrVolumeArray?.Dispose();
			signOrVolumeArray?.Dispose();
			trianglePairArray = null;
			restAngleOrVolumeArray = null;
			signOrVolumeArray = null;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[TriangleBendConstraint]");
			stringBuilder.AppendLine("  -trianglePairArray:" + trianglePairArray.ToSummary());
			stringBuilder.AppendLine("  -restAngleOrVolumeArray:" + restAngleOrVolumeArray.ToSummary());
			stringBuilder.AppendLine("  -signOrVolumeArray:" + signOrVolumeArray.ToSummary());
			return stringBuilder.ToString();
		}

		public static ConstraintData CreateData(VirtualMesh proxyMesh, in ClothParameters parameters)
		{
			ConstraintData constraintData = new ConstraintData();
			try
			{
				if (proxyMesh.TriangleCount == 0)
				{
					return null;
				}
				int edgeCount = proxyMesh.EdgeCount;
				if (edgeCount == 0)
				{
					return null;
				}
				List<ulong> list = new List<ulong>(edgeCount * 2);
				List<float> list2 = new List<float>(edgeCount * 2);
				List<sbyte> list3 = new List<sbyte>(edgeCount * 2);
				List<uint> list4 = new List<uint>(edgeCount * 2);
				HashSet<int4> hashSet = new HashSet<int4>();
				int num = 0;
				int num2 = 0;
				int vertexCount = proxyMesh.VertexCount;
				using MultiDataBuilder<byte> multiDataBuilder = new MultiDataBuilder<byte>(vertexCount, vertexCount * 2);
				for (int i = 0; i < edgeCount; i++)
				{
					int2 int5 = proxyMesh.edges[i];
					if (!proxyMesh.edgeToTriangles.ContainsKey(int5))
					{
						continue;
					}
					FixedList128Bytes<ushort> fixedList128Bytes = NativeMultiHashMapExtensions.MC2ToFixedList128Bytes(ref proxyMesh.edgeToTriangles, int5);
					int length = fixedList128Bytes.Length;
					for (int j = 0; j < length - 1; j++)
					{
						int index = fixedList128Bytes[j];
						int3 tri = proxyMesh.triangles[index];
						for (int k = j + 1; k < length; k++)
						{
							int index2 = fixedList128Bytes[k];
							int3 tri2 = proxyMesh.triangles[index2];
							int2 restTriangleVertex = MathUtility.GetRestTriangleVertex(tri, tri2, int5);
							int4 a = new int4(restTriangleVertex.x, restTriangleVertex.y, int5.x, int5.y);
							VertexAttribute vertexAttribute = proxyMesh.attributes[a.x];
							VertexAttribute vertexAttribute2 = proxyMesh.attributes[a.y];
							VertexAttribute vertexAttribute3 = proxyMesh.attributes[a.z];
							VertexAttribute vertexAttribute4 = proxyMesh.attributes[a.w];
							if ((vertexAttribute.IsDontMove() && vertexAttribute2.IsDontMove() && vertexAttribute3.IsDontMove() && vertexAttribute4.IsDontMove()) || vertexAttribute.IsInvalid() || vertexAttribute2.IsInvalid() || vertexAttribute3.IsInvalid() || vertexAttribute4.IsInvalid())
							{
								continue;
							}
							ulong item = DataUtility.Pack64(in a);
							InitDihedralAngle(proxyMesh, a.x, a.y, a.z, a.w, out var restAngle, out var signFlag);
							float num3 = math.abs(math.degrees(restAngle));
							if (num3 < 120f)
							{
								list.Add(item);
								list2.Add(restAngle);
								list3.Add(signFlag);
								uint item2 = DataUtility.Pack32(multiDataBuilder.CountValuesForKey(a.x), multiDataBuilder.CountValuesForKey(a.y), multiDataBuilder.CountValuesForKey(a.z), multiDataBuilder.CountValuesForKey(a.w));
								list4.Add(item2);
								multiDataBuilder.Add(a.x, 0);
								multiDataBuilder.Add(a.y, 0);
								multiDataBuilder.Add(a.z, 0);
								multiDataBuilder.Add(a.w, 0);
								num++;
							}
							if (num3 >= 90f && num3 <= 179f)
							{
								int4 item3 = DataUtility.PackInt4(a);
								if (!hashSet.Contains(item3))
								{
									InitVolume(proxyMesh, a.x, a.y, a.z, a.w, out restAngle, out signFlag);
									list.Add(item);
									list2.Add(restAngle);
									list3.Add(signFlag);
									hashSet.Add(item3);
									uint item4 = DataUtility.Pack32(multiDataBuilder.CountValuesForKey(a.x), multiDataBuilder.CountValuesForKey(a.y), multiDataBuilder.CountValuesForKey(a.z), multiDataBuilder.CountValuesForKey(a.w));
									list4.Add(item4);
									multiDataBuilder.Add(a.x, 0);
									multiDataBuilder.Add(a.y, 0);
									multiDataBuilder.Add(a.z, 0);
									multiDataBuilder.Add(a.w, 0);
									num2++;
								}
							}
						}
					}
				}
				constraintData.trianglePairArray = ((list.Count > 0) ? list.ToArray() : null);
				constraintData.restAngleOrVolumeArray = ((list2.Count > 0) ? list2.ToArray() : null);
				constraintData.signOrVolumeArray = ((list3.Count > 0) ? list3.ToArray() : null);
				constraintData.writeDataArray = ((list4.Count > 0) ? list4.ToArray() : null);
				constraintData.writeBufferCount = multiDataBuilder.Count();
				constraintData.writeIndexArray = multiDataBuilder.ToIndexArray();
				constraintData.result.SetSuccess();
				return constraintData;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				constraintData.result.SetError(Define.Result.Constraint_CreateTriangleBendingException);
				throw;
			}
		}

		private static void InitVolume(VirtualMesh proxyMesh, int v0, int v1, int v2, int v3, out float volumeRest, out sbyte signFlag)
		{
			float3 float5 = MathUtility.TransformPoint(proxyMesh.localPositions[v0], in proxyMesh.initLocalToWorld);
			float3 float6 = MathUtility.TransformPoint(proxyMesh.localPositions[v1], in proxyMesh.initLocalToWorld);
			float3 float7 = MathUtility.TransformPoint(proxyMesh.localPositions[v2], in proxyMesh.initLocalToWorld);
			float3 float8 = MathUtility.TransformPoint(proxyMesh.localPositions[v3], in proxyMesh.initLocalToWorld);
			volumeRest = 1f / 6f * math.dot(math.cross(float6 - float5, float7 - float5), float8 - float5);
			volumeRest *= 1000f;
			signFlag = 100;
		}

		private static void InitDihedralAngle(VirtualMesh proxyMesh, int v0, int v1, int v2, int v3, out float restAngle, out sbyte signFlag)
		{
			float3 float5 = proxyMesh.localPositions[v0];
			float3 float6 = proxyMesh.localPositions[v1];
			float3 float7 = proxyMesh.localPositions[v2];
			float3 float8 = proxyMesh.localPositions[v3];
			float3 x = math.cross(float7 - float5, float8 - float5);
			float3 x2 = math.cross(float8 - float6, float7 - float6);
			float3 x3 = math.normalize(x);
			x2 = math.normalize(x2);
			float a = math.dot(x3, x2);
			a = MathUtility.Clamp1(a);
			restAngle = math.acos(a);
			float num = math.sign(math.dot(y: float8 - float7, x: math.cross(x3, x2)));
			signFlag = (sbyte)((!(num < 0f)) ? 1 : (-1));
		}

		internal void Register(ClothProcess cprocess)
		{
			if (cprocess?.bendingConstraintData?.IsValid() == true)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				ConstraintData bendingConstraintData = cprocess.bendingConstraintData;
				teamDataRef.bendingPairChunk = trianglePairArray.AddRange(bendingConstraintData.trianglePairArray);
				restAngleOrVolumeArray.AddRange(bendingConstraintData.restAngleOrVolumeArray);
				signOrVolumeArray.AddRange(bendingConstraintData.signOrVolumeArray);
			}
		}

		internal void Exit(ClothProcess cprocess)
		{
			if (cprocess != null && cprocess.TeamId > 0)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				trianglePairArray.Remove(teamDataRef.bendingPairChunk);
				restAngleOrVolumeArray.Remove(teamDataRef.bendingPairChunk);
				signOrVolumeArray.Remove(teamDataRef.bendingPairChunk);
				teamDataRef.bendingPairChunk.Clear();
			}
		}

		internal unsafe static void SolverConstraint(DataChunk chunk, in float4 simulationPower, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float> depthArray, ref NativeArray<float3> nextPosArray, ref NativeArray<float> frictionArray, ref NativeArray<ulong> trianglePairArray, ref NativeArray<float> restAngleOrVolumeArray, ref NativeArray<sbyte> signOrVolumeArray, ref NativeArray<float3> tempVectorBufferA, ref NativeArray<int> tempCountBuffer)
		{
			if (param.triangleBendingConstraint.method == Method.None || !tdata.bendingPairChunk.IsValid)
			{
				return;
			}
			float stiffness = param.triangleBendingConstraint.stiffness;
			if (stiffness < 1E-06f)
			{
				return;
			}
			stiffness = math.saturate(stiffness * simulationPower.y);
			int startIndex = tdata.particleChunk.startIndex;
			int startIndex2 = tdata.proxyCommonChunk.startIndex;
			int* unsafePtr = (int*)tempVectorBufferA.GetUnsafePtr();
			int* unsafePtr2 = (int*)tempCountBuffer.GetUnsafePtr();
			int num = tdata.bendingPairChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				int4 obj = DataUtility.Unpack64(trianglePairArray[num]);
				int4 int5 = obj + startIndex;
				int4 int6 = obj + startIndex2;
				float3x4 nextPosBuffer = 0;
				float3x4 addPosBuffer = 0;
				float4 invMassBuffer = 1;
				for (int i = 0; i < 4; i++)
				{
					int index = int5[i];
					int index2 = int6[i];
					nextPosBuffer[i] = nextPosArray[index];
					float friction = frictionArray[index];
					float depth = depthArray[index2];
					bool flag = attributes[index2].IsDontMove();
					invMassBuffer[i] = (flag ? 0.01f : MathUtility.CalcInverseMass(friction, depth));
				}
				int num3 = num - tdata.bendingPairChunk.startIndex;
				int index3 = tdata.bendingPairChunk.startIndex + num3;
				float num4 = restAngleOrVolumeArray[index3];
				sbyte b = signOrVolumeArray[index3];
				bool flag2 = false;
				if (b == 100)
				{
					float num5 = num4 * tdata.scaleRatio;
					num5 *= tdata.negativeScaleSign;
					flag2 = CalcVolume(in nextPosBuffer, in invMassBuffer, num5, stiffness, ref addPosBuffer);
				}
				else if (param.triangleBendingConstraint.method == Method.DihedralAngle)
				{
					flag2 = CalcDihedralAngle(0f, in nextPosBuffer, in invMassBuffer, num4, stiffness, ref addPosBuffer);
				}
				else if (param.triangleBendingConstraint.method == Method.DirectionDihedralAngle)
				{
					float num6 = ((b >= 0) ? 1 : (-1));
					num4 *= num6;
					num4 *= tdata.negativeScaleSign;
					flag2 = CalcDihedralAngle(num6, in nextPosBuffer, in invMassBuffer, num4, stiffness, ref addPosBuffer);
				}
				if (flag2)
				{
					for (int j = 0; j < 4; j++)
					{
						int index = int5[j];
						InterlockUtility.AddFloat3(index, addPosBuffer[j], unsafePtr2, unsafePtr);
					}
				}
				num2++;
				num++;
			}
		}

		internal unsafe static void SumConstraint(DataChunk chunk, ref TeamManager.TeamData tdata, ref ClothParameters param, ref NativeArray<VertexAttribute> attributes, ref NativeArray<float3> nextPosArray, ref NativeArray<float3> tempVectorBufferA, ref NativeArray<int> tempCountBuffer)
		{
			if (param.triangleBendingConstraint.method == Method.None || !tdata.bendingPairChunk.IsValid || param.triangleBendingConstraint.stiffness < 1E-06f)
			{
				return;
			}
			int startIndex = tdata.particleChunk.startIndex;
			int startIndex2 = tdata.proxyCommonChunk.startIndex;
			int* unsafePtr = (int*)tempVectorBufferA.GetUnsafePtr();
			int* unsafePtr2 = (int*)tempCountBuffer.GetUnsafePtr();
			int num = startIndex + chunk.startIndex;
			int num2 = startIndex2 + chunk.startIndex;
			int num3 = 0;
			while (num3 < chunk.dataLength)
			{
				if (!attributes[num2].IsDontMove())
				{
					int num4 = unsafePtr2[num];
					if (num4 > 0)
					{
						int num5 = num * 3;
						float3 float5 = new float3(unsafePtr[num5], unsafePtr[num5 + 1], unsafePtr[num5 + 2]);
						float5 /= (float)num4;
						float5 *= 1E-06f;
						nextPosArray[num] += float5;
					}
				}
				tempCountBuffer[num] = 0;
				tempVectorBufferA[num] = 0;
				num3++;
				num++;
				num2++;
			}
		}

		private static bool CalcVolume(in float3x4 nextPosBuffer, in float4 invMassBuffer, float volumeRest, float stiffness, ref float3x4 addPosBuffer)
		{
			float3 float5 = nextPosBuffer[0];
			float3 float6 = nextPosBuffer[1];
			float3 float7 = nextPosBuffer[2];
			float3 float8 = nextPosBuffer[3];
			float num = invMassBuffer[0];
			float num2 = invMassBuffer[1];
			float num3 = invMassBuffer[2];
			float num4 = invMassBuffer[3];
			float num5 = 1f / 6f * math.dot(math.cross(float6 - float5, float7 - float5), float8 - float5);
			num5 *= 1000f;
			float3 float9 = math.cross(float6 - float7, float8 - float7);
			float3 float10 = math.cross(float7 - float5, float8 - float5);
			float3 float11 = math.cross(float5 - float6, float8 - float6);
			float3 float12 = math.cross(float6 - float5, float7 - float5);
			float num6 = num * math.lengthsq(float9) + num2 * math.lengthsq(float10) + num3 * math.lengthsq(float11) + num4 * math.lengthsq(float12);
			num6 *= 1000f;
			if (math.abs(num6) < 1E-06f)
			{
				return false;
			}
			num6 = stiffness * (volumeRest - num5) / num6;
			addPosBuffer[0] = num6 * num * float9;
			addPosBuffer[1] = num6 * num2 * float10;
			addPosBuffer[2] = num6 * num3 * float11;
			addPosBuffer[3] = num6 * num4 * float12;
			return true;
		}

		private static bool CalcDihedralAngle(float sign, in float3x4 nextPosBuffer, in float4 invMassBuffer, float restAngle, float stiffness, ref float3x4 addPosBuffer)
		{
			float3 float5 = nextPosBuffer[0];
			float3 float6 = nextPosBuffer[1];
			float3 float7 = nextPosBuffer[2];
			float3 float8 = nextPosBuffer[3];
			float num = invMassBuffer[0];
			float num2 = invMassBuffer[1];
			float num3 = invMassBuffer[2];
			float num4 = invMassBuffer[3];
			float3 float9 = float8 - float7;
			float num5 = math.length(float9);
			if (num5 < 1E-08f)
			{
				return false;
			}
			float num6 = 1f / num5;
			float3 float10 = math.cross(float7 - float5, float8 - float5);
			float3 float11 = math.cross(float8 - float6, float7 - float6);
			float num7 = math.lengthsq(float10);
			float num8 = math.lengthsq(float11);
			if (num7 == 0f || num8 == 0f)
			{
				return false;
			}
			float10 /= num7;
			float11 /= num8;
			float3 float12 = num5 * float10;
			float3 float13 = num5 * float11;
			float3 float14 = math.dot(float5 - float8, float9) * num6 * float10 + math.dot(float6 - float8, float9) * num6 * float11;
			float3 float15 = math.dot(float7 - float5, float9) * num6 * float10 + math.dot(float7 - float6, float9) * num6 * float11;
			float10 = math.normalize(float10);
			float11 = math.normalize(float11);
			float num9 = math.acos(MathUtility.Clamp1(math.dot(float10, float11)));
			float num10 = num * math.lengthsq(float12) + num2 * math.lengthsq(float13) + num3 * math.lengthsq(float14) + num4 * math.lengthsq(float15);
			if (num10 == 0f)
			{
				return false;
			}
			float num11 = math.sign(math.dot(math.cross(float10, float11), float9));
			if (sign != 0f)
			{
				num9 *= num11;
			}
			else
			{
				num10 *= num11;
			}
			num10 = (restAngle - num9) / num10 * stiffness;
			float3 float16 = (0f - num) * num10 * float12;
			float3 float17 = (0f - num2) * num10 * float13;
			float3 float18 = (0f - num3) * num10 * float14;
			float3 float19 = (0f - num4) * num10 * float15;
			addPosBuffer[0] = float16;
			addPosBuffer[1] = float17;
			addPosBuffer[2] = float18;
			addPosBuffer[3] = float19;
			return true;
		}
	}
}
