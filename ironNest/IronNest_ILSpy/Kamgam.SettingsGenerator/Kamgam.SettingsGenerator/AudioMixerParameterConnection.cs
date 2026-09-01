using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator;

public class AudioMixerParameterConnection : Connection<float>
{
	[StructLayout((LayoutKind)3)]
	private struct _003CsetOneFrameLater_003Ed__6 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public AudioMixerParameterConnection _003C_003E4__this;

		public float value;

		private TaskAwaiter _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_00cc: Expected I4, but got I8
			//IL_00d7: Expected O, but got Ref
			//IL_0106: Expected O, but got Ref
			AudioMixerParameterConnection audioMixerParameterConnection = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				TaskAwaiter taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				Task task = Task.Delay(10);
				TaskAwaiter awaiter = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj = default(object);
				if (obj == null)
				{
					_003C_003E1__state = 0;
					TaskAwaiter taskAwaiter = default(TaskAwaiter);
					_003C_003Eu__1 = taskAwaiter;
					AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			bool flag = audioMixerParameterConnection.Mixer.SetFloat(audioMixerParameterConnection.ExposedParameterName, value);
			audioMixerParameterConnection._scheduledDelayedSet = false;
			_003C_003E1__state = -2;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_000b: Expected O, but got Ref
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public AudioMixer Mixer;

	public string ExposedParameterName;

	protected bool _scheduledDelayedSet;

	public AudioMixerParameterConnection(AudioMixer mixer, string exposedParameterName)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		Mixer = mixer;
		ExposedParameterName = exposedParameterName;
	}

	public override float Get()
	{
		//IL_002d: Expected F4, but got I4
		if (!Mixer.GetFloat(ExposedParameterName, out var value))
		{
			return 0f;
		}
		return value;
	}

	public override void Set(float value)
	{
		bool flag = Mixer.SetFloat(ExposedParameterName, value);
		int frameCount = Time.frameCount;
		if (frameCount < 1 && !_scheduledDelayedSet)
		{
			_scheduledDelayedSet = true;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
			AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
			_003CsetOneFrameLater_003Ed__6 stateMachine = default(_003CsetOneFrameLater_003Ed__6);
			asyncVoidMethodBuilder2.Start(ref stateMachine);
		}
	}

	protected void setOneFrameLater(float value)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CsetOneFrameLater_003Ed__6 stateMachine = default(_003CsetOneFrameLater_003Ed__6);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}
}
