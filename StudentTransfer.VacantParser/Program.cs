using StudentTransfer.VacantParser;

var vacantList = await VacantListParser.ParseVacantItemsAsync();

foreach (var item in vacantList!)
{
    Console.WriteLine($"{item.Code} {item.Name} {item.Course}");
}

Console.WriteLine($"\nVacantList Count: {vacantList.Count}\n");