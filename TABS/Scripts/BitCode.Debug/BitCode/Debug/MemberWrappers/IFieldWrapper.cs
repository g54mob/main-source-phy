using System.Reflection;

namespace BitCode.Debug.MemberWrappers
{
	public interface IFieldWrapper : IMemberWrapper<FieldInfo>, IMemberWrapper, IReadableMember, IWriteableMember
	{
	}
}
