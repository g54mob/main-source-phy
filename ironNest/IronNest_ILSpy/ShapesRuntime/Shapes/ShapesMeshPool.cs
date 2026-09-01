using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public static class ShapesMeshPool
{
	private static int meshesAllocated = 0;

	private static Stack<Mesh> meshPool;

	public static int MeshCountInPool
	{
		get
		{
			//IL_001d: Expected I4, but got O
			Stack<Mesh> stack = meshPool;
			if (meshPool != null)
			{
				return stack._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public static int MeshesAllocatedCount => meshesAllocated;

	public static int MeshCountInUse
	{
		get
		{
			//IL_005c: Expected I4, but got O
			Stack<Mesh> stack = meshPool;
			if (meshPool != null)
			{
				return meshesAllocated - stack._size;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public static Mesh GetMesh()
	{
		Stack<Mesh> stack = meshPool;
		if (meshPool != null)
		{
			if (stack._size <= 0)
			{
				int num = meshesAllocated + 1;
				meshesAllocated = num;
				Mesh mesh = new Mesh();
				if ((object)mesh != null)
				{
					mesh.name = "Pooled Mesh";
					mesh.hideFlags = HideFlags.DontSave;
					return mesh;
				}
			}
			else if (meshPool != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
				Mesh mesh2 = default(Mesh);
				if ((object)mesh2 != null)
				{
					mesh2.Clear();
					return mesh2;
				}
			}
		}
		return (Mesh)(object)new NullReferenceException();
	}

	public static void Release(Mesh m)
	{
		meshPool.Push(m);
	}

	static ShapesMeshPool()
	{
		Stack<Mesh> stack = new Stack<Mesh>();
		meshPool = stack;
	}
}
