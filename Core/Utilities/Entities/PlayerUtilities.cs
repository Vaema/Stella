using Microsoft.Xna.Framework;
using Terraria;

namespace Stella.Core.Utilities;

public static partial class Utilities
{
    /// <summary>
    ///     Gives a given <see cref="Player"/> infinite flight.
    /// </summary>
    /// <param name="p">The player to apply infinite flight to.</param>
    public static void GrantInfiniteFlight(this Player p) =>
        p.wingTime = p.wingTimeMax;

    /// <summary>
    ///     Gets the current mouse item for a given <see cref="Player"/>. This supports <see cref="Main.mouseItem"/> (the item held by the cursor) and <see cref="Player.HeldItem"/> (the item in use with the hotbar).
    /// </summary>
    /// <param name="p">The player to retrieve the mouse item for.</param>
    public static Item HeldMouseItem(this Player p)
    {
        if (!Main.mouseItem.IsAir)
            return Main.mouseItem;

        return p.HeldItem;
    }

    /// <summary>
    ///     Gets the player's center.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns></returns>
    public static Vector2 GetPlayerCenter(this Player player) => player.MountedCenter.Floor() + new Vector2(0, player.gfxOffY);

    /// <summary>
    ///     Checks if the player is alive.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns></returns>
    public static bool Alive(this Player player) => player != null && player.active && !player.dead;
}
