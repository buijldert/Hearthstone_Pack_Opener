[System.Serializable]
public class Pack
{
    public enum Expansion { Classic, GoblinsVersusGnomes, TheGrandTournament, WhispersOfTheOldGods };
    public Expansion packExpansion;
    public int packCount;
}
