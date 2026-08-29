using Battlehub.RTCommon;
using UnityEngine;

public class RTESceneWindow : RuntimeCameraWindow
{
	public override Camera Camera
	{
		get
		{
			return m_camera;
		}
		set
		{
			if (!(m_camera == value))
			{
				if (m_camera != null)
				{
					UnregisterGraphicsCamera();
				}
				m_camera = value;
				if (m_camera != null)
				{
					RegisterGraphicsCamera();
				}
			}
		}
	}

	protected override void AwakeOverride()
	{
		WindowType = RuntimeWindowType.Scene;
		IsPointerOver = true;
		if (m_camera != null)
		{
			RegisterGraphicsCamera();
		}
		base.Editor.RegisterWindow(this);
		if (m_pointer == null)
		{
			m_pointer = base.gameObject.AddComponent<Pointer>();
		}
		if (RenderPipelineInfo.Type == RPType.Standard && base.gameObject.GetComponent<RTEGraphicsLayer>() == null)
		{
			base.gameObject.AddComponent<RTEGraphicsLayer>();
		}
	}

	protected override void OnEnable()
	{
	}

	protected override void OnDisable()
	{
	}

	protected override void OnDestroyOverride()
	{
		UnregisterGraphicsCamera();
		base.Editor.UnregisterWindow(this);
	}

	protected override void UpdateOverride()
	{
	}
}
