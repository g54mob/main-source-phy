using Landfall.TABS;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class CameraSpawnObject : MonoBehaviour
{
	public enum FireModes
	{
		Tap = 0,
		Spray = 1,
		Beam = 2,
		SuperSpray = 3,
		SuperBeam = 4
	}

	public LocalizeText proj;

	public LocalizeText rate;

	public GameObject objectToSpawn;

	public GameObject[] objectsToSpawn;

	public string[] soundToPlay;

	public FireModes fireModes;

	private PlayerActions m_playerActions;

	private Transform mainCamTransform;

	private InputService inputService;

	private int currentSelectedIndex;

	private int maxIndex;

	private int currentObject;

	private float cd = 0.05f;

	private float spread;

	private float counter;

	private void Start()
	{
		rate.LocaleID = "FIREMODE_TAP";
		inputService = ServiceLocator.GetService<InputService>();
		m_playerActions = PlayerActions.Instance;
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
		maxIndex = objectsToSpawn.Length - 1;
		objectToSpawn = objectsToSpawn[currentSelectedIndex];
	}

	private void Update()
	{
		counter += Time.unscaledDeltaTime;
		if (fireModes == FireModes.Tap)
		{
			if (m_playerActions.m_TriggerAbility.WasPressed && inputService.CurrentState == InputService.InputState.Gameplay)
			{
				if (soundToPlay[currentObject] != "")
				{
					ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundToPlay[currentObject], 1f, mainCamTransform.position + mainCamTransform.forward * 0.5f + mainCamTransform.up * -0.3f);
				}
				Object.Instantiate(objectToSpawn, mainCamTransform.position + mainCamTransform.forward * 0.5f + mainCamTransform.up * -0.3f, mainCamTransform.rotation);
			}
		}
		else if (m_playerActions.m_TriggerAbility.IsPressed && inputService.CurrentState == InputService.InputState.Gameplay && counter > cd)
		{
			if (soundToPlay[currentObject] != "")
			{
				ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundToPlay[currentObject], 1f, mainCamTransform.position + mainCamTransform.forward * 0.5f + mainCamTransform.up * -0.3f);
			}
			counter = 0f;
			Object.Instantiate(objectToSpawn, mainCamTransform.position + mainCamTransform.forward * 0.5f + mainCamTransform.up * -0.3f, Quaternion.LookRotation(mainCamTransform.forward + 0.15f * spread * Random.insideUnitSphere));
		}
		ChangeObjectMode();
		ChangeObjectType();
		ProjectileEntity component = objectToSpawn.GetComponent<ProjectileEntity>();
		if (component != null)
		{
			string localeID = component.Entity.Name;
			proj.LocaleID = localeID;
		}
	}

	private void ChangeObjectType()
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			if (m_playerActions.m_CanTriggerTypeChange.IsPressed && m_playerActions.m_CycleAbilityTypes.WasPressed && inputService.CurrentState == InputService.InputState.Gameplay)
			{
				int num = (currentSelectedIndex = ((currentSelectedIndex + 1 <= maxIndex) ? (currentSelectedIndex + 1) : 0));
				objectToSpawn = objectsToSpawn[num];
			}
			return;
		}
		currentObject -= (int)Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * 100f, -1f, 1f);
		if ((int)Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * 100f, -1f, 1f) != 0)
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("Bugs/SwitchProjectile", 1f, base.transform.position);
		}
		currentObject = Mathf.Clamp(currentObject, 0, objectsToSpawn.Length - 1);
		objectToSpawn = objectsToSpawn[currentObject];
	}

	private void ChangeObjectMode()
	{
		if (PlayerActions.Instance.InputType == InputType.Controller)
		{
			if (!m_playerActions.m_CycleAbilityTypes.WasPressed || m_playerActions.m_CanTriggerTypeChange.IsPressed)
			{
				return;
			}
		}
		else if (!m_playerActions.m_CycleAbilityModes.WasPressed)
		{
			return;
		}
		if (inputService.CurrentState == InputService.InputState.Gameplay)
		{
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("Bugs/SwitchProjectile", 1f, base.transform.position);
			switch (fireModes)
			{
			case FireModes.Tap:
				fireModes = FireModes.Spray;
				rate.LocaleID = "FIREMODE_SPRAY";
				cd = 0.15f;
				spread = 1f;
				break;
			case FireModes.Spray:
				fireModes = FireModes.Beam;
				rate.LocaleID = "FIREMODE_BEAM";
				cd = 0.15f;
				spread = 0f;
				break;
			case FireModes.Beam:
				fireModes = FireModes.SuperSpray;
				rate.LocaleID = "FIREMODE_SUPER_SPRAY";
				cd = 0.05f;
				spread = 1f;
				break;
			case FireModes.SuperSpray:
				fireModes = FireModes.SuperBeam;
				rate.LocaleID = "FIREMODE_SUPER_BEAM";
				cd = 0.05f;
				spread = 0f;
				break;
			case FireModes.SuperBeam:
				fireModes = FireModes.Tap;
				rate.LocaleID = "FIREMODE_TAP";
				spread = 0f;
				break;
			}
		}
	}
}
