using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Randomiser
{
	// randomise a list of cards
    public static List<Card> RandomiseCards(List<Card> cards)
	{
		var newCards = new List<Card>();
		var numList = new List<int>();
		int i = 0;

		while (i < cards.Count)
		{
			numList.Add(i++);
		}

		while (numList.Count > 0)
		{
			int index = Random.Range(0, numList.Count);
			int rand = numList[index];

			numList.RemoveAt(index);
			newCards.Add(cards[rand]);
		}

		return newCards;
	}
}
