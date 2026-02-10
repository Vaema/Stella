using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stella.Core.BaseEntities;

public abstract class BaseChargeWeapon : ModItem
{
    /// <summary>
    ///     Set to false if you do not want the weapon to have held right-click functionality.
    ///     Defaults to true.
    /// </summary>
    public virtual bool AllowsRepeatedRightClick => true;

    /// <summary>
    ///     Set to true if the weapon is a staff.
    ///     Defaults to false.
    /// </summary>
    public virtual bool IsStaffWeapon => false;

    /// <summary>
    ///     The damage type for the weapon.
    ///     Defaults to <see cref="DamageClass.MeleeNoSpeed"/>.
    /// </summary>
    public virtual DamageClass DamageType => DamageClass.MeleeNoSpeed;

    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = AllowsRepeatedRightClick;
        Item.staff[Type] = IsStaffWeapon;

        SafeSetStaticDefaults();
    }

    /// <summary>
    ///     Sets any SetStaticDefaults properties for the charge weapon without changing
    ///     the properties that a charge weapon should have by default.
    /// </summary>
    public virtual void SafeSetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Item.DamageType = DamageType;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.channel = true;

        // For any mods using this library:
        // Set any default properties with this virtual method, and do NOT change
        // the properties that a charge weapon should have by default.
        SafeSetDefaults();
    }

    /// <summary>
    ///     Sets any SetDefaults properties for the charge weapon without changing
    ///     the properties that a charge weapon should have by default.
    /// </summary>
    public virtual void SafeSetDefaults()
    {
    }

    public override bool AltFunctionUse(Player player) => true;
}
