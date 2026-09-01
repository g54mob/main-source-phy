using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesaturationScript : MonoBehaviour
{
	public float desaturation = 0.6f;

	private Image[] images;

	private float previousDesaturation = -1f;

	private void GetImages()
	{
		images = GetComponentsInChildren<Image>();
		Image[] array = images;
		foreach (Image image in array)
		{
			if (image != null)
			{
				Material material = new Material(image.materialForRendering);
				image.material = material;
			}
		}
	}

	public void SetDesaturation(float desaturation)
	{
		if (images == null || images.Length == 0)
		{
			GetImages();
		}
		desaturation = Mathf.Clamp01(desaturation);
		if (previousDesaturation == desaturation)
		{
			return;
		}
		previousDesaturation = desaturation;
		Image[] array = images;
		foreach (Image image in array)
		{
			if (image != null)
			{
				image.materialForRendering.SetFloat("_Desaturation", desaturation);
			}
		}
	}

	public void EnableDesaturation(bool enable)
	{
		SetDesaturation(enable ? desaturation : 0f);
	}

	private void Update()
	{
		if (PlayerActions.Instance.InputType != InputType.Controller)
		{
			return;
		}
		GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
		if (currentSelectedGameObject != null)
		{
			if (currentSelectedGameObject != base.gameObject)
			{
				EnableDesaturation(enable: true);
			}
			else
			{
				EnableDesaturation(enable: false);
			}
		}
	}
}
