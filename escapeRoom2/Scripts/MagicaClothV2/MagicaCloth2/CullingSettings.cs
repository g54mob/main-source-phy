using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class CullingSettings : IDataValidate
	{
		public enum CameraCullingMode
		{
			Off = 0,
			Reset = 10,
			Keep = 20,
			AnimatorLinkage = 30
		}

		public enum CameraCullingMethod
		{
			AutomaticRenderer = 0,
			ManualRenderer = 10
		}

		public struct CullingParams
		{
			public bool useDistanceCulling;

			public float distanceCullingLength;

			public float distanceCullingFadeRatio;

			public void Convert(CullingSettings cullingSettings)
			{
				useDistanceCulling = cullingSettings.distanceCullingLength.use;
				distanceCullingLength = cullingSettings.distanceCullingLength.value;
				distanceCullingFadeRatio = cullingSettings.distanceCullingFadeRatio;
			}
		}

		public CameraCullingMode cameraCullingMode = CameraCullingMode.AnimatorLinkage;

		public CameraCullingMethod cameraCullingMethod;

		public List<Renderer> cameraCullingRenderers = new List<Renderer>();

		public CheckSliderSerializeData distanceCullingLength;

		[Range(0f, 1f)]
		public float distanceCullingFadeRatio;

		public GameObject distanceCullingReferenceObject;

		public CullingSettings()
		{
			distanceCullingLength = new CheckSliderSerializeData(use: false, 30f);
			distanceCullingFadeRatio = 0.2f;
		}

		public void DataValidate()
		{
			distanceCullingLength.DataValidate(0f, 100f);
			distanceCullingFadeRatio = Mathf.Clamp01(distanceCullingFadeRatio);
		}

		public CullingSettings Clone()
		{
			return new CullingSettings
			{
				cameraCullingMode = cameraCullingMode,
				cameraCullingMethod = cameraCullingMethod,
				cameraCullingRenderers = new List<Renderer>(cameraCullingRenderers),
				distanceCullingLength = distanceCullingLength.Clone(),
				distanceCullingFadeRatio = distanceCullingFadeRatio,
				distanceCullingReferenceObject = distanceCullingReferenceObject
			};
		}

		public override int GetHashCode()
		{
			return 0;
		}
	}
}
