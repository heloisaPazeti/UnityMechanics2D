using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple scriptable object for the characters
/// </summary>
[CreateAssetMenu(fileName = "New Actor", menuName = "Actors")]
public class Actor : ScriptableObject
{
    //characters properties
    public string ActorName;
    public Sprite Avatar;
    public Color txtColor;

    //Base Actor is a basic characters, usually just with different name
    public bool isBaseActor;
}
