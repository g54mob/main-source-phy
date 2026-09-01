using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SleepyNodes
{
	[Serializable]
	public class NodePort
	{
		public enum IO
		{
			Input = 0,
			Output = 1
		}

		[Serializable]
		private class PortConnection
		{
			[SerializeField]
			public string fieldName;

			[SerializeField]
			public Node node;

			[NonSerialized]
			private NodePort port;

			[SerializeField]
			public List<Vector2> reroutePoints;

			public NodePort Port => null;

			public PortConnection(NodePort port)
			{
			}

			private NodePort GetPort()
			{
				return null;
			}
		}

		private Type valueType;

		[SerializeField]
		private string _fieldName;

		[SerializeField]
		private Node _node;

		[SerializeField]
		private string _typeQualifiedName;

		[SerializeField]
		private List<PortConnection> connections;

		[SerializeField]
		private IO _direction;

		[SerializeField]
		private Node.ConnectionType _connectionType;

		[SerializeField]
		private Node.TypeConstraint _typeConstraint;

		[SerializeField]
		private bool _dynamic;

		public int ConnectionCount => 0;

		public NodePort Connection => null;

		public IO direction => default(IO);

		public Node.ConnectionType connectionType => default(Node.ConnectionType);

		public Node.TypeConstraint typeConstraint => default(Node.TypeConstraint);

		public bool IsConnected => false;

		public bool IsInput => false;

		public bool IsOutput => false;

		public string fieldName => null;

		public Node node => null;

		public bool IsDynamic => false;

		public bool IsStatic => false;

		public Type ValueType
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public T GetConnectedNode<T>() where T : Node
		{
			return null;
		}

		public T GetConnectedNode<T>(out string field) where T : Node
		{
			field = null;
			return null;
		}

		public List<T> GetConnectedNodes<T>() where T : Node
		{
			return null;
		}

		public NodePort(FieldInfo fieldInfo)
		{
		}

		public NodePort(NodePort nodePort, Node node)
		{
		}

		public NodePort(string fieldName, Type type, IO direction, Node.ConnectionType connectionType, Node.TypeConstraint typeConstraint, Node node)
		{
		}

		public void VerifyConnections()
		{
		}

		public object GetOutputValue()
		{
			return null;
		}

		public object GetInputValue()
		{
			return null;
		}

		public object[] GetInputValues()
		{
			return null;
		}

		public T GetInputValue<T>()
		{
			return default(T);
		}

		public T[] GetInputValues<T>()
		{
			return null;
		}

		public bool TryGetInputValue<T>(out T value)
		{
			value = default(T);
			return false;
		}

		public float GetInputSum(float fallback)
		{
			return 0f;
		}

		public int GetInputSum(int fallback)
		{
			return 0;
		}

		public void Connect(NodePort port)
		{
		}

		public List<NodePort> GetConnections()
		{
			return null;
		}

		public NodePort GetConnection(int i)
		{
			return null;
		}

		public int GetConnectionIndex(NodePort port)
		{
			return 0;
		}

		public bool IsConnectedTo(NodePort port)
		{
			return false;
		}

		public bool CanConnectTo(NodePort port)
		{
			return false;
		}

		public void Disconnect(NodePort port)
		{
		}

		public void Disconnect(int i)
		{
		}

		public void ClearConnections()
		{
		}

		public List<Vector2> GetReroutePoints(int index)
		{
			return null;
		}

		public void SwapConnections(NodePort targetPort)
		{
		}

		public void AddConnections(NodePort targetPort)
		{
		}

		public void MoveConnections(NodePort targetPort)
		{
		}

		public void Redirect(List<Node> oldNodes, List<Node> newNodes)
		{
		}
	}
}
