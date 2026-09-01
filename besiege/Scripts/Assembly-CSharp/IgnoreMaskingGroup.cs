using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("UI/Canvas/Ignore Masking Group")]
public class IgnoreMaskingGroup : MonoBehaviour
{
	private void OnEnable()
	{
		DisableMaskingForChildren();
	}

	private void OnDisable()
	{
		RestoreChildrenMasking();
	}

	private void DisableMaskingForChildren()
	{
		ToggleMaskingForChildren(false);
	}

	private void RestoreChildrenMasking()
	{
		ToggleMaskingForChildren(true);
	}

	private void ToggleMaskingForChildren(bool toggleOn)
	{
		MaskableGraphic[] componentsInChildren = GetComponentsInChildren<MaskableGraphic>();
		foreach (MaskableGraphic maskableGraphic in componentsInChildren)
		{
			maskableGraphic.maskable = toggleOn;
			maskableGraphic.RecalculateClipping();
		}
	}
}
