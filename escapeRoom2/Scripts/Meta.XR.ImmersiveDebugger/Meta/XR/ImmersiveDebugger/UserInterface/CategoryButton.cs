using System;
using Meta.XR.ImmersiveDebugger.UserInterface.Generic;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	public class CategoryButton : Toggle
	{
		private string _category;

		private int _counter;

		private Label _label;

		private Label _subLabel;

		private Flex _flex;

		internal string CategoryName
		{
			get
			{
				return _category;
			}
			set
			{
				_category = value;
				_label.Content = _category;
			}
		}

		internal int Counter
		{
			get
			{
				return _counter;
			}
			set
			{
				_counter = value;
				_counter = Math.Max(0, _counter);
				Label subLabel = _subLabel;
				subLabel.Content = _counter switch
				{
					0 => "No objects tracked", 
					1 => "1 object tracked", 
					_ => $"{_counter} objects tracked", 
				};
			}
		}

		protected override void Setup(Controller owner)
		{
			base.Setup(owner);
			_flex = Append<Flex>("flex");
			_flex.LayoutStyle = Style.Load<LayoutStyle>("CategoryButtonFlex");
			_label = _flex.Append<Label>("label");
			_label.LayoutStyle = Style.Load<LayoutStyle>("CategoryLabel");
			_label.TextStyle = Style.Load<TextStyle>("CategoryLabel");
			_subLabel = _flex.Append<Label>("sublabel");
			_subLabel.LayoutStyle = Style.Load<LayoutStyle>("CategorySubLabel");
			_subLabel.TextStyle = Style.Load<TextStyle>("CategorySubLabel");
			base.IconStyle = Style.Load<ImageStyle>("None");
			base.BackgroundStyle = Style.Instantiate<ImageStyle>("CategoryButtonBackground");
		}
	}
}
