using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class MonitorConnection : ConnectionWithOptions<string>, IConnectionWithSettingsAccess
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CwaitForMonitorSwitchToComplete_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public MonitorConnection _003C_003E4__this;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

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

		public static bool AllowMonitorChangeOnMobile;

		public static bool ForceMonitorUpdate;

		public static int FramesToWaitAfterMonitorSwitch;

		public bool RefreshResolversAfterCompletion;

		public bool TryToPreserveResolutionOnMonitorChange;

		protected Resolution? _resolutionBeforeMonitorChange;

		protected Vector2Int _windowSizeBeforeMonitorChange;

		protected List<DisplayInfo> _values;

		protected List<string> _labels;

		protected int _lastKnownMonitorIndex;

		protected int _lastSetFrame;

		protected AsyncOperation _moveOperation;

		protected bool _moveOperationFailed;

		private static List<SettingOption> s_tmpOptionSettingsList;

		private static List<ResolutionConnection> s_tmpResolutionConnections;

		private static ResolutionConnection s_tmpResolutionConnection;

		protected Settings _settings;

		public event Action OnComplete
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		protected List<DisplayInfo> getDisplayInfos()
		{
			return null;
		}

		public override List<string> GetOptionLabels()
		{
			return null;
		}

		public override void RefreshOptionLabels()
		{
		}

		public override void SetOptionLabels(List<string> optionLabels)
		{
		}

		public override int Get()
		{
			return 0;
		}

		public override void Set(int index)
		{
		}

		private void moveToMonitor(int index)
		{
		}

		[AsyncStateMachine(typeof(_003CwaitForMonitorSwitchToComplete_003Ed__26))]
		private void waitForMonitorSwitchToComplete()
		{
		}

		protected void updateResolutionConnectionToClosetsResolution(ResolutionConnection connection, Settings settings, int width, int height, int refreshRate)
		{
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
