using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialGameController : GameController {

    Queue<string> tutorialText = new Queue<string>(new List<string>()
        {
        "Good morning and welcome to your new role as Green Meadows Waste Management Solutions Inc©’s new Elevation Consultant!\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Your job will mean ensuring that employees can get to where they need to go in a safe and timely manner.\n\nI’ll be walking you through your first morning.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Fortunately, last night was our Annual Mid-February Office Christmas party, so we’re expecting a slow morning.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[jim_walk_on]",
        "This is Jim.#",
        "[jim_bing]",
        "Jim works in accounting, which is on the 3rd Floor.\n\nThankfully, he’s not in too much of a rush to get to his desk today, so I’ll get a chance to show you the ropes.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[jim_into_lift]",
        "Let's get a move on. Head on up to level 3.\n\n\n\n<b>Press the {UP} key to go up.\nPress {DOWN} to move down if you overshoot.</b>#",
        "[lift_to_3]",
        "Jim has just realised he needs the toilet. Quite urgently.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[jim_exclaim]",
        "The nearest toilets are on the 2nd Floor. Let’s try and get him there as quick as we can.\n\n\n\n<b>Press the {DOWN} key to go down.</b>#",
        "[lift_to_2]",
        "We should probably let him out. Sooner rather than later.\n\n\n\n<b>Press the {RIGHT} key to open the doors</b>#",
        "[let_jim_off]",
        "[jim_off_lift]",
        "[barbra_walk_on]",
        "This is Barbra.#",
        "[barbra_exclaim]",
        "Barbra is a Central Optimisation Associate, and so is obviously a very busy person.\n\nSo busy in fact that she’s late to a meeting on the 6th Floor.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[barbra_on_lift]",
        "Thanks to the latest in Low-speed Electron Accretion technology we can measure our employee’s stress levels remotely.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[stressometer]",
        "This is Barbra’s Stress-O-Meter, all of your passengers will have one.\n\nYou can see their stress, and their destination.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "The longer they have to wait until they get to their destination, the more stressed they’ll get.\n\nI’ve been asked to advice you not to let this get too high.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "We should probably get going. Get Barbra up to the <b>6th Floor</b> ASAP.\n\n\n\n<b>• {UP} to go up\n• {DOWN} to go down\n• {RIGHT} to open the doors.</b>#",
        "[lift_to_6]",
        "[barbra_exit_lift]",
        "Fantastic! I'll just pop off for my coffee break - I'll be back, but let's see how you do without me.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[boss_leave]",
        "[boss_return]",
        "Great job! Just one more thing before I hand you the reigns...\n\n\n\n<b>Press {SPACE} to continue</b>",
        "We've modded your elevator just a little bit.\n\nTry it out - <b>press {1}</b> while moving to activate turbo boosting!#",
        "[boost_tutorial]",
        "See how much fun that is?!\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[add_freeze]",
        "We've also added the latest cryogenic technology to freeze your passengers' Stress-O-Meters for a few seconds.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Just remember to <b>press {2}</b> if you're in a bit of a pickle if you want to see what it does!\n\nI'll let you try it out while I take another coffee break.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Well, you seem to have got the hang of this quite quickly. I’d say that’s half an hour for lunch.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Unless you'd like me to go through it again, just to be sure?\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Do you want to repeat the tutorial? \n\n\n\n<b>{UP}: Yes\n{DOWN}: No</b>#",
        "[tutorial_repeat_choice]"
        }
    );

    List<string>failBarbra = new List<string>()
    {
        "Oh no! Barbra's rage meter filled! Maybe we should go back in time to before we met her?\n\n\n\n<b>[Press {SPACE} to continue]</b>",
        "[barbra_reset]",
        "[barbra_walk_on]",
        "This is Barbra.#",
        "[barbra_exclaim]",
        "Barbra is a Central Optimisation Associate, and so is obviously a very busy person.\n\nSo busy in fact that she’s late to a meeting on the 6th Floor.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[barbra_on_lift]",
        "Thanks to the latest in Low-speed Electron Accretion technology we can measure our employee’s stress levels remotely.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[stressometer]",
        "This is Barbra’s stress-o-meter, all of your passengers will have one.\n\nYou can see their stress, and their destination.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "The longer they have to wait until they get to their destination, the more stressed they’ll get.\n\nI’ve been asked to advice you not to let this get too high.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "We should probably get going. Get Barbra up to the 6th Floor ASAP.\n\n\n\n<b>Press {UP} to go up\nPress {DOWN} to go down\nPress {RIGHT} to open the doors.</b>#",
        "[lift_to_6]",
        "[barbra_exit_lift]",
        "Fantastic! I'll just pop off for my coffee break - I'll be back, but let's see how you do without me.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[boss_leave]",
        "[boss_return]",
        "Great job! Just one more thing before I hand you the reigns...\n\n\n\n<b>Press {SPACE} to continue</b>",
        "We've modded your elevator just a little bit.\n\nTry it out - <b>press {1}</b> while moving to activate turbo boosting!#",
        "[boost_tutorial]",
        "See how much fun that is?!\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[add_freeze]",
        "We've also added the latest cryogenic technology to freeze your passengers' Stress-O-Meters for a few seconds.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Just remember to <b>press {2}</b> if you're in a bit of a pickle if you want to see what it does!\n\nI'll let you try it out while I take another coffee break.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Well, you seem to have got the hang of this quite quickly. I’d say that’s half an hour for lunch.\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Unless you'd like me to go through it again, just to be sure?\n\n\n\n<b>Press {SPACE} to continue</b>",
        "Do you want to repeat the tutorial? \n\n\n\n<b>{UP}: Yes\n{DOWN}: No</b>#",
        "[tutorial_repeat_choice]"
    };

    List<string> failFirstPassengers = new List<string>()
    {
        "Oh no!"
    };

    List<string> failSecondPassengers = new List<string>()
    {

    };

    [SerializeField]
    GameObject tutorialPassengerPrefab;

    [SerializeField]
    private string currentText;
    private bool tutorialStateAcknowledged;

    public Text textbox;

    private AudioSource speaker;
    public AudioClip jimDingSound;
    public AudioClip barbraDingSound;
    public AudioClip exclaimSound;

    [SerializeField]
    private GameObject powerupsUI;

    [SerializeField]
    private GameObject boostSprite;
    [SerializeField]
    private GameObject freezeSprite;

    [SerializeField]
    private UnityEngine.UI.Text twoText;

    private bool onYourOwn;
    private int passengersDelivered;
    private int passengersToDeliver;
    private bool tutorialWantingFreeze;

	// Use this for initialization
	void Start () {
        onYourOwn = false;
        tutorialWantingFreeze = false;
        passengersDelivered = 0;
        speaker = this.GetComponent<AudioSource>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        passengers = new List<Passenger>();
        elevator.enabled = false;
        AdvanceTutorial();

        failFirstPassengers.AddRange(failSecondPassengers);
        failBarbra.AddRange(failFirstPassengers);
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentText)
        {
            case "[jim_walk_on]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    //Do some stuff to make jim move on + AdvanceTutorial()...
                    StartCoroutine(JimAppearCoroutine());

                }
                break;
            case "[jim_bing]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(ThisIsJim());
                }
                break;
            case "[jim_into_lift]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(JimEntersLift());
                }
                break;
            case "[lift_to_3]":
                if (!tutorialStateAcknowledged) {
                    tutorialStateAcknowledged = true;
                    elevator.enabled = true;
                    elevator.StartTutorial();
                }
                if (elevator.GetFloor() == 3 && Mathf.Abs(elevator.Velocity()) < 2)
                {
                    Debug.Log("lock");
                    elevator.Lock();
                    AdvanceTutorial();
                }
                break;
            case "[jim_exclaim]":
                speaker.PlayOneShot(exclaimSound);
                AdvanceTutorial();
                break;
            case "[lift_to_2]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    elevator.Unlock();
                }
                if(elevator.GetFloor() == 2 && Mathf.Abs(elevator.Velocity()) < 1.5f)
                {
                    elevator.Lock();
                    AdvanceTutorial();
                }
                break;
            case "[let_jim_off]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                   
                }
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    AdvanceTutorial();
                }
                break;
            case "[jim_off_lift]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(JimLeavesLift());
                }
                break;
            case "[barbra_walk_on]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(BarbraAppear());
                }
                break;
            case "[barbra_exclaim]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(ThisIsBarbra());
                }
                break;
            case "[barbra_on_lift]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(BarbraEntersLift());
                }
                break;
            case "[stressometer]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(MoveText(new Vector3(271.2f, -200.0f), 0.35f));
                    StartCoroutine(MoveBossAndSpeechBubble(-1.31f, 0.35f, false));
                    barbra.GetComponent<TutorialPassenger>().Card(cardManager.ConstructCard(barbra.GetComponent<TutorialPassenger>()));
                    AdvanceTutorial();
                }
                break;
            case "[lift_to_6]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    barbra.GetComponent<TutorialPassenger>().StartCard();
                    elevator.Unlock();
                    
                } else if (barbra.GetComponent<TutorialPassenger>().GetRage() >= 100.0f)
                {
                    elevator.Lock();
                    tutorialText = new Queue<string>(failBarbra);
                    AdvanceTutorial();
                } else if (elevator.GetFloor() == 6 && elevator.Velocity() < 3.0f && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    AdvanceTutorial();
                }
                break;
            case "[barbra_exit_lift]":
                if (!tutorialStateAcknowledged)
                {
                    StartCoroutine(MoveText(new Vector3(271.2f, -60.0f), 0.8f));
                    StartCoroutine(MoveBossAndSpeechBubble(1.31f, 0.8f, false));
                    tutorialStateAcknowledged = true;
                    StartCoroutine(BarbraLeavesLift());
                }
                break;
            case "[boss_leave]":
                if (!tutorialStateAcknowledged)
                {
                    textbox.text = "";
                    StartCoroutine(MoveBossAndSpeechBubble(-10f, 1.4f, false));
                    tutorialStateAcknowledged = true;
                    passengersToDeliver = 6;
                    onYourOwn = true;
                    elevator.Unlock();
                    RequestPassenger();
                }
                break;
            case "[boss_return]":
                if(!tutorialStateAcknowledged)
                {
                    StartCoroutine(MoveBossAndSpeechBubble(10f, 1.4f, true));
                    tutorialStateAcknowledged = true;
                }
                break;
            case "[boost_tutorial]":
                if(!tutorialStateAcknowledged)
                {
                    StartCoroutine(ShowPowerups());
                    elevator.MakeBoostAvailable();
                    tutorialStateAcknowledged = true;
                } else if (elevator.Boosting() == true)
                {
                    AdvanceTutorial();
                }
                break;
            case "[add_freeze]":
                if (!tutorialStateAcknowledged)
                {
                    StartCoroutine(ShowFreeze());
                    elevator.MakeFreezeAvailable();
                    tutorialStateAcknowledged = true;
                }
                break;
            case "[boss_leave_2]":
                if (!tutorialStateAcknowledged)
                {
                    textbox.text = "";
                    StartCoroutine(MoveBossAndSpeechBubble(-10f, 1.4f, false));
                    tutorialStateAcknowledged = true;
                    passengersToDeliver = 10;
                    onYourOwn = true;
                    tutorialWantingFreeze = true;
                    RequestPassenger();
                }
                break;
            case "[boss_return_2]":
                if (!tutorialStateAcknowledged)
                {
                    StartCoroutine(MoveBossAndSpeechBubble(10f, 1.4f, true));
                    tutorialStateAcknowledged = true;
                }
                break;
            case "[tutorial_repeat_choice]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                } else
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    } else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        SceneManager.LoadScene(1);
                    }
                }
                break;
            case "[fade_out]":

                break;

            case "[barbra_reset]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(BarbraReset());
                }
                break;
            default:
                if (!tutorialStateAcknowledged) {
                    tutorialStateAcknowledged = true;
                    if (!(currentText[0] == '['))
                        if (currentText[currentText.Length - 1] == '#')
                        {
                            currentText = currentText.Remove(currentText.Length - 1);
                            textbox.text = currentText;
                            AdvanceTutorial();
                        } else
                        {
                            textbox.text = currentText;
                        }
                } else if(Input.GetKeyDown(KeyCode.Space))
                {
                    AdvanceTutorial();
                }
                break;
        }

        

    }

    private void AdvanceTutorial()
    {
        currentText = tutorialText.Dequeue();
        tutorialStateAcknowledged = false;
    }

    

    public override void RequestPassenger()
    {
        if(!onYourOwn)
        {
            return;
        } else if (passengersDelivered < passengersToDeliver || tutorialWantingFreeze)
        {
            base.RequestPassenger();
        }
    }

    public override int GetPassengerCount()
    {
        if (!onYourOwn)
        {
            return int.MaxValue; ;
        }
        else
        {
            return base.GetPassengerCount();
        }
        
    }

    public override void BroadcastFloor(int floorNo)
    {
        if(!onYourOwn)
        {
            return;
        } else
        {
            base.BroadcastFloor(floorNo);
        }
    }

    public override void RemovePassenger(Passenger passenger)
    {
        base.RemovePassenger(passenger);

        passengersDelivered++;

        if (passengersDelivered >= passengersToDeliver && base.GetPassengerCount() == 0)
        {
            onYourOwn = false;
            passengersDelivered = 0;
            AdvanceTutorial();
        }
    }

    public override void FreezePassengers()
    {
        tutorialWantingFreeze = false;
        base.FreezePassengers();
    }

    /*These are all tutorial coroutines*/
    GameObject jim;
    GameObject barbra;
    Card barbraCard;

    IEnumerator JimAppearCoroutine()
    {
        jim = Instantiate(tutorialPassengerPrefab, elevator.transform);

        //move Jim from off-screen to middle of corridor
        jim.transform.localPosition = new Vector3(12, -0.25f, -2);
        Vector3 toPosition = new Vector3(6, -0.25f, -2);
        var fromPosition = jim.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            jim.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }

        AdvanceTutorial();

        yield return null;
    }

    IEnumerator ThisIsJim()
    {
        speaker.PlayOneShot(jimDingSound);

        yield return new WaitForSeconds(2.0f);

        AdvanceTutorial();
    }

    IEnumerator JimEntersLift()
    {
        Vector3 toPosition = new Vector3(0.41f, -0.25f, -2);
        var fromPosition = jim.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            jim.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }

        AdvanceTutorial();

        yield return null;
    }

    IEnumerator JimLeavesLift()
    {
        Vector3 toPosition = new Vector3(12f, -0.25f, -2);
        var fromPosition = jim.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1.5f)
        {
            jim.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
        Destroy(jim);
        AdvanceTutorial();

        yield return null;
    }

    IEnumerator BarbraAppear()
    {
        barbra = Instantiate(tutorialPassengerPrefab, elevator.transform);

        //move Jim from off-screen to middle of corridor
        barbra.transform.localPosition = new Vector3(12, -0.25f, -2);
        Vector3 toPosition = new Vector3(6, -0.25f, -2);
        var fromPosition = barbra.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            barbra.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }

        AdvanceTutorial();

        yield return null;
    }

    IEnumerator ThisIsBarbra()
    {
        yield return new WaitForSeconds(0.4f);
        speaker.PlayOneShot(barbraDingSound);
        yield return new WaitForSeconds(1f);
        AdvanceTutorial();
        yield return null;
    }

    IEnumerator BarbraEntersLift()
    {
        Vector3 toPosition = new Vector3(0.41f, -0.25f, -2);
        var fromPosition = barbra.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            barbra.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }

        AdvanceTutorial();

        yield return null;
    }

    IEnumerator MoveText(Vector3 toPosition, float inTime)
    {
        Vector3 fromPosition = textbox.GetComponent<RectTransform>().anchoredPosition;
        for(var t = 0f; t < 1; t+= Time.deltaTime / inTime)
        {
            textbox.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
    }

    IEnumerator MoveBossAndSpeechBubble(float byYAmount, float inTime, bool advanceInThis)
    {
        GameObject boss = GameObject.Find("Boss");
        GameObject speech = GameObject.Find("Speech Bubble");

        Vector3 bossFrom = boss.transform.position;
        Vector3 speechFrom = speech.transform.position;

        Vector3 bossTo = bossFrom + new Vector3(0, byYAmount);
        Vector3 speechTo = speechFrom + new Vector3(0, byYAmount);

        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            boss.transform.position = Vector3.Lerp(bossFrom, bossTo, t);
            speech.transform.position = Vector3.Lerp(speechFrom, speechTo, t);
            yield return null;
        }

        if (advanceInThis) AdvanceTutorial();
    }

    IEnumerator BarbraLeavesLift()
    {
        cardManager.DismissCard(barbra.GetComponent<TutorialPassenger>().Card());
        Vector3 toPosition = new Vector3(12.0f, -0.25f, -2);
        var fromPosition = barbra.transform.localPosition;
        for (var t = 0f; t < 1; t += Time.deltaTime / 1)
        {
            barbra.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            yield return null;
        }
        elevator.Unlock();
        AdvanceTutorial();

        yield return null;
    }

    IEnumerator BarbraReset()
    {
        cardManager.DismissCard(barbra.GetComponent<TutorialPassenger>().Card());
        Destroy(barbra);
        textbox.GetComponent<RectTransform>().anchoredPosition = new Vector3(271.2f, -60.0f);
        elevator.transform.position = new Vector3(-2.5f, -1.25f);
        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
        GameObject.Find("Boss").transform.position = new Vector3(-8.039978f, -1.15f);
        GameObject.Find("Speech Bubble").transform.position = new Vector3(-6.475952f, 2.36f);
        AdvanceTutorial();
        yield return null;
    }

    IEnumerator ShowPowerups()
    {
        Vector3 panelStart = powerupsUI.transform.position;
        Vector3 boostStart = boostSprite.transform.localPosition;

        Vector3 panelTo = panelStart + new Vector3(0, 180.4f);
        Vector3 boostTo = new Vector3(-6.9344f, -4.25f, 1);

        twoText.enabled = false;

        for(var t = 0f; t < 1; t += Time.deltaTime / 0.8f)
        {
            powerupsUI.transform.position = Vector3.Lerp(panelStart, panelTo, t);
            boostSprite.transform.localPosition = Vector3.Lerp(boostStart, boostTo, t);
            yield return null;
        }
    }

    IEnumerator ShowFreeze()
    {
        elevator.Lock();

        Vector3 freezeStart = freezeSprite.transform.localPosition;
        Vector3 freezeTo = new Vector3(-5.0694f, -4.25f, 1);

        for(var t = 0f; t < 1; t += Time.deltaTime / 0.8f)
        {
            freezeSprite.transform.localPosition = Vector3.Lerp(freezeStart, freezeTo, t);
            yield return null;
        }

        twoText.enabled = true;

        Debug.Log("elevator.GetFloor() = " + elevator.GetFloor());
        int nearestFloor = (elevator.GetFloor() == 0 ? NearestFloor() : elevator.GetFloor());
        Debug.Log("nearest floor = " + nearestFloor);
        float yPosition = GetYPositionOfFloor(nearestFloor);

        Debug.Log("Elevator.y = " + elevator.gameObject.transform.position.y + "; yPosition = " + yPosition);

        if(elevator.transform.position.y != yPosition)
        {
            Debug.Log("snapping to " + yPosition);
            Vector3 fromPosition = elevator.gameObject.transform.position;
            Vector3 toPosition = new Vector3(elevator.transform.position.x, yPosition);
            for (var t = 0f; t < 1; t += Time.deltaTime / 0.6f)
            {
                elevator.gameObject.transform.position = Vector3.Lerp(fromPosition, toPosition, t);
                yield return null;
            }
        }

        AdvanceTutorial();
    }

    private int NearestFloor()
    {
        Debug.Log("finding nearest floor");
        int floorNo = 1;
        foreach (GameObject floor in elevator.Floors())
        {
            if (elevator.gameObject.transform.position.y >= floor.transform.position.y - 1.25f &&
                elevator.gameObject.transform.position.y < floor.transform.position.y + 1.25f)
            {
                return floorNo;
            }
            floorNo++;
        }
        return floorNo;
    }

    private float GetYPositionOfFloor(int floorNo)
    {
        int floor = 1;
        foreach(GameObject floorObj in elevator.Floors())
        {
            if (floor == floorNo) return floorObj.transform.position.y;
            floor++;
        }
        return (2.5f * 7) - 6.25f;
    }
}


// It’ll probably be a bit busy when you get back so bring your “A” Game.