using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
	public enum Type
	{
		Gear = 0,
		Puzzles = 1
	}

	public SpinnerUI ui;

	[Header("Gear")]
	public Vector3 gearEulerSpeed = new Vector3(0f, 0f, -45f);

	[Header("Puzzle")]
	public float puzzleCooldownAmount = 1f;

	private int currentLoadingImageIndex;

	private float cooldownToNextImage;

	private void OnEnable()
	{
		reset();
	}

	private Type getType()
	{
		return Type.Gear;
	}

	public void reset()
	{
		Image[] loading_List;
		if (getType() == Type.Gear)
		{
			ui.Gear.gameObject.SetActive(value: true);
			loading_List = ui.Loading_List;
			for (int i = 0; i < loading_List.Length; i++)
			{
				loading_List[i].gameObject.SetActive(value: false);
			}
			return;
		}
		ui.Gear.gameObject.SetActive(value: false);
		currentLoadingImageIndex = 0;
		cooldownToNextImage = puzzleCooldownAmount;
		loading_List = ui.Loading_List;
		foreach (Image obj in loading_List)
		{
			obj.gameObject.SetActive(value: true);
			Color color = obj.color;
			color.a = 0f;
			obj.color = color;
		}
	}

	private void Update()
	{
		if (getType() == Type.Gear)
		{
			updateGear();
		}
		else
		{
			updatePuzzles();
		}
		void updateGear()
		{
			Vector3 eulerAngles = ui.Gear.transform.rotation.eulerAngles;
			eulerAngles += gearEulerSpeed * Time.deltaTime;
			ui.Gear.transform.rotation = Quaternion.Euler(eulerAngles);
		}
		void updatePuzzles()
		{
			for (int i = 0; i < ui.Loading_List.Length; i++)
			{
				Image image = ui.Loading_List[i];
				if (i == currentLoadingImageIndex)
				{
					Color color = image.color;
					color.a = 1f;
					image.color = color;
					cooldownToNextImage -= Time.deltaTime;
					if (cooldownToNextImage <= 0f)
					{
						currentLoadingImageIndex = (i + 1) % ui.Loading_List.Length;
						cooldownToNextImage = puzzleCooldownAmount;
					}
				}
				else
				{
					float num = 1f / (float)(ui.Loading_List.Length - 1) / puzzleCooldownAmount;
					float a = Mathf.MoveTowards(image.color.a, 0f, Time.deltaTime * num);
					Color color2 = image.color;
					color2.a = a;
					image.color = color2;
				}
			}
		}
	}
}
