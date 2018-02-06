using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

    [SerializeField]
    private GameObject corridorPrefab;

    [SerializeField]
    private float lowestPositionOfElevator;
    [SerializeField]
    private int noOfFloors;

    private GameObject[] floors;

	// Use this for initialization
	void Start () {
        floors = new GameObject[noOfFloors];
        
        float corridorHeight = corridorPrefab.GetComponent<Renderer>().bounds.size.y;

        Vector3 floorPosition = new Vector3(3.82f, lowestPositionOfElevator - corridorHeight);

        //spawn all the floors
		for(int i = 0; i < noOfFloors; i++)
        {
            floorPosition.y += corridorHeight;
            floors[i] = Instantiate(corridorPrefab, floorPosition, transform.rotation);
        }

        //give the Elevator all the info it needs from this script
        GameObject.Find("Elevator").GetComponent<ElevatorMove>().Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float BottomOfScreen()
    {
        return lowestPositionOfElevator;
    }

    public int NoOfFloors()
    {
        return noOfFloors;
    }

    public GameObject[] Floors()
    {
        return floors;
    }
}
