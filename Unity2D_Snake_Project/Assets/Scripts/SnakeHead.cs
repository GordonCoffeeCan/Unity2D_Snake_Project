using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

	public List<Transform> bodyList = new List<Transform>();
	public float velocity = 0.35f;
	public int step;
	

	private int x;
	private int y;

	private Vector3 headPos;
	private Transform canvas;

	public GameObject bodyPrefab;

	public Sprite[] bodySprites = new Sprite[2];

	private void Awake() {
		canvas = GameObject.Find("Canvas").transform;
    }

	private void Start() {
		x = 0; y = step;
		InvokeRepeating("Move", 0, velocity);
    }

	private void Update() {

		if (Input.GetKeyDown(KeyCode.Space)) {
			CancelInvoke();
			InvokeRepeating("Move", 0, velocity - 0.2f);
		} 
		
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke();
			InvokeRepeating("Move", 0, velocity);
		}

		if (Input.GetKey(KeyCode.W) && y != -step) {
			this.transform.localRotation = Quaternion.Euler(Vector3.zero);
			x = 0; y = step;
		}
		if (Input.GetKey(KeyCode.S) && y != step) {
			this.transform.localRotation = Quaternion.Euler(0, 0, 180);
			x = 0; y = -step;
		}
		if (Input.GetKey(KeyCode.A) && x != step) {
			this.transform.localRotation = Quaternion.Euler(0, 0, 90);
			x = -step; y = 0;
		}
		if (Input.GetKey(KeyCode.D) && x != -step) {
			this.transform.localRotation = Quaternion.Euler(0, 0, -90);
			x = step; y = 0;
		}
	}

	private void Move() {
		headPos = this.transform.localPosition; //Save the current head postion
		this.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y, headPos.z); //Move the head position to the expected new position;
		if(bodyList.Count > 0) {
            /*Last on move to the 1st method;
            bodyList.Last().localPosition = headPos;
            bodyList.Insert(0, bodyList.Last());
            bodyList.RemoveAt(bodyList.Count - 1);*/

            //Move 1 forward together method; count from the last one in the bodyList;
            for (int i = bodyList.Count - 2; i >= 0; i--) { //Moving from the last one in the bodyList.
				bodyList[i + 1].localPosition = bodyList[i].localPosition; //Every body part moves to the postion of the one before it.
            }
			bodyList[0].localPosition = headPos; //Move the 1st body part to the last head position;
		}
    }

	private void Grow() {
		int index = (bodyList.Count % 2 == 0) ? 0 : 1;
		GameObject body = Instantiate(bodyPrefab, new Vector3(2000, 2000, 0), Quaternion.identity);
		body.GetComponent<Image>().sprite = bodySprites[index];
		body.transform.SetParent(canvas, false);
		bodyList.Add(body.transform);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Food")) {
			Destroy(collision.gameObject);
			FoodMaker.Instance.MakeFood((Random.Range(0, 100) < 20) ? true : false);
			MainUIController.Instance.UpdateUI();
			Grow();
		} else if (collision.tag == "Reward") {
			Destroy(collision.gameObject);
			MainUIController.Instance.UpdateUI(Random.Range(5 , 16) * 10);
			Grow();
		} else if (collision.tag == "Body") {
			Debug.Log("Die");
		} else{
            switch (collision.name) {
				case "Up":
					transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
					break;
				case "Down":
					transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
					break;
				case "Left":
					transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
					break;
				case "Right":
					transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z);
					break;
			}
		}
    }
}
