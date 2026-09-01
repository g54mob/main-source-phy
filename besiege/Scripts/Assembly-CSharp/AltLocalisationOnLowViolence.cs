using Localisation;
using UnityEngine;

public class AltLocalisationOnLowViolence : MonoBehaviour
{
	public TextMesh tm;

	public DynamicText dt;

	public int id;

	public void Start()
	{
		if (SingleInstance<StatMaster>.Instance.LowViolence)
		{
			LowViolenceLocal();
		}
	}

	public void LowViolenceLocal()
	{
		if ((bool)dt)
		{
			ReferenceMaster.SetDynamicText(dt, LocalisationManager.GetTranslation(id));
		}
		if ((bool)tm)
		{
			tm.text = LocalisationManager.GetTranslation(id);
		}
	}
}
