﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private bool isRunning = false;
    private float timePlaying = 0.0f;
    private bool isEnding = false;

    [SerializeField]
    private GameObject passengerPrefab;

    [SerializeField]
    private ElevatorMove elevator;

	[SerializeField]
	private CardManager cardManager;

    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text aMPMText;

    [SerializeField]
    private Image fadeOutPanel;

    [SerializeField]
    private GameOverScreen gameOverScreen;

    List<Passenger> passengers;
    int positionCount;

	//Getters and Setters
	public CardManager GetCardManager(){return cardManager;}
	public ElevatorMove GetElevator(){return elevator;}

	// Use this for initialization
	void Start () {
        passengers = new List<Passenger>();
		cardManager = GameObject.Find ("CardManager").GetComponent<CardManager> ();
        isRunning = true;
        StartCoroutine(StartDayFadeIn());
	}

    // Update is called once per frame
    void Update() {
        if (isRunning)
        {
            timePlaying += Time.deltaTime * 40.0f;
            bool isAm;
            timeText.text = ToClockString(timePlaying, out isAm, 540.0f);
            if (isAm)
            {
                aMPMText.text = "AM";
            } else
            {
                aMPMText.text = "PM";
            }
        }

        if (isRunning && timePlaying >= 480)
        {
            isRunning = false;
            isEnding = true;
            StartCoroutine(FlashClock());
        }

        if (isEnding && passengers.Count == 0)
        {
            isEnding = false;
            elevator.Lock();
            StartCoroutine(EndDayFadeOut());
        }
	}

    public void GameOver()
    {
        if (isRunning || isEnding)
        {
            isRunning = false;
            gameOverScreen.ShowScreen(LevelController.GetLevel() - 1, timePlaying);
        } else
        {
            if (Input.GetKeyUp(KeyCode.Space)){
                LevelController.RestartGame();
            }
        }
    }

    IEnumerator FlashClock()
    {
        bool state = true;
        while (true)
        {
            state = !state;
            timeText.enabled = state;
            aMPMText.enabled = state;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator StartDayFadeIn()
    {
        for (float i = 1f; i > 0f; i -= Time.deltaTime)
        {
            fadeOutPanel.color = new Color(0f, 0f, 0f, i);
            yield return null;
        }
        fadeOutPanel.color = new Color(0f, 0f, 0f, 0f);
    }

    IEnumerator EndDayFadeOut()
    {
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            fadeOutPanel.color = new Color(0f, 0f, 0f, i);
            yield return null;
        }
        fadeOutPanel.color = new Color(0f, 0f, 0f, 1f);
        LevelController.LoadNextLevel();
    }

    public void RequestPassenger()
    {
		int toSpawn = 0;
		int rand = Random.Range (0, 10);
		if (passengers.Count < 4) {
			if (rand < 4) {
				toSpawn = 3;
			} else if (rand < 7) {
				toSpawn = 2;
			} else {
				toSpawn = 1;
			}
		} else if (passengers.Count < 5) {
			if (rand < 6) {
				toSpawn = 2;
			} else {
				toSpawn = 1;
			}
		} else if(passengers.Count < 6){
			toSpawn = 1;
        }

        if (isEnding)
        {
            toSpawn = 0;
            StartCoroutine(UnlockAfterTime(1f));
        }

		for (int i = 0; i < toSpawn; i++) {
			GameObject passenger = Instantiate(passengerPrefab, elevator.transform);

			passenger.transform.localPosition = new Vector3(6, -0.25f, -1);

			if (i == toSpawn - 1) {
				passenger.GetComponent<Passenger> ().AssignAsKeyHolder ();
			}

			passengers.Add(passenger.GetComponent<Passenger>());
		}
    }

    IEnumerator UnlockAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        elevator.Unlock();
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

    public void FreezePassengers()
    {
        foreach(Passenger passenger in passengers)
        {
            passenger.Freeze();
        }
    }

    public void UnfreezePassengers()
    {
        foreach(Passenger passenger in passengers)
        {
            passenger.Unfreeze();
        }
    }

    private string ToClockString(float time, out bool isAm)
    {
        return ToClockString(time, out isAm, 0.0f);
    }

    private string ToClockString(float time, out bool isAm, float offset)
    {
        time += offset;
        time = Mathf.FloorToInt(time);

        int timeHours = Mathf.FloorToInt(time / 720);
        isAm = ((timeHours % 2) == 0);

        int timeMinutes = (Mathf.FloorToInt(time / 60) % 24);
        timeMinutes = (timeMinutes == 0 ? 12 : timeMinutes);
        int timeSeconds = ((int) time % 60);

        return (timeMinutes < 10 ? "0" + timeMinutes.ToString() : timeMinutes.ToString()) 
            + ":" 
            + (timeSeconds < 10 ? "0" + timeSeconds.ToString() : timeSeconds.ToString());
    }

    public void RequestGameRestart()
    {
        LevelController.RestartGame();
    }

    public void RequestMainMenu()
    {
        LevelController.GoToMainMenu();
    }

}
