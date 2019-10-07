using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class SlotManager : Singleton<SlotManager>
{
    // Start is called before the first frame update
    [SerializeField] private Slot[] slots = null;
    [SerializeField] private Sprite[] itemSprites = null;

    private int[] slotResults = new int[3];
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = itemSprites[i];            
        }

        // AvoidWinResult();
    }

    IEnumerator RollingSlots(){
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].StartRolling();            
            yield return new WaitForSeconds(0.3f);   
        }
        yield return new WaitForSeconds(2);   
        
        // if(PrizeManager.Instance.CheckCanGivePrizeByTime())
            RandomResult();
        // else
            // AvoidWinResult();
    }

    public void GoRolling(){
        StartCoroutine(RollingSlots());        
    }

    public void SetSlotResults(int value){
        slotResults[0] = value;
        slotResults[1] = value;
        slotResults[2] = value;
    }

    public void RandomResult(){
        Debug.Log("Random Result");

        slotResults[0] = Random.Range(0, itemSprites.Length);
        slotResults[1] = Random.Range(0, itemSprites.Length);
        slotResults[2] = Random.Range(0, itemSprites.Length);

        slotResults[0] = 0;
        slotResults[1] = 0;
        slotResults[2] = 0;

        PrizeManager.Instance.CheckResult();

        
    }

    public void StopRollingSlots(){

        //Stop Rolling Slots
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Result Slot " + i + ": " + slotResults[i]);
            slots[i].StopRolling(i * 0.3f);
        }
    }


    

    public void AvoidWinResult(){
        Debug.Log("Avoiding Win Result");
        int avoidSlot = Random.Range(0,3);

        slotResults[0] = Random.Range(0, itemSprites.Length);
        slotResults[1] = Random.Range(0, itemSprites.Length);
        slotResults[2] = Random.Range(0, itemSprites.Length);

        // slotResults[0] = 0;
        // slotResults[1] = 0;
        // slotResults[2] = 0;

        while (slotResults[0] == slotResults[1] && slotResults[1] == slotResults[2])
        {
            Debug.Log("Result Same, Random Again");
            slotResults[avoidSlot] = Random.Range(0, itemSprites.Length);
        }
        
        PrizeManager.Instance.CheckResult();                
    }

    public void EmptyPrizeResult(){
        Debug.Log("Avoiding Win Result");
        int avoidSlot = Random.Range(0,3);

        slotResults[0] = Random.Range(0, itemSprites.Length);
        slotResults[1] = Random.Range(0, itemSprites.Length);
        slotResults[2] = Random.Range(0, itemSprites.Length);

        // slotResults[0] = 0;
        // slotResults[1] = 0;
        // slotResults[2] = 0;

        while (slotResults[0] == slotResults[1] && slotResults[1] == slotResults[2])
        {
            Debug.Log("Result Same, Random Again");
            slotResults[avoidSlot] = Random.Range(0, itemSprites.Length);
        }
    }

    public Sprite GetItemSprite(int index){
        return itemSprites[index];
    }

    public Sprite[] GetItemSprites(){
        return itemSprites;
    }

    public int GetSlotResultA(){
        return slotResults[0];
    }
    public int GetSlotResultB(){
        return slotResults[1];
    }
    public int GetSlotResultC(){
        return slotResults[2];
    }

   
}
