using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewEntity")]
    public class EntityScriptableObject : ScriptableObject
    {
        public string Name;

        public GameObject EntityModel;
        public float MaxHP;
        public float Atk;
        public float Spd;
        public float Def;
        public float Res;

        public List<ArtifactScriptableObject> EquipedArtifacts;
    }
}
