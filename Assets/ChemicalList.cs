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

    public int hydrogenAmount = 0;
    public int oxygenAmount = 0;
    public int carbonAmount = 0;
    public int mercuryAmount = 0;
    public int uraniumAmount = 0;

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
