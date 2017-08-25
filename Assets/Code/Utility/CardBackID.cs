using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardBackID : MonoBehaviour
{
	public void ChangeCardBackID()
    {
        PlayerPrefs.SetString("CardBackID", EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name);
    }
}
