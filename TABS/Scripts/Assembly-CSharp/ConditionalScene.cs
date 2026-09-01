using System;

[Serializable]
public class ConditionalScene
{
	public enum ConditionType
	{
		AllTrue = 0,
		OneTrue = 1
	}

	public enum Condition
	{
		IsBeforeAprilFirst = 0,
		HasNotBeenTriggered = 1,
		IsEarlyAccess = 2,
		NeverHappen = 3,
		Switch = 4,
		TitleSafeScreen = 5
	}

	public string sceneToLoad = "";

	public string conditionKey = "";

	public Condition[] conditions;

	public ConditionType conditionType;
}
