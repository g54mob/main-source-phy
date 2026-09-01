using UnityEngine;

public class ConquerTextController : ZoneCompleteFanfare
{
	public MeshRenderer[] slaveTexts;

	public override void SetTextActive(bool active)
	{
		base.SetTextActive(active);
		for (int i = 0; i < slaveTexts.Length; i++)
		{
			slaveTexts[i].gameObject.SetActive(active);
		}
	}

	public override void SetTextAlpha(float alpha)
	{
		base.SetTextAlpha(alpha);
		for (int i = 0; i < slaveTexts.Length; i++)
		{
			Color color = slaveTexts[i].material.color;
			slaveTexts[i].material.color = new Color(color.r, color.g, color.b, alpha * alpha);
		}
	}
}
