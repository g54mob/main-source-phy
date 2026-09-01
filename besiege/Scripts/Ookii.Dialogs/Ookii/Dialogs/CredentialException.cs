using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	[Serializable]
	public class CredentialException : Win32Exception
	{
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException()
			: base(Resources.CredentialError)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(int error)
			: base(error)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(string message)
			: base(message)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(int error, string message)
			: base(error, message)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public CredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected CredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
