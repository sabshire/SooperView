using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SooperView
{
    internal class ConfigHandler
    {
        private static readonly string configFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
        private static readonly string configEncoderSettingsFile = Path.Combine(configFolder, "EncoderSettings.config");
        private static EncoderSettings DefaultSettings;

        public static void Setup(EncoderSettings defaultSettings)
        {
            DefaultSettings = defaultSettings;
        }

        public static void SaveConfigFile(EncoderSettings currentSettings) 
        {
            CreateConfigFile(currentSettings.Hardware, currentSettings.Encoding, currentSettings.Colorspace, currentSettings.Tune, currentSettings.Resolution, currentSettings.CRF, currentSettings.Preset);
        }

        public static EncoderSettings LoadConfigFile() 
        {
            // If config file doesn't exist or values don't exist load default value

            if (!File.Exists(configEncoderSettingsFile))
            {
                // File doesn't exist, create one
                LogHelper.LogMessageColor(typeof(ConfigHandler), $"Creating Config File at: {configEncoderSettingsFile}", Color.Blue);
                CreateConfigFile(DefaultSettings.Hardware, DefaultSettings.Encoding, DefaultSettings.Colorspace, DefaultSettings.Tune, DefaultSettings.Resolution, DefaultSettings.CRF, DefaultSettings.Preset);
                return DefaultSettings;
            }
            else
            {
                EncoderSettings settings = new EncoderSettings();
                // Config file exists, load current settings
                string[] lines = File.ReadAllLines(configEncoderSettingsFile);
                string[] addedSettings = new string[lines.Length]; // Using this to check if a setting has a value or needs a default
                int lineNum = -1;
                foreach (string line in lines)
                {
                    lineNum++;
                    if (string.IsNullOrEmpty(line) || !line.Contains("=")) { continue; }  // Skip bad / empty lines

                    // Get Key and Value from current line
                    string[] parts = line.Split(new[] { '=' }, 2);
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (addedSettings.Contains(key)) { continue; } // Skip, setting already in config (probably user error)

                    // Add current keys to new settings
                    switch (key)
                    {
                        case "HARDWARE":
                            settings.Hardware = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "ENCODING":
                            settings.Encoding = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "COLORSPACE":
                            settings.Colorspace = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "TUNE":
                            settings.Tune = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "RESOLUTION":
                            settings.Resolution = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "CRF":
                            settings.CRF = Convert.ToInt32(value);
                            addedSettings[lineNum] = key;
                            break;

                        case "PRESET":
                            settings.Preset = value;
                            addedSettings[lineNum] = key;
                            break;
                    }
                }

                // Load default values for any keys missing
                if (!addedSettings.Contains("HARDWARE")) { settings.Hardware = DefaultSettings.Hardware; }
                if (!addedSettings.Contains("ENCODING")) { settings.Encoding = DefaultSettings.Encoding; }
                if (!addedSettings.Contains("COLORSPACE")) { settings.Colorspace = DefaultSettings.Colorspace; }
                if (!addedSettings.Contains("TUNE")) { settings.Tune = DefaultSettings.Tune; }
                if (!addedSettings.Contains("RESOLUTION")) { settings.Resolution = DefaultSettings.Resolution; }
                if (!addedSettings.Contains("CRF")) { settings.CRF = DefaultSettings.CRF; }
                if (!addedSettings.Contains("PRESET")) 
                {
                    // Load preset default dynamically
                    var form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                    int presetDefault = form.GrabPresetDefault($"{settings.Hardware}{settings.Encoding}");

                    // Convert presetDefault to string
                    if (settings.Hardware == 1) // NVIDIA GPU
                    {
                        settings.Preset = form.GrabPresetList(1)[presetDefault];
                    }

                    else if (settings.Hardware == 2) // INTEL GPU
                    {
                        settings.Preset = form.GrabPresetList(2)[presetDefault];
                    }

                    else if (settings.Hardware == 3) // AMD GPU
                    {
                        settings.Preset = form.GrabPresetList(3)[presetDefault];
                    }

                    else if (settings.Hardware == 0)
                    {
                        if (settings.Encoding == 2) // SVT-AV1 Encoder
                        {
                            settings.Preset = form.GrabPresetList(4)[presetDefault];
                        }
                        else if ((settings.Encoding == 0) || (settings.Encoding == 1)) // H264 (0) and H265 (1)
                        {
                            settings.Preset = form.GrabPresetList(0)[presetDefault];
                        }
                        else
                        {
                            LogHelper.LogMessageColor(typeof(ConfigHandler), "ConfigHandler: LoadConfigFile() no default preset found matching hardware and encoder", Color.Red); // No defaults (Shouldn't Happen)
                        }
                    }
                }
                return settings;
            }
        }

        private static void CreateConfigFile(int hardware, int encoding, int colorspace, int tune, int resolution, int crf, string preset)
        {
            string[] lines =
            {
                "### DO NOT EDIT THIS FILE UNLESS YOU KNOW WHAT YOU ARE DOING. DELETING THIS FILE WILL REMAKE IT ON APP RESTART ###",
                $"HARDWARE={hardware}",
                $"ENCODING={encoding}",
                $"COLORSPACE={colorspace}",
                $"TUNE={tune}",
                $"RESOLUTION={resolution}",
                $"CRF={crf}",
                $"PRESET={preset}"
            };
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            File.WriteAllLines(configEncoderSettingsFile, lines);
        }

    }
}
