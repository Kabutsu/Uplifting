using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

    [SerializeField]
    private GameObject gameOverCanvasContents;

    [SerializeField]
    private Text resultText;

	// Use this for initialization
	void Start () {
        gameOverCanvasContents.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowScreen(int daysSurvived, float timeSurvivedCurrDay)
    {

        gameOverCanvasContents.SetActive(true);

        string resultString = "";
        //Days (Levels)
        resultString += daysSurvived + " days,\n";

        //Hours (minutes)
        timeSurvivedCurrDay = Mathf.Floor(timeSurvivedCurrDay);
        int timeSurvivedMinutes = (int) timeSurvivedCurrDay / 60;
        resultString += timeSurvivedMinutes + " hours and ";

        //Minutes (seconds)
        int timeSurvivedSeconds = (int)timeSurvivedCurrDay % 60;
        resultString += timeSurvivedSeconds + " minutes";

        resultText.text = resultString;

    }
}
