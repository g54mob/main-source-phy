using System;

namespace FMOD
{
	public struct CREATESOUNDEXINFO
	{
		public int cbsize;

		public uint length;

		public uint fileoffset;

		public int numchannels;

		public int defaultfrequency;

		public SOUND_FORMAT format;

		public uint decodebuffersize;

		public int initialsubsound;

		public int numsubsounds;

		public IntPtr inclusionlist;

		public int inclusionlistnum;

		public IntPtr pcmreadcallback_internal;

		public IntPtr pcmsetposcallback_internal;

		public IntPtr nonblockcallback_internal;

		public IntPtr dlsname;

		public IntPtr encryptionkey;

		public int maxpolyphony;

		public IntPtr userdata;

		public SOUND_TYPE suggestedsoundtype;

		public IntPtr fileuseropen_internal;

		public IntPtr fileuserclose_internal;

		public IntPtr fileuserread_internal;

		public IntPtr fileuserseek_internal;

		public IntPtr fileuserasyncread_internal;

		public IntPtr fileuserasynccancel_internal;

		public IntPtr fileuserdata;

		public int filebuffersize;

		public CHANNELORDER channelorder;

		public IntPtr initialsoundgroup;

		public uint initialseekposition;

		public TIMEUNIT initialseekpostype;

		public int ignoresetfilesystem;

		public uint audioqueuepolicy;

		public uint minmidigranularity;

		public int nonblockthreadid;

		public IntPtr fsbguid;

		public SOUND_PCMREAD_CALLBACK pcmreadcallback
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public SOUND_PCMSETPOS_CALLBACK pcmsetposcallback
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public SOUND_NONBLOCK_CALLBACK nonblockcallback
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_OPEN_CALLBACK fileuseropen
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_CLOSE_CALLBACK fileuserclose
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_READ_CALLBACK fileuserread
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_SEEK_CALLBACK fileuserseek
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_ASYNCREAD_CALLBACK fileuserasyncread
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public FILE_ASYNCCANCEL_CALLBACK fileuserasynccancel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
	}
}
