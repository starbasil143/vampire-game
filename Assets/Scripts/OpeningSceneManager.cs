using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject _player;
    public GameObject DialogueBox;
    public GameObject HelpText;
    public GameObject ContinueIcon;
    public GameObject NormalUI;
    public GameObject fragmentimage;
    public List<int> SkippableDialogues;
    public List<int> ClosableDialogues;
    private TMP_Text currentText;
    public bool dialogueIsHeld;

    public String[] Dialogues;    
    private int dialogueNumber = 0;

    private void Start()
    {
        DialogueBox.SetActive(true);
        currentText = DialogueBox.GetComponentInChildren<TMP_Text>();
        _player.GetComponent<PlayerScript>().onPause = true;
        _player.GetComponent<PlayerMovement>().onPause = true;
        _player.GetComponent<PlayerScript>().inTutorial = true;
    }

    private void Update()
    {
        if (InputManager.Continue)
        {
            if (SkippableDialogues.Contains(dialogueNumber))
            {
                ContinueDialogue();
            }
            if (ClosableDialogues.Contains(dialogueNumber))
            {
                HoldDialogue();
            }
        }
    }

    public void GetDialogueTrigger(Collider2D collision)
    {
        ContinueDialogue();
        Destroy(collision);
    }


    public void HoldDialogue()
    {
        DialogueBox.SetActive(false);
        dialogueIsHeld = true;
    }
    public void ContinueDialogue()
    {
        if (dialogueIsHeld)
        {
            DialogueBox.SetActive(true);
            dialogueIsHeld = false;
        }
        dialogueNumber++;
        currentText.text = Dialogues[dialogueNumber];
        if (SkippableDialogues.Contains(dialogueNumber))
        {
            ContinueIcon.SetActive(true);
        }
        else
        {
            ContinueIcon.SetActive(false);
        }
        switch (dialogueNumber)
        {
            case 1:
                _player.GetComponent<Animator>().Play("LightActivate");
            break;
            case 2:
            break;
            case 3:
                _player.GetComponent<PlayerScript>().onPause = false;
                _player.GetComponent<PlayerMovement>().onPause = false;
                _player.GetComponent<PlayerScript>().inTutorial = false;
            break;
            case 6:
                NormalUI.SetActive(true);
            break;
            case 9:
                fragmentimage.SetActive(true);
            break;
            case 10:
                fragmentimage.SetActive(false);
            break;
        }
    }
}
