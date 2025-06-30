using System.Collections;
using System.Collections.Generic;


namespace Game
{
    public enum BetType
    {
        No_Bet,
        Lower,
        Higher
    }
    public enum GameState
    {
        Initialize,
        Start,
        Show_Dealer_Card,
        Await_Player_Bet,
        Show_Player_Card,
        Resolve_Bet_Result,
        Player_Win,
        Dealer_Win,
        Tie,
        Round_Ended,
        New_Round,
        Deck_Empty
    }

    public enum RuleType
    {
        Modify_Set,
        Modify_Value
    }

    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}
