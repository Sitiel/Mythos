using UnityEngine;
using System.Collections;

public class Damage 
{
    public int nbDamage;
    public Entity caller; 

    public Damage(int a, Entity b){
        nbDamage = a;
        caller = b;
    }
}
