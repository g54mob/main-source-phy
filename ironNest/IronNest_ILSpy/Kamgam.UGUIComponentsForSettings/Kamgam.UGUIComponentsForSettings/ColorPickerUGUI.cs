using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class ColorPickerUGUI : MonoBehaviour
{
	public delegate void OnColorChangedDelegate(Color color);

	public delegate void OnSelectionChangedDelegate(int selectedIndex);

	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public ColorPickerButtonUGUI colorBtn;

		public ColorPickerUGUI _003C_003E4__this;

		internal void _003Cget_ColorButtons_003Eb__0()
		{
			//IL_0055: Expected O, but got I4
			//IL_009c: Expected O, but got I
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ca: Expected O, but got Unknown
			ColorPickerButtonUGUI[] colorButtons = _003C_003E4__this.ColorButtons;
			if (colorButtons == null)
			{
				return;
			}
			ColorPickerButtonUGUI[] colorButtons2 = _003C_003E4__this.ColorButtons;
			if (colorButtons2.Length == 0)
			{
				return;
			}
			ColorPickerButtonUGUI[] array = _003C_003E4__this.ColorButtons;
			object obj = 32;
			int num = 0;
			int num2 = 0;
			while (num < array.Length)
			{
				ColorPickerButtonUGUI[] colorButtons3 = _003C_003E4__this.ColorButtons;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v5+v60 @ rax_v11 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
				if ((UnityEngine.Object)0 != colorBtn)
				{
					num2++;
					obj += 8;
					ColorPickerButtonUGUI[] colorButtons4 = _003C_003E4__this.ColorButtons;
					num = num2;
					array = colorButtons4;
					continue;
				}
				_003C_003E4__this.SelectedIndex = num2;
				break;
			}
			_003C_003E4__this.SetActive(active: false);
		}
	}

	public GameObject Active;

	public Image ColorImage;

	public UnityEvent<Color> OnColorChangedEvent;

	public OnColorChangedDelegate OnColorChanged;

	public UnityEvent<int> OnSelectionChangedEvent;

	public OnSelectionChangedDelegate OnSelectionChanged;

	protected ColorPickerButtonUGUI[] _colorButtons;

	protected int _selectedIndex;

	public ColorPickerButtonUGUI[] ColorButtons
	{
		get
		{
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			//IL_0065: Expected O, but got I4
			//IL_006f: Expected O, but got I4
			//IL_0158: Expected O, but got I
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_0166: Expected O, but got Unknown
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0174: Expected O, but got Unknown
			if (_colorButtons == null)
			{
				ColorPickerButtonUGUI[] componentsInChildren = GetComponentsInChildren<ColorPickerButtonUGUI>(includeInactive: true);
				_colorButtons = componentsInChildren;
				ColorPickerButtonUGUI[] colorButtons = _colorButtons;
				if (_colorButtons == null)
				{
					goto IL_01ab;
				}
				object obj = _colorButtons + 32;
				object obj2 = 0;
				object obj3 = 0;
				object obj4 = default(object);
				while ((nint)obj3 < colorButtons.Length)
				{
					_003C_003Ec__DisplayClass10_0 CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClass10_0();
					if (CS_0024_003C_003E8__locals12 != null)
					{
						CS_0024_003C_003E8__locals12._003C_003E4__this = this;
						CS_0024_003C_003E8__locals12.colorBtn = (ColorPickerButtonUGUI)obj;
						if ((object)CS_0024_003C_003E8__locals12.colorBtn != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							if (obj4 != null)
							{
								UnityAction call = delegate
								{
									//IL_0055: Expected O, but got I4
									//IL_009c: Expected O, but got I
									//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
									//IL_00ca: Expected O, but got Unknown
									ColorPickerButtonUGUI[] colorButtons2 = CS_0024_003C_003E8__locals12._003C_003E4__this.ColorButtons;
									if (colorButtons2 != null)
									{
										ColorPickerButtonUGUI[] colorButtons3 = CS_0024_003C_003E8__locals12._003C_003E4__this.ColorButtons;
										if (colorButtons3.Length != 0)
										{
											ColorPickerButtonUGUI[] array = CS_0024_003C_003E8__locals12._003C_003E4__this.ColorButtons;
											object obj5 = 32;
											int num = 0;
											int num2 = 0;
											while (num < array.Length)
											{
												ColorPickerButtonUGUI[] colorButtons4 = CS_0024_003C_003E8__locals12._003C_003E4__this.ColorButtons;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rsi_v5+v60 @ rax_v11 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
												if (!((UnityEngine.Object)0 != CS_0024_003C_003E8__locals12.colorBtn))
												{
													CS_0024_003C_003E8__locals12._003C_003E4__this.SelectedIndex = num2;
													break;
												}
												num2++;
												obj5 += 8;
												ColorPickerButtonUGUI[] colorButtons5 = CS_0024_003C_003E8__locals12._003C_003E4__this.ColorButtons;
												num = num2;
												array = colorButtons5;
											}
											CS_0024_003C_003E8__locals12._003C_003E4__this.SetActive(active: false);
										}
									}
								};
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ stack_8_v4+100]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ stack_8_v4+100]");
									((UnityEvent)0).AddListener(call);
									obj2++;
									obj += 8;
									obj3 = obj2;
									continue;
								}
							}
						}
					}
					goto IL_01ab;
				}
			}
			return _colorButtons;
			IL_01ab:
			return (ColorPickerButtonUGUI[])(object)new NullReferenceException();
		}
	}

	public bool IsActive
	{
		get
		{
			//IL_006b: Expected I4, but got O
			if ((object)Active != null)
			{
				GameObject gameObject = Active.gameObject;
				if ((object)gameObject != null)
				{
					return gameObject.activeSelf;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public unsafe int SelectedIndex
	{
		get
		{
			return _selectedIndex;
		}
		set
		{
			//IL_00ab: Expected O, but got Ref
			//IL_00e6: Expected O, but got Ref
			if (value == _selectedIndex)
			{
				return;
			}
			_selectedIndex = value;
			ColorPickerButtonUGUI[] colorButtons = ColorButtons;
			if (colorButtons != null)
			{
				ColorPickerButtonUGUI[] colorButtons2 = ColorButtons;
				if (colorButtons2.Length > _selectedIndex)
				{
					ColorPickerButtonUGUI[] colorButtons3 = ColorButtons;
					int selectedIndex = _selectedIndex;
					ColorPickerButtonUGUI colorPickerButtonUGUI = colorButtons3[selectedIndex];
					Color color = default(Color);
					ColorImage.color = (Color)(&color);
					bool flag = OnColorChangedEvent == null;
					color = colorPickerButtonUGUI._color;
					if (!flag)
					{
						OnColorChangedEvent.Invoke((Color)(&color));
						color = colorPickerButtonUGUI._color;
					}
					OnColorChangedDelegate onColorChanged = OnColorChanged;
					if (OnColorChanged != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v154.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
					}
				}
			}
			if (OnSelectionChangedEvent != null)
			{
				object obj = default(object);
				OnSelectionChangedEvent.Invoke((int)(&obj));
			}
			OnSelectionChangedDelegate onSelectionChanged = OnSelectionChanged;
			if (OnSelectionChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v216.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	public void Update()
	{
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl escapeKey = Keyboard._003Ccurrent_003Ek__BackingField.escapeKey;
			if (escapeKey.wasReleasedThisFrame)
			{
				goto IL_0072;
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			return;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (gamepad._003CselectButton_003Ek__BackingField.wasReleasedThisFrame)
		{
			goto IL_0072;
		}
		return;
		IL_0072:
		GameObject gameObject = Active.gameObject;
		if (gameObject.activeSelf)
		{
			SetActive(active: false);
		}
	}

	public void Toggle()
	{
		GameObject gameObject = Active.gameObject;
		GameObject gameObject2 = Active.gameObject;
		bool activeSelf = gameObject2.activeSelf;
		bool active = (byte)((activeSelf ? 1u : 0u) ^ 1u) != 0;
		gameObject.SetActive(active);
	}

	public void SetActive(bool active)
	{
		GameObject gameObject = Active.gameObject;
		bool activeSelf = gameObject.activeSelf;
		if (active != activeSelf && !active)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Component component = default(Component);
			GameObject go = component.gameObject;
			SelectionUtils.SetSelected(go);
		}
		GameObject gameObject2 = Active.gameObject;
		gameObject2.SetActive(active);
	}

	protected unsafe void updateColorImage(Color color)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		ColorImage.color = (Color)(&obj);
	}

	private void onColorButtonClick(ColorPickerButtonUGUI button)
	{
		//IL_004b: Expected O, but got I4
		//IL_008b: Expected O, but got I
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		ColorPickerButtonUGUI[] colorButtons = ColorButtons;
		if (colorButtons == null)
		{
			return;
		}
		ColorPickerButtonUGUI[] colorButtons2 = ColorButtons;
		if (colorButtons2.Length == 0)
		{
			return;
		}
		ColorPickerButtonUGUI[] array = ColorButtons;
		object obj = 32;
		int num = 0;
		int num2 = 0;
		while (num < array.Length)
		{
			ColorPickerButtonUGUI[] colorButtons3 = ColorButtons;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rsi_v6+v173 @ rax_v12 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
			if ((UnityEngine.Object)0 != button)
			{
				num2++;
				obj += 8;
				ColorPickerButtonUGUI[] colorButtons4 = ColorButtons;
				num = num2;
				array = colorButtons4;
				continue;
			}
			SelectedIndex = num2;
			break;
		}
		SetActive(active: false);
	}

	public unsafe void SetColorOptions(IList<Color> colorOptions)
	{
		//IL_0028: Expected O, but got I4
		//IL_005f: Expected O, but got I4
		//IL_0071: Expected O, but got I4
		//IL_007a: Expected O, but got I4
		//IL_0193: Expected O, but got I
		//IL_00fe: Expected O, but got Ref
		//IL_00fe: Expected O, but got I
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_011e: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		ColorPickerButtonUGUI[] colorButtons = ColorButtons;
		ColorPickerButtonUGUI colorPickerButtonUGUI = (ColorPickerButtonUGUI)colorButtons.Length;
		ColorPickerButtonUGUI colorPickerButtonUGUI2 = default(ColorPickerButtonUGUI);
		if ((nint)colorPickerButtonUGUI2 > colorButtons.Length)
		{
			colorPickerButtonUGUI = colorPickerButtonUGUI2;
		}
		if ((nint)colorPickerButtonUGUI <= 0)
		{
			return;
		}
		object obj = 0;
		int num = 0;
		object obj2 = 32;
		object obj3 = 0;
		object obj4 = default(object);
		object obj5 = default(object);
		object obj6 = default(object);
		do
		{
			ColorPickerButtonUGUI[] colorButtons2 = _colorButtons;
			if ((nint)obj < colorButtons2.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4))
				{
					ColorPickerButtonUGUI[] colorButtons3 = _colorButtons;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180068410");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v6+v112 @ rax_v33 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
					((ColorPickerButtonUGUI)0).Color = (Color)(&obj5);
					ColorPickerButtonUGUI[] colorButtons4 = _colorButtons;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v6+v114 @ rax_v36 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
					GameObject gameObject = ((Component)0).gameObject;
					gameObject.SetActive(value: true);
					obj5 = obj6;
					goto IL_028d;
				}
			}
			ColorPickerButtonUGUI[] colorButtons5 = ColorButtons;
			if ((nint)obj < colorButtons5.Length)
			{
				ColorPickerButtonUGUI[] colorButtons6 = ColorButtons;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v6+v117 @ rax_v29 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
				GameObject gameObject2 = ((Component)0).gameObject;
				gameObject2.SetActive(value: false);
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				string text = num.ToString();
				ColorPickerButtonUGUI[] colorButtons7 = ColorButtons;
				string text2 = num.ToString();
				string message = "ColorPickerUGUI: There are more color options (" + text + ") in the than there are ColorPickerButtonUGUI buttons (" + text2 + "). Please add more buttons to the UI.";
				Debug.LogWarning(message);
				num = colorButtons7.Length;
			}
			goto IL_028d;
			IL_028d:
			obj++;
			obj3++;
			obj2 += 8;
		}
		while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<ColorPickerButtonUGUI, UIntPtr>(ref colorPickerButtonUGUI));
	}

	public unsafe List<Color> GetColorOptions()
	{
		//IL_000e: Expected O, but got I4
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_0066: Expected O, but got I
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00d9: Expected O, but got Ref
		List<Color> list = new List<Color>();
		ColorPickerButtonUGUI[] array = ColorButtons;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		object obj4 = default(object);
		while (true)
		{
			if ((nint)obj2 < array.Length)
			{
				ColorPickerButtonUGUI[] colorButtons = ColorButtons;
				if ((nint)obj3 >= colorButtons.Length)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rdi_v5+v85 @ rax_v13 (Kamgam.UGUIComponentsForSettings.ColorPickerButtonUGUI[])]");
				GameObject gameObject = ((Component)0).gameObject;
				if (gameObject.activeSelf)
				{
					ColorPickerButtonUGUI[] colorButtons2 = ColorButtons;
					if ((nint)obj3 >= colorButtons2.Length)
					{
						break;
					}
					list.Add((Color)(&obj4));
				}
				obj3++;
				obj += 8;
				ColorPickerButtonUGUI[] colorButtons3 = ColorButtons;
				obj2 = obj3;
				array = colorButtons3;
				continue;
			}
			return list;
		}
		return (List<Color>)(object)new IndexOutOfRangeException();
	}

	public ColorPickerUGUI()
	{
		//IL_000f: Expected I4, but got I8
		_selectedIndex = -1;
		base._002Ector();
	}
}
