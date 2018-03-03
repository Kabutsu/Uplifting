using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private AudioClip crashSound;
    
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

    private bool freezeAvailable = true;
    private bool boostAvailable = true;
    private bool boosting;
    private const int POWERUP_REFRESH_TIME = 6;

    [SerializeField]
    private GameObject freezeUI;
    [SerializeField]
    private GameObject boostUI;

    [SerializeField]
    private Sprite freezeSprite;
    [SerializeField]
    private Sprite freezeSpriteFaded;
    [SerializeField]
    private Sprite boostSprite;
    [SerializeField]
    private Sprite boostSpriteFaded;
    [SerializeField]
    private Text freezeCountdown;
    [SerializeField]
    private Image frozenPanel;
    [SerializeField]
    private Text boostCountdown;

    private GameObject boss;
    private GameObject speechBubble;

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

        freezeUI.GetComponent<SpriteRenderer>().sprite = freezeSprite;
        boostUI.GetComponent<SpriteRenderer>().sprite = boostSprite;
        
        boss = GameObject.Find("Boss");
        speechBubble = GameObject.Find("Speech Bubble");
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
		if(Input.GetKey(KeyCode.UpArrow) && !Locked() && !boosting)
        {
            velocity = (velocity < maxSpeed ? (velocity < 0 ? velocity + acceleration * 2 : velocity + acceleration) : velocity);
        }

        //move elevator down
        if (Input.GetKey(KeyCode.DownArrow) && !Locked() && !boosting)
        {
            velocity = (velocity > 0-maxSpeed ? (velocity > 0 ? velocity - acceleration * 2 : velocity - acceleration) : velocity);
        }
        
        if(Input.GetKey(KeyCode.Alpha1) && boostAvailable && velocity != 0)
        {
            StartCoroutine(BoostElevator(4, 1));
        }

        //Powerups/Freeze
        if (Input.GetKeyDown(KeyCode.Alpha2) && freezeAvailable)
        {
            StartCoroutine(FreezePassengers(2f));
        }

        //snap the elevator to a floor if it is within a certain distance of the floor
        if (Input.GetKeyDown(KeyCode.RightArrow) && Mathf.Abs(velocity) <= maxSpeed/3 && GetFloor() != LastFloor())
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
                if (velocity >= 3 * maxSpeed / 4) mainCamera.GetComponent<AudioSource>().PlayOneShot(crashSound);
                velocity = 0;
            } else if (transform.position.y < bottomOfScreen)
            {
                transform.position = new Vector3(elevatorX, bottomOfScreen);
                if (velocity <= -(3 * maxSpeed / 4)) mainCamera.GetComponent<AudioSource>().PlayOneShot(crashSound);
                velocity = 0;
            }
            if (noOfFloors > 4 &&
                (velocity < 0 && mainCamera.transform.position.y > 0 && mainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position).y <= cameraMoveDownAt) ^
                (velocity > 0 && mainCamera.transform.position.y < ((noOfFloors - 4) * 2.5f) && mainCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position).y >= cameraMoveUpAt))
            {
                mainCamera.transform.Translate(new Vector3(0, moveAmount));
                if(boss != null && speechBubble != null)
                {
                    boss.transform.Translate(new Vector3(0, moveAmount));
                    speechBubble.transform.Translate(new Vector3(0, moveAmount));
                }
            }
                
        }


        //reduce velocity by acceleration/2 each frame if not boosting
        if(!boosting) velocity = (velocity > 0 ? velocity - acceleration / 2 : (velocity < 0 ? velocity + acceleration / 2 : velocity));
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

    IEnumerator FreezePassengers(float forTime)
    {
        freezeUI.GetComponent<SpriteRenderer>().sprite = freezeSpriteFaded;
        freezeAvailable = false;
        controller.FreezePassengers();
        frozenPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(forTime);
        controller.UnfreezePassengers();
        frozenPanel.transform.localScale = new Vector3(0f, 0f, 0f);
        forTime = POWERUP_REFRESH_TIME;
        while (forTime > 0.0f)
        {
            forTime -= Time.deltaTime;
            freezeCountdown.text = ((int)Mathf.Round(forTime)).ToString();
            yield return new WaitForEndOfFrame();
        }
        freezeAvailable = true;
        freezeCountdown.text = "";
        freezeUI.GetComponent<SpriteRenderer>().sprite = freezeSprite;
    }

    IEnumerator BoostElevator(float byAmount, float forTime)
    {
        boostUI.GetComponent<SpriteRenderer>().sprite = boostSpriteFaded;
        float originalVelocity = velocity;
        velocity *= byAmount;
        boosting = true;
        boostAvailable = false;
        yield return new WaitForSeconds(forTime);
        boosting = false;
        velocity = 3 * originalVelocity / 4;
        forTime = POWERUP_REFRESH_TIME;
        while (forTime > 0.0f)
        {
            forTime -= Time.deltaTime;
            boostCountdown.text = ((int)Mathf.Round(forTime)).ToString();
            yield return new WaitForEndOfFrame();
        }
        boostAvailable = true;
        boostCountdown.text = "";
        boostUI.GetComponent<SpriteRenderer>().sprite = boostSprite;
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

    public float Velocity()
    {
        return velocity;
    }

    public bool Boosting()
    {
        return boosting;
    }

    public void StartTutorial()
    {
        boostAvailable = false;
        freezeAvailable = false;
    }

    public void MakeBoostAvailable()
    {
        boostAvailable = true;
    }
}
