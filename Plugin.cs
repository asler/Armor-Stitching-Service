﻿using System;
using System.Reflection;
using SPT.Reflection.Patching;
using BepInEx;
using BepInEx.Configuration;
using EFT;

namespace armorMod
{
    [BepInPlugin("com.dvize.ASS", "dvize.ASS", "1.7.2")]
    public class AssPlugin : BaseUnityPlugin
    {
        internal static ConfigEntry<Boolean> ArmorServiceMode
        {
            get; set;
        }
        internal static ConfigEntry<Boolean> WeaponServiceMode
        {
            get; set;
        }
        internal static ConfigEntry<float> TimeDelayRepairInSec
        {
            get; set;
        }
        internal static ConfigEntry<float> ArmorRepairRateOverTime
        {
            get; set;
        }
        internal static ConfigEntry<float> MaxDurabilityDegradationRateOverTime
        {
            get; set;
        }
        internal static ConfigEntry<float> MaxDurabilityCap
        {
            get; set;
        }

        internal static ConfigEntry<float> weaponTimeDelayRepairInSec
        {
            get; set;
        }
        internal static ConfigEntry<float> weaponRepairRateOverTime
        {
            get; set;
        }
        internal static ConfigEntry<float> weaponMaxDurabilityDegradationRateOverTime
        {
            get; set;
        }
        internal static ConfigEntry<float> weaponMaxDurabilityCap
        {
            get; set;
        }

        internal static ConfigEntry<Boolean> fixFaceShieldBullets
        {
            get; set;
        }

        internal void Awake()
        {
            ArmorServiceMode = Config.Bind("1. Main Settings", "Enable/Disable Armor Repair", true, new ConfigDescription("Enables the Armor Repairing Options Below",
                null, new ConfigurationManagerAttributes { IsAdvanced = false, Order = 2 }));
            WeaponServiceMode = Config.Bind("1. Main Settings", "Enable/Disable Weapon Repair", true, new ConfigDescription("Enables the Weapon Repairing Options Below",
                null, new ConfigurationManagerAttributes { IsAdvanced = false, Order = 1 }));

            TimeDelayRepairInSec = Config.Bind("2. Armor Repair Settings", "Time Delay Repair in Sec", 60f, new ConfigDescription("How Long Before you were last hit that it repairs armor",
                new AcceptableValueRange<float>(0f, 1200f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 5 }));
            ArmorRepairRateOverTime = Config.Bind("2. Armor Repair Settings", "Armor Repair Rate", 0.5f, new ConfigDescription("How much durability per second is repaired",
                new AcceptableValueRange<float>(0f, 10f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 4 }));
            MaxDurabilityDegradationRateOverTime = Config.Bind("2. Armor Repair Settings", "Max Durability Drain Rate", 0.025f, new ConfigDescription("How much max durability per second of repairs is drained",
                new AcceptableValueRange<float>(0f, 1f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 3 }));
            MaxDurabilityCap = Config.Bind("2. Armor Repair Settings", "Max Durability Cap", 100f, new ConfigDescription("Maximum durability percentage to which armor will be able to repair to. For example, setting to 80 would repair your armor to maximum of 80% of it's max durability",
                new AcceptableValueRange<float>(0f, 100f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = true, Order = 2 }));

            weaponTimeDelayRepairInSec = Config.Bind("3. Weapon Repair Settings", "Time Delay Repair in Sec", 60f, new ConfigDescription("How Long Before you were last hit that it repairs weapon. Doesn't Make sense but i'm too lazy to change.",
                new AcceptableValueRange<float>(0f, 1200f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 5 }));
            weaponRepairRateOverTime = Config.Bind("3. Weapon Repair Settings", "Weapon Repair Rate", 0.5f, new ConfigDescription("How much durability per second is repaired",
                new AcceptableValueRange<float>(0f, 10f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 4 }));
            weaponMaxDurabilityDegradationRateOverTime = Config.Bind("3. Weapon Repair Settings", "Max Durability Drain Rate", 0f, new ConfigDescription("How much max durability per second of repairs is drained (set really low if using)",
                new AcceptableValueRange<float>(0f, 1f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 3 }));
            weaponMaxDurabilityCap = Config.Bind("3. Weapon Repair Settings", "Max Durability Cap", 100f, new ConfigDescription("Maximum durability percentage to which weapon will be able to repair to. For example, setting to 80 would repair your armor to maximum of 80% of it's max durability",
                new AcceptableValueRange<float>(0f, 100f), new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = true, Order = 2 }));


            fixFaceShieldBullets = Config.Bind("4. Face Shield", "Fix Bullet Cracks", true, new ConfigDescription("Enables Repairing Bullet Cracks in FaceShield",
                null, new ConfigurationManagerAttributes { IsAdvanced = false, ShowRangeAsPercent = false, Order = 2 }));

            new NewGamePatch().Enable();
        }

        internal class NewGamePatch : ModulePatch
        {
            protected override MethodBase GetTargetMethod() => typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));

            [PatchPrefix]
            internal static void PatchPrefix()
            {
                AssComponent.Enable();
            }
        }
    }

}

