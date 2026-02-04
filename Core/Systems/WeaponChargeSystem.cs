//using Stella.Core.Globals.Players;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Stella.Core.Systems;

//public class WeaponChargeSystem : ModSystem
//{
//    public override void PostUpdatePlayers()
//    {
//        if (Main.netMode == NetmodeID.MultiplayerClient)
//            return;

//        foreach (Player player in Main.player)
//        {
//            if (player == null || !player.active)
//                continue;

//            var chargePlayer = player.GetModPlayer<WeaponChargePlayer>();
//            foreach (var ability in chargePlayer.Abilities.Values)
//                ability.Update();
//        }
//    }
//}
