using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	private List<Card> currentCards;
	public GameObject cardPrefab;
	public Canvas UICanvas;

	// Use this for initialization
	void Start () {
		currentCards = new List<Card> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void DismissCard (Card cardToDismiss){
		int indexBeingDismissed = cardToDismiss.GetIndex ();


		currentCards.Remove (cardToDismiss);
		cardToDismiss.Dismiss ();
		foreach (Card c in currentCards.FindAll((c) => {return c.GetIndex() > indexBeingDismissed;})) {
			c.ShiftUp ();
		}

	}

	public Card ConstructCard(Passenger passenger){
		GameObject go = GameObject.Instantiate (cardPrefab, UICanvas.transform);
		Card c = go.GetComponent<Card> ();
		c.Initialize (currentCards.Count, passenger);
		currentCards.Add (c);
		return c;
	}
}
