using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/TRS Shape", ModuleName = "TRS Shape", Description = "Transform,Rotate,Scale a Shape")]
	[HelpURL("https://curvyeditor.com/doclink/cgtrsshape")]
	public class ModifierTRSShape : TRSModuleBase, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Name = "Shape A", ModifiesData = true)]
		public CGModuleInputSlot InShape = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGShape))]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();

		[Obsolete("IOnRequestPath.PathLength and CGDataRequestRasterization.SplineAbsoluteLength are no more needed. SplineInputModuleBase.getPathLength is used instead")]
		public float PathLength
		{
			get
			{
				if (!IsConfigured)
				{
					return 0f;
				}
				return InShape.SourceSlot().OnRequestPathModule.PathLength;
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
				return InShape.SourceSlot().PathProvider.PathIsClosed;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			if (requestedSlot == OutShape)
			{
				CGShape data = InShape.GetData<CGShape>(requests);
				if ((bool)data)
				{
					Matrix4x4 matrix = base.Matrix;
					Matrix4x4 matrix4x = Matrix4x4.TRS(base.Transpose, Quaternion.Euler(base.Rotation), Vector3.one);
					for (int i = 0; i < data.Count; i++)
					{
						data.Position[i] = matrix.MultiplyPoint3x4(data.Position[i]);
						data.Normal[i] = matrix4x.MultiplyVector(data.Normal[i]);
					}
					data.Recalculate();
				}
				return new CGData[1] { data };
			}
			return null;
		}
	}
}
