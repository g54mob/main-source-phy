using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public class UILeaderboard : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	[CompilerGenerated]
	private struct _003CFetchCombined_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> _003C_003Et__builder;

		private TaskAwaiter<ClientCombinedLeaderboardResponse> _003C_003Eu__1;

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
	private struct _003CRefresh_003Ed__16 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public UILeaderboard _003C_003E4__this;

		private ClientCombinedLeaderboardResponse _003Cresult_003E5__2;

		private BackgroundThreadAwaitable _003C_003Eu__1;

		private TaskAwaiter<ClientCombinedLeaderboardResponse> _003C_003Eu__2;

		private MainThreadAwaitable _003C_003Eu__3;

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

	private const int DefaultLeaderboardCount = 20;

	public bool HasRun;

	public Gamemodes LeaderboardGamemode;

	public UILeaderboardEntry Prefab_Entry;

	public Transform Transform_ListRoot;

	public UILeaderboardEntry Entry_Self;

	public int EntryCount;

	public static bool FetchInProgress;

	public static ClientCombinedLeaderboardResponse MostRecentCombinedData;

	private static readonly object FetchLock;

	private static Task<ClientCombinedLeaderboardResponse> fetchTask;

	private List<UILeaderboardEntry> spawnedUIEntries;

	private bool refreshInProgress;

	private void Start()
	{
	}

	public static void RefreshAll(bool force)
	{
	}

	public void RefreshNow()
	{
	}

	[AsyncStateMachine(typeof(_003CRefresh_003Ed__16))]
	public Task Refresh()
	{
		return null;
	}

	private static Task<ClientCombinedLeaderboardResponse> GetCombinedOnce()
	{
		return null;
	}

	[AsyncStateMachine(typeof(_003CFetchCombined_003Ed__18))]
	private static Task<ClientCombinedLeaderboardResponse> FetchCombined()
	{
		return null;
	}

	private static List<LeaderboardEntryResponse> GetEntries(ClientCombinedLeaderboardResponse data, Gamemodes gamemode)
	{
		return null;
	}

	private static GetMyLeaderboardResponse GetSelf(ClientCombinedLeaderboardResponse data, Gamemodes gamemode)
	{
		return null;
	}

	public static void CleanupImageCache(TimeSpan? maxAge = null)
	{
	}
}
