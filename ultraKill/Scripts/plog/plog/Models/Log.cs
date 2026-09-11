using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace plog.Models
{
	public record Log(string Message, Level Level, IEnumerable<Tag>? ExtraTags = null, string? StackTrace = null, object? Context = null)
	{
		public readonly Level Level = Level;

		public readonly string Message = Message;

		public object? Context = Context;

		public IEnumerable<Tag>? ExtraTags = ExtraTags;

		public string? StackTrace = StackTrace;

		public DateTime Timestamp = DateTime.UtcNow;

		[CompilerGenerated]
		public void Deconstruct(out string Message, out Level Level, out IEnumerable<Tag>? ExtraTags, out string? StackTrace, out object? Context)
		{
			Message = this.Message;
			Level = this.Level;
			ExtraTags = this.ExtraTags;
			StackTrace = this.StackTrace;
			Context = this.Context;
		}
	}
}
