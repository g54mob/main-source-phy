using System;
using System.Collections.Generic;
using System.Linq;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Shape Extrusion", ModuleName = "Shape Extrusion", Description = "Simple Shape Extrusion")]
	[HelpURL("https://curvyeditor.com/doclink/cgbuildshapeextrusion")]
	public class BuildShapeExtrusion : CGModule, IPathProvider
	{
		public enum ScaleModeEnum
		{
			Simple = 0,
			Advanced = 1
		}

		public enum CrossShiftModeEnum
		{
			None = 0,
			ByOrientation = 1,
			Custom = 2
		}

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, RequestDataOnly = true)]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGShape) }, Array = true, ArrayType = SlotInfo.SlotArrayType.Hidden, RequestDataOnly = true)]
		public CGModuleInputSlot InCross = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVolume))]
		public CGModuleOutputSlot OutVolume = new CGModuleOutputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGVolume))]
		public CGModuleOutputSlot OutVolumeHollow = new CGModuleOutputSlot();

		[Tab("Path")]
		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "Defines how densely the path spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_Resolution = 50;

		[SerializeField]
		private bool m_Optimize = true;

		[FieldCondition("m_Optimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "", "", Tooltip = "Max angle")]
		private float m_AngleThreshold = 10f;

		[Tab("Cross")]
		[FieldAction("CBEditCrossButton", ActionAttribute.ActionEnum.Callback, Position = ActionAttribute.ActionPositionEnum.Above)]
		[FloatRegion(UseSlider = true, RegionOptionsPropertyName = "CrossRangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_CrossRange = FloatRegion.ZeroOne;

		[SerializeField]
		[RangeEx(1f, 100f, "Resolution", "", Tooltip = "Defines how densely the cross spline's sampling points are. When the value is 100, the number of sampling points per world distance unit is equal to the spline's Max Points Per Unit")]
		private int m_CrossResolution = 50;

		[SerializeField]
		[Label("Optimize", "")]
		private bool m_CrossOptimize = true;

		[FieldCondition("m_CrossOptimize", true, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		[RangeEx(0.1f, 120f, "Angle Threshold", "", Tooltip = "Max angle")]
		private float m_CrossAngleThreshold = 10f;

		[SerializeField]
		[Label("Include CP", "")]
		private bool m_CrossIncludeControlpoints;

		[SerializeField]
		[Label("Hard Edges", "")]
		private bool m_CrossHardEdges;

		[SerializeField]
		[Label("Materials", "")]
		private bool m_CrossMaterials;

		[SerializeField]
		[Label("Extended UV", "")]
		private bool m_CrossExtendedUV;

		[SerializeField]
		[Label("Shift", "", Tooltip = "Defines a shift to be applied on the output volume's cross.\r\nThis shift is used when interpolating values (position, normal, ...) along the volume's surface.")]
		private CrossShiftModeEnum m_CrossShiftMode = CrossShiftModeEnum.ByOrientation;

		[SerializeField]
		[RangeEx(0f, 1f, "Value", "Shift By", Slider = true)]
		[FieldCondition("m_CrossShiftMode", CrossShiftModeEnum.Custom, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private float m_CrossShiftValue;

		[Label("Reverse Normal", "Reverse Vertex Normals?")]
		[SerializeField]
		private bool m_CrossReverseNormals;

		[Tab("Scale")]
		[Label("Mode", "")]
		[SerializeField]
		private ScaleModeEnum m_ScaleMode;

		[FieldCondition("m_ScaleMode", ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Label("Reference", "")]
		[SerializeField]
		private CGReferenceMode m_ScaleReference = CGReferenceMode.Self;

		[FieldCondition("m_ScaleMode", ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[Label("Offset", "")]
		[SerializeField]
		private float m_ScaleOffset;

		[SerializeField]
		[Label("Uniform", "", Tooltip = "Use a single curve")]
		private bool m_ScaleUniform = true;

		[SerializeField]
		private float m_ScaleX = 1f;

		[SerializeField]
		[FieldCondition("m_ScaleUniform", false, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		private float m_ScaleY = 1f;

		[SerializeField]
		[FieldCondition("m_ScaleMode", ScaleModeEnum.Advanced, false, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[AnimationCurveEx("Multiplier X", "")]
		[Tooltip("Defines scale multiplier depending on the TF, the relative position of a point on the path")]
		private AnimationCurve m_ScaleCurveX = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[SerializeField]
		[FieldCondition("m_ScaleUniform", false, false, ConditionalAttribute.OperatorEnum.AND, "m_ScaleMode", ScaleModeEnum.Advanced, false)]
		[AnimationCurveEx("Multiplier Y", "")]
		[Tooltip("Defines scale multiplier depending on the TF, the relative position of a point on the path")]
		private AnimationCurve m_ScaleCurveY = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[Tab("Hollow")]
		[RangeEx(0f, 1f, "", "", Slider = true, Label = "Inset")]
		[SerializeField]
		private float m_HollowInset;

		[Label("Reverse Normal", "Reverse Vertex Normals?")]
		[SerializeField]
		private bool m_HollowReverseNormals;

		public float From
		{
			get
			{
				return m_Range.From;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (m_Range.From != num)
				{
					m_Range.From = num;
				}
				base.Dirty = true;
			}
		}

		public float To
		{
			get
			{
				return m_Range.To;
			}
			set
			{
				float num = Mathf.Max(From, value);
				if (ClampPath)
				{
					num = DTMath.Repeat(value, 1f);
				}
				if (m_Range.To != num)
				{
					m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public float Length
		{
			get
			{
				if (!ClampPath)
				{
					return m_Range.To;
				}
				return m_Range.To - m_Range.From;
			}
			set
			{
				float num = (ClampPath ? (value - m_Range.To) : value);
				if (m_Range.To != num)
				{
					m_Range.To = num;
				}
				base.Dirty = true;
			}
		}

		public int Resolution
		{
			get
			{
				return m_Resolution;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (m_Resolution != num)
				{
					m_Resolution = num;
				}
				base.Dirty = true;
			}
		}

		public bool Optimize
		{
			get
			{
				return m_Optimize;
			}
			set
			{
				if (m_Optimize != value)
				{
					m_Optimize = value;
				}
				base.Dirty = true;
			}
		}

		public float AngleThreshold
		{
			get
			{
				return m_AngleThreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (m_AngleThreshold != num)
				{
					m_AngleThreshold = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossFrom
		{
			get
			{
				return m_CrossRange.From;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (m_CrossRange.From != num)
				{
					m_CrossRange.From = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossTo
		{
			get
			{
				return m_CrossRange.To;
			}
			set
			{
				float num = Mathf.Max(CrossFrom, value);
				if (ClampCross)
				{
					num = DTMath.Repeat(value, 1f);
				}
				if (m_CrossRange.To != num)
				{
					m_CrossRange.To = num;
				}
				base.Dirty = true;
			}
		}

		public float CrossLength
		{
			get
			{
				if (!ClampCross)
				{
					return m_CrossRange.To;
				}
				return m_CrossRange.To - m_CrossRange.From;
			}
			set
			{
				float num = (ClampCross ? (value - m_CrossRange.To) : value);
				if (m_CrossRange.To != num)
				{
					m_CrossRange.To = num;
				}
				base.Dirty = true;
			}
		}

		public int CrossResolution
		{
			get
			{
				return m_CrossResolution;
			}
			set
			{
				int num = Mathf.Clamp(value, 1, 100);
				if (m_CrossResolution != num)
				{
					m_CrossResolution = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossOptimize
		{
			get
			{
				return m_CrossOptimize;
			}
			set
			{
				if (m_CrossOptimize != value)
				{
					m_CrossOptimize = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossAngleThreshold
		{
			get
			{
				return m_CrossAngleThreshold;
			}
			set
			{
				float num = Mathf.Clamp(value, 0.1f, 120f);
				if (m_CrossAngleThreshold != num)
				{
					m_CrossAngleThreshold = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossIncludeControlPoints
		{
			get
			{
				return m_CrossIncludeControlpoints;
			}
			set
			{
				if (m_CrossIncludeControlpoints != value)
				{
					m_CrossIncludeControlpoints = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossHardEdges
		{
			get
			{
				return m_CrossHardEdges;
			}
			set
			{
				if (m_CrossHardEdges != value)
				{
					m_CrossHardEdges = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossMaterials
		{
			get
			{
				return m_CrossMaterials;
			}
			set
			{
				if (m_CrossMaterials != value)
				{
					m_CrossMaterials = value;
				}
				base.Dirty = true;
			}
		}

		public bool CrossExtendedUV
		{
			get
			{
				return m_CrossExtendedUV;
			}
			set
			{
				if (m_CrossExtendedUV != value)
				{
					m_CrossExtendedUV = value;
				}
				base.Dirty = true;
			}
		}

		public CrossShiftModeEnum CrossShiftMode
		{
			get
			{
				return m_CrossShiftMode;
			}
			set
			{
				if (m_CrossShiftMode != value)
				{
					m_CrossShiftMode = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossShiftValue
		{
			get
			{
				return m_CrossShiftValue;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (m_CrossShiftValue != num)
				{
					m_CrossShiftValue = num;
				}
				base.Dirty = true;
			}
		}

		public bool CrossReverseNormals
		{
			get
			{
				return m_CrossReverseNormals;
			}
			set
			{
				if (m_CrossReverseNormals != value)
				{
					m_CrossReverseNormals = value;
				}
				base.Dirty = true;
			}
		}

		public ScaleModeEnum ScaleMode
		{
			get
			{
				return m_ScaleMode;
			}
			set
			{
				if (m_ScaleMode != value)
				{
					m_ScaleMode = value;
				}
				base.Dirty = true;
			}
		}

		public CGReferenceMode ScaleReference
		{
			get
			{
				return m_ScaleReference;
			}
			set
			{
				if (m_ScaleReference != value)
				{
					m_ScaleReference = value;
				}
				base.Dirty = true;
			}
		}

		public bool ScaleUniform
		{
			get
			{
				return m_ScaleUniform;
			}
			set
			{
				if (m_ScaleUniform != value)
				{
					m_ScaleUniform = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleOffset
		{
			get
			{
				return m_ScaleOffset;
			}
			set
			{
				if (m_ScaleOffset != value)
				{
					m_ScaleOffset = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleX
		{
			get
			{
				return m_ScaleX;
			}
			set
			{
				if (m_ScaleX != value)
				{
					m_ScaleX = value;
				}
				base.Dirty = true;
			}
		}

		public float ScaleY
		{
			get
			{
				return m_ScaleY;
			}
			set
			{
				if (m_ScaleY != value)
				{
					m_ScaleY = value;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve ScaleMultiplierX
		{
			get
			{
				return m_ScaleCurveX;
			}
			set
			{
				if (m_ScaleCurveX != value)
				{
					m_ScaleCurveX = value;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve ScaleMultiplierY
		{
			get
			{
				return m_ScaleCurveY;
			}
			set
			{
				if (m_ScaleCurveY != value)
				{
					m_ScaleCurveY = value;
				}
				base.Dirty = true;
			}
		}

		public float HollowInset
		{
			get
			{
				return m_HollowInset;
			}
			set
			{
				float num = Mathf.Clamp01(value);
				if (m_HollowInset != num)
				{
					m_HollowInset = num;
				}
				base.Dirty = true;
			}
		}

		public bool HollowReverseNormals
		{
			get
			{
				return m_HollowReverseNormals;
			}
			set
			{
				if (m_HollowReverseNormals != value)
				{
					m_HollowReverseNormals = value;
				}
				base.Dirty = true;
			}
		}

		public int PathSamples { get; private set; }

		public int CrossSamples { get; private set; }

		public int CrossGroups { get; private set; }

		public IExternalInput Cross
		{
			get
			{
				if (!IsConfigured)
				{
					return null;
				}
				return InCross.SourceSlot().ExternalInput;
			}
		}

		public Vector3 CrossPosition { get; protected set; }

		public Quaternion CrossRotation { get; protected set; }

		public bool PathIsClosed => InPath.SourceSlot().PathProvider.PathIsClosed;

		private bool ClampPath
		{
			get
			{
				if (!InPath.IsLinked)
				{
					return true;
				}
				return !InPath.SourceSlot().PathProvider.PathIsClosed;
			}
		}

		private bool ClampCross
		{
			get
			{
				if (!InCross.IsLinked)
				{
					return true;
				}
				return !InCross.SourceSlot().PathProvider.PathIsClosed;
			}
		}

		private RegionOptions<float> RangeOptions
		{
			get
			{
				if (ClampPath)
				{
					return RegionOptions<float>.MinMax(0f, 1f);
				}
				return new RegionOptions<float>
				{
					LabelFrom = "Start",
					ClampFrom = DTValueClamping.Min,
					FromMin = 0f,
					LabelTo = "Length",
					ClampTo = DTValueClamping.Range,
					ToMin = 0f,
					ToMax = 1f
				};
			}
		}

		private RegionOptions<float> CrossRangeOptions
		{
			get
			{
				if (ClampCross)
				{
					return RegionOptions<float>.MinMax(0f, 1f);
				}
				return new RegionOptions<float>
				{
					LabelFrom = "Start",
					ClampFrom = DTValueClamping.Min,
					FromMin = 0f,
					LabelTo = "Length",
					ClampTo = DTValueClamping.Range,
					ToMin = 0f,
					ToMax = 1f
				};
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 270f;
			Properties.LabelWidth = 100f;
		}

		public override void Reset()
		{
			base.Reset();
			From = 0f;
			To = 1f;
			Resolution = 50;
			AngleThreshold = 10f;
			Optimize = true;
			CrossFrom = 0f;
			CrossTo = 1f;
			CrossResolution = 50;
			CrossAngleThreshold = 10f;
			CrossOptimize = true;
			CrossIncludeControlPoints = false;
			CrossHardEdges = false;
			CrossMaterials = false;
			CrossShiftMode = CrossShiftModeEnum.ByOrientation;
			ScaleMode = ScaleModeEnum.Simple;
			ScaleUniform = true;
			ScaleX = 1f;
			ScaleY = 1f;
			ScaleMultiplierX = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			ScaleMultiplierY = AnimationCurve.Linear(0f, 1f, 1f, 1f);
			HollowInset = 0f;
			CrossExtendedUV = false;
			CrossReverseNormals = false;
			HollowReverseNormals = false;
			ScaleReference = CGReferenceMode.Self;
			ScaleOffset = 0f;
		}

		public override void Refresh()
		{
			base.Refresh();
			if (Length == 0f)
			{
				OutVolume.SetData((CGData[])null);
				OutVolumeHollow.SetData((CGData[])null);
				return;
			}
			List<CGDataRequestParameter> list = new List<CGDataRequestParameter>();
			list.Add(new CGDataRequestRasterization(From, Length, Resolution, AngleThreshold, Optimize ? CGDataRequestRasterization.ModeEnum.Optimized : CGDataRequestRasterization.ModeEnum.Even));
			CGPath data = InPath.GetData<CGPath>(list.ToArray());
			list.Clear();
			CGDataRequestRasterization item = ((InCross.LinkedSlots.Count != 1 || !(InCross.LinkedSlots[0].Info is ShapeOutputSlotInfo) || !(InCross.LinkedSlots[0].Info as ShapeOutputSlotInfo).OutputsVariableShape || !data) ? new CGDataRequestRasterization(CrossFrom, CrossLength, CrossResolution, CrossAngleThreshold, CrossOptimize ? CGDataRequestRasterization.ModeEnum.Optimized : CGDataRequestRasterization.ModeEnum.Even) : new CGDataRequestShapeRasterization(data.F, CrossFrom, CrossLength, CrossResolution, CrossAngleThreshold, CrossOptimize ? CGDataRequestRasterization.ModeEnum.Optimized : CGDataRequestRasterization.ModeEnum.Even));
			list.Add(item);
			if (CrossIncludeControlPoints || CrossHardEdges || CrossMaterials)
			{
				list.Add(new CGDataRequestMetaCGOptions(CrossHardEdges, CrossMaterials, CrossIncludeControlPoints, CrossExtendedUV));
			}
			List<CGShape> allData = InCross.GetAllData<CGShape>(list.ToArray());
			bool flag = !data || data.Count == 0;
			List<int> source = allData.Select((CGShape c) => c?.Count ?? 0).Distinct().ToList();
			bool flag2;
			if (source.Count() != 1 || source.First() == 0)
			{
				flag2 = true;
				UIMessages.Add("Shape Extrusion: All input Crosses are expected to have the same non zero number of sample points.");
			}
			else
			{
				flag2 = false;
			}
			if (flag || flag2)
			{
				OutVolume.ClearData();
				OutVolumeHollow.ClearData();
				return;
			}
			CGShape cGShape = allData[0];
			CGVolume cGVolume = CGVolume.Get(OutVolume.GetData<CGVolume>(), data, cGShape);
			CGVolume cGVolume2 = (OutVolumeHollow.IsLinked ? CGVolume.Get(OutVolumeHollow.GetData<CGVolume>(), data, cGShape) : null);
			bool flag3 = cGVolume2;
			PathSamples = data.Count;
			CrossSamples = cGShape.Count;
			CrossGroups = cGShape.MaterialGroups.Count;
			CrossPosition = cGVolume.Position[0];
			CrossRotation = Quaternion.LookRotation(cGVolume.Direction[0], cGVolume.Normal[0]);
			Vector3 vector = (ScaleUniform ? new Vector3(ScaleX, ScaleX, 1f) : new Vector3(ScaleX, ScaleY, 1f));
			Vector3 scale = vector;
			int num = 0;
			float[] array = ((ScaleReference == CGReferenceMode.Source) ? data.SourceF : data.F);
			float num2 = ((!CrossReverseNormals) ? 1 : (-1));
			float num3 = ((!HollowReverseNormals) ? 1 : (-1));
			bool flag4 = allData.Count == 1;
			int count = data.Count;
			for (int num4 = 0; num4 < count; num4++)
			{
				CGShape cGShape2;
				if (flag4)
				{
					cGShape2 = cGShape;
				}
				else
				{
					int index = Mathf.RoundToInt((float)(allData.Count - 1) * data.F[num4]);
					cGShape2 = allData[index];
				}
				Vector3[] position = cGShape2.Position;
				Vector3[] normal = cGShape2.Normal;
				Quaternion quaternion = Quaternion.LookRotation(data.Direction[num4], data.Normal[num4]);
				getScaleInternal(array[num4], vector, ref scale);
				Matrix4x4 matrix4x = Matrix4x4.TRS(data.Position[num4], quaternion, scale);
				Matrix4x4 matrix4x2 = (flag3 ? Matrix4x4.TRS(data.Position[num4], quaternion, scale * (1f - HollowInset)) : default(Matrix4x4));
				int count2 = cGShape2.Count;
				for (int num5 = 0; num5 < count2; num5++)
				{
					cGVolume.Vertex[num] = matrix4x.MultiplyPoint(position[num5]);
					Vector3 vector2 = quaternion * normal[num5];
					cGVolume.VertexNormal[num].x = vector2.x * num2;
					cGVolume.VertexNormal[num].y = vector2.y * num2;
					cGVolume.VertexNormal[num].z = vector2.z * num2;
					if (flag3)
					{
						cGVolume2.Vertex[num] = matrix4x2.MultiplyPoint(position[num5]);
						cGVolume2.VertexNormal[num].x = vector2.x * num3;
						cGVolume2.VertexNormal[num].y = vector2.y * num3;
						cGVolume2.VertexNormal[num].z = vector2.z * num3;
					}
					num++;
				}
			}
			switch (CrossShiftMode)
			{
			case CrossShiftModeEnum.ByOrientation:
			{
				cGVolume.CrossFShift = 0f;
				for (int num6 = 0; num6 < cGShape.Count - 1; num6++)
				{
					if (DTMath.RayLineSegmentIntersection(cGVolume.Position[0], cGVolume.Normal[0], cGVolume.Vertex[num6], cGVolume.Vertex[num6 + 1], out var _, out var frag))
					{
						cGVolume.CrossFShift = DTMath.SnapPrecision(cGVolume.CrossF[num6] + (cGVolume.CrossF[num6 + 1] - cGVolume.CrossF[num6]) * frag, 2);
						break;
					}
				}
				break;
			}
			case CrossShiftModeEnum.Custom:
				cGVolume.CrossFShift = CrossShiftValue;
				break;
			case CrossShiftModeEnum.None:
				cGVolume.CrossFShift = 0f;
				break;
			default:
				throw new ArgumentOutOfRangeException("CrossShiftMode");
			}
			if (cGVolume2 != null)
			{
				cGVolume2.CrossFShift = cGVolume.CrossFShift;
			}
			OutVolume.SetData(cGVolume);
			OutVolumeHollow.SetData(cGVolume2);
		}

		public Vector3 GetScale(float f)
		{
			Vector3 baseScale = (ScaleUniform ? new Vector3(ScaleX, ScaleX, 1f) : new Vector3(ScaleX, ScaleY, 1f));
			Vector3 scale = Vector3.zero;
			getScaleInternal(f, baseScale, ref scale);
			return scale;
		}

		private void getScaleInternal(float f, Vector3 baseScale, ref Vector3 scale)
		{
			if (ScaleMode == ScaleModeEnum.Advanced)
			{
				float time = DTMath.Repeat(f - ScaleOffset, 1f);
				float num = baseScale.x * ScaleMultiplierX.Evaluate(time);
				scale.Set(num, m_ScaleUniform ? num : (baseScale.y * ScaleMultiplierY.Evaluate(time)), 1f);
			}
			else
			{
				scale = baseScale;
			}
		}
	}
}
