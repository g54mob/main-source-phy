using System;
using UnityEngine;
using UnityEngine.UI;

public class SandboxToggleSliderHandle : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public Image m_HandleImage;

	private Action m_Callback;

	public void SetCallback(Action callback)
	{
		m_Callback = callback;
	}

	public void AnimComplete()
	{
		m_Callback?.Invoke();
	}
}
