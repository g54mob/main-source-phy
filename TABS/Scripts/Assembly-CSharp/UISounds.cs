using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISounds : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IValidatable
{
	public bool forceSound;

	[SerializeField]
	private string enterRef;

	[SerializeField]
	private string clickRef;

	[SerializeField]
	private AudioPathData enterPathData;

	[SerializeField]
	private AudioPathData clickPathData;

	private CodeAnimation anim;

	private SoundPlayer m_soundPlayer;

	public string EnterRef
	{
		get
		{
			return enterRef;
		}
		set
		{
			enterRef = value;
			AudioPathData.ValidateAndAssignPathData(enterRef, ref enterPathData, base.gameObject);
		}
	}

	public string ClickRef
	{
		get
		{
			return clickRef;
		}
		set
		{
			clickRef = value;
			AudioPathData.ValidateAndAssignPathData(clickRef, ref clickPathData, base.gameObject);
		}
	}

	public bool Validate()
	{
		bool num = AudioPathData.ValidateAndAssignPathData(enterRef, ref enterPathData, base.gameObject);
		bool flag = AudioPathData.ValidateAndAssignPathData(clickRef, ref clickPathData, base.gameObject);
		return num && flag;
	}

	private void Awake()
	{
		ValidateAudioPaths();
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		anim = GetComponentInParent<CodeAnimation>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (base.enabled && (!anim || !anim.isPlaying || forceSound))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(clickPathData, 1f, base.transform.position);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (base.enabled && (!anim || !anim.isPlaying))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(enterPathData, 1f, base.transform.position);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	private void ValidateAudioPaths()
	{
		if ((!string.IsNullOrEmpty(enterRef) || !string.IsNullOrEmpty(clickRef)) && (enterPathData == null || string.IsNullOrEmpty(enterPathData.Category) || clickPathData == null || string.IsNullOrEmpty(clickPathData.Category)))
		{
			Validate();
		}
	}
}
