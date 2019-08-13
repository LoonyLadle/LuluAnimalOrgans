# Lulu's Animal Organs
Allows harvesting and installing natural organs of animals.


## Balance
The value of animal organs is scaled against the value of human organs; a grizzly bear has a base market value of 700 which is divided by a human's value of 1750 for a final value of 0.4, so a grizzly bear organ is worth 40% as much as a human organ. More or less valuable animals have correspondingly valuable organs.

It is possible to use animal surgery to train doctors very quickly at little risk, especially with a sterile animal hospital with high quality animal beds. This was not considered a balance concern. If you consider power leveling doctors on animals to be an exploit then you will have to make do with self-restraint.


## Compatibility
Lulu's Animal Organs should be compatible with all animal mods. Surgery mods that add additional harvestable parts will require a simple xpath compatibility patch to add their recipes to `AnimalThingBase`. A patch for [Lulu's Vanilla Surgery Expansion](https://github.com/LoonyLadle/LuluVanillaSurgeryExpansion) is included which can also be used as a template.


## Technical Details
This mod adds *absolutely no new defs*; all functionality is achieved by patching the existing defs and code. The most notable patch is the removal of the `RaceProps.Animal` check in `MedicalRecipesUtility.IsCleanAndDroppable` to properly allow the harvest of animal organs. This may have unexpected consequences for mods that rely on this check for other purposes, but in the author's opinion this seems highly unlikely.

The core recipes have been xpath patched onto `AnimalThingBase`, so this mod should function for all new animals added by other mods that inherit from this common def. It should not affect humanlikes or mechanoids.
