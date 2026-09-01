using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Mix Paths", ModuleName = "Mix Paths", Description = "Interpolates between two paths")]
	[HelpURL("https://curvyeditor.com/doclink/cgmixpaths")]
	public class ModifierMixPaths : CGModule, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path A")]
		public CGModuleInputSlot InPathA = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path B")]
		public CGModuleInputSlot InPathB = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGPath))]
		public CGModuleOutputSlot OutPath = new CGModuleOutputSlot();

		[SerializeField]
		[RangeEx(-1f, 1f, "", "", Tooltip = "Mix between the paths. Values between -1 for Path A and 1 for Path B")]
		private float m_Mix;

		public float Mix
		{
			get
			{
				return m_Mix;
			}
			set
			{
				if (m_Mix != value)
				{
					m_Mix = value;
				}
				base.Dirty = true;
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
				return Mathf.Max(InPathA.SourceSlot().OnRequestPathModule.PathLength, InPathB.SourceSlot().OnRequestPathModule.PathLength);
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
				if (InPathA.SourceSlot().PathProvider.PathIsClosed)
				{
					return InPathB.SourceSlot().PathProvider.PathIsClosed;
				}
				return false;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 200f;
			Properties.LabelWidth = 50f;
		}

		public override void Reset()
		{
			base.Reset();
			Mix = 0f;
		}

		public CGData[] OnSlotDataRequest(CGModuleInputSlot requestedBy, CGModuleOutputSlot requestedSlot, params CGDataRequestParameter[] requests)
		{
			if (!CGModule.GetRequestParameter<CGDataRequestRasterization>(ref requests))
			{
				return null;
			}
			CGPath data = InPathA.GetData<CGPath>(requests);
			CGPath data2 = InPathB.GetData<CGPath>(requests);
			return new CGData[1] { MixPath(data, data2, Mix, UIMessages) };
		}

		public static CGPath MixPath(CGPath pathA, CGPath pathB, float mix, [NotNull] List<string> warningsContainer)
		{
			if (pathA == null)
			{
				return pathB;
			}
			if (pathB == null)
			{
				return pathA;
			}
			int num = Mathf.Max(pathA.Count, pathB.Count);
			CGPath cGPath = new CGPath();
			cGPath.Direction = new Vector3[num];
			ModifierMixShapes.InterpolateShape(cGPath, pathA, pathB, mix, warningsContainer);
			float t = (mix + 1f) * 0.5f;
			Vector3[] array = new Vector3[num];
			if (pathA.Count == num)
			{
				for (int i = 0; i < num; i++)
				{
					float frag;
					int fIndex = pathB.GetFIndex(pathA.F[i], out frag);
					Vector3 b = Vector3.SlerpUnclamped(pathB.Direction[fIndex], pathB.Direction[fIndex + 1], frag);
					array[i] = Vector3.SlerpUnclamped(pathA.Direction[i], b, t);
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					float frag2;
					int fIndex2 = pathA.GetFIndex(pathB.F[j], out frag2);
					Vector3 a = Vector3.SlerpUnclamped(pathA.Direction[fIndex2], pathA.Direction[fIndex2 + 1], frag2);
					array[j] = Vector3.SlerpUnclamped(a, pathB.Direction[j], t);
				}
			}
			cGPath.Direction = array;
			return cGPath;
		}
	}
}
