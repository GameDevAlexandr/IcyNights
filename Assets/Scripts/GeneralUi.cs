using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUi : MonoBehaviour
{
    public static GameObject characterPanel;
    public static GameObject hint;
    public static Text hintText;
    [SerializeField] private GameObject charPanel;
    [SerializeField] private GameObject hintPanel;
    private void Awake()
    {
        characterPanel = charPanel;
        hint = hintPanel;
        hint.SetActive(true);
        hintText = GetComponentInChildren<Text>();
        hint.SetActive(false);
    }

}
