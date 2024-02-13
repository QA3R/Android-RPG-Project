using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill 
{
    void ExecuteSkill(Entities.Entity caster, Entities.Entity receiver);
}
