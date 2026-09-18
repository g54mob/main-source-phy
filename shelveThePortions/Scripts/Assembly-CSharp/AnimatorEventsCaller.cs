using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventsCaller : MonoBehaviour
{
	public UnityEvent event1;

	public UnityEvent event2;

	public UnityEvent event3;

	public UnityEvent event4;

	public UnityEvent event5;

	public UnityEvent event6;

	public UnityEvent event7;

	public UnityEvent event8;

	public void Event1()
	{
		event1.Invoke();
	}

	public void Event2()
	{
		event2.Invoke();
	}

	public void Event3()
	{
		event3.Invoke();
	}

	public void Event4()
	{
		event4.Invoke();
	}

	public void Event5()
	{
		event5.Invoke();
	}

	public void Event6()
	{
		event6.Invoke();
	}

	public void Event7()
	{
		event7.Invoke();
	}

	public void Event8()
	{
		event8.Invoke();
	}

	public void PlayAudioPotionDrop()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Drop);
	}

	public void PlayAudioPotionShelf()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.Potion_Shelve);
	}
}
