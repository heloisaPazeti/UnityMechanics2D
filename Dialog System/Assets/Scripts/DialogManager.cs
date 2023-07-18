using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject avatarSprite;

    private Animator anim; //for open and close animation
    private AudioSource audioSource; // for the audio of dialog

    //Properties of Typing Effect
    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    private Queue<string> sentences; //List of sentences to be displayed
    private DialogTrigger trigger;

    private void Awake()
    {
        //initialize Queue and Get Components
        sentences = new Queue<string>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    #region "Dialog"

    /// <summary>
    /// Initialize the settings of the dialog display
    /// </summary>
    /// <param name="inserts"></param>
    /// <param name="actor"></param>
    /// <param name="cutTrigger"></param>
    public void StartDialogue(string[] inserts, Actor actor, DialogTrigger cutTrigger)
    {
        dialogueText.color = actor.txtColor;

        if (nameText != null) // sets the name of the character
        {
            nameText.text = actor.ActorName;
            nameText.color = dialogueText.color;
        }

        if (actor.Avatar != null && avatarSprite != null) //sets the avatar of the character
        {
            avatarSprite.SetActive(true);
            avatarSprite.GetComponent<Image>().sprite = actor.Avatar;
        }
        else if (avatarSprite != null) //or just erase the avatar
            avatarSprite.SetActive(false);

        trigger = cutTrigger;
        anim.SetBool("Open", true); //turn on the animation


        sentences.Clear(); //cleans the list

        foreach (string sentence in inserts)
            sentences.Enqueue(sentence); //add in the Queue every sentence

        DisplayNextSentence(); //start passing the dialogs
    }

    /// <summary>
    /// Move on to the next sentence in the dialog stack
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) //if finished the stack ends the dialog
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); //gets the next sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentences(sentence)); //start displaying next sentence
    }

    /// <summary>
    /// Make the last calls to end dialog
    /// </summary>
    private void EndDialogue()
    {
        if (trigger != null) //calls an end action if there's one
            trigger.EndDialogue();

        anim.SetBool("Open", false); //shut down the animation
        trigger = null;
    }

    #endregion

    /// <summary>
    /// Create "animation" of typing text of the given sentence
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeSentences(string sentence)
    {
        dialogueText.text = leadingCharBeforeDelay ? leadingChar : ""; //writes leading char

        yield return new WaitForSeconds(delayBeforeStart);
        audioSource.Play(); //plays sound

        foreach (char c in sentence) //writes char by char the sentence
        {
            if (dialogueText.text.Length > 0) //when dialog text is not empty
                dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
                // sets the dialog text as the dialog text except for the leading char
                //Basically, erases the leading char

            dialogueText.text += c; //add new char
            dialogueText.text += leadingChar; //add again leading char

            yield return new WaitForSeconds(timeBtwChars); //wait time
        }

        audioSource.Stop(); //after ended, stops sound

        if (leadingChar != "") //erases leading char if there was one
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
    }
}
