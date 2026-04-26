using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Stella.Core.WeaponReworks.Melee;

/// <summary>
/// Root configuration for a data-driven sword loaded from JSON.
/// </summary>
public class SwordConfig
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("internalName")]
    public string InternalName { get; set; }

    [JsonPropertyName("baseStats")]
    public BaseStats BaseStats { get; set; }

    [JsonPropertyName("comboSystem")]
    public ComboSystem ComboSystem { get; set; }

    [JsonPropertyName("trails")]
    public List<TrailEffect> Trails { get; set; } = [];

    [JsonPropertyName("animationSpeed")]
    public float AnimationSpeed { get; set; } = 1f;
}

public class BaseStats
{
    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    [JsonPropertyName("knockback")]
    public float Knockback { get; set; }

    [JsonPropertyName("critChance")]
    public int CritChance { get; set; } = 4;

    [JsonPropertyName("useTime")]
    public int UseTime { get; set; }

    [JsonPropertyName("useAnimation")]
    public int UseAnimation { get; set; }
}

public class ComboSystem
{
    [JsonPropertyName("maxComboCount")]
    public int MaxComboCount { get; set; } = 3;

    [JsonPropertyName("comboResetFrames")]
    public int ComboResetFrames { get; set; } = 30;

    [JsonPropertyName("attacks")]
    public List<ComboAttack> Attacks { get; set; } = [];
}

public class ComboAttack
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("frameStart")]
    public int FrameStart { get; set; }

    [JsonPropertyName("frameDuration")]
    public int FrameDuration { get; set; }

    [JsonPropertyName("damageMultiplier")]
    public float DamageMultiplier { get; set; } = 1f;

    [JsonPropertyName("knockbackMultiplier")]
    public float KnockbackMultiplier { get; set; } = 1f;

    [JsonPropertyName("arc")]
    public SwingArc Arc { get; set; }

    [JsonPropertyName("trailIndices")]
    public List<int> TrailIndices { get; set; } = [];

    [JsonPropertyName("soundIndex")]
    public int? SoundIndex { get; set; }
}

public class SwingArc
{
    [JsonPropertyName("startAngle")]
    public float StartAngle { get; set; } // in degrees

    [JsonPropertyName("endAngle")]
    public float EndAngle { get; set; }

    [JsonPropertyName("distance")]
    public float Distance { get; set; } = 30f;

    [JsonPropertyName("width")]
    public float Width { get; set; } = 20f;
}

public class TrailEffect
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } // "particle", "line", "glow"

    [JsonPropertyName("color")]
    public string Color { get; set; } = "255,255,255,255"; // R,G,B,A

    [JsonPropertyName("particleType")]
    public string ParticleType { get; set; }

    [JsonPropertyName("particleCount")]
    public int ParticleCount { get; set; } = 5;

    [JsonPropertyName("duration")]
    public int Duration { get; set; } = 10;

    [JsonPropertyName("scale")]
    public float Scale { get; set; } = 1f;

    [JsonPropertyName("offset")]
    public Vector2Config Offset { get; set; } = new();
}

public class Vector2Config
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }

    public Microsoft.Xna.Framework.Vector2 ToVector2() => new(X, Y);
}
