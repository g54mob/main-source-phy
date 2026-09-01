using System.Reflection;

namespace BitCode.Debug.MemberWrappers
{
	public interface IMethodWrapper : IMemberWrapper<MethodInfo>, IInvokableMember, IMemberWrapper
	{
	}
}
