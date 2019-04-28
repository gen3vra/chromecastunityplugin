using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Cast.RemoteDisplay;
using Google.Cast.RemoteDisplay.Internal;

public class Debug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Casting: " + CastRemoteDisplayManager.GetInstance().IsCasting() + "\nCast App Id: " + CastRemoteDisplayManager.GetInstance().CastAppId;
	}

    public void TestButton()
    {
        UnityEngine.Debug.LogError("NO ERROR: Yep, it's working.");
        //CastRemoteDisplayExtensionManager.workaroundCallback();
    }

    public void Updated()
    {
        //UnityEngine.Debug.LogError("NO ERROR: Cast devices updated.");
    }

    public void Started()
    {
        UnityEngine.Debug.LogError("NO ERROR: Session started.");
    }

    public void Ended()
    {
        UnityEngine.Debug.LogError("NO ERROR: Session ended.");
    }

    public void DisplayError()
    {
        UnityEngine.Debug.LogError("There was an error.");
    }
}
