using System.Collections.Generic;
using Localisation;
using UnityEngine;

[AddComponentMenu("Physics/Wind/Entity")]
public class WindEntity : GenericEntity, ILocalisationAware
{
	public WindController windController;

	public GameObject[] visuals;

	public GameObject[] localWindVisuals;

	private MSlider windForceSlider;

	private MToggle IsGlobal;

	private MMenu display;

	private int state;

	public override void Init()
	{
		if (isInitialized)
		{
			return;
		}
		base.Init();
		windForceSlider = AddSlider(2490, GenericEntity.LOGIC_PREFIX + "force", windController.windForce, 1f, 1000f, string.Empty);
		windForceSlider.ValueChanged += OnForceChanged;
		IsGlobal = AddToggle(2491, GenericEntity.LOGIC_PREFIX + "global", !windController.useCollider);
		display = AddMenu("look", state, new List<string>
		{
			LocalisationManager.GetTranslation(3288),
			LocalisationManager.GetTranslation(3289),
			LocalisationManager.GetTranslation(3290),
			LocalisationManager.GetTranslation(3291)
		});
		display.ValueChanged += DisplayChanged;
		if (localWindVisuals.Length != 0)
		{
			for (int i = 0; i < localWindVisuals.Length; i++)
			{
				localWindVisuals[i].gameObject.SetActive(!IsGlobal.IsActive);
			}
		}
		IsGlobal.Toggled += OnIsGlobalChange;
	}

	public override bool TriggerEvaluate()
	{
		return false;
	}

	private void DisplayChanged(int index)
	{
		state = index;
	}

	protected override void Awake()
	{
		base.Awake();
		windController.windEntity = this;
	}

	protected override void Start()
	{
		base.Start();
		if (StatMaster.levelSimulating)
		{
			UpdateVisuals();
		}
	}

	public void UpdateVisuals()
	{
		if (IsGlobal.IsActive)
		{
			state = 0;
		}
		if (state == 3)
		{
			for (int i = 0; i < visuals.Length - 2; i++)
			{
				visuals[i].SetActive(false);
			}
			visuals[visuals.Length - 1].SetActive(false);
		}
		else
		{
			for (int j = 0; j < visuals.Length - state; j++)
			{
				visuals[j].SetActive(false);
			}
		}
	}

	public void RestoreVisuals()
	{
		for (int i = 0; i < visuals.Length; i++)
		{
			visuals[i].SetActive(true);
		}
		if (localWindVisuals.Length != 0)
		{
			for (int j = 0; j < localWindVisuals.Length; j++)
			{
				localWindVisuals[j].gameObject.SetActive(!IsGlobal.IsActive);
			}
		}
	}

	protected void OnForceChanged(float newValue)
	{
		windController.windForce = newValue;
	}

	protected void OnIsGlobalChange(bool newValue)
	{
		if (entity.isSimulating)
		{
			return;
		}
		if (newValue)
		{
			if (StatMaster.IsGlobalWindPresent)
			{
				IsGlobal.IsActive = false;
			}
			else
			{
				StatMaster.IsGlobalWindPresent = newValue;
				windController.useCollider = !newValue;
			}
		}
		else if (!windController.useCollider && StatMaster.IsGlobalWindPresent)
		{
			windController.useCollider = !newValue;
			StatMaster.IsGlobalWindPresent = newValue;
		}
		else
		{
			windController.useCollider = !newValue;
		}
		if (localWindVisuals.Length != 0)
		{
			for (int i = 0; i < localWindVisuals.Length; i++)
			{
				localWindVisuals[i].gameObject.SetActive(!IsGlobal.IsActive);
			}
		}
		display.DisplayInMapper = !IsGlobal.IsActive;
	}

	public override void UpdateOnTransformEvent()
	{
		windController.UpdateBounds();
	}

	protected override void OnEnable()
	{
		UpdateSimState();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (!entity.isSimulating && StatMaster.IsGlobalWindPresent && (bool)windController && !windController.useCollider)
		{
			StatMaster.IsGlobalWindPresent = false;
		}
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		if (display != null)
		{
			display.Items = new List<string>
			{
				LocalisationManager.GetTranslation(3288),
				LocalisationManager.GetTranslation(3289),
				LocalisationManager.GetTranslation(3290),
				LocalisationManager.GetTranslation(3291)
			};
		}
	}
}
