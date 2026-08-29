using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class WindManager : IManager, IDisposable, IValid
	{
		public struct WindData
		{
			public BitField32 flag;

			public MagicaWindZone.Mode mode;

			public float3 size;

			public float main;

			public float turbulence;

			public float zoneVolume;

			public float3 worldWindDirection;

			public float3 worldPositin;

			public quaternion worldRotation;

			public float3 worldScale;

			public float4x4 worldToLocalMatrix;

			public float4x4 attenuation;

			public bool IsValid()
			{
				return flag.IsSet(0);
			}

			public bool IsEnable()
			{
				return flag.IsSet(1);
			}

			public bool IsAddition()
			{
				return flag.IsSet(2);
			}
		}

		public const int Flag_Valid = 0;

		public const int Flag_Enable = 1;

		public const int Flag_Addition = 2;

		public ExNativeArray<WindData> windDataArray;

		private bool isValid;

		private Dictionary<int, MagicaWindZone> windZoneDict = new Dictionary<int, MagicaWindZone>();

		public int WindCount => windDataArray?.Count ?? 0;

		public void Dispose()
		{
			isValid = false;
			windDataArray?.Dispose();
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			Dispose();
			windDataArray = new ExNativeArray<WindData>(64);
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		public int AddWind(MagicaWindZone windZone)
		{
			if (!isValid || windZone == null)
			{
				return -1;
			}
			WindData data = default(WindData);
			data.flag.SetBits(0, value: true);
			DataChunk dataChunk = windDataArray.Add(data);
			windZoneDict.Add(dataChunk.startIndex, windZone);
			return dataChunk.startIndex;
		}

		public void RemoveWind(int windId)
		{
			if (isValid && windId >= 0)
			{
				DataChunk chunk = new DataChunk(windId);
				windDataArray.RemoveAndFill(chunk);
				windZoneDict.Remove(chunk.startIndex);
			}
		}

		public void SetEnable(int windId, bool sw)
		{
			if (isValid && windId >= 0)
			{
				WindData value = windDataArray[windId];
				value.flag.SetBits(1, sw);
				windDataArray[windId] = value;
			}
		}

		internal void AlwaysWindUpdate()
		{
			foreach (KeyValuePair<int, MagicaWindZone> item in windZoneDict)
			{
				int key = item.Key;
				MagicaWindZone value = item.Value;
				if (key >= 0 && !(value == null))
				{
					Transform transform = value.transform;
					ref WindData reference = ref windDataArray.GetRef(key);
					reference.mode = value.mode;
					switch (value.mode)
					{
					case MagicaWindZone.Mode.BoxDirection:
						reference.size = value.size;
						break;
					case MagicaWindZone.Mode.SphereDirection:
					case MagicaWindZone.Mode.SphereRadial:
						reference.size = value.radius;
						break;
					}
					reference.main = value.main;
					reference.turbulence = value.turbulence;
					reference.flag.SetBits(2, value.IsAddition());
					reference.worldPositin = transform.position;
					reference.worldRotation = transform.rotation;
					reference.worldScale = transform.lossyScale;
					reference.worldToLocalMatrix = transform.worldToLocalMatrix;
					float zoneVolume = 0f;
					switch (reference.mode)
					{
					case MagicaWindZone.Mode.GlobalDirection:
						zoneVolume = float.MaxValue;
						break;
					case MagicaWindZone.Mode.BoxDirection:
					{
						float3 float5 = reference.size * reference.worldScale;
						zoneVolume = float5.x * float5.y * float5.z;
						break;
					}
					case MagicaWindZone.Mode.SphereDirection:
					case MagicaWindZone.Mode.SphereRadial:
					{
						float num = reference.size.x * reference.worldScale.x;
						zoneVolume = 1.3333334f * num * num * num * MathF.PI;
						break;
					}
					}
					reference.zoneVolume = zoneVolume;
					if (value.IsDirection())
					{
						reference.worldWindDirection = value.GetWindDirection();
					}
					else
					{
						reference.attenuation = DataUtility.ConvertAnimationCurve(value.attenuation);
					}
				}
			}
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Wind Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Wind Manager. Invalid.");
			}
			else
			{
				stringBuilder.AppendLine($"Wind Manager. Count:{WindCount}");
				int windCount = WindCount;
				for (int i = 0; i < windCount; i++)
				{
					WindData windData = windDataArray[i];
					if (windData.flag.IsSet(0))
					{
						stringBuilder.AppendLine($"  [{i}] flag:0x{windData.flag.Value:X}, mode:{windData.mode}");
					}
				}
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
