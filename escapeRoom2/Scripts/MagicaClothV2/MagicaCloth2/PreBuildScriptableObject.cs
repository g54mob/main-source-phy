using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[CreateAssetMenu(fileName = "Data", menuName = "MagicaCloth2/PreBuildScriptableObject")]
	public class PreBuildScriptableObject : ScriptableObject
	{
		public List<SharePreBuildData> sharePreBuildDataList = new List<SharePreBuildData>();

		public bool HasPreBuildData(string buildId)
		{
			return GetPreBuildData(buildId) != null;
		}

		public SharePreBuildData GetPreBuildData(string buildId)
		{
			foreach (SharePreBuildData sharePreBuildData in sharePreBuildDataList)
			{
				if (sharePreBuildData.CheckBuildId(buildId))
				{
					return sharePreBuildData;
				}
			}
			return null;
		}

		public void AddPreBuildData(SharePreBuildData sdata)
		{
			int num = sharePreBuildDataList.FindIndex((SharePreBuildData x) => x.buildId == sdata.buildId);
			if (num >= 0)
			{
				sharePreBuildDataList[num] = sdata;
			}
			else
			{
				sharePreBuildDataList.Add(sdata);
			}
		}

		public void Warmup()
		{
			if (Application.isPlaying)
			{
				sharePreBuildDataList.ForEach(delegate(SharePreBuildData sdata)
				{
					MagicaManager.PreBuild.RegisterPreBuildData(sdata, referenceIncrement: false);
				});
			}
		}
	}
}
