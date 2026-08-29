using System.Collections.Generic;

public class Tags
{
	public class Tag
	{
		public string name;

		public bool isActive;

		public bool isAny;

		public bool doNotDisplayInPublish;
	}

	public const string tagInGameWalkthrough = "tagInGameWalkthrough";

	public List<Tag> playTimeTagsList = new List<Tag>
	{
		new Tag
		{
			name = "Any",
			doNotDisplayInPublish = true
		},
		new Tag
		{
			name = "tagQuickLength"
		},
		new Tag
		{
			name = "tagMediumLength"
		},
		new Tag
		{
			name = "tagLongLength"
		}
	};

	public List<Tag> difficultyTagsList = new List<Tag>
	{
		new Tag
		{
			name = "Any",
			doNotDisplayInPublish = true
		},
		new Tag
		{
			name = "tagEasy"
		},
		new Tag
		{
			name = "tagMedium"
		},
		new Tag
		{
			name = "tagHard"
		}
	};

	public List<Tag> languagesTagsList = new List<Tag>
	{
		new Tag
		{
			name = "Any",
			doNotDisplayInPublish = true
		},
		new Tag
		{
			name = "English"
		},
		new Tag
		{
			name = "简体中文 (Simplified Chinese)"
		},
		new Tag
		{
			name = "繁體中文 (Traditional Chinese)"
		},
		new Tag
		{
			name = "日本語 (Japanese)"
		},
		new Tag
		{
			name = "한국어 (Korean)"
		},
		new Tag
		{
			name = "Deutsch (German)"
		},
		new Tag
		{
			name = "Francais (French)"
		},
		new Tag
		{
			name = "Türk (Turkish)"
		},
		new Tag
		{
			name = "tagOtherLanguages"
		}
	};

	public List<Tag> themesTagsList = new List<Tag>
	{
		new Tag
		{
			name = "Any",
			doNotDisplayInPublish = true
		},
		new Tag
		{
			name = "AnyButHorror",
			doNotDisplayInPublish = true
		},
		new Tag
		{
			name = "tagHistoric"
		},
		new Tag
		{
			name = "tagModern"
		},
		new Tag
		{
			name = "tagFuturistic"
		},
		new Tag
		{
			name = "tagHorror"
		},
		new Tag
		{
			name = "tagAdventure"
		},
		new Tag
		{
			name = "tagMystery"
		},
		new Tag
		{
			name = "tagMeme"
		},
		new Tag
		{
			name = "tagOther"
		}
	};

	public List<Tag> miscellaneousTagsList = new List<Tag>
	{
		new Tag
		{
			name = "tagNumberGame"
		},
		new Tag
		{
			name = "tagOtherGame"
		},
		new Tag
		{
			name = "tagExternalKnowledgeRequired"
		},
		new Tag
		{
			name = "tagSoundRequired"
		},
		new Tag
		{
			name = "tagReadingRequired"
		},
		new Tag
		{
			name = "tagColorblindFrendly"
		},
		new Tag
		{
			name = "tagHasProfanity"
		}
	};

	public List<Tag> minNumPlayersTagsList = new List<Tag>
	{
		new Tag
		{
			name = "tagMin1"
		},
		new Tag
		{
			name = "tagMin2"
		},
		new Tag
		{
			name = "tagMin3"
		},
		new Tag
		{
			name = "tagMin4"
		},
		new Tag
		{
			name = "tagMin5"
		},
		new Tag
		{
			name = "tagMin6"
		},
		new Tag
		{
			name = "tagMin7"
		},
		new Tag
		{
			name = "tagMin8"
		}
	};

	public List<Tag> maxNumPlayersTagsList = new List<Tag>
	{
		new Tag
		{
			name = "tagMax1"
		},
		new Tag
		{
			name = "tagMax2"
		},
		new Tag
		{
			name = "tagMax3"
		},
		new Tag
		{
			name = "tagMax4"
		},
		new Tag
		{
			name = "tagMax5"
		},
		new Tag
		{
			name = "tagMax6"
		},
		new Tag
		{
			name = "tagMax7"
		},
		new Tag
		{
			name = "tagMax8"
		}
	};

	public List<Tag> pineTagsList = new List<Tag>
	{
		new Tag
		{
			name = "tagVerified"
		},
		new Tag
		{
			name = "tagAward"
		}
	};

	public List<Tag> codeAddedTagsList = new List<Tag>
	{
		new Tag
		{
			name = "tagInGameWalkthrough"
		}
	};
}
