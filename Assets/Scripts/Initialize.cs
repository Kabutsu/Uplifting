using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

    [SerializeField]
    private float bottomOfScreen;
    [SerializeField]
    private int noOfFloors;

	// Use this for initialization
	void Start () {

		for(int i = 0; i < noOfFloors; i++)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float BottomOfScreen()
    {
        return bottomOfScreen;
    }

    public int NoOfFloors()
    {
        return noOfFloors;
    }
}
