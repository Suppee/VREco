using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class co2Counter : MonoBehaviour
{
    public int co2Count;
    public float countInterval;
    public TextMeshProUGUI co2DisplayText;

    private void Start()
    {
        co2DisplayText.text = (co2Count.ToString() + " CO2 per " + countInterval + " seconds");
        StartCoroutine(DisplayCO2Value(countInterval));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CarbonDioxide")) {
            co2Count++;
        }
    }

    IEnumerator DisplayCO2Value(float interval)
    {
        co2Count = 0;
        yield return new WaitForSeconds(interval);
        co2DisplayText.text = (co2Count.ToString() + " CO2 per " + countInterval + " seconds");
        StartCoroutine(DisplayCO2Value(countInterval));
    }
}
