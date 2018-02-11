using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject passengerPrefab;

    [SerializeField]
    private ElevatorMove elevator;

	[SerializeField]
	private CardManager cardManager;

    List<Passenger> passengers;
    int positionCount;

	//Getters and Setters
	public CardManager GetCardManager(){return cardManager;}
	public ElevatorMove GetElevator(){return elevator;}

	// Use this for initialization
	void Start () {
        passengers = new List<Passenger>();
		cardManager = GameObject.Find ("CardManager").GetComponent<CardManager> ();
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

	public int GetPassengerCount(){return passengers.Count;}

}
