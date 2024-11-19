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
                0.2f,
                "Ammount to scale by ship class in unfixed mode (1 + (this value x ship class))");

            cfgShuttle = Config.Bind<float>(
                "Config",
                "Shuttle",
                1f,
                "Ammount to scale for shuttles in fixed mode.");
            cfgYacht = Config.Bind<float>(
                "Config",
                "Yacht",
                1.1f,
                "Ammount to scale for yachts in fixed mode.");
            cfgCorvette = Config.Bind<float>(
                "Config",
                "Corvette",
                1.25f,
                "Ammount to scale for corvettes in fixed mode.");
            cfgFrigate = Config.Bind<float>(
                "Config",
                "Frigate",
                1.5f,
                "Ammount to scale for frigates in fixed mode.");
            cfgCruiser = Config.Bind<float>(
                "Config",
                "Cruiser",
                1.8f,
                "Ammount to scale for cruisers in fixed mode.");
            cfgDread = Config.Bind<float>(
                "Config",
                "Dread",
                2f,
                "Ammount to scale for dreads in fixed mode.");
        }

        [HarmonyPatch(typeof(PlayerControl), "ShowAimObject")]
        [HarmonyPrefix]
        private static void PlayerControl_ShowAimObjPre(PlayerControl __instance)
        {
            if (__instance.AimObj == null)
                return;

            if (!cfgFixedMode.Value)
            {
                float scale = 1 + (__instance.GetSpaceShip.shipClass * cfgByClass.Value);
                __instance.AimObj.transform.localScale = new Vector3(scale, 1.0f, scale);
            } 
            else
            {
                float scale = 1.0f;
                switch(__instance.GetSpaceShip.shipClass)
                {
                    case 1:
                        scale = cfgShuttle.Value;
                        break;
                    case 2:
                        scale = cfgYacht.Value;
                        break;
                    case 3:
                        scale = cfgCorvette.Value;
                        break;
                    case 4:
                        scale = cfgFrigate.Value;
                        break;
                    case 5:
                        scale = cfgCruiser.Value;
                        break;
                    case 6:
                        scale = cfgDread.Value;
                        break;
                }

                __instance.AimObj.transform.localScale = new Vector3(scale, 1.0f, scale);
            }
            
        }
    }
}
