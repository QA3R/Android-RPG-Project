using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public interface IDamageable
{
    void ReceiveDMG(float dmgReceived, Entities.Entity receiver) 
    {
        receiver.Hp -= dmgReceived;
    }
    
}
