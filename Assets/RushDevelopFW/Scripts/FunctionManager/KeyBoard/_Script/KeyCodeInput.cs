using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeInput : MonoBehaviour {

    private InputField input;
    private Text dispTxt;
    private void Awake()
    {
        input = GameObject.FindGameObjectWithTag("CHInput").GetComponent<InputField>();
        dispTxt = GameObject.FindGameObjectWithTag("display").GetComponent<Text>();

        GameObject text = new GameObject(gameObject.name);
        text.transform.SetParent(transform, false);
        Text codeTxt=text.AddComponent<Text>();
        codeTxt.font = Font.CreateDynamicFontFromOSFont("Arial", 10);
        codeTxt.color = Color.black;
        codeTxt.resizeTextForBestFit = true;
        codeTxt.resizeTextMaxSize = 20;
        codeTxt.resizeTextMinSize = 0;
        codeTxt.verticalOverflow = VerticalWrapMode.Overflow;
        codeTxt.horizontalOverflow = HorizontalWrapMode.Overflow;
        codeTxt.raycastTarget = false;
        codeTxt.alignment = TextAnchor.MiddleCenter;

        gameObject.AddComponent<Button>().onClick.AddListener(() =>
        {
            if (input.gameObject.activeSelf == false)
            {
                input.gameObject.SetActive(true);
            }
        });
        if (gameObject.name == "delete")
        {   
            codeTxt.text = gameObject.name+"(删除)";
            codeTxt.fontSize = 0;
            gameObject.GetComponent<Button>().onClick.AddListener(() => {
                if(input.text!="")
                {
                    input.text = input.text.Remove(input.text.Length - 1, 1);
                }
                else
                {
                    if(dispTxt.text!="")
                    {
                        dispTxt.text = dispTxt.text.Remove(dispTxt.text.Length - 1, 1);
                    }
                }

            });
        }
        else if (gameObject.name == "enter")
        {
            codeTxt.text = gameObject.name + "(确定)";
            gameObject.GetComponent<Button>().onClick.AddListener(() => { dispTxt.text += input.text;input.text = ""; input.gameObject.SetActive(false); });
        }
        else
        {
            codeTxt.text = gameObject.name;
            gameObject.GetComponent<Button>().onClick.AddListener(() => { input.text += gameObject.name; });
        }

       

        


    }
}
