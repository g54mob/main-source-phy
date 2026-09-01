using System;
using System.Collections.Generic;
using Koenigz.PerfectCulling.IO;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	[PreferBinarySerialization]
	public class PerfectCullingVolumeBakeData : PerfectCullingBakeData
	{
		[Serializable]
		public struct VisibilitySet
		{
			public byte[] compressed;

			public ushort len;
		}

		[Serializable]
		public struct RawData
		{
			public ushort[] uncompressed;

			public uint mergeBitmask;
		}

		public Vector3 cellCount;

		public Vector3 cellSize;

		public Quaternion orientation;

		[HideInInspector]
		public VisibilitySet[] data;

		[HideInInspector]
		public RawData[] rawData;

		public int numberOfGroups;

		private const int MaxValue = 15;

		private const int HeaderBitSize = 2;

		private static uint[] BITS;

		private static BitStreamReader m_bitStreamReader;

		public void SetVolumeBakeData(PerfectCullingVolume volume)
		{
		}

		public override void PrepareForBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public override void SetRawData(int index, ushort[] indices, uint mergeBitmask, bool validateData = true)
		{
		}

		private void SetDataCompressed(int index, ushort[] indices, BitStreamReader streamReader, BitStreamWriter streamWriter)
		{
		}

		public override void CompleteBake()
		{
		}

		public void MergeDownsample(Vector3Int mergeAxes)
		{
		}

		public override void SampleAtIndex(int index, List<ushort> indices)
		{
		}

		private void SampleAtIndex(int index, List<ushort> indices, BitStreamReader bitStreamReader)
		{
		}

		public override int SearchIndexForClosestNonEmptyCell(int index)
		{
			return 0;
		}

		public override void DrawInspectorGUI()
		{
		}
	}
}
