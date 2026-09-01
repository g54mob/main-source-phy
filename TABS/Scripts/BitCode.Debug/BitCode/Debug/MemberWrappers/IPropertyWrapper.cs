using System.Reflection;

namespace BitCode.Debug.MemberWrappers
{
	public interface IPropertyWrapper : IMemberWrapper<PropertyInfo>, IMemberWrapper, IReadableMember, IWriteableMember
	{
	}
}
