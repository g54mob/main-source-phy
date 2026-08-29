using System;
using GLTFast.Jobs;
using Unity.Jobs;
using UnityEngine;

[Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__1854482118041315048
{
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobParallelForExtensions.EarlyJobInit<CreateIndicesInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<CreateIndicesInt32FlippedJob>();
			IJobParallelForExtensions.EarlyJobInit<CreateIndicesForTriangleStripJob>();
			IJobParallelForExtensions.EarlyJobInit<CreateIndicesForTriangleFanJob>();
			IJobParallelForExtensions.EarlyJobInit<RecalculateIndicesForTriangleStripJob>();
			IJobParallelForExtensions.EarlyJobInit<RecalculateIndicesForTriangleFanJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt8ToInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt8ToInt32FlippedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt16ToInt32FlippedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt16ToInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt32ToInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<ConvertIndicesUInt32ToInt32FlippedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsUInt8ToFloatInterleavedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertUVsUInt8ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsUInt16ToFloatInterleavedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertUVsUInt16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsInt16ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsInt16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsInt8ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsInt8ToFloatInterleavedNormalizedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertColorsRGBFloatToRGBAFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertColorsRgbUInt8ToRGBAFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertColorsRgbUInt16ToRGBAFloatJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertColorsRgbaUInt16ToRGBAFloatJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertColorsRGBAFloatToRGBAFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertColorsRgbaUInt8ToRGBAFloatJob>();
			IJobExtensions.EarlyJobInit<MemCopyJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertVector3FloatToFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertRotationsFloatToFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertRotationsInt16ToFloatJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertRotationsInt8ToFloatJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertUVsFloatToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertVector3FloatToFloatInterleavedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertVector3SparseJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertTangentsFloatToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertBoneWeightsFloatToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertBoneWeightsUInt8ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertBoneWeightsUInt16ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertTangentsInt16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertTangentsInt8ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsUInt16ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsUInt16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsInt16ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertVector3Int16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertNormalsInt16ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsInt8ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertVector3Int8ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertNormalsInt8ToFloatInterleavedNormalizedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsUInt8ToFloatInterleavedJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<ConvertPositionsUInt8ToFloatInterleavedNormalizedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertBoneJointsUInt8ToUInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<ConvertBoneJointsUInt16ToUInt32Job>();
			IJobParallelForExtensions.EarlyJobInit<SortAndNormalizeBoneWeightsJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertMatricesJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertScalarInt8ToFloatNormalizedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertScalarUInt8ToFloatNormalizedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertScalarInt16ToFloatNormalizedJob>();
			IJobParallelForExtensions.EarlyJobInit<ConvertScalarUInt16ToFloatNormalizedJob>();
		}
		catch (Exception ex)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex);
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		CreateJobReflectionData();
	}
}
