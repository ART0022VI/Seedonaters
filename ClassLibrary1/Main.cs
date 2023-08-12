using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CedMod.Addons.QuerySystem;
using CedMod.Components;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using MEC;
using Mirror;
using Newtonsoft.Json;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using PluginAPI.Core;
using RemoteAdmin;
using RemoteAdmin.Communication;
using UnityEngine;
using UnityEngine.Networking;
using Utils;
using VoiceChat;
using Player = Exiled.API.Features.Player;

namespace Seedonaters
{
    public class Main : Plugin<Config>
    {
        public override string Author => "Discord => pelemenb";
        public override string Name => "Seedonaters";
        public override Version Version => new Version(1, 0, 0);
        private Harmony _harmony;
        public static Main inst;
        public override PluginPriority Priority => PluginPriority.Medium;

        public override void OnEnabled()
        {
            inst = this;
            _harmony = new Harmony($"com.example.{nameof(Seedonaters)}");
            _harmony.PatchAll();
        }

        public override void OnDisabled()
        {
            _harmony?.UnpatchAll(_harmony.Id);
            _harmony = null;
        }
    }

    [HarmonyPatch(typeof(RaPlayerList), nameof(RaPlayerList.ReceiveData), new Type[] {typeof(CommandSender), typeof(string)})]
    public static class RaPlayerListPatch
    {
        public static void Prefix(RaPlayerList __instance, ref string data, CommandSender sender)
        {
            string[] lines = data.Split('\n');
            StringBuilder modifiedData = new StringBuilder();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string steamId = line.Split(':')[1].Trim();
                Player player = Player.Get(steamId);

                if (player != null && Main.inst.Config.Coins.Contains(steamId))
                {
                    modifiedData.AppendLine($"<size=15><color=#00FFF6>[💰]</color></size> {line}");
                }
                else
                {
                    modifiedData.AppendLine(line);
                }
            }

            data = modifiedData.ToString();
        }
    }
}
