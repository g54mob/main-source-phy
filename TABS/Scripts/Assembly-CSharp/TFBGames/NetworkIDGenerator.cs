using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class NetworkIDGenerator
	{
		private readonly long m_rangeMin;

		private readonly long m_rangeMax;

		private long m_nextID;

		private readonly HashSet<int> m_usedIDs;

		public NetworkIDGenerator(int rangeMin, int rangeMax, bool keepListOfUsedIds = false)
		{
			m_rangeMin = rangeMin;
			m_rangeMax = rangeMax;
			m_nextID = m_rangeMin;
			if (keepListOfUsedIds)
			{
				m_usedIDs = new HashSet<int>();
			}
		}

		public int GetNextID()
		{
			long nextID;
			if (m_usedIDs == null)
			{
				nextID = m_nextID;
				m_nextID++;
				if (m_nextID > m_rangeMax)
				{
					m_nextID = m_rangeMin;
				}
				return (int)nextID;
			}
			nextID = m_nextID;
			long num = m_rangeMax - m_rangeMin + 1;
			bool flag = false;
			while (num > 0)
			{
				if (!m_usedIDs.Contains((int)nextID))
				{
					flag = true;
					break;
				}
				nextID++;
				if (nextID > m_rangeMax)
				{
					nextID = m_rangeMin;
				}
				num--;
			}
			m_nextID = nextID + 1;
			if (m_nextID > m_rangeMax)
			{
				m_nextID = m_rangeMin;
			}
			if (flag)
			{
				m_usedIDs.Add((int)nextID);
			}
			else
			{
				Debug.LogError("No more unique IDs left. Did you call FreedID to free IDs?");
			}
			return (int)nextID;
		}

		public void FreedID(int id)
		{
			m_usedIDs.Remove(id);
		}
	}
}
