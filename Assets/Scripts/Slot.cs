using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    
    private GameObject content;
    private bool rolling;
    private GameObject resultSlotItem;


    // Start is called before the first frame update
    void Start()
    {
        content = transform.GetChild(0).GetChild(0).gameObject;
        resultSlotItem = transform.GetChild(0).GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(rolling){
            content.transform.position = new Vector3(
                content.transform.position.x,
                content.transform.position.y + 20,
                content.transform.position.z
            );
        }
    }

    void SetChoosedItem(){
        int prizeIndex = 0;
        int beforePrizeIndex = 0;
        int afterPrizeIndex = 0;
        int choosedItemIndex = 0;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                choosedItemIndex = SlotManager.Instance.GetSlotResultA();                                
                break;
            case 1:
                choosedItemIndex = SlotManager.Instance.GetSlotResultB();                       
                break;
            case 2:
                choosedItemIndex = SlotManager.Instance.GetSlotResultC();                       
                break;
            default:
                choosedItemIndex = SlotManager.Instance.GetSlotResultA();                       
                break;
        }

        

        beforePrizeIndex = choosedItemIndex - 1;
        prizeIndex = choosedItemIndex;
        afterPrizeIndex = choosedItemIndex + 1;

        //Set before item
        if(choosedItemIndex == 0)
            beforePrizeIndex = SlotManager.Instance.GetPrizeSprites().Length - 1;
        
        if(choosedItemIndex == SlotManager.Instance.GetPrizeSprites().Length - 1)
            afterPrizeIndex = 0;
        
        //
        resultSlotItem.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = SlotManager.Instance.GetPrizeSprite(beforePrizeIndex);
        //
        resultSlotItem.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = SlotManager.Instance.GetPrizeSprite(prizeIndex);
        //
        resultSlotItem.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = SlotManager.Instance.GetPrizeSprite(afterPrizeIndex);
        
        




        // Debug.Log("Index: " + transform.GetSiblingIndex());
        // Debug.Log("A: " + SlotManager.Instance.GetSlotResultA());
        // Debug.Log("B: " + SlotManager.Instance.GetSlotResultB());
        // Debug.Log("C: " + SlotManager.Instance.GetSlotResultC());
    }

    public void StartRolling(){
        rolling = true;
        StartCoroutine(Rolling());
    }

    public void StopRolling(){
        rolling = false;
        resultSlotItem.SetActive(true);
        content.SetActive(false);

        //set result item
        SetChoosedItem();

       
        
    }

    IEnumerator Rolling(){
        resultSlotItem.SetActive(false);
        content.SetActive(true);
        
        yield return new WaitForSeconds(2);        
        StopRolling();

        //For Auto Rolling
        if(transform.GetSiblingIndex() == 0){
            yield return new WaitForSeconds(2);        
            SlotManager.Instance.GoRolling();
        }
    }

    
}
