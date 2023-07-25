using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public void StartInteraction()
    {
        DialogManager.GetInstance().EnterDialog(inkJSON);
    }
}
