using System.Text.RegularExpressions;
using HtmlAgilityPack;
using StudentTransfer.Dal.Entities.Vacant;

namespace StudentTransfer.VacantParser;

public static class VacantListParser
{
    public static async Task<List<EducationDirection>> ParseVacantItemsAsync()
    {
        const string url = "https://urfu.ru/sveden/vacant/";
        var htmlPage = "";
        
        using (var client = new HttpClient())
        {
            using (var response = await client.GetAsync(url))
            {
                using (var content = response.Content)
                {
                    htmlPage = await content.ReadAsStringAsync();
                }
            }   
        }
        
        var vacantList = ParseHtml(htmlPage);
        return vacantList;
    }

    private static List<EducationDirection> ParseHtml(string html)
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
                
                return new EducationDirection
                {
                    Code = td[0].InnerText,
                    Name = cleanedName,
                    Level = EducationLevelConverter.ConvertToLevel(td[2].InnerText),
                    Course = Convert.ToInt32(td[3].InnerText),
                    Form = EducationFormConverter.ConvertToForm(td[4].InnerText),
                    FederalBudgets = Convert.ToInt32(td[5].InnerText),
                    SubjectsBudgets = Convert.ToInt32(td[6].InnerText),
                    LocalBudgets = Convert.ToInt32(td[7].InnerText),
                    Contracts = Convert.ToInt32(td[8].InnerText)
                };
            }).ToList();
    } 
}