using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using input.core;
public class inputManager : MonoBehaviour {

	private InputField input;
	private engine en;
	private string[] result;

	private int page = 0;
	private int current = 0;
	private int surplus = 0;


	private Button DS1;
	private Button DS2;
	private Button DS3;
	private Button DS4;
	private Button DS5;
	private Button DS6;
	private Button DS7;
	private Button DS8;
	private Button pre;
	private Button nex;

	private Button clr;

	private Text[] displayWord = new Text[8];

	private Text disp;

	void getDisplayObject()
	{
		input = GameObject.FindGameObjectWithTag ("CHInput").GetComponent<InputField> ();

		DS1 = GameObject.FindGameObjectWithTag ("DS1").GetComponent<Button> ();		
		DS2 = GameObject.FindGameObjectWithTag ("DS2").GetComponent<Button> ();
		DS3 = GameObject.FindGameObjectWithTag ("DS3").GetComponent<Button> ();
		DS4 = GameObject.FindGameObjectWithTag ("DS4").GetComponent<Button> ();
		DS5 = GameObject.FindGameObjectWithTag ("DS5").GetComponent<Button> ();
		DS6 = GameObject.FindGameObjectWithTag ("DS6").GetComponent<Button> ();
		DS7 = GameObject.FindGameObjectWithTag ("DS7").GetComponent<Button> ();
		DS8 = GameObject.FindGameObjectWithTag ("DS8").GetComponent<Button> ();

		pre = GameObject.FindGameObjectWithTag ("pre").GetComponent<Button> ();
		nex = GameObject.FindGameObjectWithTag ("nex").GetComponent<Button> ();

		nex = GameObject.FindGameObjectWithTag ("nex").GetComponent<Button> ();

		displayWord[0] = DS1.transform.Find ("Text").GetComponent<Text> ();
		displayWord[1] = DS2.transform.Find ("Text").GetComponent<Text> ();
		displayWord[2] = DS3.transform.Find ("Text").GetComponent<Text> ();
		displayWord[3] = DS4.transform.Find ("Text").GetComponent<Text> ();
		displayWord[4] = DS5.transform.Find ("Text").GetComponent<Text> ();
		displayWord[5] = DS6.transform.Find ("Text").GetComponent<Text> ();
		displayWord[6] = DS7.transform.Find ("Text").GetComponent<Text> ();
		displayWord[7] = DS8.transform.Find ("Text").GetComponent<Text> ();


		disp = GameObject.FindGameObjectWithTag ("display").GetComponent<Text> ();


		//clr = GameObject.FindGameObjectWithTag ("clear").GetComponent<Button> ();
	}

	void blindButtonClick()
	{
		DS1.onClick.AddListener (delegate {
			buttonClick("DS1");
		});
		DS2.onClick.AddListener (delegate {
			buttonClick("DS2");
		});
		DS3.onClick.AddListener (delegate {
			buttonClick("DS3");
		});
		DS4.onClick.AddListener (delegate {
			buttonClick("DS4");
		});		
		DS5.onClick.AddListener (delegate {
			buttonClick("DS5");
		});		
		DS6.onClick.AddListener (delegate {
			buttonClick("DS6");
		});
		DS7.onClick.AddListener (delegate {
			buttonClick("DS7");
		});
		DS8.onClick.AddListener (delegate {
			buttonClick("DS8");
		});

		pre.onClick.AddListener (delegate {
			preword();
		});
		nex.onClick.AddListener (delegate {
			nextword();
		});

		//clr.onClick.AddListener (delegate {
		//	disp.text = "";	
		//});

	}

	void buttonClick(string tag)
	{
		string tmp = null;

		switch (tag) {
		case "DS1":	
			tmp = displayWord[0].text;
			break;
		case "DS2":	
			tmp = displayWord[1].text;
			break;
		case "DS3":	
			tmp = displayWord[2].text;
			break;
		case "DS4":	
			tmp = displayWord[3].text;
			break;
		case "DS5":	
			tmp = displayWord[4].text;
			break;
		case "DS6":	
			tmp = displayWord[5].text;
			break;
		case "DS7":	
			tmp = displayWord[6].text;
			break;
		case "DS8":	
			tmp = displayWord[7].text;
			break;
		default:
			break;
		}

		disp.text = disp.text + tmp;
		input.text = "";
		input.ActivateInputField ();
        input.gameObject.SetActive(false);
		
	}
	void nextword()
	{
		if (current == page)
			current = page;
		else
			current++;
		display (result);
	}

	void preword()
	{
		if (current == 0)
			current = 0;
		else
			current--;
		display (result);
	}

	void display(string[] str)
	{
		clear ();
		if (page == 0 && surplus != 0) {
			for (int i = 0; i < surplus; i++) {
				displayWord [i].text = str [i];
			}
		
		} else if (page > 0) {
			if (current == page) {
				for (int i = 0; i < surplus; i++) {
					displayWord [i].text = str [page * 8 + i];
				}
			} else {
				for (int i = 0; i < 8; i++) {
					displayWord [i].text = str [current * 8 + i];
				}
			}
		}
	}

	void clear()
	{
		for (int i = 0; i < 8; i++) {
			displayWord [i].text = "";
		}
	}

	// Use this for initialization
	void Start () {
		getDisplayObject ();
		blindButtonClick ();


		en = new engine ();

		input.onValueChanged.AddListener (delegate {
			valueChanged();
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void valueChanged()
	{
		string str;
		en.search (input.text,out str);
		if (str != null) {
			result = null;
			result = str.Split (' ');
			current = 0;
			page = result.Length / 8;
			surplus = result.Length % 8;

			display (result);

			//Debug.Log (str + page + surplus);
		} else {
			//Debug.Log ("");
			clear();
		}
	}
}
