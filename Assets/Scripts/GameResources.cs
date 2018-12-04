using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResources : MonoBehaviour {
    public Text foodText;
    public Text woodText;
    public Text stoneText;

    public int food = 0;
    public int wood = 0;
    public int stone = 0;

	// Use this for initialization
	void Start () {
        foodText.text = food.ToString();
        woodText.text = wood.ToString();
        stoneText.text = stone.ToString();
	}


    public void updateFood(int f){
        food += f;
        foodText.text = food.ToString();
    }

    public void updateWood(int w){
        wood += w;
        woodText.text = wood.ToString();
    }

    public void updateStone(int s){
        stone += s;
        stoneText.text = stone.ToString();
    }
}
