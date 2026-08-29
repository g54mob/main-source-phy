using System.Collections.Generic;

public class TriggerEvent
{
	public TriggerEventType type;

	public HashSet<NetPlayerId> playersInTrigger;

	public HashSet<Interactive> interactivesInTrigger;

	public HashSet<NetPlayerId> playersEnteredThisEvent = new HashSet<NetPlayerId>();

	public HashSet<Interactive> interactivesEnteredThisEvent = new HashSet<Interactive>();

	public HashSet<NetPlayerId> playersLeftThisEvent = new HashSet<NetPlayerId>();

	public HashSet<Interactive> interactivesLeftThisEvent = new HashSet<Interactive>();

	public bool wasEmpty
	{
		get
		{
			if (playersInTrigger.SetEquals(playersEnteredThisEvent))
			{
				return interactivesInTrigger.SetEquals(interactivesEnteredThisEvent);
			}
			return false;
		}
	}

	public bool isEmpty
	{
		get
		{
			if (playersInTrigger.Count == 0)
			{
				return interactivesInTrigger.Count == 0;
			}
			return false;
		}
	}

	public TriggerEvent(TriggerEventType type, HashSet<NetPlayerId> playersInTrigger, HashSet<Interactive> interactivesInTrigger)
	{
		this.type = type;
		this.playersInTrigger = playersInTrigger;
		this.interactivesInTrigger = interactivesInTrigger;
	}

	public void addEntered(HashSet<NetPlayerId> playersEnteredThisEvent, HashSet<Interactive> interactivesEnteredThisEvent)
	{
		this.playersEnteredThisEvent = new HashSet<NetPlayerId>(playersEnteredThisEvent);
		this.interactivesEnteredThisEvent = new HashSet<Interactive>(interactivesEnteredThisEvent);
	}

	public void addLeft(HashSet<NetPlayerId> playersLeftThisEvent, HashSet<Interactive> interactivesLeftThisEvent)
	{
		this.playersLeftThisEvent = new HashSet<NetPlayerId>(playersLeftThisEvent);
		this.interactivesLeftThisEvent = new HashSet<Interactive>(interactivesLeftThisEvent);
	}
}
