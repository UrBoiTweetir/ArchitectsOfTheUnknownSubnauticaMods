﻿using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseInteriorSpawner : AlienBaseSpawner
    {
        private float firstFloorY = 0;
        private float secondFloorY = 18;

        public override IEnumerator ConstructBase()
        {
            //Entrance hallway
            yield return StartCoroutine(SpawnPrefabsArray(pedestal_empty2, 4f, new Vector3(1, 1, 10), Vector3.one, new Vector3(-3f + 0.5f, 2f, 40f)));
            yield return StartCoroutine(SpawnPrefabsArray(pedestal_empty2, 4f, new Vector3(1, 1, 10), Vector3.one, new Vector3(3f + 1.5f + 1.5f, 2f, 40f)));

            //Aquarium
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(5.16f, firstFloorY, -20.88f), new Vector3(0f, 74f, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(-1.69f, firstFloorY, -20.82f), new Vector3(0f, 150f, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(3.24f, firstFloorY, -28.05f), new Vector3(0f, -40f, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_ameboid, new Vector3(-7.96f, firstFloorY, -22.14f), new Vector3(0f, 50, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(1.97f, firstFloorY, -31.44f), new Vector3(0f, 50, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(0.16f, firstFloorY, -15.88f), new Vector3(0f, 0f, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(-1.37f, firstFloorY, -25.05f), new Vector3(0f, -220f, 0f)));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(-1.37f, firstFloorY, -25.05f), new Vector3(0f, -220f, 0f)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(-2f, firstFloorY + 3f, -25)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(-1f, firstFloorY + 3f, -25)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(0f, firstFloorY + 3f, -25)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(1f, firstFloorY + 3f, -25)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(2f, firstFloorY + 3f, -25)));
            yield return StartCoroutine(SpawnPrefab(creature_rockgrub, new Vector3(3f, firstFloorY + 3f, -25)));

            //Aquarium room
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(25.62f, firstFloorY, 2.72f), new Vector3(0f, -135f, 0f), Vector3.one * 1.2f));
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(-25.62f, firstFloorY, 2.72f), new Vector3(0f, 135f, 0f), Vector3.one * 1.2f));
            yield return StartCoroutine(SpawnPrefab(supplies_purpleTablet, new Vector3(9.54f, 0f, 1.89f), Vector3.up * -68f));
            yield return StartCoroutine(SpawnPrefab(Mod.voidBaseTerminal.ClassID, new Vector3(0f, 0f, -8), new Vector3(0f, -180, 0f)));

            //Upper aquarium room
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(25.62f, secondFloorY, 2.72f), new Vector3(0f, -135f, 0f), Vector3.one * 1.2f));
            yield return StartCoroutine(SpawnPrefab(structure_doorwaySmall, new Vector3(-25.62f, secondFloorY, 2.72f), new Vector3(0f, 135f, 0f), Vector3.one * 1.2f));

            //Egg room
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, new Vector3(0f, secondFloorY, 32f)));
            yield return StartCoroutine(SpawnPrefab(Mod.gargEgg.ClassID, new Vector3(0f, secondFloorY + 1f, 32f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(2.5f, secondFloorY, 32f - 2.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(2.5f, secondFloorY, 32f + 2.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-2.5f, secondFloorY, 32f - 2.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-2.5f, secondFloorY, 32f + 2.5f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(-3f, secondFloorY + 1f, 32f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(3f, secondFloorY + 1f, 32f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(0f, secondFloorY + 6f, 32f), Vector3.zero, new Vector3(1f, 0.87f, 1f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(0f, secondFloorY + 6f, 32f), (Vector3.left * 180f), new Vector3(1.5f, 0.5f, 1.5f)));

            //Left lower room
            yield return StartCoroutine(SpawnPrefab(Mod.cachePingsTerminal.ClassID, new Vector3(22, 0f, 28), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(18, 0f, 24), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(26, 0f, 32), new Vector3(0f, -45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableIonCube, new Vector3(33.3f, 0f, 14f), new Vector3(0f, 27f, 0f)));
            yield return SpawnRelicInCase(new Vector3(33.78f, 0f, 11.34f), Mod.builderRelic.ClassID, new Vector3(0f, 1.35f, 0f), new Vector3(0f, -135f, 0f));

            //Right lower room
            yield return StartCoroutine(SpawnPrefab(Mod.spamTerminal.ClassID, new Vector3(-22, 0f, 28), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-18, 0f, 24), new Vector3(0f, 45f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-26, 0f, 32), new Vector3(0f, 45f, 0f)));
            yield return SpawnRelicInCase(new Vector3(-33.78f, 0f, 11.34f), Mod.bladeRelic.ClassID, new Vector3(0f, 1.35f, 0f), new Vector3(0f, 135f, 0f));
        }
    }
}
