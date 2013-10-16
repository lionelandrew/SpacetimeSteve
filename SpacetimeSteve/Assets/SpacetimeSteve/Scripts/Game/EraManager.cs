using UnityEngine;
using System.Collections;

public class EraManager : MonoBehaviour {

    public enum Era
    {
        StoneAge,
        AztecCivilization,
        OldKingdom,
        ShanghaiDynasty,
        RomanEmpire,
        NewWorldEra,
        RevolutionEra,
        ProhibitionEra,
        ModernDay,
        FuturisticEra
    }

    public int wavePerEraAmount = 10;

    public Era currentEra = 0;

    public void SetCurrentEra(int waveNumber)
    {
        int tempEra = (int)(waveNumber / wavePerEraAmount);

        if ((int)currentEra == tempEra)
            return;
        else
        {
            currentEra = (Era)tempEra;
        }
    }
}
