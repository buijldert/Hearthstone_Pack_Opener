    [System.Serializable]
	public class Card 
	{
        public enum Rarity { Common, Rare, Epic, Legendary};

        public string cardName;
        public Rarity cardRarity;
        public int cardCount;
        public int cardManaCost;
        public int cardCraftValue;
        public int cardDisenchantValue;
	}