using UnityEngine;

public class EngineControlsLightsController : MonoBehaviour
{
	[SerializeField]
	private DieselEngineController _engineController;

	[Header("Fuel light")]
	[SerializeField]
	private GameObject _fuelLightRed;

	[SerializeField]
	private GameObject _fuelLightYellow;

	[SerializeField]
	private GameObject _fuelLightGreen;

	[Header("Injection light")]
	[SerializeField]
	private GameObject _injectionLightRed;

	[SerializeField]
	private GameObject _injectionLightYellow;

	[SerializeField]
	private GameObject _injectionLightGreen;

	private bool _wasEngineRunning;

	private bool _wasFuelOk;

	private bool _wasInjectionTimingOk;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private bool IsFuelOk(bool isEngineRunning)
	{
		return false;
	}

	private bool IsInjectionTimingOk(bool isEngineRunning)
	{
		return false;
	}

	private void UpdateFuelLightState(bool isEngineRunning, bool isFuelOk)
	{
	}

	private void UpdateInjectionTimingLightState(bool isEngineRunning, bool isInjectionTimingOk)
	{
	}
}
