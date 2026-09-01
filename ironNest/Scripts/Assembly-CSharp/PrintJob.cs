using System;
using System.Collections.Generic;

[Serializable]
public class PrintJob
{
	public long jobId;

	public string sourceId;

	public List<string> lines;

	public DateTime submittedUtc;

	public object userData;

	public bool complete;

	public PrintJob(long jobId, string sourceId, IEnumerable<string> lines, object userData = null)
	{
	}
}
