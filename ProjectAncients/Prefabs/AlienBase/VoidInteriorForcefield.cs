﻿using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;
using System.Collections;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class VoidInteriorForcefield : Spawnable
    {
        public VoidInteriorForcefield() : base("VoidInteriorForcefield", "Forcefield", ".")
        {
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab("2d72ad6c-d30d-41be-baa7-0c1dba757b7c", out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject.Destroy(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            GameObject.Destroy(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
            obj.SetActive(false);
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("2d72ad6c-d30d-41be-baa7-0c1dba757b7c");
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            
            GameObject obj = GameObject.Instantiate(prefab);
            GameObject.Destroy(obj.SearchChild("DoorSetMotorModeCollider_Walk", ECCStringComparison.Equals));
            GameObject.Destroy(obj.SearchChild("DoorSetMotorModeCollider_Swim", ECCStringComparison.Equals));
            obj.SetActive(false);
            
            gameObject.Set(obj);
        }
#endif

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };
    }
}