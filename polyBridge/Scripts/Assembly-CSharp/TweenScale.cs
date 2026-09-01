using System.Collections;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
	public Vector3 m_ScaleTo;

	public float m_Delay;

	public float m_Time;

	public iTween.EaseType m_EaseType;

	public iTween.LoopType m_LoopType;

	public bool m_PlayOnEnable;

	private Vector3 m_OriginalScale;

	private bool m_Awake;

	private void Awake()
	{
		m_OriginalScale = base.transform.localScale;
		m_Awake = true;
	}

	private void OnEnable()
	{
		if (m_PlayOnEnable)
		{
			Play();
		}
	}

	public void Play()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("scale", m_ScaleTo);
		hashtable.Add("time", m_Time);
		hashtable.Add("delay", m_Delay);
		hashtable.Add("easetype", m_EaseType);
		hashtable.Add("looptype", m_LoopType);
		hashtable.Add("ignoretimescale", true);
		iTween.ScaleTo(base.gameObject, hashtable);
	}

	public void PlayReverse()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("scale", m_OriginalScale);
		hashtable.Add("time", m_Time);
		hashtable.Add("delay", m_Delay);
		hashtable.Add("easetype", m_EaseType);
		hashtable.Add("looptype", m_LoopType);
		hashtable.Add("ignoretimescale", true);
		iTween.ScaleTo(base.gameObject, hashtable);
	}

	public void Stop()
	{
		iTween.Stop(base.gameObject, "scale");
	}

	public void Reset()
	{
		if (m_Awake)
		{
			base.transform.localScale = m_OriginalScale;
		}
	}

	public void SetOriginalScale(Vector3 newOrigScale)
	{
		m_OriginalScale = newOrigScale;
	}
}
