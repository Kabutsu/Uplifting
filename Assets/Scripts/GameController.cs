using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private bool isRunning = false;
    private bool isOver = false;
    private float timePlaying = 0.0f;
    private bool isEnding = false;

    [SerializeField]
    protected GameObject passengerPrefab;

    [SerializeField]
    protected ElevatorMove elevator;

	[SerializeField]
	protected CardManager cardManager;

    [SerializeField]
    private Text timeText;
    [SerializeField]

    private Text aMPMText;
    [SerializeField]
    private Image fadeOutPanel;

    [SerializeField]
    private Text dayMarker;
    [SerializeField]
    private Text floorsText;
    [SerializeField]
    private Text startInstructText;

    [SerializeField]
    private GameOverScreen gameOverScreen;

    protected List<Passenger> passengers;
    protected int positionCount;

	//Getters and Setters
	public CardManager GetCardManager(){return cardManager;}
	public ElevatorMove GetElevator(){return elevator;}

	// Use this for initialization
	void Start () {
        passengers = new List<Passenger>();
		cardManager = GameObject.Find ("CardManager").GetComponent<CardManager> ();
        dayMarker.text = "Day " + LevelController.GetLevel().ToString();
        floorsText.text = (LevelController.GetLevel() + 6).ToString() + " Floors";
        StartCoroutine(FadeInMessageOfTheDay());
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
        } else {
            if (!isRunning)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) && !isEnding)
                {
                    StartCoroutine(FadeOutMessageOfTheDay());
                    isRunning = true;
                }
                    
            } else
            {
                if (Input.GetKeyUp(KeyCode.Space) && isOver)
                {
                    RequestGameRestart();
                }
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
            isOver = true;
            StartCoroutine(GameOverFadeOutIn());
        } else
        {
            if (Input.GetKeyUp(KeyCode.Space)){
                RequestGameRestart();
            }
        }
    }
    
    private IEnumerator FadeInMessageOfTheDay()
    {
        Color c = new Color(0f, 0f, 0f, 0f);
        dayMarker.color = c;
        floorsText.color = c;
        startInstructText.color = c;

        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0f, 1f, i);
            dayMarker.color = c;
            floorsText.color = c;
            startInstructText.color = c;
            yield return null;
        }

        c.a = 1f;
        dayMarker.color = c;
        floorsText.color = c;
        startInstructText.color = c;
    }
    
    private IEnumerator FadeOutMessageOfTheDay()
    {
        Debug.Log("fade");
        Color c = new Color(0f, 0f, 0f, 1f);
        dayMarker.color = c;
        floorsText.color = c;
        startInstructText.color = c;

        for (float i = 1f; i > 0f; i -= Time.deltaTime * 5f)
        {
            c.a = Mathf.Lerp(0f, 1f, i);
            Debug.Log(c.a);
            dayMarker.color = c;
            floorsText.color = c;
            startInstructText.color = c;
            yield return null;
        }

        c.a = 0f;
        dayMarker.color = c;
        floorsText.color = c;
        startInstructText.color = c;
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

    IEnumerator GameOverFadeOutIn()
    {
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            fadeOutPanel.color = new Color(0f, 0f, 0f, i);
            yield return null;
        }
        fadeOutPanel.color = new Color(0f, 0f, 0f, 1f);
        gameOverScreen.ShowScreen(LevelController.GetLevel() - 1, timePlaying);
        for (float i = 1f; i > 0f; i -= Time.deltaTime)
        {
            fadeOutPanel.color = new Color(0f, 0f, 0f, i);
            yield return null;
        }
        fadeOutPanel.color = new Color(0f, 0f, 0f, 0f);
        fadeOutPanel.GetComponentInParent<Canvas>().sortingOrder = 0;
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

    public virtual void RequestPassenger()
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

    public virtual void RemovePassenger(Passenger passenger)
    {
        passengers.Remove(passenger);
    }

	public virtual int GetPassengerCount(){return passengers.Count;}

    public virtual void FreezePassengers()
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
        timeSeconds -= (timeSeconds % 5);

        return (timeMinutes < 10 ? "0" + timeMinutes.ToString() : timeMinutes.ToString()) 
            + ":" 
            + (timeSeconds < 10 ? "0" + timeSeconds.ToString() : timeSeconds.ToString());
    }

    public void RequestGameRestart()
    {
        StartCoroutine(FadeOutAndRestart());
    }

    IEnumerator FadeOutAndRestart()
    {
        fadeOutPanel.GetComponentInParent<Canvas>().sortingOrder = 2;
        for (float i = 0f; i < 1f; i += Time.deltaTime)
        {
            fadeOutPanel.color = new Color(0f, 0f, 0f, i);
            yield return null;
        }
        fadeOutPanel.color = new Color(0f, 0f, 0f, 1f);
        LevelController.RestartGame();
    }

    public void RequestMainMenu()
    {
        LevelController.GoToMainMenu();
    }

}
