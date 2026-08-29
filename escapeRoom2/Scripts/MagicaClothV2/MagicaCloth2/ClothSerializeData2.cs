using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class ClothSerializeData2 : IDataValidate, IValid, ITransform
	{
		[SerializeField]
		public ClothInitSerializeData initData = new ClothInitSerializeData();

		[SerializeField]
		public SelectionData selectionData = new SelectionData();

		[NonSerialized]
		public Dictionary<Transform, VertexAttribute> boneAttributeDict = new Dictionary<Transform, VertexAttribute>();

		[NonSerialized]
		public List<VertexAttribute[]> vertexAttributeList = new List<VertexAttribute[]>();

		public PreBuildSerializeData preBuildData = new PreBuildSerializeData();

		public bool IsValid()
		{
			return true;
		}

		public void DataValidate()
		{
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			initData.GetUsedTransform(transformSet);
			preBuildData.GetUsedTransform(transformSet);
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			initData.ReplaceTransform(replaceDict);
			preBuildData.ReplaceTransform(replaceDict);
		}
	}
}
