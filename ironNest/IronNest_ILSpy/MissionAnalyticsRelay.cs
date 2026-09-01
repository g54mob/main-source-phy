using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using SleepyNodes;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MissionAnalyticsRelay : MonoBehaviour
{
	private sealed class _003CCoRetrySubscribe_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MissionAnalyticsRelay _003C_003E4__this;

		private float _003Cdeadline_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoRetrySubscribe_003Ed__10(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_006d: Expected I4, but got I8
			//IL_0178: Expected I4, but got O
			MissionAnalyticsRelay missionAnalyticsRelay = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				float num = realtimeSinceStartup + 3f;
				_003Cdeadline_003E5__2 = num;
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0164;
				}
				_003C_003E1__state = -1;
			}
			if ((object)_003C_003E4__this != null)
			{
				if (!missionAnalyticsRelay._subscribed)
				{
					float realtimeSinceStartup2 = Time.realtimeSinceStartup;
					if (_003Cdeadline_003E5__2 > realtimeSinceStartup2)
					{
						_003C_003E4__this.TrySubscribe();
						if (!missionAnalyticsRelay._subscribed)
						{
							_003C_003E2__current = null;
							_003C_003E1__state = 1;
							return true;
						}
					}
					else if (!missionAnalyticsRelay._subscribed && missionAnalyticsRelay.enableDebugLogs)
					{
						Debug.LogWarning("[MissionAnalyticsRelay] MissionManager not found within timeout.");
					}
				}
				goto IL_0164;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0164:
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private bool enableDebugLogs = true;

	private MissionManager _mm;

	private bool _subscribed;

	private string _activeMissionID;

	private float _missionStartTime;

	private bool _completionPending;

	private void Awake()
	{
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
	}

	private void OnEnable()
	{
		TrySubscribe();
		UnityAction<Scene> value = OnSceneUnloaded;
		SceneManager.sceneUnloaded += value;
	}

	private void Start()
	{
		if (!_subscribed)
		{
			_003CCoRetrySubscribe_003Ed__10 obj = new _003CCoRetrySubscribe_003Ed__10(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private void OnDisable()
	{
		if (_subscribed && _mm != null)
		{
			Action<MissionGraph, MissionGraph> value = OnMissionChanging;
			_mm.MissionChanging -= value;
			Action<MissionGraph, MissionGraph> value2 = OnMissionChanged;
			_mm.MissionChanged -= value2;
			_subscribed = false;
		}
		UnityAction<Scene> value3 = OnSceneUnloaded;
		SceneManager.sceneUnloaded -= value3;
	}

	private IEnumerator CoRetrySubscribe()
	{
		_003CCoRetrySubscribe_003Ed__10 obj = new _003CCoRetrySubscribe_003Ed__10(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void TrySubscribe()
	{
		if (_subscribed)
		{
			return;
		}
		_mm = MissionManager._003CInstance_003Ek__BackingField;
		if (_mm != null)
		{
			Action<MissionGraph, MissionGraph> value = OnMissionChanging;
			_mm.MissionChanging += value;
			Action<MissionGraph, MissionGraph> value2 = OnMissionChanged;
			_mm.MissionChanged += value2;
			bool flag = !enableDebugLogs;
			_subscribed = true;
			if (!flag)
			{
				Debug.Log("[MissionAnalyticsRelay] Subscribed to MissionManager.");
			}
		}
	}

	private void Unsubscribe()
	{
		if (_subscribed && _mm != null)
		{
			Action<MissionGraph, MissionGraph> value = OnMissionChanging;
			_mm.MissionChanging -= value;
			Action<MissionGraph, MissionGraph> value2 = OnMissionChanged;
			_mm.MissionChanged -= value2;
			_subscribed = false;
		}
	}

	private void OnMissionChanging(MissionGraph oldMission, MissionGraph newMission)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F33]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_activeMissionID != null)
		{
			_completionPending = true;
			SendEvent("mission_completed", _activeMissionID);
			_activeMissionID = null;
			_missionStartTime = 0f;
		}
	}

	private void OnMissionChanged(MissionGraph oldMission, MissionGraph newMission)
	{
		_completionPending = false;
		_activeMissionID = newMission.MissionID;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		bool flag = !enableDebugLogs;
		_missionStartTime = realtimeSinceStartup;
		if (!flag)
		{
			string message = "[MissionAnalyticsRelay] Timer started for '" + newMission.MissionID + "'";
			Debug.Log(message);
		}
	}

	private void OnSceneUnloaded(Scene scene)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F35]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_activeMissionID == null)
		{
			return;
		}
		Scene scene2 = default(Scene);
		string a = scene2.name;
		if (string.Equals(a, _activeMissionID, StringComparison.OrdinalIgnoreCase))
		{
			if (!_completionPending)
			{
				SendEvent("mission_abandoned", _activeMissionID);
				_activeMissionID = null;
				_missionStartTime = 0f;
			}
			else
			{
				_completionPending = false;
			}
		}
	}

	private void SendEvent(string eventName, string missionId)
	{
		//IL_0219: Invalid comparison between F4 and I4
		//IL_003c: Expected F4, but got I4
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0099: Expected O, but got I4
		//IL_00a2: Expected O, but got I4
		//IL_00b5: Expected I4, but got O
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_00ed: Expected O, but got I4
		if (_missionStartTime > 0f)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num = realtimeSinceStartup - _missionStartTime;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		}
		else
		{
			float num = 0f;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F37]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		object value;
		string text4;
		if (!string.IsNullOrEmpty(missionId))
		{
			string text = missionId.Trim();
			string text2 = text.ToLowerInvariant();
			char[] array = text2.ToCharArray();
			object obj = array + 32;
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < array.Length)
			{
				if (!char.IsLetterOrDigit((char)(int)obj) && (nint)obj != 95)
				{
					obj = 95;
				}
				obj2++;
				obj += 2;
				obj3 = obj2;
			}
			string text3 = ((string)null).CreateString(array);
			if (text3._stringLength > 40)
			{
				text3 = text3.Substring(0, 40);
			}
			value = text3;
			text4 = eventName;
		}
		else
		{
			value = "unknown";
			text4 = eventName;
		}
		dictionary.Add("mission_name", value);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value2 = default(object);
		dictionary.Add("duration_seconds", value2);
		if (enableDebugLogs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[MissionAnalyticsRelay] '{text4}': {missionId}, {arg}s";
			Debug.Log(message);
		}
		TrySendAnalyticsEvent(text4, dictionary);
	}

	private void ClearActiveSession()
	{
		_activeMissionID = null;
		_missionStartTime = 0f;
	}

	private string SanitizeName(string raw)
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00e5: Expected O, but got I4
		//IL_00ee: Expected O, but got I4
		//IL_0101: Expected I4, but got O
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0139: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39F37]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(raw))
		{
			if (raw != null)
			{
				string text = raw.Trim();
				if (text != null)
				{
					string text2 = text.ToLowerInvariant();
					if (text2 != null)
					{
						char[] array = text2.ToCharArray();
						if (array != null)
						{
							object obj = array + 32;
							object obj2 = 0;
							object obj3 = 0;
							while ((nint)obj2 < array.Length)
							{
								if (!char.IsLetterOrDigit((char)(int)obj) && (nint)obj != 95)
								{
									obj = 95;
								}
								obj3++;
								obj += 2;
								obj2 = obj3;
							}
							string text3 = ((string)null).CreateString(array);
							if (text3 != null)
							{
								if (text3._stringLength > 40)
								{
									text3 = text3.Substring(0, 40);
								}
								return text3;
							}
						}
					}
				}
			}
			return (string)(object)new NullReferenceException();
		}
		return "unknown";
	}

	private void TrySendAnalyticsEvent(string eventName, IDictionary<string, object> parameters)
	{
		//IL_003d: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_0154: Expected O, but got I
		//IL_0185: Expected O, but got I
		//IL_0c6f: Expected O, but got I4
		//IL_0c93: Expected I, but got O
		//IL_0ca3: Expected O, but got I
		//IL_0cde: Expected O, but got I
		//IL_079b: Expected O, but got I
		//IL_0d57: Expected I, but got O
		//IL_0d67: Expected O, but got I
		//IL_0291: Expected O, but got I
		//IL_080c: Expected O, but got I
		//IL_083d: Expected O, but got I
		//IL_037b: Expected I, but got O
		//IL_030c: Expected O, but got I
		//IL_0337: Expected I, but got O
		//IL_0347: Expected O, but got I
		//IL_08e7: Expected O, but got I
		//IL_0918: Expected O, but got I
		//IL_046a: Expected I, but got O
		//IL_03fb: Expected O, but got I
		//IL_0426: Expected I, but got O
		//IL_0436: Expected O, but got I
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Expected O, but got Unknown
		//IL_0a14: Expected O, but got I4
		//IL_0a48: Expected O, but got I4
		//IL_0a6c: Expected I, but got O
		//IL_0a7c: Expected O, but got I
		//IL_0ab7: Expected O, but got I
		//IL_0544: Expected I, but got O
		//IL_054d: Expected O, but got I4
		//IL_055b: Expected I, but got O
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Expected O, but got Unknown
		//IL_0edc: Expected O, but got I
		//IL_0ee4: Expected O, but got I
		//IL_0581: Expected O, but got I4
		//IL_0b3e: Expected I, but got O
		//IL_0b4e: Expected O, but got I
		//IL_0b89: Expected O, but got I
		//IL_0624: Expected I, but got O
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Expected O, but got Unknown
		//IL_05a5: Expected I, but got O
		//IL_05b5: Expected O, but got I
		//IL_05e0: Expected I, but got O
		//IL_05f0: Expected O, but got I
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Expected O, but got Unknown
		//IL_06f6: Expected I, but got O
		//IL_0ebe: Expected I, but got O
		//IL_0677: Expected I, but got O
		//IL_0687: Expected O, but got I
		//IL_06b2: Expected I, but got O
		//IL_06c2: Expected O, but got I
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Expected I, but got Unknown
		//IL_0745: Expected I, but got O
		IAnalyticsService instance = AnalyticsService.Instance;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		Type[] array = new Type[2];
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		RuntimeTypeHandle runtimeTypeHandle = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle = Type.GetTypeFromHandle(runtimeTypeHandle);
		bool flag = (object)typeFromHandle == null;
		string text = null;
		Type type = (Type)runtimeTypeHandle;
		if (!flag)
		{
			object obj = array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rdx_v121+40]");
			text = (string)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj2 = default(object);
			bool flag2 = obj2 == null;
			type = typeFromHandle;
			if (flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				text = null;
				Type type2 = default(Type);
				type = type2;
				throw type2;
			}
		}
		if (array.Length > 0)
		{
			array[0] = typeFromHandle;
			Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
			bool flag3 = (object)typeFromHandle2 == null;
			string text2 = null;
			Type typeFromHandle3 = typeof(IDictionary<string, object>);
			if (!flag3)
			{
				object obj3 = array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v363 @ rdx_v119+40]");
				text2 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj4 = default(object);
				bool flag4 = obj4 == null;
				typeFromHandle3 = typeFromHandle2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v363 @ rdx_v119+40]");
				text = (string)0;
				type = typeFromHandle2;
				if (flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					text2 = null;
					Type type3 = default(Type);
					typeFromHandle3 = type3;
					throw type3;
				}
			}
			IDictionary<string, object> dictionary;
			if (array.Length > 1)
			{
				array[1] = typeFromHandle2;
				Type type4 = default(Type);
				Type[] types = default(Type[]);
				ParameterModifier[] modifiers = default(ParameterModifier[]);
				MethodInfo method = type4.GetMethod("CustomData", (BindingFlags)20, null, types, modifiers);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
				object obj5 = default(object);
				string text6 = default(string);
				Binder binder;
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7190");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805DABC0");
					object obj6 = default(object);
					bool flag5 = obj6 == null;
					dictionary = null;
					binder = null;
					if (!flag5)
					{
						Type[] array2 = new Type[2];
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						RuntimeTypeHandle runtimeTypeHandle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
						Type typeFromHandle4 = Type.GetTypeFromHandle(runtimeTypeHandle2);
						bool flag6 = array2 == null;
						dictionary = null;
						binder = null;
						string text3 = null;
						if (flag6)
						{
							throw new NullReferenceException();
						}
						bool flag7 = (object)typeFromHandle4 == null;
						string text4 = null;
						Type type5 = (Type)runtimeTypeHandle2;
						nint num;
						if (!flag7)
						{
							object obj7 = array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1369 @ rdx_v117+40]");
							text4 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj8 = default(object);
							bool flag8 = obj8 == null;
							type5 = typeFromHandle4;
							dictionary = null;
							num = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1369 @ rdx_v117+40]");
							text2 = (string)0;
							typeFromHandle3 = typeFromHandle4;
							if (flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text4 = null;
								Type type6 = default(Type);
								type5 = type6;
								throw type6;
							}
						}
						bool flag9 = array2.Length <= 0;
						dictionary = null;
						num = unchecked((nint)null);
						if (flag9)
						{
							throw new IndexOutOfRangeException();
						}
						array2[0] = typeFromHandle4;
						Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
						bool flag10 = (object)typeFromHandle5 == null;
						string text5 = null;
						Type typeFromHandle6 = typeof(IDictionary<string, object>);
						if (!flag10)
						{
							object obj9 = array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1609 @ rdx_v115+40]");
							text5 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj10 = default(object);
							bool flag11 = obj10 == null;
							typeFromHandle6 = typeFromHandle5;
							dictionary = null;
							num = unchecked((nint)null);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1609 @ rdx_v115+40]");
							text4 = (string)0;
							type5 = typeFromHandle5;
							if (flag11)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text5 = null;
								Type type7 = default(Type);
								typeFromHandle6 = type7;
								throw type7;
							}
						}
						bool flag12 = array2.Length <= 1;
						dictionary = null;
						num = unchecked((nint)null);
						if (flag12)
						{
							throw new IndexOutOfRangeException();
						}
						array2[1] = typeFromHandle5;
						runtimeTypeHandle2 = (RuntimeTypeHandle)(array2 + 40);
						Type type8 = default(Type);
						bool flag13 = (object)type8 == null;
						dictionary = null;
						binder = null;
						text3 = (string)(object)typeFromHandle5;
						if (flag13)
						{
							throw new NullReferenceException();
						}
						MethodInfo method2 = type8.GetMethod("CustomData", (BindingFlags)24, null, types, modifiers);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
						object obj11 = default(object);
						bool flag14 = obj11 == null;
						dictionary = null;
						binder = null;
						if (!flag14)
						{
							object[] array3 = new object[2];
							bool flag15 = array3 == null;
							dictionary = null;
							num = unchecked((nint)null);
							text3 = (string)2;
							nint num2 = (nint)typeof(object[]);
							if (!flag15)
							{
								bool flag16 = text6 == null;
								string text7 = (string)2;
								string typeFromHandle7 = (string)(object)typeof(object[]);
								if (!flag16)
								{
									nint num3 = (nint)array3;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1954 @ rdx_v113 (Il2CppClass<System.Object[]>)+40]");
									text7 = (string)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj12 = default(object);
									bool flag17 = obj12 == null;
									typeFromHandle7 = text6;
									dictionary = null;
									num = unchecked((nint)null);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1954 @ rdx_v113 (Il2CppClass<System.Object[]>)+40]");
									text5 = (string)0;
									typeFromHandle6 = (Type)(object)text6;
									if (flag17)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										text7 = null;
										string text8 = default(string);
										typeFromHandle7 = text8;
										throw text8;
									}
								}
								bool flag18 = array3.Length <= 0;
								dictionary = null;
								num = unchecked((nint)null);
								if (!flag18)
								{
									array3[0] = text6;
									IDictionary<string, object> dictionary2 = (IDictionary<string, object>)(array3 + 32);
									if (parameters != null)
									{
										nint num4 = (nint)array3;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2005 @ rdx_v111 (Il2CppClass<System.Object[]>)+40]");
										text6 = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj13 = default(object);
										bool flag19 = obj13 == null;
										dictionary2 = parameters;
										dictionary = null;
										num = unchecked((nint)null);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2005 @ rdx_v111 (Il2CppClass<System.Object[]>)+40]");
										text7 = (string)0;
										typeFromHandle7 = (string)(object)parameters;
										if (flag19)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											text3 = null;
											IDictionary<string, object> dictionary3 = default(IDictionary<string, object>);
											dictionary2 = dictionary3;
											throw dictionary3;
										}
									}
									bool flag20 = array3.Length <= 1;
									dictionary = null;
									num = unchecked((nint)null);
									text3 = text6;
									if (!flag20)
									{
										array3[1] = parameters;
										num2 = (nint)(array3 + 40);
										bool flag21 = (object)method2 == null;
										dictionary = null;
										num = unchecked((nint)null);
										text3 = (string)(object)parameters;
										if (!flag21)
										{
											object obj14 = method2.Invoke(null, array3);
											return;
										}
										throw new NullReferenceException();
									}
									num2 = (nint)dictionary2;
									throw new IndexOutOfRangeException();
								}
								throw new IndexOutOfRangeException();
							}
							binder = (Binder)num;
							runtimeTypeHandle2 = (RuntimeTypeHandle)num2;
							throw new NullReferenceException();
						}
					}
					Type[] array4 = new Type[2];
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					RuntimeTypeHandle runtimeTypeHandle3 = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle8 = Type.GetTypeFromHandle(runtimeTypeHandle3);
					bool flag22 = array4 == null;
					string text9 = null;
					string text10 = (string)runtimeTypeHandle3;
					if (!flag22)
					{
						bool flag23 = (object)typeFromHandle8 == null;
						string text11 = null;
						if (!flag23)
						{
							object obj15 = array4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1419 @ rdx_v94+40]");
							text11 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj16 = default(object);
							bool flag24 = obj16 == null;
							runtimeTypeHandle3 = (RuntimeTypeHandle)typeFromHandle8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1419 @ rdx_v94+40]");
							string text3 = (string)0;
							RuntimeTypeHandle runtimeTypeHandle2 = (RuntimeTypeHandle)typeFromHandle8;
							if (flag24)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								text11 = null;
								RuntimeTypeHandle runtimeTypeHandle4 = default(RuntimeTypeHandle);
								runtimeTypeHandle3 = runtimeTypeHandle4;
								throw runtimeTypeHandle4;
							}
						}
						if (array4.Length > 0)
						{
							array4[0] = typeFromHandle8;
							Type typeFromHandle9 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(IDictionary<string, object>));
							bool flag25 = (object)typeFromHandle9 == null;
							string text12 = null;
							RuntimeTypeHandle typeFromHandle10 = (RuntimeTypeHandle)typeof(IDictionary<string, object>);
							if (!flag25)
							{
								object obj17 = array4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1622 @ rdx_v92+40]");
								text12 = (string)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj18 = default(object);
								bool flag26 = obj18 == null;
								typeFromHandle10 = (RuntimeTypeHandle)typeFromHandle9;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1622 @ rdx_v92+40]");
								text11 = (string)0;
								runtimeTypeHandle3 = (RuntimeTypeHandle)typeFromHandle9;
								if (flag26)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									text12 = null;
									RuntimeTypeHandle runtimeTypeHandle5 = default(RuntimeTypeHandle);
									typeFromHandle10 = runtimeTypeHandle5;
									throw runtimeTypeHandle5;
								}
							}
							if (array4.Length > 1)
							{
								array4[1] = typeFromHandle9;
								MethodInfo method3 = type4.GetMethod("RecordEvent", (BindingFlags)20, null, types, modifiers);
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
								object obj19 = default(object);
								if (obj19 == null)
								{
									if (enableDebugLogs)
									{
										Debug.LogWarning("[MissionAnalyticsRelay] No supported Analytics send API found. Check package version.");
									}
									return;
								}
								object[] array5 = new object[2];
								bool flag27 = array5 == null;
								dictionary = null;
								binder = null;
								text9 = (string)2;
								text10 = (string)(object)typeof(object[]);
								if (!flag27)
								{
									bool flag28 = text6 == null;
									string text13 = (string)2;
									string typeFromHandle11 = (string)(object)typeof(object[]);
									if (!flag28)
									{
										nint num5 = (nint)array5;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1941 @ rdx_v88 (Il2CppClass<System.Object[]>)+40]");
										text13 = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
										object obj20 = default(object);
										bool flag29 = obj20 == null;
										typeFromHandle11 = text6;
										dictionary = null;
										binder = null;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1941 @ rdx_v88 (Il2CppClass<System.Object[]>)+40]");
										text12 = (string)0;
										typeFromHandle10 = (RuntimeTypeHandle)text6;
										if (flag29)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
											text13 = null;
											string text14 = default(string);
											typeFromHandle11 = text14;
											throw text14;
										}
									}
									bool flag30 = array5.Length <= 0;
									dictionary = null;
									binder = null;
									if (!flag30)
									{
										array5[0] = text6;
										IDictionary<string, object> dictionary4 = (IDictionary<string, object>)(array5 + 32);
										if (parameters != null)
										{
											nint num6 = (nint)array5;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1991 @ rdx_v86 (Il2CppClass<System.Object[]>)+40]");
											text6 = (string)0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj21 = default(object);
											bool flag31 = obj21 == null;
											dictionary4 = parameters;
											dictionary = null;
											binder = null;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1991 @ rdx_v86 (Il2CppClass<System.Object[]>)+40]");
											text13 = (string)0;
											typeFromHandle11 = (string)(object)parameters;
											if (flag31)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
												text9 = null;
												string text15 = default(string);
												text10 = text15;
												throw text15;
											}
										}
										bool flag32 = array5.Length <= 1;
										dictionary = null;
										binder = null;
										text9 = text6;
										text10 = (string)(object)dictionary4;
										if (!flag32)
										{
											array5[1] = parameters;
											text10 = (string)(array5 + 40);
											bool flag33 = (object)method3 == null;
											dictionary = null;
											binder = null;
											text9 = (string)(object)parameters;
											if (!flag33)
											{
												object obj22 = method3.Invoke(instance, array5);
												return;
											}
											throw new NullReferenceException();
										}
										throw new IndexOutOfRangeException();
									}
									throw new IndexOutOfRangeException();
								}
								throw new NullReferenceException();
							}
							throw new IndexOutOfRangeException();
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
				}
				object[] array6 = new object[2];
				bool flag34 = text6 == null;
				string text16 = (string)2;
				string typeFromHandle12 = (string)(object)typeof(object[]);
				if (!flag34)
				{
					nint num7 = (nint)array6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v759 @ rdx_v66 (Il2CppClass<System.Object[]>)+40]");
					text16 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj23 = default(object);
					bool flag35 = obj23 == null;
					typeFromHandle12 = text6;
					dictionary = null;
					binder = null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v759 @ rdx_v66 (Il2CppClass<System.Object[]>)+40]");
					string text9 = (string)0;
					string text10 = text6;
					if (flag35)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						text16 = null;
						string text17 = default(string);
						typeFromHandle12 = text17;
						throw text17;
					}
				}
				bool flag36 = array6.Length <= 0;
				dictionary = null;
				binder = null;
				if (!flag36)
				{
					array6[0] = text6;
					if (parameters != null)
					{
						nint num8 = (nint)array6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1076 @ rdx_v64 (Il2CppClass<System.Object[]>)+40]");
						text16 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj24 = default(object);
						bool flag37 = obj24 == null;
						dictionary = null;
						binder = null;
						typeFromHandle12 = (string)(object)parameters;
						if (flag37)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							object obj25 = default(object);
							throw obj25;
						}
					}
					array6[1] = parameters;
					object obj26 = method.Invoke(instance, array6);
					return;
				}
				throw new IndexOutOfRangeException();
			}
			dictionary = parameters;
			throw new IndexOutOfRangeException();
		}
		throw new IndexOutOfRangeException();
	}
}
