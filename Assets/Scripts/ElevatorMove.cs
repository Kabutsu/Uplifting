﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour {
    
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;
    private float velocity;

    [SerializeField]
    private float marginOfError = 0.85f;

    [SerializeField]
    private Camera mainCamera;
    
    private GameController controller;

    private Initialize init;


    private int noOfFloors;
    private GameObject[] floors;
    
    private float bottomOfScreen;
    private float topOfScreen;

    private int cameraHeight;
    private float cameraMoveUpAt;
    private float cameraMoveDownAt;

    private float elevatorX;

    private int lastFloor;

    private bool locked;

	// Use this for initialization
	void Start () {
        elevatorX = transform.position.x;
        cameraHeight = mainCamera.pixelHeight;
        cameraMoveDownAt = cameraHeight / 4f;
        cameraMoveUpAt = 3f * cameraMoveDownAt;

        velocity = 0;
        SetLastFloor(0);

        locked = false;
        controller = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    //called by Initialize script; sets objects used by the elevator to objects created by Initialize script
    public void Initialize()
    {
        init = GameObject.Find("Game Controller").GetComponent<Initialize>();
        noOfFloors = init.NoOfFloors();
        bottomOfScreen = init.BottomOfScreen();
        topOfScreen = (noOfFloors <= 4 ? Mathf.Abs(init.BottomOfScreen()) : Mathf.Abs(init.BottomOfScreen()) + ((noOfFloors - 4) * 2.5f));
        floors = init.Floors();
    }
	
	// Update is called once per frame
	void Update () {
        //move elevator up
		if(!Locked() && Input.GetKey(KeyCode.UpArrow))
        {
            velocity = (velocity < maxSpeed ? (velocity < 0 ? velocity + acceleration * 2 : velocity + acceleration) : velocity);
        }

        //move elevator down
        if (!Locked() && Input.GetKey(KeyCode.DownArrow))
        {
            velocity = (velocity > 0-maxSpeed ? (velocity > 0 ? velocity - acceleration * 2 : velocity - acceleration) : velocity);
        }

        //snap the elevator to a floor if it is within a certain distance of the floor
        if(Input.GetKeyDown(KeyCode.RightArrow) && Mathf.Abs(velocity) <= maxSpeed/3 && GetFloor() != LastFloor())
        {
            foreach(GameObject floor in floors)
            {
                if(transform.position.y >= floor.transform.position.y - marginOfError &&
                    transform.position.y <= floor.transform.position.y + marginOfError)
                {
                    velocity = 0;
                    StartCoroutine(SnapToFloor(new Vector3(elevatorX, floor.transform.position.y), 0.6f));
                    break;
                }
            }
            if (GetFloor() != 0)
            {
                controller.BroadcastFloor(GetFloor());
                SetLastFloor(GetFloor());
				if (controller.GetPassengerCount () < 7) {
					controller.RequestPassenger();
				}
            }
        }

        //move elevator & camera
        float moveAmount = velocity * Time.deltaTime;
        if(transform.position.y <= topOfScreen && transform.position.y >= bottomOfScreen)
        {
            transform.Translate(new Vector3(0, moveAmount));
            if(transform.position.y > topOfScreen)
            {
                transform.position = new Vector3(elevatorX, topOfScreen);
                velocity = 0;
            } else if (transform.position.y < bottomOfScreen)
            {
                transform.position = new Vector3(elevatorX, bottomOfScreen);
                velocity = 0;
            }
            if (noOfFloors > 4 &&
                (velocity < 0 && mainCamera.transform.position.y > 0 && mainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position).y <= cameraMoveDownAt) ^
                (velocity > 0 && mainCamera.transform.position.y < ((noOfFloors - 4) * 2.5f) && mainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position).y >= cameraMoveUpAt))
                mainCamera.transform.Translate(new Vector3(0, moveAmount));
        }

        //reduce velocity by acceleration/2
        velocity = (velocity > 0 ? velocity - acceleration / 2 : (velocity < 0 ? velocity + acceleration / 2 : velocity));
        Debug.Log(velocity);
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

    public int GetFloor()
    {
        int floorNo = 1;
        foreach(GameObject floor in floors)
        {
            if(transform.position.y >= floor.transform.position.y - marginOfError &&
                transform.position.y <= floor.transform.position.y + marginOfError)
            {
                return floorNo;
            }
            floorNo++;
        }

        return 0;
    }

    private void SetLastFloor(int floor)
    {
        lastFloor = floor;
    }

    private int LastFloor()
    {
        return lastFloor;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    public bool Locked()
    {
        return locked;
    }
}
