// See https://aka.ms/new-console-template for more information
using Spendee.Database;

Console.WriteLine("Hello, World!");

var wallet = new WalletRepository("Server=127.0.0.1;Database=Spendee;Uid=utente1;Pwd=test2910;");
var categories = new CategoryRepository("Server=127.0.0.1;Database=Spendee;Uid=utente1;Pwd=test2910;");

foreach (var item in await wallet.GetTransactionsByWalletIdAsync(1))
    Console.WriteLine(item);

foreach (var item in await wallet.GetAllWalletsAsync())
    Console.WriteLine(item);

foreach (var item in await categories.GetAllCategoriesAsync())
    Console.WriteLine(item);

Console.ReadKey();