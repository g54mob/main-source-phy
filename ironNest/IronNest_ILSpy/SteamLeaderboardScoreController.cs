using System;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration;
using UnityEngine;

public class SteamLeaderboardScoreController : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public SteamLeaderboardScoreController _003C_003E4__this;

		public bool forceUpdate;

		public int toSubmit;

		public Action<LeaderboardScoreUploaded, bool> _003C_003E9__1;

		internal unsafe void _003CSubmitScoreInternal_003Eb__0(LeaderboardData board, bool ioError)
		{
			//IL_0021: Expected O, but got I4
			SteamLeaderboardScoreController steamLeaderboardScoreController = _003C_003E4__this;
			bool flag = !steamLeaderboardScoreController.verboseLogging;
			object obj = ioError;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				bool isValid = ((LeaderboardData*)board)->IsValid;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg = default(object);
				object obj2 = default(object);
				string message = $"[SteamLeaderboardScoreController] LeaderboardData.Get callback: ioError={arg}, isValid={obj2}";
				Debug.Log(message);
				obj = obj2;
				bool flag2 = ioError;
			}
			SteamLeaderboardScoreController steamLeaderboardScoreController2;
			string text;
			string text2;
			if (!ioError)
			{
				bool isValid2 = ((LeaderboardData*)board)->IsValid;
				steamLeaderboardScoreController2 = _003C_003E4__this;
				if (isValid2)
				{
					if (steamLeaderboardScoreController2.verboseLogging != ioError)
					{
						bool flag3 = forceUpdate;
						object arg2 = "ForceUpdate";
						if (!flag3)
						{
							arg2 = "KeepBest";
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						SteamLeaderboardScoreController steamLeaderboardScoreController3 = _003C_003E4__this;
						object arg3 = default(object);
						string message2 = $"[SteamLeaderboardScoreController] Uploading ({arg2}) score={arg3} to '{steamLeaderboardScoreController3.leaderboardApiName}'...";
						Debug.Log(message2);
					}
					Action<LeaderboardScoreUploaded, bool> callback = _003C_003E9__1;
					if (_003C_003E9__1 == null)
					{
						callback = (_003C_003E9__1 = delegate
						{
							//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
							//IL_1002: Expected O, but got Unknown
							//IL_004d: Expected O, but got I
							//IL_0056: Unknown result type (might be due to invalid IL or missing references)
							//IL_005b: Expected O, but got Unknown
							//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
							//IL_0c91: Expected O, but got Unknown
							//IL_0665: Unknown result type (might be due to invalid IL or missing references)
							//IL_066a: Expected O, but got Unknown
							//IL_0681: Expected O, but got I
							//IL_0ccc: Expected I, but got O
							//IL_0cdc: Expected O, but got I
							//IL_00b1: Expected I, but got O
							//IL_00c1: Expected O, but got I
							//IL_00f2: Expected O, but got I
							//IL_0d56: Expected I, but got O
							//IL_0d66: Expected O, but got I
							//IL_015e: Expected O, but got I
							//IL_0167: Unknown result type (might be due to invalid IL or missing references)
							//IL_016c: Expected O, but got Unknown
							//IL_06d2: Expected I, but got O
							//IL_06e2: Expected O, but got I
							//IL_0713: Expected O, but got I
							//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
							//IL_0e63: Expected O, but got Unknown
							//IL_0df2: Expected I, but got O
							//IL_0e02: Expected O, but got I
							//IL_0754: Unknown result type (might be due to invalid IL or missing references)
							//IL_0759: Expected O, but got Unknown
							//IL_01a0: Expected I, but got O
							//IL_01b0: Expected O, but got I
							//IL_01e1: Expected O, but got I
							//IL_0e97: Expected I, but got O
							//IL_0ea7: Expected O, but got I
							//IL_024d: Expected O, but got I
							//IL_0256: Unknown result type (might be due to invalid IL or missing references)
							//IL_025b: Expected O, but got Unknown
							//IL_0f34: Expected O, but got I4
							//IL_07ce: Expected I, but got O
							//IL_07de: Expected O, but got I
							//IL_081c: Expected O, but got I
							//IL_028f: Expected I, but got O
							//IL_029f: Expected O, but got I
							//IL_02d0: Expected O, but got I
							//IL_0862: Unknown result type (might be due to invalid IL or missing references)
							//IL_0867: Expected O, but got Unknown
							//IL_033c: Expected O, but got I
							//IL_0345: Unknown result type (might be due to invalid IL or missing references)
							//IL_034a: Expected O, but got Unknown
							//IL_0fa7: Expected O, but got I4
							//IL_08bf: Expected I, but got O
							//IL_08cf: Expected O, but got I
							//IL_0908: Expected O, but got I
							//IL_037e: Expected I, but got O
							//IL_038e: Expected O, but got I
							//IL_03bf: Expected O, but got I
							//IL_0984: Expected O, but got I
							//IL_098d: Unknown result type (might be due to invalid IL or missing references)
							//IL_0992: Expected O, but got Unknown
							//IL_042b: Expected O, but got I
							//IL_0434: Unknown result type (might be due to invalid IL or missing references)
							//IL_0439: Expected O, but got Unknown
							//IL_09c6: Expected I, but got O
							//IL_09d6: Expected O, but got I
							//IL_0a0f: Expected O, but got I
							//IL_046d: Expected I, but got O
							//IL_047d: Expected O, but got I
							//IL_04ae: Expected O, but got I
							//IL_0a83: Expected O, but got I
							//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
							//IL_0a91: Expected O, but got Unknown
							//IL_051a: Expected O, but got I
							//IL_0523: Unknown result type (might be due to invalid IL or missing references)
							//IL_0528: Expected O, but got Unknown
							//IL_0ac5: Expected I, but got O
							//IL_0ad5: Expected O, but got I
							//IL_0b0e: Expected O, but got I
							//IL_055c: Expected I, but got O
							//IL_056c: Expected O, but got I
							//IL_059d: Expected O, but got I
							//IL_0b82: Expected O, but got I
							//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
							//IL_0b90: Expected O, but got Unknown
							//IL_0bc4: Expected I, but got O
							//IL_0bd4: Expected O, but got I
							//IL_0c0d: Expected O, but got I
							SteamLeaderboardScoreController steamLeaderboardScoreController4 = _003C_003E4__this;
							bool flag4 = (object)_003C_003E4__this == null;
							object obj3 = this;
							bool flag6;
							object obj5 = default(object);
							bool flag7 = default(bool);
							LeaderboardScoreUploaded leaderboardScoreUploaded3 = default(LeaderboardScoreUploaded);
							if (!flag4)
							{
								bool flag5 = !steamLeaderboardScoreController4.verboseLogging;
								flag6 = flag7;
								if (!flag5)
								{
									object[] array = new object[6];
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
									object obj4 = 0;
									LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 + 32);
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									if (array == null)
									{
										throw new NullReferenceException();
									}
									object obj6 = default(object);
									if (obj6 != null)
									{
										nint num = (nint)array;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
										leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj7 = default(object);
										bool flag8 = obj7 == null;
										obj4 = obj6;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
										LeaderboardScoreUploaded leaderboardScoreUploaded2 = (LeaderboardScoreUploaded)0;
										obj3 = obj6;
										if (flag8)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											object obj8 = default(object);
											throw obj8;
										}
									}
									if (array.Length > 0)
									{
										array[0] = obj6;
										bool success = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Success;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
										obj4 = 0;
										leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 - 32);
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj9 = default(object);
										if (obj9 != null)
										{
											nint num2 = (nint)array;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj10 = default(object);
											bool flag9 = obj10 == null;
											obj4 = obj9;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded4 = (LeaderboardScoreUploaded)0;
											object obj11 = obj9;
											if (flag9)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj12 = default(object);
												throw obj12;
											}
										}
										if (array.Length > 1)
										{
											array[1] = obj9;
											bool scoreChanged = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
											obj4 = 0;
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 - 31);
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											object obj13 = default(object);
											if (obj13 != null)
											{
												nint num3 = (nint)array;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
												leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj14 = default(object);
												bool flag10 = obj14 == null;
												obj4 = obj13;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
												LeaderboardScoreUploaded leaderboardScoreUploaded5 = (LeaderboardScoreUploaded)0;
												object obj15 = obj13;
												if (flag10)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj16 = default(object);
													throw obj16;
												}
											}
											if (array.Length > 2)
											{
												array[2] = obj13;
												int score = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												obj4 = 0;
												leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 - 28);
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												object obj17 = default(object);
												if (obj17 != null)
												{
													nint num4 = (nint)array;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
													leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj18 = default(object);
													bool flag11 = obj18 == null;
													obj4 = obj17;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
													LeaderboardScoreUploaded leaderboardScoreUploaded6 = (LeaderboardScoreUploaded)0;
													object obj19 = obj17;
													if (flag11)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														object obj20 = default(object);
														throw obj20;
													}
												}
												if (array.Length > 3)
												{
													array[3] = obj17;
													int globalRankNew = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
													obj4 = 0;
													leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 - 24);
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object obj21 = default(object);
													if (obj21 != null)
													{
														nint num5 = (nint)array;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
														leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj22 = default(object);
														bool flag12 = obj22 == null;
														obj4 = obj21;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
														LeaderboardScoreUploaded leaderboardScoreUploaded7 = (LeaderboardScoreUploaded)0;
														object obj23 = obj21;
														if (flag12)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															object obj24 = default(object);
															throw obj24;
														}
													}
													if (array.Length > 4)
													{
														array[4] = obj21;
														int globalRankPrevious = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
														obj4 = 0;
														leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj5 - 20);
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														object obj25 = default(object);
														if (obj25 != null)
														{
															nint num6 = (nint)array;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
															leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj26 = default(object);
															bool flag13 = obj26 == null;
															obj4 = obj25;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
															LeaderboardScoreUploaded leaderboardScoreUploaded8 = (LeaderboardScoreUploaded)0;
															object obj27 = obj25;
															if (flag13)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																object obj28 = default(object);
																throw obj28;
															}
														}
														if (array.Length > 5)
														{
															array[5] = obj25;
															string message4 = string.Format("[SteamLeaderboardScoreController] Upload callback: uploadError={0}, success={1}, scoreChanged={2}, score={3}, rankNew={4}, rankPrev={5}", array);
															Debug.Log(message4);
															flag6 = false;
															goto IL_061a;
														}
													}
												}
											}
										}
									}
									throw new IndexOutOfRangeException();
								}
								goto IL_061a;
							}
							goto IL_1050;
							IL_061a:
							if (flag7)
							{
								object obj29 = obj5 + 32;
								_ = toSubmit;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								SteamLeaderboardScoreController steamLeaderboardScoreController5 = _003C_003E4__this;
								object arg4 = default(object);
								string message5 = $"[SteamLeaderboardScoreController] Failed to upload score {arg4} to '{steamLeaderboardScoreController5.leaderboardApiName}'.";
								Debug.LogError(message5);
								return;
							}
							object[] args;
							string format;
							if (((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged)
							{
								object[] array2 = new object[6];
								LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj5 + 32);
								_ = toSubmit;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
								object obj30 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								if (array2 != null)
								{
									LeaderboardScoreUploaded leaderboardScoreUploaded10 = default(LeaderboardScoreUploaded);
									if ((object)leaderboardScoreUploaded10 != null)
									{
										nint num7 = (nint)array2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
										leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj31 = default(object);
										bool flag14 = obj31 == null;
										obj30 = leaderboardScoreUploaded10;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
										LeaderboardScoreUploaded leaderboardScoreUploaded11 = (LeaderboardScoreUploaded)0;
										object obj32 = leaderboardScoreUploaded10;
										if (flag14)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											object obj33 = default(object);
											throw obj33;
										}
									}
									if (array2.Length > 0)
									{
										obj30 = array2 + 32;
										array2[0] = leaderboardScoreUploaded10;
										SteamLeaderboardScoreController steamLeaderboardScoreController6 = _003C_003E4__this;
										bool flag15 = (object)_003C_003E4__this == null;
										leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
										if (flag15)
										{
											goto IL_1196;
										}
										bool flag16 = steamLeaderboardScoreController6.leaderboardApiName == null;
										leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
										if (!flag16)
										{
											nint num8 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj34 = default(object);
											bool flag17 = obj34 == null;
											obj30 = steamLeaderboardScoreController6.leaderboardApiName;
											flag7 = flag6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
											object obj4 = steamLeaderboardScoreController6.leaderboardApiName;
											if (flag17)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj35 = default(object);
												throw obj35;
											}
										}
										if (array2.Length > 1)
										{
											obj30 = array2 + 40;
											array2[1] = steamLeaderboardScoreController6.leaderboardApiName;
											bool flag18 = forceUpdate;
											object obj36 = "ForceUpdate";
											if (!flag18)
											{
												obj36 = "KeepBest";
											}
											bool flag19 = obj36 == null;
											object obj37 = steamLeaderboardScoreController6.leaderboardApiName;
											if (!flag19)
											{
												nint num9 = (nint)array2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
												obj37 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj38 = default(object);
												bool flag20 = obj38 == null;
												obj30 = obj36;
												flag7 = flag6;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
												object obj39 = 0;
												object obj40 = obj36;
												if (flag20)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj41 = default(object);
													throw obj41;
												}
											}
											bool flag21 = array2.Length <= 2;
											flag7 = flag6;
											leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)obj37;
											if (!flag21)
											{
												array2[2] = obj36;
												int score2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
												obj30 = 0;
												leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj5 - 20);
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												object obj42 = default(object);
												if (obj42 != null)
												{
													nint num10 = (nint)array2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
													leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj43 = default(object);
													bool flag22 = obj43 == null;
													obj30 = obj42;
													flag7 = flag6;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
													LeaderboardScoreUploaded leaderboardScoreUploaded12 = (LeaderboardScoreUploaded)0;
													object obj44 = obj42;
													if (flag22)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														object obj45 = default(object);
														throw obj45;
													}
												}
												bool flag23 = array2.Length <= 3;
												flag7 = flag6;
												if (!flag23)
												{
													array2[3] = obj42;
													int globalRankNew2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
													obj30 = 0;
													leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj5 - 24);
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object obj46 = default(object);
													if (obj46 != null)
													{
														nint num11 = (nint)array2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
														leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj47 = default(object);
														bool flag24 = obj47 == null;
														obj30 = obj46;
														flag7 = flag6;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
														LeaderboardScoreUploaded leaderboardScoreUploaded13 = (LeaderboardScoreUploaded)0;
														object obj48 = obj46;
														if (flag24)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															object obj49 = default(object);
															throw obj49;
														}
													}
													bool flag25 = array2.Length <= 4;
													flag7 = flag6;
													if (!flag25)
													{
														array2[4] = obj46;
														int globalRankPrevious2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
														obj30 = 0;
														leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj5 - 28);
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														object obj50 = default(object);
														if (obj50 != null)
														{
															nint num12 = (nint)array2;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
															leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj51 = default(object);
															bool flag26 = obj51 == null;
															obj30 = obj50;
															flag7 = flag6;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
															LeaderboardScoreUploaded leaderboardScoreUploaded14 = (LeaderboardScoreUploaded)0;
															object obj52 = obj50;
															if (flag26)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																object obj53 = default(object);
																throw obj53;
															}
														}
														bool flag27 = array2.Length <= 5;
														flag7 = flag6;
														if (!flag27)
														{
															array2[5] = obj50;
															args = array2;
															format = "Submitted {0} to '{1}' ({2}). New best: {3}, rank: {4} (prev {5}).";
															goto IL_12d4;
														}
													}
												}
											}
											throw new IndexOutOfRangeException();
										}
									}
									throw new IndexOutOfRangeException();
								}
								goto IL_1196;
							}
							object[] array3 = new object[4];
							object obj54 = obj5 + 32;
							_ = toSubmit;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj55 = default(object);
							if (obj55 != null)
							{
								nint num13 = (nint)array3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v783 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
								LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj56 = default(object);
								bool flag28 = obj56 == null;
								object obj30 = obj55;
								if (flag28)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									string text3 = default(string);
									throw text3;
								}
							}
							array3[0] = obj55;
							SteamLeaderboardScoreController steamLeaderboardScoreController7 = _003C_003E4__this;
							if (steamLeaderboardScoreController7.leaderboardApiName != null)
							{
								nint num14 = (nint)array3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1203 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
								object obj57 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj58 = default(object);
								bool flag29 = obj58 == null;
								string leaderboardApiName = steamLeaderboardScoreController7.leaderboardApiName;
								if (flag29)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj59 = default(object);
									throw obj59;
								}
							}
							array3[1] = steamLeaderboardScoreController7.leaderboardApiName;
							bool flag30 = forceUpdate;
							object obj60 = "ForceUpdate";
							if (!flag30)
							{
								obj60 = "KeepBest";
							}
							if (obj60 != null)
							{
								nint num15 = (nint)array3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1438 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
								object obj61 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj62 = default(object);
								bool flag31 = obj62 == null;
								object obj63 = obj60;
								if (flag31)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj64 = default(object);
									throw obj64;
								}
							}
							array3[2] = obj60;
							int globalRankNew3 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
							object obj65 = obj5 - 16;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj66 = default(object);
							if (obj66 != null)
							{
								nint num16 = (nint)array3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1692 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
								object obj67 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj68 = default(object);
								bool flag32 = obj68 == null;
								object obj69 = obj66;
								if (flag32)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj70 = default(object);
									throw obj70;
								}
							}
							array3[3] = obj66;
							args = array3;
							format = "Submitted {0} to '{1}' ({2}). Best unchanged, current rank: {3}.";
							goto IL_12d4;
							IL_1196:
							throw new NullReferenceException();
							IL_1050:
							throw new NullReferenceException();
							IL_12d4:
							string text4 = string.Format(format, args);
							Debug.Log(text4);
							SteamLeaderboardScoreController steamLeaderboardScoreController8 = _003C_003E4__this;
							bool flag33 = (object)_003C_003E4__this == null;
							flag7 = false;
							leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
							obj3 = text4;
							if (!flag33)
							{
								if (!steamLeaderboardScoreController8.resetScoreAfterSubmit)
								{
									return;
								}
								steamLeaderboardScoreController8.pendingScore = 0;
								SteamLeaderboardScoreController steamLeaderboardScoreController9 = _003C_003E4__this;
								bool flag34 = (object)_003C_003E4__this == null;
								flag7 = false;
								leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
								obj3 = text4;
								if (!flag34)
								{
									if (steamLeaderboardScoreController9.verboseLogging)
									{
										Debug.Log("[SteamLeaderboardScoreController] Resetting pendingScore to 0 after successful submit (Reset Score After Submit enabled).");
									}
									return;
								}
							}
							goto IL_1050;
						});
					}
					if (!forceUpdate)
					{
						((LeaderboardData*)board)->UploadScoreKeepBest(toSubmit, callback);
					}
					else
					{
						((LeaderboardData*)board)->UploadScoreForceUpdate(toSubmit, callback);
					}
					return;
				}
				text = "' was not found or is not valid.";
				text2 = "[SteamLeaderboardScoreController] Leaderboard '";
			}
			else
			{
				steamLeaderboardScoreController2 = _003C_003E4__this;
				text = "'.";
				text2 = "[SteamLeaderboardScoreController] IO error while finding leaderboard '";
			}
			string message3 = text2 + steamLeaderboardScoreController2.leaderboardApiName + text;
			Debug.LogError(message3);
		}

		internal unsafe void _003CSubmitScoreInternal_003Eb__1(LeaderboardScoreUploaded result, bool uploadError)
		{
			//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
			//IL_1002: Expected O, but got Unknown
			//IL_004d: Expected O, but got I
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c91: Expected O, but got Unknown
			//IL_0665: Unknown result type (might be due to invalid IL or missing references)
			//IL_066a: Expected O, but got Unknown
			//IL_0681: Expected O, but got I
			//IL_0ccc: Expected I, but got O
			//IL_0cdc: Expected O, but got I
			//IL_00b1: Expected I, but got O
			//IL_00c1: Expected O, but got I
			//IL_00f2: Expected O, but got I
			//IL_0d56: Expected I, but got O
			//IL_0d66: Expected O, but got I
			//IL_015e: Expected O, but got I
			//IL_0167: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Expected O, but got Unknown
			//IL_06d2: Expected I, but got O
			//IL_06e2: Expected O, but got I
			//IL_0713: Expected O, but got I
			//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e63: Expected O, but got Unknown
			//IL_0df2: Expected I, but got O
			//IL_0e02: Expected O, but got I
			//IL_0754: Unknown result type (might be due to invalid IL or missing references)
			//IL_0759: Expected O, but got Unknown
			//IL_01a0: Expected I, but got O
			//IL_01b0: Expected O, but got I
			//IL_01e1: Expected O, but got I
			//IL_0e97: Expected I, but got O
			//IL_0ea7: Expected O, but got I
			//IL_024d: Expected O, but got I
			//IL_0256: Unknown result type (might be due to invalid IL or missing references)
			//IL_025b: Expected O, but got Unknown
			//IL_0f34: Expected O, but got I4
			//IL_07ce: Expected I, but got O
			//IL_07de: Expected O, but got I
			//IL_081c: Expected O, but got I
			//IL_028f: Expected I, but got O
			//IL_029f: Expected O, but got I
			//IL_02d0: Expected O, but got I
			//IL_0862: Unknown result type (might be due to invalid IL or missing references)
			//IL_0867: Expected O, but got Unknown
			//IL_033c: Expected O, but got I
			//IL_0345: Unknown result type (might be due to invalid IL or missing references)
			//IL_034a: Expected O, but got Unknown
			//IL_0fa7: Expected O, but got I4
			//IL_08bf: Expected I, but got O
			//IL_08cf: Expected O, but got I
			//IL_0908: Expected O, but got I
			//IL_037e: Expected I, but got O
			//IL_038e: Expected O, but got I
			//IL_03bf: Expected O, but got I
			//IL_0984: Expected O, but got I
			//IL_098d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0992: Expected O, but got Unknown
			//IL_042b: Expected O, but got I
			//IL_0434: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Expected O, but got Unknown
			//IL_09c6: Expected I, but got O
			//IL_09d6: Expected O, but got I
			//IL_0a0f: Expected O, but got I
			//IL_046d: Expected I, but got O
			//IL_047d: Expected O, but got I
			//IL_04ae: Expected O, but got I
			//IL_0a83: Expected O, but got I
			//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a91: Expected O, but got Unknown
			//IL_051a: Expected O, but got I
			//IL_0523: Unknown result type (might be due to invalid IL or missing references)
			//IL_0528: Expected O, but got Unknown
			//IL_0ac5: Expected I, but got O
			//IL_0ad5: Expected O, but got I
			//IL_0b0e: Expected O, but got I
			//IL_055c: Expected I, but got O
			//IL_056c: Expected O, but got I
			//IL_059d: Expected O, but got I
			//IL_0b82: Expected O, but got I
			//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b90: Expected O, but got Unknown
			//IL_0bc4: Expected I, but got O
			//IL_0bd4: Expected O, but got I
			//IL_0c0d: Expected O, but got I
			SteamLeaderboardScoreController steamLeaderboardScoreController = _003C_003E4__this;
			bool flag = (object)_003C_003E4__this == null;
			object obj = this;
			bool flag3;
			object obj3 = default(object);
			bool flag4 = default(bool);
			LeaderboardScoreUploaded leaderboardScoreUploaded3 = default(LeaderboardScoreUploaded);
			if (!flag)
			{
				bool flag2 = !steamLeaderboardScoreController.verboseLogging;
				flag3 = flag4;
				if (!flag2)
				{
					object[] array = new object[6];
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
					object obj2 = 0;
					LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 + 32);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (array != null)
					{
						object obj4 = default(object);
						if (obj4 != null)
						{
							nint num = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
							leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj5 = default(object);
							bool flag5 = obj5 == null;
							obj2 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
							LeaderboardScoreUploaded leaderboardScoreUploaded2 = (LeaderboardScoreUploaded)0;
							obj = obj4;
							if (flag5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj6 = default(object);
								throw obj6;
							}
						}
						if (array.Length > 0)
						{
							array[0] = obj4;
							bool success = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Success;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
							obj2 = 0;
							leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 - 32);
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object obj7 = default(object);
							if (obj7 != null)
							{
								nint num2 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
								leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj8 = default(object);
								bool flag6 = obj8 == null;
								obj2 = obj7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
								LeaderboardScoreUploaded leaderboardScoreUploaded4 = (LeaderboardScoreUploaded)0;
								object obj9 = obj7;
								if (flag6)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj10 = default(object);
									throw obj10;
								}
							}
							if (array.Length > 1)
							{
								array[1] = obj7;
								bool scoreChanged = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
								obj2 = 0;
								leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 - 31);
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object obj11 = default(object);
								if (obj11 != null)
								{
									nint num3 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
									leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj12 = default(object);
									bool flag7 = obj12 == null;
									obj2 = obj11;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
									LeaderboardScoreUploaded leaderboardScoreUploaded5 = (LeaderboardScoreUploaded)0;
									object obj13 = obj11;
									if (flag7)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj14 = default(object);
										throw obj14;
									}
								}
								if (array.Length > 2)
								{
									array[2] = obj11;
									int score = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
									obj2 = 0;
									leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 - 28);
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object obj15 = default(object);
									if (obj15 != null)
									{
										nint num4 = (nint)array;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
										leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj16 = default(object);
										bool flag8 = obj16 == null;
										obj2 = obj15;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
										LeaderboardScoreUploaded leaderboardScoreUploaded6 = (LeaderboardScoreUploaded)0;
										object obj17 = obj15;
										if (flag8)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											object obj18 = default(object);
											throw obj18;
										}
									}
									if (array.Length > 3)
									{
										array[3] = obj15;
										int globalRankNew = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
										obj2 = 0;
										leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 - 24);
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj19 = default(object);
										if (obj19 != null)
										{
											nint num5 = (nint)array;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj20 = default(object);
											bool flag9 = obj20 == null;
											obj2 = obj19;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded7 = (LeaderboardScoreUploaded)0;
											object obj21 = obj19;
											if (flag9)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj22 = default(object);
												throw obj22;
											}
										}
										if (array.Length > 4)
										{
											array[4] = obj19;
											int globalRankPrevious = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
											obj2 = 0;
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj3 - 20);
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											object obj23 = default(object);
											if (obj23 != null)
											{
												nint num6 = (nint)array;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
												leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj24 = default(object);
												bool flag10 = obj24 == null;
												obj2 = obj23;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
												LeaderboardScoreUploaded leaderboardScoreUploaded8 = (LeaderboardScoreUploaded)0;
												object obj25 = obj23;
												if (flag10)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj26 = default(object);
													throw obj26;
												}
											}
											if (array.Length > 5)
											{
												array[5] = obj23;
												string message = string.Format("[SteamLeaderboardScoreController] Upload callback: uploadError={0}, success={1}, scoreChanged={2}, score={3}, rankNew={4}, rankPrev={5}", array);
												Debug.Log(message);
												flag3 = false;
												goto IL_061a;
											}
										}
									}
								}
							}
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
				}
				goto IL_061a;
			}
			goto IL_1050;
			IL_061a:
			object[] args;
			string format;
			if (!flag4)
			{
				if (((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged)
				{
					object[] array2 = new object[6];
					LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj3 + 32);
					_ = toSubmit;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					object obj27 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (array2 != null)
					{
						LeaderboardScoreUploaded leaderboardScoreUploaded10 = default(LeaderboardScoreUploaded);
						if ((object)leaderboardScoreUploaded10 != null)
						{
							nint num7 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
							leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj28 = default(object);
							bool flag11 = obj28 == null;
							obj27 = leaderboardScoreUploaded10;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
							LeaderboardScoreUploaded leaderboardScoreUploaded11 = (LeaderboardScoreUploaded)0;
							object obj29 = leaderboardScoreUploaded10;
							if (flag11)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj30 = default(object);
								throw obj30;
							}
						}
						if (array2.Length > 0)
						{
							obj27 = array2 + 32;
							array2[0] = leaderboardScoreUploaded10;
							SteamLeaderboardScoreController steamLeaderboardScoreController2 = _003C_003E4__this;
							bool flag12 = (object)_003C_003E4__this == null;
							leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
							if (flag12)
							{
								goto IL_1196;
							}
							bool flag13 = steamLeaderboardScoreController2.leaderboardApiName == null;
							leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
							if (!flag13)
							{
								nint num8 = (nint)array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
								leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj31 = default(object);
								bool flag14 = obj31 == null;
								obj27 = steamLeaderboardScoreController2.leaderboardApiName;
								flag4 = flag3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
								LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
								object obj2 = steamLeaderboardScoreController2.leaderboardApiName;
								if (flag14)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj32 = default(object);
									throw obj32;
								}
							}
							if (array2.Length > 1)
							{
								obj27 = array2 + 40;
								array2[1] = steamLeaderboardScoreController2.leaderboardApiName;
								bool flag15 = forceUpdate;
								object obj33 = "ForceUpdate";
								if (!flag15)
								{
									obj33 = "KeepBest";
								}
								bool flag16 = obj33 == null;
								object obj34 = steamLeaderboardScoreController2.leaderboardApiName;
								if (!flag16)
								{
									nint num9 = (nint)array2;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
									obj34 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj35 = default(object);
									bool flag17 = obj35 == null;
									obj27 = obj33;
									flag4 = flag3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
									object obj36 = 0;
									object obj37 = obj33;
									if (flag17)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj38 = default(object);
										throw obj38;
									}
								}
								bool flag18 = array2.Length <= 2;
								flag4 = flag3;
								leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)obj34;
								if (!flag18)
								{
									array2[2] = obj33;
									int score2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
									obj27 = 0;
									leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj3 - 20);
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object obj39 = default(object);
									if (obj39 != null)
									{
										nint num10 = (nint)array2;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
										leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj40 = default(object);
										bool flag19 = obj40 == null;
										obj27 = obj39;
										flag4 = flag3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
										LeaderboardScoreUploaded leaderboardScoreUploaded12 = (LeaderboardScoreUploaded)0;
										object obj41 = obj39;
										if (flag19)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											object obj42 = default(object);
											throw obj42;
										}
									}
									bool flag20 = array2.Length <= 3;
									flag4 = flag3;
									if (!flag20)
									{
										array2[3] = obj39;
										int globalRankNew2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
										obj27 = 0;
										leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj3 - 24);
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object obj43 = default(object);
										if (obj43 != null)
										{
											nint num11 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj44 = default(object);
											bool flag21 = obj44 == null;
											obj27 = obj43;
											flag4 = flag3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded13 = (LeaderboardScoreUploaded)0;
											object obj45 = obj43;
											if (flag21)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj46 = default(object);
												throw obj46;
											}
										}
										bool flag22 = array2.Length <= 4;
										flag4 = flag3;
										if (!flag22)
										{
											array2[4] = obj43;
											int globalRankPrevious2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
											obj27 = 0;
											leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj3 - 28);
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											object obj47 = default(object);
											if (obj47 != null)
											{
												nint num12 = (nint)array2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
												leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj48 = default(object);
												bool flag23 = obj48 == null;
												obj27 = obj47;
												flag4 = flag3;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
												LeaderboardScoreUploaded leaderboardScoreUploaded14 = (LeaderboardScoreUploaded)0;
												object obj49 = obj47;
												if (flag23)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj50 = default(object);
													throw obj50;
												}
											}
											bool flag24 = array2.Length <= 5;
											flag4 = flag3;
											if (!flag24)
											{
												array2[5] = obj47;
												args = array2;
												format = "Submitted {0} to '{1}' ({2}). New best: {3}, rank: {4} (prev {5}).";
												goto IL_12d4;
											}
										}
									}
								}
								throw new IndexOutOfRangeException();
							}
						}
						throw new IndexOutOfRangeException();
					}
					goto IL_1196;
				}
				object[] array3 = new object[4];
				object obj51 = obj3 + 32;
				_ = toSubmit;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj52 = default(object);
				if (obj52 != null)
				{
					nint num13 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v783 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
					LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj53 = default(object);
					bool flag25 = obj53 == null;
					object obj27 = obj52;
					if (flag25)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text = default(string);
						throw text;
					}
				}
				array3[0] = obj52;
				SteamLeaderboardScoreController steamLeaderboardScoreController3 = _003C_003E4__this;
				if (steamLeaderboardScoreController3.leaderboardApiName != null)
				{
					nint num14 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1203 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
					object obj54 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj55 = default(object);
					bool flag26 = obj55 == null;
					string leaderboardApiName = steamLeaderboardScoreController3.leaderboardApiName;
					if (flag26)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj56 = default(object);
						throw obj56;
					}
				}
				array3[1] = steamLeaderboardScoreController3.leaderboardApiName;
				bool flag27 = forceUpdate;
				object obj57 = "ForceUpdate";
				if (!flag27)
				{
					obj57 = "KeepBest";
				}
				if (obj57 != null)
				{
					nint num15 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1438 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
					object obj58 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj59 = default(object);
					bool flag28 = obj59 == null;
					object obj60 = obj57;
					if (flag28)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj61 = default(object);
						throw obj61;
					}
				}
				array3[2] = obj57;
				int globalRankNew3 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
				object obj62 = obj3 - 16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj63 = default(object);
				if (obj63 != null)
				{
					nint num16 = (nint)array3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1692 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
					object obj64 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj65 = default(object);
					bool flag29 = obj65 == null;
					object obj66 = obj63;
					if (flag29)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj67 = default(object);
						throw obj67;
					}
				}
				array3[3] = obj63;
				args = array3;
				format = "Submitted {0} to '{1}' ({2}). Best unchanged, current rank: {3}.";
				goto IL_12d4;
			}
			object obj68 = obj3 + 32;
			_ = toSubmit;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			SteamLeaderboardScoreController steamLeaderboardScoreController4 = _003C_003E4__this;
			object arg = default(object);
			string message2 = $"[SteamLeaderboardScoreController] Failed to upload score {arg} to '{steamLeaderboardScoreController4.leaderboardApiName}'.";
			Debug.LogError(message2);
			return;
			IL_1196:
			throw new NullReferenceException();
			IL_1050:
			throw new NullReferenceException();
			IL_12d4:
			string text2 = string.Format(format, args);
			Debug.Log(text2);
			SteamLeaderboardScoreController steamLeaderboardScoreController5 = _003C_003E4__this;
			bool flag30 = (object)_003C_003E4__this == null;
			flag4 = false;
			leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
			obj = text2;
			if (!flag30)
			{
				if (!steamLeaderboardScoreController5.resetScoreAfterSubmit)
				{
					return;
				}
				steamLeaderboardScoreController5.pendingScore = 0;
				SteamLeaderboardScoreController steamLeaderboardScoreController6 = _003C_003E4__this;
				bool flag31 = (object)_003C_003E4__this == null;
				flag4 = false;
				leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
				obj = text2;
				if (!flag31)
				{
					if (steamLeaderboardScoreController6.verboseLogging)
					{
						Debug.Log("[SteamLeaderboardScoreController] Resetting pendingScore to 0 after successful submit (Reset Score After Submit enabled).");
					}
					return;
				}
			}
			goto IL_1050;
		}
	}

	private static SteamLeaderboardScoreController _003CInstance_003Ek__BackingField;

	private string leaderboardApiName;

	private int startingScore;

	private bool resetScoreAfterSubmit;

	private bool useForceUpdate;

	private bool verboseLogging;

	private int pendingScore;

	public static SteamLeaderboardScoreController Instance
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

	public int CurrentScore => pendingScore;

	private void Awake()
	{
		if (_003CInstance_003Ek__BackingField == null)
		{
			_003CInstance_003Ek__BackingField = this;
		}
		pendingScore = startingScore;
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	public void AddToScore(int amount)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A909]");
		bool flag = (nint)0 < (nint)0;
		int num = pendingScore + amount;
		int num2 = 0;
		if (!flag)
		{
			num2 = num;
		}
		bool flag2 = !verboseLogging;
		pendingScore = num2;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			string message = $"[SteamLeaderboardScoreController] AddToScore: +{arg} => pendingScore={arg2}";
			Debug.Log(message);
		}
	}

	public void SetScore(int value)
	{
		bool flag = value < 0;
		int num = 0;
		if (!flag)
		{
			num = value;
		}
		bool flag2 = !verboseLogging;
		pendingScore = num;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[SteamLeaderboardScoreController] SetScore: pendingScore={arg}";
			Debug.Log(message);
		}
	}

	public void ResetScore()
	{
		bool flag = !verboseLogging;
		pendingScore = 0;
		if (!flag)
		{
			Debug.Log("[SteamLeaderboardScoreController] ResetScore: pendingScore=0");
		}
	}

	public void SubmitScore()
	{
		SubmitScoreInternal(useForceUpdate);
	}

	public void SubmitScoreForceUpdate()
	{
		SubmitScoreInternal(forceUpdate: true);
	}

	private unsafe void SubmitScoreInternal(bool forceUpdate)
	{
		//IL_0075: Expected I, but got O
		//IL_00e4: Expected I, but got O
		//IL_00f4: Expected O, but got I
		//IL_0179: Expected I, but got O
		//IL_0189: Expected O, but got I
		//IL_0207: Expected I, but got O
		//IL_0217: Expected O, but got I
		//IL_0295: Expected I, but got O
		//IL_02a5: Expected O, but got I
		_003C_003Ec__DisplayClass19_0 CS_0024_003C_003E8__locals31 = new _003C_003Ec__DisplayClass19_0();
		CS_0024_003C_003E8__locals31._003C_003E4__this = this;
		CS_0024_003C_003E8__locals31.forceUpdate = forceUpdate;
		if (verboseLogging)
		{
			object[] array = new object[5];
			if (leaderboardApiName != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj2 = default(object);
					throw obj2;
				}
			}
			array[0] = leaderboardApiName;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj3 = default(object);
			if (obj3 != null)
			{
				nint num2 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v543 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj5 = default(object);
				bool flag = obj5 == null;
				object obj6 = obj3;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj7 = default(object);
					throw obj7;
				}
			}
			array[1] = obj3;
			bool flag2 = CS_0024_003C_003E8__locals31.forceUpdate;
			object obj8 = "ForceUpdate";
			if (!flag2)
			{
				obj8 = "KeepBest";
			}
			if (obj8 != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v623 @ rdx_v45 (Il2CppClass<System.Object[]>)+40]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj10 = default(object);
				bool flag3 = obj10 == null;
				object obj11 = obj8;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj12 = default(object);
					throw obj12;
				}
			}
			array[2] = obj8;
			int frameCount = Time.frameCount;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj13 = default(object);
			if (obj13 != null)
			{
				nint num4 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v736 @ rdx_v43 (Il2CppClass<System.Object[]>)+40]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj15 = default(object);
				bool flag4 = obj15 == null;
				object obj16 = obj13;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj17 = default(object);
					throw obj17;
				}
			}
			array[3] = obj13;
			float unscaledTime = Time.unscaledTime;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj18 = default(object);
			if (obj18 != null)
			{
				nint num5 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ rdx_v41 (Il2CppClass<System.Object[]>)+40]");
				object obj19 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj20 = default(object);
				bool flag5 = obj20 == null;
				object obj21 = obj18;
				if (flag5)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj22 = default(object);
					throw obj22;
				}
			}
			array[4] = obj18;
			string message = string.Format("[SteamLeaderboardScoreController] SubmitScore: attempt -> api='{0}', pendingScore={1}, mode={2}, frame={3}, t={4:F3}", array);
			Debug.Log(message);
		}
		if (!string.IsNullOrWhiteSpace(leaderboardApiName))
		{
			CS_0024_003C_003E8__locals31.toSubmit = pendingScore;
			if (verboseLogging)
			{
				string message2 = "[SteamLeaderboardScoreController] SubmitScore: resolving leaderboard '" + leaderboardApiName + "'...";
				Debug.Log(message2);
			}
			Action<LeaderboardData, bool> callback = delegate(LeaderboardData board, bool ioError)
			{
				//IL_0021: Expected O, but got I4
				SteamLeaderboardScoreController steamLeaderboardScoreController = CS_0024_003C_003E8__locals31._003C_003E4__this;
				bool flag6 = !steamLeaderboardScoreController.verboseLogging;
				object obj23 = ioError;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					bool isValid = ((LeaderboardData*)board)->IsValid;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					object obj24 = default(object);
					string message3 = $"[SteamLeaderboardScoreController] LeaderboardData.Get callback: ioError={arg}, isValid={obj24}";
					Debug.Log(message3);
					obj23 = obj24;
					bool flag7 = ioError;
				}
				SteamLeaderboardScoreController steamLeaderboardScoreController2;
				string text;
				string text2;
				if (!ioError)
				{
					bool isValid2 = ((LeaderboardData*)board)->IsValid;
					steamLeaderboardScoreController2 = CS_0024_003C_003E8__locals31._003C_003E4__this;
					if (isValid2)
					{
						if (steamLeaderboardScoreController2.verboseLogging != ioError)
						{
							bool flag8 = CS_0024_003C_003E8__locals31.forceUpdate;
							object arg2 = "ForceUpdate";
							if (!flag8)
							{
								arg2 = "KeepBest";
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							SteamLeaderboardScoreController steamLeaderboardScoreController3 = CS_0024_003C_003E8__locals31._003C_003E4__this;
							object arg3 = default(object);
							string message4 = $"[SteamLeaderboardScoreController] Uploading ({arg2}) score={arg3} to '{steamLeaderboardScoreController3.leaderboardApiName}'...";
							Debug.Log(message4);
						}
						Action<LeaderboardScoreUploaded, bool> callback2 = CS_0024_003C_003E8__locals31._003C_003E9__1;
						if (CS_0024_003C_003E8__locals31._003C_003E9__1 == null)
						{
							callback2 = (CS_0024_003C_003E8__locals31._003C_003E9__1 = delegate
							{
								//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
								//IL_1002: Expected O, but got Unknown
								//IL_004d: Expected O, but got I
								//IL_0056: Unknown result type (might be due to invalid IL or missing references)
								//IL_005b: Expected O, but got Unknown
								//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
								//IL_0c91: Expected O, but got Unknown
								//IL_0665: Unknown result type (might be due to invalid IL or missing references)
								//IL_066a: Expected O, but got Unknown
								//IL_0681: Expected O, but got I
								//IL_0ccc: Expected I, but got O
								//IL_0cdc: Expected O, but got I
								//IL_00b1: Expected I, but got O
								//IL_00c1: Expected O, but got I
								//IL_00f2: Expected O, but got I
								//IL_0d56: Expected I, but got O
								//IL_0d66: Expected O, but got I
								//IL_015e: Expected O, but got I
								//IL_0167: Unknown result type (might be due to invalid IL or missing references)
								//IL_016c: Expected O, but got Unknown
								//IL_06d2: Expected I, but got O
								//IL_06e2: Expected O, but got I
								//IL_0713: Expected O, but got I
								//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
								//IL_0e63: Expected O, but got Unknown
								//IL_0df2: Expected I, but got O
								//IL_0e02: Expected O, but got I
								//IL_0754: Unknown result type (might be due to invalid IL or missing references)
								//IL_0759: Expected O, but got Unknown
								//IL_01a0: Expected I, but got O
								//IL_01b0: Expected O, but got I
								//IL_01e1: Expected O, but got I
								//IL_0e97: Expected I, but got O
								//IL_0ea7: Expected O, but got I
								//IL_024d: Expected O, but got I
								//IL_0256: Unknown result type (might be due to invalid IL or missing references)
								//IL_025b: Expected O, but got Unknown
								//IL_0f34: Expected O, but got I4
								//IL_07ce: Expected I, but got O
								//IL_07de: Expected O, but got I
								//IL_081c: Expected O, but got I
								//IL_028f: Expected I, but got O
								//IL_029f: Expected O, but got I
								//IL_02d0: Expected O, but got I
								//IL_0862: Unknown result type (might be due to invalid IL or missing references)
								//IL_0867: Expected O, but got Unknown
								//IL_033c: Expected O, but got I
								//IL_0345: Unknown result type (might be due to invalid IL or missing references)
								//IL_034a: Expected O, but got Unknown
								//IL_0fa7: Expected O, but got I4
								//IL_08bf: Expected I, but got O
								//IL_08cf: Expected O, but got I
								//IL_0908: Expected O, but got I
								//IL_037e: Expected I, but got O
								//IL_038e: Expected O, but got I
								//IL_03bf: Expected O, but got I
								//IL_0984: Expected O, but got I
								//IL_098d: Unknown result type (might be due to invalid IL or missing references)
								//IL_0992: Expected O, but got Unknown
								//IL_042b: Expected O, but got I
								//IL_0434: Unknown result type (might be due to invalid IL or missing references)
								//IL_0439: Expected O, but got Unknown
								//IL_09c6: Expected I, but got O
								//IL_09d6: Expected O, but got I
								//IL_0a0f: Expected O, but got I
								//IL_046d: Expected I, but got O
								//IL_047d: Expected O, but got I
								//IL_04ae: Expected O, but got I
								//IL_0a83: Expected O, but got I
								//IL_0a8c: Unknown result type (might be due to invalid IL or missing references)
								//IL_0a91: Expected O, but got Unknown
								//IL_051a: Expected O, but got I
								//IL_0523: Unknown result type (might be due to invalid IL or missing references)
								//IL_0528: Expected O, but got Unknown
								//IL_0ac5: Expected I, but got O
								//IL_0ad5: Expected O, but got I
								//IL_0b0e: Expected O, but got I
								//IL_055c: Expected I, but got O
								//IL_056c: Expected O, but got I
								//IL_059d: Expected O, but got I
								//IL_0b82: Expected O, but got I
								//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
								//IL_0b90: Expected O, but got Unknown
								//IL_0bc4: Expected I, but got O
								//IL_0bd4: Expected O, but got I
								//IL_0c0d: Expected O, but got I
								SteamLeaderboardScoreController steamLeaderboardScoreController4 = CS_0024_003C_003E8__locals31._003C_003E4__this;
								bool flag9 = (object)CS_0024_003C_003E8__locals31._003C_003E4__this == null;
								object obj25 = CS_0024_003C_003E8__locals31;
								bool flag11;
								object obj27 = default(object);
								bool flag12 = default(bool);
								LeaderboardScoreUploaded leaderboardScoreUploaded3 = default(LeaderboardScoreUploaded);
								if (!flag9)
								{
									bool flag10 = !steamLeaderboardScoreController4.verboseLogging;
									flag11 = flag12;
									if (!flag10)
									{
										object[] array2 = new object[6];
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
										object obj26 = 0;
										LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 + 32);
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										if (array2 == null)
										{
											throw new NullReferenceException();
										}
										object obj28 = default(object);
										if (obj28 != null)
										{
											nint num6 = (nint)array2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj29 = default(object);
											bool flag13 = obj29 == null;
											obj26 = obj28;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rdx_v140 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded2 = (LeaderboardScoreUploaded)0;
											obj25 = obj28;
											if (flag13)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj30 = default(object);
												throw obj30;
											}
										}
										if (array2.Length > 0)
										{
											array2[0] = obj28;
											bool success = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Success;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
											obj26 = 0;
											leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 - 32);
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											object obj31 = default(object);
											if (obj31 != null)
											{
												nint num7 = (nint)array2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
												leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj32 = default(object);
												bool flag14 = obj32 == null;
												obj26 = obj31;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1026 @ rdx_v138 (Il2CppClass<System.Object[]>)+40]");
												LeaderboardScoreUploaded leaderboardScoreUploaded4 = (LeaderboardScoreUploaded)0;
												object obj33 = obj31;
												if (flag14)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj34 = default(object);
													throw obj34;
												}
											}
											if (array2.Length > 1)
											{
												array2[1] = obj31;
												bool scoreChanged = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
												obj26 = 0;
												leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 - 31);
												Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
												object obj35 = default(object);
												if (obj35 != null)
												{
													nint num8 = (nint)array2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
													leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj36 = default(object);
													bool flag15 = obj36 == null;
													obj26 = obj35;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1298 @ rdx_v136 (Il2CppClass<System.Object[]>)+40]");
													LeaderboardScoreUploaded leaderboardScoreUploaded5 = (LeaderboardScoreUploaded)0;
													object obj37 = obj35;
													if (flag15)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														object obj38 = default(object);
														throw obj38;
													}
												}
												if (array2.Length > 2)
												{
													array2[2] = obj35;
													int score = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
													obj26 = 0;
													leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 - 28);
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object obj39 = default(object);
													if (obj39 != null)
													{
														nint num9 = (nint)array2;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
														leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj40 = default(object);
														bool flag16 = obj40 == null;
														obj26 = obj39;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1644 @ rdx_v134 (Il2CppClass<System.Object[]>)+40]");
														LeaderboardScoreUploaded leaderboardScoreUploaded6 = (LeaderboardScoreUploaded)0;
														object obj41 = obj39;
														if (flag16)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															object obj42 = default(object);
															throw obj42;
														}
													}
													if (array2.Length > 3)
													{
														array2[3] = obj39;
														int globalRankNew = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
														obj26 = 0;
														leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 - 24);
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														object obj43 = default(object);
														if (obj43 != null)
														{
															nint num10 = (nint)array2;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
															leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj44 = default(object);
															bool flag17 = obj44 == null;
															obj26 = obj43;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1750 @ rdx_v132 (Il2CppClass<System.Object[]>)+40]");
															LeaderboardScoreUploaded leaderboardScoreUploaded7 = (LeaderboardScoreUploaded)0;
															object obj45 = obj43;
															if (flag17)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																object obj46 = default(object);
																throw obj46;
															}
														}
														if (array2.Length > 4)
														{
															array2[4] = obj43;
															int globalRankPrevious = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
															obj26 = 0;
															leaderboardScoreUploaded = (LeaderboardScoreUploaded)(obj27 - 20);
															Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
															object obj47 = default(object);
															if (obj47 != null)
															{
																nint num11 = (nint)array2;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
																leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj48 = default(object);
																bool flag18 = obj48 == null;
																obj26 = obj47;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1825 @ rdx_v130 (Il2CppClass<System.Object[]>)+40]");
																LeaderboardScoreUploaded leaderboardScoreUploaded8 = (LeaderboardScoreUploaded)0;
																object obj49 = obj47;
																if (flag18)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	object obj50 = default(object);
																	throw obj50;
																}
															}
															if (array2.Length > 5)
															{
																array2[5] = obj47;
																string message6 = string.Format("[SteamLeaderboardScoreController] Upload callback: uploadError={0}, success={1}, scoreChanged={2}, score={3}, rankNew={4}, rankPrev={5}", array2);
																Debug.Log(message6);
																flag11 = false;
																goto IL_061a;
															}
														}
													}
												}
											}
										}
										throw new IndexOutOfRangeException();
									}
									goto IL_061a;
								}
								goto IL_1050;
								IL_061a:
								if (flag12)
								{
									object obj51 = obj27 + 32;
									_ = CS_0024_003C_003E8__locals31.toSubmit;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									SteamLeaderboardScoreController steamLeaderboardScoreController5 = CS_0024_003C_003E8__locals31._003C_003E4__this;
									object arg4 = default(object);
									string message7 = $"[SteamLeaderboardScoreController] Failed to upload score {arg4} to '{steamLeaderboardScoreController5.leaderboardApiName}'.";
									Debug.LogError(message7);
									return;
								}
								object[] args;
								string format;
								if (((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->ScoreChanged)
								{
									object[] array3 = new object[6];
									LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj27 + 32);
									_ = CS_0024_003C_003E8__locals31.toSubmit;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
									object obj52 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									if (array3 != null)
									{
										LeaderboardScoreUploaded leaderboardScoreUploaded10 = default(LeaderboardScoreUploaded);
										if ((object)leaderboardScoreUploaded10 != null)
										{
											nint num12 = (nint)array3;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
											leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj53 = default(object);
											bool flag19 = obj53 == null;
											obj52 = leaderboardScoreUploaded10;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v744 @ rdx_v102 (Il2CppClass<System.Object[]>)+40]");
											LeaderboardScoreUploaded leaderboardScoreUploaded11 = (LeaderboardScoreUploaded)0;
											object obj54 = leaderboardScoreUploaded10;
											if (flag19)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												object obj55 = default(object);
												throw obj55;
											}
										}
										if (array3.Length > 0)
										{
											obj52 = array3 + 32;
											array3[0] = leaderboardScoreUploaded10;
											SteamLeaderboardScoreController steamLeaderboardScoreController6 = CS_0024_003C_003E8__locals31._003C_003E4__this;
											bool flag20 = (object)CS_0024_003C_003E8__locals31._003C_003E4__this == null;
											leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
											if (flag20)
											{
												goto IL_1196;
											}
											bool flag21 = steamLeaderboardScoreController6.leaderboardApiName == null;
											leaderboardScoreUploaded9 = leaderboardScoreUploaded10;
											if (!flag21)
											{
												nint num13 = (nint)array3;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
												leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
												object obj56 = default(object);
												bool flag22 = obj56 == null;
												obj52 = steamLeaderboardScoreController6.leaderboardApiName;
												flag12 = flag11;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1158 @ rdx_v100 (Il2CppClass<System.Object[]>)+40]");
												LeaderboardScoreUploaded leaderboardScoreUploaded = (LeaderboardScoreUploaded)0;
												object obj26 = steamLeaderboardScoreController6.leaderboardApiName;
												if (flag22)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
													object obj57 = default(object);
													throw obj57;
												}
											}
											if (array3.Length > 1)
											{
												obj52 = array3 + 40;
												array3[1] = steamLeaderboardScoreController6.leaderboardApiName;
												bool flag23 = CS_0024_003C_003E8__locals31.forceUpdate;
												object obj58 = "ForceUpdate";
												if (!flag23)
												{
													obj58 = "KeepBest";
												}
												bool flag24 = obj58 == null;
												object obj59 = steamLeaderboardScoreController6.leaderboardApiName;
												if (!flag24)
												{
													nint num14 = (nint)array3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
													obj59 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
													object obj60 = default(object);
													bool flag25 = obj60 == null;
													obj52 = obj58;
													flag12 = flag11;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1408 @ rdx_v98 (Il2CppClass<System.Object[]>)+40]");
													object obj61 = 0;
													object obj62 = obj58;
													if (flag25)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
														object obj63 = default(object);
														throw obj63;
													}
												}
												bool flag26 = array3.Length <= 2;
												flag12 = flag11;
												leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)obj59;
												if (!flag26)
												{
													array3[2] = obj58;
													int score2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->Score;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
													obj52 = 0;
													leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj27 - 20);
													Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
													object obj64 = default(object);
													if (obj64 != null)
													{
														nint num15 = (nint)array3;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
														leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
														object obj65 = default(object);
														bool flag27 = obj65 == null;
														obj52 = obj64;
														flag12 = flag11;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1679 @ rdx_v96 (Il2CppClass<System.Object[]>)+40]");
														LeaderboardScoreUploaded leaderboardScoreUploaded12 = (LeaderboardScoreUploaded)0;
														object obj66 = obj64;
														if (flag27)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
															object obj67 = default(object);
															throw obj67;
														}
													}
													bool flag28 = array3.Length <= 3;
													flag12 = flag11;
													if (!flag28)
													{
														array3[3] = obj64;
														int globalRankNew2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
														obj52 = 0;
														leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj27 - 24);
														Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
														object obj68 = default(object);
														if (obj68 != null)
														{
															nint num16 = (nint)array3;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
															leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
															object obj69 = default(object);
															bool flag29 = obj69 == null;
															obj52 = obj68;
															flag12 = flag11;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1786 @ rdx_v94 (Il2CppClass<System.Object[]>)+40]");
															LeaderboardScoreUploaded leaderboardScoreUploaded13 = (LeaderboardScoreUploaded)0;
															object obj70 = obj68;
															if (flag29)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																object obj71 = default(object);
																throw obj71;
															}
														}
														bool flag30 = array3.Length <= 4;
														flag12 = flag11;
														if (!flag30)
														{
															array3[4] = obj68;
															int globalRankPrevious2 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankPrevious;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
															obj52 = 0;
															leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)(obj27 - 28);
															Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
															object obj72 = default(object);
															if (obj72 != null)
															{
																nint num17 = (nint)array3;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
																leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
																object obj73 = default(object);
																bool flag31 = obj73 == null;
																obj52 = obj72;
																flag12 = flag11;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ rdx_v92 (Il2CppClass<System.Object[]>)+40]");
																LeaderboardScoreUploaded leaderboardScoreUploaded14 = (LeaderboardScoreUploaded)0;
																object obj74 = obj72;
																if (flag31)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																	object obj75 = default(object);
																	throw obj75;
																}
															}
															bool flag32 = array3.Length <= 5;
															flag12 = flag11;
															if (!flag32)
															{
																array3[5] = obj72;
																args = array3;
																format = "Submitted {0} to '{1}' ({2}). New best: {3}, rank: {4} (prev {5}).";
																goto IL_12d4;
															}
														}
													}
												}
												throw new IndexOutOfRangeException();
											}
										}
										throw new IndexOutOfRangeException();
									}
									goto IL_1196;
								}
								object[] array4 = new object[4];
								object obj76 = obj27 + 32;
								_ = CS_0024_003C_003E8__locals31.toSubmit;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object obj77 = default(object);
								if (obj77 != null)
								{
									nint num18 = (nint)array4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v783 @ rdx_v71 (Il2CppClass<System.Object[]>)+40]");
									LeaderboardScoreUploaded leaderboardScoreUploaded9 = (LeaderboardScoreUploaded)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj78 = default(object);
									bool flag33 = obj78 == null;
									object obj52 = obj77;
									if (flag33)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										string text3 = default(string);
										throw text3;
									}
								}
								array4[0] = obj77;
								SteamLeaderboardScoreController steamLeaderboardScoreController7 = CS_0024_003C_003E8__locals31._003C_003E4__this;
								if (steamLeaderboardScoreController7.leaderboardApiName != null)
								{
									nint num19 = (nint)array4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1203 @ rdx_v69 (Il2CppClass<System.Object[]>)+40]");
									object obj79 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj80 = default(object);
									bool flag34 = obj80 == null;
									string text4 = steamLeaderboardScoreController7.leaderboardApiName;
									if (flag34)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj81 = default(object);
										throw obj81;
									}
								}
								array4[1] = steamLeaderboardScoreController7.leaderboardApiName;
								bool flag35 = CS_0024_003C_003E8__locals31.forceUpdate;
								object obj82 = "ForceUpdate";
								if (!flag35)
								{
									obj82 = "KeepBest";
								}
								if (obj82 != null)
								{
									nint num20 = (nint)array4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1438 @ rdx_v67 (Il2CppClass<System.Object[]>)+40]");
									object obj83 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj84 = default(object);
									bool flag36 = obj84 == null;
									object obj85 = obj82;
									if (flag36)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj86 = default(object);
										throw obj86;
									}
								}
								array4[2] = obj82;
								int globalRankNew3 = ((LeaderboardScoreUploaded*)leaderboardScoreUploaded3)->GlobalRankNew;
								object obj87 = obj27 - 16;
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								object obj88 = default(object);
								if (obj88 != null)
								{
									nint num21 = (nint)array4;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1692 @ rdx_v65 (Il2CppClass<System.Object[]>)+40]");
									object obj89 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj90 = default(object);
									bool flag37 = obj90 == null;
									object obj91 = obj88;
									if (flag37)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj92 = default(object);
										throw obj92;
									}
								}
								array4[3] = obj88;
								args = array4;
								format = "Submitted {0} to '{1}' ({2}). Best unchanged, current rank: {3}.";
								goto IL_12d4;
								IL_1196:
								throw new NullReferenceException();
								IL_1050:
								throw new NullReferenceException();
								IL_12d4:
								string text5 = string.Format(format, args);
								Debug.Log(text5);
								SteamLeaderboardScoreController steamLeaderboardScoreController8 = CS_0024_003C_003E8__locals31._003C_003E4__this;
								bool flag38 = (object)CS_0024_003C_003E8__locals31._003C_003E4__this == null;
								flag12 = false;
								leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
								obj25 = text5;
								if (!flag38)
								{
									if (!steamLeaderboardScoreController8.resetScoreAfterSubmit)
									{
										return;
									}
									steamLeaderboardScoreController8.pendingScore = 0;
									SteamLeaderboardScoreController steamLeaderboardScoreController9 = CS_0024_003C_003E8__locals31._003C_003E4__this;
									bool flag39 = (object)CS_0024_003C_003E8__locals31._003C_003E4__this == null;
									flag12 = false;
									leaderboardScoreUploaded3 = (LeaderboardScoreUploaded)0;
									obj25 = text5;
									if (!flag39)
									{
										if (steamLeaderboardScoreController9.verboseLogging)
										{
											Debug.Log("[SteamLeaderboardScoreController] Resetting pendingScore to 0 after successful submit (Reset Score After Submit enabled).");
										}
										return;
									}
								}
								goto IL_1050;
							});
						}
						if (!CS_0024_003C_003E8__locals31.forceUpdate)
						{
							((LeaderboardData*)board)->UploadScoreKeepBest(CS_0024_003C_003E8__locals31.toSubmit, callback2);
						}
						else
						{
							((LeaderboardData*)board)->UploadScoreForceUpdate(CS_0024_003C_003E8__locals31.toSubmit, callback2);
						}
						return;
					}
					text = "' was not found or is not valid.";
					text2 = "[SteamLeaderboardScoreController] Leaderboard '";
				}
				else
				{
					steamLeaderboardScoreController2 = CS_0024_003C_003E8__locals31._003C_003E4__this;
					text = "'.";
					text2 = "[SteamLeaderboardScoreController] IO error while finding leaderboard '";
				}
				string message5 = text2 + steamLeaderboardScoreController2.leaderboardApiName + text;
				Debug.LogError(message5);
			};
			LeaderboardData.Get(leaderboardApiName, callback);
		}
		else
		{
			Debug.LogError("[SteamLeaderboardScoreController] SubmitScore: aborted, leaderboard API name is empty.");
		}
	}

	public void SubmitScoreWithValue(int value)
	{
		if (verboseLogging)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			bool flag = useForceUpdate;
			object arg = "ForceUpdate";
			if (!flag)
			{
				arg = "KeepBest";
			}
			object arg2 = default(object);
			string message = $"[SteamLeaderboardScoreController] SubmitScoreWithValue: value={arg2}, mode={arg}";
			Debug.Log(message);
		}
		pendingScore = value;
		SubmitScoreInternal(useForceUpdate);
	}

	public SteamLeaderboardScoreController()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A90E]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		leaderboardApiName = "TestHighScore";
		resetScoreAfterSubmit = true;
		base._002Ector();
	}
}
