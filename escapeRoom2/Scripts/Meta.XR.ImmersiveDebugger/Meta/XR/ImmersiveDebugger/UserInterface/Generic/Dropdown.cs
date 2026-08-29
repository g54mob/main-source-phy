using System;
using System.Reflection;
using Meta.XR.ImmersiveDebugger.Manager;
using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.UserInterface.Generic
{
	public class Dropdown : Controller
	{
		private Flex _flex;

		private TweakEnum _tweak;

		private ButtonWithLabel _baseLabel;

		private bool IsMenuVisible => _flex.Visibility;

		public string Label
		{
			get
			{
				return _baseLabel.Label;
			}
			set
			{
				_baseLabel.Label = value;
				_tweak.Value = value;
			}
		}

		internal void SetupMenu(TweakEnum tweak)
		{
			_tweak = tweak;
			Label = _tweak.Value;
			SetupDropdownList();
		}

		protected override void Setup(Controller owner)
		{
			base.Setup(owner);
			_baseLabel = Append<ButtonWithLabel>("label");
			_baseLabel.LayoutStyle = Style.Instantiate<LayoutStyle>("DropdownValueItem");
			_baseLabel.TextStyle = Style.Load<TextStyle>("MemberValue");
			_baseLabel.BackgroundStyle = Style.Instantiate<ImageStyle>("DropdownValueBackground");
			ButtonWithLabel baseLabel = _baseLabel;
			baseLabel.Callback = (Action)Delegate.Combine(baseLabel.Callback, new Action(OnDropdownClick));
			Icon icon = _baseLabel.Append<Icon>("icon");
			icon.LayoutStyle = Style.Load<LayoutStyle>("DropdownArrowIcon");
			ImageStyle imageStyle = Style.Load<ImageStyle>("DownArrowIcon");
			icon.Texture = imageStyle.icon;
			icon.Color = imageStyle.color;
		}

		private void OnDropdownClick()
		{
			SetDropdownMenuVisibility(!IsMenuVisible);
		}

		internal void OnMenuItemClick(DropdownMenuItem menuItem)
		{
			Label = menuItem.Label;
			SetDropdownMenuVisibility(visible: false);
		}

		private void SetDropdownMenuVisibility(bool visible)
		{
			if (visible)
			{
				_flex.Show();
			}
			else
			{
				_flex.Hide();
			}
		}

		private void HideDropdownItems()
		{
			_flex.Hide();
		}

		private void SetupDropdownList()
		{
			_flex = Append<Flex>("list");
			_flex.LayoutStyle = Style.Load<LayoutStyle>("DropdownValuesFlex");
			Canvas canvas = _flex.gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			canvas.sortingOrder = 5;
			_flex.gameObject.AddComponent<CanvasGroup>();
			_flex.gameObject.AddComponent<OVRRaycaster>().sortOrder = 5;
			Array array = null;
			Type type = (_tweak.Member as FieldInfo)?.FieldType;
			Type type2 = (_tweak.Member as PropertyInfo)?.PropertyType;
			if (type != null)
			{
				array = Enum.GetValues(type);
			}
			else if (type2 != null)
			{
				array = Enum.GetValues(type2);
			}
			foreach (object item in array)
			{
				AppendValue(item.ToString());
			}
			HideDropdownItems();
		}

		private void AppendValue(string data)
		{
			DropdownMenuItem dropdownMenuItem = _flex.Append<DropdownMenuItem>("menu_item_" + data);
			dropdownMenuItem.Label = data;
			dropdownMenuItem.RegisterDropdownSourceMenu(this);
		}
	}
}
