using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenAnimated : MonoBehaviour
{
	public AudioSource m_AudioSource;

	public SplashScreenAnimated m_SplashScreenAnimated;

	public float offset;

	public float circleOffset;

	public float circleTime;

	public float leavesContainerTime;

	public float leavesUnfoldTime;

	public float offsetOuterLeaves;

	public float offsetBlob;

	public float blobScaleTime;

	public float offsetText;

	public float textTime;

	public float offsetCallback;

	public RectTransform blobContainer;

	public RectTransform innerLeavesContainer;

	public RectTransform outerLeavesContainer;

	public RectTransform[] innerLeaves;

	public RectTransform[] outerLeaves;

	public Image circleImage;

	public RectTransform mask;

	public RectTransform text;

	private Sequence m_sequence;

	private bool m_Finished;

	private float m_VolumeNormalized;

	private void Awake()
	{
		m_AudioSource.clip.LoadAudioData();
	}

	private void Update()
	{
		if (GameInput.AnyKeyDown() && m_sequence != null)
		{
			m_sequence.Complete(withCallbacks: true);
			m_Finished = true;
		}
	}

	private void OnDisable()
	{
		m_sequence.Complete();
		m_Finished = true;
	}

	public bool IsFinished()
	{
		return m_Finished;
	}

	public void Animate(float volumeNormalized)
	{
		m_VolumeNormalized = volumeNormalized;
		blobContainer.localScale = Vector3.zero;
		outerLeavesContainer.localScale = Vector3.zero;
		innerLeavesContainer.localScale = Vector3.zero;
		for (int i = 0; i < innerLeaves.Length; i++)
		{
			innerLeaves[i].localRotation = Quaternion.identity;
			outerLeaves[i].localRotation = Quaternion.identity;
		}
		Color color = circleImage.color;
		color.a = 0f;
		circleImage.color = color;
		mask.localPosition = new Vector3(-2000f, 0f, 0f);
		text.localPosition = new Vector3(2000f, 0f, 0f);
		color.a = 1f;
		m_sequence = DOTween.Sequence();
		m_sequence.Insert(offset, circleImage.DOColor(color, circleTime).OnPlay(AnimStarted));
		m_sequence.Insert(offset + circleOffset, innerLeavesContainer.DOScale(new Vector3(1f, 1f, 1f), leavesContainerTime).SetEase(Ease.OutSine));
		for (int j = 0; j < innerLeaves.Length; j++)
		{
			m_sequence.Insert(offset + circleOffset, innerLeaves[j].DOLocalRotate(new Vector3(0f, 0f, (j + 1) * 60), leavesUnfoldTime, RotateMode.LocalAxisAdd).SetEase(Ease.OutExpo));
		}
		m_sequence.Insert(offset + offsetOuterLeaves + circleOffset, outerLeavesContainer.DOScale(new Vector3(1f, 1f, 1f), leavesContainerTime).SetEase(Ease.OutSine));
		for (int k = 0; k < outerLeaves.Length; k++)
		{
			m_sequence.Insert(offset + offsetOuterLeaves + circleOffset, outerLeaves[k].DOLocalRotate(new Vector3(0f, 0f, (k + 1) * 60), leavesUnfoldTime, RotateMode.LocalAxisAdd).SetEase(Ease.OutExpo));
		}
		m_sequence.Insert(offset + offsetOuterLeaves + offsetBlob, blobContainer.DOScale(new Vector3(1f, 1f, 1f), blobScaleTime).SetEase(Ease.OutBounce));
		m_sequence.Insert(offset + offsetOuterLeaves + offsetBlob + offsetText, mask.DOLocalMove(Vector3.zero, textTime).SetEase(Ease.Linear));
		m_sequence.Insert(offset + offsetOuterLeaves + offsetBlob + offsetText, text.DOLocalMove(Vector3.zero, textTime).SetEase(Ease.Linear));
		m_sequence.InsertCallback(offset + offsetOuterLeaves + offsetBlob + offsetText + textTime + offsetCallback, Finish);
	}

	private void AnimStarted()
	{
		if (m_AudioSource.clip.loadState == AudioDataLoadState.Loaded && m_VolumeNormalized > 0.001f)
		{
			m_AudioSource.PlayOneShot(m_AudioSource.clip, m_VolumeNormalized);
		}
	}

	private void Finish()
	{
		m_Finished = true;
	}
}
