using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

public static class NodeDataCache
{
	[Serializable]
	private class PortDataCache : Dictionary<Type, List<NodePort>>, ISerializationCallbackReceiver
	{
		private List<Type> keys;

		private List<List<NodePort>> values;

		public unsafe void OnBeforeSerialize()
		{
			//IL_0176: Expected O, but got I4
			//IL_01ae: Expected O, but got Ref
			List<Type> list = keys;
			int version = list._version + 1;
			list._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				list._size = 0;
			}
			else
			{
				list._size = 0;
				if (list._size > 0)
				{
					Array.Clear(list._items, 0, list._size);
				}
			}
			List<List<NodePort>> list2 = values;
			int version2 = list2._version + 1;
			list2._version = version2;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<List<NodePort>>())
			{
				list2._size = 0;
			}
			else
			{
				list2._size = 0;
				if (list2._size > 0)
				{
					Array.Clear(list2._items, 0, list2._size);
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			object obj2 = 0;
			Enumerator enumerator = default(Enumerator);
			Type item = default(Type);
			List<NodePort> item2 = default(List<NodePort>);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
					bool flag = keys == null;
					object obj3 = (object)(&obj2);
					if (flag)
					{
						break;
					}
					keys.Add(item);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					values.Add(item2);
					continue;
				}
				enumerator.Dispose();
				return;
			}
			throw new NullReferenceException();
		}

		public unsafe void OnAfterDeserialize()
		{
			//IL_0078: Expected I, but got O
			//IL_007d: Expected I, but got O
			//IL_00cc: Expected O, but got Ref
			//IL_012e: Expected I, but got O
			//IL_0152: Expected I, but got O
			Clear();
			List<Type> list = keys;
			bool flag = keys == null;
			nint num = 0;
			if (!flag)
			{
				List<List<NodePort>> list2 = values;
				bool flag2 = values == null;
				num = 0;
				if (!flag2)
				{
					bool flag3 = list._size != list2._size;
					num = 0;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800094C0");
						throw new NullReferenceException();
					}
					List<Type> list3 = keys;
					num = 0;
					nint num2 = unchecked((nint)null);
					nint num3 = unchecked((nint)null);
					Type type = default(Type);
					List<NodePort> list5 = default(List<NodePort>);
					while (true)
					{
						if (num3 < list3._size)
						{
							if (keys == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							bool flag4 = values == null;
							num = num2;
							List<NodePort> list4 = (List<NodePort>)(&type);
							nint num4 = 0;
							if (flag4)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Add(type, list5);
							list3 = keys;
							num2++;
							bool flag5 = keys == null;
							num = (nint)type;
							list4 = list5;
							num4 = 0;
							if (flag5)
							{
								break;
							}
							num = (nint)type;
							list4 = list5;
							num4 = 0;
							num3 = num2;
							continue;
						}
						return;
					}
				}
			}
			throw new NullReferenceException();
		}

		public PortDataCache()
		{
			List<Type> list = new List<Type>();
			keys = list;
			values = new List<List<NodePort>>();
			base._002Ector();
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<object, bool> _003C_003E9__6_0;

		public static Func<object, bool> _003C_003E9__6_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal bool _003CCachePorts_003Eb__6_0(object x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = x == null;
			object obj = null;
			object obj4;
			if (!flag)
			{
				nint num = (nint)typeof(Node.InputAttribute);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+C8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(Node.InputAttribute);
					obj4 = 1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				obj4 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = obj4 == null;
			obj = null;
			if (!flag3)
			{
				obj = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = obj == null;
			return !flag4;
		}

		internal bool _003CCachePorts_003Eb__6_1(object x)
		{
			//IL_0013: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			bool flag = x == null;
			object obj = null;
			object obj4;
			if (!flag)
			{
				nint num = (nint)typeof(Node.OutputAttribute);
				nint num2 = (nint)x;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+C8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
					bool flag2 = 0 == (nint)typeof(Node.OutputAttribute);
					obj4 = 1;
					if (flag2)
					{
						goto IL_00d3;
					}
				}
				obj4 = null;
				goto IL_00d3;
			}
			goto IL_00f5;
			IL_00d3:
			bool flag3 = obj4 == null;
			obj = null;
			if (!flag3)
			{
				obj = x;
			}
			goto IL_00f5;
			IL_00f5:
			bool flag4 = obj == null;
			return !flag4;
		}
	}

	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public Type baseType;

		public Func<Type, bool> _003C_003E9__0;

		internal bool _003CBuildCache_003Eb__0(Type t)
		{
			//IL_0092: Expected I4, but got O
			//IL_007a: Expected I, but got O
			if ((object)t != null)
			{
				if (t.IsAbstract)
				{
					return false;
				}
				Type type = baseType;
				if ((object)baseType != null)
				{
					nint num = (nint)type;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v87 @ r8_v1 (Il2CppClass<System.Type>)+298] (should have been resolved before IL gen)");
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private static PortDataCache portDataCache;

	private static bool Initialized
	{
		get
		{
			bool flag = (nint)portDataCache < 0;
			bool flag2 = portDataCache == null;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			return flag4 & flag3;
		}
	}

	public unsafe static void UpdatePorts(Node node, Dictionary<string, NodePort> ports)
	{
		//IL_0273: Expected O, but got Ref
		//IL_0298: Expected O, but got Ref
		//IL_01a4: Expected O, but got I
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_0527: Expected O, but got Ref
		//IL_05ca: Expected O, but got I4
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Expected O, but got Unknown
		if ((nint)portDataCache <= 0)
		{
			BuildCache();
		}
		Dictionary<string, NodePort> dictionary = new Dictionary<string, NodePort>();
		Dictionary<string, List<NodePort>> dictionary2 = new Dictionary<string, List<NodePort>>();
		if ((object)node != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			if (portDataCache != null)
			{
				Type key = default(Type);
				if (portDataCache.TryGetValue(key, out var value))
				{
					bool flag = value == null;
					List<NodePort> list = value;
					Dictionary<string, List<NodePort>> dictionary3 = null;
					Dictionary<string, List<NodePort>> dictionary4 = null;
					if (flag)
					{
						goto IL_06be;
					}
					object obj = default(object);
					Dictionary<string, List<NodePort>> dictionary5 = default(Dictionary<string, List<NodePort>>);
					NodePort value2 = default(NodePort);
					while ((nint)dictionary3 < list._size)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj != null && portDataCache != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
							if (dictionary5 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (dictionary != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ stack_-50_v14+18]");
									dictionary.Add((string)0, value2);
									dictionary4 = (Dictionary<string, List<NodePort>>)(dictionary4 + 1);
									if (value != null)
									{
										list = value;
										dictionary3 = dictionary4;
										continue;
									}
								}
							}
						}
						goto IL_06be;
					}
				}
				if (ports != null)
				{
					Dictionary<string, NodePort>.ValueCollection values = ports.Values;
					List<NodePort> list2 = Enumerable.ToList(values);
					if (list2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						NodePort value3 = null;
						List<NodePort>.Enumerator enumerator = default(List<NodePort>.Enumerator);
						List<NodePort>.Enumerator enumerator2 = default(List<NodePort>.Enumerator);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							bool flag2 = (object)enumerator2 == null;
							NodePort nodePort = (NodePort)(&enumerator);
							if (!flag2)
							{
								bool flag3 = dictionary == null;
								nodePort = (NodePort)(&enumerator);
								if (!flag3)
								{
									bool flag4 = dictionary.TryGetValue(((NodePort)enumerator2)._fieldName, out value3);
									if (!flag4)
									{
										if (((NodePort)enumerator2)._dynamic != flag4)
										{
											continue;
										}
									}
									else if (!((NodePort)enumerator2)._dynamic)
									{
										if (0 == 0)
										{
											throw new NullReferenceException();
										}
										if (((NodePort)enumerator2)._direction == value3._direction && ((NodePort)enumerator2)._connectionType == value3._connectionType && ((NodePort)enumerator2)._typeConstraint == value3._typeConstraint)
										{
											Type valueType = ((NodePort)null).ValueType;
											((NodePort)enumerator2).ValueType = valueType;
											continue;
										}
										if (!((NodePort)enumerator2)._dynamic)
										{
											if (0 == 0)
											{
												throw new NullReferenceException();
											}
											if (((NodePort)enumerator2)._direction == value3._direction)
											{
												List<NodePort> connections = ((NodePort)enumerator2).GetConnections();
												if (dictionary2 == null)
												{
													throw new NullReferenceException();
												}
												dictionary2.Add(((NodePort)enumerator2)._fieldName, connections);
											}
										}
									}
									((NodePort)enumerator2).ClearConnections();
									bool flag5 = ports.Remove(((NodePort)enumerator2)._fieldName);
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						if (dictionary != null)
						{
							Dictionary<string, NodePort>.ValueCollection values2 = dictionary.Values;
							if (values2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
								Node node2 = node;
								Dictionary<string, NodePort> dictionary6 = dictionary;
								Dictionary<string, NodePort>.ValueCollection.Enumerator enumerator3 = default(Dictionary<string, NodePort>.ValueCollection.Enumerator);
								NodePort nodePort2 = default(NodePort);
								while (true)
								{
									if (enumerator3.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										bool flag6 = nodePort2 == null;
										NodePort nodePort = (NodePort)(&enumerator3);
										if (flag6)
										{
											break;
										}
										if (ports.ContainsKey(nodePort2._fieldName))
										{
											continue;
										}
										NodePort nodePort3 = new NodePort(nodePort2, node2);
										if (dictionary2 != null)
										{
											if (dictionary2.TryGetValue(nodePort2._fieldName, out var value4))
											{
												object obj2 = 0;
												while (true)
												{
													if (value4 != null)
													{
														object obj3 = obj2;
														string fieldName = ((NodePort)(object)value4)._fieldName;
														if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) >= System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref fieldName))
														{
															break;
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														if (dictionary != null)
														{
															if (nodePort3 == null)
															{
																throw new NullReferenceException();
															}
															if (nodePort3.CanConnectTo((NodePort)(object)dictionary))
															{
																nodePort3.Connect((NodePort)(object)dictionary);
															}
														}
														obj2++;
														continue;
													}
													throw new NullReferenceException();
												}
												node2 = node;
											}
											if (nodePort2 != null)
											{
												ports.Add(nodePort2._fieldName, nodePort3);
												continue;
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									enumerator3.Dispose();
									return;
								}
								throw new NullReferenceException();
							}
						}
					}
				}
			}
		}
		goto IL_06be;
		IL_06be:
		throw new NullReferenceException();
	}

	private static void BuildCache()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00ad: Expected O, but got I4
		//IL_00b6: Expected O, but got I4
		//IL_02bb: Expected O, but got I4
		//IL_02c4: Expected O, but got I4
		//IL_00ef: Expected O, but got I
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_010a: Expected O, but got I
		//IL_036c: Expected I, but got O
		//IL_0176: Expected I, but got O
		//IL_0145: Expected O, but got I
		//IL_0152: Expected O, but got I4
		//IL_019b: Expected I, but got O
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_01c0: Expected I, but got O
		//IL_0241: Expected O, but got I4
		_003C_003Ec__DisplayClass4_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass4_0();
		PortDataCache portDataCache = new PortDataCache();
		List<Type> keys = new List<Type>();
		portDataCache.keys = keys;
		List<List<NodePort>> values = new List<List<NodePort>>();
		portDataCache.values = values;
		portDataCache._002Ector();
		NodeDataCache.portDataCache = portDataCache;
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Node));
		CS_0024_003C_003E8__locals6.baseType = typeFromHandle;
		List<Type> list = new List<Type>();
		AppDomain curDomain = AppDomain.getCurDomain();
		Assembly[] assemblies = curDomain.GetAssemblies();
		object obj = assemblies + 32;
		object obj2 = 0;
		object obj3 = 0;
		IEnumerable<Type> source2 = default(IEnumerable<Type>);
		while ((nint)obj3 < assemblies.Length)
		{
			object obj4 = obj;
			object obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v417 @ rdx_v17+278] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v32+10]");
			string text = (string)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v32+10]");
			int num = ((string)0).IndexOf('.');
			if (num != -1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rax_v32+10]");
				string text2 = ((string)0).Substring(0, num);
				object obj6 = 0;
				text = text2;
			}
			bool flag = text != "UnityEditor";
			nint num2 = unchecked((nint)null);
			if (flag)
			{
				bool flag2 = text != "UnityEngine";
				num2 = unchecked((nint)null);
				if (flag2)
				{
					bool flag3 = text != "System";
					num2 = unchecked((nint)null);
					if (flag3)
					{
						bool flag4 = text != "mscorlib";
						num2 = unchecked((nint)null);
						if (flag4)
						{
							object obj7 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v513 @ rdx_v26+248] (should have been resolved before IL gen)");
							Func<Type, bool> predicate = CS_0024_003C_003E8__locals6._003C_003E9__0;
							if (CS_0024_003C_003E8__locals6._003C_003E9__0 == null)
							{
								Func<Type, bool> func = (CS_0024_003C_003E8__locals6._003C_003E9__0 = delegate(Type t)
								{
									//IL_0092: Expected I4, but got O
									//IL_007a: Expected I, but got O
									if ((object)t != null)
									{
										if (t.IsAbstract)
										{
											return false;
										}
										Type baseType = CS_0024_003C_003E8__locals6.baseType;
										if ((object)CS_0024_003C_003E8__locals6.baseType != null)
										{
											nint num3 = (nint)baseType;
											Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v87 @ r8_v1 (Il2CppClass<System.Type>)+298] (should have been resolved before IL gen)");
										}
									}
									NullReferenceException ex = new NullReferenceException();
									return (byte)(int)ex != 0;
								});
								object obj6 = 0;
								predicate = func;
							}
							IEnumerable<Type> source = Enumerable.Where(source2, predicate);
							Type[] collection = Enumerable.ToArray(source);
							list.AddRange(collection);
							num2 = 0;
						}
					}
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		object obj8 = 0;
		object obj9 = 0;
		Type nodeType = default(Type);
		while ((nint)obj9 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			CachePorts(nodeType);
			obj8++;
			obj9 = obj8;
		}
	}

	public static List<FieldInfo> GetNodeFields(Type nodeType)
	{
		if ((object)nodeType != null)
		{
			FieldInfo[] fields = nodeType.GetFields((BindingFlags)52);
			List<FieldInfo> list = new List<FieldInfo>(fields);
			Type type = nodeType;
			object obj = default(object);
			while (true)
			{
				Type baseType = type.BaseType;
				Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Node));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
				if (obj != null)
				{
					if ((object)baseType == null)
					{
						break;
					}
					IEnumerable<FieldInfo> fields2 = baseType.GetFields((BindingFlags)36);
					if (list == null)
					{
						break;
					}
					list.AddRange(fields2);
					type = baseType;
					continue;
				}
				return list;
			}
		}
		return (List<FieldInfo>)(object)new NullReferenceException();
	}

	private static void CachePorts(Type nodeType)
	{
		//IL_00b0: Expected O, but got I4
		//IL_00ba: Expected O, but got I4
		//IL_0154: Expected I, but got O
		//IL_016c: Expected O, but got I
		//IL_01ec: Expected O, but got I4
		//IL_0141: Expected O, but got I4
		//IL_053b: Expected O, but got I4
		//IL_01a8: Expected O, but got I
		//IL_01de: Expected O, but got I4
		//IL_024f: Expected I, but got O
		//IL_0257: Expected I, but got O
		//IL_0267: Expected O, but got I
		//IL_02e7: Expected O, but got I4
		//IL_023c: Expected O, but got I4
		//IL_05ac: Expected O, but got I4
		//IL_02a3: Expected O, but got I
		//IL_02d9: Expected O, but got I4
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Expected O, but got Unknown
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		FieldInfo[] fields = nodeType.GetFields((BindingFlags)52);
		List<FieldInfo> list = new List<FieldInfo>(fields);
		Type type = nodeType;
		object obj = default(object);
		while (true)
		{
			Type baseType = type.BaseType;
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Node));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
			if (obj == null)
			{
				break;
			}
			IEnumerable<FieldInfo> fields2 = baseType.GetFields((BindingFlags)36);
			list.AddRange(fields2);
			type = baseType;
		}
		object obj2 = 0;
		object obj3 = 0;
		object obj5 = default(object);
		object obj6 = default(object);
		object obj12 = default(object);
		object obj18 = default(object);
		object obj19 = default(object);
		FieldInfo fieldInfo = default(FieldInfo);
		List<NodePort> list2 = default(List<NodePort>);
		while ((nint)obj3 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj4 = obj5;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v495 @ r8_v10+208] (should have been resolved before IL gen)");
			Func<object, bool> func = _003C_003Ec._003C_003E9__6_0;
			if (_003C_003Ec._003C_003E9__6_0 == null)
			{
				func = (_003C_003Ec._003C_003E9__6_0 = delegate(object x)
				{
					//IL_0013: Expected I, but got O
					//IL_001b: Expected I, but got O
					//IL_002b: Expected O, but got I
					//IL_0067: Expected O, but got I
					//IL_008c: Expected O, but got I4
					bool flag3 = x == null;
					object obj20 = null;
					object obj23;
					if (!flag3)
					{
						nint num6 = (nint)typeof(Node.InputAttribute);
						nint num7 = (nint)x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
						object obj21 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+130]");
						nint num8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
						if (num8 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+C8]");
							object obj22 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
							bool flag4 = 0 == (nint)typeof(Node.InputAttribute);
							obj23 = 1;
							if (flag4)
							{
								goto IL_00d3;
							}
						}
						obj23 = null;
						goto IL_00d3;
					}
					goto IL_00f5;
					IL_00d3:
					bool flag5 = obj23 == null;
					obj20 = null;
					if (!flag5)
					{
						obj20 = x;
					}
					goto IL_00f5;
					IL_00f5:
					bool flag6 = obj20 == null;
					return !flag6;
				});
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
			object obj7;
			if (obj6 == null)
			{
				obj7 = 0;
				goto IL_05ba;
			}
			nint num = (nint)typeof(Node.InputAttribute);
			object obj8 = obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v602 @ rdx_v44 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r9_v19+130]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v602 @ rdx_v44 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
			object obj11;
			if (num2 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ r9_v19+C8]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v666 @ rax_v82+FFFFFFF8+v604 @ rax_v78*8]");
				if (0 == (nint)typeof(Node.InputAttribute))
				{
					obj11 = 1;
					goto IL_0523;
				}
			}
			obj11 = 0;
			goto IL_0523;
			IL_039a:
			obj2++;
			obj3 = obj2;
			continue;
			IL_05ba:
			Func<object, bool> func2 = _003C_003Ec._003C_003E9__6_1;
			if (_003C_003Ec._003C_003E9__6_1 == null)
			{
				func2 = (_003C_003Ec._003C_003E9__6_1 = delegate(object x)
				{
					//IL_0013: Expected I, but got O
					//IL_001b: Expected I, but got O
					//IL_002b: Expected O, but got I
					//IL_0067: Expected O, but got I
					//IL_008c: Expected O, but got I4
					bool flag3 = x == null;
					object obj20 = null;
					object obj23;
					if (!flag3)
					{
						nint num6 = (nint)typeof(Node.OutputAttribute);
						nint num7 = (nint)x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
						object obj21 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+130]");
						nint num8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r8_v2 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
						if (num8 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r9_v2 (Il2CppClass<System.Object>)+C8]");
							object obj22 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ rax_v8+FFFFFFF8+v40 @ rax_v4*8]");
							bool flag4 = 0 == (nint)typeof(Node.OutputAttribute);
							obj23 = 1;
							if (flag4)
							{
								goto IL_00d3;
							}
						}
						obj23 = null;
						goto IL_00d3;
					}
					goto IL_00f5;
					IL_00d3:
					bool flag5 = obj23 == null;
					obj20 = null;
					if (!flag5)
					{
						obj20 = x;
					}
					goto IL_00f5;
					IL_00f5:
					bool flag6 = obj20 == null;
					return !flag6;
				});
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
			object obj13;
			nint num3;
			if (obj12 == null)
			{
				num3 = 0;
				obj13 = 0;
				goto IL_0574;
			}
			nint num4 = (nint)typeof(Node.OutputAttribute);
			num3 = (nint)obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v784 @ rdx_v41 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r9_v9 (Il2CppMethodInfo)+130]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v784 @ rdx_v41 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj16;
			if (num5 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r9_v9 (Il2CppMethodInfo)+C8]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v841 @ rax_v67+FFFFFFF8+v786 @ rax_v62*8]");
				if (0 == (nint)typeof(Node.OutputAttribute))
				{
					obj16 = 1;
					goto IL_0594;
				}
			}
			obj16 = 0;
			goto IL_0594;
			IL_0594:
			bool flag = obj16 == null;
			obj13 = 0;
			if (!flag)
			{
				obj13 = obj12;
			}
			goto IL_0574;
			IL_0523:
			bool flag2 = obj11 == null;
			obj7 = 0;
			if (!flag2)
			{
				obj7 = obj6;
			}
			goto IL_05ba;
			IL_0574:
			if (obj7 == null)
			{
				if (obj13 == null)
				{
					goto IL_039a;
				}
			}
			else if (obj13 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj17 = obj18;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1029 @ rdx_v22+1B8] (should have been resolved before IL gen)");
				string fullName = nodeType.FullName;
				string message = "Field " + (string)obj19 + " of type " + fullName + " cannot be both input and output.";
				Debug.LogError(message);
				obj2++;
				obj3 = obj2;
				continue;
			}
			if (!portDataCache.ContainsKey(nodeType))
			{
				List<NodePort> value = new List<NodePort>();
				portDataCache.Add(nodeType, value);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			NodePort item = new NodePort(fieldInfo);
			list2.Add(item);
			goto IL_039a;
		}
	}
}
