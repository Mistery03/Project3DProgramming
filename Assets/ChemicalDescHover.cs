using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChemicalDescHover : MonoBehaviour
{
    public Image chemicalImage;
    public TextMeshProUGUI chemicalName;
    public TextMeshProUGUI chemicalDesc;

    public void setHoverContent(ChemicalData chemical)
    {
        chemicalImage.sprite = chemical.ItemImage;
        chemicalName.text = chemical.Name;
        chemicalDesc.text = chemical.Description;
    }
}
