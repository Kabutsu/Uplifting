using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject passengerPrefab;

    [SerializeField]
    private ElevatorMove elevator;

    List<Passenger> passengers;
    int positionCount;

	// Use this for initialization
	void Start () {
        passengers = new List<Passenger>();
        positionCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestPassenger()
    {
        if(passengers.Count < 6)
        {
            GameObject passenger = Instantiate(passengerPrefab, elevator.transform);

            passenger.transform.localPosition = new Vector3(6, -0.25f, -1);

            passenger.GetComponent<Passenger>().Spawn();
            
            passengers.Add(passenger.GetComponent<Passenger>());
        }
    }

    public void BroadcastFloor(int floorNo)
    {
        List<Passenger> temp_passengers = new List<Passenger>(passengers);
        foreach(Passenger passenger in temp_passengers)
        {
            passenger.NotifyFloor(floorNo);
        }
    }

    public void RemovePassenger(Passenger passenger)
    {
        passengers.Remove(passenger);
    }
}
