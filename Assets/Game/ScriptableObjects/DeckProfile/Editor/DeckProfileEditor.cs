using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [CustomEditor(typeof(DeckProfile))]
    public class DeckProfileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DeckProfile holder = (DeckProfile)target;

            ShowSortButton(holder);
        }

        void ShowSortButton(DeckProfile holder)
        {
            GUIContent sortButton = new GUIContent("Sort", "Cards by Suit");
            if (GUILayout.Button(sortButton))
            {
                Sort(holder);
            }
        }

        void Sort(DeckProfile holder)
        {
            SortCards(holder);
        }

        void SortCards(DeckProfile holder)
        {
            holder.Cards = holder.Cards
                .OrderBy(card => card.Suit)
                .ThenBy(card => card.Rank)
                .ToArray();
        }
    }
}