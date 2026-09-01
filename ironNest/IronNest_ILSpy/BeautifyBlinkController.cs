using Beautify.Universal;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

public class BeautifyBlinkController : MonoBehaviour
{
	private Volume targetVolume;

	private float initialBlinkValue;

	private bool verboseLogging = true;

	private Beautify.Universal.Beautify beautify;

	private float currentBlinkValue;

	public float BlinkValue
	{
		get
		{
			return currentBlinkValue;
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
					currentBlinkValue = value;
					return;
				}
			}
			currentBlinkValue = num;
		}
	}

	public bool IsReady => beautify != null;

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
				currentBlinkValue = value;
				return;
			}
		}
		currentBlinkValue = num;
	}

	private void Awake()
	{
		ResolveBeautify();
		currentBlinkValue = initialBlinkValue;
	}

	private void Update()
	{
		if (this.beautify != null)
		{
			Beautify.Universal.Beautify beautify = this.beautify;
			beautify.vignettingBlink.overrideState = true;
			Beautify.Universal.Beautify beautify2 = this.beautify;
			beautify2.vignettingBlink.value = currentBlinkValue;
		}
	}

	private unsafe void ResolveBeautify()
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected Ref, but got Unknown
		if (targetVolume == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Volume volume = default(Volume);
			targetVolume = volume;
		}
		string text;
		string text2;
		string text3;
		if (targetVolume != null)
		{
			VolumeProfile profile = targetVolume.profile;
			if (profile != null)
			{
				VolumeProfile profile2 = targetVolume.profile;
				bool flag = profile2.TryGet<Beautify.Universal.Beautify>(out *(Beautify.Universal.Beautify*)(this + 48));
				if (flag || verboseLogging == flag)
				{
					return;
				}
				VolumeProfile profile3 = targetVolume.profile;
				text = profile3.name;
				text2 = "'. Add one via Add Override → Kronnect → Beautify.";
				text3 = "[BeautifyBlinkController] No Beautify override found in the Volume Profile '";
			}
			else
			{
				if (!verboseLogging)
				{
					return;
				}
				text = base.name;
				text2 = "' has no Profile assigned.";
				text3 = "[BeautifyBlinkController] The Volume on '";
			}
		}
		else
		{
			if (!verboseLogging)
			{
				return;
			}
			text = base.name;
			text2 = "'. Assign one in the Inspector.";
			text3 = "[BeautifyBlinkController] No Volume found on '";
		}
		string message = text3 + text + text2;
		Debug.LogWarning(message, this);
	}
}
