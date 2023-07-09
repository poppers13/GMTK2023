using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // store cards
    [SerializeField] private List<Card> _drawPile;
    private List<Card> _discardPile;
    private int _cursorIndex = 0;

    [SerializeField] private InventoryManager _inv;

    // values for the size of the deck window, and how many cards there are
    // both these values assume that the deck's origin is in the *bottom-right*
    [SerializeField] private float _width = 6;
    [SerializeField] private float _height = 2;
    [SerializeField] private int _cardsToShow = 8;

    // how many cards can be selected for shuffling at once
    private int shuffleNum = 3;

    // store the list of given cards in the draw pile
    // NOTE: this method assumes the card order has already been randomised
    public void InitialiseCards(List<Card> cards)
	{
        _drawPile = new List<Card>();

        for (int i = 0; i < cards.Count; i++)
		{
            var c = cards[i];
            _drawPile.Add(c); // to ensure a deep copy
            SetCardPos(i); // move card to its position
		}

        _discardPile = new List<Card>();

        UpdateVisibility();
	}

    // all cards selected for shuffling
    private List<int> selectedCards
	{
        get 
        {
            var startIndex = _cursorIndex;
            var indexes = new List<int>();
            var maxNum = Mathf.Min(_cardsToShow, _drawPile.Count);

            // create a list of the 3 indexes selected
            for (var i = 0; i < shuffleNum; i++)
            {
                var newIndex = (startIndex + i) % maxNum;
                indexes.Add(newIndex);
            }

            return indexes;
        }
	}

    private void PlaceCursorInBounds()
	{
        var maxNum = Mathf.Min(_cardsToShow, _drawPile.Count);

        if (_cursorIndex < 0)
        {
            _cursorIndex += maxNum;
        }
        if (_cursorIndex >= maxNum)
		{
            _cursorIndex -= maxNum;
		}
    }

    public void SetCardPos(int index)
	{
        var c = _drawPile[index];
        var xd = _width / _cardsToShow; // x distance between each card

        var newx = this.transform.position.x - _width + (xd * index);
        var newy = this.transform.position.y + (_height / 2);

        c.SetNewPos(new Vector3(newx, newy, index));
	}

    public void Shuffle(int startIndex)
	{
        var replacements = new Dictionary<int, Card>();
        var indexes = selectedCards;

        // find the 3 cards involved in this set
        for (var a = 0; a < indexes.Count; a++)
		{
            var oldIndex = (indexes[a]);
            var newIndex = indexes[(a + 1) % indexes.Count];
            replacements.Add(newIndex, _drawPile[oldIndex]);
		}

        // re-arrange the cards in the draw pile
        foreach (KeyValuePair<int, Card> entry in replacements)
        {
            _drawPile[entry.Key] = entry.Value;
        }

        // update card positions
        foreach (int i in replacements.Keys)
		{
            SetCardPos(i);
		}
	}

    // play the top card of the draw pile, then remove it from the draw pile and add it to the discard
    public void PlayTopCard(BattleBoard board, int currentRow)
	{
        var c = _drawPile[0];
        c.Play(board, currentRow);
        _discardPile.Add(c);
        _drawPile.Remove(c);

        // move all cards
        for (var i = 0; i < _drawPile.Count; i++)
		{
            SetCardPos(i);
		}
	}

    public void ResetDeck()
	{
        // dunno if this is actually necessary, but i ain't taking any fuckin chances
        foreach (var c in _discardPile)
		{
            _drawPile.Add(c);
		}

        _discardPile.Clear(); // remove all references in discard pile (i.e. empty it)

        _drawPile = Randomiser.RandomiseCards(_drawPile); // shuffle deck

        // move all cards
        for (var i = 0; i < _drawPile.Count; i++)
        {
            SetCardPos(i);
        }

        UpdateVisibility();
	}

    public void UpdateVisibility()
	{
        var selectIndexes = selectedCards; // retrieve at start, so it's consistent throughout
        var maxNum = Mathf.Min(_cardsToShow, _drawPile.Count); // max number of available cards

        // make all selected cards fully visible
        foreach (var i in selectIndexes)
		{
            _drawPile[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        // make all cards in the visible part of the draw pile partially dim
        for (var i = 0; i < maxNum; i++)
        {
            if (selectIndexes.Contains(i))
			{
                continue; // since this card is selected, don't change its color
			}
            _drawPile[i].GetComponent<SpriteRenderer>().color = Color.grey;
        }

        // make all discarded cards invisible
        foreach (var c in _discardPile)
        {
            c.GetComponent<SpriteRenderer>().color = Color.clear;
        }

        // make all cards at end of draw pile invisible
        for (var i = maxNum; i < _drawPile.Count; i++)
		{
            _drawPile[i].GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }

    // retrieve input for moving/shuffling cards
    void Update()
    {
        // if draw pile is < hand size, re-shuffle
        if (_drawPile.Count < 6)
		{
            ResetDeck();
		}

        // moving cursor
        if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
            _cursorIndex--;
            PlaceCursorInBounds();
            print("New cursor index is " + _cursorIndex);
            UpdateVisibility();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _cursorIndex++;
            PlaceCursorInBounds();
            print("New cursor index is " + _cursorIndex);
            UpdateVisibility();
        }

        // shuffle cards
        if (Input.GetKeyDown(KeyCode.Space))
		{
            Shuffle(_cursorIndex);
		}

        // TEST: if press P, play the top card
        if (Input.GetKeyDown(KeyCode.P))
		{
            PlayTopCard(new BattleBoard(), 0);

            _cursorIndex--;
            PlaceCursorInBounds();
            print("New cursor index is " + _cursorIndex);
            UpdateVisibility();
        }
    }
}
