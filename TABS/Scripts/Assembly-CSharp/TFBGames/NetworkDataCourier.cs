using System;
using System.Collections.Generic;
using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class NetworkDataCourier : GlobalEventListener, INetworkDataCourier, IService
	{
		private const int MaxBandwidth = 20480;

		private const string TextureChannelName = "TextureChannel";

		private const int TextureChannelPriority = 4;

		private UdpChannelName m_textureChannel;

		private List<byte> m_bytes = new List<byte>();

		public event TextureReceivedEventHandler TextureReceived;

		public void SendTexture(NetworkTextureType textureType, Texture2D texture)
		{
			if (!BoltNetwork.IsRunning || texture == null)
			{
				return;
			}
			BoltConnection boltConnection = null;
			if (BoltNetwork.IsClient)
			{
				boltConnection = BoltNetwork.Server;
			}
			else if (BoltNetwork.IsServer && BoltNetwork.Clients != null)
			{
				using (IEnumerator<BoltConnection> enumerator = BoltNetwork.Clients.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						boltConnection = enumerator.Current;
					}
				}
			}
			if (boltConnection != null)
			{
				byte[] data = PackTexture(textureType, texture);
				boltConnection.StreamBytes(m_textureChannel, data);
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public override bool PersistBetweenStartupAndShutdown()
		{
			return true;
		}

		public override void BoltStartBegin()
		{
			m_textureChannel = BoltNetwork.CreateStreamChannel("TextureChannel", UdpChannelMode.Reliable, 4);
		}

		public override void Connected(BoltConnection connection)
		{
			connection.SetStreamBandwidth(20480);
		}

		public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
		{
			if (data.Channel.Equals(m_textureChannel))
			{
				OnTextureReceived(data);
			}
		}

		private void OnTextureReceived(UdpStreamData data)
		{
			if (this.TextureReceived != null)
			{
				NetworkTextureType textureType;
				Texture2D texture = UnpackTexture(data.Data, out textureType);
				this.TextureReceived?.Invoke(textureType, texture);
			}
		}

		private byte[] PackTexture(NetworkTextureType textureType, Texture2D texture)
		{
			m_bytes.Clear();
			m_bytes.Add((byte)textureType);
			byte[] bytes = BitConverter.GetBytes((ushort)texture.width);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)bytes);
			}
			m_bytes.AddRange(bytes);
			bytes = BitConverter.GetBytes((ushort)texture.height);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)bytes);
			}
			m_bytes.AddRange(bytes);
			m_bytes.Add((byte)texture.format);
			m_bytes.AddRange(texture.GetRawTextureData());
			return m_bytes.ToArray();
		}

		private Texture2D UnpackTexture(byte[] bytes, out NetworkTextureType textureType)
		{
			int num = 0;
			textureType = (NetworkTextureType)bytes[num++];
			byte[] array = new byte[2];
			Buffer.BlockCopy(bytes, num, array, 0, 2);
			num += 2;
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)array);
			}
			byte[] array2 = new byte[2];
			Buffer.BlockCopy(bytes, num, array2, 0, 2);
			num += 2;
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)array2);
			}
			Vector2Int vector2Int = new Vector2Int(BitConverter.ToUInt16(array, 0), BitConverter.ToUInt16(array2, 0));
			TextureFormat textureFormat = (TextureFormat)bytes[num++];
			int num2 = bytes.Length - num;
			byte[] array3 = new byte[num2];
			Buffer.BlockCopy(bytes, num, array3, 0, num2);
			Texture2D texture2D = new Texture2D(vector2Int.x, vector2Int.y, textureFormat, mipChain: false);
			texture2D.LoadRawTextureData(array3);
			texture2D.Apply();
			return texture2D;
		}
	}
}
