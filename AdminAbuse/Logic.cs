using System;
using Smod2;
using Smod2.API;
using scp4aiur;
using System.Linq;
using System.Collections.Generic;
using RemoteAdmin;
using Object = UnityEngine.Object;

namespace AdminAbuse
{
	static class Logic
	{
		public static void Snap(int num)
		{
			List<Player> PlayerList = PluginManager.Manager.Server.GetPlayers();
			List<Player> KillPlayers = new List<Player>();
			Random rand = new Random();

			for (int i = 0; i < num; i++)
			{
				if (PlayerList.Count > 0)
				{
					Player player = PlayerList[rand.Next(PlayerList.Count)];
					player.Kill();
					PlayerList.Remove(player);
				}
			}
		}

		public static void FlapGenerators(float innacuracy = 0)
		{
			foreach (Generator079 generator in Plugin.generators)
				generator.NetworkisDoorOpen = !generator.NetworkisDoorOpen;
			if (Plugin.tFlappyBird)
				Timing.In(FlapGenerators, 1.5f);
			else
				foreach (Generator079 generator in Plugin.generators)
					generator.NetworkisDoorOpen = false;
		}

		public static void PrepBomberman()
		{
			Plugin.PlayerList = PluginManager.Manager.Server.GetPlayers();
			RunBomberman();
		}

		public static void RunBomberman(float innacuracy = 0)
		{
			if (Plugin.tBomberman)
			{
				foreach (Player player in Plugin.PlayerList.Where(x => x.TeamRole.Role != Role.SPECTATOR))
				{
					player.ThrowGrenade(ItemType.FRAG_GRENADE, false, new Vector(0.0f, 0.0f, 0.0f), true, player.GetPosition(), true, 0.0f, false);
				}
				Timing.In(RunBomberman, 1f);
			}
			else
			{
				if (Plugin.pBomberman.Count > 0)
				{
					foreach (Player player in Plugin.pBomberman.Select(FindPlayer).Where(x => x.TeamRole.Role != Role.SPECTATOR))
					{
						player.ThrowGrenade(ItemType.FRAG_GRENADE, false, new Vector(0.0f, 0.0f, 0.0f), true, player.GetPosition(), true, 0.0f, false);
					}
					Timing.In(RunBomberman, 1f);
				}
			}
		}

		public static Player FindPlayer(string steamid)
		{
			foreach (Player player in PluginManager.Manager.Server.GetPlayers().Where(x => x.SteamId == steamid))
				return player;
			return null;
		}

		public static int LevenshteinDistance(string s, string t)
		{
			int n = s.Length;
			int m = t.Length;
			int[,] d = new int[n + 1, m + 1];

			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			for (int i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (int j = 0; j <= m; d[0, j] = j++)
			{
			}

			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= m; j++)
				{
					int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			return d[n, m];
		}

		public static Player GetPlayer(string args, out Player playerOut)
		{
			int maxNameLength = 31, LastnameDifference = 31;
			Player plyer = null;
			string str1 = args.ToLower();
			foreach (Player pl in PluginManager.Manager.Server.GetPlayers(str1))
			{
				if (!pl.Name.ToLower().Contains(args.ToLower())) { goto NoPlayer; }
				if (str1.Length < maxNameLength)
				{
					int x = maxNameLength - str1.Length;
					int y = maxNameLength - pl.Name.Length;
					string str2 = pl.Name;
					for (int i = 0; i < x; i++)
					{
						str1 += "z";
					}
					for (int i = 0; i < y; i++)
					{
						str2 += "z";
					}
					int nameDifference = LevenshteinDistance(str1, str2);
					if (nameDifference < LastnameDifference)
					{
						LastnameDifference = nameDifference;
						plyer = pl;
					}
				}
				NoPlayer:;
			}
			playerOut = plyer;
			return playerOut;
		}
	}
}
