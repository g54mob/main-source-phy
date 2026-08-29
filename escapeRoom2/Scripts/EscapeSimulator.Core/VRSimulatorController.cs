using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

[InputControlLayout(stateType = typeof(VRSimulatorControllerState), commonUsages = new string[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = false, displayName = "VRSimulatorController", updateBeforeRender = true)]
public class VRSimulatorController : XRController
{
	public Vector2Control primary2DAxis { get; private set; }

	public AxisControl trigger { get; private set; }

	public AxisControl grip { get; private set; }

	public Vector2Control secondary2DAxis { get; private set; }

	public ButtonControl primaryButton { get; private set; }

	public ButtonControl primaryTouch { get; private set; }

	public ButtonControl secondaryButton { get; private set; }

	public ButtonControl secondaryTouch { get; private set; }

	public ButtonControl gripButton { get; private set; }

	public ButtonControl triggerButton { get; private set; }

	public ButtonControl menuButton { get; private set; }

	public ButtonControl primary2DAxisClick { get; private set; }

	public ButtonControl primary2DAxisTouch { get; private set; }

	public ButtonControl secondary2DAxisClick { get; private set; }

	public ButtonControl secondary2DAxisTouch { get; private set; }

	public AxisControl batteryLevel { get; private set; }

	public ButtonControl userPresence { get; private set; }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void registerLayout()
	{
		InputSystem.RegisterLayout<VRSimulatorController>(null, default(InputDeviceMatcher).WithInterface("VRSimulatorController"));
	}

	protected override void FinishSetup()
	{
		base.FinishSetup();
		primary2DAxis = GetChildControl<Vector2Control>("primary2DAxis");
		trigger = GetChildControl<AxisControl>("trigger");
		grip = GetChildControl<AxisControl>("grip");
		secondary2DAxis = GetChildControl<Vector2Control>("secondary2DAxis");
		primaryButton = GetChildControl<ButtonControl>("primaryButton");
		primaryTouch = GetChildControl<ButtonControl>("primaryTouch");
		secondaryButton = GetChildControl<ButtonControl>("secondaryButton");
		secondaryTouch = GetChildControl<ButtonControl>("secondaryTouch");
		gripButton = GetChildControl<ButtonControl>("gripButton");
		triggerButton = GetChildControl<ButtonControl>("triggerButton");
		menuButton = GetChildControl<ButtonControl>("menuButton");
		primary2DAxisClick = GetChildControl<ButtonControl>("primary2DAxisClick");
		primary2DAxisTouch = GetChildControl<ButtonControl>("primary2DAxisTouch");
		secondary2DAxisClick = GetChildControl<ButtonControl>("secondary2DAxisClick");
		secondary2DAxisTouch = GetChildControl<ButtonControl>("secondary2DAxisTouch");
		batteryLevel = GetChildControl<AxisControl>("batteryLevel");
		userPresence = GetChildControl<ButtonControl>("userPresence");
	}
}
