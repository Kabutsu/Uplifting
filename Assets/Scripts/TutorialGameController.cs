using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialGameController : GameController {

    Queue<string> tutorialText = new Queue<string>(new List<string>()
        {
        "Good morning and welcome to your new role as Green Meadows Waste Management Solutions Inc©’s new Elevation Consultant!@",
        "It's your job to make sure our employees don't get so stressed they revolt!\n\nI’ll be walking you through your first morning.@",
        "[jim_walk_on]",
        "This is Jim.#",
        "[jim_bing]",
        "Jim works in accounting, which is on the 2rd Floor.\n\nLet's get him up there.@",
        "[jim_into_lift]",
        "[lift_to_2]",
        "We should probably let him out. Don't want him waiting here forever!#",
        "[let_jim_off]",
        "[jim_off_lift]",
        "[barbra_walk_on]",
        "This is Barbra.#",
        "[barbra_exclaim]",
        "[barbra_on_lift]",
        "Barbra is just a *little* bit stressed right now.@",
        "[stressometer]",
        "This is Barbra’s Stress-O-Meter.\n\nIt show's her stress and her destination.\n\nDon't let her stress get too high or bad things will happen!@",
        "We should get up to the <b>6th Floor</b> ASAP.#",
        "[lift_to_6]",
        "[barbra_exit_lift]",
        "Fantastic! I'll just pop off for my coffee break - I'll be back, but let's see how you do without me.@",
        "[boss_leave]",
        "[boss_return]",
        "Great job! Just one more thing before I hand you the reigns...@",
        "%We've modded your elevator just a little bit.\n\nFirst of all, turbo injection boosting! Try it out!\n\n\n\n<b>Press     while moving to BOOST</b>#",
        "[boost_tutorial]",
        "See how much fun that is?!@",
        "[add_freeze]",
        "We've also added the latest cryogenic technology to freeze your passengers' Stress-O-Meters for a few seconds.@",
        "$I'll let you try it out while I take another coffee break.\n\n\n\n<b>Press     while you have passengers to FREEZE</b>@",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Time for you to fly solo.\n\nRemeber each day starts at 9am and ends at 5pm - make sure you let all passengers off at the end of the day or they'll get mad!@",
        "Unless you'd like me to go through it all again, just to be sure?@",
        "^Do you want to repeat the tutorial? \n\n\n<b>   : Yes\n\n   : No</b>#",
        "[tutorial_repeat_choice]"
        }
    );

    List<string>failBarbra = new List<string>()
    {
        "Oh no! Barbra's rage meter filled! Maybe we should go back in time to before we met her?@",
        "[barbra_reset]",
        "[barbra_walk_on]",
        "This is Barbra.#",
        "[barbra_exclaim]",
        "[barbra_on_lift]",
        "Barbra is just a *little* bit stressed right now.@",
        "[stressometer]",
        "This is Barbra’s Stress-O-Meter.\n\nIt show's her stress and her destination.\n\nDon't let her stress get too high or bad things will happen!@",
        "We should get up to the <b>6th Floor</b> ASAP.#",
        "[lift_to_6]",
        "[barbra_exit_lift]",
        "Fantastic! I'll just pop off for my coffee break - I'll be back, but let's see how you do without me.@",
        "[boss_leave]",
        "[boss_return]",
        "Great job! Just one more thing before I hand you the reigns...@",
        "%We've modded your elevator just a little bit.\n\nFirst of all, turbo injection boosting! Try it out!\n\n\n\n<b>Press     while moving to BOOST</b>#",
        "[boost_tutorial]",
        "See how much fun that is?!@",
        "[add_freeze]",
        "We've also added the latest cryogenic technology to freeze your passengers' Stress-O-Meters for a few seconds.@",
        "$I'll let you try it out while I take another coffee break.\n\n\n\n<b>• Press     while you have passengers to FREEZE</b>@",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Time for you to fly solo.\n\nRemeber each day starts at 9am and ends at 5pm - make sure you let all passengers off at the end of the day or they'll get mad!@",
        "Unless you'd like me to go through it all again, just to be sure?@",
        "^Do you want to repeat the tutorial? \n\n\n<b>   : Yes\n\n   : No</b>#",
        "[tutorial_repeat_choice]"
    };

    List<string> failFirstPassengers = new List<string>()
    {
        "[boss_return_2]",
        "Oh no! You let them get too angry!\n\nNot to worry.\n\nWe'll just travel back to before I left so you can have another go.@",
        "[passenger_1_reset]",
        "So I'm just off for my coffee break - let's see if you can keep these employees from rioting in the lift!@",
        "[boss_leave]",
        "[boss_return]",
        "Great job! Just one more thing before I hand you the reigns...@",
        "%We've modded your elevator just a little bit.\n\nFirst of all, turbo injection boosting! Try it out!\n\n\n\n<b>Press     while moving to BOOST</b>#",
        "[boost_tutorial]",
        "See how much fun that is?!@",
        "[add_freeze]",
        "We've also added the latest cryogenic technology to freeze your passengers' Stress-O-Meters for a few seconds.@",
        "$I'll let you try it out while I take another coffee break.\n\n\n\n<b>• Press     while you have passengers to FREEZE</b>@",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Time for you to fly solo.\n\nRemeber each day starts at 9am and ends at 5pm - make sure you let all passengers off at the end of the day or they'll get mad!@",
        "Unless you'd like me to go through it all again, just to be sure?@",
        "^Do you want to repeat the tutorial? \n\n\n<b>   : Yes\n\n   : No</b>#",
        "[tutorial_repeat_choice]"
    };

    List<string> failSecondPassengers = new List<string>()
    {
        "[boss_return_2]",
        "*Not to worry - let's have another go!\n\nRemember, if you're in a pickle:\n\n<b>• Press     for BOOST</b>\n\n<b>• Press     for FREEZE</b>\n\n\nGood luck!@",
        "[passenger_2_reset]",
        "[boss_leave_2]",
        "[boss_return_2]",
        "Time for you to fly solo.\n\nRemeber each day starts at 9am and ends at 5pm - make sure you let all passengers off at the end of the day or they'll get mad!@",
        "Unless you'd like me to go through it all again, just to be sure?@",
        "^Do you want to repeat the tutorial? \n\n\n<b>   : Yes\n\n   : No</b>#",
        "[tutorial_repeat_choice]"
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

    private SpriteRenderer key_space;
    private SpriteRenderer key_up;
    private SpriteRenderer key_right;
    private SpriteRenderer key_down;
    private SpriteRenderer key_1;
    private SpriteRenderer key_2;
    private SpriteRenderer key_up_option;
    private SpriteRenderer key_down_option;

    private GameObject bossUI;

    private bool onYourOwn;
    private int passengersDelivered;
    private int passengersToDeliver;

    private bool gameOver;

	// Use this for initialization
	void Start () {
        onYourOwn = false;
        gameOver = false;
        passengersDelivered = 0;
        speaker = this.GetComponent<AudioSource>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        bossUI = GameObject.Find("BossUI");
        passengers = new List<Passenger>();
        elevator.enabled = false;
        AdvanceTutorial();

        key_space = GameObject.Find("Key_Space").GetComponent<SpriteRenderer>();
        key_up = GameObject.Find("Key_Up").GetComponent<SpriteRenderer>();
        key_right = GameObject.Find("Key_Right").GetComponent<SpriteRenderer>();
        key_down = GameObject.Find("Key_Down").GetComponent<SpriteRenderer>();
        key_1 = GameObject.Find("Key_1").GetComponent<SpriteRenderer>();
        key_2 = GameObject.Find("Key_2").GetComponent<SpriteRenderer>();
        key_up_option = GameObject.Find("Key_Up_Option").GetComponent<SpriteRenderer>();
        key_down_option = GameObject.Find("Key_Down_Option").GetComponent<SpriteRenderer>();

        key_space.enabled = false;
        key_up.enabled = false;
        key_right.enabled = false;
        key_down.enabled = false;
        key_1.enabled = false;
        key_2.enabled = false;
        key_up_option.enabled = false;
        key_down_option.enabled = false;

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
                    
                }
                if (elevator.GetFloor() == 3 && Mathf.Abs(elevator.Velocity()) < 2)
                {
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
                    elevator.enabled = true;
                    elevator.StartTutorial();
                    key_up.enabled = true;
                    key_space.enabled = false;
                    elevator.Unlock();
                }
                if (elevator.GetFloor() == 2)
                {
                    key_up.enabled = false;
                    key_down.enabled = false;
                    if (Mathf.Abs(elevator.Velocity()) < 1.5f)
                    {
                        elevator.Lock();
                        AdvanceTutorial();
                    }
                } else if (NearestFloor() > 2)
                {
                    key_down.enabled = true;
                    key_up.enabled = false;
                } else if (NearestFloor() < 2)
                {
                    key_up.enabled = true;
                    key_down.enabled = false;
                } 
                break;
            case "[let_jim_off]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    key_right.enabled = true;
                }
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    key_right.enabled = false;
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
                    key_space.enabled = false;
                    barbra.GetComponent<TutorialPassenger>().StartCard();
                    elevator.Unlock();
                    
                } else if (barbra.GetComponent<TutorialPassenger>().GetRage() >= 100.0f)
                {
                    elevator.Lock();
                    key_right.enabled = false;
                    key_down.enabled = false;
                    key_up.enabled = false;
                    tutorialText = new Queue<string>(failBarbra);
                    AdvanceTutorial();
                } else if (elevator.GetFloor() == 6)
                {
                    key_up.enabled = false;
                    key_down.enabled = false;

                    key_right.enabled = true;
                    if (elevator.Velocity() < 3.0f && Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        elevator.Lock();
                        AdvanceTutorial();
                    }
                    
                } else if (NearestFloor() > 6)
                {
                    key_down.enabled = true;
                    key_up.enabled = false;

                    key_right.enabled = false;
                }
                else if (NearestFloor() < 6)
                {
                    key_up.enabled = true;
                    key_down.enabled = false;

                    key_right.enabled = false;
                }


                break;
            case "[barbra_exit_lift]":
                if (!tutorialStateAcknowledged)
                {
                    key_right.enabled = false;
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
                    StartCoroutine(MoveBossAndSpeechBubble(-10f, 1.2f, false));
                    tutorialStateAcknowledged = true;
                    passengersToDeliver = 4;
                    elevator.SetMaxPassengers(4);
                    onYourOwn = true;
                    elevator.Unlock();
                    gameOver = false;
                    RequestPassenger();
                }
                break;
            case "[boss_return]":
                if(!tutorialStateAcknowledged)
                {
                    StartCoroutine(MoveBossAndSpeechBubble(10f, 1.2f, true));
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
                    tutorialStateAcknowledged = true;
                }
                break;
            case "[boss_leave_2]":
                if (!tutorialStateAcknowledged)
                {
                    textbox.text = "";
                    key_2.enabled = false;
                    StartCoroutine(MoveBossAndSpeechBubble(-10f, 1.2f, false));
                    tutorialStateAcknowledged = true;
                    passengersToDeliver = 6;
                    elevator.MakeFreezeAvailable();
                    onYourOwn = true;
                    elevator.Unlock();
                    gameOver = false;
                    RequestPassenger();
                }
                break;
            case "[boss_return_2]":
                if (!tutorialStateAcknowledged)
                {
                    StartCoroutine(MoveBossAndSpeechBubble(10f, 1.2f, true));
                    tutorialStateAcknowledged = true;
                }
                break;

            case "[passenger_1_reset]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(PassengerReset());
                }
                break;
            case "[passenger_2_reset]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(PassengerReset());
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
                        LevelController.GoToMainMenu();
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
                    {
                        if (currentText[0] == '%')
                        {
                            key_1.enabled = true;
                            key_2.enabled = false;
                            currentText = currentText.Substring(1);

                            if (bossUI.transform.localPosition.y < 1)
                                bossUI.transform.localPosition = Vector3.zero;
                        }
                        else if (currentText[0] == '$')
                        {
                            key_2.enabled = true;
                            key_1.enabled = false;
                            currentText = currentText.Substring(1);

                            if (bossUI.transform.localPosition.y < 1)
                                bossUI.transform.localPosition = Vector3.zero;
                        }
                        else if (currentText[0] == '*')
                        {
                            currentText = currentText.Substring(1);

                            if (bossUI.transform.localPosition.y < 1)
                                bossUI.transform.localPosition = Vector3.zero;

                            key_1.gameObject.transform.localPosition = new Vector3(-7.4f, 3.25f, 9);
                            key_2.gameObject.transform.localPosition = new Vector3(-7.4f, 2.75f, 9);

                            key_1.enabled = true;
                            key_2.enabled = true;

                        } else
                        {
                            key_1.enabled = false;
                            key_2.enabled = false;
                            if(currentText[0] == '^')
                            {
                                key_up_option.enabled = true;
                                key_down_option.enabled = true;
                                currentText = currentText.Substring(1);

                                if (bossUI.transform.localPosition.y < 1)
                                    bossUI.transform.localPosition = Vector3.zero;
                            }
                        }

                        if (currentText[currentText.Length - 1] == '#')
                        {
                            currentText = currentText.Remove(currentText.Length - 1);
                            textbox.text = currentText;
                            key_space.enabled = false;
                            AdvanceTutorial();
                        }
                        else
                        {
                            if (currentText[currentText.Length - 1] == '@')
                            {
                                key_space.enabled = true;
                                currentText = currentText.Remove(currentText.Length - 1);
                            }
                            else key_space.enabled = false;

                            textbox.text = currentText;
                        }

                        
                    }
                    else key_space.enabled = false;
                    
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
        if (!onYourOwn)
        {
            return;
        }
        else if (passengersDelivered < passengersToDeliver)
        {
            int toSpawn = 0;
            int rand = Random.Range(0, 10);
            if (passengers.Count < 2)
            {
                if (rand < 4)
                {
                    toSpawn = 2;
                }
                else
                {
                    toSpawn = 1;
                }
            }
            else if (passengers.Count < 4)
            {
                if (rand < 2)
                {
                    toSpawn = 2;
                }
                else
                {
                    toSpawn = 1;
                }
            }

            for (int i = 0; i < toSpawn; i++)
            {
                GameObject passenger = Instantiate(passengerPrefab, elevator.transform);

                passenger.transform.localPosition = new Vector3(6, -0.25f, -1);

                if (i == toSpawn - 1)
                {
                    passenger.GetComponent<Passenger>().AssignAsKeyHolder();
                }

                passengers.Add(passenger.GetComponent<Passenger>());
            }
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

    public override void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            elevator.Lock();

            if (passengersToDeliver == 4)
            {
                tutorialText = new Queue<string>(failFirstPassengers);
            }
            else if (passengersToDeliver == 6)
            {
                tutorialText = new Queue<string>(failSecondPassengers);
            }

            foreach (Passenger pass in passengers) cardManager.DismissCard(pass.Card());

            AdvanceTutorial();
        }
    }

    public override void FreezePassengers()
    {
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

        textbox.text = "Jim works in accounting, which is on the 2rd Floor.\n\nLet's get him up there.";

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
        key_space.enabled = false;
        key_1.enabled = false;
        key_2.enabled = false;
        
        Vector3 bossFrom = bossUI.transform.localPosition;
        Vector3 bossTo = bossFrom + new Vector3(0, byYAmount);

        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            bossUI.transform.localPosition = Vector3.Lerp(bossFrom, bossTo, t);
            yield return null;
        }

        key_space.enabled = true;

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
        Destroy(barbra);
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
        GameObject.Find("BossUI").transform.localPosition = new Vector3(0, 0, 0);
        AdvanceTutorial();
        yield return null;
    }

    IEnumerator ShowPowerups()
    {
        Vector3 panelStart = powerupsUI.transform.position;
        Vector3 boostStart = boostSprite.transform.localPosition;

        Vector3 panelTo = panelStart + new Vector3(0, 100);
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
        
        int nearestFloor = (elevator.GetFloor() == 0 ? NearestFloor() : elevator.GetFloor());
        float yPosition = GetYPositionOfFloor(nearestFloor);
        

        if(elevator.transform.position.y != yPosition)
        {
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
    
    IEnumerator PassengerReset()
    {
        passengersDelivered = 0;
        foreach (Passenger pass in passengers.ToArray())
        {
            passengers.Remove(pass);
            Destroy(pass.gameObject);
        }

        textbox.GetComponent<RectTransform>().anchoredPosition = new Vector3(271.2f, -60.0f);
        elevator.transform.position = new Vector3(-2.5f, -1.25f);
        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);

        AdvanceTutorial();
        yield return null;
    }
}


// It’ll probably be a bit busy when you get back so bring your “A” Game.