using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SleepyNodes
{
	[Serializable]
	public abstract class Node : ScriptableObject
	{
		public enum ShowBackingValue
		{
			Never = 0,
			Unconnected = 1,
			Always = 2
		}

		public enum ConnectionType
		{
			Multiple = 0,
			Override = 1
		}

		public enum TypeConstraint
		{
			None = 0,
			Inherited = 1,
			Strict = 2
		}

		[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
		public class InputAttribute : Attribute
		{
			public ShowBackingValue backingValue;

			public ConnectionType connectionType;

			public bool dynamicPortList;

			public TypeConstraint typeConstraint;

			public bool ForceDrawSingle;

			[Obsolete("Use dynamicPortList instead")]
			public bool instancePortList
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			public InputAttribute(ShowBackingValue backingValue = ShowBackingValue.Unconnected, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false)
			{
			}
		}

		[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
		public class OutputAttribute : Attribute
		{
			public ShowBackingValue backingValue;

			public ConnectionType connectionType;

			public bool dynamicPortList;

			public TypeConstraint typeConstraint;

			public bool ForceDrawSingle;

			public OutputAttribute(ShowBackingValue backingValue = ShowBackingValue.Never, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false)
			{
			}

			[Obsolete("Use constructor with TypeConstraint")]
			public OutputAttribute(ShowBackingValue backingValue, ConnectionType connectionType, bool dynamicPortList)
			{
			}
		}

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
		public class CreateNodeMenuAttribute : Attribute
		{
			public string menuName;

			public CreateNodeMenuAttribute(string menuName)
			{
			}
		}

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
		public class NodeTintAttribute : Attribute
		{
			public Color color;

			public NodeTintAttribute(float r, float g, float b)
			{
			}

			public NodeTintAttribute(string hex)
			{
			}

			public NodeTintAttribute(byte r, byte g, byte b)
			{
			}
		}

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
		public class NodeWidthAttribute : Attribute
		{
			public int width;

			public NodeWidthAttribute(int width)
			{
			}
		}

		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
		public class NodeNameAttribute : Attribute
		{
			public string Name;

			public NodeNameAttribute(string name)
			{
			}
		}

		[Serializable]
		private class NodePortDictionary : Dictionary<string, NodePort>, ISerializationCallbackReceiver
		{
			[SerializeField]
			private List<string> keys;

			[SerializeField]
			private List<NodePort> values;

			public void OnBeforeSerialize()
			{
			}

			public void OnAfterDeserialize()
			{
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

		[CompilerGenerated]
		private sealed class _003Cget_DynamicInputs_003Ed__14 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private IEnumerator<NodePort> _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_DynamicInputs_003Ed__14(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cget_DynamicOutputs_003Ed__12 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private IEnumerator<NodePort> _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_DynamicOutputs_003Ed__12(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cget_DynamicPorts_003Ed__10 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private IEnumerator<NodePort> _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_DynamicPorts_003Ed__10(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cget_Inputs_003Ed__8 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private IEnumerator<NodePort> _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_Inputs_003Ed__8(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cget_Outputs_003Ed__6 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private IEnumerator<NodePort> _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_Outputs_003Ed__6(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[CompilerGenerated]
		private sealed class _003Cget_Ports_003Ed__4 : IEnumerable<NodePort>, IEnumerable, IEnumerator<NodePort>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private NodePort _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			public Node _003C_003E4__this;

			private Dictionary<string, NodePort>.ValueCollection.Enumerator _003C_003E7__wrap1;

			NodePort IEnumerator<NodePort>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003Cget_Ports_003Ed__4(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<NodePort> IEnumerable<NodePort>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[SerializeField]
		public NodeGraph graph;

		[SerializeField]
		public Vector2 position;

		[SerializeField]
		private NodePortDictionary ports;

		public static NodeGraph graphHotfix;

		public IEnumerable<NodePort> Ports
		{
			[IteratorStateMachine(typeof(_003Cget_Ports_003Ed__4))]
			get
			{
				return null;
			}
		}

		public IEnumerable<NodePort> Outputs
		{
			[IteratorStateMachine(typeof(_003Cget_Outputs_003Ed__6))]
			get
			{
				return null;
			}
		}

		public IEnumerable<NodePort> Inputs
		{
			[IteratorStateMachine(typeof(_003Cget_Inputs_003Ed__8))]
			get
			{
				return null;
			}
		}

		public IEnumerable<NodePort> DynamicPorts
		{
			[IteratorStateMachine(typeof(_003Cget_DynamicPorts_003Ed__10))]
			get
			{
				return null;
			}
		}

		public IEnumerable<NodePort> DynamicOutputs
		{
			[IteratorStateMachine(typeof(_003Cget_DynamicOutputs_003Ed__12))]
			get
			{
				return null;
			}
		}

		public IEnumerable<NodePort> DynamicInputs
		{
			[IteratorStateMachine(typeof(_003Cget_DynamicInputs_003Ed__14))]
			get
			{
				return null;
			}
		}

		protected void OnEnable()
		{
		}

		protected virtual void Init()
		{
		}

		public void UpdateStaticPorts()
		{
		}

		public void VerifyConnections()
		{
		}

		public NodePort AddDynamicInput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
		{
			return null;
		}

		public NodePort AddDynamicOutput(Type type, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
		{
			return null;
		}

		private NodePort AddDynamicPort(Type type, NodePort.IO direction, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
		{
			return null;
		}

		public void RemoveDynamicPort(string fieldName)
		{
		}

		public void RemoveDynamicPort(NodePort port)
		{
		}

		[ContextMenu("Clear Dynamic Ports")]
		public void ClearDynamicPorts()
		{
		}

		public NodePort GetOutputPort(string fieldName)
		{
			return null;
		}

		public T GetConnectedNode<T>(string fieldName) where T : Node
		{
			return null;
		}

		public T GetConnectedNode<T>(string fieldName, out string connectedField) where T : Node
		{
			connectedField = null;
			return null;
		}

		public List<T> GetConnectedNodes<T>(string fieldName) where T : Node
		{
			return null;
		}

		public NodePort GetInputPort(string fieldName)
		{
			return null;
		}

		public NodePort GetPort(string fieldName)
		{
			return null;
		}

		public bool HasPort(string fieldName)
		{
			return false;
		}

		public T GetInputValue<T>(string fieldName, T fallback = default(T))
		{
			return default(T);
		}

		public bool TryGetInputValue<T>(string fieldName, out T value)
		{
			value = default(T);
			return false;
		}

		public T[] GetInputValues<T>(string fieldName, params T[] fallback)
		{
			return null;
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

		public void ClearConnections()
		{
		}

		public virtual void OnDrawGizmosSelected()
		{
		}
	}
}
