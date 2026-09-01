using System;
using System.Reflection;

namespace NATTraversal
{
	public static class Util
	{
		public static bool HasFlag(this ConnectionType a, ConnectionType b)
		{
			return (a & b) != 0;
		}

		public static DelegateType CreateDelegate<ClassType, DelegateType>(string methodName, BindingFlags flags) where DelegateType : class
		{
			MethodInfo method = typeof(ClassType).GetMethod(methodName, flags);
			return Delegate.CreateDelegate(typeof(DelegateType), method) as DelegateType;
		}
	}
}
