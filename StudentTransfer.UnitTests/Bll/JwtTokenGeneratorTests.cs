using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using StudentTransfer.Bll.Services.Auth.JwtToken;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;
using static StudentTransfer.UnitTests.MockDataGenerator;

namespace StudentTransfer.UnitTests.Bll;

public class JwtTokenGeneratorTests
{
    [Fact]
    public async Task GenerateTokenAsync_ValidUser_ReturnsToken()
    {
        // Arrange
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.GenerateAndSetupUsers(1).Single();

        var jwtOptions = Options.Create(new JwtOptions
        {
            Issuer = "testIssuer",
            Audience = "testAudience",
            Secret = "testandlongSecretKey",
            ExpiryMinutes = 30
        });

        var tokenGenerator = new JwtTokenGenerator(jwtOptions, mockUserManager.Object);

        // Act
        var token = await tokenGenerator.GenerateTokenAsync(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public async Task GenerateTokenAsync_NullEmail_ThrowsNullReferenceException()
    {
        // Arrange
        var mockUserManager = MockManager.GetMockUserManager();
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = "Username",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = null,
        };
        mockUserManager.SetupUsers(new List<AppUser>{user});

        var jwtOptions = Options.Create(new JwtOptions
        {
            Issuer = "testIssuer",
            Audience = "testAudience",
            Secret = "testandlongSecretKey",
            ExpiryMinutes = 30
        });

        var tokenGenerator = new JwtTokenGenerator(jwtOptions, mockUserManager.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => tokenGenerator.GenerateTokenAsync(user));
    }
}