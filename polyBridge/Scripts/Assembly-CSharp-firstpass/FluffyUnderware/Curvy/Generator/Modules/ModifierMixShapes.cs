using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.DevTools;
using JetBrains.Annotations;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Modifier/Mix Shapes", ModuleName = "Mix Shapes", Description = "Interpolates between two shapes")]
	[HelpURL("https://curvyeditor.com/doclink/cgmixshapes")]
	public class ModifierMixShapes : CGModule, IOnRequestPath, IOnRequestProcessing, IPathProvider
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Name = "Shape A")]
		public CGModuleInputSlot InShapeA = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Name = "Shape B")]
		public CGModuleInputSlot InShapeB = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGShape))]
		public CGModuleOutputSlot OutShape = new CGModuleOutputSlot();

		[SerializeField]
		[RangeEx(-1f, 1f, "", "", Tooltip = "Mix between the shapes. Values between -1 for Shape A and 1 for Shape B")]
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
			CGShape data = InShapeA.GetData<CGShape>(requests);
			CGShape data2 = InShapeB.GetData<CGShape>(requests);
			CGShape cGShape = MixShapes(data, data2, Mix, UIMessages);
			return new CGData[1] { cGShape };
		}

		public static CGShape MixShapes(CGShape shapeA, CGShape shapeB, float mix, [NotNull] List<string> warningsContainer, bool ignoreWarnings = false)
		{
			if (shapeA == null)
			{
				return shapeB;
			}
			if (shapeB == null)
			{
				return shapeA;
			}
			CGShape cGShape = new CGShape();
			InterpolateShape(cGShape, shapeA, shapeB, mix, warningsContainer, ignoreWarnings);
			return cGShape;
		}

		public static void InterpolateShape([NotNull] CGShape resultShape, CGShape shapeA, CGShape shapeB, float mix, [NotNull] List<string> warningsContainer, bool ignoreWarnings = false)
		{
			float num = (mix + 1f) * 0.5f;
			int num2 = Mathf.Max(shapeA.Count, shapeB.Count);
			CGShape cGShape = ((shapeA.Count == num2) ? shapeA : shapeB);
			Vector3[] array = new Vector3[num2];
			Vector3[] array2 = new Vector3[num2];
			if (cGShape == shapeA)
			{
				Vector3 vector = default(Vector3);
				for (int i = 0; i < num2; i++)
				{
					float frag;
					int fIndex = shapeB.GetFIndex(shapeA.F[i], out frag);
					vector.x = shapeB.Position[fIndex].x + (shapeB.Position[fIndex + 1].x - shapeB.Position[fIndex].x) * frag;
					vector.y = shapeB.Position[fIndex].y + (shapeB.Position[fIndex + 1].y - shapeB.Position[fIndex].y) * frag;
					vector.z = shapeB.Position[fIndex].z + (shapeB.Position[fIndex + 1].z - shapeB.Position[fIndex].z) * frag;
					array[i].x = shapeA.Position[i].x + (vector.x - shapeA.Position[i].x) * num;
					array[i].y = shapeA.Position[i].y + (vector.y - shapeA.Position[i].y) * num;
					array[i].z = shapeA.Position[i].z + (vector.z - shapeA.Position[i].z) * num;
					Vector3 b = Vector3.SlerpUnclamped(shapeB.Normal[fIndex], shapeB.Normal[fIndex + 1], frag);
					array2[i] = Vector3.SlerpUnclamped(shapeA.Normal[i], b, num);
				}
			}
			else
			{
				Vector3 vector2 = default(Vector3);
				for (int j = 0; j < num2; j++)
				{
					float frag2;
					int fIndex2 = shapeA.GetFIndex(shapeB.F[j], out frag2);
					vector2.x = shapeA.Position[fIndex2].x + (shapeA.Position[fIndex2 + 1].x - shapeA.Position[fIndex2].x) * frag2;
					vector2.y = shapeA.Position[fIndex2].y + (shapeA.Position[fIndex2 + 1].y - shapeA.Position[fIndex2].y) * frag2;
					vector2.z = shapeA.Position[fIndex2].z + (shapeA.Position[fIndex2 + 1].z - shapeA.Position[fIndex2].z) * frag2;
					array[j].x = vector2.x + (shapeB.Position[j].x - vector2.x) * num;
					array[j].y = vector2.y + (shapeB.Position[j].y - vector2.y) * num;
					array[j].z = vector2.z + (shapeB.Position[j].z - vector2.z) * num;
					Vector3 a = Vector3.SlerpUnclamped(shapeA.Normal[fIndex2], shapeA.Normal[fIndex2 + 1], frag2);
					array2[j] = Vector3.SlerpUnclamped(a, shapeB.Normal[j], num);
				}
			}
			resultShape.Position = array;
			resultShape.F = new float[num2];
			resultShape.Recalculate();
			resultShape.Normal = array2;
			resultShape.Map = (float[])cGShape.Map.Clone();
			resultShape.SourceF = (float[])cGShape.SourceF.Clone();
			resultShape.MaterialGroups = cGShape.MaterialGroups.Select((SamplePointsMaterialGroup g) => g.Clone()).ToList();
			if (!ignoreWarnings)
			{
				if (shapeA.Closed != shapeB.Closed)
				{
					warningsContainer.Add("Mixing inputs with different Closed values is not supported");
				}
				if (shapeA.Seamless != shapeB.Seamless)
				{
					warningsContainer.Add("Mixing inputs with different Seamless values is not supported");
				}
				if (shapeA.SourceIsManaged != shapeB.SourceIsManaged)
				{
					warningsContainer.Add("Mixing inputs with different SourceIsManaged values is not supported");
				}
			}
			resultShape.Closed = shapeA.Closed;
			resultShape.Seamless = shapeA.Seamless;
			resultShape.SourceIsManaged = shapeA.SourceIsManaged;
		}
	}
}
