using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace MC_SVReticleScaling
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.scalingreticle";
        public const string pluginName = "SV Reticle Scaling";
        public const string pluginVersion = "1.0.0";

        public static ConfigEntry<bool> cfgFixedMode;
        public static ConfigEntry<bool> cfgSkipShuttle;

        public static ConfigEntry<float> cfgByClass;
        public static ConfigEntry<float> cfgShuttle;
        public static ConfigEntry<float> cfgYacht;
        public static ConfigEntry<float> cfgCorvette;
        public static ConfigEntry<float> cfgFrigate;
        public static ConfigEntry<float> cfgCruiser;
        public static ConfigEntry<float> cfgDread;

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));

            cfgFixedMode = Config.Bind<bool>(
                "Config",
                "Fixed Mode",
                false,
                "When enabled, uses fixed configured fixed values for each class.  When disabled, uses 'by class' value, multiplied by ship class.");

            cfgByClass = Config.Bind<float>(
                "Config",
                "By class",
                0.25f,
                "Ammount to scale by ship class in unfixed mode (1 + (this value x ship class))");
            cfgSkipShuttle = Config.Bind<bool>(
                "Config",
                "Skip shuttle",
                false,
                "Only functions when fixed mode disabled.  When enabled, skips shuttle class for by class scaling i.e. shuttles remain at default and selected value begins at yacht.  When disabled, scaling begins at shuttles.");

            cfgShuttle = Config.Bind<float>(
                "Config",
                "Shuttle",
                0f,
                "Ammount to scale for shuttles in fixed mode (1 + value).");
            cfgYacht = Config.Bind<float>(
                "Config",
                "Yacht",
                0.25f,
                "Ammount to scale for yachts in fixed mode (1 + value).");
            cfgCorvette = Config.Bind<float>(
                "Config",
                "Corvette",
                0.5f,
                "Ammount to scale for corvettes in fixed mode (1 + value).");
            cfgFrigate = Config.Bind<float>(
                "Config",
                "Frigate",
                0.75f,
                "Ammount to scale for frigates in fixed mode (1 + value).");
            cfgCruiser = Config.Bind<float>(
                "Config",
                "Cruiser",
                1.0f,
                "Ammount to scale for cruisers in fixed mode (1 + value).");
            cfgDread = Config.Bind<float>(
                "Config",
                "Dread",
                1.25f,
                "Ammount to scale for dreads in fixed mode (1 + value).");
        }

        [HarmonyPatch(typeof(PlayerControl), "ShowAimObject")]
        [HarmonyPrefix]
        private static void PlayerControl_ShowAimObjPre(PlayerControl __instance)
        {
            if (__instance.AimObj == null)
                return;

            if (!cfgFixedMode.Value)
            {
                int classVal = cfgSkipShuttle.Value ? __instance.GetSpaceShip.shipClass - 1 : __instance.GetSpaceShip.shipClass;
                float scale = 1 + (classVal * cfgByClass.Value);
                __instance.AimObj.transform.localScale = new Vector3(scale, 1.0f, scale);
            } 
            else
            {
                float scale = 1.0f;
                switch(__instance.GetSpaceShip.shipClass)
                {
                    case 1:
                        scale += cfgShuttle.Value;
                        break;
                    case 2:
                        scale += cfgYacht.Value;
                        break;
                    case 3:
                        scale += cfgCorvette.Value;
                        break;
                    case 4:
                        scale += cfgFrigate.Value;
                        break;
                    case 5:
                        scale += cfgCruiser.Value;
                        break;
                    case 6:
                        scale += cfgDread.Value;
                        break;
                }

                __instance.AimObj.transform.localScale = new Vector3(scale, 1.0f, scale);
            }
            
        }
    }
}
