using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<SimpleEnemyAI> units;
    public List<int> nbUnits;
    public bool alreadyDone;
    public float time;

}
