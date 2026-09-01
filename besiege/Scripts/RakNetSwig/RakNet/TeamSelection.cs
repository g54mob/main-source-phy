using System;
using System.Runtime.InteropServices;

namespace RakNet
{
	public class TeamSelection : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public JoinTeamType joinTeamType
		{
			get
			{
				return (JoinTeamType)RakNetPINVOKE.TeamSelection_joinTeamType_get(swigCPtr);
			}
			set
			{
				RakNetPINVOKE.TeamSelection_joinTeamType_set(swigCPtr, (int)value);
			}
		}

		internal TeamSelection(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(TeamSelection obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~TeamSelection()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						RakNetPINVOKE.delete_TeamSelection(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		public TeamSelection()
			: this(RakNetPINVOKE.new_TeamSelection__SWIG_0(), true)
		{
		}

		public TeamSelection(JoinTeamType itt)
			: this(RakNetPINVOKE.new_TeamSelection__SWIG_1((int)itt), true)
		{
		}

		public TeamSelection(JoinTeamType itt, TM_Team param)
			: this(RakNetPINVOKE.new_TeamSelection__SWIG_2((int)itt, TM_Team.getCPtr(param)), true)
		{
		}

		public TeamSelection(JoinTeamType itt, byte param)
			: this(RakNetPINVOKE.new_TeamSelection__SWIG_3((int)itt, param), true)
		{
		}

		public static TeamSelection AnyAvailable()
		{
			return new TeamSelection(RakNetPINVOKE.TeamSelection_AnyAvailable(), true);
		}

		public static TeamSelection SpecificTeam(TM_Team specificTeamToJoin)
		{
			return new TeamSelection(RakNetPINVOKE.TeamSelection_SpecificTeam(TM_Team.getCPtr(specificTeamToJoin)), true);
		}

		public static TeamSelection NoTeam(byte noTeamSubcategory)
		{
			return new TeamSelection(RakNetPINVOKE.TeamSelection_NoTeam(noTeamSubcategory), true);
		}
	}
}
