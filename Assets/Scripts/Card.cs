using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	//Animation vars
	const float CARD_SLIDE_IN_TIME_SECONDS = 0.72f;
	const float CARD_SEPERATION = -150.0f;
	const float CARD_TOP_OFFSET = -75.0f;
	const float CARD_LEFT_POSITION = 230.5f;


	//Position vars
	private RectTransform rectTrans;
	[SerializeField]
	private int index;


	//Child References
	[SerializeField]
	private Slider stressSlider;
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text titleText;


	// Use this for initialization
	void Start () {
		stressSlider = transform.Find ("Slider").GetComponent<Slider> ();
		nameText = transform.Find ("Name").GetComponent<Text> ();
		titleText = transform.Find ("Job").GetComponent<Text> ();

		stressSlider.value = 0;
		nameText.text = "";
		titleText.text = "";
	}

	public void Initialize(int initialIndex, string name, string job, int floor){
		rectTrans = this.GetComponent<RectTransform> ();
		index = initialIndex;
		StartCoroutine (Enter(index));
		nameText.text = name;
		titleText.text = job;
	}

	public void Dismiss() {
		Destroy (this.gameObject);
	}

	public int GetIndex(){
		return index;
	}

	public void ShiftUp(){
		Debug.Log ("Shifting");
		if (index != 0) {
			index--;
			Debug.Log ("new index:" + index);
			StartCoroutine(ReevaluatePosition ());
		} else {
			Debug.LogError ("Trying to shift while index is 0");
		}
	}

	IEnumerator ReevaluatePosition () {
		rectTrans.anchoredPosition = new Vector2 (CARD_LEFT_POSITION, CARD_TOP_OFFSET + (CARD_SEPERATION * (float)index));
		yield return null;
	}


	IEnumerator Enter (int index) {

		Vector2 startPoint = new Vector2 (-222.2f, CARD_TOP_OFFSET + (CARD_SEPERATION * (float)index));
		Vector2 endPoint = new Vector2 (CARD_LEFT_POSITION, CARD_TOP_OFFSET + (CARD_SEPERATION * (float)index));
		rectTrans.anchoredPosition = startPoint;

		float movementTimeElapsed = 0.0f;

		while (movementTimeElapsed < CARD_SLIDE_IN_TIME_SECONDS) {
			rectTrans.anchoredPosition = Vector2.Lerp (startPoint, endPoint, (movementTimeElapsed / CARD_SLIDE_IN_TIME_SECONDS));
			movementTimeElapsed += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}

		rectTrans.anchoredPosition = endPoint;

	}
}
