using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleepyNodes
{
	[Serializable]
	public abstract class NodeGraph : ScriptableObject
	{
		[SerializeField]
		public List<Node> nodes;

		public virtual List<Type> NodeRestriction { get; }

		public virtual List<Type> NodeTypeExludes { get; }

		public T AddNode<T>() where T : Node
		{
			return null;
		}

		public virtual Node AddNode(Type type)
		{
			return null;
		}

		public virtual Node CopyNode(Node original)
		{
			return null;
		}

		public virtual void RemoveNode(Node node)
		{
		}

		public virtual void Clear()
		{
		}

		public virtual NodeGraph Copy()
		{
			return null;
		}

		protected virtual void OnDestroy()
		{
		}
	}
}
