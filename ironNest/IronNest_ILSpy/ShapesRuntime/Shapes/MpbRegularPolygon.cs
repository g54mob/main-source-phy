using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbRegularPolygon : MetaMpb, IFillableMpb, IDashableMpb
{
	internal readonly List<float> alignment;

	internal readonly List<float> angle;

	internal readonly List<float> hollow;

	internal readonly List<float> radius;

	internal readonly List<float> radiusSpace;

	internal readonly List<float> roundness;

	internal readonly List<float> scaleMode;

	internal readonly List<float> sides;

	internal readonly List<float> thickness;

	internal readonly List<float> thicknessSpace;

	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField;

	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField;

	private readonly List<Vector4> _003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillColorEnd => _003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillEnd => _003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField;

	List<float> IFillableMpb.fillSpace => _003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField;

	List<Vector4> IFillableMpb.fillStart => _003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField;

	List<float> IFillableMpb.fillType => _003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField;

	List<float> IDashableMpb.dashOffset => _003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField;

	List<float> IDashableMpb.dashShapeModifier => _003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField;

	List<float> IDashableMpb.dashSize => _003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField;

	List<float> IDashableMpb.dashSnap => _003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField;

	List<float> IDashableMpb.dashSpace => _003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField;

	List<float> IDashableMpb.dashSpacing => _003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField;

	List<float> IDashableMpb.dashType => _003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField;

	protected override void TransferShapeProperties()
	{
		Transfer(ShapesMaterialUtils.propAlignment, alignment);
		Transfer(ShapesMaterialUtils.propAng, angle);
		Transfer(ShapesMaterialUtils.propBorder, hollow);
		Transfer(ShapesMaterialUtils.propRadius, radius);
		Transfer(ShapesMaterialUtils.propRadiusSpace, radiusSpace);
		Transfer(ShapesMaterialUtils.propRoundness, roundness);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propSides, sides);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbRegularPolygon()
	{
		List<float> list = MetaMpb.InitList<float>();
		alignment = list;
		List<float> list2 = MetaMpb.InitList<float>();
		angle = list2;
		hollow = MetaMpb.InitList<float>();
		radius = MetaMpb.InitList<float>();
		radiusSpace = MetaMpb.InitList<float>();
		roundness = MetaMpb.InitList<float>();
		scaleMode = MetaMpb.InitList<float>();
		sides = MetaMpb.InitList<float>();
		thickness = MetaMpb.InitList<float>();
		thicknessSpace = MetaMpb.InitList<float>();
		_003CShapes_002EIFillableMpb_002EfillColorEnd_003Ek__BackingField = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillEnd_003Ek__BackingField = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillSpace_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIFillableMpb_002EfillStart_003Ek__BackingField = MetaMpb.InitList<Vector4>();
		_003CShapes_002EIFillableMpb_002EfillType_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField = MetaMpb.InitList<float>();
		_003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField = MetaMpb.InitList<float>();
		base._002Ector();
	}
}
