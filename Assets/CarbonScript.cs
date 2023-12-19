using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarbonScript : MonoBehaviour
{
    public float carbonValue;

    private void Start()
    {
        carbonValue = 5.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Forest"))
        {
            carbonValue -= 2f;
            Debug.Log(carbonValue);
        }
        else if(other.gameObject.CompareTag("Mark"))
        {
            carbonValue += 3f;
        }
        else if(other.gameObject.CompareTag("Vaadomraade"))
        {
            carbonValue -= 3f;
        }
        else if(other.gameObject.CompareTag("Kvaeghold"))
        {
            carbonValue += 2f;
        }
    }
}
