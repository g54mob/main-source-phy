using System;
using System.Collections.Generic;
using FMODUnity;

[Serializable]
public class DialogueLine
{
	public string name;

	public string displayText;

	[NonSerialized]
	public string originalText;

	public List<Choice> choicesNeo;

	public EventReference lineSound;
}
