using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject passengerPrefab;

    [SerializeField]
    private ElevatorMove elevator;

    List<Passenger> passengers;

	// Use this for initialization
	void Start () {
        passengers = new List<Passenger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestPassenger()
    {
        GameObject passenger = Instantiate(passengerPrefab, elevator.transform);
        passenger.GetComponent<Passenger>().Spawn();
        passengers.Add(passenger.GetComponent<Passenger>());
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
