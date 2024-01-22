using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Deck template to create in the inspector 
/// </summary>
[CreateAssetMenu(fileName = "New Deck", menuName = "Deck Template")]
public class DeckTemplate : ScriptableObject
{
    [UDictionary.Split(80, 20)]
    public UDictionaryStringInt cardsList;

    /// <summary>
    /// Internal use dictionary (gets rid of generic data type)
    /// </summary>
    [Serializable]
    public class UDictionaryStringInt : UDictionary<string, int> { }

}
