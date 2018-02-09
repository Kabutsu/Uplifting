using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

    [SerializeField]
    private GameObject corridorPrefab;

    [SerializeField]
    private Material shaftMaterial;

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

        //make a dark elevator shaft
        GameObject shaft = GameObject.CreatePrimitive(PrimitiveType.Quad);
        shaft.transform.position = new Vector3(-2.65f, 0);
        shaft.transform.localScale = new Vector3(2.7f, 10 + ((noOfFloors - 2) *  2.5f));
        shaft.GetComponent<MeshRenderer>().material = shaftMaterial;

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
