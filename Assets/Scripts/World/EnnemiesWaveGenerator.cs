using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesWaveGenerator : MonoBehaviour {

    [SerializeField]
    public List<Wave> waves;
    private float globalTime = 0;

	// Update is called once per frame
	void Update () {
        if (GlobalVariables.townHall == null)
            return;
        globalTime += Time.deltaTime;
        foreach(Wave w in waves){
            if(!w.alreadyDone && globalTime > w.time){
                w.alreadyDone = true;
                float x = transform.position.x;
                float y = transform.position.z;
                for (int u = 0; u < w.units.Count; u++){
                    for (int i = 0; i < w.nbUnits[u]; i++)
                    {
                        x++;
                        if (x > transform.position.x + 5)
                        {
                            x = transform.position.x;
                            y++;
                        }
                        SimpleEnemyAI f = Instantiate(w.units[u], new Vector3(x, -1, y), Quaternion.identity);
                        f.townHall = GlobalVariables.townHall;
                    }
                }
            }
        }
       
	}
}
