using System;

[Serializable]
public class ShellEffectRule
{
	public ShellDefinition shell;

	public LocationEffectKind effect = LocationEffectKind.Success;

	public int requiredImpacts = 1;
}
