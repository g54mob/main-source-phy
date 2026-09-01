using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ImpactCorrectionTierController : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<CorrectionDistanceTierConfig> _003C_003E9__26_0;

		public static Comparison<CorrectionDirectionTierConfig> _003C_003E9__26_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal int _003CCacheTiers_003Eb__26_0(CorrectionDistanceTierConfig a, CorrectionDistanceTierConfig b)
		{
			//IL_00d1: Expected I4, but got O
			if ((object)a != null)
			{
				Transform transform = a.transform;
				if ((object)transform != null)
				{
					int siblingIndex = transform.GetSiblingIndex();
					if ((object)b != null)
					{
						Transform transform2 = b.transform;
						if ((object)transform2 != null)
						{
							int siblingIndex2 = transform2.GetSiblingIndex();
							int num = default(int);
							return num.CompareTo(siblingIndex2);
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}

		internal int _003CCacheTiers_003Eb__26_1(CorrectionDirectionTierConfig a, CorrectionDirectionTierConfig b)
		{
			//IL_00d1: Expected I4, but got O
			if ((object)a != null)
			{
				Transform transform = a.transform;
				if ((object)transform != null)
				{
					int siblingIndex = transform.GetSiblingIndex();
					if ((object)b != null)
					{
						Transform transform2 = b.transform;
						if ((object)transform2 != null)
						{
							int siblingIndex2 = transform2.GetSiblingIndex();
							int num = default(int);
							return num.CompareTo(siblingIndex2);
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public Transform distanceTierRoot;

	public Transform directionTierRoot;

	public bool autoReevaluateOnTierStateChange = true;

	private CorrectionDistanceTierConfig _003CActiveDistanceTier_003Ek__BackingField;

	private CorrectionDirectionTierConfig _003CActiveDirectionTier_003Ek__BackingField;

	private static ImpactCorrectionTierController _003CInstance_003Ek__BackingField;

	private static Action m_OnActiveTiersChanged;

	private static bool _queuedGlobalReevaluate;

	private readonly List<CorrectionDistanceTierConfig> _distanceTiers;

	private readonly List<CorrectionDirectionTierConfig> _directionTiers;

	public CorrectionDistanceTierConfig ActiveDistanceTier
	{
		get
		{
			return _003CActiveDistanceTier_003Ek__BackingField;
		}
		private set
		{
			_003CActiveDistanceTier_003Ek__BackingField = value;
		}
	}

	public CorrectionDirectionTierConfig ActiveDirectionTier
	{
		get
		{
			return _003CActiveDirectionTier_003Ek__BackingField;
		}
		private set
		{
			_003CActiveDirectionTier_003Ek__BackingField = value;
		}
	}

	public static ImpactCorrectionTierController Instance
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

	public static event Action OnActiveTiersChanged
	{
		add
		{
			//IL_004f: Expected I, but got O
			//IL_0065: Expected O, but got I
			Delegate obj = ImpactCorrectionTierController.m_OnActiveTiersChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Action);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(ImpactCorrectionTierController);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v5 (Il2CppClass<ImpactCorrectionTierController>)+B8]");
				object obj4 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj;
				obj = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_004f: Expected I, but got O
			//IL_0065: Expected O, but got I
			Delegate obj = ImpactCorrectionTierController.m_OnActiveTiersChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = null;
				if (!flag)
				{
					bool flag2 = (object)obj2.GetType() != typeof(Action);
					obj3 = null;
					if (!flag2)
					{
						obj3 = obj2;
					}
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(ImpactCorrectionTierController);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v5 (Il2CppClass<ImpactCorrectionTierController>)+B8]");
				object obj4 = (nint)0 + (nint)8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag3 = (object)obj5 != obj;
				obj = obj5;
				if (!flag3)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if ((bool)_003CInstance_003Ek__BackingField && _003CInstance_003Ek__BackingField != this)
		{
			string text = _003CInstance_003Ek__BackingField.name;
			string text2 = base.name;
			string message = "[ImpactCorrectionTierController] Replacing existing Instance (" + text + ") with " + text2 + ".";
			Debug.LogWarning(message, this);
		}
		_003CInstance_003Ek__BackingField = this;
		CacheTiers();
		CorrectionDistanceTierConfig highestActive = GetHighestActive(_distanceTiers);
		_003CActiveDistanceTier_003Ek__BackingField = highestActive;
		CorrectionDirectionTierConfig highestActive2 = GetHighestActive(_directionTiers);
		_003CActiveDirectionTier_003Ek__BackingField = highestActive2;
		ApplyPointerVisualSelection();
	}

	private void OnEnable()
	{
		EvaluateActiveTiers();
	}

	private void Update()
	{
		if (_queuedGlobalReevaluate)
		{
			_queuedGlobalReevaluate = false;
			CacheTiers();
			EvaluateActiveTiers();
		}
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	public void ReevaluateNow()
	{
		CacheTiers();
		EvaluateActiveTiers();
	}

	private void CacheTiers()
	{
		List<CorrectionDistanceTierConfig> distanceTiers = _distanceTiers;
		int version = distanceTiers._version + 1;
		distanceTiers._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			distanceTiers._size = 0;
		}
		else
		{
			distanceTiers._size = 0;
			if (distanceTiers._size > 0)
			{
				Array.Clear(distanceTiers._items, 0, distanceTiers._size);
			}
		}
		List<CorrectionDirectionTierConfig> directionTiers = _directionTiers;
		int version2 = directionTiers._version + 1;
		directionTiers._version = version2;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<CorrectionDirectionTierConfig>())
		{
			directionTiers._size = 0;
		}
		else
		{
			directionTiers._size = 0;
			if (directionTiers._size > 0)
			{
				Array.Clear(directionTiers._items, 0, directionTiers._size);
			}
		}
		if ((bool)distanceTierRoot)
		{
			distanceTierRoot.GetComponentsInChildren(includeInactive: true, _distanceTiers);
		}
		if ((bool)directionTierRoot)
		{
			directionTierRoot.GetComponentsInChildren(includeInactive: true, _directionTiers);
		}
		Comparison<CorrectionDistanceTierConfig> comparison = _003C_003Ec._003C_003E9__26_0;
		if (_003C_003Ec._003C_003E9__26_0 == null)
		{
			comparison = (_003C_003Ec._003C_003E9__26_0 = delegate(CorrectionDistanceTierConfig a, CorrectionDistanceTierConfig b)
			{
				//IL_00d1: Expected I4, but got O
				if ((object)a != null)
				{
					Transform transform = a.transform;
					if ((object)transform != null)
					{
						int siblingIndex = transform.GetSiblingIndex();
						if ((object)b != null)
						{
							Transform transform2 = b.transform;
							if ((object)transform2 != null)
							{
								int siblingIndex2 = transform2.GetSiblingIndex();
								int num = default(int);
								return num.CompareTo(siblingIndex2);
							}
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			});
		}
		_distanceTiers.Sort(comparison);
		Comparison<CorrectionDirectionTierConfig> comparison2 = _003C_003Ec._003C_003E9__26_1;
		if (_003C_003Ec._003C_003E9__26_1 == null)
		{
			comparison2 = (_003C_003Ec._003C_003E9__26_1 = delegate(CorrectionDirectionTierConfig a, CorrectionDirectionTierConfig b)
			{
				//IL_00d1: Expected I4, but got O
				if ((object)a != null)
				{
					Transform transform = a.transform;
					if ((object)transform != null)
					{
						int siblingIndex = transform.GetSiblingIndex();
						if ((object)b != null)
						{
							Transform transform2 = b.transform;
							if ((object)transform2 != null)
							{
								int siblingIndex2 = transform2.GetSiblingIndex();
								int num = default(int);
								return num.CompareTo(siblingIndex2);
							}
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			});
		}
		_directionTiers.Sort(comparison2);
	}

	private void EvaluateActiveTiers(bool invokeEvent = true)
	{
		CorrectionDistanceTierConfig highestActive = GetHighestActive(_distanceTiers);
		_003CActiveDistanceTier_003Ek__BackingField = highestActive;
		CorrectionDirectionTierConfig highestActive2 = GetHighestActive(_directionTiers);
		_003CActiveDirectionTier_003Ek__BackingField = highestActive2;
		ApplyPointerVisualSelection();
		if (invokeEvent)
		{
			Action onActiveTiersChanged = ImpactCorrectionTierController.m_OnActiveTiersChanged;
			if (ImpactCorrectionTierController.m_OnActiveTiersChanged != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v60.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
			}
		}
	}

	private unsafe T GetHighestActive<T>(List<T> list) where T : MonoBehaviour
	{
		//IL_0040: Expected O, but got Ref
		//IL_015b: Expected O, but got I
		//IL_0137: Expected O, but got I
		//IL_0060: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		T result = null;
		nint num = 0;
		object obj4 = default(object);
		UnityEngine.Object obj6 = default(UnityEngine.Object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ stack_18_v4+38]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808437D0");
			if (obj4 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ stack_18_v4+38]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v253 @ r8_v7+20]");
				num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((bool)obj6)
				{
					if ((object)obj6 == null)
					{
						throw new NullReferenceException();
					}
					GameObject gameObject = ((Component)obj6).gameObject;
					if ((object)gameObject == null)
					{
						break;
					}
					if (gameObject.activeInHierarchy)
					{
						result = (T)obj6;
					}
				}
				continue;
			}
			object obj7 = obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rax_v13+38]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			return result;
		}
		throw new NullReferenceException();
	}

	private void ApplyPointerVisualSelection()
	{
		//IL_007e: Expected O, but got I
		//IL_00d7: Expected O, but got I
		if (_directionTiers != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<CorrectionDirectionTierConfig>.Enumerator enumerator = default(List<CorrectionDirectionTierConfig>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!obj)
				{
					continue;
				}
				if ((object)obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ stack_18_v5 (UnityEngine.Object)+20]");
					if ((bool)(UnityEngine.Object)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ stack_18_v5 (UnityEngine.Object)+20]");
						if ((nint)0 == 0)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ stack_18_v5 (UnityEngine.Object)+20]");
						((GameObject)0).SetActive(value: false);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			if (!_003CActiveDirectionTier_003Ek__BackingField)
			{
				return;
			}
			CorrectionDirectionTierConfig correctionDirectionTierConfig = _003CActiveDirectionTier_003Ek__BackingField;
			if ((object)_003CActiveDirectionTier_003Ek__BackingField != null)
			{
				if (!correctionDirectionTierConfig.pointerVisual)
				{
					return;
				}
				CorrectionDirectionTierConfig correctionDirectionTierConfig2 = _003CActiveDirectionTier_003Ek__BackingField;
				if ((object)_003CActiveDirectionTier_003Ek__BackingField != null && (object)correctionDirectionTierConfig2.pointerVisual != null)
				{
					correctionDirectionTierConfig2.pointerVisual.SetActive(value: true);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	internal void HandleTierStateChanged()
	{
		if (autoReevaluateOnTierStateChange)
		{
			CacheTiers();
			EvaluateActiveTiers();
		}
	}

	public static void ScheduleGlobalReevaluate()
	{
		if (!_003CInstance_003Ek__BackingField)
		{
			_queuedGlobalReevaluate = true;
			return;
		}
		ImpactCorrectionTierController impactCorrectionTierController = _003CInstance_003Ek__BackingField;
		if (impactCorrectionTierController.autoReevaluateOnTierStateChange)
		{
			impactCorrectionTierController.CacheTiers();
			impactCorrectionTierController.EvaluateActiveTiers();
		}
	}

	public ImpactCorrectionTierController()
	{
		List<CorrectionDistanceTierConfig> distanceTiers = new List<CorrectionDistanceTierConfig>();
		_distanceTiers = distanceTiers;
		_directionTiers = new List<CorrectionDirectionTierConfig>();
		base._002Ector();
	}
}
