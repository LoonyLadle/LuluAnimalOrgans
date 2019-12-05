using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	public class SpecialThingFilterWorker_OrgansHumanlike : SpecialThingFilterWorker
	{
		public override bool Matches(Thing t)
		{
			return t.TryGetComp<CompOrganOrigin>().originDef.race.Humanlike;
		}
	}
}
