using UnityEngine;

namespace LevelCreator
{
	[CreateAssetMenu(menuName = "DataTables/VolumeBrushTable")]
	public class VolumeBrushTable : DataTable<VolumeBrushRow>
	{
		public Brush GetBrush(string key)
		{
			return VolumeBrushes.CreateBrush(GetRowValue(key));
		}
	}
}
