using Terraria;
using Terraria.ID;

namespace Stella.Core.Utilities;

public static partial class Utilities
{
    public static bool Alive(this Item item) => item != null && item.type > ItemID.None && item.stack > 0;
}
