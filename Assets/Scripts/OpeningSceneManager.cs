using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OpeningSceneManager : MonoBehaviour
{
    public GameObject _player;
    public GameObject DialogueBox;
    public GameObject HelpText;
    private TMP_Text currentText;

    public String[] Dialogues;    
    private int dialogueNumber = 0;

    private void Start()
    {
        currentText = DialogueBox.GetComponentInChildren<TMP_Text>();
        _player.GetComponent<PlayerScript>().onPause = true;
        _player.GetComponent<PlayerMovement>().onPause = true;
    }

    private void Update()
    {
        if (InputManager.Continue)
        {
            ContinueDialogue();
        }
    }

    public void ContinueDialogue()
    {
        dialogueNumber++;
        currentText.text = Dialogues[dialogueNumber];
        switch (dialogueNumber)
        {
            case 1:
                Destroy(HelpText);
                _player.GetComponent<Animator>().Play("LightActivate");
            break;
            case 2:
            break;
            case 3:
                _player.GetComponent<PlayerScript>().onPause = false;
                _player.GetComponent<PlayerMovement>().onPause = false;
            break;
        }
    }
}
