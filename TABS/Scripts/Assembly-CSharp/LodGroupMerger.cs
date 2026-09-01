using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LodGroupMerger : MonoBehaviour
{
	[SerializeField]
	protected LODGroup masterLodGroup;

	public void ConsolidateChildLodGroups()
	{
		if (masterLodGroup == null)
		{
			Debug.LogErrorFormat("No master LODGroup component defined on gameObject {0}.", base.gameObject.name);
			return;
		}
		IEnumerable<LODGroup> enumerable = from x in GetComponentsInChildren<LODGroup>()
			where x.enabled
			select x;
		LOD[] lODs = masterLodGroup.GetLODs();
		foreach (LODGroup item in enumerable)
		{
			if (item == masterLodGroup)
			{
				continue;
			}
			LOD[] lODs2 = item.GetLODs();
			for (int num = 0; num < lODs2.Length && num < lODs.Length; num++)
			{
				Renderer[] renderers = lODs2[num].renderers;
				if (renderers.Length != 0)
				{
					LOD lOD = lODs[num];
					Renderer[] renderers2 = lOD.renderers;
					Renderer[] array = new Renderer[renderers2.Length + renderers.Length];
					Array.Copy(renderers2, array, renderers2.Length);
					Array.Copy(renderers, 0, array, renderers2.Length, renderers.Length);
					lOD.renderers = array;
					lODs[num] = lOD;
				}
			}
			item.enabled = false;
		}
		masterLodGroup.SetLODs(lODs);
	}
}
