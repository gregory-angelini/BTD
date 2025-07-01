using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public partial class BetButton : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] BetType betType;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color selectedColor = Color.green;
        [SerializeField] Image background;
        bool isClicked = false;
        

        void ResetButton()
        {
            isClicked = false;
            background.color = defaultColor;
        }
       
        void OnBetPlaced(BetType selectedBet)
        {
            background.color = selectedBet == betType ? selectedColor : defaultColor;
        }
    }
}
