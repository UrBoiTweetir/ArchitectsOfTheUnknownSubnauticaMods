using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;


namespace LeviathanEggs.Prefabs
{
    public class ReaperEgg : EggPrefab
    {
        public ReaperEgg()
            : base("ReaperEgg", "Reaper Leviathan Egg", "Reapers hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }

        public override GameObject Model => LoadGameObject("RobotEgg");
        public override TechType HatchingCreature => TechType.ReaperLeviathan;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("RobotEgg");

        public void InitializeObject(GameObject prefab)
        {
            Material material = new Material(Shader.Find("MarmosetUBER"))
            {
                mainTexture = LoadTexture2D("RobotEggDiffuse"),
            };
            material.EnableKeyword("MARMO_NORMALMAP");
            material.EnableKeyword("MARMO_SPECMAP");
            material.EnableKeyword("MARMO_EMISSION");

            material.SetTexture(ShaderPropertyID._Illum, LoadTexture2D("RobotEggIllum"));
            material.SetTexture(ShaderPropertyID._SpecTex, LoadTexture2D("RobotEggDiffuse"));
            material.SetTexture(ShaderPropertyID._BumpMap, LoadTexture2D("RobotEggNormal"));

            Renderer[] renderers = prefab.GetAllComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
            {
                rend.material = material;
                rend.sharedMaterial = material;
            }

            prefab.AddComponent<SpawnLocations>();
        }
    }
}