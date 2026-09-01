using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbPolygon : MetaMpb, IFillableMpb
{
	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField;

	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField;

	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillColorEnd => _003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillEnd => _003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField;

	List<float> IFillableMpb.fillSpace => _003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillStart => _003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField;

	List<float> IFillableMpb.fillType => _003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField;

	protected override void TransferShapeProperties()
	{
	}

	public MpbPolygon()
	{
		List<Vector4> list = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField = list;
		List<Vector4> list2 = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField = list2;
		_003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
