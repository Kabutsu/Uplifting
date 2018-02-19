using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialPassenger : Passenger {

}

public class BarbraTutorialPassenger : TutorialPassenger
{

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
        return base.GetRage();
    }

    public void setRage()
    {

    }
}
