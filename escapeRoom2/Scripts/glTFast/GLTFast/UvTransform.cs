using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast
{
	internal struct UvTransform
	{
		public float rotation;

		public float2 scale;

		public static UvTransform FromMaterial(Material material, int scaleTransformPropertyId, int rotationPropertyId)
		{
			Vector4 vector = material.GetVector(scaleTransformPropertyId);
			Vector4 vector2 = material.GetVector(rotationPropertyId);
			return FromMatrix(new float2x2(vector.x, vector.y, vector2.x, vector2.y));
		}

		public static UvTransform FromMatrix(float2x2 scaleRotation)
		{
			float2 output;
			float2 output2;
			UvTransform result = new UvTransform
			{
				scale = new float2(Mathematics.Normalize(new float2(scaleRotation.c0.x, scaleRotation.c1.y), out output), Mathematics.Normalize(new float2(scaleRotation.c0.y, scaleRotation.c1.x), out output2))
			};
			float num = math.acos(output.x);
			if (output2.x < 0f)
			{
				num = MathF.PI * 2f - num;
			}
			result.rotation = num * 57.29578f;
			return result;
		}
	}
}
