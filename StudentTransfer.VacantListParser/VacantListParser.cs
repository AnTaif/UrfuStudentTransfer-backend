using HtmlAgilityPack;
using StudentTransfer.Dal.Models.Vacant;

namespace StudentTransfer.VacantListParser;

public static class VacantListParser
{
    public static async Task<IEnumerable<EducationDirection>> ParseVacantItemsAsync()
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

    private static IEnumerable<EducationDirection> ParseHtml(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var table = doc.DocumentNode.Descendants("div").First(e => e.HasClass("table_scroll_450"));

        return table.Descendants("tr")
            .Where(e => e.GetAttributeValue("itemprop", "") == "vacant")
            .Select(row => row.Descendants("td").ToList())
            .Select(td => new EducationDirection
            {
                Code = td[0].InnerText,
                Name = td[1].InnerText,
                Level = EducationLevelConverter.ConvertToLevel(td[2].InnerText),
                Course = Convert.ToInt32(td[3].InnerText),
                Form = EducationFormConverter.ConvertToForm(td[4].InnerText),
                FederalBudgets = Convert.ToInt32(td[5].InnerText),
                SubjectsBudgets = Convert.ToInt32(td[6].InnerText),
                LocalBudgets = Convert.ToInt32(td[7].InnerText),
                Contracts = Convert.ToInt32(td[8].InnerText)
            });
    }
}