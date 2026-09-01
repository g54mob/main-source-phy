using System.Collections.Generic;
using UnityEngine;

namespace Shapes;

internal class MpbDisc : MetaMpb, IDashableMpb
{
	internal readonly List<float> alignment;

	internal readonly List<float> angleEnd;

	internal readonly List<float> angleStart;

	internal readonly List<Vector4> colorInnerEnd;

	internal readonly List<Vector4> colorOuterEnd;

	internal readonly List<Vector4> colorOuterStart;

	internal readonly List<float> radius;

	internal readonly List<float> radiusSpace;

	internal readonly List<float> roundCaps;

	internal readonly List<float> scaleMode;

	internal readonly List<float> thickness;

	internal readonly List<float> thicknessSpace;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashOffset_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashShapeModifier_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSize_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSnap_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpace_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashSpacing_003Ek__BackingField;

	private readonly List<float> _003CShapes_002EIDashableMpb_002EdashType_003Ek__BackingField;

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
		Transfer(ShapesMaterialUtils.propAngEnd, angleEnd);
		Transfer(ShapesMaterialUtils.propAngStart, angleStart);
		Transfer(ShapesMaterialUtils.propColorInnerEnd, colorInnerEnd);
		Transfer(ShapesMaterialUtils.propColorOuterEnd, colorOuterEnd);
		Transfer(ShapesMaterialUtils.propColorOuterStart, colorOuterStart);
		Transfer(ShapesMaterialUtils.propRadius, radius);
		Transfer(ShapesMaterialUtils.propRadiusSpace, radiusSpace);
		Transfer(ShapesMaterialUtils.propRoundCaps, roundCaps);
		Transfer(ShapesMaterialUtils.propScaleMode, scaleMode);
		Transfer(ShapesMaterialUtils.propThickness, thickness);
		Transfer(ShapesMaterialUtils.propThicknessSpace, thicknessSpace);
	}

	public MpbDisc()
	{
		List<float> list = MetaMpb.InitList<float>();
		alignment = list;
		List<float> list2 = MetaMpb.InitList<float>();
		angleEnd = list2;
		angleStart = MetaMpb.InitList<float>();
		colorInnerEnd = MetaMpb.InitList<Vector4>();
		colorOuterEnd = MetaMpb.InitList<Vector4>();
		colorOuterStart = MetaMpb.InitList<Vector4>();
		radius = MetaMpb.InitList<float>();
		radiusSpace = MetaMpb.InitList<float>();
		roundCaps = MetaMpb.InitList<float>();
		scaleMode = MetaMpb.InitList<float>();
		thickness = MetaMpb.InitList<float>();
		thicknessSpace = MetaMpb.InitList<float>();
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
