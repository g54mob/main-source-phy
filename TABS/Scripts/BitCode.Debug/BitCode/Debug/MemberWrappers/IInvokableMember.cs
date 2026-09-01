using System.Collections.Generic;

namespace BitCode.Debug.MemberWrappers
{
	public interface IInvokableMember
	{
		object Invoke(IParameterResolver resolver, IReadOnlyList<string> tokens, ref int lastResolvedToken);
	}
}
