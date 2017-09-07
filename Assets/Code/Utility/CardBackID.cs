using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBackID : MonoBehaviour
{
	public void ChangeCardBackID()
    {
        Image currentImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        int listIndex = GetComponent<SetCardBacks>()._cardBackImages.FindIndex(a => a == currentImage);
        PlayerPrefs.SetInt("CardBackID", listIndex);
    }
}
