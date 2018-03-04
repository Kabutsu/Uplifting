using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour {

    [SerializeField]
    private GameObject corridorPrefab;

    [SerializeField]
    private Sprite[] corridors = new Sprite[3];

    [SerializeField]
    private UnityEngine.UI.Text floorNumberPrefab;
    [SerializeField]
    private Canvas floorNumberCanvas;

    [SerializeField]
    private Material shaftMaterial;

    [SerializeField]
    private float lowestPositionOfElevator;
    [SerializeField]
    private int noOfFloors;

    private GameObject[] floors;

	// Use this for initialization
	void Start () {

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            noOfFloors = 7;
        } else
        {
            noOfFloors = LevelController.GetLevel() + 6;
        }
        
        floors = new GameObject[noOfFloors];
        
        float corridorHeight = 2.5f;

        Vector3 floorPosition = new Vector3(4, -6.25f);

        //spawn all the floors
		for(int i = 0; i < noOfFloors; i++)
        {
            floorPosition.y += corridorHeight;
            floors[i] = Instantiate(corridorPrefab, floorPosition, transform.rotation);
            floors[i].GetComponent<SpriteRenderer>().sprite = corridors[Random.Range(0, corridors.Length)];

            UnityEngine.UI.Text floorNumber = Instantiate(floorNumberPrefab);
            floorNumber.text = "FLOOR\n" + (i + 1);
            floorNumber.transform.SetParent(floorNumberCanvas.GetComponent<Transform>());
            floorNumber.transform.position = new Vector3(-0.19f, -3.45f + (i * 2.5f));
        }

        //make a dark elevator shaft
        GameObject shaft = GameObject.CreatePrimitive(PrimitiveType.Quad);
        shaft.transform.position = new Vector3(-2, (noOfFloors * 1.25f) - 5);
        shaft.transform.localScale = new Vector3(4, noOfFloors *  2.5f);
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
