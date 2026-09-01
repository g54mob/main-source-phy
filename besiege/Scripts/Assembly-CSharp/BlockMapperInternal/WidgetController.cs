using System.Collections.Generic;
using Selectors;
using UnityEngine;

namespace BlockMapperInternal
{
	public class WidgetController : IWidgetController
	{
		[HideInInspector]
		public List<object> widgetParameters = new List<object>();

		[HideInInspector]
		public List<int> customIndices = new List<int>();

		[HideInInspector]
		public readonly List<ContainerDetails> containers = new List<ContainerDetails>();

		protected readonly string prefabPath;

		protected float lastBottom;

		protected IWidgetContainer widgetContainer;

		private BMWidgetPool.Pool widgetPool;

		protected bool hasPool;

		protected List<ContainerDetails> newContainers = new List<ContainerDetails>();

		public int selectedContainerIndex;

		[HideInInspector]
		public Selector selectedSelector;

		[HideInInspector]
		public ParameterWidget selectedWidget;

		[HideInInspector]
		public int bmControllerIndex;

		public virtual int ContainerCount
		{
			get
			{
				return containers.Count;
			}
		}

		public float StartPosition { get; private set; }

		public float EndPosition { get; private set; }

		public float CurrentEndPosition
		{
			get
			{
				return widgetContainer.TopValue() - ((containers.Count != 0) ? containers[containers.Count - 1].Bottom : 0f);
			}
		}

		public ContainerDetails FirstContainer
		{
			get
			{
				return (containers.Count != 0) ? containers[0] : null;
			}
		}

		public ContainerDetails LastContainer
		{
			get
			{
				return (containers.Count != 0) ? containers[containers.Count - 1] : null;
			}
		}

		public GameObject Widget { get; private set; }

		public float Height
		{
			get
			{
				float num = 0f;
				foreach (ContainerDetails container in containers)
				{
					num += container.Height;
				}
				return num;
			}
		}

		public bool isHidden { get; private set; }

		public bool isSingleActionWidget { get; protected set; }

		public bool isNavigatableWidget { get; protected set; }

		public WidgetController(string path)
		{
			prefabPath = path;
			BMWidgetPool instance = BMWidgetPool.Instance;
			if (instance != null)
			{
				widgetPool = instance.GetPool(path);
				hasPool = true;
			}
			else
			{
				Debug.LogWarning("Couldn't fetch widget pool!");
			}
		}

		public void RegisterToggle(object parameter = null)
		{
			widgetParameters.Add(parameter);
		}

		public void RegisterToggle(object parameter, int index)
		{
			RegisterToggle(parameter);
			customIndices.Add(index);
		}

		public void Hide()
		{
			isHidden = true;
		}

		public void Show()
		{
			isHidden = false;
		}

		public void Remove(object parameter)
		{
			int num = widgetParameters.IndexOf(parameter);
			if (num != -1)
			{
				widgetParameters.RemoveAt(num);
				customIndices.RemoveAt(num);
				ContainerDetails container = containers[num];
				containers.RemoveAt(num);
				RemoveEntry(container);
			}
		}

		private void RemoveEntry(ContainerDetails container)
		{
			if (container.selector != null)
			{
				container.selector.ResetToPool();
			}
			else if (container.widget != null)
			{
				container.widget.ResetToPool();
			}
			if (hasPool)
			{
				widgetPool.Add(container.gameObject);
			}
			else
			{
				Object.Destroy(container.gameObject);
			}
		}

		public void UpdateDisplay(IWidgetContainer targetContainer, float startPosition)
		{
			widgetContainer = targetContainer;
			StartPosition = (lastBottom = widgetContainer.TopValue() - startPosition);
			EndPosition = startPosition;
			foreach (ContainerDetails container in containers)
			{
				container.Top = lastBottom;
				lastBottom = container.Bottom;
				container.Z = widgetContainer.ZValue();
				EndPosition = widgetContainer.TopValue() - container.Bottom;
			}
		}

		public void Display(IWidgetContainer targetContainer, float startPosition)
		{
			widgetContainer = targetContainer;
			EndPosition = startPosition;
			StartPosition = (lastBottom = widgetContainer.TopValue() - startPosition);
			newContainers.Clear();
			CreateContainers();
			foreach (ContainerDetails item in new List<ContainerDetails>(containers))
			{
				if (!newContainers.Contains(item))
				{
					containers.Remove(item);
					RemoveEntry(item);
				}
			}
			foreach (ContainerDetails newContainer in newContainers)
			{
				if (!containers.Contains(newContainer))
				{
					containers.Add(newContainer);
				}
			}
		}

		protected ContainerDetails CreateContainer()
		{
			GameObject gameObject = ((!hasPool) ? Object.Instantiate(Resources.Load<GameObject>(prefabPath)) : widgetPool.Get());
			ContainerDetails component = gameObject.GetComponent<ContainerDetails>();
			UpdateParent(component);
			return component;
		}

		protected virtual void CreateContainers()
		{
			for (int i = 0; i < widgetParameters.Count; i++)
			{
				ContainerDetails container = AddOrGetContainer(i);
				float startExtension = 0f;
				float endExtension = 0f;
				InitContainer(container, i, startExtension, endExtension);
			}
		}

		protected void UpdateParent(ContainerDetails container)
		{
			Transform transform = (widgetContainer as MonoBehaviour).transform;
			Transform transform2 = container.transform;
			transform2.SetParent(transform, false);
			transform2.localScale = Vector3.one;
		}

		protected void InitContainer(ContainerDetails container, int index)
		{
			InitContainer(container, index, 0f, 0f);
		}

		protected void InitContainer(ContainerDetails container, int index, float startExtension, float endExtension)
		{
			float startPosition = (container.Top = lastBottom);
			StartPosition = startPosition;
			container.Z = widgetContainer.ZValue();
			object obj = ((index >= widgetParameters.Count) ? null : widgetParameters[index]);
			int index2 = ((index >= customIndices.Count) ? index : customIndices[index]);
			if (obj != null)
			{
				container.widget = container.GetComponentInChildren<ParameterWidget>();
				container.widget.Init(index2, obj);
			}
			container.ExtendTop(startExtension);
			container.ExtendBottom(endExtension);
			lastBottom = container.Bottom;
			EndPosition = widgetContainer.TopValue() - lastBottom;
		}

		public void ClearContainers()
		{
			while (containers.Count > 0)
			{
				ContainerDetails containerDetails = containers[0];
				containers.RemoveAt(0);
				if (containerDetails != null)
				{
					RemoveEntry(containerDetails);
				}
			}
		}

		public virtual void Clear()
		{
			ClearContainers();
			widgetParameters.Clear();
			customIndices.Clear();
		}

		protected ContainerDetails AddOrGetContainer(int index)
		{
			ContainerDetails containerDetails = CreateContainer();
			containerDetails.name = "Container<" + containerDetails.name + "> #" + index;
			Widget = containerDetails.gameObject;
			newContainers.Add(containerDetails);
			return containerDetails;
		}

		public virtual bool Up()
		{
			return true;
		}

		public virtual bool Down()
		{
			return true;
		}

		public bool Left()
		{
			return true;
		}

		public bool Right()
		{
			return true;
		}

		public void Close()
		{
			selectedContainerIndex = 0;
			for (int i = 0; i < containers.Count; i++)
			{
				if (containers[i].selector == null && containers[i].widget != null && containers[i].widget is OptionCategoryWidget)
				{
					OptionCategoryWidget optionCategoryWidget = containers[i].widget as OptionCategoryWidget;
					optionCategoryWidget.optionsController.Close();
				}
			}
		}
	}
}
