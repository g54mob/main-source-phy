using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	[DataContract]
	public class Face
	{
		[DataMember(Name = "boundingbox")]
		public BoundingBox BoundingBox { get; set; }

		[DataMember(Name = "confidence")]
		public double Confidence { get; set; }

		[DataMember(Name = "age")]
		public double Age { get; set; }

		[DataMember(Name = "smile")]
		public double Smile { get; set; }

		[DataMember(Name = "glasses")]
		public double Glasses { get; set; }

		[DataMember(Name = "sunglasses")]
		public double Sunglasses { get; set; }

		[DataMember(Name = "beard")]
		public double Beard { get; set; }

		[DataMember(Name = "mustache")]
		public double Mustache { get; set; }

		[DataMember(Name = "eye_closed")]
		public double EyeClosed { get; set; }

		[DataMember(Name = "mouth_open_wide")]
		public double MouthOpenWide { get; set; }

		[DataMember(Name = "beauty")]
		public double Beauty { get; set; }

		[DataMember(Name = "sex")]
		public double Gender { get; set; }

		[DataMember(Name = "race")]
		public Dictionary<string, double> Race { get; set; }

		[DataMember(Name = "emotion")]
		public Dictionary<string, double> Emotion { get; set; }

		[DataMember(Name = "quality")]
		public Dictionary<string, double> Quality { get; set; }

		[DataMember(Name = "pose")]
		public Dictionary<string, double> Pose { get; set; }

		[DataMember(Name = "eye_left")]
		public Point EyeLeftPosition { get; set; }

		[DataMember(Name = "eye_right")]
		public Point EyeRightPosition { get; set; }

		[DataMember(Name = "e_ll")]
		public Point EyeLeft_Left { get; set; }

		[DataMember(Name = "e_lr")]
		public Point EyeLeft_Right { get; set; }

		[DataMember(Name = "e_lu")]
		public Point EyeLeft_Up { get; set; }

		[DataMember(Name = "e_ld")]
		public Point EyeLeft_Down { get; set; }

		[DataMember(Name = "e_rl")]
		public Point EyeRight_Left { get; set; }

		[DataMember(Name = "e_rr")]
		public Point EyeRight_Right { get; set; }

		[DataMember(Name = "e_ru")]
		public Point EyeRight_Up { get; set; }

		[DataMember(Name = "e_rd")]
		public Point EyeRight_Down { get; set; }

		[DataMember(Name = "nose")]
		public Point NosePosition { get; set; }

		[DataMember(Name = "n_l")]
		public Point NoseLeft { get; set; }

		[DataMember(Name = "n_r")]
		public Point NoseRight { get; set; }

		[DataMember(Name = "mouth_l")]
		public Point MouthLeft { get; set; }

		[DataMember(Name = "mouth_r")]
		public Point MouthRight { get; set; }

		[DataMember(Name = "m_u")]
		public Point MouthUp { get; set; }

		[DataMember(Name = "m_d")]
		public Point MouthDown { get; set; }
	}
}
