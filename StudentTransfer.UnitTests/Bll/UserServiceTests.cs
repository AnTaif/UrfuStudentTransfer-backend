using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using StudentTransfer.Bll.Services.Auth.JwtToken;
using StudentTransfer.Bll.Services.Auth.User;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.User;

namespace StudentTransfer.UnitTests.Bll;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserInfoAsync_ValidUserId_ReturnsUserInfo()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var user = new AppUser
        {
            Id = Guid.Parse(userId),
            UserName = "UserName",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "example@example.com",
            PhoneNumber = "123456789",
            Telegram = "@telegram"
        };

        var mockUserManager = MockManager.GetMockUserManager();
        mockUserManager.SetupUsers(new List<AppUser>{user});

        var userService = new UserService(mockUserManager.Object, null, null, null, null);

        // Act
        var userInfo = await userService.GetUserInfoAsync(userId);

        // Assert
        Assert.NotNull(userInfo);
        Assert.Equal(user.Id, userInfo.Id);
        Assert.Equal(user.Email, userInfo.Email);
        Assert.Equal($"{user.LastName} {user.FirstName}", userInfo.FullName);
        Assert.Equal(user.PhoneNumber, userInfo.PhoneNumber);
        Assert.Equal(user.Telegram, userInfo.Telegram);
    }

    [Fact]
    public async Task RegisterUserAsync_NewUser_ReturnsRegistrationResponse()
    {
        // Arrange
        var registrationRequest = new RegistrationRequest
        {
            Email = "newuser@example.com",
            FirstName = "New",
            LastName = "User",
            Password = "password123"
        };
        
        var testUser = new AppUser
        {
            UserName = registrationRequest.Email,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            MiddleName = registrationRequest.MiddleName,
            Email = registrationRequest.Email,
        };

        var mockUserManager = MockManager.GetMockUserManager();
        mockUserManager
            .Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .ReturnsAsync((AppUser _, string _) =>
            {
                mockUserManager.SetupUsers(new List<AppUser> { testUser });
                return IdentityResult.Success;
            });

        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        jwtTokenGeneratorMock
            .Setup(x => x.GenerateTokenAsync(It.IsAny<AppUser>()))
            .ReturnsAsync("token");

        var userService = new UserService(mockUserManager.Object, null, null, jwtTokenGeneratorMock.Object, null);

        // Act
        var registrationResponse = await userService.RegisterUserAsync(registrationRequest);

        // Assert
        Assert.NotNull(registrationResponse);
        Assert.NotEmpty(registrationResponse.Token);
        Assert.Equal(registrationRequest.Email, registrationResponse.Email);
    }

    [Fact]
    public async Task SignInAsync_ValidCredentials_ReturnsLoginResponse()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "example@example.com",
            Password = "password123"
        };

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = "Username",
            FirstName = "FirstName",
            LastName = "LastName",
            Email = loginRequest.Email
        };

        var mockUserManager = MockManager.GetMockUserManager();
        mockUserManager.SetupUsers(new List<AppUser>{user});

        var signInManagerMock = new Mock<SignInManager<AppUser>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(), null, null, null, null);
        signInManagerMock
            .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        jwtTokenGeneratorMock
            .Setup(x => x.GenerateTokenAsync(It.IsAny<AppUser>()))
            .ReturnsAsync("token");

        var userService = new UserService(mockUserManager.Object, signInManagerMock.Object, null, jwtTokenGeneratorMock.Object, null);

        // Act
        var loginResponse = await userService.SignInAsync(loginRequest);

        // Assert
        Assert.NotNull(loginResponse);
        Assert.NotEmpty(loginResponse.Token);
    }
}