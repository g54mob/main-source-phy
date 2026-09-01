using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class ImpactLocation : MonoBehaviour
{
	private sealed class _003CReportLocationNextFrame_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactLocation _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CReportLocationNextFrame_003Ed__12(int _003C_003E1__state)
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
			//IL_00bd: Expected I4, but got O
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
				_003C_003E4__this.EvaluateAndReport();
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

	public ShellDefinition shell;

	public bool TriggerNormalEvents;

	private static RectTransform _cachedRootCanvas;

	public GameObject[] ScoutingStrips;

	public bool DisableIfAnyEntityMatches;

	private bool _003CScoutingStripsBlocked_003Ek__BackingField;

	public FilterEntitySet EntityFilter;

	public bool ScoutingStripsBlocked
	{
		get
		{
			return _003CScoutingStripsBlocked_003Ek__BackingField;
		}
		private set
		{
			_003CScoutingStripsBlocked_003Ek__BackingField = value;
		}
	}

	public void Init(ShellDefinition shell, bool triggerNormalEvents = true)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00b5: Expected O, but got I4
		//IL_00be: Expected O, but got I4
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		this.shell = shell;
		bool triggerNormalEvents2 = default(bool);
		TriggerNormalEvents = triggerNormalEvents2;
		FireMission fireMission = FireMission._003CInstance_003Ek__BackingField;
		Dictionary<string, MapEntity>.ValueCollection values = fireMission.Entities.Values;
		if (DisableIfAnyEntityMatches)
		{
			Func<MapEntity, bool> predicate = delegate(MapEntity x)
			{
				//IL_0052: Expected I4, but got O
				StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
				if (EntityFilter == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				return EntityFilter.Resolve(x, newState);
			};
			bool flag = Enumerable.Any(values, predicate);
			bool flag2 = !flag;
			triggerNormalEvents2 = false;
			if (!flag2)
			{
				_003CScoutingStripsBlocked_003Ek__BackingField = true;
				bool flag3 = ScoutingStrips == null;
				triggerNormalEvents2 = false;
				if (!flag3)
				{
					GameObject[] scoutingStrips = ScoutingStrips;
					object obj = ScoutingStrips + 32;
					triggerNormalEvents2 = false;
					object obj2 = 0;
					object obj3 = 0;
					while ((nint)obj2 < scoutingStrips.Length)
					{
						bool flag4 = (UnityEngine.Object)obj != null;
						bool flag5 = !flag4;
						triggerNormalEvents2 = false;
						if (!flag5)
						{
							((GameObject)obj).SetActive(false);
							triggerNormalEvents2 = false;
						}
						obj3++;
						obj += 8;
						obj2 = obj3;
					}
				}
			}
		}
		_003CReportLocationNextFrame_003Ed__12 obj4 = new _003CReportLocationNextFrame_003Ed__12(0);
		obj4._003C_003E1__state = 0;
		obj4._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj4);
	}

	private void SetScoutingStripsActive(bool active)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0028: Expected O, but got I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		if (ScoutingStrips == null)
		{
			return;
		}
		GameObject[] scoutingStrips = ScoutingStrips;
		object obj = ScoutingStrips + 32;
		object obj2 = 0;
		while ((nint)obj2 < scoutingStrips.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				((GameObject)obj).SetActive(active);
			}
			obj2++;
			obj += 8;
		}
	}

	private IEnumerator ReportLocationNextFrame()
	{
		_003CReportLocationNextFrame_003Ed__12 obj = new _003CReportLocationNextFrame_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void EvaluateAndReport()
	{
		//IL_0145: Expected O, but got Ref
		//IL_010e: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		Transform transform;
		if ((bool)obj)
		{
			transform = (Transform)obj;
		}
		else
		{
			Transform transform2 = base.transform;
			transform = transform2;
		}
		Vector3 position = transform.position;
		RectTransform rectTransform = ResolveRootCanvasRect();
		float num = default(float);
		if (rectTransform == null)
		{
			if ((bool)obj)
			{
				Transform parent = ((Transform)obj).parent;
				if ((object)parent != null)
				{
					bool flag = (object)parent.GetType() != typeof(RectTransform);
					Transform transform3 = null;
					if (!flag)
					{
						transform3 = parent;
					}
					if ((object)transform3 != null)
					{
						Vector3 vector = transform3.InverseTransformPoint((Vector3)(&num));
						goto IL_014e;
					}
				}
			}
			Transform transform4 = base.transform;
			Vector3 localPosition = transform4.localPosition;
		}
		else
		{
			Vector3 vector2 = rectTransform.InverseTransformPoint((Vector3)(&num));
		}
		goto IL_014e;
		IL_014e:
		Vector2 impactLocation = default(Vector2);
		ImpactTracker.EvaluateImpact(shell, impactLocation, TriggerNormalEvents);
		base.enabled = false;
	}

	private RectTransform ResolveRootCanvasRect()
	{
		if (_cachedRootCanvas == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if ((bool)obj)
			{
				if ((object)obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
					UnityEngine.Object obj2 = default(UnityEngine.Object);
					if (!obj2)
					{
						Debug.LogWarning("[ImpactLocation] No parent Canvas found for impact object. Falling back to immediate parent space.", this);
						return null;
					}
					if ((object)obj2 != null)
					{
						Canvas rootCanvas = ((Canvas)obj2).rootCanvas;
						RectTransform rectTransform;
						if ((bool)rootCanvas)
						{
							Canvas rootCanvas2 = ((Canvas)obj2).rootCanvas;
							if ((object)rootCanvas2 == null)
							{
								goto IL_0180;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							RectTransform rectTransform2 = default(RectTransform);
							rectTransform = rectTransform2;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							RectTransform rectTransform3 = default(RectTransform);
							rectTransform = rectTransform3;
						}
						_cachedRootCanvas = rectTransform;
						return rectTransform;
					}
				}
				goto IL_0180;
			}
			Debug.LogWarning("[ImpactLocation] No RectTransform on impact object; cannot resolve root canvas. Falling back to transform localPosition.", this);
			return null;
		}
		return _cachedRootCanvas;
		IL_0180:
		return (RectTransform)(object)new NullReferenceException();
	}

	public ImpactLocation()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00f7: Expected I, but got O
		//IL_0107: Expected O, but got I
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		TriggerNormalEvents = true;
		DisableIfAnyEntityMatches = true;
		FilterEntitySet filterEntitySet = new FilterEntitySet();
		FilterEntitySet.FilterEntityPair[] array = new FilterEntitySet.FilterEntityPair[1];
		FilterEntitySet.FilterEntityPair filterEntityPair = new FilterEntitySet.FilterEntityPair();
		bool flag = filterEntityPair == null;
		FilterEntity filterEntity = null;
		FilterEntitySet.FilterEntityPair filterEntityPair2 = filterEntityPair;
		if (!flag)
		{
			filterEntityPair.operation = FilterEntitySet.FilterEntityPair.Operation.Base;
			FilterEntity filterEntity2 = new FilterEntity();
			bool flag2 = filterEntity2 == null;
			filterEntity = null;
			filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(object)filterEntity2;
			if (!flag2)
			{
				filterEntity2.FilterEntityType = FilterEntity.FilterEntityTypes.Role;
				filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(filterEntityPair + 24);
				filterEntity2.Operation = FilterEntity.OperationTypes.Contains;
				filterEntity2.RoleValue = EntityRoles.AABattery;
				filterEntityPair.FilterEntity = filterEntity2;
				bool flag3 = array == null;
				filterEntity = filterEntity2;
				if (!flag3)
				{
					nint num = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rdx_v10 (Il2CppClass<FilterEntityPair[]>)+40]");
					filterEntity = (FilterEntity)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj = default(object);
					bool flag4 = obj == null;
					filterEntityPair2 = filterEntityPair;
					if (flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj2 = default(object);
						throw obj2;
					}
					filterEntityPair2 = (FilterEntitySet.FilterEntityPair)(array + 32);
					array[0] = filterEntityPair;
					bool flag5 = filterEntitySet == null;
					filterEntity = (FilterEntity)(object)filterEntityPair;
					if (!flag5)
					{
						filterEntitySet.FilterEntitys = array;
						EntityFilter = filterEntitySet;
						base._002Ector();
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private bool _003CInit_003Eb__10_0(MapEntity x)
	{
		//IL_0052: Expected I4, but got O
		StateNode.NodeExecutionState newState = StateNode.NodeExecutionState.NewState;
		if (EntityFilter != null)
		{
			return EntityFilter.Resolve(x, newState);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}
}
