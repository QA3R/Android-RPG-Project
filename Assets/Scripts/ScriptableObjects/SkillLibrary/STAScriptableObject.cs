using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Physical,
    Magic,
}

[CreateAssetMenu(fileName = "NewSkill")]
public class STAScriptableObject : ScriptableObject, ISkill
{ 
    public string SkillName;
    
    [TextArea(4, 10)]
    public string Description;

    public DamageType damageType;

    public float SkillMultiplier;

  
    public virtual void ExecuteSkill(Entities.Entity caster, Entities.Entity receiver)
    {
        float dmgToDeal;

        switch (damageType)
        {
            case DamageType.Physical:

                dmgToDeal = (caster.Atk / ((receiver.Def + 100) / 100)) * SkillMultiplier;
                receiver.ReceiveDmg(dmgToDeal);
                Debug.Log(receiver.name + " received "  + dmgToDeal + " damage");

                break;

            case DamageType.Magic:

                dmgToDeal = (caster.Atk / ((receiver.Res + 100) / 100)) * SkillMultiplier;
                receiver.ReceiveDmg(dmgToDeal);
                Debug.Log(receiver.name + " received " + dmgToDeal + " damage");
                break;
        }
    }
}
