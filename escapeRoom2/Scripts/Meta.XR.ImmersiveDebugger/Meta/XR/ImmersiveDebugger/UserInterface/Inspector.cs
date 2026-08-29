using System.Collections.Generic;
using System.Reflection;
using Meta.XR.ImmersiveDebugger.UserInterface.Generic;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal class Inspector : Controller, IInspector
	{
		private Label _title;

		private Flex _flex;

		private Background _background;

		private readonly Dictionary<MemberInfo, Member> _registry = new Dictionary<MemberInfo, Member>();

		private ImageStyle _backgroundImageStyle;

		public ImageStyle BackgroundStyle
		{
			set
			{
				_background.Sprite = value.sprite;
				_background.Color = value.color;
				_background.PixelDensityMultiplier = value.pixelDensityMultiplier;
			}
		}

		public string Title
		{
			get
			{
				return _title.Content;
			}
			set
			{
				_title.Content = value;
			}
		}

		protected override void Setup(Controller owner)
		{
			base.Setup(owner);
			_background = Append<Background>("background");
			_background.LayoutStyle = Style.Load<LayoutStyle>("Fill");
			_backgroundImageStyle = Style.Load<ImageStyle>("InspectorBackground");
			BackgroundStyle = _backgroundImageStyle;
			_flex = Append<Flex>("list");
			_flex.LayoutStyle = Style.Load<LayoutStyle>("InspectorFlex");
			_title = _flex.Append<Label>("title");
			_title.LayoutStyle = Style.Load<LayoutStyle>("InspectorTitle");
			_title.TextStyle = Style.Load<TextStyle>("InspectorTitle");
		}

		protected override void OnTransparencyChanged()
		{
			base.OnTransparencyChanged();
			_background.Color = (base.Transparent ? _backgroundImageStyle.colorOff : _backgroundImageStyle.color);
		}

		public void UpdateBackground(bool transparent)
		{
			base.Transparent = transparent;
			OnTransparencyChanged();
		}

		public IMember RegisterMember(MemberInfo memberInfo, DebugMember attribute)
		{
			if (!_registry.TryGetValue(memberInfo, out var value))
			{
				value = _flex.Append<Member>(memberInfo.Name);
				value.LayoutStyle = Style.Instantiate<LayoutStyle>("Member");
				value.Title = (string.IsNullOrEmpty(attribute.DisplayName) ? (memberInfo.Name ?? "") : attribute.DisplayName);
				if (!string.IsNullOrEmpty(attribute.Description))
				{
					value.RegisterDescriptor();
					value.Description = attribute.Description;
				}
				value.PillColor = attribute.Color;
				_registry.Add(memberInfo, value);
			}
			return value;
		}

		public IMember GetMember(MemberInfo memberInfo)
		{
			_registry.TryGetValue(memberInfo, out var value);
			return value;
		}
	}
}
