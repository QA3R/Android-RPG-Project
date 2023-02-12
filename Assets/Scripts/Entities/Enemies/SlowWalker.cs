using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;

namespace Entities.Enemies
{
    public class SlowWalker : Enemy
    {   
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Attack(Entity enemy)
        {
            base.Attack(enemy);
        }

        public override void AtkSkill()
        {
            //New code
            //Debuff enemy
        }
    }
}

