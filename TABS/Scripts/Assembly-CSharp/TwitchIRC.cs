using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class TwitchIRC : MonoBehaviour
{
	public class MsgEvent : UnityEvent<IRC_MessageData>
	{
	}

	[HideInInspector]
	public string channelName;

	public MsgEvent messageRecievedEvent = new MsgEvent();

	private string server = "irc.chat.twitch.tv";

	private int port = 6667;

	private bool connectedToAuth;

	private string buffer = string.Empty;

	private bool stopThreads;

	private Queue<string> commandQueue = new Queue<string>();

	private List<string> recievedMsgs = new List<string>();

	private Thread inProc;

	private Thread outProc;

	private TcpClient socket;

	public bool IsConnectedToAuth()
	{
		return connectedToAuth;
	}

	public void Disconnect()
	{
		connectedToAuth = false;
		stopThreads = true;
		Thread.Sleep(5);
		socket.Close();
		channelName = string.Empty;
		buffer = string.Empty;
		inProc.Abort();
		outProc.Abort();
	}

	public void StartIRC(string name = "", string pass = "")
	{
		stopThreads = false;
		socket = new TcpClient();
		socket.Connect(server, port);
		if (!socket.Connected)
		{
			UnityEngine.Debug.Log("Failed to connect IRC");
			return;
		}
		NetworkStream networkStream = socket.GetStream();
		StreamReader input = new StreamReader(networkStream);
		StreamWriter output = new StreamWriter(networkStream);
		if (name != "" && pass != "")
		{
			output.WriteLine("PASS " + pass);
			output.WriteLine("NICK " + name);
			output.Flush();
		}
		else
		{
			output.WriteLine("PASS 1234");
			output.WriteLine("NICK JustinFan" + Random.Range(11111, 99999));
			output.Flush();
		}
		outProc = new Thread((ThreadStart)delegate
		{
			IRCOutputProcedure(output);
		});
		outProc.Start();
		inProc = new Thread((ThreadStart)delegate
		{
			IRCInputProcedure(input, networkStream);
		});
		inProc.Start();
	}

	public void FakeMessage(string buffer)
	{
		IRC_MessageData arg = ParseMessage(buffer);
		if (!string.IsNullOrEmpty(arg.command))
		{
			messageRecievedEvent.Invoke(arg);
		}
	}

	private IRC_MessageData ParseMessage(string buffer)
	{
		IRC_MessageData result = default(IRC_MessageData);
		Dictionary<string, string> dictionary = (result.tags = new Dictionary<string, string>());
		int num = 0;
		if (buffer.StartsWith("@"))
		{
			while (num < buffer.Length && buffer[num] != ':')
			{
				int num2 = buffer.IndexOf(';', num + 1);
				if (num2 < 0)
				{
					num2 = buffer.IndexOf(':', num + 1);
					if (num2 < 0)
					{
						return default(IRC_MessageData);
					}
				}
				string[] array = buffer.Substring(num + 1, num2 - num - 1).Split('=');
				if (array.Length == 2 && array[1].Length > 0)
				{
					dictionary.Add(array[0], array[1]);
				}
				num = num2;
			}
		}
		int num3 = buffer.IndexOf(':', num + 1);
		if (num3 < 0)
		{
			num3 = buffer.Length;
		}
		string[] array2 = buffer.Substring(num, num3 - num).Split(' ');
		if (array2.Length < 3)
		{
			return default(IRC_MessageData);
		}
		result.command = array2[1];
		result.channel = array2[2];
		string text = "";
		if (num3 < buffer.Length)
		{
			text = buffer.Substring(num3 + 1);
		}
		result.text = text;
		return result;
	}

	private void IRCInputProcedure(TextReader input, NetworkStream networkStream)
	{
		while (!stopThreads)
		{
			if (!networkStream.DataAvailable)
			{
				continue;
			}
			string text = input.ReadLine();
			if (text.StartsWith("PING "))
			{
				SendCommand(text.Replace("PING", "PONG"));
			}
			else if (text.Split(' ')[1] == "001")
			{
				SendCommand("JOIN #" + channelName);
				SendCommand("CAP REQ :twitch.tv/membership twitch.tv/commands twitch.tv/tags");
				connectedToAuth = true;
			}
			else
			{
				lock (recievedMsgs)
				{
					recievedMsgs.Add(text);
				}
			}
		}
	}

	public void SendChatMessage(string message)
	{
		SendCommand("PRIVMSG #" + channelName + " :" + message);
	}

	private void IRCOutputProcedure(TextWriter output)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		while (!stopThreads)
		{
			lock (commandQueue)
			{
				if (commandQueue.Count > 0 && stopwatch.ElapsedMilliseconds > 1)
				{
					output.WriteLine(commandQueue.Peek());
					output.Flush();
					commandQueue.Dequeue();
					stopwatch.Reset();
					stopwatch.Start();
				}
			}
		}
	}

	public void SendCommand(string cmd)
	{
		lock (commandQueue)
		{
			commandQueue.Enqueue(cmd);
		}
	}

	private void OnEnable()
	{
		stopThreads = false;
	}

	private void OnDisable()
	{
		stopThreads = true;
	}

	private void OnDestroy()
	{
		stopThreads = true;
	}

	private void Update()
	{
		List<string> list;
		lock (recievedMsgs)
		{
			list = new List<string>(recievedMsgs);
			recievedMsgs.Clear();
		}
		if (list.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			IRC_MessageData arg = ParseMessage(list[i]);
			if (!string.IsNullOrEmpty(arg.command))
			{
				messageRecievedEvent.Invoke(arg);
			}
		}
	}
}
