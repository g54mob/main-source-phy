using System;
using UnityEngine;

public class DetectAspectRatio : MonoBehaviour
{
	public Camera hudCam;

	public float scaler = 1f;

	public bool useUIScale = true;

	public float cutoff = 1.75f;

	private float originalSize;

	private void Awake()
	{
		originalSize = hudCam.orthographicSize;
		ChangeOrthographicSize();
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void ChangeOrthographicSize()
	{
		float num = (200f - Mathf.Clamp(((!useUIScale) ? OptionsMaster.DefaultConfig : OptionsMaster.BesiegeConfig).UIScale, 0f, 190f)) / 100f;
		if (hudCam.aspect < cutoff)
		{
			float num2 = Mathf.Abs(cutoff - hudCam.aspect);
			hudCam.orthographicSize = (originalSize + num2 * scaler) * num;
		}
		else
		{
			hudCam.orthographicSize = originalSize * num;
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnResolutionChanged()
	{
		ChangeOrthographicSize();
		BarPositionController instance = SingleInstanceFindOnly<BarPositionController>.Instance;
		if (instance != null)
		{
			instance.AlignScaleElements();
		}
	}
}
