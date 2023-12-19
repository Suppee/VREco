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

    private void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        if (msg == "On")
        {
            plot1ref = Instantiate(forest, plot1);
        }
        else if (msg == "Off")
        {
            Destroy(plot1ref);
        }
    }
}
