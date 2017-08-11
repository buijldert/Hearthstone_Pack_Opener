    [System.Serializable]
	public class Card 
	{
        public enum Rarity { Common, Rare, Epic, Legendary};

        public string cardName;
        public Rarity cardRarity;
        public int cardCount;
	}