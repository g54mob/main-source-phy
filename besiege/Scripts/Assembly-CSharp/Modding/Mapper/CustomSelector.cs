using InternalModding;
using InternalModding.Mapper;
using Selectors;
using UnityEngine;

namespace Modding.Mapper
{
	public abstract class CustomSelector<T, TMapper> : Selector where TMapper : MCustom<T>
	{
		protected SelectorElements Elements;

		protected SelectorMaterials Materials;

		private CustomSelectorReferences references;

		private ContainerDetails container;

		protected Transform Content
		{
			get
			{
				return references.Content;
			}
		}

		protected Transform Background
		{
			get
			{
				return container.Background;
			}
		}

		protected TMapper CustomMapperType
		{
			get
			{
				return MapperType as TMapper;
			}
		}

		public sealed override void Init()
		{
			references = GetComponent<CustomSelectorReferences>();
			container = base.transform.parent.GetComponent<ContainerDetails>();
			Elements = new SelectorElements(references);
			Materials = new SelectorMaterials(references);
			TMapper customMapperType = CustomMapperType;
			customMapperType.Changed += OnChanged;
			Object.DestroyImmediate(references.Content.gameObject);
			references.Content = new GameObject("Visual").transform;
			references.Content.parent = base.transform;
			references.Content.localScale = Vector3.one;
			references.Content.localPosition = Vector3.zero;
			ModdingUtil.PerformCallback(CreateInterface);
		}

		private void OnChanged(T obj)
		{
			if (!this)
			{
				TMapper customMapperType = CustomMapperType;
				customMapperType.Changed -= OnChanged;
			}
			else
			{
				UpdateVisual();
			}
		}

		protected sealed override void UpdateVisual()
		{
			ModdingUtil.PerformCallback(UpdateInterface);
		}

		protected abstract void CreateInterface();

		protected abstract void UpdateInterface();
	}
}
