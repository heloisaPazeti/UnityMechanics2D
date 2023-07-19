using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example of a simple interaction that calls a Dialog
/// </summary>
public class SimpleInteraction : DialogTrigger
{
    public Button interactionButton;

    private void Start()
    {
        interactionButton.onClick.AddListener(OnStartInteraction);
    }

    protected virtual void OnStartInteraction()
    {
        //In other types of interaction something could happen here
        StartCoroutine(StartCutscene());
    }

    protected override void CutsceneEnded()
    {
        //In other types of interaction something could happen here
        base.CutsceneEnded();
    }
}
