using UnityEngine;

public class AssetBundleAsyncRequest<T> where T : Object
{
	public AssetBundleRequest request;

	public int frameCountWhenCreated;

	public T asset;

	public bool isDone()
	{
		if (request != null)
		{
			return request.isDone;
		}
		if (frameCountWhenCreated + 5 < Time.frameCount)
		{
			return asset != null;
		}
		return false;
	}

	public T getAsset()
	{
		if (isDone())
		{
			if (request != null)
			{
				return (T)request.asset;
			}
			if (frameCountWhenCreated + 5 < Time.frameCount)
			{
				return asset;
			}
		}
		return null;
	}
}
