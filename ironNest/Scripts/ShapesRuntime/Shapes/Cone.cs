using System;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	[AddComponentMenu("Shapes/Cone")]
	public class Cone : ShapeRenderer
	{
		[SerializeField]
		private float radius;

		[SerializeField]
		private float length;

		[SerializeField]
		private ThicknessSpace sizeSpace;

		[SerializeField]
		private bool fillCap;

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

		public float Length
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("this property is obsolete I'm sorry! this was a typo, please use SizeSpace instead!", true)]
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

		public bool FillCap
		{
			get
			{
				return false;
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
