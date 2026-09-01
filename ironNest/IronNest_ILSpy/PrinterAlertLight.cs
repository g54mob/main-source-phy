using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PrinterAlertLight : MonoBehaviour
{
	public enum PlayMode
	{
		Inactive,
		AlertCurve,
		IdleCurve
	}

	public Light pointLight;

	public float peakIntensity;

	public float baseIntensity;

	public Renderer emissiveRenderer;

	public int materialIndex;

	public Color peakEmissiveColor;

	public Color baseEmissiveColor;

	public LensFlareComponentSRP lensFlare;

	public float lensFlareIntensityScale;

	public AnimationCurve alertCurve;

	public AnimationCurve idleCurve;

	public UnityEvent onAlertStart;

	public UnityEvent onAlertCurveComplete;

	public UnityEvent onAlertStop;

	private PlayMode debugPlayMode;

	private PlayMode _mode;

	private float _elapsed;

	private float _alertCurveDuration;

	private float _idleCurveDuration;

	private Material _materialInstance;

	private Action _onAlertCurveDone;

	private static readonly int EmissionColorId;

	public bool IsActive
	{
		get
		{
			bool flag = _mode < PlayMode.Inactive;
			bool flag2 = _mode == PlayMode.Inactive;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public bool IsPlayingAlertCurve
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = _mode - 1;
			return obj == null;
		}
	}

	public bool IsPlayingIdleCurve
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = _mode - 2;
			return obj == null;
		}
	}

	private void Awake()
	{
		//IL_0231: Expected O, but got I4
		//IL_0196: Expected O, but got I
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0346: Expected O, but got I4
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		Light light = default(Light);
		if (pointLight == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			pointLight = light;
		}
		bool flag = emissiveRenderer == null;
		bool flag2 = !flag;
		Renderer renderer = (Renderer)(object)light;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Renderer renderer2 = default(Renderer);
			emissiveRenderer = renderer2;
			renderer = renderer2;
		}
		bool flag3 = lensFlare == null;
		bool flag4 = !flag3;
		LensFlareComponentSRP lensFlareComponentSRP = (LensFlareComponentSRP)(object)renderer;
		if (!flag4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			LensFlareComponentSRP lensFlareComponentSRP2 = default(LensFlareComponentSRP);
			lensFlare = lensFlareComponentSRP2;
			lensFlareComponentSRP = lensFlareComponentSRP2;
		}
		if (emissiveRenderer != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9A9C0");
			if (materialIndex >= 0)
			{
				int num = materialIndex;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rax_v35+18]");
				if ((nint)num < (nint)0)
				{
					int num2 = materialIndex;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rax_v35+20+v576 @ rax_v44 (System.Int32)*8]");
					_materialInstance = (Material)0;
					_materialInstance.EnableKeyword("_EMISSION");
					goto IL_01b5;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"[PrinterAlertLight] Material index {arg} is out of ";
			string text2 = emissiveRenderer.name;
			string message = text + "range on Renderer '" + text2 + "'.";
			Debug.LogWarning(message, this);
		}
		goto IL_01b5;
		IL_01b5:
		float alertCurveDuration = default(float);
		if (alertCurve != null && alertCurve.length != 0)
		{
			Keyframe[] keys = alertCurve.keys;
			int length = alertCurve.length;
			object obj = length - 1;
			object obj2 = obj * 28;
			object obj3 = obj2 + 32;
			object obj4 = obj3 + (object)keys;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
		}
		else
		{
			alertCurveDuration = 1f;
		}
		_alertCurveDuration = alertCurveDuration;
		bool flag5 = idleCurve == null;
		float idleCurveDuration = 1f;
		if (!flag5)
		{
			int length2 = idleCurve.length;
			bool flag6 = length2 == 0;
			idleCurveDuration = 1f;
			if (!flag6)
			{
				Keyframe[] keys2 = idleCurve.keys;
				int length3 = idleCurve.length;
				object obj5 = length3 - 1;
				object obj6 = obj5 * 28;
				object obj7 = obj6 + 32;
				object obj8 = obj7 + (object)keys2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				float num3 = default(float);
				idleCurveDuration = num3;
			}
		}
		_idleCurveDuration = idleCurveDuration;
		ApplyBrightness(0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 477 Invalid \"Jump target not found in method: 0x1804819E0\"");
		throw new NullReferenceException();
	}

	private void OnValidate()
	{
		//IL_005b: Expected O, but got I4
		if (!Application.isPlaying)
		{
			return;
		}
		bool flag = debugPlayMode == PlayMode.Inactive;
		PrinterAlertLight printerAlertLight = this;
		if (!flag)
		{
			object obj = debugPlayMode - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 77 Invalid \"Jump target not found in method: 0x180481990\"");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 84 Invalid \"Jump target not found in method: 0x180481930\"");
			PrinterAlertLight printerAlertLight2 = default(PrinterAlertLight);
			printerAlertLight = printerAlertLight2;
		}
		printerAlertLight.Deactivate();
	}

	private void Update()
	{
		//IL_017a: Invalid comparison between F4 and I4
		//IL_01b4: Expected F4, but got I4
		//IL_00a2: Invalid comparison between I4 and F4
		//IL_01d5: Invalid comparison between I4 and F4
		//IL_00f8: Expected F4, but got I4
		//IL_0220: Expected F4, but got I4
		//IL_025b: Expected O, but got I4
		//IL_00ea: Expected O, but got I4
		if (_mode == PlayMode.Inactive)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		float num = (_elapsed = deltaTime + _elapsed);
		if (_mode == PlayMode.AlertCurve)
		{
			if (num < _alertCurveDuration)
			{
				float num2 = alertCurve.Evaluate(num);
				object obj;
				if (!(0f > num2))
				{
					if (num2 > 1f)
					{
						ApplyBrightness(1f);
						obj = 0;
						goto IL_0226;
					}
				}
				else
				{
					num2 = 0f;
				}
				ApplyBrightness(num2);
				obj = 0;
			}
			else
			{
				_mode = PlayMode.IdleCurve;
				if (onAlertCurveComplete != null)
				{
					onAlertCurveComplete.Invoke();
				}
				Action onAlertCurveDone = _onAlertCurveDone;
				if (_onAlertCurveDone != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v282.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
				}
				debugPlayMode = PlayMode.IdleCurve;
			}
		}
		goto IL_0226;
		IL_0226:
		if (_mode != PlayMode.IdleCurve)
		{
			return;
		}
		float time = ((!(_idleCurveDuration > 0f)) ? 0f : MathF.FMod(_elapsed, _idleCurveDuration));
		float num3 = idleCurve.Evaluate(time);
		if (!(0f > num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		ApplyBrightness(num3);
	}

	public void PlayAlertCurve(Action onDone = null)
	{
		_onAlertCurveDone = onDone;
		_mode = PlayMode.AlertCurve;
		debugPlayMode = PlayMode.AlertCurve;
		SetLightEnabled(state: true);
		if (onAlertStart != null)
		{
			onAlertStart.Invoke();
		}
	}

	public void PlayIdleCurve()
	{
		_onAlertCurveDone = null;
		_mode = PlayMode.IdleCurve;
		debugPlayMode = PlayMode.IdleCurve;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 16 Invalid \"Jump target not found in method: 0x1804819E0\"");
	}

	public void Deactivate()
	{
		_onAlertCurveDone = null;
		_mode = PlayMode.Inactive;
		debugPlayMode = PlayMode.Inactive;
		ApplyBrightness(0f);
		SetLightEnabled(state: false);
		if (onAlertStop != null)
		{
			onAlertStop.Invoke();
		}
	}

	private unsafe void ApplyBrightness(float t)
	{
		//IL_0037: Invalid comparison between I4 and F4
		//IL_008c: Expected F4, but got I4
		//IL_01bc: Invalid comparison between I4 and F4
		//IL_0149: Expected O, but got Ref
		if (pointLight != null)
		{
			float num = ((0f > t) ? 0f : ((t > 1f) ? 1f : t));
			float num2 = peakIntensity - baseIntensity;
			float num3 = num2 * num;
			float intensity = num3 + baseIntensity;
			pointLight.intensity = intensity;
		}
		if (_materialInstance != null)
		{
			if (0f > t || !(t > 1f))
			{
			}
			float num4 = default(float);
			_materialInstance.SetColor(EmissionColorId, (Color)(&num4));
		}
		if (lensFlare != null)
		{
			LensFlareComponentSRP lensFlareComponentSRP = lensFlare;
			float intensity2 = t * lensFlareIntensityScale;
			lensFlareComponentSRP.intensity = intensity2;
		}
	}

	private void SetLightEnabled(bool state)
	{
		if (pointLight != null)
		{
			pointLight.enabled = state;
		}
	}

	private static float GetCurveDuration(AnimationCurve curve)
	{
		//IL_0074: Expected O, but got I4
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		if (curve != null && curve.length != 0)
		{
			Keyframe[] keys = curve.keys;
			int length = curve.length;
			object obj = length - 1;
			object obj2 = obj * 28;
			object obj3 = obj2 + 32;
			object obj4 = obj3 + (object)keys;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
			float result = default(float);
			return result;
		}
		return 1f;
	}

	public PrinterAlertLight()
	{
		//IL_0012: Expected O, but got I
		//IL_002f: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		peakEmissiveColor = (Color)0;
		peakIntensity = 3f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D80]");
		baseEmissiveColor = (Color)0;
		lensFlareIntensityScale = 0.1f;
		alertCurve = AnimationCurve.EaseInOut(0f, 0f, 0.5f, 1f);
		idleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
		base._002Ector();
	}

	static PrinterAlertLight()
	{
		int emissionColorId = Shader.PropertyToID("_EmissionColor");
		EmissionColorId = emissionColorId;
	}
}
