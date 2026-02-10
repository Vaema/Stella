using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stella.Core.Globals.Players;

public class WeaponChargePlayer : ModPlayer
{
    public int AmountCurrent;
    public int AmountMax = 3;

    public float RegenerationRate;
    internal int RegenerationTimer;

    public bool FullyCharged;
    public bool IsHoldingLeftClick;
    public bool HeldItemIsValid;

    public override void Initialize() => FullyCharged = false;

    public override void ResetEffects()
    {
        RegenerationRate = 1f;
        FullyCharged = false;
    }

    public override void PreUpdate()
    {
        var damageType = Player.HeldItem.DamageType;

        if (damageType == DamageClass.Melee || damageType == DamageClass.MeleeNoSpeed || damageType == DamageClass.Ranged ||
            damageType == DamageClass.Magic || damageType == DamageClass.Throwing || damageType == DamageClass.Generic)
            HeldItemIsValid = true;
        else
            HeldItemIsValid = false;
    }

    public override void UpdateDead()
    {
        AmountCurrent = 0;
        FullyCharged = false;
    }

    /*public override bool PreItemCheck()
    {
        if ((Main.LocalPlayer.HeldItem.DamageType == DamageClass.Melee) || (Main.LocalPlayer.HeldItem.DamageType == DamageClass.Magic))
        {
            if (Player.channel)
                UpdateAmount();
        }

        return true;
    }*/

    public override void PostUpdateMiscEffects()
    {
        ProcessCharge();

        if (Player.controlUseItem || Player.channel)
            IsHoldingLeftClick = true;
        else
            IsHoldingLeftClick = false;
    }

    private void ProcessCharge()
    {
        int dustTimer = 0;
        dustTimer++;

        if (dustTimer < 100)
        {
            if (AmountCurrent == 1)
            {
                int i = Dust.NewDust(Player.Center, -Player.width + Player.width, -Player.height + Player.height, DustID.RedTorch);
                Main.dust[i].noGravity = true;
                Main.dust[i].scale = 2f;
            }
            else if (AmountCurrent == 2)
            {
                int i = Dust.NewDust(Player.Center, -Player.width + Player.width, -Player.height + Player.height, DustID.YellowTorch);
                Main.dust[i].noGravity = true;
                Main.dust[i].scale = 2f;
            }
            else if (AmountCurrent == 3)
            {
                int i = Dust.NewDust(Player.Center, -Player.width + Player.width, -Player.height + Player.height, DustID.GreenTorch);
                Main.dust[i].noGravity = true;
                Main.dust[i].scale = 2f;
            }
        }

        if (Main.LocalPlayer.HeldItem.type == ItemID.BeamSword && IsHoldingLeftClick)
        {
            RegenerationTimer++;
            if (RegenerationTimer > 100 / RegenerationRate)
            {
                RegenerationTimer = 0;

                if (AmountCurrent < AmountMax)
                {
                    FullyCharged = false;
                    AmountCurrent += 1;
                    CombatText.NewText(Player.getRect(), Color.Green, AmountCurrent.ToString());
                }
                else if (AmountCurrent == AmountMax)
                {
                    FullyCharged = true;
                    CombatText.NewText(Player.getRect(), Color.LimeGreen, "Ready!");
                }
            }
        }
    }
}
