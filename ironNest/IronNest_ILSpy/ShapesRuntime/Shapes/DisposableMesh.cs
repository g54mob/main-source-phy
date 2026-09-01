using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class DisposableMesh : IDisposable
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass10_0
	{
		public DisposableMesh _003C_003E4__this;

		public DrawCommand cmd;
	}

	private static int activeMeshCount;

	protected Mesh mesh;

	protected bool meshDirty;

	protected bool hasData;

	private bool hasMesh;

	private bool disposeWhenFullyReleased;

	internal List<DrawCommand> usedByCommands;

	public static int ActiveMeshCount => activeMeshCount;

	protected void EnsureMeshExists()
	{
		if (hasData)
		{
			if (!hasMesh || this.mesh == null)
			{
				Mesh mesh = ShapesMeshPool.GetMesh();
				this.mesh = mesh;
				int num = activeMeshCount + 1;
				activeMeshCount = num;
				hasMesh = true;
			}
		}
		else
		{
			Debug.LogError("Mesh requested, but there's no data to generate a mesh from");
		}
	}

	internal void RegisterToCommandBuffer(DrawCommand cmd)
	{
		DrawCommand drawCommand = default(DrawCommand);
		if (usedByCommands != null)
		{
			if (usedByCommands.Contains(drawCommand))
			{
				return;
			}
		}
		else
		{
			List<DrawCommand> list = ListPool<DrawCommand>.Alloc();
			usedByCommands = list;
		}
		usedByCommands.Add(drawCommand);
		drawCommand.cachedMeshes.Add(this);
	}

	internal void ReleaseFromCommand(DrawCommand cmd)
	{
		bool flag = usedByCommands.Remove(cmd);
		List<DrawCommand> list = usedByCommands;
		if (list._size == 0 && disposeWhenFullyReleased)
		{
			List<DrawCommand> list2 = usedByCommands;
			bool flag2 = usedByCommands == null;
			disposeWhenFullyReleased = true;
			bool flag3 = !flag2;
			if (usedByCommands != null && list2._size == 0)
			{
				ListPool<DrawCommand>.Free(usedByCommands);
				usedByCommands = null;
				flag3 = false;
			}
			if (hasMesh && !flag3)
			{
				ShapesMeshPool.Release(mesh);
				int num = activeMeshCount - 1;
				activeMeshCount = num;
				hasMesh = false;
			}
		}
	}

	public void Dispose()
	{
		List<DrawCommand> list = usedByCommands;
		bool flag = usedByCommands == null;
		disposeWhenFullyReleased = true;
		bool flag2 = !flag;
		if (usedByCommands != null && list._size == 0)
		{
			ListPool<DrawCommand>.Free(usedByCommands);
			usedByCommands = null;
			flag2 = false;
		}
		if (hasMesh && !flag2)
		{
			ShapesMeshPool.Release(mesh);
			int num = activeMeshCount - 1;
			activeMeshCount = num;
			hasMesh = false;
		}
	}

	protected void ClearMesh()
	{
		if (hasMesh)
		{
			mesh.Clear();
		}
	}

	protected virtual bool ExternallyDirty()
	{
		return false;
	}

	protected virtual void UpdateMesh()
	{
	}

	protected unsafe bool EnsureMeshIsReadyToRender(out Mesh outMesh, Action updateMesh)
	{
		//IL_01d9: Expected I4, but got O
		Action action = default(Action);
		IntPtr invoke_impl;
		IntPtr method;
		IntPtr method_code;
		if (hasData)
		{
			if (hasMesh)
			{
				if (!meshDirty)
				{
					goto IL_01d9;
				}
				if (action == null)
				{
					goto IL_01cb;
				}
				invoke_impl = ((Delegate)action).invoke_impl;
				method = ((Delegate)action).method;
				method_code = ((Delegate)action).method_code;
				goto IL_01ea;
			}
			if (hasData)
			{
				bool flag = !hasMesh;
				Action action2 = action;
				Action action3;
				if (!flag)
				{
					bool flag2 = this.mesh == null;
					bool flag3 = !flag2;
					action2 = null;
					action3 = null;
					if (flag3)
					{
						goto IL_0173;
					}
				}
				Mesh mesh = ShapesMeshPool.GetMesh();
				this.mesh = mesh;
				int num = activeMeshCount + 1;
				activeMeshCount = num;
				hasMesh = true;
				action3 = action2;
			}
			else
			{
				Debug.LogError("Mesh requested, but there's no data to generate a mesh from");
				Action action3 = action;
			}
			goto IL_0173;
		}
		ref Mesh reference = ref *(Mesh*)null;
		return false;
		IL_01ea:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v218 @ rax_v3 (System.IntPtr) (should have been resolved before IL gen)");
		meshDirty = false;
		goto IL_01d9;
		IL_01d9:
		reference = ref *(Mesh*)this.mesh;
		return hasMesh;
		IL_0173:
		if (action == null)
		{
			goto IL_01cb;
		}
		invoke_impl = ((Delegate)action).invoke_impl;
		method = ((Delegate)action).method;
		method_code = ((Delegate)action).method_code;
		goto IL_01ea;
		IL_01cb:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public DisposableMesh()
	{
		UpdateMesh();
	}

	private void _003CRegisterToCommandBuffer_003Eg__Add_007C10_0(ref _003C_003Ec__DisplayClass10_0 P_0)
	{
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		//IL_0043: Expected O, but got I
		List<DrawCommand> list = usedByCommands;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass10_0&)+8]");
		list.Add((DrawCommand)0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [ @ rdx (<>c__DisplayClass10_0&)+8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v4+38]");
		((List<DisposableMesh>)0).Add(this);
	}
}
