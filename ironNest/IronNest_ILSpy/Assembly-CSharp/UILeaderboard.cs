using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cpp2ILInjected;
using UnityEngine;

public class UILeaderboard : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<LeaderboardEntryResponse, int> _003C_003E9__16_0;

		public static Func<LeaderboardEntryResponse, bool> _003C_003E9__16_1;

		public static Func<LeaderboardEntryResponse, DateTime> _003C_003E9__16_2;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal int _003CRefresh_003Eb__16_0(LeaderboardEntryResponse x)
		{
			//IL_0035: Expected I4, but got O
			if (x != null)
			{
				return x._003CScore_003Ek__BackingField;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		internal bool _003CRefresh_003Eb__16_1(LeaderboardEntryResponse x)
		{
			//IL_0035: Expected I4, but got O
			if (x != null)
			{
				return x._003CIsPendingLocal_003Ek__BackingField;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal DateTime _003CRefresh_003Eb__16_2(LeaderboardEntryResponse x)
		{
			return (DateTime)(((object?)x?._003CCreatedAtUtc_003Ek__BackingField) ?? ((object)new NullReferenceException()));
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CFetchCombined_003Ed__18 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> _003C_003Et__builder;

		private TaskAwaiter<ClientCombinedLeaderboardResponse> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_023a: Expected O, but got Ref
			//IL_0010: Expected O, but got I4
			//IL_001f: Expected I4, but got I8
			//IL_002c: Expected O, but got I8
			//IL_01ca: Expected O, but got Ref
			//IL_00f3: Expected O, but got Ref
			//IL_0302: Expected I4, but got I8
			//IL_013b: Expected O, but got I
			//IL_0192: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse;
			if (obj2 == null)
			{
				_003C_003Eu__1 = (TaskAwaiter<ClientCombinedLeaderboardResponse>)0;
				_003C_003E1__state = -1;
				obj2 = 4294967295L;
				TaskAwaiter<ClientCombinedLeaderboardResponse> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				Task<ClientCombinedLeaderboardResponse> clientCombined = LeaderboardClient.GetClientCombined();
				bool flag = clientCombined == null;
				clientCombinedLeaderboardResponse = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<ClientCombinedLeaderboardResponse> awaiter = clientCombined.GetAwaiter();
				TaskAwaiter<ClientCombinedLeaderboardResponse> taskAwaiter = default(TaskAwaiter<ClientCombinedLeaderboardResponse>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					if (0 < 0)
					{
						FetchInProgress = false;
					}
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse2 = default(ClientCombinedLeaderboardResponse);
			MostRecentCombinedData = clientCombinedLeaderboardResponse2;
			clientCombinedLeaderboardResponse = MostRecentCombinedData;
			if (MostRecentCombinedData != null)
			{
				clientCombinedLeaderboardResponse = (ClientCombinedLeaderboardResponse)(object)clientCombinedLeaderboardResponse._003CDailyChallengeLeaderboard_003Ek__BackingField;
				if (clientCombinedLeaderboardResponse._003CDailyChallengeLeaderboard_003Ek__BackingField != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					object obj3 = (object)(&obj2);
					List<LeaderboardEntryResponse>.Enumerator enumerator = default(List<LeaderboardEntryResponse>.Enumerator);
					object obj4 = default(object);
					while (true)
					{
						if (enumerator.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (obj4 == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ stack_-68_v6+50]");
							string text = ((string)0).Replace(".zip", "_small.zip");
							continue;
						}
						if ((nint)obj3 < 0)
						{
							enumerator.Dispose();
						}
						if ((nint)obj < 0)
						{
							FetchInProgress = false;
						}
						_003C_003E1__state = -2;
						AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder2)->SetResult(clientCombinedLeaderboardResponse2);
						return;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private unsafe void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//IL_0010: Expected O, but got Ref
			AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = (AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>*)asyncTaskMethodBuilder)->SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CRefresh_003Ed__16 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public UILeaderboard _003C_003E4__this;

		private ClientCombinedLeaderboardResponse _003Cresult_003E5__2;

		private BackgroundThreadAwaitable _003C_003Eu__1;

		private TaskAwaiter<ClientCombinedLeaderboardResponse> _003C_003Eu__2;

		private MainThreadAwaitable _003C_003Eu__3;

		private unsafe void MoveNext()
		{
			//IL_0251: Expected O, but got I4
			//IL_0260: Expected I4, but got I8
			//IL_0269: Expected O, but got I4
			//IL_0276: Expected O, but got I8
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Expected O, but got Unknown
			//IL_012b: Expected O, but got I4
			//IL_10f8: Expected I4, but got I8
			//IL_0286: Expected O, but got I4
			//IL_0295: Expected I4, but got I8
			//IL_0229: Expected O, but got I4
			//IL_0238: Expected I4, but got I8
			//IL_0241: Expected O, but got I4
			//IL_0f1d: Expected O, but got Ref
			//IL_018a: Expected O, but got I4
			//IL_1174: Expected I, but got O
			//IL_118a: Expected O, but got I
			//IL_034b: Expected O, but got Ref
			//IL_01bd: Expected O, but got Ref
			//IL_0e83: Expected O, but got Ref
			//IL_1263: Expected O, but got I4
			//IL_12c1: Expected O, but got I4
			//IL_131f: Expected O, but got I4
			//IL_07f7: Expected O, but got I
			//IL_0c94: Expected O, but got I
			//IL_0a98: Expected O, but got I
			//IL_0ab7: Expected O, but got I
			//IL_0cb3: Expected O, but got I
			//IL_0cd6: Expected O, but got I
			//IL_0ae4: Expected O, but got I
			//IL_0862: Expected O, but got I
			//IL_0862: Expected O, but got I
			//IL_0885: Expected O, but got I
			//IL_08ba: Expected O, but got I
			//IL_0d07: Expected O, but got I
			//IL_08dd: Expected O, but got I
			//IL_08f5: Expected O, but got I
			//IL_0b77: Expected O, but got I
			//IL_13ff: Expected O, but got Ref
			//IL_0d76: Expected O, but got I
			//IL_0b96: Expected O, but got I
			//IL_0bb9: Expected O, but got I
			//IL_0ee2: Expected O, but got Ref
			//IL_0c21: Expected O, but got I
			//IL_0df5: Expected O, but got I
			//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c42: Expected I4, but got Unknown
			//IL_0c5b: Expected O, but got I
			//IL_0e4d: Expected O, but got I
			UILeaderboard uILeaderboard = _003C_003E4__this;
			if (_003C_003E1__state <= 2)
			{
				goto IL_00f7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
			if (num != 1)
			{
				if ((object)_003C_003E4__this == null)
				{
					throw new NullReferenceException();
				}
				GameObject gameObject = _003C_003E4__this.gameObject;
				if ((object)gameObject == null)
				{
					throw new NullReferenceException();
				}
				if (gameObject.activeInHierarchy)
				{
					_ = 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v3 (UILeaderboard)+50]");
					if ((nint)0 == 0)
					{
						_ = 1;
						goto IL_00f7;
					}
				}
			}
			goto IL_10e9;
			IL_00f7:
			object obj = default(object);
			bool flag = obj == null;
			BackgroundThreadAwaitable backgroundThreadAwaitable2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder);
			MainThreadAwaitable mainThreadAwaitable;
			MainThreadAwaitable mainThreadAwaitable2;
			if (!flag)
			{
				object obj2 = obj - 1;
				mainThreadAwaitable = (MainThreadAwaitable)0;
				if (flag)
				{
					goto IL_10fd;
				}
				if ((nint)obj2 == 1)
				{
					mainThreadAwaitable2 = _003C_003Eu__3;
					_003C_003Eu__3 = (MainThreadAwaitable)0;
					_003C_003E1__state = -1;
					mainThreadAwaitable = (MainThreadAwaitable)0;
					goto IL_03e9;
				}
				BackgroundThreadAwaitable backgroundThreadAwaitable = Awaitable.BackgroundThreadAsync();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
				BackgroundThreadAwaitable awaiter = default(BackgroundThreadAwaitable);
				bool isCompleted = awaiter.IsCompleted;
				backgroundThreadAwaitable2 = (BackgroundThreadAwaitable)0;
				if (!isCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = awaiter;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitOnCompleted(ref awaiter, ref this);
					if (0 < 0)
					{
						if ((object)asyncTaskMethodBuilder2 == null)
						{
							throw new NullReferenceException();
						}
						_ = 0;
					}
					return;
				}
			}
			else
			{
				_003C_003Eu__1 = (BackgroundThreadAwaitable)0;
				_003C_003E1__state = -1;
				backgroundThreadAwaitable2 = (BackgroundThreadAwaitable)0;
				obj = 4294967295L;
			}
			_003Cresult_003E5__2 = null;
			mainThreadAwaitable = (MainThreadAwaitable)backgroundThreadAwaitable2;
			goto IL_10fd;
			IL_10e9:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->SetResult();
			return;
			IL_0539:
			List<LeaderboardEntryResponse> second;
			AsyncTaskMethodBuilder instance;
			if ((object)LeaderboardManager.Instance != null)
			{
				bool flag2 = (object)asyncTaskMethodBuilder2 == null;
				instance = (AsyncTaskMethodBuilder)LeaderboardManager.Instance;
				if (flag2)
				{
					throw new NullReferenceException();
				}
				LeaderboardManager instance2 = LeaderboardManager.Instance;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+24]");
				List<LeaderboardEntryResponse> pendingEntries = instance2.GetPendingEntries(Gamemodes.Challange);
				bool flag3 = pendingEntries != null;
				second = pendingEntries;
				if (flag3)
				{
					goto IL_05d6;
				}
			}
			List<LeaderboardEntryResponse> list = new List<LeaderboardEntryResponse>();
			second = list;
			goto IL_05d6;
			IL_134f:
			List<LeaderboardEntryResponse> list2;
			bool flag4 = list2 == null;
			IEnumerable<LeaderboardEntryResponse> enumerable;
			instance = (AsyncTaskMethodBuilder)enumerable;
			ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse;
			ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder4;
			if (!flag4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[Leaderboard] Entries: {arg}";
				Debug.Log(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message2 = $"[Leaderboard] Self: {arg2}";
				Debug.Log(message2);
				UILeaderboardEntry.UnloadUnusedCachedTexturesForEntries(list2);
				int num2 = 0;
				instance = (AsyncTaskMethodBuilder)list2;
				UILeaderboardEntry uILeaderboardEntry3 = default(UILeaderboardEntry);
				object obj3 = default(object);
				LeaderboardEntryResponse entry = default(LeaderboardEntryResponse);
				LeaderboardEntryResponse entry2 = default(LeaderboardEntryResponse);
				while (num2 < list2._size)
				{
					if ((object)asyncTaskMethodBuilder2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
						instance = (AsyncTaskMethodBuilder)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
						if ((nint)0 != 0)
						{
							int num3 = num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1762 @ rcx_v112 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+18]");
							UILeaderboardEntry uILeaderboardEntry2;
							if ((nint)num3 >= (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+28]");
								nint num4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+30]");
								UILeaderboardEntry uILeaderboardEntry = UnityEngine.Object.Instantiate((UILeaderboardEntry)num4, (Transform)0);
								bool flag5 = (object)asyncTaskMethodBuilder2 == null;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+28]");
								instance = (AsyncTaskMethodBuilder)0;
								if (flag5)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								bool flag6 = (nint)0 == 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								instance = (AsyncTaskMethodBuilder)0;
								if (flag6)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								((List<UILeaderboardEntry>)0).Add(uILeaderboardEntry);
								uILeaderboardEntry2 = uILeaderboardEntry;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								instance = (AsyncTaskMethodBuilder)0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								if ((nint)0 == 0)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								uILeaderboardEntry2 = uILeaderboardEntry3;
							}
							if ((object)uILeaderboardEntry2 != null)
							{
								Transform transform = uILeaderboardEntry2.transform;
								bool flag7 = (object)transform == null;
								instance = (AsyncTaskMethodBuilder)uILeaderboardEntry2;
								if (!flag7)
								{
									transform.SetSiblingIndex(num2);
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									bool flag8 = obj3 == null;
									instance = (AsyncTaskMethodBuilder)list2;
									if (!flag8)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2700 @ stack_-F8_v30+60]");
										if ((nint)0 == 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											uILeaderboardEntry2.Init(num2, entry);
											num2++;
											instance = (AsyncTaskMethodBuilder)uILeaderboardEntry2;
										}
										else
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											uILeaderboardEntry2.InitLocal(num2, entry2);
											num2++;
											instance = (AsyncTaskMethodBuilder)uILeaderboardEntry2;
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
				if (clientCombinedLeaderboardResponse != null)
				{
					if ((object)asyncTaskMethodBuilder2 == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
					bool flag9 = (UnityEngine.Object)0 != null;
					bool flag10 = !flag9;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
					instance = (AsyncTaskMethodBuilder)0;
					if (!flag10)
					{
						bool flag11 = clientCombinedLeaderboardResponse2 == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
						instance = (AsyncTaskMethodBuilder)0;
						if (!flag11)
						{
							instance = (AsyncTaskMethodBuilder)clientCombinedLeaderboardResponse2._003CDailyChallengeLeaderboard_003Ek__BackingField;
							if ((object)asyncTaskMethodBuilder2 != null)
							{
								List<LeaderboardEntryResponse> list3 = clientCombinedLeaderboardResponse2._003CDailyChallengeLeaderboard_003Ek__BackingField;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+40]");
								bool flag12 = (nint)list3 <= 0;
								asyncTaskMethodBuilder4 = asyncTaskMethodBuilder2;
								if (flag12)
								{
									goto IL_13b5;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
								bool flag13 = (nint)0 == 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
								instance = (AsyncTaskMethodBuilder)0;
								if (!flag13)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
									GameObject gameObject2 = ((Component)0).gameObject;
									bool flag14 = (object)gameObject2 == null;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
									instance = (AsyncTaskMethodBuilder)0;
									if (!flag14)
									{
										gameObject2.SetActive(value: true);
										bool flag15 = (object)asyncTaskMethodBuilder2 == null;
										instance = (AsyncTaskMethodBuilder)gameObject2;
										if (!flag15)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
											bool flag16 = (nint)0 == 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
											instance = (AsyncTaskMethodBuilder)0;
											if (!flag16)
											{
												int index = clientCombinedLeaderboardResponse2._003CDailyChallengeLeaderboard_003Ek__BackingField - 1;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
												((UILeaderboardEntry)0).Init(index, (LeaderboardEntryResponse)(object)clientCombinedLeaderboardResponse);
												goto IL_13d2;
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
				}
				asyncTaskMethodBuilder4 = asyncTaskMethodBuilder2;
				goto IL_13b5;
			}
			throw new NullReferenceException();
			IL_13b5:
			if ((object)asyncTaskMethodBuilder4 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3156 @ rax_v157 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
				bool flag17 = (nint)0 == 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3156 @ rax_v157 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
				instance = (AsyncTaskMethodBuilder)0;
				if (!flag17)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3156 @ rax_v157 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
					GameObject gameObject3 = ((Component)0).gameObject;
					bool flag18 = (object)gameObject3 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3156 @ rax_v157 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+38]");
					instance = (AsyncTaskMethodBuilder)0;
					if (!flag18)
					{
						gameObject3.SetActive(value: false);
						goto IL_13d2;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_03e9:
			List<LeaderboardEntryResponse> list4;
			if (_003Cresult_003E5__2 != null)
			{
				MostRecentCombinedData = _003Cresult_003E5__2;
				nint num5 = (nint)typeof(UILeaderboard);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v771 @ rax_v228 (Il2CppClass<UILeaderboard>)+B8]");
				instance = (AsyncTaskMethodBuilder)((nint)0 + (nint)8);
				if (_003Cresult_003E5__2 != null)
				{
					ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse3 = _003Cresult_003E5__2;
					if ((object)asyncTaskMethodBuilder2 == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+24]");
					if ((nint)0 == 0)
					{
						list4 = clientCombinedLeaderboardResponse3._003CDailyChallengeLeaderboard_003Ek__BackingField;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+24]");
						if ((nint)0 != 1)
						{
							List<LeaderboardEntryResponse> list5 = new List<LeaderboardEntryResponse>();
							list4 = list5;
							instance = (AsyncTaskMethodBuilder)list5;
							goto IL_04cd;
						}
						list4 = clientCombinedLeaderboardResponse3._003CDailyChillLeaderboard_003Ek__BackingField;
					}
					bool flag19 = list4 != null;
					instance = (AsyncTaskMethodBuilder)typeof(UILeaderboard);
					if (!flag19)
					{
						List<LeaderboardEntryResponse> list6 = new List<LeaderboardEntryResponse>();
						list4 = list6;
						instance = (AsyncTaskMethodBuilder)list6;
					}
					goto IL_04cd;
				}
			}
			List<LeaderboardEntryResponse> list7 = new List<LeaderboardEntryResponse>();
			List<LeaderboardEntryResponse> first = list7;
			goto IL_0539;
			IL_10fd:
			if (4294967295L == 1L)
			{
				_003C_003Eu__2 = (TaskAwaiter<ClientCombinedLeaderboardResponse>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<ClientCombinedLeaderboardResponse> taskAwaiter = _003C_003Eu__2;
			}
			else
			{
				Task<ClientCombinedLeaderboardResponse> combinedOnce = GetCombinedOnce();
				if (combinedOnce == null)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<ClientCombinedLeaderboardResponse> awaiter2 = combinedOnce.GetAwaiter();
				TaskAwaiter<ClientCombinedLeaderboardResponse> taskAwaiter = default(TaskAwaiter<ClientCombinedLeaderboardResponse>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 1;
					_003C_003Eu__2 = taskAwaiter;
					instance = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)instance)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					if (1 < 0)
					{
						if ((object)asyncTaskMethodBuilder2 == null)
						{
							throw new NullReferenceException();
						}
						_ = 0;
					}
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse4 = default(ClientCombinedLeaderboardResponse);
			_003Cresult_003E5__2 = clientCombinedLeaderboardResponse4;
			MainThreadAwaitable mainThreadAwaitable3 = Awaitable.MainThreadAsync();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180724280");
			object obj4 = default(object);
			mainThreadAwaitable2 = (MainThreadAwaitable)obj4;
			MainThreadAwaitable awaiter3 = default(MainThreadAwaitable);
			if (awaiter3.IsCompleted)
			{
				goto IL_03e9;
			}
			_003C_003E1__state = 2;
			_003C_003Eu__3 = awaiter3;
			instance = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)instance)->AwaitOnCompleted(ref awaiter3, ref this);
			if (2 < 0)
			{
				if ((object)asyncTaskMethodBuilder2 == null)
				{
					throw new NullReferenceException();
				}
				_ = 0;
			}
			return;
			IL_04cd:
			if ((object)asyncTaskMethodBuilder2 != null)
			{
				List<LeaderboardEntryResponse> source = list4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+40]");
				IEnumerable<LeaderboardEntryResponse> source2 = Enumerable.Take(source, 0);
				List<LeaderboardEntryResponse> list8 = Enumerable.ToList(source2);
				first = list8;
				goto IL_0539;
			}
			throw new NullReferenceException();
			IL_05d6:
			IEnumerable<LeaderboardEntryResponse> source3 = Enumerable.Concat(first, second);
			Func<LeaderboardEntryResponse, int> keySelector = _003C_003Ec._003C_003E9__16_0;
			if (_003C_003Ec._003C_003E9__16_0 == null)
			{
				keySelector = (_003C_003Ec._003C_003E9__16_0 = delegate(LeaderboardEntryResponse x)
				{
					//IL_0035: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (int)ex;
					}
					return x._003CScore_003Ek__BackingField;
				});
				object obj5 = 0;
			}
			IOrderedEnumerable<LeaderboardEntryResponse> source4 = Enumerable.OrderByDescending(source3, keySelector);
			Func<LeaderboardEntryResponse, bool> keySelector2 = _003C_003Ec._003C_003E9__16_1;
			if (_003C_003Ec._003C_003E9__16_1 == null)
			{
				keySelector2 = (_003C_003Ec._003C_003E9__16_1 = delegate(LeaderboardEntryResponse x)
				{
					//IL_0035: Expected I4, but got O
					if (x == null)
					{
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
					return x._003CIsPendingLocal_003Ek__BackingField;
				});
				object obj5 = 0;
			}
			IOrderedEnumerable<LeaderboardEntryResponse> source5 = Enumerable.ThenBy(source4, keySelector2);
			Func<LeaderboardEntryResponse, DateTime> keySelector3 = _003C_003Ec._003C_003E9__16_2;
			if (_003C_003Ec._003C_003E9__16_2 == null)
			{
				keySelector3 = (_003C_003Ec._003C_003E9__16_2 = (LeaderboardEntryResponse x) => (DateTime)(((object?)x?._003CCreatedAtUtc_003Ek__BackingField) ?? ((object)new NullReferenceException())));
				object obj5 = 0;
			}
			IOrderedEnumerable<LeaderboardEntryResponse> orderedEnumerable = Enumerable.ThenBy(source5, keySelector3);
			list2 = Enumerable.ToList(orderedEnumerable);
			if (_003Cresult_003E5__2 != null)
			{
				ClientCombinedLeaderboardResponse clientCombinedLeaderboardResponse5 = _003Cresult_003E5__2;
				bool flag20 = (object)asyncTaskMethodBuilder2 == null;
				instance = (AsyncTaskMethodBuilder)orderedEnumerable;
				if (flag20)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+24]");
				if ((nint)0 == 0)
				{
					clientCombinedLeaderboardResponse2 = (ClientCombinedLeaderboardResponse)(object)clientCombinedLeaderboardResponse5._003CDailyChallengeSelf_003Ek__BackingField;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+24]");
					clientCombinedLeaderboardResponse2 = (ClientCombinedLeaderboardResponse)(object)(((nint)0 != 1) ? null : clientCombinedLeaderboardResponse5._003CDailyChillSelf_003Ek__BackingField);
				}
				bool flag21 = clientCombinedLeaderboardResponse2 == null;
				enumerable = (IEnumerable<LeaderboardEntryResponse>)typeof(UILeaderboard);
				if (!flag21)
				{
					clientCombinedLeaderboardResponse = (ClientCombinedLeaderboardResponse)(object)clientCombinedLeaderboardResponse2._003CDailyChillLeaderboard_003Ek__BackingField;
					enumerable = (IEnumerable<LeaderboardEntryResponse>)typeof(UILeaderboard);
					goto IL_134f;
				}
			}
			else
			{
				clientCombinedLeaderboardResponse2 = null;
				enumerable = orderedEnumerable;
			}
			clientCombinedLeaderboardResponse = null;
			goto IL_134f;
			IL_13d2:
			Component component = default(Component);
			while (true)
			{
				bool flag22 = (object)asyncTaskMethodBuilder2 == null;
				instance = asyncTaskMethodBuilder2;
				if (!flag22)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
					bool flag23 = (nint)0 == 0;
					instance = asyncTaskMethodBuilder2;
					if (!flag23)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2969 @ rdx_v73+18]");
						if ((nint)0 <= (nint)list2._size)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2969 @ rdx_v73+18]");
						object obj7 = -1;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag24 = (object)component == null;
						instance = (AsyncTaskMethodBuilder)component;
						if (!flag24)
						{
							GameObject gameObject4 = component.gameObject;
							UnityEngine.Object.Destroy(gameObject4);
							bool flag25 = (object)asyncTaskMethodBuilder2 == null;
							instance = (AsyncTaskMethodBuilder)gameObject4;
							if (!flag25)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								object obj8 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
								bool flag26 = (nint)0 == 0;
								instance = (AsyncTaskMethodBuilder)gameObject4;
								if (!flag26)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2774 @ rdx_v79+18]");
									int index2 = (int)(-1);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ stack_-128_v37 (System.Runtime.CompilerServices.AsyncTaskMethodBuilder)+48]");
									((List<UILeaderboardEntry>)0).RemoveAt(index2);
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
			_003Cresult_003E5__2 = null;
			object obj9 = (object)(&obj);
			if ((nint)obj9 < 0)
			{
				object obj10 = (object)(&asyncTaskMethodBuilder2);
				instance = (AsyncTaskMethodBuilder)obj10;
				if (obj10 == null)
				{
					throw new NullReferenceException();
				}
				_ = 0;
			}
			goto IL_10e9;
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

	private const int DefaultLeaderboardCount = 20;

	public bool HasRun;

	public Gamemodes LeaderboardGamemode;

	public UILeaderboardEntry Prefab_Entry;

	public Transform Transform_ListRoot;

	public UILeaderboardEntry Entry_Self;

	public int EntryCount = 19;

	public static bool FetchInProgress;

	public static ClientCombinedLeaderboardResponse MostRecentCombinedData = null;

	private static readonly object FetchLock;

	private static Task<ClientCombinedLeaderboardResponse> fetchTask;

	private List<UILeaderboardEntry> spawnedUIEntries;

	private bool refreshInProgress;

	private unsafe void Start()
	{
		//IL_001c: Expected O, but got Ref
		//IL_0024: Expected O, but got Ref
		//IL_02a8: Expected O, but got Ref
		//IL_0105: Expected O, but got I4
		//IL_032c: Expected I, but got O
		//IL_0349: Expected I, but got O
		//IL_0357: Expected I, but got O
		//IL_00b2: Expected O, but got I
		//IL_00bb: Expected O, but got I4
		//IL_013a: Expected O, but got I
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0170: Expected I, but got O
		//IL_0180: Expected O, but got I
		//IL_01b4: Expected I, but got O
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_01d2: Expected O, but got I
		//IL_0207: Expected I, but got O
		IEnumerator enumerator = Transform_ListRoot.GetEnumerator();
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj4 = default(object);
		object obj3 = (object)(&obj4);
		object obj5 = default(object);
		object obj16 = default(object);
		object obj17 = default(object);
		object obj18 = default(object);
		Component component = default(Component);
		Transform transform2;
		while (true)
		{
			object obj7;
			object obj15;
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj5 != null)
				{
					bool flag = obj2 == null;
					Transform transform = null;
					if (flag)
					{
						goto IL_02a9;
					}
					object obj6 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ r10_v7+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00f2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ r10_v7+B0]");
					obj7 = 0;
					object obj8 = 0;
					while (true)
					{
						object obj9 = obj8 + obj8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v485 @ r8_v11+v368 @ rax_v38*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						obj8++;
						object obj10 = obj8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ r10_v7+12E]");
						if ((nint)obj10 < 0)
						{
							continue;
						}
						goto IL_00f2;
					}
					object obj11 = obj8 + obj8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v485 @ r8_v11+8+v478 @ rcx_v29*8]");
					object obj12 = (nint)0 + (nint)1;
					object obj13 = obj12 << 4;
					object obj14 = obj13 + 312;
					obj15 = obj14 + obj6;
					goto IL_0314;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				obj3 = obj16;
				if (obj16 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				GameObject gameObject = Entry_Self.gameObject;
				gameObject.SetActive(value: false);
				CleanupImageCache((TimeSpan?)(object)(&obj17));
				return;
			}
			throw new NullReferenceException();
			IL_02a9:
			throw new NullReferenceException();
			IL_00f2:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj7 = 1;
			obj15 = obj18;
			goto IL_0314;
			IL_0314:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v486 @ rdx_v17] (should have been resolved before IL gen)");
			nint num = (nint)typeof(Transform);
			bool flag2 = (object)component == null;
			nint num2 = (nint)typeof(IEnumerator);
			nint num3 = (nint)typeof(Transform);
			if (flag2)
			{
				break;
			}
			num2 = (nint)component;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rcx_v21 (Il2CppClass<UnityEngine.Transform>)+130]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r9_v6 (Il2CppClass<System.Collections.IEnumerator>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ rcx_v21 (Il2CppClass<UnityEngine.Transform>)+130]");
			bool flag3 = num4 < 0;
			transform2 = (Transform)component;
			num3 = (nint)typeof(Transform);
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r9_v6 (Il2CppClass<System.Collections.IEnumerator>)+C8]");
				object obj20 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ rax_v29+FFFFFFF8+v175 @ rax_v28*8]");
				bool flag4 = 0 != (nint)typeof(Transform);
				transform2 = (Transform)component;
				num3 = (nint)typeof(Transform);
				if (!flag4)
				{
					GameObject obj21 = component.gameObject;
					UnityEngine.Object.Destroy(obj21);
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_02a9;
		}
		transform2 = (Transform)component;
		throw new NullReferenceException();
	}

	public static void RefreshAll(bool force)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0034: Expected O, but got I4
		//IL_003e: Expected O, but got I4
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		UILeaderboard[] array = UnityEngine.Object.FindObjectsByType<UILeaderboard>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		object obj = array + 32;
		object obj2 = 0;
		for (object obj3 = 0; (nint)obj3 < array.Length; obj2++, obj += 8, obj3 = obj2)
		{
			object obj4 = obj;
			if (!force)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v7+20]");
				if ((nint)0 != (force ? 1 : 0))
				{
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18059B190");
		}
	}

	public void RefreshNow()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CRefresh_003Ed__16 stateMachine = default(_003CRefresh_003Ed__16);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		Task task = asyncTaskMethodBuilder.Task;
	}

	public Task Refresh()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CRefresh_003Ed__16 stateMachine = default(_003CRefresh_003Ed__16);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}

	private static Task<ClientCombinedLeaderboardResponse> GetCombinedOnce()
	{
		object obj = default(object);
		bool lockTaken = default(bool);
		Monitor.Enter(obj, ref lockTaken);
		if (fetchTask != null)
		{
			if (fetchTask == null)
			{
				return (Task<ClientCombinedLeaderboardResponse>)(object)new NullReferenceException();
			}
			if (!fetchTask.IsCompleted)
			{
				goto IL_00d4;
			}
		}
		FetchInProgress = true;
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>.Create();
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>);
		_003CFetchCombined_003Ed__18 stateMachine = default(_003CFetchCombined_003Ed__18);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		Task<ClientCombinedLeaderboardResponse> task = asyncTaskMethodBuilder2.Task;
		fetchTask = task;
		goto IL_00d4;
		IL_00d4:
		if (lockTaken)
		{
			Monitor.Exit(obj);
		}
		return fetchTask;
	}

	private static Task<ClientCombinedLeaderboardResponse> FetchCombined()
	{
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>.Create();
		AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse> asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder<ClientCombinedLeaderboardResponse>);
		_003CFetchCombined_003Ed__18 stateMachine = default(_003CFetchCombined_003Ed__18);
		asyncTaskMethodBuilder2.Start(ref stateMachine);
		return asyncTaskMethodBuilder2.Task;
	}

	private static List<LeaderboardEntryResponse> GetEntries(ClientCombinedLeaderboardResponse data, Gamemodes gamemode)
	{
		List<LeaderboardEntryResponse> list;
		if (gamemode == Gamemodes.Challange)
		{
			if (data == null)
			{
				goto IL_009f;
			}
			list = data._003CDailyChallengeLeaderboard_003Ek__BackingField;
		}
		else
		{
			if (gamemode != Gamemodes.Chill)
			{
				goto IL_0034;
			}
			if (data == null)
			{
				goto IL_009f;
			}
			list = data._003CDailyChillLeaderboard_003Ek__BackingField;
		}
		if (list == null)
		{
			goto IL_0034;
		}
		goto IL_004b;
		IL_009f:
		return (List<LeaderboardEntryResponse>)(object)new NullReferenceException();
		IL_0034:
		List<LeaderboardEntryResponse> list2 = new List<LeaderboardEntryResponse>();
		list = list2;
		goto IL_004b;
		IL_004b:
		return list;
	}

	private static GetMyLeaderboardResponse GetSelf(ClientCombinedLeaderboardResponse data, Gamemodes gamemode)
	{
		switch (gamemode)
		{
		case Gamemodes.Challange:
			if (data != null)
			{
				return data._003CDailyChallengeSelf_003Ek__BackingField;
			}
			break;
		case Gamemodes.Chill:
			if (data != null)
			{
				return data._003CDailyChillSelf_003Ek__BackingField;
			}
			break;
		default:
			return null;
		}
		return (GetMyLeaderboardResponse)(object)new NullReferenceException();
	}

	public unsafe static void CleanupImageCache(TimeSpan? maxAge = null)
	{
		//IL_0059: Expected O, but got Ref
		//IL_0125: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407CE0");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj = default(object);
		TimeSpan timeSpan3 = default(TimeSpan);
		if (obj == null)
		{
			TimeSpan timeSpan = TimeSpan.FromDays(2.0);
			TimeSpan? timeSpan2 = (TimeSpan)(&timeSpan3);
			TimeSpan? timeSpan4 = timeSpan2;
		}
		string persistentDataPath = Application.persistentDataPath;
		string path = Path.Combine(persistentDataPath, "ImageCache");
		if (!Directory.Exists(path))
		{
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
		DateTime dateTime = utcNow - timeSpan3;
		IEnumerable<string> enumerable = Directory.EnumerateFiles(path);
		if (enumerable != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj3 = default(object);
			object obj2 = (object)(&obj3);
			string text = null;
			object obj4 = default(object);
			string path2 = default(string);
			while (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					DateTime lastAccessTimeUtc = File.GetLastAccessTimeUtc(path2);
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(path2);
					bool flag = lastAccessTimeUtc > lastWriteTimeUtc;
					bool flag2 = !flag;
					DateTime dateTime2 = lastWriteTimeUtc;
					if (!flag2)
					{
						dateTime2 = lastAccessTimeUtc;
					}
					if (dateTime2 < dateTime)
					{
						File.Delete(path2);
					}
					continue;
				}
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public UILeaderboard()
	{
		List<UILeaderboardEntry> list = new List<UILeaderboardEntry>();
		spawnedUIEntries = list;
		base._002Ector();
	}

	static UILeaderboard()
	{
		object fetchLock = new object();
		FetchLock = fetchLock;
	}
}
