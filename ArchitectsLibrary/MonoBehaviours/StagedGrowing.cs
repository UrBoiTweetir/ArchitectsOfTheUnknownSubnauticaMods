using System.Linq;
using System.Collections.Generic;
using System.Collections;
using HarmonyLib;
using UnityEngine;
using UWE;
using ArchitectsLibrary.Utility;
namespace ArchitectsLibrary.MonoBehaviours
{
    /// <summary>
    /// a <see cref="MonoBehaviour"/> that makes the creature grow up over time outside of the ACU.
    /// </summary>
    public class StagedGrowing : MonoBehaviour
    {
        /// <summary>
        /// the next Creature's <see cref="TechType"/> that the current Creature gonna turn to when it reaches the max grow size.
        /// <br/>
        /// if assigned to <see cref="TechType.None"/>, it wont turn to anything.
        /// <br/>
        /// defaulted to <see cref="TechType.None"/>.
        /// </summary>
        public TechType nextStageTechType = TechType.None;
        
        /// <summary>
        /// the amount of days its gonna take for this creature to reach the max grow size.
        /// <br/>
        /// defaulted to 5.
        /// </summary>
        public float daysToNextStage = 5;
        
        /// <summary>
        /// the initial start size for the next creature.
        /// <br/>
        /// defaulted to 1 (original size).
        /// </summary>
        public float nextStageStartSize = 1f; // the next creature start size
        
        /// <summary>
        /// the size this creature is gonna grow up to over time.
        /// </summary>
        public float maxGrowSize = 1f;

        Vector3 startSize;
        float startTime;
        float growTime;
        float t = 0;
        Dictionary<CreatureAction, float> originalSpeeds = new();

        void Start()
        {
            List<CreatureAction> creatureActions = gameObject.GetComponentsInChildren<CreatureAction>(true).Where((x) => x.GetType().GetField("swimVelocity") != null)?.ToList() ?? new List<CreatureAction>();
            foreach (CreatureAction creatureAction in creatureActions)
            {
                Traverse swimVelocity = Traverse.Create(creatureAction).Field("swimVelocity");
                if (swimVelocity.FieldExists())
                {
                    originalSpeeds[creatureAction] = swimVelocity.GetValue<float>();
                }
            }
            startSize = gameObject.transform.localScale;
            startTime = DayNightCycle.main.timePassedAsFloat;
            growTime = (1200 * daysToNextStage) ;
            CoroutineHost.StartCoroutine(StartGrowing());
        }

        IEnumerator StartGrowing()
        {
            while (t < 1)
            {
                if (Time.timeScale > 0f)
                {
                    WaterParkCreature waterParkCreature = gameObject.GetComponent<WaterParkCreature>();
                    if (waterParkCreature is null || !waterParkCreature.IsInsideWaterPark())
                    {
                        t = (DayNightCycle.main.timePassedAsFloat - startTime) / growTime;
                        var scale = Vector3.Lerp(startSize, Vector3.one * maxGrowSize, t);
                        gameObject.transform.localScale = scale;

                        foreach (KeyValuePair<CreatureAction, float> pair in originalSpeeds)
                        {
                            Traverse swimVelocity = Traverse.Create(pair.Key).Field("swimVelocity");
                            if (swimVelocity.FieldExists())
                            {
                                swimVelocity.SetValue(pair.Value * scale.x);
                            }
                        }
                    }
                    else
                    {
                        t = (DayNightCycle.main.timePassedAsFloat - startTime) / growTime;
                    }
                }
                yield return null;
            }

            if (nextStageTechType != TechType.None)
            {
                CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(nextStageTechType, false);
                yield return task;

                GameObject prefab = task.GetResult();
                prefab.SetActive(false);

                GameObject nextStageObject = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation, Vector3.one * nextStageStartSize, false);

                gameObject.SemiInActive();

                // TODO: fix Staged Growth for the ACU creatures or get rid of it completely.
                /*if (gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                {
                    gameObject.SetActive(false);
                    WaterParkCreature parkCreature = nextStageObject.EnsureComponent<WaterParkCreature>();
                    Pickupable pickupable = nextStageObject.EnsureComponent<Pickupable>();
                    WaterParkCreatureParameters waterParkCreatureParameters = WaterParkCreature.GetParameters(nextStageTechType);
                    WaterPark waterPark = waterParkCreature.currentWaterPark;
                    parkCreature.age = 0f;
                    parkCreature.parameters = waterParkCreatureParameters;
                    parkCreature.pickupable = pickupable;
                    pickupable.isPickupable = true;
                    pickupable.timeDropped = Time.time;
                    waterPark.RemoveItem(waterParkCreature);
                    waterPark.AddItem(parkCreature);
                }*/
                //nextStageObject.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
                nextStageObject.SetActive(true);
                nextStageObject.transform.localScale = Vector3.one * nextStageStartSize;
                nextStageObject.EnsureComponent<StagedGrowing>();
                Destroy(gameObject);
            }
            else
            {
                /*if (gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                    gameObject.EnsureComponent<Pickupable>().isPickupable = true;*/

                Destroy(this);
            }

            yield break;
        }
    }
}