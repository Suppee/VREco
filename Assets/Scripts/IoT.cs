using UnityEngine;
using KyleDulce.SocketIo;

public class IoT : MonoBehaviour
{
    private bool runLocal = false;
    Socket socket;

    private string currentLEDValue = "0";
    private string currentPotValue = "0";
    private float potRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        /*--------------               Connect to Server                                    ----------------------*/
        if (runLocal)
        {
            Debug.Log("Connect to Local Server");
            socket = SocketIo.establishSocketConnection("ws://localhost:3000");
        }
        else
        {
            Debug.Log("Connect to Online Server");
            socket = SocketIo.establishSocketConnection("ws://35.228.129.246:8080");
        }

        //Connect to server
        socket.connect();
        Debug.Log("Socket IO - Connected");



        /*--------------               Receive Updates                                    ----------------------*/
        //On "CurrentLEDValue"
        socket.on("CurrentLEDValue", SetCurrentLEDValue);

        //On "CurrentPotentiometerValue"
        socket.on("CurrentPotentiometerValue", SetCurrentPotentiometerValue);
    }

    void SetCurrentLEDValue(string data)
    {
        currentLEDValue = data;
        Debug.Log("CurrentLEDValue Received: " + currentLEDValue);
    }

    void SetCurrentPotentiometerValue(string data)
    {
        currentPotValue = data;
        Debug.Log("CurrentPotValue Received: " + currentPotValue);
    }

    void Update()
    {
        
        PlotManager.Instance.PlotMessage(currentPotValue);
        //Rotate 3D Object
    }
}

