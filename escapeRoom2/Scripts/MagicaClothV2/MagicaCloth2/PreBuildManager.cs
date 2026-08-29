using System;
using System.Collections.Generic;
using System.Text;
using Unity.Profiling;
using UnityEngine;

namespace MagicaCloth2
{
	public class PreBuildManager : IManager, IDisposable, IValid
	{
		internal class ShareDeserializationData : IDisposable
		{
			internal string buildId;

			internal ResultCode result;

			internal int referenceCount;

			internal List<RenderSetupData> renderSetupDataList = new List<RenderSetupData>();

			internal VirtualMesh proxyMesh;

			internal List<VirtualMesh> renderMeshList = new List<VirtualMesh>();

			internal DistanceConstraint.ConstraintData distanceConstraintData;

			internal TriangleBendingConstraint.ConstraintData bendingConstraintData;

			internal InertiaConstraint.ConstraintData inertiaConstraintData;

			public int RenderMeshCount => renderMeshList?.Count ?? 0;

			public void Dispose()
			{
				foreach (RenderSetupData renderSetupData in renderSetupDataList)
				{
					if (renderSetupData != null)
					{
						renderSetupData.isManaged = false;
						renderSetupData.Dispose();
					}
				}
				renderSetupDataList.Clear();
				if (proxyMesh != null)
				{
					proxyMesh.isManaged = false;
					proxyMesh.Dispose();
					proxyMesh = null;
				}
				foreach (VirtualMesh renderMesh in renderMeshList)
				{
					if (renderMesh != null)
					{
						renderMesh.isManaged = false;
						renderMesh.Dispose();
					}
				}
				renderMeshList.Clear();
				distanceConstraintData = null;
				bendingConstraintData = null;
				inertiaConstraintData = null;
				buildId = string.Empty;
				result.Clear();
				referenceCount = 0;
			}

			public void Deserialize(SharePreBuildData sharePreBuilddata)
			{
				result.SetProcess();
				try
				{
					ResultCode src = sharePreBuilddata.DataValidate();
					if (src.IsFaild())
					{
						result.Merge(src);
						throw new MagicaClothProcessingException();
					}
					foreach (RenderSetupData.ShareSerializationData renderSetupData in sharePreBuilddata.renderSetupDataList)
					{
						renderSetupDataList.Add(RenderSetupData.ShareDeserialize(renderSetupData));
					}
					proxyMesh = VirtualMesh.ShareDeserialize(sharePreBuilddata.proxyMesh);
					foreach (VirtualMesh.ShareSerializationData renderMesh in sharePreBuilddata.renderMeshList)
					{
						renderMeshList.Add(VirtualMesh.ShareDeserialize(renderMesh));
					}
					distanceConstraintData = sharePreBuilddata.distanceConstraintData;
					bendingConstraintData = sharePreBuilddata.bendingConstraintData;
					inertiaConstraintData = sharePreBuilddata.inertiaConstraintData;
					result.SetSuccess();
				}
				catch (MagicaClothProcessingException)
				{
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					result.SetError(Define.Result.Deserialization_Exception);
				}
			}

			public VirtualMeshContainer GetProxyMeshContainer()
			{
				return new VirtualMeshContainer
				{
					shareVirtualMesh = proxyMesh,
					uniqueData = null
				};
			}

			public VirtualMeshContainer GetRenderMeshContainer(int index)
			{
				if (index >= RenderMeshCount)
				{
					return null;
				}
				return new VirtualMeshContainer
				{
					shareVirtualMesh = renderMeshList[index],
					uniqueData = null
				};
			}
		}

		private Dictionary<SharePreBuildData, ShareDeserializationData> deserializationDict = new Dictionary<SharePreBuildData, ShareDeserializationData>();

		private bool isValid;

		private static readonly ProfilerMarker deserializationProfiler = new ProfilerMarker("PreBuild.Deserialization");

		public void Dispose()
		{
			foreach (KeyValuePair<SharePreBuildData, ShareDeserializationData> item in deserializationDict)
			{
				item.Value.Dispose();
			}
			deserializationDict.Clear();
			isValid = false;
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== PreBuild Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("PreBuild Manager. Invalid.");
			}
			else
			{
				int count = deserializationDict.Count;
				stringBuilder.AppendLine($"Count:{count}");
				foreach (KeyValuePair<SharePreBuildData, ShareDeserializationData> item in deserializationDict)
				{
					stringBuilder.AppendLine($"[{item.Key.buildId}] refcnt:{item.Value.referenceCount}, result:{item.Value.result.GetResultString()}, proxyMesh:{item.Value.proxyMesh != null}");
				}
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}

		internal ShareDeserializationData RegisterPreBuildData(SharePreBuildData sdata, bool referenceIncrement)
		{
			if (!isValid)
			{
				return null;
			}
			if (sdata == null)
			{
				return null;
			}
			if (!deserializationDict.ContainsKey(sdata))
			{
				ShareDeserializationData shareDeserializationData = new ShareDeserializationData();
				shareDeserializationData.buildId = sdata.buildId;
				shareDeserializationData.Deserialize(sdata);
				deserializationDict.Add(sdata, shareDeserializationData);
			}
			ShareDeserializationData shareDeserializationData2 = deserializationDict[sdata];
			if (referenceIncrement)
			{
				shareDeserializationData2.referenceCount++;
			}
			return shareDeserializationData2;
		}

		internal ShareDeserializationData GetPreBuildData(SharePreBuildData sdata)
		{
			if (sdata == null)
			{
				return null;
			}
			if (deserializationDict.ContainsKey(sdata))
			{
				return deserializationDict[sdata];
			}
			return null;
		}

		internal void UnregisterPreBuildData(SharePreBuildData sdata)
		{
			if (isValid && sdata != null && deserializationDict.ContainsKey(sdata))
			{
				deserializationDict[sdata].referenceCount--;
			}
		}

		internal void UnloadUnusedData()
		{
			List<SharePreBuildData> list = new List<SharePreBuildData>();
			foreach (KeyValuePair<SharePreBuildData, ShareDeserializationData> item in deserializationDict)
			{
				if (item.Value.referenceCount <= 0)
				{
					list.Add(item.Key);
				}
			}
			foreach (SharePreBuildData item2 in list)
			{
				deserializationDict[item2].Dispose();
				deserializationDict.Remove(item2);
			}
			list.Clear();
		}
	}
}
