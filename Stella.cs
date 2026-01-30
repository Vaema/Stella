global using static System.MathF;
global using static Microsoft.Xna.Framework.MathHelper;
global using static Stella.Core.Utilities.Utilities;
global using static Stella.Assets.AssetDirectories;
using System;
using Stella.Core.Graphics.Particles;
using Terraria.ModLoader;

namespace Stella;

/// <summary>
///     The central mod type for the Stella library.
/// </summary>
public class Stella : Mod
{
    public override void PostSetupContent()
    {
        ShaderManager.HasFinishedLoading = false;

        foreach (Mod mod in ModLoader.Mods)
        {
            ShaderRecompilationMonitor.LoadForMod(mod);
            ShaderManager.LoadShaders(mod);
            AtlasManager.InitializeModAtlases(mod);
            ParticleManager.InitializeManualRenderers(mod);
        }

        ShaderManager.HasFinishedLoading = true;

        while (ShaderManager.PostShaderLoadActions.TryDequeue(out Action action))
            action?.Invoke();
    }
}
