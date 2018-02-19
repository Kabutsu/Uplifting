using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameController : GameController {

    Queue<string> tutorialText = new Queue<string>(new List<string>()
        {
        "Good morning and welcome to your new role as Green Meadows Waste Management Solutions Inc©’s new Elevation Consultant!",
        "Your job will mean ensuring that employees can get to where they need to go in a safe and timely manner. I’ll be walking you through your first morning.",
        "Fortunately, last night was our Annual Mid-February Office Christmas party, so we’re expecting a slow morning.",
        "[jim_walk_on]",
        "This is Jim.",
        "[jim_bing]",
        "Jim works in accounting, which is on the 3rd Floor. Thankfully, he’s not in too much of a rush to get to his desk today, so I’ll get a chance to show you the ropes.",
        "[jim_into_lift]",
        "Let's get a move on. Head on up to level 3. [Press {UP} to go up, and {DOWN} to go down].#",
        "[lift_to_3]",
        "Jim has just realised he needs the toilet. Quite urgently.",
        "[jim_exclaim]",
        "The nearest toilets are on the 2nd Floor. Let’s try and get him there as quick as we can.",
        "[lift_to_2]",
        "We should probably let him out. Sooner rather than later. [Press {RIGHT} to open the doors]#",
        "[let_jim_off]",
        "[jim_off_lift]",
        "[barbra_walk_on]",
        "This is Barbra.",
        "[barbra_exclaim]",
        "Barbra is a Central Optimisation Associate, and so is obviously a very busy person. So busy in fact that she’s late to a meeting that hasn’t started yet on the 6th Floor.",
        "[barbra_on_lift]",
        "Thanks to the latest in Low-speed Electron Accretion technology we can measure our employee’s stress levels remotely.",
        "[stressometer]",
        "This is Barbra’s stress-o-meter, all of your passengers will have one. You can see their stress, and their destination. The longer they have to wait until they get to their destination, the more stressed they’ll get. I’ve been asked to advice you not to let this get too high.",
        "We should probably get going. Get Barbra up to the 6th Floor ASAP.",
        "[lift_to_6]",
        "[barbra_exit_lift]",
        "[evaluate]",
        "[tutorial_repeat_choice]",
        "[fade_out]"
        }
    );

    [SerializeField]
    private string currentText;
    private bool tutorialStateAcknowledged;

    private AudioSource speaker;
    public AudioClip jimDingSound;
    public AudioClip barbraDingSound;
    public AudioClip exclaimSound;
    
	// Use this for initialization
	void Start () {
        speaker = this.GetComponent<AudioSource>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        elevator.enabled = false;
        AdvanceTutorial();
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
                }
                if (elevator.GetFloor() == 3 && Mathf.Abs(elevator.Velocity()) < 3.0f)
                {
                    elevator.Lock();
                    AdvanceTutorial();
                }
                break;
            case "[jim_exclaim]":
                speaker.PlayOneShot(exclaimSound);
                Debug.Log("!");
                AdvanceTutorial();
                break;
            case "[lift_to_2]":
                if(!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    elevator.Unlock();
                }
                if(elevator.GetFloor() == 2 && Mathf.Abs(elevator.Velocity()) < 3)
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
                } else if (elevator.GetFloor() == 6 && elevator.Velocity() < 3.0f && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    AdvanceTutorial();
                }
                break;
            case "[barbra_exit_lift]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(BarbraLeavesLift());
                }
                break;
            case "[evaluate]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    StartCoroutine(ClosingStatements());
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
            default:
                if (!tutorialStateAcknowledged) {
                    tutorialStateAcknowledged = true;
                    if (!(currentText[0] == '['))
                        if (currentText[currentText.Length - 1] == '#')
                        {
                            currentText = currentText.Remove(currentText.Length - 1);
                            Debug.Log(currentText);
                            AdvanceTutorial();
                        } else
                        {
                            Debug.Log(currentText);
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
        return;
    }

    public override int GetPassengerCount()
    {
        return int.MaxValue;
    }

    public override void BroadcastFloor(int floorNo)
    {
        return;
    }

    /*These are all tutorial coroutines*/
    GameObject jim;
    GameObject barbra;
    Card barbraCard;

    IEnumerator JimAppearCoroutine()
    {
        jim = Instantiate(passengerPrefab, elevator.transform);

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
        Debug.Log("\"ding\"");

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
        barbra = Instantiate(passengerPrefab, elevator.transform);

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

        AdvanceTutorial();

        yield return null;
    }

    IEnumerator ClosingStatements()
    {
        if (barbra.GetComponent<TutorialPassenger>().GetRage() == 100.0f)
        {
            Debug.Log("A good attempt although you might need a bit more practise.");
            Debug.Log("We can give it another go if you want or I can leave you to it.");
        } else
        {
            Debug.Log("Well, you seem to have got the hang of this quite quickly. I’d say that’s half an hour for lunch.");
            Debug.Log("Unless you'd like me to go through it again, just to be sure?");
        }

        Debug.Log("You want to repeat the tutorial? {UP: Yes, DOWN: No}");
        AdvanceTutorial();
        yield return null;
    }
}


// It’ll probably be a bit busy when you get back so bring your “A” Game.