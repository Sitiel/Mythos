using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    [SerializeField]
    int widthOfTheWorld = 100;
    [SerializeField]
    int lengthOfTheWorld = 100;

    [SerializeField]
    WorldCreator creator;

	// Use this for initialization
	void Start () {
        List<NestedListWrapper> world = new List<NestedListWrapper>();
        for (int i = 0; i < widthOfTheWorld; i++){
            NestedListWrapper line = new NestedListWrapper();
            for (int j = 0; j < lengthOfTheWorld; j++){
                line.list.Add(Random.Range(0, 2)); // between 0 and 2 because 2 is exclusive so it's 0 or 1
            }
            world.Add(line);
        }
        creator.loadWorld(world);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
