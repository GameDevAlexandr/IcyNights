using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftButtonClass : MonoBehaviour
{
    [SerializeField] private CraftRecipe recipe;
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CraftState();
    }

    public void CraftState()
    {
        if (recipe.canBeCrafted)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

    }
}
