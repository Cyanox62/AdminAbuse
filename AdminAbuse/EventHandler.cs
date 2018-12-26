using Smod2.API;
using System.Linq;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;

namespace AdminAbuse
{
	class EventHandler : IEventHandlerWaitingForPlayers, IEventHandlerPlayerHurt
	{
		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			Plugin.validRanks = Plugin.instance.GetConfigList("aa_ranks");
			Plugin.generators = Generator079.generators;
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			if (Plugin.tBubbleBullets)
			{
				if (ev.Player.SteamId != ev.Attacker.SteamId && ev.DamageType != DamageType.FRAG)
					ev.Damage = 0;
			}
			else if (Plugin.pBubbleBullets.Contains(ev.Attacker.SteamId) && ev.Player.SteamId != ev.Attacker.SteamId && ev.DamageType != DamageType.FRAG)
				ev.Damage = 0;
		}
	}
}
