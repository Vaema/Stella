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

    public const string GrayscalePath = $"{TexturesPath}/Grayscale";

    public const string InvisiblePixelPath = $"{GrayscalePath}/InvisiblePixel";

    public const string PixelPath = $"{GrayscalePath}/Pixel";

    public const string NoiseTexturesPath = $"{TexturesPath}/Noise";

    #endregion

    #region Grayscale Textures

    public static readonly LazyAsset<Texture2D> BloomCircleSmall = LoadDeferred($"{GrayscalePath}/BloomCircleSmall");

    public static readonly LazyAsset<Texture2D> BloomFlare = LoadDeferred($"{GrayscalePath}/BloomFlare");

    public static readonly LazyAsset<Texture2D> BloomLineTexture = LoadDeferred($"{GrayscalePath}/BloomLine");

    public static readonly LazyAsset<Texture2D> ChromaticBurst = LoadDeferred($"{GrayscalePath}/ChromaticBurst");

    public static readonly LazyAsset<Texture2D> ShineFlare = LoadDeferred($"{GrayscalePath}/ShineFlare");

    #endregion

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

    #endregion

    #region Loader Utility

    private static LazyAsset<Texture2D> LoadDeferred(string path)
    {
        // Don't attempt to load anything server-side.
        if (Main.netMode == NetmodeID.Server)
            return default;

        return LazyAsset<Texture2D>.Request(path, AssetRequestMode.ImmediateLoad);
    }

    #endregion
}
