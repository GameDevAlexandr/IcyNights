using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestJournal : MonoBehaviour
{
    [SerializeField] private List<Text> questionsText;

    public void AddQuest(string quest)
    {
        for (int i = 0; i < questionsText.Count; i++)
        {
            if (questionsText[i].text == "")
            {
                questionsText[i].text = quest;
                return;
            }
        }
    }
}
