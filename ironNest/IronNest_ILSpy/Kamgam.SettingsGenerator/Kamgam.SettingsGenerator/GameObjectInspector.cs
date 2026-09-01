using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class GameObjectInspector
{
	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public string methodName;

		internal bool _003CGetAndCacheObjectAtPath_003Eb__0(MethodInfo m)
		{
			//IL_009e: Expected I4, but got O
			if ((object)m != null)
			{
				string name = m.Name;
				bool flag = name == methodName;
				if (!flag)
				{
					return flag;
				}
				ParameterInfo[] parameters = m.GetParameters();
				if (parameters != null)
				{
					return parameters.Length == 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass14_1
	{
		public string parameterTypeName;

		public _003C_003Ec__DisplayClass14_0 CS_0024_003C_003E8__locals1;

		internal bool _003CGetAndCacheObjectAtPath_003Eb__1(MethodInfo m)
		{
			//IL_0108: Expected I4, but got O
			string name = m.Name;
			_003C_003Ec__DisplayClass14_0 obj = CS_0024_003C_003E8__locals1;
			if (name == obj.methodName)
			{
				ParameterInfo[] parameters = m.GetParameters();
				if (parameters.Length == 1)
				{
					ParameterInfo[] parameters2 = m.GetParameters();
					if (parameters2.Length > 0)
					{
						Type parameterType = parameters2[0].ParameterType;
						string name2 = parameterType.Name;
						return name2 == parameterTypeName;
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
			}
			return false;
		}
	}

	private readonly Dictionary<string, object> _components;

	private readonly Dictionary<string, (object, PropertyInfo)> _properties;

	private readonly Dictionary<string, (object, FieldInfo)> _fields;

	private readonly Dictionary<string, (object, MethodInfo)> _getMethods;

	private readonly Dictionary<string, (object, MethodInfo)> _setMethods;

	private static BindingFlags BindingFlags = (BindingFlags)20;

	public GameObject Target;

	private static readonly Regex componentIndexRegex;

	public GameObjectInspector(GameObject go)
	{
		Dictionary<string, object> components = new Dictionary<string, object>();
		_components = components;
		Dictionary<string, (object, PropertyInfo)> properties = new Dictionary<string, (object, PropertyInfo)>();
		_properties = properties;
		Dictionary<string, (object, FieldInfo)> fields = new Dictionary<string, (object, FieldInfo)>();
		_fields = fields;
		Dictionary<string, (object, MethodInfo)> getMethods = new Dictionary<string, (object, MethodInfo)>();
		_getMethods = getMethods;
		Dictionary<string, (object, MethodInfo)> setMethods = new Dictionary<string, (object, MethodInfo)>();
		_setMethods = setMethods;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		Target = go;
	}

	public Type GetTypeOfPath(string path)
	{
		//IL_029f: Expected O, but got I
		if (string.IsNullOrEmpty(path))
		{
			goto IL_0377;
		}
		if (_properties != null)
		{
			if (!_properties.TryGetValue(path, out var _))
			{
				if (_fields == null)
				{
					goto IL_0394;
				}
				if (!_fields.TryGetValue(path, out var _))
				{
					if (_getMethods == null)
					{
						goto IL_0394;
					}
					if (!_getMethods.TryGetValue(path, out var value3))
					{
						if (_setMethods == null)
						{
							goto IL_0394;
						}
						if (!_setMethods.TryGetValue(path, out value3))
						{
							object andCacheObjectAtPath = GetAndCacheObjectAtPath(path);
						}
					}
				}
			}
			if (_properties != null)
			{
				object obj10 = default(object);
				if (!_properties.TryGetValue(path, out var _))
				{
					if (_fields != null)
					{
						object obj8 = default(object);
						if (!_fields.TryGetValue(path, out var _))
						{
							if (_getMethods != null)
							{
								object obj6 = default(object);
								if (!_getMethods.TryGetValue(path, out var _))
								{
									if (_setMethods != null)
									{
										if (!_setMethods.TryGetValue(path, out var _))
										{
											goto IL_0377;
										}
										object obj = default(object);
										if (obj != null)
										{
											object obj2 = obj;
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v443 @ rdx_v16+238] (should have been resolved before IL gen)");
											object obj3 = default(object);
											if (obj3 != null)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v20+20]");
												object obj4 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v20+20]");
												if ((nint)0 != 0)
												{
													object obj5 = obj4;
													Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v447 @ rdx_v18+1E8] (should have been resolved before IL gen)");
													Type result = default(Type);
													return result;
												}
											}
										}
									}
								}
								else if (obj6 != null)
								{
									object obj7 = obj6;
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v440 @ rdx_v13+3B8] (should have been resolved before IL gen)");
									Type result2 = default(Type);
									return result2;
								}
							}
						}
						else if (obj8 != null)
						{
							object obj9 = obj8;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v427 @ rdx_v10+248] (should have been resolved before IL gen)");
							Type result3 = default(Type);
							return result3;
						}
					}
				}
				else if (obj10 != null)
				{
					object obj11 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v414 @ rdx_v7+238] (should have been resolved before IL gen)");
					Type result4 = default(Type);
					return result4;
				}
			}
		}
		goto IL_0394;
		IL_0394:
		return (Type)(object)new NullReferenceException();
		IL_0377:
		return null;
	}

	public void Clear()
	{
		_fields.Clear();
		_properties.Clear();
		_getMethods.Clear();
		_setMethods.Clear();
		_components.Clear();
		Target = null;
	}

	public List<string> GetPaths(string path, bool includeMethods, bool getOrSetMethods, List<Type> compatibleTypes, List<string> results = null)
	{
		List<string> list = default(List<string>);
		bool flag = list != null;
		List<string> list2 = list;
		if (!flag)
		{
			List<string> list3 = new List<string>();
			list2 = list3;
		}
		if (!string.IsNullOrEmpty(path))
		{
			if (path == null)
			{
				return (List<string>)(object)new NullReferenceException();
			}
			if (!path.EndsWith(")"))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D8740");
				object obj = default(object);
				if (obj != null)
				{
					bool includeMethods2 = default(bool);
					bool getOrSetMethods2 = default(bool);
					List<Type> compatibleTypes2 = default(List<Type>);
					List<string> results2 = default(List<string>);
					List<string> memberPaths = GetMemberPaths(obj, path, includePropsAndFields: true, includeMethods2, getOrSetMethods2, compatibleTypes2, results2);
					list2 = memberPaths;
				}
			}
		}
		else
		{
			List<string> componentPaths = GetComponentPaths(list2);
			list2 = componentPaths;
		}
		return list2;
	}

	public unsafe List<string> GetComponentPaths(List<string> results = null)
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00fe: Expected O, but got I4
		//IL_0107: Expected O, but got I4
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Expected O, but got Unknown
		//IL_0233: Expected O, but got I4
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		if (Target != null)
		{
			bool flag = results != null;
			List<string> list = results;
			if (!flag)
			{
				List<string> list2 = new List<string>();
				bool flag2 = list2 == null;
				list = list2;
				if (flag2)
				{
					goto IL_0336;
				}
			}
			list.Add("gameObject");
			if ((object)Target != null)
			{
				Component[] components = Target.GetComponents<Component>();
				Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
				if (components != null)
				{
					object obj = components + 32;
					object obj2 = 0;
					object obj3 = 0;
					object obj4 = default(object);
					Type type = default(Type);
					object obj6 = default(object);
					object obj8 = default(object);
					object arg = default(object);
					while (true)
					{
						if ((nint)obj3 < components.Length)
						{
							if (obj == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(SettingReceiverGenericConnector));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABB0");
							if (obj4 == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
								if ((object)type == null)
								{
									break;
								}
								object name = type.Name;
								if (dictionary == null)
								{
									break;
								}
								object obj7;
								Type key;
								Dictionary<Type, int> dictionary2;
								if (dictionary.ContainsKey(type))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
									object obj5 = obj6 + 1;
									obj7 = obj5;
									key = type;
									dictionary2 = dictionary;
								}
								else
								{
									obj7 = 0;
									key = type;
									dictionary2 = dictionary;
								}
								dictionary2.set_Item(key, (int)(&obj7));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
								string text2;
								if (obj8 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									string text = $"{name}[{arg}]";
									object obj9 = obj8;
									text2 = text;
								}
								else
								{
									bool flag3 = name == null;
									text2 = "";
									if (!flag3)
									{
										text2 = (string)name;
									}
								}
								if (list == null)
								{
									break;
								}
								list.Add(text2);
								if (_components == null)
								{
									break;
								}
								bool flag4 = _components.TryAdd(text2, obj);
							}
							obj2++;
							obj += 8;
							obj3 = obj2;
							continue;
						}
						return list;
					}
				}
			}
			goto IL_0336;
		}
		return results;
		IL_0336:
		return (List<string>)(object)new NullReferenceException();
	}

	private unsafe List<string> GetMemberPaths(object obj, string path, bool includePropsAndFields, bool includeMethods, bool getOrSetMethods, List<Type> compatibleTypes, List<string> results = null)
	{
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Expected O, but got Unknown
		//IL_046e: Expected O, but got I4
		//IL_0477: Expected O, but got I4
		//IL_0480: Expected O, but got I4
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_018b: Expected O, but got I4
		//IL_0194: Expected O, but got I4
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Expected O, but got Unknown
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Expected O, but got Unknown
		//IL_012b: Expected O, but got I
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Expected O, but got Unknown
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Expected O, but got Unknown
		//IL_050b: Expected O, but got I4
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Expected O, but got Unknown
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Expected O, but got Unknown
		//IL_052e: Expected I, but got O
		//IL_0334: Expected I, but got O
		//IL_0262: Expected O, but got Ref
		//IL_026f: Expected O, but got I4
		//IL_0278: Expected O, but got I4
		//IL_0281: Expected O, but got I4
		//IL_0744: Expected O, but got I4
		//IL_055e: Expected O, but got I
		//IL_0366: Expected I, but got O
		//IL_03eb: Expected O, but got Ref
		//IL_03f8: Expected O, but got I4
		//IL_0401: Expected O, but got I4
		//IL_040a: Expected O, but got I4
		//IL_075a: Expected I, but got O
		//IL_0780: Expected O, but got I
		//IL_0807: Expected O, but got I4
		//IL_0811: Expected O, but got I4
		//IL_081a: Expected O, but got I4
		//IL_0822: Expected O, but got Ref
		//IL_05f2: Expected O, but got I
		//IL_061a: Expected I, but got O
		//IL_06e4: Expected O, but got I4
		//IL_06ed: Expected O, but got I4
		//IL_06f6: Expected O, but got I4
		//IL_06fe: Expected O, but got Ref
		List<string> list = default(List<string>);
		bool flag = list != null;
		List<string> list2 = list;
		if (!flag)
		{
			List<string> list3 = new List<string>();
			list2 = list3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		object obj3 = default(object);
		object obj2 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v474 @ r8_v8+6D8] (should have been resolved before IL gen)");
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v481 @ r8_v10+848] (should have been resolved before IL gen)");
		object obj5 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v489 @ r8_v12+7A8] (should have been resolved before IL gen)");
		bool flag3 = default(bool);
		bool flag2 = !flag3;
		object obj7 = default(object);
		object obj6 = obj7;
		object obj8 = obj;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type = default(Type);
			if (!type.IsPrimitive)
			{
				if (type.IsClass)
				{
					goto IL_0164;
				}
				if (type.IsValueType && !type.IsEnum)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle = Type.GetTypeFromHandle(handle);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					object obj9 = default(object);
					if (obj9 != null)
					{
						goto IL_0164;
					}
				}
			}
			goto IL_041d;
		}
		goto IL_086e;
		IL_086e:
		object obj10 = default(object);
		(object, MethodInfo) tuple6 = default((object, MethodInfo));
		(object, PropertyInfo) tuple2 = default((object, PropertyInfo));
		(object, PropertyInfo) tuple8 = default((object, PropertyInfo));
		if (obj10 != null && !path.EndsWith(")"))
		{
			object obj11 = obj6 + 32;
			(object, MethodInfo) tuple = ((object, MethodInfo))tuple2;
			(object, MethodInfo) tuple3 = ((object, MethodInfo))0;
			object obj12 = 0;
			object obj13 = 0;
			object obj16 = default(object);
			List<Type> list4 = default(List<Type>);
			object obj17 = default(object);
			Type item = default(Type);
			while (true)
			{
				object obj14 = obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1041 @ rbx_v12+18]");
				if ((nint)obj14 >= 0)
				{
					break;
				}
				object obj15 = obj13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1041 @ rbx_v12+18]");
				(object, MethodInfo) tuple5;
				string key;
				Dictionary<string, (object, MethodInfo)> dictionary;
				if ((nint)obj15 < 0)
				{
					MethodBase methodBase = (MethodBase)obj11;
					if (((MethodBase)obj11).IsSpecialName)
					{
						goto IL_08eb;
					}
					Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObsoleteAttribute));
					bool flag4 = Attribute.IsDefined((MemberInfo)obj11, typeFromHandle2);
					tuple3 = ((object, MethodInfo))0;
					if (!flag4)
					{
						ParameterInfo[] parameters = ((MethodBase)obj11).GetParameters();
						nint num = (nint)methodBase;
						if (obj16 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1429 @ rdx_v28 (Il2CppClass<System.Reflection.MethodBase>)+238]");
							tuple3 = ((object, MethodInfo))0;
							ParameterInfo[] parameters2 = ((MethodBase)obj11).GetParameters();
							if (parameters2.Length == 1)
							{
								if (parameters.Length <= 0)
								{
									goto IL_088b;
								}
								object parameterType = parameters[0].ParameterType;
								bool flag5 = list4.Contains((Type)parameterType);
								bool flag6 = !flag5;
								tuple3 = ((object, MethodInfo))0;
								if (!flag6)
								{
									string name = ((MemberInfo)obj11).Name;
									nint num2 = (nint)parameterType;
									Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1660 @ rdx_v53 (Il2CppClass<System.Object>)+1B8] (should have been resolved before IL gen)");
									string text = path + "." + name + "(" + (string)obj17 + ")";
									list2.Add(text);
									tuple = (obj, (MethodInfo)obj11);
									(object, MethodInfo) tuple4 = ((object, MethodInfo))0;
									tuple = ((object, MethodInfo))0;
									tuple5 = ((object, MethodInfo))0;
									tuple3 = ((object, MethodInfo))(&tuple4);
									key = text;
									dictionary = _setMethods;
									goto IL_0914;
								}
							}
						}
						else
						{
							ParameterInfo[] parameters3 = ((MethodBase)obj11).GetParameters();
							bool flag7 = parameters3.Length != 0;
							tuple3 = ((object, MethodInfo))0;
							if (!flag7)
							{
								nint num3 = (nint)methodBase;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1555 @ rdx_v39 (Il2CppClass<System.Reflection.MethodBase>)+3B8] (should have been resolved before IL gen)");
								bool flag8 = list4.Contains(item);
								tuple3 = ((object, MethodInfo))0;
								if (flag8)
								{
									goto IL_07ae;
								}
							}
							if (list4 == null)
							{
								goto IL_07ae;
							}
						}
					}
					goto IL_0839;
				}
				goto IL_088b;
				IL_08eb:
				obj13++;
				obj11 += 8;
				obj12 = obj13;
				continue;
				IL_0839:
				obj6 = obj7;
				goto IL_08eb;
				IL_07ae:
				string name2 = ((MemberInfo)obj11).Name;
				string text2 = path + "." + name2 + "()";
				list2.Add(text2);
				tuple6 = (obj, (MethodInfo)obj11);
				(object, MethodInfo) tuple7 = ((object, MethodInfo))0;
				tuple6 = ((object, MethodInfo))0;
				tuple5 = ((object, MethodInfo))0;
				tuple3 = ((object, MethodInfo))(&tuple7);
				key = text2;
				dictionary = _getMethods;
				goto IL_0914;
				IL_0914:
				bool flag9 = dictionary.TryAdd(key, tuple3);
				tuple8 = ((object, PropertyInfo))tuple5;
				flag3 = false;
				goto IL_0839;
			}
		}
		goto IL_041d;
		IL_041d:
		return list2;
		IL_088b:
		return (List<string>)(object)new IndexOutOfRangeException();
		IL_0164:
		object obj19 = default(object);
		object obj18 = obj19 + 32;
		(object, FieldInfo) tuple9 = ((object, FieldInfo))tuple2;
		(object, FieldInfo) tuple10 = ((object, FieldInfo))tuple8;
		object obj20 = 0;
		object obj21 = 0;
		string text3 = path;
		while (true)
		{
			object obj22 = obj20;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v480 @ rax_v20+18]");
			if ((nint)obj22 >= 0)
			{
				break;
			}
			object obj23 = obj21;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v480 @ rax_v20+18]");
			if ((nint)obj23 < 0)
			{
				Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObsoleteAttribute));
				if (!Attribute.IsDefined((MemberInfo)obj18, typeFromHandle3))
				{
					string name3 = ((MemberInfo)obj18).Name;
					string text4 = text3 + "." + name3;
					list2.Add(text4);
					tuple9 = (obj, (FieldInfo)obj18);
					bool flag10 = _fields.TryAdd(text4, ((object, FieldInfo))(&tuple6));
					tuple6 = ((object, MethodInfo))0;
					tuple9 = ((object, FieldInfo))0;
					tuple10 = ((object, FieldInfo))0;
					flag3 = false;
					text3 = path;
				}
				obj21++;
				obj18 += 8;
				obj20 = obj21;
				continue;
			}
			goto IL_088b;
		}
		object obj25 = default(object);
		object obj24 = obj25 + 32;
		tuple2 = ((object, PropertyInfo))tuple9;
		tuple8 = ((object, PropertyInfo))tuple10;
		object obj26 = null;
		obj8 = null;
		object obj29 = default(object);
		object obj30 = default(object);
		while (true)
		{
			object obj27 = obj26;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ rax_v23+18]");
			if ((nint)obj27 >= 0)
			{
				break;
			}
			object obj28 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v488 @ rax_v23+18]");
			if ((nint)obj28 < 0)
			{
				MemberInfo memberInfo = (MemberInfo)obj24;
				Type typeFromHandle4 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ObsoleteAttribute));
				if (!Attribute.IsDefined((MemberInfo)obj24, typeFromHandle4))
				{
					nint num4 = (nint)memberInfo;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1540 @ rdx_v71 (Il2CppClass<System.Reflection.MemberInfo>)+258] (should have been resolved before IL gen)");
					if (obj29 == null)
					{
						nint num5 = (nint)memberInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1583 @ rdx_v80 (Il2CppClass<System.Reflection.MemberInfo>)+268] (should have been resolved before IL gen)");
						if (obj30 == null)
						{
							goto IL_08c2;
						}
					}
					string name4 = ((MemberInfo)obj24).Name;
					string text5 = text3 + "." + name4;
					list2.Add(text5);
					tuple2 = (obj, (PropertyInfo)obj24);
					bool flag11 = _properties.TryAdd(text5, ((object, PropertyInfo))(&tuple6));
					tuple6 = ((object, MethodInfo))0;
					tuple2 = ((object, PropertyInfo))0;
					tuple8 = ((object, PropertyInfo))0;
					flag3 = false;
					text3 = path;
				}
				goto IL_08c2;
			}
			goto IL_088b;
			IL_08c2:
			obj8++;
			obj24 += 8;
			obj26 = obj8;
		}
		obj6 = obj7;
		goto IL_086e;
	}

	public unsafe object GetAndCacheObjectAtPath(string path)
	{
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Expected O, but got Unknown
		//IL_03e8: Expected O, but got I4
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Expected O, but got Unknown
		//IL_0808: Expected O, but got I
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Expected O, but got Unknown
		//IL_053c: Expected O, but got I4
		//IL_08a4: Expected O, but got I
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Expected O, but got Unknown
		//IL_08c4: Expected native int or pointer, but got O
		//IL_04b5: Expected O, but got I4
		//IL_092b: Expected O, but got I
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Expected O, but got Unknown
		//IL_0958: Expected O, but got I
		//IL_0953: Expected native int or pointer, but got O
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Expected O, but got Unknown
		//IL_090a: Expected O, but got I
		//IL_090a: Expected O, but got I
		//IL_081d: Expected O, but got I
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Expected O, but got Unknown
		//IL_083d: Expected native int or pointer, but got O
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Expected O, but got Unknown
		//IL_0999: Expected O, but got I
		//IL_0999: Expected O, but got I
		//IL_0652: Expected I4, but got O
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Expected O, but got Unknown
		//IL_0883: Expected O, but got I
		//IL_0883: Expected O, but got I
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Expected O, but got Unknown
		//IL_02e2: Expected O, but got I
		//IL_024c: Expected O, but got I
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_06d6: Expected O, but got I
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Expected O, but got Unknown
		//IL_0703: Expected O, but got I
		//IL_06fe: Expected native int or pointer, but got O
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Expected O, but got Unknown
		//IL_0744: Expected O, but got I
		//IL_0744: Expected O, but got I
		//IL_0344: Expected O, but got I
		string[] array;
		object obj;
		string key;
		if (!string.IsNullOrEmpty(path) && Target != null)
		{
			array = path.Split('.');
			if (array.Length != 0)
			{
				if (!(path != "gameObject"))
				{
					bool flag = _components.TryAdd(path, Target);
					return Target;
				}
				if (path.StartsWith("gameObject"))
				{
					obj = Target;
					key = path;
					goto IL_0a83;
				}
				if (array.Length <= 0)
				{
					goto IL_0a4f;
				}
				Match match = componentIndexRegex.Match(array[0]);
				if (match.Success)
				{
					_ = 0;
					GroupCollection groups = match.Groups;
					Group obj2 = groups.get_Item("index");
					if (obj2.Success)
					{
						GroupCollection groups2 = match.Groups;
						Group obj3 = groups2.get_Item("index");
						string value = obj3.Value;
						int num = int.Parse(value);
					}
					GroupCollection groups3 = match.Groups;
					Group obj4 = groups3.get_Item("type");
					string value2 = obj4.Value;
					Component[] components = Target.GetComponents<Component>();
					object obj5 = components + 32;
					Type type = null;
					Type type2 = null;
					Type type3;
					object obj7 = default(object);
					string text = default(string);
					Type type4 = default(Type);
					while (true)
					{
						bool flag2 = (nint)type2 >= components.Length;
						type3 = null;
						if (flag2)
						{
							break;
						}
						if ((nint)type < components.Length)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							object obj6 = obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1483 @ rdx_v77+1B8] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-50]");
							if (text != (string)0)
							{
								type = (Type)(type + 1);
								obj5 += 8;
								type2 = type;
								continue;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							type3 = type4;
							break;
						}
						goto IL_0a4f;
					}
					if (!((object)type3).Equals((object)null))
					{
						Component[] components2 = Target.GetComponents(type3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+38]");
						object obj8 = 0;
						int num2 = components2.Length;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+38]");
						if ((nint)num2 > (nint)0)
						{
							int num3 = components2.Length;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+38]");
							if ((nint)num3 <= (nint)0)
							{
								goto IL_0a4f;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+28]");
							key = (string)0;
							obj = components2[obj8];
							goto IL_0a83;
						}
					}
				}
				else
				{
					if (array.Length <= 0)
					{
						goto IL_0a4f;
					}
					string message = "Invalid component segment: '" + array[0] + "'";
					Debug.LogWarning(message);
				}
			}
		}
		goto IL_03b0;
		IL_0a83:
		if (array.Length != 1)
		{
			object obj9 = array + 40;
			Type type5 = default(Type);
			object obj11 = default(object);
			object obj14 = default(object);
			object obj15 = default(object);
			object obj19 = default(object);
			object obj22 = default(object);
			for (object obj10 = 1; (nint)obj10 < array.Length; obj10++, Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+38]"), obj9 = (nint)0 + (nint)8)
			{
				if ((nint)obj10 < array.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
					if (!((string)obj9).EndsWith(")"))
					{
						PropertyInfo property = type5.GetProperty((string)obj9, (BindingFlags)20);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
						if (obj11 != null && property.CanRead)
						{
							object obj12 = array.Length - 1;
							if (obj10 == obj12)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
								object obj13 = 0;
								(object, PropertyInfo) tuple = ((object, PropertyInfo))(obj14 - 64);
								_ = 0;
								System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)tuple, (obj, property));
								(object, PropertyInfo) value3 = ((object, PropertyInfo))(obj14 - 80);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-40]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1336 @ rax_v83+18]");
								nint num4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+28]");
								bool flag3 = ((Dictionary<string, (object, PropertyInfo)>)num4).TryAdd((string)0, value3);
								return obj;
							}
							object value4 = property.GetValue(obj);
							obj = value4;
						}
						else
						{
							FieldInfo field = type5.GetField((string)obj9, (BindingFlags)20);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
							if (obj15 == null)
							{
								continue;
							}
							object obj16 = array.Length - 1;
							if (obj10 == obj16)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
								object obj17 = 0;
								(object, FieldInfo) tuple2 = ((object, FieldInfo))(obj14 - 64);
								_ = 0;
								System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)tuple2, (obj, field));
								(object, FieldInfo) value5 = ((object, FieldInfo))(obj14 - 80);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-40]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1337 @ rax_v74+20]");
								nint num5 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+28]");
								bool flag4 = ((Dictionary<string, (object, FieldInfo)>)num5).TryAdd((string)0, value5);
								return obj;
							}
							object value6 = field.GetValue(obj);
							obj = value6;
						}
					}
					else
					{
						_003C_003Ec__DisplayClass14_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass14_0();
						int length = ((string)obj9).LastIndexOf("(");
						string methodName = ((string)obj9).Substring(0, length);
						CS_0024_003C_003E8__locals6.methodName = methodName;
						if (!((string)obj9).EndsWith("()"))
						{
							_003C_003Ec__DisplayClass14_1 CS_0024_003C_003E8__locals5 = new _003C_003Ec__DisplayClass14_1();
							int num6 = ((string)obj9).LastIndexOf("(");
							int startIndex = num6 + 1;
							string text2 = ((string)obj9).Substring(startIndex);
							string text3 = text2.Trim(')');
							((string)(object)CS_0024_003C_003E8__locals5)._stringLength = (int)text3;
							MethodInfo[] methods = type5.GetMethods((BindingFlags)20);
							Func<MethodInfo, bool> func = delegate(MethodInfo m)
							{
								//IL_0108: Expected I4, but got O
								string name = m.Name;
								_003C_003Ec__DisplayClass14_0 obj24 = CS_0024_003C_003E8__locals5.CS_0024_003C_003E8__locals1;
								if (name == obj24.methodName)
								{
									ParameterInfo[] parameters = m.GetParameters();
									if (parameters.Length == 1)
									{
										ParameterInfo[] parameters2 = m.GetParameters();
										if (parameters2.Length <= 0)
										{
											IndexOutOfRangeException ex = new IndexOutOfRangeException();
											return (byte)(int)ex != 0;
										}
										Type parameterType = parameters2[0].ParameterType;
										string name2 = parameterType.Name;
										return name2 == CS_0024_003C_003E8__locals5.parameterTypeName;
									}
								}
								return false;
							};
							object obj18 = obj14 - 80;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
							if (obj19 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
								object obj20 = 0;
								(object, MethodInfo) tuple3 = ((object, MethodInfo))(obj14 - 64);
								_ = 0;
								object item = obj;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-50]");
								System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)tuple3, (item, (MethodInfo)0));
								(object, MethodInfo) value7 = ((object, MethodInfo))(obj14 - 48);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-40]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1514 @ rax_v64+30]");
								nint num7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+28]");
								bool flag5 = ((Dictionary<string, (object, MethodInfo)>)num7).TryAdd((string)0, value7);
								return obj;
							}
							continue;
						}
						MethodInfo[] methods2 = type5.GetMethods((BindingFlags)20);
						Func<MethodInfo, bool> func2 = delegate(MethodInfo m)
						{
							//IL_009e: Expected I4, but got O
							if ((object)m != null)
							{
								string name = m.Name;
								bool flag8 = name == CS_0024_003C_003E8__locals6.methodName;
								if (!flag8)
								{
									return flag8;
								}
								ParameterInfo[] parameters = m.GetParameters();
								if (parameters != null)
								{
									return parameters.Length == 0;
								}
							}
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						};
						object obj21 = obj14 - 64;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
						if (obj22 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
							object obj23 = 0;
							(object, MethodInfo) tuple4 = ((object, MethodInfo))(obj14 - 80);
							_ = 0;
							object item2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-40]");
							System.Runtime.CompilerServices.Unsafe.Write((void*)(nint)tuple4, (item2, (MethodInfo)0));
							(object, MethodInfo) value8 = ((object, MethodInfo))(obj14 - 48);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-50]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1482 @ rax_v47+28]");
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+28]");
							bool flag6 = ((Dictionary<string, (object, MethodInfo)>)num8).TryAdd((string)0, value8);
							return obj;
						}
					}
					if (obj != null)
					{
						continue;
					}
					goto IL_03b0;
				}
				goto IL_0a4f;
			}
		}
		else
		{
			bool flag7 = _components.TryAdd(key, obj);
		}
		return obj;
		IL_0a4f:
		return new IndexOutOfRangeException();
		IL_03b0:
		return null;
	}

	public unsafe T Get<T>(string path)
	{
		//IL_007c: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_058c: Expected O, but got I
		//IL_00c6: Expected O, but got I8
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Expected O, but got Unknown
		//IL_0393: Expected O, but got I
		//IL_03a3: Expected O, but got I
		//IL_050d: Expected O, but got I
		//IL_050d: Expected O, but got I
		//IL_0527: Expected O, but got I
		//IL_0496: Expected O, but got Ref
		//IL_0559: Expected O, but got I4
		//IL_04b8: Expected O, but got Ref
		//IL_04b8: Expected O, but got Ref
		//IL_04c5: Expected O, but got Ref
		//IL_065b: Expected O, but got I
		//IL_065f: Expected O, but got I4
		//IL_0379: Expected O, but got I
		//IL_0379: Expected O, but got I
		//IL_02fa: Expected O, but got I
		object value = default(object);
		ref(object, FieldInfo) reference = ref *((object, FieldInfo)*)(&value);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		object obj = 0;
		object obj2 = obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj4;
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			obj4 = (nint)0 + (nint)15;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			reference = ref *((object, FieldInfo)*)null;
			object obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj5 > 0)
			{
				goto IL_05e3;
			}
		}
		obj4 = 1152921504606846960L;
		goto IL_05e3;
		IL_0642:
		string text;
		string key = text;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		IntPtr intPtr = default(IntPtr);
		return (T)((Dictionary<string, object>)(nint)intPtr).TryGetValue(key, out *(object*)null);
		IL_055e:
		return (T)new NullReferenceException();
		IL_053c:
		object obj6;
		string key2;
		bool flag = ((Dictionary<string, object>)obj6).TryGetValue(key2, out value);
		text = (string)flag;
		goto IL_0642;
		IL_0622:
		string text2;
		string message = text2 + path + "'.";
		Logger.Log(message);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		bool flag2 = ((Dictionary<string, object>)(&value)).TryGetValue(null, out *(object*)null);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		bool flag3 = ((Dictionary<string, object>)(&value)).TryGetValue((string)(&value), out *(object*)null);
		text = (string)(&value);
		goto IL_0642;
		IL_0517:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		object obj7 = 0;
		key2 = (string)obj7;
		object obj8 = default(object);
		obj6 = obj8;
		goto IL_053c;
		IL_05e3:
		object obj9 = obj4 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		if (path != null)
		{
			if (!path.EndsWith(")"))
			{
				if (_components != null)
				{
					if (!_components.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 184)))
					{
						if (_properties == null)
						{
							goto IL_055e;
						}
						if (!_properties.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.As<object, (object, PropertyInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 48))))
						{
							if (_fields == null)
							{
								goto IL_055e;
							}
							if (!_fields.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.As<object, (object, FieldInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 64))))
							{
								object andCacheObjectAtPath = GetAndCacheObjectAtPath(path);
							}
						}
					}
					if (_components != null)
					{
						if (_components.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 168)))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							obj6 = 0;
							key2 = (string)obj10;
							goto IL_053c;
						}
						if (_properties != null)
						{
							if (!_properties.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.As<object, (object, PropertyInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 16))))
							{
								if (_fields != null)
								{
									if (!_fields.TryGetValue(path, out *((object, FieldInfo)*)(&value)))
									{
										text2 = "No field or property found at path '";
										goto IL_0622;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+8]");
									object obj11 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+8]");
									if ((nint)0 != 0)
									{
										object obj12 = obj11;
										Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v627 @ r8_v25+2C8] (should have been resolved before IL gen)");
										goto IL_0517;
									}
								}
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+18]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+18]");
									nint num = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+10]");
									obj8 = ((PropertyInfo)num).GetValue(0);
									goto IL_0517;
								}
							}
						}
					}
				}
			}
			else if (_getMethods != null)
			{
				if (!_getMethods.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.As<object, (object, MethodInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 80))))
				{
					object andCacheObjectAtPath2 = GetAndCacheObjectAtPath(path);
				}
				if (_getMethods != null)
				{
					if (!_getMethods.TryGetValue(path, out System.Runtime.CompilerServices.Unsafe.As<object, (object, MethodInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value, 32))))
					{
						text2 = "No method found at path '";
						goto IL_0622;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+28]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+28]");
						nint num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+20]");
						obj8 = ((MethodBase)num2).Invoke(0, null);
						goto IL_0517;
					}
				}
			}
		}
		goto IL_055e;
	}

	public unsafe void Set<T>(string path, T value)
	{
		//IL_0056: Expected O, but got I
		//IL_007c: Expected O, but got I
		//IL_00a2: Expected O, but got I
		//IL_053b: Expected O, but got Ref
		//IL_013a: Expected O, but got Ref
		//IL_0167: Expected O, but got Ref
		//IL_0648: Expected O, but got Ref
		//IL_0925: Expected O, but got I
		//IL_095f: Expected O, but got I
		//IL_0965: Expected O, but got I
		//IL_05e8: Expected O, but got Ref
		//IL_0421: Expected O, but got Ref
		//IL_0431: Expected O, but got I
		//IL_01ba: Expected O, but got Ref
		//IL_0883: Expected O, but got I
		//IL_0897: Expected O, but got I
		//IL_08d9: Expected O, but got I
		//IL_08df: Expected O, but got I
		//IL_02e9: Expected O, but got Ref
		//IL_01e7: Expected O, but got Ref
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Expected O, but got Unknown
		//IL_074a: Expected O, but got I
		//IL_0485: Expected O, but got I
		//IL_06a8: Expected I, but got O
		//IL_06b8: Expected O, but got I
		//IL_06e7: Expected O, but got I
		//IL_03ad: Expected O, but got Ref
		//IL_023a: Expected O, but got Ref
		//IL_0781: Expected O, but got I
		//IL_0781: Expected O, but got I
		//IL_07f8: Expected O, but got I
		//IL_080c: Expected O, but got I
		//IL_084e: Expected O, but got I
		//IL_0854: Expected O, but got I
		//IL_034b: Expected O, but got Ref
		//IL_040e: Expected O, but got I
		(object, FieldInfo) value2 = default((object, FieldInfo));
		ref(object, FieldInfo) reference = ref value2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v380 @ r9_v2 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
		if ((nint)obj > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = 0;
			_ = 0;
			_ = 0;
			_ = 0;
			_ = ref value2;
			_ = 0;
			_ = 0;
			reference = ref *((object, FieldInfo)*)null;
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)0);
		string text = default(string);
		bool flag = text == null;
		T val = value;
		string text2 = null;
		RuntimeTypeHandle runtimeTypeHandle = (RuntimeTypeHandle)0;
		string name;
		string text3;
		string text4;
		object obj2;
		if (!flag)
		{
			if (!text.EndsWith(")"))
			{
				bool flag2 = _components == null;
				val = (T)null;
				text2 = ")";
				runtimeTypeHandle = (RuntimeTypeHandle)_components;
				if (!flag2)
				{
					ref object reference2 = ref System.Runtime.CompilerServices.Unsafe.As<(object, FieldInfo), object>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 184));
					bool flag3 = _components.TryGetValue(text, out reference2);
					nint num2 = 0;
					val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2);
					if (!flag3)
					{
						bool flag4 = _properties == null;
						num2 = 0;
						val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2);
						text2 = text;
						runtimeTypeHandle = (RuntimeTypeHandle)_properties;
						if (flag4)
						{
							goto IL_078a;
						}
						ref(object, PropertyInfo) reference3 = ref System.Runtime.CompilerServices.Unsafe.As<(object, FieldInfo), (object, PropertyInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 48));
						bool flag5 = _properties.TryGetValue(text, out reference3);
						num2 = 0;
						val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference3);
						if (!flag5)
						{
							bool flag6 = _fields == null;
							num2 = 0;
							val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference3);
							text2 = text;
							runtimeTypeHandle = (RuntimeTypeHandle)_fields;
							if (flag6)
							{
								goto IL_078a;
							}
							ref(object, FieldInfo) reference4 = ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 64);
							bool flag7 = _fields.TryGetValue(text, out reference4);
							num2 = 0;
							val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference4);
							if (!flag7)
							{
								object andCacheObjectAtPath = GetAndCacheObjectAtPath(text);
								num2 = 0;
								val = (T)null;
							}
						}
					}
					bool flag8 = _properties == null;
					text2 = text;
					runtimeTypeHandle = (RuntimeTypeHandle)_properties;
					if (!flag8)
					{
						ref(object, PropertyInfo) reference5 = ref System.Runtime.CompilerServices.Unsafe.As<(object, FieldInfo), (object, PropertyInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 16));
						if (!_properties.TryGetValue(text, out reference5))
						{
							bool flag9 = _fields == null;
							num2 = 0;
							val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference5);
							text2 = text;
							runtimeTypeHandle = (RuntimeTypeHandle)_fields;
							if (!flag9)
							{
								if (!_fields.TryGetValue(text, out value2))
								{
									bool flag10 = (object)typeFromHandle == null;
									num2 = 0;
									val = (T)(&value2);
									text2 = text;
									runtimeTypeHandle = (RuntimeTypeHandle)_fields;
									if (!flag10)
									{
										name = typeFromHandle.Name;
										text3 = "' for type ";
										text4 = "No field or property found at path '";
										goto IL_08ed;
									}
								}
								else
								{
									string text5 = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 176));
									obj2 = reference;
									nint num3 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v679 @ rcx_v42 (Il2CppClass<T>)+28]");
									if ((nint)0 < (nint)0)
									{
										text5 = (string)value;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
									nint num4 = 0;
									string key = text5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
									bool flag11 = ((Dictionary<string, (object, FieldInfo)>)num4).TryGetValue(key, out *((object, FieldInfo)*)null);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
									text2 = (string)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
									object value3 = (nint)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+8]");
									bool flag12 = (nint)0 == 0;
									num2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
									val = (T)0;
									runtimeTypeHandle = (RuntimeTypeHandle)0;
									if (!flag12)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+8]");
										((FieldInfo)0).SetValue(obj2, value3);
										goto IL_048a;
									}
								}
							}
						}
						else
						{
							string text6 = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 176));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+10]");
							obj2 = 0;
							nint num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v36 (Il2CppClass<T>)+28]");
							if ((nint)0 < (nint)0)
							{
								text6 = (string)value;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							nint num6 = 0;
							string key2 = text6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
							bool flag13 = ((Dictionary<string, (object, PropertyInfo)>)num6).TryGetValue(key2, out *((object, PropertyInfo)*)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							text2 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							object value4 = (nint)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+18]");
							bool flag14 = (nint)0 == 0;
							num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
							val = (T)0;
							runtimeTypeHandle = (RuntimeTypeHandle)0;
							if (!flag14)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+18]");
								((PropertyInfo)0).SetValue(obj2, value4);
								goto IL_048a;
							}
						}
					}
				}
			}
			else
			{
				bool flag15 = _setMethods == null;
				val = (T)null;
				text2 = ")";
				runtimeTypeHandle = (RuntimeTypeHandle)_setMethods;
				if (!flag15)
				{
					ref(object, MethodInfo) reference6 = ref System.Runtime.CompilerServices.Unsafe.As<(object, FieldInfo), (object, MethodInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 80));
					bool flag16 = _setMethods.TryGetValue(text, out reference6);
					val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference6);
					if (!flag16)
					{
						object andCacheObjectAtPath2 = GetAndCacheObjectAtPath(text);
						val = (T)null;
					}
					bool flag17 = _setMethods == null;
					nint num2 = 0;
					text2 = text;
					runtimeTypeHandle = (RuntimeTypeHandle)_setMethods;
					if (!flag17)
					{
						ref(object, MethodInfo) reference7 = ref System.Runtime.CompilerServices.Unsafe.As<(object, FieldInfo), (object, MethodInfo)>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 32));
						if (!_setMethods.TryGetValue(text, out reference7))
						{
							bool flag18 = (object)typeFromHandle == null;
							num2 = 0;
							val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference7);
							text2 = text;
							runtimeTypeHandle = (RuntimeTypeHandle)_setMethods;
							if (!flag18)
							{
								name = typeFromHandle.Name;
								text3 = "' with parameter type ";
								text4 = "No method found at path '";
								goto IL_08ed;
							}
						}
						else
						{
							object[] array = new object[1];
							T val2 = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref value2, 176));
							nint num7 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r9_v8 (Il2CppClass<T>)+28]");
							if ((nint)0 < (nint)0)
							{
								val2 = value;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							text2 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+A8]");
							RuntimeTypeHandle runtimeTypeHandle2 = (RuntimeTypeHandle)(object)(nint)0;
							bool flag19 = array == null;
							num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
							val = (T)0;
							runtimeTypeHandle = (RuntimeTypeHandle)0;
							if (!flag19)
							{
								if ((object)runtimeTypeHandle2 != null)
								{
									nint num8 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v788 @ rdx_v20 (Il2CppClass<System.Object[]>)+40]");
									text2 = (string)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj3 = default(object);
									bool flag20 = obj3 == null;
									num2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
									val = (T)0;
									runtimeTypeHandle = runtimeTypeHandle2;
									if (flag20)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj4 = default(object);
										throw obj4;
									}
								}
								runtimeTypeHandle = (RuntimeTypeHandle)(array + 32);
								array[0] = runtimeTypeHandle2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+28]");
								bool flag21 = (nint)0 == 0;
								num2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2 (Il2CppClass<T>)+FC]");
								val = (T)0;
								text2 = (string)runtimeTypeHandle2;
								if (!flag21)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+28]");
									nint num9 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.ValueTuple`2<System.Object, System.Reflection.FieldInfo>&)+20]");
									object obj5 = ((MethodBase)num9).Invoke(0, array);
									return;
								}
							}
						}
					}
				}
			}
		}
		goto IL_078a;
		IL_08ed:
		string message = text4 + text + text3 + name;
		Logger.Log(message);
		return;
		IL_048a:
		if (isStruct(obj2))
		{
			string parentPath = getParentPath(text);
			Set(parentPath, obj2);
		}
		return;
		IL_078a:
		throw new NullReferenceException();
	}

	private string getParentPath(string path)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F218]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (path != null)
		{
			int length = path.LastIndexOf(".");
			return path.Substring(0, length);
		}
		return (string)(object)new NullReferenceException();
	}

	private bool isInspectableType(Type type)
	{
		//IL_00f5: Expected I4, but got O
		//IL_00c0: Expected O, but got I
		if ((object)type != null)
		{
			if (!type.IsPrimitive)
			{
				if (type.IsClass)
				{
					return true;
				}
				if (type.IsValueType && !type.IsEnum)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle = Type.GetTypeFromHandle(handle);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					bool result = default(bool);
					return result;
				}
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool isStruct(object obj)
	{
		//IL_010b: Expected I4, but got O
		//IL_00d6: Expected O, but got I
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			Type type = default(Type);
			if ((object)type != null)
			{
				if (!type.IsPrimitive && !type.IsClass && type.IsValueType && !type.IsEnum)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle = Type.GetTypeFromHandle(handle);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					bool result = default(bool);
					return result;
				}
				return false;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public bool IsSettingCompatibleWithPath(SettingsProvider provider, string settingId, string path, bool defaultResult = true)
	{
		//IL_0167: Expected I4, but got O
		if (!(provider != null) || string.IsNullOrEmpty(path) || string.IsNullOrEmpty(settingId))
		{
			goto IL_014c;
		}
		if ((object)provider != null)
		{
			Settings settingsAssetOrRuntimeCopy = provider.GetSettingsAssetOrRuntimeCopy();
			if (!(settingsAssetOrRuntimeCopy != null))
			{
				goto IL_014c;
			}
			if ((object)settingsAssetOrRuntimeCopy != null)
			{
				ISetting setting = settingsAssetOrRuntimeCopy.GetSetting(settingId);
				if (setting == null)
				{
					goto IL_014c;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (SettingData.CompatibleTypes != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
					Type typeOfPath = GetTypeOfPath(path);
					List<Type> list = default(List<Type>);
					if (list != null)
					{
						return list.Contains(typeOfPath);
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_014c:
		bool result = default(bool);
		return result;
	}

	static GameObjectInspector()
	{
		Regex regex = new Regex("^(?<type>[a-zA-Z0-9_]+)(\\[(?<index>\\d+)\\])?$");
		componentIndexRegex = regex;
	}
}
