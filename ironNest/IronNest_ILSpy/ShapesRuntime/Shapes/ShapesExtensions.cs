using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal static class ShapesExtensions
{
	private sealed class _003CZip_003Ed__14<T1, T2, T3, TResult> : IEnumerable<TResult>, IEnumerable, IEnumerator<TResult>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private TResult _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private IEnumerable<T1> source;

		public IEnumerable<T1> _003C_003E3__source;

		private IEnumerable<T2> second;

		public IEnumerable<T2> _003C_003E3__second;

		private IEnumerable<T3> third;

		public IEnumerable<T3> _003C_003E3__third;

		private Func<T1, T2, T3, TResult> func;

		public Func<T1, T2, T3, TResult> _003C_003E3__func;

		private IEnumerator<T1> _003Ce1_003E5__2;

		private IEnumerator<T2> _003Ce2_003E5__3;

		private IEnumerator<T3> _003Ce3_003E5__4;

		unsafe TResult IEnumerator<TResult>.Current
		{
			get
			{
				//IL_0008: Expected O, but got Ref
				//IL_0018: Expected O, but got I
				//IL_0037: Expected O, but got I
				//IL_004c: Expected O, but got I
				//IL_0062: Expected O, but got I
				//IL_00aa: Expected O, but got I
				//IL_00ba: Expected O, but got I
				//IL_00d8: Expected O, but got I
				object obj2 = default(object);
				object obj = (object)(&obj2);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ r9_v1+A8]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
				object obj6 = (nint)0 + (nint)15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
				if ((nint)obj6 > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rax_v7+C0]");
					object obj8 = 0;
					object obj9 = obj8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v8+80]");
					object obj10 = (nint)0 + (nint)32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				TResult result = default(TResult);
				return result;
			}
		}

		unsafe object IEnumerator.Current
		{
			get
			{
				//IL_0008: Expected O, but got Ref
				//IL_002d: Expected O, but got I
				//IL_0043: Expected O, but got I
				//IL_00a5: Expected O, but got I
				object obj2 = default(object);
				object obj = (object)(&obj2);
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ r8_v1 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+A8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rax_v2+FC]");
				object obj4 = (nint)0 + (nint)15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rax_v2+FC]");
				if ((nint)obj4 > 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
					nint num2 = 0;
					IntPtr intPtr = num2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v8 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
					object obj5 = (nint)0 + (nint)32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				}
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object result = default(object);
				return result;
			}
		}

		public _003CZip_003Ed__14(int _003C_003E1__state)
		{
			//IL_0024: Expected O, but got I
			//IL_0045: Expected O, but got I4
			//IL_007c: Expected O, but got I
			//IL_0092: Expected O, but got I
			//IL_00b7: Expected O, but got I
			base._002Ector();
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v3 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v6 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v6 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj4 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v6 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj5 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj7 = default(object);
			object obj6 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		}

		void IDisposable.Dispose()
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Expected O, but got Unknown
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			//IL_0188: Expected O, but got I
			//IL_0198: Expected O, but got I
			//IL_01b2: Expected O, but got I
			//IL_01c2: Expected O, but got I
			//IL_01e1: Expected O, but got I
			//IL_01f1: Expected O, but got I
			//IL_00c1: Expected O, but got I
			//IL_00d1: Expected O, but got I
			//IL_012a: Expected O, but got I
			//IL_013a: Expected O, but got I
			//IL_0154: Expected O, but got I
			//IL_0164: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			object obj = obj2 + 5;
			if ((nint)obj <= 2 || (nint)obj2 == 1)
			{
				object obj3 = obj2 + 5;
				if ((nint)obj3 > 1 && (nint)obj2 != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ stack_10_v3+20]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rcx_v13+C0]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AAB0");
				}
				else if ((nint)obj2 != -5 && (nint)obj2 != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_10_v4+20]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rcx_v9+C0]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004ABA0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_10_v4+20]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ rcx_v11+C0]");
					object obj9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AAB0");
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_10_v4+20]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rcx_v3+C0]");
					object obj11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AC90");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_10_v4+20]");
					object obj12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ rcx_v5+C0]");
					object obj13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004ABA0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ stack_10_v4+20]");
					object obj14 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rcx_v7+C0]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AAB0");
				}
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_0fb3: Expected O, but got I
			//IL_0fc9: Expected O, but got I
			//IL_0fdf: Expected O, but got I
			//IL_128d: Expected O, but got Ref
			//IL_12a3: Expected O, but got I
			//IL_12b9: Expected O, but got I
			//IL_1010: Expected O, but got Ref
			//IL_1026: Expected O, but got I
			//IL_103c: Expected O, but got I
			//IL_12e8: Expected O, but got I
			//IL_12f8: Expected O, but got I
			//IL_1308: Expected O, but got I
			//IL_131e: Expected O, but got I
			//IL_1338: Expected O, but got Ref
			//IL_106d: Expected O, but got Ref
			//IL_1083: Expected O, but got I
			//IL_11d8: Expected O, but got I
			//IL_11e8: Expected O, but got I
			//IL_11f8: Expected O, but got I
			//IL_1216: Expected O, but got I
			//IL_1230: Expected O, but got I
			//IL_1240: Expected O, but got I
			//IL_1250: Expected O, but got I
			//IL_10cb: Expected O, but got Ref
			//IL_10de: Expected O, but got Ref
			//IL_10f3: Expected O, but got I
			//IL_1103: Expected O, but got I
			//IL_1113: Expected O, but got I
			//IL_001d: Expected O, but got I
			//IL_002d: Expected O, but got I
			//IL_003d: Expected O, but got I
			//IL_0055: Expected O, but got I
			//IL_0657: Expected O, but got I
			//IL_0667: Expected O, but got I
			//IL_0677: Expected O, but got I
			//IL_068f: Expected O, but got I
			//IL_007b: Expected O, but got I8
			//IL_0095: Expected O, but got I
			//IL_00a5: Expected O, but got I
			//IL_00b5: Expected O, but got I
			//IL_00d3: Expected O, but got I
			//IL_06b5: Expected O, but got I8
			//IL_06c7: Expected O, but got Ref
			//IL_06d0: Expected O, but got I4
			//IL_0102: Expected O, but got I4
			//IL_06e5: Expected O, but got I
			//IL_06f5: Expected O, but got I
			//IL_0705: Expected O, but got I
			//IL_0723: Expected O, but got I
			//IL_0120: Expected O, but got I
			//IL_0130: Expected O, but got I
			//IL_0140: Expected O, but got I
			//IL_015f: Expected O, but got I
			//IL_016f: Expected O, but got I
			//IL_017f: Expected O, but got I
			//IL_0197: Expected O, but got I
			//IL_01ad: Expected O, but got I
			//IL_0c6e: Expected O, but got I
			//IL_0c7e: Expected O, but got I
			//IL_0c8e: Expected O, but got I
			//IL_0ca8: Expected O, but got I
			//IL_0cb8: Expected O, but got I
			//IL_0cc8: Expected O, but got I
			//IL_0ce0: Expected O, but got I
			//IL_0cf6: Expected O, but got I
			//IL_01d2: Expected O, but got I
			//IL_0d1b: Expected O, but got I
			//IL_0d2e: Expected O, but got I4
			//IL_0789: Expected O, but got I
			//IL_0799: Expected O, but got I
			//IL_07a9: Expected O, but got I
			//IL_07c7: Expected O, but got I
			//IL_0203: Expected O, but got I
			//IL_0213: Expected O, but got I
			//IL_0223: Expected O, but got I
			//IL_023b: Expected O, but got I
			//IL_0d4d: Expected O, but got I
			//IL_0d5d: Expected O, but got I
			//IL_0d6d: Expected O, but got I
			//IL_0d87: Expected O, but got I
			//IL_0d97: Expected O, but got I
			//IL_0da7: Expected O, but got I
			//IL_0dbf: Expected O, but got I
			//IL_0dd5: Expected O, but got I
			//IL_0261: Expected O, but got I8
			//IL_027b: Expected O, but got I
			//IL_028b: Expected O, but got I
			//IL_029b: Expected O, but got I
			//IL_02b9: Expected O, but got I
			//IL_0dfa: Expected O, but got I
			//IL_0e0d: Expected O, but got I4
			//IL_02e8: Expected O, but got I4
			//IL_0e2c: Expected O, but got I
			//IL_0e3c: Expected O, but got I
			//IL_0e4c: Expected O, but got I
			//IL_0e66: Expected O, but got I
			//IL_0e76: Expected O, but got I
			//IL_0e86: Expected O, but got I
			//IL_0e9e: Expected O, but got I
			//IL_0eb4: Expected O, but got I
			//IL_0ed9: Expected O, but got I
			//IL_0eec: Expected O, but got I4
			//IL_082d: Expected O, but got I
			//IL_083d: Expected O, but got I
			//IL_084d: Expected O, but got I
			//IL_086b: Expected O, but got I
			//IL_0306: Expected O, but got I
			//IL_0316: Expected O, but got I
			//IL_0326: Expected O, but got I
			//IL_0345: Expected O, but got I
			//IL_0355: Expected O, but got I
			//IL_0365: Expected O, but got I
			//IL_037d: Expected O, but got I
			//IL_0393: Expected O, but got I
			//IL_03b8: Expected O, but got I
			//IL_03e9: Expected O, but got I
			//IL_03f9: Expected O, but got I
			//IL_0409: Expected O, but got I
			//IL_0421: Expected O, but got I
			//IL_08d1: Expected O, but got I
			//IL_08e1: Expected O, but got I
			//IL_08f1: Expected O, but got I
			//IL_090f: Expected O, but got I
			//IL_0931: Expected O, but got I
			//IL_0941: Expected O, but got I
			//IL_0951: Expected O, but got I
			//IL_096f: Expected O, but got I
			//IL_0447: Expected O, but got I8
			//IL_0461: Expected O, but got I
			//IL_0471: Expected O, but got I
			//IL_0481: Expected O, but got I
			//IL_049f: Expected O, but got I
			//IL_04be: Expected O, but got I
			//IL_04ce: Expected O, but got I
			//IL_04de: Expected O, but got I
			//IL_04fd: Expected O, but got I
			//IL_050d: Expected O, but got I
			//IL_051d: Expected O, but got I
			//IL_0535: Expected O, but got I
			//IL_054b: Expected O, but got I
			//IL_09b3: Expected O, but got I
			//IL_09c3: Expected O, but got I
			//IL_09d3: Expected O, but got I
			//IL_0570: Expected O, but got I
			//IL_09f2: Expected O, but got I
			//IL_0a02: Expected O, but got I
			//IL_0a12: Expected O, but got I
			//IL_0a30: Expected O, but got I
			//IL_05a1: Expected O, but got I
			//IL_05b1: Expected O, but got I
			//IL_05c1: Expected O, but got I
			//IL_05d9: Expected O, but got I
			//IL_0a56: Expected O, but got Ref
			//IL_05ff: Expected O, but got I8
			//IL_0619: Expected O, but got I
			//IL_0622: Expected O, but got I4
			//IL_0a84: Expected O, but got I
			//IL_0a94: Expected O, but got I
			//IL_0aa4: Expected O, but got I
			//IL_0ac3: Expected O, but got I
			//IL_0ad3: Expected O, but got I
			//IL_0ae3: Expected O, but got I
			//IL_0b01: Expected O, but got I
			//IL_0b27: Expected O, but got Ref
			//IL_0b4d: Expected O, but got I
			//IL_0b5d: Expected O, but got I
			//IL_0b6d: Expected O, but got I
			//IL_0b93: Expected O, but got Ref
			//IL_0ba3: Expected O, but got I
			//IL_0bc9: Expected O, but got I
			//IL_0bd9: Expected O, but got I
			//IL_0be9: Expected O, but got I
			//IL_0bf9: Expected O, but got I
			//IL_0c0f: Expected O, but got I
			//IL_0c29: Expected O, but got Ref
			//IL_1160: Expected O, but got I
			//IL_1170: Expected O, but got I
			//IL_1180: Expected O, but got I
			//IL_1196: Expected O, but got I
			//IL_11b0: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v2 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+A8]");
			object obj3 = 0;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rcx_v3 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+78]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v5+FC]");
			object obj5 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v5+FC]");
			object obj6 = default(object);
			if ((nint)obj5 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				obj6 = (object)(&obj2);
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v5 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+88]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v11+FC]");
				object obj8 = (nint)0 + (nint)15;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v11+FC]");
				if ((nint)obj8 <= 0)
				{
					goto IL_12d8;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			object obj9 = (object)(&obj2);
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rcx_v7 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+98]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v17+FC]");
			object obj11 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rax_v17+FC]");
			if ((nint)obj11 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			object obj12 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3+FC]");
			object obj13 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3+FC]");
			if ((nint)obj13 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			_ = 0;
			_ = 0;
			object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 112));
			object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ rax_v28+20]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rdx_v2+C0]");
			object obj18 = 0;
			object obj19 = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj20 = default(object);
			object obj31;
			object obj34;
			if (obj20 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj21 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rax_v136+20]");
				object obj22 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ rcx_v85+C0]");
				object obj23 = 0;
				object obj24 = obj23;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rcx_v86+80]");
				object obj25 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj26 = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj27 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v292 @ rax_v141+20]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v293 @ rdx_v76+C0]");
				object obj29 = 0;
				object obj30 = obj29;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ rdx_v77+80]");
				obj31 = (nint)0 + (nint)96;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj33 = default(object);
				object obj32 = obj33;
				bool flag = obj33 == null;
				obj34 = 0;
				if (flag)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj35 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v404 @ rax_v146+20]");
				object obj36 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ rcx_v92+C0]");
				object obj37 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj38 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v535 @ rcx_v95+20]");
				object obj39 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v536 @ rdx_v82+C0]");
				object obj40 = 0;
				object obj41 = obj40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rdx_v83+80]");
				object obj42 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rdx_v83+80]");
				object obj43 = (nint)0 + (nint)352;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rdx_v83+80]");
				object obj44 = (nint)0 + (nint)352;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj46 = default(object);
				object obj45 = obj46;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj47 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v733 @ rax_v154+20]");
				object obj48 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v734 @ rcx_v100+C0]");
				object obj49 = 0;
				object obj50 = obj49;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v736 @ rcx_v101+80]");
				object obj51 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj52 = 4294967293L;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj53 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v800 @ rax_v159+20]");
				object obj54 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v801 @ rdx_v90+C0]");
				object obj55 = 0;
				object obj56 = obj55;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v803 @ rdx_v91+80]");
				obj31 = (nint)0 + (nint)160;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj57 = default(object);
				obj32 = obj57;
				bool flag2 = obj57 == null;
				obj34 = 0;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj58 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v830 @ rax_v164+20]");
				object obj59 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v831 @ rcx_v107+C0]");
				object obj60 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj61 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v885 @ rcx_v110+20]");
				object obj62 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v886 @ rdx_v96+C0]");
				object obj63 = 0;
				object obj64 = obj63;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ rdx_v97+80]");
				object obj65 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ rdx_v97+80]");
				object obj66 = (nint)0 + (nint)384;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v888 @ rdx_v97+80]");
				object obj67 = (nint)0 + (nint)384;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj69 = default(object);
				object obj68 = obj69;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj70 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v926 @ rax_v172+20]");
				object obj71 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v927 @ rcx_v115+C0]");
				object obj72 = 0;
				object obj73 = obj72;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v929 @ rcx_v116+80]");
				object obj74 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj75 = 4294967292L;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj76 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v951 @ rax_v177+20]");
				object obj77 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v952 @ rdx_v104+C0]");
				object obj78 = 0;
				object obj79 = obj78;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v954 @ rdx_v105+80]");
				object obj80 = (nint)0 + (nint)224;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj81 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v976 @ rax_v181+20]");
				object obj82 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v977 @ rcx_v121+C0]");
				object obj83 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj84 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v995 @ rcx_v124+20]");
				object obj85 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v996 @ rdx_v109+C0]");
				object obj86 = 0;
				object obj87 = obj86;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v998 @ rdx_v110+80]");
				object obj88 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v998 @ rdx_v110+80]");
				object obj89 = (nint)0 + (nint)416;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v998 @ rdx_v110+80]");
				object obj90 = (nint)0 + (nint)416;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj92 = default(object);
				object obj91 = obj92;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj93 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rax_v189+20]");
				object obj94 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1027 @ rcx_v129+C0]");
				object obj95 = 0;
				object obj96 = obj95;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1029 @ rcx_v130+80]");
				obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj97 = 4294967291L;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+88]");
				object obj98 = 0;
				object obj99 = 0;
			}
			else
			{
				if ((nint)obj20 != 1)
				{
					_ = 0;
					return false;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj100 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ rax_v131+20]");
				object obj101 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rcx_v80+C0]");
				object obj102 = 0;
				object obj103 = obj102;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rcx_v81+80]");
				object obj32 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj104 = 4294967291L;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				object obj98 = (object)(&obj2);
				object obj99 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
			object obj105 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ rax_v50+20]");
			object obj106 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rdx_v14+C0]");
			object obj107 = 0;
			object obj108 = obj107;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rdx_v15+80]");
			obj31 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj109 = default(object);
			obj34 = obj109;
			if (obj109 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj110 = default(object);
				if (obj110 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
					object obj111 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v631 @ rax_v77+20]");
					object obj112 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rdx_v35+C0]");
					object obj113 = 0;
					object obj114 = obj113;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v634 @ rdx_v36+80]");
					obj31 = (nint)0 + (nint)384;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					object obj115 = default(object);
					obj34 = obj115;
					if (obj115 == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj116 = default(object);
					if (obj116 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
						object obj117 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v789 @ rax_v81+20]");
						object obj118 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v790 @ rdx_v40+C0]");
						object obj119 = 0;
						object obj120 = obj119;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v792 @ rdx_v41+80]");
						obj31 = (nint)0 + (nint)416;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						object obj121 = default(object);
						obj34 = obj121;
						if (obj121 == null)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						object obj122 = default(object);
						if (obj122 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
							object obj123 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v870 @ rax_v85+20]");
							object obj124 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v871 @ rdx_v45+C0]");
							object obj125 = 0;
							object obj126 = obj125;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v873 @ rdx_v46+80]");
							object obj127 = (nint)0 + (nint)288;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
							object obj129 = default(object);
							object obj128 = obj129;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
							object obj130 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v892 @ rax_v88+20]");
							object obj131 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v893 @ rdx_v49+C0]");
							object obj132 = 0;
							object obj133 = obj132;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v895 @ rdx_v50+80]");
							obj31 = (nint)0 + (nint)352;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
							object obj134 = default(object);
							bool flag3 = obj134 == null;
							object obj32 = obj134;
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
								object obj135 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v913 @ rax_v91+20]");
								object obj136 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v914 @ rcx_v51+C0]");
								object obj137 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
								object obj138 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v943 @ rax_v96+20]");
								object obj139 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v944 @ rdx_v54+C0]");
								object obj140 = 0;
								object obj141 = obj140;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v946 @ rdx_v55+80]");
								obj31 = (nint)0 + (nint)384;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
								object obj142 = default(object);
								bool flag4 = obj142 == null;
								object obj143 = (object)(&obj2);
								obj32 = obj142;
								obj34 = obj134;
								if (!flag4)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
									object obj144 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v957 @ rax_v99+20]");
									object obj145 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v958 @ rcx_v55+C0]");
									object obj146 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
									object obj147 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v981 @ rax_v104+20]");
									object obj148 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v982 @ rdx_v59+C0]");
									object obj149 = 0;
									object obj150 = obj149;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v984 @ rdx_v60+80]");
									obj31 = (nint)0 + (nint)416;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
									object obj151 = default(object);
									bool flag5 = obj151 == null;
									obj143 = (object)(&obj2);
									obj34 = obj142;
									if (!flag5)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
										object obj152 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1003 @ rax_v107+20]");
										object obj153 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1004 @ rcx_v59+C0]");
										object obj154 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
										bool flag6 = obj129 == null;
										obj143 = (object)(&obj2);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1005 @ rax_v108+60]");
										obj31 = 0;
										obj34 = obj151;
										if (!flag6)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
											object obj155 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1032 @ rdx_v64+20]");
											object obj156 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1033 @ rax_v112+C0]");
											object obj157 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1034 @ rcx_v62+98]");
											object obj158 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1035 @ rax_v113+28]");
											object obj159 = (nint)0 >> 31;
											bool flag7 = obj159 != null;
											object obj160 = (object)(&obj2);
											if (!flag7)
											{
												obj160 = obj12;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1032 @ rdx_v64+20]");
											object obj161 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1053 @ rax_v114+C0]");
											object obj162 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1054 @ rcx_v65+88]");
											object obj163 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1055 @ rax_v115+28]");
											object obj164 = (nint)0 >> 31;
											bool flag8 = obj164 != null;
											object obj165 = (object)(&obj2);
											if (!flag8)
											{
												obj165 = obj9;
											}
											goto IL_12d8;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj166 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v671 @ rax_v55+20]");
				object obj167 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v672 @ rcx_v24+C0]");
				object obj168 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AC90");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj169 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v725 @ rax_v57+20]");
				object obj170 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v726 @ rcx_v26+C0]");
				object obj171 = 0;
				object obj172 = obj171;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ rcx_v27+80]");
				object obj173 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ rcx_v27+80]");
				object obj174 = (nint)0 + (nint)416;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ rcx_v27+80]");
				object obj175 = (nint)0 + (nint)416;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj176 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj177 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v794 @ rax_v62+20]");
				object obj178 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v795 @ rcx_v31+C0]");
				object obj179 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004ABA0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj180 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v808 @ rax_v64+20]");
				object obj181 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v809 @ rcx_v33+C0]");
				object obj182 = 0;
				object obj183 = obj182;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v811 @ rcx_v34+80]");
				object obj184 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v811 @ rcx_v34+80]");
				object obj185 = (nint)0 + (nint)384;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v811 @ rcx_v34+80]");
				object obj186 = (nint)0 + (nint)384;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj187 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj188 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v878 @ rax_v69+20]");
				object obj189 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ rcx_v38+C0]");
				object obj190 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004AAB0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
				object obj191 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v897 @ rax_v71+20]");
				object obj192 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v898 @ rcx_v40+C0]");
				object obj193 = 0;
				object obj194 = obj193;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v900 @ rcx_v41+80]");
				object obj195 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v900 @ rcx_v41+80]");
				object obj196 = (nint)0 + (nint)352;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v900 @ rcx_v41+80]");
				object obj197 = (nint)0 + (nint)352;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj198 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				_ = 0;
				return false;
			}
			throw new NullReferenceException();
			IL_12d8:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1032 @ rdx_v64+20]");
			object obj199 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1070 @ rax_v116+C0]");
			object obj200 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1071 @ rcx_v68+78]");
			object obj201 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1072 @ rax_v117+28]");
			object obj202 = (nint)0 >> 31;
			bool flag9 = obj202 != null;
			object obj203 = (object)(&obj2);
			if (!flag9)
			{
				obj203 = obj6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v219 @ rdi_v14+18] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
			object obj204 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1082 @ rax_v120+20]");
			object obj205 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1083 @ rcx_v72+C0]");
			object obj206 = 0;
			object obj207 = obj206;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1085 @ rcx_v73+80]");
			object obj208 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v20 @ rbp_v1+78]");
			object obj209 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1091 @ rax_v123+20]");
			object obj210 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1092 @ rcx_v75+C0]");
			object obj211 = 0;
			object obj212 = obj211;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180037D70");
			_ = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_00b1: Expected O, but got I
			//IL_00d2: Expected O, but got I8
			//IL_0029: Expected O, but got I
			//IL_0074: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v8 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj3 = (nint)0 + (nint)352;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				nint num3 = 0;
				IntPtr intPtr3 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v12 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj5 = (nint)0 + (nint)352;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		private void _003C_003Em__Finally2()
		{
			//IL_00b1: Expected O, but got I
			//IL_00d2: Expected O, but got I8
			//IL_0029: Expected O, but got I
			//IL_0074: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = 4294967293L;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v8 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj3 = (nint)0 + (nint)384;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				nint num3 = 0;
				IntPtr intPtr3 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v12 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj5 = (nint)0 + (nint)384;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		private void _003C_003Em__Finally3()
		{
			//IL_00b1: Expected O, but got I
			//IL_00d2: Expected O, but got I8
			//IL_0029: Expected O, but got I
			//IL_0074: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = 4294967292L;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v8 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj3 = (nint)0 + (nint)416;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				nint num3 = 0;
				IntPtr intPtr3 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v12 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj5 = (nint)0 + (nint)416;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			throw ex;
		}

		IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
		{
			//IL_0120: Expected O, but got I
			//IL_0062: Expected O, but got I
			//IL_013f: Expected O, but got I
			//IL_014f: Expected O, but got I
			//IL_0167: Expected O, but got I
			//IL_0189: Expected O, but got I4
			//IL_01b2: Expected O, but got I
			//IL_01c2: Expected O, but got I
			//IL_01da: Expected O, but got I
			//IL_01f0: Expected O, but got I
			//IL_0210: Expected O, but got I
			//IL_00be: Expected O, but got I
			//IL_0237: Expected O, but got I
			//IL_00e0: Expected O, but got I4
			//IL_00f0: Expected O, but got I
			//IL_04f2: Expected O, but got I
			//IL_0262: Expected O, but got I
			//IL_0278: Expected O, but got I
			//IL_0298: Expected O, but got I
			//IL_02dd: Expected O, but got I
			//IL_030a: Expected O, but got I
			//IL_0320: Expected O, but got I
			//IL_0340: Expected O, but got I
			//IL_0385: Expected O, but got I
			//IL_03b2: Expected O, but got I
			//IL_03c8: Expected O, but got I
			//IL_03e8: Expected O, but got I
			//IL_042d: Expected O, but got I
			//IL_045a: Expected O, but got I
			//IL_0470: Expected O, but got I
			//IL_0490: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj = default(object);
			bool flag = (nint)obj != -2;
			nint num2 = 0;
			_003CZip_003Ed__14<T1, T2, T3, TResult> obj8;
			object obj6;
			object obj7;
			if (!flag)
			{
				nint num3 = 0;
				IntPtr intPtr2 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v56 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj2 = (nint)0 + (nint)64;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj3 = default(object);
				object obj4 = default(object);
				bool flag2 = obj3 != obj4;
				num2 = 0;
				if (!flag2)
				{
					nint num4 = 0;
					IntPtr intPtr3 = num4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v60 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
					object obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rbx_v12+8]");
					obj7 = 0;
					obj8 = this;
					goto IL_04c4;
				}
			}
			nint num5 = 0;
			_003CZip_003Ed__14<T1, T2, T3, TResult> obj9 = null;
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rcx_v31 (Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>)+B8]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rsi_v2+20]");
			object obj11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ rax_v46+C0]");
			object obj12 = 0;
			object obj13 = obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v112 @ rax_v47+80]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rsi_v2+20]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rcx_v38+C0]");
			object obj17 = 0;
			object obj18 = obj17;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rcx_v39+80]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rcx_v39+80]");
			object obj20 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rcx_v39+80]");
			object obj21 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj22 = default(object);
			obj6 = obj22;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rbx_v10+48]");
			obj7 = 0;
			obj8 = obj9;
			goto IL_04c4;
			IL_04c4:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			nint num7 = 0;
			IntPtr intPtr4 = num7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v6 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
			object obj23 = --128;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			if (obj8 != null)
			{
				nint num8 = 0;
				IntPtr intPtr5 = num8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v10 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj24 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v10 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj25 = (nint)0 + (nint)96;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rax_v10 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj26 = (nint)0 + (nint)96;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj28 = default(object);
				object obj27 = obj28;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				nint num9 = 0;
				IntPtr intPtr6 = num9;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v15 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj29 = (nint)0 + (nint)192;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				nint num10 = 0;
				IntPtr intPtr7 = num10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v18 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj30 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v18 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj31 = (nint)0 + (nint)160;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rax_v18 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj32 = (nint)0 + (nint)160;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj34 = default(object);
				object obj33 = obj34;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				nint num11 = 0;
				IntPtr intPtr8 = num11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rax_v23 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj35 = (nint)0 + (nint)256;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				nint num12 = 0;
				IntPtr intPtr9 = num12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ rax_v26 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj36 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ rax_v26 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj37 = (nint)0 + (nint)224;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ rax_v26 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj38 = (nint)0 + (nint)224;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj40 = default(object);
				object obj39 = obj40;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				nint num13 = 0;
				IntPtr intPtr10 = num13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v259 @ rax_v31 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj41 = (nint)0 + (nint)320;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				nint num14 = 0;
				IntPtr intPtr11 = num14;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ r8_v7 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj42 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ r8_v7 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj43 = (nint)0 + (nint)288;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ r8_v7 (Il2CppClass<Il2CppRgctx<Shapes.ShapesExtensions+<Zip>d__14`4>>)+80]");
				object obj44 = (nint)0 + (nint)288;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				object obj46 = default(object);
				object obj45 = obj46;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
				return obj8;
			}
			return (IEnumerator<TResult>)new NullReferenceException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807A5E70");
			IEnumerator result = default(IEnumerator);
			return result;
		}
	}

	public unsafe static void ForEach<T>(IEnumerable<T> elems, Action<T> action)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0056: Expected O, but got I
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		//IL_02cb: Expected O, but got I
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_030f: Expected O, but got Ref
		//IL_032b: Expected O, but got I
		//IL_0087: Expected O, but got I8
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_0099: Expected O, but got I8
		//IL_00ab: Expected O, but got I8
		//IL_00d8: Expected O, but got Ref
		//IL_00e6: Expected O, but got I4
		//IL_0122: Expected O, but got I
		//IL_0142: Expected O, but got I4
		//IL_0413: Expected O, but got I
		//IL_0421: Expected O, but got Ref
		//IL_0165: Expected O, but got I
		//IL_016e: Expected O, but got I4
		//IL_01d8: Expected O, but got I
		//IL_01f2: Expected O, but got Ref
		//IL_0230: Expected O, but got I
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj4 <= 0)
		{
			obj3 = 1152921504606846960L;
		}
		object obj5 = obj3 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj6 = (nint)0 + (nint)15;
		object obj7 = obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj7 <= 0)
		{
			obj6 = 1152921504606846960L;
		}
		object obj8 = obj6 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		object obj9 = (object)(&obj2);
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj10 = (nint)0 + (nint)15;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj11 <= 0)
		{
			obj10 = 1152921504606846960L;
		}
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		_ = 0;
		object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 96));
		object obj14 = 0;
		object obj15 = default(object);
		object obj26 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+60]");
			object obj25;
			object obj18;
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj15 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+60]");
					object obj16 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+60]");
					bool flag = (nint)0 == 0;
					obj14 = 0;
					if (flag)
					{
						break;
					}
					object obj17 = obj16;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v5+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_01a5;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v5+B0]");
					obj18 = 0;
					object obj19 = 0;
					while (true)
					{
						object obj20 = obj19 + obj19;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r9_v7+v420 @ rax_v43*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						obj19++;
						object obj21 = obj19;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r10_v5+12E]");
						if ((nint)obj21 < 0)
						{
							continue;
						}
						goto IL_01a5;
					}
					object obj22 = obj19 + obj19;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r9_v7+8+v475 @ rcx_v35*8]");
					object obj23 = (nint)0 << 4;
					object obj24 = obj23 + 312;
					obj25 = obj24 + obj17;
					goto IL_03f6;
				}
				if (obj13 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_01a5:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj25 = obj26;
			goto IL_03f6;
			IL_03f6:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+70]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v482 @ rdx_v13+8]");
			object obj27 = 0;
			obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 120));
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v496 @ rcx_v25+10] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v507 @ rcx_v29 (Il2CppClass<T>)+28]");
			object obj28 = (nint)0 >> 31;
			bool flag2 = obj28 != null;
			object obj29 = (object)(&obj2);
			if (!flag2)
			{
				obj29 = obj9;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [action @ rdx (System.Action`1<T>)+18] (should have been resolved before IL gen)");
		}
		throw new NullReferenceException();
	}

	public unsafe static Vector3 Rot90CCW(Vector3 v)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected F4, but got Unknown
		//IL_0027: Expected native int or pointer, but got O
		//IL_0034: Expected native int or pointer, but got O
		//IL_0042: Expected native int or pointer, but got O
		float y = v.y;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		float x = y ^ 0;
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->y = v.x;
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->z = 0f;
		return vector;
	}

	public static int AsInt(bool b)
	{
		bool flag = !b;
		return (!flag) ? 1 : 0;
	}

	public unsafe static Vector4 ToVector4(Rect r)
	{
		//IL_0012: Expected native int or pointer, but got O
		//IL_0024: Expected native int or pointer, but got O
		//IL_0036: Expected native int or pointer, but got O
		//IL_0048: Expected native int or pointer, but got O
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->x = r.m_XMin;
		((Vector4*)(nint)vector)->y = r.m_YMin;
		((Vector4*)(nint)vector)->z = r.m_Width;
		((Vector4*)(nint)vector)->w = r.m_Height;
		return vector;
	}

	public static float TaxicabMagnitude(Vector3 v)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		float y = v.y;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = y & 0;
		float x = v.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = x & 0;
		object obj3 = obj + obj2;
		float z = v.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = z & 0;
		return (float)obj3 + (float)obj4;
	}

	public static float AvgComponentMagnitude(Vector3 v)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		float y = v.y;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = y & 0;
		float x = v.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = x & 0;
		object obj3 = obj + obj2;
		float z = v.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj4 = z & 0;
		object obj5 = obj3 + obj4;
		return (float)obj5 / 3f;
	}

	internal unsafe static Color ColorSpaceAdjusted(Color c)
	{
		//IL_0083: Expected native int or pointer, but got O
		//IL_006c: Expected native int or pointer, but got O
		ColorSpace activeColorSpace = QualitySettings.activeColorSpace;
		Color color = default(Color);
		if (activeColorSpace == ColorSpace.Linear)
		{
			float num = Mathf.GammaToLinearSpace(c.r);
			float num2 = Mathf.GammaToLinearSpace(c.g);
			float num3 = Mathf.GammaToLinearSpace(c.b);
			float r = default(float);
			((Color*)(nint)color)->r = r;
			return color;
		}
		((Color*)(nint)color)->r = c.r;
		return color;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetInt_Shapes(Material m, int id, int value)
	{
		m.SetInt(id, value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetInt_Shapes(MaterialPropertyBlock mpb, int id, int value)
	{
		mpb.SetInt(id, value);
	}

	public static void DestroyBranched(UnityEngine.Object obj)
	{
		UnityEngine.Object.Destroy(obj);
	}

	public static void DestroyEndOfFrameEmulated(UnityEngine.Object obj)
	{
		UnityEngine.Object.Destroy(obj);
	}

	public static void TryDestroyInOnDestroy(UnityEngine.Object caller, UnityEngine.Object obj)
	{
		if (obj != null)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	public unsafe static int Product<T>(IEnumerable<T> arr, Func<T, int> mulVal)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0056: Expected O, but got I
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Expected O, but got Unknown
		//IL_0313: Expected O, but got Ref
		//IL_0329: Expected O, but got I
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Expected O, but got Unknown
		//IL_036d: Expected O, but got Ref
		//IL_0389: Expected O, but got I
		//IL_0087: Expected O, but got I8
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		//IL_0099: Expected O, but got I8
		//IL_00e6: Expected O, but got I4
		//IL_00ab: Expected O, but got I8
		//IL_012c: Expected O, but got Ref
		//IL_0143: Expected O, but got I4
		//IL_017f: Expected O, but got I
		//IL_019f: Expected O, but got I4
		//IL_04b0: Expected O, but got I
		//IL_04be: Expected O, but got Ref
		//IL_04f3: Expected O, but got Ref
		//IL_01c2: Expected O, but got I
		//IL_01cb: Expected O, but got I4
		//IL_0235: Expected O, but got I
		//IL_024f: Expected O, but got Ref
		//IL_028d: Expected O, but got I
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_0464: Expected O, but got I
		//IL_0472: Expected O, but got Ref
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj4 <= 0)
		{
			obj3 = 1152921504606846960L;
		}
		object obj5 = obj3 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj6 = (nint)0 + (nint)15;
		object obj7 = obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj7 <= 0)
		{
			obj6 = 1152921504606846960L;
		}
		object obj8 = obj6 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		object obj9 = (object)(&obj2);
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj10 = (nint)0 + (nint)15;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj11 <= 0)
		{
			obj10 = 1152921504606846960L;
		}
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		_ = 1;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rax_v20 (Il2CppClass<System.Collections.Generic.IEnumerable`1<T>>)+135]");
		int num3 = (int)((nint)0 & (nint)1);
		bool flag = num3 == 0;
		object obj13 = !flag;
		nint num4 = 0;
		if (obj13 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		_ = 0;
		object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 136));
		int num5 = 1;
		object obj15 = 0;
		object obj16 = default(object);
		object obj27 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+88]");
			object obj26;
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj16 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+88]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+88]");
					bool flag2 = (nint)0 == 0;
					obj15 = 0;
					if (flag2)
					{
						throw new NullReferenceException();
					}
					object obj18 = obj17;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v5+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_0202;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v5+B0]");
					object obj19 = 0;
					object obj20 = 0;
					while (true)
					{
						object obj21 = obj20 + obj20;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v429 @ r9_v8+v431 @ rax_v44*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						obj20++;
						object obj22 = obj20;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ r10_v5+12E]");
						if ((nint)obj22 < 0)
						{
							continue;
						}
						goto IL_0202;
					}
					object obj23 = obj20 + obj20;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v429 @ r9_v8+8+v486 @ rcx_v35*8]");
					object obj24 = (nint)0 << 4;
					object obj25 = obj24 + 312;
					obj26 = obj25 + obj18;
					goto IL_049b;
				}
				if (obj14 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				break;
			}
			throw new NullReferenceException();
			IL_0202:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj26 = obj27;
			goto IL_049b;
			IL_049b:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v493 @ rdx_v13+8]");
			object obj28 = 0;
			object obj29 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v507 @ rcx_v25+10] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			bool flag3 = mulVal == null;
			obj15 = (object)(&obj2);
			if (!flag3)
			{
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ rcx_v29 (Il2CppClass<T>)+28]");
				object obj30 = (nint)0 >> 31;
				bool flag4 = obj30 != null;
				object obj31 = (object)(&obj2);
				if (!flag4)
				{
					obj31 = obj9;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [mulVal @ rdx (System.Func`2<T, System.Int32>)+28]");
				object obj19 = 0;
				object obj32 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [mulVal @ rdx (System.Func`2<T, System.Int32>)+18] (should have been resolved before IL gen)");
				int num7 = num5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+80]");
				num5 = (int)((nint)num7 * (nint)0);
				continue;
			}
			throw new NullReferenceException();
		}
		return num5;
	}

	public unsafe static float Product<T>(IEnumerable<T> arr, Func<T, float> mulVal)
	{
		//IL_0008: Expected O, but got Ref
		//IL_005b: Expected O, but got I
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02d2: Expected O, but got I
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_0316: Expected O, but got Ref
		//IL_0332: Expected O, but got I
		//IL_008c: Expected O, but got I8
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected O, but got Unknown
		//IL_009e: Expected O, but got I8
		//IL_00b0: Expected O, but got I8
		//IL_00dd: Expected O, but got Ref
		//IL_00f4: Expected O, but got I4
		//IL_0130: Expected O, but got I
		//IL_0150: Expected O, but got I4
		//IL_044e: Expected O, but got Ref
		//IL_045e: Expected O, but got I
		//IL_0493: Expected O, but got Ref
		//IL_0173: Expected O, but got I
		//IL_017c: Expected O, but got I4
		//IL_01e6: Expected O, but got I
		//IL_0200: Expected O, but got Ref
		//IL_023e: Expected O, but got I
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_040d: Expected O, but got I
		//IL_041b: Expected O, but got Ref
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj4 <= 0)
		{
			obj3 = 1152921504606846960L;
		}
		object obj5 = obj3 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj6 = (nint)0 + (nint)15;
		object obj7 = obj6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj7 <= 0)
		{
			obj6 = 1152921504606846960L;
		}
		object obj8 = obj6 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		object obj9 = (object)(&obj2);
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj10 = (nint)0 + (nint)15;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj11 <= 0)
		{
			obj10 = 1152921504606846960L;
		}
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		_ = 1f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		_ = 0;
		object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 152));
		float num2 = 1f;
		object obj14 = 0;
		object obj15 = default(object);
		object obj26 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+98]");
			object obj25;
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj15 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+98]");
					object obj16 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+98]");
					bool flag = (nint)0 == 0;
					obj14 = 0;
					if (flag)
					{
						throw new NullReferenceException();
					}
					object obj17 = obj16;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v5+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_01b3;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v5+B0]");
					object obj18 = 0;
					object obj19 = 0;
					while (true)
					{
						object obj20 = obj19 + obj19;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ r9_v8+v435 @ rax_v42*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						obj19++;
						object obj21 = obj19;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v412 @ r10_v5+12E]");
						if ((nint)obj21 < 0)
						{
							continue;
						}
						goto IL_01b3;
					}
					object obj22 = obj19 + obj19;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v433 @ r9_v8+8+v490 @ rcx_v35*8]");
					object obj23 = (nint)0 << 4;
					object obj24 = obj23 + 312;
					obj25 = obj24 + obj17;
					goto IL_0446;
				}
				if (obj13 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				break;
			}
			throw new NullReferenceException();
			IL_01b3:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj25 = obj26;
			goto IL_0446;
			IL_0446:
			obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v497 @ rdx_v13+8]");
			object obj27 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v510 @ rcx_v25+10] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			bool flag2 = mulVal == null;
			obj14 = (object)(&obj2);
			if (!flag2)
			{
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v520 @ rcx_v29 (Il2CppClass<T>)+28]");
				object obj28 = (nint)0 >> 31;
				bool flag3 = obj28 != null;
				object obj29 = (object)(&obj2);
				if (!flag3)
				{
					obj29 = obj9;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [mulVal @ rdx (System.Func`2<T, System.Single>)+28]");
				object obj18 = 0;
				object obj30 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [mulVal @ rdx (System.Func`2<T, System.Single>)+18] (should have been resolved before IL gen)");
				float num4 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+90]");
				num2 = num4 * 0f;
				continue;
			}
			throw new NullReferenceException();
		}
		return num2;
	}

	public static IEnumerable<TResult> Zip<T1, T2, T3, TResult>(IEnumerable<T1> source, IEnumerable<T2> second, IEnumerable<T3> third, Func<T1, T2, T3, TResult> func)
	{
		//IL_0047: Expected O, but got I
		//IL_0061: Expected O, but got I
		//IL_009d: Expected O, but got I
		//IL_00b5: Expected O, but got I
		//IL_00cb: Expected O, but got I
		//IL_00eb: Expected O, but got I
		//IL_011c: Expected O, but got I
		//IL_0134: Expected O, but got I
		//IL_014a: Expected O, but got I
		//IL_016f: Expected O, but got I
		//IL_01a0: Expected O, but got I
		//IL_01b8: Expected O, but got I
		//IL_01ce: Expected O, but got I
		//IL_01ee: Expected O, but got I
		//IL_021f: Expected O, but got I
		//IL_0237: Expected O, but got I
		//IL_024d: Expected O, but got I
		//IL_0272: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
		object obj = 0;
		IEnumerable<TResult> enumerable = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18078DB20");
		if (enumerable != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
			object obj3 = 0;
			object obj4 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v2+80]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v2+80]");
			object obj6 = (nint)0 + (nint)128;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v2+80]");
			object obj7 = (nint)0 + (nint)128;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
			object obj8 = 0;
			object obj9 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rcx_v9+80]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rcx_v9+80]");
			object obj11 = (nint)0 + (nint)192;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rcx_v9+80]");
			object obj12 = (nint)0 + (nint)192;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
			object obj13 = 0;
			object obj14 = obj13;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v13+80]");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v13+80]");
			object obj16 = (nint)0 + (nint)256;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rcx_v13+80]");
			object obj17 = (nint)0 + (nint)256;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ stack_28+38]");
			object obj18 = 0;
			object obj19 = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17+80]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17+80]");
			object obj21 = (nint)0 + (nint)320;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003790");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v139 @ rcx_v17+80]");
			object obj22 = (nint)0 + (nint)320;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			return enumerable;
		}
		return (IEnumerable<TResult>)new NullReferenceException();
	}

	public static int PopCount(uint i)
	{
		//IL_0029: Expected O, but got I4
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00a5: Expected I4, but got O
		int num = (int)i >> 1;
		int num2 = num & 0x55555555;
		object obj = (int)i - num2;
		object obj2 = obj & 0x33333333;
		object obj3 = obj >> 2;
		object obj4 = obj3 & 0x33333333;
		object obj5 = obj4 + obj2;
		object obj6 = obj5 >> 4;
		object obj7 = obj6 + obj5;
		object obj8 = obj7 & 0xF0F0F0F;
		object obj9 = obj8 * 16843009;
		return obj9 >> 24;
	}
}
