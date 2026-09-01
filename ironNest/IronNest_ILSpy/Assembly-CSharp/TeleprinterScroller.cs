using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class TeleprinterScroller : MonoBehaviour
{
	public Teleprinter.Teleprinters teleprinterType;

	public Teleprinter.PrintingOrder printingOrder;

	public float scrollT;

	public float maxScrollUp;

	public bool smoothScroll;

	public float smoothSpeed;

	private LinearSliderInteractable sliderInteractable;

	private float sliderValueAtScrollZero;

	private float sliderValueAtScrollFull;

	private bool disableSliderWhilePrinting;

	private bool rotateWithScroll;

	private float maxScrollRotationDegrees;

	private float rotationSmoothSpeed;

	private GameObject printLockedObject;

	public bool debugScroll;

	private Teleprinter _printer;

	private Vector3 _basePaperLocal;

	private float _currentOffsetLocal;

	private Quaternion _baseRotationLocal;

	private Quaternion _baseRotationWorld;

	private float _currentRotationDegrees;

	private float _directionSign;

	public float ScrollOffset => _currentOffsetLocal;

	public float ScrollRotationDegrees => _currentRotationDegrees;

	public bool ScrollEnabled
	{
		get
		{
			//IL_0078: Expected I4, but got O
			bool flag = _printer != null;
			if (!flag)
			{
				return flag;
			}
			Teleprinter printer = _printer;
			if ((object)_printer != null)
			{
				return !printer._isRunning;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private void OnEnable()
	{
		TryBindPrinter();
		if (sliderInteractable != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderInteractable;
			UnityAction<float> call = HandleSliderValueChanged;
			linearSliderInteractable.OnValueChanged.RemoveListener(call);
			LinearSliderInteractable linearSliderInteractable2 = sliderInteractable;
			UnityAction<float> call2 = HandleSliderValueChanged;
			linearSliderInteractable2.OnValueChanged.AddListener(call2);
		}
	}

	private void OnDisable()
	{
		//IL_0053: Expected I, but got O
		//IL_0360: Expected I, but got O
		//IL_03a1: Expected O, but got I4
		//IL_03fe: Expected O, but got I4
		//IL_0411: Expected I, but got O
		//IL_016a: Expected O, but got I4
		//IL_016f: Expected I, but got O
		//IL_0437: Expected O, but got I4
		//IL_043c: Expected I, but got O
		//IL_0470: Expected O, but got I4
		//IL_0483: Expected I, but got O
		//IL_0286: Expected O, but got I4
		//IL_0293: Expected I, but got O
		//IL_04b1: Expected O, but got I4
		//IL_04c4: Expected I, but got O
		//IL_02d4: Expected O, but got I4
		//IL_0308: Expected O, but got I4
		Delegate obj5;
		Delegate obj6 = default(Delegate);
		NullReferenceException ex;
		Delegate typeFromHandle;
		UnityEngine.Object obj;
		nint num;
		if (_printer != null)
		{
			Teleprinter printer = _printer;
			bool flag = (object)_printer == null;
			obj = null;
			num = unchecked((nint)null);
			if (!flag)
			{
				Action value = HandlePrintingWillStart;
				Delegate obj2 = Delegate.Remove(printer.OnPrintingWillStart, value);
				object obj4;
				if ((object)obj2 == null)
				{
					printer.OnPrintingWillStart = null;
					obj = null;
				}
				else
				{
					bool flag2 = (object)obj2.GetType() != typeof(Action);
					Delegate obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					bool flag3 = (object)obj3 == null;
					obj4 = 0;
					typeFromHandle = (Delegate)(object)typeof(Action);
					num = unchecked((nint)null);
					if (flag3)
					{
						goto IL_04da;
					}
					printer.OnPrintingWillStart = (Action)obj3;
					bool flag4 = (object)obj2.GetType() != typeof(Action);
					obj = null;
					if (!flag4)
					{
						obj = (UnityEngine.Object)(object)obj2;
					}
					bool flag5 = (object)obj == null;
					obj4 = 0;
					num = unchecked((nint)null);
					obj5 = (Delegate)(object)typeof(Action);
					if (flag5)
					{
						goto IL_04e5;
					}
				}
				Teleprinter printer2 = _printer;
				bool flag6 = (object)_printer == null;
				obj4 = 0;
				num = unchecked((nint)null);
				if (!flag6)
				{
					Action value2 = HandlePrinterCleared;
					obj6 = Delegate.Remove(printer2.OnCleared, value2);
					UnityEngine.Object obj7;
					if ((object)obj6 == null)
					{
						printer2.OnCleared = null;
						obj7 = null;
					}
					else
					{
						bool flag7 = (object)obj6.GetType() != typeof(Action);
						Delegate obj8 = null;
						if (!flag7)
						{
							obj8 = obj6;
						}
						bool flag8 = (object)obj8 == null;
						obj4 = 0;
						obj = (UnityEngine.Object)(object)typeof(Action);
						num = unchecked((nint)null);
						if (flag8)
						{
							goto IL_04fd;
						}
						printer2.OnCleared = (Action)obj8;
						bool flag9 = (object)obj6.GetType() != typeof(Action);
						Delegate obj9 = null;
						if (!flag9)
						{
							obj9 = obj6;
						}
						bool flag10 = (object)obj9 == null;
						obj7 = (UnityEngine.Object)(object)obj9;
						obj4 = 0;
						obj = (UnityEngine.Object)(object)typeof(Action);
						num = unchecked((nint)null);
						ex = (NullReferenceException)(object)obj6;
						if (flag10)
						{
							goto IL_0515;
						}
					}
					Teleprinter printer3 = _printer;
					bool flag11 = (object)_printer == null;
					obj4 = 0;
					obj = obj7;
					num = unchecked((nint)null);
					if (!flag11)
					{
						UnityAction call = HandlePrintingEnded;
						bool flag12 = printer3.onAllJobsCompleted == null;
						obj4 = 0;
						obj = this;
						num = 0;
						if (!flag12)
						{
							printer3.onAllJobsCompleted.RemoveListener(call);
							obj4 = 0;
							goto IL_0312;
						}
					}
				}
			}
			goto IL_03d2;
		}
		goto IL_0312;
		IL_03d2:
		ex = new NullReferenceException();
		goto IL_0515;
		IL_04e5:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = obj5;
		goto IL_04da;
		IL_04da:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_0312:
		if (!(sliderInteractable != null))
		{
			return;
		}
		LinearSliderInteractable linearSliderInteractable = sliderInteractable;
		bool flag13 = (object)sliderInteractable == null;
		obj = null;
		num = unchecked((nint)null);
		if (!flag13)
		{
			UnityAction<float> call2 = HandleSliderValueChanged;
			bool flag14 = linearSliderInteractable.OnValueChanged == null;
			object obj4 = 0;
			obj = this;
			num = 0;
			if (!flag14)
			{
				linearSliderInteractable.OnValueChanged.RemoveListener(call2);
				return;
			}
		}
		goto IL_03d2;
		IL_0515:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_04fd;
		IL_04fd:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj5 = obj6;
		goto IL_04e5;
	}

	private void TryBindPrinter()
	{
		//IL_0a9b: Expected I, but got O
		//IL_0ac6: Expected O, but got I4
		//IL_0acb: Expected I, but got O
		//IL_0ad9: Expected I, but got O
		//IL_018d: Expected O, but got I4
		//IL_0192: Expected I, but got O
		//IL_0dc5: Expected I, but got O
		//IL_0aff: Expected O, but got I4
		//IL_0b04: Expected I, but got O
		//IL_0b12: Expected I, but got O
		//IL_0b38: Expected O, but got I4
		//IL_0b3d: Expected I, but got O
		//IL_02a9: Expected O, but got I4
		//IL_02ae: Expected I, but got O
		//IL_0b71: Expected O, but got I4
		//IL_0b76: Expected I, but got O
		//IL_0baa: Expected O, but got I4
		//IL_0baf: Expected I, but got O
		//IL_03c5: Expected O, but got I4
		//IL_03ca: Expected I, but got O
		//IL_0be3: Expected O, but got I4
		//IL_0be8: Expected I, but got O
		//IL_0c1c: Expected O, but got I4
		//IL_0c21: Expected I, but got O
		//IL_04e1: Expected O, but got I4
		//IL_04e6: Expected I, but got O
		//IL_0c5d: Expected O, but got I4
		//IL_0c62: Expected I, but got O
		//IL_052f: Expected O, but got I4
		//IL_057e: Expected O, but got I4
		//IL_0583: Expected I, but got O
		//IL_05cc: Expected O, but got I4
		//IL_061b: Expected O, but got I4
		//IL_0620: Expected I, but got O
		//IL_065e: Expected I, but got O
		//IL_0695: Expected O, but got I4
		//IL_069a: Expected I, but got O
		//IL_075d: Expected O, but got I4
		//IL_0a23: Expected I4, but got O
		//IL_06ca: Expected O, but got I4
		//IL_06cf: Expected I, but got O
		//IL_0793: Expected I, but got O
		//IL_0718: Expected O, but got F4
		//IL_0727: Expected I, but got O
		//IL_07ca: Expected O, but got I4
		//IL_07cf: Expected I, but got O
		//IL_0936: Expected O, but got I4
		//IL_07ff: Expected O, but got I4
		//IL_0804: Expected I, but got O
		//IL_0857: Expected O, but got F4
		//IL_0871: Expected O, but got I4
		//IL_0876: Expected I, but got O
		//IL_09a6: Expected O, but got I4
		//IL_09ab: Expected I, but got O
		//IL_08a4: Expected O, but got I4
		//IL_08a9: Expected I, but got O
		//IL_08b9: Expected O, but got I
		//IL_0cd2: Expected I, but got O
		//IL_0d1f: Expected O, but got I
		//IL_0d69: Invalid comparison between F4 and O
		//IL_0d81: Expected I, but got O
		//IL_08d8: Expected O, but got I
		//IL_08f8: Expected O, but got F4
		//IL_08fd: Expected I, but got O
		//IL_090d: Expected O, but got I
		//IL_09f6: Expected I, but got O
		Teleprinter teleprinter = Teleprinter.GetTeleprinter(teleprinterType);
		_printer = teleprinter;
		if (!(_printer != null))
		{
			return;
		}
		float directionSign = ((printingOrder != Teleprinter.PrintingOrder.BottomUp) ? 1f : (-1f));
		_directionSign = directionSign;
		Teleprinter printer = _printer;
		bool flag = (object)_printer == null;
		nint num = unchecked((nint)null);
		object obj = null;
		nint num3;
		Delegate obj5 = default(Delegate);
		Delegate obj7;
		Delegate obj8 = default(Delegate);
		Delegate obj10;
		Delegate obj11 = default(Delegate);
		Delegate typeFromHandle2;
		NullReferenceException ex;
		nint num2;
		Delegate typeFromHandle;
		object obj4;
		if (!flag)
		{
			Action value = HandlePrintingWillStart;
			Delegate obj2 = Delegate.Remove(printer.OnPrintingWillStart, value);
			if ((object)obj2 == null)
			{
				printer.OnPrintingWillStart = null;
				obj = null;
			}
			else
			{
				bool flag2 = (object)obj2.GetType() != typeof(Action);
				Delegate obj3 = null;
				if (!flag2)
				{
					obj3 = obj2;
				}
				bool flag3 = (object)obj3 == null;
				obj4 = 0;
				num = unchecked((nint)null);
				num2 = (nint)typeof(Action);
				if (flag3)
				{
					goto IL_0d8f;
				}
				printer.OnPrintingWillStart = (Action)obj3;
				bool flag4 = (object)obj2.GetType() != typeof(Action);
				obj = null;
				if (!flag4)
				{
					obj = obj2;
				}
				bool flag5 = obj == null;
				obj4 = 0;
				num = unchecked((nint)null);
				num3 = (nint)typeof(Action);
				if (flag5)
				{
					goto IL_0d9a;
				}
			}
			Teleprinter printer2 = _printer;
			bool flag6 = (object)_printer == null;
			obj4 = 0;
			num = unchecked((nint)null);
			if (!flag6)
			{
				Action b = HandlePrintingWillStart;
				obj5 = Delegate.Combine(printer2.OnPrintingWillStart, b);
				if ((object)obj5 == null)
				{
					printer2.OnPrintingWillStart = null;
					obj = null;
				}
				else
				{
					bool flag7 = (object)obj5.GetType() != typeof(Action);
					Delegate obj6 = null;
					if (!flag7)
					{
						obj6 = obj5;
					}
					bool flag8 = (object)obj6 == null;
					obj4 = 0;
					num = unchecked((nint)null);
					typeFromHandle = (Delegate)(object)typeof(Action);
					if (flag8)
					{
						goto IL_0db2;
					}
					printer2.OnPrintingWillStart = (Action)obj6;
					bool flag9 = (object)obj5.GetType() != typeof(Action);
					obj = null;
					if (!flag9)
					{
						obj = obj5;
					}
					bool flag10 = obj == null;
					obj4 = 0;
					num = unchecked((nint)null);
					obj7 = (Delegate)(object)typeof(Action);
					if (flag10)
					{
						goto IL_0dca;
					}
				}
				Teleprinter printer3 = _printer;
				bool flag11 = (object)_printer == null;
				obj4 = 0;
				num = unchecked((nint)null);
				if (!flag11)
				{
					Action value2 = HandlePrinterCleared;
					obj8 = Delegate.Remove(printer3.OnCleared, value2);
					if ((object)obj8 == null)
					{
						printer3.OnCleared = null;
						obj = null;
					}
					else
					{
						bool flag12 = (object)obj8.GetType() != typeof(Action);
						Delegate obj9 = null;
						if (!flag12)
						{
							obj9 = obj8;
						}
						bool flag13 = (object)obj9 == null;
						obj4 = 0;
						num = unchecked((nint)null);
						typeFromHandle2 = (Delegate)(object)typeof(Action);
						if (flag13)
						{
							goto IL_0de2;
						}
						printer3.OnCleared = (Action)obj9;
						bool flag14 = (object)obj8.GetType() != typeof(Action);
						obj = null;
						if (!flag14)
						{
							obj = obj8;
						}
						bool flag15 = obj == null;
						obj4 = 0;
						num = unchecked((nint)null);
						obj10 = (Delegate)(object)typeof(Action);
						if (flag15)
						{
							goto IL_0dfa;
						}
					}
					Teleprinter printer4 = _printer;
					bool flag16 = (object)_printer == null;
					obj4 = 0;
					num = unchecked((nint)null);
					if (!flag16)
					{
						Action b2 = HandlePrinterCleared;
						obj11 = Delegate.Combine(printer4.OnCleared, b2);
						object obj12;
						if ((object)obj11 == null)
						{
							printer4.OnCleared = null;
							obj12 = null;
						}
						else
						{
							bool flag17 = (object)obj11.GetType() != typeof(Action);
							Delegate obj13 = null;
							if (!flag17)
							{
								obj13 = obj11;
							}
							bool flag18 = (object)obj13 == null;
							obj4 = 0;
							num = unchecked((nint)null);
							obj = typeof(Action);
							if (flag18)
							{
								goto IL_0e12;
							}
							printer4.OnCleared = (Action)obj13;
							bool flag19 = (object)obj11.GetType() != typeof(Action);
							Delegate obj14 = null;
							if (!flag19)
							{
								obj14 = obj11;
							}
							bool flag20 = (object)obj14 == null;
							obj12 = obj14;
							obj4 = 0;
							num = unchecked((nint)null);
							obj = typeof(Action);
							ex = (NullReferenceException)(object)obj11;
							if (flag20)
							{
								goto IL_0e2a;
							}
						}
						Teleprinter printer5 = _printer;
						bool flag21 = (object)_printer == null;
						obj4 = 0;
						num = unchecked((nint)null);
						obj = obj12;
						if (!flag21)
						{
							UnityAction unityAction = HandlePrintingEnded;
							bool flag22 = printer5.onAllJobsCompleted == null;
							obj4 = 0;
							num = 0;
							obj = this;
							if (!flag22)
							{
								printer5.onAllJobsCompleted.RemoveListener(unityAction);
								Teleprinter printer6 = _printer;
								bool flag23 = (object)_printer == null;
								obj4 = 0;
								num = unchecked((nint)null);
								obj = unityAction;
								if (!flag23)
								{
									UnityAction unityAction2 = HandlePrintingEnded;
									bool flag24 = printer6.onAllJobsCompleted == null;
									obj4 = 0;
									num = 0;
									obj = this;
									if (!flag24)
									{
										printer6.onAllJobsCompleted.AddListener(unityAction2);
										Teleprinter printer7 = _printer;
										bool flag25 = (object)_printer == null;
										obj4 = 0;
										num = unchecked((nint)null);
										obj = unityAction2;
										if (!flag25)
										{
											bool flag26 = printer7.paperTransform != null;
											bool flag27 = !flag26;
											num = unchecked((nint)null);
											obj = null;
											if (flag27)
											{
												goto IL_0c86;
											}
											Teleprinter printer8 = _printer;
											bool flag28 = (object)_printer == null;
											obj4 = 0;
											num = unchecked((nint)null);
											obj = null;
											if (!flag28)
											{
												bool flag29 = (object)printer8.paperTransform == null;
												obj4 = 0;
												num = unchecked((nint)null);
												obj = printer8.paperTransform;
												if (!flag29)
												{
													Vector3 localPosition = printer8.paperTransform.localPosition;
													directionSign = localPosition.x;
													_basePaperLocal = (Vector3)localPosition.x;
													_ = localPosition.z;
													num = unchecked((nint)null);
													obj = printer8.paperTransform;
													goto IL_0c86;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0a5c;
		IL_0db2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		num3 = (nint)obj5;
		goto IL_0d9a;
		IL_0de2:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj7 = obj8;
		goto IL_0dca;
		IL_0d8f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_0dfa:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle2 = obj10;
		goto IL_0de2;
		IL_0a5c:
		ex = new NullReferenceException();
		goto IL_0e2a;
		IL_0d9a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		num2 = num3;
		goto IL_0d8f;
		IL_0e12:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		obj10 = obj11;
		goto IL_0dfa;
		IL_0ca5:
		if (rotateWithScroll)
		{
			Teleprinter printer9 = _printer;
			bool flag30 = (object)_printer == null;
			obj4 = 0;
			if (flag30)
			{
				goto IL_0a5c;
			}
			bool flag31 = printer9.rotateTransform != null;
			object obj16 = default(object);
			object obj15 = obj16;
			object obj18 = default(object);
			object obj17 = obj18;
			object obj20 = default(object);
			object obj19 = obj20;
			if (flag31)
			{
				Teleprinter printer10 = _printer;
				bool flag32 = (object)_printer == null;
				obj4 = 0;
				num = unchecked((nint)null);
				obj = null;
				if (flag32)
				{
					goto IL_0a5c;
				}
				nint num4 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1124 @ rax_v76 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num5 = 0;
				object obj21 = printer10.rotationAxis - Vector3.zeroVector;
				object obj23 = default(object);
				object obj24 = default(object);
				object obj22 = obj23 - obj24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rax_v74 (Teleprinter)+80]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1127 @ rcx_v70 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				object obj25 = num6 - 0;
				object obj26 = obj22 * obj22;
				obj19 = obj21 * obj21;
				obj15 = obj25 * obj25;
				object obj27 = obj26 + obj19;
				obj17 = obj27 + obj15;
				bool flag33 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj17);
				directionSign = 9.9999994E-11f;
				num = unchecked((nint)null);
				if (flag33)
				{
					goto IL_09fb;
				}
			}
			if (!debugScroll)
			{
				goto IL_0a51;
			}
			Debug.LogWarning("[TeleprinterScroller] Rotate With Scroll is enabled but the bound Teleprinter has no Rotate Transform and/or Rotation Axis configured — scroll rotation will have no effect.");
			num = unchecked((nint)null);
		}
		goto IL_09fb;
		IL_0a51:
		UpdatePrintLockedObjectState();
		return;
		IL_09fb:
		if (debugScroll)
		{
			object obj28 = default(object);
			object arg = (Teleprinter.Teleprinters)obj28;
			string message = $"[TeleprinterScroller] Bound to Teleprinter '{arg}'.";
			Debug.Log(message);
		}
		goto IL_0a51;
		IL_0dca:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = obj7;
		goto IL_0db2;
		IL_0e2a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0e12;
		IL_0c86:
		if (!rotateWithScroll)
		{
			goto IL_09fb;
		}
		Teleprinter printer11 = _printer;
		bool flag34 = (object)_printer == null;
		obj4 = 0;
		if (!flag34)
		{
			bool flag35 = printer11.rotateTransform != null;
			bool flag36 = !flag35;
			num = unchecked((nint)null);
			obj = null;
			if (flag36)
			{
				goto IL_0ca5;
			}
			Teleprinter printer12 = _printer;
			bool flag37 = (object)_printer == null;
			obj4 = 0;
			num = unchecked((nint)null);
			obj = null;
			if (!flag37)
			{
				bool flag38 = (object)printer12.rotateTransform == null;
				obj4 = 0;
				num = unchecked((nint)null);
				obj = printer12.rotateTransform;
				if (!flag38)
				{
					Quaternion localRotation = printer12.rotateTransform.localRotation;
					obj = _printer;
					directionSign = localRotation.x;
					_baseRotationLocal = (Quaternion)localRotation.x;
					bool flag39 = (object)_printer == null;
					obj4 = 0;
					num = unchecked((nint)null);
					if (!flag39)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v624 @ rdx_v6 (System.Object)+68]");
						bool flag40 = (nint)0 == 0;
						obj4 = 0;
						num = unchecked((nint)null);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v624 @ rdx_v6 (System.Object)+68]");
						obj = 0;
						if (!flag40)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v624 @ rdx_v6 (System.Object)+68]");
							Quaternion rotation = ((Transform)0).rotation;
							directionSign = rotation.x;
							_baseRotationWorld = (Quaternion)rotation.x;
							num = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v624 @ rdx_v6 (System.Object)+68]");
							obj = 0;
							goto IL_0ca5;
						}
					}
				}
			}
		}
		goto IL_0a5c;
	}

	private void HandlePrintingWillStart()
	{
		_currentOffsetLocal = 0f;
		scrollT = 0f;
		ApplyOffset();
		_currentRotationDegrees = 0f;
		ApplyScrollRotation();
		if (debugScroll)
		{
			Debug.Log("[TeleprinterScroller] Print started — scroll reset to 0.");
		}
		UpdatePrintLockedObjectState();
		BackDriveSliderToScrollZero(disableAfter: true);
	}

	private void HandlePrintingEnded()
	{
		if (_printer != null)
		{
			Teleprinter printer = _printer;
			if (printer._isRunning)
			{
				return;
			}
		}
		if (debugScroll)
		{
			Debug.Log("[TeleprinterScroller] Print ended — releasing slider back to player control.");
		}
		UpdatePrintLockedObjectState();
		if (sliderInteractable != null && disableSliderWhilePrinting)
		{
			sliderInteractable.enabled = true;
		}
		if (sliderInteractable != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderInteractable;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
			sliderInteractable.SetSliderValue(sliderValueAtScrollZero);
		}
	}

	private void HandlePrinterCleared()
	{
		//IL_0030: Expected O, but got F4
		Teleprinter printer = _printer;
		Vector3 localPosition = printer.paperTransform.localPosition;
		_basePaperLocal = (Vector3)localPosition.x;
		_ = localPosition.z;
		_currentOffsetLocal = 0f;
		scrollT = 0f;
		ApplyOffset();
		_currentRotationDegrees = 0f;
		ApplyScrollRotation();
		if (sliderInteractable != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderInteractable;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
			sliderInteractable.SetSliderValue(sliderValueAtScrollZero);
		}
	}

	private void Update()
	{
		//IL_00b7: Expected O, but got I4
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Expected O, but got Unknown
		//IL_0315: Expected O, but got F4
		//IL_0150: Expected F4, but got I4
		//IL_0119: Expected O, but got I4
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01ac: Invalid comparison between F4 and O
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Expected F4, but got Unknown
		//IL_01e5: Invalid comparison between F4 and I4
		//IL_022f: Invalid comparison between F4 and I4
		//IL_03fe: Expected O, but got F4
		//IL_03d1: Expected O, but got F4
		if (_printer == null)
		{
			TryBindPrinter();
			if (!(_printer != null))
			{
				return;
			}
		}
		Teleprinter printer = _printer;
		if (!printer._isRunning)
		{
			bool flag = printer.paperTransform != null;
			bool flag2 = !flag;
			object obj = 0;
			if (!flag2)
			{
				Teleprinter printer2 = _printer;
				Vector3 localPosition = printer2.paperTransform.localPosition;
				float num = localPosition.y - _currentOffsetLocal;
				Vector3 basePaperLocal = default(Vector3);
				_basePaperLocal = basePaperLocal;
				_ = localPosition.z;
				obj = 0;
			}
			RecoverRotationBaseWhenIdle();
			UpdatePrintLockedObjectState();
			bool flag3 = !rotateWithScroll;
			float num2 = maxScrollUp * scrollT;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			object obj2 = num2 ^ 0;
			float num3 = (float)obj2 * _directionSign;
			float num5;
			if (!flag3)
			{
				float num4 = maxScrollRotationDegrees * scrollT;
				num5 = num4 * _directionSign;
			}
			else
			{
				num5 = 0f;
			}
			if (!smoothScroll)
			{
				_currentOffsetLocal = num3;
			}
			else
			{
				float deltaTime = Time.deltaTime;
				float num6 = deltaTime * smoothSpeed;
				float num7 = num3 - _currentOffsetLocal;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj3 = num7 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
				{
					float num8 = num3 - _currentOffsetLocal;
					float num9 = ((num8 < 0f) ? (-1f) : 1f);
					float num10 = num9 * num6;
					num3 = num10 + _currentOffsetLocal;
				}
				_currentOffsetLocal = num3;
				float deltaTime2 = Time.deltaTime;
				float num11 = deltaTime2 * rotationSmoothSpeed;
				float num12 = num5 - _currentRotationDegrees;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				float num = num12 & 0;
				if (num11 < num)
				{
					float num13 = num5 - _currentRotationDegrees;
					float num14 = ((num13 < 0f) ? (-1f) : 1f);
					float num15 = num14 * num11;
					num5 = num15 + _currentRotationDegrees;
				}
			}
			_currentRotationDegrees = num5;
			ApplyOffset();
			ApplyScrollRotation();
			if (debugScroll)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object arg2 = default(object);
				object arg3 = default(object);
				string message = $"[TeleprinterScroller] scrollT={arg:F3}  offset={arg2:F3}  rotation={arg3:F1}";
				Debug.Log(message);
			}
			return;
		}
		if (printer.paperTransform != null)
		{
			Teleprinter printer3 = _printer;
			Vector3 localPosition2 = printer3.paperTransform.localPosition;
			_basePaperLocal = (Vector3)localPosition2.x;
			_ = localPosition2.z;
		}
		if (rotateWithScroll && _printer != null)
		{
			Teleprinter printer4 = _printer;
			if (printer4.rotateTransform != null)
			{
				Teleprinter printer5 = _printer;
				if (!printer5.rotateInLocalSpace)
				{
					_baseRotationWorld = (Quaternion)printer4.rotateTransform.rotation.x;
					UpdatePrintLockedObjectState();
					return;
				}
				_baseRotationLocal = (Quaternion)printer4.rotateTransform.localRotation.x;
			}
		}
		UpdatePrintLockedObjectState();
	}

	private unsafe void ApplyOffset()
	{
		//IL_007f: Expected O, but got Ref
		if (_printer != null)
		{
			Teleprinter printer = _printer;
			if (printer.paperTransform != null)
			{
				Teleprinter printer2 = _printer;
				Vector3 vector = default(Vector3);
				printer2.paperTransform.localPosition = (Vector3)(&vector);
			}
		}
	}

	private void UpdatePrintLockedObjectState()
	{
		if (!(printLockedObject != null) || !(_printer != null))
		{
			return;
		}
		Teleprinter printer = _printer;
		bool activeSelf = printLockedObject.activeSelf;
		if (activeSelf == printer._isRunning)
		{
			return;
		}
		printLockedObject.SetActive(printer._isRunning);
		if (debugScroll)
		{
			string[] array = new string[5] { "[TeleprinterScroller] Print-lock object '", null, null, null, null };
			string text = printLockedObject.name;
			array[1] = text;
			array[2] = "' set to ";
			bool flag = (byte)(~(printer._isRunning ? 1u : 0u)) != 0;
			object obj = "INACTIVE (idle)";
			if (!flag)
			{
				obj = "ACTIVE (printing)";
			}
			array[3] = (string)obj;
			array[4] = ".";
			string message = string.Concat(array);
			Debug.Log(message);
		}
	}

	private void HandleSliderValueChanged(float sliderValue)
	{
		//IL_007d: Invalid comparison between I4 and F4
		//IL_008c: Expected O, but got I4
		//IL_0189: Expected F4, but got I4
		//IL_000e: Expected O, but got I4
		//IL_0196: Invalid comparison between O and F4
		//IL_00b5: Expected O, but got I4
		//IL_00e3: Expected F4, but got I4
		//IL_00d5: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018048B277h\"");
		object obj;
		float num3;
		if (sliderValueAtScrollZero == sliderValueAtScrollFull)
		{
			obj = 0;
		}
		else
		{
			float num = sliderValueAtScrollFull - sliderValueAtScrollZero;
			float num2 = sliderValue - sliderValueAtScrollZero;
			num3 = num2 / num;
			bool flag = 0f > num3;
			obj = 0;
			if (!flag)
			{
				bool flag2 = !(num3 > 1f);
				obj = 0;
				if (!flag2)
				{
					num3 = 1f;
					obj = 0;
				}
				goto IL_018e;
			}
		}
		num3 = 0f;
		goto IL_018e;
		IL_018e:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
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
		bool flag3 = !debugScroll;
		scrollT = num3;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string message = $"[TeleprinterScroller] Slider value {arg:F3} → scrollT {arg2:F3}";
			Debug.Log(message);
		}
	}

	private void BackDriveSliderToScrollZero(bool disableAfter)
	{
		if (sliderInteractable != null)
		{
			LinearSliderInteractable linearSliderInteractable = sliderInteractable;
			if (linearSliderInteractable.isDragging)
			{
				linearSliderInteractable.EndSliderDrag();
			}
			sliderInteractable.SetSliderValue(sliderValueAtScrollZero);
			if (disableAfter && disableSliderWhilePrinting)
			{
				sliderInteractable.enabled = false;
			}
		}
	}

	private unsafe Quaternion GetScrollDeltaRotation(float degrees)
	{
		//IL_008d: Expected O, but got I
		//IL_00b0: Invalid comparison between F4 and O
		//IL_011e: Expected F4, but got O
		//IL_0119: Expected native int or pointer, but got O
		//IL_00f4: Expected O, but got F4
		Quaternion quaternion;
		if (_printer != null)
		{
			Teleprinter printer = _printer;
			if ((object)_printer == null)
			{
				return (Quaternion)new NullReferenceException();
			}
			object obj2 = default(object);
			object obj = obj2 * obj2;
			object obj3 = (object)printer.rotationAxis * (object)printer.rotationAxis;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v10 (Teleprinter)+80]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v10 (Teleprinter)+80]");
			object obj4 = num * 0;
			object obj5 = obj + obj3;
			object obj6 = obj5 + obj4;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
			{
				Vector3 axis = default(Vector3);
				Vector3 normalized = axis.normalized;
				quaternion = (Quaternion)Quaternion.Internal_AngleAxis(degrees, ref axis).x;
				goto IL_0111;
			}
		}
		quaternion = Quaternion.identityQuaternion;
		goto IL_0111;
		IL_0111:
		Quaternion quaternion2 = default(Quaternion);
		((Quaternion*)(nint)quaternion2)->x = (float)quaternion;
		return quaternion2;
	}

	private void TrackRotationBaseWhilePrinting()
	{
		//IL_00d8: Expected O, but got F4
		//IL_00b2: Expected O, but got F4
		if (!rotateWithScroll || !(_printer != null))
		{
			return;
		}
		Teleprinter printer = _printer;
		if (printer.rotateTransform != null)
		{
			Teleprinter printer2 = _printer;
			if (!printer2.rotateInLocalSpace)
			{
				_baseRotationWorld = (Quaternion)printer.rotateTransform.rotation.x;
			}
			else
			{
				_baseRotationLocal = (Quaternion)printer.rotateTransform.localRotation.x;
			}
		}
	}

	private void RecoverRotationBaseWhenIdle()
	{
		if (!rotateWithScroll || !(_printer != null))
		{
			return;
		}
		Teleprinter printer = _printer;
		if (printer.rotateTransform != null)
		{
			Quaternion scrollDeltaRotation = GetScrollDeltaRotation(_currentRotationDegrees);
			Teleprinter printer2 = _printer;
			Quaternion rotation = default(Quaternion);
			Quaternion quaternion2 = default(Quaternion);
			if (!printer2.rotateInLocalSpace)
			{
				Quaternion quaternion = Quaternion.Internal_Inverse(ref rotation);
				Quaternion rotation2 = printer.rotateTransform.rotation;
				_baseRotationWorld = quaternion2;
			}
			else
			{
				Quaternion localRotation = printer.rotateTransform.localRotation;
				Quaternion quaternion3 = Quaternion.Internal_Inverse(ref rotation);
				_baseRotationLocal = quaternion2;
			}
		}
	}

	private unsafe void ApplyScrollRotation()
	{
		//IL_0128: Expected I, but got O
		//IL_0175: Expected O, but got I
		//IL_01bf: Invalid comparison between F4 and O
		//IL_00f6: Expected O, but got Ref
		//IL_00d3: Expected O, but got Ref
		if (!rotateWithScroll || !(_printer != null))
		{
			return;
		}
		Teleprinter printer = _printer;
		if (!(printer.rotateTransform != null))
		{
			return;
		}
		Teleprinter printer2 = _printer;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v475 @ rax_v13 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		object obj = printer2.rotationAxis - Vector3.zeroVector;
		object obj3 = default(object);
		object obj4 = default(object);
		object obj2 = obj3 - obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ rax_v11 (Teleprinter)+80]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v478 @ rcx_v11 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		object obj5 = num3 - 0;
		object obj6 = obj2 * obj2;
		object obj7 = obj * obj;
		object obj8 = obj5 * obj5;
		object obj9 = obj6 + obj7;
		object obj10 = obj9 + obj8;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10))
		{
			Quaternion scrollDeltaRotation = GetScrollDeltaRotation(_currentRotationDegrees);
			Teleprinter printer3 = _printer;
			float num4 = default(float);
			if (!printer3.rotateInLocalSpace)
			{
				printer.rotateTransform.rotation = (Quaternion)(&num4);
			}
			else
			{
				printer.rotateTransform.localRotation = (Quaternion)(&num4);
			}
		}
	}

	public void ResetScroll()
	{
		scrollT = 0f;
		_currentOffsetLocal = 0f;
		ApplyOffset();
		_currentRotationDegrees = 0f;
		ApplyScrollRotation();
		if (sliderInteractable != null)
		{
			sliderInteractable.SetSliderValue(sliderValueAtScrollZero);
		}
	}

	public TeleprinterScroller()
	{
		//IL_0055: Expected I, but got O
		maxScrollUp = 5f;
		smoothSpeed = 15f;
		sliderValueAtScrollFull = 1f;
		disableSliderWhilePrinting = true;
		maxScrollRotationDegrees = 90f;
		rotationSmoothSpeed = 90f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_basePaperLocal = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		_baseRotationLocal = Quaternion.identityQuaternion;
		_directionSign = 1f;
		_baseRotationWorld = Quaternion.identityQuaternion;
		base._002Ector();
	}
}
