using System;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class ReductionSettings : IDataValidate
	{
		[Range(0f, 0.2f)]
		public float simpleDistance;

		[Range(0f, 0.2f)]
		public float shapeDistance;

		public bool IsEnabled => true;

		public float GetMaxConnectionDistance()
		{
			return math.max(math.max(0.001f, simpleDistance), shapeDistance);
		}

		public ReductionSettings Clone()
		{
			return new ReductionSettings
			{
				simpleDistance = simpleDistance,
				shapeDistance = shapeDistance
			};
		}

		public void DataValidate()
		{
			simpleDistance = Mathf.Clamp(simpleDistance, 0f, 0.2f);
			shapeDistance = Mathf.Clamp(shapeDistance, 0f, 0.2f);
		}

		public override int GetHashCode()
		{
			return 0 + simpleDistance.GetHashCode() + shapeDistance.GetHashCode();
		}

		public override string ToString()
		{
			return $"ReductionSettings. sameDist:{0.001f}, simpleDist:{simpleDistance}, shapeDist:{shapeDistance} maxStep:{100}";
		}
	}
}
