using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class PrizeManager : MonoBehaviour
{
    
    private bool canGivePrize;
    private WinPrizeData[] winPrizeDatas;
    

    void Start(){
        CreateWinPrizeData(5);
        // CheckCanGivePrize();
    }

    private void CheckCanGivePrize(){
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData tempNowDate = JsonUtility.FromJson<WinPrizeData>(jsonFile);
        Debug.Log("Day: " + tempNowDate.Day);
        Debug.Log("Hour: " + tempNowDate.Hour);
        Debug.Log("Minute: " + tempNowDate.Minute);
        Debug.Log("Prize Index: " + tempNowDate.PrizeIndex);
    }

    private WinPrizeData CreateWinPrizeData(int prizeIndex){
        WinPrizeData winPrizeData = new WinPrizeData();
        winPrizeData.Day = System.DateTime.Now.Day;
        winPrizeData.Hour = System.DateTime.Now.Hour;
        winPrizeData.Minute = System.DateTime.Now.Minute;        
        winPrizeData.PrizeIndex = prizeIndex;

        string jsonData = JsonUtility.ToJson(winPrizeData);
        // Debug.Log("Save WinData JSON: " + jsonData);

        File.WriteAllText(Application.dataPath + "/winPriteDataFile.json", jsonData);       
        return winPrizeData;
    }

    public bool GetCanGivePrize(){
        return canGivePrize;
    }


}
