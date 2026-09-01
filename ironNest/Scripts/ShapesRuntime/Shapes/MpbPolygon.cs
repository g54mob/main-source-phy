using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal class MpbPolygon : MetaMpb, IFillableMpb
	{
		List<Vector4> IFillableMpb.fillColorEnd
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<Vector4> IFillableMpb.fillEnd
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IFillableMpb.fillSpace
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<Vector4> IFillableMpb.fillStart
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		List<float> IFillableMpb.fillType
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		protected override void TransferShapeProperties()
		{
		}
	}
}
