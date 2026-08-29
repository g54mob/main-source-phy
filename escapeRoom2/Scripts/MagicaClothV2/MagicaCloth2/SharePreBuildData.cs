using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class SharePreBuildData
	{
		public int version;

		public string buildId;

		public ResultCode buildResult;

		public Vector3 buildScale;

		public List<RenderSetupData.ShareSerializationData> renderSetupDataList = new List<RenderSetupData.ShareSerializationData>();

		public VirtualMesh.ShareSerializationData proxyMesh;

		public List<VirtualMesh.ShareSerializationData> renderMeshList = new List<VirtualMesh.ShareSerializationData>();

		public DistanceConstraint.ConstraintData distanceConstraintData;

		public TriangleBendingConstraint.ConstraintData bendingConstraintData;

		public InertiaConstraint.ConstraintData inertiaConstraintData;

		public ResultCode DataValidate()
		{
			if (version != 2)
			{
				return new ResultCode(Define.Result.PreBuildData_VersionMismatch);
			}
			if (buildScale.x < 1E-08f)
			{
				return new ResultCode(Define.Result.PreBuildData_InvalidScale);
			}
			if (buildResult.IsFaild())
			{
				return buildResult;
			}
			return ResultCode.Success;
		}

		public bool CheckBuildId(string buildId)
		{
			if (string.IsNullOrEmpty(buildId))
			{
				return false;
			}
			return this.buildId == buildId;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendLine("<<<<< PreBuildData >>>>>");
			stringBuilder.AppendLine($"Version:{version}");
			stringBuilder.AppendLine("BuildID:" + buildId);
			stringBuilder.AppendLine("BuildResult:" + buildResult.GetResultString());
			stringBuilder.AppendLine($"BuildScale:{buildScale}");
			stringBuilder.AppendLine(proxyMesh.ToString());
			stringBuilder.AppendLine($"renderMeshList:{renderMeshList.Count}");
			stringBuilder.AppendLine($"renderSetupDataList:{renderSetupDataList.Count}");
			return stringBuilder.ToString();
		}
	}
}
