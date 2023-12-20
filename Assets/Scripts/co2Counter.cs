using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class co2Counter : MonoBehaviour
{
    public TextMeshProUGUI co2DisplayText;

    public float co2Count;
    public float countInterval;
    private CarbonScript riverPolScript;

    private void Start()
    {
        co2Count = 0;
        co2DisplayText.text = (co2Count.ToString() + " Kvælstof enheder per " + countInterval + " sekunder");
        StartCoroutine(DisplayCO2Value(countInterval));
    }

    private void OnTriggerEnter(Collider other)
    {
        riverPolScript = other.GetComponent<CarbonScript>();
        if(other.gameObject.CompareTag("CarbonDioxide")) {
            co2Count += riverPolScript.carbonValue;
        }
    }

    IEnumerator DisplayCO2Value(float interval)
    {
        co2Count = 0;
        yield return new WaitForSeconds(interval);
        co2DisplayText.text = (co2Count.ToString() + " Kvælstof enheder per " + countInterval + " sekunder");
        StartCoroutine(DisplayCO2Value(countInterval));
    }
}
