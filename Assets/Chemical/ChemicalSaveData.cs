using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChemicalSaveData 
{

    public int[] chemicalAmountList;

    public ChemicalSaveData(ChemicalList cl)
    {
        chemicalAmountList[0] = cl.hydrogenAmt;
        chemicalAmountList[1] = cl.oxygenAmt;
        chemicalAmountList[2] = cl.carbonAmt;
        chemicalAmountList[3] = cl.uraniumAmt;

    }


    
}
