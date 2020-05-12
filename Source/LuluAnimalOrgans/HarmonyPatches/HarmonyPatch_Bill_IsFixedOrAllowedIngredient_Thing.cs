using HarmonyLib;
using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	[HarmonyPatch(typeof(Bill), nameof(Bill.IsFixedOrAllowedIngredient), typeof(Thing))]
	public static class HarmonyPatch_Bill_IsFixedOrAllowedIngredient_Thing
	{
		// Restrict natural organ installations to whose where the organ's origin matches the patient.
		public static bool Postfix(bool __result, Bill __instance, Thing thing)
		{
			// If if the result was already false nothing we're going to do here will change that so just quit now.
			if (!__result) return false;

			// Try to get our organ origin.
			CompOrganOrigin organOrgin = thing.TryGetComp<CompOrganOrigin>();

			// Does our organ origin exist?
			if (organOrgin != null)
			{   
				// Is our bill giver (patient) a pawn and the recipe a natural part installation?
				if ((__instance.billStack.billGiver is Pawn patient) && (__instance.recipe.Worker is Recipe_InstallNaturalBodyPart))
				{
					// Return whether our origin origin def is the same as our patient.
					return organOrgin.originDef == patient.def;
				}
			}
			// Default return original result.
			return __result;
		}
	}
}
