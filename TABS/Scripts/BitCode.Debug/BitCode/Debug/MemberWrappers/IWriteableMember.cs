namespace BitCode.Debug.MemberWrappers
{
	public interface IWriteableMember : IMemberWrapper
	{
		bool CanWrite { get; }

		void SetValue(IParameterResolver resolver, string token);
	}
}
