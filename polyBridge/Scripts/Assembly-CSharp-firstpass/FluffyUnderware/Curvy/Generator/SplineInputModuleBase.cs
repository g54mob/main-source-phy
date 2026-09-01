using System;
using System.Collections.Generic;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public abstract class SplineInputModuleBase : CGModule
	{
		[Tab("General")]
		[SerializeField]
		[Tooltip("Makes this module use the cached approximations of the spline's positions and tangents")]
		private bool m_UseCache;

		[Tooltip("Whether to use local or global coordinates of the input's control points.\r\nUsing the global space when the input's transform is updating every frame will lead to the generator refreshing too frequently")]
		[SerializeField]
		private bool m_UseGlobalSpace;

		[Tab("Range")]
		[SerializeField]
		protected CurvySplineSegment m_StartCP;

		[FieldCondition("m_StartCP", null, true, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below, Action = ActionAttribute.ActionEnum.Enable)]
		[SerializeField]
		protected CurvySplineSegment m_EndCP;

		public bool UseCache
		{
			get
			{
				return m_UseCache;
			}
			set
			{
				if (m_UseCache != value)
				{
					m_UseCache = value;
				}
				base.Dirty = true;
			}
		}

		public CurvySplineSegment StartCP
		{
			get
			{
				return m_StartCP;
			}
			set
			{
				if (m_StartCP != value)
				{
					m_StartCP = value;
					ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public CurvySplineSegment EndCP
		{
			get
			{
				return m_EndCP;
			}
			set
			{
				if (m_EndCP != value)
				{
					m_EndCP = value;
					ValidateStartAndEndCps();
				}
				base.Dirty = true;
			}
		}

		public bool UseGlobalSpace
		{
			get
			{
				return m_UseGlobalSpace;
			}
			set
			{
				m_UseGlobalSpace = value;
				base.Dirty = true;
			}
		}

		public override bool IsConfigured
		{
			get
			{
				if (base.IsConfigured)
				{
					return InputSpline != null;
				}
				return false;
			}
		}

		public override bool IsInitialized
		{
			get
			{
				if (base.IsInitialized)
				{
					if (!(InputSpline == null))
					{
						return InputSpline.IsInitialized;
					}
					return true;
				}
				return false;
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
				return getPathLength(InputSpline);
			}
		}

		public bool PathIsClosed
		{
			get
			{
				if (IsConfigured)
				{
					return getPathClosed(InputSpline);
				}
				return false;
			}
		}

		protected abstract CurvySpline InputSpline { get; set; }

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 250f;
			OnSplineAssigned();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if ((bool)InputSpline)
			{
				InputSpline.OnRefresh.RemoveListener(OnSplineRefreshed);
				CurvySpline inputSpline = InputSpline;
				inputSpline.OnGlobalCoordinatesChanged = (Action<CurvySpline>)Delegate.Remove(inputSpline.OnGlobalCoordinatesChanged, new Action<CurvySpline>(OnInputSplineCoordinatesChanged));
			}
		}

		public override void Reset()
		{
			base.Reset();
			InputSpline = null;
			UseCache = false;
			StartCP = null;
			EndCP = null;
			UseGlobalSpace = false;
		}

		private void OnSplineRefreshed(CurvySplineEventArgs e)
		{
			if (base.enabled && base.gameObject.activeInHierarchy)
			{
				if (InputSpline == e.Spline)
				{
					ForceRefresh();
				}
				else
				{
					e.Spline.OnRefresh.RemoveListener(OnSplineRefreshed);
				}
			}
		}

		private void OnInputSplineCoordinatesChanged(CurvySpline sender)
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (InputSpline == sender)
			{
				if (UseGlobalSpace)
				{
					ForceRefresh();
				}
			}
			else
			{
				CurvySpline inputSpline = InputSpline;
				inputSpline.OnGlobalCoordinatesChanged = (Action<CurvySpline>)Delegate.Remove(inputSpline.OnGlobalCoordinatesChanged, new Action<CurvySpline>(OnInputSplineCoordinatesChanged));
			}
		}

		private void ForceRefresh()
		{
			base.Dirty = true;
		}

		private float getPathLength(CurvySpline spline)
		{
			if (!spline)
			{
				return 0f;
			}
			if ((bool)StartCP && (bool)EndCP)
			{
				return EndCP.Distance - StartCP.Distance;
			}
			return spline.Length;
		}

		private bool getPathClosed(CurvySpline spline)
		{
			if (!spline || !spline.Closed)
			{
				return false;
			}
			return EndCP == null;
		}

		protected CGData GetSplineData(CurvySpline spline, bool fullPath, CGDataRequestRasterization raster, CGDataRequestMetaCGOptions options)
		{
			if (spline == null || spline.Count == 0)
			{
				return null;
			}
			List<ControlPointOption> list = new List<ControlPointOption>();
			int initialMaterialID = 0;
			float initialMaxStep = float.MaxValue;
			CGShape cGShape = (fullPath ? new CGPath() : new CGShape());
			float pathLength = getPathLength(spline);
			float num;
			float num2;
			if ((bool)StartCP)
			{
				num = StartCP.Distance + pathLength * raster.Start;
				num2 = StartCP.Distance + pathLength * (raster.Start + raster.RasterizedRelativeLength);
			}
			else
			{
				num = spline.Length * raster.Start;
				num2 = spline.Length * (raster.Start + raster.RasterizedRelativeLength);
			}
			float num3 = CurvySpline.CalculateSamplingPointsPerUnit(raster.Resolution, spline.MaxPointsPerUnit);
			float num4 = (num2 - num) / (pathLength * raster.RasterizedRelativeLength * num3);
			cGShape.Length = num2 - num;
			float tf = spline.DistanceToTF(num);
			float startTF = tf;
			float num5 = ((num2 > spline.Length && spline.Closed) ? (spline.DistanceToTF(num2 - spline.Length) + 1f) : spline.DistanceToTF(num2));
			cGShape.SourceIsManaged = IsManagedResource(spline);
			cGShape.Closed = spline.Closed;
			cGShape.Seamless = spline.Closed && raster.RasterizedRelativeLength == 1f;
			if (cGShape.Length == 0f)
			{
				return cGShape;
			}
			if ((bool)options)
			{
				list = CGUtility.GetControlPointsWithOptions(options, spline, num, num2, raster.Mode == CGDataRequestRasterization.ModeEnum.Optimized, out initialMaterialID, out initialMaxStep);
			}
			List<SamplePointUData> list2 = new List<SamplePointUData>();
			List<Vector3> list3 = new List<Vector3>();
			List<float> list4 = new List<float>();
			List<float> list5 = new List<float>();
			List<Vector3> list6 = new List<Vector3>();
			List<Vector3> list7 = new List<Vector3>();
			float num6 = num;
			Vector3 localTangent = Vector3.zero;
			Vector3 up = Vector3.zero;
			List<int> list8 = new List<int>();
			int num7 = 100000;
			Vector3 localPosition;
			switch (raster.Mode)
			{
			case CGDataRequestRasterization.ModeEnum.Even:
			{
				bool flag = false;
				SamplePointsMaterialGroup samplePointsMaterialGroup = new SamplePointsMaterialGroup(initialMaterialID);
				SamplePointsPatch item = new SamplePointsPatch(0);
				CurvyClamping clamping = (cGShape.Closed ? CurvyClamping.Loop : CurvyClamping.Clamp);
				while (num6 <= num2 && --num7 > 0)
				{
					tf = spline.DistanceToTF(spline.ClampDistance(num6, clamping));
					float num8 = (num6 - num) / cGShape.Length;
					if (Mathf.Approximately(1f, num8))
					{
						num8 = 1f;
					}
					float localF;
					CurvySplineSegment curvySplineSegment = spline.TFToSegment(tf, out localF, CurvyClamping.Clamp);
					if (fullPath)
					{
						if (UseCache)
						{
							curvySplineSegment.InterpolateAndGetTangentFast(localF, out localPosition, out localTangent);
						}
						else
						{
							curvySplineSegment.InterpolateAndGetTangent(localF, out localPosition, out localTangent);
						}
						up = curvySplineSegment.GetOrientationUpFast(localF);
					}
					else
					{
						localPosition = (UseCache ? curvySplineSegment.InterpolateFast(localF) : curvySplineSegment.Interpolate(localF, spline.Interpolation));
					}
					AddPoint(num6 / spline.Length, num8, fullPath, localPosition, localTangent, up, list5, list4, list3, list6, list7);
					if (flag)
					{
						AddPoint(num6 / spline.Length, num8, fullPath, localPosition, localTangent, up, list5, list4, list3, list6, list7);
						flag = false;
					}
					num6 += num4;
					if (list.Count > 0 && num6 >= list[0].Distance)
					{
						if (list[0].UVEdge || list[0].UVShift)
						{
							list2.Add(new SamplePointUData(list3.Count, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
						}
						num6 = list[0].Distance;
						flag = list[0].HardEdge || list[0].MaterialID != samplePointsMaterialGroup.MaterialID || (options.CheckExtendedUV && list[0].UVEdge);
						if (flag)
						{
							item.End = list3.Count;
							samplePointsMaterialGroup.Patches.Add(item);
							if (samplePointsMaterialGroup.MaterialID != list[0].MaterialID)
							{
								cGShape.MaterialGroups.Add(samplePointsMaterialGroup);
								samplePointsMaterialGroup = new SamplePointsMaterialGroup(list[0].MaterialID);
							}
							item = new SamplePointsPatch(list3.Count + 1);
							if (!list[0].HardEdge)
							{
								list8.Add(list3.Count + 1);
							}
							if (list[0].UVEdge || list[0].UVShift)
							{
								list2.Add(new SamplePointUData(list3.Count + 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
							}
						}
						list.RemoveAt(0);
					}
					if (num6 > num2 && num8 < 1f)
					{
						num6 = num2;
					}
				}
				if (num7 <= 0)
				{
					Debug.LogError("[Curvy] He's dead, Jim! Deadloop in SplineInputModuleBase.GetSplineData (Even)! Please send a bug report.");
				}
				item.End = list3.Count - 1;
				samplePointsMaterialGroup.Patches.Add(item);
				if (cGShape.Closed && !spline[0].GetMetadata<MetaCGOptions>(autoCreate: true).HardEdge)
				{
					list8.Add(0);
				}
				FillData(cGShape, samplePointsMaterialGroup, list5, list4, fullPath, list3, list6, list7, UseGlobalSpace, spline.transform, base.Generator.transform);
				break;
			}
			case CGDataRequestRasterization.ModeEnum.Optimized:
			{
				bool flag = false;
				SamplePointsMaterialGroup samplePointsMaterialGroup = new SamplePointsMaterialGroup(initialMaterialID);
				SamplePointsPatch item = new SamplePointsPatch(0);
				float stepDist = num4 / spline.Length;
				float angleThreshold = raster.AngleThreshold;
				if (UseCache)
				{
					spline.InterpolateAndGetTangentFast(tf, out localPosition, out localTangent);
				}
				else
				{
					spline.InterpolateAndGetTangent(tf, out localPosition, out localTangent);
				}
				while (tf < num5 && num7-- > 0)
				{
					AddPoint(num6 / spline.Length, (num6 - num) / cGShape.Length, fullPath, localPosition, localTangent, spline.GetOrientationUpFast(tf % 1f), list5, list4, list3, list6, list7);
					float stopTF = ((list.Count > 0) ? list[0].TF : num5);
					bool flag2 = MoveByAngleExt(spline, UseCache, ref tf, initialMaxStep, angleThreshold, out localPosition, out localTangent, stopTF, cGShape.Closed, stepDist);
					num6 = spline.TFToDistance(tf);
					if (Mathf.Approximately(tf, num5) || tf > num5)
					{
						num6 = num2;
						num5 = (cGShape.Closed ? DTMath.Repeat(num5, 1f) : Mathf.Clamp01(num5));
						if (fullPath)
						{
							if (UseCache)
							{
								spline.InterpolateAndGetTangentFast(num5, out localPosition, out localTangent);
							}
							else
							{
								spline.InterpolateAndGetTangent(num5, out localPosition, out localTangent);
							}
						}
						else
						{
							localPosition = (UseCache ? spline.InterpolateFast(num5) : spline.Interpolate(num5));
						}
						AddPoint(num6 / spline.Length, (num6 - num) / cGShape.Length, fullPath, localPosition, localTangent, spline.GetOrientationUpFast(num5), list5, list4, list3, list6, list7);
						break;
					}
					if (!flag2)
					{
						continue;
					}
					if (list.Count > 0)
					{
						if (list[0].UVEdge || list[0].UVShift)
						{
							list2.Add(new SamplePointUData(list3.Count, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
						}
						num6 = list[0].Distance;
						initialMaxStep = list[0].MaxStepDistance;
						if (list[0].HardEdge || list[0].MaterialID != samplePointsMaterialGroup.MaterialID || (options.CheckExtendedUV && list[0].UVEdge))
						{
							item.End = list3.Count;
							samplePointsMaterialGroup.Patches.Add(item);
							if (samplePointsMaterialGroup.MaterialID != list[0].MaterialID)
							{
								cGShape.MaterialGroups.Add(samplePointsMaterialGroup);
								samplePointsMaterialGroup = new SamplePointsMaterialGroup(list[0].MaterialID);
							}
							item = new SamplePointsPatch(list3.Count + 1);
							if (!list[0].HardEdge)
							{
								list8.Add(list3.Count + 1);
							}
							if (list[0].UVEdge || list[0].UVShift)
							{
								list2.Add(new SamplePointUData(list3.Count + 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
							}
							AddPoint(num6 / spline.Length, (num6 - num) / cGShape.Length, fullPath, localPosition, localTangent, spline.GetOrientationUpFast(tf), list5, list4, list3, list6, list7);
						}
						list.RemoveAt(0);
						continue;
					}
					AddPoint(num6 / spline.Length, (num6 - num) / cGShape.Length, fullPath, localPosition, localTangent, spline.GetOrientationUpFast(tf), list5, list4, list3, list6, list7);
					break;
				}
				if (num7 <= 0)
				{
					Debug.LogError("[Curvy] He's dead, Jim! Deadloop in SplineInputModuleBase.GetSplineData (Optimized)! Please send a bug report.");
				}
				item.End = list3.Count - 1;
				samplePointsMaterialGroup.Patches.Add(item);
				if (list.Count > 0 && list[0].UVShift)
				{
					list2.Add(new SamplePointUData(list3.Count - 1, list[0].UVEdge, list[0].FirstU, list[0].SecondU));
				}
				if (cGShape.Closed && !spline[0].GetMetadata<MetaCGOptions>(autoCreate: true).HardEdge)
				{
					list8.Add(0);
				}
				FillData(cGShape, samplePointsMaterialGroup, list5, list4, fullPath, list3, list6, list7, UseGlobalSpace, spline.transform, base.Generator.transform);
				break;
			}
			}
			cGShape.Map = (float[])cGShape.F.Clone();
			if (!fullPath)
			{
				cGShape.RecalculateNormals(list8);
				if ((bool)options && options.CheckExtendedUV)
				{
					CalculateExtendedUV(spline, startTF, num5, list2, cGShape);
				}
			}
			return cGShape;
		}

		private static void FillData(CGShape dataToFill, SamplePointsMaterialGroup materialGroup, List<float> sourceFs, List<float> relativeFs, bool isFullPath, List<Vector3> positions, List<Vector3> tangents, List<Vector3> normals, bool considerSplineTransform, Transform splineTransform, Transform generatorTransform)
		{
			if (considerSplineTransform)
			{
				for (int i = 0; i < positions.Count; i++)
				{
					positions[i] = generatorTransform.InverseTransformPoint(splineTransform.TransformPoint(positions[i]));
				}
				for (int j = 0; j < tangents.Count; j++)
				{
					tangents[j] = generatorTransform.InverseTransformDirection(splineTransform.TransformDirection(tangents[j]));
				}
				for (int k = 0; k < normals.Count; k++)
				{
					normals[k] = generatorTransform.InverseTransformDirection(splineTransform.TransformDirection(normals[k]));
				}
			}
			dataToFill.MaterialGroups.Add(materialGroup);
			dataToFill.SourceF = sourceFs.ToArray();
			dataToFill.F = relativeFs.ToArray();
			dataToFill.Position = positions.ToArray();
			if (isFullPath)
			{
				((CGPath)dataToFill).Direction = tangents.ToArray();
				dataToFill.Normal = normals.ToArray();
			}
		}

		private static void AddPoint(float sourceF, float relativeF, bool isFullPath, Vector3 position, Vector3 tangent, Vector3 up, List<float> sourceFList, List<float> relativeFList, List<Vector3> positionList, List<Vector3> tangentList, List<Vector3> upList)
		{
			sourceFList.Add(sourceF);
			positionList.Add(position);
			relativeFList.Add(relativeF);
			if (isFullPath)
			{
				tangentList.Add(tangent);
				upList.Add(up);
			}
		}

		private static bool MoveByAngleExt(CurvySpline spline, bool useCache, ref float tf, float maxDistance, float maxAngle, out Vector3 pos, out Vector3 tan, float stopTF, bool loop, float stepDist)
		{
			if (!loop)
			{
				tf = Mathf.Clamp01(tf);
			}
			float tf2 = (loop ? (tf % 1f) : tf);
			CurvySplineSegment curvySplineSegment = spline.TFToSegment(tf2, out var localF, CurvyClamping.Clamp);
			if (useCache)
			{
				curvySplineSegment.InterpolateAndGetTangentFast(localF, out pos, out tan);
			}
			else
			{
				curvySplineSegment.InterpolateAndGetTangent(localF, out pos, out tan);
			}
			Vector3 vector = pos;
			Vector3 vector2 = tan;
			float num = 0f;
			float num2 = 0f;
			if (stopTF < tf && loop)
			{
				stopTF += 1f;
			}
			bool flag = false;
			Vector3 vector3 = default(Vector3);
			while (tf < stopTF && !flag)
			{
				tf = Mathf.Min(stopTF, tf + stepDist);
				tf2 = (loop ? (tf % 1f) : tf);
				curvySplineSegment = spline.TFToSegment(tf2, out localF, CurvyClamping.Clamp);
				if (useCache)
				{
					curvySplineSegment.InterpolateAndGetTangentFast(localF, out pos, out tan);
				}
				else
				{
					curvySplineSegment.InterpolateAndGetTangent(localF, out pos, out tan);
				}
				vector3.x = pos.x - vector.x;
				vector3.y = pos.y - vector.y;
				vector3.z = pos.z - vector.z;
				num += vector3.magnitude;
				float num3 = Vector3.Angle(vector2, tan);
				num2 += num3;
				if (num >= maxDistance || num2 >= maxAngle || (num3 == 0f && num2 > 0f))
				{
					flag = true;
					continue;
				}
				vector = pos;
				vector2 = tan;
			}
			return Mathf.Approximately(tf, stopTF);
		}

		private void CalculateExtendedUV(CurvySpline spline, float startTF, float endTF, List<SamplePointUData> ext, CGShape data)
		{
			MetaCGOptions metaCGOptions = findPreviousReferenceCPOptions(spline, startTF, out var cp);
			MetaCGOptions metaCGOptions2 = findNextReferenceCPOptions(spline, startTF, out var cp2);
			ext.Insert(0, new SamplePointUData(uv0: ((!(spline.FirstVisibleControlPoint == cp2)) ? ((data.SourceF[0] * spline.Length - cp.Distance) / (cp2.Distance - cp.Distance)) : ((data.SourceF[0] * spline.Length - cp.Distance) / (spline.Length - cp.Distance))) * (metaCGOptions2.FirstU - metaCGOptions.GetDefinedFirstU(0f)) + metaCGOptions.GetDefinedFirstU(0f), vt: 0, uvEdge: startTF == 0f && metaCGOptions.UVEdge, uv1: (startTF == 0f && metaCGOptions.UVEdge) ? metaCGOptions.SecondU : 0f));
			if (ext[ext.Count - 1].Vertex < data.Count - 1)
			{
				metaCGOptions = findPreviousReferenceCPOptions(spline, endTF, out cp);
				metaCGOptions2 = findNextReferenceCPOptions(spline, endTF, out cp2);
				float num = metaCGOptions2.FirstU;
				float definedSecondU = metaCGOptions.GetDefinedSecondU(0f);
				float num2;
				if (spline.FirstVisibleControlPoint == cp2)
				{
					num2 = (data.SourceF[data.Count - 1] * spline.Length - cp.Distance) / (spline.Length - cp.Distance);
					num = (metaCGOptions2.UVEdge ? metaCGOptions2.FirstU : ((ext.Count <= 1) ? 1f : ((float)(Mathf.FloorToInt(ext[ext.Count - 1].UVEdge ? ext[ext.Count - 1].SecondU : ext[ext.Count - 1].FirstU) + 1))));
				}
				else
				{
					num2 = (data.SourceF[data.Count - 1] * spline.Length - cp.Distance) / (cp2.Distance - cp.Distance);
				}
				ext.Add(new SamplePointUData(data.Count - 1, uvEdge: false, num2 * (num - definedSecondU) + definedSecondU, 0f));
			}
			float num3 = 0f;
			float num4 = (ext[0].UVEdge ? ext[0].SecondU : ext[0].FirstU);
			float firstU = ext[1].FirstU;
			float num5 = data.F[ext[1].Vertex] - data.F[ext[0].Vertex];
			int num6 = 1;
			for (int i = 0; i < data.Count - 1; i++)
			{
				float num7 = (data.F[i] - num3) / num5;
				data.Map[i] = (firstU - num4) * num7 + num4;
				if (ext[num6].Vertex == i)
				{
					if (ext[num6].FirstU == ext[num6 + 1].FirstU)
					{
						num4 = (ext[num6].UVEdge ? ext[num6].SecondU : ext[num6].FirstU);
						num6++;
					}
					else
					{
						num4 = ext[num6].FirstU;
					}
					firstU = ext[num6 + 1].FirstU;
					num5 = data.F[ext[num6 + 1].Vertex] - data.F[ext[num6].Vertex];
					num3 = data.F[i];
					num6++;
				}
			}
			data.Map[data.Count - 1] = ext[ext.Count - 1].FirstU;
		}

		private static MetaCGOptions findPreviousReferenceCPOptions(CurvySpline spline, float tf, out CurvySplineSegment cp)
		{
			cp = spline.TFToSegment(tf);
			MetaCGOptions metadata;
			do
			{
				metadata = cp.GetMetadata<MetaCGOptions>(autoCreate: true);
				if (spline.FirstVisibleControlPoint == cp)
				{
					return metadata;
				}
				cp = spline.GetPreviousSegment(cp);
			}
			while ((bool)cp && !metadata.UVEdge && !metadata.ExplicitU && !metadata.HasDifferentMaterial);
			return metadata;
		}

		private static MetaCGOptions findNextReferenceCPOptions(CurvySpline spline, float tf, out CurvySplineSegment cp)
		{
			cp = spline.TFToSegment(tf, out var _);
			MetaCGOptions metadata;
			do
			{
				cp = spline.GetNextControlPoint(cp);
				metadata = cp.GetMetadata<MetaCGOptions>(autoCreate: true);
				if (!spline.Closed && spline.LastVisibleControlPoint == cp)
				{
					return metadata;
				}
			}
			while (!metadata.UVEdge && !metadata.ExplicitU && !metadata.HasDifferentMaterial && !(spline.FirstSegment == cp));
			return metadata;
		}

		protected virtual void OnSplineAssigned()
		{
			if ((bool)InputSpline)
			{
				InputSpline.OnRefresh.AddListenerOnce(OnSplineRefreshed);
				CurvySpline inputSpline = InputSpline;
				inputSpline.OnGlobalCoordinatesChanged = (Action<CurvySpline>)Delegate.Combine(inputSpline.OnGlobalCoordinatesChanged, new Action<CurvySpline>(OnInputSplineCoordinatesChanged));
			}
		}

		protected void ValidateStartAndEndCps()
		{
			if (!(InputSpline == null))
			{
				if ((bool)m_StartCP && m_StartCP.Spline != InputSpline)
				{
					m_StartCP = null;
				}
				if ((bool)m_EndCP && m_EndCP.Spline != InputSpline)
				{
					m_EndCP = null;
				}
				if (InputSpline.IsInitialized && m_EndCP != null && m_StartCP != null && InputSpline.GetControlPointIndex(m_EndCP) <= InputSpline.GetControlPointIndex(m_StartCP))
				{
					m_EndCP = null;
				}
			}
		}
	}
}
