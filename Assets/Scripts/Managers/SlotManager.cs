using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterAR.Utility;

public class SlotManager : Singleton<SlotManager>
{
    // Start is called before the first frame update
    [SerializeField] private Slot[] slots;
    [SerializeField] private Sprite[] prizeSprites;

    private int slotResultA;
    private int slotResultB;
    private int slotResultC;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>().sprite = prizeSprites[i];            
        }
    }

    IEnumerator RollingSlots(){
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].StartRolling();            
            yield return new WaitForSeconds(0.3f);   
        }
    }

    public void GoRolling(){
        StartCoroutine(RollingSlots());
        RandomResult();
    }

    public void RandomResult(){
        slotResultA = Random.Range(0, prizeSprites.Length);
        slotResultB = Random.Range(0, prizeSprites.Length);
        slotResultC = Random.Range(0, prizeSprites.Length);
        Debug.Log(slotResultA + " " + slotResultB + " " + slotResultC);
    }

    public Sprite GetPrizeSprite(int index){
        return prizeSprites[index];
    }

    public Sprite[] GetPrizeSprites(){
        return prizeSprites;
    }

    public int GetSlotResultA(){
        return slotResultA;
    }
    public int GetSlotResultB(){
        return slotResultB;
    }
    public int GetSlotResultC(){
        return slotResultC;
    }

   
}
