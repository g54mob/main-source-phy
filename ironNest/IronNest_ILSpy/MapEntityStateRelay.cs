using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class MapEntityStateRelay : MonoBehaviour
{
	public MapEntityStates State;

	public bool DisableIfActive;

	public List<GameObject> GameObjects;

	public List<Behaviour> Components;

	private EntityLocation entity;

	public void OnEnable()
	{
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806963E0");
		EntityLocation entityLocation = default(EntityLocation);
		entity = entityLocation;
		bool flag = entity != null;
		MapEntityStateRelay mapEntityStateRelay = this;
		if (flag)
		{
			Action value = UpdateVisauls;
			entity.OnStateUpdated -= value;
			EntityLocation entityLocation2 = entity;
			Action b = UpdateVisauls;
			Delegate obj = entityLocation2.OnStateUpdated;
			object obj2 = entityLocation2 + 184;
			bool flag4;
			Delegate obj5 = default(Delegate);
			do
			{
				Delegate obj3 = Delegate.Combine(obj, b);
				bool flag2 = (object)obj3 == null;
				Delegate obj4 = null;
				if (!flag2)
				{
					bool flag3 = (object)obj3.GetType() != typeof(Action);
					obj4 = null;
					if (!flag3)
					{
						obj4 = obj3;
					}
					if ((object)obj4 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						throw new NullReferenceException();
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				flag4 = (object)obj5 != obj;
				obj = obj5;
			}
			while (flag4);
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 201 Invalid \"Jump target not found in method: 0x18046D6C0\"");
			MapEntityStateRelay mapEntityStateRelay2 = default(MapEntityStateRelay);
			mapEntityStateRelay = mapEntityStateRelay2;
		}
		mapEntityStateRelay.enabled = false;
	}

	private void OnDisable()
	{
		if (entity != null)
		{
			Action value = UpdateVisauls;
			entity.OnStateUpdated -= value;
		}
	}

	public void UpdateVisauls()
	{
		//IL_0075: Expected O, but got I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		if (!(entity != null))
		{
			return;
		}
		EntityLocation entityLocation = entity;
		if (entityLocation.Entity == null)
		{
			return;
		}
		MapEntity mapEntity = entityLocation.Entity;
		object obj = State & mapEntity.State;
		object obj2 = obj - State;
		bool flag = obj2 == null;
		bool flag2 = !DisableIfActive;
		bool active = flag;
		if (!flag2)
		{
			object obj3 = obj - State;
			bool flag3 = obj3 == null;
			active = !flag3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		GameObject gameObject = default(GameObject);
		List<Behaviour>.Enumerator enumerator2 = default(List<Behaviour>.Enumerator);
		Behaviour behaviour = default(Behaviour);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)gameObject == null)
				{
					break;
				}
				gameObject.SetActive(active);
				continue;
			}
			enumerator.Dispose();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator2.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((object)behaviour == null)
					{
						break;
					}
					behaviour.enabled = active;
					continue;
				}
				enumerator2.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public MapEntityStateRelay()
	{
		List<GameObject> gameObjects = new List<GameObject>();
		GameObjects = gameObjects;
		Components = new List<Behaviour>();
		base._002Ector();
	}
}
