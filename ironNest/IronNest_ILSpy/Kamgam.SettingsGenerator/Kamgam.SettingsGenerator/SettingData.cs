using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

[Serializable]
public class SettingData
{
	public enum DataType
	{
		Unknown,
		Int,
		Float,
		Bool,
		String,
		Color,
		KeyCombination,
		Option,
		ColorOption
	}

	public static Dictionary<DataType, Type> Types;

	public static Dictionary<DataType, List<Type>> CompatibleTypes;

	public string ID;

	public DataType Type;

	public int[] IntValues;

	public float[] FloatValues;

	public string[] StringValues;

	public SettingData(string path, DataType type, int[] intValues, float[] floatValues, string[] stringValues)
	{
		ID = path;
		Type = type;
		IntValues = intValues;
		float[] floatValues2 = default(float[]);
		FloatValues = floatValues2;
		string[] stringValues2 = default(string[]);
		StringValues = stringValues2;
	}

	public SettingData(string path, DataType type)
	{
		ID = path;
		Type = type;
	}

	static SettingData()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected I4, but got Unknown
		//IL_0042: Expected O, but got I
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected I4, but got Unknown
		//IL_008f: Expected O, but got I
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected I4, but got Unknown
		//IL_00dc: Expected O, but got I
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected I4, but got Unknown
		//IL_0129: Expected O, but got I
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected I4, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected I4, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected I4, but got Unknown
		//IL_01f0: Expected O, but got I
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected I4, but got Unknown
		//IL_023d: Expected O, but got I
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected I4, but got Unknown
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected I4, but got Unknown
		//IL_02ea: Expected O, but got I
		//IL_031f: Expected O, but got I
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected I4, but got Unknown
		//IL_038d: Expected O, but got I
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected I4, but got Unknown
		//IL_03fb: Expected O, but got I
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected I4, but got Unknown
		//IL_0469: Expected O, but got I
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected I4, but got Unknown
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Expected I4, but got Unknown
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Expected I4, but got Unknown
		//IL_05b8: Expected O, but got I
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Expected I4, but got Unknown
		//IL_0626: Expected O, but got I
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Expected I4, but got Unknown
		Dictionary<DataType, Type> dictionary = new Dictionary<DataType, Type>();
		object obj = default(object);
		DataType key = (DataType)(obj + 24);
		_ = 0;
		dictionary.Add(key, null);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle = System.Type.GetTypeFromHandle(handle);
		DataType key2 = (DataType)(obj + 24);
		_ = 1;
		dictionary.Add(key2, typeFromHandle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
		RuntimeTypeHandle handle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle2 = System.Type.GetTypeFromHandle(handle2);
		DataType key3 = (DataType)(obj + 24);
		_ = 2;
		dictionary.Add(key3, typeFromHandle2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
		RuntimeTypeHandle handle3 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle3 = System.Type.GetTypeFromHandle(handle3);
		DataType key4 = (DataType)(obj + 24);
		_ = 3;
		dictionary.Add(key4, typeFromHandle3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		RuntimeTypeHandle handle4 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle4 = System.Type.GetTypeFromHandle(handle4);
		DataType key5 = (DataType)(obj + 24);
		_ = 4;
		dictionary.Add(key5, typeFromHandle4);
		Type typeFromHandle5 = System.Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Color));
		DataType key6 = (DataType)(obj + 24);
		_ = 5;
		dictionary.Add(key6, typeFromHandle5);
		Type typeFromHandle6 = System.Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(KeyCombination));
		DataType key7 = (DataType)(obj + 24);
		_ = 6;
		dictionary.Add(key7, typeFromHandle6);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle5 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle7 = System.Type.GetTypeFromHandle(handle5);
		DataType key8 = (DataType)(obj + 24);
		_ = 7;
		dictionary.Add(key8, typeFromHandle7);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle6 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle8 = System.Type.GetTypeFromHandle(handle6);
		DataType key9 = (DataType)(obj + 24);
		_ = 8;
		dictionary.Add(key9, typeFromHandle8);
		Types = dictionary;
		Dictionary<DataType, List<Type>> dictionary2 = new Dictionary<DataType, List<Type>>();
		List<Type> value = new List<Type>();
		DataType key10 = (DataType)(obj + 24);
		_ = 0;
		dictionary2.Add(key10, value);
		List<Type> list = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle7 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle9 = System.Type.GetTypeFromHandle(handle7);
		list.Add(typeFromHandle9);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
		RuntimeTypeHandle handle8 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle10 = System.Type.GetTypeFromHandle(handle8);
		list.Add(typeFromHandle10);
		DataType key11 = (DataType)(obj + 24);
		_ = 1;
		dictionary2.Add(key11, list);
		List<Type> list2 = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
		RuntimeTypeHandle handle9 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle11 = System.Type.GetTypeFromHandle(handle9);
		list2.Add(typeFromHandle11);
		DataType key12 = (DataType)(obj + 24);
		_ = 2;
		dictionary2.Add(key12, list2);
		List<Type> list3 = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
		RuntimeTypeHandle handle10 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle12 = System.Type.GetTypeFromHandle(handle10);
		list3.Add(typeFromHandle12);
		DataType key13 = (DataType)(obj + 24);
		_ = 3;
		dictionary2.Add(key13, list3);
		List<Type> list4 = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		RuntimeTypeHandle handle11 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle13 = System.Type.GetTypeFromHandle(handle11);
		list4.Add(typeFromHandle13);
		DataType key14 = (DataType)(obj + 24);
		_ = 4;
		dictionary2.Add(key14, list4);
		List<Type> list5 = new List<Type>();
		Type typeFromHandle14 = System.Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Color));
		list5.Add(typeFromHandle14);
		Type typeFromHandle15 = System.Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Color32));
		list5.Add(typeFromHandle15);
		DataType key15 = (DataType)(obj + 24);
		_ = 5;
		dictionary2.Add(key15, list5);
		List<Type> list6 = new List<Type>();
		Type typeFromHandle16 = System.Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(KeyCombination));
		list6.Add(typeFromHandle16);
		DataType key16 = (DataType)(obj + 24);
		_ = 6;
		dictionary2.Add(key16, list6);
		List<Type> list7 = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle12 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle17 = System.Type.GetTypeFromHandle(handle12);
		list7.Add(typeFromHandle17);
		DataType key17 = (DataType)(obj + 24);
		_ = 7;
		dictionary2.Add(key17, list7);
		List<Type> list8 = new List<Type>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle13 = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle18 = System.Type.GetTypeFromHandle(handle13);
		list8.Add(typeFromHandle18);
		DataType key18 = (DataType)(obj + 24);
		_ = 8;
		dictionary2.Add(key18, list8);
		CompatibleTypes = dictionary2;
	}
}
