using System.Reflection;

namespace BitCode.Debug.MemberWrappers
{
	public static class MemberWrapperExtensions
	{
		public static bool IsStatic<T>(this IMemberWrapper<T> memberWrapper) where T : MemberInfo
		{
			return memberWrapper.Context == null;
		}
	}
}
