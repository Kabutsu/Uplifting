using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour {

    [SerializeField]
    private Initialize init;
    [SerializeField]
    private ElevatorMove elevator;

    private GameController controller;

    private string passengerName;
    private string job;
    private int floor;
    private int rage;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
        init = GameObject.Find("Game Controller").GetComponent<Initialize>();
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorMove>();

        passengerName = NameGenerator.Name();
        job = NameGenerator.Job();
        rage = 0;

        do
        {
            floor = Random.Range(1, init.NoOfFloors());
        } while (floor == elevator.GetFloor());
        Debug.Log(floor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {

    }

    public void NotifyFloor(int floorNo)
    {
        if (floorNo == floor) LeaveElevator();
    }

    private void LeaveElevator()
    {
        controller.RemovePassenger(this);
        Destroy(this.gameObject);
    }
}
