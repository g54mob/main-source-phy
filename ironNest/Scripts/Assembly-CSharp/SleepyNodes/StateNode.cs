using System;
using System.Collections.Generic;

namespace SleepyNodes
{
	[Serializable]
	public abstract class StateNode : Node
	{
		public class NodeExecutionState
		{
			public string ID;

			public StateNode Node;

			public Dictionary<string, object> State;

			public string lastFieldPort;

			public bool ListeningToEvents;

			public static NodeExecutionState NewState => null;

			public void Set<T>(string key, T value)
			{
			}

			public T Get<T>(string key, T defaultValue)
			{
				return default(T);
			}

			public void Set<T>(EntityContextKeys key, T value)
			{
			}

			public T Get<T>(EntityContextKeys key, T defaultValue)
			{
				return default(T);
			}

			public void Set<T>(LocationContextKeys key, T value)
			{
			}

			public T Get<T>(LocationContextKeys key, T defaultValue)
			{
				return default(T);
			}

			public bool TryGet<T>(EntityContextKeys key, out T value)
			{
				value = default(T);
				return false;
			}

			public bool TryGet<T>(LocationContextKeys key, out T value)
			{
				value = default(T);
				return false;
			}

			public bool TryGet<T>(string key, out T value)
			{
				value = default(T);
				return false;
			}
		}

		[ReadOnly]
		public string NodeID;

		[Input(ShowBackingValue.Unconnected, ConnectionType.Multiple, TypeConstraint.None, false, connectionType = ConnectionType.Multiple, backingValue = ShowBackingValue.Never)]
		public StateNode From;

		public void SetState<T>(NodeExecutionState state, string key, T value)
		{
		}

		public T GetState<T>(NodeExecutionState state, string key, T defaultValue)
		{
			return default(T);
		}

		public bool TryGetState<T>(NodeExecutionState state, string key, out T value)
		{
			value = default(T);
			return false;
		}

		private void OnValidate()
		{
		}

		public virtual void ResetNode()
		{
		}

		public virtual void OnNotification(NodeExecutionState state, string notif)
		{
		}

		public virtual void OnEnter(NodeExecutionState state)
		{
		}

		public virtual void OnExecute(NodeExecutionState state)
		{
		}

		public virtual void OnExit(NodeExecutionState state, StateNode To, string connectedFieldName)
		{
		}

		public virtual void OnExit(NodeExecutionState state, string outFieldName)
		{
		}

		public override object GetValue(NodePort port)
		{
			return null;
		}

		public virtual void OnEvent(EventNode.EventData data, NodeExecutionState state)
		{
		}
	}
}
