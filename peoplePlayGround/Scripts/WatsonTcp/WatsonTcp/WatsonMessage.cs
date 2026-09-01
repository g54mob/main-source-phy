using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WatsonTcp
{
	internal class WatsonMessage
	{
		[JsonProperty("s")]
		public MessageStatus Status;

		[JsonProperty("sreq")]
		public bool? SyncRequest;

		[JsonProperty("sresp")]
		public bool? SyncResponse;

		[JsonProperty("sts")]
		public DateTime? SenderTimestamp;

		[JsonProperty("exp")]
		public DateTime? Expiration;

		[JsonProperty("guid")]
		public string ConversationGuid;

		private Action<Severity, string> _Logger;

		private string _Header = "[WatsonMessage] ";

		private string _DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffzzz";

		private int _ReadStreamBuffer = 65536;

		private byte[] _PresharedKey;

		private Dictionary<object, object> _Metadata;

		private Stream _DataStream;

		[JsonProperty("len")]
		public long ContentLength { get; set; }

		[JsonProperty("psk")]
		public byte[] PresharedKey
		{
			get
			{
				return _PresharedKey;
			}
			set
			{
				if (value == null)
				{
					_PresharedKey = null;
					return;
				}
				if (value.Length != 16)
				{
					throw new ArgumentException("PresharedKey must be 16 bytes.");
				}
				_PresharedKey = new byte[16];
				Buffer.BlockCopy(value, 0, _PresharedKey, 0, 16);
			}
		}

		[JsonProperty("md")]
		public Dictionary<object, object> Metadata
		{
			get
			{
				return _Metadata;
			}
			set
			{
				_Metadata = value;
			}
		}

		[JsonIgnore]
		public Stream DataStream => _DataStream;

		[JsonIgnore]
		public byte[] HeaderBytes
		{
			get
			{
				string s = SerializationHelper.SerializeJson(this, pretty: false);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n\r\n");
				return WatsonCommon.AppendBytes(bytes, bytes2);
			}
		}

		internal int BufferSize
		{
			get
			{
				return _ReadStreamBuffer;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("ReadStreamBuffer must be greater than zero bytes.");
				}
				_ReadStreamBuffer = value;
			}
		}

		internal WatsonMessage()
		{
			Status = MessageStatus.Normal;
		}

		internal WatsonMessage(Dictionary<object, object> metadata, long contentLength, Stream stream, bool syncRequest, bool syncResponse, DateTime? expiration, string convGuid, Action<Severity, string> logger)
		{
			if (contentLength < 0)
			{
				throw new ArgumentException("Content length must be zero or greater.");
			}
			if (contentLength > 0 && (stream == null || !stream.CanRead))
			{
				throw new ArgumentException("Cannot read from supplied stream.");
			}
			Status = MessageStatus.Normal;
			ContentLength = contentLength;
			Metadata = metadata;
			if (syncRequest)
			{
				SyncRequest = true;
			}
			if (syncResponse)
			{
				SyncResponse = true;
			}
			Expiration = expiration;
			ConversationGuid = convGuid;
			if (SyncRequest.HasValue && SyncRequest.Value)
			{
				SenderTimestamp = DateTime.Now;
			}
			_DataStream = stream;
			_Logger = logger;
		}

		internal WatsonMessage(Stream stream, Action<Severity, string> logger)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Cannot read from stream.");
			}
			Status = MessageStatus.Normal;
			_DataStream = stream;
			_Logger = logger;
		}

		internal async Task<bool> BuildFromStream(CancellationToken token)
		{
			byte[] headerBytes = new byte[24];
			try
			{
				await _DataStream.ReadAsync(headerBytes, 0, 24, token).ConfigureAwait(continueOnCapturedContext: false);
				byte[] headerBuffer = new byte[1];
				while (true)
				{
					byte[] array = headerBytes.Skip(headerBytes.Length - 4).Take(4).ToArray();
					if (array[3] == 0 && array[2] == 0 && array[1] == 0 && array[0] == 0)
					{
						_Logger?.Invoke(Severity.Debug, _Header + "null header data, peer disconnect detected");
						return false;
					}
					if (array[3] == 10 && array[2] == 13 && array[1] == 10 && array[0] == 13)
					{
						break;
					}
					await _DataStream.ReadAsync(headerBuffer, 0, 1, token).ConfigureAwait(continueOnCapturedContext: false);
					headerBytes = WatsonCommon.AppendBytes(headerBytes, headerBuffer);
				}
				_Logger?.Invoke(Severity.Debug, _Header + "found header demarcation");
				WatsonMessage watsonMessage = SerializationHelper.DeserializeJson<WatsonMessage>(Encoding.UTF8.GetString(headerBytes));
				ContentLength = watsonMessage.ContentLength;
				PresharedKey = watsonMessage.PresharedKey;
				Status = watsonMessage.Status;
				Metadata = watsonMessage.Metadata;
				SyncRequest = watsonMessage.SyncRequest;
				SyncResponse = watsonMessage.SyncResponse;
				SenderTimestamp = watsonMessage.SenderTimestamp;
				Expiration = watsonMessage.Expiration;
				ConversationGuid = watsonMessage.ConversationGuid;
				_Logger?.Invoke(Severity.Debug, _Header + "header processing complete" + Environment.NewLine + Encoding.UTF8.GetString(headerBytes).Trim());
				return true;
			}
			catch (TaskCanceledException)
			{
				_Logger?.Invoke(Severity.Debug, _Header + "message read canceled");
				return false;
			}
			catch (OperationCanceledException)
			{
				_Logger?.Invoke(Severity.Debug, _Header + "message read canceled");
				return false;
			}
			catch (ObjectDisposedException)
			{
				_Logger?.Invoke(Severity.Debug, _Header + "socket disposed");
				return false;
			}
			catch (IOException)
			{
				_Logger?.Invoke(Severity.Debug, _Header + "non-graceful termination by peer");
				return false;
			}
			catch (Exception obj)
			{
				_Logger?.Invoke(Severity.Error, _Header + "exception encountered: " + Environment.NewLine + "Header bytes: " + BitConverter.ToString(headerBytes).Replace("-", string.Empty) + Environment.NewLine + "Exception: " + SerializationHelper.SerializeJson(obj, pretty: true) + Environment.NewLine);
				return false;
			}
		}

		public override string ToString()
		{
			string text = "---" + Environment.NewLine;
			text = text + "  Preshared key     : " + ((PresharedKey != null) ? WatsonCommon.ByteArrayToHex(PresharedKey) : "null") + Environment.NewLine;
			text = text + "  Status            : " + Status.ToString() + Environment.NewLine;
			text = text + "  SyncRequest       : " + SyncRequest + Environment.NewLine;
			text = text + "  SyncResponse      : " + SyncResponse + Environment.NewLine;
			text = text + "  ExpirationUtc     : " + (Expiration.HasValue ? Expiration.Value.ToString(_DateTimeFormat) : "null") + Environment.NewLine;
			text = text + "  Conversation      : " + ConversationGuid + Environment.NewLine;
			if (Metadata != null)
			{
				text = text + "  Metadata          : " + Metadata.Count + " entries" + Environment.NewLine;
			}
			if (DataStream != null)
			{
				text = text + "  DataStream        : present, " + ContentLength + " bytes" + Environment.NewLine;
			}
			return text;
		}
	}
}
