using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlotDrinkTrigger : MonoBehaviour
{
	private ItemSlot targetSlot;

	public UnityEvent OnDrinkTriggered;

	public UnityEvent OnDrinkFailed;

	private bool debugLog;

	private void Awake()
	{
		if (!(targetSlot == null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		ItemSlot itemSlot = default(ItemSlot);
		targetSlot = itemSlot;
		bool flag = targetSlot == null;
		if (!flag)
		{
			if (debugLog != flag)
			{
				string text = base.name;
				string message = "[ItemSlotDrinkTrigger] '" + text + "': Auto-fetched ItemSlot from this GameObject.";
				Debug.Log(message, this);
			}
		}
		else
		{
			string text2 = base.name;
			string message2 = "[ItemSlotDrinkTrigger] '" + text2 + "': No ItemSlot assigned and none found on this GameObject. TriggerDrink() will always fail.";
			Debug.LogWarning(message2, this);
		}
	}

	public void TriggerDrink()
	{
		string[] array;
		object obj3;
		UnityEvent unityEvent;
		if (targetSlot != null)
		{
			ItemSlot itemSlot = targetSlot;
			if (itemSlot.CurrentItem != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Object obj = default(Object);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					Object obj2 = default(Object);
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ stack_18_v7 (UnityEngine.Object)+20]");
						if ((nint)0 == 0)
						{
							if (debugLog)
							{
								string text = base.name;
								string text2 = itemSlot.CurrentItem.name;
								array = new string[5] { "[ItemSlotDrinkTrigger] '", text, "': TriggerDrink failed — cup '", text2, null };
								obj3 = "' is empty.";
								goto IL_036e;
							}
							goto IL_03a6;
						}
					}
					if (debugLog)
					{
						string text3 = base.name;
						string text4 = itemSlot.CurrentItem.name;
						string text5 = targetSlot.name;
						string message = "[ItemSlotDrinkTrigger] '" + text3 + "': Triggering drink on '" + text4 + "' in slot '" + text5 + "'.";
						Debug.Log(message, this);
					}
					((EspressoCupDrinker)obj).DrinkCoffee();
					unityEvent = OnDrinkTriggered;
					goto IL_04fc;
				}
				if (debugLog)
				{
					string text6 = base.name;
					string text7 = itemSlot.CurrentItem.name;
					array = new string[5] { "[ItemSlotDrinkTrigger] '", text6, "': TriggerDrink failed — item '", text7, null };
					obj3 = "' has no EspressoCupDrinker component.";
					goto IL_036e;
				}
			}
			else if (debugLog)
			{
				string text8 = base.name;
				string text9 = targetSlot.name;
				array = new string[5] { "[ItemSlotDrinkTrigger] '", text8, "': TriggerDrink failed — slot '", text9, null };
				obj3 = "' has no item.";
				goto IL_036e;
			}
			goto IL_03a6;
		}
		if (debugLog)
		{
			string text10 = base.name;
			string message2 = "[ItemSlotDrinkTrigger] '" + text10 + "': TriggerDrink failed — no ItemSlot assigned.";
			Debug.Log(message2, this);
		}
		if (OnDrinkFailed != null)
		{
			OnDrinkFailed.Invoke();
		}
		return;
		IL_03a6:
		unityEvent = OnDrinkFailed;
		goto IL_04fc;
		IL_04fc:
		unityEvent?.Invoke();
		return;
		IL_036e:
		array[4] = (string)obj3;
		string message3 = string.Concat(array);
		Debug.Log(message3, this);
		goto IL_03a6;
	}

	public ItemSlotDrinkTrigger()
	{
		UnityEvent onDrinkTriggered = new UnityEvent();
		OnDrinkTriggered = onDrinkTriggered;
		OnDrinkFailed = new UnityEvent();
		base._002Ector();
	}
}
