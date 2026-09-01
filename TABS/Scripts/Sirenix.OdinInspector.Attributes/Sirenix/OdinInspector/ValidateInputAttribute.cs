using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ValidateInputAttribute : Attribute
	{
		public string DefaultMessage;

		public string MemberName;

		public InfoMessageType MessageType;

		public bool IncludeChildren;

		public bool ContinuousValidationCheck;

		[Obsolete("Use the ContinuousValidationCheck member instead.")]
		public bool ContiniousValidationCheck
		{
			get
			{
				return ContinuousValidationCheck;
			}
			set
			{
				ContinuousValidationCheck = value;
			}
		}

		public ValidateInputAttribute(string memberName, string defaultMessage = null, InfoMessageType messageType = InfoMessageType.Error)
		{
			MemberName = memberName;
			DefaultMessage = defaultMessage;
			MessageType = messageType;
			IncludeChildren = true;
		}

		[Obsolete("Rejecting invalid input is no longer supported. Use the other constructor instead.", false)]
		public ValidateInputAttribute(string memberName, string message, InfoMessageType messageType, bool rejectedInvalidInput)
		{
			MemberName = memberName;
			DefaultMessage = message;
			MessageType = messageType;
			IncludeChildren = true;
		}
	}
}
