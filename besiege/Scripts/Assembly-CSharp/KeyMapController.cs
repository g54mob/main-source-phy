using UnityEngine;

public class KeyMapController : MonoBehaviour
{
	public static string ExplosiveBoltKey = "j";

	public static string RedMuscleKey = "k";

	public static string BlueMuscleKey = "l";

	public static string PistonKey = "h";

	public Transform KeyMapperObject;

	public Transform keyHolder;

	public OpenKeyMapper currentOpenKeyMapperCode;

	public KeyMapIndividualButton[] individualKeys;

	public Camera hudCam;

	private void Start()
	{
		individualKeys = new KeyMapIndividualButton[keyHolder.childCount];
		for (int i = 0; i < keyHolder.childCount; i++)
		{
			individualKeys[i] = keyHolder.GetChild(i).GetComponent<KeyMapIndividualButton>();
		}
	}

	private void Open(OpenKeyMapper OpenKeyMapperCode)
	{
		StatMaster.SetInMenu(true);
		KeyMapperObject.gameObject.SetActive(true);
		currentOpenKeyMapperCode = OpenKeyMapperCode;
		HighlightKey(OpenKeyMapperCode.mappedKey);
	}

	public void SetKey(string key)
	{
		currentOpenKeyMapperCode.AssignKey(key);
		HighlightKey(key);
	}

	private void HighlightKey(string key)
	{
		for (int i = 0; i < individualKeys.Length; i++)
		{
			if (individualKeys[i].myLetter == key)
			{
				individualKeys[i].Enabled();
			}
			else
			{
				individualKeys[i].Disabled();
			}
		}
	}

	private void Update()
	{
		Machine machine = Machine.Active();
		if ((bool)machine && machine.isSimulating && InputManager.LeftMouseButton())
		{
			Ray ray = hudCam.ScreenPointToRay(InputManager.CursorPosition());
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.gameObject.name == "OpenKeyMap")
			{
				Open(hitInfo.collider.gameObject.GetComponent<OpenKeyMapper>());
			}
		}
	}
}
