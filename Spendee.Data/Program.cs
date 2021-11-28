// See https://aka.ms/new-console-template for more information
using Spendee.Database;

Console.WriteLine("Hello, World!");


var wallet = new WalletRepository("Server=127.0.0.1;Database=Spendee;Uid=utente1;Pwd=test2910;");

var res = await wallet.GetTransactionsAsync(1);

Console.ReadKey();