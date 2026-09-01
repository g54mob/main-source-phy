using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeGroundsCan : MonoBehaviour
{
	public float baseQuality = 0.8f;

	public string coffeeLabel = "Coffee Grounds";

	public int maxUses = 5;

	public int remainingUses;

	public bool IsLoaded;

	public UnityEvent OnLoaded;

	public UnityEvent OnUnloaded;

	public UnityEvent<int> OnUseConsumed;

	public UnityEvent OnEmpty;

	private DraggableItem _003CDraggableItem_003Ek__BackingField;

	public bool IsEmpty
	{
		get
		{
			if (maxUses < 0)
			{
				return false;
			}
			int num = remainingUses ^ remainingUses;
			int num2 = remainingUses & num;
			bool flag = num2 < 0;
			bool flag2 = remainingUses < 0;
			bool flag3 = remainingUses == 0;
			bool flag4 = flag2 != flag;
			return flag4 | flag3;
		}
	}

	public DraggableItem DraggableItem
	{
		get
		{
			return _003CDraggableItem_003Ek__BackingField;
		}
		private set
		{
			_003CDraggableItem_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		DraggableItem draggableItem = default(DraggableItem);
		_003CDraggableItem_003Ek__BackingField = draggableItem;
		if (maxUses >= 0)
		{
			remainingUses = maxUses;
		}
		else
		{
			remainingUses = 2147483647;
		}
	}

	public void Load()
	{
		if (!IsLoaded)
		{
			IsLoaded = true;
			if (OnLoaded != null)
			{
				OnLoaded.Invoke();
			}
		}
	}

	public void Unload()
	{
		if (IsLoaded)
		{
			IsLoaded = false;
			if (OnUnloaded != null)
			{
				OnUnloaded.Invoke();
			}
		}
	}

	public unsafe bool ConsumeUse()
	{
		//IL_0100: Expected O, but got I4
		int num = maxUses ^ maxUses;
		int num2 = maxUses & num;
		bool flag = num2 < 0;
		bool flag2 = maxUses < 0;
		bool flag3 = flag2 == flag;
		object obj = !flag3;
		if (obj == null)
		{
			int num3 = remainingUses - 1;
			int num4 = 0;
			if (!flag2)
			{
				num4 = num3;
			}
			remainingUses = num4;
			if (OnUseConsumed != null)
			{
				int num5 = default(int);
				OnUseConsumed.Invoke((int)(&num5));
			}
			if (remainingUses <= 0)
			{
				if (OnEmpty != null)
				{
					OnEmpty.Invoke();
				}
				return true;
			}
		}
		return false;
	}

	public CoffeeGroundsCan()
	{
		UnityEvent onLoaded = new UnityEvent();
		OnLoaded = onLoaded;
		OnUnloaded = new UnityEvent();
		OnUseConsumed = new UnityEvent<int>();
		OnEmpty = new UnityEvent();
		base._002Ector();
	}
}
