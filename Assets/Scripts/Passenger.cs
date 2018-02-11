using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour {

    [SerializeField]
    private Initialize init;
    [SerializeField]
    private ElevatorMove elevator;

    private GameController controller;
	private CardManager cardManager;

	private Card card;

    private string passengerName;
    private string job;
    private int floor;
    private int rage;
    private float positionOnLift;

	//Getters and Setters
	public string GetPassengerName(){return passengerName;}
	public string GetPassengerJob(){return job;}
	public int GetPassengerReqFloor(){return floor;}
	public int GetRage(){return rage;}


	// Use this for initialization
	void Start () {
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
        init = GameObject.Find("Game Controller").GetComponent<Initialize>();
        elevator = controller.GetElevator();
    
		cardManager = controller.GetCardManager ();

        passengerName = NameGenerator.Name();
        job = NameGenerator.Job();
        rage = 0;
    
        positionOnLift = Random.Range(-0.9f, 1.2f);
        
        do
        {
            floor = Random.Range(1, init.NoOfFloors());
        } while (floor == elevator.GetFloor());
        elevator.Lock();

        card = cardManager.ConstructCard(this);

        StartCoroutine(MoveToPosition(new Vector3(positionOnLift, -0.25f, -1), 1.6f, false));
	}
	
	// Update is called once per frame
	void Update () {
		
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
        cardManager.DismissCard (card);
        controller.RemovePassenger(this);
        elevator.Lock();
        StartCoroutine(MoveToPosition(new Vector3(6, -0.25f, -1), 1.5f, true));
    }
}
