using UIStateManager;

namespace GamepadUI.StateManager.Core
{
	public class UIComponent : UIComponentBase
	{
		protected CanvasToggle canvasToggle;

		protected InterfaceStateManager stateManager;

		protected override void Awake()
		{
			base.Awake();
			canvasToggle = GetComponent<CanvasToggle>();
		}

		protected override void OnOpen()
		{
			if (canvasToggle != null)
			{
				canvasToggle.TurnOn();
			}
		}

		protected override void OnClose()
		{
			if (canvasToggle != null)
			{
				canvasToggle.FadeOut();
			}
		}

		public void Init(InterfaceStateManager interfaceStateManager)
		{
			stateManager = interfaceStateManager;
		}
	}
}
