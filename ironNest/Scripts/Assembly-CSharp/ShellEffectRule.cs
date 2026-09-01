using System;
using UnityEngine;

[Serializable]
public class ShellEffectRule
{
	[Tooltip("ShellDefinition this rule applies to. Impacts with this shell will be evaluated against this rule.")]
	public ShellDefinition shell;

	[Tooltip("Which effect to trigger when this shell meets the required impact count within the salvo window.\n- Success: Invokes On Success and returns ImpactOutcome.Success to impact reporters.\n- Failure: Invokes On Failure and returns ImpactOutcome.Failure to impact reporters.\n- Bonus: Invokes On Bonus and returns ImpactOutcome.Success to impact reporters.\n- None: Ignore impacts for this shell (treated as NoEffect).")]
	public LocationEffectKind effect;

	[Min(1f)]
	[Tooltip("Number of impacts with THIS shell required within the 'Salvo Window Seconds' to trigger the effect.\nExamples:\n- 1 = single-shot triggers the effect immediately.\n- 2 = dual-shot salvo required within the window.\nOnly impacts from this specific shell count toward this rule.")]
	public int requiredImpacts;
}
