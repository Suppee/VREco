using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    // Define and create instance of manager
    private static PlotManager _instance;
    public static PlotManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("GameManager is NULL");

            return _instance;
        }
    }


    public GameObject wetlands;
    public GameObject field;
    public GameObject forest;

    public Transform plot1;
    public Transform plot2;
    public Transform plot3;
    public Transform plot4;

    public GameObject plot1ref;

    public string curMsg;



    private void Awake()
    {
        _instance = this;
    }


    // ARDITY CODE 
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
   //void OnConnectionEvent(bool success)
   //{
   //    if (success)
   //        Debug.Log("Connection established");
   //    else
   //        Debug.Log("Connection attempt failed or disconnection detected");
   //}


    public void PlotMessage(string msg)
    {
        if (msg == curMsg) return;
        Debug.Log("Message arrived: " + msg);
        if (msg == "7A:96:30:1B")
        {
            if (plot1ref != null) Destroy(plot1ref);
            plot1ref = Instantiate(forest, plot1);
        }
        else if (msg == "91:18:6C:24")
        {
            if(plot1ref != null) Destroy(plot1ref);
            plot1ref = Instantiate(field, plot1);
        }
        else if (msg == "A3:1E:8D:2F")
        {
            if (plot1ref != null) Destroy(plot1ref);
            plot1ref = Instantiate(wetlands, plot1);
        }
        else if (msg == "No Card")
        {
            Destroy(plot1ref);
        }
        curMsg = msg;
    }
}
