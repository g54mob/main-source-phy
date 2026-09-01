using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelCreator
{
	[CreateAssetMenu(menuName = "DataTables/DMEditorObjectTable")]
	public class DMEditorObjectTable : DataTable<DMEditorObjectRow>
	{
		private string baseFilePath = "/LevelCreator/TABS Integration/ThumbnailGenerator/Thumbnails/";

		public List<string> GetVariants(string objectID, bool excludeInputObjectID)
		{
			DMEditorObjectRow[] rowValues = GetRowValues();
			string[] keys = GetKeys();
			List<string> list = new List<string>();
			DMEditorObjectRow rowValue = GetRowValue(objectID);
			for (int i = 0; i < rowValues.Length; i++)
			{
				if (rowValue.RadialMenuPath == rowValues[i].RadialMenuPath && (!excludeInputObjectID || (excludeInputObjectID && rowValue.ObjectName != rowValues[i].ObjectName)))
				{
					list.Add(keys[i]);
				}
			}
			return list;
		}

		public List<string> GetGroupObjectsIDs(string objectID, bool excludeInputObjectID)
		{
			DMEditorObjectRow[] rowValues = GetRowValues();
			string[] keys = GetKeys();
			List<string> list = new List<string>();
			DMEditorObjectRow rowValue = GetRowValue(objectID);
			for (int i = 0; i < rowValues.Length; i++)
			{
				if (rowValue.RadialMenuTheme == rowValues[i].RadialMenuTheme && rowValue.RadialMenuGroup == rowValues[i].RadialMenuGroup && (!excludeInputObjectID || (excludeInputObjectID && rowValue.ObjectName != rowValues[i].ObjectName)))
				{
					list.Add(keys[i]);
				}
			}
			return list;
		}

		public List<string> GetCategoryObjectsIDs(string objectID, bool excludeInputObjectID)
		{
			DMEditorObjectRow[] rowValues = GetRowValues();
			string[] keys = GetKeys();
			rowValues.OrderByDescending((DMEditorObjectRow g) => g);
			List<string> list = new List<string>();
			DMEditorObjectRow rowValue = GetRowValue(objectID);
			for (int num = 0; num < rowValues.Length; num++)
			{
				if (rowValue.RadialMenuTheme == rowValues[num].RadialMenuTheme && (!excludeInputObjectID || (excludeInputObjectID && rowValue.ObjectName != rowValues[num].ObjectName)))
				{
					list.Add(keys[num]);
				}
			}
			return list;
		}

		public List<DMEditorObjectRow> GetCategoryObjectsRows(string objectID, bool excludeInputObjectID)
		{
			DMEditorObjectRow[] rowValues = GetRowValues();
			DMEditorObjectRow rowValue = GetRowValue(objectID);
			List<DMEditorObjectRow> list = new List<DMEditorObjectRow>();
			for (int i = 0; i < rowValues.Length; i++)
			{
				if (rowValue.RadialMenuTheme == rowValues[i].RadialMenuTheme && (!excludeInputObjectID || (excludeInputObjectID && rowValue.ObjectName != rowValues[i].ObjectName)))
				{
					list.Add(rowValues[i]);
				}
			}
			return list.OrderBy((DMEditorObjectRow o) => o.RadialMenuGroup).ToList();
		}

		public string GetDisplayName(string objectID)
		{
			return GetRowValue(objectID).GetLocalizedRowName();
		}
	}
}
