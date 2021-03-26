﻿using UnityEngine;
using Story;

namespace ProjectAncients.Mono
{
	public class GargantuanBabyTeleport : MonoBehaviour
	{
		public void Start()
		{
			creature = GetComponent<Creature>();
			creature.friend = Player.main.gameObject;
			InvokeRepeating("WarpToPlayer", UnityEngine.Random.value * warpInterval, warpInterval);
			cuteFishGoal.Trigger();
		}

		public void OnDrop()
		{
			creature.leashPosition = transform.position;
			WaterParkCreature component = GetComponent<WaterParkCreature>();
			bool flag = component != null && component.IsInsideWaterPark();
			cinematicTarget.SetActive(!flag);
		}

		private void WarpToPlayer()
		{
			if (Player.main.GetBiomeString().StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			Vector3 vector = Player.main.transform.position - base.transform.position;
			if (vector.magnitude > warpDistance)
			{
				Vector3 vector2 = Player.main.transform.position - vector.normalized * this.warpDistance;
				vector2.y = Mathf.Min(vector2.y, -1f);
				int num = UWE.Utils.OverlapSphereIntoSharedBuffer(base.transform.position, 5f, -1, QueryTriggerInteraction.UseGlobal);
				for (int i = 0; i < num; i++)
				{
					if (UWE.Utils.sharedColliderBuffer[i].GetComponentInParent<SubRoot>())
					{
						return;
					}
				}
				transform.position = vector2;
			}
		}

		public float warpInterval = 5f;
		public float warpDistance = 40f;
		public Creature creature;
		public GameObject cinematicTarget;

		static StoryGoal cuteFishGoal = new StoryGoal("Goal_CuteFish", Story.GoalType.Story, 0);
	}
}