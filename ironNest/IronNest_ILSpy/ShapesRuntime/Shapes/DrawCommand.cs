using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Shapes;

public class DrawCommand : IDisposable
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<DrawCommand> _003C_003E9__9_0;

		public static Func<KeyValuePair<Camera, List<DrawCommand>>, bool> _003C_003E9__10_0;

		public static Action<DrawCommand> _003C_003E9__10_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003C_002Ecctor_003Eb__8_0(Scene scene)
		{
			FlushNullCameras();
		}

		internal void _003CClearAllCommands_003Eb__9_0(DrawCommand cmd)
		{
			cmd.Clear();
		}

		internal bool _003CFlushNullCameras_003Eb__10_0(KeyValuePair<Camera, List<DrawCommand>> kvp)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006EB80");
			UnityEngine.Object obj = default(UnityEngine.Object);
			return obj == null;
		}

		internal void _003CFlushNullCameras_003Eb__10_1(DrawCommand cmd)
		{
			cmd.Clear();
		}
	}

	private static int bufferID;

	private static int drawCommandWriteNestLevel;

	private static Stack<DrawCommand> cBuffersWriting;

	internal static Dictionary<Camera, List<DrawCommand>> cBuffersRendering;

	private bool hasValidCamera;

	internal bool hasRendered;

	internal int id;

	private bool pushPopState;

	private Camera cam;

	internal readonly List<int> cachedTextIds;

	internal readonly List<UnityEngine.Object> cachedAssets;

	internal readonly List<DisposableMesh> cachedMeshes;

	internal readonly List<ShapeDrawCall> drawCalls;

	public RenderPassEvent camEvt;

	internal static bool IsAddingDrawCommandsToBuffer
	{
		get
		{
			int num = drawCommandWriteNestLevel ^ drawCommandWriteNestLevel;
			int num2 = drawCommandWriteNestLevel & num;
			bool flag = num2 < 0;
			bool flag2 = drawCommandWriteNestLevel < 0;
			bool flag3 = drawCommandWriteNestLevel == 0;
			bool flag4 = flag2 == flag;
			bool flag5 = !flag3;
			return flag5 & flag4;
		}
	}

	internal static DrawCommand CurrentWritingCommandBuffer
	{
		get
		{
			if (cBuffersWriting != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917220");
				DrawCommand result = default(DrawCommand);
				return result;
			}
			return (DrawCommand)(object)new NullReferenceException();
		}
	}

	static DrawCommand()
	{
		Stack<DrawCommand> stack = new Stack<DrawCommand>();
		cBuffersWriting = stack;
		Dictionary<Camera, List<DrawCommand>> dictionary = new Dictionary<Camera, List<DrawCommand>>();
		cBuffersRendering = dictionary;
		UnityAction<Scene> value = delegate
		{
			FlushNullCameras();
		};
		SceneManager.sceneUnloaded += value;
	}

	public static void ClearAllCommands()
	{
		//IL_00ad: Expected O, but got I
		//IL_013d: Expected I, but got O
		//IL_0153: Expected O, but got I
		FlushNullCameras();
		if (cBuffersRendering != null)
		{
			Dictionary<Camera, List<DrawCommand>>.ValueCollection values = cBuffersRendering.Values;
			if (values != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
				Dictionary<Camera, List<DrawCommand>>.ValueCollection.Enumerator enumerator = default(Dictionary<Camera, List<DrawCommand>>.ValueCollection.Enumerator);
				List<DrawCommand> list = default(List<DrawCommand>);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Action<DrawCommand> action = _003C_003Ec._003C_003E9__9_0;
					if (_003C_003Ec._003C_003E9__9_0 == null)
					{
						Action<DrawCommand> action2 = (_003C_003Ec._003C_003E9__9_0 = delegate(DrawCommand cmd)
						{
							cmd.Clear();
						});
						nint num = (nint)typeof(_003C_003Ec);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v368 @ rax_v33 (Il2CppClass<Shapes.DrawCommand+<>c>)+B8]");
						Dictionary<Camera, List<DrawCommand>> dictionary = (Dictionary<Camera, List<DrawCommand>>)((nint)0 + (nint)8);
						action = action2;
					}
					if (list != null)
					{
						list.ForEach(action);
						list.ForEach((Action<DrawCommand>)0);
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				if (cBuffersRendering != null)
				{
					cBuffersRendering.Clear();
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe static void FlushNullCameras()
	{
		//IL_0041: Expected O, but got I4
		//IL_0199: Expected O, but got I
		//IL_0199: Expected O, but got Ref
		Func<KeyValuePair<Camera, List<DrawCommand>>, bool> predicate = _003C_003Ec._003C_003E9__10_0;
		if (_003C_003Ec._003C_003E9__10_0 == null)
		{
			predicate = (_003C_003Ec._003C_003E9__10_0 = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006EB80");
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				return obj2 == null;
			});
		}
		IEnumerable<KeyValuePair<Camera, List<DrawCommand>>> source = Enumerable.Where(cBuffersRendering, predicate);
		List<KeyValuePair<Camera, List<DrawCommand>>> list = Enumerable.ToList(source);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj = 0;
		List<KeyValuePair<Camera, List<DrawCommand>>>.Enumerator enumerator = default(List<KeyValuePair<Camera, List<DrawCommand>>>.Enumerator);
		List<DrawCommand> list2 = default(List<DrawCommand>);
		Camera key = default(Camera);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
				Action<DrawCommand> action = _003C_003Ec._003C_003E9__10_1;
				if (_003C_003Ec._003C_003E9__10_1 == null)
				{
					action = (_003C_003Ec._003C_003E9__10_1 = delegate(DrawCommand cmd)
					{
						cmd.Clear();
					});
				}
				if (list2 == null)
				{
					break;
				}
				list2.ForEach(action);
				((List<DrawCommand>)(&obj)).ForEach((Action<DrawCommand>)0);
				bool flag = cBuffersRendering.Remove(key);
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private static void RegisterCommand(DrawCommand cmd)
	{
		List<DrawCommand> list = default(List<DrawCommand>);
		if (!cBuffersRendering.TryGetValue(cmd.cam, out var _))
		{
			list = new List<DrawCommand>();
			cBuffersRendering.Add(cmd.cam, list);
		}
		list.Add(cmd);
	}

	internal static void OnCommandRendered(DrawCommand cmd)
	{
		cmd.hasRendered = true;
		if (!cBuffersRendering.TryGetValue(cmd.cam, out var value))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"Tried to remove unlisted draw command {arg}";
			Debug.LogError(message);
		}
		else
		{
			cmd.Clear();
			bool flag = value.Remove(cmd);
		}
	}

	internal unsafe DrawCommand Initialize(Camera cam, RenderPassEvent cameraEvent = RenderPassEvent.BeforeRenderingPostProcessing)
	{
		//IL_00d5: Expected I, but got O
		//IL_01bc: Expected O, but got I
		//IL_01c5: Expected O, but got Ref
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_010c: Expected O, but got I
		//IL_0121: Expected O, but got I
		//IL_012e: Expected O, but got Ref
		//IL_012e: Expected O, but got Ref
		this.cam = cam;
		int num = bufferID + 1;
		bufferID = num;
		id = bufferID;
		if (!(hasValidCamera = cam != null))
		{
			Debug.LogWarning("null camera passed into DrawCommand, nothing will be drawn");
		}
		camEvt = cameraEvent;
		if (cBuffersWriting != null)
		{
			cBuffersWriting.Push(this);
			int num2 = drawCommandWriteNestLevel + 1;
			drawCommandWriteNestLevel = num2;
			ShapesConfig instance = ShapesConfig.Instance;
			if ((object)instance != null)
			{
				pushPopState = instance.pushPopStateInDrawCommands;
				if (~(instance.pushPopStateInDrawCommands ? 1u : 0u) == 0)
				{
					nint num3 = (nint)typeof(Draw);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v18 (Il2CppClass<Shapes.Draw>)+B8]");
					nint num4 = 0;
					object obj = num4 + 200;
					object obj2 = default(object);
					DrawCommand drawCommand = (DrawCommand)(&obj2);
					object obj3 = default(object);
					obj = obj3;
					drawCommand = this;
					do
					{
						drawCommand = (DrawCommand)(drawCommand + 128);
						obj += 128;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24+10]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-60]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-50]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-40]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-30]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-20]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24-10]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v18 (Il2CppClass<Shapes.Draw>)+E4]");
					}
					while ((nint)0 != 0);
					drawCommand = (DrawCommand)obj;
					DrawCommand drawCommand2 = drawCommand;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24+10]");
					drawCommand2.hasValidCamera = false;
					DrawCommand drawCommand3 = drawCommand;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24+20]");
					drawCommand3.cam = (Camera)0;
					DrawCommand drawCommand4 = drawCommand;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v24+30]");
					drawCommand4.cachedAssets = (List<UnityEngine.Object>)0;
					Matrix4x4 matrix4x = default(Matrix4x4);
					StateStack.Push((DrawStyle)(&obj2), (Matrix4x4)(&matrix4x));
				}
				return this;
			}
		}
		return (DrawCommand)(object)new NullReferenceException();
	}

	internal void AppendToBuffer(RasterCommandBuffer cmd)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ShapeDrawCall>.Enumerator enumerator = default(List<ShapeDrawCall>.Enumerator);
		ShapeDrawCall shapeDrawCall = default(ShapeDrawCall);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			shapeDrawCall.AddToCommandBuffer(cmd);
		}
		enumerator.Dispose();
	}

	internal void AppendToBuffer(CommandBuffer cmd)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ShapeDrawCall>.Enumerator enumerator = default(List<ShapeDrawCall>.Enumerator);
		ShapeDrawCall shapeDrawCall = default(ShapeDrawCall);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			shapeDrawCall.AddToCommandBuffer(cmd);
		}
		enumerator.Dispose();
	}

	private unsafe void Clear()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0164: Expected O, but got I4
		//IL_0172: Expected O, but got I4
		//IL_017b: Expected O, but got I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_0123: Expected O, but got I
		//IL_0072: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		CleanupCachedAssetsAndMeshes();
		List<ShapeDrawCall> list = drawCalls;
		hasRendered = false;
		ShapeDrawCall shapeDrawCall = (ShapeDrawCall)0;
		List<ShapeDrawCall> list2 = null;
		object obj3 = 0;
		object obj4 = 0;
		int index = default(int);
		while (true)
		{
			List<ShapeDrawCall> list3 = drawCalls;
			object obj5 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v5 (System.Collections.Generic.List`1<Shapes.ShapeDrawCall>)+18]");
			if ((nint)obj5 >= 0)
			{
				break;
			}
			index = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+10]");
			_ = 0;
			shapeDrawCall.Cleanup();
			list = drawCalls;
			obj3++;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-60]");
			shapeDrawCall = (ShapeDrawCall)0;
			list2 = null;
			obj4 = obj3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rbx_v2 (System.Collections.Generic.List`1<Shapes.ShapeDrawCall>)+1C]");
		_ = (nint)0 + (nint)1;
		ShapeDrawCall shapeDrawCall2 = list2.get_Item(index);
		if ((object)shapeDrawCall2 == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rbx_v2 (System.Collections.Generic.List`1<Shapes.ShapeDrawCall>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rbx_v2 (System.Collections.Generic.List`1<Shapes.ShapeDrawCall>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rbx_v2 (System.Collections.Generic.List`1<Shapes.ShapeDrawCall>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		ObjectPool<DrawCommand>.Free(this);
	}

	private unsafe void CleanupCachedAssetsAndMeshes()
	{
		//IL_0106: Expected O, but got I
		//IL_0266: Expected O, but got Ref
		//IL_028b: Expected O, but got I
		//IL_029f: Expected O, but got I
		//IL_05e6: Expected O, but got I
		//IL_0353: Expected O, but got I
		//IL_0393: Expected O, but got I
		if (cachedTextIds != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<int>.Enumerator enumerator = default(List<int>.Enumerator);
			int num2 = default(int);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				ShapesTextPool instance = ShapesObjPool<TextMeshProShapes, ShapesTextPool>.Instance;
				bool flag = (object)instance == null;
				nint num = 0;
				if (!flag)
				{
					instance.ReleaseElement(num2);
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			List<int> list = cachedTextIds;
			if (cachedTextIds != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rbx_v7 (System.Collections.Generic.List`1<System.Int32>)+1C]");
				_ = (nint)0 + (nint)1;
				((List<int>.Enumerator*)null)->Dispose();
				object obj = default(object);
				if (obj == null)
				{
					_ = 0;
				}
				else
				{
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rbx_v7 (System.Collections.Generic.List`1<System.Int32>)+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rbx_v7 (System.Collections.Generic.List`1<System.Int32>)+10]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rbx_v7 (System.Collections.Generic.List`1<System.Int32>)+18]");
						Array.Clear((Array)num3, 0, 0);
					}
				}
				if (cachedAssets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<UnityEngine.Object>.Enumerator enumerator2 = default(List<UnityEngine.Object>.Enumerator);
					UnityEngine.Object obj2 = default(UnityEngine.Object);
					while (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						ShapesExtensions.DestroyBranched(obj2);
					}
					enumerator2.Dispose();
					List<UnityEngine.Object> list2 = cachedAssets;
					if (cachedAssets != null)
					{
						int version = list2._version + 1;
						list2._version = version;
						((List<UnityEngine.Object>.Enumerator*)null)->Dispose();
						object obj3 = default(object);
						if (obj3 == null)
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
						if (cachedMeshes != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
							List<DisposableMesh>.Enumerator enumerator3 = default(List<DisposableMesh>.Enumerator);
							while (enumerator3.MoveNext())
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
								bool flag2 = num2 == 0;
								List<DrawCommand> list3 = (List<DrawCommand>)(&enumerator3);
								if (flag2)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
									bool flag3 = ((List<DrawCommand>)0).Remove(this);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
									object obj4 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v327 @ rax_v41+18]");
										if ((nint)0 != 0)
										{
											continue;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+1B]");
										if ((nint)0 != 0)
										{
											_ = 1;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
											List<DrawCommand> list4 = (List<DrawCommand>)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
											bool flag4 = (nint)0 == 0;
											bool flag5 = !flag4;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
											if ((nint)0 != 0 && list4._size == 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+20]");
												ListPool<DrawCommand>.Free((List<DrawCommand>)0);
												_ = 0;
												flag5 = false;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+1A]");
											if ((nint)0 != 0 && !flag5)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ stack_18_v11 (System.Int32)+10]");
												ShapesMeshPool.Release((Mesh)0);
												int activeMeshCount = DisposableMesh.activeMeshCount - 1;
												DisposableMesh.activeMeshCount = activeMeshCount;
												_ = 0;
											}
										}
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							enumerator3.Dispose();
							List<DisposableMesh> list5 = cachedMeshes;
							if (cachedMeshes != null)
							{
								int version2 = list5._version + 1;
								list5._version = version2;
								((List<DisposableMesh>.Enumerator*)null)->Dispose();
								object obj5 = default(object);
								if (obj5 == null)
								{
									list5._size = 0;
									return;
								}
								list5._size = 0;
								if (list5._size > 0)
								{
									Array.Clear(list5._items, 0, list5._size);
								}
								return;
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void Dispose()
	{
		//IL_0053: Expected O, but got Ref
		if (IMDrawer.metaMpbPrevious != null)
		{
			MetaMpb metaMpbPrevious = IMDrawer.metaMpbPrevious;
			if (metaMpbPrevious.initialized)
			{
				ShapeDrawCall shapeDrawCall = IMDrawer.metaMpbPrevious.ExtractDrawCall();
				object obj = default(object);
				drawCalls.Add((ShapeDrawCall)(&obj));
			}
		}
		if (hasValidCamera)
		{
			List<DrawCommand> list = default(List<DrawCommand>);
			if (!cBuffersRendering.TryGetValue(cam, out var value))
			{
				list = new List<DrawCommand>();
				cBuffersRendering.Add(cam, list);
				value = list;
			}
			list.Add(this);
		}
		int num = drawCommandWriteNestLevel - 1;
		drawCommandWriteNestLevel = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
		if (pushPopState)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1810687A0");
		}
	}

	public DrawCommand()
	{
		List<int> list = new List<int>();
		cachedTextIds = list;
		List<UnityEngine.Object> list2 = new List<UnityEngine.Object>();
		cachedAssets = list2;
		List<DisposableMesh> list3 = new List<DisposableMesh>();
		cachedMeshes = list3;
		List<ShapeDrawCall> list4 = new List<ShapeDrawCall>();
		drawCalls = list4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
