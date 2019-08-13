using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
    [DefOf]
    public static class MyThingCategoryDefOf
    {
        static MyThingCategoryDefOf() => DefOfHelper.EnsureInitializedInCtor(typeof(ThingCategoryDef));

        public static ThingCategoryDef BodyPartsNatural;
    }
}