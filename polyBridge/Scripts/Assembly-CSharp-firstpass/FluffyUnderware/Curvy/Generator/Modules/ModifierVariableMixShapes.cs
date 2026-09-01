using System;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Variable Mix Shapes", ModuleName = "Variable Mix Shapes", Description = "Interpolates between two shapes in a way that varies along the shape extrusion")]
	[HelpURL("https://curvyeditor.com/doclink/cgvariablemixshapes")]
	public class ModifierVariableMixShapes : CGModule, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Name = "Shape A")]
		public CGModuleInputSlot InShapeA = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Name = "Shape B")]
		public CGModuleInputSlot InShapeB = new CGModuleInputSlot();

		[HideInInspector]
		[ShapeOutputSlotInfo(OutputsVariableShape = true, Array = true, ArrayType = SlotInfo.SlotArrayType.Hidden)]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();

		[Label("Mix Curve", "Mix between the shapes. Values (Y axis) between -1 for Shape A and 1 for Shape B. Times (X axis) between 0 for extrusion start and 1 for extrusion end")]
		[SerializeField]
		private AnimationCurve m_MixCurve = AnimationCurve.Linear(0f, -1f, 1f, 1f);

		[Obsolete("IOnRequestPath.PathLength and CGDataRequestRasterization.SplineAbsoluteLength are no more needed. SplineInputModuleBase.getPathLength is used instead")]
		public float PathLength
		{
			get
			{
				if (!IsConfigured)
				{
					return 0f;
				}
				return Mathf.Max(InShapeA.SourceSlot().OnRequestPathModule.PathLength, InShapeB.SourceSlot().OnRequestPathModule.PathLength);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				if (!IsConfigured)
				{
					return false;
				}
				if (InShapeA.SourceSlot().PathProvider.PathIsClosed)
				{
					return InShapeB.SourceSlot().PathProvider.PathIsClosed;
				}
				return false;
			}
		}

		public AnimationCurve MixCurve
		{
			get
			{
				return m_MixCurve;
			}
			set
			{
				m_MixCurve = value;
				base.Dirty = true;
			}
		}

		public override void Reset()
		{
			base.Reset();
			m_MixCurve = AnimationCurve.Linear(0f, -1f, 1f, 1f);
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestShapeRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestShapeRasterization>(ref requests);
			if (!requestParameter)
			{
				return null;
			}
			int num = requestParameter.PathF.Length;
			CGData[] array = new CGData[num];
			for (int i = 0; i < num; i++)
			{
				float mix = MixCurve.Evaluate(requestParameter.PathF[i]);
				array[i] = ModifierMixShapes.MixShapes(InShapeA.GetData<CGShape>(requests), InShapeB.GetData<CGShape>(requests), mix, UIMessages, i != 0);
			}
			return array;
		}
	}
}
