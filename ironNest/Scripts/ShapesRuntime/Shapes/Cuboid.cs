using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Cuboid")]
	public class Cuboid : ShapeRenderer
	{
		[SerializeField]
		private Vector3 size;

		[SerializeField]
		private ThicknessSpace sizeSpace;

		public Vector3 Size
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public ThicknessSpace SizeSpace
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
