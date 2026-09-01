using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MicrophoneConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CstartStopPolling_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public MicrophoneConnection _003C_003E4__this;

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

		protected float _pollIntervalInSec;

		protected List<string> _values;

		protected List<string> _labels;

		protected Settings _settings;

		protected int _selectedDeviceIndex;

		public float PollIntervalInSec
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[AsyncStateMachine(typeof(_003CstartStopPolling_003Ed__4))]
		private void startStopPolling()
		{
		}

		private void onDeviceListChanged()
		{
		}

		public MicrophoneConnection(float pollIntervalInSec = -1f)
		{
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public override void RefreshOptionLabels()
		{
		}

		protected List<string> getDeviceNames()
		{
			return null;
		}

		public override int Get()
		{
			return 0;
		}

		public override void Set(int index)
		{
		}

		public string GetSelectedDeviceName()
		{
			return null;
		}

		public AudioClip StartRecording(bool loop, int lengthSec, int frequency)
		{
			return null;
		}

		public void EndRecording()
		{
		}

		public bool IsRecording()
		{
			return false;
		}

		public int GetPosition()
		{
			return 0;
		}

		public void GetDeviceCaps(out int minFreq, out int maxFreq)
		{
			minFreq = default(int);
			maxFreq = default(int);
		}

		public void SetSettings(Settings settings)
		{
		}

		public Settings GetSettings()
		{
			return null;
		}
	}
}
