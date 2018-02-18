using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        "Let's get a move on. Head on up to level 3. [Press {UP} to go up, and {DOWN} to go down].",
        "[lift_to_3]",
        "Jim has just realised he needs the toilet. Quite urgently.",
        "[jim_exclaim]",
        "The nearest toilets are on the 2nd Floor. Let’s try and get him there as quick as we can.",
        "[lift_to_2]",
        "We should probably let him out. Sooner rather than later. [Press {RIGHT} to open the doors]",
        "[jim_off_lift]",
        "This is Barbra.",
        "[barbra_exclaim]",
        "Barbra is a Central Optimisation Associate, and so is obviously a very busy person. So busy in fact that she’s late to a meeting that hasn’t started yet on the 6th Floor.",
        "[barbra_on_lift]",
        "Thanks to the latest in Low-speed Electron Accretion technology we can measure our employee’s stress levels remotely.",
        "This is Barbra’s stress-o-meter, all of your passengers will have one. The longer they have to wait until they get to their destination, the more stressed they’ll get. I’ve been asked to advice you not to let this get too high.",
        "We should probably get going. Get Barbra up to the 6th Floor ASAP.",
        "[lift_to_6]",
        "Well, you seem to have got the hang of this quite quickly. I’d say that’s half an hour for lunch. It’ll probably be a bit busy when you get back so bring your “A” Game.",
        "[fade_out]"
        }
    );

    private string currentText;
    private bool tutorialStateAcknowledged;
    public bool readyForNextStage;
    
	// Use this for initialization
	void Start () {
        elevator.enabled = false;
        currentText = tutorialText.Dequeue();
        Debug.Log(currentText);
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentText)
        {
            case "[jim_walk_on]":
                if (!tutorialStateAcknowledged)
                {
                    tutorialStateAcknowledged = true;
                    readyForNextStage = false;
                    //Do some stuff to make jim move on + AdvanceTutorial()...
                    StartCoroutine(JimCoroutine());

                }
                else if (readyForNextStage) AdvanceTutorial();
                break;
            case "[jim_bing]":

                break;
            case "[jim_into_lift]":

                break;
            case "[lift_to_3]":

                break;
            case "[jim_exclaim]":

                break;
            case "[lift_to_2]":

                break;
            case "[jim_off_lift]":

                break;
            case "[barbra_exclaim]":

                break;
            case "[barbra_on_lift]":

                break;
            case "[lift_to_6]":

                break;
            case "[fade_out]":

                break;
            default:
                if (!tutorialStateAcknowledged) {
                    tutorialStateAcknowledged = true;
                    if (!(currentText[0] == '['))
                        Debug.Log(currentText);
                }
                
                if(Input.GetKeyDown(KeyCode.Space))
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

    }

    /*These are all tutorial coroutines*/
    GameObject jim;

    IEnumerator JimCoroutine()
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

        readyForNextStage = true;

        yield return null;
    }
}
