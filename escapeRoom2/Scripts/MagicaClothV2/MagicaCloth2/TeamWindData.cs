using Unity.Collections;

namespace MagicaCloth2
{
	public struct TeamWindData
	{
		public FixedList128Bytes<TeamWindInfo> windZoneList;

		public TeamWindInfo movingWind;

		public int ZoneCount => windZoneList.Length;

		public int IndexOf(int windId)
		{
			int length = windZoneList.Length;
			for (int i = 0; i < length; i++)
			{
				if (windZoneList[i].windId == windId)
				{
					return i;
				}
			}
			return -1;
		}

		public void ClearZoneList()
		{
			windZoneList.Clear();
		}

		public void AddOrReplaceWindZone(TeamWindInfo windInfo, in TeamWindData oldWindData)
		{
			if (windInfo.IsValid())
			{
				int num = oldWindData.IndexOf(windInfo.windId);
				if (num >= 0)
				{
					windInfo.time = oldWindData.windZoneList[num].time;
				}
				windZoneList.AddNoResize(in windInfo);
			}
		}

		public void RemoveWindZone(int windId)
		{
			int num = IndexOf(windId);
			if (num >= 0)
			{
				windZoneList.RemoveAtSwapBack(num);
			}
		}

		public void CopyFrom(in TeamWindData wdata)
		{
			windZoneList = wdata.windZoneList;
			movingWind = wdata.movingWind;
		}
	}
}
