using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrooxEngine;
using FrooxEngine.Store;
using FrooxEngine.UIX;
using Elements.Core;
using SkyFrost.Base;
using System.Reflection.Emit;

namespace SessionItemModal
{
    public class SessionItemModal : ResoniteMod
    {
        public override string Name => "SessionItemModal";
        public override string Author => "art0007i";
        public override string Version => "1.0.1";
        public override string Link => "https://github.com/art0007i/SessionItemModal/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.art0007i.SessionItemModal");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(SessionItem), "BuildUI")]
        class SessionItemModalPatch
        {
            public static void Postfix(UIBuilder ui, SyncRef<Image> ____thumbnail, SessionItem __instance)
            {
                var s = ____thumbnail.Target.Slot;
                s.AttachComponent<Button>().LocalPressed += (b, e) =>
                {
                    CoroutineManager.Manager.Value = b.World.Coroutines;
                    FrooxEngine.Store.Record r = null;
                    string id = __instance.SessionInfo.SessionId;
                    if (__instance.SessionInfo.CorrespondingWorldId != null)
                    {
                        id = __instance.SessionInfo.CorrespondingWorldId.ToString();
                    }
                    var a = new SessionInfo[] { __instance.SessionInfo };
                    b.Slot.OpenModalOverlay(float2.One * 0.8f)?.Slot.AttachComponent<LegacyWorldDetail>().SetTarget(id, prefetchedInfo: a);
                };
            }
        }
    }
}