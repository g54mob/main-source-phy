using Localisation;
using UnityEngine;

namespace SleepyNodes;

public class State_SendUINotification : StateNode
{
	public StateNode To;

	public TextIdentifier Text_Title;

	public TextIdentifier Text_Description;

	public float Duration = 5f;

	public Color Tint;

	public unsafe override void OnEnter(NodeExecutionState state)
	{
		//IL_0035: Expected O, but got Ref
		//IL_0051: Expected O, but got Ref
		base.OnEnter(state);
		string title = Text_Title.Get();
		string description = Text_Description.Get();
		object obj = default(object);
		Color? color = (Color)(&obj);
		object obj2 = default(object);
		UINotification uINotification = UINotificationManager.ShowNotification(title, description, Duration, (Color?)(object)(&obj2));
		base.OnExit(state, "To");
	}
}
