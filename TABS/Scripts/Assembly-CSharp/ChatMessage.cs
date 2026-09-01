using System;

[Serializable]
public class ChatMessage
{
	public enum BubbleTarget
	{
		self = 0,
		target = 1
	}

	public BubbleTarget bubbleTarget;

	public string text;
}
