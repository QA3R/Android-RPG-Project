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
        
        public GameObject EntityModel;
        public Sprite EntityPortrait;
        
        public string Name;
        public bool IsAlliedEntity;
        public bool IsControlable;
        
        public float MaxHP;
        public float Atk;
        public float Spd;
        public float Def;
        public float Res;

        public STAScriptableObject BasicAtk;
        public List <STAScriptableObject> Skills;


    }
}
