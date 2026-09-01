using UnityEngine;

namespace VolumetricFogAndMist
{
	[ExecuteInEditMode]
	public class FogOfWarHole : MonoBehaviour
	{
		public enum HoleShape
		{
			Disc = 0,
			Box = 1
		}

		public HoleShape shape;

		[Tooltip("The transparency of the fog")]
		[Range(0f, 1f)]
		public float alpha;

		[Tooltip("The smoothness/harshness of the hole's border")]
		[Range(0f, 1f)]
		public float smoothness = 0.85f;

		private HoleShape lastShape;

		private Vector3 lastPosition = Vector3.zero;

		private Vector3 lastScale;

		private void Start()
		{
			StampHole(base.transform.position, shape, base.transform.localScale.x, base.transform.localScale.z);
		}

		private void RestoreHole(Vector3 position, HoleShape shape, float sizeX, float sizeZ)
		{
			VolumetricFog instance = VolumetricFog.instance;
			if (!(instance == null))
			{
				instance.fogOfWarEnabled = true;
				switch (shape)
				{
				case HoleShape.Box:
					instance.ResetFogOfWarAlpha(position, sizeX * 0.5f, sizeZ * 0.5f);
					break;
				case HoleShape.Disc:
					instance.ResetFogOfWarAlpha(position, Mathf.Max(sizeX, sizeZ) * 0.5f);
					break;
				}
			}
		}

		private void StampHole(Vector3 position, HoleShape shape, float sizeX, float sizeZ)
		{
			VolumetricFog instance = VolumetricFog.instance;
			if (!(instance == null))
			{
				instance.fogOfWarEnabled = true;
				switch (shape)
				{
				case HoleShape.Box:
					instance.SetFogOfWarAlpha(new Bounds(position, new Vector3(sizeX, 0f, sizeZ)), alpha, false, 0f, smoothness, 0f, 0f);
					break;
				case HoleShape.Disc:
					instance.SetFogOfWarAlpha(position, Mathf.Max(sizeX, sizeZ) * 0.5f, alpha, false, 0f, smoothness, 0f, 0f);
					break;
				}
				lastPosition = position;
				lastShape = shape;
				lastScale = base.transform.localScale;
			}
		}

		public void Refresh()
		{
			RestoreHole(lastPosition, lastShape, lastScale.x, lastScale.z);
			StampHole(base.transform.position, shape, base.transform.localScale.x, base.transform.localScale.z);
		}
	}
}
