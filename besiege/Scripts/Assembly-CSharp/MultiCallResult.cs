using System;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using UnityEngine;

public class MultiCallResult<T> : IDisposable
{
	public delegate void APIDispatchDelegate(SteamAPICall_t callHandle, T param, bool bIOFailure);

	private Dictionary<SteamAPICall_t, CallResult<T>> callResults = new Dictionary<SteamAPICall_t, CallResult<T>>();

	private Dictionary<SteamAPICall_t, APIDispatchDelegate> callResultHandlers = new Dictionary<SteamAPICall_t, APIDispatchDelegate>();

	public static MultiCallResult<T> Create()
	{
		return new MultiCallResult<T>();
	}

	public void Dispose()
	{
		SteamAPICall_t[] array = callResults.Keys.ToArray();
		SteamAPICall_t[] array2 = array;
		foreach (SteamAPICall_t apiCallHandle in array2)
		{
			Remove(apiCallHandle);
		}
		callResults.Clear();
		callResultHandlers.Clear();
	}

	public void Set(SteamAPICall_t apiCallHandle)
	{
		Set(apiCallHandle, null);
	}

	public void Set(SteamAPICall_t apiCallHandle, APIDispatchDelegate handler)
	{
		if (callResults.ContainsKey(apiCallHandle))
		{
			Debug.LogErrorFormat("[MultiCallResult::Set] tried to set the callback twice for the same handle, type: {0}", typeof(T));
			return;
		}
		CallResult<T> callResult = CallResult<T>.Create(delegate(T param, bool bIOFailure)
		{
			if (callResultHandlers.ContainsKey(apiCallHandle))
			{
				callResultHandlers[apiCallHandle](apiCallHandle, param, bIOFailure);
			}
			Remove(apiCallHandle);
		});
		if (handler != null)
		{
			callResultHandlers.Add(apiCallHandle, handler);
		}
		callResult.Set(apiCallHandle);
		callResults.Add(apiCallHandle, callResult);
	}

	public void Remove(SteamAPICall_t apiCallHandle)
	{
		if (!callResults.ContainsKey(apiCallHandle))
		{
			Debug.LogErrorFormat("[MultiCallResult::Remove] tried to remove unexistent callback, type: {0}", typeof(T));
			return;
		}
		CallResult<T> callResult = callResults[apiCallHandle];
		callResult.Dispose();
		callResultHandlers.Remove(apiCallHandle);
		callResults.Remove(apiCallHandle);
	}
}
