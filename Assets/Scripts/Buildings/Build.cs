using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "New building", menuName = "Build")]
public class Build : ScriptableObject
{
    public GameObject preview_ok;
    public GameObject preview_nok;
    public GameObject building;
}
