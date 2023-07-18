using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public DialogInsert[] inserts;
    public char splittingChar = '|';

    [HideInInspector]
    public bool finishedDialog = true;

    private bool continueScene = false;
    protected int index = 0;
    private string sentence;

    //protected UIManager uiManager;

    protected virtual void Start()
    {
        //uiManager = FindObjectOfType<UIManager>();
    }

    public virtual IEnumerator StartCutscene()
    {
        if (index < inserts.Length)
        {
            finishedDialog = false;

            CheckLanguage();

            //if (uiManager != null)
                //uiManager.ChangeMainGUI(false);

            DialogInsert insert = inserts[index];
            Actor actor = insert.actor;
            Sprite avatar;

            if (actor.isBaseActor && insert.baseActorName != "")
            {
                actor.ActorName = insert.baseActorName;
            }

            avatar = actor.Avatar != null ? actor.Avatar : null;

            string[] insertSentences = sentence.Split(splittingChar);

            TriggerDialogue(insertSentences, actor);

            yield return new WaitUntil(() => continueScene);

            continueScene = false;
            StartCoroutine(StartCutscene());
        }
        else
        {
            CutsceneEnded();
            yield return null;
        }
    }

    public virtual void EndDialogue()
    {
        continueScene = true;
        index++;
    }

    protected virtual void CutsceneEnded()
    {
        finishedDialog = true;
        index = 0;
        //uiManager.SetJoystickMenuButtons(true);
    }

    #region "Trigger"

    public void TriggerDialogue(string[] sentences, Actor actor)
    {
        FindObjectOfType<DialogManager>().StartDialogue(sentences, actor, this);
    }

    protected void CheckLanguage()
    {
        /*
        switch (GameManager.instance.GetLanguage())
        {
            case 0:
                sentence = inserts[index].sentencesEng;
                break;

            case 1:
                sentence = inserts[index].sentencesPt;
                break;
        }
        */
    }

    #endregion
}
