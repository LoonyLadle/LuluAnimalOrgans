using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	public class StatPart_OrganOrigin : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			// Only apply our factor if it makes sense to do so.
			if (MyTryGetFactor(req, out float factor))
			{
				val *= factor;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			// Only explain our factor if it makes sense to do so.
			if (MyTryGetFactor(req, out float factor))
			{
				// "Multiplier for {0} organ"
				return "LuluAnimalOrgans_StatsReport_OrganOrigin".Translate(req.Thing.TryGetComp<CompOrganOrigin>().originDef.label) + ": x" + factor.ToStringPercent();
			}
			return null;
		}

		private bool MyTryGetFactor(StatRequest req, out float factor)
		{
			// Get our organ origin comp, if possible.
			CompOrganOrigin organOrgin = req.Thing?.TryGetComp<CompOrganOrigin>();

			if (organOrgin != null)
			{
				// Normalize against human market value (most animals are worth less than humans).
				factor = organOrgin.originDef.BaseMarketValue / ThingDefOf.Human.BaseMarketValue;
				return true;
			}
			
			// Default case if no thing or comp.
			factor = 1f;
			return false;
		}
	}
}
