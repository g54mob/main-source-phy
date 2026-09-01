using System;

namespace ModIO
{
	[Serializable]
	public class EditableModfile
	{
		public EditableStringField version = new EditableStringField();

		public EditableStringField changelog = new EditableStringField();

		public EditableStringField metadataBlob = new EditableStringField();

		public static EditableModfile CreateFromModfile(Modfile modfile)
		{
			EditableModfile editableModfile = new EditableModfile();
			editableModfile.ApplyBaseModfileChanges(modfile);
			return editableModfile;
		}

		public void ApplyBaseModfileChanges(Modfile modfile)
		{
			if (!version.isDirty)
			{
				version.value = modfile.version;
			}
			if (!changelog.isDirty)
			{
				changelog.value = modfile.changelog;
			}
			if (!metadataBlob.isDirty)
			{
				metadataBlob.value = modfile.metadataBlob;
			}
		}
	}
}
