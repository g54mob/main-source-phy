using System;

namespace ModIO
{
	[Serializable]
	public class EditableReport
	{
		public EditableResourceTypeField resourceType = new EditableResourceTypeField();

		public EditableIntField resourceId = new EditableIntField();

		public EditableReportTypeField reportType = new EditableReportTypeField();

		public EditableStringField summary = new EditableStringField();

		public EditableStringField name = new EditableStringField();

		public EditableIntField contact = new EditableIntField();

		[Obsolete("No longer supported. Use EditableReport.reportType instead.", true)]
		public EditableBoolField isDMCA;

		public static string ResourceTypeToAPIString(ReportedResourceType resourceType)
		{
			switch (resourceType)
			{
			case ReportedResourceType.Game:
				return "games";
			case ReportedResourceType.Mod:
				return "mods";
			case ReportedResourceType.User:
				return "users";
			default:
				return string.Empty;
			}
		}
	}
}
