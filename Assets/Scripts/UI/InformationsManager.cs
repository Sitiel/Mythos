using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InformationsManager : MonoBehaviour
{

    public GameObject arrow;

    private Dictionary<SimpleEnemyAI, GameObject> ennemiesArrows = new Dictionary<SimpleEnemyAI, GameObject>();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        SimpleEnemyAI[] ennemies = FindObjectsOfType<SimpleEnemyAI>();
        foreach(SimpleEnemyAI ennemy in ennemies){
            if(ennemy.isDead){
                if (ennemiesArrows.ContainsKey(ennemy))
                {
                    Destroy(ennemiesArrows[ennemy]);
                    ennemiesArrows.Remove(ennemy);
                }
                continue;
            }
            Vector3 rpos = Camera.main.WorldToScreenPoint(ennemy.transform.position + new Vector3(0,3,0));
            Vector3 pos = new Vector3(Mathf.Clamp(rpos.x, 50, Screen.width-50), Mathf.Clamp(rpos.y, 50, Screen.height-50));

            if (rpos.z < 0)
            {
                pos.x = 50;
            }

            GameObject enemyArrow = null;
            if(ennemiesArrows.ContainsKey(ennemy)){
                enemyArrow = ennemiesArrows[ennemy];
            }
            else{
                GameObject child = Instantiate(arrow);
                child.transform.parent = this.gameObject.transform;
                ennemiesArrows[ennemy] = child;
                enemyArrow = child;
            }

            enemyArrow.transform.position = pos;
        }

	}
}
