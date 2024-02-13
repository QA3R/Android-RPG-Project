using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill")]
public class SingleTargetPhysicalAtkSO : ScriptableObject, ISkill
{
    [TextArea(4, 10)]
    public string Description;

    public string SkillName;
    public float DmgMultiplier;

    public void ExecuteSkill(Entities.Entity caster, Entities.Entity receiver)
    {
        float dmgToDeal;

        if (caster.Atk <= receiver.Def)
        {
            dmgToDeal = caster.Atk * .10f;
        }
        else
        {
            dmgToDeal = caster.Atk - receiver.Def * DmgMultiplier;
        }
    }
}
