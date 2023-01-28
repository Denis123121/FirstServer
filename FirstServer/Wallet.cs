namespace FirstServer;

public class Wallet
{
    public int GoldCoins { get; set; }
    public int SilverCoins { get; set; }
    
    public void InvestInGoldCoins(int investSilver)
    {
        //1 золото - 10 cеребра
        if (investSilver%10!=0)
        {
            int remainCoins = investSilver % 10;
            investSilver -= remainCoins;

            SilverCoins += remainCoins;
            Console.WriteLine(remainCoins);
        }
       
        SilverCoins -= investSilver;
        GoldCoins += investSilver/10;

        Console.WriteLine($"Теперь у вас золотых монет - {GoldCoins}, серебрянных - {SilverCoins} ");
    }
    
    public void InvestInSilverCoins(int investGold)
    {   
        SilverCoins += investGold * 10;
        GoldCoins -= investGold;
        Console.WriteLine($"Теперь у вас золотых монет - {GoldCoins}, серебрянных - {SilverCoins} ");
    }
}