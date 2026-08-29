using System;
using System.Collections.Generic;
using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using Meta.XR.ImmersiveDebugger.Utils;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal class InspectorPanel : DebugPanel, IDebugUIPanel
	{
		public const string DefaultCategoryName = "Uncategorized";

		private ScrollView _scrollView;

		private ScrollView _categoryScrollView;

		private readonly Dictionary<string, Dictionary<Type, Dictionary<InstanceHandle, Inspector>>> _registries = new Dictionary<string, Dictionary<Type, Dictionary<InstanceHandle, Inspector>>>();

		private readonly List<CategoryButton> _categories = new List<CategoryButton>();

		private CategoryButton _selectedCategory;

		private Background _categoryBackground;

		private Vector3 _currentPosition;

		private Vector3 _targetPosition;

		private readonly float _lerpSpeed = 10f;

		private bool _lerpCompleted = true;

		private ImageStyle _categoryBackgroundImageStyle;

		private DebugInterface _debugInterface;

		private Flex Flex => _scrollView.Flex;

		private Flex CategoryFlex => _categoryScrollView.Flex;

		public ImageStyle CategoryBackgroundStyle
		{
			set
			{
				_categoryBackground.Sprite = value.sprite;
				_categoryBackground.Color = value.color;
				_categoryBackground.PixelDensityMultiplier = value.pixelDensityMultiplier;
			}
		}

		protected override void Setup(Controller owner)
		{
			base.Setup(owner);
			_debugInterface = UnityEngine.Object.FindObjectOfType<DebugInterface>();
			Flex flex = Append<Flex>("div");
			flex.LayoutStyle = Style.Load<LayoutStyle>("InspectorDivFlex");
			Flex flex2 = flex.Append<Flex>("categories_div");
			flex2.LayoutStyle = Style.Load<LayoutStyle>("CategoriesDiv");
			_categoryBackground = flex2.Append<Background>("background");
			_categoryBackground.LayoutStyle = Style.Load<LayoutStyle>("CategoriesDivBackground");
			_categoryBackgroundImageStyle = Style.Load<ImageStyle>("CategoriesDivBackground");
			CategoryBackgroundStyle = _categoryBackgroundImageStyle;
			_categoryScrollView = flex2.Append<ScrollView>("categories");
			_categoryScrollView.LayoutStyle = Style.Load<LayoutStyle>("CategoriesScrollView");
			CategoryFlex.LayoutStyle = Style.Load<LayoutStyle>("InspectorCategoryFlex");
			_scrollView = flex.Append<ScrollView>("main");
			_scrollView.LayoutStyle = Style.Load<LayoutStyle>("PanelScrollView");
			Flex.LayoutStyle = Style.Load<LayoutStyle>("InspectorMainFlex");
		}

		protected override void OnTransparencyChanged()
		{
			base.OnTransparencyChanged();
			_categoryBackground.Color = (base.Transparent ? _categoryBackgroundImageStyle.colorOff : _categoryBackgroundImageStyle.color);
		}

		public IInspector RegisterInspector(InstanceHandle instanceHandle, string category)
		{
			Dictionary<InstanceHandle, Inspector> registry;
			Inspector inspector = GetInspectorInternal(instanceHandle, category, createRegistries: true, out registry);
			if (inspector == null)
			{
				CategoryButton categoryButton = GetCategoryButton(category, create: true);
				categoryButton.Counter++;
				float progress = _scrollView.Progress;
				UnityEngine.Object instance = instanceHandle.Instance;
				string childName = ((instance != null) ? instance.name : instanceHandle.Type.Name);
				inspector = Flex.Append<Inspector>(childName);
				inspector.LayoutStyle = Style.Load<LayoutStyle>("Inspector");
				string title = ((instance != null) ? (instance.name + " - " + instanceHandle.Type.Name) : (instanceHandle.Type.Name ?? ""));
				inspector.Title = title;
				registry.Add(instanceHandle, inspector);
				_scrollView.Progress = progress;
				if (_selectedCategory != categoryButton)
				{
					Flex.Forget(inspector);
				}
			}
			return inspector;
		}

		public void UnregisterInspector(InstanceHandle instanceHandle, string category, bool allCategories)
		{
			if (allCategories)
			{
				foreach (KeyValuePair<string, Dictionary<Type, Dictionary<InstanceHandle, Inspector>>> registry2 in _registries)
				{
					registry2.Deconstruct(out var _, out var value);
					if (value.TryGetValue(instanceHandle.Type, out var value2) && value2.TryGetValue(instanceHandle, out var _))
					{
						value2.Remove(instanceHandle);
					}
				}
				return;
			}
			Dictionary<InstanceHandle, Inspector> registry;
			Inspector inspectorInternal = GetInspectorInternal(instanceHandle, category, createRegistries: false, out registry);
			if (inspectorInternal != null)
			{
				float progress = _scrollView.Progress;
				registry?.Remove(instanceHandle);
				Flex.Remove(inspectorInternal, destroy: true);
				CategoryButton categoryButton = GetCategoryButton(category);
				if (categoryButton != null)
				{
					categoryButton.Counter--;
				}
				_scrollView.Progress = progress;
			}
		}

		public IInspector GetInspector(InstanceHandle instanceHandle, string category)
		{
			Dictionary<InstanceHandle, Inspector> registry;
			return GetInspectorInternal(instanceHandle, category, createRegistries: false, out registry);
		}

		public Inspector GetInspectorInternal(InstanceHandle instanceHandle, string category, bool createRegistries, out Dictionary<InstanceHandle, Inspector> registry)
		{
			if (category == null)
			{
				category = string.Empty;
			}
			Inspector value = null;
			if (!_registries.TryGetValue(category, out var value2))
			{
				if (!createRegistries)
				{
					registry = null;
					return value;
				}
				value2 = new Dictionary<Type, Dictionary<InstanceHandle, Inspector>>();
				_registries.Add(category, value2);
			}
			if (!value2.TryGetValue(instanceHandle.Type, out registry))
			{
				if (!createRegistries)
				{
					return value;
				}
				registry = new Dictionary<InstanceHandle, Inspector>();
				value2.Add(instanceHandle.Type, registry);
			}
			registry.TryGetValue(instanceHandle, out value);
			return value;
		}

		private CategoryButton GetCategoryButton(string category, bool create = false)
		{
			category = (string.IsNullOrEmpty(category) ? "Uncategorized" : category);
			foreach (CategoryButton category2 in _categories)
			{
				if (category2.CategoryName == category)
				{
					return category2;
				}
			}
			if (create)
			{
				CategoryButton button = CategoryFlex.Append<CategoryButton>(category);
				button.LayoutStyle = Style.Instantiate<LayoutStyle>("CategoryButton");
				button.CategoryName = category;
				button.Callback = delegate
				{
					SelectCategory(button);
				};
				_categories.Add(button);
				if (_selectedCategory == null)
				{
					SelectCategory(button);
				}
				return button;
			}
			return null;
		}

		private void SelectCategory(CategoryButton category)
		{
			if (_selectedCategory == category)
			{
				return;
			}
			if (_selectedCategory != null)
			{
				_selectedCategory.State = false;
				Flex.ForgetAll();
			}
			_selectedCategory = category;
			if (_selectedCategory != null)
			{
				_selectedCategory.State = true;
				string categoryName = category.CategoryName;
				if (_registries.TryGetValue((categoryName == "Uncategorized") ? string.Empty : categoryName, out var value))
				{
					foreach (KeyValuePair<Type, Dictionary<InstanceHandle, Inspector>> item in value)
					{
						foreach (KeyValuePair<InstanceHandle, Inspector> item2 in item.Value)
						{
							Flex.Remember(item2.Value);
							if ((bool)_debugInterface)
							{
								_debugInterface.SetTransparencyRecursive(item2.Value, !_debugInterface.OpacityOverride);
							}
						}
					}
				}
			}
			_scrollView.Progress = 1f;
		}

		internal void SetPanelPosition(RuntimeSettings.DistanceOption distanceOption, bool skipAnimation = false)
		{
			ValueContainer<Vector3> valueContainer = ValueContainer<Vector3>.Load("InspectorsPanelPositions");
			_targetPosition = distanceOption switch
			{
				RuntimeSettings.DistanceOption.Close => valueContainer["Close"], 
				RuntimeSettings.DistanceOption.Far => valueContainer["Far"], 
				_ => valueContainer["Default"], 
			};
			if (skipAnimation)
			{
				base.SphericalCoordinates = _targetPosition;
				_currentPosition = _targetPosition;
			}
			else
			{
				_lerpCompleted = false;
			}
		}

		private void Update()
		{
			if (!_lerpCompleted)
			{
				_currentPosition = Utils.LerpPosition(_currentPosition, _targetPosition, _lerpSpeed);
				_lerpCompleted = _currentPosition == _targetPosition;
				base.SphericalCoordinates = _currentPosition;
			}
		}
	}
}
