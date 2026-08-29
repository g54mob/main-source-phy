using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Battlehub.Utils
{
	public class Strong
	{
		public static PropertyInfo PropertyInfo<T, U>(Expression<Func<T, U>> expression, string propertyName = null)
		{
			return (PropertyInfo)MemberInfo(expression);
		}

		public static MemberInfo MemberInfo<T, U>(Expression<Func<T, U>> expression)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				return memberExpression.Member;
			}
			throw new ArgumentException("Expression is not a member access", "expression");
		}

		public static MethodInfo MethodInfo<T>(Expression<Func<T, Delegate>> expression)
		{
			return (MethodInfo)((ConstantExpression)((MethodCallExpression)((UnaryExpression)expression.Body).Operand).Arguments.Last()).Value;
		}

		public static MethodInfo MethodInfo<T>(Expression<Action<T>> method)
		{
			return ((MethodCallExpression)method.Body).Method;
		}
	}
}
