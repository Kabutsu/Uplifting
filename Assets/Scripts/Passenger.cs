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
    private float positionOnLift;

	// Use this for initialization
	void Start ()
    {
        elevator = GameObject.Find("Elevator").GetComponent<ElevatorMove>();
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();

        passengerName = NameGenerator.Name();
        job = NameGenerator.Job();
        rage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        positionOnLift = Random.Range(-0.9f, 1.2f);
        init = GameObject.Find("Game Controller").GetComponent<Initialize>();

        elevator = GameObject.Find("Elevator").GetComponent<ElevatorMove>();
        do
        {
            floor = Random.Range(1, init.NoOfFloors());
        } while (floor == elevator.GetFloor());
        elevator.Lock();

        StartCoroutine(MoveToPosition(new Vector3(positionOnLift, -0.25f, -1), 1.6f, false));
    }

    IEnumerator MoveToPosition(Vector3 toPosition, float inTime, bool destroy)
    {
        var fromPosition = transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
        elevator.Unlock();
        if (destroy) Destroy(this.gameObject);
    }

    public void NotifyFloor(int floorNo)
    {
        if (floorNo == floor) LeaveElevator();
    }

    private void LeaveElevator()
    {
        controller.RemovePassenger(this);
        elevator.Lock();
        StartCoroutine(MoveToPosition(new Vector3(6, -0.25f, -1), 1.5f, true));
    }
}
