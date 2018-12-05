using UnityEngine;
using System.Collections;

public class PlayerChecker : MonoBehaviour
{
    public GameObject thirdPersonPlayer;
    public Camera cameraWhenPlayerDie;
	// Use this for initialization
	void Start()
	{
        cameraWhenPlayerDie.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
        if (GlobalVariables.player != null && GlobalVariables.player.transform.position.y < -20)
        {
            GlobalVariables.player.life = 0;
            GlobalVariables.player.isDead = true;
        }
        if(GlobalVariables.player != null && GlobalVariables.player.isDead){
            StartCoroutine(_revive());
        }	
	}

    public IEnumerator _revive(){
        cameraWhenPlayerDie.gameObject.SetActive(true);
        Destroy(GlobalVariables.player.transform.parent.gameObject);
        GlobalVariables.player = null;
        yield return new WaitForSeconds(10f);
        if(GlobalVariables.townHall != null)
            Instantiate(thirdPersonPlayer, GlobalVariables.townHall.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        else
            Instantiate(thirdPersonPlayer, new Vector3(0, 10, 0), Quaternion.identity);
        cameraWhenPlayerDie.gameObject.SetActive(false);
    }
}
