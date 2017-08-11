using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour 
{
    [SerializeField]
    private Card card;
    [SerializeField]
    private Image cardImage;

	public void Save()
    {
        Serializer.Save<Card>("carddata.sav", card);
    }

    public void Load(Text textField)
    {
        //Card loadedCard = Serializer.Load<Card>("carddata.sav");
        //textField.text = "Name: " + loadedCard.cardName +  
        //    "\nHealth: " + loadedCard.health
        //    + "\nAttack: " + loadedCard.attack + 
        //    "\nRarity: " + loadedCard.currentCardRarity;
        //cardImage.sprite = Resources.Load("Cards/" + loadedCard.cardName, typeof(Sprite)) as Sprite;
    }
}