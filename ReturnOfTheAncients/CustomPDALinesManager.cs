using ECCLibrary;
using SMLHelper.V2.Handlers;
using System.Collections.Generic;
using UnityEngine;

namespace RotA
{
    public static class CustomPDALinesManager
    {
        static readonly List<string> translatedSubtitleKeys = new List<string>();

        public static void PlayPDAVoiceLine(AudioClip audioClip, string subtitleKey, string subtitleDisplayText)
        {
            if (!translatedSubtitleKeys.Contains(subtitleKey))
            {
                LanguageHandler.SetLanguageLine(subtitleKey, subtitleDisplayText);
                translatedSubtitleKeys.Add(subtitleKey);
            }
            GameObject obj = new GameObject("PDA Line Instance");
            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = audioClip;
            source.volume = ECCHelpers.GetECCVolume();
            source.Play();
            GameObject.Destroy(obj, audioClip.length);
            Subtitles.main.Add(subtitleKey);
        }

        public static void PlayPDAVoiceLineFMOD(string eventPath, string subtitleKey, string subtitleDisplayText)
        {
            if (!translatedSubtitleKeys.Contains(subtitleKey))
            {
                LanguageHandler.SetLanguageLine(subtitleKey, subtitleDisplayText);
                translatedSubtitleKeys.Add(subtitleKey);
            }
            FMODAsset soundAsset = ScriptableObject.CreateInstance<FMODAsset>();
            soundAsset.path = eventPath;
            FMODUWE.PlayOneShot(soundAsset, Player.main.transform.position);
            Subtitles.main.Add(subtitleKey);
        }
    }
}
