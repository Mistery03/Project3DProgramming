using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemicalList : MonoBehaviour
{
    public TextMeshProUGUI hydrogenAmountText;
    public TextMeshProUGUI oxygenAmountText;
    public TextMeshProUGUI carbonAmountText;
    public TextMeshProUGUI mercuryAmountText;
    public TextMeshProUGUI uraniumAmountText;

    int defaultValue = 0;
    void Start()
    {
        hydrogenAmountText.text = defaultValue.ToString();
        oxygenAmountText.text = defaultValue.ToString();
        carbonAmountText.text = defaultValue.ToString();
        mercuryAmountText.text = defaultValue.ToString();
        uraniumAmountText.text = defaultValue.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
