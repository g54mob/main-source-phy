using System;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

[Serializable]
public class NodePort
{
	public enum IO
	{
		Input,
		Output
	}

	[Serializable]
	private class PortConnection
	{
		public string fieldName;

		public Node node;

		[NonSerialized]
		private NodePort port;

		public List<Vector2> reroutePoints;

		public NodePort Port
		{
			get
			{
				if (port != null)
				{
					return port;
				}
				NodePort result;
				if (node != null && !string.IsNullOrEmpty(fieldName))
				{
					if ((object)node == null)
					{
						return (NodePort)(object)new NullReferenceException();
					}
					NodePort nodePort = node.GetPort(fieldName);
					result = nodePort;
				}
				else
				{
					result = null;
				}
				port = result;
				return result;
			}
		}

		public PortConnection(NodePort port)
		{
			List<Vector2> list = new List<Vector2>();
			reroutePoints = list;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			this.port = port;
			node = port._node;
			fieldName = port._fieldName;
		}

		private NodePort GetPort()
		{
			if (node != null && !string.IsNullOrEmpty(fieldName))
			{
				if ((object)node != null)
				{
					return node.GetPort(fieldName);
				}
				return (NodePort)(object)new NullReferenceException();
			}
			return null;
		}
	}

	private Type valueType;

	private string _fieldName;

	private Node _node;

	private string _typeQualifiedName;

	private List<PortConnection> connections;

	private IO _direction;

	private Node.ConnectionType _connectionType;

	private Node.TypeConstraint _typeConstraint;

	private bool _dynamic;

	public int ConnectionCount
	{
		get
		{
			//IL_001d: Expected I4, but got O
			List<PortConnection> list = connections;
			if (connections != null)
			{
				return list._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public NodePort Connection
	{
		get
		{
			//IL_000e: Expected O, but got I4
			//IL_0017: Expected O, but got I4
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Expected O, but got Unknown
			List<PortConnection> list = connections;
			if (connections != null)
			{
				object obj = 0;
				object obj2 = 0;
				List<PortConnection> list2 = connections;
				PortConnection portConnection = default(PortConnection);
				while (true)
				{
					if ((nint)obj2 < list._size)
					{
						if (list2 == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						list2 = connections;
						if (portConnection == null)
						{
							obj++;
							if (connections == null)
							{
								break;
							}
							obj2 = obj;
							list = connections;
							continue;
						}
						if (connections == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (portConnection == null)
						{
							break;
						}
						return portConnection.Port;
					}
					return null;
				}
			}
			return (NodePort)(object)new NullReferenceException();
		}
	}

	public IO direction => _direction;

	public Node.ConnectionType connectionType => _connectionType;

	public Node.TypeConstraint typeConstraint => _typeConstraint;

	public bool IsConnected
	{
		get
		{
			//IL_0063: Expected I4, but got O
			List<PortConnection> list = connections;
			if (connections != null)
			{
				bool flag = list._size < 0;
				bool flag2 = list._size == 0;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public bool IsInput => _direction == IO.Input;

	public bool IsOutput
	{
		get
		{
			//IL_0010: Expected O, but got I4
			object obj = _direction - 1;
			return obj == null;
		}
	}

	public string fieldName => _fieldName;

	public Node node => _node;

	public bool IsDynamic => _dynamic;

	public bool IsStatic => !_dynamic;

	public Type ValueType
	{
		get
		{
			if (((object)valueType).Equals((object)null) && !string.IsNullOrEmpty(_typeQualifiedName))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A72D0");
				Type type = default(Type);
				valueType = type;
			}
			return valueType;
		}
		set
		{
			valueType = value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
			object obj = default(object);
			if (obj != null)
			{
				string assemblyQualifiedName = value.AssemblyQualifiedName;
				_typeQualifiedName = assemblyQualifiedName;
			}
		}
	}

	public T GetConnectedNode<T>() where T : Node
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		NodePort connection = Connection;
		if (connection != null)
		{
			NodePort connection2 = Connection;
			if (connection2 != null)
			{
				if (!(connection2._node != null))
				{
					goto IL_0120;
				}
				NodePort connection3 = Connection;
				if (connection3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					T val = default(T);
					if ((object)val == null)
					{
						goto IL_0120;
					}
					NodePort connection4 = Connection;
					if (connection4 != null)
					{
						return val;
					}
				}
			}
			return (T)(object)new NullReferenceException();
		}
		goto IL_0120;
		IL_0120:
		return null;
	}

	public unsafe T GetConnectedNode<T>(out string field) where T : Node
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		ref string reference = ref *(string*)null;
		NodePort connection = Connection;
		if (connection != null)
		{
			NodePort connection2 = Connection;
			if (connection2 != null)
			{
				if (!(connection2._node != null))
				{
					goto IL_013b;
				}
				NodePort connection3 = Connection;
				if (connection3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					T val = default(T);
					if ((object)val == null)
					{
						goto IL_013b;
					}
					NodePort connection4 = Connection;
					if (connection4 != null)
					{
						reference = ref *(string*)connection4._fieldName;
						return val;
					}
				}
			}
			return (T)(object)new NullReferenceException();
		}
		goto IL_013b;
		IL_013b:
		return null;
	}

	public unsafe List<T> GetConnectedNodes<T>() where T : Node
	{
		//IL_0091: Expected O, but got Ref
		//IL_00b6: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		List<T> list = new List<T>();
		if (connections != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<PortConnection>.Enumerator enumerator = default(List<PortConnection>.Enumerator);
			object obj = default(object);
			T val = default(T);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				object obj2 = (object)(&enumerator);
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ stack_8_v3+18]");
					if ((UnityEngine.Object)0 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						if ((object)val != null)
						{
							list.Add(val);
						}
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
		}
		return list;
	}

	public unsafe NodePort(FieldInfo fieldInfo)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00d6: Expected I, but got O
		//IL_00e4: Expected I, but got O
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Expected O, but got Unknown
		//IL_013f: Expected O, but got I
		//IL_0319: Expected I, but got O
		//IL_0329: Expected O, but got I
		//IL_017b: Expected O, but got I
		//IL_01a4: Expected O, but got I
		//IL_0365: Expected O, but got I
		//IL_038e: Expected O, but got I
		//IL_01ca: Expected I4, but got O
		//IL_03b4: Expected I4, but got O
		//IL_01dd: Expected I, but got O
		//IL_01fa: Expected O, but got I
		//IL_03c7: Expected I, but got O
		//IL_03e4: Expected O, but got I
		//IL_0236: Expected O, but got I
		//IL_0420: Expected O, but got I
		//IL_0280: Expected O, but got I
		//IL_0290: Expected O, but got I
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_046a: Expected O, but got I
		//IL_047a: Expected O, but got I
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Expected O, but got Unknown
		//IL_0304: Expected I, but got O
		//IL_04ee: Expected I, but got O
		//IL_0602: Expected I, but got O
		//IL_0612: Expected O, but got I
		//IL_0503: Expected O, but got I
		//IL_053a: Expected O, but got I
		//IL_054a: Expected O, but got I
		//IL_055f: Expected O, but got I
		//IL_0675: Expected I, but got O
		//IL_0683: Expected I, but got O
		List<PortConnection> list = new List<PortConnection>();
		connections = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		string name = fieldInfo.Name;
		_fieldName = name;
		Type type = (valueType = fieldInfo.FieldType);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		if (obj != null)
		{
			string assemblyQualifiedName = type.AssemblyQualifiedName;
			_typeQualifiedName = assemblyQualifiedName;
		}
		_dynamic = false;
		object[] customAttributes = fieldInfo.GetCustomAttributes(inherit: false);
		object obj2 = customAttributes + 32;
		IO iO = IO.Input;
		nint num = (nint)typeof(Node.InputAttribute);
		nint num2 = (nint)typeof(Node.OutputAttribute);
		IO iO2 = IO.Input;
		FieldInfo fieldInfo2 = fieldInfo;
		FieldInfo fieldInfo3 = fieldInfo;
		while (true)
		{
			if ((int)iO2 >= customAttributes.Length)
			{
				return;
			}
			object obj3 = obj2;
			if (obj2 == null)
			{
				goto IL_05ba;
			}
			object obj4 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r8_v8 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
			FieldInfo fieldInfo4 = (FieldInfo)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r11_v5+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r8_v8 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
			nint num6;
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ r11_v5+C8]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rax_v46+FFFFFFF8+v488 @ rax_v26 (System.Reflection.FieldInfo)*8]");
				bool flag = 0 != num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ r8_v8 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
				fieldInfo2 = (FieldInfo)0;
				if (!flag)
				{
					_direction = IO.Input;
					IO iO3 = (IO)obj2;
					nint num4 = (nint)typeof(Node.InputAttribute);
					int value__ = ((IO*)(int)iO3)->value__;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ r11_v10 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rdx_v24 (System.Int32)+130]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ r11_v10 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
					if (num5 < 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rdx_v24 (System.Int32)+C8]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v48+FFFFFFF8+v114 @ rax_v47*8]");
					if (0 != (nint)typeof(Node.InputAttribute))
					{
						break;
					}
					int value__2 = ((IO*)(int)iO3)->value__;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ r11_v10 (Il2CppClass<SleepyNodes.Node+InputAttribute>)+130]");
					object obj8 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ rax_v49 (System.Int32)+C8]");
					object obj9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v129 @ rcx_v27+FFFFFFF8+v104 @ rdx_v25*8]");
					object obj10 = 0 - typeof(Node.InputAttribute);
					bool flag2 = obj10 == null;
					bool flag3 = !flag2;
					IO iO4 = IO.Input;
					if (!flag3)
					{
						iO4 = iO3;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v591 @ rax_v51 (SleepyNodes.NodePort+IO)+14]");
					_connectionType = Node.ConnectionType.Multiple;
					fieldInfo3 = (FieldInfo)obj2;
					num6 = (nint)typeof(Node.InputAttribute);
					goto IL_05fa;
				}
			}
			object obj11 = obj2;
			num6 = (nint)obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ rdx_v17 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			FieldInfo fieldInfo5 = (FieldInfo)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r11_v4 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ rdx_v17 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			if (num7 < 0)
			{
				goto IL_05ba;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r11_v4 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+C8]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ rax_v38+FFFFFFF8+v505 @ rax_v37 (System.Reflection.FieldInfo)*8]");
			bool flag4 = 0 != num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v398 @ rdx_v17 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			fieldInfo2 = (FieldInfo)0;
			if (flag4)
			{
				goto IL_05ba;
			}
			_direction = IO.Output;
			IO iO5 = (IO)obj2;
			nint num8 = (nint)typeof(Node.OutputAttribute);
			int value__3 = ((IO*)(int)iO5)->value__;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ r11_v8 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdx_v22 (System.Int32)+130]");
			nint num9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ r11_v8 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			if (num9 < 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rdx_v22 (System.Int32)+C8]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v40+FFFFFFF8+v118 @ rax_v39*8]");
			if (0 != (nint)typeof(Node.OutputAttribute))
			{
				break;
			}
			int value__4 = ((IO*)(int)iO5)->value__;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ r11_v8 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v592 @ rax_v41 (System.Int32)+C8]");
			object obj16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rcx_v24+FFFFFFF8+v106 @ rdx_v23*8]");
			object obj17 = 0 - typeof(Node.OutputAttribute);
			bool flag5 = obj17 == null;
			bool flag6 = !flag5;
			IO iO6 = IO.Input;
			if (!flag6)
			{
				iO6 = iO5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v616 @ rax_v43 (SleepyNodes.NodePort+IO)+14]");
			_connectionType = Node.ConnectionType.Multiple;
			fieldInfo3 = (FieldInfo)obj2;
			num6 = (nint)typeof(Node.OutputAttribute);
			goto IL_05fa;
			IL_05ba:
			iO++;
			obj2 += 8;
			iO2 = iO;
			continue;
			IL_05fa:
			nint num10 = (nint)fieldInfo3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r11_v4 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdx_v19 (Il2CppClass<System.Reflection.FieldInfo>)+130]");
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r11_v4 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			if (num11 < 0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdx_v19 (Il2CppClass<System.Reflection.FieldInfo>)+C8]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v29+FFFFFFF8+v122 @ rax_v28*8]");
			if (0 != num6)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ r11_v4 (Il2CppClass<SleepyNodes.Node+OutputAttribute>)+130]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rdx_v19 (Il2CppClass<System.Reflection.FieldInfo>)+C8]");
			fieldInfo2 = (FieldInfo)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v507 @ rcx_v21 (System.Reflection.FieldInfo)+FFFFFFF8+v635 @ rdx_v20*8]");
			object obj21 = -num6;
			bool flag7 = obj21 == null;
			bool flag8 = !flag7;
			FieldInfo fieldInfo6 = null;
			if (!flag8)
			{
				fieldInfo6 = fieldInfo3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ rax_v32 (System.Reflection.FieldInfo)+1C]");
			_typeConstraint = Node.TypeConstraint.None;
			num = (nint)typeof(Node.InputAttribute);
			num2 = (nint)typeof(Node.OutputAttribute);
			goto IL_05ba;
		}
		throw new NullReferenceException();
	}

	public NodePort(NodePort nodePort, Node node)
	{
		List<PortConnection> list = new List<PortConnection>();
		connections = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		_fieldName = nodePort._fieldName;
		valueType = nodePort.valueType;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		if (obj != null)
		{
			string assemblyQualifiedName = nodePort.valueType.AssemblyQualifiedName;
			_typeQualifiedName = assemblyQualifiedName;
		}
		_direction = nodePort._direction;
		_dynamic = nodePort._dynamic;
		_connectionType = nodePort._connectionType;
		_typeConstraint = nodePort._typeConstraint;
		_node = node;
	}

	public NodePort(string fieldName, Type type, IO direction, Node.ConnectionType connectionType, Node.TypeConstraint typeConstraint, Node node)
	{
		List<PortConnection> list = new List<PortConnection>();
		connections = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		_fieldName = fieldName;
		valueType = type;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		if (obj != null)
		{
			string assemblyQualifiedName = type.AssemblyQualifiedName;
			_typeQualifiedName = assemblyQualifiedName;
		}
		Node node2 = default(Node);
		_node = node2;
		_direction = direction;
		Node.ConnectionType connectionType2 = default(Node.ConnectionType);
		_connectionType = connectionType2;
		Node.TypeConstraint typeConstraint2 = default(Node.TypeConstraint);
		_typeConstraint = typeConstraint2;
		_dynamic = true;
	}

	public void VerifyConnections()
	{
		//IL_004c: Expected O, but got I
		//IL_014c: Expected O, but got I4
		//IL_0089: Expected O, but got I
		//IL_00d6: Expected O, but got I
		//IL_00d6: Expected O, but got I
		List<PortConnection> list = connections;
		bool flag = (nint)connections < 0;
		int num = list._size - 1;
		if (flag)
		{
			return;
		}
		object obj;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ stack_8_v3+18]");
			bool flag2;
			if ((UnityEngine.Object)0 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ stack_18+10]");
				if (!string.IsNullOrEmpty((string)0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ stack_20+18]");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ stack_-28+10]");
					NodePort port = ((Node)num2).GetPort((string)0);
					flag2 = (nint)port < 0;
					if (port != null)
					{
						goto IL_0133;
					}
				}
			}
			flag2 = (nint)connections < 0;
			connections.RemoveAt(num);
			goto IL_0133;
			IL_0133:
			num--;
			obj = !flag2;
		}
		while (obj != null);
	}

	public object GetOutputValue()
	{
		//IL_0050: Expected I, but got O
		if (_direction != IO.Input)
		{
			Node node = _node;
			if ((object)_node == null)
			{
				return new NullReferenceException();
			}
			nint num = (nint)node;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v23 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+188] (should have been resolved before IL gen)");
		}
		return null;
	}

	public object GetInputValue()
	{
		//IL_0080: Expected I, but got O
		NodePort connection = Connection;
		if (connection != null && connection._direction != IO.Input)
		{
			Node node = connection._node;
			if ((object)connection._node == null)
			{
				return new NullReferenceException();
			}
			nint num = (nint)node;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v63 @ r8_v1 (Il2CppClass<SleepyNodes.Node>)+188] (should have been resolved before IL gen)");
		}
		return null;
	}

	public object[] GetInputValues()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0049: Expected I, but got O
		//IL_0310: Expected O, but got I
		//IL_02d0: Expected O, but got I4
		//IL_0094: Expected O, but got I4
		//IL_0248: Expected O, but got I4
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected I4, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0285: Expected I, but got O
		//IL_0131: Expected I, but got O
		//IL_01af: Expected O, but got I4
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01d2: Expected I, but got O
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01f7: Expected I4, but got O
		NodePort nodePort = this;
		List<PortConnection> list = connections;
		if (connections != null)
		{
			int num = list._size;
			object[] array = new object[list._size];
			object obj = array + 32;
			int num2 = 0;
			nint num3 = (nint)typeof(object[]);
			PortConnection portConnection = default(PortConnection);
			while (true)
			{
				nodePort = (NodePort)num3;
				List<PortConnection> list2 = connections;
				bool flag = connections == null;
				list = (List<PortConnection>)num;
				if (flag)
				{
					break;
				}
				if (num2 < list2._size)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag2 = portConnection == null;
					list = (List<PortConnection>)num2;
					nodePort = (NodePort)(object)portConnection;
					if (flag2)
					{
						break;
					}
					NodePort port = portConnection.Port;
					if (port != null)
					{
						object outputValue = port.GetOutputValue();
						bool flag3 = array == null;
						list = null;
						nodePort = port;
						if (flag3)
						{
							break;
						}
						if (outputValue != null)
						{
							nint num4 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rdx_v14 (Il2CppClass<System.Object[]>)+40]");
							int index = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v330 @ rdx_v14 (Il2CppClass<System.Object[]>)+40]");
							PortConnection portConnection2 = ((List<PortConnection>)outputValue).get_Item(0);
							bool flag4 = portConnection2 == null;
							nodePort = (NodePort)outputValue;
							if (flag4)
							{
								PortConnection portConnection3 = ((List<PortConnection>)(object)nodePort).get_Item(index);
								throw portConnection3;
							}
						}
						if (num2 >= array.Length)
						{
							return (object[])(object)new IndexOutOfRangeException();
						}
						object obj2 = num2 + 4;
						obj = outputValue;
						object obj3 = obj2 * 8;
						num3 = (nint)((object)array + obj3);
						num2++;
						obj += 8;
						num = (int)outputValue;
					}
					else
					{
						bool flag5 = connections == null;
						list = null;
						nodePort = (NodePort)(object)connections;
						if (flag5)
						{
							break;
						}
						connections.RemoveAt(num2);
						object obj4 = num2 - 1;
						object obj5 = obj - 8;
						num2 = obj4 + 1;
						obj = obj5 + 8;
						num = num2;
						num3 = (nint)connections;
					}
					continue;
				}
				return array;
			}
		}
		throw new NullReferenceException();
	}

	public unsafe T GetInputValue<T>()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0063: Expected O, but got I
		//IL_0081: Expected O, but got I
		//IL_014e: Expected O, but got I
		//IL_00b0: Expected O, but got I
		//IL_0129: Expected O, but got Ref
		//IL_00ec: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+38]");
		object obj3 = 0;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v1+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v1+FC]");
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v1+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r9_v1+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			object inputValue = GetInputValue();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj8 = default(object);
		if (obj8 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+38]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
			object obj11 = default(object);
			object obj10 = obj11;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			object obj10 = (object)(&obj2);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe T[] GetInputValues<T>()
	{
		//IL_0008: Expected O, but got Ref
		//IL_002e: Expected O, but got I
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007a: Expected O, but got Ref
		//IL_0089: Expected O, but got I4
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_0139: Expected I, but got O
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_019c: Expected I, but got O
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01eb: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v1 (Il2CppClass<T>)+FC]");
		object[] inputValues = default(object[]);
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			inputValues = GetInputValues();
		}
		T[] array = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ r8_v1 (Il2CppClass<T>)+FC]");
		_ = 0;
		object obj4 = inputValues + 32;
		object obj5 = (object)(&obj2);
		nint num2 = 0;
		object obj6 = 0;
		object obj7 = default(object);
		IntPtr intPtr = default(IntPtr);
		while (true)
		{
			if ((nint)obj6 < inputValues.Length)
			{
				if ((nint)obj6 >= inputValues.Length)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj7 != null)
				{
					if ((nint)obj6 >= inputValues.Length)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
					if ((nint)obj6 >= array.Length)
					{
						break;
					}
					nint num3 = (nint)array;
					object obj8 = obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ rcx_v14 (Il2CppClass<T[]>)+104]");
					object obj9 = obj8 * 0;
					object obj10 = obj9 + 32;
					object obj11 = obj10 + (object)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					if ((nint)obj6 >= array.Length)
					{
						break;
					}
					nint num4 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ rax_v28 (Il2CppClass<T[]>)+104]");
					object obj12 = 0 * obj6;
					object obj13 = obj12 + 32;
					object obj14 = obj13 + (object)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+50]");
					obj5 = 0;
					num2 = intPtr;
				}
				obj6++;
				obj4 += 8;
				continue;
			}
			return array;
		}
		return (T[])(object)new IndexOutOfRangeException();
	}

	public unsafe bool TryGetInputValue<T>(out T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0038: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			object inputValue = GetInputValue();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj4 = default(object);
		if (obj4 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			return false;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		return true;
	}

	public float GetInputSum(float fallback)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0055: Expected O, but got I4
		//IL_005e: Expected O, but got I4
		//IL_0067: Expected F4, but got I4
		//IL_006c: Expected I, but got O
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_00cf: Expected I, but got O
		object[] inputValues = GetInputValues();
		if (inputValues.Length != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
			nint num = 0;
			object obj = inputValues + 32;
			object obj2 = 0;
			object obj3 = 0;
			float num2 = 0f;
			nint num3 = unchecked((nint)null);
			object obj4 = default(object);
			while (true)
			{
				if ((nint)obj3 < inputValues.Length)
				{
					NodePort nodePort = (NodePort)obj;
					if (obj != null)
					{
						bool flag = (nint)nodePort != num;
						NodePort nodePort2 = null;
						if (!flag)
						{
							nodePort2 = (NodePort)obj;
						}
						if (nodePort2 != null)
						{
							num3 = (nint)nodePort;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rdx_v6 (Il2CppClass<SleepyNodes.NodePort>)+40]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r8_v5 (Il2CppMethodInfo)+40]");
							if (num4 != 0)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
							num = 0;
							num2 += (float)obj4;
						}
					}
					obj2++;
					obj += 8;
					obj3 = obj2;
					continue;
				}
				return num2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			throw new IndexOutOfRangeException();
		}
		return fallback;
	}

	public int GetInputSum(int fallback)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_005e: Expected O, but got I4
		//IL_0063: Expected I, but got O
		//IL_006c: Expected O, but got I4
		//IL_0163: Expected I4, but got O
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_00f0: Expected I, but got O
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected I4, but got Unknown
		object[] inputValues = GetInputValues();
		if (inputValues.Length != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
			nint num = 0;
			object obj = inputValues + 32;
			int num2 = 0;
			object obj2 = 0;
			nint num3 = unchecked((nint)null);
			object obj3 = 0;
			object obj4 = default(object);
			while (true)
			{
				if ((nint)obj2 < inputValues.Length)
				{
					if ((nint)obj3 >= inputValues.Length)
					{
						break;
					}
					NodePort nodePort = (NodePort)obj;
					if (obj != null)
					{
						bool flag = (nint)nodePort != num;
						NodePort nodePort2 = null;
						if (!flag)
						{
							nodePort2 = (NodePort)obj;
						}
						if (nodePort2 != null)
						{
							num3 = (nint)nodePort;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ rdx_v6 (Il2CppClass<SleepyNodes.NodePort>)+40]");
							nint num4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r8_v5 (Il2CppMethodInfo)+40]");
							if (num4 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
							num = 0;
							num2 += obj4;
						}
					}
					obj3++;
					obj += 8;
					obj2 = obj3;
					continue;
				}
				return num2;
			}
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			return (int)ex;
		}
		return fallback;
	}

	public void Connect(NodePort port)
	{
		//IL_021e: Expected I, but got O
		//IL_022e: Expected O, but got I
		//IL_023e: Expected O, but got I
		if (connections == null)
		{
			List<PortConnection> list = new List<PortConnection>();
			connections = list;
		}
		if (port != null)
		{
			if (port != this)
			{
				if (!IsConnectedTo(port))
				{
					if (_direction != port._direction)
					{
						if (port._connectionType == Node.ConnectionType.Override && port.ConnectionCount != 0)
						{
							port.ClearConnections();
						}
						if (_connectionType == Node.ConnectionType.Override && ConnectionCount != 0)
						{
							ClearConnections();
						}
						PortConnection item = new PortConnection(port);
						connections.Add(item);
						if (port.connections == null)
						{
							List<PortConnection> list2 = new List<PortConnection>();
							port.connections = list2;
						}
						if (!port.IsConnectedTo(this))
						{
							PortConnection item2 = new PortConnection(this);
							port.connections.Add(item2);
						}
						_node.OnCreateConnection(this, port);
						Node node = port._node;
						nint num = (nint)node;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v478 @ r9_v5 (Il2CppClass<SleepyNodes.Node>)+198]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v478 @ r9_v5 (Il2CppClass<SleepyNodes.Node>)+1A0]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v189 @ rax_v30 (should have been resolved before IL gen)");
					}
					bool flag = _direction == IO.Input;
					string text = "input";
					if (!flag)
					{
						text = "output";
					}
					string message = "Cannot connect two " + text + " connections";
					Debug.LogWarning(message);
				}
				else
				{
					Debug.LogWarning("Port already connected. ");
				}
			}
			else
			{
				Debug.LogWarning("Cannot connect port to self.");
			}
		}
		else
		{
			Debug.LogWarning("Cannot connect to null port");
		}
	}

	public List<NodePort> GetConnections()
	{
		//IL_0048: Expected O, but got I
		//IL_00b1: Expected O, but got I
		//IL_0191: Expected O, but got I
		//IL_0191: Expected O, but got I
		List<NodePort> list = new List<NodePort>();
		List<PortConnection> list2 = connections;
		bool flag = connections == null;
		int num = 0;
		int num2 = 0;
		if (!flag)
		{
			object obj = default(object);
			object obj2 = default(object);
			object obj3 = default(object);
			object obj4 = default(object);
			while (true)
			{
				if (num2 >= list2._size)
				{
					return list;
				}
				if (connections == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_8_v2+18]");
				if ((UnityEngine.Object)0 != null)
				{
					if (connections == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj2 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ stack_18+10]");
					if (!string.IsNullOrEmpty((string)0))
					{
						if (connections == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj3 == null || connections == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj4 == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ stack_20+18]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ stack_20+18]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ stack_-28+10]");
						NodePort port = ((Node)num3).GetPort((string)0);
						if (port != null)
						{
							if (list == null)
							{
								break;
							}
							list.Add(port);
							goto IL_0214;
						}
					}
				}
				if (connections == null)
				{
					break;
				}
				connections.RemoveAt(num);
				goto IL_0214;
				IL_0214:
				list2 = connections;
				num++;
				if (connections == null)
				{
					break;
				}
				num2 = num;
			}
		}
		return (List<NodePort>)(object)new NullReferenceException();
	}

	public NodePort GetConnection(int i)
	{
		//IL_0043: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_018c: Expected O, but got I
		//IL_018c: Expected O, but got I
		NodePort nodePort;
		if (connections != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+18]");
				if (!((UnityEngine.Object)0 != null))
				{
					goto IL_01b0;
				}
				if (connections != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+10]");
						if (string.IsNullOrEmpty((string)0))
						{
							goto IL_01b0;
						}
						if (connections != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj != null && connections != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (obj != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+18]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+18]");
										nint num = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ stack_8_v2+10]");
										nodePort = ((Node)num).GetPort((string)0);
										if (nodePort == null)
										{
											goto IL_01b0;
										}
										goto IL_0215;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_01e8;
		IL_01e8:
		return (NodePort)(object)new NullReferenceException();
		IL_0215:
		return nodePort;
		IL_01b0:
		if (connections == null)
		{
			goto IL_01e8;
		}
		connections.RemoveAt(i);
		nodePort = null;
		goto IL_0215;
	}

	public int GetConnectionIndex(NodePort port)
	{
		//IL_00a9: Expected I4, but got O
		//IL_009b: Expected I4, but got I8
		int num = 0;
		PortConnection portConnection = default(PortConnection);
		while (true)
		{
			List<PortConnection> list = connections;
			if (connections == null)
			{
				break;
			}
			if (num < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (portConnection == null)
				{
					break;
				}
				NodePort port2 = portConnection.Port;
				if (port2 != port)
				{
					num++;
					continue;
				}
				return num;
			}
			return -1;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public bool IsConnectedTo(NodePort port)
	{
		//IL_00f6: Expected O, but got I4
		//IL_00ff: Expected O, but got I4
		//IL_00d2: Expected I4, but got O
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		List<PortConnection> list = connections;
		bool flag = connections == null;
		object obj = 0;
		object obj2 = 0;
		if (!flag)
		{
			PortConnection portConnection = default(PortConnection);
			while (true)
			{
				if ((nint)obj2 < list._size)
				{
					if (connections == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (portConnection == null)
					{
						break;
					}
					NodePort port2 = portConnection.Port;
					if (port2 != port)
					{
						list = connections;
						obj++;
						if (connections == null)
						{
							break;
						}
						obj2 = obj;
						continue;
					}
					return true;
				}
				return false;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public bool CanConnectTo(NodePort port)
	{
		//IL_02c7: Expected I4, but got O
		if (port != null)
		{
			NodePort nodePort2;
			NodePort nodePort3;
			if (port._direction != IO.Input)
			{
				bool flag = _direction != IO.Input;
				NodePort nodePort = null;
				if (!flag)
				{
					nodePort = this;
				}
				bool flag2 = nodePort != null;
				nodePort2 = nodePort;
				nodePort3 = port;
				if (!flag2)
				{
					goto IL_0070;
				}
			}
			else
			{
				bool flag3 = _direction != IO.Input;
				nodePort2 = port;
				nodePort3 = this;
				if (!flag3)
				{
					nodePort2 = port;
					nodePort3 = null;
				}
			}
			if (nodePort3 != null)
			{
				if (nodePort2._typeConstraint == Node.TypeConstraint.Inherited)
				{
					Type type = nodePort2.ValueType;
					Type c = nodePort3.ValueType;
					if ((object)type == null)
					{
						goto IL_02b9;
					}
					if (!type.IsAssignableFrom(c))
					{
						goto IL_0070;
					}
				}
				if (nodePort2._typeConstraint == Node.TypeConstraint.Strict)
				{
					Type type2 = nodePort2.ValueType;
					Type type3 = nodePort3.ValueType;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					object obj = default(object);
					if (obj != null)
					{
						goto IL_0070;
					}
				}
				if (nodePort3._typeConstraint == Node.TypeConstraint.Inherited)
				{
					Type type4 = nodePort3.ValueType;
					Type c2 = nodePort2.ValueType;
					if ((object)type4 == null)
					{
						goto IL_02b9;
					}
					if (!type4.IsAssignableFrom(c2))
					{
						goto IL_0070;
					}
				}
				if (nodePort3._typeConstraint == Node.TypeConstraint.Strict)
				{
					Type type5 = nodePort3.ValueType;
					Type type6 = nodePort2.ValueType;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					object obj2 = default(object);
					if (obj2 != null)
					{
						goto IL_0070;
					}
				}
				return true;
			}
			goto IL_0070;
		}
		goto IL_02b9;
		IL_02b9:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0070:
		return false;
	}

	public void Disconnect(NodePort port)
	{
		//IL_00c0: Expected O, but got I4
		List<PortConnection> list = connections;
		bool flag = (nint)connections < 0;
		int num = list._size - 1;
		PortConnection portConnection = default(PortConnection);
		if (!flag)
		{
			object obj2;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port2 = portConnection.Port;
				object obj = (object)port2 - (object)port;
				bool flag2 = (nint)obj < 0;
				if (port2 == port)
				{
					flag2 = (nint)connections < 0;
					connections.RemoveAt(num);
				}
				num--;
				obj2 = !flag2;
			}
			while (obj2 != null);
		}
		if (port != null)
		{
			int num2 = 0;
			while (true)
			{
				List<PortConnection> list2 = port.connections;
				if (num2 >= list2._size)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port3 = portConnection.Port;
				if (port3 == this)
				{
					port.connections.RemoveAt(num2);
				}
				num2++;
			}
		}
		_node.OnRemoveConnection(this);
		port?._node.OnRemoveConnection(port);
	}

	public void Disconnect(int i)
	{
		//IL_0047: Expected O, but got I4
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		PortConnection portConnection = default(PortConnection);
		NodePort port = portConnection.Port;
		if (port != null)
		{
			object obj = 0;
			while (true)
			{
				List<PortConnection> list = port.connections;
				if ((nint)obj >= list._size)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port2 = portConnection.Port;
				if (port2 == this)
				{
					port.connections.RemoveAt(i);
				}
				obj++;
			}
		}
		connections.RemoveAt(i);
		_node.OnRemoveConnection(this);
		port?._node.OnRemoveConnection(port);
	}

	public void ClearConnections()
	{
		List<PortConnection> list = connections;
		PortConnection portConnection = default(PortConnection);
		while (list._size > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			NodePort port = portConnection.Port;
			Disconnect(port);
			list = connections;
		}
	}

	public List<Vector2> GetReroutePoints(int index)
	{
		//IL_0039: Expected O, but got I
		if (connections != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ stack_8_v2+28]");
				return (List<Vector2>)0;
			}
		}
		return (List<Vector2>)(object)new NullReferenceException();
	}

	public void SwapConnections(NodePort targetPort)
	{
		//IL_0055: Expected O, but got I4
		//IL_00e0: Expected O, but got I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_0202: Expected O, but got I4
		//IL_020b: Expected O, but got I4
		//IL_0274: Expected O, but got I4
		//IL_027d: Expected O, but got I4
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		List<PortConnection> list = connections;
		List<PortConnection> list2 = targetPort.connections;
		List<NodePort> list3 = new List<NodePort>();
		List<NodePort> list4 = new List<NodePort>();
		bool flag = list._size <= 0;
		object obj = 0;
		PortConnection portConnection = default(PortConnection);
		if (!flag)
		{
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port = portConnection.Port;
				list3.Add(port);
				obj++;
			}
			while ((nint)obj < list._size);
		}
		bool flag2 = list2._size <= 0;
		object obj2 = 0;
		if (!flag2)
		{
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port2 = portConnection.Port;
				list4.Add(port2);
				obj2++;
			}
			while ((nint)obj2 < list2._size);
		}
		List<PortConnection> list5 = connections;
		while (list5._size > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			NodePort port3 = portConnection.Port;
			Disconnect(port3);
			list5 = connections;
		}
		List<PortConnection> list6 = targetPort.connections;
		NodePort port5 = default(NodePort);
		NodePort port6 = default(NodePort);
		do
		{
			if (list6._size > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port4 = portConnection.Port;
				targetPort.Disconnect(port4);
				list6 = targetPort.connections;
				continue;
			}
			object obj3 = 0;
			object obj4 = 0;
			while ((nint)obj4 < list3._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				targetPort.Connect(port5);
				obj3++;
				obj4 = obj3;
			}
			object obj5 = 0;
			object obj6 = 0;
			while ((nint)obj6 < list4._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Connect(port6);
				obj5++;
				obj6 = obj5;
			}
			return;
		}
		while (targetPort.connections != null);
		throw new NullReferenceException();
	}

	public void AddConnections(NodePort targetPort)
	{
		//IL_002a: Expected O, but got I4
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		List<PortConnection> list = targetPort.connections;
		bool flag = list._size <= 0;
		object obj = 0;
		if (!flag)
		{
			PortConnection portConnection = default(PortConnection);
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port = portConnection.Port;
				Connect(port);
				obj++;
			}
			while ((nint)obj < list._size);
		}
	}

	public void MoveConnections(NodePort targetPort)
	{
		//IL_0033: Expected O, but got I4
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		List<PortConnection> list = connections;
		PortConnection portConnection = default(PortConnection);
		if (list._size > 0)
		{
			object obj = 0;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				NodePort port = portConnection.Port;
				Connect(port);
				obj++;
			}
			while ((nint)obj < list._size);
		}
		List<PortConnection> list2 = connections;
		while (list2._size > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			NodePort port2 = portConnection.Port;
			Disconnect(port2);
			list2 = connections;
		}
	}

	public unsafe void Redirect(List<Node> oldNodes, List<Node> newNodes)
	{
		//IL_0036: Expected O, but got Ref
		//IL_005b: Expected O, but got Ref
		//IL_007e: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<PortConnection>.Enumerator enumerator = default(List<PortConnection>.Enumerator);
		object obj = default(object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = obj == null;
				List<Node> list = (List<Node>)(&enumerator);
				if (!flag)
				{
					bool flag2 = oldNodes == null;
					list = (List<Node>)(&enumerator);
					if (flag2)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ stack_8_v3+18]");
					int num = oldNodes.IndexOf((Node)0);
					if (num >= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}
}
