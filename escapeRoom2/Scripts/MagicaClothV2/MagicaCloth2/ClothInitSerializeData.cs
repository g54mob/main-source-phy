using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class ClothInitSerializeData : ITransform
	{
		public const int InitDataVersion = 2;

		public int initVersion;

		public int localHash;

		public int globalHash;

		public ClothProcess.ClothType clothType;

		public TransformRecordSerializeData clothTransformRecord;

		public TransformRecordSerializeData normalAdjustmentTransformRecord;

		public List<TransformRecordSerializeData> customSkinningBoneRecords;

		public List<RenderSetupSerializeData> clothSetupDataList;

		public bool HasData()
		{
			if (initVersion == 0)
			{
				return false;
			}
			if (localHash == 0 || globalHash == 0)
			{
				return false;
			}
			return true;
		}

		public void Clear()
		{
			initVersion = 0;
			localHash = 0;
			globalHash = 0;
			clothType = ClothProcess.ClothType.MeshCloth;
			clothTransformRecord = new TransformRecordSerializeData();
			normalAdjustmentTransformRecord = new TransformRecordSerializeData();
			customSkinningBoneRecords = new List<TransformRecordSerializeData>();
			clothSetupDataList = new List<RenderSetupSerializeData>();
		}

		public ResultCode DataValidate(ClothProcess cprocess)
		{
			if (localHash == 0 || globalHash == 0)
			{
				return new ResultCode(Define.Result.InitSerializeData_InvalidHash);
			}
			if (initVersion == 0)
			{
				return new ResultCode(Define.Result.InitSerializeData_InvalidVersion);
			}
			if (clothSetupDataList == null || clothSetupDataList.Count == 0)
			{
				return new ResultCode(Define.Result.InitSerializeData_InvalidSetupData);
			}
			ClothSerializeData serializeData = cprocess.cloth.SerializeData;
			if (clothType != serializeData.clothType)
			{
				return new ResultCode(Define.Result.InitSerializeData_ClothTypeMismatch);
			}
			if (clothType == ClothProcess.ClothType.MeshCloth)
			{
				int count = serializeData.sourceRenderers.Count;
				if (clothSetupDataList.Count != count)
				{
					return new ResultCode(Define.Result.InitSerializeData_SetupCountMismatch);
				}
				for (int i = 0; i < count; i++)
				{
					if (!clothSetupDataList[i].DataValidateMeshCloth(serializeData.sourceRenderers[i]))
					{
						return new ResultCode(Define.Result.InitSerializeData_MeshClothSetupValidationError);
					}
				}
			}
			else if (clothType == ClothProcess.ClothType.BoneCloth)
			{
				if (clothSetupDataList.Count != 1)
				{
					return new ResultCode(Define.Result.InitSerializeData_SetupCountMismatch);
				}
				if (!clothSetupDataList[0].DataValidateBoneCloth(serializeData, RenderSetupData.SetupType.BoneCloth))
				{
					return new ResultCode(Define.Result.InitSerializeData_BoneClothSetupValidationError);
				}
			}
			else if (clothType == ClothProcess.ClothType.BoneSpring)
			{
				if (clothSetupDataList.Count != 1)
				{
					return new ResultCode(Define.Result.InitSerializeData_SetupCountMismatch);
				}
				if (!clothSetupDataList[0].DataValidateBoneCloth(serializeData, RenderSetupData.SetupType.BoneSpring))
				{
					return new ResultCode(Define.Result.InitSerializeData_BoneSpringSetupValidationError);
				}
			}
			if (serializeData.customSkinningSetting.skinningBones.Count != customSkinningBoneRecords.Count)
			{
				return new ResultCode(Define.Result.InitSerializeData_CustomSkinningBoneCountMismatch);
			}
			if (initVersion <= 1 && clothType == ClothProcess.ClothType.MeshCloth)
			{
				for (int j = 0; j < serializeData.sourceRenderers.Count; j++)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = serializeData.sourceRenderers[j] as SkinnedMeshRenderer;
					if ((bool)skinnedMeshRenderer && (bool)skinnedMeshRenderer.sharedMesh && skinnedMeshRenderer.sharedMesh.name.Contains("(Clone)"))
					{
						return new ResultCode(Define.Result.InitSerializeData_InvalidCloneMesh);
					}
				}
			}
			return ResultCode.Success;
		}

		public bool Serialize(ClothSerializeData sdata, TransformRecord clothTransformRecord, TransformRecord normalAdjustmentTransformRecord, List<RenderSetupData> setupList)
		{
			initVersion = 2;
			clothType = sdata.clothType;
			this.clothTransformRecord = new TransformRecordSerializeData();
			this.clothTransformRecord.Serialize(clothTransformRecord);
			this.normalAdjustmentTransformRecord = new TransformRecordSerializeData();
			this.normalAdjustmentTransformRecord.Serialize(normalAdjustmentTransformRecord);
			customSkinningBoneRecords = new List<TransformRecordSerializeData>();
			int count = sdata.customSkinningSetting.skinningBones.Count;
			for (int i = 0; i < count; i++)
			{
				TransformRecord tr = new TransformRecord(sdata.customSkinningSetting.skinningBones[i], read: true);
				TransformRecordSerializeData transformRecordSerializeData = new TransformRecordSerializeData();
				transformRecordSerializeData.Serialize(tr);
				customSkinningBoneRecords.Add(transformRecordSerializeData);
			}
			clothSetupDataList = new List<RenderSetupSerializeData>();
			if (setupList != null && setupList.Count > 0)
			{
				foreach (RenderSetupData setup in setupList)
				{
					RenderSetupSerializeData renderSetupSerializeData = new RenderSetupSerializeData();
					renderSetupSerializeData.Serialize(setup);
					clothSetupDataList.Add(renderSetupSerializeData);
				}
			}
			localHash = GetLocalHash();
			globalHash = GetGlobalHash();
			return true;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			clothTransformRecord?.GetUsedTransform(transformSet);
			normalAdjustmentTransformRecord?.GetUsedTransform(transformSet);
			customSkinningBoneRecords?.ForEach(delegate(TransformRecordSerializeData x)
			{
				x.GetUsedTransform(transformSet);
			});
			clothSetupDataList?.ForEach(delegate(RenderSetupSerializeData x)
			{
				x.GetUsedTransform(transformSet);
			});
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			clothTransformRecord?.ReplaceTransform(replaceDict);
			normalAdjustmentTransformRecord?.ReplaceTransform(replaceDict);
			customSkinningBoneRecords?.ForEach(delegate(TransformRecordSerializeData x)
			{
				x.ReplaceTransform(replaceDict);
			});
			clothSetupDataList?.ForEach(delegate(RenderSetupSerializeData x)
			{
				x.ReplaceTransform(replaceDict);
			});
		}

		private int GetLocalHash()
		{
			int hash = 0;
			hash += initVersion * 9876;
			hash += (int)clothType * 5656;
			hash += clothTransformRecord?.GetLocalHash() ?? 0;
			hash += normalAdjustmentTransformRecord?.GetLocalHash() ?? 0;
			customSkinningBoneRecords?.ForEach(delegate(TransformRecordSerializeData x)
			{
				hash += x?.GetLocalHash() ?? 0;
			});
			clothSetupDataList?.ForEach(delegate(RenderSetupSerializeData x)
			{
				hash += x?.GetLocalHash() ?? 0;
			});
			return hash;
		}

		private int GetGlobalHash()
		{
			int hash = 0;
			hash += clothTransformRecord?.GetGlobalHash() ?? 0;
			hash += normalAdjustmentTransformRecord?.GetGlobalHash() ?? 0;
			customSkinningBoneRecords?.ForEach(delegate(TransformRecordSerializeData x)
			{
				hash += x?.GetGlobalHash() ?? 0;
			});
			clothSetupDataList?.ForEach(delegate(RenderSetupSerializeData x)
			{
				hash += x?.GetGlobalHash() ?? 0;
			});
			return hash;
		}
	}
}
