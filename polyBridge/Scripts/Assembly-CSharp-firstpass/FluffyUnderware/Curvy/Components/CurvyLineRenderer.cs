using System;
using UnityEngine;

namespace FluffyUnderware.Curvy.Components
{
	[AddComponentMenu("Curvy/Misc/Curvy Line Renderer")]
	[RequireComponent(typeof(LineRenderer))]
	[ExecuteInEditMode]
	[HelpURL("https://curvyeditor.com/doclink/curvylinerenderer")]
	public class CurvyLineRenderer : MonoBehaviour
	{
		public CurvySpline m_Spline;

		private LineRenderer mRenderer;

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
					UnbindEvents();
					m_Spline = value;
					BindEvents();
					Refresh();
				}
			}
		}

		private void Awake()
		{
			mRenderer = GetComponent<LineRenderer>();
			if (m_Spline == null)
			{
				m_Spline = GetComponent<CurvySpline>();
			}
		}

		private void OnEnable()
		{
			mRenderer = GetComponent<LineRenderer>();
			BindEvents();
		}

		private void OnDisable()
		{
			UnbindEvents();
		}

		private void Start()
		{
			Refresh();
		}

		private void Update()
		{
			EnforceWorldSpaceUsage();
		}

		private void EnforceWorldSpaceUsage()
		{
			if (!mRenderer.useWorldSpace)
			{
				mRenderer.useWorldSpace = true;
			}
		}

		public void Refresh()
		{
			if ((bool)Spline && Spline.IsInitialized)
			{
				EnforceWorldSpaceUsage();
				Vector3[] approximation = Spline.GetApproximation(Space.World);
				mRenderer.positionCount = approximation.Length;
				mRenderer.SetPositions(approximation);
			}
			else if (mRenderer != null)
			{
				EnforceWorldSpaceUsage();
				mRenderer.positionCount = 0;
			}
		}

		private void OnSplineRefresh(CurvySplineEventArgs e)
		{
			Refresh();
		}

		private void OnSplineCoordinatesChanged(CurvySpline spline)
		{
			Refresh();
		}

		private void BindEvents()
		{
			if ((bool)Spline)
			{
				Spline.OnRefresh.AddListenerOnce(OnSplineRefresh);
				CurvySpline spline = Spline;
				spline.OnGlobalCoordinatesChanged = (Action<CurvySpline>)Delegate.Combine(spline.OnGlobalCoordinatesChanged, new Action<CurvySpline>(OnSplineCoordinatesChanged));
			}
		}

		private void UnbindEvents()
		{
			if ((bool)Spline)
			{
				Spline.OnRefresh.RemoveListener(OnSplineRefresh);
				CurvySpline spline = Spline;
				spline.OnGlobalCoordinatesChanged = (Action<CurvySpline>)Delegate.Remove(spline.OnGlobalCoordinatesChanged, new Action<CurvySpline>(OnSplineCoordinatesChanged));
			}
		}
	}
}
