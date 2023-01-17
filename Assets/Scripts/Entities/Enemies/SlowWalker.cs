using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

namespace Entities.Enemies
{
    public class SlowWalker : Enemy
    {
        [SerializeField] private EntityScriptableObject entityType;

        public SlowWalker()
        {
            Name = "";
            Hp= 1f;
            Atk= 1f;
            Spd= 1f;
            Def = 1f;
            Res= 1f;
        }

        // Start is called before the first frame update
        void Start()
        {
            Name = entityType.Name;
            Hp = entityType.MaxHP;
            Atk = entityType.Atk;
            Spd = entityType.Spd;
            Def = entityType.Def;
            Res = entityType.Res;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

