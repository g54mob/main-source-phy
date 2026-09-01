using System.Reflection;
using UnityEngine;

namespace ModIO.UI
{
	public static class MemberReferenceExtensions
	{
		public static void SetValue(this MemberReference reference, EditableModProfile profile, string textValue)
		{
			FieldInfo editableModProfileField = GetEditableModProfileField(reference.MemberPath);
			if (editableModProfileField == null)
			{
				Debug.LogWarning("Could not find member '" + reference.MemberPath + "' on EditableModProfile");
			}
			else
			{
				editableModProfileField.SetValue(profile, textValue);
			}
		}

		private static FieldInfo GetEditableModProfileField(string memberPath)
		{
			switch (memberPath)
			{
			case "descriptionAsText":
				memberPath = "descriptionAsHTML";
				break;
			}
			return typeof(EditableModProfile).GetField(memberPath, BindingFlags.Instance | BindingFlags.Public);
		}
	}
}
