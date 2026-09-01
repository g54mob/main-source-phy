using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace SleepyNodes;

[Serializable]
public abstract class NodeGraph : ScriptableObject
{
	public List<Node> nodes;

	private readonly List<Type> _003CNodeRestriction_003Ek__BackingField;

	private readonly List<Type> _003CNodeTypeExludes_003Ek__BackingField;

	public virtual List<Type> NodeRestriction => _003CNodeRestriction_003Ek__BackingField;

	public virtual List<Type> NodeTypeExludes => _003CNodeTypeExludes_003Ek__BackingField;

	public T AddNode<T>() where T : Node
	{
		//IL_0011: Expected O, but got I
		//IL_001a: Expected I, but got O
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)0);
		nint num = (nint)this;
		Node node = AddNode(typeFromHandle);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		T result = default(T);
		return result;
	}

	public virtual Node AddNode(Type type)
	{
		//IL_000d: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		Node.graphHotfix = this;
		ScriptableObject scriptableObject = ScriptableObject.CreateInstance(type);
		if ((object)scriptableObject != null)
		{
			nint num = (nint)scriptableObject;
			nint num2 = (nint)typeof(Node);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v4 (Il2CppClass<SleepyNodes.Node>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v3 (Il2CppClass<UnityEngine.ScriptableObject>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rdx_v4 (Il2CppClass<SleepyNodes.Node>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v3 (Il2CppClass<UnityEngine.ScriptableObject>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v8+FFFFFFF8+v53 @ rax_v7*8]");
				if (0 == (nint)typeof(Node) && nodes != null)
				{
					nodes.Add((Node)scriptableObject);
					return (Node)scriptableObject;
				}
			}
		}
		return (Node)(object)new NullReferenceException();
	}

	public virtual Node CopyNode(Node original)
	{
		Node.graphHotfix = this;
		Node node = UnityEngine.Object.Instantiate(original);
		if ((object)node != null)
		{
			node.graph = this;
			node.ClearConnections();
			if (nodes != null)
			{
				nodes.Add(node);
				return node;
			}
		}
		return (Node)(object)new NullReferenceException();
	}

	public virtual void RemoveNode(Node node)
	{
		node.ClearConnections();
		bool flag = nodes.Remove(node);
		if (Application.isPlaying)
		{
			UnityEngine.Object.Destroy(node);
		}
	}

	public virtual void Clear()
	{
		//IL_003a: Expected O, but got I4
		//IL_0043: Expected O, but got I4
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		if (Application.isPlaying)
		{
			List<Node> list = nodes;
			object obj = 0;
			object obj2 = 0;
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			while ((nint)obj2 < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityEngine.Object.Destroy(obj3);
				list = nodes;
				obj++;
				obj2 = obj;
			}
		}
		List<Node> list2 = nodes;
		int version = list2._version + 1;
		list2._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj4 = default(object);
		if (obj4 == null)
		{
			list2._size = 0;
			return;
		}
		list2._size = 0;
		if (list2._size > 0)
		{
			Array.Clear(list2._items, 0, list2._size);
		}
	}

	public unsafe virtual NodeGraph Copy()
	{
		//IL_01c6: Expected O, but got I4
		//IL_01ff: Expected I4, but got O
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_03b1: Expected I, but got O
		NodeGraph nodeGraph = UnityEngine.Object.Instantiate(this);
		List<Node> list = nodes;
		bool flag = nodes == null;
		int num = 0;
		Node node = null;
		Node node2 = null;
		if (!flag)
		{
			UnityEngine.Object obj = default(UnityEngine.Object);
			Node node4 = default(Node);
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			Node node8 = default(Node);
			object obj3 = default(object);
			NodePort nodePort = default(NodePort);
			while (true)
			{
				if ((nint)node < list._size)
				{
					node2 = (Node)(object)nodes;
					if (nodes == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj != null)
					{
						Node.graphHotfix = nodeGraph;
						node2 = (Node)(object)nodes;
						if (nodes == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Node node3 = UnityEngine.Object.Instantiate(node4);
						bool flag2 = (object)node3 == null;
						node2 = node4;
						if (flag2)
						{
							break;
						}
						node3.graph = nodeGraph;
						node2 = (Node)(node3 + 24);
						if ((object)nodeGraph == null)
						{
							break;
						}
						bool flag3 = nodeGraph.nodes == null;
						node2 = (Node)(object)nodeGraph.nodes;
						if (flag3)
						{
							break;
						}
						nodeGraph.nodes.set_Item(num, node3);
					}
					Node node5 = (Node)(num + 1);
					list = nodes;
					bool flag4 = nodes == null;
					node2 = node5;
					if (flag4)
					{
						break;
					}
					num = (int)node5;
					node = node5;
					continue;
				}
				bool flag5 = (object)nodeGraph == null;
				Node node6 = null;
				Node node7 = null;
				if (flag5)
				{
					break;
				}
				while (true)
				{
					List<Node> list2 = nodeGraph.nodes;
					if (nodeGraph.nodes == null)
					{
						break;
					}
					if ((nint)node7 < list2._size)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj2 != null)
						{
							if (nodeGraph.nodes == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if ((object)node8 == null)
							{
								break;
							}
							IEnumerable<NodePort> ports = node8.Ports;
							if (ports == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							int num2 = (int)(&node6);
							nint num3 = 0;
							node2 = null;
							while (true)
							{
								if ((object)node6 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
									if (obj3 == null)
									{
										break;
									}
									bool flag6 = (object)node6 == null;
									node2 = null;
									if (!flag6)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
										bool flag7 = nodePort == null;
										node2 = null;
										if (!flag7)
										{
											nodePort.Redirect(nodes, nodeGraph.nodes);
											num3 = unchecked((nint)null);
											continue;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							bool flag8 = ((int*)num2)->m_value == 0;
							node = null;
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
								node = null;
							}
							num = (int)(&node6);
						}
						node7 = (Node)(node7 + 1);
						continue;
					}
					return nodeGraph;
				}
				break;
			}
		}
		throw new NullReferenceException();
	}

	protected virtual void OnDestroy()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<SleepyNodes.NodeGraph>)+1C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<SleepyNodes.NodeGraph>)+1D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected NodeGraph()
	{
		List<Node> list = new List<Node>();
		nodes = list;
		_003CNodeRestriction_003Ek__BackingField = new List<Type> { Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Node)) };
		_003CNodeTypeExludes_003Ek__BackingField = new List<Type>();
		base._002Ector();
	}
}
