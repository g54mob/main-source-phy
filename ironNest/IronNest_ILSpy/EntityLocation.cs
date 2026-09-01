using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityLocation : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MapEntityIcon, string> _003C_003E9__40_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CAwake_003Eb__40_0(MapEntityIcon x)
		{
			if ((object)x != null)
			{
				return x.ID;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass48_0
	{
		public ShellDefinition shell;

		internal bool _003CTakeDamage_003Eb__0(string x)
		{
			//IL_0052: Expected I4, but got O
			ShellDefinition shellDefinition = shell;
			if ((object)shell != null)
			{
				return x == shellDefinition.ShellId;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003CReportLocationNextFrame_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public EntityLocation _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CReportLocationNextFrame_003Ed__46(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0031: Expected I4, but got I8
			//IL_007a: Expected I4, but got I8
			//IL_00c7: Expected I4, but got O
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				bool flag = _003C_003E4__this.RecalculateAndRegister((byte)_003C_003E1__state != 0);
			}
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

	public Image Image_Stars;

	public Sprite[] Sprites_Stars;

	public Image Image_Armour;

	public Sprite[] Sprites_Armour;

	public Image Image_Icon;

	public CanvasGroup VisibilityGroup;

	public GameObject VisualRoot;

	public bool UpdateVisualOnMove;

	public string RevealAreaTag;

	public float RectanglePadding;

	public bool StartWithVisualRootHidden;

	public float ScanWindowDurationSeconds;

	public float ScanIntervalSeconds;

	public UnityEvent<EntityLocation> OnDestroyed_Ally;

	public UnityEvent<EntityLocation> OnDestroyed;

	public UnityEvent<EntityLocation> OnTakeDamage_Ally;

	public UnityEvent<EntityLocation> OnTakeDamage;

	public UnityEvent<EntityLocation> OnImmuneToShellHit;

	public UnityEvent<EntityLocation> OnMove;

	public UnityEvent<EntityLocation> OnRevealed;

	public MapEntity Entity;

	public static Dictionary<string, MapEntityIcon> PossibleMapIcons;

	private Action m_OnStateUpdated;

	private static RectTransform _rootCanvasRect;

	private static bool _warnedNoCanvas;

	private static bool _warnedMissingRevealTag;

	private static Vector3 _lastRootPos;

	private static Quaternion _lastRootRot;

	private static Vector3 _lastRootScale;

	private static bool _rootTransformCached;

	private Vector3 _visualRootWorldPosition;

	private bool _hasVisualRootWorldPosition;

	private bool _hasReportedLocation;

	private bool _scanActive;

	private float _scanWindowEndTime;

	private float _nextScanTime;

	public Vector2 LocalPosition
	{
		get
		{
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 localPosition = transform.localPosition;
				Vector2 result = default(Vector2);
				return result;
			}
			return (Vector2)new NullReferenceException();
		}
	}

	public event Action OnStateUpdated
	{
		add
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 184;
			Delegate obj2 = this.m_OnStateUpdated;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0089: Expected O, but got Unknown
			object obj = this + 184;
			Delegate obj2 = this.m_OnStateUpdated;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag)
				{
					bool flag2 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag2)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public void Awake()
	{
		if (PossibleMapIcons != null)
		{
			int count = PossibleMapIcons.Count;
			if (count > 0)
			{
				return;
			}
		}
		MapEntityIcon[] source = Resources.LoadAll<MapEntityIcon>("MapEntityIcons");
		Func<MapEntityIcon, string> keySelector = _003C_003Ec._003C_003E9__40_0;
		if (_003C_003Ec._003C_003E9__40_0 == null)
		{
			keySelector = (_003C_003Ec._003C_003E9__40_0 = (MapEntityIcon x) => (string)(((object)x != null) ? ((object)x.ID) : ((object)new NullReferenceException())));
		}
		Dictionary<string, MapEntityIcon> possibleMapIcons = Enumerable.ToDictionary(source, keySelector);
		PossibleMapIcons = possibleMapIcons;
		int count2 = PossibleMapIcons.Count;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string message = $"Found {arg} Icons Dynamically";
		Debug.Log(message);
	}

	private void OnEnable()
	{
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact += value;
	}

	private void OnDisable()
	{
		Action<Vector2, float> value = OnImpact;
		ImpactTracker.OnImpact -= value;
	}

	private void LateUpdate()
	{
		//IL_00a9: Invalid comparison between I4 and F4
		//IL_00bb: Expected F4, but got I4
		if (Entity == null)
		{
			return;
		}
		KeepVisualRootLocked();
		if (!_scanActive)
		{
			return;
		}
		float time = Time.time;
		if (time < _scanWindowEndTime)
		{
			float time2 = Time.time;
			if (time2 < _nextScanTime)
			{
				return;
			}
			float time3 = Time.time;
			bool flag = !(0f < ScanIntervalSeconds);
			float num = 0f;
			if (!flag)
			{
				num = ScanIntervalSeconds;
			}
			float nextScanTime = num + time3;
			_nextScanTime = nextScanTime;
			if (!EvaluateRevealArea())
			{
				return;
			}
		}
		_scanWindowEndTime = 0f;
		_scanActive = false;
	}

	public unsafe void Init(MapEntity entity)
	{
		//IL_048e: Expected I, but got O
		//IL_022a: Expected I, but got O
		//IL_0253: Expected O, but got I4
		//IL_0519: Expected O, but got I4
		//IL_063e: Expected I, but got O
		//IL_0657: Expected F4, but got O
		//IL_0671: Expected O, but got I
		//IL_02bd: Expected O, but got I4
		//IL_04fc: Expected F4, but got I4
		//IL_0501: Expected I, but got O
		//IL_04d4: Expected O, but got Ref
		//IL_04d9: Expected I, but got O
		//IL_03a6: Expected I, but got O
		//IL_02d7: Expected O, but got I
		//IL_02e1: Expected I, but got O
		//IL_0277: Expected O, but got I
		//IL_03cf: Expected O, but got I4
		//IL_0558: Expected O, but got I4
		//IL_0572: Expected O, but got I4
		//IL_0439: Expected O, but got I4
		//IL_0453: Expected O, but got I
		//IL_045d: Expected I, but got O
		//IL_0462: Expected I, but got O
		//IL_03f3: Expected O, but got I
		Entity = entity;
		GameObject gameObject = base.gameObject;
		gameObject.name = entity.ID;
		bool flag = !StartWithVisualRootHidden;
		_hasVisualRootWorldPosition = false;
		if (!flag && VisualRoot != null)
		{
			GameObject gameObject2 = base.gameObject;
			if (VisualRoot != gameObject2)
			{
				VisualRoot.SetActive(value: false);
			}
		}
		Image image_Icon;
		Sprite sprite;
		MapEntityIcon value;
		if (entity.IconRaw == null)
		{
			bool flag2 = string.IsNullOrEmpty(entity.Icon);
			value = null;
			if (!flag2)
			{
				bool flag3 = PossibleMapIcons.TryGetValue(entity.Icon, out value);
				bool flag4 = !flag3;
				nint num = 0;
				if (!flag4)
				{
					image_Icon = Image_Icon;
					sprite = value.Icon;
					num = 0;
					goto IL_061e;
				}
			}
			goto IL_0191;
		}
		image_Icon = Image_Icon;
		sprite = entity.IconRaw;
		value = null;
		goto IL_061e;
		IL_0493:
		nint num6;
		if (entity.Scale > 0)
		{
			Transform transform = base.transform;
			nint num2 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ rcx_v35 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num3 = 0;
			float num4 = (float)Vector3.oneVector;
			int scale = entity.Scale;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rdx_v23 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
			object obj = (nint)scale * (nint)0;
			float num5 = default(float);
			transform.localScale = (Vector3)(&num5);
			num6 = unchecked((nint)null);
		}
		if (entity.State < MapEntityStates.None)
		{
			VisibilityGroup.alpha = 0f;
			float num4 = 0f;
			num6 = unchecked((nint)null);
		}
		object obj2 = entity.State & MapEntityStates.Destroyed;
		if (obj2 != null)
		{
			MapEntity entity2 = Entity;
			object obj3 = entity2.Role & EntityRoles.Ally;
			bool flag5 = obj3 == null;
			object obj4 = !flag5;
			UnityEvent<EntityLocation> unityEvent = ((obj4 != null) ? OnDestroyed_Ally : OnDestroyed);
			if (unityEvent != null)
			{
				unityEvent.Invoke(this);
				num6 = 0;
			}
			RevealVisualRoot();
		}
		Action onStateUpdated = this.m_OnStateUpdated;
		if (this.m_OnStateUpdated != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v1007.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		_003CReportLocationNextFrame_003Ed__46 obj5 = new _003CReportLocationNextFrame_003Ed__46(0);
		obj5._003C_003E1__state = 0;
		obj5._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj5);
		return;
		IL_030d:
		if (entity.Armour > 0 && Sprites_Armour != null)
		{
			Sprite[] sprites_Armour = Sprites_Armour;
			if (sprites_Armour.Length != 0)
			{
				GameObject gameObject3 = Image_Armour.gameObject;
				gameObject3.SetActive(value: true);
				nint num7 = (nint)Sprites_Armour;
				bool flag6 = (nint)Sprites_Armour < 0;
				object obj6 = entity.Armour - 1;
				Image image_Armour;
				if (!flag6)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v42 (Il2CppMethodInfo)+18]");
					object obj7 = -1;
					image_Armour = Image_Armour;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7))
					{
						obj7 = obj6;
					}
				}
				else
				{
					image_Armour = Image_Armour;
					object obj7 = 0;
				}
				Image image = image_Armour;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v42 (Il2CppMethodInfo)+20+v167 @ rax_v46*8]");
				image.sprite = (Sprite)0;
				nint num = (nint)Sprites_Armour;
				num6 = unchecked((nint)null);
				goto IL_0493;
			}
		}
		GameObject gameObject4 = Image_Armour.gameObject;
		gameObject4.SetActive(value: false);
		num6 = unchecked((nint)null);
		goto IL_0493;
		IL_061e:
		image_Icon.sprite = sprite;
		goto IL_0191;
		IL_0191:
		if (entity.Stars > 0 && Sprites_Stars != null)
		{
			Sprite[] sprites_Stars = Sprites_Stars;
			if (sprites_Stars.Length != 0)
			{
				GameObject gameObject5 = Image_Stars.gameObject;
				gameObject5.SetActive(value: true);
				nint num8 = (nint)Sprites_Stars;
				bool flag7 = (nint)Sprites_Stars < 0;
				object obj8 = entity.Stars - 1;
				Image image_Stars;
				if (!flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rcx_v50 (Il2CppMethodInfo)+18]");
					object obj9 = -1;
					image_Stars = Image_Stars;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9))
					{
						obj9 = obj8;
					}
				}
				else
				{
					image_Stars = Image_Stars;
					object obj9 = 0;
				}
				Image image2 = image_Stars;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rcx_v50 (Il2CppMethodInfo)+20+v161 @ rax_v58*8]");
				image2.sprite = (Sprite)0;
				nint num = (nint)Sprites_Stars;
				goto IL_030d;
			}
		}
		GameObject gameObject6 = Image_Stars.gameObject;
		gameObject6.SetActive(value: false);
		goto IL_030d;
	}

	private void OnDestroy()
	{
		if (Entity != null)
		{
			MapEntity entity = Entity;
			ImpactTracker.UnregisterEntity(entity.ID);
		}
	}

	private IEnumerator ReportLocationNextFrame()
	{
		_003CReportLocationNextFrame_003Ed__46 obj = new _003CReportLocationNextFrame_003Ed__46(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe bool RecalculateAndRegister(bool forceRegister)
	{
		//IL_006e: Expected I, but got O
		//IL_05ee: Expected I4, but got O
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0171: Invalid comparison between F4 and I4
		//IL_019a: Expected O, but got I4
		//IL_0443: Expected O, but got Ref
		//IL_0262: Invalid comparison between F4 and I4
		//IL_028b: Expected O, but got I4
		//IL_02c3: Expected I, but got O
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_038b: Invalid comparison between F4 and I4
		//IL_03b4: Expected O, but got I4
		//IL_0535: Invalid comparison between F4 and I4
		//IL_0635: Expected O, but got I4
		RectTransform rectTransform;
		UnityEngine.Object obj = default(UnityEngine.Object);
		object obj3 = default(object);
		object obj4 = default(object);
		float x = default(float);
		if (Entity != null)
		{
			rectTransform = ResolveRootCanvasRect();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			if (rectTransform != null && obj != null)
			{
				nint num = (nint)typeof(EntityLocation);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rcx_v10 (Il2CppClass<EntityLocation>)+B8]");
				nint num2 = 0;
				if (_rootTransformCached)
				{
					if ((object)rectTransform == null)
					{
						goto IL_05e0;
					}
					Vector3 position = rectTransform.position;
					float num3 = (float)_lastRootPos - position.x;
					object obj2 = obj3 - obj4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ rax_v11 (Il2CppStaticFields<EntityLocation>)+1C]");
					object obj5 = 0 - position.z;
					object obj6 = obj2 * obj2;
					float num4 = num3 * num3;
					object obj7 = obj5 * obj5;
					float num5 = (float)obj6 + num4;
					float num6 = num5 + (float)obj7;
					bool flag = 9.9999994E-11f < num6;
					float num7 = 9.9999994E-11f - num6;
					bool flag2 = num7 == 0f;
					bool flag3 = !flag;
					bool flag4 = !flag2;
					object obj8 = flag4 & flag3;
					bool flag5 = obj8 == null;
					x = position.x;
					if (!flag5)
					{
						Quaternion rotation = rectTransform.rotation;
						object obj9 = obj4 * obj4;
						float num8 = (float)_lastRootRot * rotation.x;
						float num9 = (float)obj9 + num8;
						object obj10 = obj4 * obj4;
						object obj11 = obj4 * obj4;
						float num10 = num9 + (float)obj10;
						float num11 = num10 + (float)obj11;
						bool flag6 = num11 < 0.999999f;
						float num12 = num11 - 0.999999f;
						bool flag7 = num12 == 0f;
						bool flag8 = !flag6;
						bool flag9 = !flag7;
						object obj12 = flag9 & flag8;
						bool flag10 = obj12 == null;
						x = position.x;
						if (!flag10)
						{
							nint num13 = (nint)typeof(EntityLocation);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v599 @ rax_v43 (Il2CppClass<EntityLocation>)+B8]");
							nint num14 = 0;
							Vector3 lossyScale = rectTransform.lossyScale;
							float num15 = (float)_lastRootScale - lossyScale.x;
							object obj13 = obj4 - obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v600 @ rcx_v27 (Il2CppStaticFields<EntityLocation>)+38]");
							object obj14 = 0 - lossyScale.z;
							object obj15 = obj13 * obj13;
							float num16 = num15 * num15;
							object obj16 = obj14 * obj14;
							float num17 = (float)obj15 + num16;
							float num18 = num17 + (float)obj16;
							bool flag11 = 9.9999994E-11f < num18;
							float num19 = 9.9999994E-11f - num18;
							bool flag12 = num19 == 0f;
							bool flag13 = !flag11;
							bool flag14 = !flag12;
							object obj17 = flag14 & flag13;
							bool flag15 = obj17 != null;
							x = lossyScale.x;
							x = lossyScale.x;
							if (flag15)
							{
								goto IL_060d;
							}
						}
					}
					CacheRootTransform(rectTransform);
				}
				else
				{
					CacheRootTransform(rectTransform);
				}
				goto IL_060d;
			}
		}
		bool result = false;
		goto IL_0643;
		IL_05e0:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_060d:
		if ((object)obj != null)
		{
			Vector3 position2 = ((Transform)obj).position;
			if ((object)rectTransform != null)
			{
				Vector3 vector = rectTransform.InverseTransformPoint((Vector3)(&x));
				bool flag16 = !_hasReportedLocation;
				bool flag17;
				if (!_hasReportedLocation)
				{
					flag17 = false;
				}
				else
				{
					Transform transform = base.transform;
					if ((object)transform == null)
					{
						goto IL_05e0;
					}
					Vector3 localPosition = transform.localPosition;
					float num20 = vector.x - localPosition.x;
					object obj18 = obj3 - obj4;
					float num21 = num20 * num20;
					object obj19 = obj18 * obj18;
					float num22 = (float)obj19 + num21;
					bool flag18 = num22 < 1E-06f;
					float num23 = num22 - 1E-06f;
					flag16 = num23 == 0f;
					bool flag19 = !flag18;
					bool flag20 = !flag16;
					flag17 = flag20 & flag19;
				}
				object obj20 = !flag16;
				if (obj20 != null || !_hasReportedLocation)
				{
					_hasReportedLocation = true;
					ImpactTracker.RegisterEntity(this);
				}
				bool flag21 = (byte)((forceRegister ? 1u : 0u) ^ 1u) != 0;
				result = flag21;
				if (!flag17)
				{
					result = false;
				}
				goto IL_0643;
			}
		}
		goto IL_05e0;
		IL_0643:
		return result;
	}

	public bool TakeDamage(ShellDefinition shell, int damage, string shellInstanceId = "")
	{
		//IL_012a: Expected O, but got I4
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected I4, but got Unknown
		//IL_0210: Expected O, but got I4
		//IL_022a: Expected O, but got I4
		//IL_0683: Expected I4, but got O
		//IL_03aa: Expected O, but got I
		//IL_040f: Expected O, but got I
		_003C_003Ec__DisplayClass48_0 CS_0024_003C_003E8__locals7 = new _003C_003Ec__DisplayClass48_0();
		CS_0024_003C_003E8__locals7.shell = shell;
		Dictionary<string, object> dictionary;
		object value5;
		if (damage > 0)
		{
			if (CS_0024_003C_003E8__locals7.shell != null)
			{
				MapEntity entity = Entity;
				Func<string, bool> predicate = delegate(string x)
				{
					//IL_0052: Expected I4, but got O
					ShellDefinition shell3 = CS_0024_003C_003E8__locals7.shell;
					if ((object)CS_0024_003C_003E8__locals7.shell == null)
					{
						NullReferenceException ex2 = new NullReferenceException();
						return (byte)(int)ex2 != 0;
					}
					return x == shell3.ShellId;
				};
				if (Enumerable.Any(entity.ImmuneShells, predicate))
				{
					if (OnImmuneToShellHit != null)
					{
						OnImmuneToShellHit.Invoke(this);
					}
					goto IL_00ea;
				}
			}
			MapEntity entity2 = Entity;
			bool flag = (nint)Entity < 0;
			object obj = damage - entity2.Armour;
			if (!flag)
			{
				bool flag2 = (nint)obj < 0;
				if ((nint)obj > 0)
				{
					int num = entity2.Health - obj;
					if (!flag2)
					{
						if (num > entity2.MaxHealth)
						{
							num = entity2.MaxHealth;
						}
					}
					else
					{
						num = 0;
					}
					entity2.Health = num;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D71C0");
					MapEntityStates mapEntityStates = default(MapEntityStates);
					FireMission._003CInstance_003Ek__BackingField.SetEntityState(Entity, mapEntityStates);
					MapEntity entity3 = Entity;
					object obj2 = entity3.Role & EntityRoles.Ally;
					bool flag3 = obj2 == null;
					object obj3 = !flag3;
					((obj3 != null) ? OnTakeDamage_Ally : OnTakeDamage)?.Invoke(this);
					MapEntity entity4 = Entity;
					if (entity4.Health <= 0)
					{
						MapEntity entity5 = Entity;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D71C0");
						MapEntityStates newState = default(MapEntityStates);
						FireMission._003CInstance_003Ek__BackingField.SetEntityState(entity5, newState);
						EventData_EntityDestroyed eventData_EntityDestroyed = new EventData_EntityDestroyed();
						eventData_EntityDestroyed.Entity = Entity;
						eventData_EntityDestroyed.ImpactShell = CS_0024_003C_003E8__locals7.shell;
						FireMission._003CInstance_003Ek__BackingField.ProcessEvent(eventData_EntityDestroyed);
						base.enabled = false;
						MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
						if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
						{
							MissionManager.MissionState currentMissionState = missionManager.CurrentMissionState;
							if (missionManager.CurrentMissionState != null)
							{
								MedalTrackedValues trackingValues = currentMissionState.TrackingValues;
								if (currentMissionState.TrackingValues != null && trackingValues.Data_KilledEntities != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB660");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v69+68]");
									object obj4 = 0;
									MedalTrackedValues.Data_KilledEntity data_KilledEntity = new MedalTrackedValues.Data_KilledEntity();
									data_KilledEntity.Entity = Entity;
									float time = Time.time;
									data_KilledEntity.KilledAtTime = time;
									data_KilledEntity.ShellInstanceId = shellInstanceId;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rcx_v49+28]");
									((MedalTrackedValues)0).TrackKill(data_KilledEntity);
								}
							}
						}
						dictionary = new Dictionary<string, object>();
						MapEntity entity6 = Entity;
						if (Entity != null && dictionary != null)
						{
							dictionary.Add("ID", entity6.ID);
							if (Entity != null)
							{
								object value = (EntityRoles)mapEntityStates;
								dictionary.Add("Role", value);
								if (Entity != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									object value2 = default(object);
									dictionary.Add("Enemy", value2);
									if (Entity != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										object value3 = default(object);
										dictionary.Add("Ally", value3);
										if (Entity != null)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
											object value4 = default(object);
											dictionary.Add("Stars", value4);
											ShellDefinition shell2 = CS_0024_003C_003E8__locals7.shell;
											if ((object)CS_0024_003C_003E8__locals7.shell != null)
											{
												value5 = shell2.ShellId;
												if (shell2.ShellId != null)
												{
													goto IL_0683;
												}
											}
											value5 = "No Shell";
											goto IL_0683;
										}
									}
								}
							}
						}
						NullReferenceException ex = new NullReferenceException();
						return (byte)(int)ex != 0;
					}
				}
			}
		}
		goto IL_00ea;
		IL_00ea:
		return false;
		IL_0683:
		dictionary.Add("Shell", value5);
		AnalyticsManager.Analytics_Generic("EntityDestroyed", 0.0, dictionary);
		return true;
	}

	public unsafe void OnEntityMoved()
	{
		//IL_00d2: Expected O, but got Ref
		//IL_0107: Expected O, but got F4
		if (UpdateVisualOnMove && VisualRoot != null && _hasVisualRootWorldPosition)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Transform transform = VisualRoot.transform;
			UnityEngine.Object obj = default(UnityEngine.Object);
			Transform transform2;
			if (obj != null)
			{
				transform2 = (Transform)obj;
			}
			else
			{
				Transform transform3 = base.transform;
				transform2 = transform3;
			}
			Vector3 position = transform2.position;
			object obj2 = default(object);
			transform.position = (Vector3)(&obj2);
			Transform transform4 = VisualRoot.transform;
			Vector3 position2 = transform4.position;
			_visualRootWorldPosition = (Vector3)position2.x;
			_ = position2.z;
		}
		KeepVisualRootLocked();
		if (OnMove != null)
		{
			OnMove.Invoke(this);
		}
	}

	public void OnEntityStateChanged(MapEntityStates oldState, MapEntityStates newState)
	{
		//IL_01bb: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_00b4: Expected O, but got I4
		//IL_00d0: Expected O, but got I4
		//IL_0042: Expected F4, but got I4
		//IL_010f: Expected O, but got I4
		//IL_0129: Expected O, but got I4
		object obj = newState & MapEntityStates.Hidden;
		CanvasGroup visibilityGroup;
		float alpha;
		MapEntityStates mapEntityStates;
		if (oldState >= MapEntityStates.None)
		{
			bool flag = obj == null;
			mapEntityStates = newState;
			if (flag)
			{
				goto IL_008c;
			}
			visibilityGroup = VisibilityGroup;
			alpha = 0f;
		}
		else
		{
			bool flag2 = obj != null;
			mapEntityStates = newState;
			if (flag2)
			{
				goto IL_008c;
			}
			visibilityGroup = VisibilityGroup;
			alpha = 1f;
		}
		visibilityGroup.alpha = alpha;
		mapEntityStates = MapEntityStates.None;
		goto IL_008c;
		IL_008c:
		object obj2 = oldState & MapEntityStates.Destroyed;
		bool flag3 = obj2 == null;
		object obj3 = !flag3;
		if (obj3 == null)
		{
			object obj4 = newState & MapEntityStates.Destroyed;
			if (obj4 != null)
			{
				MapEntity entity = Entity;
				object obj5 = entity.Role & EntityRoles.Ally;
				bool flag4 = obj5 == null;
				object obj6 = !flag4;
				UnityEvent<EntityLocation> unityEvent = ((obj6 != null) ? OnDestroyed_Ally : OnDestroyed);
				if (unityEvent != null)
				{
					unityEvent.Invoke(this);
					mapEntityStates = MapEntityStates.None;
				}
				RevealVisualRoot();
			}
		}
		Action onStateUpdated = this.m_OnStateUpdated;
		if (this.m_OnStateUpdated != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v193.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
	}

	private void OnImpact(Vector2 impactLocation, float impactRadius)
	{
		//IL_0071: Invalid comparison between I4 and F4
		//IL_0083: Expected F4, but got I4
		if (Entity == null)
		{
			return;
		}
		if (!EvaluateRevealArea())
		{
			float time = Time.time;
			_scanActive = true;
			float scanWindowEndTime = time + ScanWindowDurationSeconds;
			_scanWindowEndTime = scanWindowEndTime;
			bool flag = !(0f < ScanIntervalSeconds);
			float num = 0f;
			if (!flag)
			{
				num = ScanIntervalSeconds;
			}
			float nextScanTime = num + time;
			_nextScanTime = nextScanTime;
		}
		else
		{
			_scanActive = false;
			_scanWindowEndTime = 0f;
		}
	}

	private unsafe bool EvaluateRevealArea()
	{
		//IL_01e5: Expected I4, but got O
		//IL_0210: Expected O, but got Ref
		//IL_0123: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		Transform transform;
		if (obj != null)
		{
			if ((object)obj == null)
			{
				goto IL_01d7;
			}
			transform = (Transform)obj;
		}
		else
		{
			Transform transform2 = base.transform;
			if ((object)transform2 == null)
			{
				goto IL_01d7;
			}
			transform = transform2;
		}
		Vector3 position = transform.position;
		Vector3 visualRootWorldPosition = default(Vector3);
		if (VisualRoot != null)
		{
			if ((object)VisualRoot == null)
			{
				goto IL_01d7;
			}
			if (VisualRoot.activeSelf && _hasVisualRootWorldPosition)
			{
				bool flag = CheckTaggedRectangles((Vector3)(&visualRootWorldPosition));
				bool flag2 = !flag;
				visualRootWorldPosition = _visualRootWorldPosition;
				if (!flag2)
				{
					if (VisualRoot != null)
					{
						if ((object)VisualRoot == null)
						{
							goto IL_01d7;
						}
						VisualRoot.SetActive(value: false);
					}
					_hasVisualRootWorldPosition = false;
					visualRootWorldPosition = _visualRootWorldPosition;
				}
			}
		}
		bool flag3 = CheckTaggedRectangles((Vector3)(&visualRootWorldPosition));
		if (!flag3)
		{
			return flag3;
		}
		RevealVisualRoot();
		return true;
		IL_01d7:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe bool CheckTaggedRectangles(Vector3 worldPos)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0067: Expected O, but got I4
		//IL_018e: Expected I4, but got O
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0113: Expected O, but got Ref
		if (!string.IsNullOrEmpty(RevealAreaTag))
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(RevealAreaTag);
			if (array != null && array.Length != 0)
			{
				object obj = array + 32;
				object obj2 = 0;
				float num = default(float);
				while ((nint)obj2 < array.Length)
				{
					if ((nint)obj2 < array.Length)
					{
						if ((UnityEngine.Object)obj != null && ((GameObject)obj).activeInHierarchy)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if (this != null && IsWorldPointInsideRectTransform((RectTransform)(object)this, (Vector3)(&num), RectanglePadding))
							{
								return true;
							}
						}
						obj2++;
						obj += 8;
						continue;
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
			}
		}
		return false;
	}

	private unsafe void RevealVisualRoot()
	{
		//IL_00d5: Expected O, but got Ref
		//IL_00e4: Expected O, but got F4
		_scanWindowEndTime = 0f;
		_scanActive = false;
		if (VisualRoot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			Transform transform;
			if (obj != null)
			{
				transform = (Transform)obj;
			}
			else
			{
				Transform transform2 = base.transform;
				transform = transform2;
			}
			Vector3 position = transform.position;
			if (!VisualRoot.activeSelf)
			{
				VisualRoot.SetActive(value: true);
			}
			Transform transform3 = VisualRoot.transform;
			object obj2 = default(object);
			transform3.position = (Vector3)(&obj2);
			_visualRootWorldPosition = (Vector3)position.x;
			_ = position.z;
			_hasVisualRootWorldPosition = true;
			if (OnRevealed != null)
			{
				OnRevealed.Invoke(this);
			}
		}
	}

	private void HideVisualRoot()
	{
		if (VisualRoot != null)
		{
			VisualRoot.SetActive(value: false);
		}
		_hasVisualRootWorldPosition = false;
	}

	private unsafe void KeepVisualRootLocked()
	{
		//IL_009d: Expected O, but got Ref
		bool flag = VisualRoot == null;
		if (!flag && _hasVisualRootWorldPosition != flag)
		{
			Transform transform = VisualRoot.transform;
			if (UpdateVisualOnMove)
			{
				Transform transform2 = base.transform;
				Vector3 position = transform2.position;
			}
			object obj = default(object);
			transform.position = (Vector3)(&obj);
		}
	}

	public void StartScanWindow()
	{
		//IL_003a: Invalid comparison between I4 and F4
		//IL_004c: Expected F4, but got I4
		float time = Time.time;
		_scanActive = true;
		float scanWindowEndTime = time + ScanWindowDurationSeconds;
		_scanWindowEndTime = scanWindowEndTime;
		bool flag = !(0f < ScanIntervalSeconds);
		float num = 0f;
		if (!flag)
		{
			num = ScanIntervalSeconds;
		}
		float nextScanTime = num + time;
		_nextScanTime = nextScanTime;
	}

	public void StopScanWindow()
	{
		_scanActive = false;
		_scanWindowEndTime = 0f;
	}

	private RectTransform ResolveRootCanvasRect()
	{
		if (!(_rootCanvasRect == null))
		{
			goto IL_01b1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		object message;
		if (obj != null)
		{
			if ((object)obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				if (!(obj2 != null))
				{
					if (_warnedNoCanvas)
					{
						goto IL_01b7;
					}
					message = "[TargetLocation] No parent Canvas found. Cannot resolve root map space.";
					goto IL_01b9;
				}
				if ((object)obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					RectTransform rootCanvasRect = default(RectTransform);
					_rootCanvasRect = rootCanvasRect;
					if (_rootCanvasRect == null && !_warnedNoCanvas)
					{
						Debug.LogWarning("[TargetLocation] Parent Canvas has no RectTransform?");
						_warnedNoCanvas = true;
					}
					goto IL_01b1;
				}
			}
			return (RectTransform)(object)new NullReferenceException();
		}
		if (_warnedNoCanvas)
		{
			goto IL_01b7;
		}
		message = "[TargetLocation] No RectTransform found on target.";
		goto IL_01b9;
		IL_01b1:
		return _rootCanvasRect;
		IL_01b9:
		Debug.LogWarning(message);
		_warnedNoCanvas = true;
		goto IL_01b7;
		IL_01b7:
		return null;
	}

	private void CacheRootTransform(RectTransform rootRect)
	{
		//IL_0020: Expected I, but got O
		//IL_003e: Expected O, but got F4
		//IL_0068: Expected O, but got F4
		//IL_0083: Expected I, but got O
		//IL_00a1: Expected O, but got F4
		Vector3 position = rootRect.position;
		nint num = (nint)typeof(EntityLocation);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v4 (Il2CppClass<EntityLocation>)+B8]");
		nint num2 = 0;
		_lastRootPos = (Vector3)position.x;
		_ = position.z;
		_lastRootRot = (Quaternion)rootRect.rotation.x;
		Vector3 lossyScale = rootRect.lossyScale;
		nint num3 = (nint)typeof(EntityLocation);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v8 (Il2CppClass<EntityLocation>)+B8]");
		nint num4 = 0;
		_lastRootScale = (Vector3)lossyScale.x;
		_ = lossyScale.z;
		_rootTransformCached = true;
	}

	private unsafe static bool IsWorldPointInsideRectTransform(RectTransform rect, Vector3 worldPoint, float padding)
	{
		//IL_022b: Expected I4, but got O
		//IL_0012: Expected O, but got Ref
		//IL_003b: Invalid comparison between F4 and I4
		//IL_0193: Invalid comparison between O and F4
		//IL_01c3: Invalid comparison between F4 and O
		//IL_01e1: Invalid comparison between F4 and I4
		if ((object)rect != null)
		{
			object obj = default(object);
			Vector3 vector = rect.InverseTransformPoint((Vector3)(&obj));
			Rect rect2 = rect.rect;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018046A69Dh\"");
			float num;
			float num2;
			float num3 = default(float);
			float num4;
			float num5 = default(float);
			float num6;
			float num7 = default(float);
			if (padding == 0f)
			{
				num = rect2.m_XMin;
				num2 = num3;
				num4 = num5;
				num6 = num7;
			}
			else
			{
				num = rect2.m_XMin - padding;
				float num8 = num5 + rect2.m_XMin;
				num2 = num3 - padding;
				float num9 = num8 - num;
				float num10 = num9 + num;
				float num11 = num7 + num3;
				float num12 = num10 + padding;
				float num13 = num11 - num2;
				num4 = num12 - num;
				float num14 = num13 + num2;
				float num15 = num14 + padding;
				num6 = num15 - num2;
			}
			if (!(vector.x < num))
			{
				float num16 = num4 + num;
				object obj2 = default(object);
				if (num16 > vector.x && System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
				{
					float num17 = num6 + num2;
					bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num17) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
					float num18 = num17 - (float)obj2;
					bool flag2 = num18 == 0f;
					bool flag3 = !flag;
					bool flag4 = !flag2;
					return flag4 & flag3;
				}
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public EntityLocation()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A3A8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		RevealAreaTag = "ImpactRevealArea";
		StartWithVisualRootHidden = true;
		ScanWindowDurationSeconds = 3f;
		ScanIntervalSeconds = 0.2f;
		base._002Ector();
	}
}
