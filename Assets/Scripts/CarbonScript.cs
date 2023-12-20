using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarbonScript : MonoBehaviour
{
    public float carbonValue;

    Color lerpedColor = Color.white;
    Renderer renderer;

    // 0 - 60 value color


    [SerializeField] private GameObject vaad;
    [SerializeField] private GameObject kvaeg;
    [SerializeField] private GameObject forest;


    public Color greenColor;
    public Color blueColor;
    public Color redColor;

    [SerializeField] public float interpolationSpeed = 1.0f;

    private void Awake()
    {
        carbonValue = 12f;
        renderer = GetComponent<Renderer>();
        greenColor = forest.GetComponent<Renderer>().sharedMaterial.color;
        blueColor = vaad.GetComponent<Renderer>().sharedMaterial.color;
        redColor = kvaeg.GetComponent<Renderer>().sharedMaterial.color;
    }
    private void Start()
    {
        
    }

    void Update()
    {
        if(carbonValue <= 12)
        {
            lerpedColor = Color.Lerp(Color.green, blueColor, Mathf.InverseLerp(0, 12, carbonValue));
        }
        else if(carbonValue <= 24 && carbonValue > 12)
        {
            lerpedColor = Color.Lerp(redColor, blueColor, Mathf.InverseLerp(24, 12, carbonValue));
        }


        renderer.material.color = Color.Lerp(renderer.material.color, lerpedColor, Time.deltaTime * interpolationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Forest"))
        {
            carbonValue -= 2f;
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
