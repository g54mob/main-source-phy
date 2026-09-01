using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageController : MonoBehaviour
{
	public ShipMultibodyAI steering;

	public List<ShipPartHitManager> topHullParts = new List<ShipPartHitManager>();

	public List<ShipPartHitManager> bottomHullParts = new List<ShipPartHitManager>();

	public List<ShipPartHitManager> breaking = new List<ShipPartHitManager>();

	private Coroutine routine;

	private bool? checkBottomPieces = false;

	public int piecesBrokenOffToSink = 3;

	private int startCount;

	public void Awake()
	{
		if ((bool)steering)
		{
			ShipMultibodyAI shipMultibodyAI = steering;
			shipMultibodyAI.Crash = (Action)Delegate.Combine(shipMultibodyAI.Crash, new Action(Sink));
			ShipMultibodyAI shipMultibodyAI2 = steering;
			shipMultibodyAI2.TakeInWater = (Action<float>)Delegate.Combine(shipMultibodyAI2.TakeInWater, new Action<float>(TakeInWater));
			startCount = steering.bInfo.Length;
		}
	}

	public void OnPartJointBreak(ShipPartHitManager part)
	{
		if (!breaking.Contains(part))
		{
			breaking.Add(part);
			if (routine == null)
			{
				routine = StartCoroutine(IEProcessPartJointBreak(true));
			}
		}
	}

	public IEnumerator IEProcessPartJointBreak(bool checkInverse)
	{
		yield return new WaitForFixedUpdate();
		foreach (ShipPartHitManager part in breaking)
		{
			ProcessPartJointBreak(part, checkInverse);
		}
		if (checkBottomPieces == true)
		{
			ProcessPartJointBreak(bottomHullParts[0], false);
		}
		checkBottomPieces = null;
		routine = null;
		breaking.Clear();
	}

	public void ProcessPartJointBreak(ShipPartHitManager part, bool checkInverse)
	{
		if (part.sinking && !part.nonCompartmentalizedSinking)
		{
			return;
		}
		int count = 0;
		List<ShipPartHitManager> list = new List<ShipPartHitManager>();
		GetBaseConnectionCount(part, list, ref count);
		switch (count)
		{
		case 0:
		{
			if (!steering)
			{
				break;
			}
			List<BasicInfo> list2 = new List<BasicInfo>(steering.bInfo);
			foreach (ShipPartHitManager item in list)
			{
				list2.Remove(item.basicInfo);
				item.BreakOff();
				item.Sink();
			}
			if (!CheckIfSinking(list2.Count))
			{
				steering.bInfo = list2.ToArray();
				bool? flag = checkBottomPieces;
				if (!flag.HasValue)
				{
					checkBottomPieces = true;
				}
			}
			break;
		}
		case 1:
			list[0].AffectNeighbors();
			Sink();
			checkBottomPieces = false;
			break;
		case 2:
			list[0].AffectNeighbors();
			if (list[1] != null)
			{
				list[1].AffectNeighbors();
			}
			Sink();
			checkBottomPieces = false;
			break;
		case 3:
		{
			List<BasicInfo> list2 = new List<BasicInfo>();
			foreach (ShipPartHitManager item2 in list)
			{
				list2.Add(item2.basicInfo);
			}
			if ((bool)steering)
			{
				BasicInfo[] bInfo2 = steering.bInfo;
				foreach (BasicInfo basicInfo2 in bInfo2)
				{
					if (!list2.Contains(basicInfo2))
					{
						ShipPartHitManager component2 = basicInfo2.GetComponent<ShipPartHitManager>();
						component2.AffectNeighbors();
					}
				}
			}
			Sink();
			checkBottomPieces = false;
			break;
		}
		case 4:
		{
			if (!checkInverse || CheckIfSinking(list.Count))
			{
				break;
			}
			List<BasicInfo> list2 = new List<BasicInfo>();
			foreach (ShipPartHitManager item3 in list)
			{
				list2.Add(item3.basicInfo);
			}
			if ((bool)steering)
			{
				BasicInfo[] bInfo = steering.bInfo;
				foreach (BasicInfo basicInfo in bInfo)
				{
					if (!list2.Contains(basicInfo))
					{
						ShipPartHitManager component = basicInfo.GetComponent<ShipPartHitManager>();
						component.BreakOff();
						component.Sink();
					}
				}
				steering.bInfo = list2.ToArray();
			}
			checkBottomPieces = false;
			break;
		}
		}
	}

	private bool CheckIfSinking(int count)
	{
		if (count <= startCount - piecesBrokenOffToSink)
		{
			Sink();
			checkBottomPieces = false;
			return true;
		}
		return false;
	}

	public void SinkAll(bool completly)
	{
		foreach (ShipPartHitManager topHullPart in topHullParts)
		{
			topHullPart.Sink(completly);
		}
		foreach (ShipPartHitManager bottomHullPart in bottomHullParts)
		{
			bottomHullPart.Sink(completly);
		}
	}

	public void Sink()
	{
		if ((bool)steering)
		{
			steering.broken = true;
			steering.HandleParticles();
		}
		foreach (ShipPartHitManager topHullPart in topHullParts)
		{
			topHullPart.Sink();
		}
		foreach (ShipPartHitManager bottomHullPart in bottomHullParts)
		{
			bottomHullPart.Sink();
		}
	}

	public void TakeInWater(float pct)
	{
		foreach (ShipPartHitManager topHullPart in topHullParts)
		{
			topHullPart.SetWaterTaken(pct);
		}
		foreach (ShipPartHitManager bottomHullPart in bottomHullParts)
		{
			bottomHullPart.SetWaterTaken(pct);
		}
	}

	public void GetBaseConnectionCount(ShipPartHitManager part, List<ShipPartHitManager> burned, ref int count)
	{
		if (part.sinking && !part.nonCompartmentalizedSinking)
		{
			return;
		}
		burned.Add(part);
		if (bottomHullParts.Contains(part))
		{
			count++;
		}
		for (int i = 0; i < part.joints.Count; i++)
		{
			if (!(part.joints[i] == null) && !(part.joints[i].breakForce <= 0f))
			{
				ShipPartHitManager neighbour = GetNeighbour(part.basicInfo.Rigidbody, part.joints[i]);
				if (!burned.Contains(neighbour))
				{
					GetBaseConnectionCount(neighbour, burned, ref count);
				}
			}
		}
	}

	public ShipPartHitManager GetNeighbour(Rigidbody r, Joint j)
	{
		if (j.connectedBody == r)
		{
			return j.GetComponent<ShipPartHitManager>();
		}
		return j.connectedBody.GetComponent<ShipPartHitManager>();
	}
}
