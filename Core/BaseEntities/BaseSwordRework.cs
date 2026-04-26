//using System;
//using Microsoft.Xna.Framework;
//using Stella.Core.WeaponReworks.Melee;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Stella.Core.BaseEntities;

///// <summary>
///// Base class for swords that are configured via JSON.
///// Other mods can inherit from this to create their own data-driven swords.
///// </summary>
//public abstract class DataDrivenSword : ModItem
//{
//    protected SwordConfig Config { get; set; }

//    private int comboCount;
//    private int comboResetTimer;
//    private int attackFrameCounter;
//    private ComboAttack currentAttack;

//    public event EventHandler<ComboAttackEventArgs> OnComboAttackStart;
//    public event EventHandler<ComboAttackEventArgs> OnComboAttackEnd;

//    public override void SetDefaults()
//    {
//        if (Config == null)
//        {
//            Mod.Logger.Error($"{GetType().Name} has no loaded config!");
//            return;
//        }

//        // Apply base stats from the config.
//        item.damage = Config.BaseStats.Damage;
//        item.knockBack = Config.BaseStats.Knockback;
//        item.crit = Config.BaseStats.CritChance;
//        item.useTime = Config.BaseStats.UseTime;
//        item.useAnimation = Config.BaseStats.UseAnimation;

//        item.melee = true;
//        item.noUseGraphic = false;
//        Item.useStyle = ItemUseStyleID.Swing;

//        item.width = 40;
//        item.height = 40;

//        item.value = Item.buyPrice(0, 5, 0, 0);
//        item.rare = ItemRarityID.White;
//    }

//    public override void UseItemFrame(Player player)
//    {
//        if (Config?.ComboSystem == null || Config.ComboSystem.Attacks.Count == 0)
//            return;

//        // Update combo reset timer
//        if (comboResetTimer > 0)
//        {
//            comboResetTimer--;
//        }
//        else if (comboCount > 0)
//        {
//            ResetCombo();
//        }

//        // Update attack frame counter
//        if (player.itemAnimation > 0)
//        {
//            attackFrameCounter++;

//            // Determine which combo attack we're in
//            int attackIndex = Math.Min(comboCount, Config.ComboSystem.Attacks.Count - 1);
//            currentAttack = Config.ComboSystem.Attacks[attackIndex];

//            // Check if we should trigger attack effects
//            if (attackFrameCounter == currentAttack.FrameStart)
//            {
//                OnComboAttackStart?.Invoke(this, new ComboAttackEventArgs(currentAttack, comboCount));
//                ApplyTrailEffects(player, currentAttack);
//            }

//            // Update animation frame
//            player.itemFrame = (attackFrameCounter * 3) / currentAttack.FrameDuration;
//        }
//        else
//        {
//            attackFrameCounter = 0;
//            currentAttack = null;
//        }
//    }

//    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
//    {
//        if (currentAttack == null)
//            return;

//        // Apply combo-specific damage and knockback multipliers
//        int adjustedDamage = (int)(damage * currentAttack.DamageMultiplier);
//        float adjustedKnockback = knockback * currentAttack.KnockbackMultiplier;

//        target.StrikeNPC(adjustedDamage, adjustedKnockback, player.direction);

//        // Increment combo on successful hit
//        if (comboCount < Config.ComboSystem.MaxComboCount)
//        {
//            comboCount++;
//        }

//        comboResetTimer = Config.ComboSystem.ComboResetFrames;

//        OnComboAttackEnd?.Invoke(this, new ComboAttackEventArgs(currentAttack, comboCount - 1));
//    }

//    private void ApplyTrailEffects(Player player, ComboAttack attack)
//    {
//        if (attack.TrailIndices == null || attack.TrailIndices.Count == 0)
//            return;

//        foreach (int trailIndex in attack.TrailIndices)
//        {
//            if (trailIndex >= 0 && trailIndex < Config.Trails.Count)
//            {
//                TrailEffect trail = Config.Trails[trailIndex];
//                SpawnTrail(player, trail, attack.Arc);
//            }
//        }
//    }

//    private void SpawnTrail(Player player, TrailEffect trail, SwingArc arc)
//    {
//        Vector2 direction = Vector2.UnitX.RotatedBy(MathHelper.ToRadians(arc.StartAngle));
//        Vector2 position = player.Center + direction * arc.Distance;

//        // Parse color from string "R,G,B,A"
//        string[] colorParts = trail.Color.Split(',');
//        Color trailColor = new(
//            int.Parse(colorParts[0]),
//            int.Parse(colorParts[1]),
//            int.Parse(colorParts[2]),
//            int.Parse(colorParts[3])
//        );

//        if (trail.Type == "particle")
//        {
//            for (int i = 0; i < trail.ParticleCount; i++)
//            {
//                Vector2 velocity = Vector2.UnitX.RotatedByRandom(MathHelper.Pi) * 3f;
//                Dust dust = Dust.NewDustPerfect(position, DustID.IceRod, velocity, 100, trailColor, trail.Scale);
//                dust.noGravity = true;
//            }
//        }
//    }

//    private void ResetCombo()
//    {
//        comboCount = 0;
//        comboResetTimer = 0;
//    }

//    public int GetCurrentCombo() => comboCount;
//}

//public class ComboAttackEventArgs : EventArgs
//{
//    public ComboAttack Attack { get; set; }
//    public int ComboIndex { get; set; }

//    public ComboAttackEventArgs(ComboAttack attack, int comboIndex)
//    {
//        Attack = attack;
//        ComboIndex = comboIndex;
//    }
//}
