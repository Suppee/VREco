using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class co2Counter : MonoBehaviour
{
    public int count;
    public float countInterval;
    public TextMeshProUGUI co2DisplayText;

    private void Start()
    {
        StartCoroutine(DisplayCO2Value(countInterval));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CarbonDioxide")) {
            count++;
        }
    }

    IEnumerator DisplayCO2Value(float interval)
    {
        count = 0;
        yield return new WaitForSeconds(interval);
        co2DisplayText.text = (count.ToString() + " CO2 per " + countInterval + " seconds");
        StartCoroutine(DisplayCO2Value(countInterval));
    }
}
