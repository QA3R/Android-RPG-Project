using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkill")]
public class SingleTargetMagicAtkSO : ScriptableObject, ISkill
{ 
    [TextArea(4, 10)]
    public string Description;

    public string SkillName;
    public float DmgMultiplier;

  
    public void ExecuteSkill(Entities.Entity caster, Entities.Entity receiver)
    {
        float dmgToDeal;

        if (caster.Atk <= receiver.Res)
        {
            dmgToDeal = caster.Atk * .10f;
        }
        else
        {
            dmgToDeal = caster.Atk - receiver.Res * DmgMultiplier;
        }
    }
}
