using UnityEngine;
using System.Collections;

public class SendOSC : MonoBehaviour {


	public OSC oscReference;

    public string address;

    public string sendMSG;


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SendMsg(address, sendMSG);
        }
    }

	public void SendMsg(string address,string Msg)
    {
        OscMessage message = new OscMessage();
        message.address = "/" + address;
        message.values.Add(Msg);
        oscReference.Send(message);
    }
}
