using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

[Serializable]
public abstract class Node : ScriptableObject
{
	public enum ShowBackingValue
	{
		Never,
		Unconnected,
		Always
	}

	public enum ConnectionType
	{
		Multiple,
		Override
	}

	public enum TypeConstraint
	{
		None,
		Inherited,
		Strict
	}

	public class InputAttribute : Attribute
	{
		public ShowBackingValue backingValue;

		public ConnectionType connectionType;

		public bool dynamicPortList;

		public TypeConstraint typeConstraint;

		public bool ForceDrawSingle;

		public bool instancePortList
		{
			get
			{
				return dynamicPortList;
			}
			set
			{
				dynamicPortList = value;
			}
		}

		public InputAttribute(ShowBackingValue backingValue = ShowBackingValue.Unconnected, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false)
		{
			this.backingValue = backingValue;
			this.typeConstraint = typeConstraint;
			bool flag = default(bool);
			this.dynamicPortList = flag;
			this.connectionType = connectionType;
		}
	}

	public class OutputAttribute : Attribute
	{
		public ShowBackingValue backingValue;

		public ConnectionType connectionType;

		public bool dynamicPortList;

		public TypeConstraint typeConstraint;

		public bool ForceDrawSingle;

		public OutputAttribute(ShowBackingValue backingValue = ShowBackingValue.Never, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false)
		{
			this.backingValue = backingValue;
			this.typeConstraint = typeConstraint;
			bool flag = default(bool);
			this.dynamicPortList = flag;
			this.connectionType = connectionType;
		}

		public OutputAttribute(ShowBackingValue backingValue, ConnectionType connectionType, bool dynamicPortList)
		{
			this.backingValue = backingValue;
			this.dynamicPortList = dynamicPortList;
			this.connectionType = connectionType;
			typeConstraint = TypeConstraint.None;
		}
	}

	public class CreateNodeMenuAttribute : Attribute
	{
		public string menuName;

		public CreateNodeMenuAttribute(string menuName)
		{
			this.menuName = menuName;
		}
	}

	public class NodeTintAttribute : Attribute
	{
		public Color color;

		public NodeTintAttribute(float r, float g, float b)
		{
			Color color = default(Color);
			this.color = color;
		}

		public unsafe NodeTintAttribute(string hex)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected Ref, but got Unknown
			base._002Ector();
			bool flag = ColorUtility.TryParseHtmlString(hex, out *(Color*)(this + 16));
		}

		public NodeTintAttribute(byte r, byte g, byte b)
		{
			Color color = default(Color);
			this.color = color;
		}
	}

	public class NodeWidthAttribute : Attribute
	{
		public int width;

		public NodeWidthAttribute(int width)
		{
			this.width = width;
		}
	}

	public class NodeNameAttribute : Attribute
	{
		public string Name;

		public NodeNameAttribute(string name)
		{
			Name = name;
		}
	}

	[Serializable]
	private class NodePortDictionary : Dictionary<string, NodePort>, ISerializationCallbackReceiver
	{
		private List<string> keys;

		private List<NodePort> values;

		public unsafe void OnBeforeSerialize()
		{
			//IL_0176: Expected O, but got I4
			//IL_01ae: Expected O, but got Ref
			List<string> list = keys;
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
			List<NodePort> list2 = values;
			int version2 = list2._version + 1;
			list2._version = version2;
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<NodePort>())
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
			string item = default(string);
			NodePort item2 = default(NodePort);
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
			//IL_0072: Expected I, but got O
			//IL_007d: Expected I, but got O
			//IL_00cc: Expected O, but got Ref
			//IL_012e: Expected I, but got O
			//IL_0152: Expected I, but got O
			Clear();
			List<string> list = keys;
			bool flag = keys == null;
			nint num = 0;
			if (!flag)
			{
				List<NodePort> list2 = values;
				bool flag2 = values == null;
				num = 0;
				if (!flag2)
				{
					bool flag3 = list._size != list2._size;
					num = 0;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
						throw new NullReferenceException();
					}
					List<string> list3 = keys;
					nint num2 = unchecked((nint)null);
					num = 0;
					nint num3 = unchecked((nint)null);
					string text = default(string);
					NodePort nodePort2 = default(NodePort);
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
							NodePort nodePort = (NodePort)(&text);
							nint num4 = 0;
							if (flag4)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Add(text, nodePort2);
							list3 = keys;
							num2++;
							bool flag5 = keys == null;
							num = (nint)text;
							nodePort = nodePort2;
							num4 = 0;
							if (flag5)
							{
								break;
							}
							num = (nint)text;
							nodePort = nodePort2;
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

		public NodePortDictionary()
		{
			List<string> list = new List<string>();
			keys = list;
			values = new List<NodePort>();
			base._002Ector();
		}
	}

	[Serializable]
	public class FlowPointEntry
	{
	}

	[Serializable]
	public class FlowPoint : FlowPointEntry
	{
	}

	private sealed class _003Cget_DynamicInputs_003Ed__14 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private IEnumerator<NodePort> _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_DynamicInputs_003Ed__14(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ stack_8_v3+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}

		private bool MoveNext()
		{
			//IL_0285: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_00dd: Expected O, but got I
			//IL_0102: Expected I, but got O
			//IL_0137: Expected I, but got O
			//IL_016a: Expected I, but got O
			//IL_01a0: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				IEnumerable<NodePort> ports = ((Node)0).Ports;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IntPtr intPtr = default(IntPtr);
				num = intPtr;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			object obj3 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
					obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
					bool flag = (nint)0 == 0;
					num = (nint)typeof(IEnumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						bool flag2 = obj3 == null;
						num = (nint)typeof(IEnumerator<NodePort>);
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+44]");
							bool flag3 = (nint)0 == 0;
							num = (nint)typeof(IEnumerator<NodePort>);
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+38]");
								bool flag4 = (nint)0 != 0;
								num = (nint)typeof(IEnumerator<NodePort>);
								if (!flag4)
								{
									_ = 1;
									return true;
								}
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0031: Expected I4, but got I8
			bool flag = _003C_003E7__wrap1 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicInputs_003Ed__14 obj2 = new _003Cget_DynamicInputs_003Ed__14(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicInputs_003Ed__14 obj2 = new _003Cget_DynamicInputs_003Ed__14(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private sealed class _003Cget_DynamicOutputs_003Ed__12 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private IEnumerator<NodePort> _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_DynamicOutputs_003Ed__12(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ stack_8_v3+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}

		private bool MoveNext()
		{
			//IL_0285: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_00dd: Expected O, but got I
			//IL_0102: Expected I, but got O
			//IL_0137: Expected I, but got O
			//IL_016a: Expected I, but got O
			//IL_01a0: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				IEnumerable<NodePort> ports = ((Node)0).Ports;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IntPtr intPtr = default(IntPtr);
				num = intPtr;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			object obj3 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
					obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
					bool flag = (nint)0 == 0;
					num = (nint)typeof(IEnumerator);
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						bool flag2 = obj3 == null;
						num = (nint)typeof(IEnumerator<NodePort>);
						if (!flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+44]");
							bool flag3 = (nint)0 == 0;
							num = (nint)typeof(IEnumerator<NodePort>);
							if (!flag3)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+38]");
								bool flag4 = (nint)0 != 1;
								num = (nint)typeof(IEnumerator<NodePort>);
								if (!flag4)
								{
									_ = 1;
									return true;
								}
							}
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0031: Expected I4, but got I8
			bool flag = _003C_003E7__wrap1 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicOutputs_003Ed__12 obj2 = new _003Cget_DynamicOutputs_003Ed__12(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicOutputs_003Ed__12 obj2 = new _003Cget_DynamicOutputs_003Ed__12(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private sealed class _003Cget_DynamicPorts_003Ed__10 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private IEnumerator<NodePort> _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_DynamicPorts_003Ed__10(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ stack_8_v3+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}

		private bool MoveNext()
		{
			//IL_024f: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_00dd: Expected O, but got I
			//IL_0102: Expected I, but got O
			//IL_0137: Expected I, but got O
			//IL_016a: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				IEnumerable<NodePort> ports = ((Node)0).Ports;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IntPtr intPtr = default(IntPtr);
				num = intPtr;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			object obj3 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						bool flag = (nint)0 == 0;
						num = (nint)typeof(IEnumerator);
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							bool flag2 = obj3 == null;
							num = (nint)typeof(IEnumerator<NodePort>);
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+44]");
								bool flag3 = (nint)0 == 0;
								num = (nint)typeof(IEnumerator<NodePort>);
								if (!flag3)
								{
									_ = 1;
									return true;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					break;
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0031: Expected I4, but got I8
			bool flag = _003C_003E7__wrap1 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicPorts_003Ed__10 obj2 = new _003Cget_DynamicPorts_003Ed__10(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_DynamicPorts_003Ed__10 obj2 = new _003Cget_DynamicPorts_003Ed__10(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private sealed class _003Cget_Inputs_003Ed__8 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private IEnumerator<NodePort> _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_Inputs_003Ed__8(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ stack_8_v3+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}

		private bool MoveNext()
		{
			//IL_0252: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_00dd: Expected O, but got I
			//IL_0102: Expected I, but got O
			//IL_0137: Expected I, but got O
			//IL_016d: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				IEnumerable<NodePort> ports = ((Node)0).Ports;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IntPtr intPtr = default(IntPtr);
				num = intPtr;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			object obj3 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						bool flag = (nint)0 == 0;
						num = (nint)typeof(IEnumerator);
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							bool flag2 = obj3 == null;
							num = (nint)typeof(IEnumerator<NodePort>);
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+38]");
								bool flag3 = (nint)0 != 0;
								num = (nint)typeof(IEnumerator<NodePort>);
								if (!flag3)
								{
									_ = 1;
									return true;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					break;
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0031: Expected I4, but got I8
			bool flag = _003C_003E7__wrap1 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Inputs_003Ed__8 obj2 = new _003Cget_Inputs_003Ed__8(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Inputs_003Ed__8 obj2 = new _003Cget_Inputs_003Ed__8(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private sealed class _003Cget_Outputs_003Ed__6 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private IEnumerator<NodePort> _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_Outputs_003Ed__6(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		void IDisposable.Dispose()
		{
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ stack_8_v3+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
			}
		}

		private bool MoveNext()
		{
			//IL_0252: Expected O, but got I
			//IL_0045: Expected O, but got I
			//IL_00dd: Expected O, but got I
			//IL_0102: Expected I, but got O
			//IL_0137: Expected I, but got O
			//IL_016d: Expected I, but got O
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				IEnumerable<NodePort> ports = ((Node)0).Ports;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				IntPtr intPtr = default(IntPtr);
				num = intPtr;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj2 = default(object);
			object obj3 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
						bool flag = (nint)0 == 0;
						num = (nint)typeof(IEnumerator);
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							bool flag2 = obj3 == null;
							num = (nint)typeof(IEnumerator<NodePort>);
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rax_v25+38]");
								bool flag3 = (nint)0 != 1;
								num = (nint)typeof(IEnumerator<NodePort>);
								if (!flag3)
								{
									_ = 1;
									return true;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					break;
				}
				throw new NullReferenceException();
			}
			_ = 4294967295L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+30]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
			//IL_0031: Expected I4, but got I8
			bool flag = _003C_003E7__wrap1 == null;
			_003C_003E1__state = -1;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Outputs_003Ed__6 obj2 = new _003Cget_Outputs_003Ed__6(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Outputs_003Ed__6 obj2 = new _003Cget_Outputs_003Ed__6(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	private sealed class _003Cget_Ports_003Ed__4 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private NodePort _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		public Node _003C_003E4__this;

		private Dictionary<string, NodePort>.ValueCollection.Enumerator _003C_003E7__wrap1;

		NodePort IEnumerator<NodePort>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003Cget_Ports_003Ed__4(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			_003C_003El__initialThreadId = num;
		}

		unsafe void IDisposable.Dispose()
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			if (_003C_003E1__state == -3 || _003C_003E1__state == 1)
			{
				_ = 4294967295L;
				object obj = default(object);
				Dictionary<string, NodePort>.ValueCollection.Enumerator enumerator = (Dictionary<string, NodePort>.ValueCollection.Enumerator)(obj + 48);
				((Dictionary<string, NodePort>.ValueCollection.Enumerator*)enumerator)->Dispose();
			}
		}

		private unsafe bool MoveNext()
		{
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Expected O, but got Unknown
			//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c2: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0105: Expected O, but got Unknown
			//IL_006a: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
			if ((nint)0 == 0)
			{
				_ = 4294967295L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rdx_v9 (Il2CppMethodInfo)+28]");
				if ((nint)0 == 0)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rdx_v9 (Il2CppMethodInfo)+28]");
				Dictionary<string, NodePort>.ValueCollection values = ((Dictionary<string, NodePort>)0).Values;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v32 @ stack_8_v2+10]");
				if ((nint)0 != 1)
				{
					return false;
				}
			}
			_ = 4294967293L;
			object obj = default(object);
			Dictionary<string, NodePort>.ValueCollection.Enumerator enumerator = (Dictionary<string, NodePort>.ValueCollection.Enumerator)(obj + 48);
			if (((Dictionary<string, NodePort>.ValueCollection.Enumerator*)enumerator)->MoveNext())
			{
				object obj2 = obj + 48;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				_ = 1;
				return true;
			}
			_ = 4294967295L;
			Dictionary<string, NodePort>.ValueCollection.Enumerator enumerator2 = (Dictionary<string, NodePort>.ValueCollection.Enumerator)(obj + 48);
			((Dictionary<string, NodePort>.ValueCollection.Enumerator*)enumerator2)->Dispose();
			_ = 0;
			_ = 0;
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private unsafe void _003C_003Em__Finally1()
		{
			//IL_0014: Expected I4, but got I8
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			_003C_003E1__state = -1;
			Dictionary<string, NodePort>.ValueCollection.Enumerator enumerator = (Dictionary<string, NodePort>.ValueCollection.Enumerator)(this + 48);
			((Dictionary<string, NodePort>.ValueCollection.Enumerator*)enumerator)->Dispose();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}

		IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Ports_003Ed__4 obj2 = new _003Cget_Ports_003Ed__4(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_003C_003E1__state == -2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
				object obj = default(object);
				if (_003C_003El__initialThreadId == (nint)obj)
				{
					_003C_003E1__state = 0;
					return this;
				}
			}
			_003Cget_Ports_003Ed__4 obj2 = new _003Cget_Ports_003Ed__4(0);
			obj2._003C_003E1__state = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj2._003C_003El__initialThreadId = num;
			obj2._003C_003E4__this = _003C_003E4__this;
			return obj2;
		}
	}

	public NodeGraph graph;

	public Vector2 position;

	private NodePortDictionary ports;

	public static NodeGraph graphHotfix;

	public IEnumerable<NodePort> Ports
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_Ports_003Ed__4 obj = new _003Cget_Ports_003Ed__4(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	public IEnumerable<NodePort> Outputs
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_Outputs_003Ed__6 obj = new _003Cget_Outputs_003Ed__6(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	public IEnumerable<NodePort> Inputs
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_Inputs_003Ed__8 obj = new _003Cget_Inputs_003Ed__8(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	public IEnumerable<NodePort> DynamicPorts
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_DynamicPorts_003Ed__10 obj = new _003Cget_DynamicPorts_003Ed__10(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	public IEnumerable<NodePort> DynamicOutputs
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_DynamicOutputs_003Ed__12 obj = new _003Cget_DynamicOutputs_003Ed__12(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	public IEnumerable<NodePort> DynamicInputs
	{
		get
		{
			//IL_0042: Expected I4, but got I8
			_003Cget_DynamicInputs_003Ed__14 obj = new _003Cget_DynamicInputs_003Ed__14(0);
			obj._003C_003E1__state = -2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
			int num = default(int);
			obj._003C_003El__initialThreadId = num;
			obj._003C_003E4__this = this;
			return obj;
		}
	}

	protected void OnEnable()
	{
		//IL_0059: Expected I, but got O
		//IL_0069: Expected O, but got I
		//IL_0079: Expected O, but got I
		while (true)
		{
			if (graphHotfix != null)
			{
				graph = graphHotfix;
			}
			graphHotfix = null;
			NodeDataCache.UpdatePorts(this, ports);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.Node>)+178]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rdx_v6 (Il2CppClass<SleepyNodes.Node>)+180]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v80 @ rax_v9 (should have been resolved before IL gen)");
		}
	}

	protected virtual void Init()
	{
	}

	public void UpdateStaticPorts()
	{
		NodeDataCache.UpdatePorts(this, ports);
	}

	public unsafe void VerifyConnections()
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_013b: Expected O, but got I
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		IEnumerable<NodePort> enumerable = Ports;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		NodePort nodePort = default(NodePort);
		object obj = (object)(&nodePort);
		NodePort nodePort2 = null;
		object obj2 = default(object);
		object obj11 = default(object);
		NodePort nodePort3 = default(NodePort);
		while (true)
		{
			object obj10;
			object obj3;
			if (nodePort != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = nodePort == null;
					nodePort2 = null;
					if (flag)
					{
						break;
					}
					nint num = (nint)nodePort;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r8_v9+v260 @ rcx_v18*8]");
							if (0 == (nint)typeof(IEnumerator<NodePort>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_00ea;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r8_v9+8+v314 @ rcx_v20*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0218;
					}
					goto IL_00ea;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_00ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			obj3 = 0;
			goto IL_0218;
			IL_0218:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v319 @ rdx_v11] (should have been resolved before IL gen)");
			nodePort3.VerifyConnections();
		}
		throw new NullReferenceException();
	}

	public NodePort AddDynamicInput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
	{
		TypeConstraint typeConstraint2 = default(TypeConstraint);
		string fieldName2 = default(string);
		return AddDynamicPort(type, NodePort.IO.Input, connectionType, typeConstraint2, fieldName2);
	}

	public NodePort AddDynamicOutput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
	{
		TypeConstraint typeConstraint2 = default(TypeConstraint);
		string fieldName2 = default(string);
		return AddDynamicPort(type, NodePort.IO.Output, connectionType, typeConstraint2, fieldName2);
	}

	private NodePort AddDynamicPort(Type type, NodePort.IO direction, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
	{
		string text = default(string);
		string text2;
		NodePort nodePort;
		if (text == null)
		{
			int num = 0;
			text2 = "dynamicInput_0";
			int num2 = 0;
			while (ports != null)
			{
				bool flag = ports.ContainsKey(text2);
				bool flag2 = !flag;
				nint num3 = 0;
				if (!flag2)
				{
					num2++;
					string text3 = num.ToString();
					string text4 = "dynamicInput_" + text3;
					num = num2;
					text2 = text4;
					continue;
				}
				goto IL_0155;
			}
		}
		else if (ports != null)
		{
			bool flag3 = ports.ContainsKey(text);
			bool flag4 = !flag3;
			int num = 0;
			text2 = text;
			nint num3 = 0;
			if (flag4)
			{
				goto IL_0155;
			}
			string text5 = base.name;
			string message = "Port '" + text + "' already exists in " + text5;
			Debug.LogWarning(message, this);
			if (ports != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				NodePort nodePort2 = default(NodePort);
				nodePort = nodePort2;
				goto IL_02f0;
			}
		}
		goto IL_0266;
		IL_0266:
		return (NodePort)(object)new NullReferenceException();
		IL_0155:
		nodePort = null;
		List<NodePort.PortConnection> connections = new List<NodePort.PortConnection>();
		nodePort.connections = connections;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		nodePort._fieldName = text2;
		nodePort.valueType = type;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
		object obj = default(object);
		if (obj != null)
		{
			if ((object)type == null)
			{
				goto IL_0266;
			}
			string assemblyQualifiedName = type.AssemblyQualifiedName;
			nodePort._typeQualifiedName = assemblyQualifiedName;
		}
		nodePort._direction = direction;
		nodePort._node = this;
		TypeConstraint typeConstraint2 = default(TypeConstraint);
		nodePort._typeConstraint = typeConstraint2;
		nodePort._dynamic = true;
		nodePort._connectionType = connectionType;
		if (ports == null)
		{
			goto IL_0266;
		}
		ports.Add(text2, nodePort);
		goto IL_02f0;
		IL_02f0:
		return nodePort;
	}

	public void RemoveDynamicPort(string fieldName)
	{
		NodePort port = GetPort(fieldName);
		if (port != null)
		{
			NodePort port2 = GetPort(fieldName);
			RemoveDynamicPort(port2);
			return;
		}
		string message = "port " + fieldName + " doesn't exist";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentException ex = new ArgumentException(message);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public void RemoveDynamicPort(NodePort port)
	{
		NodePort nodePort = default(NodePort);
		if (nodePort != null)
		{
			if (nodePort._dynamic)
			{
				nodePort.ClearConnections();
				bool flag = ports == null;
				NodePort nodePort2 = null;
				if (!flag)
				{
					bool flag2 = ports.Remove(nodePort._fieldName);
					return;
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentException ex = new ArgumentException("cannot remove static port");
			ex._002Ector("cannot remove static port");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex2 = new ArgumentNullException("port");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public unsafe void ClearDynamicPorts()
	{
		//IL_01c1: Expected I4, but got I8
		//IL_0144: Expected O, but got Ref
		//IL_00d2: Expected I, but got O
		_003Cget_DynamicPorts_003Ed__10 obj = new _003Cget_DynamicPorts_003Ed__10(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E4__this = this;
		List<NodePort> list = new List<NodePort>(obj);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<NodePort>.Enumerator enumerator = default(List<NodePort>.Enumerator);
		NodePort nodePort = default(NodePort);
		nint num2;
		object obj2;
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				bool flag = nodePort == null;
				num2 = 0;
				obj2 = (object)(&nodePort);
				if (!flag)
				{
					if (!nodePort._dynamic)
					{
						break;
					}
					nodePort.ClearConnections();
					bool flag2 = ports.Remove(nodePort._fieldName);
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ArgumentNullException ex = new ArgumentNullException("port");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
			enumerator.Dispose();
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentException ex2 = new ArgumentException("cannot remove static port");
		ex2._002Ector("cannot remove static port");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		num2 = unchecked((nint)null);
		object obj3 = default(object);
		obj2 = obj3;
		throw ex2;
	}

	public NodePort GetOutputPort(string fieldName)
	{
		NodePort port = GetPort(fieldName);
		if (port != null)
		{
			bool flag = port._direction != NodePort.IO.Output;
			NodePort result = null;
			if (!flag)
			{
				result = port;
			}
			return result;
		}
		return port;
	}

	public T GetConnectedNode<T>(string fieldName) where T : Node
	{
		T port = (T)(object)GetPort(fieldName);
		if ((object)port != null)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rbx_v3 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			NodePort connection = ((NodePort)(object)port).Connection;
			if (connection != null)
			{
				NodePort connection2 = ((NodePort)(object)port).Connection;
				if (connection2 != null)
				{
					if (!(connection2._node != null))
					{
						goto IL_016a;
					}
					NodePort connection3 = ((NodePort)(object)port).Connection;
					if (connection3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						T val = default(T);
						if ((object)val == null)
						{
							goto IL_016a;
						}
						NodePort connection4 = ((NodePort)(object)port).Connection;
						if (connection4 != null)
						{
							return val;
						}
					}
				}
				return (T)(object)new NullReferenceException();
			}
			goto IL_016a;
		}
		return port;
		IL_016a:
		return null;
	}

	public unsafe T GetConnectedNode<T>(string fieldName, out string connectedField) where T : Node
	{
		ref string reference = ref *(string*)null;
		NodePort port = GetPort(fieldName);
		bool flag = port == null;
		T val = (T)(object)port;
		if (!flag)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rbx_v4 (Il2CppMethodInfo)+38]");
			if ((nint)0 == 0)
			{
			}
			reference = ref *(string*)null;
			NodePort connection = port.Connection;
			if (connection != null)
			{
				NodePort connection2 = port.Connection;
				if (connection2 != null)
				{
					if (!(connection2._node != null))
					{
						goto IL_0193;
					}
					NodePort connection3 = port.Connection;
					if (connection3 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						if ((object)val == null)
						{
							goto IL_0193;
						}
						NodePort connection4 = port.Connection;
						if (connection4 != null)
						{
							reference = ref *(string*)connection4._fieldName;
							goto IL_01ab;
						}
					}
				}
				return (T)(object)new NullReferenceException();
			}
			goto IL_0193;
		}
		goto IL_01ab;
		IL_01ab:
		return val;
		IL_0193:
		val = null;
		goto IL_01ab;
	}

	public List<T> GetConnectedNodes<T>(string fieldName) where T : Node
	{
		List<T> port = (List<T>)(object)GetPort(fieldName);
		if (port != null)
		{
			return ((NodePort)(object)port).GetConnectedNodes<T>();
		}
		return port;
	}

	public NodePort GetInputPort(string fieldName)
	{
		NodePort port = GetPort(fieldName);
		if (port != null)
		{
			bool flag = port._direction != NodePort.IO.Input;
			NodePort result = null;
			if (!flag)
			{
				result = port;
			}
			return result;
		}
		return port;
	}

	public NodePort GetPort(string fieldName)
	{
		if (ports != null)
		{
			bool flag = ports.TryGetValue(fieldName, out var value);
			NodePort result = value;
			if (!flag)
			{
				result = null;
			}
			return result;
		}
		return (NodePort)(object)new NullReferenceException();
	}

	public bool HasPort(string fieldName)
	{
		//IL_002b: Expected I4, but got O
		if (ports != null)
		{
			return ports.ContainsKey(fieldName);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public unsafe T GetInputValue<T>(string fieldName, T fallback = default(T))
	{
		//IL_0008: Expected O, but got Ref
		//IL_0027: Expected O, but got I
		//IL_007b: Expected O, but got I
		//IL_008b: Expected O, but got I
		//IL_00a1: Expected O, but got I
		//IL_0105: Expected O, but got I
		//IL_0113: Expected O, but got Ref
		//IL_0123: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
		Node node = (Node)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (SleepyNodes.Node)+38]");
		bool flag = (nint)0 != 0;
		Node node2 = this;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			node2 = node;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (SleepyNodes.Node)+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v2+8]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v1+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ r9_v1+FC]");
		NodePort port = default(NodePort);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			port = GetPort(fieldName);
			if (port == null)
			{
				goto IL_00f5;
			}
		}
		if (!port.IsConnected)
		{
			goto IL_00f5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180732720");
		goto IL_0158;
		IL_00f5:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rdi_v1 (SleepyNodes.Node)+38]");
		object obj6 = 0;
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rax_v11+8]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rcx_v6+28]");
		if ((nint)0 < (nint)0)
		{
			val = fallback;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		goto IL_0158;
		IL_0158:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe bool TryGetInputValue<T>(string fieldName, out T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_003d: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r10_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r10_v1 (Il2CppClass<T>)+FC]");
		NodePort port = default(NodePort);
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			port = GetPort(fieldName);
		}
		if (port != null && port.IsConnected)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180732720");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
			return true;
		}
		return false;
	}

	public T[] GetInputValues<T>(string fieldName, T[] fallback)
	{
		NodePort port = GetPort(fieldName);
		if (port != null && port.IsConnected)
		{
			return port.GetInputValues<T>();
		}
		return fallback;
	}

	public virtual object GetValue(NodePort port)
	{
		return null;
	}

	public virtual void OnCreateConnection(NodePort from, NodePort to)
	{
	}

	public virtual void OnRemoveConnection(NodePort port)
	{
	}

	public unsafe void ClearConnections()
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_013b: Expected O, but got I
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		IEnumerable<NodePort> enumerable = Ports;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		NodePort nodePort = default(NodePort);
		object obj = (object)(&nodePort);
		NodePort nodePort2 = null;
		object obj2 = default(object);
		object obj11 = default(object);
		NodePort nodePort3 = default(NodePort);
		while (true)
		{
			object obj10;
			object obj3;
			if (nodePort != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = nodePort == null;
					nodePort2 = null;
					if (flag)
					{
						break;
					}
					nint num = (nint)nodePort;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r8_v9+v260 @ rcx_v18*8]");
							if (0 == (nint)typeof(IEnumerator<NodePort>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ r10_v5 (Il2CppClass<SleepyNodes.NodePort>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_00ea;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r8_v9+8+v314 @ rcx_v20*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0218;
					}
					goto IL_00ea;
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_00ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			obj3 = 0;
			goto IL_0218;
			IL_0218:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v319 @ rdx_v11] (should have been resolved before IL gen)");
			nodePort3.ClearConnections();
		}
		throw new NullReferenceException();
	}

	public virtual void OnDrawGizmosSelected()
	{
	}

	protected Node()
	{
		NodePortDictionary nodePortDictionary = new NodePortDictionary();
		List<string> keys = new List<string>();
		nodePortDictionary.keys = keys;
		nodePortDictionary.values = new List<NodePort>();
		nodePortDictionary._002Ector();
		ports = nodePortDictionary;
		base._002Ector();
	}
}
