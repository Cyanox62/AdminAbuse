using Smod2.Commands;
using System.Collections.Generic;
using Smod2.API;
using scp4aiur;
using System.Linq;

namespace AdminAbuse
{
	class PlayerToggle
	{
		string[] returnString;

		public PlayerToggle(string[] args, List<string> list, ref bool toggle, string optionName)
		{
			if (args.Length > 1)
			{
				Player cPlayer = Logic.GetPlayer(args[1], out cPlayer);
				if (cPlayer != null)
				{
					bool contains = false;
					if (list.Contains(cPlayer.SteamId))
						list.Remove(cPlayer.SteamId);
					else
					{
						list.Add(cPlayer.SteamId);
						contains = true;
					}
					returnString = new string[] { $"Turned {(contains ? "on" : "off")} {optionName} for {cPlayer.Name}." };
				}
				else
				{
					returnString = new string[] { $"Can't find player." };
				}
			}
			else
			{
				toggle = !toggle;
				returnString = new string[] { $"Toggled {optionName} {(toggle ? "on" : "off")}." };
			}
		}

		public string[] ReturnString()
		{
			return returnString;
		}
	}
}
