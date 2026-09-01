using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Input/Spline Path", ModuleName = "Input Spline Path", Description = "Spline Path")]
	[HelpURL("https://curvyeditor.com/doclink/cginputsplinepath")]
	public class InputSplinePath : SplineInputModuleBase, IExternalInput, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot Path = new CGModuleOutputSlot();

		[Tab("General", Sort = 0)]
		[SerializeField]
		[CGResourceManager("Spline")]
		[FieldCondition("m_Spline", null, false, ActionAttribute.ActionEnum.ShowWarning, "Create or assign spline", ActionAttribute.ActionPositionEnum.Below)]
		private CurvySpline m_Spline;

		public CurvySpline Spline
		{
			get
			{
				return m_Spline;
			}
			set
			{
				if (m_Spline != value)
				{
					m_Spline = value;
					OnSplineAssigned();
					ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public bool SupportsIPE => false;

		protected override CurvySpline InputSpline
		{
			get
			{
				return Spline;
			}
			set
			{
				Spline = value;
			}
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			CGDataRequestRasterization requestParameter = CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests);
			CGDataRequestMetaCGOptions requestParameter2 = CGModule.GetRequestParameter<CGDataRequestMetaCGOptions>(ref requests);
			if ((bool)requestParameter2)
			{
				if (requestParameter2.CheckMaterialID)
				{
					requestParameter2.CheckMaterialID = false;
					UIMessages.Add("MaterialID option not supported!");
				}
				if (requestParameter2.IncludeControlPoints)
				{
					requestParameter2.IncludeControlPoints = false;
					UIMessages.Add("IncludeCP option not supported!");
				}
			}
			if (!requestParameter || requestParameter.RasterizedRelativeLength == 0f)
			{
				return null;
			}
			CGData splineData = GetSplineData(Spline, fullPath: true, requestParameter, requestParameter2);
			return new CGData[1] { splineData };
		}

		public override void OnTemplateCreated()
		{
			base.OnTemplateCreated();
			if ((bool)Spline && !IsManagedResource(Spline))
			{
				Spline = null;
			}
		}
	}
}
