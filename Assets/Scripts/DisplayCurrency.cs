using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DisplayCurrency : MonoBehaviour
{
    public int currency;
    public TextMeshProUGUI currencyText;
    public GameObject currencyObj;
    private int savedCurrency = 0;

   

    // Start is called before the first frame update
    void Start()
    {
        currencyText = currencyObj.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Awake()
    {
        if(savedCurrency == 0)
        {
            currencyText.text = "Currency: " + savedCurrency.ToString("D");
        }
        currency = savedCurrency;

    }
    void LateUpdate()
    {
        if ( savedCurrency != currency)
        {
            savedCurrency = currency;
            currencyText.text = "Currency: " + currency.ToString("D");

        }


    }

    public void AddCurrency(string name)
    {
        Debug.Log("AddCurrency");
        int ID = 0;
        if (name.Contains("BlueCube"))
            ID = 1;

        switch (ID)
        {
            case 1:
                currency += 1;
                break;
            default:
                break;
        }
 
    }
}
