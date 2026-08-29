using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class DebugButtonAttribute : Attribute
{
	public string label;

	public Tint tint;

	public PostClickAction postClickAction;

	public int priority;

	public object[] args;

	public DebugButtonAttribute(string label = null, Tint tint = Tint.Default, PostClickAction postClickAction = (PostClickAction)0, int priority = 0, params object[] args)
	{
		this.label = label;
		this.tint = tint;
		this.postClickAction = postClickAction;
		this.priority = priority;
		this.args = args;
	}
}
