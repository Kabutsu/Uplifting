using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPassenger : Passenger {

    private bool cardStarted = false;
    new public const float TIME_TO_LIVE = 15f;
    private float timeAlive;

    private void Update()
    {
        if (cardStarted)
        {
            if (timeAlive >= 0.0f)
            {
                if (timeAlive < TIME_TO_LIVE)
                {
                    rage = Mathf.Lerp(0.0f, 100.0f, timeAlive / TIME_TO_LIVE);
                    timeAlive += Time.deltaTime;
                }
                else
                {
                    rage = 100.0f;
                    cardStarted = false;
                }
            }
        }
    }

    private void Start()
    {
        timeAlive = -1;
    }

    public override string GetPassengerName()
    {
        return "Barbra Stitt";
    }

    public override string GetPassengerJob()
    {
        return "Central Optimisation Associate";
    }

    public override int GetPassengerReqFloor()
    {
        return 6;
    }

    public override float GetRage()
    {
        return rage;
    }

    public void setRage()
    {
        return;
    }

    public void Card(Card card)
    {
        this.card = card;
    }

    public void StartCard()
    {
        cardStarted = true;
        timeAlive = 0.0f;
    }

    public void StopCard()
    {
        cardStarted = false;

    }
}