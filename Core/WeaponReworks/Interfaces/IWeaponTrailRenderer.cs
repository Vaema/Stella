using Microsoft.Xna.Framework;
using Stella.Core.WeaponReworks.Melee;

namespace Stella.Core.WeaponReworks.Interfaces;

/// <summary>
/// Interface for weapon trail rendering.
/// </summary>
public interface IWeaponTrailRenderer
{
    /// <summary>
    /// Renders a custom trail effect.
    /// </summary>
    void RenderTrail(Vector2 position, SwingArc arc, TrailEffect trail, float progress);
}
