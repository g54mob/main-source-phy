using System;
using System.Reflection;

namespace BitCode.Debug.MemberWrappers
{
	public interface IMemberWrapper
	{
		MemberInfo Member { get; }

		object Context { get; }

		Type MemberType { get; }
	}
	public interface IMemberWrapper<out T> : IMemberWrapper where T : MemberInfo
	{
		new T Member { get; }
	}
}
