using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewEntity")]
    public class EntityScriptaleObject : ScriptableObject
    {
        public bool controlable;
        public string Name;

        public GameObject EntityModel;

        public int currentHP;
        public int MaxHP;
        public int Atk;
        public int Spd;
        public int Def;
        public int Res;
    }
}
