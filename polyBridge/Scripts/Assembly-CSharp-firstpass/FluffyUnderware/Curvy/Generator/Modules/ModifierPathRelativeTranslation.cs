using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Path Relative Translation", ModuleName = "Path Relative Translation", Description = "Translates a path relatively to it's direction, instead of relatively to the world as does the TRS Path module.")]
	[HelpURL("https://curvyeditor.com/doclink/cgpathrelativetranslation")]
	public class ModifierPathRelativeTranslation : CGModule, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path A", ModifiesData = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[SerializeField]
		[Tooltip("The translation amount")]
		private float lateralTranslation;

		public float LateralTranslation
		{
			get
			{
				return lateralTranslation;
			}
			set
			{
				if (lateralTranslation != value)
				{
					lateralTranslation = value;
					base.Dirty = true;
				}
			}
		}

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
					for (int i = 0; i < data.Count; i++)
					{
						Vector3 vector = Vector3.Cross(data.Normal[i], data.Direction[i]) * lateralTranslation;
						data.Position[i].x = data.Position[i].x + vector.x;
						data.Position[i].y = data.Position[i].y + vector.y;
						data.Position[i].z = data.Position[i].z + vector.z;
					}
					data.Recalculate();
				}
				return new CGData[1] { data };
			}
			return null;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 230f;
			Properties.LabelWidth = 165f;
		}

		public override void Reset()
		{
			base.Reset();
			LateralTranslation = 0f;
		}
	}
}
