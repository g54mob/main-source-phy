using System;
using Steamworks;
using UnityEngine;

public class SteamManager
{
	public static readonly AppId m_AppId = 1850160;

	private static bool m_Initialized;

	private static AuthTicket m_AuthTicket;

	private static string m_TicketAsString;

	private static float m_NextTimeCancelTicket;

	private static readonly int TICKET_LIFETIME_SECONDS = 1800;

	public static void Init()
	{
		m_Initialized = SteamClientInit();
		m_NextTimeCancelTicket = Time.realtimeSinceStartup + (float)TICKET_LIFETIME_SECONDS;
	}

	public static bool IsLoggedOn()
	{
		try
		{
			return m_Initialized && SteamClient.IsLoggedOn;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static void ShutDown()
	{
		SteamClient.Shutdown();
	}

	public static void UpdateManual()
	{
		SteamClient.RunCallbacks();
		MaybeCancelSteamTicket();
	}

	public static string GetTicket()
	{
		return m_TicketAsString;
	}

	private static bool SteamClientInit()
	{
		try
		{
			SteamClient.Init(m_AppId);
			return true;
		}
		catch (Exception)
		{
			Debug.LogWarning("Facepunch.Steamworks Init failed");
			return false;
		}
	}

	public static void SteamCancelAuthSessionTicket()
	{
		try
		{
			if (m_AuthTicket != null)
			{
				m_AuthTicket.Cancel();
				m_AuthTicket = null;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SteamCancelAuthSessionTicket(): " + ex.Message);
		}
	}

	public static void RegisterTicket(AuthTicket ticket)
	{
		if (ticket != null)
		{
			m_AuthTicket = ticket;
			m_TicketAsString = BitConverter.ToString(m_AuthTicket.Data).Replace("-", string.Empty);
		}
	}

	public static bool HasAuthTicket()
	{
		return m_AuthTicket != null;
	}

	private static void MaybeCancelSteamTicket()
	{
		if (Time.realtimeSinceStartup > m_NextTimeCancelTicket)
		{
			SteamCancelAuthSessionTicket();
			m_NextTimeCancelTicket = Time.realtimeSinceStartup + (float)TICKET_LIFETIME_SECONDS;
		}
	}
}
