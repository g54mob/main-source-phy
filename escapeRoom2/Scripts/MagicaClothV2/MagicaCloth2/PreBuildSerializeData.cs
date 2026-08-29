using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class PreBuildSerializeData : ITransform
	{
		public bool enabled;

		public string buildId;

		public PreBuildScriptableObject preBuildScriptableObject;

		public UniquePreBuildData uniquePreBuildData;

		public bool UsePreBuild()
		{
			return enabled;
		}

		public ResultCode DataValidate()
		{
			if (uniquePreBuildData == null)
			{
				return new ResultCode(Define.Result.PreBuildData_Empty);
			}
			SharePreBuildData sharePreBuildData = GetSharePreBuildData();
			if (sharePreBuildData == null)
			{
				return new ResultCode(Define.Result.PreBuildData_Empty);
			}
			ResultCode result = sharePreBuildData.DataValidate();
			if (result.IsFaild())
			{
				return result;
			}
			result = uniquePreBuildData.DataValidate();
			if (result.IsFaild())
			{
				return result;
			}
			return ResultCode.Success;
		}

		public SharePreBuildData GetSharePreBuildData()
		{
			if (preBuildScriptableObject == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(buildId))
			{
				return null;
			}
			return preBuildScriptableObject.GetPreBuildData(buildId);
		}

		public static string GenerateBuildID()
		{
			return Guid.NewGuid().ToString().Substring(0, 8);
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			uniquePreBuildData.GetUsedTransform(transformSet);
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			uniquePreBuildData.ReplaceTransform(replaceDict);
		}
	}
}
