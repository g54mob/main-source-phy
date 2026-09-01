using System;
using System.Collections.Generic;
using BlockMapperInternal;
using Modding.Mapper;
using Selectors;
using UnityEngine;

namespace InternalModding.Mapper
{
	public class CustomController : WidgetController
	{
		private List<MapperType> mapperTypes = new List<MapperType>();

		public CustomController(string path)
			: base(path)
		{
			hasPool = false;
		}

		public void RegisterToggle(MapperType t)
		{
			mapperTypes.Add(t);
		}

		public void Remove(MapperType t)
		{
			int num = mapperTypes.IndexOf(t);
			if (num != -1)
			{
				mapperTypes.RemoveAt(num);
				ContainerDetails containerDetails = containers[num];
				containers.RemoveAt(num);
				UnityEngine.Object.DestroyImmediate(containerDetails.gameObject);
			}
		}

		protected override void CreateContainers()
		{
			for (int i = 0; i < mapperTypes.Count; i++)
			{
				MapperType mapperType = mapperTypes[i];
				if (mapperType.DisplayInMapper)
				{
					ContainerDetails container = AddOrGetContainer(i, mapperType);
					InitContainer(container, i);
				}
			}
		}

		protected ContainerDetails AddOrGetContainer(int index, MapperType mapperType)
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
			Type selectorType = CustomMapperTypes.GetSelectorType(mapperType);
			GameObject gameObject = containerDetails.transform.FindChild("Selector").gameObject;
			Selector selector = (Selector)gameObject.GetComponent(selectorType);
			if (selector == null)
			{
				selector = (Selector)gameObject.AddComponent(selectorType);
			}
			containerDetails.selector = selector;
			selector.MapperType = mapperType;
			selector.Init();
			containerDetails.name = "Container<" + mapperType.GetType().Name + "> #" + index;
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
