using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSets : MonoBehaviour
{
    [System.Serializable]
    public class CardSetGroups
    {
        public List<CardSetGroup> cardSetGroups;
    }

    [System.Serializable]
    public class CardSetGroup
    {
        public List<CardSet> cardSets;
    }

    [System.Serializable]
    public class CardSet
    {
        public List<Sprite> cards;
    }

    public CardSetGroups cardSetGroups;


}
