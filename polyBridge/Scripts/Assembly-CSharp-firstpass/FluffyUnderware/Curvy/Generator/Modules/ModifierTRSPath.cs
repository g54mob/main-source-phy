using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/TRS Path", ModuleName = "TRS Path", Description = "Transform,Rotate,Scale a Path")]
	[HelpURL("https://curvyeditor.com/doclink/cgtrspath")]
	public class ModifierTRSPath : TRSModuleBase, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path A", ModifiesData = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[Obsolete("IOnRequestPath.PathLength and CGDataRequestRasterization.SplineAbsoluteLength are no more needed. SplineInputModuleBase.getPathLength is used instead")]
		public float PathLength
		{
			get
			{
				if (!IsConfigured)
				{
					return 0f;
				}
				return InPath.SourceSlot().OnRequestPathModule.PathLength;
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
				return InPath.SourceSlot().PathProvider.PathIsClosed;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			if (requestedSlot == OutPath)
			{
				CGPath data = InPath.GetData<CGPath>(requests);
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
