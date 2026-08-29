using System;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class WindSettings : IValid, IDataValidate
	{
		[Range(0f, 2f)]
		public float influence = 1f;

		[Range(0f, 2f)]
		public float frequency = 1f;

		[Range(0f, 2f)]
		public float turbulence = 1f;

		[Range(0f, 1f)]
		public float blend = 0.7f;

		[Range(0f, 1f)]
		public float synchronization = 0.7f;

		[Range(0f, 1f)]
		public float depthWeight;

		[Range(0f, 10f)]
		public float movingWind;

		public bool IsValid()
		{
			return influence > 1E-08f;
		}

		public void DataValidate()
		{
			influence = Mathf.Clamp(influence, 0f, 2f);
			frequency = Mathf.Clamp(frequency, 0f, 2f);
			turbulence = Mathf.Clamp(turbulence, 0f, 2f);
			blend = Mathf.Clamp01(blend);
			synchronization = Mathf.Clamp01(synchronization);
			depthWeight = Mathf.Clamp01(depthWeight);
			movingWind = Mathf.Clamp(movingWind, 0f, 10f);
		}

		public WindSettings Clone()
		{
			return new WindSettings
			{
				influence = influence,
				frequency = frequency,
				turbulence = turbulence,
				blend = blend,
				synchronization = synchronization,
				depthWeight = depthWeight,
				movingWind = movingWind
			};
		}
	}
}
