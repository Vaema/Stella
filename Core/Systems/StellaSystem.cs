using Terraria.ModLoader;

namespace Stella.Core.Systems;

public class StellaSystem : ModSystem
{
    public override void PreUpdateEntities() => UpdateBossCache();
}
