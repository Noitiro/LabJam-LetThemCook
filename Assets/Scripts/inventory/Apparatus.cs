using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Apparatus : MonoBehaviour
{
    public string name = "Alembic";
    public double workTime = 10.0;
    public AudioManager Audio;

    public List<GameObject> apparatusInputSlots = new List<GameObject>();
    public GameObject apparatusOutputSlot = null;

    bool isWorking = false;
    double workTimer = 0;

    List<ItemSlot> inputItemSlots = new List<ItemSlot>();
    ItemSlot outputItemSlot = null;

    GameObject clock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject x in apparatusInputSlots)
        {
            inputItemSlots.Add(x.GetComponent<ItemSlot>());
        }

        outputItemSlot = apparatusOutputSlot.GetComponent<ItemSlot>();


        clock = transform.Find("Clock").gameObject;
        clock.GetComponent<Image>().fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWorking)
        {
            workTimer -= Time.deltaTime;

            clock.GetComponent<Image>().fillAmount = (float)(workTime / workTime);

            if (workTimer <= 0)
            {
                ClearAll();
            }
        }
    }

    public void StartWork()
    {
        bool isEmpty = true;

        foreach (ItemSlot slot in inputItemSlots)
        {
            if (slot.itemInSlot != null)
            {
                isEmpty = false;
                break;
            }
        }

        if(!isEmpty)
        {
            workTimer = workTime;
            isWorking = true;

            Audio.Play(name);
        }
    }

    public void ClearAll()
    {
        foreach (ItemSlot slot in inputItemSlots)
        {
            slot.ClearSlot();
        }

        outputItemSlot.ClearSlot();

        if (isWorking) isWorking = false;
        Audio.FadeOut(name);
    }
}
