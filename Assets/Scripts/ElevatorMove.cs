using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour {

    [SerializeField]
    protected float speed;

    [SerializeField]
    private float marginOfError = 0.85f;

    [SerializeField]
    private int noOfFloors;
    [SerializeField]
    public GameObject[] floors;

    [SerializeField]
    private float bottomOfScreen = -3.75f;
    [SerializeField]
    private float topOfScreen = 3.75f;

    private float elevatorX;

	// Use this for initialization
	void Start () {
        elevatorX = transform.position.x;
        floors = new GameObject[noOfFloors];
    }
	
	// Update is called once per frame
	void Update () {
        //move elevator up
		if(Input.GetKey(KeyCode.UpArrow))
        {
            if(transform.position.y <= topOfScreen) transform.Translate(new Vector3(0, speed * Time.deltaTime));
        }

        //move elevator down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if(transform.position.y >= bottomOfScreen) transform.Translate(new Vector3(0, -(speed * Time.deltaTime)));
        }

        //snap the elevator to a floor if it is within a certain distance of the floor
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            foreach(GameObject floor in floors)
            {
                if(transform.position.y >= floor.transform.position.y - marginOfError &&
                    transform.position.y <= floor.transform.position.y + marginOfError)
                {
                    StartCoroutine(SnapToFloor(new Vector3(elevatorX, floor.transform.position.y), 1f));
                    break;
                }
            }
        }
    }

    //move the elevator to the same position as a floor in a specified time
    IEnumerator SnapToFloor(Vector3 toPosition, float inTime)
    {
        var fromPosition = transform.position;
        for(var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
    }
}
