namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Common methods to interact with the Precursor Fabricator.
    /// </summary>
    public static class PrecursorFabricatorService
    {
        /// <summary>
        /// Adds a Crafting Node to the Precursor Fabricator
        /// </summary>
        /// <param name="techType"><see cref="TechType"/> to add</param>
        public static void SubscribeToFabricator(TechType techType)
        {
            if (Main.PrecursorFabricator.IsPatched)
            {
                Main.PrecursorFabricator.Root.AddCraftingNode(techType);
                return;
            }
            
            Main.TechTypesToAdd.Add(techType);
        }
    }
}