using System.Collections.Generic;
using System.Diagnostics;
using plog.Handlers;
using plog.Models;

namespace plog
{
	public class Logger
	{
		private readonly List<ILogHandler> _handlers = new List<ILogHandler>();

		public readonly Tag? Tag;

		private Logger? _parent;

		public bool NotifyParent = true;

		public static Logger Root { get; } = new Logger();

		public Logger? Parent
		{
			get
			{
				if (Root == this)
				{
					return null;
				}
				return _parent ?? Root;
			}
			set
			{
				_parent = value;
			}
		}

		public void Info(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Info, extraTags, stackTrace, context);
		}

		public void Fine(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Fine, extraTags, stackTrace, context);
		}

		public void Warning(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Warning, extraTags, stackTrace, context);
		}

		public void Error(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Error, extraTags, stackTrace, context);
		}

		public void Exception(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Exception, extraTags, stackTrace, context);
		}

		public void CommandLine(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.CommandLine, extraTags, stackTrace, context);
		}

		public void Config(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Config, extraTags, stackTrace, context);
		}

		[Conditional("DEBUG")]
		public void Debug(string message, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Record(message, Level.Debug, extraTags, stackTrace, context);
		}

		public Logger()
		{
			Tag = null;
		}

		public Logger(string name)
		{
			Tag = new Tag(name);
		}

		public Logger(object self)
		{
			Tag = new Tag(self);
		}

		public void AddHandler(ILogHandler handler, bool overrideExisting = false)
		{
			if (_handlers.Contains(handler))
			{
				if (!overrideExisting)
				{
					return;
				}
				_handlers.Remove(handler);
			}
			_handlers.Add(handler);
		}

		public bool RemoveHandler(ILogHandler handler)
		{
			return _handlers.Remove(handler);
		}

		public void ClearHandlers()
		{
			_handlers.Clear();
		}

		public void Record(string message, Level level, IEnumerable<Tag>? extraTags = null, string? stackTrace = null, object? context = null)
		{
			Log log = new Log(message, level, extraTags, stackTrace, context);
			Record(log, this);
			if (NotifyParent)
			{
				Parent?.Record(log, this);
			}
		}

		public void Record(Log log)
		{
			Record(log, this);
			if (NotifyParent)
			{
				Parent?.Record(log, this);
			}
		}

		protected void Record(Log log, Logger source)
		{
			foreach (ILogHandler handler in _handlers)
			{
				log = handler.HandleRecord(source, log);
			}
		}
	}
}
