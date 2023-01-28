﻿namespace FirstServer;

public class WalletCommands
{
    private Wallet _wallet = new Wallet();
    
    public void InvestInGoldCoins(int investSilver)
    {
        //1 золото - 10 cеребра
        if (investSilver%10!=0)
        {
            int remainCoins = investSilver % 10;
            investSilver -= remainCoins;

            _wallet.SilverCoins += remainCoins;
            Console.WriteLine(remainCoins);
        }
       
        _wallet.SilverCoins -= investSilver;
        _wallet.GoldCoins += investSilver/10;

        Console.WriteLine($"Теперь у вас золотых монет - {_wallet.GoldCoins}, серебрянных - {_wallet.SilverCoins} ");
    }
    
    public void InvestInSilverCoins(int investGold)
    {   
        _wallet.SilverCoins += investGold * 10;
        _wallet.GoldCoins -= investGold;
        Console.WriteLine($"Теперь у вас золотых монет - {_wallet.GoldCoins}, серебрянных - {_wallet.SilverCoins} ");
    }
}