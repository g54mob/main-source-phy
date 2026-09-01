using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration;
using Newtonsoft.Json;
using SleepyNodes;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
	[Serializable]
	public class LocalSubmissionQueue
	{
		public List<LocalSubmission> Submissions;

		public LocalSubmissionQueue()
		{
			List<LocalSubmission> submissions = new List<LocalSubmission>();
			Submissions = submissions;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
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

	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public TaskCompletionSource<string> tcs;

		internal void _003CGetSteamAvatarBase64_003Eb__0(Texture2D tex)
		{
			if (tex != null)
			{
				byte[] inArray = ImageConversion.EncodeToJPG(tex, 80);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string message = $"[Leaderboard] Avatar Size: {arg}";
				Debug.Log(message);
				string result = Convert.ToBase64String(inArray);
				bool flag = tcs.TrySetResult(result);
			}
			else
			{
				Debug.LogError("[Leaderboard] No Avatar Loaded");
				bool flag2 = tcs.TrySetResult(null);
			}
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CLeaderboard_CompleteRun_003Ed__25 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		public Action onCompleted;

		private PostLeaderboardScoreRequest _003Csubmission_003E5__2;

		private byte[] _003CzipBytes_003E5__3;

		private TaskAwaiter<PostLeaderboardScoreResponse> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_0c40: Expected O, but got I4
			//IL_00fc: Expected O, but got Ref
			//IL_010f: Expected O, but got Ref
			//IL_0151: Expected O, but got I4
			//IL_016a: Expected I4, but got I8
			//IL_0173: Expected O, but got I4
			//IL_0ced: Expected I, but got O
			//IL_09b2: Expected O, but got Ref
			//IL_0cb8: Expected I4, but got I8
			//IL_0cd1: Expected O, but got Ref
			//IL_0055: Expected O, but got I
			//IL_0f06: Expected O, but got Ref
			//IL_00c5: Expected I, but got O
			//IL_0e2c: Expected O, but got I
			//IL_027b: Expected I, but got O
			//IL_0284: Expected O, but got I4
			//IL_03d5: Expected O, but got I
			//IL_03e3: Expected O, but got Ref
			//IL_03f8: Expected O, but got I
			//IL_0add: Expected O, but got I4
			//IL_04eb: Expected I, but got O
			//IL_0436: Expected O, but got I
			//IL_0386: Expected I, but got O
			//IL_052e: Expected O, but got I
			//IL_0e64: Expected O, but got I
			//IL_0e64: Expected O, but got I
			//IL_0471: Expected O, but got I
			//IL_047f: Expected O, but got Ref
			//IL_04d8: Expected O, but got Ref
			//IL_0ebd: Expected O, but got I
			//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c1: Expected O, but got Unknown
			//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d6: Expected O, but got Unknown
			//IL_0676: Expected O, but got I
			//IL_06e6: Expected O, but got Ref
			//IL_072c: Expected O, but got I
			//IL_073c: Expected O, but got I
			//IL_0767: Expected O, but got I4
			//IL_079e: Expected O, but got I4
			//IL_07cd: Expected O, but got I
			//IL_07dd: Expected O, but got I
			//IL_0808: Expected O, but got I4
			//IL_083f: Expected O, but got I4
			//IL_0863: Expected O, but got I
			//IL_0871: Expected O, but got Ref
			//IL_0897: Expected native int or pointer, but got O
			//IL_08aa: Expected O, but got Ref
			//IL_08b8: Expected O, but got Ref
			//IL_08eb: Expected O, but got I4
			//IL_0943: Expected O, but got I4
			//IL_0996: Expected O, but got I4
			//IL_0a9f: Expected O, but got Ref
			//IL_0ac0: Expected O, but got Ref
			TaskAwaiter<PostLeaderboardScoreResponse> awaiter = default(TaskAwaiter<PostLeaderboardScoreResponse>);
			TaskAwaiter<PostLeaderboardScoreResponse> taskAwaiter = (TaskAwaiter<PostLeaderboardScoreResponse>)(&awaiter);
			_ = 0;
			_ = 0;
			taskAwaiter = (TaskAwaiter<PostLeaderboardScoreResponse>)0;
			_ = 0;
			_ = _003C_003E1__state;
			_ = _003C_003E4__this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+120]");
			if ((nint)0 == 0)
			{
				goto IL_00e8;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
			nint num2 = default(nint);
			if (num != 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v189+50]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v210 @ rax_v189+30]");
					if ((nint)0 != 0)
					{
						_ = 1;
						_003Csubmission_003E5__2 = null;
						_003CzipBytes_003E5__3 = null;
						num2 = unchecked((nint)null);
						goto IL_00e8;
					}
					Debug.LogError("[Leaderboard] No CurrentRun");
				}
			}
			goto IL_0ca9;
			IL_09a4:
			RenderTexture renderTexture = (RenderTexture)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 16));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			Task<PostLeaderboardScoreResponse> task = taskAwaiter.m_task;
			bool flag = taskAwaiter.m_task == null;
			nint num3 = 0;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rax_v67 (System.Threading.Tasks.Task`1<PostLeaderboardScoreResponse>)+10]");
				bool flag2 = (nint)0 == 0;
				num3 = 0;
				if (!flag2)
				{
					Debug.Log("[Leaderboard] Submitted");
					Action action = onCompleted;
					bool flag3 = onCompleted == null;
					nint num4 = 0;
					if (!flag3)
					{
						num4 = ((Delegate)action).method;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v660.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
					}
					object obj2 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 64));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180582E40");
					goto IL_0ca9;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			InvalidOperationException ex = new InvalidOperationException("Score submission was not accepted.");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			bool flag4 = false;
			byte[] array2 = default(byte[]);
			byte[] array = array2;
			throw ex;
			IL_00e8:
			_ = 0;
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 288));
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 296));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+120]");
			if ((nint)0 == 0)
			{
				taskAwaiter = _003C_003Eu__1;
				_003C_003Eu__1 = (TaskAwaiter<PostLeaderboardScoreResponse>)0;
				_ = 4294967295L;
				_003C_003E1__state = -1;
				Guid guid = (Guid)0;
				goto IL_09a4;
			}
			Debug.Log("[Leaderboard] Taking Screenshot");
			nint num5 = (nint)typeof(MissionManager);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v374 @ rax_v78 (Il2CppClass<MissionManager>)+B8]");
			nint num6 = 0;
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			byte[] array5;
			if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
			{
				MissionGraph missionGraph = missionManager._003CCurrentMission_003Ek__BackingField;
				if ((object)missionManager._003CCurrentMission_003Ek__BackingField != null)
				{
					bool flag5 = missionGraph.MissionType != MissionGraph.MissionTypes.Challange;
					byte[] array3 = null;
					if (!flag5)
					{
						ReplayManager instance = ReplayManager.Instance;
						bool flag6 = (object)ReplayManager.Instance == null;
						array3 = null;
						if (!flag6)
						{
							bool flag7 = ReplayManager.Instance.CanRecordFrame();
							bool flag8 = !flag7;
							array3 = null;
							if (!flag8)
							{
								byte[] array4;
								if (ReplayManager.Instance.CanRecordFrame())
								{
									renderTexture = instance.cameraOutput;
									array4 = ReplayManager.CaptureToBytes(instance.RenderCam, instance.cameraOutput, instance.destinationTexture);
									num2 = (nint)instance.destinationTexture;
									object obj5 = 0;
								}
								else
								{
									renderTexture = null;
									array4 = null;
								}
								if (instance.frames == null)
								{
									throw new NullReferenceException();
								}
								instance.frames.Add(array4);
								num2 = 0;
								array3 = array4;
							}
						}
					}
					MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
					if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
					{
						MissionGraph missionGraph2 = missionManager2._003CCurrentMission_003Ek__BackingField;
						if ((object)missionManager2._003CCurrentMission_003Ek__BackingField != null)
						{
							if (missionGraph2.MissionType == MissionGraph.MissionTypes.Challange)
							{
								ReplayManager instance2 = ReplayManager.Instance;
								if ((object)ReplayManager.Instance != null)
								{
									IReadOnlyList<byte[]> frames = instance2.frames;
									bool flag9 = instance2.frames == null;
									renderTexture = (RenderTexture)(object)array3;
									if (flag9)
									{
										Guid guid = (Guid)0;
										num3 = num2;
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v519 @ rax_v170 (System.Collections.Generic.IReadOnlyList`1<System.Byte[]>)+18]");
									if ((nint)0 > (nint)0)
									{
										array5 = ReplayManager.CreateFrameZip(instance2.frames);
										num2 = unchecked((nint)null);
										goto IL_0dd2;
									}
								}
							}
							array5 = null;
							goto IL_0dd2;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_0e86:
			bool flag11;
			bool flag10 = !flag11;
			bool flag12 = !flag10;
			PostLeaderboardScoreRequest postLeaderboardScoreRequest;
			postLeaderboardScoreRequest._003CClientTampered_003Ek__BackingField = flag12;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1363 @ rdx_v44+30]");
				postLeaderboardScoreRequest._003CRunData_003Ek__BackingField = (LeaderboardRunData)0;
				postLeaderboardScoreRequest._003CImageExtension_003Ek__BackingField = "jpg";
				if ((object)PerformanceTracker.Instance != null)
				{
					string text = PerformanceTracker.Instance.CaptureJson(resetAfterCapture: true);
					postLeaderboardScoreRequest._003CPerformanceStatsJson_003Ek__BackingField = text;
					_003Csubmission_003E5__2 = postLeaderboardScoreRequest;
					InvalidOperationException ex2 = (InvalidOperationException)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 56));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
					if ((nint)0 != 0)
					{
						nint num7 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
						object obj7 = (nint)0 + (nint)56;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1647 @ rdx_v50 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
						array = (byte[])0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						object obj8 = default(object);
						bool flag13 = obj8 == null;
						flag4 = false;
						object obj5 = 0;
						if (!flag13)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
							bool flag14 = (nint)0 == 0;
							flag4 = false;
							obj5 = 0;
							if (flag14)
							{
								throw new NullReferenceException();
							}
							nint num8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
							object obj9 = (nint)0 + (nint)56;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1663 @ rdx_v52 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
							array = (byte[])0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
							object obj10 = default(object);
							bool flag15 = obj10 == null;
							flag4 = false;
							obj5 = 0;
							if (!flag15)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
								bool flag16 = (nint)0 == 0;
								flag4 = false;
								obj5 = 0;
								if (flag16)
								{
									throw new NullReferenceException();
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
								object obj11 = (nint)0 + (nint)56;
								object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 16));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
								Guid guid = Guid.Empty;
								_ = Guid.Empty;
								System.Runtime.CompilerServices.Unsafe.Write(&((TaskAwaiter<PostLeaderboardScoreResponse>*)(nint)taskAwaiter)->m_task, taskAwaiter.m_task);
								array = (byte[])System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 128));
								object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 16));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
								object obj14 = default(object);
								bool flag17 = obj14 != null;
								flag4 = false;
								obj5 = 0;
								if (!flag17)
								{
									Debug.Log("[Leaderboard] Submitting");
									Task<PostLeaderboardScoreResponse> task2 = LeaderboardClient.SubmitScore(_003Csubmission_003E5__2, _003CzipBytes_003E5__3);
									bool flag18 = task2 == null;
									flag4 = false;
									obj5 = 0;
									array = _003CzipBytes_003E5__3;
									if (!flag18)
									{
										TaskAwaiter<PostLeaderboardScoreResponse> awaiter2 = task2.GetAwaiter();
										taskAwaiter = awaiter2;
										bool isCompleted = awaiter.IsCompleted;
										bool flag19 = !isCompleted;
										obj5 = 0;
										if (!flag19)
										{
											goto IL_09a4;
										}
										_ = 0;
										_003C_003E1__state = 0;
										_003C_003Eu__1 = taskAwaiter;
										AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
										((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
										AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 64));
										((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder2)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
										return;
									}
									throw new NullReferenceException();
								}
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						InvalidOperationException ex3 = new InvalidOperationException("No session key");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex3;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_0dd2:
			_003CzipBytes_003E5__3 = array5;
			postLeaderboardScoreRequest = new PostLeaderboardScoreRequest();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC77]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			postLeaderboardScoreRequest._003CImageExtension_003Ek__BackingField = "jpg";
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1202 @ rax_v93+38]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1202 @ rax_v93+48]");
				_ = 0;
				nint num9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1300 @ rdx_v41 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
				renderTexture = (RenderTexture)0;
				List<byte[]> list = (List<byte[]>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 80));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1300 @ rdx_v41 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
				list.Add((byte[])0);
				object obj16 = default(object);
				Guid guid;
				if (obj16 != null)
				{
					nint num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1450 @ rcx_v94 (Il2CppClass<System.Guid>)+FC]");
					object obj17 = (nint)0 + (nint)15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1450 @ rcx_v94 (Il2CppClass<System.Guid>)+FC]");
					if ((nint)obj17 > 0)
					{
						nint num11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1300 @ rdx_v41 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
						((List<byte[]>)num11).Add((byte[])0);
					}
					nint num12 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1553 @ rcx_v96 (Il2CppClass<System.Nullable`1<System.Guid>>)+80]");
					byte[] item = (byte[])((nint)0 + (nint)32);
					List<byte[]> list2 = (List<byte[]>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 80));
					list2.Add(item);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					nint num13 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref awaiter, 16));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					guid = (Guid)taskAwaiter.m_task;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1450 @ rcx_v94 (Il2CppClass<System.Guid>)+FC]");
					num3 = 0;
					renderTexture = (RenderTexture)(&awaiter);
				}
				else
				{
					nint num14 = (nint)typeof(Guid);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1403 @ rax_v147 (Il2CppClass<System.Guid>)+B8]");
					nint num13 = 0;
					guid = Guid.Empty;
					num3 = num2;
				}
				if (postLeaderboardScoreRequest != null)
				{
					postLeaderboardScoreRequest._003CSessionId_003Ek__BackingField = guid;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
					InvalidOperationException ex2 = (InvalidOperationException)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1 (System.Runtime.CompilerServices.TaskAwaiter`1<PostLeaderboardScoreResponse>)+128]");
					if ((nint)0 != 0)
					{
						ex2 = (InvalidOperationException)(object)((Exception)ex2)._helpURL;
						if (((Exception)ex2)._helpURL != null)
						{
							if (((Exception)ex2)._stackTrace == null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1367 @ rcx_v65 (System.InvalidOperationException)+24]");
								object obj18 = 0 - ((Exception)ex2)._innerException;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1367 @ rcx_v65 (System.InvalidOperationException)+2C]");
								object obj19 = obj18 ^ 0;
								object obj20 = obj19 - (object)((Exception)ex2)._helpURL;
								object obj21 = ~obj20;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1367 @ rcx_v65 (System.InvalidOperationException)+34]");
								if (0 == (nint)obj21)
								{
									MissionStatsTracker instance3 = MissionStatsTracker.Instance;
									if ((object)MissionStatsTracker.Instance != null)
									{
										flag11 = instance3.requisitionPointsTampered;
										goto IL_0e86;
									}
									throw new NullReferenceException();
								}
							}
							flag11 = true;
							goto IL_0e86;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			IL_0ca9:
			_003C_003E1__state = -2;
			_003Csubmission_003E5__2 = null;
			_003CzipBytes_003E5__3 = null;
			AsyncVoidMethodBuilder asyncVoidMethodBuilder3 = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder3)->SetResult();
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

	[StructLayout((LayoutKind)3)]
	private struct _003CLeaderboard_StartRun_003Ed__24 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public LeaderboardManager _003C_003E4__this;

		public Gamemodes gamemode;

		private TaskAwaiter<GetSessionKeyResponse> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_00cb: Expected O, but got I4
			//IL_00da: Expected I4, but got I8
			//IL_0191: Expected O, but got Ref
			//IL_02b7: Expected I4, but got I8
			//IL_02c2: Expected O, but got Ref
			//IL_0202: Expected O, but got Ref
			LeaderboardManager leaderboardManager = _003C_003E4__this;
			if (_003C_003E1__state != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
				if (num == 1)
				{
					goto IL_02a8;
				}
				Debug.Log("[Leaderboard] Starting Run");
				LeaderboardRunData leaderboardRunData = new LeaderboardRunData();
				List<LeaderboardRunData.ActionData> actions = new List<LeaderboardRunData.ActionData>();
				leaderboardRunData.Actions = actions;
				leaderboardManager.CurrentRun = leaderboardRunData;
				leaderboardManager.CurrentRun.Score = 0;
				leaderboardManager.currentGamemode = gamemode;
			}
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<GetSessionKeyResponse>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<GetSessionKeyResponse> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				if ((object)PerformanceTracker.Instance == null)
				{
					throw new NullReferenceException();
				}
				string performanceJson = PerformanceTracker.Instance.CaptureJson(resetAfterCapture: true, includeDeviceInfo: true);
				Task<GetSessionKeyResponse> sessionKey = LeaderboardClient.GetSessionKey(gamemode, performanceJson);
				TaskAwaiter<GetSessionKeyResponse> awaiter = sessionKey.GetAwaiter();
				TaskAwaiter<GetSessionKeyResponse> taskAwaiter = default(TaskAwaiter<GetSessionKeyResponse>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object obj = default(object);
			Guid? sessionId = (leaderboardManager.currentSessionId = (Guid)(&obj));
			_ = 0;
			LeaderboardRunData currentRun = leaderboardManager.CurrentRun;
			currentRun.SessionId = sessionId;
			_ = 0;
			Debug.Log("[Leaderboard] Got Session");
			goto IL_02a8;
			IL_02a8:
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

	[StructLayout((LayoutKind)3)]
	private struct _003CPushOperationState_003Ed__23 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public OperationState state;

		private TaskAwaiter<bool> _003C_003Eu__1;

		private unsafe void MoveNext()
		{
			//IL_006a: Expected O, but got I4
			//IL_0079: Expected I4, but got I8
			//IL_019e: Expected I4, but got I8
			//IL_01a9: Expected O, but got Ref
			//IL_0124: Expected O, but got Ref
			if (_003C_003E1__state != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
				if (num == 1)
				{
					goto IL_018f;
				}
			}
			if (_003C_003E1__state == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter<bool>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<bool> taskAwaiter = _003C_003Eu__1;
			}
			else
			{
				PostOperationStateRequest postOperationStateRequest = new PostOperationStateRequest();
				postOperationStateRequest._003COperationState_003Ek__BackingField = state;
				Task<bool> task = LeaderboardClient.PushOperationState(postOperationStateRequest);
				TaskAwaiter<bool> awaiter = task.GetAwaiter();
				TaskAwaiter<bool> taskAwaiter = default(TaskAwaiter<bool>);
				if (!taskAwaiter.IsCompleted)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = taskAwaiter;
					AsyncVoidMethodBuilder asyncVoidMethodBuilder = (AsyncVoidMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncVoidMethodBuilder*)asyncVoidMethodBuilder)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			goto IL_018f;
			IL_018f:
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

	[StructLayout((LayoutKind)3)]
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

		private unsafe void MoveNext()
		{
			//IL_0015: Expected O, but got I4
			//IL_00ce: Expected O, but got I4
			//IL_00dd: Expected I4, but got I8
			//IL_00ea: Expected I4, but got I8
			//IL_0124: Expected O, but got I4
			//IL_0133: Expected I4, but got I8
			//IL_0140: Expected I4, but got I8
			//IL_0587: Expected O, but got I4
			//IL_0596: Expected I4, but got I8
			//IL_061a: Expected O, but got I4
			//IL_05d0: Expected O, but got I4
			//IL_05df: Expected I4, but got I8
			//IL_08aa: Expected I4, but got I8
			//IL_0078: Expected O, but got I4
			//IL_037e: Expected O, but got Ref
			//IL_06e4: Expected O, but got I
			//IL_0810: Expected O, but got Ref
			//IL_0090: Expected I, but got O
			//IL_0099: Expected O, but got I4
			//IL_04ba: Expected I, but got O
			//IL_03b3: Expected I, but got O
			//IL_03b8: Expected I, but got O
			//IL_0687: Expected O, but got I4
			//IL_07f2: Expected O, but got Ref
			//IL_04a0: Expected I, but got O
			//IL_0520: Expected O, but got Ref
			//IL_02ec: Expected O, but got I4
			//IL_0346: Expected O, but got Ref
			//IL_0572: Expected O, but got Ref
			//IL_0577: Expected I, but got O
			//IL_07a6: Expected O, but got Ref
			int num = _003C_003E1__state;
			long? num4;
			nint num3 = default(nint);
			if (_003C_003E1__state > 1)
			{
				object obj = _003C_003E1__state - 2;
				bool flag = (nint)obj <= 1;
				string text = null;
				if (flag)
				{
					goto IL_084b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				int num2 = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
				if (num2 == 1)
				{
					goto IL_089b;
				}
				_003CsteamId_003E5__2 = (long?)(object)0;
				_003Cusername_003E5__3 = null;
				_003CavatarBase64_003E5__4 = null;
				num3 = unchecked((nint)null);
				num4 = (long?)(object)0;
			}
			TaskAwaiter awaiter = default(TaskAwaiter);
			if (num == 0)
			{
				_003C_003Eu__1 = (TaskAwaiter)0;
				_003C_003E1__state = -1;
				num = -1;
				awaiter = _003C_003Eu__1;
			}
			else
			{
				if (num == 1)
				{
					_003C_003Eu__2 = (TaskAwaiter<string>)0;
					_003C_003E1__state = -1;
					num = -1;
					goto IL_0302;
				}
				Task task = Task.Delay(1000);
				TaskAwaiter awaiter2 = task.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj2 = default(object);
				if (obj2 == null)
				{
					_003C_003E1__state = 0;
					_003C_003Eu__1 = awaiter;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			UserData me = UserData.Me;
			if ((object)me == null)
			{
				Debug.LogError("[Leaderboard] No Steam User");
				string text = null;
				goto IL_0901;
			}
			UserData userData = default(UserData);
			long? num5 = (_003CsteamId_003E5__2 = (nint)(&userData));
			UserData userData2 = default(UserData);
			string name = userData2.Name;
			if (!string.IsNullOrWhiteSpace(name))
			{
				string name2 = userData2.Name;
				_003Cusername_003E5__3 = name2;
			}
			_003C_003Ec__DisplayClass21_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass21_0();
			Debug.Log("[Leaderboard] Requesting avatar");
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			CS_0024_003C_003E8__locals4.tcs = tcs;
			Action<Texture2D> callback = delegate(Texture2D tex)
			{
				if (tex != null)
				{
					byte[] inArray2 = ImageConversion.EncodeToJPG(tex, 80);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg3 = default(object);
					string message2 = $"[Leaderboard] Avatar Size: {arg3}";
					Debug.Log(message2);
					string result = Convert.ToBase64String(inArray2);
					bool flag10 = CS_0024_003C_003E8__locals4.tcs.TrySetResult(result);
				}
				else
				{
					Debug.LogError("[Leaderboard] No Avatar Loaded");
					bool flag11 = CS_0024_003C_003E8__locals4.tcs.TrySetResult(null);
				}
			};
			UserData userData3 = default(UserData);
			userData3.LoadAvatar(callback);
			TaskCompletionSource<string> tcs2 = CS_0024_003C_003E8__locals4.tcs;
			TaskAwaiter<string> awaiter3 = tcs2._task.GetAwaiter();
			TaskAwaiter<string> awaiter4 = default(TaskAwaiter<string>);
			bool isCompleted = awaiter4.IsCompleted;
			bool flag2 = !isCompleted;
			Color color = (Color)0;
			num4 = num5;
			if (!flag2)
			{
				goto IL_0302;
			}
			_003C_003E1__state = 1;
			_003C_003Eu__2 = awaiter4;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder2)->AwaitUnsafeOnCompleted(ref awaiter4, ref this);
			return;
			IL_0901:
			string text2 = PlayerPrefs.GetString("username");
			nint num6;
			if (!string.IsNullOrWhiteSpace(text2))
			{
				bool flag3 = text2.StartsWith("Player-");
				bool flag4 = !flag3;
				num3 = unchecked((nint)null);
				num6 = unchecked((nint)null);
				if (flag4)
				{
					goto IL_03f6;
				}
			}
			bool flag5 = string.IsNullOrWhiteSpace(_003Cusername_003E5__3);
			bool flag6 = !flag5;
			num6 = num3;
			if (!flag6)
			{
				goto IL_03f6;
			}
			PlayerPrefs.SetString("username", _003Cusername_003E5__3);
			num6 = unchecked((nint)null);
			goto IL_04bf;
			IL_0302:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			string text3 = default(string);
			_003CavatarBase64_003E5__4 = text3;
			num3 = 0;
			goto IL_0901;
			IL_03f6:
			if (!string.IsNullOrWhiteSpace(text2))
			{
				_003Cusername_003E5__3 = text2;
			}
			if (string.IsNullOrWhiteSpace(_003Cusername_003E5__3))
			{
				int num7 = UnityEngine.Random.Range(100000, 999999);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				string text4 = $"Operator-{arg}";
				_003Cusername_003E5__3 = text4;
				PlayerPrefs.SetString("username", _003Cusername_003E5__3);
				num6 = unchecked((nint)null);
			}
			goto IL_04bf;
			IL_04bf:
			bool flag7 = string.IsNullOrWhiteSpace(_003CavatarBase64_003E5__4);
			bool flag8 = !flag7;
			num3 = num6;
			if (!flag8)
			{
				Debug.LogError("[Leaderboard] No Base64 Avatar - Using Default");
				bool mipChain = default(bool);
				Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGB24, mipChain);
				Color gray = Color.gray;
				float num8 = default(float);
				texture2D.SetPixel(0, 0, (Color)(&num8));
				texture2D.Apply();
				byte[] inArray = ImageConversion.EncodeToJPG(texture2D, 80);
				string text5 = Convert.ToBase64String(inArray);
				UnityEngine.Object.Destroy(texture2D);
				_003CavatarBase64_003E5__4 = text5;
				color = (Color)(&num8);
				num3 = unchecked((nint)null);
			}
			goto IL_084b;
			IL_0772:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180CF3BB0");
			goto IL_089b;
			IL_089b:
			_003C_003E1__state = -2;
			_003Cusername_003E5__3 = null;
			_003CavatarBase64_003E5__4 = null;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->SetResult();
			return;
			IL_084b:
			if (num == 2)
			{
				_003C_003Eu__3 = (TaskAwaiter<RegisterResponse>)0;
				_003C_003E1__state = -1;
				TaskAwaiter<RegisterResponse> taskAwaiter = _003C_003Eu__3;
			}
			else
			{
				if (num == 3)
				{
					_003C_003Eu__1 = (TaskAwaiter)0;
					_003C_003E1__state = -1;
					awaiter = _003C_003Eu__1;
					goto IL_0772;
				}
				RegisterRequest registerRequest = new RegisterRequest();
				registerRequest._003CSteamId_003Ek__BackingField = _003CsteamId_003E5__2;
				registerRequest._003CGogId_003Ek__BackingField = (long?)(object)0;
				registerRequest._003CUsername_003Ek__BackingField = _003Cusername_003E5__3;
				registerRequest._003CAvatarBase64_003Ek__BackingField = _003CavatarBase64_003E5__4;
				Task<RegisterResponse> task2 = LeaderboardClient.Register(registerRequest);
				TaskAwaiter<RegisterResponse> awaiter5 = task2.GetAwaiter();
				TaskAwaiter<RegisterResponse> taskAwaiter = default(TaskAwaiter<RegisterResponse>);
				bool isCompleted2 = taskAwaiter.IsCompleted;
				bool flag9 = !isCompleted2;
				long? num9 = (long?)(object)0;
				if (flag9)
				{
					_003C_003E1__state = 2;
					_003C_003Eu__3 = taskAwaiter;
					AsyncTaskMethodBuilder asyncTaskMethodBuilder4 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder4)->AwaitUnsafeOnCompleted(ref taskAwaiter, ref this);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object obj3 = default(object);
			if (obj3 != null)
			{
				object arg2 = (Guid)num5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ stack_18_v6+20]");
				string message = $"[Leaderboard] Registered user: {0} ({arg2})";
				Debug.Log(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
				AsyncTaskMethodBuilder asyncTaskMethodBuilder5 = default(AsyncTaskMethodBuilder);
				_003CRetryPendingSubmissions_003Ed__36 stateMachine = default(_003CRetryPendingSubmissions_003Ed__36);
				asyncTaskMethodBuilder5.Start(ref stateMachine);
				Task task3 = asyncTaskMethodBuilder5.Task;
				TaskAwaiter awaiter6 = task3.GetAwaiter();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D2870");
				object obj4 = default(object);
				if (obj4 != null)
				{
					goto IL_0772;
				}
				_003C_003E1__state = 3;
				_003C_003Eu__1 = awaiter;
				AsyncTaskMethodBuilder asyncTaskMethodBuilder6 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
				((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder6)->AwaitUnsafeOnCompleted(ref awaiter, ref this);
				return;
			}
			Debug.LogError("[Leaderboard] Registration returned no response.");
			goto IL_089b;
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

	[StructLayout((LayoutKind)3)]
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

		private unsafe void MoveNext()
		{
			//IL_01dd: Expected O, but got Ref
			//IL_01e5: Expected O, but got Ref
			//IL_0243: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Expected O, but got Unknown
			//IL_0345: Expected native int or pointer, but got O
			//IL_02c5: Expected O, but got I4
			//IL_02c0: Expected native int or pointer, but got O
			//IL_02d7: Expected I4, but got I8
			//IL_02d2: Expected native int or pointer, but got O
			//IL_02e4: Expected O, but got I8
			//IL_031c: Expected O, but got I4
			//IL_0317: Expected native int or pointer, but got O
			//IL_032e: Expected I4, but got I8
			//IL_0329: Expected native int or pointer, but got O
			//IL_033b: Expected O, but got I8
			//IL_05ba: Expected O, but got Ref
			//IL_0277: Unknown result type (might be due to invalid IL or missing references)
			//IL_027c: Expected O, but got Unknown
			//IL_028e: Expected native int or pointer, but got O
			//IL_029c: Expected O, but got I4
			//IL_0c15: Expected I4, but got I8
			//IL_0899: Expected O, but got Ref
			//IL_0be3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0be8: Expected O, but got Unknown
			//IL_0c25: Expected O, but got Ref
			//IL_05e1: Expected O, but got I
			//IL_0607: Expected I, but got O
			//IL_060f: Expected O, but got Ref
			//IL_101b: Expected O, but got I4
			//IL_1016: Expected native int or pointer, but got O
			//IL_102a: Expected O, but got Ref
			//IL_03c2: Expected O, but got I
			//IL_08d1: Expected O, but got Ref
			//IL_008f: Expected O, but got I
			//IL_0d51: Expected I, but got O
			//IL_0418: Expected O, but got I
			//IL_090e: Expected O, but got Ref
			//IL_0f0b: Expected I, but got O
			//IL_0669: Expected O, but got I
			//IL_067e: Expected O, but got I
			//IL_06a9: Expected I, but got O
			//IL_00e9: Expected O, but got I
			//IL_0f52: Expected O, but got Ref
			//IL_06f9: Expected I, but got O
			//IL_0119: Expected I, but got O
			//IL_0580: Expected O, but got I4
			//IL_0950: Expected O, but got Ref
			//IL_0b93: Expected native int or pointer, but got O
			//IL_0ba0: Expected native int or pointer, but got O
			//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb3: Expected O, but got Unknown
			//IL_0497: Expected native int or pointer, but got O
			//IL_0751: Expected I, but got O
			//IL_0c95: Expected O, but got I4
			//IL_09a0: Expected O, but got I
			//IL_0ce3: Expected I, but got O
			//IL_0776: Expected O, but got I4
			//IL_07a1: Expected I, but got O
			//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc6: Expected O, but got Unknown
			//IL_09d3: Expected O, but got I
			//IL_07c6: Expected O, but got I4
			//IL_07fa: Expected I, but got O
			//IL_0ac2: Expected O, but got Ref
			//IL_085f: Expected O, but got I
			//IL_0b43: Expected native int or pointer, but got O
			//IL_0b50: Expected native int or pointer, but got O
			//IL_0b5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b63: Expected O, but got Unknown
			//IL_0b09: Expected native int or pointer, but got O
			//IL_0b18: Expected native int or pointer, but got O
			//IL_0b35: Expected I, but got O
			//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b76: Expected O, but got Unknown
			LeaderboardManager leaderboardManager = _003C_003E4__this;
			nint num2 = default(nint);
			if (_003C_003E1__state > 1)
			{
				if ((object)_003C_003E4__this != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (LeaderboardManager)+51]");
					if ((nint)0 == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
						if ((nint)0 == 0)
						{
							_ = 1;
						}
						int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
						if (num != 1)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (LeaderboardManager)+58]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v3 (LeaderboardManager)+58]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v208+10]");
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v208+10]");
									object obj2 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rcx_v158+18]");
									if ((nint)0 != 0)
									{
										_ = 1;
										num2 = unchecked((nint)null);
										goto IL_1038;
									}
								}
							}
						}
					}
					goto IL_0c06;
				}
				throw new NullReferenceException();
			}
			goto IL_1038;
			IL_0e78:
			object obj3 = default(object);
			_003CRetryPendingSubmissions_003Ed__36 obj4 = default(_003CRetryPendingSubmissions_003Ed__36);
			LeaderboardManager leaderboardManager2 = default(LeaderboardManager);
			nint num3;
			object obj8;
			object obj9;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder2 = default(AsyncTaskMethodBuilder);
			byte[] array;
			object obj5 = default(object);
			LocalSubmission localSubmission2;
			object obj6;
			object obj7;
			if (obj3 == null)
			{
				System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003Eu__1, (TaskAwaiter<GetSessionKeyResponse>)0);
				((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003E1__state = -1;
				obj3 = 4294967295L;
			}
			else
			{
				if ((nint)obj3 == 1)
				{
					System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003Eu__2, (TaskAwaiter<PostLeaderboardScoreResponse>)0);
					((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003E1__state = -1;
					obj3 = 4294967295L;
					goto IL_086d;
				}
				System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003CzipBytes_003E5__4, null);
				LocalSubmission localSubmission = obj4._003Csubmission_003E5__3;
				bool flag = obj4._003Csubmission_003E5__3 == null;
				array = null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				if (!string.IsNullOrWhiteSpace(localSubmission.ReplayFileName))
				{
					bool flag2 = (object)leaderboardManager2 == null;
					array = null;
					obj5 = obj6;
					string text = (string)num3;
					localSubmission2 = null;
					if (flag2)
					{
						throw new NullReferenceException();
					}
					string localSubmissionFolder = leaderboardManager2.LocalSubmissionFolder;
					localSubmission2 = obj4._003Csubmission_003E5__3;
					bool flag3 = obj4._003Csubmission_003E5__3 == null;
					array = null;
					obj5 = obj6;
					text = (string)num3;
					if (flag3)
					{
						throw new NullReferenceException();
					}
					string text2 = Path.Combine(localSubmissionFolder, localSubmission2.ReplayFileName);
					bool flag4 = File.Exists(text2);
					bool flag5 = !flag4;
					array = null;
					obj5 = obj6;
					string text3 = text2;
					if (flag5)
					{
						List<LocalSubmission>.Enumerator enumerator = ((List<LocalSubmission>)(object)typeof(FileNotFoundException)).GetEnumerator();
						FileNotFoundException ex = new FileNotFoundException("Pending replay file was not found.", text3);
						ex._002Ector("Pending replay file was not found.", text3);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						obj7 = 0;
						text = text3;
						LocalSubmission localSubmission3 = default(LocalSubmission);
						localSubmission2 = localSubmission3;
						throw ex;
					}
					byte[] array2 = File.ReadAllBytes(text2);
					System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003CzipBytes_003E5__4, array2);
				}
				bool flag6 = (object)PerformanceTracker.Instance == null;
				array = null;
				if (flag6)
				{
					throw new NullReferenceException();
				}
				string performanceJson = PerformanceTracker.Instance.CaptureJson(resetAfterCapture: true, includeDeviceInfo: true);
				LocalSubmission localSubmission4 = obj4._003Csubmission_003E5__3;
				bool flag7 = obj4._003Csubmission_003E5__3 == null;
				array = null;
				if (flag7)
				{
					throw new NullReferenceException();
				}
				Task<GetSessionKeyResponse> sessionKey = LeaderboardClient.GetSessionKey(localSubmission4.Gamemode, performanceJson);
				bool flag8 = sessionKey == null;
				array = null;
				if (flag8)
				{
					throw new NullReferenceException();
				}
				TaskAwaiter<GetSessionKeyResponse> awaiter = sessionKey.GetAwaiter();
				TaskAwaiter<GetSessionKeyResponse> taskAwaiter = default(TaskAwaiter<GetSessionKeyResponse>);
				bool isCompleted = taskAwaiter.IsCompleted;
				bool flag9 = !isCompleted;
				obj7 = 0;
				if (flag9)
				{
					((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003E1__state = 0;
					System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003Eu__1, taskAwaiter);
					AsyncTaskMethodBuilder asyncTaskMethodBuilder = (AsyncTaskMethodBuilder)(obj4 + 8);
					TaskAwaiter<GetSessionKeyResponse> awaiter2 = default(TaskAwaiter<GetSessionKeyResponse>);
					((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder)->AwaitUnsafeOnCompleted(ref awaiter2, ref *(_003CRetryPendingSubmissions_003Ed__36*)obj4);
					bool flag10 = (nint)obj8 >= 0;
					ref TaskAwaiter<GetSessionKeyResponse> awaiter3 = ref awaiter2;
					if (!flag10)
					{
						List<LocalSubmission>.Enumerator enumerator2 = (List<LocalSubmission>.Enumerator)(obj9 + 40);
						((List<LocalSubmission>.Enumerator*)enumerator2)->Dispose();
						awaiter3 = ref *(TaskAwaiter<GetSessionKeyResponse>*)null;
					}
					asyncTaskMethodBuilder2.AwaitUnsafeOnCompleted(ref awaiter3, ref *(_003CRetryPendingSubmissions_003Ed__36*)obj4);
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object obj10 = default(object);
			bool flag11 = obj10 == null;
			array = null;
			nint num4 = 0;
			byte[] array3 = (byte[])(&obj10);
			Guid guid2;
			Guid empty;
			if (!flag11)
			{
				empty = Guid.Empty;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ stack_-98+10]");
				obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180645F80");
				object obj11 = default(object);
				bool flag12 = obj11 != null;
				array = null;
				num4 = unchecked((nint)null);
				Guid guid = default(Guid);
				array3 = (byte[])(&guid);
				if (!flag12)
				{
					PostLeaderboardScoreRequest postLeaderboardScoreRequest = new PostLeaderboardScoreRequest();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC77]");
					if ((nint)0 == 0)
					{
						_ = 1;
					}
					postLeaderboardScoreRequest._003CImageExtension_003Ek__BackingField = "jpg";
					bool flag13 = postLeaderboardScoreRequest == null;
					array = null;
					num4 = unchecked((nint)null);
					array3 = null;
					if (!flag13)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ stack_-98+10]");
						guid2 = (Guid)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ stack_-98+10]");
						postLeaderboardScoreRequest._003CSessionId_003Ek__BackingField = (Guid)0;
						LocalSubmission localSubmission5 = obj4._003Csubmission_003E5__3;
						bool flag14 = obj4._003Csubmission_003E5__3 == null;
						array = null;
						num4 = unchecked((nint)null);
						array3 = null;
						if (!flag14)
						{
							postLeaderboardScoreRequest._003CClientTampered_003Ek__BackingField = localSubmission5.ClientTampered;
							LocalSubmission localSubmission6 = obj4._003Csubmission_003E5__3;
							bool flag15 = obj4._003Csubmission_003E5__3 == null;
							array = null;
							num4 = unchecked((nint)null);
							array3 = (byte[])(object)obj4._003Csubmission_003E5__3;
							if (!flag15)
							{
								postLeaderboardScoreRequest._003CRunData_003Ek__BackingField = localSubmission6.RunData;
								array3 = (byte[])(object)obj4._003Csubmission_003E5__3;
								bool flag16 = obj4._003Csubmission_003E5__3 == null;
								array = null;
								num4 = unchecked((nint)null);
								if (!flag16)
								{
									postLeaderboardScoreRequest._003CImageExtension_003Ek__BackingField = (string)array3[32];
									array3 = (byte[])(object)obj4._003Csubmission_003E5__3;
									bool flag17 = obj4._003Csubmission_003E5__3 == null;
									array = null;
									num4 = unchecked((nint)null);
									if (!flag17)
									{
										postLeaderboardScoreRequest._003CPerformanceStatsJson_003Ek__BackingField = (string)array3[40];
										Task<PostLeaderboardScoreResponse> task = LeaderboardClient.SubmitScore(postLeaderboardScoreRequest, obj4._003CzipBytes_003E5__4);
										bool flag18 = task == null;
										array = null;
										num4 = unchecked((nint)null);
										array3 = obj4._003CzipBytes_003E5__4;
										if (!flag18)
										{
											TaskAwaiter<PostLeaderboardScoreResponse> awaiter4 = task.GetAwaiter();
											TaskAwaiter<PostLeaderboardScoreResponse> taskAwaiter2 = default(TaskAwaiter<PostLeaderboardScoreResponse>);
											bool isCompleted2 = taskAwaiter2.IsCompleted;
											bool flag19 = !isCompleted2;
											object obj12 = obj6;
											guid = empty;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v402 @ stack_-98+10]");
											empty = (Guid)0;
											if (!flag19)
											{
												goto IL_086d;
											}
											((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003E1__state = 1;
											System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003Eu__2, taskAwaiter2);
											AsyncTaskMethodBuilder asyncTaskMethodBuilder3 = (AsyncTaskMethodBuilder)(obj4 + 8);
											TaskAwaiter<PostLeaderboardScoreResponse> awaiter5 = default(TaskAwaiter<PostLeaderboardScoreResponse>);
											((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder3)->AwaitUnsafeOnCompleted(ref awaiter5, ref *(_003CRetryPendingSubmissions_003Ed__36*)obj4);
											bool flag20 = (nint)obj8 >= 0;
											ref TaskAwaiter<PostLeaderboardScoreResponse> awaiter6 = ref awaiter5;
											if (!flag20)
											{
												List<LocalSubmission>.Enumerator enumerator3 = (List<LocalSubmission>.Enumerator)(obj9 + 40);
												((List<LocalSubmission>.Enumerator*)enumerator3)->Dispose();
												awaiter6 = ref *(TaskAwaiter<PostLeaderboardScoreResponse>*)null;
											}
											asyncTaskMethodBuilder2.AwaitUnsafeOnCompleted(ref awaiter6, ref *(_003CRetryPendingSubmissions_003Ed__36*)obj4);
											return;
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						empty = guid2;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			InvalidOperationException ex2 = new InvalidOperationException("No retry session key.");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex2;
			IL_023a:
			List<LocalSubmission>.Enumerator enumerator4 = (List<LocalSubmission>.Enumerator)(obj4 + 40);
			List<LocalSubmission>.Enumerator enumerator5 = default(List<LocalSubmission>.Enumerator);
			if (((List<LocalSubmission>.Enumerator*)enumerator4)->MoveNext())
			{
				object obj13 = obj4 + 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				LocalSubmission localSubmission7 = default(LocalSubmission);
				System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003Csubmission_003E5__3, localSubmission7);
				obj7 = 0;
				obj6 = obj5;
				empty = (Guid)enumerator5;
				num3 = 0;
				goto IL_0e78;
			}
			if ((nint)obj8 < 0)
			{
				List<LocalSubmission>.Enumerator enumerator6 = (List<LocalSubmission>.Enumerator)(obj9 + 40);
				((List<LocalSubmission>.Enumerator*)enumerator6)->Dispose();
			}
			array = null;
			System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003C_003E7__wrap1, (List<LocalSubmission>.Enumerator)0);
			_ = 0;
			List<LocalSubmission>.Enumerator enumerator7 = ((List<LocalSubmission>)(&asyncTaskMethodBuilder2)).GetEnumerator();
			goto IL_0c06;
			IL_086d:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091FAF0");
			object obj14 = default(object);
			bool flag21 = obj14 == null;
			array = null;
			nint num5 = 0;
			localSubmission2 = (LocalSubmission)(&obj14);
			if (!flag21)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v394 @ stack_-90_v2+10]");
				bool flag22 = (nint)0 == 0;
				array = null;
				num5 = 0;
				localSubmission2 = (LocalSubmission)(&obj14);
				if (!flag22)
				{
					LocalSubmission localSubmission8 = obj4._003Csubmission_003E5__3;
					bool flag23 = (object)leaderboardManager2 == null;
					array = null;
					num5 = 0;
					localSubmission2 = (LocalSubmission)(&obj14);
					if (flag23)
					{
						throw new NullReferenceException();
					}
					LocalSubmissionQueue localSubmissionQueue = leaderboardManager2.localSubmissionQueue;
					bool flag24 = leaderboardManager2.localSubmissionQueue == null;
					array = null;
					num5 = 0;
					localSubmission2 = (LocalSubmission)(&obj14);
					if (!flag24)
					{
						bool flag25 = localSubmissionQueue.Submissions == null;
						array = null;
						obj5 = obj6;
						num5 = 0;
						localSubmission2 = (LocalSubmission)(&obj14);
						if (!flag25)
						{
							bool flag26 = localSubmissionQueue.Submissions.Remove(obj4._003Csubmission_003E5__3);
							bool flag27 = obj4._003Csubmission_003E5__3 == null;
							array = null;
							obj5 = obj6;
							string text = (string)0;
							localSubmission2 = obj4._003Csubmission_003E5__3;
							if (!flag27)
							{
								bool flag28 = string.IsNullOrWhiteSpace(localSubmission8.ReplayFileName);
								text = (string)0;
								if (!flag28)
								{
									string localSubmissionFolder2 = leaderboardManager2.LocalSubmissionFolder;
									string path = Path.Combine(localSubmissionFolder2, localSubmission8.ReplayFileName);
									bool flag29 = File.Exists(path);
									bool flag30 = !flag29;
									text = null;
									if (!flag30)
									{
										File.Delete(path);
										text = null;
									}
								}
								leaderboardManager2.SaveLocalSubmissionQueue();
								bool flag31 = obj4._003Csubmission_003E5__3 == null;
								array = null;
								obj5 = obj6;
								localSubmission2 = null;
								if (!flag31)
								{
									Gamemodes gamemodes = default(Gamemodes);
									object arg = gamemodes;
									bool flag32 = obj4._003Csubmission_003E5__3 == null;
									array = null;
									obj5 = obj6;
									localSubmission2 = (LocalSubmission)(&gamemodes);
									if (!flag32)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj15 = default(object);
										string message = $"[Leaderboard] Submitted pending {arg} score: {obj15}";
										Debug.Log(message);
										System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003CzipBytes_003E5__4, null);
										System.Runtime.CompilerServices.Unsafe.Write(&((_003CRetryPendingSubmissions_003Ed__36*)(nint)obj4)->_003Csubmission_003E5__3, null);
										obj5 = obj6;
										enumerator5 = (List<LocalSubmission>.Enumerator)empty;
										num2 = (nint)obj15;
										goto IL_023a;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							num5 = (nint)text;
							throw new NullReferenceException();
						}
						obj6 = obj5;
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			InvalidOperationException ex3 = new InvalidOperationException("Pending score submission was not accepted.");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			guid2 = empty;
			num4 = unchecked((nint)null);
			byte[] array4 = default(byte[]);
			array3 = array4;
			throw ex3;
			IL_0c06:
			_003C_003E1__state = -2;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder4 = (AsyncTaskMethodBuilder)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			((AsyncTaskMethodBuilder*)asyncTaskMethodBuilder4)->SetResult();
			return;
			IL_1038:
			if ((nint)obj3 > 1)
			{
				LocalSubmissionQueue localSubmissionQueue2 = default(LocalSubmissionQueue);
				if ((object)leaderboardManager2 == null)
				{
					empty = (Guid)enumerator5;
					string text3 = (string)(object)localSubmissionQueue2.Submissions;
					throw new NullReferenceException();
				}
				localSubmissionQueue2 = leaderboardManager2.localSubmissionQueue;
				if (leaderboardManager2.localSubmissionQueue == null)
				{
					throw new NullReferenceException();
				}
				List<LocalSubmission> list = new List<LocalSubmission>(localSubmissionQueue2.Submissions);
				if (list == null)
				{
					throw new NullReferenceException();
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<LocalSubmission>.Enumerator enumerator8 = default(List<LocalSubmission>.Enumerator);
				_003C_003E7__wrap1 = enumerator8;
				object obj16 = default(object);
				obj5 = obj16;
				enumerator5 = enumerator8;
				num2 = 0;
			}
			obj8 = (object)(&obj3);
			obj9 = (object)(&obj4);
			bool flag33 = (nint)obj3 <= 1;
			byte[] array5 = null;
			byte[] array6 = null;
			object obj17 = default(object);
			obj7 = obj17;
			array5 = null;
			array6 = null;
			obj6 = obj5;
			empty = (Guid)enumerator5;
			num3 = num2;
			if (!flag33)
			{
				goto IL_023a;
			}
			goto IL_0e78;
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

	public const string OptOutPrefsKey = "LeaderboardOptOut";

	public static LeaderboardManager Instance;

	public string APIEndpoint;

	public string SecretKey;

	public LeaderboardRunData CurrentRun;

	private Guid? currentSessionId;

	private Gamemodes currentGamemode;

	private bool isSubmitting;

	private bool isRetryingPendingSubmissions;

	private LocalSubmissionQueue localSubmissionQueue;

	public static bool OptOut
	{
		get
		{
			//IL_0054: Expected O, but got I4
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
			object obj = num - 1;
			return obj == null;
		}
	}

	private string LocalSubmissionFolder
	{
		get
		{
			string persistentDataPath = Application.persistentDataPath;
			return Path.Combine(persistentDataPath, "LeaderboardSubmissions");
		}
	}

	private string LocalSubmissionQueuePath
	{
		get
		{
			string localSubmissionFolder = LocalSubmissionFolder;
			return Path.Combine(localSubmissionFolder, "pending-submissions.json");
		}
	}

	private void Awake()
	{
		if (!(Instance != null))
		{
			LeaderboardClient.Init(APIEndpoint, SecretKey);
			LoadLocalSubmissionQueue();
		}
		else
		{
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void Start()
	{
		Instance = this;
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		Task task = RegisterUser();
	}

	public Task RegisterUser()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CRegisterUser_003Ed__20 stateMachine = default(_003CRegisterUser_003Ed__20);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}

	private Task<string> GetSteamAvatarBase64(UserData user)
	{
		_003C_003Ec__DisplayClass21_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass21_0();
		Debug.Log("[Leaderboard] Requesting avatar");
		TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
		if (CS_0024_003C_003E8__locals6 != null)
		{
			CS_0024_003C_003E8__locals6.tcs = tcs;
			Action<Texture2D> callback = delegate(Texture2D tex)
			{
				if (tex != null)
				{
					byte[] inArray = ImageConversion.EncodeToJPG(tex, 80);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					string message = $"[Leaderboard] Avatar Size: {arg}";
					Debug.Log(message);
					string result = Convert.ToBase64String(inArray);
					bool flag = CS_0024_003C_003E8__locals6.tcs.TrySetResult(result);
				}
				else
				{
					Debug.LogError("[Leaderboard] No Avatar Loaded");
					bool flag2 = CS_0024_003C_003E8__locals6.tcs.TrySetResult(null);
				}
			};
			UserData userData = default(UserData);
			userData.LoadAvatar(callback);
			TaskCompletionSource<string> tcs2 = CS_0024_003C_003E8__locals6.tcs;
			if (CS_0024_003C_003E8__locals6.tcs != null)
			{
				return tcs2._task;
			}
		}
		return (Task<string>)(object)new NullReferenceException();
	}

	private unsafe string GetFallbackAvatarBase64()
	{
		//IL_0039: Expected O, but got Ref
		bool mipChain = default(bool);
		Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGB24, mipChain);
		Color gray = Color.gray;
		if ((object)texture2D != null)
		{
			object obj = default(object);
			texture2D.SetPixel(0, 0, (Color)(&obj));
			texture2D.Apply();
			byte[] inArray = ImageConversion.EncodeToJPG(texture2D, 80);
			string result = Convert.ToBase64String(inArray);
			UnityEngine.Object.Destroy(texture2D);
			return result;
		}
		return (string)(object)new NullReferenceException();
	}

	public static void PushOperationState(OperationState state)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CPushOperationState_003Ed__23 stateMachine = default(_003CPushOperationState_003Ed__23);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public void Leaderboard_StartRun(Gamemodes gamemode)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CLeaderboard_StartRun_003Ed__24 stateMachine = default(_003CLeaderboard_StartRun_003Ed__24);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public void Leaderboard_CompleteRun(Action onCompleted)
	{
		AsyncVoidMethodBuilder asyncVoidMethodBuilder = AsyncVoidMethodBuilder.Create();
		AsyncVoidMethodBuilder asyncVoidMethodBuilder2 = default(AsyncVoidMethodBuilder);
		_003CLeaderboard_CompleteRun_003Ed__25 stateMachine = default(_003CLeaderboard_CompleteRun_003Ed__25);
		asyncVoidMethodBuilder2.Start(ref stateMachine);
	}

	public void RecordAction(string action, string details, int scoreDelta, bool includeImage = false)
	{
		//IL_0103: Expected I4, but got I8
		//IL_0131: Expected O, but got I4
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected I4, but got Unknown
		//IL_01af: Expected O, but got I4
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected I4, but got Unknown
		//IL_01d3: Expected O, but got I4
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_0250: Expected I4, but got I8
		//IL_0274: Expected I4, but got I8
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected I4, but got Unknown
		//IL_02bc: Expected I4, but got I8
		//IL_02d6: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC7A]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		int num = PlayerPrefs.GetInt("LeaderboardOptOut", 0);
		if (num == 1)
		{
			return;
		}
		if (CurrentRun != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[Leaderboard] {action} | {details} | {arg}";
			Debug.Log(message);
			LeaderboardRunData currentRun = CurrentRun;
			LeaderboardRunData.ActionData actionData = new LeaderboardRunData.ActionData();
			actionData.ActionName = action;
			actionData.Details = details;
			actionData.ScoreDelta = scoreDelta;
			DateTime utcNow = DateTime.UtcNow;
			actionData.TimestampUTC = utcNow;
			LeaderboardRunData currentRun2 = CurrentRun;
			List<LeaderboardRunData.ActionData> actions = currentRun2.Actions;
			int num2 = (int)(actions._size & 0x80000003L);
			if ((nint)actions < 0)
			{
				object obj = num2 - 1;
				object obj2 = obj | -4;
				num2 = obj2 + 1;
			}
			bool flag = num2 != 0;
			string performanceStatsJson = null;
			if (!flag)
			{
				string text = PerformanceTracker.Instance.CaptureJson(resetAfterCapture: true);
				performanceStatsJson = text;
			}
			actionData.PerformanceStatsJson = performanceStatsJson;
			currentRun.Actions.Add(actionData);
			if (CurrentRun != null)
			{
				LeaderboardRunData currentRun3 = CurrentRun;
				object obj3 = currentRun3._a - currentRun3._b;
				int num3 = obj3 ^ currentRun3._key;
				object obj4 = num3 - currentRun3._salt;
				object obj5 = ~obj4;
				if (currentRun3._check != (nint)obj5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC99]");
					if ((nint)0 == 0)
					{
						_ = 1;
					}
					currentRun3._t = true;
					PlayerPrefs.SetInt("1577626b-18aa-47c6-8067-1bf1f5127fa6", 1);
				}
				object obj6 = obj4 + scoreDelta;
				int key = UnityEngine.Random.Range(-2147483648, 2147483647);
				currentRun3._key = key;
				object obj7 = obj6 + (currentRun3._salt = UnityEngine.Random.Range(-2147483648, 2147483647));
				int num4 = obj7 ^ currentRun3._key;
				int num5 = (currentRun3._b = UnityEngine.Random.Range(-2147483648, 2147483647));
				int check = (int)(~obj6);
				int a = num5 + num4;
				currentRun3._check = check;
				currentRun3._a = a;
			}
			else
			{
				Debug.LogError("[Leaderboard] Unable to modify score. No 'CurrentRun'");
			}
		}
		else
		{
			Debug.LogError("[Leaderboard] Unable to RecordAction. No 'CurrentRun'");
		}
	}

	private void ModifyScore(int value)
	{
		//IL_0026: Expected O, but got I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected I4, but got Unknown
		//IL_004a: Expected O, but got I4
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00c7: Expected I4, but got I8
		//IL_00eb: Expected I4, but got I8
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected I4, but got Unknown
		//IL_0133: Expected I4, but got I8
		//IL_014d: Expected I4, but got O
		if (CurrentRun != null)
		{
			LeaderboardRunData currentRun = CurrentRun;
			object obj = currentRun._a - currentRun._b;
			int num = obj ^ currentRun._key;
			object obj2 = num - currentRun._salt;
			object obj3 = ~obj2;
			if (currentRun._check != (nint)obj3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC99]");
				if ((nint)0 == 0)
				{
					_ = 1;
				}
				currentRun._t = true;
				PlayerPrefs.SetInt("1577626b-18aa-47c6-8067-1bf1f5127fa6", 1);
			}
			object obj4 = value + obj2;
			int key = UnityEngine.Random.Range(-2147483648, 2147483647);
			currentRun._key = key;
			object obj5 = obj4 + (currentRun._salt = UnityEngine.Random.Range(-2147483648, 2147483647));
			int num2 = obj5 ^ currentRun._key;
			int num3 = (currentRun._b = UnityEngine.Random.Range(-2147483648, 2147483647));
			int check = (int)(~obj4);
			int a = num3 + num2;
			currentRun._check = check;
			currentRun._a = a;
		}
		else
		{
			Debug.LogError("[Leaderboard] Unable to modify score. No 'CurrentRun'");
		}
	}

	public List<LeaderboardEntryResponse> GetPendingEntries(Gamemodes gamemode)
	{
		//IL_0098: Expected O, but got I
		//IL_00e4: Expected O, but got I
		//IL_0120: Expected O, but got I
		//IL_0135: Expected O, but got I
		List<LeaderboardEntryResponse> list = new List<LeaderboardEntryResponse>();
		LocalSubmissionQueue localSubmissionQueue = this.localSubmissionQueue;
		if (this.localSubmissionQueue != null && localSubmissionQueue.Submissions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<LocalSubmission>.Enumerator enumerator = default(List<LocalSubmission>.Enumerator);
			object obj = default(object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj == null)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+18]");
				if ((nint)0 == (nint)gamemode)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+50]");
					string text;
					if (string.IsNullOrWhiteSpace((string)0))
					{
						text = null;
					}
					else
					{
						string localSubmissionFolder = LocalSubmissionFolder;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+50]");
						string text2 = Path.Combine(localSubmissionFolder, (string)0);
						text = text2;
					}
					LeaderboardEntryResponse leaderboardEntryResponse = new LeaderboardEntryResponse();
					if (leaderboardEntryResponse == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+1C]");
					leaderboardEntryResponse._003CScore_003Ek__BackingField = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+28]");
					leaderboardEntryResponse._003CUsername_003Ek__BackingField = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ stack_8_v3+20]");
					leaderboardEntryResponse._003CCreatedAtUtc_003Ek__BackingField = (DateTime)0;
					leaderboardEntryResponse._003CIsPendingLocal_003Ek__BackingField = true;
					if (!File.Exists(text))
					{
						text = null;
					}
					leaderboardEntryResponse._003CLocalReplayPath_003Ek__BackingField = text;
					list.Add(leaderboardEntryResponse);
				}
			}
			enumerator.Dispose();
		}
		return list;
	}

	private void LoadLocalSubmissionQueue()
	{
		//IL_006c: Expected O, but got I4
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		LocalSubmissionQueue localSubmissionQueue = new LocalSubmissionQueue();
		this.localSubmissionQueue = localSubmissionQueue;
		string localSubmissionFolder = LocalSubmissionFolder;
		string path = Path.Combine(localSubmissionFolder, "pending-submissions.json");
		if (File.Exists(path))
		{
			string localSubmissionQueuePath = LocalSubmissionQueuePath;
			byte[] array = File.ReadAllBytes(localSubmissionQueuePath);
			object obj = 0;
			while ((nint)obj < array.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r14_v5+20+v109 @ rax_v12 (System.Byte[])]");
				_ = (nuint)0u ^ (nuint)0x42u;
				obj++;
			}
			byte[] bytes = Decompress(array);
			Encoding uTF = Encoding.UTF8;
			string text = uTF.GetString(bytes);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
			LocalSubmissionQueue localSubmissionQueue2 = default(LocalSubmissionQueue);
			bool flag = localSubmissionQueue2 != null;
			LocalSubmissionQueue localSubmissionQueue3 = localSubmissionQueue2;
			if (!flag)
			{
				LocalSubmissionQueue localSubmissionQueue4 = new LocalSubmissionQueue();
				localSubmissionQueue3 = localSubmissionQueue4;
			}
			this.localSubmissionQueue = localSubmissionQueue3;
			LocalSubmissionQueue localSubmissionQueue5 = this.localSubmissionQueue;
			if (localSubmissionQueue5.Submissions == null)
			{
				List<LocalSubmission> submissions = new List<LocalSubmission>();
				localSubmissionQueue5.Submissions = submissions;
			}
		}
	}

	private void SaveLocalSubmissionQueue()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_005d: Expected O, but got I4
		//IL_0066: Expected O, but got I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		string localSubmissionFolder = LocalSubmissionFolder;
		DirectoryInfo directoryInfo = Directory.CreateDirectory(localSubmissionFolder);
		string s = JsonConvert.SerializeObject(localSubmissionQueue);
		Encoding uTF = Encoding.UTF8;
		byte[] bytes = uTF.GetBytes(s);
		byte[] array = Compress(bytes);
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj2 < array.Length)
		{
			obj ^= 0x42;
			obj3++;
			obj++;
			obj2 = obj3;
		}
		string localSubmissionFolder2 = LocalSubmissionFolder;
		string path = Path.Combine(localSubmissionFolder2, "pending-submissions.json");
		File.WriteAllBytes(path, array);
	}

	private byte[] Compress(byte[] data)
	{
		//IL_009d: Expected I, but got O
		MemoryStream memoryStream = new MemoryStream();
		GZipStream gZipStream2 = default(GZipStream);
		GZipStream gZipStream = new GZipStream(gZipStream2, CompressionMode.Compress);
		gZipStream._002Ector(gZipStream2, CompressionMode.Compress);
		if (data != null)
		{
			GZipStream gZipStream3 = default(GZipStream);
			if (gZipStream3 != null)
			{
				gZipStream3.Write(data, 0, data.Length);
				if (gZipStream3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				nint num = (nint)gZipStream2;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v162 @ rdx_v7 (Il2CppClass<System.IO.Compression.GZipStream>)+3E8] (should have been resolved before IL gen)");
				if (gZipStream2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				byte[] result = default(byte[]);
				return result;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private byte[] Decompress(byte[] data)
	{
		//IL_005d: Expected I, but got O
		MemoryStream memoryStream = new MemoryStream(data);
		Stream stream = default(Stream);
		GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
		gZipStream._002Ector(stream, CompressionMode.Decompress);
		MemoryStream memoryStream2 = new MemoryStream();
		Stream stream2 = default(Stream);
		if (stream2 != null)
		{
			Stream stream3 = default(Stream);
			stream2.CopyTo(stream3);
			nint num = (nint)stream3;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v115 @ rdx_v6 (Il2CppClass<System.IO.Stream>)+3E8] (should have been resolved before IL gen)");
			if (stream3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			if (stream2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			if (stream != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			byte[] result = default(byte[]);
			return result;
		}
		throw new NullReferenceException();
	}

	private byte[] Encrypt(byte[] data)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		object obj = data + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < data.Length)
			{
				if ((nint)obj3 >= data.Length)
				{
					break;
				}
				obj ^= 0x42;
				obj3++;
				obj++;
				obj2 = obj3;
				continue;
			}
			return data;
		}
		return (byte[])(object)new IndexOutOfRangeException();
	}

	private byte[] Decrypt(byte[] data)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		object obj = data + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < data.Length)
			{
				if ((nint)obj3 >= data.Length)
				{
					break;
				}
				obj ^= 0x42;
				obj3++;
				obj++;
				obj2 = obj3;
				continue;
			}
			return data;
		}
		return (byte[])(object)new IndexOutOfRangeException();
	}

	private void QueueFailedSubmission(PostLeaderboardScoreRequest submission, byte[] zipBytes)
	{
		//IL_00b6: Expected O, but got I4
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected I4, but got Unknown
		//IL_01fa: Expected O, but got I4
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected I4, but got Unknown
		//IL_021e: Expected O, but got I4
		if (CurrentRun == null)
		{
			return;
		}
		if (this.localSubmissionQueue == null)
		{
			LocalSubmissionQueue localSubmissionQueue = new LocalSubmissionQueue();
			this.localSubmissionQueue = localSubmissionQueue;
		}
		LocalSubmission localSubmission = new LocalSubmission();
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		string submissionID = guid2.ToString("N");
		localSubmission.SubmissionID = submissionID;
		localSubmission.Gamemode = currentGamemode;
		LeaderboardRunData currentRun = CurrentRun;
		object obj = currentRun._a - currentRun._b;
		int num = obj ^ currentRun._key;
		int num2 = num - currentRun._salt;
		int num3 = ~num2;
		int num4;
		if (currentRun._check != num3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC99]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			currentRun._t = true;
			PlayerPrefs.SetInt("1577626b-18aa-47c6-8067-1bf1f5127fa6", 1);
			num4 = 1;
		}
		else
		{
			num4 = 1;
		}
		localSubmission.Score = num2;
		DateTime utcNow = DateTime.UtcNow;
		localSubmission.CreatedAtUtc = utcNow;
		string username = PlayerPrefs.GetString("username", "Operator");
		localSubmission.Username = username;
		if (submission != null)
		{
			num4 = (submission._003CClientTampered_003Ek__BackingField ? 1 : 0);
		}
		else
		{
			LeaderboardRunData currentRun2 = CurrentRun;
			if (!currentRun2._t)
			{
				object obj2 = currentRun2._a - currentRun2._b;
				int num5 = obj2 ^ currentRun2._key;
				object obj3 = num5 - currentRun2._salt;
				object obj4 = ~obj3;
				if (currentRun2._check == (nint)obj4)
				{
					MissionStatsTracker instance = MissionStatsTracker.Instance;
					num4 = (instance.requisitionPointsTampered ? 1 : 0);
				}
			}
		}
		bool flag = num4 == 0;
		bool clientTampered = !flag;
		localSubmission.ClientTampered = clientTampered;
		localSubmission.RunData = CurrentRun;
		string performanceStatsJson;
		if (submission != null && submission._003CImageExtension_003Ek__BackingField != null)
		{
			localSubmission.ImageExtension = submission._003CImageExtension_003Ek__BackingField;
			performanceStatsJson = submission._003CPerformanceStatsJson_003Ek__BackingField;
		}
		else
		{
			localSubmission.ImageExtension = "jpg";
			performanceStatsJson = submission?._003CPerformanceStatsJson_003Ek__BackingField;
		}
		localSubmission.PerformanceStatsJson = performanceStatsJson;
		if (zipBytes != null && zipBytes.Length != 0)
		{
			string localSubmissionFolder = LocalSubmissionFolder;
			DirectoryInfo directoryInfo = Directory.CreateDirectory(localSubmissionFolder);
			string replayFileName = localSubmission.SubmissionID + ".zip";
			localSubmission.ReplayFileName = replayFileName;
			string localSubmissionFolder2 = LocalSubmissionFolder;
			string path = Path.Combine(localSubmissionFolder2, localSubmission.ReplayFileName);
			File.WriteAllBytes(path, zipBytes);
		}
		LocalSubmissionQueue localSubmissionQueue2 = this.localSubmissionQueue;
		localSubmissionQueue2.Submissions.Add(localSubmission);
		SaveLocalSubmissionQueue();
		Gamemodes gamemodes = default(Gamemodes);
		object arg = gamemodes;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		string message = $"[Leaderboard] Saved pending {arg} score: {arg2}";
		Debug.Log(message);
	}

	private Task RetryPendingSubmissions()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180788D50");
		AsyncTaskMethodBuilder asyncTaskMethodBuilder = default(AsyncTaskMethodBuilder);
		_003CRetryPendingSubmissions_003Ed__36 stateMachine = default(_003CRetryPendingSubmissions_003Ed__36);
		asyncTaskMethodBuilder.Start(ref stateMachine);
		return asyncTaskMethodBuilder.Task;
	}

	private void RemovePendingSubmission(LocalSubmission submission)
	{
		LocalSubmissionQueue localSubmissionQueue = this.localSubmissionQueue;
		bool flag = localSubmissionQueue.Submissions.Remove(submission);
		if (!string.IsNullOrWhiteSpace(submission.ReplayFileName))
		{
			string localSubmissionFolder = LocalSubmissionFolder;
			string path = Path.Combine(localSubmissionFolder, submission.ReplayFileName);
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
		SaveLocalSubmissionQueue();
	}

	public LeaderboardManager()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AC8F]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		APIEndpoint = "";
		SecretKey = "";
		base._002Ector();
	}
}
