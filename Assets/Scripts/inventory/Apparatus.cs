using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Apparatus : MonoBehaviour
{
    public string name = "Alembic";
    public AlchemyEnums.Instruments ApparatusType;

    public double workTime = 5.0;
    public AudioManager Audio;
    public RecipeSearcher Searcher;

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

        switch(name)
        {
            case "Alembic":
                ApparatusType = AlchemyEnums.Instruments.Alembic;
                break;

            case "Mortar nad Pestle":
                ApparatusType = AlchemyEnums.Instruments.Mortar;
                break;

            case "Athanor":
                ApparatusType = AlchemyEnums.Instruments.Athanor;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isWorking)
        {
            workTimer -= Time.deltaTime;

            clock.GetComponent<Image>().fillAmount = (float)(workTimer / workTime);

            if (workTimer <= 0)
            {
                //AlchemyEnums.Ingredients? ReturnRecipe(AlchemyEnums.Instruments InstrumentType, List<AlchemyEnums.Ingredients> Ingredients)

                List<AlchemyEnums.Ingredients> Ingredients = new List<AlchemyEnums.Ingredients>();

                foreach (ItemSlot slot in inputItemSlots)
                {
                    if (slot.itemInSlot != AlchemyEnums.Ingredients.Null) Ingredients.Add(slot.itemInSlot);
                }

                AlchemyEnums.Ingredients OutputRecipe = Searcher.ReturnRecipe(ApparatusType, Ingredients);

                if (OutputRecipe != AlchemyEnums.Ingredients.Null)
                {
                    RecipeSO ItemRecipe = Searcher.ReturnIngredientRecipe(OutputRecipe);

                    //outputItemSlot.itemInSlot = OutputRecipe;
                    //outputItemSlot.itemImage = ItemRecipe.icon;
                    //outputItemSlot.button.GetComponent<Image>().sprite = ItemRecipe.icon;

                    outputItemSlot.SetItem(OutputRecipe, ItemRecipe.icon);
                }

                clock.GetComponent<Image>().fillAmount = 0.0f;
                ClearAll();
            }
        }
    }

    public void StartWork()
    {
        bool isEmpty = true;

        foreach (ItemSlot slot in inputItemSlots)
        {
            if (slot.itemInSlot != AlchemyEnums.Ingredients.Null)
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

        //outputItemSlot.ClearSlot();

        if (isWorking) isWorking = false;
        Audio.FadeOut(name);
    }

    public void ReduceCookingTime(float amount)
    {
        workTime -= amount;
        if (workTime < 0.5f) workTime = 0.5f;
    }
}
