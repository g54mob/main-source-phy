namespace BitCode.Profiles.Rules
{
	public interface ISelectionRule
	{
		bool RuleMatches(IProfileSelectionState state);
	}
}
