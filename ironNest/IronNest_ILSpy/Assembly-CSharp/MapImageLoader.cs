using SleepyNodes;
using UnityEngine;
using UnityEngine.UI;

public class MapImageLoader : MonoBehaviour
{
	public enum ImageTypes
	{
		Primary,
		Topography
	}

	public ImageTypes ImageType;

	public Image Image_Map;

	public void Start()
	{
		Object obj;
		if (ImageType == ImageTypes.Primary)
		{
			MissionManager missionManager = MissionManager._003CInstance_003Ek__BackingField;
			if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
			{
				MissionGraph missionGraph = missionManager._003CCurrentMission_003Ek__BackingField;
				if ((object)missionManager._003CCurrentMission_003Ek__BackingField != null)
				{
					obj = missionGraph.MapOverride;
					goto IL_0055;
				}
			}
			obj = null;
			goto IL_0055;
		}
		Object obj2;
		if (ImageType == ImageTypes.Topography)
		{
			MissionManager missionManager2 = MissionManager._003CInstance_003Ek__BackingField;
			if ((object)MissionManager._003CInstance_003Ek__BackingField != null)
			{
				MissionGraph missionGraph2 = missionManager2._003CCurrentMission_003Ek__BackingField;
				if ((object)missionManager2._003CCurrentMission_003Ek__BackingField != null)
				{
					obj2 = missionGraph2.MapTopographyOverride;
					goto IL_011c;
				}
			}
			obj2 = null;
			goto IL_011c;
		}
		Object obj3 = null;
		goto IL_017b;
		IL_017b:
		if (obj3 != null)
		{
			Image_Map.sprite = (Sprite)obj3;
		}
		return;
		IL_0055:
		bool flag = obj != null;
		bool flag2 = !flag;
		obj3 = null;
		if (!flag2)
		{
			MissionManager missionManager3 = MissionManager._003CInstance_003Ek__BackingField;
			MissionGraph missionGraph3 = missionManager3._003CCurrentMission_003Ek__BackingField;
			obj3 = missionGraph3.MapOverride;
		}
		goto IL_017b;
		IL_011c:
		bool flag3 = obj2 != null;
		bool flag4 = !flag3;
		obj3 = null;
		if (!flag4)
		{
			MissionManager missionManager4 = MissionManager._003CInstance_003Ek__BackingField;
			MissionGraph missionGraph4 = missionManager4._003CCurrentMission_003Ek__BackingField;
			obj3 = missionGraph4.MapTopographyOverride;
		}
		goto IL_017b;
	}
}
