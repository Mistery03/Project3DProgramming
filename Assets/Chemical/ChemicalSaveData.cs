using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChemicalSaveData 
{

    public int hydrogenAmount; 
    public int oxygenAmount;
    public int carbonAmount; 
    public int uraniumAmount;

    public ChemicalSaveData(ChemicalList cl)
    {
        hydrogenAmount = cl.hydrogenAmt;
        oxygenAmount = cl.oxygenAmt;
        carbonAmount = cl.carbonAmt;
        uraniumAmount = cl.uraniumAmt;

    }


    
}
