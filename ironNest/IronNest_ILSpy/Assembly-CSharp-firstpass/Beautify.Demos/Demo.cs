using Beautify.Universal;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Beautify.Demos;

public class Demo : MonoBehaviour
{
	public Texture lutTexture;

	private void Start()
	{
		UpdateText();
	}

	private void Update()
	{
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Expected I4, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected I4, but got Unknown
		//IL_049d: Expected F4, but got I
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Expected I4, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Expected O, but got Unknown
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Expected I4, but got Unknown
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Expected I4, but got Unknown
		//IL_06da: Invalid comparison between F4 and I4
		//IL_0797: Invalid comparison between F4 and I4
		//IL_07a6: Expected F4, but got I4
		//IL_06fa: Expected F4, but got I4
		float num = default(float);
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl jKey = Keyboard._003Ccurrent_003Ek__BackingField.jKey;
			if (jKey.wasPressedThisFrame)
			{
				Beautify.Universal.Beautify settings = BeautifySettings.settings;
				float value = settings.bloomIntensity.value;
				num += 0.1f;
				settings.bloomIntensity.value = num;
			}
		}
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl tKey = Keyboard._003Ccurrent_003Ek__BackingField.tKey;
			if (tKey.wasPressedThisFrame)
			{
				goto IL_00e3;
			}
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		if (Mouse._003Ccurrent_003Ek__BackingField != null && mouse._003CleftButton_003Ek__BackingField.wasPressedThisFrame)
		{
			goto IL_00e3;
		}
		goto IL_0142;
		IL_00e3:
		Beautify.Universal.Beautify settings2 = BeautifySettings.settings;
		Beautify.Universal.Beautify settings3 = BeautifySettings.settings;
		bool value2 = settings3.disabled.value;
		bool value3 = !value2;
		settings2.disabled.value = value3;
		UpdateText();
		goto IL_0142;
		IL_0142:
		if (InputProxy.GetKeyDown(KeyCode.B))
		{
			BeautifySettings.Blink(0.2f);
			num = 0.2f;
		}
		if (InputProxy.GetKeyDown(KeyCode.C))
		{
			Beautify.Universal.Beautify settings4 = BeautifySettings.settings;
			Beautify.Universal.Beautify settings5 = BeautifySettings.settings;
			bool value4 = settings5.compareMode.value;
			bool value5 = !value4;
			settings4.compareMode.value = value5;
		}
		object obj = default(object);
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl nKey = Keyboard._003Ccurrent_003Ek__BackingField.nKey;
			if (nKey.wasPressedThisFrame)
			{
				Beautify.Universal.Beautify settings6 = BeautifySettings.settings;
				Beautify.Universal.Beautify settings7 = BeautifySettings.settings;
				bool value6 = settings7.nightVision.value;
				bool x = (byte)(obj + 32) != 0;
				bool flag = !value6;
				settings6.nightVision.Override(x);
			}
		}
		if (InputProxy.GetKeyDown(KeyCode.F))
		{
			Beautify.Universal.Beautify settings8 = BeautifySettings.settings;
			if (!settings8.blurIntensity.overrideState)
			{
				Beautify.Universal.Beautify settings9 = BeautifySettings.settings;
				float x2 = (float)obj + 32f;
				_ = 1082130432;
				settings9.blurIntensity.Override(x2);
			}
			else
			{
				Beautify.Universal.Beautify settings10 = BeautifySettings.settings;
				settings10.blurIntensity.overrideState = false;
			}
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha1))
		{
			Beautify.Universal.Beautify settings11 = BeautifySettings.settings;
			float x3 = (float)obj + 32f;
			_ = 1036831949;
			settings11.brightness.Override(x3);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha2))
		{
			Beautify.Universal.Beautify settings12 = BeautifySettings.settings;
			float x4 = (float)obj + 32f;
			_ = 1056964608;
			settings12.brightness.Override(x4);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha3))
		{
			Beautify.Universal.Beautify settings13 = BeautifySettings.settings;
			settings13.brightness.overrideState = false;
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha4))
		{
			Beautify.Universal.Beautify settings14 = BeautifySettings.settings;
			bool x5 = (byte)(obj + 32) != 0;
			_ = 1;
			settings14.outline.Override(x5);
			Beautify.Universal.Beautify settings15 = BeautifySettings.settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F00]");
			num = 0f;
			Color x6 = (Color)(obj - 48);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F00]");
			_ = 0;
			settings15.outlineColor.Override(x6);
			Beautify.Universal.Beautify settings16 = BeautifySettings.settings;
			bool x7 = (byte)(obj + 32) != 0;
			_ = 1;
			settings16.outlineCustomize.Override(x7);
			Beautify.Universal.Beautify settings17 = BeautifySettings.settings;
			float x8 = (float)obj + 32f;
			_ = 1069547520;
			settings17.outlineSpread.Override(x8);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha5))
		{
			Beautify.Universal.Beautify settings18 = BeautifySettings.settings;
			settings18.outline.overrideState = false;
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha6))
		{
			Beautify.Universal.Beautify settings19 = BeautifySettings.settings;
			bool x9 = (byte)(obj + 32) != 0;
			_ = 1;
			settings19.lut.Override(x9);
			Beautify.Universal.Beautify settings20 = BeautifySettings.settings;
			float x10 = (float)obj + 32f;
			_ = 1065353216;
			settings20.lutIntensity.Override(x10);
			Beautify.Universal.Beautify settings21 = BeautifySettings.settings;
			settings21.lutTexture.Override(lutTexture);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha7))
		{
			Beautify.Universal.Beautify settings22 = BeautifySettings.settings;
			bool x11 = (byte)(obj + 32) != 0;
			_ = 0;
			settings22.lut.Override(x11);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha8))
		{
			Beautify.Universal.Beautify settings23 = BeautifySettings.settings;
			float value7 = settings23.anamorphicFlaresIntensity.value;
			Beautify.Universal.Beautify settings24 = BeautifySettings.settings;
			num = ((!(num > 0f)) ? 1f : 0f);
			float x12 = (float)obj + 32f;
			settings24.anamorphicFlaresIntensity.Override(x12);
		}
		if (InputProxy.GetKeyDown(KeyCode.Alpha9))
		{
			Beautify.Universal.Beautify settings25 = BeautifySettings.settings;
			float value8 = settings25.blurIntensity.value;
			Beautify.Universal.Beautify settings26 = BeautifySettings.settings;
			bool flag2 = num > 0f;
			float num2 = 0f;
			if (!flag2)
			{
				num2 = 1f;
			}
			float x13 = (float)obj + 32f;
			settings26.blurIntensity.Override(x13);
		}
	}

	private void UpdateText()
	{
		Beautify.Universal.Beautify settings = BeautifySettings.settings;
		object obj2 = default(object);
		if (!settings.disabled.value)
		{
			GameObject gameObject = GameObject.Find("Beautify");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			object obj = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v164 @ r8_v6+5E8] (should have been resolved before IL gen)");
		}
		else
		{
			GameObject gameObject2 = GameObject.Find("Beautify");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			object obj3 = obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v166 @ r8_v3+5E8] (should have been resolved before IL gen)");
		}
	}
}
