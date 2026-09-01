using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;

namespace Shapes;

public class TextElement : IDisposable
{
	private static int idCounter;

	public readonly int id;

	private StringBuilder sb;

	public TextMeshProShapes Tmp
	{
		get
		{
			ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
			if ((object)instance != null)
			{
				return instance.GetElement(id);
			}
			return (TextMeshProShapes)(object)new NullReferenceException();
		}
	}

	public static int GetNextId()
	{
		int num = idCounter + 1;
		idCounter = num;
		return idCounter;
	}

	public TextElement()
	{
		StringBuilder stringBuilder = new StringBuilder();
		sb = stringBuilder;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		int num = idCounter + 1;
		idCounter = num;
		id = idCounter;
	}

	public void Dispose()
	{
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		instance.ReleaseElement(id);
	}

	public void ClearText()
	{
		StringBuilder stringBuilder = sb.Clear();
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		TextMeshProShapes element = instance.GetElement(id);
		element.SetText(sb);
	}

	public unsafe void AppendInt(int value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 12)
	{
		//IL_0008: Expected O, but got Ref
		//IL_01df: Expected O, but got I4
		//IL_005d: Expected I, but got O
		//IL_0217: Expected O, but got I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0079: Expected O, but got Ref
		//IL_0095: Expected O, but got Ref
		//IL_013a: Expected O, but got Ref
		//IL_0142: Expected O, but got Ref
		//IL_0150: Expected O, but got Ref
		//IL_017f: Expected O, but got Ref
		Span<char> span2 = default(Span<char>);
		Span<char> span = (Span<char>)(&span2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B42649]");
		bool flag = (nint)0 == 0;
		_ = 0;
		object obj = maxCharCount + maxCharCount;
		void* pointer;
		if (!flag)
		{
			object obj2 = obj + 15;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				pointer = &span2;
				goto IL_0204;
			}
		}
		pointer = (void*)unchecked((nint)null);
		goto IL_0204;
		IL_0204:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		span = (Span<char>)0;
		span2 = new Span<char>(pointer, maxCharCount);
		CultureInfo invariantCulture = CultureInfo.InvariantCulture;
		ReadOnlySpan<char> format2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		ref int charsWritten = ref System.Runtime.CompilerServices.Unsafe.As<Span<char>, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 120));
		Span<char> destination = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		int num = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 104));
		IFormatProvider provider = default(IFormatProvider);
		bool flag2 = ((int*)num)->TryFormat(destination, out charsWritten, format2, provider);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		if (0 > (nint)span)
		{
			System.ThrowHelper.ThrowArgumentOutOfRangeException();
		}
		ref char reference = ref System.Runtime.CompilerServices.Unsafe.Add(ref *(char*)span, 0);
		_ = 0;
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		span = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference);
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18090CA50");
		ReadOnlySpan<char> value2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+10]");
		_ = 0;
		StringBuilder stringBuilder = sb.Append(value2);
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		TextMeshProShapes element = instance.GetElement(id);
		element.SetText(sb);
	}

	public unsafe void AppendFloat(float value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 32)
	{
		//IL_0008: Expected O, but got Ref
		//IL_01e1: Expected O, but got I4
		//IL_005d: Expected I, but got O
		//IL_0219: Expected O, but got I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0079: Expected O, but got Ref
		//IL_0095: Expected O, but got Ref
		//IL_00c8: Expected Ref, but got F4
		//IL_013c: Expected O, but got Ref
		//IL_0144: Expected O, but got Ref
		//IL_0152: Expected O, but got Ref
		//IL_0181: Expected O, but got Ref
		Span<char> span2 = default(Span<char>);
		Span<char> span = (Span<char>)(&span2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B4264A]");
		bool flag = (nint)0 == 0;
		_ = 0;
		object obj = maxCharCount + maxCharCount;
		void* pointer;
		if (!flag)
		{
			object obj2 = obj + 15;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				pointer = &span2;
				goto IL_0206;
			}
		}
		pointer = (void*)unchecked((nint)null);
		goto IL_0206;
		IL_0206:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		span = (Span<char>)0;
		span2 = new Span<char>(pointer, maxCharCount);
		CultureInfo invariantCulture = CultureInfo.InvariantCulture;
		ReadOnlySpan<char> format2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		ref int charsWritten = ref System.Runtime.CompilerServices.Unsafe.As<Span<char>, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 120));
		Span<char> destination = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		float num = (float)(ref span2) + 104f;
		IFormatProvider provider = default(IFormatProvider);
		bool flag2 = ((float*)num)->TryFormat(destination, out charsWritten, format2, provider);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		if (0 > (nint)span)
		{
			System.ThrowHelper.ThrowArgumentOutOfRangeException();
		}
		ref char reference = ref System.Runtime.CompilerServices.Unsafe.Add(ref *(char*)span, 0);
		_ = 0;
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		span = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference);
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18090CA50");
		ReadOnlySpan<char> value2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+10]");
		_ = 0;
		StringBuilder stringBuilder = sb.Append(value2);
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		TextMeshProShapes element = instance.GetElement(id);
		element.SetText(sb);
	}

	public unsafe void AppendDouble(double value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 32)
	{
		//IL_0008: Expected O, but got Ref
		//IL_01e1: Expected O, but got I4
		//IL_005d: Expected I, but got O
		//IL_0219: Expected O, but got I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0079: Expected O, but got Ref
		//IL_0095: Expected O, but got Ref
		//IL_00c8: Expected Ref, but got F8
		//IL_013c: Expected O, but got Ref
		//IL_0144: Expected O, but got Ref
		//IL_0152: Expected O, but got Ref
		//IL_0181: Expected O, but got Ref
		Span<char> span2 = default(Span<char>);
		Span<char> span = (Span<char>)(&span2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B4264B]");
		bool flag = (nint)0 == 0;
		_ = 0;
		object obj = maxCharCount + maxCharCount;
		void* pointer;
		if (!flag)
		{
			object obj2 = obj + 15;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
				pointer = &span2;
				goto IL_0206;
			}
		}
		pointer = (void*)unchecked((nint)null);
		goto IL_0206;
		IL_0206:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		span = (Span<char>)0;
		span2 = new Span<char>(pointer, maxCharCount);
		CultureInfo invariantCulture = CultureInfo.InvariantCulture;
		ReadOnlySpan<char> format2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		ref int charsWritten = ref System.Runtime.CompilerServices.Unsafe.As<Span<char>, int>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 120));
		Span<char> destination = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		double num = (double)(ref span2) + 104.0;
		IFormatProvider provider = default(IFormatProvider);
		bool flag2 = ((double*)num)->TryFormat(destination, out charsWritten, format2, provider);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm6,8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		if (0 > (nint)span)
		{
			System.ThrowHelper.ThrowArgumentOutOfRangeException();
		}
		ref char reference = ref System.Runtime.CompilerServices.Unsafe.Add(ref *(char*)span, 0);
		_ = 0;
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		span = (Span<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference);
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 16));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+78]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18090CA50");
		ReadOnlySpan<char> value2 = (ReadOnlySpan<char>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref span2, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (System.Span`1<System.Char>)+10]");
		_ = 0;
		StringBuilder stringBuilder = sb.Append(value2);
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		TextMeshProShapes element = instance.GetElement(id);
		element.SetText(sb);
	}

	public unsafe void AppendString(ReadOnlySpan<char> stringValue)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		StringBuilder stringBuilder = sb.Append((ReadOnlySpan<char>)(&obj));
		ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
		TextMeshProShapes element = instance.GetElement(id);
		element.SetText(sb);
	}
}
