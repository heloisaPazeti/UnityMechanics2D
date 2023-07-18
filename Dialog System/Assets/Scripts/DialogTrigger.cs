using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public DialogInsert[] inserts;

    [HideInInspector]
    public bool finishedDialog = true;

    private bool continueScene = false;
    protected int index = 0;

    public virtual IEnumerator StartCutscene()
    {
        while(index < inserts.Length)
        {
            finishedDialog = false;

            DialogInsert insert = inserts[index];
            Actor actor = insert.actor;
            Sprite avatar;

            if (actor.isBaseActor && insert.baseActorName != "")
                actor.ActorName = insert.baseActorName;

            avatar = actor.Avatar != null ? actor.Avatar : null;

            TriggerDialogue(actor);
            yield return new WaitUntil(() => continueScene);

            continueScene = false;
        }
        
        CutsceneEnded();
        yield return null;
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
    }

    #region "Trigger"

    public void TriggerDialogue(Actor actor)
    {
        FindObjectOfType<DialogManager>().StartDialogue(inserts[index], actor, this);
    }

    #endregion
}
