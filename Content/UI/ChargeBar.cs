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
    private UIText Text;
    private UIElement Area;
    private UIImage Frame;

    private Color GradientA;
    private Color GradientB;

    public override void OnInitialize()
    {
        Area = new UIElement();
        Area.Left.Set(-Area.Width.Pixels - 600, 1f);
        Area.Top.Set(30, 0f);
        Area.Width.Set(104, 0f);
        Area.Height.Set(16, 0f);

        Frame = new UIImage(ModContent.Request<Texture2D>("Stella/Content/UI/ChargeBarFrame"));
        Frame.Left.Set(22, 0f);
        Frame.Top.Set(0, 0f);
        Frame.Width.Set(104, 0f);
        Frame.Height.Set(16, 0f);

        Text = new UIText("0/0");
        Text.Left.Set(0, 0f);
        Text.Top.Set(40, 0f);
        Text.Width.Set(104, 0f);
        Text.Height.Set(16, 0f);

        GradientA = Color.Lime;
        GradientB = Color.Red;

        Area.Append(Text);
        Area.Append(Frame);
        Append(Area);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var player = Main.LocalPlayer.GetModPlayer<WeaponChargePlayer>();

        float quotient = (float)player.AmountCurrent / player.AmountMax;
        quotient = Utils.Clamp(quotient, 0f, 1f);

        Rectangle hitbox = Frame.GetInnerDimensions().ToRectangle();
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
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(GradientA, GradientB, percent));
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (Main.LocalPlayer.HeldItem.type != ModContent.ItemType<BaseChargeWeapon>())
            return;

        var player = Main.LocalPlayer.GetModPlayer<WeaponChargePlayer>();
        Text.SetText(WeaponChargeSystem.ChargeText.Format(player.AmountCurrent, player.AmountMax));
    }
}
