using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesWaveGenerator : MonoBehaviour {

    [SerializeField]
    public List<Wave> waves;
    private float globalTime = 0;
    public SimpleEnemyAI fantassin;
    public GameObject townHall;

	// Update is called once per frame
	void Update () {
        globalTime += Time.deltaTime;
        foreach(Wave w in waves){
            if(!w.alreadyDone && globalTime > w.time){
                w.alreadyDone = true;
                float x = 1;
                float y = 1;
                for (int i = 0; i < w.nbFantassins; i++){
                    x++;
                    if (x > 5)
                    {
                        x = 1;
                        y++;
                    }
                    SimpleEnemyAI f = Instantiate(fantassin, new Vector3(x, -1, y), Quaternion.identity);
                    f.townHall = this.townHall;
                }
            }
        }
       
	}
}
