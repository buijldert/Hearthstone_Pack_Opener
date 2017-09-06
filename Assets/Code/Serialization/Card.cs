    [System.Serializable]
	public class Card 
	{
        public enum Rarity { Common, Rare, Epic, Legendary, None};

        public string cardName;
        public Rarity cardRarity;
        public Pack.Expansion cardExpansion;
        public int cardCount;
        public int cardManaCost;
        public int cardCraftValue;
        public int cardDisenchantValue;
	}