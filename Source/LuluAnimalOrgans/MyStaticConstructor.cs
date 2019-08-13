using Harmony;
using System.Text;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
    [StaticConstructorOnStartup]
    public static class MyStaticConstructor
    {
        private const string replaceMe = "human ";

        static MyStaticConstructor()
        {
            // Execute our Harmony patches.
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.loonyladle.animalorgans");
            harmony.PatchAll();

            StringBuilder stringBuilder = new StringBuilder();
            bool first = true;
            stringBuilder.Append("[LuluAnimalOrgans] Dynamic patched the following defs: ");

            // Search all ThingDefs in the DefDatabase.
            foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                // Patch natural body parts whose description contains "human " (note the space)
                if ((thingDef.description?.Contains(replaceMe) ?? false) && (thingDef.thingCategories?.Contains(MyThingCategoryDefOf.BodyPartsNatural) ?? false))
                {
                    // Remove all instances of "human" in the def's label.
                    thingDef.description = thingDef.description.Replace(replaceMe, null);

                    // Build the log string.
                    if (first)
                    {
                        stringBuilder.Append(thingDef.defName);
                        first = false;
                    }
                    else
                    {
                        stringBuilder.AppendWithComma(thingDef.defName);
                    }
                }
            }
            // Report on patched defs.
            Log.Message(stringBuilder.ToString());
        }
    }
}
