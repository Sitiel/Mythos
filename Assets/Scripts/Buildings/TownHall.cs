using UnityEngine;
using System.Collections;

public class TownHall : Building
{

    public override void build()
    {
        if (GlobalVariables.townHall != null){
            Destroy(this.transform.parent.gameObject);
            return;
        }
            

        base.build();
        GlobalVariables.townHall = this.gameObject;
    }

    public override void updateLife(Damage d)
    {
        base.updateLife(d);
        if (isDead)
        {
            Debug.Log("You loose");
            FindObjectOfType<UIManager>().gameOver();
        }
    }
}
