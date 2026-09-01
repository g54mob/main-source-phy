using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Torus")]
	public class Torus : ShapeRenderer
	{
		[SerializeField]
		private float radius;

		[SerializeField]
		private float thickness;

		[SerializeField]
		private ThicknessSpace thicknessSpace;

		[SerializeField]
		private ThicknessSpace radiusSpace;

		[SerializeField]
		private AngularUnit angUnitInput;

		[SerializeField]
		private float angRadiansStart;

		[SerializeField]
		private float angRadiansEnd;

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

		public float Thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public ThicknessSpace ThicknessSpace
		{
			get
			{
				return default(ThicknessSpace);
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

		public float AngRadiansStart
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float AngRadiansEnd
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		internal override bool HasDetailLevels => false;

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
