using UnityEngine;
using VInspector;

public class CatController : MonoBehaviour, IHighlight
{
	[SerializeField]
	private Transform cameraTarget_CatRight;

	[SerializeField]
	private Transform cameraTarget_CatLeft;

	[SerializeField]
	private Outline outline;

	[SerializeField]
	private ParticleSystem pettingParticle;

	[SerializeField]
	private LoopAudioSource prrLoopAudioSource;

	[SerializeField]
	private ParticleSystem upgradeParticle;

	[Space]
	public bool isCatPetted;

	[Space]
	public string catNameID;

	[Space]
	public bool isKitten;

	[SerializeField]
	private bool isDebugOn;

	[Space]
	[SerializeField]
	[ReadOnly]
	private bool isHighlighted;

	[SerializeField]
	[ReadOnly]
	private bool isPetHighlighted;

	[SerializeField]
	[ReadOnly]
	private bool isHintHighlighted;

	private bool isGettingPetted;

	private void Start()
	{
		SetHighlight(flag: false);
		outline.UpdateMaterialProperties();
	}

	public void InteractWithCat()
	{
		if (SceneSingleton<UpgradesManager>.Instance.aviliableSkillPoints > 0 || isKitten || !SceneSingleton<TutorialManager>.Instance.isTutorialCompleted)
		{
			SceneSingleton<CharacterCamerasController>.Instance.SetMovingPuzzleCameraTarget(cameraTarget_CatRight, delegate
			{
				SceneSingleton<UIManager>.Instance.ShowCatCameraZoom(DisableCat);
				SceneSingleton<CatPanelUI>.Instance.SetUpgradeUI();
				SceneSingleton<CatPanelUI>.Instance.UpdateUpgradeUI();
				SceneSingleton<CatsManager>.Instance.InteractWithCat(this);
			});
		}
		else
		{
			SceneSingleton<CharacterCamerasController>.Instance.SetMovingPuzzleCameraTarget(cameraTarget_CatLeft, delegate
			{
				SceneSingleton<UIManager>.Instance.ShowCatCameraZoom(DisableCat);
				SceneSingleton<CatsManager>.Instance.UpdateHoldedPotion();
				SceneSingleton<CatsManager>.Instance.InteractWithCat(this);
			});
		}
	}

	public void GoToPotionCamera()
	{
		SceneSingleton<CharacterCamerasController>.Instance.SetMovingPuzzleCameraTarget(cameraTarget_CatLeft, delegate
		{
			SceneSingleton<CatsManager>.Instance.UpdateHoldedPotion();
		});
	}

	public void GoToUpgradeCamera()
	{
		SceneSingleton<CharacterCamerasController>.Instance.SetMovingPuzzleCameraTarget(cameraTarget_CatRight, delegate
		{
			SceneSingleton<CatPanelUI>.Instance.SetUpgradeUI();
		});
	}

	public virtual void DisableCat()
	{
		SceneSingleton<CatsManager>.Instance.DisableCatUI();
		SceneSingleton<CharacterCamerasController>.Instance.ResetMovingCamera();
		pettingParticle.Stop();
		isPetHighlighted = false;
		StopPetting();
		SetHighlight(flag: false);
	}

	public void SetUpgradeHighlight(bool flag)
	{
		if (!(upgradeParticle == null))
		{
			if (flag)
			{
				upgradeParticle.Play();
			}
			else
			{
				upgradeParticle.Stop();
			}
		}
	}

	public void SetHintHighlight(bool flag)
	{
		isHintHighlighted = flag;
		UpdateOutline();
	}

	public void SetHighlight(bool flag)
	{
		isHighlighted = flag;
		UpdateOutline();
	}

	private void UpdateOutline()
	{
		outline.enabled = isHighlighted || isHintHighlighted || isPetHighlighted;
		outline.OutlineColor = Color.white;
	}

	public void SetPettingHighlight(bool flag)
	{
		isPetHighlighted = flag;
		UpdateOutline();
	}

	public void StartPetting()
	{
		if (!isGettingPetted)
		{
			isGettingPetted = true;
			isCatPetted = true;
			SceneSingleton<CatsManager>.Instance.UpdatePettingCount();
			prrLoopAudioSource.PlayAudio();
			pettingParticle.Play();
			SceneSingleton<TutorialManager>.Instance.CompleteTutorialStep(TutorialStepType.Tutorial_PetCat);
		}
	}

	public void StopPetting()
	{
		isGettingPetted = false;
		prrLoopAudioSource.StopAudio();
		pettingParticle.Stop();
	}
}
