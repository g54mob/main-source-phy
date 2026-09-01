using System;
using System.Collections.Generic;
using mattmc3.dotmore.Extensions;

namespace mattmc3.dotmore.Collections.Generic
{
	public class TreeNode<T>
	{
		private List<TreeNode<T>> _children;

		public T Value { get; private set; }

		public TreeNode<T> Parent { get; private set; }

		public TreeNode<T> FirstChild
		{
			get
			{
				return (!HasChild) ? null : _children[0];
			}
		}

		public TreeNode<T> LastChild
		{
			get
			{
				return (!HasChild) ? null : _children[_children.Count - 1];
			}
		}

		public TreeNode<T> PreviousSibling { get; private set; }

		public TreeNode<T> FollowingSibling { get; private set; }

		public TreeNode<T> Root
		{
			get
			{
				TreeNode<T> treeNode = this;
				while (!treeNode.IsRoot)
				{
					treeNode = treeNode.Parent;
				}
				return treeNode;
			}
		}

		public int Depth
		{
			get
			{
				int num = 0;
				TreeNode<T> treeNode = this;
				while (!treeNode.IsRoot)
				{
					treeNode = treeNode.Parent;
					num++;
				}
				return num;
			}
		}

		public int ChildCount
		{
			get
			{
				return _children.Count;
			}
		}

		public bool HasChild
		{
			get
			{
				return ChildCount > 0;
			}
		}

		public bool IsLeaf
		{
			get
			{
				return ChildCount == 0;
			}
		}

		public bool IsRoot
		{
			get
			{
				return Parent == null;
			}
		}

		public bool IsFirstSibling
		{
			get
			{
				return PreviousSibling == null;
			}
		}

		public bool IsLastSibling
		{
			get
			{
				return FollowingSibling == null;
			}
		}

		public TreeNode()
		{
			Parent = null;
			FollowingSibling = null;
			PreviousSibling = null;
			_children = new List<TreeNode<T>>();
		}

		public TreeNode(T value)
			: this()
		{
			Value = value;
		}

		public bool Contains(T childValue)
		{
			if (Value.Equals(childValue))
			{
				return true;
			}
			foreach (TreeNode<T> descendant in GetDescendants())
			{
				if (descendant.Value.Equals(childValue))
				{
					return true;
				}
			}
			return false;
		}

		public bool Contains(TreeNode<T> child)
		{
			if (Equals(child))
			{
				return true;
			}
			foreach (TreeNode<T> descendant in GetDescendants())
			{
				if (descendant.Equals(child))
				{
					return true;
				}
			}
			return false;
		}

		public TreeNode<T> PrependChild(T childValue)
		{
			return AttachChild(0, childValue);
		}

		public void PrependChild(TreeNode<T> child)
		{
			AttachChild(0, child);
		}

		public TreeNode<T> AppendChild(T childValue)
		{
			return AttachChild(ChildCount, childValue);
		}

		public void AppendChild(TreeNode<T> child)
		{
			AttachChild(ChildCount, child);
		}

		public TreeNode<T> AttachChild(int index, T childValue)
		{
			TreeNode<T> treeNode = new TreeNode<T>(childValue);
			AttachChild(index, treeNode);
			return treeNode;
		}

		public void AttachChild(int index, TreeNode<T> child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (0 > index || index > ChildCount)
			{
				throw new IndexOutOfRangeException("The index specified is not valid: " + index);
			}
			child.Detach();
			TreeNode<T> treeNode = null;
			TreeNode<T> treeNode2 = null;
			if (index != 0)
			{
				treeNode = _children[index - 1];
			}
			if (index != ChildCount)
			{
				treeNode2 = _children[index];
			}
			child.PreviousSibling = treeNode;
			child.FollowingSibling = treeNode2;
			if (treeNode != null)
			{
				treeNode.FollowingSibling = child;
			}
			if (treeNode2 != null)
			{
				treeNode2.PreviousSibling = child;
			}
			child.Parent = this;
			_children.Insert(index, child);
		}

		public void AttachFollowingSibling(TreeNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (IsRoot)
			{
				node.Detach();
				node.FollowingSibling = FollowingSibling;
				node.PreviousSibling = this;
				if (FollowingSibling != null)
				{
					FollowingSibling.PreviousSibling = node;
				}
				FollowingSibling = node;
			}
			else
			{
				int num = Parent._children.IndexOf(this);
				if (num < 0 || num >= Parent.ChildCount)
				{
					throw new Exception("Unexpected error");
				}
				Parent.AttachChild(num + 1, node);
			}
		}

		public void AttachPreviousSibling(TreeNode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (IsRoot)
			{
				node.Detach();
				node.PreviousSibling = PreviousSibling;
				node.FollowingSibling = this;
				if (PreviousSibling != null)
				{
					PreviousSibling.FollowingSibling = node;
				}
				PreviousSibling = node;
			}
			else
			{
				int num = Parent._children.IndexOf(this);
				if (num < 0 || num >= Parent.ChildCount)
				{
					throw new Exception("Unexpected error");
				}
				Parent.AttachChild(num, node);
			}
		}

		public void Detach()
		{
			if (Parent != null)
			{
				if (!Parent._children.Remove(this))
				{
					throw new Exception("Unexpected error detaching node.");
				}
				Parent = null;
			}
			if (PreviousSibling != null)
			{
				PreviousSibling.FollowingSibling = FollowingSibling;
			}
			if (FollowingSibling != null)
			{
				FollowingSibling.PreviousSibling = PreviousSibling;
			}
			PreviousSibling = null;
			FollowingSibling = null;
		}

		public void RemoveAllChildren()
		{
			while (_children.Count > 0)
			{
				RemoveChildAt(0);
			}
		}

		public bool RemoveChild(TreeNode<T> childNode)
		{
			int num = _children.IndexOf(childNode);
			if (num >= 0)
			{
				RemoveChildAt(num);
				return true;
			}
			return false;
		}

		public void RemoveChildAt(int index)
		{
			if (0 > index || index >= ChildCount)
			{
				throw new IndexOutOfRangeException("The index provided is out of range: " + index);
			}
			TreeNode<T> treeNode = _children[index];
			treeNode.Detach();
		}

		public IEnumerable<TreeNode<T>> GetAncestors()
		{
			TreeNode<T> curNode = this;
			while (!curNode.IsRoot)
			{
				curNode = curNode.Parent;
				yield return curNode;
			}
		}

		public IEnumerable<TreeNode<T>> GetChildren()
		{
			return _children;
		}

		public IEnumerable<TreeNode<T>> GetFollowingSiblings()
		{
			TreeNode<T> curNode = this;
			while (!curNode.IsLastSibling)
			{
				curNode = curNode.FollowingSibling;
				yield return curNode;
			}
		}

		public IEnumerable<TreeNode<T>> GetPreviousSiblings()
		{
			TreeNode<T> curNode = this;
			while (!curNode.IsFirstSibling)
			{
				curNode = curNode.PreviousSibling;
				yield return curNode;
			}
		}

		public IEnumerable<TreeNode<T>> GetDescendants()
		{
			foreach (TreeNode<T> child in _children)
			{
				yield return child;
				foreach (TreeNode<T> descendant in child.GetDescendants())
				{
					yield return descendant;
				}
			}
		}

		public IEnumerable<TreeNode<T>> GetSelfAndDescendants(TreeTraversal traversal)
		{
			switch (traversal)
			{
			case TreeTraversal.BreadthFirst:
				yield return this;
				{
					foreach (TreeNode<T> item in GetDescendantsBreadthFirst(new TreeNode<T>[1] { this }))
					{
						yield return item;
					}
					break;
				}
			case TreeTraversal.DepthFirstPreOrder:
				yield return this;
				{
					foreach (TreeNode<T> descendant in GetDescendants())
					{
						yield return descendant;
					}
					break;
				}
			case TreeTraversal.DepthFirstPostOrder:
			{
				foreach (TreeNode<T> item2 in GetDescendantsDepthFirstPostOrder(this))
				{
					yield return item2;
				}
				break;
			}
			default:
				throw new ArgumentException("Traversal method unhandled [{0}]".FormatWith(traversal), "traversal");
			}
		}

		private IEnumerable<TreeNode<T>> GetDescendantsBreadthFirst(IEnumerable<TreeNode<T>> nodes)
		{
			List<TreeNode<T>> children = new List<TreeNode<T>>();
			foreach (TreeNode<T> parent in nodes)
			{
				foreach (TreeNode<T> child in parent.GetChildren())
				{
					yield return child;
					children.Add(child);
				}
			}
			if (children.Count == 0)
			{
				yield break;
			}
			foreach (TreeNode<T> item in GetDescendantsBreadthFirst(children))
			{
				yield return item;
			}
		}

		private IEnumerable<TreeNode<T>> GetDescendantsDepthFirstPostOrder(TreeNode<T> node)
		{
			foreach (TreeNode<T> child in node.GetChildren())
			{
				foreach (TreeNode<T> item in GetDescendantsDepthFirstPostOrder(child))
				{
					yield return item;
				}
			}
			yield return node;
		}
	}
}
