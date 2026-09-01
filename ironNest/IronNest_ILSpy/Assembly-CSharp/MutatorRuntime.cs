using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class MutatorRuntime : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Converter<MutatorDefinition, string> _003C_003E9__11_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CSetActiveMutators_003Eb__11_0(MutatorDefinition m)
		{
			if ((object)m != null)
			{
				return m.displayName;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private static MutatorRuntime _003CInstance_003Ek__BackingField;

	private Action<IReadOnlyList<MutatorDefinition>> m_MutatorsChanged;

	public bool verbose;

	private readonly List<MutatorDefinition> _activeList;

	private readonly HashSet<MutatorDefinition> _activeSet;

	public static MutatorRuntime Instance
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

	public IReadOnlyList<MutatorDefinition> ActiveMutators => _activeList;

	public event Action<IReadOnlyList<MutatorDefinition>> MutatorsChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 32;
			Delegate obj2 = this.m_MutatorsChanged;
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
			object obj = this + 32;
			Delegate obj2 = this.m_MutatorsChanged;
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
		}
		else
		{
			_003CInstance_003Ek__BackingField = this;
			GameObject target = base.gameObject;
			UnityEngine.Object.DontDestroyOnLoad(target);
		}
	}

	public void SetActiveMutators(IList<MutatorDefinition> mutators)
	{
		//IL_00b6: Expected I, but got O
		//IL_0126: Expected I, but got O
		//IL_0319: Expected I, but got O
		List<MutatorDefinition> activeList = _activeList;
		int version = activeList._version + 1;
		activeList._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			activeList._size = 0;
		}
		else
		{
			activeList._size = 0;
			if (activeList._size > 0)
			{
				Array.Clear(activeList._items, 0, activeList._size);
				nint num = unchecked((nint)null);
			}
		}
		_activeSet.Clear();
		if (mutators != null)
		{
			int num2 = 0;
			int num3 = 0;
			object obj2 = default(object);
			while (true)
			{
				int count = mutators.Count;
				bool flag = num2 >= count;
				nint num = (nint)typeof(ICollection<MutatorDefinition>);
				if (flag)
				{
					break;
				}
				MutatorDefinition mutatorDefinition = mutators.get_Item(num3);
				if (mutatorDefinition != null)
				{
					_activeSet.Add(mutatorDefinition);
					if (obj2 != null)
					{
						_activeList.Add(mutatorDefinition);
					}
				}
				num3++;
				num2 = num3;
			}
		}
		if (verbose)
		{
			List<MutatorDefinition> activeList2 = _activeList;
			object message;
			if (activeList2._size == 0)
			{
				message = "[MutatorRuntime] Active mutators set: (none)";
			}
			else
			{
				Converter<MutatorDefinition, string> converter = _003C_003Ec._003C_003E9__11_0;
				if (_003C_003Ec._003C_003E9__11_0 == null)
				{
					Converter<MutatorDefinition, string> converter2 = (_003C_003Ec._003C_003E9__11_0 = (MutatorDefinition m) => (string)(((object)m != null) ? ((object)m.displayName) : ((object)new NullReferenceException())));
					nint num = unchecked((nint)null);
					converter = converter2;
				}
				List<string> values = activeList2.ConvertAll(converter);
				string text = string.Join(", ", values);
				string text2 = "[MutatorRuntime] Active mutators set: " + text;
				message = text2;
			}
			Debug.Log(message);
		}
		Action<IReadOnlyList<MutatorDefinition>> mutatorsChanged = this.m_MutatorsChanged;
		if (this.m_MutatorsChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v381 @ rcx_v10 (System.Action`1<System.Collections.Generic.IReadOnlyList`1<MutatorDefinition>>)+18] (should have been resolved before IL gen)");
		}
	}

	public void ClearActiveMutators()
	{
		SetActiveMutators(null);
	}

	public bool IsActive(MutatorDefinition mutator)
	{
		//IL_006b: Expected I4, but got O
		bool flag = mutator != null;
		if (!flag)
		{
			return flag;
		}
		if (_activeSet != null)
		{
			return _activeSet.Contains(mutator);
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public MutatorRuntime()
	{
		List<MutatorDefinition> activeList = new List<MutatorDefinition>();
		_activeList = activeList;
		_activeSet = new HashSet<MutatorDefinition>();
		base._002Ector();
	}
}
