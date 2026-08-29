using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class NormalAlignmentSettings : IValid, IDataValidate, ITransform
	{
		public enum AlignmentMode
		{
			None = 0,
			BoundingBoxCenter = 1,
			Transform = 2
		}

		public AlignmentMode alignmentMode;

		public Transform adjustmentTransform;

		public void DataValidate()
		{
		}

		public bool IsValid()
		{
			AlignmentMode alignmentMode = this.alignmentMode;
			if ((uint)alignmentMode <= 2u)
			{
				return true;
			}
			return false;
		}

		public NormalAlignmentSettings Clone()
		{
			return new NormalAlignmentSettings
			{
				alignmentMode = alignmentMode,
				adjustmentTransform = adjustmentTransform
			};
		}

		public override int GetHashCode()
		{
			int num = 0;
			num += (int)alignmentMode * 105;
			if ((bool)adjustmentTransform)
			{
				num += adjustmentTransform.GetInstanceID();
				num += adjustmentTransform.position.GetHashCode();
			}
			return num;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			if ((bool)adjustmentTransform)
			{
				transformSet.Add(adjustmentTransform);
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if ((bool)adjustmentTransform && replaceDict.ContainsKey(adjustmentTransform.GetInstanceID()))
			{
				adjustmentTransform = replaceDict[adjustmentTransform.GetInstanceID()];
			}
		}
	}
}
