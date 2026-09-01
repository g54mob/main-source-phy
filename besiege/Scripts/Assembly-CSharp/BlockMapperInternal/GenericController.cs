using System.Collections.Generic;
using Selectors;
using UnityEngine;

namespace BlockMapperInternal
{
	public class GenericController<T> : WidgetController where T : MapperType
	{
		public List<T> mapperTypes = new List<T>();

		public GenericController(string prefabPath)
			: base(prefabPath)
		{
		}

		public void RegisterToggle(T mapperType)
		{
			mapperTypes.Add(mapperType);
		}

		public void Remove(T mapperType)
		{
			int num = mapperTypes.IndexOf(mapperType);
			if (num != -1)
			{
				mapperTypes.RemoveAt(num);
				ContainerDetails containerDetails = containers[num];
				containers.RemoveAt(num);
				Object.DestroyImmediate(containerDetails.gameObject);
			}
		}

		protected override void CreateContainers()
		{
			for (int i = 0; i < mapperTypes.Count; i++)
			{
				T mapperType = mapperTypes[i];
				if (mapperType.DisplayInMapper)
				{
					ContainerDetails container = AddOrGetContainer(i, mapperType);
					InitContainer(container, i);
				}
			}
		}

		protected ContainerDetails AddOrGetContainer(int index, T mapperType)
		{
			ContainerDetails containerDetails = null;
			foreach (ContainerDetails container in containers)
			{
				if (container.selector.MapperType == mapperType)
				{
					containerDetails = container;
					break;
				}
			}
			if (containerDetails == null)
			{
				containerDetails = CreateContainer();
			}
			Selector selector = (containerDetails.selector = containerDetails.GetComponentInChildren<Selector>());
			selector.MapperType = mapperType;
			selector.Init();
			containerDetails.name = "Container<" + typeof(T).Name + "> #" + index;
			newContainers.Add(containerDetails);
			return containerDetails;
		}

		public override void Clear()
		{
			base.Clear();
			mapperTypes.Clear();
		}
	}
}
