using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass15_0
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CRecordFrameDelayed_003Eb__0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public _003C_003Ec__DisplayClass15_0 _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private MainThreadAwaitable _003C_003Eu__2;

			private unsafe void MoveNext()
			{
				//IL_007d: Invalid comparison between I4 and F4
				//IL_008f: Expected F4, but got I4
				//IL_0010: Expected O, but got I4
				//IL_001f: Expected I4, but got I8
				//IL_005b: Expected O, but got I4
				//IL_006a: Expected I4, but got I8
				//IL_048b: Expected I, but got O
				//IL_03a8: Expected I4, but got I8
				//IL_03b8: Expected O, but got Ref
				//IL_03ec: Expected O, but got Ref
				//IL_0243: Expected I4, but got F8
				//IL_0424: Expected O, but got Ref
				_003C_003Ec__DisplayClass15_0 obj = _003C_003E4__this;
				TaskAwaiter taskAwaiter = default(TaskAwaiter);
				if (_003C_003E1__state == 0)
				{
					_003C_003Eu__1 = (TaskAwaiter)0;
					_003C_003E1__state = -1;
					taskAwaiter = _003C_003Eu__1;
					goto IL_0293;
				}
				if (_003C_003E1__state == 1)
				{
					_003C_003Eu__2 = (MainThreadAwaitable)0;
					_003C_003E1__state = -1;
					goto IL_02e1;
				}
				bool flag = !(0f < obj.delaySeconds);
				float num = 0f;
				if (!flag)
				{
					num = obj.delaySeconds;
				}
				float num2 = num * 1000f;
				nint num3 = (nint)typeof(Math);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rcx_v26 (Il2CppClass<System.Math>)+E4]");
				double num4;
				int num6 = default(int);
				int millisecondsDelay;
				if ((nint)0 >= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm1,xmm0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180595EFFh\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rcx_v26 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm6\"");
						num4 = Math.Floor(0.5);
						goto IL_023b;
					}
					int num5 = num6 & 1;
					bool flag2 = num5 == 0;
					millisecondsDelay = num6;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [182206E88h]\"");
						millisecondsDelay = num6;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180595F37h\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rcx_v26 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [182206D18h]\"");
						num4 = Math.Ceiling(num2);
						goto IL_023b;
					}
					int num7 = num6 & 1;
					bool flag3 = num7 == 0;
					millisecondsDelay = num6;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [182206E88h]\"");
						millisecondsDelay = num6;
					}
				}
				goto IL_0248;
				IL_02e1:
				ReplayManager replayManager = obj._003C_003E4__this;
				if (obj.session == replayManager.frameSession && replayManager.CanRecordFrame())
				{
					ReplayManager replayManager2 = obj._003C_003E4__this;
					byte[] item = CaptureToBytes(replayManager2.RenderCam, replayManager2.cameraOutput, replayManager2.destinationTexture);
					replayManager2.frames.Add(item);
				}
				_003C_003E1__state = -2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetResult();
				return;
				IL_0293:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
				MainThreadAwaitable mainThreadAwaitable = Awaitable.MainThreadAsync();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
				MainThreadAwaitable awaiter = default(MainThreadAwaitable);
				if (awaiter.IsCompleted)
				{
					goto IL_02e1;
				}
				_003C_003E1__state = 1;
				_003C_003Eu__2 = awaiter;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->AwaitOnCompleted(ref awaiter, ref this);
				return;
				IL_0248:
				Task task = Task.Delay(millisecondsDelay);
				TaskAwaiter awaiter2 = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj2 = default(object);
				if (obj2 != null)
				{
					goto IL_0293;
				}
				_003C_003E1__state = 0;
				_003C_003Eu__1 = taskAwaiter;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
				return;
				IL_023b:
				millisecondsDelay = (int)num4;
				goto IL_0248;
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//IL_0010: Expected O, but got Ref
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public float delaySeconds;

		public int session;

		public ReplayManager _003C_003E4__this;

		internal Task _003CRecordFrameDelayed_003Eb__0()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003CRecordFrameDelayed_003Eb__0_003Ed stateMachine = default(_003C_003CRecordFrameDelayed_003Eb__0_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		}
	}

	public static ReplayManager Instance;

	public Camera RenderCam;

	public int RenderCamWidth;

	public int RenderCamHeight;

	private readonly List<byte[]> frames;

	private Texture2D destinationTexture;

	private RenderTexture cameraOutput;

	private int frameSession;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		if (RenderCam != null)
		{
			RenderCam.enabled = false;
			bool flag = default(bool);
			Texture2D texture2D = new Texture2D(RenderCamWidth, RenderCamHeight, TextureFormat.RGB24, flag);
			destinationTexture = texture2D;
			RenderTexture renderTexture = new RenderTexture(RenderCamWidth, RenderCamHeight, 24, flag ? RenderTextureFormat.Depth : RenderTextureFormat.ARGB32);
			renderTexture.antiAliasing = 4;
			cameraOutput = renderTexture;
			RenderCam.targetTexture = cameraOutput;
		}
	}

	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(destinationTexture);
		if (cameraOutput != null)
		{
			cameraOutput.Release();
			UnityEngine.Object.Destroy(cameraOutput);
		}
	}

	public void ClearFrames()
	{
		int num = frameSession + 1;
		frameSession = num;
		List<byte[]> list = frames;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			list._size = 0;
			return;
		}
		list._size = 0;
		if (list._size > 0)
		{
			Array.Clear(list._items, 0, list._size);
		}
	}

	public IReadOnlyList<byte[]> GetCurrentFrames()
	{
		return frames;
	}

	public void RecordFrameImmediate()
	{
		if (CanRecordFrame())
		{
			byte[] item = ((!CanRecordFrame()) ? null : CaptureToBytes(RenderCam, cameraOutput, destinationTexture));
			frames.Add(item);
		}
	}

	public byte[] CaptureFrameBytes()
	{
		if (CanRecordFrame())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 24 Invalid \"Jump target not found in method: 0x18058AB80\"");
		}
		return null;
	}

	public void RecordFrameDelayed(float delaySeconds = 3f)
	{
		_003C_003Ec__DisplayClass15_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass15_0();
		CS_0024_003C_003E8__locals3.delaySeconds = delaySeconds;
		CS_0024_003C_003E8__locals3._003C_003E4__this = this;
		if (CanRecordFrame())
		{
			CS_0024_003C_003E8__locals3.session = frameSession;
			Func<Task> function = delegate
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
				_003C_003Ec__DisplayClass15_0._003C_003CRecordFrameDelayed_003Eb__0_003Ed stateMachine = default(_003C_003Ec__DisplayClass15_0._003C_003CRecordFrameDelayed_003Eb__0_003Ed);
				asyncTaskMethodBuilder.Start(ref stateMachine);
				return asyncTaskMethodBuilder.Task;
			};
			Task task = Task.Run(function);
		}
	}

	public byte[] CreateFrameZip(string extension = "jpg")
	{
		IReadOnlyList<byte[]> readOnlyList = frames;
		if (frames != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v2 (System.Collections.Generic.IReadOnlyList`1<System.Byte[]>)+18]");
			if ((nint)0 > (nint)0)
			{
				return CreateFrameZip(frames, extension);
			}
			return null;
		}
		return (byte[])(object)new NullReferenceException();
	}

	private bool CanRecordFrame()
	{
		if (RenderCam != null && cameraOutput != null && destinationTexture != null)
		{
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			bool flag = (object)MissionManager._003CInstance_003Ek__BackingField == null;
			UnityEngine.Object obj = MissionManager._003CInstance_003Ek__BackingField;
			if (!flag)
			{
				obj = missionManager._003CCurrentMission_003Ek__BackingField;
			}
			return obj != null;
		}
		return false;
	}

	public unsafe static byte[] CaptureToBytes(Camera cam, RenderTexture target, Texture2D targetTexture)
	{
		//IL_0090: Expected O, but got Ref
		RenderTexture active = RenderTexture.GetActive();
		RenderTexture.SetActive(target);
		if ((object)cam != null)
		{
			cam.Render();
			if ((object)target != null)
			{
				int width = target.width;
				int height = target.height;
				object obj = default(object);
				targetTexture.ReadPixels((Rect)(&obj), 0, 0);
				targetTexture.Apply();
				byte[] result = ImageConversion.EncodeToJPG(targetTexture, 60);
				RenderTexture active2 = default(RenderTexture);
				RenderTexture.SetActive(active2);
				return result;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public unsafe static byte[] CreateFrameZip(IReadOnlyList<byte[]> replayFrames, string extension = "jpg")
	{
		//IL_0024: Expected O, but got Ref
		//IL_003c: Expected O, but got I4
		//IL_005a: Expected I, but got O
		//IL_00e5: Expected O, but got I4
		//IL_0092: Expected O, but got I
		//IL_009b: Expected O, but got I4
		//IL_021f: Expected O, but got I
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_0298: Expected I, but got O
		//IL_02aa: Expected O, but got Ref
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		MemoryStream memoryStream = new MemoryStream();
		Stream stream = default(Stream);
		ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true);
		zipArchive._002Ector(stream, ZipArchiveMode.Create, leaveOpen: true);
		ZipArchive zipArchive2 = default(ZipArchive);
		object obj = (object)(&zipArchive2);
		bool flag = replayFrames == null;
		object obj2 = 0;
		string arg = extension;
		if (!flag)
		{
			object obj11 = default(object);
			object arg2 = default(object);
			object obj12 = default(object);
			object obj13 = default(object);
			byte[] result = default(byte[]);
			object obj15 = default(object);
			while (true)
			{
				nint num = (nint)replayFrames;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ r10_v3 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00d2;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ r10_v3 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+B0]");
				object obj3 = 0;
				object obj4 = 0;
				while (true)
				{
					object obj5 = obj4 + obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r8_v6+v218 @ rax_v47*8]");
					if (0 == (nint)typeof(IReadOnlyCollection<byte[]>))
					{
						break;
					}
					obj4++;
					object obj6 = obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ r10_v3 (Il2CppClass<System.Collections.Generic.IReadOnlyList`1<System.Byte[]>>)+12E]");
					if ((nint)obj6 < 0)
					{
						continue;
					}
					goto IL_00d2;
				}
				object obj7 = obj4 + obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r8_v6+8+v276 @ rcx_v32*8]");
				object obj8 = (nint)0 << 4;
				object obj9 = obj8 + 312;
				object obj10 = obj9 + num;
				goto IL_036c;
				IL_036c:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v282 @ rdx_v6] (should have been resolved before IL gen)");
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string entryName = $"frame_{arg2:000}.{arg}";
					if (zipArchive2 != null)
					{
						ZipArchiveEntry zipArchiveEntry = zipArchive2.CreateEntry(entryName, CompressionLevel.NoCompression);
						if (zipArchiveEntry != null)
						{
							Stream stream2 = zipArchiveEntry.Open();
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
							if (obj12 == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800073E0");
							if (obj13 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							}
							obj2++;
							arg = extension;
							continue;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					obj3 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				if (stream != null)
				{
					nint num2 = (nint)stream;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v411 @ rdx_v10 (Il2CppClass<System.IO.Compression.ZipArchive>)+3E8] (should have been resolved before IL gen)");
					object obj14 = (object)(&stream);
					if (obj14 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					return result;
				}
				throw new NullReferenceException();
				IL_00d2:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 0;
				obj10 = obj15;
				goto IL_036c;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public ReplayManager()
	{
		List<byte[]> list = new List<byte[]>();
		frames = list;
		base._002Ector();
	}
}
