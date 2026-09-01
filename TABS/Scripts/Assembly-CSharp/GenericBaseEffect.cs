using UnityEngine.Events;

public class GenericBaseEffect : UnitEffectBase
{
	public UnityEvent InitEvent;

	public UnityEvent PingEvent;

	public UnityEvent AnyEvent;

	public override void DoEffect()
	{
		InitEvent.Invoke();
		AnyEvent.Invoke();
	}

	public override void Ping()
	{
		PingEvent.Invoke();
		AnyEvent.Invoke();
	}
}
