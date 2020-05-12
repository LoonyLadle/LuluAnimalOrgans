using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.AnimalOrgans
{
	[HarmonyPatch(typeof(MedicalRecipesUtility), nameof(MedicalRecipesUtility.IsCleanAndDroppable))]
	public static class HarmonyPatch_MedicalRecipesUtility_IsCleanAndDroppable
	{
		// Transpile to remove the check "!pawn.RaceProps.Animal"
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			// We will yield code instructions in blocks seperated by ret opcodes.
			List<CodeInstruction> blockInstructions = new List<CodeInstruction>();
			// If set to true while processing a block, it will not be yielded.
			bool discard = false;

			foreach (CodeInstruction instruction in instructions)
			{
				// Add this instruction to the block.
				blockInstructions.Add(instruction);

				// If an unwanted operand is found in this block, set our discard flag.
				if (instruction.opcode == OpCodes.Callvirt && (MethodInfo)instruction.operand == typeof(RaceProperties).GetProperty(nameof(RaceProperties.Animal)).GetGetMethod())
				{
					discard = true;
				}
				// Have we reached the end of the block?
				else if (instruction.opcode == OpCodes.Ret)
				{
					// Yield all instructions in block unless our discard flag is set.
					foreach (CodeInstruction blockInstruction in blockInstructions)
					{
						if (discard)
						{
							blockInstruction.opcode = OpCodes.Nop;
							blockInstruction.operand = null;
						}
						yield return blockInstruction;
					}
					// Reset our block state.
					blockInstructions.Clear();
					discard = false;
				}
			}

			// We're finished here.
			yield break;
		}
	}
}
