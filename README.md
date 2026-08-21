# Stella

## Information
Stella is a general-purpose library for Terraria mods to use. It is also a continuation of the Luminance library made by Lucille Karma. Why? Luminance is planned to be deprecated, and it was pretty laggy to begin with.

## Credits
- Lucille Karma and the Luminance contributors, for creating Luminance.
- Tomat, for originally informing me that Luminance is being deprecated.

## How to Use
To make your mod use Stella, add `modReferences = Stella` in your mod's `build.txt`. In order to be able to reference the mod when programming, download `Stella.dll`, `Stella.pdb` and `Stella.xml` from the latest GitHub release, or extract the mod in-game and take the files from there. Add the DLL as a project reference, or directly add it into your mod's CSPROJ. Ensure you reference the latest mod version to avoid things breaking. The PDB and XML files ensure you can view the documentation from your mod directly. It's recommended to add these three files to the `buildIgnore` in `build.txt` to avoid unnecessarily packaging them with your mod.

## Important Notice
Some of the features that are in Luminance were NOT migrated into Stella. It is because of them being redundant and unnecessary, especially with tModLoader 1.4.5 around the corner.