using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Events;
using scp4aiur;
using System.Collections.Generic;

namespace AdminAbuse
{
	[PluginDetails(
	author = "4aiur, Cyanox",
	name = "AdminAbuse",
	description = "",
	id = "4aiur.cyanox.admin.abuse",
	version = "1.0.0",
	SmodMajor = 3,
	SmodMinor = 2,
	SmodRevision = 0)]

	public class Plugin : Smod2.Plugin
    {
		public static Plugin instance;

		public static string[] validRanks;
		public static List<Player> PlayerList = new List<Player>();
		public static List<string> pBubbleBullets = new List<string>();
		public static List<string> pBomberman = new List<string>();
		public static List<Generator079> generators;

		public static bool tBubbleBullets = false;
		public static bool tBomberman = false;
		public static bool tFlappyBird = false;

		public override void OnEnable() { }

		public override void OnDisable() { }

		public override void Register()
		{
			instance = this;

			Timing.Init(this);

			AddConfig(new ConfigSetting("aa_ranks", new[] { "owner", "admin" }, SettingType.LIST, true, "Valid ranks for the ADMINABUSE command."));

			AddEventHandlers(new EventHandler());

			AddCommands(new string[] { "aa", "adminabuse" }, new CommandHandler());
		}
	}
}
