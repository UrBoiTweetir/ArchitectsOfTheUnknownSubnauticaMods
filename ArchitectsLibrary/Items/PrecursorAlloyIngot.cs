﻿using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Items
{
    class PrecursorAlloyIngot : Craftable
    {
        GameObject prefab;

        public PrecursorAlloyIngot() : base("PrecursorIngot", "Precursor Alloy Ingot", "An alien resource with mysterious properties and unprecedented integrity.")
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.TitaniumIngot, 1), new Ingredient(TechType.PrecursorIonCrystal, 2) });
        }

        public override bool UnlockedAtStart => false;

#if SN1
        public override GameObject GetGameObject()
        {
            if (prefab is null)
            {
                prefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"));
                prefab.SetActive(false);

                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.EnsureComponent<Pickupable>();
                prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 15f;
                rb.isKinematic = true;
                prefab.EnsureComponent<WorldForces>();
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                var inspect = prefab.EnsureComponent<InspectOnFirstPickup>();
                inspect.pickupAble = prefab.GetComponent<Pickupable>();
                inspect.collision = prefab.GetComponent<Collider>();
                inspect.rigidBody = prefab.GetComponent<Rigidbody>();
                inspect.animParam = "holding_precursorkey";
                inspect.inspectDuration = 4.1f;

                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 12);
            }
            return prefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (prefab is null)
            {
                prefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"));
                prefab.SetActive(false);

                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.EnsureComponent<Pickupable>();
                prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 15f;
                rb.isKinematic = true;
                prefab.EnsureComponent<WorldForces>();
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                var inspect = prefab.EnsureComponent<InspectOnFirstPickup>();
                inspect.pickupAble = prefab.GetComponent<Pickupable>();
                inspect.collision = prefab.GetComponent<Collider>();
                inspect.rigidBody = prefab.GetComponent<Rigidbody>();
                inspect.animParam = "holding_precursorkey";
                inspect.inspectDuration = 4.1f;

                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 12);
            }

            yield return null;
            gameObject.Set(prefab);
        }
#endif
    }
}
