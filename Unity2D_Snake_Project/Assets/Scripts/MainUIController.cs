using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {
	private static MainUIController _instance;

	public static MainUIController Instance {
        get {
			return _instance;
        }
    }

	public int score = 0;
	public int length = 0;

	public Text msgText;
	public Text scoreText;
	public Text lengthText;
	public Image bgImage;
	private Color tempColor;

	private void Awake() {
		_instance = this;
    }

	public void UpdateUI(int s = 5, int l = 1) {
		score += s;
		length += l;

		scoreText.text = "得分:\n" + score;
		lengthText.text = "长度:\n" + length;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(score / 100) {
			case 3:
				ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
				bgImage.color = tempColor;
				msgText.text = "阶段" + 2;
				break;
			case 5:
				ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
				bgImage.color = tempColor;
				msgText.text = "阶段" + 3;
				break;
			case 7:
				ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
				bgImage.color = tempColor;
				msgText.text = "阶段" + 4;
				break;
			case 9:
				ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
				bgImage.color = tempColor;
				msgText.text = "阶段" + 5;
				break;
			case 11:
				ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
				bgImage.color = tempColor;
				msgText.text = "无尽";
				break;

		}
	}
}
