using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Stella.Assets;

public class AssetDirectories : ModSystem
{
    #region Texture Path Constants

    public const string TexturesPath = "Stella/Assets/Textures";

    public const string GreyscaleTexturesPath = $"{TexturesPath}/GreyscaleTextures";

    public const string InvisiblePixelPath = $"{GreyscaleTexturesPath}/InvisiblePixel";

    public const string PixelPath = $"{GreyscaleTexturesPath}/Pixel";

    public const string NoiseTexturesPath = $"{TexturesPath}/Noise";

    #endregion Texture Path Constants

    #region Greyscale Textures

    public static readonly LazyAsset<Texture2D> BloomCircleSmall = LoadDeferred($"{GreyscaleTexturesPath}/BloomCircleSmall");

    public static readonly LazyAsset<Texture2D> BloomFlare = LoadDeferred($"{GreyscaleTexturesPath}/BloomFlare");

    public static readonly LazyAsset<Texture2D> BloomLineTexture = LoadDeferred($"{GreyscaleTexturesPath}/BloomLine");

    public static readonly LazyAsset<Texture2D> ChromaticBurst = LoadDeferred($"{GreyscaleTexturesPath}/ChromaticBurst");

    public static readonly LazyAsset<Texture2D> ShineFlare = LoadDeferred($"{GreyscaleTexturesPath}/ShineFlare");

    #endregion Greyscale Textures

    #region Noise Textures

    public static readonly LazyAsset<Texture2D> DendriticNoise = LoadDeferred($"{NoiseTexturesPath}/DendriticNoise");

    public static readonly LazyAsset<Texture2D> DendriticNoiseZoomedOut = LoadDeferred($"{NoiseTexturesPath}/DendriticNoiseZoomedOut");

    public static readonly LazyAsset<Texture2D> TurbulentNoise = LoadDeferred($"{NoiseTexturesPath}/TurbulentNoise");

    public static readonly LazyAsset<Texture2D> WavyBlotchNoise = LoadDeferred($"{NoiseTexturesPath}/WavyBlotchNoise");

    #endregion Noise Textures

    #region Pixels

    // Self-explanatory. Sometimes shaders need a "blank slate" in the form of an invisible texture to draw their true contents onto, which this can be beneficial for.
    public static readonly LazyAsset<Texture2D> InvisiblePixel = LoadDeferred(InvisiblePixelPath);

    // Self-explanatory.
    public static readonly LazyAsset<Texture2D> Pixel = LoadDeferred(PixelPath);

    #endregion Pixels

    #region Loader Utility

    private static LazyAsset<Texture2D> LoadDeferred(string path)
    {
        // Don't attempt to load anything server-side.
        if (Main.netMode == NetmodeID.Server)
            return default;

        return LazyAsset<Texture2D>.Request(path, AssetRequestMode.ImmediateLoad);
    }

    #endregion Loader Utility
}
