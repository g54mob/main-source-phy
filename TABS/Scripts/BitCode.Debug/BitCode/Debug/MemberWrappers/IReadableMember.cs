namespace BitCode.Debug.MemberWrappers
{
	public interface IReadableMember : IMemberWrapper
	{
		bool CanRead { get; }

		object GetValue();
	}
}
