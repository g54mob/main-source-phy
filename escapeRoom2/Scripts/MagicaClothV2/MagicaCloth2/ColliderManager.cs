using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class ColliderManager : IManager, IDisposable, IValid
	{
		public enum ColliderType : byte
		{
			None = 0,
			Sphere = 1,
			CapsuleX_Center = 2,
			CapsuleY_Center = 3,
			CapsuleZ_Center = 4,
			CapsuleX_Start = 5,
			CapsuleY_Start = 6,
			CapsuleZ_Start = 7,
			Plane = 8,
			Box = 9
		}

		internal struct WorkData
		{
			public AABB aabb;

			public float2 radius;

			public float3x2 oldPos;

			public float3x2 nextPos;

			public quaternion inverseOldRot;

			public quaternion rot;
		}

		public ExNativeArray<short> teamIdArray;

		public const byte Flag_Valid = 16;

		public const byte Flag_Enable = 32;

		public const byte Flag_Reset = 64;

		public const byte Flag_Reverse = 128;

		public ExNativeArray<ExBitFlag8> flagArray;

		public ExNativeArray<float3> centerArray;

		public ExNativeArray<float3> sizeArray;

		public ExNativeArray<float3> framePositions;

		public ExNativeArray<quaternion> frameRotations;

		public ExNativeArray<float3> frameScales;

		public ExNativeArray<float3> oldFramePositions;

		public ExNativeArray<quaternion> oldFrameRotations;

		public ExNativeArray<float3> nowPositions;

		public ExNativeArray<quaternion> nowRotations;

		public ExNativeArray<float3> oldPositions;

		public ExNativeArray<quaternion> oldRotations;

		public HashSet<ColliderComponent> colliderSet = new HashSet<ColliderComponent>();

		private bool isValid;

		internal ExNativeArray<WorkData> workDataArray;

		public int DataCount => teamIdArray?.Count ?? 0;

		public int ColliderCount => colliderSet.Count;

		public void Dispose()
		{
			isValid = false;
			teamIdArray?.Dispose();
			flagArray?.Dispose();
			centerArray?.Dispose();
			sizeArray?.Dispose();
			framePositions?.Dispose();
			frameRotations?.Dispose();
			frameScales?.Dispose();
			nowPositions?.Dispose();
			nowRotations?.Dispose();
			oldFramePositions?.Dispose();
			oldFrameRotations?.Dispose();
			oldPositions?.Dispose();
			oldRotations?.Dispose();
			workDataArray?.Dispose();
			teamIdArray = null;
			flagArray = null;
			sizeArray = null;
			framePositions = null;
			frameRotations = null;
			frameScales = null;
			nowPositions = null;
			nowRotations = null;
			oldFramePositions = null;
			oldFrameRotations = null;
			oldPositions = null;
			oldRotations = null;
			workDataArray = null;
			colliderSet.Clear();
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			teamIdArray = new ExNativeArray<short>(256);
			flagArray = new ExNativeArray<ExBitFlag8>(256);
			centerArray = new ExNativeArray<float3>(256);
			sizeArray = new ExNativeArray<float3>(256);
			framePositions = new ExNativeArray<float3>(256);
			frameRotations = new ExNativeArray<quaternion>(256);
			frameScales = new ExNativeArray<float3>(256);
			nowPositions = new ExNativeArray<float3>(256);
			nowRotations = new ExNativeArray<quaternion>(256);
			oldFramePositions = new ExNativeArray<float3>(256);
			oldFrameRotations = new ExNativeArray<quaternion>(256);
			oldPositions = new ExNativeArray<float3>(256);
			oldRotations = new ExNativeArray<quaternion>(256);
			workDataArray = new ExNativeArray<WorkData>(256);
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		public void Register(ClothProcess cprocess)
		{
			if (isValid)
			{
				int colliderLength = cprocess.cloth.SerializeData.colliderCollisionConstraint.ColliderLength;
				if (colliderLength > 0)
				{
					int teamId = cprocess.TeamId;
					ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
					teamDataRef.colliderChunk = teamIdArray.AddRange(colliderLength, (short)teamId);
					flagArray.AddRange(colliderLength, default(ExBitFlag8));
					centerArray.AddRange(colliderLength);
					sizeArray.AddRange(colliderLength);
					framePositions.AddRange(colliderLength);
					frameRotations.AddRange(colliderLength);
					frameScales.AddRange(colliderLength);
					nowPositions.AddRange(colliderLength);
					nowRotations.AddRange(colliderLength);
					oldFramePositions.AddRange(colliderLength);
					oldFrameRotations.AddRange(colliderLength);
					oldPositions.AddRange(colliderLength);
					oldRotations.AddRange(colliderLength);
					workDataArray.AddRange(colliderLength);
					teamDataRef.colliderTransformChunk = MagicaManager.Bone.AddTransform(colliderLength, teamId);
					teamDataRef.colliderCount = 0;
					cprocess.colliderList.AddRange(new ColliderComponent[colliderLength]);
					InitColliders(cprocess);
				}
			}
		}

		public void Exit(ClothProcess cprocess)
		{
			if (!isValid)
			{
				return;
			}
			int teamId = cprocess.TeamId;
			foreach (ColliderComponent collider in cprocess.colliderList)
			{
				if ((bool)collider)
				{
					collider.Exit(teamId);
					colliderSet.Remove(collider);
				}
			}
			cprocess.colliderList.Clear();
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
			DataChunk colliderChunk = teamDataRef.colliderChunk;
			teamIdArray.RemoveAndFill(colliderChunk, 0);
			flagArray.RemoveAndFill(colliderChunk);
			centerArray.Remove(colliderChunk);
			sizeArray.Remove(colliderChunk);
			framePositions.Remove(colliderChunk);
			frameRotations.Remove(colliderChunk);
			frameScales.Remove(colliderChunk);
			nowPositions.Remove(colliderChunk);
			nowRotations.Remove(colliderChunk);
			oldFramePositions.Remove(colliderChunk);
			oldFrameRotations.Remove(colliderChunk);
			oldPositions.Remove(colliderChunk);
			oldRotations.Remove(colliderChunk);
			workDataArray.Remove(colliderChunk);
			teamDataRef.colliderChunk.Clear();
			teamDataRef.colliderCount = 0;
			MagicaManager.Bone.RemoveTransform(teamDataRef.colliderTransformChunk);
			teamDataRef.colliderTransformChunk.Clear();
		}

		internal void InitColliders(ClothProcess cprocess)
		{
			List<ColliderComponent> colliderList = cprocess.cloth.SerializeData.colliderCollisionConstraint.colliderList;
			if (colliderList.Count == 0)
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
			int num = 0;
			for (int i = 0; i < colliderList.Count; i++)
			{
				ColliderComponent colliderComponent = colliderList[i];
				if ((bool)colliderComponent && !cprocess.colliderList.Contains(colliderComponent))
				{
					AddColliderInternal(cprocess, colliderComponent, num, teamDataRef.colliderChunk.startIndex + num, teamDataRef.colliderTransformChunk.startIndex + num);
					teamDataRef.colliderCount++;
					num++;
				}
			}
		}

		internal void UpdateColliders(ClothProcess cprocess)
		{
			if (!isValid)
			{
				return;
			}
			List<ColliderComponent> colliderList = cprocess.cloth.SerializeData.colliderCollisionConstraint.colliderList;
			int num = 0;
			int colliderCapacity = cprocess.ColliderCapacity;
			while (num < colliderCapacity)
			{
				ColliderComponent colliderComponent = cprocess.colliderList[num];
				if ((bool)colliderComponent && !colliderList.Contains(colliderComponent))
				{
					RemoveCollider(colliderComponent, cprocess.TeamId);
				}
				else
				{
					num++;
				}
			}
			foreach (ColliderComponent item in colliderList)
			{
				if ((bool)item && cprocess.GetColliderIndex(item) < 0)
				{
					AddCollider(cprocess, item);
				}
			}
		}

		private void AddCollider(ClothProcess cprocess, ColliderComponent col)
		{
			if (isValid && !(col == null) && cprocess.GetColliderIndex(col) < 0)
			{
				ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(cprocess.TeamId);
				if (!teamDataRef.colliderChunk.IsValid)
				{
					int num = 8;
					teamDataRef.colliderChunk = teamIdArray.AddRange(num, (short)cprocess.TeamId);
					flagArray.AddRange(num, default(ExBitFlag8));
					centerArray.AddRange(num);
					sizeArray.AddRange(num);
					framePositions.AddRange(num);
					frameRotations.AddRange(num);
					frameScales.AddRange(num);
					nowPositions.AddRange(num);
					nowRotations.AddRange(num);
					oldFramePositions.AddRange(num);
					oldFrameRotations.AddRange(num);
					oldPositions.AddRange(num);
					oldRotations.AddRange(num);
					workDataArray.AddRange(num);
					teamDataRef.colliderTransformChunk = MagicaManager.Bone.AddTransform(num, cprocess.TeamId);
					teamDataRef.colliderCount = 0;
					cprocess.colliderList.AddRange(new ColliderComponent[num]);
				}
				else if (teamDataRef.ColliderCount == teamDataRef.colliderChunk.dataLength)
				{
					int num2 = teamDataRef.colliderChunk.dataLength + 8;
					DataChunk colliderChunk = teamDataRef.colliderChunk;
					teamDataRef.colliderChunk = teamIdArray.Expand(colliderChunk, num2);
					flagArray.ExpandAndFill(colliderChunk, num2);
					centerArray.Expand(colliderChunk, num2);
					sizeArray.Expand(colliderChunk, num2);
					framePositions.Expand(colliderChunk, num2);
					frameRotations.Expand(colliderChunk, num2);
					frameScales.Expand(colliderChunk, num2);
					nowPositions.Expand(colliderChunk, num2);
					nowRotations.Expand(colliderChunk, num2);
					oldFramePositions.Expand(colliderChunk, num2);
					oldFrameRotations.Expand(colliderChunk, num2);
					oldPositions.Expand(colliderChunk, num2);
					oldRotations.Expand(colliderChunk, num2);
					workDataArray.Expand(colliderChunk, num2);
					DataChunk colliderTransformChunk = teamDataRef.colliderTransformChunk;
					teamDataRef.colliderTransformChunk = MagicaManager.Bone.Expand(colliderTransformChunk, num2);
					cprocess.colliderList.AddRange(new ColliderComponent[8]);
				}
				int colliderCount = teamDataRef.colliderCount;
				int arrayIndex = teamDataRef.colliderChunk.startIndex + colliderCount;
				int transformIndex = teamDataRef.colliderTransformChunk.startIndex + colliderCount;
				AddColliderInternal(cprocess, col, colliderCount, arrayIndex, transformIndex);
				teamDataRef.colliderCount++;
			}
		}

		internal void RemoveCollider(ColliderComponent col, int teamId)
		{
			if (!isValid)
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
			int colliderCount = teamDataRef.colliderCount;
			if (colliderCount == 0)
			{
				return;
			}
			ClothProcess clothProcess = MagicaManager.Team.GetClothProcess(teamId);
			if (clothProcess == null)
			{
				return;
			}
			int colliderIndex = clothProcess.GetColliderIndex(col);
			if (colliderIndex >= 0)
			{
				int num = teamDataRef.colliderChunk.startIndex + colliderIndex;
				int num2 = teamDataRef.colliderTransformChunk.startIndex + colliderIndex;
				int num3 = colliderCount - 1;
				int num4 = teamDataRef.colliderChunk.startIndex + num3;
				int num5 = teamDataRef.colliderTransformChunk.startIndex + num3;
				if (num < num4)
				{
					flagArray[num] = flagArray[num4];
					teamIdArray[num] = teamIdArray[num4];
					centerArray[num] = centerArray[num4];
					sizeArray[num] = sizeArray[num4];
					framePositions[num] = framePositions[num4];
					frameRotations[num] = frameRotations[num4];
					frameScales[num] = frameScales[num4];
					nowPositions[num] = nowPositions[num4];
					nowRotations[num] = nowRotations[num4];
					oldFramePositions[num] = oldFramePositions[num4];
					oldFrameRotations[num] = oldFrameRotations[num4];
					oldPositions[num] = oldPositions[num4];
					oldRotations[num] = oldRotations[num4];
					flagArray[num4] = default(ExBitFlag8);
					teamIdArray[num4] = 0;
					MagicaManager.Bone.CopyTransform(num5, num2);
					MagicaManager.Bone.SetTransform(null, default(ExBitFlag8), num5, 0);
					clothProcess.colliderList[colliderIndex] = clothProcess.colliderList[num3];
					clothProcess.colliderList[num3] = null;
				}
				else
				{
					flagArray[num] = default(ExBitFlag8);
					teamIdArray[num] = 0;
					MagicaManager.Bone.SetTransform(null, default(ExBitFlag8), num2, 0);
					clothProcess.colliderList[colliderIndex] = null;
				}
				teamDataRef.colliderCount--;
				colliderSet.Remove(col);
			}
		}

		private void AddColliderInternal(ClothProcess cprocess, ColliderComponent col, int index, int arrayIndex, int transformIndex)
		{
			int teamId = cprocess.TeamId;
			teamIdArray[arrayIndex] = (short)teamId;
			ExBitFlag8 value = DataUtility.SetColliderType(default(ExBitFlag8), col.GetColliderType());
			value.SetFlag(16, sw: true);
			value.SetFlag(32, col.isActiveAndEnabled);
			value.SetFlag(64, sw: true);
			value.SetFlag(128, col.IsReverseDirection());
			flagArray[arrayIndex] = value;
			centerArray[arrayIndex] = col.center;
			sizeArray[arrayIndex] = col.GetSize();
			Vector3 position = col.transform.position;
			Quaternion rotation = col.transform.rotation;
			Vector3 localScale = col.transform.localScale;
			framePositions[arrayIndex] = position;
			frameRotations[arrayIndex] = rotation;
			frameScales[arrayIndex] = localScale;
			nowPositions[arrayIndex] = position;
			nowRotations[arrayIndex] = rotation;
			oldFramePositions[arrayIndex] = position;
			oldFrameRotations[arrayIndex] = rotation;
			oldPositions[arrayIndex] = position;
			oldRotations[arrayIndex] = rotation;
			cprocess.colliderList[index] = col;
			col.Register(teamId);
			bool sw = cprocess.IsEnable && value.IsSet(32);
			ExBitFlag8 flag = new ExBitFlag8(1);
			flag.SetFlag(16, sw);
			MagicaManager.Bone.SetTransform(col.transform, flag, transformIndex, teamId);
			colliderSet.Add(col);
		}

		internal void EnableCollider(ColliderComponent col, int teamId, bool sw)
		{
			if (!IsValid())
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
			if (teamDataRef.IsValid)
			{
				ClothProcess clothProcess = MagicaManager.Team.GetClothProcess(teamId);
				int colliderIndex = clothProcess.GetColliderIndex(col);
				if (colliderIndex >= 0)
				{
					int index = teamDataRef.colliderChunk.startIndex + colliderIndex;
					ExBitFlag8 value = flagArray[index];
					value.SetFlag(32, sw);
					value.SetFlag(64, sw: true);
					flagArray[index] = value;
					int index2 = teamDataRef.colliderTransformChunk.startIndex + colliderIndex;
					bool sw2 = clothProcess.IsEnable && value.IsSet(32);
					MagicaManager.Bone.EnableTransform(index2, sw2);
				}
			}
		}

		internal void EnableTeamCollider(int teamId)
		{
			if (!IsValid())
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
			if (teamDataRef.IsValid && teamDataRef.ColliderCount != 0)
			{
				bool isEnable = teamDataRef.IsEnable;
				DataChunk colliderTransformChunk = teamDataRef.colliderTransformChunk;
				for (int i = 0; i < colliderTransformChunk.dataLength; i++)
				{
					int index = teamDataRef.colliderChunk.startIndex + i;
					int index2 = colliderTransformChunk.startIndex + i;
					ExBitFlag8 value = flagArray[index];
					value.SetFlag(64, sw: true);
					flagArray[index] = value;
					bool sw = isEnable && value.IsSet(32);
					MagicaManager.Bone.EnableTransform(index2, sw);
				}
			}
		}

		internal void UpdateParameters(ColliderComponent col, int teamId)
		{
			if (!IsValid())
			{
				return;
			}
			ref TeamManager.TeamData teamDataRef = ref MagicaManager.Team.GetTeamDataRef(teamId);
			if (teamDataRef.IsValid)
			{
				int colliderIndex = MagicaManager.Team.GetClothProcess(teamId).GetColliderIndex(col);
				if (colliderIndex >= 0)
				{
					int index = teamDataRef.colliderChunk.startIndex + colliderIndex;
					ExBitFlag8 flag = flagArray[index];
					flag = DataUtility.SetColliderType(flag, col.GetColliderType());
					flag.SetFlag(128, col.IsReverseDirection());
					flagArray[index] = flag;
					centerArray[index] = col.center;
					sizeArray[index] = math.max(col.GetSize(), 0.0001f);
				}
			}
		}

		internal static void SimulationPreUpdate(DataChunk chunk, ref TeamManager.TeamData tdata, ref InertiaConstraint.CenterData cdata, ref NativeArray<ExBitFlag8> flagArray, ref NativeArray<float3> centerArray, ref NativeArray<float3> framePositions, ref NativeArray<quaternion> frameRotations, ref NativeArray<float3> frameScales, ref NativeArray<float3> oldFramePositions, ref NativeArray<quaternion> oldFrameRotations, ref NativeArray<float3> nowPositions, ref NativeArray<quaternion> nowRotations, ref NativeArray<float3> oldPositions, ref NativeArray<quaternion> oldRotations, ref NativeArray<float3> transformPositionArray, ref NativeArray<quaternion> transformRotationArray, ref NativeArray<float3> transformScaleArray)
		{
			int num = tdata.colliderChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				ExBitFlag8 value = flagArray[num];
				if (value.IsSet(16) && value.IsSet(32))
				{
					float3 v = centerArray[num];
					int num3 = num - tdata.colliderChunk.startIndex;
					int index = tdata.colliderTransformChunk.startIndex + num3;
					float3 value2 = transformPositionArray[index];
					quaternion quaternion2 = transformRotationArray[index];
					float3 float5 = transformScaleArray[index];
					value2 += math.mul(quaternion2, v) * float5;
					framePositions[num] = value2;
					frameRotations[num] = quaternion2;
					frameScales[num] = float5;
					if (tdata.IsReset || value.IsSet(64))
					{
						oldFramePositions[num] = value2;
						oldFrameRotations[num] = quaternion2;
						nowPositions[num] = value2;
						nowRotations[num] = quaternion2;
						oldPositions[num] = value2;
						oldRotations[num] = quaternion2;
						value.SetFlag(64, sw: false);
						flagArray[num] = value;
					}
					else if (tdata.IsInertiaShift || tdata.IsNegativeScaleTeleport)
					{
						float3 pos = oldFramePositions[num];
						quaternion rot = oldFrameRotations[num];
						float3 pos2 = nowPositions[num];
						quaternion rot2 = nowRotations[num];
						float3 pos3 = oldPositions[num];
						quaternion rot3 = oldRotations[num];
						if (tdata.IsNegativeScaleTeleport)
						{
							float4x4 m = cdata.negativeScaleMatrix;
							pos = MathUtility.TransformPoint(in pos, in m);
							rot = MathUtility.TransformRotation(in rot, in m, in tdata.negativeScaleChange);
							pos2 = MathUtility.TransformPoint(in pos2, in m);
							rot2 = MathUtility.TransformRotation(in rot2, in m, in tdata.negativeScaleChange);
							pos3 = MathUtility.TransformPoint(in pos3, in m);
							rot3 = MathUtility.TransformRotation(in rot3, in m, in tdata.negativeScaleChange);
						}
						if (tdata.IsInertiaShift)
						{
							float3 oldPivotPosition = cdata.oldComponentWorldPosition;
							pos = MathUtility.ShiftPosition(in pos, in oldPivotPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
							rot = math.mul(cdata.frameComponentShiftRotation, rot);
							pos2 = MathUtility.ShiftPosition(in pos2, in oldPivotPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
							rot2 = math.mul(cdata.frameComponentShiftRotation, rot2);
							pos3 = MathUtility.ShiftPosition(in pos3, in oldPivotPosition, in cdata.frameComponentShiftVector, in cdata.frameComponentShiftRotation);
							rot3 = math.mul(cdata.frameComponentShiftRotation, rot3);
						}
						oldFramePositions[num] = pos;
						oldFrameRotations[num] = rot;
						nowPositions[num] = pos2;
						nowRotations[num] = rot2;
						oldPositions[num] = pos3;
						oldRotations[num] = rot3;
					}
				}
				num2++;
				num++;
			}
		}

		internal static void SimulationStartStep(ref TeamManager.TeamData tdata, ref InertiaConstraint.CenterData cdata, ref NativeArray<ExBitFlag8> flagArray, ref NativeArray<float3> sizeArray, ref NativeArray<float3> framePositions, ref NativeArray<quaternion> frameRotations, ref NativeArray<float3> frameScales, ref NativeArray<float3> oldFramePositions, ref NativeArray<quaternion> oldFrameRotations, ref NativeArray<float3> nowPositions, ref NativeArray<quaternion> nowRotations, ref NativeArray<float3> oldPositions, ref NativeArray<quaternion> oldRotations, ref NativeArray<WorkData> workDataArray)
		{
			int num = tdata.colliderChunk.startIndex;
			int num2 = 0;
			while (num2 < tdata.colliderChunk.dataLength)
			{
				ExBitFlag8 flag = flagArray[num];
				if (flag.IsSet(16) && flag.IsSet(32))
				{
					float3 float5 = math.lerp(oldFramePositions[num], framePositions[num], tdata.frameInterpolation);
					quaternion q = math.slerp(oldFrameRotations[num], frameRotations[num], tdata.frameInterpolation);
					q = math.normalize(q);
					nowPositions[num] = float5;
					nowRotations[num] = q;
					float3 start = oldPositions[num];
					quaternion q2 = oldRotations[num];
					start = math.lerp(start, float5, cdata.stepMoveInertiaRatio);
					q2 = math.slerp(q2, q, cdata.stepRotationInertiaRatio);
					oldPositions[num] = start;
					oldRotations[num] = math.normalize(q2);
					ColliderType colliderType = DataUtility.GetColliderType(in flag);
					WorkData value = default(WorkData);
					float3 float6 = sizeArray[num];
					float3 x = frameScales[num];
					value.inverseOldRot = math.inverse(q2);
					value.rot = q;
					switch (colliderType)
					{
					case ColliderType.Sphere:
					{
						float num3 = float6.x * math.abs(x.x);
						value.radius = num3;
						AABB aabb = new AABB(math.min(start, float5), math.max(start, float5));
						aabb.Expand(num3);
						value.aabb = aabb;
						value.oldPos.c0 = start;
						value.nextPos.c0 = float5;
						break;
					}
					case ColliderType.CapsuleX_Center:
					case ColliderType.CapsuleY_Center:
					case ColliderType.CapsuleZ_Center:
					case ColliderType.CapsuleX_Start:
					case ColliderType.CapsuleY_Start:
					case ColliderType.CapsuleZ_Start:
					{
						bool num4 = (int)colliderType >= 2 && (int)colliderType <= 4;
						float3 obj;
						switch (colliderType)
						{
						default:
							obj = math.forward();
							break;
						case ColliderType.CapsuleY_Center:
						case ColliderType.CapsuleY_Start:
							obj = math.up();
							break;
						case ColliderType.CapsuleX_Center:
						case ColliderType.CapsuleX_Start:
							obj = math.right();
							break;
						}
						float3 float7 = obj;
						float x2 = math.dot(x, float7);
						float7 *= math.sign(x2);
						float num5 = math.abs(x2);
						if (flag.IsSet(128))
						{
							float7 = -float7;
						}
						float6 *= num5;
						float x3 = float6.x;
						float y = float6.y;
						float z = float6.z;
						float num6 = (num4 ? (z * 0.5f) : 0f);
						float num7 = (num4 ? (z * 0.5f) : (z - x3));
						num6 = math.max(num6 - x3, 0f);
						num7 = math.max(num7 - y, 0f);
						float3 float8 = start + math.mul(q2, float7 * num6);
						float3 float9 = start - math.mul(q2, float7 * num7);
						float3 float10 = float5 + math.mul(q, float7 * num6);
						float3 float11 = float5 - math.mul(q, float7 * num7);
						AABB aabb2 = new AABB(math.min(float8, float10) - x3, math.max(float8, float10) + x3);
						aabb2.Encapsulate(new AABB(math.min(float9, float11) - y, math.max(float9, float11) + y));
						value.aabb = aabb2;
						value.radius = new float2(x3, y);
						value.oldPos = new float3x2(float8, float9);
						value.nextPos = new float3x2(float10, float11);
						break;
					}
					default:
						if (colliderType == ColliderType.Plane)
						{
							float3 v = math.up();
							v *= math.sign(x.y);
							float3 c = math.mul(q, v);
							value.oldPos.c0 = c;
							value.nextPos.c0 = float5;
						}
						break;
					}
					workDataArray[num] = value;
				}
				num2++;
				num++;
			}
		}

		internal static void SimulationEndStep(DataChunk chunk, ref TeamManager.TeamData tdata, ref NativeArray<float3> nowPositions, ref NativeArray<quaternion> nowRotations, ref NativeArray<float3> oldPositions, ref NativeArray<quaternion> oldRotations)
		{
			int num = tdata.colliderChunk.startIndex + chunk.startIndex;
			int num2 = 0;
			while (num2 < chunk.dataLength)
			{
				oldPositions[num] = nowPositions[num];
				oldRotations[num] = nowRotations[num];
				num2++;
				num++;
			}
		}

		internal static void SimulationPostUpdate(ref TeamManager.TeamData tdata, ref NativeArray<float3> framePositions, ref NativeArray<quaternion> frameRotations, ref NativeArray<float3> oldFramePositions, ref NativeArray<quaternion> oldFrameRotations)
		{
			if (tdata.colliderCount != 0 && tdata.IsRunning)
			{
				int num = tdata.colliderChunk.startIndex;
				int num2 = 0;
				while (num2 < tdata.colliderChunk.dataLength)
				{
					oldFramePositions[num] = framePositions[num];
					oldFrameRotations[num] = frameRotations[num];
					num2++;
					num++;
				}
			}
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Collider Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Collider Manager. Invalid.");
			}
			else
			{
				stringBuilder.AppendLine($"Collider Manager. Collider:{colliderSet.Count}");
				stringBuilder.AppendLine("  -flagArray:" + flagArray.ToSummary());
				stringBuilder.AppendLine("  -centerArray:" + centerArray.ToSummary());
				stringBuilder.AppendLine("  -sizeArray:" + sizeArray.ToSummary());
				stringBuilder.AppendLine("  -framePositions:" + framePositions.ToSummary());
				stringBuilder.AppendLine("  -frameRotations:" + frameRotations.ToSummary());
				stringBuilder.AppendLine("  -frameScales:" + frameScales.ToSummary());
				stringBuilder.AppendLine("  -oldFramePositions:" + oldFramePositions.ToSummary());
				stringBuilder.AppendLine("  -oldFrameRotations:" + oldFrameRotations.ToSummary());
				stringBuilder.AppendLine("  -nowPositions:" + nowPositions.ToSummary());
				stringBuilder.AppendLine("  -nowRotations:" + nowRotations.ToSummary());
				stringBuilder.AppendLine("  -oldPositions:" + oldPositions.ToSummary());
				stringBuilder.AppendLine("  -oldRotations:" + oldRotations.ToSummary());
				stringBuilder.AppendLine("[Colliders]");
				int num = teamIdArray?.Count ?? 0;
				for (int i = 0; i < num; i++)
				{
					ExBitFlag8 flag = flagArray[i];
					if (flag.IsSet(16))
					{
						ColliderType colliderType = DataUtility.GetColliderType(in flag);
						stringBuilder.AppendLine($"  [{i}] tid:{teamIdArray[i]}, flag:0x{flag.Value:X}, type:{colliderType}, size:{sizeArray[i]}, cen:{centerArray[i]}");
					}
				}
				stringBuilder.AppendLine("[Collider Names]");
				foreach (ColliderComponent item in colliderSet)
				{
					string text = item?.name ?? "(null)";
					stringBuilder.AppendLine("  " + text);
				}
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
