using UnityEngine;

namespace VLB
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[SelectionBase]
	[HelpURL("http://saladgamer.com/vlb-doc/comp-lightbeam-hd/")]
	[AddComponentMenu("VLB/HD/Volumetric Light Beam HD (2D)")]
	public class VolumetricLightBeamHD2D : VolumetricLightBeamHD
	{
		[SerializeField]
		private int m_SortingLayerID;

		[SerializeField]
		private int m_SortingOrder;

		public int sortingLayerID
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string sortingLayerName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int sortingOrder
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public override Dimensions GetDimensions()
		{
			return default(Dimensions);
		}

		public override bool DoesSupportSorting2D()
		{
			return false;
		}

		public override int GetSortingLayerID()
		{
			return 0;
		}

		public override int GetSortingOrder()
		{
			return 0;
		}

		public override void CopyPropsFrom(VolumetricLightBeamAbstractBase beamSrc, BeamProps beamProps)
		{
		}
	}
}
