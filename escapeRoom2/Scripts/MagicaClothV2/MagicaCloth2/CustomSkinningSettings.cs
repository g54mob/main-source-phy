using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public class CustomSkinningSettings : IValid, IDataValidate, ITransform
	{
		public bool enable;

		public List<Transform> skinningBones = new List<Transform>();

		public void DataValidate()
		{
		}

		public bool IsValid()
		{
			if (!enable)
			{
				return false;
			}
			if (skinningBones.Count == 0)
			{
				return false;
			}
			if (!skinningBones.Any((Transform n) => n != null))
			{
				return false;
			}
			return true;
		}

		public CustomSkinningSettings Clone()
		{
			return new CustomSkinningSettings
			{
				enable = enable,
				skinningBones = new List<Transform>(skinningBones)
			};
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			foreach (Transform skinningBone in skinningBones)
			{
				if ((bool)skinningBone)
				{
					transformSet.Add(skinningBone);
				}
			}
		}

		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			for (int i = 0; i < skinningBones.Count; i++)
			{
				Transform transform = skinningBones[i];
				if ((bool)transform && replaceDict.ContainsKey(transform.GetInstanceID()))
				{
					skinningBones[i] = replaceDict[transform.GetInstanceID()];
				}
			}
		}
	}
}
