using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the dialog block to be displayed 
/// </summary>
[System.Serializable]
public class DialogInsert : MonoBehaviour
{
    public Actor actor;
    public string baseActorName;

    [TextArea(3, 10)]
    public string sentencesEng;

    [TextArea(3, 10)]
    public string sentencesPt;
}
