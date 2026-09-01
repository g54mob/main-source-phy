using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Audio;

namespace Kamgam.SettingsGenerator
{
	public class AudioMixerParameterConnection : Connection<float>
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CsetOneFrameLater_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public AudioMixerParameterConnection _003C_003E4__this;

			public float value;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
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
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float value)
		{
		}

		[AsyncStateMachine(typeof(_003CsetOneFrameLater_003Ed__6))]
		protected void setOneFrameLater(float value)
		{
		}
	}
}
