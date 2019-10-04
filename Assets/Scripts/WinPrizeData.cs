using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public class WinPrizeData
{
    public int Day;
    public int Hour;
    public int Minute;
    public int PrizeIndex;

    public WinPrizeData(int Day, int Hour, int Minute, int prizeIndex){
        this.Day = Day;
        this.Hour = Hour;
        this.Minute = Minute;
        this.PrizeIndex = PrizeIndex;
    }

    public WinPrizeData(){
        
    }

    
}
