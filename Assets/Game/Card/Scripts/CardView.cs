using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] Image cardImage;
        [SerializeField] CanvasGroup canvasGroup;


        public void SetSprite(Sprite faceSprite)
        {
            cardImage.sprite = faceSprite;
            cardImage.SetNativeSize();
        }
    }
}
