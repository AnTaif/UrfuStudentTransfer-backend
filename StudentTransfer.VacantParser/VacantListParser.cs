using System.Text.RegularExpressions;
using HtmlAgilityPack;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Dal.Enums;

namespace StudentTransfer.VacantParser;

public static class VacantListParser
{
    private const string VacantUrl = "https://urfu.ru/sveden/vacant/";
    
    public static async Task<List<VacantDirection>?> ParseVacantItemsAsync()
    {
        try
        {
            var htmlPage = await DownloadHtmlAsync();
            var vacantList = ParseHtml(htmlPage);

            return vacantList;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"VacantListParser Error: {ex.Message}");
            return null;
        }
    }
    
    private static async Task<string> DownloadHtmlAsync()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(VacantUrl);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    private static List<VacantDirection> ParseHtml(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var table = doc.DocumentNode.Descendants("div").First(e => e.HasClass("table_scroll_450"));

        return table.Descendants("tr")
            .Where(e => e.GetAttributeValue("itemprop", "") == "vacant")
            .Select(row => row.Descendants("td").ToList())
            .Select(td =>
            {
                var nameInput = td[1].InnerText.Replace("\n", " ");
                var cleanedName = Regex.Replace(nameInput, @"\s+", " ");
                
                return new VacantDirection
                {
                    Code = td[0].InnerText,
                    Name = cleanedName,
                    Level = td[2].InnerText.ConvertToLevel(),
                    Course = Convert.ToInt32(td[3].InnerText),
                    Form = td[4].InnerText.ConvertToForm(),
                    FederalBudgets = Convert.ToInt32(td[5].InnerText),
                    SubjectsBudgets = Convert.ToInt32(td[6].InnerText),
                    LocalBudgets = Convert.ToInt32(td[7].InnerText),
                    Contracts = Convert.ToInt32(td[8].InnerText)
                };
            }).ToList();
    } 
}