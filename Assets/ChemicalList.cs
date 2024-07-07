using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ChemicalList : MonoBehaviour
{
    public TextMeshProUGUI hydrogenAmountText;
    public TextMeshProUGUI oxygenAmountText;
    public TextMeshProUGUI carbonAmountText;
    public TextMeshProUGUI uraniumAmountText;

    int defaultValue = 0;

    public int carbonAmt =0, oxygenAmt=0, uraniumAmt=0,hydrogenAmt=0;
    void Start()
    {

        refreshCount();

    }
    public void addChemical(ChemicalData chemical, int amount = -1)
    {
        switch(chemical.ID)
        {
            case 0:
                if (amount == 1)
                    carbonAmt++;
                else
                    carbonAmt += amount;

                break;
            case 1:
                if (amount == 1)
                    oxygenAmt++;
                else
                    oxygenAmt += amount;

                break;
            case 2:
                if (amount == 1)
                    uraniumAmt++;
                else
                    uraniumAmt += amount;

                break;
            case 3:
                if (amount == 1)
                    hydrogenAmt++;
                else
                    hydrogenAmt += amount;
                break;
        }
    }

    public void refreshCount()
    {
        carbonAmountText.text = carbonAmt.ToString();
        oxygenAmountText.text = oxygenAmt.ToString();
        uraniumAmountText.text = uraniumAmt.ToString();
        hydrogenAmountText.text = hydrogenAmt.ToString();

    }

    public void saveData()
    {
        SaveSystem.saveChemical(this);
    }

    public void loadData()
    {
        ChemicalSaveData data = SaveSystem.loadChemical();
        hydrogenAmt = data.hydrogenAmount;
        oxygenAmt = data.oxygenAmount;
        carbonAmt = data.carbonAmount;
        uraniumAmt = data.uraniumAmount;
        refreshCount();
    }

    public void restartData()
    {
        hydrogenAmt = 0;
        oxygenAmt = 0;
        carbonAmt = 0;
        uraniumAmt = 0;
        SaveSystem.saveChemical(this);
    }
}
