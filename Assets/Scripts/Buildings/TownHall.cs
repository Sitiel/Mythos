using UnityEngine;
using System.Collections;

public class TownHall : Building
{

    public override void build()
    {
        //Center building of the game, because it is the main building 
        //I'm placing it in the global variables to make it easier to find it and not using the slow function Find of Unity
        if (GlobalVariables.townHall != null){
            Destroy(this.transform.parent.gameObject);
            return;
        }

        base.build();
        GlobalVariables.townHall = this.gameObject;
    }

    public override void updateLife(Damage d)
    {
        //Loosing the townhall make you loose the game
        base.updateLife(d);
        if (isDead)
        {
            Debug.Log("You loose");
            FindObjectOfType<UIManager>().gameOver();
        }
    }
}
