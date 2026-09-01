using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Heathen.SteamworksIntegration;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
	[Serializable]
	public class LocalSubmissionQueue
	{
		public List<LocalSubmission> Submissions;
	}

	[Serializable]
	public class LocalSubmission
	{
		public string SubmissionID;

		public Gamemodes Gamemode;

		public int Score;

		public DateTime CreatedAtUtc;

		public string Username;

		public bool ClientTampered;

		public LeaderboardRunData RunData;

		public string ImageExtension;

		public string PerformanceStatsJson;

		public string ReplayFileName;
	}

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CLeaderboard_CompleteRun_003Ed__25 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		public Action onCompleted;

		private PostLeaderboardScoreRequest _003Csubmission_003E5__2;

		private byte[] _003CzipBytes_003E5__3;

		private TaskAwaiter<PostLeaderboardScoreResponse> _003C_003Eu__1;

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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CLeaderboard_StartRun_003Ed__24 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		public Gamemodes gamemode;

		private TaskAwaiter<GetSessionKeyResponse> _003C_003Eu__1;

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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CPushOperationState_003Ed__23 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public OperationState state;

		private TaskAwaiter<bool> _003C_003Eu__1;

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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CRegisterUser_003Ed__20 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		private long? _003CsteamId_003E5__2;

		private string _003Cusername_003E5__3;

		private string _003CavatarBase64_003E5__4;

		private TaskAwaiter _003C_003Eu__1;

		private TaskAwaiter<string> _003C_003Eu__2;

		private TaskAwaiter<RegisterResponse> _003C_003Eu__3;

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

	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CRetryPendingSubmissions_003Ed__36 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		private List<LocalSubmission>.Enumerator _003C_003E7__wrap1;

		private LocalSubmission _003Csubmission_003E5__3;

		private byte[] _003CzipBytes_003E5__4;

		private TaskAwaiter<GetSessionKeyResponse> _003C_003Eu__1;

		private TaskAwaiter<PostLeaderboardScoreResponse> _003C_003Eu__2;

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

	public const string OptOutPrefsKey = "LeaderboardOptOut";

	public static LeaderboardManager Instance;

	[Header("Setup")]
	public string APIEndpoint;

	public string SecretKey;

	[ReadOnly]
	public LeaderboardRunData CurrentRun;

	private Guid? currentSessionId;

	private Gamemodes currentGamemode;

	private bool isSubmitting;

	private bool isRetryingPendingSubmissions;

	private LocalSubmissionQueue localSubmissionQueue;

	public static bool OptOut => false;

	private string LocalSubmissionFolder => null;

	private string LocalSubmissionQueuePath => null;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	[AsyncStateMachine(typeof(_003CRegisterUser_003Ed__20))]
	public Task RegisterUser()
	{
		return null;
	}

	private Task<string> GetSteamAvatarBase64(UserData user)
	{
		return null;
	}

	private string GetFallbackAvatarBase64()
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CPushOperationState_003Ed__23))]
	public static void PushOperationState(OperationState state)
	{
	}

	[AsyncStateMachine(typeof(_003CLeaderboard_StartRun_003Ed__24))]
	public void Leaderboard_StartRun(Gamemodes gamemode)
	{
	}

	[AsyncStateMachine(typeof(_003CLeaderboard_CompleteRun_003Ed__25))]
	public void Leaderboard_CompleteRun(Action onCompleted)
	{
	}

	public void RecordAction(string action, string details, int scoreDelta, bool includeImage = false)
	{
	}

	private void ModifyScore(int value)
	{
	}

	public List<LeaderboardEntryResponse> GetPendingEntries(Gamemodes gamemode)
	{
		return null;
	}

	private void LoadLocalSubmissionQueue()
	{
	}

	private void SaveLocalSubmissionQueue()
	{
	}

	private byte[] Compress(byte[] data)
	{
		return null;
	}

	private byte[] Decompress(byte[] data)
	{
		return null;
	}

	private byte[] Encrypt(byte[] data)
	{
		return null;
	}

	private byte[] Decrypt(byte[] data)
	{
		return null;
	}

	private void QueueFailedSubmission(PostLeaderboardScoreRequest submission, byte[] zipBytes)
	{
	}

	[AsyncStateMachine(typeof(_003CRetryPendingSubmissions_003Ed__36))]
	private Task RetryPendingSubmissions()
	{
		return null;
	}

	private void RemovePendingSubmission(LocalSubmission submission)
	{
	}
}
