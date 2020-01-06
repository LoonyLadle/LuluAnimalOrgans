using RimWorld;
using System.Linq;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	public class CompOrganOrigin : ThingComp
	{
		// Default to human originator.
		public ThingDef originDef = ThingDefOf.Human;

		public override void PostExposeData()
		{
			Scribe_Defs.Look(ref originDef, nameof(originDef));
		}

		public override void PostPostGeneratedForTrader(TraderKindDef trader, int forTile, Faction forFaction)
		{
			// 20% chance generated organ will be for an animal when generated for a trader.
			if (Rand.Chance(0.2f))
			{
				// Select a random pawn def that this trader will trade.
				if (DefDatabase<ThingDef>.AllDefs.Where(d => typeof(Pawn).IsAssignableFrom(d.thingClass) && trader.WillTrade(d)).TryRandomElement(out ThingDef result))
				{
					// Set the organ's origin to that def.
					originDef = result;
				}
			}
		}

		public override string TransformLabel(string label)
		{
			// Will change "lung" to "human lung", for example.
			return originDef.label + " " + label;
		}

		public override bool AllowStackWith(Thing other)
		{
			CompOrganOrigin otherComp = other.TryGetComp<CompOrganOrigin>();
			return (otherComp != null) && (otherComp.originDef == originDef);
		}

		public override void PostSplitOff(Thing piece)
		{
			base.PostSplitOff(piece);
			piece.TryGetComp<CompOrganOrigin>().originDef = originDef;
		}
	}
}
