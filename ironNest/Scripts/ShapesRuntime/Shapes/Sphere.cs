using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Sphere")]
	public class Sphere : ShapeRenderer
	{
		[SerializeField]
		private float radius;

		[SerializeField]
		private ThicknessSpace radiusSpace;

		public float Radius
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public ThicknessSpace RadiusSpace
		{
			get
			{
				return default(ThicknessSpace);
			}
			set
			{
			}
		}

		internal override bool HasDetailLevels => false;

		internal override bool HasScaleModes => false;

		private protected override void SetAllMaterialProperties()
		{
		}

		private protected override void ShapeClampRanges()
		{
		}

		private protected override void GetMaterials(Material[] mats)
		{
		}

		private protected override Mesh GetInitialMeshAsset()
		{
			return null;
		}

		private protected override Bounds GetUnpaddedLocalBounds_Internal()
		{
			return default(Bounds);
		}
	}
}
