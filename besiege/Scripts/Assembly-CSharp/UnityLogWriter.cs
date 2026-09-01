using System.IO;
using System.Text;
using UnityEngine;

public class UnityLogWriter : TextWriter
{
	public override Encoding Encoding
	{
		get
		{
			return Encoding.UTF8;
		}
	}

	public override void WriteLine(string value)
	{
		Debug.Log(value);
	}
}
