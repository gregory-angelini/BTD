using System.Collections;
using System.Collections.Generic;


namespace Game
{
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }


        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}