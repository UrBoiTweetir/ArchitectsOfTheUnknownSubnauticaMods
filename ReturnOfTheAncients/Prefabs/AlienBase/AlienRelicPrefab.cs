using ArchitectsLibrary.Utility;
using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class AlienRelicPrefab : Spawnable
    {
        static FMODAsset relicSoundAsset;
        
        GameObject _processedPrefab;
        
        readonly float scale;
        readonly GameObject model;
        public AlienRelicPrefab(string classId, string friendlyName, string description, GameObject model, float scale) : base(classId, friendlyName, description)
        {
            this.model = model;
            this.scale = scale;
        }

#if SN1
        public override GameObject GetGameObject()
        {
            if (_processedPrefab)
            {
                _processedPrefab.SetActive(true);
                return _processedPrefab;
            }
            
            ValidateSoundAsset();
            GameObject obj = new();
            obj.SetActive(false);
            GameObject instantiatedModel = GameObject.Instantiate(model, obj.transform, false);
            instantiatedModel.transform.localPosition = Vector3.zero;
            instantiatedModel.transform.localScale = Vector3.one * scale;
            CapsuleCollider capsule = obj.AddComponent<CapsuleCollider>();
            capsule.height = 6.296409f;
            capsule.radius = 1.1f;
            capsule.direction = 1;
            obj.EnsureComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            ECCHelpers.ApplySNShaders(obj, new UBERMaterialProperties(8f, 1f, 2f));
            MaterialUtils.ApplyPrecursorMaterials(obj, 35f);
            MaterialUtils.FixIonCubeMaterials(obj, 1f);
            var soundEmitter = obj.EnsureComponent<FMOD_CustomLoopingEmitter>();
            soundEmitter.asset = relicSoundAsset;
            soundEmitter.playOnAwake = true;

            _processedPrefab = GameObject.Instantiate(obj);
            return obj;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (_processedPrefab)
            {
                _processedPrefab.SetActive(true);
                gameObject.Set(_processedPrefab);
                yield break;
            }

            ValidateSoundAsset();
            GameObject obj = new();
            obj.SetActive(false);
            GameObject instantiatedModel = GameObject.Instantiate(model, obj.transform, false);
            instantiatedModel.transform.localPosition = Vector3.zero;
            instantiatedModel.transform.localScale = Vector3.one * scale;
            CapsuleCollider capsule = obj.AddComponent<CapsuleCollider>();
            capsule.height = 6.296409f;
            capsule.radius = 1.1f;
            capsule.direction = 1;
            obj.EnsureComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            ECCHelpers.ApplySNShaders(obj, new UBERMaterialProperties(8f, 1f, 2f));
            MaterialUtils.ApplyPrecursorMaterials(obj, 35f);
            MaterialUtils.FixIonCubeMaterials(obj, 1f);
            var soundEmitter = obj.EnsureComponent<FMOD_CustomLoopingEmitter>();
            soundEmitter.asset = relicSoundAsset;
            soundEmitter.playOnAwake = true;

            _processedPrefab = GameObject.Instantiate(obj);
            _processedPrefab.SetActive(false);
            gameObject.Set(obj);
        }
#endif

        void ValidateSoundAsset()
        {
            if (relicSoundAsset == null)
            {
                relicSoundAsset = ScriptableObject.CreateInstance<FMODAsset>();
                relicSoundAsset.id = "{398345e3-b59f-4eb3-8fd0-f4e87f282968}";
                relicSoundAsset.path = "event:/env/prec_artifact_loop";
            }
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Near,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = TechType
        };
    }
}
