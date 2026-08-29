using System;

[AttributeUsage(AttributeTargets.Field)]
public class PuzzleInfoAttribute : Attribute
{
	public string nameDisplayedInChat { get; }

	public bool requiresTeamwork { get; }

	public PuzzleInfoAttribute(string nameDisplayedInChat, bool requiresTeamwork = false)
	{
		this.nameDisplayedInChat = nameDisplayedInChat;
		this.requiresTeamwork = requiresTeamwork;
	}
}
