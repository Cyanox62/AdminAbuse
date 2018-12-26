using Smod2.Commands;
using System.Collections.Generic;
using Smod2.API;
using scp4aiur;
using System.Linq;

namespace AdminAbuse
{
	class CommandHandler : ICommandHandler
	{
		public string GetUsage()
		{
			return "AA / ADMINABUSE";
		}

		public string GetCommandDescription()
		{
			return "Allows you to abuse your players.";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			bool isPlayerValid = sender is Server;

			Player player = sender as Player;
			if (!isPlayerValid && player != null)
			{
				isPlayerValid = Plugin.validRanks.Contains(player.GetRankName());
			}

			if (isPlayerValid)
			{
				if (args.Length > 0)
				{
					switch (args[0])
					{
						case "snap":
							if (args.Length > 1)
							{
								if (int.TryParse(args[1], out int a))
								{
									Logic.Snap(a);
									return new[] { $"Snapped {a} players." };
								}
							}
							else
							{
								return new[] { "You must specify a size." };
							}
							break;

						case "bubblebullets":
						case "bb":
							PlayerToggle bbpt = new PlayerToggle(args, Plugin.pBubbleBullets, ref Plugin.tBubbleBullets, "bubble bullets");
							return bbpt.ReturnString();

						case "flappybird":
							Plugin.tFlappyBird = !Plugin.tFlappyBird;
							Logic.FlapGenerators();
							break;

						case "bomberman":
							PlayerToggle bmpt = new PlayerToggle(args, Plugin.pBomberman, ref Plugin.tBomberman, "bomberman");
							Logic.PrepBomberman();
							return bmpt.ReturnString();
					}
				}
				else
				{
					return new string[] { GetUsage() };
				}
			}
			return new[] { "Your rank is not allowed to run this command!" };
		}
	}
}
