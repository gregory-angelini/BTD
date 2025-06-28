using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Game
{
    public static class CardUtils
    {
        public static IEnumerable<CardProfile> FilterBySuit(IEnumerable<CardProfile> cards, Suit suit)
        {
            return cards.Where(c => c.Suit == suit);
        }

        public static IEnumerable<CardProfile> FilterByRank(IEnumerable<CardProfile> cards, Rank rank)
        {
            return cards.Where(c => c.Rank == rank);
        }
    }
}
