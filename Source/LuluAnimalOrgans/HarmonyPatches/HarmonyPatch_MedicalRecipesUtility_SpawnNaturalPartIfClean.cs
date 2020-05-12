using HarmonyLib;
using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	[HarmonyPatch(typeof(MedicalRecipesUtility), nameof(MedicalRecipesUtility.SpawnNaturalPartIfClean))]
	public static class HarmonyPatch_MedicalRecipesUtility_SpawnNaturalPartIfClean
	{
		// After spawning a natural organ, set its origin to that of the pawn it was extracted from.
		public static void Postfix(Thing __result, Pawn pawn)
		{
			// Try to get our organ origin.
			CompOrganOrigin organOrigin = __result.TryGetComp<CompOrganOrigin>();

			// Does our organ origin exist?
			if (organOrigin != null)
			{
				// Set the origin def of our organ to the pawn's def.
				organOrigin.originDef = pawn.def;
			}
		}
	}
}
