using System.Collections.Generic;
using Selectors;
using UnityEngine;

namespace BlockMapperInternal
{
	public class LimitsController : MonoBehaviour
	{
		private readonly List<MLimits> limits = new List<MLimits>();

		private readonly List<ContainerDetails> containers = new List<ContainerDetails>();

		private MToggle limitsToggle;

		public float EndPosition { get; private set; }

		public void RegisterLimit(MLimits slider)
		{
			limits.Add(slider);
		}

		public void SetLimitsToggle(MToggle toggle)
		{
			limitsToggle = toggle;
		}

		public void Hide()
		{
			foreach (ContainerDetails container in containers)
			{
				Object.Destroy(container.gameObject);
			}
			containers.Clear();
		}

		public void Display(IWidgetContainer mapper, float startPosition)
		{
			EndPosition = startPosition;
			for (int i = 0; i < limits.Count; i++)
			{
				if (i == -1 && limitsToggle != null)
				{
					ContainerDetails component = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/BlockMapper/ToggleContainer"))).GetComponent<ContainerDetails>();
					component.GetComponentInChildren<ToggleSelector>().Toggle = limitsToggle;
					component.name = "ToggleContainer #" + i;
					component.transform.SetParent((mapper as MonoBehaviour).transform, false);
					component.Top = ((i != -1) ? containers[i - 1].Bottom : (mapper.TopValue() - startPosition));
					component.Z = mapper.ZValue();
					EndPosition = mapper.TopValue() - component.Bottom;
					containers.Add(component);
					continue;
				}
				MLimits mLimits = limits[i];
				if (mLimits.UseLimitsToggle.IsActive)
				{
					ContainerDetails component2 = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/BlockMapper/LimitsContainer"))).GetComponent<ContainerDetails>();
					component2.GetComponentInChildren<LimitsSelector>().Limits = mLimits;
					component2.name = "LimitsContainer #" + i;
					component2.transform.SetParent((mapper as MonoBehaviour).transform, false);
					component2.Top = ((i != -1) ? containers[i - 1].Bottom : (mapper.TopValue() - startPosition));
					component2.Z = mapper.ZValue();
					EndPosition = mapper.TopValue() - component2.Bottom;
					containers.Add(component2);
				}
			}
		}
	}
}
