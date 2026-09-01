using System;
using System.Collections.Generic;
using Cpp2ILInjected;

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
		List<string> list = new List<string>();
		this.lines = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		this.jobId = jobId;
		this.sourceId = sourceId;
		if (lines != null)
		{
			this.lines.AddRange(lines);
		}
		DateTime utcNow = DateTime.UtcNow;
		object obj = default(object);
		this.userData = obj;
		submittedUtc = utcNow;
	}
}
