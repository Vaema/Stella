using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Stella.Core.WeaponReworks.Melee;

/// <summary>
/// Loads and validates sword configurations from JSON files.
/// </summary>
public static class SwordConfigLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Loads a single sword configuration from a JSON file.
    /// </summary>
    public static SwordConfig LoadFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Stella.Instance.Logger.Error($"Sword config file not found: {filePath}");
                return null;
            }

            string json = File.ReadAllText(filePath);
            SwordConfig config = JsonSerializer.Deserialize<SwordConfig>(json, JsonOptions);

            if (!ValidateConfig(config, out List<string> errors))
            {
                foreach (string error in errors)
                    Stella.Instance.Logger.Error($"Config validation error in {Path.GetFileName(filePath)}: {error}");

                return null;
            }

            Stella.Instance.Logger.Info($"Loaded sword config: {config.Name} ({config.InternalName})");
            return config;
        }
        catch (JsonException ex)
        {
            Stella.Instance.Logger.Error($"Failed to parse JSON from {filePath}: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Stella.Instance.Logger.Error($"Unexpected error loading {filePath}: {ex}");
            return null;
        }
    }

    /// <summary>
    /// Loads all sword configurations from a directory.
    /// </summary>
    public static Dictionary<string, SwordConfig> LoadFromDirectory(string directoryPath)
    {
        var configs = new Dictionary<string, SwordConfig>();

        if (!Directory.Exists(directoryPath))
        {
            Stella.Instance.Logger.Warn($"Sword config directory not found: {directoryPath}");
            return configs;
        }

        string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");
        Stella.Instance.Logger.Info($"Found {jsonFiles.Length} sword config files in {directoryPath}");

        foreach (string filePath in jsonFiles)
        {
            SwordConfig config = LoadFromFile(filePath);
            if (config != null)
                configs[config.InternalName] = config;
        }

        return configs;
    }

    /// <summary>
    /// Validates a sword configuration for required fields and logical consistency.
    /// </summary>
    public static bool ValidateConfig(SwordConfig config, out List<string> errors)
    {
        errors = [];

        if (config == null)
        {
            errors.Add("Config is null");
            return false;
        }

        if (string.IsNullOrWhiteSpace(config.InternalName))
            errors.Add("Missing 'internalName' field");

        if (string.IsNullOrWhiteSpace(config.Name))
            errors.Add("Missing 'name' field");

        if (config.BaseStats == null)
            errors.Add("Missing 'baseStats' section");
        else
        {
            if (config.BaseStats.Damage <= 0)
                errors.Add("'baseStats.damage' must be greater than 0");

            if (config.BaseStats.UseTime <= 0)
                errors.Add("'baseStats.useTime' must be greater than 0");

            if (config.BaseStats.UseAnimation <= 0)
                errors.Add("'baseStats.useAnimation' must be greater than 0");
        }

        if (config.ComboSystem?.Attacks != null)
        {
            for (int i = 0; i < config.ComboSystem.Attacks.Count; i++)
            {
                ComboAttack attack = config.ComboSystem.Attacks[i];

                if (string.IsNullOrWhiteSpace(attack.Type))
                    errors.Add($"Combo attack {i}: missing 'type' field");

                if (attack.FrameDuration <= 0)
                    errors.Add($"Combo attack {i}: 'frameDuration' must be greater than 0");

                if (attack.Arc == null)
                    errors.Add($"Combo attack {i}: missing 'arc' section");

                if (attack.DamageMultiplier <= 0)
                    errors.Add($"Combo attack {i}: 'damageMultiplier' must be greater than 0");
            }
        }

        return errors.Count == 0;
    }
}
