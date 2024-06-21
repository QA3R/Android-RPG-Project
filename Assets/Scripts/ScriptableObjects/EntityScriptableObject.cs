using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using ScriptableObjects;
using Entities;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="NewEntity")]
    public class EntityScriptableObject : ScriptableObject
    {
        public string Name;
        public Sprite EntityPortrait;
        public GameObject EntityModel;
        public float MaxHP;
        public float Atk;
        public float Spd;
        public float Def;
        public float Res;

        public STAScriptableObject BasicAtk;
        public List <STAScriptableObject> Skills;


    }
}
