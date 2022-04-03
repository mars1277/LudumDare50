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

        public int GetBeastFaceId()
        {
            return Random.Range(0, cards.Count);
        }
    }
    public static int previousCardSetId;
    public CardSetGroups cardSetGroups;

    public CardSet GetRandomCardSetInRandomizedOrder(int roundDifficulty)
    {
        if (roundDifficulty > cardSetGroups.cardSetGroups.Count - 1)
        {
            roundDifficulty = cardSetGroups.cardSetGroups.Count - 1;
        }
        CardSetGroup cardSetGroup = cardSetGroups.cardSetGroups[roundDifficulty];
        int randomCardSetId = Random.Range(0, cardSetGroup.cardSets.Count);
        while (previousCardSetId == randomCardSetId)
        {
            randomCardSetId = Random.Range(0, cardSetGroup.cardSets.Count);
        }
        CardSet cardSet = cardSetGroup.cardSets[randomCardSetId];
        for (int i = 0; i < cardSet.cards.Count; i++)
        {
            Sprite temp = cardSet.cards[i];
            int randomIndex = Random.Range(i, cardSet.cards.Count);
            cardSet.cards[i] = cardSet.cards[randomIndex];
            cardSet.cards[randomIndex] = temp;
        }
        previousCardSetId = randomCardSetId;
        Debug.Log("kecske " + roundDifficulty + " - " + randomCardSetId);
        return cardSet;
    }


}
