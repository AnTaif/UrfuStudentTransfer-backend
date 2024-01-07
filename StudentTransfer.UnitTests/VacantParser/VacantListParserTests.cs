using StudentTransfer.VacantParser;

namespace StudentTransfer.UnitTests.VacantParser;

public class VacantListParserTests
{
    [Fact]
    public void ParseHtml_ShouldReturnListOfVacantDirections()
    {
        // Arrange
        const string html = "<div class='table_scroll_450'><tr itemprop='vacant'><td>09.03.03</td><td>Название</td><td>высшее образование - бакалавриат</td><td>1</td><td>очная</td><td>0</td><td>0</td><td>0</td><td>0</td></tr></div>";

        // Act
        var result = VacantListParser.ParseHtml(html);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ParseHtml_ShouldHandleInvalidHtmlGracefully()
    {
        // Arrange
        const string invalidHtml = "Invalid HTML content";

        // Act && Assert
        Assert.Throws<InvalidOperationException>(() => VacantListParser.ParseHtml(invalidHtml));
    }
}