using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
	public enum GamePhase
	{
		MainMenu,
		BrowsingMap,
		MissionActive
	}

	[Serializable]
	public class MissionState
	{
		public bool Complete;

		public bool Failed;

		public Dictionary<string, int> Medals;

		public float StartTime;

		public float CompleteTime;

		public MedalTrackedValues TrackingValues;

		public MissionState()
		{
			Dictionary<string, int> medals = new Dictionary<string, int>();
			Medals = medals;
			MedalTrackedValues medalTrackedValues = new MedalTrackedValues();
			List<MedalTrackedValues.Data_KilledEntity> data_KilledEntities = new List<MedalTrackedValues.Data_KilledEntity>();
			medalTrackedValues.Data_KilledEntities = data_KilledEntities;
			List<MedalTrackedValues.Data_ShellFired> data_ShellsFired = new List<MedalTrackedValues.Data_ShellFired>();
			medalTrackedValues.Data_ShellsFired = data_ShellsFired;
			List<MedalTrackedValues.Data_PunchcardUsed> data_PunchcardsUsed = new List<MedalTrackedValues.Data_PunchcardUsed>();
			medalTrackedValues.Data_PunchcardsUsed = data_PunchcardsUsed;
			Dictionary<string, float> customValues = new Dictionary<string, float>();
			medalTrackedValues.CustomValues = customValues;
			TrackingValues = medalTrackedValues;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CFinishMission_003Eb__61_1_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			private TaskAwaiter _003C_003Eu__1;

			private unsafe void MoveNext()
			{
				//IL_0010: Expected O, but got I4
				//IL_001f: Expected I4, but got I8
				//IL_00a7: Expected I4, but got I8
				//IL_00b7: Expected O, but got Ref
				//IL_00eb: Expected O, but got Ref
				if (_003C_003E1__state == 0)
				{
					_003C_003Eu__1 = (TaskAwaiter)0;
					_003C_003E1__state = -1;
					TaskAwaiter taskAwaiter = _003C_003Eu__1;
				}
				else
				{
					Task task = Task.Delay(1000);
					TaskAwaiter awaiter = task.GetAwaiter();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
					object obj = default(object);
					if (obj == null)
					{
						_003C_003E1__state = 0;
						TaskAwaiter taskAwaiter = default(TaskAwaiter);
						_003C_003Eu__1 = taskAwaiter;
						AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
				UILeaderboard.RefreshAll(force: true);
				_003C_003E1__state = -2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->SetResult();
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

		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Task> _003C_003E9__61_1;

		public static Action _003C_003E9__61_0;

		public static Func<PunchcardDefinitionV2, bool> _003C_003E9__73_0;

		public static Func<PunchcardDefinitionV2, string> _003C_003E9__73_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CFinishMission_003Eb__61_0()
		{
			Func<Task> function = _003C_003E9__61_1;
			if (_003C_003E9__61_1 == null)
			{
				function = (_003C_003E9__61_1 = delegate
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
					_003C_003CFinishMission_003Eb__61_1_003Ed stateMachine = default(_003C_003CFinishMission_003Eb__61_1_003Ed);
					asyncTaskMethodBuilder.Start(ref stateMachine);
					return asyncTaskMethodBuilder.Task;
				});
			}
			Task task = Task.Run(function);
		}

		internal Task _003CFinishMission_003Eb__61_1()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003CFinishMission_003Eb__61_1_003Ed stateMachine = default(_003C_003CFinishMission_003Eb__61_1_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		}

		internal bool _003CSetupMissionPunchcards_003Eb__73_0(PunchcardDefinitionV2 x)
		{
			//IL_0076: Expected I4, but got O
			bool flag = x != null;
			if (!flag)
			{
				return flag;
			}
			if ((object)x != null)
			{
				bool flag2 = string.IsNullOrEmpty(x.ID);
				return (byte)((flag2 ? 1u : 0u) ^ 1u) != 0;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal string _003CSetupMissionPunchcards_003Eb__73_1(PunchcardDefinitionV2 x)
		{
			if ((object)x != null)
			{
				return x.ID;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass56_0
	{
		public MissionManager _003C_003E4__this;

		public string unloadingName;

		internal void _003CUnloadMainMenuIfLoaded_003Eb__0(AsyncOperation _)
		{
			//IL_004e: Expected O, but got I
			//IL_005e: Expected O, but got I
			//IL_006e: Expected O, but got I
			MissionManager missionManager = _003C_003E4__this;
			Action<string> mainMenuUnloaded = missionManager.m_MainMenuUnloaded;
			if (missionManager.m_MainMenuUnloaded != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+18]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+28]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+40]");
				object obj3 = 0;
				string text = unloadingName;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v31 @ rax_v2 (should have been resolved before IL gen)");
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass60_0
	{
		public MissionManager _003C_003E4__this;

		public MissionGraph prevMission;

		public MissionGraph mission;

		internal void _003CLoadMission_003Eb__0(AsyncOperation _)
		{
			MissionManager missionManager = _003C_003E4__this;
			string message = "[MissionManager] Loaded mission scene: " + missionManager._003CCurrentMissionSceneName_003Ek__BackingField;
			Debug.Log(message);
			MissionManager missionManager2 = _003C_003E4__this;
			Action<MissionGraph, MissionGraph> missionChanged = missionManager2.m_MissionChanged;
			if (missionManager2.m_MissionChanged != null)
			{
				MissionManager missionManager3 = _003C_003E4__this;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v202 @ rcx_v7 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+18] (should have been resolved before IL gen)");
			}
			MissionManager missionManager4 = _003C_003E4__this;
			missionManager4._003CCurrentMission_003Ek__BackingField.OnMissionLoaded();
			_003C_003E4__this.SetupMissionPunchcards();
			UnlockableSceneObject.RefreshAll();
			MissionManager missionManager5 = _003C_003E4__this;
			missionManager5._003CCurrentMission_003Ek__BackingField.Run();
			MissionGraph missionGraph = mission;
			AnalyticsManager.Analytics_Mission("MissionStart", missionGraph.MissionID);
		}
	}

	private sealed class _003C_003Ec__DisplayClass61_0
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CFinishMission_003Eb__2_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public _003C_003Ec__DisplayClass61_0 _003C_003E4__this;

			private unsafe void MoveNext()
			{
				//IL_0022: Expected I4, but got I8
				//IL_0032: Expected O, but got Ref
				_003C_003Ec__DisplayClass61_0 obj = _003C_003E4__this;
				LeaderboardManager.PushOperationState(obj.newState);
				_003C_003E1__state = -2;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->SetResult();
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

		public OperationState newState;

		internal Task _003CFinishMission_003Eb__2()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003CFinishMission_003Eb__2_003Ed stateMachine = default(_003C_003CFinishMission_003Eb__2_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		}
	}

	private static MissionManager _003CInstance_003Ek__BackingField;

	private GamePhase _003CCurrentPhase_003Ek__BackingField;

	private Action<GamePhase, GamePhase> m_PhaseChanged;

	public MissionSceneReference mainMenuScene;

	public bool autoLoadMainMenuOnStart = true;

	public bool autoManageMainMenu;

	public GameObject SceneObject_EndOfMission;

	private string _003CCurrentMissionSceneName_003Ek__BackingField;

	private OperationGraph _003CCurrentOperation_003Ek__BackingField;

	private MissionGraph _003CCurrentMission_003Ek__BackingField;

	public DraggableItemGridArea TurretGrid;

	public MissionState CurrentMissionState;

	private string loadedMainMenuScene;

	private Action<MissionGraph, MissionGraph> m_MissionChanging;

	private Action<MissionGraph, MissionGraph> m_MissionChanged;

	private Action<string> m_MainMenuLoading;

	private Action<string> m_MainMenuLoaded;

	private Action<string> m_MainMenuUnloading;

	private Action<string> m_MainMenuUnloaded;

	public static MissionManager Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public GamePhase CurrentPhase
	{
		get
		{
			return _003CCurrentPhase_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentPhase_003Ek__BackingField = value;
		}
	}

	public string CurrentMissionSceneName
	{
		get
		{
			return _003CCurrentMissionSceneName_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentMissionSceneName_003Ek__BackingField = value;
		}
	}

	public OperationGraph CurrentOperation
	{
		get
		{
			return _003CCurrentOperation_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentOperation_003Ek__BackingField = value;
		}
	}

	public MissionGraph CurrentMission
	{
		get
		{
			return _003CCurrentMission_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentMission_003Ek__BackingField = value;
		}
	}

	public static string BasePath
	{
		get
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			return Path.Combine(folderPath, "IronNest/Missions/Exported.dat");
		}
	}

	public event Action<GamePhase, GamePhase> PhaseChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_PhaseChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_PhaseChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<MissionGraph, MissionGraph> MissionChanging
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
			Delegate obj2 = this.m_MissionChanging;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 120;
			Delegate obj2 = this.m_MissionChanging;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<MissionGraph, MissionGraph> MissionChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 128;
			Delegate obj2 = this.m_MissionChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 128;
			Delegate obj2 = this.m_MissionChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<string> MainMenuLoading
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 136;
			Delegate obj2 = this.m_MainMenuLoading;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 136;
			Delegate obj2 = this.m_MainMenuLoading;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<string> MainMenuLoaded
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 144;
			Delegate obj2 = this.m_MainMenuLoaded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 144;
			Delegate obj2 = this.m_MainMenuLoaded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<string> MainMenuUnloading
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 152;
			Delegate obj2 = this.m_MainMenuUnloading;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 152;
			Delegate obj2 = this.m_MainMenuUnloading;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<string> MainMenuUnloaded
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 160;
			Delegate obj2 = this.m_MainMenuUnloaded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 160;
			Delegate obj2 = this.m_MainMenuUnloaded;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (_003CInstance_003Ek__BackingField != null && _003CInstance_003Ek__BackingField != this)
		{
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		_003CInstance_003Ek__BackingField = this;
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		if (MutatorRuntime._003CInstance_003Ek__BackingField == null)
		{
			GameObject gameObject = new GameObject("MutatorRuntime");
			MutatorRuntime mutatorRuntime = gameObject.AddComponent<MutatorRuntime>();
		}
	}

	private void Start()
	{
		if ((object)MutatorRuntime._003CInstance_003Ek__BackingField != null)
		{
			MutatorRuntime._003CInstance_003Ek__BackingField.ClearActiveMutators();
		}
		ShaderVariantCollection shaderVariantCollection = Resources.Load<ShaderVariantCollection>("ShaderWarmup");
		if (!(shaderVariantCollection != null))
		{
			Debug.LogWarning("[MissionManager] No ShaderVariantCollection found at Resources/ShaderWarmup. Shader compilation may stall on first mission load. Create one via Edit > Project Settings > Graphics > Shader Preloading > Save to asset...");
		}
		else
		{
			shaderVariantCollection.WarmUp();
			Debug.Log("[MissionManager] Shader warmup complete.");
		}
		if (autoLoadMainMenuOnStart && mainMenuScene != null)
		{
			MissionSceneReference missionSceneReference = mainMenuScene;
			if (!string.IsNullOrEmpty(missionSceneReference.sceneName))
			{
				LoadMainMenu();
				return;
			}
		}
		Debug.Log("[MissionManager] Idle. No Main Menu, no default operation, and no Missions configured.");
	}

	private void Update()
	{
		//IL_020e: Expected I, but got O
		//IL_021e: Expected O, but got I
		//IL_022e: Expected O, but got I
		while (!(_003CCurrentMission_003Ek__BackingField == null))
		{
			MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
			nint num = (nint)missionGraph;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v108 @ rdx_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+208]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v108 @ rdx_v4 (Il2CppClass<SleepyNodes.MissionGraph>)+210]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v109 @ rax_v8 (should have been resolved before IL gen)");
		}
		KeyControl f3Key = Keyboard._003Ccurrent_003Ek__BackingField.f3Key;
		if (f3Key.wasPressedThisFrame)
		{
			string basePath = BasePath;
			if (File.Exists(basePath))
			{
				string basePath2 = BasePath;
				string basePath3 = BasePath;
				FileInfo fileInfo = new FileInfo(basePath3);
				DateTime lastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
				object obj3 = default(object);
				object arg = (DateTime)obj3;
				string message = $"Loading Dynamic Mission | From: {basePath2} | {arg}";
				Debug.Log(message);
				string basePath4 = BasePath;
				string json = File.ReadAllText(basePath4);
				bool inlineLoc = default(bool);
				bool importPunchcards = default(bool);
				bool importZones = default(bool);
				MissionGraph missionGraph2 = MissionImporter.ImportMission(json, null, updateMissionData: true, isPerminant: false, inlineLoc, importPunchcards, importZones);
				missionGraph2.MissionType = MissionGraph.MissionTypes.Tutorial;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message2 = $"Loading Dynamic Mission | Mission File Loaded | {missionGraph2.MissionName} - {missionGraph2.MissionID} | Nodes: {arg2}";
				Debug.Log(message2);
				OperationGraph operationGraph = ScriptableObject.CreateInstance<OperationGraph>();
				MissionNode missionNode = operationGraph.AddNode<MissionNode>();
				missionNode.Mission = missionGraph2;
				operationGraph.OperationID = "DynamicImport";
				Debug.Log("Loading Dynamic Mission | Operation Created | ");
				StartOperation(operationGraph, missionGraph2);
			}
		}
	}

	public void LoadMainMenu()
	{
		UnloadCurrentMissionSceneIfAny();
		if ((object)MutatorRuntime._003CInstance_003Ek__BackingField != null)
		{
			MutatorRuntime._003CInstance_003Ek__BackingField.ClearActiveMutators();
		}
		if (mainMenuScene != null)
		{
			MissionSceneReference missionSceneReference = mainMenuScene;
			if (!string.IsNullOrEmpty(missionSceneReference.sceneName))
			{
				MissionSceneReference missionSceneReference2 = mainMenuScene;
				Scene sceneByName = SceneManager.GetSceneByName(missionSceneReference2.sceneName);
				Scene scene = default(Scene);
				if (scene.IsValid() && scene.isLoaded)
				{
					MissionSceneReference missionSceneReference3 = mainMenuScene;
					loadedMainMenuScene = missionSceneReference3.sceneName;
					string message = "[MissionManager] Main Menu already loaded: " + loadedMainMenuScene;
					Debug.Log(message);
					return;
				}
				Action<string> mainMenuLoading = this.m_MainMenuLoading;
				if (this.m_MainMenuLoading != null)
				{
					MissionSceneReference missionSceneReference4 = mainMenuScene;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v180 @ rcx_v16 (System.Action`1<System.String>)+18] (should have been resolved before IL gen)");
				}
				MissionSceneReference missionSceneReference5 = mainMenuScene;
				AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(missionSceneReference5.sceneName, LoadSceneMode.Additive);
				if (asyncOperation != null)
				{
					Action<AsyncOperation> value = delegate
					{
						//IL_0097: Expected O, but got I
						//IL_00a7: Expected O, but got I
						//IL_00b7: Expected O, but got I
						MissionSceneReference missionSceneReference7 = mainMenuScene;
						loadedMainMenuScene = missionSceneReference7.sceneName;
						_003CCurrentOperation_003Ek__BackingField = null;
						_003CCurrentMission_003Ek__BackingField = null;
						string message3 = "[MissionManager] Loaded Main Menu: " + loadedMainMenuScene;
						Debug.Log(message3);
						SetPhase(GamePhase.MainMenu);
						Action<string> mainMenuLoaded = this.m_MainMenuLoaded;
						if (this.m_MainMenuLoaded != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+18]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+28]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+40]");
							object obj3 = 0;
							string text = loadedMainMenuScene;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v61 @ rax_v10 (should have been resolved before IL gen)");
						}
					};
					asyncOperation.completed += value;
				}
				else
				{
					MissionSceneReference missionSceneReference6 = mainMenuScene;
					string message2 = "[MissionManager] Failed to start async load for Main Menu: " + missionSceneReference6.sceneName;
					Debug.LogError(message2);
				}
				return;
			}
		}
		Debug.LogWarning("[MissionManager] Main Menu scene is not configured.");
	}

	private void UnloadMainMenuIfLoaded()
	{
		_003C_003Ec__DisplayClass56_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass56_0();
		CS_0024_003C_003E8__locals6._003C_003E4__this = this;
		if (string.IsNullOrEmpty(loadedMainMenuScene))
		{
			return;
		}
		Scene sceneByName = SceneManager.GetSceneByName(loadedMainMenuScene);
		Scene scene = default(Scene);
		if (scene.IsValid() && scene.isLoaded)
		{
			CS_0024_003C_003E8__locals6.unloadingName = loadedMainMenuScene;
			Action<string> mainMenuUnloading = this.m_MainMenuUnloading;
			if (this.m_MainMenuUnloading != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v215 @ rcx_v14 (System.Action`1<System.String>)+18] (should have been resolved before IL gen)");
			}
			AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(CS_0024_003C_003E8__locals6.unloadingName);
			string message = "[MissionManager] Unloading Main Menu: " + CS_0024_003C_003E8__locals6.unloadingName;
			Debug.Log(message);
			loadedMainMenuScene = null;
			if (asyncOperation == null)
			{
				return;
			}
			Action<AsyncOperation> value = delegate
			{
				//IL_004e: Expected O, but got I
				//IL_005e: Expected O, but got I
				//IL_006e: Expected O, but got I
				MissionManager missionManager = CS_0024_003C_003E8__locals6._003C_003E4__this;
				Action<string> mainMenuUnloaded = missionManager.m_MainMenuUnloaded;
				if (missionManager.m_MainMenuUnloaded != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+18]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+28]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rcx_v2 (System.Action`1<System.String>)+40]");
					object obj3 = 0;
					string unloadingName = CS_0024_003C_003E8__locals6.unloadingName;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v31 @ rax_v2 (should have been resolved before IL gen)");
				}
			};
			asyncOperation.completed += value;
		}
		else
		{
			loadedMainMenuScene = null;
		}
	}

	public void StartOperation(OperationGraph operation, MissionGraph mission)
	{
		if (operation != null)
		{
			List<MissionNode> missions = operation.Missions;
			if (missions != null)
			{
				List<MissionNode> missions2 = operation.Missions;
				if (missions2._size != 0)
				{
					_003CCurrentOperation_003Ek__BackingField = operation;
					ProgressionManager._003CInstance_003Ek__BackingField.StartOperation(operation);
					if ((object)MutatorRuntime._003CInstance_003Ek__BackingField != null)
					{
						MutatorRuntime._003CInstance_003Ek__BackingField.SetActiveMutators(mission.mutators);
					}
					if (autoManageMainMenu)
					{
						UnloadMainMenuIfLoaded();
					}
					SetPhase(GamePhase.MissionActive);
					LoadMission(mission);
					return;
				}
			}
		}
		Debug.LogError("[MissionManager] Cannot start operation: null or empty.");
	}

	public void EndOperationAndReturnToMenu()
	{
		UnloadCurrentMissionSceneIfAny();
		if (_003CCurrentMission_003Ek__BackingField != null)
		{
			_003CCurrentMission_003Ek__BackingField.OnMissionUnloaded();
			_003CCurrentMission_003Ek__BackingField = null;
		}
		_003CCurrentOperation_003Ek__BackingField = null;
		if ((object)MutatorRuntime._003CInstance_003Ek__BackingField != null)
		{
			MutatorRuntime._003CInstance_003Ek__BackingField.ClearActiveMutators();
		}
		if (autoManageMainMenu)
		{
			LoadMainMenu();
		}
	}

	public void EnterBrowsingMap()
	{
		if (autoManageMainMenu)
		{
			UnloadMainMenuIfLoaded();
		}
		SetPhase(GamePhase.BrowsingMap);
	}

	private unsafe void LoadMission(MissionGraph mission, bool forceReload = false)
	{
		//IL_08b5: Expected O, but got Ref
		//IL_03c0: Expected O, but got Ref
		//IL_0387: Expected O, but got I
		//IL_061f: Expected O, but got I
		//IL_062f: Expected O, but got I
		//IL_0701: Expected I, but got O
		_003C_003Ec__DisplayClass60_0 CS_0024_003C_003E8__locals15 = new _003C_003Ec__DisplayClass60_0();
		bool flag = CS_0024_003C_003E8__locals15 == null;
		_003C_003Ec__DisplayClass60_0 obj = CS_0024_003C_003E8__locals15;
		if (flag)
		{
			goto IL_07b6;
		}
		CS_0024_003C_003E8__locals15._003C_003E4__this = this;
		CS_0024_003C_003E8__locals15.mission = mission;
		object message;
		if (CS_0024_003C_003E8__locals15.mission != null)
		{
			if (!(_003CCurrentMission_003Ek__BackingField == CS_0024_003C_003E8__locals15.mission) || forceReload)
			{
				CS_0024_003C_003E8__locals15.prevMission = _003CCurrentMission_003Ek__BackingField;
				if (_003CCurrentMission_003Ek__BackingField != null)
				{
					obj = (_003C_003Ec__DisplayClass60_0)(object)_003CCurrentMission_003Ek__BackingField;
					if ((object)_003CCurrentMission_003Ek__BackingField == null)
					{
						goto IL_07b6;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
					_003CCurrentMission_003Ek__BackingField = null;
				}
				_003CCurrentMission_003Ek__BackingField = CS_0024_003C_003E8__locals15.mission;
				MissionState missionState = new MissionState();
				Dictionary<string, int> medals = new Dictionary<string, int>();
				missionState.Medals = medals;
				MedalTrackedValues medalTrackedValues = new MedalTrackedValues();
				List<MedalTrackedValues.Data_KilledEntity> data_KilledEntities = new List<MedalTrackedValues.Data_KilledEntity>();
				medalTrackedValues.Data_KilledEntities = data_KilledEntities;
				List<MedalTrackedValues.Data_ShellFired> data_ShellsFired = new List<MedalTrackedValues.Data_ShellFired>();
				medalTrackedValues.Data_ShellsFired = data_ShellsFired;
				List<MedalTrackedValues.Data_PunchcardUsed> data_PunchcardsUsed = new List<MedalTrackedValues.Data_PunchcardUsed>();
				medalTrackedValues.Data_PunchcardsUsed = data_PunchcardsUsed;
				Dictionary<string, float> customValues = new Dictionary<string, float>();
				medalTrackedValues.CustomValues = customValues;
				missionState.TrackingValues = medalTrackedValues;
				missionState._002Ector();
				float time = Time.time;
				missionState.StartTime = time;
				CurrentMissionState = missionState;
				if ((object)ReplayManager.Instance != null)
				{
					ReplayManager.Instance.ClearFrames();
				}
				MissionState currentMissionState = CurrentMissionState;
				bool flag2 = CurrentMissionState == null;
				obj = (_003C_003Ec__DisplayClass60_0)(object)ReplayManager.Instance;
				if (!flag2)
				{
					obj = (_003C_003Ec__DisplayClass60_0)(object)currentMissionState.TrackingValues;
					if (currentMissionState.TrackingValues != null)
					{
						_ = currentMissionState.StartTime;
						MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
						if ((object)_003CCurrentMission_003Ek__BackingField != null)
						{
							obj = (_003C_003Ec__DisplayClass60_0)(object)missionGraph.Medals;
							if (missionGraph.Medals != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								List<MedalCategoryDefinition>.Enumerator enumerator = default(List<MedalCategoryDefinition>.Enumerator);
								UnityEngine.Object obj2 = default(UnityEngine.Object);
								object obj3 = default(object);
								while (enumerator.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (obj2 != null)
									{
										MissionState currentMissionState2 = CurrentMissionState;
										if (CurrentMissionState == null)
										{
											throw new NullReferenceException();
										}
										if ((object)obj2 == null)
										{
											throw new NullReferenceException();
										}
										if (currentMissionState2.Medals == null)
										{
											throw new NullReferenceException();
										}
										Dictionary<string, int> medals2 = currentMissionState2.Medals;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ stack_-50_v8 (UnityEngine.Object)+18]");
										medals2.set_Item((string)0, (int)(&obj3));
										nint num = 0;
									}
								}
								enumerator.Dispose();
								MissionGraph missionGraph2 = _003CCurrentMission_003Ek__BackingField;
								bool flag3 = (object)_003CCurrentMission_003Ek__BackingField == null;
								obj = (_003C_003Ec__DisplayClass60_0)(&enumerator);
								if (!flag3)
								{
									bool flag4 = missionGraph2.MissionType != MissionGraph.MissionTypes.Campaign;
									obj = (_003C_003Ec__DisplayClass60_0)(&enumerator);
									if (flag4)
									{
										goto IL_044b;
									}
									OperationGraph operationGraph = _003CCurrentOperation_003Ek__BackingField;
									bool flag5 = (object)_003CCurrentOperation_003Ek__BackingField == null;
									obj = (_003C_003Ec__DisplayClass60_0)(object)ProgressionManager._003CInstance_003Ek__BackingField;
									if (!flag5)
									{
										bool flag6 = (object)ProgressionManager._003CInstance_003Ek__BackingField == null;
										obj = (_003C_003Ec__DisplayClass60_0)(object)ProgressionManager._003CInstance_003Ek__BackingField;
										if (!flag6)
										{
											OperationState operation = ProgressionManager._003CInstance_003Ek__BackingField.GetOperation(operationGraph.OperationID);
											bool flag7 = operation == null;
											obj = (_003C_003Ec__DisplayClass60_0)(object)ProgressionManager._003CInstance_003Ek__BackingField;
											if (!flag7)
											{
												LoadOperationState(operation);
												obj = (_003C_003Ec__DisplayClass60_0)(object)this;
											}
											goto IL_044b;
										}
									}
								}
							}
						}
					}
				}
				goto IL_07b6;
			}
			message = "[MissionManager] Mission already loaded.";
		}
		else
		{
			message = "[MissionManager] Mission missing";
		}
		goto IL_0978;
		IL_0978:
		Debug.LogError(message);
		return;
		IL_07b6:
		throw new NullReferenceException();
		IL_0929:
		string text;
		if (string.IsNullOrWhiteSpace(text))
		{
			string message2 = "[MissionManager] Loaded mission scene: " + _003CCurrentMissionSceneName_003Ek__BackingField;
			Debug.Log(message2);
			Action<MissionGraph, MissionGraph> missionChanged = this.m_MissionChanged;
			bool flag8 = this.m_MissionChanged == null;
			MissionGraph missionGraph3 = null;
			if (!flag8)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1197 @ rcx_v74 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+28]");
				nint num = 0;
				missionGraph3 = _003CCurrentMission_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1197 @ rcx_v74 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+18] (should have been resolved before IL gen)");
			}
			bool flag9 = (object)_003CCurrentMission_003Ek__BackingField == null;
			obj = (_003C_003Ec__DisplayClass60_0)(object)_003CCurrentMission_003Ek__BackingField;
			if (!flag9)
			{
				_003CCurrentMission_003Ek__BackingField.OnMissionLoaded();
				SetupMissionPunchcards();
				UnlockableSceneObject.RefreshAll();
				obj = (_003C_003Ec__DisplayClass60_0)(object)_003CCurrentMission_003Ek__BackingField;
				if ((object)_003CCurrentMission_003Ek__BackingField != null)
				{
					nint num2 = (nint)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1229 @ rdx_v53 (Il2CppClass<MissionManager+<>c__DisplayClass60_0>)+1F8] (should have been resolved before IL gen)");
					return;
				}
			}
			goto IL_07b6;
		}
		_003CCurrentMissionSceneName_003Ek__BackingField = text;
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_003CCurrentMissionSceneName_003Ek__BackingField, LoadSceneMode.Additive);
		if (asyncOperation != null)
		{
			Action<AsyncOperation> value = delegate
			{
				MissionManager missionManager = CS_0024_003C_003E8__locals15._003C_003E4__this;
				string message3 = "[MissionManager] Loaded mission scene: " + missionManager._003CCurrentMissionSceneName_003Ek__BackingField;
				Debug.Log(message3);
				MissionManager missionManager2 = CS_0024_003C_003E8__locals15._003C_003E4__this;
				Action<MissionGraph, MissionGraph> missionChanged2 = missionManager2.m_MissionChanged;
				if (missionManager2.m_MissionChanged != null)
				{
					MissionManager missionManager3 = CS_0024_003C_003E8__locals15._003C_003E4__this;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v202 @ rcx_v7 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+18] (should have been resolved before IL gen)");
				}
				MissionManager missionManager4 = CS_0024_003C_003E8__locals15._003C_003E4__this;
				missionManager4._003CCurrentMission_003Ek__BackingField.OnMissionLoaded();
				CS_0024_003C_003E8__locals15._003C_003E4__this.SetupMissionPunchcards();
				UnlockableSceneObject.RefreshAll();
				MissionManager missionManager5 = CS_0024_003C_003E8__locals15._003C_003E4__this;
				missionManager5._003CCurrentMission_003Ek__BackingField.Run();
				MissionGraph mission2 = CS_0024_003C_003E8__locals15.mission;
				AnalyticsManager.Analytics_Mission("MissionStart", mission2.MissionID);
			};
			asyncOperation.completed += value;
			return;
		}
		string text2 = "[MissionManager] Failed to start async load for mission: " + _003CCurrentMissionSceneName_003Ek__BackingField;
		message = text2;
		goto IL_0978;
		IL_044b:
		MissionGraph missionGraph4 = _003CCurrentMission_003Ek__BackingField;
		if ((object)_003CCurrentMission_003Ek__BackingField != null)
		{
			if (missionGraph4.MissionType == MissionGraph.MissionTypes.Challange)
			{
				bool flag10 = (object)LeaderboardManager.Instance == null;
				obj = (_003C_003Ec__DisplayClass60_0)(object)LeaderboardManager.Instance;
				if (flag10)
				{
					goto IL_07b6;
				}
				LeaderboardManager.Instance.Leaderboard_StartRun(Gamemodes.Challange);
				obj = (_003C_003Ec__DisplayClass60_0)(object)LeaderboardManager.Instance;
			}
			MissionGraph missionGraph5 = _003CCurrentMission_003Ek__BackingField;
			if ((object)_003CCurrentMission_003Ek__BackingField != null)
			{
				if (missionGraph5.MissionType == MissionGraph.MissionTypes.Chill)
				{
					bool flag11 = (object)LeaderboardManager.Instance == null;
					obj = (_003C_003Ec__DisplayClass60_0)(object)LeaderboardManager.Instance;
					if (flag11)
					{
						goto IL_07b6;
					}
					LeaderboardManager.Instance.Leaderboard_StartRun(Gamemodes.Chill);
				}
				Action<MissionGraph, MissionGraph> missionChanging = this.m_MissionChanging;
				if (this.m_MissionChanging != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1072 @ rcx_v57 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+28]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1072 @ rcx_v57 (System.Action`2<SleepyNodes.MissionGraph, SleepyNodes.MissionGraph>)+18] (should have been resolved before IL gen)");
				}
				UnloadCurrentMissionSceneIfAny();
				MissionGraph missionGraph6 = _003CCurrentMission_003Ek__BackingField;
				bool flag12 = (object)_003CCurrentMission_003Ek__BackingField == null;
				obj = (_003C_003Ec__DisplayClass60_0)(object)this;
				if (!flag12)
				{
					MissionSceneReference sceneReference = missionGraph6.SceneReference;
					if (missionGraph6.SceneReference != null)
					{
						text = sceneReference.sceneName;
						if (sceneReference.sceneName != null)
						{
							goto IL_0929;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1127 @ rax_v93+B8]");
					object obj5 = 0;
					text = (string)obj5;
					goto IL_0929;
				}
			}
		}
		goto IL_07b6;
	}

	public unsafe void FinishMission()
	{
		//IL_0f53: Expected I, but got O
		//IL_0f69: Expected O, but got I
		//IL_03af: Expected F4, but got I4
		//IL_040d: Expected O, but got I
		//IL_0442: Expected I, but got O
		//IL_0ffc: Expected O, but got Ref
		//IL_04fa: Expected O, but got I
		//IL_0519: Expected O, but got I
		//IL_0a2f: Expected O, but got I
		//IL_0a3d: Expected O, but got Ref
		//IL_0acc: Expected I, but got O
		//IL_05a4: Expected O, but got I
		//IL_05d9: Expected O, but got I
		//IL_0601: Expected O, but got I
		//IL_08f4: Expected O, but got I
		//IL_063e: Expected O, but got I
		//IL_0cb0: Expected O, but got Ref
		//IL_0673: Expected O, but got I
		//IL_0984: Expected O, but got I
		//IL_069b: Expected O, but got I
		//IL_0872: Expected O, but got I
		//IL_06d8: Expected O, but got I
		//IL_070d: Expected O, but got I
		//IL_08cd: Expected O, but got I
		//IL_0735: Expected O, but got I
		//IL_0816: Expected O, but got I
		//IL_077e: Expected O, but got I
		//IL_0845: Expected O, but got I
		//IL_0845: Expected O, but got I
		//IL_07c4: Expected O, but got I
		//IL_07c4: Expected O, but got I
		MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
		bool flag = (object)_003CCurrentMission_003Ek__BackingField == null;
		_003C_003Ec__DisplayClass61_0 obj = (_003C_003Ec__DisplayClass61_0)(object)this;
		_003C_003Ec__DisplayClass61_0 CS_0024_003C_003E8__locals3;
		Dictionary<string, int>.Enumerator enumerator2 = default(Dictionary<string, int>.Enumerator);
		List<MedalCategoryDefinition>.Enumerator enumerator3 = default(List<MedalCategoryDefinition>.Enumerator);
		AchievementUnlockEvent key;
		nint num5;
		object obj3;
		if (!flag)
		{
			if (missionGraph.MissionType == MissionGraph.MissionTypes.Challange || missionGraph.MissionType == MissionGraph.MissionTypes.Chill)
			{
				Action onCompleted = _003C_003Ec._003C_003E9__61_0;
				bool flag2 = _003C_003Ec._003C_003E9__61_0 != null;
				obj = (_003C_003Ec__DisplayClass61_0)(object)typeof(_003C_003Ec);
				if (!flag2)
				{
					Action action = (_003C_003Ec._003C_003E9__61_0 = delegate
					{
						Func<Task> function2 = _003C_003Ec._003C_003E9__61_1;
						if (_003C_003Ec._003C_003E9__61_1 == null)
						{
							function2 = (_003C_003Ec._003C_003E9__61_1 = delegate
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
								AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
								_003C_003Ec._003C_003CFinishMission_003Eb__61_1_003Ed stateMachine = default(_003C_003Ec._003C_003CFinishMission_003Eb__61_1_003Ed);
								asyncTaskMethodBuilder.Start(ref stateMachine);
								return asyncTaskMethodBuilder.Task;
							});
						}
						Task task2 = Task.Run(function2);
					});
					nint num = (nint)typeof(_003C_003Ec);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1030 @ rax_v178 (Il2CppClass<MissionManager+<>c>)+B8]");
					obj = (_003C_003Ec__DisplayClass61_0)((nint)0 + (nint)16);
					onCompleted = action;
				}
				if ((object)LeaderboardManager.Instance == null)
				{
					goto IL_0dfb;
				}
				LeaderboardManager.Instance.Leaderboard_CompleteRun(onCompleted);
			}
			if (CurrentMissionState != null)
			{
				MissionState currentMissionState = CurrentMissionState;
				if (currentMissionState.Complete && _003CCurrentMission_003Ek__BackingField != null)
				{
					CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass61_0();
					MissionGraph missionGraph2 = _003CCurrentMission_003Ek__BackingField;
					bool flag3 = (object)_003CCurrentMission_003Ek__BackingField == null;
					obj = CS_0024_003C_003E8__locals3;
					if (!flag3)
					{
						bool flag4 = missionGraph2.achievementForClearing == AchievementType.None;
						obj = CS_0024_003C_003E8__locals3;
						if (!flag4)
						{
							MissionGraph missionGraph3 = _003CCurrentMission_003Ek__BackingField;
							AchievementUnlockEvent achievementUnlockEvent = new AchievementUnlockEvent(missionGraph3.achievementForClearing);
							bool flag5 = (object)AchievementsManager.Instance == null;
							obj = (_003C_003Ec__DisplayClass61_0)(object)achievementUnlockEvent;
							if (flag5)
							{
								goto IL_0dfb;
							}
							AchievementsManager.Instance.EventManager_OnAchievementUnlocked(achievementUnlockEvent);
							obj = (_003C_003Ec__DisplayClass61_0)(object)AchievementsManager.Instance;
						}
						MissionState currentMissionState2 = CurrentMissionState;
						if (CurrentMissionState != null)
						{
							obj = (_003C_003Ec__DisplayClass61_0)(object)currentMissionState2.TrackingValues;
							if (currentMissionState2.TrackingValues != null)
							{
								_ = currentMissionState2.StartTime;
								MissionState currentMissionState3 = CurrentMissionState;
								if (CurrentMissionState != null)
								{
									obj = (_003C_003Ec__DisplayClass61_0)(object)currentMissionState3.TrackingValues;
									if (currentMissionState3.TrackingValues != null)
									{
										_ = currentMissionState3.CompleteTime;
										MissionState currentMissionState4 = CurrentMissionState;
										if (CurrentMissionState != null)
										{
											MedalTrackedValues trackingValues = currentMissionState4.TrackingValues;
											float time = Time.time;
											bool flag6 = currentMissionState4.TrackingValues == null;
											obj = null;
											if (!flag6)
											{
												trackingValues.MissionEndTime = time;
												MissionState currentMissionState5 = CurrentMissionState;
												bool flag7 = CurrentMissionState == null;
												obj = null;
												if (!flag7)
												{
													float value;
													if (CounterBatteryTimer._003CInstance_003Ek__BackingField != null)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18041E8C0");
														CounterBatteryTimer counterBatteryTimer = default(CounterBatteryTimer);
														bool flag8 = (object)counterBatteryTimer == null;
														obj = null;
														if (flag8)
														{
															goto IL_0dfb;
														}
														value = counterBatteryTimer.TimeRemaining;
														obj = (_003C_003Ec__DisplayClass61_0)(object)counterBatteryTimer;
													}
													else
													{
														value = 0f;
														obj = (_003C_003Ec__DisplayClass61_0)(object)CounterBatteryTimer._003CInstance_003Ek__BackingField;
													}
													if (currentMissionState5.TrackingValues != null)
													{
														currentMissionState5.TrackingValues.SetValue(MedalTrackedValue.CounterBatteryTimeRemaining, value);
														obj = (_003C_003Ec__DisplayClass61_0)(object)_003CCurrentMission_003Ek__BackingField;
														if ((object)_003CCurrentMission_003Ek__BackingField != null)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rcx_v22 (MissionManager+<>c__DisplayClass61_0)+98]");
															obj = (_003C_003Ec__DisplayClass61_0)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rcx_v22 (MissionManager+<>c__DisplayClass61_0)+98]");
															if ((nint)0 != 0)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
																nint num2 = unchecked((nint)null);
																List<MedalCategoryDefinition>.Enumerator enumerator = default(List<MedalCategoryDefinition>.Enumerator);
																UnityEngine.Object obj2 = default(UnityEngine.Object);
																PunchcardRuntime[] array = default(PunchcardRuntime[]);
																while (enumerator.MoveNext())
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
																	if (!(obj2 != null))
																	{
																		continue;
																	}
																	MissionState currentMissionState6 = CurrentMissionState;
																	bool flag9 = CurrentMissionState == null;
																	Dictionary<string, int> dictionary = (Dictionary<string, int>)(object)obj2;
																	if (!flag9)
																	{
																		dictionary = (Dictionary<string, int>)(object)currentMissionState6.TrackingValues;
																		if (currentMissionState6.TrackingValues != null)
																		{
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+38]");
																			bool flag10 = (nint)0 == 0;
																			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+38]");
																			dictionary = (Dictionary<string, int>)0;
																			if (!flag10)
																			{
																				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+38]");
																				Dictionary<string, float>.KeyCollection keys = ((Dictionary<string, float>)0).Keys;
																				string text = string.Join(", ", keys);
																				string text2 = "[MissionManager] Checking Medals | " + text;
																				Debug.LogError(text2);
																				bool flag11 = (object)obj2 == null;
																				dictionary = (Dictionary<string, int>)(object)text2;
																				if (!flag11)
																				{
																					MissionState currentMissionState7 = CurrentMissionState;
																					bool flag12 = CurrentMissionState == null;
																					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+70]");
																					dictionary = (Dictionary<string, int>)0;
																					if (!flag12)
																					{
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+70]");
																						bool flag13 = (nint)0 == 0;
																						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+70]");
																						dictionary = (Dictionary<string, int>)0;
																						if (!flag13)
																						{
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+70]");
																							if (!((MedalConditionSet)0).Resolve(currentMissionState7.TrackingValues))
																							{
																								MissionState currentMissionState8 = CurrentMissionState;
																								bool flag14 = CurrentMissionState == null;
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+60]");
																								dictionary = (Dictionary<string, int>)0;
																								if (flag14)
																								{
																									throw new NullReferenceException();
																								}
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+60]");
																								bool flag15 = (nint)0 == 0;
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+60]");
																								dictionary = (Dictionary<string, int>)0;
																								if (flag15)
																								{
																									throw new NullReferenceException();
																								}
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+60]");
																								bool flag16 = ((MedalConditionSet)0).Resolve(currentMissionState8.TrackingValues);
																								MissionState currentMissionState9 = CurrentMissionState;
																								if (!flag16)
																								{
																									bool flag17 = CurrentMissionState == null;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+50]");
																									dictionary = (Dictionary<string, int>)0;
																									if (flag17)
																									{
																										throw new NullReferenceException();
																									}
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+50]");
																									bool flag18 = (nint)0 == 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+50]");
																									dictionary = (Dictionary<string, int>)0;
																									if (flag18)
																									{
																										throw new NullReferenceException();
																									}
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+50]");
																									bool flag19 = ((MedalConditionSet)0).Resolve(currentMissionState9.TrackingValues);
																									dictionary = (Dictionary<string, int>)(object)CurrentMissionState;
																									if (!flag19)
																									{
																										if (dictionary != null)
																										{
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																											dictionary = (Dictionary<string, int>)0;
																											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																											if ((nint)0 != 0)
																											{
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																												nint num3 = 0;
																												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+18]");
																												((Dictionary<string, int>)num3).set_Item((string)0, (int)(&array));
																												num2 = 0;
																												continue;
																											}
																											throw new NullReferenceException();
																										}
																										throw new NullReferenceException();
																									}
																									if (CurrentMissionState == null)
																									{
																										throw new NullReferenceException();
																									}
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																									bool flag20 = (nint)0 == 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																									dictionary = (Dictionary<string, int>)0;
																									if (flag20)
																									{
																										throw new NullReferenceException();
																									}
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1817 @ rcx_v6 (System.Collections.Generic.Dictionary`2<System.String, System.Int32>)+18]");
																									nint num4 = 0;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+18]");
																									((Dictionary<string, int>)num4).set_Item((string)0, (int)(&array));
																									num2 = 0;
																								}
																								else
																								{
																									bool flag21 = CurrentMissionState == null;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+60]");
																									dictionary = (Dictionary<string, int>)0;
																									if (flag21)
																									{
																										throw new NullReferenceException();
																									}
																									bool flag22 = currentMissionState9.Medals == null;
																									dictionary = currentMissionState9.Medals;
																									if (flag22)
																									{
																										throw new NullReferenceException();
																									}
																									Dictionary<string, int> medals = currentMissionState9.Medals;
																									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+18]");
																									medals.set_Item((string)0, (int)(&array));
																									num2 = 0;
																								}
																							}
																							else
																							{
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+18]");
																								string text3 = "[MissionManager] Checking Medals | " + (string)0 + " -> Gold";
																								Debug.LogError(text3);
																								MissionState currentMissionState10 = CurrentMissionState;
																								bool flag23 = CurrentMissionState == null;
																								dictionary = (Dictionary<string, int>)(object)text3;
																								if (flag23)
																								{
																									throw new NullReferenceException();
																								}
																								bool flag24 = currentMissionState10.Medals == null;
																								dictionary = currentMissionState10.Medals;
																								if (flag24)
																								{
																									throw new NullReferenceException();
																								}
																								Dictionary<string, int> medals2 = currentMissionState10.Medals;
																								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ stack_20_v27 (UnityEngine.Object)+18]");
																								medals2.set_Item((string)0, (int)(&array));
																								num2 = 0;
																							}
																							continue;
																						}
																						throw new NullReferenceException();
																					}
																					throw new NullReferenceException();
																				}
																				throw new NullReferenceException();
																			}
																			throw new NullReferenceException();
																		}
																		throw new NullReferenceException();
																	}
																	throw new NullReferenceException();
																}
																enumerator.Dispose();
																MissionState currentMissionState11 = CurrentMissionState;
																bool flag25 = CurrentMissionState == null;
																obj = (_003C_003Ec__DisplayClass61_0)(&enumerator);
																if (!flag25 && currentMissionState11.Medals != null)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
																	object obj4 = default(object);
																	obj3 = obj4;
																	while (enumerator2.MoveNext())
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
																		bool flag26 = (nint)obj2 == 3;
																		obj3 = enumerator3;
																		if (flag26)
																		{
																			continue;
																		}
																		goto IL_0a17;
																	}
																	enumerator2.Dispose();
																	MissionGraph missionGraph4 = _003CCurrentMission_003Ek__BackingField;
																	if ((object)_003CCurrentMission_003Ek__BackingField != null)
																	{
																		AchievementUnlockEvent achievementUnlockEvent2 = new AchievementUnlockEvent(missionGraph4.achievementForGolding);
																		if ((object)AchievementsManager.Instance != null)
																		{
																			AchievementsManager.Instance.EventManager_OnAchievementUnlocked(achievementUnlockEvent2);
																			key = achievementUnlockEvent2;
																			num5 = unchecked((nint)null);
																			goto IL_0ad1;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					goto IL_0dfb;
				}
			}
			goto IL_0d4c;
		}
		goto IL_0dfb;
		IL_0a17:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		obj3 = enumerator3;
		key = (AchievementUnlockEvent)0;
		num5 = 0;
		obj = (_003C_003Ec__DisplayClass61_0)(&enumerator2);
		goto IL_0ad1;
		IL_0ad1:
		MissionGraph missionGraph5 = _003CCurrentMission_003Ek__BackingField;
		if ((object)_003CCurrentMission_003Ek__BackingField != null)
		{
			if (missionGraph5.UnlockedPunchcards != null)
			{
				List<PunchcardDefinitionV2> unlockedPunchcards = missionGraph5.UnlockedPunchcards;
				if (unlockedPunchcards._size > 0)
				{
					((Dictionary<string, int>)null).set_Item((string)(object)key, (int)num5);
					MissionGraph missionGraph6 = _003CCurrentMission_003Ek__BackingField;
					ProgressionManager progressionManager = default(ProgressionManager);
					List<string> list = progressionManager.UnlockPunchcards(missionGraph6.UnlockedPunchcards);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					MissionGraph missionGraph7 = _003CCurrentMission_003Ek__BackingField;
					object arg = default(object);
					string message = $"[MissionManager] Unlocked {arg} new punchcards from '{missionGraph7.MissionID}'.";
					Debug.Log(message);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18041E900");
					PunchcardRuntime[] cards = (((object)RequisitionConsoleManager.Instance == null) ? null : RequisitionConsoleManager.Instance.GetAllCards());
					ProgressionManager progressionManager2 = default(ProgressionManager);
					if ((object)progressionManager2 != null)
					{
						progressionManager2.SaveUnlockedCardStates(cards);
						if ((object)ProgressionManager._003CInstance_003Ek__BackingField != null)
						{
							ProgressionManager._003CInstance_003Ek__BackingField.SaveProgression();
							goto IL_0c34;
						}
					}
					goto IL_0dfb;
				}
			}
			goto IL_0c34;
		}
		goto IL_0dfb;
		IL_0c34:
		OperationState newState = SaveOperationState();
		CS_0024_003C_003E8__locals3.newState = newState;
		Func<Task> function = delegate
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
			_003C_003Ec__DisplayClass61_0._003C_003CFinishMission_003Eb__2_003Ed stateMachine = default(_003C_003Ec__DisplayClass61_0._003C_003CFinishMission_003Eb__2_003Ed);
			asyncTaskMethodBuilder.Start(ref stateMachine);
			return asyncTaskMethodBuilder.Task;
		};
		Task task = Task.Run(function);
		MissionGraph missionGraph8 = _003CCurrentMission_003Ek__BackingField;
		Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
		MissionStatsTracker instance = MissionStatsTracker.Instance;
		ProtectedInt protectedInt = default(ProtectedInt);
		int num6 = (ProtectedInt)(&protectedInt);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value2 = default(object);
		dictionary2.Add("RQ", value2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value3 = default(object);
		dictionary2.Add("PC", value3);
		MissionState currentMissionState12 = CurrentMissionState;
		dictionary2.Add("Medals", currentMissionState12.Medals);
		AnalyticsManager.Analytics_Mission("MissionEnded", missionGraph8.MissionID, 0.0, dictionary2);
		goto IL_0d4c;
		IL_0d4c:
		if (SceneObject_EndOfMission != null)
		{
			MissionState currentMissionState13 = CurrentMissionState;
			if (CurrentMissionState != null && currentMissionState13.Complete)
			{
				if ((object)SceneObject_EndOfMission == null)
				{
					goto IL_0dfb;
				}
				SceneObject_EndOfMission.SetActive(value: true);
			}
		}
		ReturnToMap();
		return;
		IL_0dfb:
		throw new NullReferenceException();
	}

	public void ReturnToMap()
	{
		//IL_0086: Expected O, but got I4
		bool flag;
		if (!(_003CCurrentMission_003Ek__BackingField != null))
		{
			flag = false;
		}
		else
		{
			MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
			if (missionGraph.MissionType == MissionGraph.MissionTypes.Challange)
			{
				flag = true;
			}
			else
			{
				object obj = missionGraph.MissionType - 3;
				bool flag2 = obj == null;
				flag = flag2;
			}
		}
		if (_003CCurrentMission_003Ek__BackingField != null)
		{
			_003CCurrentMission_003Ek__BackingField.OnMissionUnloaded();
			_003CCurrentMission_003Ek__BackingField = null;
		}
		UnloadCurrentMissionSceneIfAny();
		if (flag && _003CCurrentOperation_003Ek__BackingField != null)
		{
			OperationGraph operationGraph = _003CCurrentOperation_003Ek__BackingField;
			OperationState operation = ProgressionManager._003CInstance_003Ek__BackingField.GetOperation(operationGraph.OperationID);
			if (operation != null)
			{
				LoadOperationState(operation);
			}
		}
		OperationGraph operationGraph2 = _003CCurrentOperation_003Ek__BackingField;
		bool flag3 = (object)_003CCurrentOperation_003Ek__BackingField == null;
		string text = null;
		if (!flag3)
		{
			text = operationGraph2.OperationID;
		}
		string message = "[MissionManager] Returning to map. Operation '" + text + "' remains active.";
		Debug.Log(message);
		SetPhase(GamePhase.BrowsingMap);
	}

	private void UnloadCurrentMissionSceneIfAny()
	{
		if (!string.IsNullOrEmpty(_003CCurrentMissionSceneName_003Ek__BackingField))
		{
			AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(_003CCurrentMissionSceneName_003Ek__BackingField);
			string message = "[MissionManager] Unloading mission scene: " + _003CCurrentMissionSceneName_003Ek__BackingField;
			Debug.Log(message);
			_003CCurrentMissionSceneName_003Ek__BackingField = null;
		}
	}

	public void MarkMissionComplete()
	{
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (!currentMissionState.Failed && !currentMissionState.Complete)
			{
				currentMissionState.Complete = true;
				MissionState currentMissionState2 = CurrentMissionState;
				float time = Time.time;
				currentMissionState2.CompleteTime = time;
				Debug.Log("[MissionManager] Marked Mission As Complete");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
				EventData_MissionCompleted evt = new EventData_MissionCompleted();
				FireMission fireMission = default(FireMission);
				fireMission.ProcessEvent(evt);
				MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
				AnalyticsManager.Analytics_Mission("MissionComplete", missionGraph.MissionID);
			}
		}
		else
		{
			Debug.Log("[MissionManager] No Active Mission To Complete");
		}
	}

	public void MarkMissionFailed()
	{
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (!currentMissionState.Failed && !currentMissionState.Complete)
			{
				currentMissionState.Failed = true;
				Debug.Log("[MissionManager] Marked Mission As Failed");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407BF0");
				EventData_MissionFailed evt = new EventData_MissionFailed();
				FireMission fireMission = default(FireMission);
				fireMission.ProcessEvent(evt);
				MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
				AnalyticsManager.Analytics_Mission("MissionFailed", missionGraph.MissionID);
			}
		}
		else
		{
			Debug.Log("[MissionManager] No Active Mission To Fail");
		}
	}

	public void ModifyTrackingValue(MedalTrackedValue trackingId, float value)
	{
		//IL_00fe: Expected I4, but got O
		//IL_012c: Expected I, but got O
		//IL_0199: Expected I, but got O
		//IL_01a9: Expected O, but got I
		//IL_021e: Expected I, but got O
		//IL_022e: Expected O, but got I
		//IL_02a3: Expected I, but got O
		//IL_02b3: Expected O, but got I
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (currentMissionState.TrackingValues != null)
			{
				MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
				if (trackingValues.CustomValues != null)
				{
					float value2 = trackingValues.GetValue(trackingId);
					MissionState currentMissionState2 = CurrentMissionState;
					float value3 = value2 + value;
					currentMissionState2.TrackingValues.SetValue(trackingId, value3);
					object[] array = new object[4];
					object obj2 = default(object);
					object obj = (MedalTrackedValue)obj2;
					if (obj != null)
					{
						nint num = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj3 = default(object);
						if (obj3 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj4 = default(object);
							throw obj4;
						}
					}
					array[0] = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj5 = default(object);
					if (obj5 != null)
					{
						nint num2 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj7 = default(object);
						bool flag = obj7 == null;
						object obj8 = obj5;
						if (flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj9 = default(object);
							throw obj9;
						}
					}
					array[1] = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj10 = default(object);
					if (obj10 != null)
					{
						nint num3 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ rdx_v31 (Il2CppClass<System.Object[]>)+40]");
						object obj11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj12 = default(object);
						bool flag2 = obj12 == null;
						object obj13 = obj10;
						if (flag2)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj14 = default(object);
							throw obj14;
						}
					}
					array[2] = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object obj15 = default(object);
					if (obj15 != null)
					{
						nint num4 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v588 @ rdx_v29 (Il2CppClass<System.Object[]>)+40]");
						object obj16 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj17 = default(object);
						bool flag3 = obj17 == null;
						object obj18 = obj15;
						if (flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj19 = default(object);
							throw obj19;
						}
					}
					array[3] = obj15;
					string message = string.Format("[MissionManager] Adjusted TrackingID '{0}' | {1} | Old: {2} | New: {3}", array);
					Debug.Log(message);
					return;
				}
			}
		}
		Debug.Log("[MissionManager] No Active Mission To Modify Medals On");
	}

	public unsafe void ModifyCustomTrackingValue(string trackingId, float value)
	{
		//IL_00ed: Expected F4, but got Ref
		//IL_0125: Expected I, but got O
		//IL_013e: Expected O, but got I
		//IL_039d: Expected O, but got I
		//IL_01a1: Expected I, but got O
		//IL_01b1: Expected O, but got I
		//IL_01ca: Expected O, but got I
		//IL_0235: Expected I, but got O
		//IL_0245: Expected O, but got I
		//IL_025e: Expected O, but got I
		//IL_02f4: Expected I, but got O
		//IL_0304: Expected O, but got I
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (currentMissionState.TrackingValues != null)
			{
				MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
				if (trackingValues.CustomValues != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180690EA0");
					MissionState currentMissionState2 = CurrentMissionState;
					MedalTrackedValues trackingValues2 = currentMissionState2.TrackingValues;
					float num2 = default(float);
					float num = num2 + value;
					trackingValues2.CustomValues.set_Item(trackingId, (float)(nint)(&num2));
					object[] array = new object[4];
					if (trackingId != null)
					{
						nint num3 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
						((Dictionary<string, float>)(object)trackingId).set_Item((string)0, value);
						object obj = default(object);
						if (obj == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ rdx_v35 (Il2CppClass<System.Object[]>)+40]");
							((Dictionary<string, float>)(object)trackingId).set_Item((string)0, value);
							Dictionary<string, float> dictionary = default(Dictionary<string, float>);
							throw dictionary;
						}
					}
					array[0] = trackingId;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, float> dictionary2 = default(Dictionary<string, float>);
					if (dictionary2 != null)
					{
						nint num4 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
						string key = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v517 @ rdx_v33 (Il2CppClass<System.Object[]>)+40]");
						dictionary2.set_Item((string)0, value);
						object obj2 = default(object);
						bool flag = obj2 == null;
						Dictionary<string, float> dictionary3 = dictionary2;
						if (flag)
						{
							dictionary3.set_Item(key, value);
							Dictionary<string, float> dictionary4 = default(Dictionary<string, float>);
							throw dictionary4;
						}
					}
					array[1] = dictionary2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, float> dictionary5 = default(Dictionary<string, float>);
					if (dictionary5 != null)
					{
						nint num5 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rdx_v31 (Il2CppClass<System.Object[]>)+40]");
						string key2 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v609 @ rdx_v31 (Il2CppClass<System.Object[]>)+40]");
						dictionary5.set_Item((string)0, value);
						object obj3 = default(object);
						bool flag2 = obj3 == null;
						Dictionary<string, float> dictionary6 = dictionary5;
						if (flag2)
						{
							dictionary6.set_Item(key2, value);
							Dictionary<string, float> dictionary7 = default(Dictionary<string, float>);
							throw dictionary7;
						}
					}
					array[2] = dictionary5;
					MissionState currentMissionState3 = CurrentMissionState;
					MedalTrackedValues trackingValues3 = currentMissionState3.TrackingValues;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Dictionary<string, float> dictionary8 = default(Dictionary<string, float>);
					if (dictionary8 != null)
					{
						nint num6 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v657 @ rdx_v29 (Il2CppClass<System.Object[]>)+40]");
						string key3 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj4 = default(object);
						bool flag3 = obj4 == null;
						Dictionary<string, float> dictionary9 = dictionary8;
						if (flag3)
						{
							dictionary9.set_Item(key3, value);
							object obj5 = default(object);
							throw obj5;
						}
					}
					array[3] = dictionary8;
					string message = string.Format("[MissionManager] Adjusted TrackingID '{0}' | {1} | Old: {2} | New: {3}", array);
					Debug.Log(message);
					return;
				}
			}
		}
		Debug.Log("[MissionManager] No Active Mission To Modify Medals On");
	}

	public void SetTrackingValue(MedalTrackedValue trackingId, float value)
	{
		//IL_00b7: Expected I4, but got O
		object message;
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (currentMissionState.TrackingValues != null)
			{
				MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
				if (trackingValues.CustomValues != null)
				{
					trackingValues.SetValue(trackingId, value);
					object obj = default(object);
					object arg = (MedalTrackedValue)obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg2 = default(object);
					object arg3 = default(object);
					string text = $"[MissionManager] Set TrackingID '{arg}' | {arg2} | New: {arg3}";
					message = text;
					goto IL_011d;
				}
			}
		}
		message = "[MissionManager] No Active Mission To Modify Medals On";
		goto IL_011d;
		IL_011d:
		Debug.Log(message);
	}

	public unsafe void SetCustomTrackingValue(string trackingId, float value)
	{
		//IL_00b3: Expected F4, but got Ref
		object message;
		if (_003CCurrentMission_003Ek__BackingField != null && CurrentMissionState != null)
		{
			MissionState currentMissionState = CurrentMissionState;
			if (currentMissionState.TrackingValues != null)
			{
				MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
				if (trackingValues.CustomValues != null)
				{
					object obj = default(object);
					trackingValues.CustomValues.set_Item(trackingId, (float)(nint)(&obj));
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					MissionState currentMissionState2 = CurrentMissionState;
					MedalTrackedValues trackingValues2 = currentMissionState2.TrackingValues;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					object arg2 = default(object);
					string text = $"[MissionManager] Set TrackingID '{trackingId}' | {arg} | New: {arg2}";
					message = text;
					goto IL_0140;
				}
			}
		}
		message = "[MissionManager] No Active Mission To Modify Medals On";
		goto IL_0140;
		IL_0140:
		Debug.Log(message);
	}

	public void ReloadCurrentMission()
	{
		if (!(_003CCurrentMission_003Ek__BackingField == null))
		{
			LoadMission(_003CCurrentMission_003Ek__BackingField, forceReload: true);
		}
		else
		{
			Debug.LogWarning("[MissionManager] No current mission. Cannot reload.");
		}
	}

	public unsafe OperationState SaveOperationState()
	{
		//IL_0119: Expected I, but got O
		//IL_0142: Expected O, but got I
		//IL_015a: Expected O, but got Ref
		//IL_0175: Expected O, but got Ref
		//IL_05f3: Expected I, but got O
		//IL_0624: Expected O, but got I
		//IL_0354: Expected O, but got I4
		//IL_040c: Expected O, but got Ref
		//IL_0475: Expected O, but got Ref
		//IL_04b8: Expected O, but got Ref
		ProgressionManager progressionManager = ProgressionManager._003CInstance_003Ek__BackingField;
		OperationGraph operationGraph = _003CCurrentOperation_003Ek__BackingField;
		OperationState operation;
		if ((object)_003CCurrentOperation_003Ek__BackingField != null && (object)ProgressionManager._003CInstance_003Ek__BackingField != null)
		{
			operation = ProgressionManager._003CInstance_003Ek__BackingField.GetOperation(operationGraph.OperationID);
			PunchcardRuntime[] cards = (((object)RequisitionConsoleManager.Instance == null) ? null : RequisitionConsoleManager.Instance.GetAllCards());
			bool flag = (object)ProgressionManager._003CInstance_003Ek__BackingField == null;
			progressionManager = (ProgressionManager)(object)RequisitionConsoleManager.Instance;
			if (!flag)
			{
				ProgressionManager._003CInstance_003Ek__BackingField.SaveUnlockedCardStates(cards);
				MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
				bool flag2 = (object)_003CCurrentMission_003Ek__BackingField == null;
				progressionManager = ProgressionManager._003CInstance_003Ek__BackingField;
				if (!flag2)
				{
					if (missionGraph.MissionType != MissionGraph.MissionTypes.Campaign && missionGraph.MissionType != MissionGraph.MissionTypes.Tutorial)
					{
						bool flag3 = operation == null;
						progressionManager = ProgressionManager._003CInstance_003Ek__BackingField;
						if (!flag3)
						{
							goto IL_05af;
						}
					}
					else
					{
						nint num = (nint)typeof(MissionStatsTracker);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v726 @ rax_v61 (Il2CppClass<MissionStatsTracker>)+B8]");
						nint num2 = 0;
						bool flag4 = (object)MissionStatsTracker.Instance == null;
						progressionManager = (ProgressionManager)num2;
						if (!flag4)
						{
							ProtectedInt protectedInt = default(ProtectedInt);
							int requisitionPoints = (ProtectedInt)(&protectedInt);
							bool flag5 = operation == null;
							progressionManager = (ProgressionManager)(&protectedInt);
							if (!flag5)
							{
								operation.RequisitionPoints = requisitionPoints;
								nint num3 = (nint)typeof(PowderChargeInventory);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v758 @ rax_v65 (Il2CppClass<PowderChargeInventory>)+B8]");
								nint num4 = 0;
								PowderChargeInventory powderChargeInventory = PowderChargeInventory._003CInstance_003Ek__BackingField;
								bool flag6 = (object)PowderChargeInventory._003CInstance_003Ek__BackingField == null;
								progressionManager = (ProgressionManager)num4;
								if (!flag6)
								{
									operation.PowderCharges = powderChargeInventory._currentCharges;
									goto IL_05af;
								}
							}
						}
					}
				}
			}
		}
		goto IL_0545;
		IL_05af:
		MissionGraph missionGraph2 = _003CCurrentMission_003Ek__BackingField;
		bool flag7 = (object)_003CCurrentMission_003Ek__BackingField == null;
		progressionManager = (ProgressionManager)(object)operation.MissionStates;
		OperationState.MissionState missionState = default(OperationState.MissionState);
		if (!flag7)
		{
			bool flag8 = operation.MissionStates == null;
			progressionManager = (ProgressionManager)(object)operation.MissionStates;
			if (!flag8)
			{
				bool flag9 = operation.MissionStates.TryGetValue(missionGraph2.MissionID, out var _);
				progressionManager = (ProgressionManager)(object)operation.MissionStates;
				if (flag9)
				{
					goto IL_031e;
				}
				missionState = new OperationState.MissionState();
				MissionGraph missionGraph3 = _003CCurrentMission_003Ek__BackingField;
				bool flag10 = (object)_003CCurrentMission_003Ek__BackingField == null;
				progressionManager = (ProgressionManager)(object)missionState;
				if (!flag10)
				{
					bool flag11 = missionState == null;
					progressionManager = (ProgressionManager)(object)missionState;
					if (!flag11)
					{
						missionState.MissionID = missionGraph3.MissionID;
						MissionGraph missionGraph4 = _003CCurrentMission_003Ek__BackingField;
						bool flag12 = (object)_003CCurrentMission_003Ek__BackingField == null;
						progressionManager = (ProgressionManager)(object)operation.MissionStates;
						if (!flag12)
						{
							bool flag13 = operation.MissionStates == null;
							progressionManager = (ProgressionManager)(object)operation.MissionStates;
							if (!flag13)
							{
								operation.MissionStates.set_Item(missionGraph4.MissionID, missionState);
								progressionManager = (ProgressionManager)(object)operation.MissionStates;
								goto IL_031e;
							}
						}
					}
				}
			}
		}
		goto IL_0545;
		IL_0545:
		throw new NullReferenceException();
		IL_031e:
		MissionState currentMissionState = CurrentMissionState;
		if (CurrentMissionState != null)
		{
			progressionManager = (ProgressionManager)currentMissionState.Complete;
			if (missionState != null)
			{
				missionState.Completed = currentMissionState.Complete;
				MissionState currentMissionState2 = CurrentMissionState;
				if (CurrentMissionState != null)
				{
					progressionManager = (ProgressionManager)(object)currentMissionState2.Medals;
					if (currentMissionState2.Medals != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
						Dictionary<string, int>.Enumerator enumerator = default(Dictionary<string, int>.Enumerator);
						object obj = default(object);
						object obj2 = default(object);
						object obj3 = default(object);
						string key = default(string);
						PunchcardRuntime[] array = default(PunchcardRuntime[]);
						while (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							bool flag14 = missionState == null;
							progressionManager = (ProgressionManager)(&enumerator);
							if (!flag14)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806910A0");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
								{
									bool flag15 = missionState == null;
									progressionManager = (ProgressionManager)(&obj3);
									if (flag15)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
									bool flag16 = missionState.Medals == null;
									progressionManager = (ProgressionManager)(&obj3);
									if (flag16)
									{
										throw new NullReferenceException();
									}
									missionState.Medals.set_Item(key, (int)(&array));
								}
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						progressionManager = ProgressionManager._003CInstance_003Ek__BackingField;
						if ((object)ProgressionManager._003CInstance_003Ek__BackingField != null)
						{
							ProgressionManager._003CInstance_003Ek__BackingField.SaveProgression();
							OperationGraph operationGraph2 = _003CCurrentOperation_003Ek__BackingField;
							if ((object)_003CCurrentOperation_003Ek__BackingField != null && (object)ProgressionManager._003CInstance_003Ek__BackingField != null)
							{
								ProgressionManager._003CInstance_003Ek__BackingField.SaveOperation(operationGraph2.OperationID);
								Debug.Log("[MissionManager] Saved State");
								return operation;
							}
						}
					}
				}
			}
		}
		goto IL_0545;
	}

	public void LoadOperationState(OperationState state)
	{
		//IL_010d: Expected O, but got I4
		if (state != null)
		{
			OperationGraph operationGraph = _003CCurrentOperation_003Ek__BackingField;
			if (state.OperationID == operationGraph.OperationID)
			{
				MissionStatsTracker instance = MissionStatsTracker.Instance;
				instance.requisitionPointsTampered = false;
				bool flag = state.RequisitionPoints < 0;
				int num = 0;
				if (!flag)
				{
					num = state.RequisitionPoints;
				}
				instance.requisitionPoints = num;
				instance.reqPoints = (ProtectedInt)((ProtectedInt)num).encryptedValue;
				instance.UpdateMissionOdometers();
				instance.UpdateCampaignOdometers();
				PowderChargeInventory._003CInstance_003Ek__BackingField.CurrentCharges = state.PowderCharges;
			}
		}
		else
		{
			Debug.LogWarning("[MissionManager] No operation state found.");
		}
	}

	private void SetupMissionPunchcards()
	{
		//IL_006c: Expected O, but got I
		//IL_0321: Expected O, but got I4
		//IL_037f: Expected O, but got I4
		//IL_0154: Expected O, but got I
		//IL_0199: Expected O, but got I
		UnityEngine.Object instance = RequisitionConsoleManager.Instance;
		object message2;
		if (RequisitionConsoleManager.Instance != null)
		{
			RequisitionConsoleManager.Instance.InitializeConsole();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r15_v1 (UnityEngine.Object)+38]");
			if ((nint)0 != 0)
			{
				ProgressionManager progressionManager = ProgressionManager._003CInstance_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r15_v1 (UnityEngine.Object)+38]");
				List<PunchcardDefinitionV2> list = progressionManager.BuildUnlockedPunchcards((Dictionary<string, PunchcardDefinitionV2>)0);
				Func<PunchcardDefinitionV2, bool> predicate = _003C_003Ec._003C_003E9__73_0;
				if (_003C_003Ec._003C_003E9__73_0 == null)
				{
					Func<PunchcardDefinitionV2, bool> func = (_003C_003Ec._003C_003E9__73_0 = delegate(PunchcardDefinitionV2 x)
					{
						//IL_0076: Expected I4, but got O
						bool flag = x != null;
						if (!flag)
						{
							return flag;
						}
						if ((object)x == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						bool flag2 = string.IsNullOrEmpty(x.ID);
						return (byte)((flag2 ? 1u : 0u) ^ 1u) != 0;
					});
					object obj = 0;
					predicate = func;
				}
				IEnumerable<PunchcardDefinitionV2> source = Enumerable.Where(list, predicate);
				Func<PunchcardDefinitionV2, string> selector = _003C_003Ec._003C_003E9__73_1;
				if (_003C_003Ec._003C_003E9__73_1 == null)
				{
					Func<PunchcardDefinitionV2, string> func2 = (_003C_003Ec._003C_003E9__73_1 = (PunchcardDefinitionV2 x) => (string)(((object)x != null) ? ((object)x.ID) : ((object)new NullReferenceException())));
					object obj = 0;
					selector = func2;
				}
				IEnumerable<string> collection = Enumerable.Select(source, selector);
				HashSet<string> hashSet = new HashSet<string>(collection);
				MissionGraph missionGraph = _003CCurrentMission_003Ek__BackingField;
				if (missionGraph.RequiredPunchcards != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<PunchcardDefinitionV2>.Enumerator enumerator = default(List<PunchcardDefinitionV2>.Enumerator);
					UnityEngine.Object obj2 = default(UnityEngine.Object);
					object arg = default(object);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (!(obj2 != null))
							{
								continue;
							}
							if ((object)obj2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ stack_18_v6 (UnityEngine.Object)+18]");
								if (string.IsNullOrEmpty((string)0))
								{
									continue;
								}
								if (hashSet != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v567 @ stack_18_v6 (UnityEngine.Object)+18]");
									if (!hashSet.Contains((string)0))
									{
										PunchcardDefinitionV2 punchcardDefinitionV = UnityEngine.Object.Instantiate((PunchcardDefinitionV2)obj2);
										if ((object)punchcardDefinitionV == null)
										{
											break;
										}
										punchcardDefinitionV.RemainingUses = punchcardDefinitionV.MaxUses;
										list.Add(punchcardDefinitionV);
										hashSet.Add(punchcardDefinitionV.ID);
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator.Dispose();
						RequisitionConsoleManager.Instance.RebuildDeck(list);
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string message = $"[MissionManager] Setup {arg} mission punchcards.";
						Debug.Log(message);
						return;
					}
					throw new NullReferenceException();
				}
				RequisitionConsoleManager.Instance.RebuildDeck(list);
				return;
			}
			message2 = "[MissionManager] Punchcard definitions missing. Cannot setup punchcards.";
		}
		else
		{
			message2 = "[MissionManager] RequisitionConsoleManager missing. Cannot setup punchcards.";
		}
		Debug.LogWarning(message2);
	}

	private static void EnsureMutatorRuntime()
	{
		if (MutatorRuntime._003CInstance_003Ek__BackingField == null)
		{
			GameObject gameObject = new GameObject("MutatorRuntime");
			MutatorRuntime mutatorRuntime = gameObject.AddComponent<MutatorRuntime>();
		}
	}

	private void SetPhase(GamePhase next)
	{
		//IL_0018: Expected I4, but got O
		//IL_0025: Expected I4, but got O
		if (_003CCurrentPhase_003Ek__BackingField != next)
		{
			_003CCurrentPhase_003Ek__BackingField = next;
			object obj = default(object);
			object arg = (GamePhase)obj;
			object obj2 = default(object);
			object arg2 = (GamePhase)obj2;
			string message = $"[MissionManager] Phase: {arg} → {arg2}";
			Debug.Log(message);
			Action<GamePhase, GamePhase> phaseChanged = this.m_PhaseChanged;
			if (this.m_PhaseChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v131 @ rcx_v8 (System.Action`2<MissionManager+GamePhase, MissionManager+GamePhase>)+18] (should have been resolved before IL gen)");
			}
		}
	}

	private void _003CLoadMainMenu_003Eb__55_0(AsyncOperation _)
	{
		//IL_0097: Expected O, but got I
		//IL_00a7: Expected O, but got I
		//IL_00b7: Expected O, but got I
		MissionSceneReference missionSceneReference = mainMenuScene;
		loadedMainMenuScene = missionSceneReference.sceneName;
		_003CCurrentOperation_003Ek__BackingField = null;
		_003CCurrentMission_003Ek__BackingField = null;
		string message = "[MissionManager] Loaded Main Menu: " + loadedMainMenuScene;
		Debug.Log(message);
		SetPhase(GamePhase.MainMenu);
		Action<string> mainMenuLoaded = this.m_MainMenuLoaded;
		if (this.m_MainMenuLoaded != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+18]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+28]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v9 (System.Action`1<System.String>)+40]");
			object obj3 = 0;
			string text = loadedMainMenuScene;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v61 @ rax_v10 (should have been resolved before IL gen)");
		}
	}
}
