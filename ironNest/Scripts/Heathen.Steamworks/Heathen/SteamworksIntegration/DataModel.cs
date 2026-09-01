using System;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	public abstract class DataModel : ScriptableObject
	{
		public string extension;

		[Header("Events")]
		public UnityEvent evtDataUpdated;

		[NonSerialized]
		public RemoteStorageFile[] AvailableFiles;

		public abstract Type DataType { get; }

		public void Refresh()
		{
		}

		public abstract void LoadByteArray(byte[] data);

		public abstract void LoadJson(string json);

		public abstract void LoadFileAddress(RemoteStorageFile address);

		public abstract void LoadFileAddress(string address);

		public abstract void LoadFileAddressAsync(RemoteStorageFile address, Action<bool> callback);

		public abstract void LoadFileAddressAsync(string address, Action<bool> callback);

		public abstract byte[] ToByteArray();

		public abstract string ToJson();

		public abstract bool Save(string filename);

		public abstract void SaveAsync(string filename, Action<RemoteStorageFileWriteAsyncComplete_t, bool> callback);
	}
	public abstract class DataModel<T> : DataModel
	{
		public T data;

		public override Type DataType => null;

		public override void LoadByteArray(byte[] data)
		{
		}

		public override void LoadJson(string json)
		{
		}

		public override string ToJson()
		{
			return null;
		}

		public override byte[] ToByteArray()
		{
			return null;
		}

		public override void SaveAsync(string filename, Action<RemoteStorageFileWriteAsyncComplete_t, bool> callback)
		{
		}

		public override bool Save(string filename)
		{
			return false;
		}

		public override void LoadFileAddress(RemoteStorageFile address)
		{
		}

		public override void LoadFileAddressAsync(RemoteStorageFile address, Action<bool> callback)
		{
		}

		public override void LoadFileAddress(string address)
		{
		}

		public override void LoadFileAddressAsync(string address, Action<bool> callback)
		{
		}
	}
}
