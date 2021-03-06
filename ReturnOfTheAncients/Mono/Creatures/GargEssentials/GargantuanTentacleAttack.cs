namespace RotA.Mono.Creatures.GargEssentials
{
    using ECCLibrary;
    using System.Collections;
    using UnityEngine;
    
    public class GargantuanTentacleAttack : MeleeAttack
    {
        GargantuanGrab grab;
        AudioSource attackSource;
        ECCAudio.AudioClipPool biteClipPool;
        GargantuanBehaviour behaviour;
        TrailManager leftTentacleTrail;
        TrailManager rightTentacleTrail;

        void Start()
        {
            grab = GetComponent<GargantuanGrab>();
            attackSource = gameObject.AddComponent<AudioSource>();
            attackSource.minDistance = 10f;
            attackSource.maxDistance = 40f;
            attackSource.spatialBlend = 1f;
            attackSource.volume = ECCHelpers.GetECCVolume();
            biteClipPool = ECCAudio.CreateClipPool("GargTentacleAttack");
            gameObject.SearchChild("TentacleTrigger").EnsureComponent<OnTouch>().onTouch = new OnTouch.OnTouchEvent();
            gameObject.SearchChild("TentacleTrigger").EnsureComponent<OnTouch>().onTouch.AddListener(OnTouch);
            behaviour = GetComponent<GargantuanBehaviour>();
            leftTentacleTrail = gameObject.SearchChild("MLT").GetComponent<TrailManager>();
            rightTentacleTrail = gameObject.SearchChild("MRT").GetComponent<TrailManager>();
        }
        public override void OnTouch(Collider collider) //A long method having to do with interaction with an object and the mouth.
        {
            if (frozen) //Stasis rifle = no attack
            {
                return;
            }
            if (liveMixin.IsAlive() && Time.time > behaviour.timeCanAttackAgain) //If it can attack, continue
            {
                Creature thisCreature = gameObject.GetComponent<Creature>();
                if (thisCreature.Aggression.Value >= 0.9f && thisCreature.Hunger.Value >= 0.6f) //This creature must be super angry to do this
                {
                    GameObject target = GetTarget(collider);
                    if (lastTarget.target != target)
                    {
                        return;
                    }
                    if (!grab.IsHoldingVehicle())
                    {
                        Player player = target.GetComponent<Player>();
                        if (player != null)
                        {
                            if (!player.CanBeAttacked() || !player.liveMixin.IsAlive() || player.cinematicModeActive)
                            {
                                return;
                            }
                        }
                        else if (grab.GetCanGrabVehicle())
                        {
                            SeaMoth component4 = target.GetComponent<SeaMoth>();
                            if (component4 && !component4.docked)
                            {
                                grab.GrabGenericSub(component4);
                                thisCreature.Aggression.Value -= 0.25f;
                                return;
                            }
                            Exosuit component5 = target.GetComponent<Exosuit>();
                            if (component5 && !component5.docked)
                            {
                                grab.GrabExosuit(component5);
                                thisCreature.Aggression.Value -= 0.25f;
                                return;
                            }
                        }
                        LiveMixin liveMixin = target.GetComponent<LiveMixin>();
                        if (liveMixin == null) return;
                        if (!liveMixin.IsAlive())
                        {
                            return;
                        }
                        if (!CanAttackTargetFromPosition(target))
                        {
                            return;
                        }
                        else
                        {
                            var num = DamageSystem.CalculateDamage(GetBiteDamage(target), DamageType.Normal, target);
                            if (liveMixin.health - num <= 0f) // make sure that the nodamage cheat is not on
                            {
                                StartCoroutine(PerformBiteAttack(target));
                                behaviour.timeCanAttackAgain = Time.time + 4f;
                                attackSource.clip = biteClipPool.GetRandomClip();
                                attackSource.Play();
                            }
                        }
                        StartCoroutine(PlayTentacleAnimation());
                        thisCreature.Aggression.Value -= 0.15f;
                    }
                }
            }
        }
        private bool CanAttackTargetFromPosition(GameObject target) //A quick raycast check to stop the Gargantuan from attacking through walls. Taken from the game's code (shh).
        {
            Vector3 direction = target.transform.position - transform.position;
            float magnitude = direction.magnitude;
            int num = UWE.Utils.RaycastIntoSharedBuffer(transform.position, direction, magnitude, -5, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < num; i++)
            {
                Collider collider = UWE.Utils.sharedHitBuffer[i].collider;
                GameObject gameObject = (collider.attachedRigidbody != null) ? collider.attachedRigidbody.gameObject : collider.gameObject;
                if (!(gameObject == target) && !(gameObject == base.gameObject) && !(gameObject.GetComponent<Creature>() != null))
                {
                    return false;
                }
            }
            return true;
        }
        public override float GetBiteDamage(GameObject target) //Extra damage to Cyclops. Otherwise, does its base damage.
        {
            if (target.GetComponent<SubControl>() != null)
            {
                return 300f; //cyclops damage
            }
            if (target.GetComponent<Player>())
            {
                return 50f;
            }
            if (target.GetComponent<Vehicle>())
            {
                return 100f;
            }
            return 2500f; //base damage
        }
        public void OnVehicleReleased() //Called by gargantuan behavior. Gives a cooldown until the next bite.
        {
            behaviour.timeCanAttackAgain = Time.time + 4f;
        }
        private IEnumerator PerformBiteAttack(GameObject target) //A delayed attack, to let him chomp down first.
        {
            yield return new WaitForSeconds(2f);
            if (target) target.GetComponent<LiveMixin>().TakeDamage(GetBiteDamage(target));
        }
        IEnumerator PlayTentacleAnimation()
        {
            float random = Random.value;
            creature.GetAnimator().SetFloat("random", random);
            TrailManager trailToDisable;
            if (random < 0.5f)
            {
                trailToDisable = rightTentacleTrail;
            }
            else
            {
                trailToDisable = leftTentacleTrail;
            }
            creature.GetAnimator().SetTrigger("tentacleattack");
            trailToDisable.SetEnabled(false);
            yield return new WaitForSeconds(4f);
            trailToDisable.SetEnabled(true);
        }
    }
}
