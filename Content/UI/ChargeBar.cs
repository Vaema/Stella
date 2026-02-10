using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent;

using Stella.Core.Systems;
using Stella.Core.BaseEntities;
using Stella.Core.Globals.Players;

namespace Stella.Content.UI;

public class ChargeBar : UIState
{
    private UIText text;
    private UIElement area;
    private UIImage frame;

    private Color gradientA;
    private Color gradientB;

    public override void OnInitialize()
    {
        area = new UIElement();
        area.Left.Set(-area.Width.Pixels - 600, 1f);
        area.Top.Set(30, 0f);
        area.Width.Set(104, 0f);
        area.Height.Set(16, 0f);

        frame = new UIImage(ModContent.Request<Texture2D>("Terradux/Content/UI/Charge/ChargeFrame"));
        frame.Left.Set(22, 0f);
        frame.Top.Set(0, 0f);
        frame.Width.Set(104, 0f);
        frame.Height.Set(16, 0f);

        text = new UIText("0/0");
        text.Left.Set(0, 0f);
        text.Top.Set(40, 0f);
        text.Width.Set(104, 0f);
        text.Height.Set(16, 0f);

        gradientA = Color.Lime;
        gradientB = Color.Red;

        area.Append(text);
        area.Append(frame);
        Append(area);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var player = Main.LocalPlayer.GetModPlayer<WeaponChargePlayer>();

        float quotient = (float)player.AmountCurrent / player.AmountMax;
        quotient = Utils.Clamp(quotient, 0f, 1f);

        Rectangle hitbox = frame.GetInnerDimensions().ToRectangle();
        hitbox.X += 48;
        hitbox.Width -= 96;
        hitbox.Y += 4;
        hitbox.Height -= 8;

        int left = hitbox.Left;
        int right = hitbox.Right;
        int steps = (int)((right - left) * quotient);
        for (int i = 0; i < steps; i += 1)
        {
            float percent = (float)i / (right - left);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (Main.LocalPlayer.HeldItem.type != ModContent.ItemType<BaseChargeWeapon>())
            return;

        var player = Main.LocalPlayer.GetModPlayer<WeaponChargePlayer>();
        text.SetText(WeaponChargeSystem.ChargeText.Format(player.AmountCurrent, player.AmountMax));
    }
}
