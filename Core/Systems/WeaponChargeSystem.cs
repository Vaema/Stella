using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Stella.Content.UI;

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Stella.Core.Systems;

[Autoload(Side = ModSide.Client)]
public class WeaponChargeSystem : ModSystem
{
    private UserInterface ChargeInterface;
    
    internal ChargeBar ChargeBar = new();

    public static LocalizedText ChargeText { get; private set; }

    public override void Load()
    {
        ChargeBar = new();
        ChargeInterface = new();
        ChargeInterface.SetState(ChargeBar);
        ChargeText ??= Mod.GetLocalization($"UI.Charge");
    }

    public override void UpdateUI(GameTime gameTime) =>
        ChargeInterface?.Update(gameTime);

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bar", StringComparison.Ordinal));
        if (index != -1)
        {
            layers.Insert(index, new LegacyGameInterfaceLayer(
                "Stella: Charge Bar",
                delegate {

                    ChargeInterface.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}
