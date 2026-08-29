using System;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTHandles
{
	[DefaultExecutionOrder(5)]
	public class BaseHandleModel : RTEComponent
	{
		private RuntimeHandlesComponent m_appearance;

		[NonSerialized]
		private RTHColors m_colorsOverride;

		private float m_modelScale = 1f;

		private float m_selectionMargin = 1f;

		protected RuntimeHandleAxis m_selectedAxis;

		protected LockObject m_lockObj = new LockObject();

		public RuntimeHandlesComponent Appearance
		{
			get
			{
				return m_appearance;
			}
			set
			{
				m_appearance = value;
			}
		}

		public RTHColors Colors
		{
			get
			{
				if (m_colorsOverride != null)
				{
					return m_colorsOverride;
				}
				if (m_appearance != null)
				{
					return m_appearance.Colors;
				}
				return new RTHColors();
			}
			set
			{
				m_colorsOverride = value;
			}
		}

		public float ModelScale
		{
			get
			{
				return m_modelScale;
			}
			set
			{
				if (m_modelScale != value)
				{
					m_modelScale = value;
					if (base.enabled && base.gameObject.activeSelf)
					{
						UpdateModel();
					}
				}
			}
		}

		public float SelectionMargin
		{
			get
			{
				return m_selectionMargin;
			}
			set
			{
				if (m_selectionMargin != value)
				{
					m_selectionMargin = value;
					if (base.enabled && base.gameObject.activeSelf)
					{
						UpdateModel();
					}
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			SetLayer(base.transform, Window.Editor.CameraLayerSettings.RuntimeGraphicsLayer + Window.Index);
		}

		protected virtual void OnEnable()
		{
			UpdateModel();
		}

		protected virtual void OnDisable()
		{
			IRTECamera rTECamera = GetRTECamera();
			if (rTECamera != null)
			{
				rTECamera.RenderersCache.Remove(GetRenderers());
				rTECamera.RenderersCache.Refresh();
			}
		}

		protected virtual void Update()
		{
		}

		public virtual void UpdateModel()
		{
			PushUpdatesToGraphicLayer();
		}

		private IRTECamera GetRTECamera()
		{
			if (Window == null || Window.Camera == null)
			{
				return null;
			}
			IRTEGraphicsLayer iRTEGraphicsLayer = Window.IOCContainer.Resolve<IRTEGraphicsLayer>();
			if (iRTEGraphicsLayer != null)
			{
				return iRTEGraphicsLayer.Camera;
			}
			return IOC.Resolve<IRTEGraphics>().GetOrCreateCamera(Window.Camera, CameraEvent.AfterImageEffectsOpaque, meshesCache: false, renderersCache: false);
		}

		public void PushUpdatesToGraphicLayer()
		{
			IRTECamera rTECamera = GetRTECamera();
			if (rTECamera != null && base.gameObject.activeInHierarchy && rTECamera.RenderersCache != null)
			{
				Renderer[] renderers = GetRenderers();
				rTECamera.RenderersCache.Remove(renderers);
				rTECamera.RenderersCache.Add(renderers, forceRender: false, forceMatrixRecalculationPerRender: true);
				rTECamera.RenderersCache.Refresh();
			}
		}

		protected virtual Renderer[] GetRenderers()
		{
			return base.gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);
		}

		private void SetLayer(Transform t, int layer)
		{
			t.gameObject.layer = layer;
			foreach (Transform item in t)
			{
				SetLayer(item, layer);
			}
		}

		public virtual void SetLock(LockObject lockObj)
		{
			if (lockObj == null)
			{
				lockObj = new LockObject();
			}
			m_lockObj = lockObj;
		}

		public virtual void Select(RuntimeHandleAxis axis)
		{
			m_selectedAxis = axis;
		}

		public virtual void SetScale(Vector3 scale)
		{
		}

		public virtual RuntimeHandleAxis HitTest(Ray ray, out float distance)
		{
			distance = float.PositiveInfinity;
			return RuntimeHandleAxis.None;
		}
	}
}
