using System;
using UnityEngine;

public class BeautifyBlinkRelay : MonoBehaviour
{
	private BeautifyBlinkController controller;

	private float blinkProxy;

	private bool useMaxBlend;

	private bool verboseLogging = true;

	public float BlinkProxy
	{
		get
		{
			return blinkProxy;
		}
		set
		{
			//IL_0009: Invalid comparison between I4 and F4
			//IL_0018: Expected F4, but got I4
			bool flag = 0f > value;
			float num = 0f;
			if (!flag)
			{
				bool flag2 = value > 1f;
				num = 1f;
				if (!flag2)
				{
					blinkProxy = value;
					return;
				}
			}
			blinkProxy = num;
		}
	}

	public void SetBlink(float value)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_0018: Expected F4, but got I4
		bool flag = 0f > value;
		float num = 0f;
		if (!flag)
		{
			bool flag2 = value > 1f;
			num = 1f;
			if (!flag2)
			{
				blinkProxy = value;
				return;
			}
		}
		blinkProxy = num;
	}

	private void Awake()
	{
		if (controller == null && verboseLogging)
		{
			string text = base.name;
			string message = "[BeautifyBlinkRelay] '" + text + "' has no BeautifyBlinkController assigned. Drag the controller GameObject into the Controller field.";
			Debug.LogWarning(message, this);
		}
	}

	private void OnDisable()
	{
		ResetBlink();
	}

	private void OnDestroy()
	{
		ResetBlink();
	}

	private void ResetBlink()
	{
		blinkProxy = 0f;
		if (controller != null)
		{
			BeautifyBlinkController beautifyBlinkController = controller;
			beautifyBlinkController.currentBlinkValue = 0f;
		}
	}

	private void Update()
	{
		//IL_0113: Invalid comparison between I4 and F4
		//IL_0096: Expected F4, but got I4
		if (!(controller != null))
		{
			return;
		}
		BeautifyBlinkController beautifyBlinkController = controller;
		float num;
		if (!useMaxBlend)
		{
			num = blinkProxy;
		}
		else
		{
			num = blinkProxy;
			if (!(blinkProxy > beautifyBlinkController.currentBlinkValue))
			{
				return;
			}
			if ((object)beautifyBlinkController == null)
			{
				throw new NullReferenceException();
			}
		}
		if (!(0f > num))
		{
			if (num > 1f)
			{
				beautifyBlinkController.currentBlinkValue = 1f;
				return;
			}
		}
		else
		{
			num = 0f;
		}
		beautifyBlinkController.currentBlinkValue = num;
	}
}
