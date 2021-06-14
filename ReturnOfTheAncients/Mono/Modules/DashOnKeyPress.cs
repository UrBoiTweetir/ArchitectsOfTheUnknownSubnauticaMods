﻿using UnityEngine;
using ArchitectsLibrary.Utility;

namespace RotA.Mono.Modules
{
    public class DashOnKeyPress : MonoBehaviour
    {
        Exosuit exosuit;
        private AudioSource audioSource;
        const float maxThrustConsumption = 0.3f;
        const float energyConsumption = 15f;

        void OnEnable()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.maxDistance = 50f;
            audioSource.volume = ECCLibrary.ECCHelpers.GetECCVolume();
        }
        void OnDisable()
        {
            Destroy(audioSource);
        }
        public void Start()
        {
            if (!uGUI.isLoading)
            {
                ErrorMessage.AddMessage(string.Format("Press {0} to initiate an Ion dash.", LanguageUtils.FormatKeyCode(Mod.config.PrawnSuitDashKey)));
            }
            exosuit = GetComponent<Exosuit>();
        }

        void Update()
        {
            if(Player.main.currentMountedVehicle == exosuit)
            {
                if (Input.GetKeyDown(Mod.config.PrawnSuitDashKey))
                {
                    float thrustPowerToUse = Mathf.Min(exosuit.thrustPower, maxThrustConsumption);
                    if(thrustPowerToUse >= 0.05f)
                    {
                        if (!exosuit.ConsumeEnergy(energyConsumption))
                            return;
                        exosuit.thrustPower -= thrustPowerToUse;
                        exosuit.useRigidbody.AddForce(GetThrustForce(thrustPowerToUse), ForceMode.VelocityChange);
                        exosuit.fxcontrol.Play(1);
                    }
                }
            }
        }

        Vector3 GetThrustForce(float thrustPower)
        {
            return MainCamera.camera.transform.forward * thrustPower * 60f;
        }
    }
}
