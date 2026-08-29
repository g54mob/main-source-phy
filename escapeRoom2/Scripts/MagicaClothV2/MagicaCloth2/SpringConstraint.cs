using System;
using UnityEngine;

namespace MagicaCloth2
{
	public class SpringConstraint : IDisposable
	{
		[Serializable]
		public class SerializeData : IDataValidate
		{
			public bool useSpring;

			[Range(0.001f, 0.2f)]
			public float springPower;

			[Range(0f, 0.5f)]
			public float limitDistance;

			[Range(0f, 1f)]
			public float normalLimitRatio;

			[Range(0f, 1f)]
			public float springNoise;

			public SerializeData()
			{
				useSpring = true;
				springPower = 0.04f;
				limitDistance = 0.1f;
				normalLimitRatio = 1f;
				springNoise = 0f;
			}

			public void DataValidate()
			{
				springPower = Math.Clamp(springPower, 0.001f, 1f);
				limitDistance = Mathf.Max(limitDistance, 0f);
				normalLimitRatio = Mathf.Clamp01(normalLimitRatio);
				springNoise = Mathf.Clamp01(springNoise);
			}

			public SerializeData Clone()
			{
				return new SerializeData
				{
					useSpring = useSpring,
					springPower = springPower,
					limitDistance = limitDistance,
					normalLimitRatio = normalLimitRatio,
					springNoise = springNoise
				};
			}
		}

		public struct SpringConstraintParams
		{
			public float springPower;

			public float limitDistance;

			public float normalLimitRatio;

			public float springNoise;

			public void Convert(SerializeData sdata, ClothProcess.ClothType clothType)
			{
				springPower = ((clothType == ClothProcess.ClothType.BoneSpring && sdata.useSpring) ? sdata.springPower : 0f);
				limitDistance = sdata.limitDistance;
				normalLimitRatio = sdata.normalLimitRatio;
				springNoise = sdata.springNoise;
			}
		}

		public void Dispose()
		{
		}
	}
}
