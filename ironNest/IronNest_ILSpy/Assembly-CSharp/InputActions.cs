using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct PlayerActions(InputActions wrapper)
	{
		private InputActions m_Wrapper = wrapper;

		public InputAction Move
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Move;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Look
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Look;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Fire
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Fire;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Jump
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Jump;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Sprint
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Sprint;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Crouch
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Crouch;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Activate
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Activate;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Freecam
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Player_Freecam;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public bool enabled
		{
			get
			{
				//IL_0070: Expected I4, but got O
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null && wrapper.m_Player != null)
				{
					return wrapper.m_Player.enabled;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}

		public InputActionMap Get()
		{
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null)
			{
				return wrapper.m_Player;
			}
			return (InputActionMap)(object)new NullReferenceException();
		}

		public void Enable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_Player.Enable();
		}

		public void Disable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_Player.Disable();
		}

		public static implicit operator InputActionMap(PlayerActions set)
		{
			//IL_002a: Expected O, but got I
			if ((object)set != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [set @ rcx (InputActions+PlayerActions)+18]");
				return (InputActionMap)0;
			}
			return (InputActionMap)(object)new NullReferenceException();
		}

		public void AddCallbacks(IPlayerActions instance)
		{
			//IL_0089: Expected I, but got O
			//IL_00c1: Expected O, but got I
			//IL_00ca: Expected O, but got I4
			//IL_0158: Expected I, but got O
			//IL_13e9: Expected O, but got I
			//IL_13f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13f7: Expected O, but got Unknown
			//IL_13ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_1404: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_0190: Expected O, but got I
			//IL_0199: Expected O, but got I4
			//IL_0227: Expected I, but got O
			//IL_142c: Expected O, but got I
			//IL_1435: Unknown result type (might be due to invalid IL or missing references)
			//IL_143a: Expected O, but got Unknown
			//IL_1442: Unknown result type (might be due to invalid IL or missing references)
			//IL_1447: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			//IL_025f: Expected O, but got I
			//IL_0268: Expected O, but got I4
			//IL_02f6: Expected I, but got O
			//IL_146f: Expected O, but got I
			//IL_1478: Unknown result type (might be due to invalid IL or missing references)
			//IL_147d: Expected O, but got Unknown
			//IL_1485: Unknown result type (might be due to invalid IL or missing references)
			//IL_148a: Expected O, but got Unknown
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_027b: Expected O, but got Unknown
			//IL_032e: Expected O, but got I
			//IL_0337: Expected O, but got I4
			//IL_03c5: Expected I, but got O
			//IL_14b2: Expected O, but got I
			//IL_14c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_14ce: Expected O, but got Unknown
			//IL_14d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_14db: Expected O, but got Unknown
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_03fd: Expected O, but got I
			//IL_0406: Expected O, but got I4
			//IL_0494: Expected I, but got O
			//IL_1503: Expected O, but got I
			//IL_151a: Unknown result type (might be due to invalid IL or missing references)
			//IL_151f: Expected O, but got Unknown
			//IL_1527: Unknown result type (might be due to invalid IL or missing references)
			//IL_152c: Expected O, but got Unknown
			//IL_0414: Unknown result type (might be due to invalid IL or missing references)
			//IL_0419: Expected O, but got Unknown
			//IL_04cc: Expected O, but got I
			//IL_04d5: Expected O, but got I4
			//IL_0563: Expected I, but got O
			//IL_1554: Expected O, but got I
			//IL_156b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1570: Expected O, but got Unknown
			//IL_1578: Unknown result type (might be due to invalid IL or missing references)
			//IL_157d: Expected O, but got Unknown
			//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e8: Expected O, but got Unknown
			//IL_059b: Expected O, but got I
			//IL_05a4: Expected O, but got I4
			//IL_0632: Expected I, but got O
			//IL_15a5: Expected O, but got I
			//IL_15bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_15c1: Expected O, but got Unknown
			//IL_15c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_15ce: Expected O, but got Unknown
			//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Expected O, but got Unknown
			//IL_066a: Expected O, but got I
			//IL_0673: Expected O, but got I4
			//IL_0701: Expected I, but got O
			//IL_15f6: Expected O, but got I
			//IL_160d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1612: Expected O, but got Unknown
			//IL_161a: Unknown result type (might be due to invalid IL or missing references)
			//IL_161f: Expected O, but got Unknown
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_0686: Expected O, but got Unknown
			//IL_0739: Expected O, but got I
			//IL_0742: Expected O, but got I4
			//IL_07d0: Expected I, but got O
			//IL_1647: Expected O, but got I
			//IL_165e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1663: Expected O, but got Unknown
			//IL_166b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1670: Expected O, but got Unknown
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_0755: Expected O, but got Unknown
			//IL_0808: Expected O, but got I
			//IL_0811: Expected O, but got I4
			//IL_089f: Expected I, but got O
			//IL_1698: Expected O, but got I
			//IL_16af: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b4: Expected O, but got Unknown
			//IL_16bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_16c1: Expected O, but got Unknown
			//IL_081f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0824: Expected O, but got Unknown
			//IL_08d7: Expected O, but got I
			//IL_08e0: Expected O, but got I4
			//IL_096e: Expected I, but got O
			//IL_16e9: Expected O, but got I
			//IL_1700: Unknown result type (might be due to invalid IL or missing references)
			//IL_1705: Expected O, but got Unknown
			//IL_170d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1712: Expected O, but got Unknown
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f3: Expected O, but got Unknown
			//IL_09a6: Expected O, but got I
			//IL_09af: Expected O, but got I4
			//IL_0a3d: Expected I, but got O
			//IL_173a: Expected O, but got I
			//IL_1751: Unknown result type (might be due to invalid IL or missing references)
			//IL_1756: Expected O, but got Unknown
			//IL_175e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1763: Expected O, but got Unknown
			//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c2: Expected O, but got Unknown
			//IL_0a75: Expected O, but got I
			//IL_0a7e: Expected O, but got I4
			//IL_0b0c: Expected I, but got O
			//IL_178b: Expected O, but got I
			//IL_17a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a7: Expected O, but got Unknown
			//IL_17af: Unknown result type (might be due to invalid IL or missing references)
			//IL_17b4: Expected O, but got Unknown
			//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Expected O, but got Unknown
			//IL_0b44: Expected O, but got I
			//IL_0b4d: Expected O, but got I4
			//IL_0bdb: Expected I, but got O
			//IL_17dc: Expected O, but got I
			//IL_17f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f8: Expected O, but got Unknown
			//IL_1800: Unknown result type (might be due to invalid IL or missing references)
			//IL_1805: Expected O, but got Unknown
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Expected O, but got Unknown
			//IL_0c13: Expected O, but got I
			//IL_0c1c: Expected O, but got I4
			//IL_0caa: Expected I, but got O
			//IL_182d: Expected O, but got I
			//IL_1844: Unknown result type (might be due to invalid IL or missing references)
			//IL_1849: Expected O, but got Unknown
			//IL_1851: Unknown result type (might be due to invalid IL or missing references)
			//IL_1856: Expected O, but got Unknown
			//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2f: Expected O, but got Unknown
			//IL_0ce2: Expected O, but got I
			//IL_0ceb: Expected O, but got I4
			//IL_0d79: Expected I, but got O
			//IL_187e: Expected O, but got I
			//IL_1895: Unknown result type (might be due to invalid IL or missing references)
			//IL_189a: Expected O, but got Unknown
			//IL_18a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_18a7: Expected O, but got Unknown
			//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfe: Expected O, but got Unknown
			//IL_0db1: Expected O, but got I
			//IL_0dba: Expected O, but got I4
			//IL_0e48: Expected I, but got O
			//IL_18cf: Expected O, but got I
			//IL_18e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_18eb: Expected O, but got Unknown
			//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_18f8: Expected O, but got Unknown
			//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcd: Expected O, but got Unknown
			//IL_0e80: Expected O, but got I
			//IL_0e89: Expected O, but got I4
			//IL_0f17: Expected I, but got O
			//IL_1920: Expected O, but got I
			//IL_1937: Unknown result type (might be due to invalid IL or missing references)
			//IL_193c: Expected O, but got Unknown
			//IL_1944: Unknown result type (might be due to invalid IL or missing references)
			//IL_1949: Expected O, but got Unknown
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9c: Expected O, but got Unknown
			//IL_0f4f: Expected O, but got I
			//IL_0f58: Expected O, but got I4
			//IL_0fe6: Expected I, but got O
			//IL_1971: Expected O, but got I
			//IL_1988: Unknown result type (might be due to invalid IL or missing references)
			//IL_198d: Expected O, but got Unknown
			//IL_1995: Unknown result type (might be due to invalid IL or missing references)
			//IL_199a: Expected O, but got Unknown
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6b: Expected O, but got Unknown
			//IL_101e: Expected O, but got I
			//IL_1027: Expected O, but got I4
			//IL_10b5: Expected I, but got O
			//IL_19c2: Expected O, but got I
			//IL_19d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_19de: Expected O, but got Unknown
			//IL_19e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_19eb: Expected O, but got Unknown
			//IL_1035: Unknown result type (might be due to invalid IL or missing references)
			//IL_103a: Expected O, but got Unknown
			//IL_10ed: Expected O, but got I
			//IL_10f6: Expected O, but got I4
			//IL_1184: Expected I, but got O
			//IL_1a13: Expected O, but got I
			//IL_1a2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2f: Expected O, but got Unknown
			//IL_1a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a3c: Expected O, but got Unknown
			//IL_1104: Unknown result type (might be due to invalid IL or missing references)
			//IL_1109: Expected O, but got Unknown
			//IL_11bc: Expected O, but got I
			//IL_11c5: Expected O, but got I4
			//IL_1253: Expected I, but got O
			//IL_1a64: Expected O, but got I
			//IL_1a7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a80: Expected O, but got Unknown
			//IL_1a88: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a8d: Expected O, but got Unknown
			//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d8: Expected O, but got Unknown
			//IL_128b: Expected O, but got I
			//IL_1294: Expected O, but got I4
			//IL_1322: Expected I, but got O
			//IL_1ab5: Expected O, but got I
			//IL_1acc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ad1: Expected O, but got Unknown
			//IL_1ad9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ade: Expected O, but got Unknown
			//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a7: Expected O, but got Unknown
			//IL_135a: Expected O, but got I
			//IL_1363: Expected O, but got I4
			//IL_1b06: Expected O, but got I
			//IL_1b1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b22: Expected O, but got Unknown
			//IL_1b2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b2f: Expected O, but got Unknown
			//IL_1371: Unknown result type (might be due to invalid IL or missing references)
			//IL_1376: Expected O, but got Unknown
			if (instance == null)
			{
				return;
			}
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			InputActions wrapper2 = m_Wrapper;
			wrapper2.m_PlayerActionsCallbackInterfaces.Add(instance);
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1078 @ rax_v12+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0101;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1020 @ r8_v149+v1023 @ rax_v383*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v177 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0101;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1020 @ r8_v149+8+v1081 @ rcx_v266*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_0116;
			IL_06aa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_06bf;
			IL_06bf:
			InputActions wrapper4;
			Action<InputAction.CallbackContext> value2;
			wrapper4.m_Player_Fire.performed += value2;
			InputActions wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1832 @ rax_v52+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0779;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1774 @ r8_v125+v1777 @ rax_v299*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v185 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_0779;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1774 @ r8_v125+8+v1835 @ rcx_v218*8]");
			object obj14 = (nint)0 + (nint)2;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_078e;
			IL_139a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_13af;
			IL_0d22:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0d37;
			IL_0d37:
			InputActions wrapper6;
			Action<InputAction.CallbackContext> value4;
			wrapper6.m_Player_Crouch.started += value4;
			InputActions wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2592 @ rax_v92+8]");
			Action<InputAction.CallbackContext> value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0df1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2534 @ r8_v101+v2537 @ rax_v211*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_0df1;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2534 @ r8_v101+8+v2595 @ rcx_v170*8]");
			object obj23 = (nint)0 + (nint)5;
			object obj24 = obj23 << 4;
			object obj25 = obj24 + 312;
			object obj26 = obj25 + num3;
			goto IL_0e06;
			IL_12cb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_12e0;
			IL_0101:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0116;
			IL_0116:
			wrapper3.m_Player_Move.started += value;
			InputActions wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1171 @ rax_v17+8]");
			Action<InputAction.CallbackContext> value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01d0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj27 = 0;
			object obj28 = 0;
			while (true)
			{
				object obj29 = obj28 + obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1113 @ r8_v146+v1116 @ rax_v374*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj28++;
				object obj30 = obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj30 < 0)
				{
					continue;
				}
				goto IL_01d0;
			}
			object obj31 = obj28 + obj28;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1113 @ r8_v146+8+v1174 @ rcx_v260*8]");
			object obj32 = (nint)0 << 4;
			object obj33 = obj32 + 312;
			object obj34 = obj33 + num4;
			goto IL_01e5;
			IL_0fa4:
			InputActions wrapper9;
			Action<InputAction.CallbackContext> value7;
			wrapper9.m_Player_Activate.started += value7;
			InputActions wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2877 @ rax_v107+8]");
			Action<InputAction.CallbackContext> value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_105e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj35 = 0;
			object obj36 = 0;
			while (true)
			{
				object obj37 = obj36 + obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2819 @ r8_v92+v2822 @ rax_v178*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj36++;
				object obj38 = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj38 < 0)
				{
					continue;
				}
				goto IL_105e;
			}
			object obj39 = obj36 + obj36;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2819 @ r8_v92+8+v2880 @ rcx_v152*8]");
			object obj40 = (nint)0 + (nint)6;
			object obj41 = obj40 << 4;
			object obj42 = obj41 + 312;
			object obj43 = obj42 + num5;
			goto IL_1073;
			IL_0779:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_078e;
			IL_078e:
			wrapper5.m_Player_Fire.canceled += value3;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1927 @ rax_v57+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0848;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj44 = 0;
			object obj45 = 0;
			while (true)
			{
				object obj46 = obj45 + obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1869 @ r8_v122+v1872 @ rax_v288*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj45++;
				object obj47 = obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj47 < 0)
				{
					continue;
				}
				goto IL_0848;
			}
			object obj48 = obj45 + obj45;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1869 @ r8_v122+8+v1930 @ rcx_v212*8]");
			object obj49 = (nint)0 + (nint)3;
			object obj50 = obj49 << 4;
			object obj51 = obj50 + 312;
			object obj52 = obj51 + num6;
			goto IL_085d;
			IL_0df1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e06;
			IL_01d0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_01e5;
			IL_01e5:
			wrapper8.m_Player_Move.performed += value6;
			InputActions wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1264 @ rax_v22+8]");
			Action<InputAction.CallbackContext> value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_029f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj53 = 0;
			object obj54 = 0;
			while (true)
			{
				object obj55 = obj54 + obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1206 @ r8_v143+v1209 @ rax_v365*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj54++;
				object obj56 = obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj56 < 0)
				{
					continue;
				}
				goto IL_029f;
			}
			object obj57 = obj54 + obj54;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1206 @ r8_v143+8+v1267 @ rcx_v254*8]");
			object obj58 = (nint)0 << 4;
			object obj59 = obj58 + 312;
			object obj60 = obj59 + num7;
			goto IL_02b4;
			IL_12e0:
			InputActions wrapper13;
			Action<InputAction.CallbackContext> value11;
			wrapper13.m_Player_Freecam.performed += value11;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3255 @ rax_v127+8]");
			Action<InputAction.CallbackContext> value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3185 @ r9_v50 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_139a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3185 @ r9_v50 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj61 = 0;
			object obj62 = 0;
			while (true)
			{
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3199 @ r8_v80+v3204 @ rax_v134*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj62++;
				object obj64 = obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3185 @ r9_v50 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj64 < 0)
				{
					continue;
				}
				goto IL_139a;
			}
			object obj65 = obj62 + obj62;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3199 @ r8_v80+8+v3258 @ rcx_v129*8]");
			object obj66 = (nint)0 + (nint)7;
			object obj67 = obj66 << 4;
			object obj68 = obj67 + 312;
			object obj69 = obj68 + num8;
			goto IL_13af;
			IL_0ab5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0aca;
			IL_0aca:
			InputActions wrapper15;
			Action<InputAction.CallbackContext> value13;
			wrapper15.m_Player_Sprint.started += value13;
			InputActions wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2307 @ rax_v77+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b84;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj70 = 0;
			object obj71 = 0;
			while (true)
			{
				object obj72 = obj71 + obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2249 @ r8_v110+v2252 @ rax_v244*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj71++;
				object obj73 = obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj73 < 0)
				{
					continue;
				}
				goto IL_0b84;
			}
			object obj74 = obj71 + obj71;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2249 @ r8_v110+8+v2310 @ rcx_v188*8]");
			object obj75 = (nint)0 + (nint)4;
			object obj76 = obj75 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_0b99;
			IL_0e06:
			wrapper7.m_Player_Crouch.performed += value5;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2687 @ rax_v97+8]");
			Action<InputAction.CallbackContext> value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ec0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2629 @ r8_v98+v2632 @ rax_v200*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_0ec0;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2629 @ r8_v98+8+v2690 @ rcx_v164*8]");
			object obj84 = (nint)0 + (nint)5;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_0ed5;
			IL_029f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_02b4;
			IL_02b4:
			wrapper12.m_Player_Move.canceled += value10;
			InputActions wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1357 @ rax_v27+8]");
			Action<InputAction.CallbackContext> value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_036e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1299 @ r8_v140+v1302 @ rax_v354*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_036e;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1299 @ r8_v140+8+v1360 @ rcx_v248*8]");
			object obj93 = (nint)0 + (nint)1;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_0383;
			IL_13af:
			wrapper14.m_Player_Freecam.canceled += value12;
			return;
			IL_0848:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_085d;
			IL_085d:
			wrapper11.m_Player_Jump.started += value9;
			InputActions wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2022 @ rax_v62+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0917;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1964 @ r8_v119+v1967 @ rax_v277*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v187 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_0917;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1964 @ r8_v119+8+v2025 @ rcx_v206*8]");
			object obj102 = (nint)0 + (nint)3;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_092c;
			IL_1073:
			wrapper10.m_Player_Activate.performed += value8;
			InputActions wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2972 @ rax_v112+8]");
			Action<InputAction.CallbackContext> value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_112d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2914 @ r8_v89+v2917 @ rax_v167*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_112d;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2914 @ r8_v89+8+v2975 @ rcx_v146*8]");
			object obj111 = (nint)0 + (nint)6;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_1142;
			IL_036e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0383;
			IL_0383:
			wrapper18.m_Player_Look.started += value16;
			InputActions wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1452 @ rax_v32+8]");
			Action<InputAction.CallbackContext> value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_043d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1394 @ r8_v137+v1397 @ rax_v343*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_043d;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1394 @ r8_v137+8+v1455 @ rcx_v242*8]");
			object obj120 = (nint)0 + (nint)1;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_0452;
			IL_0ed5:
			wrapper17.m_Player_Crouch.canceled += value15;
			wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2782 @ rax_v102+8]");
			value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f8f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2724 @ r8_v95+v2727 @ rax_v189*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v195 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_0f8f;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2724 @ r8_v95+8+v2785 @ rcx_v158*8]");
			object obj129 = (nint)0 + (nint)6;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_0fa4;
			IL_0c53:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c68;
			IL_0c68:
			InputActions wrapper22;
			Action<InputAction.CallbackContext> value20;
			wrapper22.m_Player_Sprint.canceled += value20;
			wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2497 @ rax_v87+8]");
			value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d22;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2439 @ r8_v104+v2442 @ rax_v222*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_0d22;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2439 @ r8_v104+8+v2500 @ rcx_v176*8]");
			object obj138 = (nint)0 + (nint)5;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_0d37;
			IL_112d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1142;
			IL_043d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0452;
			IL_0452:
			wrapper21.m_Player_Look.performed += value19;
			InputActions wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1547 @ rax_v37+8]");
			Action<InputAction.CallbackContext> value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_050c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1489 @ r8_v134+v1492 @ rax_v332*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_050c;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1489 @ r8_v134+8+v1550 @ rcx_v236*8]");
			object obj147 = (nint)0 + (nint)1;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_0521;
			IL_11fc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1211;
			IL_0917:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_092c;
			IL_092c:
			wrapper19.m_Player_Jump.performed += value17;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2117 @ rax_v67+8]");
			Action<InputAction.CallbackContext> value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_09e6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2059 @ r8_v116+v2062 @ rax_v266*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_09e6;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2059 @ r8_v116+8+v2120 @ rcx_v200*8]");
			object obj156 = (nint)0 + (nint)3;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_09fb;
			IL_1142:
			wrapper20.m_Player_Activate.canceled += value18;
			InputActions wrapper25 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3067 @ rax_v117+8]");
			Action<InputAction.CallbackContext> value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r10_v25 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_11fc;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r10_v25 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3009 @ r8_v86+v3012 @ rax_v156*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ r10_v25 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_11fc;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3009 @ r8_v86+8+v3070 @ rcx_v140*8]");
			object obj165 = (nint)0 + (nint)7;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_1211;
			IL_050c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0521;
			IL_0521:
			wrapper23.m_Player_Look.canceled += value21;
			InputActions wrapper26 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1642 @ rax_v42+8]");
			Action<InputAction.CallbackContext> value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_05db;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1584 @ r8_v131+v1587 @ rax_v321*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_05db;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1584 @ r8_v131+8+v1645 @ rcx_v230*8]");
			object obj174 = (nint)0 + (nint)2;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_05f0;
			IL_0ec0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0ed5;
			IL_0b84:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b99;
			IL_0b99:
			wrapper16.m_Player_Sprint.performed += value14;
			wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2402 @ rax_v82+8]");
			value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0c53;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2344 @ r8_v107+v2347 @ rax_v233*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_0c53;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2344 @ r8_v107+8+v2405 @ rcx_v182*8]");
			object obj183 = (nint)0 + (nint)4;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_0c68;
			IL_105e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1073;
			IL_05db:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_05f0;
			IL_05f0:
			wrapper26.m_Player_Fire.started += value24;
			wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1737 @ rax_v47+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_06aa;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ r8_v128+v1682 @ rax_v310*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_06aa;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ r8_v128+8+v1740 @ rcx_v224*8]");
			object obj192 = (nint)0 + (nint)2;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_06bf;
			IL_1211:
			wrapper25.m_Player_Freecam.started += value23;
			wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3162 @ rax_v122+8]");
			value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r10_v26 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_12cb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r10_v26 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3104 @ r8_v83+v3107 @ rax_v145*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r10_v26 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_12cb;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3104 @ r8_v83+8+v3165 @ rcx_v134*8]");
			object obj201 = (nint)0 + (nint)7;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_12e0;
			IL_09e6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_09fb;
			IL_09fb:
			wrapper24.m_Player_Jump.canceled += value22;
			wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2212 @ rax_v72+8]");
			value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ab5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2154 @ r8_v113+v2157 @ rax_v255*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_0ab5;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2154 @ r8_v113+8+v2215 @ rcx_v194*8]");
			object obj210 = (nint)0 + (nint)4;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_0aca;
			IL_0f8f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0fa4;
		}

		private void UnregisterCallbacks(IPlayerActions instance)
		{
			//IL_002b: Expected I, but got O
			//IL_0063: Expected O, but got I
			//IL_006c: Expected O, but got I4
			//IL_00fa: Expected I, but got O
			//IL_1387: Expected O, but got I
			//IL_1390: Unknown result type (might be due to invalid IL or missing references)
			//IL_1395: Expected O, but got Unknown
			//IL_139d: Unknown result type (might be due to invalid IL or missing references)
			//IL_13a2: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0132: Expected O, but got I
			//IL_013b: Expected O, but got I4
			//IL_01c9: Expected I, but got O
			//IL_13ca: Expected O, but got I
			//IL_13d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_13d8: Expected O, but got Unknown
			//IL_13e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e5: Expected O, but got Unknown
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Expected O, but got Unknown
			//IL_0201: Expected O, but got I
			//IL_020a: Expected O, but got I4
			//IL_0298: Expected I, but got O
			//IL_140d: Expected O, but got I
			//IL_1416: Unknown result type (might be due to invalid IL or missing references)
			//IL_141b: Expected O, but got Unknown
			//IL_1423: Unknown result type (might be due to invalid IL or missing references)
			//IL_1428: Expected O, but got Unknown
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Expected O, but got Unknown
			//IL_02d0: Expected O, but got I
			//IL_02d9: Expected O, but got I4
			//IL_0367: Expected I, but got O
			//IL_1450: Expected O, but got I
			//IL_1467: Unknown result type (might be due to invalid IL or missing references)
			//IL_146c: Expected O, but got Unknown
			//IL_1474: Unknown result type (might be due to invalid IL or missing references)
			//IL_1479: Expected O, but got Unknown
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ec: Expected O, but got Unknown
			//IL_039f: Expected O, but got I
			//IL_03a8: Expected O, but got I4
			//IL_0436: Expected I, but got O
			//IL_14a1: Expected O, but got I
			//IL_14b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_14bd: Expected O, but got Unknown
			//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_14ca: Expected O, but got Unknown
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Expected O, but got Unknown
			//IL_046e: Expected O, but got I
			//IL_0477: Expected O, but got I4
			//IL_0505: Expected I, but got O
			//IL_14f2: Expected O, but got I
			//IL_1509: Unknown result type (might be due to invalid IL or missing references)
			//IL_150e: Expected O, but got Unknown
			//IL_1516: Unknown result type (might be due to invalid IL or missing references)
			//IL_151b: Expected O, but got Unknown
			//IL_0485: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Expected O, but got Unknown
			//IL_053d: Expected O, but got I
			//IL_0546: Expected O, but got I4
			//IL_05d4: Expected I, but got O
			//IL_1543: Expected O, but got I
			//IL_155a: Unknown result type (might be due to invalid IL or missing references)
			//IL_155f: Expected O, but got Unknown
			//IL_1567: Unknown result type (might be due to invalid IL or missing references)
			//IL_156c: Expected O, but got Unknown
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_0559: Expected O, but got Unknown
			//IL_060c: Expected O, but got I
			//IL_0615: Expected O, but got I4
			//IL_06a3: Expected I, but got O
			//IL_1594: Expected O, but got I
			//IL_15ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_15b0: Expected O, but got Unknown
			//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_15bd: Expected O, but got Unknown
			//IL_0623: Unknown result type (might be due to invalid IL or missing references)
			//IL_0628: Expected O, but got Unknown
			//IL_06db: Expected O, but got I
			//IL_06e4: Expected O, but got I4
			//IL_0772: Expected I, but got O
			//IL_15e5: Expected O, but got I
			//IL_15fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1601: Expected O, but got Unknown
			//IL_1609: Unknown result type (might be due to invalid IL or missing references)
			//IL_160e: Expected O, but got Unknown
			//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f7: Expected O, but got Unknown
			//IL_07aa: Expected O, but got I
			//IL_07b3: Expected O, but got I4
			//IL_0841: Expected I, but got O
			//IL_1636: Expected O, but got I
			//IL_164d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1652: Expected O, but got Unknown
			//IL_165a: Unknown result type (might be due to invalid IL or missing references)
			//IL_165f: Expected O, but got Unknown
			//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Expected O, but got Unknown
			//IL_0879: Expected O, but got I
			//IL_0882: Expected O, but got I4
			//IL_0910: Expected I, but got O
			//IL_1687: Expected O, but got I
			//IL_169e: Unknown result type (might be due to invalid IL or missing references)
			//IL_16a3: Expected O, but got Unknown
			//IL_16ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b0: Expected O, but got Unknown
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_0895: Expected O, but got Unknown
			//IL_0948: Expected O, but got I
			//IL_0951: Expected O, but got I4
			//IL_09df: Expected I, but got O
			//IL_16d8: Expected O, but got I
			//IL_16ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_16f4: Expected O, but got Unknown
			//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_1701: Expected O, but got Unknown
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0964: Expected O, but got Unknown
			//IL_0a17: Expected O, but got I
			//IL_0a20: Expected O, but got I4
			//IL_0aae: Expected I, but got O
			//IL_1729: Expected O, but got I
			//IL_1740: Unknown result type (might be due to invalid IL or missing references)
			//IL_1745: Expected O, but got Unknown
			//IL_174d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1752: Expected O, but got Unknown
			//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a33: Expected O, but got Unknown
			//IL_0ae6: Expected O, but got I
			//IL_0aef: Expected O, but got I4
			//IL_0b7d: Expected I, but got O
			//IL_177a: Expected O, but got I
			//IL_1791: Unknown result type (might be due to invalid IL or missing references)
			//IL_1796: Expected O, but got Unknown
			//IL_179e: Unknown result type (might be due to invalid IL or missing references)
			//IL_17a3: Expected O, but got Unknown
			//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b02: Expected O, but got Unknown
			//IL_0bb5: Expected O, but got I
			//IL_0bbe: Expected O, but got I4
			//IL_0c4c: Expected I, but got O
			//IL_17cb: Expected O, but got I
			//IL_17e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_17e7: Expected O, but got Unknown
			//IL_17ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f4: Expected O, but got Unknown
			//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0c84: Expected O, but got I
			//IL_0c8d: Expected O, but got I4
			//IL_0d1b: Expected I, but got O
			//IL_181c: Expected O, but got I
			//IL_1833: Unknown result type (might be due to invalid IL or missing references)
			//IL_1838: Expected O, but got Unknown
			//IL_1840: Unknown result type (might be due to invalid IL or missing references)
			//IL_1845: Expected O, but got Unknown
			//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca0: Expected O, but got Unknown
			//IL_0d53: Expected O, but got I
			//IL_0d5c: Expected O, but got I4
			//IL_0dea: Expected I, but got O
			//IL_186d: Expected O, but got I
			//IL_1884: Unknown result type (might be due to invalid IL or missing references)
			//IL_1889: Expected O, but got Unknown
			//IL_1891: Unknown result type (might be due to invalid IL or missing references)
			//IL_1896: Expected O, but got Unknown
			//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6f: Expected O, but got Unknown
			//IL_0e22: Expected O, but got I
			//IL_0e2b: Expected O, but got I4
			//IL_0eb9: Expected I, but got O
			//IL_18be: Expected O, but got I
			//IL_18d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_18da: Expected O, but got Unknown
			//IL_18e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_18e7: Expected O, but got Unknown
			//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3e: Expected O, but got Unknown
			//IL_0ef1: Expected O, but got I
			//IL_0efa: Expected O, but got I4
			//IL_0f88: Expected I, but got O
			//IL_190f: Expected O, but got I
			//IL_1926: Unknown result type (might be due to invalid IL or missing references)
			//IL_192b: Expected O, but got Unknown
			//IL_1933: Unknown result type (might be due to invalid IL or missing references)
			//IL_1938: Expected O, but got Unknown
			//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Expected O, but got Unknown
			//IL_0fc0: Expected O, but got I
			//IL_0fc9: Expected O, but got I4
			//IL_1057: Expected I, but got O
			//IL_1960: Expected O, but got I
			//IL_1977: Unknown result type (might be due to invalid IL or missing references)
			//IL_197c: Expected O, but got Unknown
			//IL_1984: Unknown result type (might be due to invalid IL or missing references)
			//IL_1989: Expected O, but got Unknown
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdc: Expected O, but got Unknown
			//IL_108f: Expected O, but got I
			//IL_1098: Expected O, but got I4
			//IL_1126: Expected I, but got O
			//IL_19b1: Expected O, but got I
			//IL_19c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_19cd: Expected O, but got Unknown
			//IL_19d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_19da: Expected O, but got Unknown
			//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ab: Expected O, but got Unknown
			//IL_115e: Expected O, but got I
			//IL_1167: Expected O, but got I4
			//IL_11f5: Expected I, but got O
			//IL_1a02: Expected O, but got I
			//IL_1a19: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a1e: Expected O, but got Unknown
			//IL_1a26: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a2b: Expected O, but got Unknown
			//IL_1175: Unknown result type (might be due to invalid IL or missing references)
			//IL_117a: Expected O, but got Unknown
			//IL_122d: Expected O, but got I
			//IL_1236: Expected O, but got I4
			//IL_12c4: Expected I, but got O
			//IL_1a53: Expected O, but got I
			//IL_1a6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a6f: Expected O, but got Unknown
			//IL_1a77: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a7c: Expected O, but got Unknown
			//IL_1244: Unknown result type (might be due to invalid IL or missing references)
			//IL_1249: Expected O, but got Unknown
			//IL_12fc: Expected O, but got I
			//IL_1305: Expected O, but got I4
			//IL_1aa4: Expected O, but got I
			//IL_1abb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ac0: Expected O, but got Unknown
			//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1acd: Expected O, but got Unknown
			//IL_1313: Unknown result type (might be due to invalid IL or missing references)
			//IL_1318: Expected O, but got Unknown
			InputActions wrapper = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v965 @ rax_v6+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ r10_v2 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00a3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ r10_v2 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v907 @ r8_v145+v910 @ rax_v377*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ r10_v2 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_00a3;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v907 @ r8_v145+8+v968 @ rcx_v262*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_00b8;
			IL_133c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1351;
			IL_0cc4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0cd9;
			IL_0cd9:
			InputActions wrapper2;
			Action<InputAction.CallbackContext> value2;
			wrapper2.m_Player_Crouch.started -= value2;
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2479 @ rax_v86+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d93;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2421 @ r8_v97+v2424 @ rax_v205*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r10_v18 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_0d93;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2421 @ r8_v97+8+v2482 @ rcx_v166*8]");
			object obj14 = (nint)0 + (nint)5;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_0da8;
			IL_126d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1282;
			IL_00a3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_00b8;
			IL_00b8:
			wrapper.m_Player_Move.started -= value;
			InputActions wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1058 @ rax_v11+8]");
			Action<InputAction.CallbackContext> value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v3 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0172;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v3 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1000 @ r8_v142+v1003 @ rax_v368*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r10_v3 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_0172;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1000 @ r8_v142+8+v1061 @ rcx_v256*8]");
			object obj23 = (nint)0 << 4;
			object obj24 = obj23 + 312;
			object obj25 = obj24 + num3;
			goto IL_0187;
			IL_0f46:
			InputActions wrapper5;
			Action<InputAction.CallbackContext> value5;
			wrapper5.m_Player_Activate.started -= value5;
			InputActions wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2764 @ rax_v101+8]");
			Action<InputAction.CallbackContext> value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1000;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj26 = 0;
			object obj27 = 0;
			while (true)
			{
				object obj28 = obj27 + obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2706 @ r8_v88+v2709 @ rax_v172*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj27++;
				object obj29 = obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r10_v21 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj29 < 0)
				{
					continue;
				}
				goto IL_1000;
			}
			object obj30 = obj27 + obj27;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2706 @ r8_v88+8+v2767 @ rcx_v148*8]");
			object obj31 = (nint)0 + (nint)6;
			object obj32 = obj31 << 4;
			object obj33 = obj32 + 312;
			object obj34 = obj33 + num4;
			goto IL_1015;
			IL_071b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0730;
			IL_0730:
			InputActions wrapper7;
			Action<InputAction.CallbackContext> value7;
			wrapper7.m_Player_Fire.canceled -= value7;
			InputActions wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1814 @ rax_v51+8]");
			Action<InputAction.CallbackContext> value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_07ea;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj35 = 0;
			object obj36 = 0;
			while (true)
			{
				object obj37 = obj36 + obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1756 @ r8_v118+v1759 @ rax_v282*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj36++;
				object obj38 = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ r10_v11 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj38 < 0)
				{
					continue;
				}
				goto IL_07ea;
			}
			object obj39 = obj36 + obj36;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1756 @ r8_v118+8+v1817 @ rcx_v208*8]");
			object obj40 = (nint)0 + (nint)3;
			object obj41 = obj40 << 4;
			object obj42 = obj41 + 312;
			object obj43 = obj42 + num5;
			goto IL_07ff;
			IL_0d93:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0da8;
			IL_0172:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0187;
			IL_0187:
			wrapper4.m_Player_Move.performed -= value4;
			InputActions wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1151 @ rax_v16+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0241;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj44 = 0;
			object obj45 = 0;
			while (true)
			{
				object obj46 = obj45 + obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1093 @ r8_v139+v1096 @ rax_v359*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj45++;
				object obj47 = obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ r10_v4 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj47 < 0)
				{
					continue;
				}
				goto IL_0241;
			}
			object obj48 = obj45 + obj45;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1093 @ r8_v139+8+v1154 @ rcx_v250*8]");
			object obj49 = (nint)0 << 4;
			object obj50 = obj49 + 312;
			object obj51 = obj50 + num6;
			goto IL_0256;
			IL_1282:
			InputActions wrapper10;
			Action<InputAction.CallbackContext> value10;
			wrapper10.m_Player_Freecam.performed -= value10;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3142 @ rax_v121+8]");
			Action<InputAction.CallbackContext> value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3072 @ r9_v48 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_133c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3072 @ r9_v48 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj52 = 0;
			object obj53 = 0;
			while (true)
			{
				object obj54 = obj53 + obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3086 @ r8_v76+v3091 @ rax_v128*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj53++;
				object obj55 = obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3072 @ r9_v48 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj55 < 0)
				{
					continue;
				}
				goto IL_133c;
			}
			object obj56 = obj53 + obj53;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3086 @ r8_v76+8+v3145 @ rcx_v125*8]");
			object obj57 = (nint)0 + (nint)7;
			object obj58 = obj57 << 4;
			object obj59 = obj58 + 312;
			object obj60 = obj59 + num7;
			goto IL_1351;
			IL_0a57:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0a6c;
			IL_0a6c:
			InputActions wrapper12;
			Action<InputAction.CallbackContext> value12;
			wrapper12.m_Player_Sprint.started -= value12;
			InputActions wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2194 @ rax_v71+8]");
			Action<InputAction.CallbackContext> value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b26;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj61 = 0;
			object obj62 = 0;
			while (true)
			{
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2136 @ r8_v106+v2139 @ rax_v238*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj62++;
				object obj64 = obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ r10_v15 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj64 < 0)
				{
					continue;
				}
				goto IL_0b26;
			}
			object obj65 = obj62 + obj62;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2136 @ r8_v106+8+v2197 @ rcx_v184*8]");
			object obj66 = (nint)0 + (nint)4;
			object obj67 = obj66 << 4;
			object obj68 = obj67 + 312;
			object obj69 = obj68 + num8;
			goto IL_0b3b;
			IL_0da8:
			wrapper3.m_Player_Crouch.performed -= value3;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2574 @ rax_v91+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0e62;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj70 = 0;
			object obj71 = 0;
			while (true)
			{
				object obj72 = obj71 + obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2516 @ r8_v94+v2519 @ rax_v194*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj71++;
				object obj73 = obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r10_v19 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj73 < 0)
				{
					continue;
				}
				goto IL_0e62;
			}
			object obj74 = obj71 + obj71;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2516 @ r8_v94+8+v2577 @ rcx_v160*8]");
			object obj75 = (nint)0 + (nint)5;
			object obj76 = obj75 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_0e77;
			IL_0241:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0256;
			IL_0256:
			wrapper9.m_Player_Move.canceled -= value9;
			InputActions wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1244 @ rax_v21+8]");
			Action<InputAction.CallbackContext> value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0310;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1186 @ r8_v136+v1189 @ rax_v348*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r10_v5 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_0310;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1186 @ r8_v136+8+v1247 @ rcx_v244*8]");
			object obj84 = (nint)0 + (nint)1;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_0325;
			IL_1351:
			wrapper11.m_Player_Freecam.canceled -= value11;
			return;
			IL_07ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_07ff;
			IL_07ff:
			wrapper8.m_Player_Jump.started -= value8;
			InputActions wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1909 @ rax_v56+8]");
			Action<InputAction.CallbackContext> value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_08b9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1851 @ r8_v115+v1854 @ rax_v271*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ r10_v12 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_08b9;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1851 @ r8_v115+8+v1912 @ rcx_v202*8]");
			object obj93 = (nint)0 + (nint)3;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_08ce;
			IL_1015:
			wrapper6.m_Player_Activate.performed -= value6;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2859 @ rax_v106+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_10cf;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2801 @ r8_v85+v2804 @ rax_v161*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ r10_v22 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_10cf;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2801 @ r8_v85+8+v2862 @ rcx_v142*8]");
			object obj102 = (nint)0 + (nint)6;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_10e4;
			IL_0310:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0325;
			IL_0325:
			wrapper15.m_Player_Look.started -= value15;
			InputActions wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1339 @ rax_v26+8]");
			Action<InputAction.CallbackContext> value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_03df;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1281 @ r8_v133+v1284 @ rax_v337*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r10_v6 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_03df;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1281 @ r8_v133+8+v1342 @ rcx_v238*8]");
			object obj111 = (nint)0 + (nint)1;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_03f4;
			IL_0e77:
			wrapper14.m_Player_Crouch.canceled -= value14;
			wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2669 @ rax_v96+8]");
			value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f31;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2611 @ r8_v91+v2614 @ rax_v183*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v171 @ r10_v20 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_0f31;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2611 @ r8_v91+8+v2672 @ rcx_v154*8]");
			object obj120 = (nint)0 + (nint)6;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_0f46;
			IL_0bf5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c0a;
			IL_0c0a:
			InputActions wrapper19;
			Action<InputAction.CallbackContext> value19;
			wrapper19.m_Player_Sprint.canceled -= value19;
			wrapper2 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2384 @ rax_v81+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0cc4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2326 @ r8_v100+v2329 @ rax_v216*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ r10_v17 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_0cc4;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2326 @ r8_v100+8+v2387 @ rcx_v172*8]");
			object obj129 = (nint)0 + (nint)5;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_0cd9;
			IL_10cf:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_10e4;
			IL_03df:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_03f4;
			IL_03f4:
			wrapper18.m_Player_Look.performed -= value18;
			InputActions wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1434 @ rax_v31+8]");
			Action<InputAction.CallbackContext> value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_04ae;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1376 @ r8_v130+v1379 @ rax_v326*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r10_v7 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_04ae;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1376 @ r8_v130+8+v1437 @ rcx_v232*8]");
			object obj138 = (nint)0 + (nint)1;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_04c3;
			IL_119e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_11b3;
			IL_08b9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_08ce;
			IL_08ce:
			wrapper16.m_Player_Jump.performed -= value16;
			InputActions wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2004 @ rax_v61+8]");
			Action<InputAction.CallbackContext> value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0988;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1946 @ r8_v112+v1949 @ rax_v260*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ r10_v13 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_0988;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1946 @ r8_v112+8+v2007 @ rcx_v196*8]");
			object obj147 = (nint)0 + (nint)3;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_099d;
			IL_10e4:
			wrapper17.m_Player_Activate.canceled -= value17;
			InputActions wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2954 @ rax_v111+8]");
			Action<InputAction.CallbackContext> value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_119e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ r8_v82+v2899 @ rax_v150*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v23 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_119e;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2896 @ r8_v82+8+v2957 @ rcx_v136*8]");
			object obj156 = (nint)0 + (nint)7;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_11b3;
			IL_04ae:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_04c3;
			IL_04c3:
			wrapper20.m_Player_Look.canceled -= value20;
			InputActions wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1529 @ rax_v36+8]");
			Action<InputAction.CallbackContext> value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_057d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1471 @ r8_v127+v1474 @ rax_v315*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r10_v8 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_057d;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1471 @ r8_v127+8+v1532 @ rcx_v226*8]");
			object obj165 = (nint)0 + (nint)2;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_0592;
			IL_0e62:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e77;
			IL_0b26:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b3b;
			IL_0b3b:
			wrapper13.m_Player_Sprint.performed -= value13;
			wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2289 @ rax_v76+8]");
			value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0bf5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2231 @ r8_v103+v2234 @ rax_v227*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r10_v16 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_0bf5;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2231 @ r8_v103+8+v2292 @ rcx_v178*8]");
			object obj174 = (nint)0 + (nint)4;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_0c0a;
			IL_1000:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1015;
			IL_057d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0592;
			IL_0592:
			wrapper23.m_Player_Fire.started -= value23;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1624 @ rax_v41+8]");
			Action<InputAction.CallbackContext> value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_064c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1566 @ r8_v124+v1569 @ rax_v304*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v160 @ r10_v9 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_064c;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1566 @ r8_v124+8+v1627 @ rcx_v220*8]");
			object obj183 = (nint)0 + (nint)2;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_0661;
			IL_11b3:
			wrapper22.m_Player_Freecam.started -= value22;
			wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3049 @ rax_v116+8]");
			value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_126d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2991 @ r8_v79+v2994 @ rax_v139*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ r10_v24 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_126d;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2991 @ r8_v79+8+v3052 @ rcx_v130*8]");
			object obj192 = (nint)0 + (nint)7;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_1282;
			IL_0988:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_099d;
			IL_099d:
			wrapper21.m_Player_Jump.canceled -= value21;
			wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2099 @ rax_v66+8]");
			value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0a57;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2041 @ r8_v109+v2044 @ rax_v249*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r10_v14 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_0a57;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2041 @ r8_v109+8+v2102 @ rcx_v190*8]");
			object obj201 = (nint)0 + (nint)4;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_0a6c;
			IL_0f31:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0f46;
			IL_064c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0661;
			IL_0661:
			wrapper24.m_Player_Fire.performed -= value24;
			wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1719 @ rax_v46+8]");
			value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_071b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1661 @ r8_v121+v1664 @ rax_v293*8]");
				if (0 == (nint)typeof(IPlayerActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ r10_v10 (Il2CppClass<InputActions+IPlayerActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_071b;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1661 @ r8_v121+8+v1722 @ rcx_v214*8]");
			object obj210 = (nint)0 + (nint)2;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_0730;
		}

		public void RemoveCallbacks(IPlayerActions instance)
		{
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public unsafe void SetCallbacks(IPlayerActions instance)
		{
			//IL_01d2: Expected O, but got Ref
			//IL_008d: Expected O, but got Ref
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null && wrapper.m_PlayerActionsCallbackInterfaces != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<IPlayerActions>.Enumerator enumerator = default(List<IPlayerActions>.Enumerator);
				IPlayerActions instance2 = default(IPlayerActions);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					UnregisterCallbacks(instance2);
				}
				enumerator.Dispose();
				InputActions wrapper2 = m_Wrapper;
				bool flag = m_Wrapper == null;
				List<IPlayerActions>.Enumerator enumerator2 = (List<IPlayerActions>.Enumerator)(&enumerator);
				if (!flag)
				{
					List<IPlayerActions> playerActionsCallbackInterfaces = wrapper2.m_PlayerActionsCallbackInterfaces;
					bool flag2 = wrapper2.m_PlayerActionsCallbackInterfaces == null;
					enumerator2 = (List<IPlayerActions>.Enumerator)(&enumerator);
					if (!flag2)
					{
						int version = playerActionsCallbackInterfaces._version + 1;
						playerActionsCallbackInterfaces._version = version;
						((List<IPlayerActions>.Enumerator*)null)->Dispose();
						object obj = default(object);
						if (obj == null)
						{
							playerActionsCallbackInterfaces._size = 0;
						}
						else
						{
							playerActionsCallbackInterfaces._size = 0;
							if (playerActionsCallbackInterfaces._size > 0)
							{
								Array.Clear(playerActionsCallbackInterfaces._items, 0, playerActionsCallbackInterfaces._size);
							}
						}
						AddCallbacks(instance);
						return;
					}
				}
			}
			throw new NullReferenceException();
		}
	}

	public struct UIActions(InputActions wrapper)
	{
		private InputActions m_Wrapper = wrapper;

		public InputAction Click
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Click;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Point
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Point;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Navigate
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Navigate;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction MoveUI
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_MoveUI;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Submit
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Submit;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Cancel
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Cancel;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction ScrollWheel
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_ScrollWheel;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction MiddleClick
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_MiddleClick;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction TrackedDevicePosition
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_TrackedDevicePosition;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction TrackedDeviceOrientation
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_TrackedDeviceOrientation;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Up
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Up;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Down
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_UI_Down;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public bool enabled
		{
			get
			{
				//IL_0070: Expected I4, but got O
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null && wrapper.m_UI != null)
				{
					return wrapper.m_UI.enabled;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}

		public InputActionMap Get()
		{
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null)
			{
				return wrapper.m_UI;
			}
			return (InputActionMap)(object)new NullReferenceException();
		}

		public void Enable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_UI.Enable();
		}

		public void Disable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_UI.Disable();
		}

		public static implicit operator InputActionMap(UIActions set)
		{
			//IL_002a: Expected O, but got I
			if ((object)set != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [set @ rcx (InputActions+UIActions)+68]");
				return (InputActionMap)0;
			}
			return (InputActionMap)(object)new NullReferenceException();
		}

		public void AddCallbacks(IUIActions instance)
		{
			//IL_0089: Expected I, but got O
			//IL_00c1: Expected O, but got I
			//IL_00ca: Expected O, but got I4
			//IL_0158: Expected I, but got O
			//IL_1d9d: Expected O, but got I
			//IL_1da6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dab: Expected O, but got Unknown
			//IL_1db3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db8: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_0190: Expected O, but got I
			//IL_0199: Expected O, but got I4
			//IL_0227: Expected I, but got O
			//IL_1de0: Expected O, but got I
			//IL_1de9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dee: Expected O, but got Unknown
			//IL_1df6: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dfb: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			//IL_025f: Expected O, but got I
			//IL_0268: Expected O, but got I4
			//IL_02f6: Expected I, but got O
			//IL_1e23: Expected O, but got I
			//IL_1e2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e31: Expected O, but got Unknown
			//IL_1e39: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e3e: Expected O, but got Unknown
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_027b: Expected O, but got Unknown
			//IL_032e: Expected O, but got I
			//IL_0337: Expected O, but got I4
			//IL_03c5: Expected I, but got O
			//IL_1e66: Expected O, but got I
			//IL_1e7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e82: Expected O, but got Unknown
			//IL_1e8a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e8f: Expected O, but got Unknown
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_03fd: Expected O, but got I
			//IL_0406: Expected O, but got I4
			//IL_0494: Expected I, but got O
			//IL_1eb7: Expected O, but got I
			//IL_1ece: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ed3: Expected O, but got Unknown
			//IL_1edb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ee0: Expected O, but got Unknown
			//IL_0414: Unknown result type (might be due to invalid IL or missing references)
			//IL_0419: Expected O, but got Unknown
			//IL_04cc: Expected O, but got I
			//IL_04d5: Expected O, but got I4
			//IL_0563: Expected I, but got O
			//IL_1f08: Expected O, but got I
			//IL_1f1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f24: Expected O, but got Unknown
			//IL_1f2c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f31: Expected O, but got Unknown
			//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e8: Expected O, but got Unknown
			//IL_059b: Expected O, but got I
			//IL_05a4: Expected O, but got I4
			//IL_0632: Expected I, but got O
			//IL_1f59: Expected O, but got I
			//IL_1f70: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f75: Expected O, but got Unknown
			//IL_1f7d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f82: Expected O, but got Unknown
			//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Expected O, but got Unknown
			//IL_066a: Expected O, but got I
			//IL_0673: Expected O, but got I4
			//IL_0701: Expected I, but got O
			//IL_1faa: Expected O, but got I
			//IL_1fc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fc6: Expected O, but got Unknown
			//IL_1fce: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fd3: Expected O, but got Unknown
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_0686: Expected O, but got Unknown
			//IL_0739: Expected O, but got I
			//IL_0742: Expected O, but got I4
			//IL_07d0: Expected I, but got O
			//IL_1ffb: Expected O, but got I
			//IL_2012: Unknown result type (might be due to invalid IL or missing references)
			//IL_2017: Expected O, but got Unknown
			//IL_201f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2024: Expected O, but got Unknown
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_0755: Expected O, but got Unknown
			//IL_0808: Expected O, but got I
			//IL_0811: Expected O, but got I4
			//IL_089f: Expected I, but got O
			//IL_204c: Expected O, but got I
			//IL_2063: Unknown result type (might be due to invalid IL or missing references)
			//IL_2068: Expected O, but got Unknown
			//IL_2070: Unknown result type (might be due to invalid IL or missing references)
			//IL_2075: Expected O, but got Unknown
			//IL_081f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0824: Expected O, but got Unknown
			//IL_08d7: Expected O, but got I
			//IL_08e0: Expected O, but got I4
			//IL_096e: Expected I, but got O
			//IL_209d: Expected O, but got I
			//IL_20b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_20b9: Expected O, but got Unknown
			//IL_20c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_20c6: Expected O, but got Unknown
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f3: Expected O, but got Unknown
			//IL_09a6: Expected O, but got I
			//IL_09af: Expected O, but got I4
			//IL_0a3d: Expected I, but got O
			//IL_20ee: Expected O, but got I
			//IL_2105: Unknown result type (might be due to invalid IL or missing references)
			//IL_210a: Expected O, but got Unknown
			//IL_2112: Unknown result type (might be due to invalid IL or missing references)
			//IL_2117: Expected O, but got Unknown
			//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c2: Expected O, but got Unknown
			//IL_0a75: Expected O, but got I
			//IL_0a7e: Expected O, but got I4
			//IL_0b0c: Expected I, but got O
			//IL_213f: Expected O, but got I
			//IL_2156: Unknown result type (might be due to invalid IL or missing references)
			//IL_215b: Expected O, but got Unknown
			//IL_2163: Unknown result type (might be due to invalid IL or missing references)
			//IL_2168: Expected O, but got Unknown
			//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Expected O, but got Unknown
			//IL_0b44: Expected O, but got I
			//IL_0b4d: Expected O, but got I4
			//IL_0bdb: Expected I, but got O
			//IL_2190: Expected O, but got I
			//IL_21a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_21ac: Expected O, but got Unknown
			//IL_21b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_21b9: Expected O, but got Unknown
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Expected O, but got Unknown
			//IL_0c13: Expected O, but got I
			//IL_0c1c: Expected O, but got I4
			//IL_0caa: Expected I, but got O
			//IL_21e1: Expected O, but got I
			//IL_21f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_21fd: Expected O, but got Unknown
			//IL_2205: Unknown result type (might be due to invalid IL or missing references)
			//IL_220a: Expected O, but got Unknown
			//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2f: Expected O, but got Unknown
			//IL_0ce2: Expected O, but got I
			//IL_0ceb: Expected O, but got I4
			//IL_0d79: Expected I, but got O
			//IL_2232: Expected O, but got I
			//IL_2249: Unknown result type (might be due to invalid IL or missing references)
			//IL_224e: Expected O, but got Unknown
			//IL_2256: Unknown result type (might be due to invalid IL or missing references)
			//IL_225b: Expected O, but got Unknown
			//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfe: Expected O, but got Unknown
			//IL_0db1: Expected O, but got I
			//IL_0dba: Expected O, but got I4
			//IL_0e48: Expected I, but got O
			//IL_2283: Expected O, but got I
			//IL_229a: Unknown result type (might be due to invalid IL or missing references)
			//IL_229f: Expected O, but got Unknown
			//IL_22a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_22ac: Expected O, but got Unknown
			//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcd: Expected O, but got Unknown
			//IL_0e80: Expected O, but got I
			//IL_0e89: Expected O, but got I4
			//IL_0f17: Expected I, but got O
			//IL_22d4: Expected O, but got I
			//IL_22eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_22f0: Expected O, but got Unknown
			//IL_22f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_22fd: Expected O, but got Unknown
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9c: Expected O, but got Unknown
			//IL_0f4f: Expected O, but got I
			//IL_0f58: Expected O, but got I4
			//IL_0fe6: Expected I, but got O
			//IL_2325: Expected O, but got I
			//IL_233c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2341: Expected O, but got Unknown
			//IL_2349: Unknown result type (might be due to invalid IL or missing references)
			//IL_234e: Expected O, but got Unknown
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6b: Expected O, but got Unknown
			//IL_101e: Expected O, but got I
			//IL_1027: Expected O, but got I4
			//IL_10b5: Expected I, but got O
			//IL_2376: Expected O, but got I
			//IL_238d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2392: Expected O, but got Unknown
			//IL_239a: Unknown result type (might be due to invalid IL or missing references)
			//IL_239f: Expected O, but got Unknown
			//IL_1035: Unknown result type (might be due to invalid IL or missing references)
			//IL_103a: Expected O, but got Unknown
			//IL_10ed: Expected O, but got I
			//IL_10f6: Expected O, but got I4
			//IL_1184: Expected I, but got O
			//IL_23c7: Expected O, but got I
			//IL_23de: Unknown result type (might be due to invalid IL or missing references)
			//IL_23e3: Expected O, but got Unknown
			//IL_23eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_23f0: Expected O, but got Unknown
			//IL_1104: Unknown result type (might be due to invalid IL or missing references)
			//IL_1109: Expected O, but got Unknown
			//IL_11bc: Expected O, but got I
			//IL_11c5: Expected O, but got I4
			//IL_1253: Expected I, but got O
			//IL_2418: Expected O, but got I
			//IL_242f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2434: Expected O, but got Unknown
			//IL_243c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2441: Expected O, but got Unknown
			//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d8: Expected O, but got Unknown
			//IL_128b: Expected O, but got I
			//IL_1294: Expected O, but got I4
			//IL_1322: Expected I, but got O
			//IL_2469: Expected O, but got I
			//IL_2480: Unknown result type (might be due to invalid IL or missing references)
			//IL_2485: Expected O, but got Unknown
			//IL_248d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2492: Expected O, but got Unknown
			//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a7: Expected O, but got Unknown
			//IL_135a: Expected O, but got I
			//IL_1363: Expected O, but got I4
			//IL_13f1: Expected I, but got O
			//IL_24ba: Expected O, but got I
			//IL_24d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d6: Expected O, but got Unknown
			//IL_24de: Unknown result type (might be due to invalid IL or missing references)
			//IL_24e3: Expected O, but got Unknown
			//IL_1371: Unknown result type (might be due to invalid IL or missing references)
			//IL_1376: Expected O, but got Unknown
			//IL_1429: Expected O, but got I
			//IL_1432: Expected O, but got I4
			//IL_14c0: Expected I, but got O
			//IL_250b: Expected O, but got I
			//IL_2522: Unknown result type (might be due to invalid IL or missing references)
			//IL_2527: Expected O, but got Unknown
			//IL_252f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2534: Expected O, but got Unknown
			//IL_1440: Unknown result type (might be due to invalid IL or missing references)
			//IL_1445: Expected O, but got Unknown
			//IL_14f8: Expected O, but got I
			//IL_1501: Expected O, but got I4
			//IL_158f: Expected I, but got O
			//IL_255c: Expected O, but got I
			//IL_2573: Unknown result type (might be due to invalid IL or missing references)
			//IL_2578: Expected O, but got Unknown
			//IL_2580: Unknown result type (might be due to invalid IL or missing references)
			//IL_2585: Expected O, but got Unknown
			//IL_150f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1514: Expected O, but got Unknown
			//IL_15c7: Expected O, but got I
			//IL_15d0: Expected O, but got I4
			//IL_165e: Expected I, but got O
			//IL_25ad: Expected O, but got I
			//IL_25c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_25c9: Expected O, but got Unknown
			//IL_25d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_25d6: Expected O, but got Unknown
			//IL_15de: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e3: Expected O, but got Unknown
			//IL_1696: Expected O, but got I
			//IL_169f: Expected O, but got I4
			//IL_172d: Expected I, but got O
			//IL_25fe: Expected O, but got I
			//IL_2615: Unknown result type (might be due to invalid IL or missing references)
			//IL_261a: Expected O, but got Unknown
			//IL_2622: Unknown result type (might be due to invalid IL or missing references)
			//IL_2627: Expected O, but got Unknown
			//IL_16ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b2: Expected O, but got Unknown
			//IL_1765: Expected O, but got I
			//IL_176e: Expected O, but got I4
			//IL_17fc: Expected I, but got O
			//IL_264f: Expected O, but got I
			//IL_2666: Unknown result type (might be due to invalid IL or missing references)
			//IL_266b: Expected O, but got Unknown
			//IL_2673: Unknown result type (might be due to invalid IL or missing references)
			//IL_2678: Expected O, but got Unknown
			//IL_177c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1781: Expected O, but got Unknown
			//IL_1834: Expected O, but got I
			//IL_183d: Expected O, but got I4
			//IL_18cb: Expected I, but got O
			//IL_26a0: Expected O, but got I
			//IL_26b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_26bc: Expected O, but got Unknown
			//IL_26c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_26c9: Expected O, but got Unknown
			//IL_184b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1850: Expected O, but got Unknown
			//IL_1903: Expected O, but got I
			//IL_190c: Expected O, but got I4
			//IL_199a: Expected I, but got O
			//IL_26f1: Expected O, but got I
			//IL_2708: Unknown result type (might be due to invalid IL or missing references)
			//IL_270d: Expected O, but got Unknown
			//IL_2715: Unknown result type (might be due to invalid IL or missing references)
			//IL_271a: Expected O, but got Unknown
			//IL_191a: Unknown result type (might be due to invalid IL or missing references)
			//IL_191f: Expected O, but got Unknown
			//IL_19d2: Expected O, but got I
			//IL_19db: Expected O, but got I4
			//IL_1a69: Expected I, but got O
			//IL_2742: Expected O, but got I
			//IL_2759: Unknown result type (might be due to invalid IL or missing references)
			//IL_275e: Expected O, but got Unknown
			//IL_2766: Unknown result type (might be due to invalid IL or missing references)
			//IL_276b: Expected O, but got Unknown
			//IL_19e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ee: Expected O, but got Unknown
			//IL_1aa1: Expected O, but got I
			//IL_1aaa: Expected O, but got I4
			//IL_1b38: Expected I, but got O
			//IL_2793: Expected O, but got I
			//IL_27aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_27af: Expected O, but got Unknown
			//IL_27b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_27bc: Expected O, but got Unknown
			//IL_1ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1abd: Expected O, but got Unknown
			//IL_1b70: Expected O, but got I
			//IL_1b79: Expected O, but got I4
			//IL_1c07: Expected I, but got O
			//IL_27e4: Expected O, but got I
			//IL_27fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_2800: Expected O, but got Unknown
			//IL_2808: Unknown result type (might be due to invalid IL or missing references)
			//IL_280d: Expected O, but got Unknown
			//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b8c: Expected O, but got Unknown
			//IL_1c3f: Expected O, but got I
			//IL_1c48: Expected O, but got I4
			//IL_1cd6: Expected I, but got O
			//IL_2835: Expected O, but got I
			//IL_284c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2851: Expected O, but got Unknown
			//IL_2859: Unknown result type (might be due to invalid IL or missing references)
			//IL_285e: Expected O, but got Unknown
			//IL_1c56: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c5b: Expected O, but got Unknown
			//IL_1d0e: Expected O, but got I
			//IL_1d17: Expected O, but got I4
			//IL_2886: Expected O, but got I
			//IL_289d: Unknown result type (might be due to invalid IL or missing references)
			//IL_28a2: Expected O, but got Unknown
			//IL_28aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_28af: Expected O, but got Unknown
			//IL_1d25: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d2a: Expected O, but got Unknown
			if (instance == null)
			{
				return;
			}
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_UIActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			InputActions wrapper2 = m_Wrapper;
			wrapper2.m_UIActionsCallbackInterfaces.Add(instance);
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1474 @ rax_v12+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0101;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1416 @ r8_v221+v1419 @ rax_v575*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0101;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1416 @ r8_v221+8+v1477 @ rcx_v398*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_0116;
			IL_09e6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_09fb;
			IL_09fb:
			InputActions wrapper4;
			Action<InputAction.CallbackContext> value2;
			wrapper4.m_UI_MoveUI.canceled += value2;
			InputActions wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2608 @ rax_v72+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ab5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2550 @ r8_v185+v2553 @ rax_v447*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_0ab5;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2550 @ r8_v185+8+v2611 @ rcx_v326*8]");
			object obj14 = (nint)0 + (nint)4;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_0aca;
			IL_1bb0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1bc5;
			IL_12cb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_12e0;
			IL_12e0:
			InputActions wrapper6;
			Action<InputAction.CallbackContext> value4;
			wrapper6.m_UI_MiddleClick.performed += value4;
			InputActions wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3653 @ rax_v127+8]");
			Action<InputAction.CallbackContext> value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_139a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3595 @ r8_v152+v3598 @ rax_v326*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_139a;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3595 @ r8_v152+8+v3656 @ rcx_v260*8]");
			object obj23 = (nint)0 + (nint)7;
			object obj24 = obj23 << 4;
			object obj25 = obj24 + 312;
			object obj26 = obj25 + num3;
			goto IL_13af;
			IL_1607:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_161c;
			IL_0101:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0116;
			IL_0116:
			wrapper3.m_UI_Click.started += value;
			InputActions wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1567 @ rax_v17+8]");
			Action<InputAction.CallbackContext> value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01d0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj27 = 0;
			object obj28 = 0;
			while (true)
			{
				object obj29 = obj28 + obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1509 @ r8_v218+v1512 @ rax_v566*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj28++;
				object obj30 = obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj30 < 0)
				{
					continue;
				}
				goto IL_01d0;
			}
			object obj31 = obj28 + obj28;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1509 @ r8_v218+8+v1570 @ rcx_v392*8]");
			object obj32 = (nint)0 << 4;
			object obj33 = obj32 + 312;
			object obj34 = obj33 + num4;
			goto IL_01e5;
			IL_161c:
			InputActions wrapper9;
			Action<InputAction.CallbackContext> value7;
			wrapper9.m_UI_TrackedDevicePosition.canceled += value7;
			InputActions wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4033 @ rax_v147+8]");
			Action<InputAction.CallbackContext> value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_16d6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj35 = 0;
			object obj36 = 0;
			while (true)
			{
				object obj37 = obj36 + obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3975 @ r8_v140+v3978 @ rax_v282*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj36++;
				object obj38 = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj38 < 0)
				{
					continue;
				}
				goto IL_16d6;
			}
			object obj39 = obj36 + obj36;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3975 @ r8_v140+8+v4036 @ rcx_v236*8]");
			object obj40 = (nint)0 + (nint)9;
			object obj41 = obj40 << 4;
			object obj42 = obj41 + 312;
			object obj43 = obj42 + num5;
			goto IL_16eb;
			IL_0ab5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0aca;
			IL_0aca:
			wrapper5.m_UI_Submit.started += value3;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2703 @ rax_v77+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b84;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj44 = 0;
			object obj45 = 0;
			while (true)
			{
				object obj46 = obj45 + obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2645 @ r8_v182+v2648 @ rax_v436*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj45++;
				object obj47 = obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj47 < 0)
				{
					continue;
				}
				goto IL_0b84;
			}
			object obj48 = obj45 + obj45;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2645 @ r8_v182+8+v2706 @ rcx_v320*8]");
			object obj49 = (nint)0 + (nint)4;
			object obj50 = obj49 << 4;
			object obj51 = obj50 + 312;
			object obj52 = obj51 + num6;
			goto IL_0b99;
			IL_1538:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_154d;
			IL_01d0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_01e5;
			IL_01e5:
			wrapper8.m_UI_Click.performed += value6;
			InputActions wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1660 @ rax_v22+8]");
			Action<InputAction.CallbackContext> value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_029f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj53 = 0;
			object obj54 = 0;
			while (true)
			{
				object obj55 = obj54 + obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1602 @ r8_v215+v1605 @ rax_v557*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj54++;
				object obj56 = obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj56 < 0)
				{
					continue;
				}
				goto IL_029f;
			}
			object obj57 = obj54 + obj54;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1602 @ r8_v215+8+v1663 @ rcx_v386*8]");
			object obj58 = (nint)0 << 4;
			object obj59 = obj58 + 312;
			object obj60 = obj59 + num7;
			goto IL_02b4;
			IL_1c7f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1c94;
			IL_0f8f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0fa4;
			IL_0fa4:
			InputActions wrapper13;
			Action<InputAction.CallbackContext> value11;
			wrapper13.m_UI_ScrollWheel.started += value11;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3273 @ rax_v107+8]");
			Action<InputAction.CallbackContext> value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_105e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj61 = 0;
			object obj62 = 0;
			while (true)
			{
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3215 @ r8_v164+v3218 @ rax_v370*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj62++;
				object obj64 = obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj64 < 0)
				{
					continue;
				}
				goto IL_105e;
			}
			object obj65 = obj62 + obj62;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3215 @ r8_v164+8+v3276 @ rcx_v284*8]");
			object obj66 = (nint)0 + (nint)6;
			object obj67 = obj66 << 4;
			object obj68 = obj67 + 312;
			object obj69 = obj68 + num8;
			goto IL_1073;
			IL_154d:
			InputActions wrapper15;
			Action<InputAction.CallbackContext> value13;
			wrapper15.m_UI_TrackedDevicePosition.performed += value13;
			wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3938 @ rax_v142+8]");
			value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1607;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj70 = 0;
			object obj71 = 0;
			while (true)
			{
				object obj72 = obj71 + obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3880 @ r8_v143+v3883 @ rax_v293*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj71++;
				object obj73 = obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj73 < 0)
				{
					continue;
				}
				goto IL_1607;
			}
			object obj74 = obj71 + obj71;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3880 @ r8_v143+8+v3941 @ rcx_v242*8]");
			object obj75 = (nint)0 + (nint)8;
			object obj76 = obj75 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_161c;
			IL_029f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_02b4;
			IL_02b4:
			wrapper12.m_UI_Click.canceled += value10;
			InputActions wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1753 @ rax_v27+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_036e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1695 @ r8_v212+v1698 @ rax_v546*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_036e;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1695 @ r8_v212+8+v1756 @ rcx_v380*8]");
			object obj84 = (nint)0 + (nint)1;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_0383;
			IL_1469:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_147e;
			IL_0b84:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b99;
			IL_0b99:
			wrapper11.m_UI_Submit.performed += value9;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2798 @ rax_v82+8]");
			Action<InputAction.CallbackContext> value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0c53;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2740 @ r8_v179+v2743 @ rax_v425*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_0c53;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2740 @ r8_v179+8+v2801 @ rcx_v314*8]");
			object obj93 = (nint)0 + (nint)4;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_0c68;
			IL_1c94:
			InputActions wrapper18;
			Action<InputAction.CallbackContext> value16;
			wrapper18.m_UI_Down.performed += value16;
			InputActions wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4791 @ rax_v187+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4721 @ r9_v74 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1d4e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4721 @ r9_v74 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4735 @ r8_v116+v4740 @ rax_v194*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4721 @ r9_v74 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_1d4e;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4735 @ r8_v116+8+v4794 @ rcx_v189*8]");
			object obj102 = (nint)0 + (nint)11;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_1d63;
			IL_036e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0383;
			IL_0383:
			wrapper16.m_UI_Point.started += value14;
			InputActions wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1848 @ rax_v32+8]");
			Action<InputAction.CallbackContext> value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_043d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1790 @ r8_v209+v1793 @ rax_v535*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_043d;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1790 @ r8_v209+8+v1851 @ rcx_v374*8]");
			object obj111 = (nint)0 + (nint)1;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_0452;
			IL_1d63:
			wrapper19.m_UI_Down.canceled += value17;
			return;
			IL_11fc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1211;
			IL_1211:
			InputActions wrapper21;
			Action<InputAction.CallbackContext> value19;
			wrapper21.m_UI_MiddleClick.started += value19;
			wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3558 @ rax_v122+8]");
			value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_12cb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3500 @ r8_v155+v3503 @ rax_v337*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_12cb;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3500 @ r8_v155+8+v3561 @ rcx_v266*8]");
			object obj120 = (nint)0 + (nint)7;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_12e0;
			IL_1943:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1958;
			IL_043d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0452;
			IL_0452:
			wrapper20.m_UI_Point.performed += value18;
			InputActions wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1943 @ rax_v37+8]");
			Action<InputAction.CallbackContext> value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_050c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1885 @ r8_v206+v1888 @ rax_v524*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_050c;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1885 @ r8_v206+8+v1946 @ rcx_v368*8]");
			object obj129 = (nint)0 + (nint)1;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_0521;
			IL_147e:
			InputActions wrapper23;
			Action<InputAction.CallbackContext> value21;
			wrapper23.m_UI_TrackedDevicePosition.started += value21;
			wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3843 @ rax_v137+8]");
			value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1538;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3785 @ r8_v146+v3788 @ rax_v304*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_1538;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3785 @ r8_v146+8+v3846 @ rcx_v248*8]");
			object obj138 = (nint)0 + (nint)8;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_154d;
			IL_0c53:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c68;
			IL_0c68:
			wrapper17.m_UI_Submit.canceled += value15;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2893 @ rax_v87+8]");
			Action<InputAction.CallbackContext> value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d22;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2835 @ r8_v176+v2838 @ rax_v414*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_0d22;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2835 @ r8_v176+8+v2896 @ rcx_v308*8]");
			object obj147 = (nint)0 + (nint)5;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_0d37;
			IL_1958:
			InputActions wrapper25;
			Action<InputAction.CallbackContext> value23;
			wrapper25.m_UI_Up.started += value23;
			InputActions wrapper26 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4413 @ rax_v167+8]");
			Action<InputAction.CallbackContext> value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1a12;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4355 @ r8_v128+v4358 @ rax_v238*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_1a12;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4355 @ r8_v128+8+v4416 @ rcx_v212*8]");
			object obj156 = (nint)0 + (nint)10;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_1a27;
			IL_050c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0521;
			IL_0521:
			wrapper22.m_UI_Point.canceled += value20;
			InputActions wrapper27 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2038 @ rax_v42+8]");
			Action<InputAction.CallbackContext> value25 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_05db;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1980 @ r8_v203+v1983 @ rax_v513*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_05db;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1980 @ r8_v203+8+v2041 @ rcx_v362*8]");
			object obj165 = (nint)0 + (nint)2;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_05f0;
			IL_17a5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_17ba;
			IL_105e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1073;
			IL_1073:
			wrapper14.m_UI_ScrollWheel.performed += value12;
			InputActions wrapper28 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3368 @ rax_v112+8]");
			Action<InputAction.CallbackContext> value26 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_112d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3310 @ r8_v161+v3313 @ rax_v359*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_112d;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3310 @ r8_v161+8+v3371 @ rcx_v278*8]");
			object obj174 = (nint)0 + (nint)6;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_1142;
			IL_1d4e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1d63;
			IL_05db:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_05f0;
			IL_05f0:
			wrapper27.m_UI_Navigate.started += value25;
			InputActions wrapper29 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2133 @ rax_v47+8]");
			Action<InputAction.CallbackContext> value27 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_06aa;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2075 @ r8_v200+v2078 @ rax_v502*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_06aa;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2075 @ r8_v200+8+v2136 @ rcx_v356*8]");
			object obj183 = (nint)0 + (nint)2;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_06bf;
			IL_1889:
			InputActions wrapper30;
			Action<InputAction.CallbackContext> value28;
			wrapper30.m_UI_TrackedDeviceOrientation.canceled += value28;
			wrapper25 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4318 @ rax_v162+8]");
			value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1943;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4260 @ r8_v131+v4263 @ rax_v249*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_1943;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4260 @ r8_v131+8+v4321 @ rcx_v218*8]");
			object obj192 = (nint)0 + (nint)10;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_1958;
			IL_0d22:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0d37;
			IL_0d37:
			wrapper24.m_UI_Cancel.started += value22;
			InputActions wrapper31 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2988 @ rax_v92+8]");
			Action<InputAction.CallbackContext> value29 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0df1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2930 @ r8_v173+v2933 @ rax_v403*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_0df1;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2930 @ r8_v173+8+v2991 @ rcx_v302*8]");
			object obj201 = (nint)0 + (nint)5;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_0e06;
			IL_16d6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_16eb;
			IL_06aa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_06bf;
			IL_06bf:
			wrapper29.m_UI_Navigate.performed += value27;
			InputActions wrapper32 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2228 @ rax_v52+8]");
			Action<InputAction.CallbackContext> value30 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0779;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2170 @ r8_v197+v2173 @ rax_v491*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_0779;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2170 @ r8_v197+8+v2231 @ rcx_v350*8]");
			object obj210 = (nint)0 + (nint)2;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_078e;
			IL_1bc5:
			InputActions wrapper33;
			Action<InputAction.CallbackContext> value31;
			wrapper33.m_UI_Down.started += value31;
			wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4698 @ rax_v182+8]");
			value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num25 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v38 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1c7f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v38 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj214 = 0;
			object obj215 = 0;
			while (true)
			{
				object obj216 = obj215 + obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4640 @ r8_v119+v4643 @ rax_v205*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj215++;
				object obj217 = obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v38 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj217 < 0)
				{
					continue;
				}
				goto IL_1c7f;
			}
			object obj218 = obj215 + obj215;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4640 @ r8_v119+8+v4701 @ rcx_v194*8]");
			object obj219 = (nint)0 + (nint)11;
			object obj220 = obj219 << 4;
			object obj221 = obj220 + 312;
			object obj222 = obj221 + num25;
			goto IL_1c94;
			IL_139a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_13af;
			IL_13af:
			wrapper7.m_UI_MiddleClick.canceled += value5;
			wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3748 @ rax_v132+8]");
			value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num26 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1469;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj223 = 0;
			object obj224 = 0;
			while (true)
			{
				object obj225 = obj224 + obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3690 @ r8_v149+v3693 @ rax_v315*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj224++;
				object obj226 = obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj226 < 0)
				{
					continue;
				}
				goto IL_1469;
			}
			object obj227 = obj224 + obj224;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3690 @ r8_v149+8+v3751 @ rcx_v254*8]");
			object obj228 = (nint)0 + (nint)8;
			object obj229 = obj228 << 4;
			object obj230 = obj229 + 312;
			object obj231 = obj230 + num26;
			goto IL_147e;
			IL_16eb:
			wrapper10.m_UI_TrackedDeviceOrientation.started += value8;
			InputActions wrapper34 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4128 @ rax_v152+8]");
			Action<InputAction.CallbackContext> value32 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num27 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_17a5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj232 = 0;
			object obj233 = 0;
			while (true)
			{
				object obj234 = obj233 + obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4070 @ r8_v137+v4073 @ rax_v271*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj233++;
				object obj235 = obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj235 < 0)
				{
					continue;
				}
				goto IL_17a5;
			}
			object obj236 = obj233 + obj233;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4070 @ r8_v137+8+v4131 @ rcx_v230*8]");
			object obj237 = (nint)0 + (nint)9;
			object obj238 = obj237 << 4;
			object obj239 = obj238 + 312;
			object obj240 = obj239 + num27;
			goto IL_17ba;
			IL_0779:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_078e;
			IL_078e:
			wrapper32.m_UI_Navigate.canceled += value30;
			InputActions wrapper35 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2323 @ rax_v57+8]");
			Action<InputAction.CallbackContext> value33 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num28 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0848;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj241 = 0;
			object obj242 = 0;
			while (true)
			{
				object obj243 = obj242 + obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2265 @ r8_v194+v2268 @ rax_v480*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj242++;
				object obj244 = obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj244 < 0)
				{
					continue;
				}
				goto IL_0848;
			}
			object obj245 = obj242 + obj242;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2265 @ r8_v194+8+v2326 @ rcx_v344*8]");
			object obj246 = (nint)0 + (nint)3;
			object obj247 = obj246 << 4;
			object obj248 = obj247 + 312;
			object obj249 = obj248 + num28;
			goto IL_085d;
			IL_1ae1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1af6;
			IL_0df1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e06;
			IL_0e06:
			wrapper31.m_UI_Cancel.performed += value29;
			InputActions wrapper36 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3083 @ rax_v97+8]");
			Action<InputAction.CallbackContext> value34 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num29 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ec0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj250 = 0;
			object obj251 = 0;
			while (true)
			{
				object obj252 = obj251 + obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3025 @ r8_v170+v3028 @ rax_v392*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj251++;
				object obj253 = obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj253 < 0)
				{
					continue;
				}
				goto IL_0ec0;
			}
			object obj254 = obj251 + obj251;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3025 @ r8_v170+8+v3086 @ rcx_v296*8]");
			object obj255 = (nint)0 + (nint)5;
			object obj256 = obj255 << 4;
			object obj257 = obj256 + 312;
			object obj258 = obj257 + num29;
			goto IL_0ed5;
			IL_1874:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1889;
			IL_0848:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_085d;
			IL_085d:
			wrapper35.m_UI_MoveUI.started += value33;
			InputActions wrapper37 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2418 @ rax_v62+8]");
			Action<InputAction.CallbackContext> value35 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num30 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0917;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj259 = 0;
			object obj260 = 0;
			while (true)
			{
				object obj261 = obj260 + obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2360 @ r8_v191+v2363 @ rax_v469*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj260++;
				object obj262 = obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj262 < 0)
				{
					continue;
				}
				goto IL_0917;
			}
			object obj263 = obj260 + obj260;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2360 @ r8_v191+8+v2421 @ rcx_v338*8]");
			object obj264 = (nint)0 + (nint)3;
			object obj265 = obj264 << 4;
			object obj266 = obj265 + 312;
			object obj267 = obj266 + num30;
			goto IL_092c;
			IL_17ba:
			wrapper34.m_UI_TrackedDeviceOrientation.performed += value32;
			wrapper30 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4223 @ rax_v157+8]");
			value28 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num31 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1874;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj268 = 0;
			object obj269 = 0;
			while (true)
			{
				object obj270 = obj269 + obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4165 @ r8_v134+v4168 @ rax_v260*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj269++;
				object obj271 = obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj271 < 0)
				{
					continue;
				}
				goto IL_1874;
			}
			object obj272 = obj269 + obj269;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4165 @ r8_v134+8+v4226 @ rcx_v224*8]");
			object obj273 = (nint)0 + (nint)9;
			object obj274 = obj273 << 4;
			object obj275 = obj274 + 312;
			object obj276 = obj275 + num31;
			goto IL_1889;
			IL_112d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1142;
			IL_1142:
			wrapper28.m_UI_ScrollWheel.canceled += value26;
			wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3463 @ rax_v117+8]");
			value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num32 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_11fc;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj277 = 0;
			object obj278 = 0;
			while (true)
			{
				object obj279 = obj278 + obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3405 @ r8_v158+v3408 @ rax_v348*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj278++;
				object obj280 = obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj280 < 0)
				{
					continue;
				}
				goto IL_11fc;
			}
			object obj281 = obj278 + obj278;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3405 @ r8_v158+8+v3466 @ rcx_v272*8]");
			object obj282 = (nint)0 + (nint)7;
			object obj283 = obj282 << 4;
			object obj284 = obj283 + 312;
			object obj285 = obj284 + num32;
			goto IL_1211;
			IL_1a12:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1a27;
			IL_0917:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_092c;
			IL_092c:
			wrapper37.m_UI_MoveUI.performed += value35;
			wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2513 @ rax_v67+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num33 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_09e6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj286 = 0;
			object obj287 = 0;
			while (true)
			{
				object obj288 = obj287 + obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2455 @ r8_v188+v2458 @ rax_v458*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj287++;
				object obj289 = obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj289 < 0)
				{
					continue;
				}
				goto IL_09e6;
			}
			object obj290 = obj287 + obj287;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2455 @ r8_v188+8+v2516 @ rcx_v332*8]");
			object obj291 = (nint)0 + (nint)3;
			object obj292 = obj291 << 4;
			object obj293 = obj292 + 312;
			object obj294 = obj293 + num33;
			goto IL_09fb;
			IL_1af6:
			InputActions wrapper38;
			Action<InputAction.CallbackContext> value36;
			wrapper38.m_UI_Up.canceled += value36;
			wrapper33 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4603 @ rax_v177+8]");
			value31 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num34 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v37 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1bb0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v37 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj295 = 0;
			object obj296 = 0;
			while (true)
			{
				object obj297 = obj296 + obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4545 @ r8_v122+v4548 @ rax_v216*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj296++;
				object obj298 = obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v37 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj298 < 0)
				{
					continue;
				}
				goto IL_1bb0;
			}
			object obj299 = obj296 + obj296;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4545 @ r8_v122+8+v4606 @ rcx_v200*8]");
			object obj300 = (nint)0 + (nint)11;
			object obj301 = obj300 << 4;
			object obj302 = obj301 + 312;
			object obj303 = obj302 + num34;
			goto IL_1bc5;
			IL_0ec0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0ed5;
			IL_0ed5:
			wrapper36.m_UI_Cancel.canceled += value34;
			wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3178 @ rax_v102+8]");
			value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num35 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f8f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj304 = 0;
			object obj305 = 0;
			while (true)
			{
				object obj306 = obj305 + obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3120 @ r8_v167+v3123 @ rax_v381*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj305++;
				object obj307 = obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj307 < 0)
				{
					continue;
				}
				goto IL_0f8f;
			}
			object obj308 = obj305 + obj305;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3120 @ r8_v167+8+v3181 @ rcx_v290*8]");
			object obj309 = (nint)0 + (nint)6;
			object obj310 = obj309 << 4;
			object obj311 = obj310 + 312;
			object obj312 = obj311 + num35;
			goto IL_0fa4;
			IL_1a27:
			wrapper26.m_UI_Up.performed += value24;
			wrapper38 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4508 @ rax_v172+8]");
			value36 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num36 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1ae1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj313 = 0;
			object obj314 = 0;
			while (true)
			{
				object obj315 = obj314 + obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4450 @ r8_v125+v4453 @ rax_v227*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj314++;
				object obj316 = obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj316 < 0)
				{
					continue;
				}
				goto IL_1ae1;
			}
			object obj317 = obj314 + obj314;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4450 @ r8_v125+8+v4511 @ rcx_v206*8]");
			object obj318 = (nint)0 + (nint)10;
			object obj319 = obj318 << 4;
			object obj320 = obj319 + 312;
			object obj321 = obj320 + num36;
			goto IL_1af6;
		}

		private void UnregisterCallbacks(IUIActions instance)
		{
			//IL_002b: Expected I, but got O
			//IL_0063: Expected O, but got I
			//IL_006c: Expected O, but got I4
			//IL_00fa: Expected I, but got O
			//IL_1d3b: Expected O, but got I
			//IL_1d44: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d49: Expected O, but got Unknown
			//IL_1d51: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d56: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0132: Expected O, but got I
			//IL_013b: Expected O, but got I4
			//IL_01c9: Expected I, but got O
			//IL_1d7e: Expected O, but got I
			//IL_1d87: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d8c: Expected O, but got Unknown
			//IL_1d94: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d99: Expected O, but got Unknown
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Expected O, but got Unknown
			//IL_0201: Expected O, but got I
			//IL_020a: Expected O, but got I4
			//IL_0298: Expected I, but got O
			//IL_1dc1: Expected O, but got I
			//IL_1dca: Unknown result type (might be due to invalid IL or missing references)
			//IL_1dcf: Expected O, but got Unknown
			//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ddc: Expected O, but got Unknown
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Expected O, but got Unknown
			//IL_02d0: Expected O, but got I
			//IL_02d9: Expected O, but got I4
			//IL_0367: Expected I, but got O
			//IL_1e04: Expected O, but got I
			//IL_1e1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e20: Expected O, but got Unknown
			//IL_1e28: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e2d: Expected O, but got Unknown
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ec: Expected O, but got Unknown
			//IL_039f: Expected O, but got I
			//IL_03a8: Expected O, but got I4
			//IL_0436: Expected I, but got O
			//IL_1e55: Expected O, but got I
			//IL_1e6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e71: Expected O, but got Unknown
			//IL_1e79: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e7e: Expected O, but got Unknown
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Expected O, but got Unknown
			//IL_046e: Expected O, but got I
			//IL_0477: Expected O, but got I4
			//IL_0505: Expected I, but got O
			//IL_1ea6: Expected O, but got I
			//IL_1ebd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ec2: Expected O, but got Unknown
			//IL_1eca: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ecf: Expected O, but got Unknown
			//IL_0485: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Expected O, but got Unknown
			//IL_053d: Expected O, but got I
			//IL_0546: Expected O, but got I4
			//IL_05d4: Expected I, but got O
			//IL_1ef7: Expected O, but got I
			//IL_1f0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f13: Expected O, but got Unknown
			//IL_1f1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f20: Expected O, but got Unknown
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_0559: Expected O, but got Unknown
			//IL_060c: Expected O, but got I
			//IL_0615: Expected O, but got I4
			//IL_06a3: Expected I, but got O
			//IL_1f48: Expected O, but got I
			//IL_1f5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f64: Expected O, but got Unknown
			//IL_1f6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f71: Expected O, but got Unknown
			//IL_0623: Unknown result type (might be due to invalid IL or missing references)
			//IL_0628: Expected O, but got Unknown
			//IL_06db: Expected O, but got I
			//IL_06e4: Expected O, but got I4
			//IL_0772: Expected I, but got O
			//IL_1f99: Expected O, but got I
			//IL_1fb0: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb5: Expected O, but got Unknown
			//IL_1fbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fc2: Expected O, but got Unknown
			//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f7: Expected O, but got Unknown
			//IL_07aa: Expected O, but got I
			//IL_07b3: Expected O, but got I4
			//IL_0841: Expected I, but got O
			//IL_1fea: Expected O, but got I
			//IL_2001: Unknown result type (might be due to invalid IL or missing references)
			//IL_2006: Expected O, but got Unknown
			//IL_200e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2013: Expected O, but got Unknown
			//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Expected O, but got Unknown
			//IL_0879: Expected O, but got I
			//IL_0882: Expected O, but got I4
			//IL_0910: Expected I, but got O
			//IL_203b: Expected O, but got I
			//IL_2052: Unknown result type (might be due to invalid IL or missing references)
			//IL_2057: Expected O, but got Unknown
			//IL_205f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2064: Expected O, but got Unknown
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_0895: Expected O, but got Unknown
			//IL_0948: Expected O, but got I
			//IL_0951: Expected O, but got I4
			//IL_09df: Expected I, but got O
			//IL_208c: Expected O, but got I
			//IL_20a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_20a8: Expected O, but got Unknown
			//IL_20b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_20b5: Expected O, but got Unknown
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0964: Expected O, but got Unknown
			//IL_0a17: Expected O, but got I
			//IL_0a20: Expected O, but got I4
			//IL_0aae: Expected I, but got O
			//IL_20dd: Expected O, but got I
			//IL_20f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_20f9: Expected O, but got Unknown
			//IL_2101: Unknown result type (might be due to invalid IL or missing references)
			//IL_2106: Expected O, but got Unknown
			//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a33: Expected O, but got Unknown
			//IL_0ae6: Expected O, but got I
			//IL_0aef: Expected O, but got I4
			//IL_0b7d: Expected I, but got O
			//IL_212e: Expected O, but got I
			//IL_2145: Unknown result type (might be due to invalid IL or missing references)
			//IL_214a: Expected O, but got Unknown
			//IL_2152: Unknown result type (might be due to invalid IL or missing references)
			//IL_2157: Expected O, but got Unknown
			//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b02: Expected O, but got Unknown
			//IL_0bb5: Expected O, but got I
			//IL_0bbe: Expected O, but got I4
			//IL_0c4c: Expected I, but got O
			//IL_217f: Expected O, but got I
			//IL_2196: Unknown result type (might be due to invalid IL or missing references)
			//IL_219b: Expected O, but got Unknown
			//IL_21a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_21a8: Expected O, but got Unknown
			//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0c84: Expected O, but got I
			//IL_0c8d: Expected O, but got I4
			//IL_0d1b: Expected I, but got O
			//IL_21d0: Expected O, but got I
			//IL_21e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_21ec: Expected O, but got Unknown
			//IL_21f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_21f9: Expected O, but got Unknown
			//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca0: Expected O, but got Unknown
			//IL_0d53: Expected O, but got I
			//IL_0d5c: Expected O, but got I4
			//IL_0dea: Expected I, but got O
			//IL_2221: Expected O, but got I
			//IL_2238: Unknown result type (might be due to invalid IL or missing references)
			//IL_223d: Expected O, but got Unknown
			//IL_2245: Unknown result type (might be due to invalid IL or missing references)
			//IL_224a: Expected O, but got Unknown
			//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6f: Expected O, but got Unknown
			//IL_0e22: Expected O, but got I
			//IL_0e2b: Expected O, but got I4
			//IL_0eb9: Expected I, but got O
			//IL_2272: Expected O, but got I
			//IL_2289: Unknown result type (might be due to invalid IL or missing references)
			//IL_228e: Expected O, but got Unknown
			//IL_2296: Unknown result type (might be due to invalid IL or missing references)
			//IL_229b: Expected O, but got Unknown
			//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3e: Expected O, but got Unknown
			//IL_0ef1: Expected O, but got I
			//IL_0efa: Expected O, but got I4
			//IL_0f88: Expected I, but got O
			//IL_22c3: Expected O, but got I
			//IL_22da: Unknown result type (might be due to invalid IL or missing references)
			//IL_22df: Expected O, but got Unknown
			//IL_22e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_22ec: Expected O, but got Unknown
			//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Expected O, but got Unknown
			//IL_0fc0: Expected O, but got I
			//IL_0fc9: Expected O, but got I4
			//IL_1057: Expected I, but got O
			//IL_2314: Expected O, but got I
			//IL_232b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2330: Expected O, but got Unknown
			//IL_2338: Unknown result type (might be due to invalid IL or missing references)
			//IL_233d: Expected O, but got Unknown
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdc: Expected O, but got Unknown
			//IL_108f: Expected O, but got I
			//IL_1098: Expected O, but got I4
			//IL_1126: Expected I, but got O
			//IL_2365: Expected O, but got I
			//IL_237c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2381: Expected O, but got Unknown
			//IL_2389: Unknown result type (might be due to invalid IL or missing references)
			//IL_238e: Expected O, but got Unknown
			//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ab: Expected O, but got Unknown
			//IL_115e: Expected O, but got I
			//IL_1167: Expected O, but got I4
			//IL_11f5: Expected I, but got O
			//IL_23b6: Expected O, but got I
			//IL_23cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_23d2: Expected O, but got Unknown
			//IL_23da: Unknown result type (might be due to invalid IL or missing references)
			//IL_23df: Expected O, but got Unknown
			//IL_1175: Unknown result type (might be due to invalid IL or missing references)
			//IL_117a: Expected O, but got Unknown
			//IL_122d: Expected O, but got I
			//IL_1236: Expected O, but got I4
			//IL_12c4: Expected I, but got O
			//IL_2407: Expected O, but got I
			//IL_241e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2423: Expected O, but got Unknown
			//IL_242b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2430: Expected O, but got Unknown
			//IL_1244: Unknown result type (might be due to invalid IL or missing references)
			//IL_1249: Expected O, but got Unknown
			//IL_12fc: Expected O, but got I
			//IL_1305: Expected O, but got I4
			//IL_1393: Expected I, but got O
			//IL_2458: Expected O, but got I
			//IL_246f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2474: Expected O, but got Unknown
			//IL_247c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2481: Expected O, but got Unknown
			//IL_1313: Unknown result type (might be due to invalid IL or missing references)
			//IL_1318: Expected O, but got Unknown
			//IL_13cb: Expected O, but got I
			//IL_13d4: Expected O, but got I4
			//IL_1462: Expected I, but got O
			//IL_24a9: Expected O, but got I
			//IL_24c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_24c5: Expected O, but got Unknown
			//IL_24cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d2: Expected O, but got Unknown
			//IL_13e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e7: Expected O, but got Unknown
			//IL_149a: Expected O, but got I
			//IL_14a3: Expected O, but got I4
			//IL_1531: Expected I, but got O
			//IL_24fa: Expected O, but got I
			//IL_2511: Unknown result type (might be due to invalid IL or missing references)
			//IL_2516: Expected O, but got Unknown
			//IL_251e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2523: Expected O, but got Unknown
			//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_14b6: Expected O, but got Unknown
			//IL_1569: Expected O, but got I
			//IL_1572: Expected O, but got I4
			//IL_1600: Expected I, but got O
			//IL_254b: Expected O, but got I
			//IL_2562: Unknown result type (might be due to invalid IL or missing references)
			//IL_2567: Expected O, but got Unknown
			//IL_256f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2574: Expected O, but got Unknown
			//IL_1580: Unknown result type (might be due to invalid IL or missing references)
			//IL_1585: Expected O, but got Unknown
			//IL_1638: Expected O, but got I
			//IL_1641: Expected O, but got I4
			//IL_16cf: Expected I, but got O
			//IL_259c: Expected O, but got I
			//IL_25b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_25b8: Expected O, but got Unknown
			//IL_25c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_25c5: Expected O, but got Unknown
			//IL_164f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1654: Expected O, but got Unknown
			//IL_1707: Expected O, but got I
			//IL_1710: Expected O, but got I4
			//IL_179e: Expected I, but got O
			//IL_25ed: Expected O, but got I
			//IL_2604: Unknown result type (might be due to invalid IL or missing references)
			//IL_2609: Expected O, but got Unknown
			//IL_2611: Unknown result type (might be due to invalid IL or missing references)
			//IL_2616: Expected O, but got Unknown
			//IL_171e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1723: Expected O, but got Unknown
			//IL_17d6: Expected O, but got I
			//IL_17df: Expected O, but got I4
			//IL_186d: Expected I, but got O
			//IL_263e: Expected O, but got I
			//IL_2655: Unknown result type (might be due to invalid IL or missing references)
			//IL_265a: Expected O, but got Unknown
			//IL_2662: Unknown result type (might be due to invalid IL or missing references)
			//IL_2667: Expected O, but got Unknown
			//IL_17ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f2: Expected O, but got Unknown
			//IL_18a5: Expected O, but got I
			//IL_18ae: Expected O, but got I4
			//IL_193c: Expected I, but got O
			//IL_268f: Expected O, but got I
			//IL_26a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_26ab: Expected O, but got Unknown
			//IL_26b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_26b8: Expected O, but got Unknown
			//IL_18bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c1: Expected O, but got Unknown
			//IL_1974: Expected O, but got I
			//IL_197d: Expected O, but got I4
			//IL_1a0b: Expected I, but got O
			//IL_26e0: Expected O, but got I
			//IL_26f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_26fc: Expected O, but got Unknown
			//IL_2704: Unknown result type (might be due to invalid IL or missing references)
			//IL_2709: Expected O, but got Unknown
			//IL_198b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1990: Expected O, but got Unknown
			//IL_1a43: Expected O, but got I
			//IL_1a4c: Expected O, but got I4
			//IL_1ada: Expected I, but got O
			//IL_2731: Expected O, but got I
			//IL_2748: Unknown result type (might be due to invalid IL or missing references)
			//IL_274d: Expected O, but got Unknown
			//IL_2755: Unknown result type (might be due to invalid IL or missing references)
			//IL_275a: Expected O, but got Unknown
			//IL_1a5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a5f: Expected O, but got Unknown
			//IL_1b12: Expected O, but got I
			//IL_1b1b: Expected O, but got I4
			//IL_1ba9: Expected I, but got O
			//IL_2782: Expected O, but got I
			//IL_2799: Unknown result type (might be due to invalid IL or missing references)
			//IL_279e: Expected O, but got Unknown
			//IL_27a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_27ab: Expected O, but got Unknown
			//IL_1b29: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b2e: Expected O, but got Unknown
			//IL_1be1: Expected O, but got I
			//IL_1bea: Expected O, but got I4
			//IL_1c78: Expected I, but got O
			//IL_27d3: Expected O, but got I
			//IL_27ea: Unknown result type (might be due to invalid IL or missing references)
			//IL_27ef: Expected O, but got Unknown
			//IL_27f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_27fc: Expected O, but got Unknown
			//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bfd: Expected O, but got Unknown
			//IL_1cb0: Expected O, but got I
			//IL_1cb9: Expected O, but got I4
			//IL_2824: Expected O, but got I
			//IL_283b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2840: Expected O, but got Unknown
			//IL_2848: Unknown result type (might be due to invalid IL or missing references)
			//IL_284d: Expected O, but got Unknown
			//IL_1cc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ccc: Expected O, but got Unknown
			InputActions wrapper = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1361 @ rax_v6+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v2 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00a3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v2 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1303 @ r8_v217+v1306 @ rax_v569*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ r10_v2 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_00a3;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1303 @ r8_v217+8+v1364 @ rcx_v394*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_00b8;
			IL_1b52:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1b67;
			IL_126d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1282;
			IL_1282:
			InputActions wrapper2;
			Action<InputAction.CallbackContext> value2;
			wrapper2.m_UI_MiddleClick.performed -= value2;
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3540 @ rax_v121+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_133c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3482 @ r8_v148+v3485 @ rax_v320*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ r10_v25 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_133c;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3482 @ r8_v148+8+v3543 @ rcx_v256*8]");
			object obj14 = (nint)0 + (nint)7;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_1351;
			IL_15a9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_15be;
			IL_00a3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_00b8;
			IL_00b8:
			wrapper.m_UI_Click.started -= value;
			InputActions wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1454 @ rax_v11+8]");
			Action<InputAction.CallbackContext> value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v3 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0172;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v3 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1396 @ r8_v214+v1399 @ rax_v560*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r10_v3 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_0172;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1396 @ r8_v214+8+v1457 @ rcx_v388*8]");
			object obj23 = (nint)0 << 4;
			object obj24 = obj23 + 312;
			object obj25 = obj24 + num3;
			goto IL_0187;
			IL_15be:
			InputActions wrapper5;
			Action<InputAction.CallbackContext> value5;
			wrapper5.m_UI_TrackedDevicePosition.canceled -= value5;
			InputActions wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3920 @ rax_v141+8]");
			Action<InputAction.CallbackContext> value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1678;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj26 = 0;
			object obj27 = 0;
			while (true)
			{
				object obj28 = obj27 + obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3862 @ r8_v136+v3865 @ rax_v276*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj27++;
				object obj29 = obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r10_v29 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj29 < 0)
				{
					continue;
				}
				goto IL_1678;
			}
			object obj30 = obj27 + obj27;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3862 @ r8_v136+8+v3923 @ rcx_v232*8]");
			object obj31 = (nint)0 + (nint)9;
			object obj32 = obj31 << 4;
			object obj33 = obj32 + 312;
			object obj34 = obj33 + num4;
			goto IL_168d;
			IL_0a57:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0a6c;
			IL_0a6c:
			InputActions wrapper7;
			Action<InputAction.CallbackContext> value7;
			wrapper7.m_UI_Submit.started -= value7;
			InputActions wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2590 @ rax_v71+8]");
			Action<InputAction.CallbackContext> value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b26;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj35 = 0;
			object obj36 = 0;
			while (true)
			{
				object obj37 = obj36 + obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2532 @ r8_v178+v2535 @ rax_v430*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj36++;
				object obj38 = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r10_v15 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj38 < 0)
				{
					continue;
				}
				goto IL_0b26;
			}
			object obj39 = obj36 + obj36;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2532 @ r8_v178+8+v2593 @ rcx_v316*8]");
			object obj40 = (nint)0 + (nint)4;
			object obj41 = obj40 << 4;
			object obj42 = obj41 + 312;
			object obj43 = obj42 + num5;
			goto IL_0b3b;
			IL_14da:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_14ef;
			IL_0172:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0187;
			IL_0187:
			wrapper4.m_UI_Click.performed -= value4;
			InputActions wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1547 @ rax_v16+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0241;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj44 = 0;
			object obj45 = 0;
			while (true)
			{
				object obj46 = obj45 + obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1489 @ r8_v211+v1492 @ rax_v551*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj45++;
				object obj47 = obj45;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v203 @ r10_v4 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj47 < 0)
				{
					continue;
				}
				goto IL_0241;
			}
			object obj48 = obj45 + obj45;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1489 @ r8_v211+8+v1550 @ rcx_v382*8]");
			object obj49 = (nint)0 << 4;
			object obj50 = obj49 + 312;
			object obj51 = obj50 + num6;
			goto IL_0256;
			IL_1c21:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1c36;
			IL_0f31:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0f46;
			IL_0f46:
			InputActions wrapper10;
			Action<InputAction.CallbackContext> value10;
			wrapper10.m_UI_ScrollWheel.started -= value10;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3160 @ rax_v101+8]");
			Action<InputAction.CallbackContext> value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1000;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj52 = 0;
			object obj53 = 0;
			while (true)
			{
				object obj54 = obj53 + obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3102 @ r8_v160+v3105 @ rax_v364*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj53++;
				object obj55 = obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ r10_v21 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj55 < 0)
				{
					continue;
				}
				goto IL_1000;
			}
			object obj56 = obj53 + obj53;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3102 @ r8_v160+8+v3163 @ rcx_v280*8]");
			object obj57 = (nint)0 + (nint)6;
			object obj58 = obj57 << 4;
			object obj59 = obj58 + 312;
			object obj60 = obj59 + num7;
			goto IL_1015;
			IL_14ef:
			InputActions wrapper12;
			Action<InputAction.CallbackContext> value12;
			wrapper12.m_UI_TrackedDevicePosition.performed -= value12;
			wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3825 @ rax_v136+8]");
			value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_15a9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj61 = 0;
			object obj62 = 0;
			while (true)
			{
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3767 @ r8_v139+v3770 @ rax_v287*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj62++;
				object obj64 = obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v227 @ r10_v28 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj64 < 0)
				{
					continue;
				}
				goto IL_15a9;
			}
			object obj65 = obj62 + obj62;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3767 @ r8_v139+8+v3828 @ rcx_v238*8]");
			object obj66 = (nint)0 + (nint)8;
			object obj67 = obj66 << 4;
			object obj68 = obj67 + 312;
			object obj69 = obj68 + num8;
			goto IL_15be;
			IL_0241:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0256;
			IL_0256:
			wrapper9.m_UI_Click.canceled -= value9;
			InputActions wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1640 @ rax_v21+8]");
			Action<InputAction.CallbackContext> value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0310;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj70 = 0;
			object obj71 = 0;
			while (true)
			{
				object obj72 = obj71 + obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1582 @ r8_v208+v1585 @ rax_v540*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj71++;
				object obj73 = obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v204 @ r10_v5 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj73 < 0)
				{
					continue;
				}
				goto IL_0310;
			}
			object obj74 = obj71 + obj71;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1582 @ r8_v208+8+v1643 @ rcx_v376*8]");
			object obj75 = (nint)0 + (nint)1;
			object obj76 = obj75 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_0325;
			IL_140b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1420;
			IL_0b26:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b3b;
			IL_0b3b:
			wrapper8.m_UI_Submit.performed -= value8;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2685 @ rax_v76+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0bf5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2627 @ r8_v175+v2630 @ rax_v419*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r10_v16 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_0bf5;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2627 @ r8_v175+8+v2688 @ rcx_v310*8]");
			object obj84 = (nint)0 + (nint)4;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_0c0a;
			IL_1c36:
			InputActions wrapper15;
			Action<InputAction.CallbackContext> value15;
			wrapper15.m_UI_Down.performed -= value15;
			InputActions wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4678 @ rax_v181+8]");
			Action<InputAction.CallbackContext> value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4608 @ r9_v72 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1cf0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4608 @ r9_v72 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4622 @ r8_v112+v4627 @ rax_v188*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4608 @ r9_v72 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_1cf0;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4622 @ r8_v112+8+v4681 @ rcx_v185*8]");
			object obj93 = (nint)0 + (nint)11;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_1d05;
			IL_0310:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0325;
			IL_0325:
			wrapper13.m_UI_Point.started -= value13;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1735 @ rax_v26+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_03df;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1677 @ r8_v205+v1680 @ rax_v529*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r10_v6 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_03df;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1677 @ r8_v205+8+v1738 @ rcx_v370*8]");
			object obj102 = (nint)0 + (nint)1;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_03f4;
			IL_1d05:
			wrapper16.m_UI_Down.canceled -= value16;
			return;
			IL_119e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_11b3;
			IL_11b3:
			InputActions wrapper18;
			Action<InputAction.CallbackContext> value18;
			wrapper18.m_UI_MiddleClick.started -= value18;
			wrapper2 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3445 @ rax_v116+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_126d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3387 @ r8_v151+v3390 @ rax_v331*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r10_v24 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_126d;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3387 @ r8_v151+8+v3448 @ rcx_v262*8]");
			object obj111 = (nint)0 + (nint)7;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_1282;
			IL_18e5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_18fa;
			IL_03df:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_03f4;
			IL_03f4:
			wrapper17.m_UI_Point.performed -= value17;
			InputActions wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1830 @ rax_v31+8]");
			Action<InputAction.CallbackContext> value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_04ae;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1772 @ r8_v202+v1775 @ rax_v518*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ r10_v7 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_04ae;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1772 @ r8_v202+8+v1833 @ rcx_v364*8]");
			object obj120 = (nint)0 + (nint)1;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_04c3;
			IL_1420:
			InputActions wrapper20;
			Action<InputAction.CallbackContext> value20;
			wrapper20.m_UI_TrackedDevicePosition.started -= value20;
			wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3730 @ rax_v131+8]");
			value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_14da;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3672 @ r8_v142+v3675 @ rax_v298*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ r10_v27 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_14da;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3672 @ r8_v142+8+v3733 @ rcx_v244*8]");
			object obj129 = (nint)0 + (nint)8;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_14ef;
			IL_0bf5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c0a;
			IL_0c0a:
			wrapper14.m_UI_Submit.canceled -= value14;
			InputActions wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2780 @ rax_v81+8]");
			Action<InputAction.CallbackContext> value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0cc4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2722 @ r8_v172+v2725 @ rax_v408*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r10_v17 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_0cc4;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2722 @ r8_v172+8+v2783 @ rcx_v304*8]");
			object obj138 = (nint)0 + (nint)5;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_0cd9;
			IL_18fa:
			InputActions wrapper22;
			Action<InputAction.CallbackContext> value22;
			wrapper22.m_UI_Up.started -= value22;
			InputActions wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4300 @ rax_v161+8]");
			Action<InputAction.CallbackContext> value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_19b4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4242 @ r8_v124+v4245 @ rax_v232*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ r10_v33 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_19b4;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4242 @ r8_v124+8+v4303 @ rcx_v208*8]");
			object obj147 = (nint)0 + (nint)10;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_19c9;
			IL_04ae:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_04c3;
			IL_04c3:
			wrapper19.m_UI_Point.canceled -= value19;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1925 @ rax_v36+8]");
			Action<InputAction.CallbackContext> value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_057d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1867 @ r8_v199+v1870 @ rax_v507*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r10_v8 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_057d;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1867 @ r8_v199+8+v1928 @ rcx_v358*8]");
			object obj156 = (nint)0 + (nint)2;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_0592;
			IL_1747:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_175c;
			IL_1000:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1015;
			IL_1015:
			wrapper11.m_UI_ScrollWheel.performed -= value11;
			InputActions wrapper25 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3255 @ rax_v106+8]");
			Action<InputAction.CallbackContext> value25 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_10cf;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3197 @ r8_v157+v3200 @ rax_v353*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r10_v22 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_10cf;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3197 @ r8_v157+8+v3258 @ rcx_v274*8]");
			object obj165 = (nint)0 + (nint)6;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_10e4;
			IL_1cf0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1d05;
			IL_057d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0592;
			IL_0592:
			wrapper24.m_UI_Navigate.started -= value24;
			InputActions wrapper26 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2020 @ rax_v41+8]");
			Action<InputAction.CallbackContext> value26 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_064c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1962 @ r8_v196+v1965 @ rax_v496*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ r10_v9 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_064c;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1962 @ r8_v196+8+v2023 @ rcx_v352*8]");
			object obj174 = (nint)0 + (nint)2;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_0661;
			IL_182b:
			InputActions wrapper27;
			Action<InputAction.CallbackContext> value27;
			wrapper27.m_UI_TrackedDeviceOrientation.canceled -= value27;
			wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4205 @ rax_v156+8]");
			value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_18e5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4147 @ r8_v127+v4150 @ rax_v243*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ r10_v32 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_18e5;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4147 @ r8_v127+8+v4208 @ rcx_v214*8]");
			object obj183 = (nint)0 + (nint)10;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_18fa;
			IL_0cc4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0cd9;
			IL_0cd9:
			wrapper21.m_UI_Cancel.started -= value21;
			InputActions wrapper28 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2875 @ rax_v86+8]");
			Action<InputAction.CallbackContext> value28 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d93;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2817 @ r8_v169+v2820 @ rax_v397*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ r10_v18 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_0d93;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2817 @ r8_v169+8+v2878 @ rcx_v298*8]");
			object obj192 = (nint)0 + (nint)5;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_0da8;
			IL_1678:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_168d;
			IL_064c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0661;
			IL_0661:
			wrapper26.m_UI_Navigate.performed -= value26;
			InputActions wrapper29 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2115 @ rax_v46+8]");
			Action<InputAction.CallbackContext> value29 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_071b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2057 @ r8_v193+v2060 @ rax_v485*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ r10_v10 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_071b;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2057 @ r8_v193+8+v2118 @ rcx_v346*8]");
			object obj201 = (nint)0 + (nint)2;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_0730;
			IL_1b67:
			InputActions wrapper30;
			Action<InputAction.CallbackContext> value30;
			wrapper30.m_UI_Down.started -= value30;
			wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4585 @ rax_v176+8]");
			value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1c21;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4527 @ r8_v115+v4530 @ rax_v199*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ r10_v36 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_1c21;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4527 @ r8_v115+8+v4588 @ rcx_v190*8]");
			object obj210 = (nint)0 + (nint)11;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_1c36;
			IL_133c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1351;
			IL_1351:
			wrapper3.m_UI_MiddleClick.canceled -= value3;
			wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3635 @ rax_v126+8]");
			value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num25 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_140b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj214 = 0;
			object obj215 = 0;
			while (true)
			{
				object obj216 = obj215 + obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3577 @ r8_v145+v3580 @ rax_v309*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj215++;
				object obj217 = obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ r10_v26 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj217 < 0)
				{
					continue;
				}
				goto IL_140b;
			}
			object obj218 = obj215 + obj215;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3577 @ r8_v145+8+v3638 @ rcx_v250*8]");
			object obj219 = (nint)0 + (nint)8;
			object obj220 = obj219 << 4;
			object obj221 = obj220 + 312;
			object obj222 = obj221 + num25;
			goto IL_1420;
			IL_168d:
			wrapper6.m_UI_TrackedDeviceOrientation.started -= value6;
			InputActions wrapper31 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4015 @ rax_v146+8]");
			Action<InputAction.CallbackContext> value31 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num26 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1747;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj223 = 0;
			object obj224 = 0;
			while (true)
			{
				object obj225 = obj224 + obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3957 @ r8_v133+v3960 @ rax_v265*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj224++;
				object obj226 = obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ r10_v30 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj226 < 0)
				{
					continue;
				}
				goto IL_1747;
			}
			object obj227 = obj224 + obj224;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3957 @ r8_v133+8+v4018 @ rcx_v226*8]");
			object obj228 = (nint)0 + (nint)9;
			object obj229 = obj228 << 4;
			object obj230 = obj229 + 312;
			object obj231 = obj230 + num26;
			goto IL_175c;
			IL_071b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0730;
			IL_0730:
			wrapper29.m_UI_Navigate.canceled -= value29;
			InputActions wrapper32 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2210 @ rax_v51+8]");
			Action<InputAction.CallbackContext> value32 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num27 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_07ea;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj232 = 0;
			object obj233 = 0;
			while (true)
			{
				object obj234 = obj233 + obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2152 @ r8_v190+v2155 @ rax_v474*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj233++;
				object obj235 = obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ r10_v11 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj235 < 0)
				{
					continue;
				}
				goto IL_07ea;
			}
			object obj236 = obj233 + obj233;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2152 @ r8_v190+8+v2213 @ rcx_v340*8]");
			object obj237 = (nint)0 + (nint)3;
			object obj238 = obj237 << 4;
			object obj239 = obj238 + 312;
			object obj240 = obj239 + num27;
			goto IL_07ff;
			IL_1a83:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1a98;
			IL_0d93:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0da8;
			IL_0da8:
			wrapper28.m_UI_Cancel.performed -= value28;
			InputActions wrapper33 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2970 @ rax_v91+8]");
			Action<InputAction.CallbackContext> value33 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num28 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0e62;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj241 = 0;
			object obj242 = 0;
			while (true)
			{
				object obj243 = obj242 + obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2912 @ r8_v166+v2915 @ rax_v386*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj242++;
				object obj244 = obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v19 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj244 < 0)
				{
					continue;
				}
				goto IL_0e62;
			}
			object obj245 = obj242 + obj242;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2912 @ r8_v166+8+v2973 @ rcx_v292*8]");
			object obj246 = (nint)0 + (nint)5;
			object obj247 = obj246 << 4;
			object obj248 = obj247 + 312;
			object obj249 = obj248 + num28;
			goto IL_0e77;
			IL_1816:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_182b;
			IL_07ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_07ff;
			IL_07ff:
			wrapper32.m_UI_MoveUI.started -= value32;
			InputActions wrapper34 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2305 @ rax_v56+8]");
			Action<InputAction.CallbackContext> value34 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num29 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_08b9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj250 = 0;
			object obj251 = 0;
			while (true)
			{
				object obj252 = obj251 + obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2247 @ r8_v187+v2250 @ rax_v463*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj251++;
				object obj253 = obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ r10_v12 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj253 < 0)
				{
					continue;
				}
				goto IL_08b9;
			}
			object obj254 = obj251 + obj251;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2247 @ r8_v187+8+v2308 @ rcx_v334*8]");
			object obj255 = (nint)0 + (nint)3;
			object obj256 = obj255 << 4;
			object obj257 = obj256 + 312;
			object obj258 = obj257 + num29;
			goto IL_08ce;
			IL_175c:
			wrapper31.m_UI_TrackedDeviceOrientation.performed -= value31;
			wrapper27 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4110 @ rax_v151+8]");
			value27 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num30 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1816;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj259 = 0;
			object obj260 = 0;
			while (true)
			{
				object obj261 = obj260 + obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4052 @ r8_v130+v4055 @ rax_v254*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj260++;
				object obj262 = obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ r10_v31 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj262 < 0)
				{
					continue;
				}
				goto IL_1816;
			}
			object obj263 = obj260 + obj260;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4052 @ r8_v130+8+v4113 @ rcx_v220*8]");
			object obj264 = (nint)0 + (nint)9;
			object obj265 = obj264 << 4;
			object obj266 = obj265 + 312;
			object obj267 = obj266 + num30;
			goto IL_182b;
			IL_10cf:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_10e4;
			IL_10e4:
			wrapper25.m_UI_ScrollWheel.canceled -= value25;
			wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3350 @ rax_v111+8]");
			value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num31 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_119e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj268 = 0;
			object obj269 = 0;
			while (true)
			{
				object obj270 = obj269 + obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3292 @ r8_v154+v3295 @ rax_v342*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj269++;
				object obj271 = obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ r10_v23 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj271 < 0)
				{
					continue;
				}
				goto IL_119e;
			}
			object obj272 = obj269 + obj269;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3292 @ r8_v154+8+v3353 @ rcx_v268*8]");
			object obj273 = (nint)0 + (nint)7;
			object obj274 = obj273 << 4;
			object obj275 = obj274 + 312;
			object obj276 = obj275 + num31;
			goto IL_11b3;
			IL_19b4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_19c9;
			IL_08b9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_08ce;
			IL_08ce:
			wrapper34.m_UI_MoveUI.performed -= value34;
			InputActions wrapper35 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2400 @ rax_v61+8]");
			Action<InputAction.CallbackContext> value35 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num32 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0988;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj277 = 0;
			object obj278 = 0;
			while (true)
			{
				object obj279 = obj278 + obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2342 @ r8_v184+v2345 @ rax_v452*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj278++;
				object obj280 = obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ r10_v13 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj280 < 0)
				{
					continue;
				}
				goto IL_0988;
			}
			object obj281 = obj278 + obj278;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2342 @ r8_v184+8+v2403 @ rcx_v328*8]");
			object obj282 = (nint)0 + (nint)3;
			object obj283 = obj282 << 4;
			object obj284 = obj283 + 312;
			object obj285 = obj284 + num32;
			goto IL_099d;
			IL_1a98:
			InputActions wrapper36;
			Action<InputAction.CallbackContext> value36;
			wrapper36.m_UI_Up.canceled -= value36;
			wrapper30 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4490 @ rax_v171+8]");
			value30 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num33 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1b52;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj286 = 0;
			object obj287 = 0;
			while (true)
			{
				object obj288 = obj287 + obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4432 @ r8_v118+v4435 @ rax_v210*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj287++;
				object obj289 = obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v234 @ r10_v35 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj289 < 0)
				{
					continue;
				}
				goto IL_1b52;
			}
			object obj290 = obj287 + obj287;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4432 @ r8_v118+8+v4493 @ rcx_v196*8]");
			object obj291 = (nint)0 + (nint)11;
			object obj292 = obj291 << 4;
			object obj293 = obj292 + 312;
			object obj294 = obj293 + num33;
			goto IL_1b67;
			IL_0e62:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e77;
			IL_0e77:
			wrapper33.m_UI_Cancel.canceled -= value33;
			wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3065 @ rax_v96+8]");
			value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num34 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f31;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj295 = 0;
			object obj296 = 0;
			while (true)
			{
				object obj297 = obj296 + obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3007 @ r8_v163+v3010 @ rax_v375*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj296++;
				object obj298 = obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r10_v20 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj298 < 0)
				{
					continue;
				}
				goto IL_0f31;
			}
			object obj299 = obj296 + obj296;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3007 @ r8_v163+8+v3068 @ rcx_v286*8]");
			object obj300 = (nint)0 + (nint)6;
			object obj301 = obj300 << 4;
			object obj302 = obj301 + 312;
			object obj303 = obj302 + num34;
			goto IL_0f46;
			IL_19c9:
			wrapper23.m_UI_Up.performed -= value23;
			wrapper36 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4395 @ rax_v166+8]");
			value36 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num35 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1a83;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj304 = 0;
			object obj305 = 0;
			while (true)
			{
				object obj306 = obj305 + obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4337 @ r8_v121+v4340 @ rax_v221*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj305++;
				object obj307 = obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ r10_v34 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj307 < 0)
				{
					continue;
				}
				goto IL_1a83;
			}
			object obj308 = obj305 + obj305;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4337 @ r8_v121+8+v4398 @ rcx_v202*8]");
			object obj309 = (nint)0 + (nint)10;
			object obj310 = obj309 << 4;
			object obj311 = obj310 + 312;
			object obj312 = obj311 + num35;
			goto IL_1a98;
			IL_0988:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_099d;
			IL_099d:
			wrapper35.m_UI_MoveUI.canceled -= value35;
			wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2495 @ rax_v66+8]");
			value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num36 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0a57;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+B0]");
			object obj313 = 0;
			object obj314 = 0;
			while (true)
			{
				object obj315 = obj314 + obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2437 @ r8_v181+v2440 @ rax_v441*8]");
				if (0 == (nint)typeof(IUIActions))
				{
					break;
				}
				obj314++;
				object obj316 = obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v213 @ r10_v14 (Il2CppClass<InputActions+IUIActions>)+12E]");
				if ((nint)obj316 < 0)
				{
					continue;
				}
				goto IL_0a57;
			}
			object obj317 = obj314 + obj314;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2437 @ r8_v181+8+v2498 @ rcx_v322*8]");
			object obj318 = (nint)0 + (nint)4;
			object obj319 = obj318 << 4;
			object obj320 = obj319 + 312;
			object obj321 = obj320 + num36;
			goto IL_0a6c;
		}

		public void RemoveCallbacks(IUIActions instance)
		{
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public unsafe void SetCallbacks(IUIActions instance)
		{
			//IL_01d2: Expected O, but got Ref
			//IL_008d: Expected O, but got Ref
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null && wrapper.m_UIActionsCallbackInterfaces != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<IUIActions>.Enumerator enumerator = default(List<IUIActions>.Enumerator);
				IUIActions instance2 = default(IUIActions);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					UnregisterCallbacks(instance2);
				}
				enumerator.Dispose();
				InputActions wrapper2 = m_Wrapper;
				bool flag = m_Wrapper == null;
				List<IUIActions>.Enumerator enumerator2 = (List<IUIActions>.Enumerator)(&enumerator);
				if (!flag)
				{
					List<IUIActions> uIActionsCallbackInterfaces = wrapper2.m_UIActionsCallbackInterfaces;
					bool flag2 = wrapper2.m_UIActionsCallbackInterfaces == null;
					enumerator2 = (List<IUIActions>.Enumerator)(&enumerator);
					if (!flag2)
					{
						int version = uIActionsCallbackInterfaces._version + 1;
						uIActionsCallbackInterfaces._version = version;
						((List<IUIActions>.Enumerator*)null)->Dispose();
						object obj = default(object);
						if (obj == null)
						{
							uIActionsCallbackInterfaces._size = 0;
						}
						else
						{
							uIActionsCallbackInterfaces._size = 0;
							if (uIActionsCallbackInterfaces._size > 0)
							{
								Array.Clear(uIActionsCallbackInterfaces._items, 0, uIActionsCallbackInterfaces._size);
							}
						}
						AddCallbacks(instance);
						return;
					}
				}
			}
			throw new NullReferenceException();
		}
	}

	public struct UniversalActions(InputActions wrapper)
	{
		private InputActions m_Wrapper = wrapper;

		public InputAction PointerDelta
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_PointerDelta;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Navigate
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_Navigate;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction PointerPosition
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_PointerPosition;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction PrimaryClick
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_PrimaryClick;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction SecondaryClick
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_SecondaryClick;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Tertiaryclick
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_Tertiaryclick;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction ToggleClipboard
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_ToggleClipboard;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction FocuseClipboard
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_FocuseClipboard;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Escape
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_Escape;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction FreecamScrollWheel
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_FreecamScrollWheel;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction UnequipGasmask
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_UnequipGasmask;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CinamaticHideCursorToggle
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CinamaticHideCursorToggle;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CinamaticAutoReload
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CinamaticAutoReload;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CinamaticLightSwitch
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CinamaticLightSwitch;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CinamaticSwingForce
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CinamaticSwingForce;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CheatRevealallonmap
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CheatRevealallonmap;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CheatImpactF9
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CheatImpactF9;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CheatImpactF10
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CheatImpactF10;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction CheatImpactF11
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_CheatImpactF11;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction RotateLeft
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_RotateLeft;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction RotateRight
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_RotateRight;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Cinamatic4kScreenshot
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_Cinamatic4kScreenshot;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction ContinueEnter
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_ContinueEnter;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction PickUp
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_PickUp;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction Interact
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_Interact;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public InputAction SlowCursor
		{
			get
			{
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null)
				{
					return wrapper.m_Universal_SlowCursor;
				}
				return (InputAction)(object)new NullReferenceException();
			}
		}

		public bool enabled
		{
			get
			{
				//IL_0070: Expected I4, but got O
				InputActions wrapper = m_Wrapper;
				if (m_Wrapper != null && wrapper.m_Universal != null)
				{
					return wrapper.m_Universal.enabled;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}

		public InputActionMap Get()
		{
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null)
			{
				return wrapper.m_Universal;
			}
			return (InputActionMap)(object)new NullReferenceException();
		}

		public void Enable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_Universal.Enable();
		}

		public void Disable()
		{
			InputActions wrapper = m_Wrapper;
			wrapper.m_Universal.Disable();
		}

		public static implicit operator InputActionMap(UniversalActions set)
		{
			//IL_002a: Expected O, but got I
			if ((object)set != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [set @ rcx (InputActions+UniversalActions)+D8]");
				return (InputActionMap)0;
			}
			throw new NullReferenceException();
		}

		public void AddCallbacks(IUniversalActions instance)
		{
			//IL_0089: Expected I, but got O
			//IL_00c1: Expected O, but got I
			//IL_00ca: Expected O, but got I4
			//IL_0158: Expected I, but got O
			//IL_3713: Expected O, but got I
			//IL_371c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3721: Expected O, but got Unknown
			//IL_3729: Unknown result type (might be due to invalid IL or missing references)
			//IL_372e: Expected O, but got Unknown
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Expected O, but got Unknown
			//IL_0190: Expected O, but got I
			//IL_0199: Expected O, but got I4
			//IL_0227: Expected I, but got O
			//IL_3756: Expected O, but got I
			//IL_375f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3764: Expected O, but got Unknown
			//IL_376c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3771: Expected O, but got Unknown
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			//IL_025f: Expected O, but got I
			//IL_0268: Expected O, but got I4
			//IL_02f6: Expected I, but got O
			//IL_3799: Expected O, but got I
			//IL_37a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_37a7: Expected O, but got Unknown
			//IL_37af: Unknown result type (might be due to invalid IL or missing references)
			//IL_37b4: Expected O, but got Unknown
			//IL_0276: Unknown result type (might be due to invalid IL or missing references)
			//IL_027b: Expected O, but got Unknown
			//IL_032e: Expected O, but got I
			//IL_0337: Expected O, but got I4
			//IL_03c5: Expected I, but got O
			//IL_37dc: Expected O, but got I
			//IL_37f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_37f8: Expected O, but got Unknown
			//IL_3800: Unknown result type (might be due to invalid IL or missing references)
			//IL_3805: Expected O, but got Unknown
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_03fd: Expected O, but got I
			//IL_0406: Expected O, but got I4
			//IL_0494: Expected I, but got O
			//IL_382d: Expected O, but got I
			//IL_3844: Unknown result type (might be due to invalid IL or missing references)
			//IL_3849: Expected O, but got Unknown
			//IL_3851: Unknown result type (might be due to invalid IL or missing references)
			//IL_3856: Expected O, but got Unknown
			//IL_0414: Unknown result type (might be due to invalid IL or missing references)
			//IL_0419: Expected O, but got Unknown
			//IL_04cc: Expected O, but got I
			//IL_04d5: Expected O, but got I4
			//IL_0563: Expected I, but got O
			//IL_387e: Expected O, but got I
			//IL_3895: Unknown result type (might be due to invalid IL or missing references)
			//IL_389a: Expected O, but got Unknown
			//IL_38a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_38a7: Expected O, but got Unknown
			//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e8: Expected O, but got Unknown
			//IL_059b: Expected O, but got I
			//IL_05a4: Expected O, but got I4
			//IL_0632: Expected I, but got O
			//IL_38cf: Expected O, but got I
			//IL_38e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_38eb: Expected O, but got Unknown
			//IL_38f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_38f8: Expected O, but got Unknown
			//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b7: Expected O, but got Unknown
			//IL_066a: Expected O, but got I
			//IL_0673: Expected O, but got I4
			//IL_0701: Expected I, but got O
			//IL_3920: Expected O, but got I
			//IL_3937: Unknown result type (might be due to invalid IL or missing references)
			//IL_393c: Expected O, but got Unknown
			//IL_3944: Unknown result type (might be due to invalid IL or missing references)
			//IL_3949: Expected O, but got Unknown
			//IL_0681: Unknown result type (might be due to invalid IL or missing references)
			//IL_0686: Expected O, but got Unknown
			//IL_0739: Expected O, but got I
			//IL_0742: Expected O, but got I4
			//IL_07d0: Expected I, but got O
			//IL_3971: Expected O, but got I
			//IL_3988: Unknown result type (might be due to invalid IL or missing references)
			//IL_398d: Expected O, but got Unknown
			//IL_3995: Unknown result type (might be due to invalid IL or missing references)
			//IL_399a: Expected O, but got Unknown
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_0755: Expected O, but got Unknown
			//IL_0808: Expected O, but got I
			//IL_0811: Expected O, but got I4
			//IL_089f: Expected I, but got O
			//IL_39c2: Expected O, but got I
			//IL_39d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_39de: Expected O, but got Unknown
			//IL_39e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_39eb: Expected O, but got Unknown
			//IL_081f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0824: Expected O, but got Unknown
			//IL_08d7: Expected O, but got I
			//IL_08e0: Expected O, but got I4
			//IL_096e: Expected I, but got O
			//IL_3a13: Expected O, but got I
			//IL_3a2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a2f: Expected O, but got Unknown
			//IL_3a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a3c: Expected O, but got Unknown
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_08f3: Expected O, but got Unknown
			//IL_09a6: Expected O, but got I
			//IL_09af: Expected O, but got I4
			//IL_0a3d: Expected I, but got O
			//IL_3a64: Expected O, but got I
			//IL_3a7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a80: Expected O, but got Unknown
			//IL_3a88: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a8d: Expected O, but got Unknown
			//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c2: Expected O, but got Unknown
			//IL_0a75: Expected O, but got I
			//IL_0a7e: Expected O, but got I4
			//IL_0b0c: Expected I, but got O
			//IL_3ab5: Expected O, but got I
			//IL_3acc: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ad1: Expected O, but got Unknown
			//IL_3ad9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ade: Expected O, but got Unknown
			//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Expected O, but got Unknown
			//IL_0b44: Expected O, but got I
			//IL_0b4d: Expected O, but got I4
			//IL_0bdb: Expected I, but got O
			//IL_3b06: Expected O, but got I
			//IL_3b1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b22: Expected O, but got Unknown
			//IL_3b2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b2f: Expected O, but got Unknown
			//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b60: Expected O, but got Unknown
			//IL_0c13: Expected O, but got I
			//IL_0c1c: Expected O, but got I4
			//IL_0caa: Expected I, but got O
			//IL_3b57: Expected O, but got I
			//IL_3b6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b73: Expected O, but got Unknown
			//IL_3b7b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b80: Expected O, but got Unknown
			//IL_0c2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2f: Expected O, but got Unknown
			//IL_0ce2: Expected O, but got I
			//IL_0ceb: Expected O, but got I4
			//IL_0d79: Expected I, but got O
			//IL_3ba8: Expected O, but got I
			//IL_3bbf: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bc4: Expected O, but got Unknown
			//IL_3bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bd1: Expected O, but got Unknown
			//IL_0cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cfe: Expected O, but got Unknown
			//IL_0db1: Expected O, but got I
			//IL_0dba: Expected O, but got I4
			//IL_0e48: Expected I, but got O
			//IL_3bf9: Expected O, but got I
			//IL_3c10: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c15: Expected O, but got Unknown
			//IL_3c1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c22: Expected O, but got Unknown
			//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dcd: Expected O, but got Unknown
			//IL_0e80: Expected O, but got I
			//IL_0e89: Expected O, but got I4
			//IL_0f17: Expected I, but got O
			//IL_3c4a: Expected O, but got I
			//IL_3c61: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c66: Expected O, but got Unknown
			//IL_3c6e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c73: Expected O, but got Unknown
			//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e9c: Expected O, but got Unknown
			//IL_0f4f: Expected O, but got I
			//IL_0f58: Expected O, but got I4
			//IL_0fe6: Expected I, but got O
			//IL_3c9b: Expected O, but got I
			//IL_3cb2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cb7: Expected O, but got Unknown
			//IL_3cbf: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cc4: Expected O, but got Unknown
			//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f6b: Expected O, but got Unknown
			//IL_101e: Expected O, but got I
			//IL_1027: Expected O, but got I4
			//IL_10b5: Expected I, but got O
			//IL_3cec: Expected O, but got I
			//IL_3d03: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d08: Expected O, but got Unknown
			//IL_3d10: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d15: Expected O, but got Unknown
			//IL_1035: Unknown result type (might be due to invalid IL or missing references)
			//IL_103a: Expected O, but got Unknown
			//IL_10ed: Expected O, but got I
			//IL_10f6: Expected O, but got I4
			//IL_1184: Expected I, but got O
			//IL_3d3d: Expected O, but got I
			//IL_3d54: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d59: Expected O, but got Unknown
			//IL_3d61: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d66: Expected O, but got Unknown
			//IL_1104: Unknown result type (might be due to invalid IL or missing references)
			//IL_1109: Expected O, but got Unknown
			//IL_11bc: Expected O, but got I
			//IL_11c5: Expected O, but got I4
			//IL_1253: Expected I, but got O
			//IL_3d8e: Expected O, but got I
			//IL_3da5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3daa: Expected O, but got Unknown
			//IL_3db2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3db7: Expected O, but got Unknown
			//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_11d8: Expected O, but got Unknown
			//IL_128b: Expected O, but got I
			//IL_1294: Expected O, but got I4
			//IL_1322: Expected I, but got O
			//IL_3ddf: Expected O, but got I
			//IL_3df6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dfb: Expected O, but got Unknown
			//IL_3e03: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e08: Expected O, but got Unknown
			//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_12a7: Expected O, but got Unknown
			//IL_135a: Expected O, but got I
			//IL_1363: Expected O, but got I4
			//IL_13f1: Expected I, but got O
			//IL_3e30: Expected O, but got I
			//IL_3e47: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e4c: Expected O, but got Unknown
			//IL_3e54: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e59: Expected O, but got Unknown
			//IL_1371: Unknown result type (might be due to invalid IL or missing references)
			//IL_1376: Expected O, but got Unknown
			//IL_1429: Expected O, but got I
			//IL_1432: Expected O, but got I4
			//IL_14c0: Expected I, but got O
			//IL_3e81: Expected O, but got I
			//IL_3e98: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e9d: Expected O, but got Unknown
			//IL_3ea5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3eaa: Expected O, but got Unknown
			//IL_1440: Unknown result type (might be due to invalid IL or missing references)
			//IL_1445: Expected O, but got Unknown
			//IL_14f8: Expected O, but got I
			//IL_1501: Expected O, but got I4
			//IL_158f: Expected I, but got O
			//IL_3ed2: Expected O, but got I
			//IL_3ee9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3eee: Expected O, but got Unknown
			//IL_3ef6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3efb: Expected O, but got Unknown
			//IL_150f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1514: Expected O, but got Unknown
			//IL_15c7: Expected O, but got I
			//IL_15d0: Expected O, but got I4
			//IL_165e: Expected I, but got O
			//IL_3f23: Expected O, but got I
			//IL_3f3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f3f: Expected O, but got Unknown
			//IL_3f47: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f4c: Expected O, but got Unknown
			//IL_15de: Unknown result type (might be due to invalid IL or missing references)
			//IL_15e3: Expected O, but got Unknown
			//IL_1696: Expected O, but got I
			//IL_169f: Expected O, but got I4
			//IL_172d: Expected I, but got O
			//IL_3f74: Expected O, but got I
			//IL_3f8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f90: Expected O, but got Unknown
			//IL_3f98: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f9d: Expected O, but got Unknown
			//IL_16ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_16b2: Expected O, but got Unknown
			//IL_1765: Expected O, but got I
			//IL_176e: Expected O, but got I4
			//IL_17fc: Expected I, but got O
			//IL_3fc5: Expected O, but got I
			//IL_3fdc: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fe1: Expected O, but got Unknown
			//IL_3fe9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fee: Expected O, but got Unknown
			//IL_177c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1781: Expected O, but got Unknown
			//IL_1834: Expected O, but got I
			//IL_183d: Expected O, but got I4
			//IL_18cb: Expected I, but got O
			//IL_4016: Expected O, but got I
			//IL_402d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4032: Expected O, but got Unknown
			//IL_403a: Unknown result type (might be due to invalid IL or missing references)
			//IL_403f: Expected O, but got Unknown
			//IL_184b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1850: Expected O, but got Unknown
			//IL_1903: Expected O, but got I
			//IL_190c: Expected O, but got I4
			//IL_199a: Expected I, but got O
			//IL_4067: Expected O, but got I
			//IL_407e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4083: Expected O, but got Unknown
			//IL_408b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4090: Expected O, but got Unknown
			//IL_191a: Unknown result type (might be due to invalid IL or missing references)
			//IL_191f: Expected O, but got Unknown
			//IL_19d2: Expected O, but got I
			//IL_19db: Expected O, but got I4
			//IL_1a69: Expected I, but got O
			//IL_40b8: Expected O, but got I
			//IL_40cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_40d4: Expected O, but got Unknown
			//IL_40dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_40e1: Expected O, but got Unknown
			//IL_19e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_19ee: Expected O, but got Unknown
			//IL_1aa1: Expected O, but got I
			//IL_1aaa: Expected O, but got I4
			//IL_1b38: Expected I, but got O
			//IL_4109: Expected O, but got I
			//IL_4120: Unknown result type (might be due to invalid IL or missing references)
			//IL_4125: Expected O, but got Unknown
			//IL_412d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4132: Expected O, but got Unknown
			//IL_1ab8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1abd: Expected O, but got Unknown
			//IL_1b70: Expected O, but got I
			//IL_1b79: Expected O, but got I4
			//IL_1c07: Expected I, but got O
			//IL_415a: Expected O, but got I
			//IL_4171: Unknown result type (might be due to invalid IL or missing references)
			//IL_4176: Expected O, but got Unknown
			//IL_417e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4183: Expected O, but got Unknown
			//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b8c: Expected O, but got Unknown
			//IL_1c3f: Expected O, but got I
			//IL_1c48: Expected O, but got I4
			//IL_1cd6: Expected I, but got O
			//IL_41ab: Expected O, but got I
			//IL_41c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_41c7: Expected O, but got Unknown
			//IL_41cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_41d4: Expected O, but got Unknown
			//IL_1c56: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c5b: Expected O, but got Unknown
			//IL_1d0e: Expected O, but got I
			//IL_1d17: Expected O, but got I4
			//IL_1da5: Expected I, but got O
			//IL_41fc: Expected O, but got I
			//IL_4213: Unknown result type (might be due to invalid IL or missing references)
			//IL_4218: Expected O, but got Unknown
			//IL_4220: Unknown result type (might be due to invalid IL or missing references)
			//IL_4225: Expected O, but got Unknown
			//IL_1d25: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d2a: Expected O, but got Unknown
			//IL_1ddd: Expected O, but got I
			//IL_1de6: Expected O, but got I4
			//IL_1e74: Expected I, but got O
			//IL_424d: Expected O, but got I
			//IL_4264: Unknown result type (might be due to invalid IL or missing references)
			//IL_4269: Expected O, but got Unknown
			//IL_4271: Unknown result type (might be due to invalid IL or missing references)
			//IL_4276: Expected O, but got Unknown
			//IL_1df4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1df9: Expected O, but got Unknown
			//IL_1eac: Expected O, but got I
			//IL_1eb5: Expected O, but got I4
			//IL_1f43: Expected I, but got O
			//IL_429e: Expected O, but got I
			//IL_42b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_42ba: Expected O, but got Unknown
			//IL_42c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_42c7: Expected O, but got Unknown
			//IL_1ec3: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ec8: Expected O, but got Unknown
			//IL_1f7b: Expected O, but got I
			//IL_1f84: Expected O, but got I4
			//IL_2012: Expected I, but got O
			//IL_42ef: Expected O, but got I
			//IL_4306: Unknown result type (might be due to invalid IL or missing references)
			//IL_430b: Expected O, but got Unknown
			//IL_4313: Unknown result type (might be due to invalid IL or missing references)
			//IL_4318: Expected O, but got Unknown
			//IL_1f92: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f97: Expected O, but got Unknown
			//IL_204a: Expected O, but got I
			//IL_2053: Expected O, but got I4
			//IL_20e1: Expected I, but got O
			//IL_4340: Expected O, but got I
			//IL_4357: Unknown result type (might be due to invalid IL or missing references)
			//IL_435c: Expected O, but got Unknown
			//IL_4364: Unknown result type (might be due to invalid IL or missing references)
			//IL_4369: Expected O, but got Unknown
			//IL_2061: Unknown result type (might be due to invalid IL or missing references)
			//IL_2066: Expected O, but got Unknown
			//IL_2119: Expected O, but got I
			//IL_2122: Expected O, but got I4
			//IL_21b0: Expected I, but got O
			//IL_4391: Expected O, but got I
			//IL_43a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_43ad: Expected O, but got Unknown
			//IL_43b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_43ba: Expected O, but got Unknown
			//IL_2130: Unknown result type (might be due to invalid IL or missing references)
			//IL_2135: Expected O, but got Unknown
			//IL_21e8: Expected O, but got I
			//IL_21f1: Expected O, but got I4
			//IL_227f: Expected I, but got O
			//IL_43e2: Expected O, but got I
			//IL_43f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_43fe: Expected O, but got Unknown
			//IL_4406: Unknown result type (might be due to invalid IL or missing references)
			//IL_440b: Expected O, but got Unknown
			//IL_21ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_2204: Expected O, but got Unknown
			//IL_22b7: Expected O, but got I
			//IL_22c0: Expected O, but got I4
			//IL_234e: Expected I, but got O
			//IL_4433: Expected O, but got I
			//IL_444a: Unknown result type (might be due to invalid IL or missing references)
			//IL_444f: Expected O, but got Unknown
			//IL_4457: Unknown result type (might be due to invalid IL or missing references)
			//IL_445c: Expected O, but got Unknown
			//IL_22ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d3: Expected O, but got Unknown
			//IL_2386: Expected O, but got I
			//IL_238f: Expected O, but got I4
			//IL_241d: Expected I, but got O
			//IL_4484: Expected O, but got I
			//IL_449b: Unknown result type (might be due to invalid IL or missing references)
			//IL_44a0: Expected O, but got Unknown
			//IL_44a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_44ad: Expected O, but got Unknown
			//IL_239d: Unknown result type (might be due to invalid IL or missing references)
			//IL_23a2: Expected O, but got Unknown
			//IL_2455: Expected O, but got I
			//IL_245e: Expected O, but got I4
			//IL_24ec: Expected I, but got O
			//IL_44d5: Expected O, but got I
			//IL_44ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_44f1: Expected O, but got Unknown
			//IL_44f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_44fe: Expected O, but got Unknown
			//IL_246c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2471: Expected O, but got Unknown
			//IL_2524: Expected O, but got I
			//IL_252d: Expected O, but got I4
			//IL_25bb: Expected I, but got O
			//IL_4526: Expected O, but got I
			//IL_453d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4542: Expected O, but got Unknown
			//IL_454a: Unknown result type (might be due to invalid IL or missing references)
			//IL_454f: Expected O, but got Unknown
			//IL_253b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2540: Expected O, but got Unknown
			//IL_25f3: Expected O, but got I
			//IL_25fc: Expected O, but got I4
			//IL_268a: Expected I, but got O
			//IL_4577: Expected O, but got I
			//IL_458e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4593: Expected O, but got Unknown
			//IL_459b: Unknown result type (might be due to invalid IL or missing references)
			//IL_45a0: Expected O, but got Unknown
			//IL_260a: Unknown result type (might be due to invalid IL or missing references)
			//IL_260f: Expected O, but got Unknown
			//IL_26c2: Expected O, but got I
			//IL_26cb: Expected O, but got I4
			//IL_2759: Expected I, but got O
			//IL_45c8: Expected O, but got I
			//IL_45df: Unknown result type (might be due to invalid IL or missing references)
			//IL_45e4: Expected O, but got Unknown
			//IL_45ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_45f1: Expected O, but got Unknown
			//IL_26d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_26de: Expected O, but got Unknown
			//IL_2791: Expected O, but got I
			//IL_279a: Expected O, but got I4
			//IL_2828: Expected I, but got O
			//IL_4619: Expected O, but got I
			//IL_4630: Unknown result type (might be due to invalid IL or missing references)
			//IL_4635: Expected O, but got Unknown
			//IL_463d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4642: Expected O, but got Unknown
			//IL_27a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_27ad: Expected O, but got Unknown
			//IL_2860: Expected O, but got I
			//IL_2869: Expected O, but got I4
			//IL_28f7: Expected I, but got O
			//IL_466a: Expected O, but got I
			//IL_4681: Unknown result type (might be due to invalid IL or missing references)
			//IL_4686: Expected O, but got Unknown
			//IL_468e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4693: Expected O, but got Unknown
			//IL_2877: Unknown result type (might be due to invalid IL or missing references)
			//IL_287c: Expected O, but got Unknown
			//IL_292f: Expected O, but got I
			//IL_2938: Expected O, but got I4
			//IL_29c6: Expected I, but got O
			//IL_46bb: Expected O, but got I
			//IL_46d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_46d7: Expected O, but got Unknown
			//IL_46df: Unknown result type (might be due to invalid IL or missing references)
			//IL_46e4: Expected O, but got Unknown
			//IL_2946: Unknown result type (might be due to invalid IL or missing references)
			//IL_294b: Expected O, but got Unknown
			//IL_29fe: Expected O, but got I
			//IL_2a07: Expected O, but got I4
			//IL_2a95: Expected I, but got O
			//IL_470c: Expected O, but got I
			//IL_4723: Unknown result type (might be due to invalid IL or missing references)
			//IL_4728: Expected O, but got Unknown
			//IL_4730: Unknown result type (might be due to invalid IL or missing references)
			//IL_4735: Expected O, but got Unknown
			//IL_2a15: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a1a: Expected O, but got Unknown
			//IL_2acd: Expected O, but got I
			//IL_2ad6: Expected O, but got I4
			//IL_2b64: Expected I, but got O
			//IL_475d: Expected O, but got I
			//IL_4774: Unknown result type (might be due to invalid IL or missing references)
			//IL_4779: Expected O, but got Unknown
			//IL_4781: Unknown result type (might be due to invalid IL or missing references)
			//IL_4786: Expected O, but got Unknown
			//IL_2ae4: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ae9: Expected O, but got Unknown
			//IL_2b9c: Expected O, but got I
			//IL_2ba5: Expected O, but got I4
			//IL_2c33: Expected I, but got O
			//IL_47ae: Expected O, but got I
			//IL_47c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_47ca: Expected O, but got Unknown
			//IL_47d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_47d7: Expected O, but got Unknown
			//IL_2bb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bb8: Expected O, but got Unknown
			//IL_2c6b: Expected O, but got I
			//IL_2c74: Expected O, but got I4
			//IL_2d02: Expected I, but got O
			//IL_47ff: Expected O, but got I
			//IL_4816: Unknown result type (might be due to invalid IL or missing references)
			//IL_481b: Expected O, but got Unknown
			//IL_4823: Unknown result type (might be due to invalid IL or missing references)
			//IL_4828: Expected O, but got Unknown
			//IL_2c82: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c87: Expected O, but got Unknown
			//IL_2d3a: Expected O, but got I
			//IL_2d43: Expected O, but got I4
			//IL_2dd1: Expected I, but got O
			//IL_4850: Expected O, but got I
			//IL_4867: Unknown result type (might be due to invalid IL or missing references)
			//IL_486c: Expected O, but got Unknown
			//IL_4874: Unknown result type (might be due to invalid IL or missing references)
			//IL_4879: Expected O, but got Unknown
			//IL_2d51: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d56: Expected O, but got Unknown
			//IL_2e09: Expected O, but got I
			//IL_2e12: Expected O, but got I4
			//IL_2ea0: Expected I, but got O
			//IL_48a1: Expected O, but got I
			//IL_48b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_48bd: Expected O, but got Unknown
			//IL_48c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_48ca: Expected O, but got Unknown
			//IL_2e20: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e25: Expected O, but got Unknown
			//IL_2ed8: Expected O, but got I
			//IL_2ee1: Expected O, but got I4
			//IL_2f6f: Expected I, but got O
			//IL_48f2: Expected O, but got I
			//IL_4909: Unknown result type (might be due to invalid IL or missing references)
			//IL_490e: Expected O, but got Unknown
			//IL_4916: Unknown result type (might be due to invalid IL or missing references)
			//IL_491b: Expected O, but got Unknown
			//IL_2eef: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ef4: Expected O, but got Unknown
			//IL_2fa7: Expected O, but got I
			//IL_2fb0: Expected O, but got I4
			//IL_303e: Expected I, but got O
			//IL_4943: Expected O, but got I
			//IL_495a: Unknown result type (might be due to invalid IL or missing references)
			//IL_495f: Expected O, but got Unknown
			//IL_4967: Unknown result type (might be due to invalid IL or missing references)
			//IL_496c: Expected O, but got Unknown
			//IL_2fbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fc3: Expected O, but got Unknown
			//IL_3076: Expected O, but got I
			//IL_307f: Expected O, but got I4
			//IL_310d: Expected I, but got O
			//IL_4994: Expected O, but got I
			//IL_49ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_49b0: Expected O, but got Unknown
			//IL_49b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_49bd: Expected O, but got Unknown
			//IL_308d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3092: Expected O, but got Unknown
			//IL_3145: Expected O, but got I
			//IL_314e: Expected O, but got I4
			//IL_31dc: Expected I, but got O
			//IL_49e5: Expected O, but got I
			//IL_49fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a01: Expected O, but got Unknown
			//IL_4a09: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a0e: Expected O, but got Unknown
			//IL_315c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3161: Expected O, but got Unknown
			//IL_3214: Expected O, but got I
			//IL_321d: Expected O, but got I4
			//IL_4a36: Expected O, but got I
			//IL_4a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a52: Expected O, but got Unknown
			//IL_4a5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a5f: Expected O, but got Unknown
			//IL_322b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3230: Expected O, but got Unknown
			if (instance == null)
			{
				return;
			}
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_UniversalActionsCallbackInterfaces.Contains(instance))
			{
				return;
			}
			InputActions wrapper2 = m_Wrapper;
			wrapper2.m_UniversalActionsCallbackInterfaces.Add(instance);
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2828 @ rax_v12+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0101;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2770 @ r8_v425+v2773 @ rax_v1071*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v285 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_0101;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2770 @ r8_v425+8+v2831 @ rcx_v748*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_0116;
			IL_1fd0:
			InputActions wrapper4;
			Action<InputAction.CallbackContext> value2;
			wrapper4.m_Universal_CinamaticAutoReload.canceled += value2;
			InputActions wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6527 @ rax_v207+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_208a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6469 @ r8_v308+v6472 @ rax_v646*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_208a;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6469 @ r8_v308+8+v6530 @ rcx_v514*8]");
			object obj14 = (nint)0 + (nint)13;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_209f;
			IL_24aa:
			InputActions wrapper6;
			Action<InputAction.CallbackContext> value4;
			wrapper6.m_Universal_CinamaticSwingForce.canceled += value4;
			InputActions wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7097 @ rax_v237+8]");
			Action<InputAction.CallbackContext> value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2564;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7039 @ r8_v290+v7042 @ rax_v580*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_2564;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7039 @ r8_v290+8+v7100 @ rcx_v478*8]");
			object obj23 = (nint)0 + (nint)15;
			object obj24 = obj23 << 4;
			object obj25 = obj24 + 312;
			object obj26 = obj25 + num3;
			goto IL_2579;
			IL_112d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1142;
			IL_1142:
			InputActions wrapper8;
			Action<InputAction.CallbackContext> value6;
			wrapper8.m_Universal_ToggleClipboard.canceled += value6;
			InputActions wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4817 @ rax_v117+8]");
			Action<InputAction.CallbackContext> value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_11fc;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj27 = 0;
			object obj28 = 0;
			while (true)
			{
				object obj29 = obj28 + obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4759 @ r8_v362+v4762 @ rax_v844*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj28++;
				object obj30 = obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj30 < 0)
				{
					continue;
				}
				goto IL_11fc;
			}
			object obj31 = obj28 + obj28;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4759 @ r8_v362+8+v4820 @ rcx_v622*8]");
			object obj32 = (nint)0 + (nint)7;
			object obj33 = obj32 << 4;
			object obj34 = obj33 + 312;
			object obj35 = obj34 + num4;
			goto IL_1211;
			IL_216e:
			InputActions wrapper10;
			Action<InputAction.CallbackContext> value8;
			wrapper10.m_Universal_CinamaticLightSwitch.performed += value8;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6717 @ rax_v217+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2228;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj36 = 0;
			object obj37 = 0;
			while (true)
			{
				object obj38 = obj37 + obj37;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6659 @ r8_v302+v6662 @ rax_v624*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj37++;
				object obj39 = obj37;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj39 < 0)
				{
					continue;
				}
				goto IL_2228;
			}
			object obj40 = obj37 + obj37;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6659 @ r8_v302+8+v6720 @ rcx_v502*8]");
			object obj41 = (nint)0 + (nint)13;
			object obj42 = obj41 << 4;
			object obj43 = obj42 + 312;
			object obj44 = obj43 + num5;
			goto IL_223d;
			IL_2a3e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2a53;
			IL_0101:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0116;
			IL_0116:
			wrapper3.m_Universal_PointerDelta.started += value;
			InputActions wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2921 @ rax_v17+8]");
			Action<InputAction.CallbackContext> value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01d0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj45 = 0;
			object obj46 = 0;
			while (true)
			{
				object obj47 = obj46 + obj46;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2863 @ r8_v422+v2866 @ rax_v1062*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj46++;
				object obj48 = obj46;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj48 < 0)
				{
					continue;
				}
				goto IL_01d0;
			}
			object obj49 = obj46 + obj46;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2863 @ r8_v422+8+v2924 @ rcx_v742*8]");
			object obj50 = (nint)0 << 4;
			object obj51 = obj50 + 312;
			object obj52 = obj51 + num6;
			goto IL_01e5;
			IL_2f18:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2f2d;
			IL_2e5e:
			InputActions wrapper13;
			Action<InputAction.CallbackContext> value11;
			wrapper13.m_Universal_CheatImpactF11.canceled += value11;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8237 @ rax_v297+8]");
			Action<InputAction.CallbackContext> value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2f18;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj53 = 0;
			object obj54 = 0;
			while (true)
			{
				object obj55 = obj54 + obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8179 @ r8_v254+v8182 @ rax_v448*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj54++;
				object obj56 = obj54;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj56 < 0)
				{
					continue;
				}
				goto IL_2f18;
			}
			object obj57 = obj54 + obj54;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8179 @ r8_v254+8+v8240 @ rcx_v406*8]");
			object obj58 = (nint)0 + (nint)19;
			object obj59 = obj58 << 4;
			object obj60 = obj59 + 312;
			object obj61 = obj60 + num7;
			goto IL_2f2d;
			IL_2228:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_223d;
			IL_2a53:
			InputActions wrapper15;
			Action<InputAction.CallbackContext> value13;
			wrapper15.m_Universal_CheatImpactF10.started += value13;
			InputActions wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7762 @ rax_v272+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2b0d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj62 = 0;
			object obj63 = 0;
			while (true)
			{
				object obj64 = obj63 + obj63;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7704 @ r8_v269+v7707 @ rax_v503*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj63++;
				object obj65 = obj63;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj65 < 0)
				{
					continue;
				}
				goto IL_2b0d;
			}
			object obj66 = obj63 + obj63;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7704 @ r8_v269+8+v7765 @ rcx_v436*8]");
			object obj67 = (nint)0 + (nint)17;
			object obj68 = obj67 << 4;
			object obj69 = obj68 + 312;
			object obj70 = obj69 + num8;
			goto IL_2b22;
			IL_01d0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_01e5;
			IL_01e5:
			wrapper12.m_Universal_PointerDelta.performed += value10;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3014 @ rax_v22+8]");
			Action<InputAction.CallbackContext> value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_029f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj71 = 0;
			object obj72 = 0;
			while (true)
			{
				object obj73 = obj72 + obj72;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2956 @ r8_v419+v2959 @ rax_v1053*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj72++;
				object obj74 = obj72;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj74 < 0)
				{
					continue;
				}
				goto IL_029f;
			}
			object obj75 = obj72 + obj72;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2956 @ r8_v419+8+v3017 @ rcx_v736*8]");
			object obj76 = (nint)0 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_02b4;
			IL_11fc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1211;
			IL_1211:
			wrapper9.m_Universal_FocuseClipboard.started += value7;
			InputActions wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4912 @ rax_v122+8]");
			Action<InputAction.CallbackContext> value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_12cb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4854 @ r8_v359+v4857 @ rax_v833*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_12cb;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4854 @ r8_v359+8+v4915 @ rcx_v616*8]");
			object obj84 = (nint)0 + (nint)7;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_12e0;
			IL_1a12:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1a27;
			IL_2633:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2648;
			IL_029f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_02b4;
			IL_02b4:
			wrapper17.m_Universal_PointerDelta.canceled += value15;
			InputActions wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3107 @ rax_v27+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_036e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3049 @ r8_v416+v3052 @ rax_v1042*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v288 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_036e;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3049 @ r8_v416+8+v3110 @ rcx_v730*8]");
			object obj93 = (nint)0 + (nint)1;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_0383;
			IL_1a27:
			InputActions wrapper20;
			Action<InputAction.CallbackContext> value18;
			wrapper20.m_Universal_UnequipGasmask.performed += value18;
			InputActions wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5862 @ rax_v172+8]");
			Action<InputAction.CallbackContext> value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v317 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1ae1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v317 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5804 @ r8_v329+v5807 @ rax_v723*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v317 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_1ae1;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5804 @ r8_v329+8+v5865 @ rcx_v556*8]");
			object obj102 = (nint)0 + (nint)10;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_1af6;
			IL_1e1d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1e32;
			IL_30b6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_30cb;
			IL_2648:
			InputActions wrapper22;
			Action<InputAction.CallbackContext> value20;
			wrapper22.m_Universal_CheatRevealallonmap.performed += value20;
			InputActions wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7287 @ rax_v247+8]");
			Action<InputAction.CallbackContext> value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2702;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7229 @ r8_v284+v7232 @ rax_v558*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_2702;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7229 @ r8_v284+8+v7290 @ rcx_v466*8]");
			object obj111 = (nint)0 + (nint)15;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_2717;
			IL_036e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0383;
			IL_0383:
			wrapper19.m_Universal_Navigate.started += value17;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3202 @ rax_v32+8]");
			Action<InputAction.CallbackContext> value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_043d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3144 @ r8_v413+v3147 @ rax_v1031*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_043d;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3144 @ r8_v413+8+v3205 @ rcx_v724*8]");
			object obj120 = (nint)0 + (nint)1;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_0452;
			IL_12cb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_12e0;
			IL_12e0:
			wrapper18.m_Universal_FocuseClipboard.performed += value16;
			InputActions wrapper25 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5007 @ rax_v127+8]");
			Action<InputAction.CallbackContext> value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v308 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_139a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v308 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4949 @ r8_v356+v4952 @ rax_v822*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v308 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_139a;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4949 @ r8_v356+8+v5010 @ rcx_v610*8]");
			object obj129 = (nint)0 + (nint)7;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_13af;
			IL_1e32:
			InputActions wrapper26;
			Action<InputAction.CallbackContext> value24;
			wrapper26.m_Universal_CinamaticAutoReload.started += value24;
			InputActions wrapper27 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6337 @ rax_v197+8]");
			Action<InputAction.CallbackContext> value25 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1eec;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6279 @ r8_v314+v6282 @ rax_v668*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_1eec;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6279 @ r8_v314+8+v6340 @ rcx_v526*8]");
			object obj138 = (nint)0 + (nint)12;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_1f01;
			IL_230c:
			InputActions wrapper28;
			Action<InputAction.CallbackContext> value26;
			wrapper28.m_Universal_CinamaticSwingForce.started += value26;
			InputActions wrapper29 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6907 @ rax_v227+8]");
			Action<InputAction.CallbackContext> value27 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_23c6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6849 @ r8_v296+v6852 @ rax_v602*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_23c6;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6849 @ r8_v296+8+v6910 @ rcx_v490*8]");
			object obj147 = (nint)0 + (nint)14;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_23db;
			IL_043d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0452;
			IL_0452:
			wrapper24.m_Universal_Navigate.performed += value22;
			InputActions wrapper30 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3297 @ rax_v37+8]");
			Action<InputAction.CallbackContext> value28 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_050c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3239 @ r8_v410+v3242 @ rax_v1020*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_050c;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3239 @ r8_v410+8+v3300 @ rcx_v718*8]");
			object obj156 = (nint)0 + (nint)1;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_0521;
			IL_2984:
			InputActions wrapper31;
			Action<InputAction.CallbackContext> value29;
			wrapper31.m_Universal_CheatImpactF9.canceled += value29;
			wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7667 @ rax_v267+8]");
			value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2a3e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7609 @ r8_v272+v7612 @ rax_v514*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_2a3e;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7609 @ r8_v272+8+v7670 @ rcx_v442*8]");
			object obj165 = (nint)0 + (nint)17;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_2a53;
			IL_2ffc:
			InputActions wrapper32;
			Action<InputAction.CallbackContext> value30;
			wrapper32.m_Universal_RotateLeft.performed += value30;
			InputActions wrapper33 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8427 @ rax_v307+8]");
			Action<InputAction.CallbackContext> value31 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_30b6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8369 @ r8_v248+v8372 @ rax_v426*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_30b6;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8369 @ r8_v248+8+v8430 @ rcx_v394*8]");
			object obj174 = (nint)0 + (nint)19;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_30cb;
			IL_319a:
			InputActions wrapper34;
			Action<InputAction.CallbackContext> value32;
			wrapper34.m_Universal_RotateRight.started += value32;
			InputActions wrapper35 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8615 @ rax_v317+8]");
			Action<InputAction.CallbackContext> value33 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8545 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_3254;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8545 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8559 @ r8_v242+v8564 @ rax_v404*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8545 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_3254;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8559 @ r8_v242+8+v8618 @ rcx_v383*8]");
			object obj183 = (nint)0 + (nint)20;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_3269;
			IL_23db:
			wrapper29.m_Universal_CinamaticSwingForce.performed += value27;
			wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7002 @ rax_v232+8]");
			value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2495;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6944 @ r8_v293+v6947 @ rax_v591*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v329 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_2495;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6944 @ r8_v293+8+v7005 @ rcx_v484*8]");
			object obj192 = (nint)0 + (nint)14;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_24aa;
			IL_050c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0521;
			IL_0521:
			wrapper30.m_Universal_Navigate.canceled += value28;
			InputActions wrapper36 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3392 @ rax_v42+8]");
			Action<InputAction.CallbackContext> value34 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_05db;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3334 @ r8_v407+v3337 @ rax_v1009*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_05db;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3334 @ r8_v407+8+v3395 @ rcx_v712*8]");
			object obj201 = (nint)0 + (nint)2;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_05f0;
			IL_139a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_13af;
			IL_13af:
			wrapper25.m_Universal_FocuseClipboard.canceled += value23;
			InputActions wrapper37 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5102 @ rax_v132+8]");
			Action<InputAction.CallbackContext> value35 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1469;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5044 @ r8_v353+v5047 @ rax_v811*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_1469;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5044 @ r8_v353+8+v5105 @ rcx_v604*8]");
			object obj210 = (nint)0 + (nint)8;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_147e;
			IL_1ae1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1af6;
			IL_2b0d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2b22;
			IL_05db:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_05f0;
			IL_05f0:
			wrapper36.m_Universal_PointerPosition.started += value34;
			InputActions wrapper38 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3487 @ rax_v47+8]");
			Action<InputAction.CallbackContext> value36 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num25 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_06aa;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj214 = 0;
			object obj215 = 0;
			while (true)
			{
				object obj216 = obj215 + obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3429 @ r8_v404+v3432 @ rax_v998*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj215++;
				object obj217 = obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj217 < 0)
				{
					continue;
				}
				goto IL_06aa;
			}
			object obj218 = obj215 + obj215;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3429 @ r8_v404+8+v3490 @ rcx_v706*8]");
			object obj219 = (nint)0 + (nint)2;
			object obj220 = obj219 << 4;
			object obj221 = obj220 + 312;
			object obj222 = obj221 + num25;
			goto IL_06bf;
			IL_1af6:
			wrapper21.m_Universal_UnequipGasmask.canceled += value19;
			InputActions wrapper39 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5957 @ rax_v177+8]");
			Action<InputAction.CallbackContext> value37 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num26 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1bb0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj223 = 0;
			object obj224 = 0;
			while (true)
			{
				object obj225 = obj224 + obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5899 @ r8_v326+v5902 @ rax_v712*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj224++;
				object obj226 = obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj226 < 0)
				{
					continue;
				}
				goto IL_1bb0;
			}
			object obj227 = obj224 + obj224;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5899 @ r8_v326+8+v5960 @ rcx_v550*8]");
			object obj228 = (nint)0 + (nint)11;
			object obj229 = obj228 << 4;
			object obj230 = obj229 + 312;
			object obj231 = obj230 + num26;
			goto IL_1bc5;
			IL_2564:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2579;
			IL_2d7a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2d8f;
			IL_2b22:
			wrapper16.m_Universal_CheatImpactF10.performed += value14;
			InputActions wrapper40 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7857 @ rax_v277+8]");
			Action<InputAction.CallbackContext> value38 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num27 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2bdc;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj232 = 0;
			object obj233 = 0;
			while (true)
			{
				object obj234 = obj233 + obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7799 @ r8_v266+v7802 @ rax_v492*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj233++;
				object obj235 = obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v338 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj235 < 0)
				{
					continue;
				}
				goto IL_2bdc;
			}
			object obj236 = obj233 + obj233;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7799 @ r8_v266+8+v7860 @ rcx_v430*8]");
			object obj237 = (nint)0 + (nint)17;
			object obj238 = obj237 << 4;
			object obj239 = obj238 + 312;
			object obj240 = obj239 + num27;
			goto IL_2bf1;
			IL_06aa:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_06bf;
			IL_06bf:
			wrapper38.m_Universal_PointerPosition.performed += value36;
			InputActions wrapper41 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3582 @ rax_v52+8]");
			Action<InputAction.CallbackContext> value39 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num28 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0779;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj241 = 0;
			object obj242 = 0;
			while (true)
			{
				object obj243 = obj242 + obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3524 @ r8_v401+v3527 @ rax_v987*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj242++;
				object obj244 = obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj244 < 0)
				{
					continue;
				}
				goto IL_0779;
			}
			object obj245 = obj242 + obj242;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3524 @ r8_v401+8+v3585 @ rcx_v700*8]");
			object obj246 = (nint)0 + (nint)2;
			object obj247 = obj246 << 4;
			object obj248 = obj247 + 312;
			object obj249 = obj248 + num28;
			goto IL_078e;
			IL_1469:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_147e;
			IL_147e:
			wrapper37.m_Universal_Escape.started += value35;
			InputActions wrapper42 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5197 @ rax_v137+8]");
			Action<InputAction.CallbackContext> value40 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num29 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1538;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj250 = 0;
			object obj251 = 0;
			while (true)
			{
				object obj252 = obj251 + obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5139 @ r8_v350+v5142 @ rax_v800*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj251++;
				object obj253 = obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj253 < 0)
				{
					continue;
				}
				goto IL_1538;
			}
			object obj254 = obj251 + obj251;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5139 @ r8_v350+8+v5200 @ rcx_v598*8]");
			object obj255 = (nint)0 + (nint)8;
			object obj256 = obj255 << 4;
			object obj257 = obj256 + 312;
			object obj258 = obj257 + num29;
			goto IL_154d;
			IL_28b5:
			InputActions wrapper43;
			Action<InputAction.CallbackContext> value41;
			wrapper43.m_Universal_CheatImpactF9.performed += value41;
			wrapper31 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7572 @ rax_v262+8]");
			value29 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num30 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_296f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj259 = 0;
			object obj260 = 0;
			while (true)
			{
				object obj261 = obj260 + obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7514 @ r8_v275+v7517 @ rax_v525*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj260++;
				object obj262 = obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj262 < 0)
				{
					continue;
				}
				goto IL_296f;
			}
			object obj263 = obj260 + obj260;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7514 @ r8_v275+8+v7575 @ rcx_v448*8]");
			object obj264 = (nint)0 + (nint)16;
			object obj265 = obj264 << 4;
			object obj266 = obj265 + 312;
			object obj267 = obj266 + num30;
			goto IL_2984;
			IL_223d:
			wrapper11.m_Universal_CinamaticLightSwitch.canceled += value9;
			wrapper28 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6812 @ rax_v222+8]");
			value26 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num31 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_22f7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj268 = 0;
			object obj269 = 0;
			while (true)
			{
				object obj270 = obj269 + obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6754 @ r8_v299+v6757 @ rax_v613*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj269++;
				object obj271 = obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj271 < 0)
				{
					continue;
				}
				goto IL_22f7;
			}
			object obj272 = obj269 + obj269;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6754 @ r8_v299+8+v6815 @ rcx_v496*8]");
			object obj273 = (nint)0 + (nint)14;
			object obj274 = obj273 << 4;
			object obj275 = obj274 + 312;
			object obj276 = obj275 + num31;
			goto IL_230c;
			IL_0779:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_078e;
			IL_078e:
			wrapper41.m_Universal_PointerPosition.canceled += value39;
			InputActions wrapper44 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3677 @ rax_v57+8]");
			Action<InputAction.CallbackContext> value42 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num32 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0848;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj277 = 0;
			object obj278 = 0;
			while (true)
			{
				object obj279 = obj278 + obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3619 @ r8_v398+v3622 @ rax_v976*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj278++;
				object obj280 = obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj280 < 0)
				{
					continue;
				}
				goto IL_0848;
			}
			object obj281 = obj278 + obj278;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3619 @ r8_v398+8+v3680 @ rcx_v694*8]");
			object obj282 = (nint)0 + (nint)3;
			object obj283 = obj282 << 4;
			object obj284 = obj283 + 312;
			object obj285 = obj284 + num32;
			goto IL_085d;
			IL_2f2d:
			wrapper14.m_Universal_RotateLeft.started += value12;
			wrapper32 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8332 @ rax_v302+8]");
			value30 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num33 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2fe7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj286 = 0;
			object obj287 = 0;
			while (true)
			{
				object obj288 = obj287 + obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8274 @ r8_v251+v8277 @ rax_v437*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj287++;
				object obj289 = obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj289 < 0)
				{
					continue;
				}
				goto IL_2fe7;
			}
			object obj290 = obj287 + obj287;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8274 @ r8_v251+8+v8335 @ rcx_v400*8]");
			object obj291 = (nint)0 + (nint)19;
			object obj292 = obj291 << 4;
			object obj293 = obj292 + 312;
			object obj294 = obj293 + num33;
			goto IL_2ffc;
			IL_23c6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_23db;
			IL_2d8f:
			InputActions wrapper45;
			Action<InputAction.CallbackContext> value43;
			wrapper45.m_Universal_CheatImpactF11.performed += value43;
			wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8142 @ rax_v292+8]");
			value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num34 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2e49;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj295 = 0;
			object obj296 = 0;
			while (true)
			{
				object obj297 = obj296 + obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8084 @ r8_v257+v8087 @ rax_v459*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj296++;
				object obj298 = obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj298 < 0)
				{
					continue;
				}
				goto IL_2e49;
			}
			object obj299 = obj296 + obj296;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8084 @ r8_v257+8+v8145 @ rcx_v412*8]");
			object obj300 = (nint)0 + (nint)18;
			object obj301 = obj300 << 4;
			object obj302 = obj301 + 312;
			object obj303 = obj302 + num34;
			goto IL_2e5e;
			IL_2579:
			wrapper7.m_Universal_CheatRevealallonmap.started += value5;
			wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7192 @ rax_v242+8]");
			value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num35 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2633;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj304 = 0;
			object obj305 = 0;
			while (true)
			{
				object obj306 = obj305 + obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7134 @ r8_v287+v7137 @ rax_v569*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj305++;
				object obj307 = obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj307 < 0)
				{
					continue;
				}
				goto IL_2633;
			}
			object obj308 = obj305 + obj305;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7134 @ r8_v287+8+v7195 @ rcx_v472*8]");
			object obj309 = (nint)0 + (nint)15;
			object obj310 = obj309 << 4;
			object obj311 = obj310 + 312;
			object obj312 = obj311 + num35;
			goto IL_2648;
			IL_0848:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_085d;
			IL_085d:
			wrapper44.m_Universal_PrimaryClick.started += value42;
			InputActions wrapper46 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3772 @ rax_v62+8]");
			Action<InputAction.CallbackContext> value44 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num36 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0917;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj313 = 0;
			object obj314 = 0;
			while (true)
			{
				object obj315 = obj314 + obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3714 @ r8_v395+v3717 @ rax_v965*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj314++;
				object obj316 = obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj316 < 0)
				{
					continue;
				}
				goto IL_0917;
			}
			object obj317 = obj314 + obj314;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3714 @ r8_v395+8+v3775 @ rcx_v688*8]");
			object obj318 = (nint)0 + (nint)3;
			object obj319 = obj318 << 4;
			object obj320 = obj319 + 312;
			object obj321 = obj320 + num36;
			goto IL_092c;
			IL_1538:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_154d;
			IL_154d:
			wrapper42.m_Universal_Escape.performed += value40;
			InputActions wrapper47 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5292 @ rax_v142+8]");
			Action<InputAction.CallbackContext> value45 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num37 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1607;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj322 = 0;
			object obj323 = 0;
			while (true)
			{
				object obj324 = obj323 + obj323;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5234 @ r8_v347+v5237 @ rax_v789*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj323++;
				object obj325 = obj323;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v311 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj325 < 0)
				{
					continue;
				}
				goto IL_1607;
			}
			object obj326 = obj323 + obj323;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5234 @ r8_v347+8+v5295 @ rcx_v592*8]");
			object obj327 = (nint)0 + (nint)8;
			object obj328 = obj327 << 4;
			object obj329 = obj328 + 312;
			object obj330 = obj329 + num37;
			goto IL_161c;
			IL_1bb0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1bc5;
			IL_3269:
			wrapper35.m_Universal_RotateRight.performed += value33;
			InputActions wrapper48 = m_Wrapper;
			IntPtr method = default(IntPtr);
			Action<InputAction.CallbackContext> value46 = new Action<InputAction.CallbackContext>(instance, method);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper48.m_Universal_RotateRight.canceled += value46;
			InputActions wrapper49 = m_Wrapper;
			IntPtr method2 = default(IntPtr);
			Action<InputAction.CallbackContext> value47 = new Action<InputAction.CallbackContext>(instance, method2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper49.m_Universal_Cinamatic4kScreenshot.started += value47;
			InputActions wrapper50 = m_Wrapper;
			IntPtr method3 = default(IntPtr);
			Action<InputAction.CallbackContext> value48 = new Action<InputAction.CallbackContext>(instance, method3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper50.m_Universal_Cinamatic4kScreenshot.performed += value48;
			InputActions wrapper51 = m_Wrapper;
			IntPtr method4 = default(IntPtr);
			Action<InputAction.CallbackContext> value49 = new Action<InputAction.CallbackContext>(instance, method4);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper51.m_Universal_Cinamatic4kScreenshot.canceled += value49;
			InputActions wrapper52 = m_Wrapper;
			IntPtr method5 = default(IntPtr);
			Action<InputAction.CallbackContext> value50 = new Action<InputAction.CallbackContext>(instance, method5);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper52.m_Universal_ContinueEnter.started += value50;
			InputActions wrapper53 = m_Wrapper;
			IntPtr method6 = default(IntPtr);
			Action<InputAction.CallbackContext> value51 = new Action<InputAction.CallbackContext>(instance, method6);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper53.m_Universal_ContinueEnter.performed += value51;
			InputActions wrapper54 = m_Wrapper;
			IntPtr method7 = default(IntPtr);
			Action<InputAction.CallbackContext> value52 = new Action<InputAction.CallbackContext>(instance, method7);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper54.m_Universal_ContinueEnter.canceled += value52;
			InputActions wrapper55 = m_Wrapper;
			IntPtr method8 = default(IntPtr);
			Action<InputAction.CallbackContext> value53 = new Action<InputAction.CallbackContext>(instance, method8);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper55.m_Universal_PickUp.started += value53;
			InputActions wrapper56 = m_Wrapper;
			IntPtr method9 = default(IntPtr);
			Action<InputAction.CallbackContext> value54 = new Action<InputAction.CallbackContext>(instance, method9);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper56.m_Universal_PickUp.performed += value54;
			InputActions wrapper57 = m_Wrapper;
			IntPtr method10 = default(IntPtr);
			Action<InputAction.CallbackContext> value55 = new Action<InputAction.CallbackContext>(instance, method10);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper57.m_Universal_PickUp.canceled += value55;
			InputActions wrapper58 = m_Wrapper;
			IntPtr method11 = default(IntPtr);
			Action<InputAction.CallbackContext> value56 = new Action<InputAction.CallbackContext>(instance, method11);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper58.m_Universal_Interact.started += value56;
			InputActions wrapper59 = m_Wrapper;
			IntPtr method12 = default(IntPtr);
			Action<InputAction.CallbackContext> value57 = new Action<InputAction.CallbackContext>(instance, method12);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper59.m_Universal_Interact.performed += value57;
			InputActions wrapper60 = m_Wrapper;
			IntPtr method13 = default(IntPtr);
			Action<InputAction.CallbackContext> value58 = new Action<InputAction.CallbackContext>(instance, method13);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper60.m_Universal_Interact.canceled += value58;
			InputActions wrapper61 = m_Wrapper;
			IntPtr method14 = default(IntPtr);
			Action<InputAction.CallbackContext> value59 = new Action<InputAction.CallbackContext>(instance, method14);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper61.m_Universal_SlowCursor.started += value59;
			InputActions wrapper62 = m_Wrapper;
			IntPtr method15 = default(IntPtr);
			Action<InputAction.CallbackContext> value60 = new Action<InputAction.CallbackContext>(instance, method15);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper62.m_Universal_SlowCursor.performed += value60;
			InputActions wrapper63 = m_Wrapper;
			IntPtr method16 = default(IntPtr);
			Action<InputAction.CallbackContext> value61 = new Action<InputAction.CallbackContext>(instance, method16);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper63.m_Universal_SlowCursor.canceled += value61;
			return;
			IL_0917:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_092c;
			IL_092c:
			wrapper46.m_Universal_PrimaryClick.performed += value44;
			InputActions wrapper64 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3867 @ rax_v67+8]");
			Action<InputAction.CallbackContext> value62 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num38 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_09e6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj331 = 0;
			object obj332 = 0;
			while (true)
			{
				object obj333 = obj332 + obj332;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3809 @ r8_v392+v3812 @ rax_v954*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj332++;
				object obj334 = obj332;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj334 < 0)
				{
					continue;
				}
				goto IL_09e6;
			}
			object obj335 = obj332 + obj332;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3809 @ r8_v392+8+v3870 @ rcx_v682*8]");
			object obj336 = (nint)0 + (nint)3;
			object obj337 = obj336 << 4;
			object obj338 = obj337 + 312;
			object obj339 = obj338 + num38;
			goto IL_09fb;
			IL_1bc5:
			wrapper39.m_Universal_CinamaticHideCursorToggle.started += value37;
			InputActions wrapper65 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6052 @ rax_v182+8]");
			Action<InputAction.CallbackContext> value63 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num39 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1c7f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj340 = 0;
			object obj341 = 0;
			while (true)
			{
				object obj342 = obj341 + obj341;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5994 @ r8_v323+v5997 @ rax_v701*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj341++;
				object obj343 = obj341;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v319 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj343 < 0)
				{
					continue;
				}
				goto IL_1c7f;
			}
			object obj344 = obj341 + obj341;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5994 @ r8_v323+8+v6055 @ rcx_v544*8]");
			object obj345 = (nint)0 + (nint)11;
			object obj346 = obj345 << 4;
			object obj347 = obj346 + 312;
			object obj348 = obj347 + num39;
			goto IL_1c94;
			IL_1eec:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1f01;
			IL_27d1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_27e6;
			IL_3254:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_3269;
			IL_09e6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_09fb;
			IL_09fb:
			wrapper64.m_Universal_PrimaryClick.canceled += value62;
			InputActions wrapper66 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3962 @ rax_v72+8]");
			Action<InputAction.CallbackContext> value64 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num40 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ab5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj349 = 0;
			object obj350 = 0;
			while (true)
			{
				object obj351 = obj350 + obj350;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3904 @ r8_v389+v3907 @ rax_v943*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj350++;
				object obj352 = obj350;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj352 < 0)
				{
					continue;
				}
				goto IL_0ab5;
			}
			object obj353 = obj350 + obj350;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3904 @ r8_v389+8+v3965 @ rcx_v676*8]");
			object obj354 = (nint)0 + (nint)4;
			object obj355 = obj354 << 4;
			object obj356 = obj355 + 312;
			object obj357 = obj356 + num40;
			goto IL_0aca;
			IL_1607:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_161c;
			IL_161c:
			wrapper47.m_Universal_Escape.canceled += value45;
			InputActions wrapper67 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5387 @ rax_v147+8]");
			Action<InputAction.CallbackContext> value65 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num41 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_16d6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj358 = 0;
			object obj359 = 0;
			while (true)
			{
				object obj360 = obj359 + obj359;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5329 @ r8_v344+v5332 @ rax_v778*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj359++;
				object obj361 = obj359;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj361 < 0)
				{
					continue;
				}
				goto IL_16d6;
			}
			object obj362 = obj359 + obj359;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5329 @ r8_v344+8+v5390 @ rcx_v586*8]");
			object obj363 = (nint)0 + (nint)9;
			object obj364 = obj363 << 4;
			object obj365 = obj364 + 312;
			object obj366 = obj365 + num41;
			goto IL_16eb;
			IL_1f01:
			wrapper27.m_Universal_CinamaticAutoReload.performed += value25;
			wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6432 @ rax_v202+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num42 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1fbb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj367 = 0;
			object obj368 = 0;
			while (true)
			{
				object obj369 = obj368 + obj368;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6374 @ r8_v311+v6377 @ rax_v657*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj368++;
				object obj370 = obj368;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj370 < 0)
				{
					continue;
				}
				goto IL_1fbb;
			}
			object obj371 = obj368 + obj368;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6374 @ r8_v311+8+v6435 @ rcx_v520*8]");
			object obj372 = (nint)0 + (nint)12;
			object obj373 = obj372 << 4;
			object obj374 = obj373 + 312;
			object obj375 = obj374 + num42;
			goto IL_1fd0;
			IL_2bdc:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2bf1;
			IL_0ab5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0aca;
			IL_0aca:
			wrapper66.m_Universal_SecondaryClick.started += value64;
			InputActions wrapper68 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4057 @ rax_v77+8]");
			Action<InputAction.CallbackContext> value66 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num43 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b84;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj376 = 0;
			object obj377 = 0;
			while (true)
			{
				object obj378 = obj377 + obj377;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3999 @ r8_v386+v4002 @ rax_v932*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj377++;
				object obj379 = obj377;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj379 < 0)
				{
					continue;
				}
				goto IL_0b84;
			}
			object obj380 = obj377 + obj377;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3999 @ r8_v386+8+v4060 @ rcx_v670*8]");
			object obj381 = (nint)0 + (nint)4;
			object obj382 = obj381 << 4;
			object obj383 = obj382 + 312;
			object obj384 = obj383 + num43;
			goto IL_0b99;
			IL_208a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_209f;
			IL_30cb:
			wrapper33.m_Universal_RotateLeft.canceled += value31;
			wrapper34 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8522 @ rax_v312+8]");
			value32 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num44 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ r10_v64 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_3185;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ r10_v64 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj385 = 0;
			object obj386 = 0;
			while (true)
			{
				object obj387 = obj386 + obj386;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8464 @ r8_v245+v8467 @ rax_v415*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj386++;
				object obj388 = obj386;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ r10_v64 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj388 < 0)
				{
					continue;
				}
				goto IL_3185;
			}
			object obj389 = obj386 + obj386;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8464 @ r8_v245+8+v8525 @ rcx_v388*8]");
			object obj390 = (nint)0 + (nint)20;
			object obj391 = obj390 << 4;
			object obj392 = obj391 + 312;
			object obj393 = obj392 + num44;
			goto IL_319a;
			IL_27e6:
			InputActions wrapper69;
			Action<InputAction.CallbackContext> value67;
			wrapper69.m_Universal_CheatImpactF9.started += value67;
			wrapper43 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7477 @ rax_v257+8]");
			value41 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num45 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_28a0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj394 = 0;
			object obj395 = 0;
			while (true)
			{
				object obj396 = obj395 + obj395;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7419 @ r8_v278+v7422 @ rax_v536*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj395++;
				object obj397 = obj395;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj397 < 0)
				{
					continue;
				}
				goto IL_28a0;
			}
			object obj398 = obj395 + obj395;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7419 @ r8_v278+8+v7480 @ rcx_v454*8]");
			object obj399 = (nint)0 + (nint)16;
			object obj400 = obj399 << 4;
			object obj401 = obj400 + 312;
			object obj402 = obj401 + num45;
			goto IL_28b5;
			IL_2bf1:
			wrapper40.m_Universal_CheatImpactF10.canceled += value38;
			InputActions wrapper70 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7952 @ rax_v282+8]");
			Action<InputAction.CallbackContext> value68 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num46 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2cab;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj403 = 0;
			object obj404 = 0;
			while (true)
			{
				object obj405 = obj404 + obj404;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7894 @ r8_v263+v7897 @ rax_v481*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj404++;
				object obj406 = obj404;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj406 < 0)
				{
					continue;
				}
				goto IL_2cab;
			}
			object obj407 = obj404 + obj404;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7894 @ r8_v263+8+v7955 @ rcx_v424*8]");
			object obj408 = (nint)0 + (nint)18;
			object obj409 = obj408 << 4;
			object obj410 = obj409 + 312;
			object obj411 = obj410 + num46;
			goto IL_2cc0;
			IL_0b84:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b99;
			IL_0b99:
			wrapper68.m_Universal_SecondaryClick.performed += value66;
			InputActions wrapper71 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4152 @ rax_v82+8]");
			Action<InputAction.CallbackContext> value69 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num47 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0c53;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj412 = 0;
			object obj413 = 0;
			while (true)
			{
				object obj414 = obj413 + obj413;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4094 @ r8_v383+v4097 @ rax_v921*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj413++;
				object obj415 = obj413;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj415 < 0)
				{
					continue;
				}
				goto IL_0c53;
			}
			object obj416 = obj413 + obj413;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4094 @ r8_v383+8+v4155 @ rcx_v664*8]");
			object obj417 = (nint)0 + (nint)4;
			object obj418 = obj417 << 4;
			object obj419 = obj418 + 312;
			object obj420 = obj419 + num47;
			goto IL_0c68;
			IL_16d6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_16eb;
			IL_16eb:
			wrapper67.m_Universal_FreecamScrollWheel.started += value65;
			InputActions wrapper72 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5482 @ rax_v152+8]");
			Action<InputAction.CallbackContext> value70 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num48 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_17a5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj421 = 0;
			object obj422 = 0;
			while (true)
			{
				object obj423 = obj422 + obj422;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5424 @ r8_v341+v5427 @ rax_v767*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj422++;
				object obj424 = obj422;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v313 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj424 < 0)
				{
					continue;
				}
				goto IL_17a5;
			}
			object obj425 = obj422 + obj422;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5424 @ r8_v341+8+v5485 @ rcx_v580*8]");
			object obj426 = (nint)0 + (nint)9;
			object obj427 = obj426 << 4;
			object obj428 = obj427 + 312;
			object obj429 = obj428 + num48;
			goto IL_17ba;
			IL_1c7f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1c94;
			IL_2702:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2717;
			IL_0c53:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c68;
			IL_0c68:
			wrapper71.m_Universal_SecondaryClick.canceled += value69;
			InputActions wrapper73 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4247 @ rax_v87+8]");
			Action<InputAction.CallbackContext> value71 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num49 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d22;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj430 = 0;
			object obj431 = 0;
			while (true)
			{
				object obj432 = obj431 + obj431;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4189 @ r8_v380+v4192 @ rax_v910*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj431++;
				object obj433 = obj431;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v300 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj433 < 0)
				{
					continue;
				}
				goto IL_0d22;
			}
			object obj434 = obj431 + obj431;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4189 @ r8_v380+8+v4250 @ rcx_v658*8]");
			object obj435 = (nint)0 + (nint)5;
			object obj436 = obj435 << 4;
			object obj437 = obj436 + 312;
			object obj438 = obj437 + num49;
			goto IL_0d37;
			IL_1c94:
			wrapper65.m_Universal_CinamaticHideCursorToggle.performed += value63;
			InputActions wrapper74 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6147 @ rax_v187+8]");
			Action<InputAction.CallbackContext> value72 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num50 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1d4e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj439 = 0;
			object obj440 = 0;
			while (true)
			{
				object obj441 = obj440 + obj440;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6089 @ r8_v320+v6092 @ rax_v690*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj440++;
				object obj442 = obj440;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj442 < 0)
				{
					continue;
				}
				goto IL_1d4e;
			}
			object obj443 = obj440 + obj440;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6089 @ r8_v320+8+v6150 @ rcx_v538*8]");
			object obj444 = (nint)0 + (nint)11;
			object obj445 = obj444 << 4;
			object obj446 = obj445 + 312;
			object obj447 = obj446 + num50;
			goto IL_1d63;
			IL_209f:
			wrapper5.m_Universal_CinamaticLightSwitch.started += value3;
			wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6622 @ rax_v212+8]");
			value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num51 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2159;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj448 = 0;
			object obj449 = 0;
			while (true)
			{
				object obj450 = obj449 + obj449;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6564 @ r8_v305+v6567 @ rax_v635*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj449++;
				object obj451 = obj449;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj451 < 0)
				{
					continue;
				}
				goto IL_2159;
			}
			object obj452 = obj449 + obj449;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6564 @ r8_v305+8+v6625 @ rcx_v508*8]");
			object obj453 = (nint)0 + (nint)13;
			object obj454 = obj453 << 4;
			object obj455 = obj454 + 312;
			object obj456 = obj455 + num51;
			goto IL_216e;
			IL_2fe7:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2ffc;
			IL_2717:
			wrapper23.m_Universal_CheatRevealallonmap.canceled += value21;
			wrapper69 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7382 @ rax_v252+8]");
			value67 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num52 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_27d1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj457 = 0;
			object obj458 = 0;
			while (true)
			{
				object obj459 = obj458 + obj458;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7324 @ r8_v281+v7327 @ rax_v547*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj458++;
				object obj460 = obj458;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj460 < 0)
				{
					continue;
				}
				goto IL_27d1;
			}
			object obj461 = obj458 + obj458;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7324 @ r8_v281+8+v7385 @ rcx_v460*8]");
			object obj462 = (nint)0 + (nint)16;
			object obj463 = obj462 << 4;
			object obj464 = obj463 + 312;
			object obj465 = obj464 + num52;
			goto IL_27e6;
			IL_0d22:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0d37;
			IL_0d37:
			wrapper73.m_Universal_Tertiaryclick.started += value71;
			InputActions wrapper75 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4342 @ rax_v92+8]");
			Action<InputAction.CallbackContext> value73 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num53 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0df1;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj466 = 0;
			object obj467 = 0;
			while (true)
			{
				object obj468 = obj467 + obj467;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4284 @ r8_v377+v4287 @ rax_v899*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj467++;
				object obj469 = obj467;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj469 < 0)
				{
					continue;
				}
				goto IL_0df1;
			}
			object obj470 = obj467 + obj467;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4284 @ r8_v377+8+v4345 @ rcx_v652*8]");
			object obj471 = (nint)0 + (nint)5;
			object obj472 = obj471 << 4;
			object obj473 = obj472 + 312;
			object obj474 = obj473 + num53;
			goto IL_0e06;
			IL_17a5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_17ba;
			IL_17ba:
			wrapper72.m_Universal_FreecamScrollWheel.performed += value70;
			InputActions wrapper76 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5577 @ rax_v157+8]");
			Action<InputAction.CallbackContext> value74 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num54 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1874;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj475 = 0;
			object obj476 = 0;
			while (true)
			{
				object obj477 = obj476 + obj476;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5519 @ r8_v338+v5522 @ rax_v756*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj476++;
				object obj478 = obj476;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v314 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj478 < 0)
				{
					continue;
				}
				goto IL_1874;
			}
			object obj479 = obj476 + obj476;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5519 @ r8_v338+8+v5580 @ rcx_v574*8]");
			object obj480 = (nint)0 + (nint)9;
			object obj481 = obj480 << 4;
			object obj482 = obj481 + 312;
			object obj483 = obj482 + num54;
			goto IL_1889;
			IL_2159:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_216e;
			IL_2495:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_24aa;
			IL_0df1:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e06;
			IL_0e06:
			wrapper75.m_Universal_Tertiaryclick.performed += value73;
			InputActions wrapper77 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4437 @ rax_v97+8]");
			Action<InputAction.CallbackContext> value75 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num55 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v302 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0ec0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v302 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj484 = 0;
			object obj485 = 0;
			while (true)
			{
				object obj486 = obj485 + obj485;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4379 @ r8_v374+v4382 @ rax_v888*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj485++;
				object obj487 = obj485;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v302 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj487 < 0)
				{
					continue;
				}
				goto IL_0ec0;
			}
			object obj488 = obj485 + obj485;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4379 @ r8_v374+8+v4440 @ rcx_v646*8]");
			object obj489 = (nint)0 + (nint)5;
			object obj490 = obj489 << 4;
			object obj491 = obj490 + 312;
			object obj492 = obj491 + num55;
			goto IL_0ed5;
			IL_28a0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_28b5;
			IL_296f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2984;
			IL_22f7:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_230c;
			IL_3185:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_319a;
			IL_0ec0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0ed5;
			IL_0ed5:
			wrapper77.m_Universal_Tertiaryclick.canceled += value75;
			InputActions wrapper78 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4532 @ rax_v102+8]");
			Action<InputAction.CallbackContext> value76 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num56 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f8f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj493 = 0;
			object obj494 = 0;
			while (true)
			{
				object obj495 = obj494 + obj494;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4474 @ r8_v371+v4477 @ rax_v877*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj494++;
				object obj496 = obj494;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj496 < 0)
				{
					continue;
				}
				goto IL_0f8f;
			}
			object obj497 = obj494 + obj494;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4474 @ r8_v371+8+v4535 @ rcx_v640*8]");
			object obj498 = (nint)0 + (nint)6;
			object obj499 = obj498 << 4;
			object obj500 = obj499 + 312;
			object obj501 = obj500 + num56;
			goto IL_0fa4;
			IL_1874:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1889;
			IL_1889:
			wrapper76.m_Universal_FreecamScrollWheel.canceled += value74;
			InputActions wrapper79 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5672 @ rax_v162+8]");
			Action<InputAction.CallbackContext> value77 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num57 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1943;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj502 = 0;
			object obj503 = 0;
			while (true)
			{
				object obj504 = obj503 + obj503;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5614 @ r8_v335+v5617 @ rax_v745*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj503++;
				object obj505 = obj503;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v315 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj505 < 0)
				{
					continue;
				}
				goto IL_1943;
			}
			object obj506 = obj503 + obj503;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5614 @ r8_v335+8+v5675 @ rcx_v568*8]");
			object obj507 = (nint)0 + (nint)10;
			object obj508 = obj507 << 4;
			object obj509 = obj508 + 312;
			object obj510 = obj509 + num57;
			goto IL_1958;
			IL_1d4e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1d63;
			IL_2cab:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2cc0;
			IL_0f8f:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0fa4;
			IL_0fa4:
			wrapper78.m_Universal_ToggleClipboard.started += value76;
			InputActions wrapper80 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4627 @ rax_v107+8]");
			Action<InputAction.CallbackContext> value78 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num58 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_105e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj511 = 0;
			object obj512 = 0;
			while (true)
			{
				object obj513 = obj512 + obj512;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4569 @ r8_v368+v4572 @ rax_v866*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj512++;
				object obj514 = obj512;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj514 < 0)
				{
					continue;
				}
				goto IL_105e;
			}
			object obj515 = obj512 + obj512;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4569 @ r8_v368+8+v4630 @ rcx_v634*8]");
			object obj516 = (nint)0 + (nint)6;
			object obj517 = obj516 << 4;
			object obj518 = obj517 + 312;
			object obj519 = obj518 + num58;
			goto IL_1073;
			IL_1d63:
			wrapper74.m_Universal_CinamaticHideCursorToggle.canceled += value72;
			wrapper26 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6242 @ rax_v192+8]");
			value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num59 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1e1d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj520 = 0;
			object obj521 = 0;
			while (true)
			{
				object obj522 = obj521 + obj521;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6184 @ r8_v317+v6187 @ rax_v679*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj521++;
				object obj523 = obj521;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v321 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj523 < 0)
				{
					continue;
				}
				goto IL_1e1d;
			}
			object obj524 = obj521 + obj521;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6184 @ r8_v317+8+v6245 @ rcx_v532*8]");
			object obj525 = (nint)0 + (nint)12;
			object obj526 = obj525 << 4;
			object obj527 = obj526 + 312;
			object obj528 = obj527 + num59;
			goto IL_1e32;
			IL_1fbb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1fd0;
			IL_2e49:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2e5e;
			IL_2cc0:
			wrapper70.m_Universal_CheatImpactF11.started += value68;
			wrapper45 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8047 @ rax_v287+8]");
			value43 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num60 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2d7a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj529 = 0;
			object obj530 = 0;
			while (true)
			{
				object obj531 = obj530 + obj530;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7989 @ r8_v260+v7992 @ rax_v470*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj530++;
				object obj532 = obj530;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj532 < 0)
				{
					continue;
				}
				goto IL_2d7a;
			}
			object obj533 = obj530 + obj530;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7989 @ r8_v260+8+v8050 @ rcx_v418*8]");
			object obj534 = (nint)0 + (nint)18;
			object obj535 = obj534 << 4;
			object obj536 = obj535 + 312;
			object obj537 = obj536 + num60;
			goto IL_2d8f;
			IL_105e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1073;
			IL_1073:
			wrapper80.m_Universal_ToggleClipboard.performed += value78;
			wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4722 @ rax_v112+8]");
			value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num61 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_112d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj538 = 0;
			object obj539 = 0;
			while (true)
			{
				object obj540 = obj539 + obj539;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4664 @ r8_v365+v4667 @ rax_v855*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj539++;
				object obj541 = obj539;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj541 < 0)
				{
					continue;
				}
				goto IL_112d;
			}
			object obj542 = obj539 + obj539;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4664 @ r8_v365+8+v4725 @ rcx_v628*8]");
			object obj543 = (nint)0 + (nint)6;
			object obj544 = obj543 << 4;
			object obj545 = obj544 + 312;
			object obj546 = obj545 + num61;
			goto IL_1142;
			IL_1943:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1958;
			IL_1958:
			wrapper79.m_Universal_UnequipGasmask.started += value77;
			wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5767 @ rax_v167+8]");
			value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num62 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1a12;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj547 = 0;
			object obj548 = 0;
			while (true)
			{
				object obj549 = obj548 + obj548;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5709 @ r8_v332+v5712 @ rax_v734*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj548++;
				object obj550 = obj548;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj550 < 0)
				{
					continue;
				}
				goto IL_1a12;
			}
			object obj551 = obj548 + obj548;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5709 @ r8_v332+8+v5770 @ rcx_v562*8]");
			object obj552 = (nint)0 + (nint)10;
			object obj553 = obj552 << 4;
			object obj554 = obj553 + 312;
			object obj555 = obj554 + num62;
			goto IL_1a27;
		}

		private void UnregisterCallbacks(IUniversalActions instance)
		{
			//IL_002b: Expected I, but got O
			//IL_0063: Expected O, but got I
			//IL_006c: Expected O, but got I4
			//IL_00fa: Expected I, but got O
			//IL_3739: Expected O, but got I
			//IL_3742: Unknown result type (might be due to invalid IL or missing references)
			//IL_3747: Expected O, but got Unknown
			//IL_374f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3754: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_0132: Expected O, but got I
			//IL_013b: Expected O, but got I4
			//IL_01c9: Expected I, but got O
			//IL_377c: Expected O, but got I
			//IL_3785: Unknown result type (might be due to invalid IL or missing references)
			//IL_378a: Expected O, but got Unknown
			//IL_3792: Unknown result type (might be due to invalid IL or missing references)
			//IL_3797: Expected O, but got Unknown
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Expected O, but got Unknown
			//IL_0201: Expected O, but got I
			//IL_020a: Expected O, but got I4
			//IL_0298: Expected I, but got O
			//IL_37bf: Expected O, but got I
			//IL_37c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_37cd: Expected O, but got Unknown
			//IL_37d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_37da: Expected O, but got Unknown
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Expected O, but got Unknown
			//IL_02d0: Expected O, but got I
			//IL_02d9: Expected O, but got I4
			//IL_0367: Expected I, but got O
			//IL_3802: Expected O, but got I
			//IL_3819: Unknown result type (might be due to invalid IL or missing references)
			//IL_381e: Expected O, but got Unknown
			//IL_3826: Unknown result type (might be due to invalid IL or missing references)
			//IL_382b: Expected O, but got Unknown
			//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ec: Expected O, but got Unknown
			//IL_039f: Expected O, but got I
			//IL_03a8: Expected O, but got I4
			//IL_0436: Expected I, but got O
			//IL_3853: Expected O, but got I
			//IL_386a: Unknown result type (might be due to invalid IL or missing references)
			//IL_386f: Expected O, but got Unknown
			//IL_3877: Unknown result type (might be due to invalid IL or missing references)
			//IL_387c: Expected O, but got Unknown
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03bb: Expected O, but got Unknown
			//IL_046e: Expected O, but got I
			//IL_0477: Expected O, but got I4
			//IL_0505: Expected I, but got O
			//IL_38a4: Expected O, but got I
			//IL_38bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_38c0: Expected O, but got Unknown
			//IL_38c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_38cd: Expected O, but got Unknown
			//IL_0485: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Expected O, but got Unknown
			//IL_053d: Expected O, but got I
			//IL_0546: Expected O, but got I4
			//IL_05d4: Expected I, but got O
			//IL_38f5: Expected O, but got I
			//IL_390c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3911: Expected O, but got Unknown
			//IL_3919: Unknown result type (might be due to invalid IL or missing references)
			//IL_391e: Expected O, but got Unknown
			//IL_0554: Unknown result type (might be due to invalid IL or missing references)
			//IL_0559: Expected O, but got Unknown
			//IL_060c: Expected O, but got I
			//IL_0615: Expected O, but got I4
			//IL_06a3: Expected I, but got O
			//IL_3946: Expected O, but got I
			//IL_395d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3962: Expected O, but got Unknown
			//IL_396a: Unknown result type (might be due to invalid IL or missing references)
			//IL_396f: Expected O, but got Unknown
			//IL_0623: Unknown result type (might be due to invalid IL or missing references)
			//IL_0628: Expected O, but got Unknown
			//IL_06db: Expected O, but got I
			//IL_06e4: Expected O, but got I4
			//IL_0772: Expected I, but got O
			//IL_3997: Expected O, but got I
			//IL_39ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_39b3: Expected O, but got Unknown
			//IL_39bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_39c0: Expected O, but got Unknown
			//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f7: Expected O, but got Unknown
			//IL_07aa: Expected O, but got I
			//IL_07b3: Expected O, but got I4
			//IL_0841: Expected I, but got O
			//IL_39e8: Expected O, but got I
			//IL_39ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a04: Expected O, but got Unknown
			//IL_3a0c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a11: Expected O, but got Unknown
			//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c6: Expected O, but got Unknown
			//IL_0879: Expected O, but got I
			//IL_0882: Expected O, but got I4
			//IL_0910: Expected I, but got O
			//IL_3a39: Expected O, but got I
			//IL_3a50: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a55: Expected O, but got Unknown
			//IL_3a5d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a62: Expected O, but got Unknown
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_0895: Expected O, but got Unknown
			//IL_0948: Expected O, but got I
			//IL_0951: Expected O, but got I4
			//IL_09df: Expected I, but got O
			//IL_3a8a: Expected O, but got I
			//IL_3aa1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3aa6: Expected O, but got Unknown
			//IL_3aae: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ab3: Expected O, but got Unknown
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0964: Expected O, but got Unknown
			//IL_0a17: Expected O, but got I
			//IL_0a20: Expected O, but got I4
			//IL_0aae: Expected I, but got O
			//IL_3adb: Expected O, but got I
			//IL_3af2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3af7: Expected O, but got Unknown
			//IL_3aff: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b04: Expected O, but got Unknown
			//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a33: Expected O, but got Unknown
			//IL_0ae6: Expected O, but got I
			//IL_0aef: Expected O, but got I4
			//IL_0b7d: Expected I, but got O
			//IL_3b2c: Expected O, but got I
			//IL_3b43: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b48: Expected O, but got Unknown
			//IL_3b50: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b55: Expected O, but got Unknown
			//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b02: Expected O, but got Unknown
			//IL_0bb5: Expected O, but got I
			//IL_0bbe: Expected O, but got I4
			//IL_0c4c: Expected I, but got O
			//IL_3b7d: Expected O, but got I
			//IL_3b94: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b99: Expected O, but got Unknown
			//IL_3ba1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ba6: Expected O, but got Unknown
			//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0c84: Expected O, but got I
			//IL_0c8d: Expected O, but got I4
			//IL_0d1b: Expected I, but got O
			//IL_3bce: Expected O, but got I
			//IL_3be5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bea: Expected O, but got Unknown
			//IL_3bf2: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bf7: Expected O, but got Unknown
			//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ca0: Expected O, but got Unknown
			//IL_0d53: Expected O, but got I
			//IL_0d5c: Expected O, but got I4
			//IL_0dea: Expected I, but got O
			//IL_3c1f: Expected O, but got I
			//IL_3c36: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c3b: Expected O, but got Unknown
			//IL_3c43: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c48: Expected O, but got Unknown
			//IL_0d6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d6f: Expected O, but got Unknown
			//IL_0e22: Expected O, but got I
			//IL_0e2b: Expected O, but got I4
			//IL_0eb9: Expected I, but got O
			//IL_3c70: Expected O, but got I
			//IL_3c87: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c8c: Expected O, but got Unknown
			//IL_3c94: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c99: Expected O, but got Unknown
			//IL_0e39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e3e: Expected O, but got Unknown
			//IL_0ef1: Expected O, but got I
			//IL_0efa: Expected O, but got I4
			//IL_0f88: Expected I, but got O
			//IL_3cc1: Expected O, but got I
			//IL_3cd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cdd: Expected O, but got Unknown
			//IL_3ce5: Unknown result type (might be due to invalid IL or missing references)
			//IL_3cea: Expected O, but got Unknown
			//IL_0f08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f0d: Expected O, but got Unknown
			//IL_0fc0: Expected O, but got I
			//IL_0fc9: Expected O, but got I4
			//IL_1057: Expected I, but got O
			//IL_3d12: Expected O, but got I
			//IL_3d29: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d2e: Expected O, but got Unknown
			//IL_3d36: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d3b: Expected O, but got Unknown
			//IL_0fd7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0fdc: Expected O, but got Unknown
			//IL_108f: Expected O, but got I
			//IL_1098: Expected O, but got I4
			//IL_1126: Expected I, but got O
			//IL_3d63: Expected O, but got I
			//IL_3d7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d7f: Expected O, but got Unknown
			//IL_3d87: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d8c: Expected O, but got Unknown
			//IL_10a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_10ab: Expected O, but got Unknown
			//IL_115e: Expected O, but got I
			//IL_1167: Expected O, but got I4
			//IL_11f5: Expected I, but got O
			//IL_3db4: Expected O, but got I
			//IL_3dcb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dd0: Expected O, but got Unknown
			//IL_3dd8: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ddd: Expected O, but got Unknown
			//IL_1175: Unknown result type (might be due to invalid IL or missing references)
			//IL_117a: Expected O, but got Unknown
			//IL_122d: Expected O, but got I
			//IL_1236: Expected O, but got I4
			//IL_12c4: Expected I, but got O
			//IL_3e05: Expected O, but got I
			//IL_3e1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e21: Expected O, but got Unknown
			//IL_3e29: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e2e: Expected O, but got Unknown
			//IL_1244: Unknown result type (might be due to invalid IL or missing references)
			//IL_1249: Expected O, but got Unknown
			//IL_12fc: Expected O, but got I
			//IL_1305: Expected O, but got I4
			//IL_1393: Expected I, but got O
			//IL_3e56: Expected O, but got I
			//IL_3e6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e72: Expected O, but got Unknown
			//IL_3e7a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e7f: Expected O, but got Unknown
			//IL_1313: Unknown result type (might be due to invalid IL or missing references)
			//IL_1318: Expected O, but got Unknown
			//IL_13cb: Expected O, but got I
			//IL_13d4: Expected O, but got I4
			//IL_1462: Expected I, but got O
			//IL_3ea7: Expected O, but got I
			//IL_3ebe: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ec3: Expected O, but got Unknown
			//IL_3ecb: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ed0: Expected O, but got Unknown
			//IL_13e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_13e7: Expected O, but got Unknown
			//IL_149a: Expected O, but got I
			//IL_14a3: Expected O, but got I4
			//IL_1531: Expected I, but got O
			//IL_3ef8: Expected O, but got I
			//IL_3f0f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f14: Expected O, but got Unknown
			//IL_3f1c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f21: Expected O, but got Unknown
			//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_14b6: Expected O, but got Unknown
			//IL_1569: Expected O, but got I
			//IL_1572: Expected O, but got I4
			//IL_1600: Expected I, but got O
			//IL_3f49: Expected O, but got I
			//IL_3f60: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f65: Expected O, but got Unknown
			//IL_3f6d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f72: Expected O, but got Unknown
			//IL_1580: Unknown result type (might be due to invalid IL or missing references)
			//IL_1585: Expected O, but got Unknown
			//IL_1638: Expected O, but got I
			//IL_1641: Expected O, but got I4
			//IL_16cf: Expected I, but got O
			//IL_3f9a: Expected O, but got I
			//IL_3fb1: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fb6: Expected O, but got Unknown
			//IL_3fbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fc3: Expected O, but got Unknown
			//IL_164f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1654: Expected O, but got Unknown
			//IL_1707: Expected O, but got I
			//IL_1710: Expected O, but got I4
			//IL_179e: Expected I, but got O
			//IL_3feb: Expected O, but got I
			//IL_4002: Unknown result type (might be due to invalid IL or missing references)
			//IL_4007: Expected O, but got Unknown
			//IL_400f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4014: Expected O, but got Unknown
			//IL_171e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1723: Expected O, but got Unknown
			//IL_17d6: Expected O, but got I
			//IL_17df: Expected O, but got I4
			//IL_186d: Expected I, but got O
			//IL_403c: Expected O, but got I
			//IL_4053: Unknown result type (might be due to invalid IL or missing references)
			//IL_4058: Expected O, but got Unknown
			//IL_4060: Unknown result type (might be due to invalid IL or missing references)
			//IL_4065: Expected O, but got Unknown
			//IL_17ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_17f2: Expected O, but got Unknown
			//IL_18a5: Expected O, but got I
			//IL_18ae: Expected O, but got I4
			//IL_193c: Expected I, but got O
			//IL_408d: Expected O, but got I
			//IL_40a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_40a9: Expected O, but got Unknown
			//IL_40b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_40b6: Expected O, but got Unknown
			//IL_18bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_18c1: Expected O, but got Unknown
			//IL_1974: Expected O, but got I
			//IL_197d: Expected O, but got I4
			//IL_1a0b: Expected I, but got O
			//IL_40de: Expected O, but got I
			//IL_40f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_40fa: Expected O, but got Unknown
			//IL_4102: Unknown result type (might be due to invalid IL or missing references)
			//IL_4107: Expected O, but got Unknown
			//IL_198b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1990: Expected O, but got Unknown
			//IL_1a43: Expected O, but got I
			//IL_1a4c: Expected O, but got I4
			//IL_1ada: Expected I, but got O
			//IL_412f: Expected O, but got I
			//IL_4146: Unknown result type (might be due to invalid IL or missing references)
			//IL_414b: Expected O, but got Unknown
			//IL_4153: Unknown result type (might be due to invalid IL or missing references)
			//IL_4158: Expected O, but got Unknown
			//IL_1a5a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a5f: Expected O, but got Unknown
			//IL_1b12: Expected O, but got I
			//IL_1b1b: Expected O, but got I4
			//IL_1ba9: Expected I, but got O
			//IL_4180: Expected O, but got I
			//IL_4197: Unknown result type (might be due to invalid IL or missing references)
			//IL_419c: Expected O, but got Unknown
			//IL_41a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_41a9: Expected O, but got Unknown
			//IL_1b29: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b2e: Expected O, but got Unknown
			//IL_1be1: Expected O, but got I
			//IL_1bea: Expected O, but got I4
			//IL_1c78: Expected I, but got O
			//IL_41d1: Expected O, but got I
			//IL_41e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_41ed: Expected O, but got Unknown
			//IL_41f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_41fa: Expected O, but got Unknown
			//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bfd: Expected O, but got Unknown
			//IL_1cb0: Expected O, but got I
			//IL_1cb9: Expected O, but got I4
			//IL_1d47: Expected I, but got O
			//IL_4222: Expected O, but got I
			//IL_4239: Unknown result type (might be due to invalid IL or missing references)
			//IL_423e: Expected O, but got Unknown
			//IL_4246: Unknown result type (might be due to invalid IL or missing references)
			//IL_424b: Expected O, but got Unknown
			//IL_1cc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ccc: Expected O, but got Unknown
			//IL_1d7f: Expected O, but got I
			//IL_1d88: Expected O, but got I4
			//IL_1e16: Expected I, but got O
			//IL_4273: Expected O, but got I
			//IL_428a: Unknown result type (might be due to invalid IL or missing references)
			//IL_428f: Expected O, but got Unknown
			//IL_4297: Unknown result type (might be due to invalid IL or missing references)
			//IL_429c: Expected O, but got Unknown
			//IL_1d96: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d9b: Expected O, but got Unknown
			//IL_1e4e: Expected O, but got I
			//IL_1e57: Expected O, but got I4
			//IL_1ee5: Expected I, but got O
			//IL_42c4: Expected O, but got I
			//IL_42db: Unknown result type (might be due to invalid IL or missing references)
			//IL_42e0: Expected O, but got Unknown
			//IL_42e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_42ed: Expected O, but got Unknown
			//IL_1e65: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e6a: Expected O, but got Unknown
			//IL_1f1d: Expected O, but got I
			//IL_1f26: Expected O, but got I4
			//IL_1fb4: Expected I, but got O
			//IL_4315: Expected O, but got I
			//IL_432c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4331: Expected O, but got Unknown
			//IL_4339: Unknown result type (might be due to invalid IL or missing references)
			//IL_433e: Expected O, but got Unknown
			//IL_1f34: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f39: Expected O, but got Unknown
			//IL_1fec: Expected O, but got I
			//IL_1ff5: Expected O, but got I4
			//IL_2083: Expected I, but got O
			//IL_4366: Expected O, but got I
			//IL_437d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4382: Expected O, but got Unknown
			//IL_438a: Unknown result type (might be due to invalid IL or missing references)
			//IL_438f: Expected O, but got Unknown
			//IL_2003: Unknown result type (might be due to invalid IL or missing references)
			//IL_2008: Expected O, but got Unknown
			//IL_20bb: Expected O, but got I
			//IL_20c4: Expected O, but got I4
			//IL_2152: Expected I, but got O
			//IL_43b7: Expected O, but got I
			//IL_43ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_43d3: Expected O, but got Unknown
			//IL_43db: Unknown result type (might be due to invalid IL or missing references)
			//IL_43e0: Expected O, but got Unknown
			//IL_20d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_20d7: Expected O, but got Unknown
			//IL_218a: Expected O, but got I
			//IL_2193: Expected O, but got I4
			//IL_2221: Expected I, but got O
			//IL_4408: Expected O, but got I
			//IL_441f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4424: Expected O, but got Unknown
			//IL_442c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4431: Expected O, but got Unknown
			//IL_21a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_21a6: Expected O, but got Unknown
			//IL_2259: Expected O, but got I
			//IL_2262: Expected O, but got I4
			//IL_22f0: Expected I, but got O
			//IL_4459: Expected O, but got I
			//IL_4470: Unknown result type (might be due to invalid IL or missing references)
			//IL_4475: Expected O, but got Unknown
			//IL_447d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4482: Expected O, but got Unknown
			//IL_2270: Unknown result type (might be due to invalid IL or missing references)
			//IL_2275: Expected O, but got Unknown
			//IL_2328: Expected O, but got I
			//IL_2331: Expected O, but got I4
			//IL_23bf: Expected I, but got O
			//IL_44aa: Expected O, but got I
			//IL_44c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_44c6: Expected O, but got Unknown
			//IL_44ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_44d3: Expected O, but got Unknown
			//IL_233f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2344: Expected O, but got Unknown
			//IL_23f7: Expected O, but got I
			//IL_2400: Expected O, but got I4
			//IL_248e: Expected I, but got O
			//IL_44fb: Expected O, but got I
			//IL_4512: Unknown result type (might be due to invalid IL or missing references)
			//IL_4517: Expected O, but got Unknown
			//IL_451f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4524: Expected O, but got Unknown
			//IL_240e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2413: Expected O, but got Unknown
			//IL_24c6: Expected O, but got I
			//IL_24cf: Expected O, but got I4
			//IL_255d: Expected I, but got O
			//IL_454c: Expected O, but got I
			//IL_4563: Unknown result type (might be due to invalid IL or missing references)
			//IL_4568: Expected O, but got Unknown
			//IL_4570: Unknown result type (might be due to invalid IL or missing references)
			//IL_4575: Expected O, but got Unknown
			//IL_24dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_24e2: Expected O, but got Unknown
			//IL_2595: Expected O, but got I
			//IL_259e: Expected O, but got I4
			//IL_262c: Expected I, but got O
			//IL_459d: Expected O, but got I
			//IL_45b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_45b9: Expected O, but got Unknown
			//IL_45c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_45c6: Expected O, but got Unknown
			//IL_25ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_25b1: Expected O, but got Unknown
			//IL_2664: Expected O, but got I
			//IL_266d: Expected O, but got I4
			//IL_26fb: Expected I, but got O
			//IL_45ee: Expected O, but got I
			//IL_4605: Unknown result type (might be due to invalid IL or missing references)
			//IL_460a: Expected O, but got Unknown
			//IL_4612: Unknown result type (might be due to invalid IL or missing references)
			//IL_4617: Expected O, but got Unknown
			//IL_267b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2680: Expected O, but got Unknown
			//IL_2733: Expected O, but got I
			//IL_273c: Expected O, but got I4
			//IL_27ca: Expected I, but got O
			//IL_463f: Expected O, but got I
			//IL_4656: Unknown result type (might be due to invalid IL or missing references)
			//IL_465b: Expected O, but got Unknown
			//IL_4663: Unknown result type (might be due to invalid IL or missing references)
			//IL_4668: Expected O, but got Unknown
			//IL_274a: Unknown result type (might be due to invalid IL or missing references)
			//IL_274f: Expected O, but got Unknown
			//IL_2802: Expected O, but got I
			//IL_280b: Expected O, but got I4
			//IL_2899: Expected I, but got O
			//IL_4690: Expected O, but got I
			//IL_46a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_46ac: Expected O, but got Unknown
			//IL_46b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_46b9: Expected O, but got Unknown
			//IL_2819: Unknown result type (might be due to invalid IL or missing references)
			//IL_281e: Expected O, but got Unknown
			//IL_28d1: Expected O, but got I
			//IL_28da: Expected O, but got I4
			//IL_2968: Expected I, but got O
			//IL_46e1: Expected O, but got I
			//IL_46f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_46fd: Expected O, but got Unknown
			//IL_4705: Unknown result type (might be due to invalid IL or missing references)
			//IL_470a: Expected O, but got Unknown
			//IL_28e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_28ed: Expected O, but got Unknown
			//IL_29a0: Expected O, but got I
			//IL_29a9: Expected O, but got I4
			//IL_2a37: Expected I, but got O
			//IL_4732: Expected O, but got I
			//IL_4749: Unknown result type (might be due to invalid IL or missing references)
			//IL_474e: Expected O, but got Unknown
			//IL_4756: Unknown result type (might be due to invalid IL or missing references)
			//IL_475b: Expected O, but got Unknown
			//IL_29b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_29bc: Expected O, but got Unknown
			//IL_2a6f: Expected O, but got I
			//IL_2a78: Expected O, but got I4
			//IL_2b06: Expected I, but got O
			//IL_4783: Expected O, but got I
			//IL_479a: Unknown result type (might be due to invalid IL or missing references)
			//IL_479f: Expected O, but got Unknown
			//IL_47a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_47ac: Expected O, but got Unknown
			//IL_2a86: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a8b: Expected O, but got Unknown
			//IL_2b3e: Expected O, but got I
			//IL_2b47: Expected O, but got I4
			//IL_2bd5: Expected I, but got O
			//IL_47d4: Expected O, but got I
			//IL_47eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_47f0: Expected O, but got Unknown
			//IL_47f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_47fd: Expected O, but got Unknown
			//IL_2b55: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b5a: Expected O, but got Unknown
			//IL_2c0d: Expected O, but got I
			//IL_2c16: Expected O, but got I4
			//IL_2ca4: Expected I, but got O
			//IL_4825: Expected O, but got I
			//IL_483c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4841: Expected O, but got Unknown
			//IL_4849: Unknown result type (might be due to invalid IL or missing references)
			//IL_484e: Expected O, but got Unknown
			//IL_2c24: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c29: Expected O, but got Unknown
			//IL_2cdc: Expected O, but got I
			//IL_2ce5: Expected O, but got I4
			//IL_2d73: Expected I, but got O
			//IL_4876: Expected O, but got I
			//IL_488d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4892: Expected O, but got Unknown
			//IL_489a: Unknown result type (might be due to invalid IL or missing references)
			//IL_489f: Expected O, but got Unknown
			//IL_2cf3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2cf8: Expected O, but got Unknown
			//IL_2dab: Expected O, but got I
			//IL_2db4: Expected O, but got I4
			//IL_2e42: Expected I, but got O
			//IL_48c7: Expected O, but got I
			//IL_48de: Unknown result type (might be due to invalid IL or missing references)
			//IL_48e3: Expected O, but got Unknown
			//IL_48eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_48f0: Expected O, but got Unknown
			//IL_2dc2: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dc7: Expected O, but got Unknown
			//IL_2e7a: Expected O, but got I
			//IL_2e83: Expected O, but got I4
			//IL_2f11: Expected I, but got O
			//IL_4918: Expected O, but got I
			//IL_492f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4934: Expected O, but got Unknown
			//IL_493c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4941: Expected O, but got Unknown
			//IL_2e91: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e96: Expected O, but got Unknown
			//IL_2f49: Expected O, but got I
			//IL_2f52: Expected O, but got I4
			//IL_2fe0: Expected I, but got O
			//IL_4969: Expected O, but got I
			//IL_4980: Unknown result type (might be due to invalid IL or missing references)
			//IL_4985: Expected O, but got Unknown
			//IL_498d: Unknown result type (might be due to invalid IL or missing references)
			//IL_4992: Expected O, but got Unknown
			//IL_2f60: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f65: Expected O, but got Unknown
			//IL_3018: Expected O, but got I
			//IL_3021: Expected O, but got I4
			//IL_30af: Expected I, but got O
			//IL_49ba: Expected O, but got I
			//IL_49d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_49d6: Expected O, but got Unknown
			//IL_49de: Unknown result type (might be due to invalid IL or missing references)
			//IL_49e3: Expected O, but got Unknown
			//IL_302f: Unknown result type (might be due to invalid IL or missing references)
			//IL_3034: Expected O, but got Unknown
			//IL_30e7: Expected O, but got I
			//IL_30f0: Expected O, but got I4
			//IL_317e: Expected I, but got O
			//IL_4a0b: Expected O, but got I
			//IL_4a22: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a27: Expected O, but got Unknown
			//IL_4a2f: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a34: Expected O, but got Unknown
			//IL_30fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_3103: Expected O, but got Unknown
			//IL_31b6: Expected O, but got I
			//IL_31bf: Expected O, but got I4
			//IL_324d: Expected I, but got O
			//IL_4a5c: Expected O, but got I
			//IL_4a73: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a78: Expected O, but got Unknown
			//IL_4a80: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a85: Expected O, but got Unknown
			//IL_31cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_31d2: Expected O, but got Unknown
			//IL_3285: Expected O, but got I
			//IL_328e: Expected O, but got I4
			//IL_4aad: Expected O, but got I
			//IL_4ac4: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ac9: Expected O, but got Unknown
			//IL_4ad1: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ad6: Expected O, but got Unknown
			//IL_329c: Unknown result type (might be due to invalid IL or missing references)
			//IL_32a1: Expected O, but got Unknown
			InputActions wrapper = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2717 @ rax_v6+8]");
			Action<InputAction.CallbackContext> value = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ r10_v2 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00a3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ r10_v2 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj = 0;
			object obj2 = 0;
			while (true)
			{
				object obj3 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2659 @ r8_v424+v2662 @ rax_v1076*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj2++;
				object obj4 = obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ r10_v2 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj4 < 0)
				{
					continue;
				}
				goto IL_00a3;
			}
			object obj5 = obj2 + obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2659 @ r8_v424+8+v2720 @ rcx_v751*8]");
			object obj6 = (nint)0 << 4;
			object obj7 = obj6 + 312;
			object obj8 = obj7 + num;
			goto IL_00b8;
			IL_2eba:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2ecf;
			IL_19b4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_19c9;
			IL_19c9:
			InputActions wrapper2;
			Action<InputAction.CallbackContext> value2;
			wrapper2.m_Universal_UnequipGasmask.performed -= value2;
			InputActions wrapper3 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5751 @ rax_v166+8]");
			Action<InputAction.CallbackContext> value3 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num2 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1a83;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj9 = 0;
			object obj10 = 0;
			while (true)
			{
				object obj11 = obj10 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5693 @ r8_v328+v5696 @ rax_v728*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj10++;
				object obj12 = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v401 @ r10_v34 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj12 < 0)
				{
					continue;
				}
				goto IL_1a83;
			}
			object obj13 = obj10 + obj10;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5693 @ r8_v328+8+v5754 @ rcx_v559*8]");
			object obj14 = (nint)0 + (nint)10;
			object obj15 = obj14 << 4;
			object obj16 = obj15 + 312;
			object obj17 = obj16 + num2;
			goto IL_1a98;
			IL_29e0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_29f5;
			IL_00a3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_00b8;
			IL_00b8:
			wrapper.m_Universal_PointerDelta.started -= value;
			InputActions wrapper4 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2810 @ rax_v11+8]");
			Action<InputAction.CallbackContext> value4 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num3 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ r10_v3 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0172;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ r10_v3 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj18 = 0;
			object obj19 = 0;
			while (true)
			{
				object obj20 = obj19 + obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2752 @ r8_v421+v2755 @ rax_v1067*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj19++;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ r10_v3 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj21 < 0)
				{
					continue;
				}
				goto IL_0172;
			}
			object obj22 = obj19 + obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2752 @ r8_v421+8+v2813 @ rcx_v745*8]");
			object obj23 = (nint)0 << 4;
			object obj24 = obj23 + 312;
			object obj25 = obj24 + num3;
			goto IL_0187;
			IL_25d5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_25ea;
			IL_119e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_11b3;
			IL_11b3:
			InputActions wrapper5;
			Action<InputAction.CallbackContext> value5;
			wrapper5.m_Universal_FocuseClipboard.started -= value5;
			InputActions wrapper6 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4801 @ rax_v116+8]");
			Action<InputAction.CallbackContext> value6 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num4 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_126d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj26 = 0;
			object obj27 = 0;
			while (true)
			{
				object obj28 = obj27 + obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4743 @ r8_v358+v4746 @ rax_v838*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj27++;
				object obj29 = obj27;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ r10_v24 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj29 < 0)
				{
					continue;
				}
				goto IL_126d;
			}
			object obj30 = obj27 + obj27;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4743 @ r8_v358+8+v4804 @ rcx_v619*8]");
			object obj31 = (nint)0 + (nint)7;
			object obj32 = obj31 << 4;
			object obj33 = obj32 + 312;
			object obj34 = obj33 + num4;
			goto IL_1282;
			IL_2aaf:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2ac4;
			IL_0172:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0187;
			IL_0187:
			wrapper4.m_Universal_PointerDelta.performed -= value4;
			InputActions wrapper7 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2903 @ rax_v16+8]");
			Action<InputAction.CallbackContext> value7 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num5 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0241;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj35 = 0;
			object obj36 = 0;
			while (true)
			{
				object obj37 = obj36 + obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2845 @ r8_v418+v2848 @ rax_v1058*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj36++;
				object obj38 = obj36;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ r10_v4 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj38 < 0)
				{
					continue;
				}
				goto IL_0241;
			}
			object obj39 = obj36 + obj36;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2845 @ r8_v418+8+v2906 @ rcx_v739*8]");
			object obj40 = (nint)0 << 4;
			object obj41 = obj40 + 312;
			object obj42 = obj41 + num5;
			goto IL_0256;
			IL_313c:
			InputActions wrapper8;
			Action<InputAction.CallbackContext> value8;
			wrapper8.m_Universal_RotateRight.started -= value8;
			InputActions wrapper9 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8506 @ rax_v311+8]");
			Action<InputAction.CallbackContext> value9 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num6 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v430 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_31f6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v430 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj43 = 0;
			object obj44 = 0;
			while (true)
			{
				object obj45 = obj44 + obj44;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8448 @ r8_v241+v8451 @ rax_v409*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj44++;
				object obj46 = obj44;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v430 @ r10_v63 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj46 < 0)
				{
					continue;
				}
				goto IL_31f6;
			}
			object obj47 = obj44 + obj44;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8448 @ r8_v241+8+v8509 @ rcx_v385*8]");
			object obj48 = (nint)0 + (nint)20;
			object obj49 = obj48 << 4;
			object obj50 = obj49 + 312;
			object obj51 = obj50 + num6;
			goto IL_320b;
			IL_202c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2041;
			IL_2041:
			InputActions wrapper10;
			Action<InputAction.CallbackContext> value10;
			wrapper10.m_Universal_CinamaticLightSwitch.started -= value10;
			InputActions wrapper11 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6511 @ rax_v206+8]");
			Action<InputAction.CallbackContext> value11 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num7 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_20fb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj52 = 0;
			object obj53 = 0;
			while (true)
			{
				object obj54 = obj53 + obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6453 @ r8_v304+v6456 @ rax_v640*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj53++;
				object obj55 = obj53;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v409 @ r10_v42 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj55 < 0)
				{
					continue;
				}
				goto IL_20fb;
			}
			object obj56 = obj53 + obj53;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6453 @ r8_v304+8+v6514 @ rcx_v511*8]");
			object obj57 = (nint)0 + (nint)13;
			object obj58 = obj57 << 4;
			object obj59 = obj58 + 312;
			object obj60 = obj59 + num7;
			goto IL_2110;
			IL_2ac4:
			InputActions wrapper12;
			Action<InputAction.CallbackContext> value12;
			wrapper12.m_Universal_CheatImpactF10.performed -= value12;
			InputActions wrapper13 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7746 @ rax_v271+8]");
			Action<InputAction.CallbackContext> value13 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num8 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v422 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2b7e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v422 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj61 = 0;
			object obj62 = 0;
			while (true)
			{
				object obj63 = obj62 + obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7688 @ r8_v265+v7691 @ rax_v497*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj62++;
				object obj64 = obj62;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v422 @ r10_v55 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj64 < 0)
				{
					continue;
				}
				goto IL_2b7e;
			}
			object obj65 = obj62 + obj62;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7688 @ r8_v265+8+v7749 @ rcx_v433*8]");
			object obj66 = (nint)0 + (nint)17;
			object obj67 = obj66 << 4;
			object obj68 = obj67 + 312;
			object obj69 = obj68 + num8;
			goto IL_2b93;
			IL_0241:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0256;
			IL_0256:
			wrapper7.m_Universal_PointerDelta.canceled -= value7;
			InputActions wrapper14 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2996 @ rax_v21+8]");
			Action<InputAction.CallbackContext> value14 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num9 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0310;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj70 = 0;
			object obj71 = 0;
			while (true)
			{
				object obj72 = obj71 + obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2938 @ r8_v415+v2941 @ rax_v1047*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj71++;
				object obj73 = obj71;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v372 @ r10_v5 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj73 < 0)
				{
					continue;
				}
				goto IL_0310;
			}
			object obj74 = obj71 + obj71;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2938 @ r8_v415+8+v2999 @ rcx_v733*8]");
			object obj75 = (nint)0 + (nint)1;
			object obj76 = obj75 << 4;
			object obj77 = obj76 + 312;
			object obj78 = obj77 + num9;
			goto IL_0325;
			IL_2506:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_251b;
			IL_126d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1282;
			IL_1282:
			wrapper6.m_Universal_FocuseClipboard.performed -= value6;
			InputActions wrapper15 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4896 @ rax_v121+8]");
			Action<InputAction.CallbackContext> value15 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num10 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v392 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_133c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v392 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj79 = 0;
			object obj80 = 0;
			while (true)
			{
				object obj81 = obj80 + obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4838 @ r8_v355+v4841 @ rax_v827*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj80++;
				object obj82 = obj80;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v392 @ r10_v25 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj82 < 0)
				{
					continue;
				}
				goto IL_133c;
			}
			object obj83 = obj80 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4838 @ r8_v355+8+v4899 @ rcx_v613*8]");
			object obj84 = (nint)0 + (nint)7;
			object obj85 = obj84 << 4;
			object obj86 = obj85 + 312;
			object obj87 = obj86 + num10;
			goto IL_1351;
			IL_25ea:
			InputActions wrapper16;
			Action<InputAction.CallbackContext> value16;
			wrapper16.m_Universal_CheatRevealallonmap.performed -= value16;
			InputActions wrapper17 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7176 @ rax_v241+8]");
			Action<InputAction.CallbackContext> value17 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num11 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_26a4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj88 = 0;
			object obj89 = 0;
			while (true)
			{
				object obj90 = obj89 + obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7118 @ r8_v283+v7121 @ rax_v563*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj89++;
				object obj91 = obj89;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ r10_v49 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj91 < 0)
				{
					continue;
				}
				goto IL_26a4;
			}
			object obj92 = obj89 + obj89;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7118 @ r8_v283+8+v7179 @ rcx_v469*8]");
			object obj93 = (nint)0 + (nint)15;
			object obj94 = obj93 << 4;
			object obj95 = obj94 + 312;
			object obj96 = obj95 + num11;
			goto IL_26b9;
			IL_0310:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0325;
			IL_0325:
			wrapper14.m_Universal_Navigate.started -= value14;
			InputActions wrapper18 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3091 @ rax_v26+8]");
			Action<InputAction.CallbackContext> value18 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num12 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_03df;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj97 = 0;
			object obj98 = 0;
			while (true)
			{
				object obj99 = obj98 + obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3033 @ r8_v412+v3036 @ rax_v1036*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj98++;
				object obj100 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ r10_v6 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj100 < 0)
				{
					continue;
				}
				goto IL_03df;
			}
			object obj101 = obj98 + obj98;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3033 @ r8_v412+8+v3094 @ rcx_v727*8]");
			object obj102 = (nint)0 + (nint)1;
			object obj103 = obj102 << 4;
			object obj104 = obj103 + 312;
			object obj105 = obj104 + num12;
			goto IL_03f4;
			IL_2ecf:
			InputActions wrapper19;
			Action<InputAction.CallbackContext> value19;
			wrapper19.m_Universal_RotateLeft.started -= value19;
			InputActions wrapper20 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8221 @ rax_v296+8]");
			Action<InputAction.CallbackContext> value20 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num13 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2f89;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj106 = 0;
			object obj107 = 0;
			while (true)
			{
				object obj108 = obj107 + obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8163 @ r8_v250+v8166 @ rax_v442*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj107++;
				object obj109 = obj107;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r10_v60 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj109 < 0)
				{
					continue;
				}
				goto IL_2f89;
			}
			object obj110 = obj107 + obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8163 @ r8_v250+8+v8224 @ rcx_v403*8]");
			object obj111 = (nint)0 + (nint)19;
			object obj112 = obj111 << 4;
			object obj113 = obj112 + 312;
			object obj114 = obj113 + num13;
			goto IL_2f9e;
			IL_1a83:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1a98;
			IL_1a98:
			wrapper3.m_Universal_UnequipGasmask.canceled -= value3;
			InputActions wrapper21 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5846 @ rax_v171+8]");
			Action<InputAction.CallbackContext> value21 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num14 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1b52;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj115 = 0;
			object obj116 = 0;
			while (true)
			{
				object obj117 = obj116 + obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5788 @ r8_v325+v5791 @ rax_v717*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj116++;
				object obj118 = obj116;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ r10_v35 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj118 < 0)
				{
					continue;
				}
				goto IL_1b52;
			}
			object obj119 = obj116 + obj116;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5788 @ r8_v325+8+v5849 @ rcx_v553*8]");
			object obj120 = (nint)0 + (nint)11;
			object obj121 = obj120 << 4;
			object obj122 = obj121 + 312;
			object obj123 = obj122 + num14;
			goto IL_1b67;
			IL_26a4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_26b9;
			IL_03df:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_03f4;
			IL_03f4:
			wrapper18.m_Universal_Navigate.performed -= value18;
			InputActions wrapper22 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3186 @ rax_v31+8]");
			Action<InputAction.CallbackContext> value22 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num15 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_04ae;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj124 = 0;
			object obj125 = 0;
			while (true)
			{
				object obj126 = obj125 + obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3128 @ r8_v409+v3131 @ rax_v1025*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj125++;
				object obj127 = obj125;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ r10_v7 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj127 < 0)
				{
					continue;
				}
				goto IL_04ae;
			}
			object obj128 = obj125 + obj125;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3128 @ r8_v409+8+v3189 @ rcx_v721*8]");
			object obj129 = (nint)0 + (nint)1;
			object obj130 = obj129 << 4;
			object obj131 = obj130 + 312;
			object obj132 = obj131 + num15;
			goto IL_04c3;
			IL_251b:
			InputActions wrapper23;
			Action<InputAction.CallbackContext> value23;
			wrapper23.m_Universal_CheatRevealallonmap.started -= value23;
			wrapper16 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7081 @ rax_v236+8]");
			value16 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num16 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_25d5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj133 = 0;
			object obj134 = 0;
			while (true)
			{
				object obj135 = obj134 + obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7023 @ r8_v286+v7026 @ rax_v574*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj134++;
				object obj136 = obj134;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ r10_v48 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj136 < 0)
				{
					continue;
				}
				goto IL_25d5;
			}
			object obj137 = obj134 + obj134;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7023 @ r8_v286+8+v7084 @ rcx_v475*8]");
			object obj138 = (nint)0 + (nint)15;
			object obj139 = obj138 << 4;
			object obj140 = obj139 + 312;
			object obj141 = obj140 + num16;
			goto IL_25ea;
			IL_133c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1351;
			IL_1351:
			wrapper15.m_Universal_FocuseClipboard.canceled -= value15;
			InputActions wrapper24 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4991 @ rax_v126+8]");
			Action<InputAction.CallbackContext> value24 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num17 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_140b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj142 = 0;
			object obj143 = 0;
			while (true)
			{
				object obj144 = obj143 + obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4933 @ r8_v352+v4936 @ rax_v816*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj143++;
				object obj145 = obj143;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r10_v26 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj145 < 0)
				{
					continue;
				}
				goto IL_140b;
			}
			object obj146 = obj143 + obj143;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4933 @ r8_v352+8+v4994 @ rcx_v607*8]");
			object obj147 = (nint)0 + (nint)8;
			object obj148 = obj147 << 4;
			object obj149 = obj148 + 312;
			object obj150 = obj149 + num17;
			goto IL_1420;
			IL_26b9:
			wrapper17.m_Universal_CheatRevealallonmap.canceled -= value17;
			InputActions wrapper25 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7271 @ rax_v246+8]");
			Action<InputAction.CallbackContext> value25 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num18 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2773;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj151 = 0;
			object obj152 = 0;
			while (true)
			{
				object obj153 = obj152 + obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7213 @ r8_v280+v7216 @ rax_v552*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj152++;
				object obj154 = obj152;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ r10_v50 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj154 < 0)
				{
					continue;
				}
				goto IL_2773;
			}
			object obj155 = obj152 + obj152;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7213 @ r8_v280+8+v7274 @ rcx_v463*8]");
			object obj156 = (nint)0 + (nint)16;
			object obj157 = obj156 << 4;
			object obj158 = obj157 + 312;
			object obj159 = obj158 + num18;
			goto IL_2788;
			IL_04ae:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_04c3;
			IL_04c3:
			wrapper22.m_Universal_Navigate.canceled -= value22;
			InputActions wrapper26 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3281 @ rax_v36+8]");
			Action<InputAction.CallbackContext> value26 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num19 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_057d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj160 = 0;
			object obj161 = 0;
			while (true)
			{
				object obj162 = obj161 + obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3223 @ r8_v406+v3226 @ rax_v1014*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj161++;
				object obj163 = obj161;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ r10_v8 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj163 < 0)
				{
					continue;
				}
				goto IL_057d;
			}
			object obj164 = obj161 + obj161;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3223 @ r8_v406+8+v3284 @ rcx_v715*8]");
			object obj165 = (nint)0 + (nint)2;
			object obj166 = obj165 << 4;
			object obj167 = obj166 + 312;
			object obj168 = obj167 + num19;
			goto IL_0592;
			IL_2f89:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2f9e;
			IL_1e8e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1ea3;
			IL_1ea3:
			InputActions wrapper27;
			Action<InputAction.CallbackContext> value27;
			wrapper27.m_Universal_CinamaticAutoReload.performed -= value27;
			InputActions wrapper28 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6321 @ rax_v196+8]");
			Action<InputAction.CallbackContext> value28 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num20 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v407 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1f5d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v407 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj169 = 0;
			object obj170 = 0;
			while (true)
			{
				object obj171 = obj170 + obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6263 @ r8_v310+v6266 @ rax_v662*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj170++;
				object obj172 = obj170;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v407 @ r10_v40 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj172 < 0)
				{
					continue;
				}
				goto IL_1f5d;
			}
			object obj173 = obj170 + obj170;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6263 @ r8_v310+8+v6324 @ rcx_v523*8]");
			object obj174 = (nint)0 + (nint)12;
			object obj175 = obj174 << 4;
			object obj176 = obj175 + 312;
			object obj177 = obj176 + num20;
			goto IL_1f72;
			IL_29f5:
			InputActions wrapper29;
			Action<InputAction.CallbackContext> value29;
			wrapper29.m_Universal_CheatImpactF10.started -= value29;
			wrapper12 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7651 @ rax_v266+8]");
			value12 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num21 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2aaf;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj178 = 0;
			object obj179 = 0;
			while (true)
			{
				object obj180 = obj179 + obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7593 @ r8_v268+v7596 @ rax_v508*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj179++;
				object obj181 = obj179;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r10_v54 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj181 < 0)
				{
					continue;
				}
				goto IL_2aaf;
			}
			object obj182 = obj179 + obj179;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7593 @ r8_v268+8+v7654 @ rcx_v439*8]");
			object obj183 = (nint)0 + (nint)17;
			object obj184 = obj183 << 4;
			object obj185 = obj184 + 312;
			object obj186 = obj185 + num21;
			goto IL_2ac4;
			IL_057d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0592;
			IL_0592:
			wrapper26.m_Universal_PointerPosition.started -= value26;
			InputActions wrapper30 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3376 @ rax_v41+8]");
			Action<InputAction.CallbackContext> value30 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num22 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_064c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj187 = 0;
			object obj188 = 0;
			while (true)
			{
				object obj189 = obj188 + obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3318 @ r8_v403+v3321 @ rax_v1003*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj188++;
				object obj190 = obj188;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v376 @ r10_v9 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj190 < 0)
				{
					continue;
				}
				goto IL_064c;
			}
			object obj191 = obj188 + obj188;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3318 @ r8_v403+8+v3379 @ rcx_v709*8]");
			object obj192 = (nint)0 + (nint)2;
			object obj193 = obj192 << 4;
			object obj194 = obj193 + 312;
			object obj195 = obj194 + num22;
			goto IL_0661;
			IL_320b:
			wrapper9.m_Universal_RotateRight.performed -= value9;
			InputActions wrapper31 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8599 @ rax_v316+8]");
			Action<InputAction.CallbackContext> value31 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num23 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8529 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_32c5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8529 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj196 = 0;
			object obj197 = 0;
			while (true)
			{
				object obj198 = obj197 + obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8543 @ r8_v238+v8548 @ rax_v398*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj197++;
				object obj199 = obj197;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8529 @ r9_v126 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj199 < 0)
				{
					continue;
				}
				goto IL_32c5;
			}
			object obj200 = obj197 + obj197;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8543 @ r8_v238+8+v8602 @ rcx_v380*8]");
			object obj201 = (nint)0 + (nint)20;
			object obj202 = obj201 << 4;
			object obj203 = obj202 + 312;
			object obj204 = obj203 + num23;
			goto IL_32da;
			IL_140b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1420;
			IL_1420:
			wrapper24.m_Universal_Escape.started -= value24;
			InputActions wrapper32 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5086 @ rax_v131+8]");
			Action<InputAction.CallbackContext> value32 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num24 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_14da;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj205 = 0;
			object obj206 = 0;
			while (true)
			{
				object obj207 = obj206 + obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5028 @ r8_v349+v5031 @ rax_v805*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj206++;
				object obj208 = obj206;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r10_v27 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj208 < 0)
				{
					continue;
				}
				goto IL_14da;
			}
			object obj209 = obj206 + obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5028 @ r8_v349+8+v5089 @ rcx_v601*8]");
			object obj210 = (nint)0 + (nint)8;
			object obj211 = obj210 << 4;
			object obj212 = obj211 + 312;
			object obj213 = obj212 + num24;
			goto IL_14ef;
			IL_2b7e:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2b93;
			IL_064c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0661;
			IL_0661:
			wrapper30.m_Universal_PointerPosition.performed -= value30;
			InputActions wrapper33 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3471 @ rax_v46+8]");
			Action<InputAction.CallbackContext> value33 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num25 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_071b;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj214 = 0;
			object obj215 = 0;
			while (true)
			{
				object obj216 = obj215 + obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3413 @ r8_v400+v3416 @ rax_v992*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj215++;
				object obj217 = obj215;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v377 @ r10_v10 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj217 < 0)
				{
					continue;
				}
				goto IL_071b;
			}
			object obj218 = obj215 + obj215;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3413 @ r8_v400+8+v3474 @ rcx_v703*8]");
			object obj219 = (nint)0 + (nint)2;
			object obj220 = obj219 << 4;
			object obj221 = obj220 + 312;
			object obj222 = obj221 + num25;
			goto IL_0730;
			IL_244c:
			InputActions wrapper34;
			Action<InputAction.CallbackContext> value34;
			wrapper34.m_Universal_CinamaticSwingForce.canceled -= value34;
			wrapper23 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6986 @ rax_v231+8]");
			value23 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num26 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2506;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj223 = 0;
			object obj224 = 0;
			while (true)
			{
				object obj225 = obj224 + obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6928 @ r8_v289+v6931 @ rax_v585*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj224++;
				object obj226 = obj224;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r10_v47 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj226 < 0)
				{
					continue;
				}
				goto IL_2506;
			}
			object obj227 = obj224 + obj224;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6928 @ r8_v289+8+v6989 @ rcx_v481*8]");
			object obj228 = (nint)0 + (nint)15;
			object obj229 = obj228 << 4;
			object obj230 = obj229 + 312;
			object obj231 = obj230 + num26;
			goto IL_251b;
			IL_1b52:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1b67;
			IL_1b67:
			wrapper21.m_Universal_CinamaticHideCursorToggle.started -= value21;
			InputActions wrapper35 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5941 @ rax_v176+8]");
			Action<InputAction.CallbackContext> value35 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num27 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v403 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1c21;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v403 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj232 = 0;
			object obj233 = 0;
			while (true)
			{
				object obj234 = obj233 + obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5883 @ r8_v322+v5886 @ rax_v706*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj233++;
				object obj235 = obj233;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v403 @ r10_v36 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj235 < 0)
				{
					continue;
				}
				goto IL_1c21;
			}
			object obj236 = obj233 + obj233;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5883 @ r8_v322+8+v5944 @ rcx_v547*8]");
			object obj237 = (nint)0 + (nint)11;
			object obj238 = obj237 << 4;
			object obj239 = obj238 + 312;
			object obj240 = obj239 + num27;
			goto IL_1c36;
			IL_2b93:
			wrapper13.m_Universal_CheatImpactF10.canceled -= value13;
			InputActions wrapper36 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7841 @ rax_v276+8]");
			Action<InputAction.CallbackContext> value36 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num28 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2c4d;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj241 = 0;
			object obj242 = 0;
			while (true)
			{
				object obj243 = obj242 + obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7783 @ r8_v262+v7786 @ rax_v486*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj242++;
				object obj244 = obj242;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v423 @ r10_v56 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj244 < 0)
				{
					continue;
				}
				goto IL_2c4d;
			}
			object obj245 = obj242 + obj242;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7783 @ r8_v262+8+v7844 @ rcx_v427*8]");
			object obj246 = (nint)0 + (nint)18;
			object obj247 = obj246 << 4;
			object obj248 = obj247 + 312;
			object obj249 = obj248 + num28;
			goto IL_2c62;
			IL_071b:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0730;
			IL_0730:
			wrapper33.m_Universal_PointerPosition.canceled -= value33;
			InputActions wrapper37 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3566 @ rax_v51+8]");
			Action<InputAction.CallbackContext> value37 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num29 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_07ea;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj250 = 0;
			object obj251 = 0;
			while (true)
			{
				object obj252 = obj251 + obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3508 @ r8_v397+v3511 @ rax_v981*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj251++;
				object obj253 = obj251;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v378 @ r10_v11 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj253 < 0)
				{
					continue;
				}
				goto IL_07ea;
			}
			object obj254 = obj251 + obj251;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3508 @ r8_v397+8+v3569 @ rcx_v697*8]");
			object obj255 = (nint)0 + (nint)3;
			object obj256 = obj255 << 4;
			object obj257 = obj256 + 312;
			object obj258 = obj257 + num29;
			goto IL_07ff;
			IL_2deb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2e00;
			IL_14da:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_14ef;
			IL_14ef:
			wrapper32.m_Universal_Escape.performed -= value32;
			InputActions wrapper38 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5181 @ rax_v136+8]");
			Action<InputAction.CallbackContext> value38 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num30 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_15a9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj259 = 0;
			object obj260 = 0;
			while (true)
			{
				object obj261 = obj260 + obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5123 @ r8_v346+v5126 @ rax_v794*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj260++;
				object obj262 = obj260;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ r10_v28 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj262 < 0)
				{
					continue;
				}
				goto IL_15a9;
			}
			object obj263 = obj260 + obj260;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5123 @ r8_v346+8+v5184 @ rcx_v595*8]");
			object obj264 = (nint)0 + (nint)8;
			object obj265 = obj264 << 4;
			object obj266 = obj265 + 312;
			object obj267 = obj266 + num30;
			goto IL_15be;
			IL_3058:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_306d;
			IL_07ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_07ff;
			IL_07ff:
			wrapper37.m_Universal_PrimaryClick.started -= value37;
			InputActions wrapper39 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3661 @ rax_v56+8]");
			Action<InputAction.CallbackContext> value39 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num31 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_08b9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj268 = 0;
			object obj269 = 0;
			while (true)
			{
				object obj270 = obj269 + obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3603 @ r8_v394+v3606 @ rax_v970*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj269++;
				object obj271 = obj269;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ r10_v12 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj271 < 0)
				{
					continue;
				}
				goto IL_08b9;
			}
			object obj272 = obj269 + obj269;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3603 @ r8_v394+8+v3664 @ rcx_v691*8]");
			object obj273 = (nint)0 + (nint)3;
			object obj274 = obj273 << 4;
			object obj275 = obj274 + 312;
			object obj276 = obj275 + num31;
			goto IL_08ce;
			IL_2926:
			InputActions wrapper40;
			Action<InputAction.CallbackContext> value40;
			wrapper40.m_Universal_CheatImpactF9.canceled -= value40;
			wrapper29 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7556 @ rax_v261+8]");
			value29 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num32 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v420 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_29e0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v420 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj277 = 0;
			object obj278 = 0;
			while (true)
			{
				object obj279 = obj278 + obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7498 @ r8_v271+v7501 @ rax_v519*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj278++;
				object obj280 = obj278;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v420 @ r10_v53 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj280 < 0)
				{
					continue;
				}
				goto IL_29e0;
			}
			object obj281 = obj278 + obj278;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7498 @ r8_v271+8+v7559 @ rcx_v445*8]");
			object obj282 = (nint)0 + (nint)17;
			object obj283 = obj282 << 4;
			object obj284 = obj283 + 312;
			object obj285 = obj284 + num32;
			goto IL_29f5;
			IL_21ca:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_21df;
			IL_21df:
			InputActions wrapper41;
			Action<InputAction.CallbackContext> value41;
			wrapper41.m_Universal_CinamaticLightSwitch.canceled -= value41;
			InputActions wrapper42 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6701 @ rax_v216+8]");
			Action<InputAction.CallbackContext> value42 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num33 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2299;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj286 = 0;
			object obj287 = 0;
			while (true)
			{
				object obj288 = obj287 + obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6643 @ r8_v298+v6646 @ rax_v618*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj287++;
				object obj289 = obj287;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v411 @ r10_v44 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj289 < 0)
				{
					continue;
				}
				goto IL_2299;
			}
			object obj290 = obj287 + obj287;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6643 @ r8_v298+8+v6704 @ rcx_v499*8]");
			object obj291 = (nint)0 + (nint)14;
			object obj292 = obj291 << 4;
			object obj293 = obj292 + 312;
			object obj294 = obj293 + num33;
			goto IL_22ae;
			IL_2368:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_237d;
			IL_08b9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_08ce;
			IL_08ce:
			wrapper39.m_Universal_PrimaryClick.performed -= value39;
			InputActions wrapper43 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3756 @ rax_v61+8]");
			Action<InputAction.CallbackContext> value43 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num34 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0988;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj295 = 0;
			object obj296 = 0;
			while (true)
			{
				object obj297 = obj296 + obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3698 @ r8_v391+v3701 @ rax_v959*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj296++;
				object obj298 = obj296;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ r10_v13 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj298 < 0)
				{
					continue;
				}
				goto IL_0988;
			}
			object obj299 = obj296 + obj296;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3698 @ r8_v391+8+v3759 @ rcx_v685*8]");
			object obj300 = (nint)0 + (nint)3;
			object obj301 = obj300 << 4;
			object obj302 = obj301 + 312;
			object obj303 = obj302 + num34;
			goto IL_099d;
			IL_2e00:
			InputActions wrapper44;
			Action<InputAction.CallbackContext> value44;
			wrapper44.m_Universal_CheatImpactF11.canceled -= value44;
			wrapper19 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8126 @ rax_v291+8]");
			value19 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num35 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v426 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2eba;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v426 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj304 = 0;
			object obj305 = 0;
			while (true)
			{
				object obj306 = obj305 + obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8068 @ r8_v253+v8071 @ rax_v453*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj305++;
				object obj307 = obj305;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v426 @ r10_v59 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj307 < 0)
				{
					continue;
				}
				goto IL_2eba;
			}
			object obj308 = obj305 + obj305;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8068 @ r8_v253+8+v8129 @ rcx_v409*8]");
			object obj309 = (nint)0 + (nint)19;
			object obj310 = obj309 << 4;
			object obj311 = obj310 + 312;
			object obj312 = obj311 + num35;
			goto IL_2ecf;
			IL_15a9:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_15be;
			IL_15be:
			wrapper38.m_Universal_Escape.canceled -= value38;
			InputActions wrapper45 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5276 @ rax_v141+8]");
			Action<InputAction.CallbackContext> value45 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num36 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1678;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj313 = 0;
			object obj314 = 0;
			while (true)
			{
				object obj315 = obj314 + obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5218 @ r8_v343+v5221 @ rax_v783*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj314++;
				object obj316 = obj314;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ r10_v29 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj316 < 0)
				{
					continue;
				}
				goto IL_1678;
			}
			object obj317 = obj314 + obj314;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5218 @ r8_v343+8+v5279 @ rcx_v589*8]");
			object obj318 = (nint)0 + (nint)9;
			object obj319 = obj318 << 4;
			object obj320 = obj319 + 312;
			object obj321 = obj320 + num36;
			goto IL_168d;
			IL_237d:
			InputActions wrapper46;
			Action<InputAction.CallbackContext> value46;
			wrapper46.m_Universal_CinamaticSwingForce.performed -= value46;
			wrapper34 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6891 @ rax_v226+8]");
			value34 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num37 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2437;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj322 = 0;
			object obj323 = 0;
			while (true)
			{
				object obj324 = obj323 + obj323;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6833 @ r8_v292+v6836 @ rax_v596*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj323++;
				object obj325 = obj323;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v413 @ r10_v46 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj325 < 0)
				{
					continue;
				}
				goto IL_2437;
			}
			object obj326 = obj323 + obj323;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6833 @ r8_v292+8+v6894 @ rcx_v487*8]");
			object obj327 = (nint)0 + (nint)14;
			object obj328 = obj327 << 4;
			object obj329 = obj328 + 312;
			object obj330 = obj329 + num37;
			goto IL_244c;
			IL_0988:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_099d;
			IL_099d:
			wrapper43.m_Universal_PrimaryClick.canceled -= value43;
			InputActions wrapper47 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3851 @ rax_v66+8]");
			Action<InputAction.CallbackContext> value47 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num38 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0a57;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj331 = 0;
			object obj332 = 0;
			while (true)
			{
				object obj333 = obj332 + obj332;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3793 @ r8_v388+v3796 @ rax_v948*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj332++;
				object obj334 = obj332;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ r10_v14 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj334 < 0)
				{
					continue;
				}
				goto IL_0a57;
			}
			object obj335 = obj332 + obj332;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3793 @ r8_v388+8+v3854 @ rcx_v679*8]");
			object obj336 = (nint)0 + (nint)4;
			object obj337 = obj336 << 4;
			object obj338 = obj337 + 312;
			object obj339 = obj338 + num38;
			goto IL_0a6c;
			IL_2299:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_22ae;
			IL_1c21:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1c36;
			IL_1c36:
			wrapper35.m_Universal_CinamaticHideCursorToggle.performed -= value35;
			InputActions wrapper48 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6036 @ rax_v181+8]");
			Action<InputAction.CallbackContext> value48 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num39 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1cf0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj340 = 0;
			object obj341 = 0;
			while (true)
			{
				object obj342 = obj341 + obj341;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5978 @ r8_v319+v5981 @ rax_v695*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj341++;
				object obj343 = obj341;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ r10_v37 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj343 < 0)
				{
					continue;
				}
				goto IL_1cf0;
			}
			object obj344 = obj341 + obj341;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5978 @ r8_v319+8+v6039 @ rcx_v541*8]");
			object obj345 = (nint)0 + (nint)11;
			object obj346 = obj345 << 4;
			object obj347 = obj346 + 312;
			object obj348 = obj347 + num39;
			goto IL_1d05;
			IL_32c5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_32da;
			IL_0a57:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0a6c;
			IL_0a6c:
			wrapper47.m_Universal_SecondaryClick.started -= value47;
			InputActions wrapper49 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3946 @ rax_v71+8]");
			Action<InputAction.CallbackContext> value49 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num40 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v382 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0b26;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v382 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj349 = 0;
			object obj350 = 0;
			while (true)
			{
				object obj351 = obj350 + obj350;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3888 @ r8_v385+v3891 @ rax_v937*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj350++;
				object obj352 = obj350;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v382 @ r10_v15 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj352 < 0)
				{
					continue;
				}
				goto IL_0b26;
			}
			object obj353 = obj350 + obj350;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3888 @ r8_v385+8+v3949 @ rcx_v673*8]");
			object obj354 = (nint)0 + (nint)4;
			object obj355 = obj354 << 4;
			object obj356 = obj355 + 312;
			object obj357 = obj356 + num40;
			goto IL_0b3b;
			IL_3127:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_313c;
			IL_1678:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_168d;
			IL_168d:
			wrapper45.m_Universal_FreecamScrollWheel.started -= value45;
			InputActions wrapper50 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5371 @ rax_v146+8]");
			Action<InputAction.CallbackContext> value50 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num41 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1747;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj358 = 0;
			object obj359 = 0;
			while (true)
			{
				object obj360 = obj359 + obj359;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5313 @ r8_v340+v5316 @ rax_v772*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj359++;
				object obj361 = obj359;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v30 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj361 < 0)
				{
					continue;
				}
				goto IL_1747;
			}
			object obj362 = obj359 + obj359;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5313 @ r8_v340+8+v5374 @ rcx_v583*8]");
			object obj363 = (nint)0 + (nint)9;
			object obj364 = obj363 << 4;
			object obj365 = obj364 + 312;
			object obj366 = obj365 + num41;
			goto IL_175c;
			IL_2c4d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2c62;
			IL_0b26:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0b3b;
			IL_0b3b:
			wrapper49.m_Universal_SecondaryClick.performed -= value49;
			InputActions wrapper51 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4041 @ rax_v76+8]");
			Action<InputAction.CallbackContext> value51 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num42 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0bf5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj367 = 0;
			object obj368 = 0;
			while (true)
			{
				object obj369 = obj368 + obj368;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3983 @ r8_v382+v3986 @ rax_v926*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj368++;
				object obj370 = obj368;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ r10_v16 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj370 < 0)
				{
					continue;
				}
				goto IL_0bf5;
			}
			object obj371 = obj368 + obj368;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3983 @ r8_v382+8+v4044 @ rcx_v667*8]");
			object obj372 = (nint)0 + (nint)4;
			object obj373 = obj372 << 4;
			object obj374 = obj373 + 312;
			object obj375 = obj374 + num42;
			goto IL_0c0a;
			IL_2f9e:
			wrapper20.m_Universal_RotateLeft.performed -= value20;
			InputActions wrapper52 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8316 @ rax_v301+8]");
			Action<InputAction.CallbackContext> value52 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num43 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_3058;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj376 = 0;
			object obj377 = 0;
			while (true)
			{
				object obj378 = obj377 + obj377;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8258 @ r8_v247+v8261 @ rax_v431*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj377++;
				object obj379 = obj377;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ r10_v61 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj379 < 0)
				{
					continue;
				}
				goto IL_3058;
			}
			object obj380 = obj377 + obj377;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8258 @ r8_v247+8+v8319 @ rcx_v397*8]");
			object obj381 = (nint)0 + (nint)19;
			object obj382 = obj381 << 4;
			object obj383 = obj382 + 312;
			object obj384 = obj383 + num43;
			goto IL_306d;
			IL_1f5d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1f72;
			IL_1f72:
			wrapper28.m_Universal_CinamaticAutoReload.canceled -= value28;
			wrapper10 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6416 @ rax_v201+8]");
			value10 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num44 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_202c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj385 = 0;
			object obj386 = 0;
			while (true)
			{
				object obj387 = obj386 + obj386;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6358 @ r8_v307+v6361 @ rax_v651*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj386++;
				object obj388 = obj386;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v41 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj388 < 0)
				{
					continue;
				}
				goto IL_202c;
			}
			object obj389 = obj386 + obj386;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6358 @ r8_v307+8+v6419 @ rcx_v517*8]");
			object obj390 = (nint)0 + (nint)13;
			object obj391 = obj390 << 4;
			object obj392 = obj391 + 312;
			object obj393 = obj392 + num44;
			goto IL_2041;
			IL_2c62:
			wrapper36.m_Universal_CheatImpactF11.started -= value36;
			InputActions wrapper53 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7936 @ rax_v281+8]");
			Action<InputAction.CallbackContext> value53 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num45 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v424 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2d1c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v424 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj394 = 0;
			object obj395 = 0;
			while (true)
			{
				object obj396 = obj395 + obj395;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7878 @ r8_v259+v7881 @ rax_v475*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj395++;
				object obj397 = obj395;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v424 @ r10_v57 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj397 < 0)
				{
					continue;
				}
				goto IL_2d1c;
			}
			object obj398 = obj395 + obj395;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7878 @ r8_v259+8+v7939 @ rcx_v421*8]");
			object obj399 = (nint)0 + (nint)18;
			object obj400 = obj399 << 4;
			object obj401 = obj400 + 312;
			object obj402 = obj401 + num45;
			goto IL_2d31;
			IL_0bf5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0c0a;
			IL_0c0a:
			wrapper51.m_Universal_SecondaryClick.canceled -= value51;
			InputActions wrapper54 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4136 @ rax_v81+8]");
			Action<InputAction.CallbackContext> value54 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num46 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0cc4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj403 = 0;
			object obj404 = 0;
			while (true)
			{
				object obj405 = obj404 + obj404;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4078 @ r8_v379+v4081 @ rax_v915*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj404++;
				object obj406 = obj404;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ r10_v17 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj406 < 0)
				{
					continue;
				}
				goto IL_0cc4;
			}
			object obj407 = obj404 + obj404;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4078 @ r8_v379+8+v4139 @ rcx_v661*8]");
			object obj408 = (nint)0 + (nint)5;
			object obj409 = obj408 << 4;
			object obj410 = obj409 + 312;
			object obj411 = obj410 + num46;
			goto IL_0cd9;
			IL_2842:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2857;
			IL_1747:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_175c;
			IL_175c:
			wrapper50.m_Universal_FreecamScrollWheel.performed -= value50;
			InputActions wrapper55 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5466 @ rax_v151+8]");
			Action<InputAction.CallbackContext> value55 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num47 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1816;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj412 = 0;
			object obj413 = 0;
			while (true)
			{
				object obj414 = obj413 + obj413;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5408 @ r8_v337+v5411 @ rax_v761*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj413++;
				object obj415 = obj413;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ r10_v31 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj415 < 0)
				{
					continue;
				}
				goto IL_1816;
			}
			object obj416 = obj413 + obj413;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5408 @ r8_v337+8+v5469 @ rcx_v577*8]");
			object obj417 = (nint)0 + (nint)9;
			object obj418 = obj417 << 4;
			object obj419 = obj418 + 312;
			object obj420 = obj419 + num47;
			goto IL_182b;
			IL_32da:
			wrapper31.m_Universal_RotateRight.canceled -= value31;
			InputActions wrapper56 = m_Wrapper;
			IntPtr method = default(IntPtr);
			Action<InputAction.CallbackContext> value56 = new Action<InputAction.CallbackContext>(instance, method);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper56.m_Universal_Cinamatic4kScreenshot.started -= value56;
			InputActions wrapper57 = m_Wrapper;
			IntPtr method2 = default(IntPtr);
			Action<InputAction.CallbackContext> value57 = new Action<InputAction.CallbackContext>(instance, method2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper57.m_Universal_Cinamatic4kScreenshot.performed -= value57;
			InputActions wrapper58 = m_Wrapper;
			IntPtr method3 = default(IntPtr);
			Action<InputAction.CallbackContext> value58 = new Action<InputAction.CallbackContext>(instance, method3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper58.m_Universal_Cinamatic4kScreenshot.canceled -= value58;
			InputActions wrapper59 = m_Wrapper;
			IntPtr method4 = default(IntPtr);
			Action<InputAction.CallbackContext> value59 = new Action<InputAction.CallbackContext>(instance, method4);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper59.m_Universal_ContinueEnter.started -= value59;
			InputActions wrapper60 = m_Wrapper;
			IntPtr method5 = default(IntPtr);
			Action<InputAction.CallbackContext> value60 = new Action<InputAction.CallbackContext>(instance, method5);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper60.m_Universal_ContinueEnter.performed -= value60;
			InputActions wrapper61 = m_Wrapper;
			IntPtr method6 = default(IntPtr);
			Action<InputAction.CallbackContext> value61 = new Action<InputAction.CallbackContext>(instance, method6);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper61.m_Universal_ContinueEnter.canceled -= value61;
			InputActions wrapper62 = m_Wrapper;
			IntPtr method7 = default(IntPtr);
			Action<InputAction.CallbackContext> value62 = new Action<InputAction.CallbackContext>(instance, method7);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper62.m_Universal_PickUp.started -= value62;
			InputActions wrapper63 = m_Wrapper;
			IntPtr method8 = default(IntPtr);
			Action<InputAction.CallbackContext> value63 = new Action<InputAction.CallbackContext>(instance, method8);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper63.m_Universal_PickUp.performed -= value63;
			InputActions wrapper64 = m_Wrapper;
			IntPtr method9 = default(IntPtr);
			Action<InputAction.CallbackContext> value64 = new Action<InputAction.CallbackContext>(instance, method9);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper64.m_Universal_PickUp.canceled -= value64;
			InputActions wrapper65 = m_Wrapper;
			IntPtr method10 = default(IntPtr);
			Action<InputAction.CallbackContext> value65 = new Action<InputAction.CallbackContext>(instance, method10);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper65.m_Universal_Interact.started -= value65;
			InputActions wrapper66 = m_Wrapper;
			IntPtr method11 = default(IntPtr);
			Action<InputAction.CallbackContext> value66 = new Action<InputAction.CallbackContext>(instance, method11);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper66.m_Universal_Interact.performed -= value66;
			InputActions wrapper67 = m_Wrapper;
			IntPtr method12 = default(IntPtr);
			Action<InputAction.CallbackContext> value67 = new Action<InputAction.CallbackContext>(instance, method12);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper67.m_Universal_Interact.canceled -= value67;
			InputActions wrapper68 = m_Wrapper;
			IntPtr method13 = default(IntPtr);
			Action<InputAction.CallbackContext> value68 = new Action<InputAction.CallbackContext>(instance, method13);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper68.m_Universal_SlowCursor.started -= value68;
			InputActions wrapper69 = m_Wrapper;
			IntPtr method14 = default(IntPtr);
			Action<InputAction.CallbackContext> value69 = new Action<InputAction.CallbackContext>(instance, method14);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper69.m_Universal_SlowCursor.performed -= value69;
			InputActions wrapper70 = m_Wrapper;
			IntPtr method15 = default(IntPtr);
			Action<InputAction.CallbackContext> value70 = new Action<InputAction.CallbackContext>(instance, method15);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007C10");
			wrapper70.m_Universal_SlowCursor.canceled -= value70;
			return;
			IL_0cc4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0cd9;
			IL_0cd9:
			wrapper54.m_Universal_Tertiaryclick.started -= value54;
			InputActions wrapper71 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4231 @ rax_v86+8]");
			Action<InputAction.CallbackContext> value71 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num48 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0d93;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj421 = 0;
			object obj422 = 0;
			while (true)
			{
				object obj423 = obj422 + obj422;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4173 @ r8_v376+v4176 @ rax_v904*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj422++;
				object obj424 = obj422;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ r10_v18 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj424 < 0)
				{
					continue;
				}
				goto IL_0d93;
			}
			object obj425 = obj422 + obj422;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4173 @ r8_v376+8+v4234 @ rcx_v655*8]");
			object obj426 = (nint)0 + (nint)5;
			object obj427 = obj426 << 4;
			object obj428 = obj427 + 312;
			object obj429 = obj428 + num48;
			goto IL_0da8;
			IL_22ae:
			wrapper42.m_Universal_CinamaticSwingForce.started -= value42;
			wrapper46 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6796 @ rax_v221+8]");
			value46 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num49 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2368;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj430 = 0;
			object obj431 = 0;
			while (true)
			{
				object obj432 = obj431 + obj431;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6738 @ r8_v295+v6741 @ rax_v607*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj431++;
				object obj433 = obj431;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v45 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj433 < 0)
				{
					continue;
				}
				goto IL_2368;
			}
			object obj434 = obj431 + obj431;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6738 @ r8_v295+8+v6799 @ rcx_v493*8]");
			object obj435 = (nint)0 + (nint)14;
			object obj436 = obj435 << 4;
			object obj437 = obj436 + 312;
			object obj438 = obj437 + num49;
			goto IL_237d;
			IL_1cf0:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1d05;
			IL_1d05:
			wrapper48.m_Universal_CinamaticHideCursorToggle.canceled -= value48;
			InputActions wrapper72 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6131 @ rax_v186+8]");
			Action<InputAction.CallbackContext> value72 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num50 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1dbf;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj439 = 0;
			object obj440 = 0;
			while (true)
			{
				object obj441 = obj440 + obj440;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6073 @ r8_v316+v6076 @ rax_v684*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj440++;
				object obj442 = obj440;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ r10_v38 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj442 < 0)
				{
					continue;
				}
				goto IL_1dbf;
			}
			object obj443 = obj440 + obj440;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6073 @ r8_v316+8+v6134 @ rcx_v535*8]");
			object obj444 = (nint)0 + (nint)12;
			object obj445 = obj444 << 4;
			object obj446 = obj445 + 312;
			object obj447 = obj446 + num50;
			goto IL_1dd4;
			IL_2773:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2788;
			IL_0d93:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0da8;
			IL_0da8:
			wrapper71.m_Universal_Tertiaryclick.performed -= value71;
			InputActions wrapper73 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4326 @ rax_v91+8]");
			Action<InputAction.CallbackContext> value73 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num51 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0e62;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj448 = 0;
			object obj449 = 0;
			while (true)
			{
				object obj450 = obj449 + obj449;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4268 @ r8_v373+v4271 @ rax_v893*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj449++;
				object obj451 = obj449;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r10_v19 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj451 < 0)
				{
					continue;
				}
				goto IL_0e62;
			}
			object obj452 = obj449 + obj449;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4268 @ r8_v373+8+v4329 @ rcx_v649*8]");
			object obj453 = (nint)0 + (nint)5;
			object obj454 = obj453 << 4;
			object obj455 = obj454 + 312;
			object obj456 = obj455 + num51;
			goto IL_0e77;
			IL_2857:
			InputActions wrapper74;
			Action<InputAction.CallbackContext> value74;
			wrapper74.m_Universal_CheatImpactF9.performed -= value74;
			wrapper40 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7461 @ rax_v256+8]");
			value40 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num52 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v419 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2911;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v419 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj457 = 0;
			object obj458 = 0;
			while (true)
			{
				object obj459 = obj458 + obj458;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7403 @ r8_v274+v7406 @ rax_v530*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj458++;
				object obj460 = obj458;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v419 @ r10_v52 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj460 < 0)
				{
					continue;
				}
				goto IL_2911;
			}
			object obj461 = obj458 + obj458;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7403 @ r8_v274+8+v7464 @ rcx_v451*8]");
			object obj462 = (nint)0 + (nint)16;
			object obj463 = obj462 << 4;
			object obj464 = obj463 + 312;
			object obj465 = obj464 + num52;
			goto IL_2926;
			IL_1816:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_182b;
			IL_182b:
			wrapper55.m_Universal_FreecamScrollWheel.canceled -= value55;
			InputActions wrapper75 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5561 @ rax_v156+8]");
			Action<InputAction.CallbackContext> value75 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num53 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_18e5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj466 = 0;
			object obj467 = 0;
			while (true)
			{
				object obj468 = obj467 + obj467;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5503 @ r8_v334+v5506 @ rax_v750*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj467++;
				object obj469 = obj467;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ r10_v32 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj469 < 0)
				{
					continue;
				}
				goto IL_18e5;
			}
			object obj470 = obj467 + obj467;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5503 @ r8_v334+8+v5564 @ rcx_v571*8]");
			object obj471 = (nint)0 + (nint)10;
			object obj472 = obj471 << 4;
			object obj473 = obj472 + 312;
			object obj474 = obj473 + num53;
			goto IL_18fa;
			IL_2788:
			wrapper25.m_Universal_CheatImpactF9.started -= value25;
			wrapper74 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7366 @ rax_v251+8]");
			value74 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num54 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2842;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj475 = 0;
			object obj476 = 0;
			while (true)
			{
				object obj477 = obj476 + obj476;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7308 @ r8_v277+v7311 @ rax_v541*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj476++;
				object obj478 = obj476;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r10_v51 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj478 < 0)
				{
					continue;
				}
				goto IL_2842;
			}
			object obj479 = obj476 + obj476;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7308 @ r8_v277+8+v7369 @ rcx_v457*8]");
			object obj480 = (nint)0 + (nint)16;
			object obj481 = obj480 << 4;
			object obj482 = obj481 + 312;
			object obj483 = obj482 + num54;
			goto IL_2857;
			IL_0e62:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0e77;
			IL_0e77:
			wrapper73.m_Universal_Tertiaryclick.canceled -= value73;
			InputActions wrapper76 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4421 @ rax_v96+8]");
			Action<InputAction.CallbackContext> value76 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num55 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0f31;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj484 = 0;
			object obj485 = 0;
			while (true)
			{
				object obj486 = obj485 + obj485;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4363 @ r8_v370+v4366 @ rax_v882*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj485++;
				object obj487 = obj485;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r10_v20 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj487 < 0)
				{
					continue;
				}
				goto IL_0f31;
			}
			object obj488 = obj485 + obj485;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4363 @ r8_v370+8+v4424 @ rcx_v643*8]");
			object obj489 = (nint)0 + (nint)6;
			object obj490 = obj489 << 4;
			object obj491 = obj490 + 312;
			object obj492 = obj491 + num55;
			goto IL_0f46;
			IL_2911:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2926;
			IL_20fb:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2110;
			IL_2110:
			wrapper11.m_Universal_CinamaticLightSwitch.performed -= value11;
			wrapper41 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6606 @ rax_v211+8]");
			value41 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num56 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v410 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_21ca;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v410 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj493 = 0;
			object obj494 = 0;
			while (true)
			{
				object obj495 = obj494 + obj494;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6548 @ r8_v301+v6551 @ rax_v629*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj494++;
				object obj496 = obj494;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v410 @ r10_v43 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj496 < 0)
				{
					continue;
				}
				goto IL_21ca;
			}
			object obj497 = obj494 + obj494;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6548 @ r8_v301+8+v6609 @ rcx_v505*8]");
			object obj498 = (nint)0 + (nint)13;
			object obj499 = obj498 << 4;
			object obj500 = obj499 + 312;
			object obj501 = obj500 + num56;
			goto IL_21df;
			IL_31f6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_320b;
			IL_0f31:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_0f46;
			IL_0f46:
			wrapper76.m_Universal_ToggleClipboard.started -= value76;
			InputActions wrapper77 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4516 @ rax_v101+8]");
			Action<InputAction.CallbackContext> value77 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num57 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1000;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj502 = 0;
			object obj503 = 0;
			while (true)
			{
				object obj504 = obj503 + obj503;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4458 @ r8_v367+v4461 @ rax_v871*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj503++;
				object obj505 = obj503;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v388 @ r10_v21 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj505 < 0)
				{
					continue;
				}
				goto IL_1000;
			}
			object obj506 = obj503 + obj503;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4458 @ r8_v367+8+v4519 @ rcx_v637*8]");
			object obj507 = (nint)0 + (nint)6;
			object obj508 = obj507 << 4;
			object obj509 = obj508 + 312;
			object obj510 = obj509 + num57;
			goto IL_1015;
			IL_306d:
			wrapper52.m_Universal_RotateLeft.canceled -= value52;
			wrapper8 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8411 @ rax_v306+8]");
			value8 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num58 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v429 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_3127;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v429 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj511 = 0;
			object obj512 = 0;
			while (true)
			{
				object obj513 = obj512 + obj512;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8353 @ r8_v244+v8356 @ rax_v420*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj512++;
				object obj514 = obj512;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v429 @ r10_v62 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj514 < 0)
				{
					continue;
				}
				goto IL_3127;
			}
			object obj515 = obj512 + obj512;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8353 @ r8_v244+8+v8414 @ rcx_v391*8]");
			object obj516 = (nint)0 + (nint)20;
			object obj517 = obj516 << 4;
			object obj518 = obj517 + 312;
			object obj519 = obj518 + num58;
			goto IL_313c;
			IL_18e5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_18fa;
			IL_18fa:
			wrapper75.m_Universal_UnequipGasmask.started -= value75;
			wrapper2 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5656 @ rax_v161+8]");
			value2 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num59 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_19b4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj520 = 0;
			object obj521 = 0;
			while (true)
			{
				object obj522 = obj521 + obj521;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5598 @ r8_v331+v5601 @ rax_v739*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj521++;
				object obj523 = obj521;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v400 @ r10_v33 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj523 < 0)
				{
					continue;
				}
				goto IL_19b4;
			}
			object obj524 = obj521 + obj521;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5598 @ r8_v331+8+v5659 @ rcx_v565*8]");
			object obj525 = (nint)0 + (nint)10;
			object obj526 = obj525 << 4;
			object obj527 = obj526 + 312;
			object obj528 = obj527 + num59;
			goto IL_19c9;
			IL_2d1c:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_2d31;
			IL_1000:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1015;
			IL_1015:
			wrapper77.m_Universal_ToggleClipboard.performed -= value77;
			InputActions wrapper78 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4611 @ rax_v106+8]");
			Action<InputAction.CallbackContext> value78 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num60 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_10cf;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj529 = 0;
			object obj530 = 0;
			while (true)
			{
				object obj531 = obj530 + obj530;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4553 @ r8_v364+v4556 @ rax_v860*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj530++;
				object obj532 = obj530;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ r10_v22 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj532 < 0)
				{
					continue;
				}
				goto IL_10cf;
			}
			object obj533 = obj530 + obj530;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4553 @ r8_v364+8+v4614 @ rcx_v631*8]");
			object obj534 = (nint)0 + (nint)6;
			object obj535 = obj534 << 4;
			object obj536 = obj535 + 312;
			object obj537 = obj536 + num60;
			goto IL_10e4;
			IL_2437:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_244c;
			IL_1dbf:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_1dd4;
			IL_1dd4:
			wrapper72.m_Universal_CinamaticAutoReload.started -= value72;
			wrapper27 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6226 @ rax_v191+8]");
			value27 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num61 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v406 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_1e8e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v406 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj538 = 0;
			object obj539 = 0;
			while (true)
			{
				object obj540 = obj539 + obj539;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6168 @ r8_v313+v6171 @ rax_v673*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj539++;
				object obj541 = obj539;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v406 @ r10_v39 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj541 < 0)
				{
					continue;
				}
				goto IL_1e8e;
			}
			object obj542 = obj539 + obj539;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6168 @ r8_v313+8+v6229 @ rcx_v529*8]");
			object obj543 = (nint)0 + (nint)12;
			object obj544 = obj543 << 4;
			object obj545 = obj544 + 312;
			object obj546 = obj545 + num61;
			goto IL_1ea3;
			IL_2d31:
			wrapper53.m_Universal_CheatImpactF11.performed -= value53;
			wrapper44 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8031 @ rax_v286+8]");
			value44 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num62 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v425 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_2deb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v425 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj547 = 0;
			object obj548 = 0;
			while (true)
			{
				object obj549 = obj548 + obj548;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7973 @ r8_v256+v7976 @ rax_v464*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj548++;
				object obj550 = obj548;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v425 @ r10_v58 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj550 < 0)
				{
					continue;
				}
				goto IL_2deb;
			}
			object obj551 = obj548 + obj548;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7973 @ r8_v256+8+v8034 @ rcx_v415*8]");
			object obj552 = (nint)0 + (nint)18;
			object obj553 = obj552 << 4;
			object obj554 = obj553 + 312;
			object obj555 = obj554 + num62;
			goto IL_2e00;
			IL_10cf:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			goto IL_10e4;
			IL_10e4:
			wrapper78.m_Universal_ToggleClipboard.canceled -= value78;
			wrapper5 = m_Wrapper;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4706 @ rax_v111+8]");
			value5 = new Action<InputAction.CallbackContext>(instance, (IntPtr)0);
			nint num63 = (nint)instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_119e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+B0]");
			object obj556 = 0;
			object obj557 = 0;
			while (true)
			{
				object obj558 = obj557 + obj557;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4648 @ r8_v361+v4651 @ rax_v849*8]");
				if (0 == (nint)typeof(IUniversalActions))
				{
					break;
				}
				obj557++;
				object obj559 = obj557;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r10_v23 (Il2CppClass<InputActions+IUniversalActions>)+12E]");
				if ((nint)obj559 < 0)
				{
					continue;
				}
				goto IL_119e;
			}
			object obj560 = obj557 + obj557;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4648 @ r8_v361+8+v4709 @ rcx_v625*8]");
			object obj561 = (nint)0 + (nint)7;
			object obj562 = obj561 << 4;
			object obj563 = obj562 + 312;
			object obj564 = obj563 + num63;
			goto IL_11b3;
		}

		public void RemoveCallbacks(IUniversalActions instance)
		{
			InputActions wrapper = m_Wrapper;
			if (wrapper.m_UniversalActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public unsafe void SetCallbacks(IUniversalActions instance)
		{
			//IL_01d2: Expected O, but got Ref
			//IL_008d: Expected O, but got Ref
			InputActions wrapper = m_Wrapper;
			if (m_Wrapper != null && wrapper.m_UniversalActionsCallbackInterfaces != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<IUniversalActions>.Enumerator enumerator = default(List<IUniversalActions>.Enumerator);
				IUniversalActions instance2 = default(IUniversalActions);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					UnregisterCallbacks(instance2);
				}
				enumerator.Dispose();
				InputActions wrapper2 = m_Wrapper;
				bool flag = m_Wrapper == null;
				List<IUniversalActions>.Enumerator enumerator2 = (List<IUniversalActions>.Enumerator)(&enumerator);
				if (!flag)
				{
					List<IUniversalActions> universalActionsCallbackInterfaces = wrapper2.m_UniversalActionsCallbackInterfaces;
					bool flag2 = wrapper2.m_UniversalActionsCallbackInterfaces == null;
					enumerator2 = (List<IUniversalActions>.Enumerator)(&enumerator);
					if (!flag2)
					{
						int version = universalActionsCallbackInterfaces._version + 1;
						universalActionsCallbackInterfaces._version = version;
						((List<IUniversalActions>.Enumerator*)null)->Dispose();
						object obj = default(object);
						if (obj == null)
						{
							universalActionsCallbackInterfaces._size = 0;
						}
						else
						{
							universalActionsCallbackInterfaces._size = 0;
							if (universalActionsCallbackInterfaces._size > 0)
							{
								Array.Clear(universalActionsCallbackInterfaces._items, 0, universalActionsCallbackInterfaces._size);
							}
						}
						AddCallbacks(instance);
						return;
					}
				}
			}
			throw new NullReferenceException();
		}
	}

	public interface IPlayerActions
	{
		void OnMove(InputAction.CallbackContext context);

		void OnLook(InputAction.CallbackContext context);

		void OnFire(InputAction.CallbackContext context);

		void OnJump(InputAction.CallbackContext context);

		void OnSprint(InputAction.CallbackContext context);

		void OnCrouch(InputAction.CallbackContext context);

		void OnActivate(InputAction.CallbackContext context);

		void OnFreecam(InputAction.CallbackContext context);
	}

	public interface IUIActions
	{
		void OnClick(InputAction.CallbackContext context);

		void OnPoint(InputAction.CallbackContext context);

		void OnNavigate(InputAction.CallbackContext context);

		void OnMoveUI(InputAction.CallbackContext context);

		void OnSubmit(InputAction.CallbackContext context);

		void OnCancel(InputAction.CallbackContext context);

		void OnScrollWheel(InputAction.CallbackContext context);

		void OnMiddleClick(InputAction.CallbackContext context);

		void OnTrackedDevicePosition(InputAction.CallbackContext context);

		void OnTrackedDeviceOrientation(InputAction.CallbackContext context);

		void OnUp(InputAction.CallbackContext context);

		void OnDown(InputAction.CallbackContext context);
	}

	public interface IUniversalActions
	{
		void OnPointerDelta(InputAction.CallbackContext context);

		void OnNavigate(InputAction.CallbackContext context);

		void OnPointerPosition(InputAction.CallbackContext context);

		void OnPrimaryClick(InputAction.CallbackContext context);

		void OnSecondaryClick(InputAction.CallbackContext context);

		void OnTertiaryclick(InputAction.CallbackContext context);

		void OnToggleClipboard(InputAction.CallbackContext context);

		void OnFocuseClipboard(InputAction.CallbackContext context);

		void OnEscape(InputAction.CallbackContext context);

		void OnFreecamScrollWheel(InputAction.CallbackContext context);

		void OnUnequipGasmask(InputAction.CallbackContext context);

		void OnCinamaticHideCursorToggle(InputAction.CallbackContext context);

		void OnCinamaticAutoReload(InputAction.CallbackContext context);

		void OnCinamaticLightSwitch(InputAction.CallbackContext context);

		void OnCinamaticSwingForce(InputAction.CallbackContext context);

		void OnCheatRevealallonmap(InputAction.CallbackContext context);

		void OnCheatImpactF9(InputAction.CallbackContext context);

		void OnCheatImpactF10(InputAction.CallbackContext context);

		void OnCheatImpactF11(InputAction.CallbackContext context);

		void OnRotateLeft(InputAction.CallbackContext context);

		void OnRotateRight(InputAction.CallbackContext context);

		void OnCinamatic4kScreenshot(InputAction.CallbackContext context);

		void OnContinueEnter(InputAction.CallbackContext context);

		void OnPickUp(InputAction.CallbackContext context);

		void OnInteract(InputAction.CallbackContext context);

		void OnSlowCursor(InputAction.CallbackContext context);
	}

	private readonly InputActionAsset _003Casset_003Ek__BackingField;

	private readonly InputActionMap m_Player;

	private List<IPlayerActions> m_PlayerActionsCallbackInterfaces;

	private readonly InputAction m_Player_Move;

	private readonly InputAction m_Player_Look;

	private readonly InputAction m_Player_Fire;

	private readonly InputAction m_Player_Jump;

	private readonly InputAction m_Player_Sprint;

	private readonly InputAction m_Player_Crouch;

	private readonly InputAction m_Player_Activate;

	private readonly InputAction m_Player_Freecam;

	private readonly InputActionMap m_UI;

	private List<IUIActions> m_UIActionsCallbackInterfaces;

	private readonly InputAction m_UI_Click;

	private readonly InputAction m_UI_Point;

	private readonly InputAction m_UI_Navigate;

	private readonly InputAction m_UI_MoveUI;

	private readonly InputAction m_UI_Submit;

	private readonly InputAction m_UI_Cancel;

	private readonly InputAction m_UI_ScrollWheel;

	private readonly InputAction m_UI_MiddleClick;

	private readonly InputAction m_UI_TrackedDevicePosition;

	private readonly InputAction m_UI_TrackedDeviceOrientation;

	private readonly InputAction m_UI_Up;

	private readonly InputAction m_UI_Down;

	private readonly InputActionMap m_Universal;

	private List<IUniversalActions> m_UniversalActionsCallbackInterfaces;

	private readonly InputAction m_Universal_PointerDelta;

	private readonly InputAction m_Universal_Navigate;

	private readonly InputAction m_Universal_PointerPosition;

	private readonly InputAction m_Universal_PrimaryClick;

	private readonly InputAction m_Universal_SecondaryClick;

	private readonly InputAction m_Universal_Tertiaryclick;

	private readonly InputAction m_Universal_ToggleClipboard;

	private readonly InputAction m_Universal_FocuseClipboard;

	private readonly InputAction m_Universal_Escape;

	private readonly InputAction m_Universal_FreecamScrollWheel;

	private readonly InputAction m_Universal_UnequipGasmask;

	private readonly InputAction m_Universal_CinamaticHideCursorToggle;

	private readonly InputAction m_Universal_CinamaticAutoReload;

	private readonly InputAction m_Universal_CinamaticLightSwitch;

	private readonly InputAction m_Universal_CinamaticSwingForce;

	private readonly InputAction m_Universal_CheatRevealallonmap;

	private readonly InputAction m_Universal_CheatImpactF9;

	private readonly InputAction m_Universal_CheatImpactF10;

	private readonly InputAction m_Universal_CheatImpactF11;

	private readonly InputAction m_Universal_RotateLeft;

	private readonly InputAction m_Universal_RotateRight;

	private readonly InputAction m_Universal_Cinamatic4kScreenshot;

	private readonly InputAction m_Universal_ContinueEnter;

	private readonly InputAction m_Universal_PickUp;

	private readonly InputAction m_Universal_Interact;

	private readonly InputAction m_Universal_SlowCursor;

	private int m_KeyboardMouseSchemeIndex;

	private int m_GamepadSchemeIndex;

	private int m_TouchSchemeIndex;

	private int m_JoystickSchemeIndex;

	private int m_XRSchemeIndex;

	public InputActionAsset asset => _003Casset_003Ek__BackingField;

	public InputBinding? bindingMask
	{
		get
		{
			//IL_0010: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_0057: Expected O, but got I
			//IL_0069: Expected O, but got I
			//IL_007b: Expected O, but got I
			//IL_008d: Expected O, but got I
			//IL_009f: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+38]");
				InputActions inputActions = (InputActions)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+48]");
				_003Casset_003Ek__BackingField = (InputActionAsset)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+58]");
				m_PlayerActionsCallbackInterfaces = (List<IPlayerActions>)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+68]");
				m_Player_Look = (InputAction)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+78]");
				m_Player_Jump = (InputAction)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rax_v1+88]");
				m_Player_Crouch = (InputAction)0;
				return (InputBinding?)this;
			}
			return (InputBinding?)new NullReferenceException();
		}
		set
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<UnityEngine.InputSystem.InputBinding>)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<UnityEngine.InputSystem.InputBinding>)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<UnityEngine.InputSystem.InputBinding>)+30]");
			_ = 0;
			object obj = default(object);
			InputBinding? inputBinding = (InputBinding?)(object)(obj - 104);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<UnityEngine.InputSystem.InputBinding>)+40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [value @ rdx (System.Nullable`1<UnityEngine.InputSystem.InputBinding>)+50]");
			_ = 0;
			_003Casset_003Ek__BackingField.bindingMask = inputBinding;
		}
	}

	public unsafe ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			//IL_002e: Expected O, but got Ref
			//IL_004c: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			if ((nint)0 != 0)
			{
				object obj = default(object);
				ReadOnlyArray<InputDevice>? readOnlyArray = ((InputActionAsset)(&obj)).devices;
				InputActions inputActions = (InputActions)readOnlyArray;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2 (System.Nullable`1<UnityEngine.InputSystem.Utilities.ReadOnlyArray`1<UnityEngine.InputSystem.InputDevice>>)+10]");
				_003Casset_003Ek__BackingField = (InputActionAsset)0;
				return (ReadOnlyArray<InputDevice>?)this;
			}
			return (ReadOnlyArray<InputDevice>?)new NullReferenceException();
		}
		set
		{
			//IL_000f: Expected O, but got Ref
			object obj = default(object);
			_003Casset_003Ek__BackingField.devices = (ReadOnlyArray<InputDevice>?)(object)(&obj);
		}
	}

	public unsafe ReadOnlyArray<InputControlScheme> controlSchemes
	{
		get
		{
			//IL_002e: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+10]");
			if ((nint)0 != 0)
			{
				object obj = default(object);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&obj)).controlSchemes;
				InputActions inputActions = (InputActions)readOnlyArray;
				return (ReadOnlyArray<InputControlScheme>)this;
			}
			return (ReadOnlyArray<InputControlScheme>)new NullReferenceException();
		}
	}

	public IEnumerable<InputBinding> bindings
	{
		get
		{
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				return _003Casset_003Ek__BackingField.bindings;
			}
			return (IEnumerable<InputBinding>)new NullReferenceException();
		}
	}

	public PlayerActions Player
	{
		get
		{
			PlayerActions result = default(PlayerActions);
			return result;
		}
	}

	public UIActions UI
	{
		get
		{
			UIActions result = default(UIActions);
			return result;
		}
	}

	public UniversalActions Universal
	{
		get
		{
			UniversalActions result = default(UniversalActions);
			return result;
		}
	}

	public unsafe InputControlScheme KeyboardMouseScheme
	{
		get
		{
			//IL_0050: Expected O, but got Ref
			//IL_006b: Expected native int or pointer, but got O
			//IL_0078: Expected native int or pointer, but got O
			if (m_KeyboardMouseSchemeIndex == -1)
			{
				if ((object)_003Casset_003Ek__BackingField == null)
				{
					goto IL_0082;
				}
				int keyboardMouseSchemeIndex = _003Casset_003Ek__BackingField.FindControlSchemeIndex("Keyboard&Mouse");
				m_KeyboardMouseSchemeIndex = keyboardMouseSchemeIndex;
			}
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				string name = default(string);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&name)).controlSchemes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				InputControlScheme inputControlScheme = default(InputControlScheme);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_Name, name);
				InputControlScheme.DeviceRequirement[] deviceRequirements = default(InputControlScheme.DeviceRequirement[]);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_DeviceRequirements, deviceRequirements);
				return inputControlScheme;
			}
			goto IL_0082;
			IL_0082:
			return (InputControlScheme)new NullReferenceException();
		}
	}

	public unsafe InputControlScheme GamepadScheme
	{
		get
		{
			//IL_0050: Expected O, but got Ref
			//IL_006b: Expected native int or pointer, but got O
			//IL_0078: Expected native int or pointer, but got O
			if (m_GamepadSchemeIndex == -1)
			{
				if ((object)_003Casset_003Ek__BackingField == null)
				{
					goto IL_0082;
				}
				int gamepadSchemeIndex = _003Casset_003Ek__BackingField.FindControlSchemeIndex("Gamepad");
				m_GamepadSchemeIndex = gamepadSchemeIndex;
			}
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				string name = default(string);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&name)).controlSchemes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				InputControlScheme inputControlScheme = default(InputControlScheme);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_Name, name);
				InputControlScheme.DeviceRequirement[] deviceRequirements = default(InputControlScheme.DeviceRequirement[]);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_DeviceRequirements, deviceRequirements);
				return inputControlScheme;
			}
			goto IL_0082;
			IL_0082:
			return (InputControlScheme)new NullReferenceException();
		}
	}

	public unsafe InputControlScheme TouchScheme
	{
		get
		{
			//IL_0050: Expected O, but got Ref
			//IL_006b: Expected native int or pointer, but got O
			//IL_0078: Expected native int or pointer, but got O
			if (m_TouchSchemeIndex == -1)
			{
				if ((object)_003Casset_003Ek__BackingField == null)
				{
					goto IL_0082;
				}
				int touchSchemeIndex = _003Casset_003Ek__BackingField.FindControlSchemeIndex("Touch");
				m_TouchSchemeIndex = touchSchemeIndex;
			}
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				string name = default(string);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&name)).controlSchemes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				InputControlScheme inputControlScheme = default(InputControlScheme);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_Name, name);
				InputControlScheme.DeviceRequirement[] deviceRequirements = default(InputControlScheme.DeviceRequirement[]);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_DeviceRequirements, deviceRequirements);
				return inputControlScheme;
			}
			goto IL_0082;
			IL_0082:
			return (InputControlScheme)new NullReferenceException();
		}
	}

	public unsafe InputControlScheme JoystickScheme
	{
		get
		{
			//IL_0050: Expected O, but got Ref
			//IL_006b: Expected native int or pointer, but got O
			//IL_0078: Expected native int or pointer, but got O
			if (m_JoystickSchemeIndex == -1)
			{
				if ((object)_003Casset_003Ek__BackingField == null)
				{
					goto IL_0082;
				}
				int joystickSchemeIndex = _003Casset_003Ek__BackingField.FindControlSchemeIndex("Joystick");
				m_JoystickSchemeIndex = joystickSchemeIndex;
			}
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				string name = default(string);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&name)).controlSchemes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				InputControlScheme inputControlScheme = default(InputControlScheme);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_Name, name);
				InputControlScheme.DeviceRequirement[] deviceRequirements = default(InputControlScheme.DeviceRequirement[]);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_DeviceRequirements, deviceRequirements);
				return inputControlScheme;
			}
			goto IL_0082;
			IL_0082:
			return (InputControlScheme)new NullReferenceException();
		}
	}

	public unsafe InputControlScheme XRScheme
	{
		get
		{
			//IL_0050: Expected O, but got Ref
			//IL_006b: Expected native int or pointer, but got O
			//IL_0078: Expected native int or pointer, but got O
			if (m_XRSchemeIndex == -1)
			{
				if ((object)_003Casset_003Ek__BackingField == null)
				{
					goto IL_0082;
				}
				int xRSchemeIndex = _003Casset_003Ek__BackingField.FindControlSchemeIndex("XR");
				m_XRSchemeIndex = xRSchemeIndex;
			}
			if ((object)_003Casset_003Ek__BackingField != null)
			{
				string name = default(string);
				ReadOnlyArray<InputControlScheme> readOnlyArray = ((InputActionAsset)(&name)).controlSchemes;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808EA770");
				InputControlScheme inputControlScheme = default(InputControlScheme);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_Name, name);
				InputControlScheme.DeviceRequirement[] deviceRequirements = default(InputControlScheme.DeviceRequirement[]);
				System.Runtime.CompilerServices.Unsafe.Write(&((InputControlScheme*)(nint)inputControlScheme)->m_DeviceRequirements, deviceRequirements);
				return inputControlScheme;
			}
			goto IL_0082;
			IL_0082:
			return (InputControlScheme)new NullReferenceException();
		}
	}

	public InputActions()
	{
		//IL_0061: Expected I4, but got I8
		List<IPlayerActions> playerActionsCallbackInterfaces = new List<IPlayerActions>();
		m_PlayerActionsCallbackInterfaces = playerActionsCallbackInterfaces;
		List<IUIActions> uIActionsCallbackInterfaces = new List<IUIActions>();
		m_UIActionsCallbackInterfaces = uIActionsCallbackInterfaces;
		List<IUniversalActions> universalActionsCallbackInterfaces = new List<IUniversalActions>();
		m_UniversalActionsCallbackInterfaces = universalActionsCallbackInterfaces;
		m_KeyboardMouseSchemeIndex = -1;
		m_TouchSchemeIndex = -1;
		m_XRSchemeIndex = -1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		InputActionAsset inputActionAsset = InputActionAsset.FromJson("{\n    \"version\": 1,\n    \"name\": \"Input Actions\",\n    \"maps\": [\n        {\n            \"name\": \"Player\",\n            \"id\": \"abea5975-667d-4eec-9f5a-b39a22e2fab0\",\n            \"actions\": [\n                {\n                    \"name\": \"Move\",\n                    \"type\": \"Value\",\n                    \"id\": \"76296153-be39-4772-8c0c-662b6ead0ba8\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Look\",\n                    \"type\": \"Value\",\n                    \"id\": \"d3c64e21-8733-419f-932a-b7dbc097e86f\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Fire\",\n                    \"type\": \"Button\",\n                    \"id\": \"573cc447-c81e-45a2-a65e-7c9cfac247b9\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Jump\",\n                    \"type\": \"Button\",\n                    \"id\": \"0926c1cc-3e85-4c78-82ed-959bca934eb3\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Sprint\",\n                    \"type\": \"Button\",\n                    \"id\": \"cd269e70-feab-4b10-86a1-3c7dd2814603\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Crouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"4dd67444-bf34-4f8b-91fd-52bf302a64ce\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Activate\",\n                    \"type\": \"Button\",\n                    \"id\": \"cbad107c-3450-4258-9ebf-d2f9da9dbf53\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Freecam\",\n                    \"type\": \"Button\",\n                    \"id\": \"4c287985-2172-4603-910d-4655ea5b7125\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"978bfe49-cc26-4a3d-ab7b-7d7a29327403\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"StickDeadzone\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"00ca640b-d935-4593-8157-c05846ea39b3\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"e2062cb9-1b15-46a2-838c-2f8d72a0bdd9\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"8180e8bd-4097-4f4e-ab88-4523101a6ce9\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"320bffee-a40b-4347-ac70-c210eb8bc73a\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"1c5327b5-f71c-4f60-99c7-4e737386f1d1\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"d2581a9b-1d11-4566-b27d-b92aff5fabbc\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"2e46982e-44cc-431b-9f0b-c11910bf467a\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcfe95b8-67b9-4526-84b5-5d0bc98d6400\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"77bff152-3580-4b21-b6de-dcd0c7e41164\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1635d3fe-58b6-4ba9-a4e2-f4b964f6b5c8\",\n                    \"path\": \"<XRController>/{Primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3ea4d645-4504-4529-b061-ab81934c3752\",\n                    \"path\": \"<Joystick>/stick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Move\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c1f7a91b-d0fd-4a62-997e-7fb9b69bf235\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"StickDeadzone\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8c8e490b-c610-4785-884f-f04217b23ca4\",\n                    \"path\": \"<Pointer>/delta\",\n                    \"interactions\": \"\",\n                    \"processors\": \"ScaleVector2(x=0.1,y=0.1)\",\n                    \"groups\": \";Keyboard&Mouse;Touch\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3e5f5442-8668-4b27-a940-df99bad7e831\",\n                    \"path\": \"<Joystick>/{Hatswitch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Look\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"143bb1cd-cc10-4eca-a2f0-a3664166fe91\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"05f6913d-c316-48b2-a6bb-e225f14c7960\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"886e731e-7071-4ae4-95c0-e61739dad6fd\",\n                    \"path\": \"<Touchscreen>/primaryTouch/tap\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Touch\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ee3d0cd2-254e-47a7-a8cb-bc94d9658c54\",\n                    \"path\": \"<Joystick>/trigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8255d333-5683-4943-a58a-ccb207ff1dce\",\n                    \"path\": \"<XRController>/{PrimaryAction}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Fire\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"851a0d38-cfee-4065-80b0-913f54202046\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Jump\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"64d8c847-068c-4a9c-b95a-cd092218c51e\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Jump\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b441b993-f75f-4787-a277-b3d9ab6ca777\",\n                    \"path\": \"<Keyboard>/leftShift\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Sprint\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b54ea346-dc8d-44b8-92eb-6077272267eb\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Sprint\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0aa2f8c8-24b4-48a1-a311-5242db02533a\",\n                    \"path\": \"<Keyboard>/ctrl\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Crouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"16c37e4a-aa8b-4c1e-a408-42d5119731bc\",\n                    \"path\": \"<Gamepad>/leftStickPress\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Crouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"09014bf0-383f-41c4-a810-b73a3c6d8b03\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Activate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7ea8d408-5aa9-49af-948a-4115418a5916\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Activate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"42a0a2e3-fdbf-44fb-82d3-ec439e9d5447\",\n                    \"path\": \"<Keyboard>/p\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Freecam\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"UI\",\n            \"id\": \"95835912-0b90-4e99-b09d-fabfdb102340\",\n            \"actions\": [\n                {\n                    \"name\": \"Click\",\n                    \"type\": \"Button\",\n                    \"id\": \"cc8bbb68-334b-4504-8aeb-1e2fca54aa0a\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Point\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"a0f763c1-73d0-4a05-b86e-bc4ced757249\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"Value\",\n                    \"id\": \"6576c4c3-8174-4b64-bb85-0d933d240847\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Move UI\",\n                    \"type\": \"Value\",\n                    \"id\": \"f8efc406-bd48-4410-b4a3-489450fd3393\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Submit\",\n                    \"type\": \"Button\",\n                    \"id\": \"ae4ed172-c32f-4847-af73-8c4b7de7cc2e\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cancel\",\n                    \"type\": \"Button\",\n                    \"id\": \"fa66647b-672c-4ad8-a675-89ea075f1033\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"ScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"ca8dec9b-3746-401d-9448-bd8c03d0ea72\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MiddleClick\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"b035d674-2f84-4eee-9244-eff9a239b078\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDevicePosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"e8f837de-5eb3-47cf-91d3-ed4d074f0966\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"TrackedDeviceOrientation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"81e8c864-7a6a-4970-abe6-359ed2511950\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Up\",\n                    \"type\": \"Button\",\n                    \"id\": \"b6c7dd65-39bb-49cc-aa25-b7061a750294\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Down\",\n                    \"type\": \"Button\",\n                    \"id\": \"36ea6cb1-23a0-490a-9415-4e25073c3313\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"Gamepad\",\n                    \"id\": \"809f371f-c5e2-4e7a-83a1-d867598f40dd\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"9144cbe6-05e1-4687-a6d7-24f99d23dd81\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"2db08d65-c5fb-421b-983f-c71163608d67\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"58748904-2ea9-4a80-8579-b500e6a76df8\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"8ba04515-75aa-45de-966d-393d9bbd1c14\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"712e721c-bdfb-4b23-a86c-a0d9fcfea921\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"fcd248ae-a788-4676-a12e-f4d81205600b\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"1f04d9bc-c50b-41a1-bfcc-afb75475ec20\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fb8277d4-c5cd-4663-9dc7-ee3f0b506d90\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Joystick\",\n                    \"id\": \"e25d9774-381c-4a61-b47c-7b6b299ad9f9\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"3db53b26-6601-41be-9887-63ac74e79d19\",\n                    \"path\": \"<Joystick>/stick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"0cb3e13e-3d90-4178-8ae6-d9c5501d653f\",\n                    \"path\": \"<Joystick>/stick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"0392d399-f6dd-4c82-8062-c1e9c0d34835\",\n                    \"path\": \"<Joystick>/stick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"942a66d9-d42f-43d6-8d70-ecb4ba5363bc\",\n                    \"path\": \"<Joystick>/stick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"Keyboard\",\n                    \"id\": \"ff527021-f211-4c02-933e-5976594c46ed\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"563fbfdd-0f09-408d-aa75-8642c4f08ef0\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"eb480147-c587-4a33-85ed-eb0ab9942c43\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"2bf42165-60bc-42ca-8072-8c13ab40239b\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"85d264ad-e0a0-4565-b7ff-1a37edde51ac\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"74214943-c580-44e4-98eb-ad7eebe17902\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"cea9b045-a000-445b-95b8-0c171af70a3b\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"8607c725-d935-4808-84b1-8354e29bab63\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"4cda81dc-9edd-4e03-9d7c-a71a14345d0b\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"77d3da7f-da29-4032-9764-e057c4b7e976\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"StickDeadzone\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"b265e9d6-bcdd-43a2-a22b-a3c534617162\",\n                    \"path\": \"Dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"47cb23ae-23ad-4e27-bb30-932fd9a1992d\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"b70728fd-d2da-4c31-8273-2f1d7e80d319\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"820f198d-894f-401e-b49b-00f899195abb\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"e159f6b1-a4a4-4065-b784-1f40fb825d9a\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"9326512e-cfda-4b03-8776-c37636c5f399\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"938a63ad-f517-460f-b700-cb0af90a5554\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"bbd0030f-875a-472e-9ce1-72e4dccaafa6\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"f3bfaa02-7a3d-4cf3-a207-324f22cdfcec\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a93fa2a0-5df6-4d00-8ee1-a28a14b822d6\",\n                    \"path\": \"<XRController>/{Primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9ce431a0-0968-49c8-802a-b494f0664639\",\n                    \"path\": \"<Joystick>/stick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Joystick\",\n                    \"action\": \"Move UI\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9e92bb26-7e3b-4ec4-b06b-3c8f8e498ddc\",\n                    \"path\": \"*/{Submit}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse;Gamepad;Touch;Joystick;XR\",\n                    \"action\": \"Submit\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"82627dcc-3b13-4ba9-841d-e4b746d6553e\",\n                    \"path\": \"*/{Cancel}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Touch;Joystick;XR\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5e8fefb1-455c-4618-801c-8d7d4424f260\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fe5daf77-0655-4c72-a9ee-d2f94b144063\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0c61fbc8-9069-4f20-934e-fa369dc63c38\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cancel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"38c99815-14ea-4617-8627-164d27641299\",\n                    \"path\": \"<Mouse>/scroll\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"ScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"99cdb8cc-2ae0-4e5c-835e-11b7c5c71950\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"ScaleVector2(x=0.1,y=0.1)\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"ScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"24066f69-da47-44f3-a07e-0015fb02eb2e\",\n                    \"path\": \"<Mouse>/middleButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"MiddleClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7236c0d9-6ca3-47cf-a6ee-a97f5b59ea77\",\n                    \"path\": \"<XRController>/devicePosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"TrackedDevicePosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"23e01e3a-f935-4948-8d8b-9bcac77714fb\",\n                    \"path\": \"<XRController>/deviceRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"TrackedDeviceOrientation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1999e7e5-6270-4a58-a989-7116bcdbe468\",\n                    \"path\": \"<VirtualMouse>/{Point}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"61866afa-625b-4297-8898-fb694f95190f\",\n                    \"path\": \"<Mouse>/{Point}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Point\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"891f2e7c-fcd9-49a1-a78c-26b4963932d9\",\n                    \"path\": \"<VirtualMouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6b019390-681b-4900-ac0c-37be0a43a4b0\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f3b5278a-2173-4200-a522-ef92e6befa5e\",\n                    \"path\": \"<Gamepad>/dpad/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Up\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"25dc18e4-e8de-4e60-ba5b-6aaf9b54c5f4\",\n                    \"path\": \"<Gamepad>/dpad/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Down\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Universal\",\n            \"id\": \"bf676b2e-2599-4f03-a50c-392a32e104d9\",\n            \"actions\": [\n                {\n                    \"name\": \"PointerDelta\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"2df2a503-0730-434e-866c-e2be707ccec0\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"StickDeadzone,ScaleVector2\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Navigate\",\n                    \"type\": \"Value\",\n                    \"id\": \"c506c268-6e97-4951-81c7-b9a27325c187\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerPosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"bdff7940-6d5c-4e0f-9019-c39e6ed0de33\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PrimaryClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"e732daad-b2ee-4128-aa48-3b58c9223f19\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"SecondaryClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"38ba0e71-527d-452c-9665-f2b81c68a526\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Tertiary click\",\n                    \"type\": \"Button\",\n                    \"id\": \"c0cc6031-5530-49f9-a18b-61b4ea6973db\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Toggle Clipboard\",\n                    \"type\": \"Button\",\n                    \"id\": \"e8c9b384-1c96-491a-bdcd-d7d82a0a2b7a\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Focuse Clipboard\",\n                    \"type\": \"Button\",\n                    \"id\": \"4c878f27-2d84-4280-9963-2cb648427133\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Escape\",\n                    \"type\": \"Button\",\n                    \"id\": \"ecf9ba32-d1c6-4073-ad23-83b0dbc04b96\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"FreecamScrollWheel\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"981e5743-e0fe-4282-b7f4-4e2aa6efbd92\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Unequip Gasmask\",\n                    \"type\": \"Button\",\n                    \"id\": \"dd3b9f15-85c3-4c31-83dc-f24a1cd6321b\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cinamatic - HideCursorToggle\",\n                    \"type\": \"Button\",\n                    \"id\": \"04b983b4-ee15-48d4-93bc-aad582150faa\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cinamatic - AutoReload\",\n                    \"type\": \"Button\",\n                    \"id\": \"4a49b6ad-e9ca-4811-aae6-4b982e9f1451\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold(duration=1,pressPoint=1)\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cinamatic - Light Switch\",\n                    \"type\": \"Button\",\n                    \"id\": \"344f0fe9-0948-4761-b56e-be3e59fc7ea4\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cinamatic - Swing Force\",\n                    \"type\": \"Button\",\n                    \"id\": \"8acff8d4-639a-41a6-a88f-dd649d159085\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cheat - Reveal all on map\",\n                    \"type\": \"Button\",\n                    \"id\": \"03e72ffd-19e9-4ef6-893a-c503ea92cf8c\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold(duration=1)\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cheat - Impact F9\",\n                    \"type\": \"Button\",\n                    \"id\": \"830e42a3-1dc4-4744-ae1b-5f1b65360bff\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold(duration=1)\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cheat - Impact F10\",\n                    \"type\": \"Button\",\n                    \"id\": \"7592ba23-7856-4990-b594-7e336f2751a3\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold(duration=1)\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cheat - Impact F11\",\n                    \"type\": \"Button\",\n                    \"id\": \"dff08519-cecb-4ee4-b94a-9d6f63483c43\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold(duration=1)\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Rotate Left\",\n                    \"type\": \"Button\",\n                    \"id\": \"06cdd15b-b254-4f90-9eac-2f504672b35c\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Rotate Right\",\n                    \"type\": \"Button\",\n                    \"id\": \"c0ce738f-8220-4357-ada8-6c10385a0a61\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Cinamatic - 4k Screenshot\",\n                    \"type\": \"Button\",\n                    \"id\": \"cdca7094-e1d5-4a4e-88ff-3eab85fcce70\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Continue - Enter\",\n                    \"type\": \"Button\",\n                    \"id\": \"53587d7e-4034-4617-a74c-5dfe71bff777\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PickUp\",\n                    \"type\": \"Button\",\n                    \"id\": \"a16dfd9d-3105-463c-9b7b-5ad3bb2ba855\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Interact\",\n                    \"type\": \"Button\",\n                    \"id\": \"225471cf-7284-4aac-8151-6aa07e4533b6\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SlowCursor\",\n                    \"type\": \"Button\",\n                    \"id\": \"6ebab036-56ed-4376-b638-2653d8b272d1\",\n                    \"expectedControlType\": \"\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"a22b61f2-c952-4055-913f-401f925d6dcf\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"PointerDelta\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Gamepad\",\n                    \"id\": \"d5c09ef7-3dcb-4bbc-8931-e69ee629ffd3\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"f7eb7873-014c-4958-aeb2-5f0ebce16365\",\n                    \"path\": \"<Gamepad>/leftStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"96986604-9dfa-4597-9b45-c483f2704acc\",\n                    \"path\": \"<Gamepad>/rightStick/up\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"dfecfcaa-93a7-4003-98ed-64b2f8e220fd\",\n                    \"path\": \"<Gamepad>/leftStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"9fdf4dd7-e579-4e7b-ae14-0de138e0d5f1\",\n                    \"path\": \"<Gamepad>/rightStick/down\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"2d8b5bcb-0190-4558-8990-4f98a9050d88\",\n                    \"path\": \"<Gamepad>/leftStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"4a3b7b55-6887-42ee-b4b3-d19d4e263b88\",\n                    \"path\": \"<Gamepad>/rightStick/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"5b2e5a5a-3c0a-4176-a4bd-4a2ccad19506\",\n                    \"path\": \"<Gamepad>/leftStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"39ae495d-1430-40b3-869d-d59d0531ce99\",\n                    \"path\": \"<Gamepad>/rightStick/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a66e9b2b-41ba-4338-8fba-c95d95ce3201\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Navigate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e4596307-e19b-4715-8f2e-a3cc09aa8d87\",\n                    \"path\": \"<Mouse>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"PointerPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"31942676-f4c8-41ae-86eb-065602cbb2bc\",\n                    \"path\": \"<Pen>/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard&Mouse\",\n                    \"action\": \"PointerPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"042b0415-3392-4485-9cc2-d38b3791ea1f\",\n                    \"path\": \"<Touchscreen>/touch*/position\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Touch\",\n                    \"action\": \"PointerPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2fa29f4f-76e6-4a21-8522-1f04e402a874\",\n                    \"path\": \"<Mouse>/leftButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6ec095c5-6ae9-4815-8ed0-66d758d5a665\",\n                    \"path\": \"<Keyboard>/numpadMultiply\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7cee4861-8da6-4bf3-b6a2-ddf2dc1f54f7\",\n                    \"path\": \"<Pen>/tip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Touch;Gamepad\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"260aa4a5-64c2-4fa4-90d0-0ac6618d1d89\",\n                    \"path\": \"<Touchscreen>/touch*/press\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Touch\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f80b94b6-f84d-4563-84e8-bbef96877391\",\n                    \"path\": \"<XRController>/trigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"XR\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b7cfc926-6493-49ba-a55a-828b87202829\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"PrimaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8fbfae54-3964-4a7a-be28-b1f7741efb7b\",\n                    \"path\": \"<Mouse>/rightButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"SecondaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"46c915d7-23dd-457d-808a-6d8dbf685b15\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"SecondaryClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a2806590-b9db-4296-832d-1b6b522f22e4\",\n                    \"path\": \"<Keyboard>/capsLock\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Toggle Clipboard\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ef038207-2a5f-4944-bf08-880ff3f24ab4\",\n                    \"path\": \"<Gamepad>/dpad/left\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Toggle Clipboard\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b955cd2e-95a2-435a-b4b2-f815185ecba1\",\n                    \"path\": \"<Keyboard>/tab\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Focuse Clipboard\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4cf1248a-f6e1-4845-bca6-e1d11084a1b3\",\n                    \"path\": \"<Gamepad>/dpad/right\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Focuse Clipboard\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9c8aac2a-51e0-4620-b7c2-3b09072d1259\",\n                    \"path\": \"<Keyboard>/escape\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Escape\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3c640d27-f73d-43ee-ae28-baef73357482\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Escape\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a8e944c3-1d15-4895-9d0f-7abe64ce65cf\",\n                    \"path\": \"<Keyboard>/f1\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cinamatic - AutoReload\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e2e69c92-bbcc-4859-a428-995646891570\",\n                    \"path\": \"<Mouse>/scroll\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"FreecamScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"744cc2db-2f4f-4e2e-8ad8-c3b48f624395\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"ScaleVector2(x=0.1,y=0.1)\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"FreecamScrollWheel\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d1e9ca09-33cd-4840-9a0b-0b001d686f40\",\n                    \"path\": \"<Keyboard>/g\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Unequip Gasmask\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"44140256-cc8c-40e2-b63d-3f8eb669a22d\",\n                    \"path\": \"<Gamepad>/buttonEast\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Unequip Gasmask\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fc359151-f9bd-4bce-aafe-42baa137c92a\",\n                    \"path\": \"<Keyboard>/f2\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cinamatic - HideCursorToggle\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2a05adf8-789c-4ec4-9a14-9ab964cc1d8d\",\n                    \"path\": \"<Keyboard>/f3\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cinamatic - Light Switch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7dd2083f-af40-46ed-a117-004be1439f1c\",\n                    \"path\": \"<Keyboard>/f4\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cinamatic - Swing Force\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"22271d32-f35f-47d6-bfe5-f0d8aa01ab56\",\n                    \"path\": \"<Keyboard>/f10\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cheat - Impact F10\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e2645e78-5a9f-47d5-aa61-2e6cfcd79a89\",\n                    \"path\": \"<Keyboard>/f11\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cheat - Impact F11\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b5e45af3-dfe6-4329-9b8c-023ab7dc37a7\",\n                    \"path\": \"<Keyboard>/f9\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cheat - Impact F9\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b67cbe00-09d6-4773-940c-8eb444adb026\",\n                    \"path\": \"<Keyboard>/f8\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cheat - Reveal all on map\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d6f6980f-ec72-4286-8cba-8253e58c6a86\",\n                    \"path\": \"<Keyboard>/numpad4\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate Left\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7ab3419a-97a0-484e-9b1d-5d5eb80ea8a2\",\n                    \"path\": \"<Keyboard>/numpad6\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate Right\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"83d7876d-571d-49a3-aed0-91642d47e8ca\",\n                    \"path\": \"<Keyboard>/f6\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Cinamatic - 4k Screenshot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"10695721-2170-448e-be7e-7e252eda76f5\",\n                    \"path\": \"<Keyboard>/enter\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Continue - Enter\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"effe4c51-076a-43df-bd49-b9a9572f7253\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Continue - Enter\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bba50864-bcd3-4332-b55f-55c7368327b4\",\n                    \"path\": \"<Keyboard>/f\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"PickUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4b039603-4b7f-4331-8345-b28a8f49fa5c\",\n                    \"path\": \"<Gamepad>/buttonWest\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"PickUp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d8e3c6c1-f365-4f91-b715-e8ce17463efd\",\n                    \"path\": \"<Mouse>/middleButton\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Tertiary click\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fa63ff1c-eb6a-4db9-903d-62fb383f0cea\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Keyboard&Mouse\",\n                    \"action\": \"Interact\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"27c282e3-9ce5-4696-b7c3-aadae9247707\",\n                    \"path\": \"<Gamepad>/buttonSouth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"Interact\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7424cb9f-de75-481d-b40b-290d0b4688c6\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \";Gamepad\",\n                    \"action\": \"SlowCursor\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": [\n        {\n            \"name\": \"Keyboard&Mouse\",\n            \"bindingGroup\": \"Keyboard&Mouse\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Keyboard>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                },\n                {\n                    \"devicePath\": \"<Mouse>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Gamepad\",\n            \"bindingGroup\": \"Gamepad\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Gamepad>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Touch\",\n            \"bindingGroup\": \"Touch\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Touchscreen>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Joystick\",\n            \"bindingGroup\": \"Joystick\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Joystick>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"XR\",\n            \"bindingGroup\": \"XR\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<XRController>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        }\n    ]\n}");
		_003Casset_003Ek__BackingField = inputActionAsset;
		InputActionMap player = _003Casset_003Ek__BackingField.FindActionMap("Player", throwIfNotFound: true);
		m_Player = player;
		InputAction player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
		m_Player_Move = player_Move;
		InputAction player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
		m_Player_Look = player_Look;
		InputAction player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
		m_Player_Fire = player_Fire;
		InputAction player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
		m_Player_Jump = player_Jump;
		InputAction player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
		m_Player_Sprint = player_Sprint;
		InputAction player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
		m_Player_Crouch = player_Crouch;
		InputAction player_Activate = m_Player.FindAction("Activate", throwIfNotFound: true);
		m_Player_Activate = player_Activate;
		InputAction player_Freecam = m_Player.FindAction("Freecam", throwIfNotFound: true);
		m_Player_Freecam = player_Freecam;
		InputActionMap uI = _003Casset_003Ek__BackingField.FindActionMap("UI", throwIfNotFound: true);
		m_UI = uI;
		InputAction uI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
		m_UI_Click = uI_Click;
		InputAction uI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
		m_UI_Point = uI_Point;
		InputAction uI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
		m_UI_Navigate = uI_Navigate;
		InputAction uI_MoveUI = m_UI.FindAction("Move UI", throwIfNotFound: true);
		m_UI_MoveUI = uI_MoveUI;
		InputAction uI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
		m_UI_Submit = uI_Submit;
		InputAction uI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
		m_UI_Cancel = uI_Cancel;
		InputAction uI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
		m_UI_ScrollWheel = uI_ScrollWheel;
		InputAction uI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
		m_UI_MiddleClick = uI_MiddleClick;
		InputAction uI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
		m_UI_TrackedDevicePosition = uI_TrackedDevicePosition;
		InputAction uI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
		m_UI_TrackedDeviceOrientation = uI_TrackedDeviceOrientation;
		InputAction uI_Up = m_UI.FindAction("Up", throwIfNotFound: true);
		m_UI_Up = uI_Up;
		InputAction uI_Down = m_UI.FindAction("Down", throwIfNotFound: true);
		m_UI_Down = uI_Down;
		InputActionMap universal = _003Casset_003Ek__BackingField.FindActionMap("Universal", throwIfNotFound: true);
		m_Universal = universal;
		InputAction universal_PointerDelta = m_Universal.FindAction("PointerDelta", throwIfNotFound: true);
		m_Universal_PointerDelta = universal_PointerDelta;
		InputAction universal_Navigate = m_Universal.FindAction("Navigate", throwIfNotFound: true);
		m_Universal_Navigate = universal_Navigate;
		InputAction universal_PointerPosition = m_Universal.FindAction("PointerPosition", throwIfNotFound: true);
		m_Universal_PointerPosition = universal_PointerPosition;
		InputAction universal_PrimaryClick = m_Universal.FindAction("PrimaryClick", throwIfNotFound: true);
		m_Universal_PrimaryClick = universal_PrimaryClick;
		InputAction universal_SecondaryClick = m_Universal.FindAction("SecondaryClick", throwIfNotFound: true);
		m_Universal_SecondaryClick = universal_SecondaryClick;
		InputAction universal_Tertiaryclick = m_Universal.FindAction("Tertiary click", throwIfNotFound: true);
		m_Universal_Tertiaryclick = universal_Tertiaryclick;
		InputAction universal_ToggleClipboard = m_Universal.FindAction("Toggle Clipboard", throwIfNotFound: true);
		m_Universal_ToggleClipboard = universal_ToggleClipboard;
		InputAction universal_FocuseClipboard = m_Universal.FindAction("Focuse Clipboard", throwIfNotFound: true);
		m_Universal_FocuseClipboard = universal_FocuseClipboard;
		InputAction universal_Escape = m_Universal.FindAction("Escape", throwIfNotFound: true);
		m_Universal_Escape = universal_Escape;
		InputAction universal_FreecamScrollWheel = m_Universal.FindAction("FreecamScrollWheel", throwIfNotFound: true);
		m_Universal_FreecamScrollWheel = universal_FreecamScrollWheel;
		InputAction universal_UnequipGasmask = m_Universal.FindAction("Unequip Gasmask", throwIfNotFound: true);
		m_Universal_UnequipGasmask = universal_UnequipGasmask;
		InputAction universal_CinamaticHideCursorToggle = m_Universal.FindAction("Cinamatic - HideCursorToggle", throwIfNotFound: true);
		m_Universal_CinamaticHideCursorToggle = universal_CinamaticHideCursorToggle;
		InputAction universal_CinamaticAutoReload = m_Universal.FindAction("Cinamatic - AutoReload", throwIfNotFound: true);
		m_Universal_CinamaticAutoReload = universal_CinamaticAutoReload;
		InputAction universal_CinamaticLightSwitch = m_Universal.FindAction("Cinamatic - Light Switch", throwIfNotFound: true);
		m_Universal_CinamaticLightSwitch = universal_CinamaticLightSwitch;
		InputAction universal_CinamaticSwingForce = m_Universal.FindAction("Cinamatic - Swing Force", throwIfNotFound: true);
		m_Universal_CinamaticSwingForce = universal_CinamaticSwingForce;
		InputAction universal_CheatRevealallonmap = m_Universal.FindAction("Cheat - Reveal all on map", throwIfNotFound: true);
		m_Universal_CheatRevealallonmap = universal_CheatRevealallonmap;
		InputAction universal_CheatImpactF = m_Universal.FindAction("Cheat - Impact F9", throwIfNotFound: true);
		m_Universal_CheatImpactF9 = universal_CheatImpactF;
		InputAction universal_CheatImpactF2 = m_Universal.FindAction("Cheat - Impact F10", throwIfNotFound: true);
		m_Universal_CheatImpactF10 = universal_CheatImpactF2;
		InputAction universal_CheatImpactF3 = m_Universal.FindAction("Cheat - Impact F11", throwIfNotFound: true);
		m_Universal_CheatImpactF11 = universal_CheatImpactF3;
		InputAction universal_RotateLeft = m_Universal.FindAction("Rotate Left", throwIfNotFound: true);
		m_Universal_RotateLeft = universal_RotateLeft;
		InputAction universal_RotateRight = m_Universal.FindAction("Rotate Right", throwIfNotFound: true);
		m_Universal_RotateRight = universal_RotateRight;
		InputAction universal_Cinamatic4kScreenshot = m_Universal.FindAction("Cinamatic - 4k Screenshot", throwIfNotFound: true);
		m_Universal_Cinamatic4kScreenshot = universal_Cinamatic4kScreenshot;
		InputAction universal_ContinueEnter = m_Universal.FindAction("Continue - Enter", throwIfNotFound: true);
		m_Universal_ContinueEnter = universal_ContinueEnter;
		InputAction universal_PickUp = m_Universal.FindAction("PickUp", throwIfNotFound: true);
		m_Universal_PickUp = universal_PickUp;
		InputAction universal_Interact = m_Universal.FindAction("Interact", throwIfNotFound: true);
		m_Universal_Interact = universal_Interact;
		InputAction universal_SlowCursor = m_Universal.FindAction("SlowCursor", throwIfNotFound: true);
		m_Universal_SlowCursor = universal_SlowCursor;
	}

	~InputActions()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}

	public void Dispose()
	{
		UnityEngine.Object.Destroy(_003Casset_003Ek__BackingField);
	}

	public bool Contains(InputAction action)
	{
		//IL_0045: Expected I4, but got O
		if ((object)_003Casset_003Ek__BackingField != null)
		{
			return _003Casset_003Ek__BackingField.Contains(action);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public IEnumerator<InputAction> GetEnumerator()
	{
		if ((object)_003Casset_003Ek__BackingField != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1816A9F40");
			IEnumerator<InputAction> result = default(IEnumerator<InputAction>);
			return result;
		}
		return (IEnumerator<InputAction>)new NullReferenceException();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		if ((object)_003Casset_003Ek__BackingField != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1816A9F40");
			IEnumerator result = default(IEnumerator);
			return result;
		}
		return (IEnumerator)new NullReferenceException();
	}

	public void Enable()
	{
		_003Casset_003Ek__BackingField.Enable();
	}

	public void Disable()
	{
		_003Casset_003Ek__BackingField.Disable();
	}

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		if ((object)_003Casset_003Ek__BackingField != null)
		{
			return _003Casset_003Ek__BackingField.FindAction(actionNameOrId, throwIfNotFound);
		}
		return (InputAction)(object)new NullReferenceException();
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		//IL_0093: Expected I4, but got O
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		if ((object)_003Casset_003Ek__BackingField != null)
		{
			_ = bindingMask.m_Name;
			_ = bindingMask.m_Path;
			_ = bindingMask.m_Processors;
			_ = bindingMask.m_Action;
			object obj = default(object);
			InputBinding mask = (InputBinding)(obj - 104);
			_ = bindingMask.m_OverridePath;
			_ = bindingMask.m_OverrideProcessors;
			return _003Casset_003Ek__BackingField.FindBinding(mask, out action);
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}
}
