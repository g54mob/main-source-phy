using System;
using Cpp2ILInjected;
using UnityEngine;

public class ProximityActivate : MonoBehaviour
{
	public Transform distanceActivator;

	public Transform lookAtActivator;

	public float distance;

	public Transform activator;

	public bool activeState;

	public CanvasGroup target;

	public bool lookAtCamera = true;

	public bool enableInfoPanel;

	public GameObject infoIcon;

	private float alpha;

	public CanvasGroup infoPanel;

	private Quaternion originRotation;

	private Quaternion targetRotation;

	private void Start()
	{
		//IL_0021: Expected O, but got F4
		//IL_003e: Expected F4, but got I4
		//IL_0059: Expected F4, but got I8
		Transform transform = base.transform;
		originRotation = (Quaternion)transform.rotation.x;
		bool flag = activeState;
		float num = 1f;
		if (!flag)
		{
			num = 4.2949673E+09f;
		}
		alpha = num;
		if (activator == null)
		{
			Camera main = Camera.main;
			Transform transform2 = main.transform;
			activator = transform2;
		}
		bool active = infoPanel != null;
		infoIcon.SetActive(active);
	}

	private bool IsTargetNear()
	{
		//IL_03fb: Expected I4, but got O
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Expected O, but got Unknown
		//IL_024d: Expected I, but got O
		//IL_026d: Expected F4, but got I
		//IL_03a6: Expected I, but got O
		//IL_03c6: Expected F4, but got I
		//IL_0474: Expected O, but got I
		//IL_0491: Expected O, but got I
		//IL_0538: Expected O, but got I
		//IL_0555: Expected O, but got I
		object obj5 = default(object);
		if ((object)distanceActivator != null)
		{
			Vector3 position = distanceActivator.position;
			_ = position.x;
			if ((object)activator != null)
			{
				Vector3 position2 = activator.position;
				float num = position.z - position2.z;
				_ = position2.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-60]");
				float num2 = 0f - position2.x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-5C]");
				object obj2 = default(object);
				object obj = 0 - obj2;
				float num3 = distance * distance;
				float num4 = num * num;
				object obj3 = obj * obj;
				float num5 = num2 * num2;
				float num6 = (float)obj3 + num5;
				float num7 = num6 + num4;
				if (!(num3 > num7))
				{
					goto IL_03df;
				}
				if (!(lookAtActivator != null))
				{
					goto IL_0278;
				}
				if ((object)lookAtActivator != null)
				{
					Vector3 position3 = lookAtActivator.position;
					_ = position3.x;
					if ((object)activator != null)
					{
						Vector3 position4 = activator.position;
						_ = position4.x;
						float num8 = position3.z - position4.z;
						if ((object)activator != null)
						{
							Vector3 forward = activator.forward;
							_ = forward.x;
							object obj4 = obj5 - 96;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
							float num10;
							if (forward.x > 1E-05f)
							{
								float num9 = num8 / forward.x;
								num10 = num9;
							}
							else
							{
								nint num11 = (nint)typeof(Vector3);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v557 @ rax_v31 (Il2CppClass<UnityEngine.Vector3>)+B8]");
								nint num12 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v558 @ rcx_v26 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
								num10 = 0f;
								_ = Vector3.zeroVector;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-50]");
							nint num13 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-60]");
							object obj6 = num13 * 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-4C]");
							nint num14 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-5C]");
							object obj7 = num14 * 0;
							object obj8 = obj7 + obj6;
							float num15 = forward.z * num10;
							num4 = (float)obj8 + num15;
							if (!(num4 > 0.95f))
							{
								goto IL_0278;
							}
							goto IL_03d1;
						}
					}
				}
			}
		}
		goto IL_03ed;
		IL_03df:
		return false;
		IL_03ed:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0278:
		if ((object)target != null)
		{
			Transform transform = target.transform;
			if ((object)transform != null)
			{
				Vector3 position5 = transform.position;
				_ = position5.x;
				if ((object)activator != null)
				{
					Vector3 position6 = activator.position;
					_ = position6.x;
					float num16 = position5.z - position6.z;
					if ((object)activator != null)
					{
						Vector3 forward2 = activator.forward;
						_ = forward2.x;
						object obj9 = obj5 - 80;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
						float num18;
						if (forward2.x > 1E-05f)
						{
							float num17 = num16 / forward2.x;
							num18 = num17;
						}
						else
						{
							nint num19 = (nint)typeof(Vector3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v599 @ rax_v21 (Il2CppClass<UnityEngine.Vector3>)+B8]");
							nint num20 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v18 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
							num18 = 0f;
							_ = Vector3.zeroVector;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-40]");
						nint num21 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-60]");
						object obj10 = num21 * 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-3C]");
						nint num22 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-5C]");
						object obj11 = num22 * 0;
						object obj12 = obj11 + obj10;
						float num23 = forward2.z * num18;
						float num24 = (float)obj12 + num23;
						if (num24 > 0.95f)
						{
							goto IL_03d1;
						}
						goto IL_03df;
					}
				}
			}
		}
		goto IL_03ed;
		IL_03d1:
		return true;
	}

	private unsafe void Update()
	{
		//IL_00c5: Invalid comparison between I4 and F4
		//IL_0110: Expected F4, but got I4
		//IL_01cb: Expected F4, but got I4
		//IL_0274: Expected F4, but got O
		//IL_034e: Invalid comparison between I4 and F4
		//IL_03e3: Expected O, but got F4
		//IL_0207: Expected F4, but got I4
		//IL_0384: Invalid comparison between I4 and F4
		//IL_0243: Expected F4, but got I4
		//IL_02ed: Expected O, but got Ref
		if (activeState)
		{
			bool flag = IsTargetNear();
			if (!flag)
			{
				alpha = -1f;
				activeState = flag;
				enableInfoPanel = flag;
			}
		}
		else if (IsTargetNear())
		{
			alpha = 1f;
			activeState = true;
		}
		float num = target.alpha;
		float deltaTime = Time.deltaTime;
		float num2 = deltaTime * alpha;
		float num3 = num2 + num;
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
		target.alpha = num3;
		if (infoPanel != null)
		{
			if (Input.GetKeyDownInt(KeyCode.Space))
			{
				bool flag2 = !enableInfoPanel;
				enableInfoPanel = flag2;
			}
			float num4 = infoPanel.alpha;
			float num5 = ((!enableInfoPanel) ? 0f : alpha);
			if (!(0f > num5))
			{
				if (num5 > 1f)
				{
					num5 = 1f;
				}
			}
			else
			{
				num5 = 0f;
			}
			float deltaTime2 = Time.deltaTime;
			float num6 = deltaTime2 * 10f;
			if (!(0f > num6))
			{
				if (num6 > 1f)
				{
					num6 = 1f;
				}
			}
			else
			{
				num6 = 0f;
			}
			float num7 = num5 - num4;
			float num8 = num7 * num6;
			float num9 = num8 + num4;
			infoPanel.alpha = num9;
		}
		if (lookAtCamera)
		{
			float num10;
			if (!activeState)
			{
				num10 = (float)originRotation;
			}
			else
			{
				Vector3 position = activator.position;
				Transform transform = base.transform;
				Vector3 position2 = transform.position;
				Vector3 forward = default(Vector3);
				Vector3 upwards = default(Vector3);
				num10 = Quaternion.Internal_LookRotation(ref forward, ref upwards).x;
			}
			targetRotation = (Quaternion)num10;
			Transform transform2 = base.transform;
			Transform transform3 = base.transform;
			Quaternion rotation = transform3.rotation;
			float deltaTime3 = Time.deltaTime;
			Quaternion a = default(Quaternion);
			Quaternion b = default(Quaternion);
			Quaternion quaternion = Quaternion.Internal_Slerp(ref a, ref b, deltaTime3);
			transform2.rotation = (Quaternion)(&b);
		}
	}
}
