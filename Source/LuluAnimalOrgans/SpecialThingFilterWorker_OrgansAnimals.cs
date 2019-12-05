using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	public class SpecialThingFilterWorker_OrgansAnimal : SpecialThingFilterWorker
	{
		public override bool Matches(Thing t)
		{
			return t.TryGetComp<CompOrganOrigin>().originDef.race.Animal;
		}
	}
}
