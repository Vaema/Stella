using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace Stella.Common.Utilities;

public static partial class Utilities
{
    /// <summary>
    ///     Stops a vanilla rain event if one is already happening.
    /// </summary>
    public static void StopRain(bool clearWeather = false, bool worldSync = true)
    {
        if (clearWeather)
            Main.StopRain();
        else
            Main.raining = false;

        if (worldSync)
        {
            if (Main.netMode != NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }
    }

    /// <summary>
    ///     Stops a vanilla sandstorm if one is already happening.
    /// </summary>
    public static void StopSandstorm()
    {
        if (Main.netMode != NetmodeID.MultiplayerClient)
            Sandstorm.Happening = false;
    }

    /// <summary>
    ///     Changes the in-game time.
    /// </summary>
    /// <param name="changeToDay">Sets it to day in-game if true.</param>
    public static void ChangeTime(bool changeToDay)
    {
        Main.time = 0D;
        Main.dayTime = changeToDay;

        if (Main.netMode != NetmodeID.Server)
            NetMessage.SendData(MessageID.WorldData);
    }

    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
        if (condition)
            list.Add(type);
    }
}
