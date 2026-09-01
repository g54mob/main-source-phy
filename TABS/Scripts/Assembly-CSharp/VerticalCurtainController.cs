using System.Collections;
using Landfall.TABS.GameMode;
using TMPro;
using UnityEngine;

public class VerticalCurtainController : MonoBehaviour
{
	public delegate void OnAnimationDoneDelegate();

	private Animator m_animator;

	private LocalizeText m_topText;

	private TMP_Text m_topSubText;

	private bool m_areCurtainsClosed;

	private Coroutine m_animationRoutine;

	private bool m_isAnimationDone;

	private bool m_animationAborted;

	public bool AreCurtainsClosed
	{
		get
		{
			return m_areCurtainsClosed;
		}
		private set
		{
			m_areCurtainsClosed = value;
		}
	}

	public OnAnimationDoneDelegate OnAnimationDoneCallback { get; set; }

	private void Awake()
	{
		m_animator = GetComponent<Animator>();
		ServiceLocator.GetService<GameModeService>().CurrentGameMode.CurtainController = this;
	}

	private void OnExitedState()
	{
		m_isAnimationDone = true;
	}

	private void Start()
	{
		m_animator.updateMode = AnimatorUpdateMode.UnscaledTime;
		LocalizeText[] componentsInChildren = base.gameObject.GetComponentsInChildren<LocalizeText>();
		m_topText = componentsInChildren[0];
		m_topSubText = componentsInChildren[1].Text;
	}

	public void CloseCurtains(string headerText, string subText)
	{
		m_topText.LocaleID = headerText;
		m_topSubText.text = subText;
		m_areCurtainsClosed = true;
		m_animator.SetBool("PlayClose", value: true);
		base.enabled = false;
		StartIsAnimationDone();
	}

	public void PeekCurtains(string headerText, string subText)
	{
		m_topText.LocaleID = headerText;
		m_topSubText.text = subText;
		PeekCurtains();
	}

	public void PeekCurtains()
	{
		m_areCurtainsClosed = true;
		m_animator.SetBool("PlayIn", value: true);
		m_animator.SetBool("PlayOut", value: false);
		ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("UI/Win", 1f, Vector3.zero);
		StartIsAnimationDone();
	}

	public void OpenCurtains()
	{
		m_areCurtainsClosed = false;
		m_animator.SetBool("PlayOut", value: true);
		m_animator.SetBool("PlayIn", value: false);
		StartIsAnimationDone();
	}

	private void StartIsAnimationDone()
	{
		m_isAnimationDone = false;
		m_animationRoutine = StartCoroutine(IsAnimationDone());
	}

	private IEnumerator IsAnimationDone()
	{
		while (!m_isAnimationDone)
		{
			yield return null;
		}
		OnAnimationDoneCallback?.Invoke();
		m_isAnimationDone = false;
	}

	public void OnBarsClosed()
	{
		m_isAnimationDone = true;
	}
}
