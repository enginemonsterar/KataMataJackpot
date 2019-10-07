using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MonsterAR.Utility;


[System.Serializable]
public class PrizeManager : Singleton<PrizeManager>
{
    [SerializeField] private Sprite[] prizeSprites;
    private bool canGivePrize;
    private WinPrizeData[] winPrizeDatas;
    

    void Start(){
        // CreateWinPrizeData(0);
        // CreateWinPrizeData(1);
        // CreateWinPrizeData(2);
        // CreateWinPrizeData(3);
        // ReadAllJsonData();
        // Debug.Log(CheckCanGivePrizeByTime());
        // Debug.Log("Can Give Prize by Index 0: " + CheckCanGivePrizeByPrizeIndex(0));
        // Debug.Log("Can Give Prize by Index 1: " + CheckCanGivePrizeByPrizeIndex(1));
        // Debug.Log("Can Give Prize by Index 2: " + CheckCanGivePrizeByPrizeIndex(2));
        // Debug.Log("Can Give Prize by Index 3: " + CheckCanGivePrizeByPrizeIndex(3));
    }

    private void ReadAllJsonData(){
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData[] tempData = JsonHelper.FromJson<WinPrizeData>(jsonFile);
        
        if(tempData != null){
            for (int i = 0; i < tempData.Length; i++)
            {
                Debug.Log("===============  "+ i +"  =============");
                Debug.Log("Day " + i + ": " +tempData[i].Day);
                Debug.Log("Hour "  + i + ": " + tempData[i].Hour);
                Debug.Log("Minute " + i + ": " + tempData[i].Minute);
                Debug.Log("Prize Index " + i + ": " + tempData[i].PrizeIndex);
            }
            
        }else
        {
            Debug.LogWarning("No Data on JsonFile");               
        }
    }

    public int GetJsonDataLenght(){
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData[] tempData = JsonHelper.FromJson<WinPrizeData>(jsonFile);
        if(tempData != null)
            return tempData.Length;
        else
        {
            return 0;
        }
    }

    public void CheckResult(){
        bool anySlotAvailable = false;
        if(SlotManager.Instance.GetSlotResultA() == SlotManager.Instance.GetSlotResultB() && SlotManager.Instance.GetSlotResultB() == SlotManager.Instance.GetSlotResultC()){                
                        
            if(PrizeManager.Instance.GetJsonDataLenght() == 0){
                PrizeManager.Instance.CreateWinPrizeData(SlotManager.Instance.GetSlotResultA());
            }else{
                if(CheckCanGivePrizeByPrizeIndex(SlotManager.Instance.GetSlotResultA())){                        
                    CreateWinPrizeData(SlotManager.Instance.GetSlotResultA());
                }else{
                    Debug.LogWarning("Prize Index " +  SlotManager.Instance.GetSlotResultA() + " is Empty" );
                    for (int i = 0; i < prizeSprites.Length; i++)
                    {
                        if(CheckCanGivePrizeByPrizeIndex(i)){
                            CreateWinPrizeData(i);
                            SlotManager.Instance.SetSlotResults(i);
                            anySlotAvailable = true;
                            break;
                        }
                    }
                }
            }

            if(!anySlotAvailable){
                SlotManager.Instance.EmptyPrizeResult();
            }

            SlotManager.Instance.StopRollingSlots();
        }else
        {            
            SlotManager.Instance.StopRollingSlots();
            //For Auto Rolling
            // SlotManager.Instance.GoRolling();
        }

    }

    public bool CheckCanGivePrizeByTime(){
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData[] tempData = JsonHelper.FromJson<WinPrizeData>(jsonFile);
                
        if(tempData != null){
            int lastDataIndex = tempData.Length - 1;            
            
            if((System.DateTime.Now.Hour - tempData[lastDataIndex].Hour) >= 2){
                return true;
            }else{
                return false;
            }
            
        }else
        {
            Debug.LogWarning("No Data on JsonFile");
            return true;
        }
    }

    public bool CheckCanGivePrizeByPrizeIndex(int prizeIndex){
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData[] tempData = JsonHelper.FromJson<WinPrizeData>(jsonFile);
                
        if(tempData != null){
            int lastDataIndex = tempData.Length - 1;            
            bool canGive = true;
            for (int i = 0; i < tempData.Length; i++)
            {
                if(tempData[i].PrizeIndex == prizeIndex)
                    canGive = false;                
            }            

            return canGive;
            
        }else
        {
            Debug.LogWarning("No Data on JsonFile");
            return true;
        }
    }

    public void CreateWinPrizeData(int prizeIndex){

        //Get All item on Json
        string jsonFile = File.ReadAllText(Application.dataPath + "/winPriteDataFile.json");        
        WinPrizeData[] tempData = JsonHelper.FromJson<WinPrizeData>(jsonFile);


        //Create array with new data slot                
        WinPrizeData[] winPrizeData;
        int newDataIndex;
        if(tempData != null){
            winPrizeData = new WinPrizeData[tempData.Length + 1];
            newDataIndex = tempData.Length;
            // Debug.Log(winPrizeData.Length);
            for (int i = 0; i < tempData.Length; i++)
            {
                winPrizeData[i] = new WinPrizeData();
                winPrizeData[i].Day = tempData[i].Day;
                winPrizeData[i].Hour = tempData[i].Hour;
                winPrizeData[i].Minute = tempData[i].Minute;
                winPrizeData[i].PrizeIndex = tempData[i].PrizeIndex;
            }
        }
        else{
            newDataIndex = 0;
            winPrizeData = new WinPrizeData[1];
        }
        
        
        winPrizeData[newDataIndex] = new WinPrizeData();
        winPrizeData[newDataIndex].Day = System.DateTime.Now.Day;
        winPrizeData[newDataIndex].Hour = System.DateTime.Now.Hour;
        winPrizeData[newDataIndex].Minute = System.DateTime.Now.Minute;        
        winPrizeData[newDataIndex].PrizeIndex = prizeIndex ;

        string jsonData = JsonHelper.ToJson(winPrizeData, true);
        // Debug.Log("Save WinData JSON: " + jsonData);

        File.WriteAllText(Application.dataPath + "/winPriteDataFile.json", jsonData);       
    
    }

    public bool GetCanGivePrize(){
        return canGivePrize;
    }


}
