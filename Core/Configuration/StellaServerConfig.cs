using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace Stella.Core.Configuration;

public class StellaServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)]
    public bool BehaviorOverrides { get; set; }
}
