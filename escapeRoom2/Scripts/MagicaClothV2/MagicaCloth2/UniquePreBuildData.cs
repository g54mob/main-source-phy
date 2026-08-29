using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class UniquePreBuildData : ITransform
	{
		public int version;

		public ResultCode buildResult;

		public List<RenderSetupData.UniqueSerializationData> renderSetupDataList = new List<RenderSetupData.UniqueSerializationData>();

		public VirtualMesh.UniqueSerializationData proxyMesh;

		public List<VirtualMesh.UniqueSerializationData> renderMeshList = new List<VirtualMesh.UniqueSerializationData>();

		public ResultCode DataValidate()
		{
			if (version != 2)
			{
				return new ResultCode(Define.Result.PreBuildData_VersionMismatch);
			}
			if (buildResult.IsFaild())
			{
				return buildResult;
			}
			return ResultCode.Success;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			renderSetupDataList.ForEach(delegate(RenderSetupData.UniqueSerializationData x)
			{
				x?.GetUsedTransform(transformSet);
			});
			proxyMesh?.GetUsedTransform(transformSet);
			renderMeshList.ForEach(delegate(VirtualMesh.UniqueSerializationData x)
			{
				x?.GetUsedTransform(transformSet);
			});
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			renderSetupDataList.ForEach(delegate(RenderSetupData.UniqueSerializationData x)
			{
				x?.ReplaceTransform(replaceDict);
			});
			proxyMesh?.ReplaceTransform(replaceDict);
			renderMeshList.ForEach(delegate(VirtualMesh.UniqueSerializationData x)
			{
				x?.ReplaceTransform(replaceDict);
			});
		}
	}
}
