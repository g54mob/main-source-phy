using UnityEngine;

public class OpenKeyMapper : MonoBehaviour
{
	public TextMesh keyTextMesh;

	public string mappedKey;

	public KeyMapController KeyMapControllerCode;

	public KeyBlockType myBlockType;

	private void Start()
	{
		KeyMapControllerCode = GameObject.Find("KeyMapper").GetComponent<KeyMapController>();
		GetMyKey();
	}

	private void GetMyKey()
	{
		if (myBlockType == KeyBlockType.ExplosiveBoltBlock)
		{
			AssignKey(KeyMapController.ExplosiveBoltKey);
		}
		else if (myBlockType == KeyBlockType.RedMuscleBlock)
		{
			AssignKey(KeyMapController.RedMuscleKey);
		}
		else if (myBlockType == KeyBlockType.BlueMuscleBlock)
		{
			AssignKey(KeyMapController.BlueMuscleKey);
		}
		else if (myBlockType == KeyBlockType.PistonBlock)
		{
			AssignKey(KeyMapController.PistonKey);
		}
	}

	public void AssignKey(string letter)
	{
		letter = letter.ToLower();
		if (myBlockType == KeyBlockType.ExplosiveBoltBlock)
		{
			KeyMapController.ExplosiveBoltKey = letter;
		}
		else if (myBlockType == KeyBlockType.RedMuscleBlock)
		{
			KeyMapController.RedMuscleKey = letter;
		}
		else if (myBlockType == KeyBlockType.BlueMuscleBlock)
		{
			KeyMapController.BlueMuscleKey = letter;
		}
		else if (myBlockType == KeyBlockType.PistonBlock)
		{
			KeyMapController.PistonKey = letter;
		}
		mappedKey = letter;
		keyTextMesh.text = letter.ToUpper();
	}
}
