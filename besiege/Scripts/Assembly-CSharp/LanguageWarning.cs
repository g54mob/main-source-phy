using System;
using System.Linq;
using Localisation;
using UnityEngine;
using mattmc3.dotmore.Collections.Generic;

public class LanguageWarning : MonoBehaviour
{
	public GameObject DialogChild;

	public void Awake()
	{
		MinimiseWindow componentInChildren = GetComponentInChildren<MinimiseWindow>();
		componentInChildren.OnMinimise = (Action)Delegate.Combine(componentInChildren.OnMinimise, new Action(OnMinimise));
		base.gameObject.SetActive(false);
	}

	public void OnLanguageChanged()
	{
		DialogChild.gameObject.SetActive(true);
		OnEnable();
	}

	public void OnEnable()
	{
		string currLangISO = SingleInstance<LocalisationManager>.Instance.currLangISO;
		base.gameObject.SetActive(false);
		if (currLangISO == "English")
		{
			DialogChild.SetActive(false);
		}
		if (OptionsMaster.BesiegeConfig.ShownLanguageWarningFor.Contains(currLangISO))
		{
			DialogChild.SetActive(false);
		}
	}

	private void OnMinimise()
	{
		OptionsMaster.BesiegeConfig.ShownLanguageWarningFor = OptionsMaster.BesiegeConfig.ShownLanguageWarningFor.Append(SingleInstance<LocalisationManager>.Instance.currLangISO).ToArray();
	}
}
