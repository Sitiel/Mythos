using UnityEngine;
using System.Collections;

public class Unit : Entity
{

	// Update is called once per frame
    public override void updateLife(Damage d)
	{
        int lifeBefore = this.life;
        base.updateLife(d);

        body.velocity = new Vector3(0, 0, 0);
        if (isDead)
        {
            animator.SetTrigger("Death1Trigger");
            Destroy(this.gameObject, 30f);
            return;
        }


        if (lifeBefore > life)
        {
            animator.SetTrigger("GetHit3Trigger");
        }

	}

    protected Vector3 getNearestPointTo(GameObject g)
    {
        Collider c = g.GetComponent<Collider>();
        return c.ClosestPoint(this.transform.position);
    }


    protected float getRealDistBetweenGameObject(GameObject b)
    {
        //Calculate distance between the 2 colliders
        Collider c = GetComponent<Collider>();
        return Vector3.Distance(c.ClosestPoint(b.transform.position), getNearestPointTo(b));
    }
}
