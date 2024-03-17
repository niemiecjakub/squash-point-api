using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashPointAPI.Controllers;
using SquashPointAPI.Dto.League;
using SquashPointAPI.Interfaces;
using SquashPointAPI.Models;

namespace SquashPointApi.test;

public class LeagueControllerTest
{
    private readonly Mock<ILeagueRepository> _mockLeagueRepository;
    private readonly Mock<UserManager<Player>> _mockUserManager;
    private readonly LeagueController _controller;

    public LeagueControllerTest()
    {
        _mockLeagueRepository = new Mock<ILeagueRepository>();
        _mockUserManager =
            new Mock<UserManager<Player>>(MockBehavior.Strict);
        _controller = new LeagueController(_mockLeagueRepository.Object, _mockUserManager.Object);
    }

    [Fact]
    public async Task GetLeagues_ReturnsOkResult()
    {
        // Arrange
        var leagues = new List<League>();
        _mockLeagueRepository.Setup(repo => repo.GetLeaguesAsync()).ReturnsAsync(leagues);

        // Act
        var result = await _controller.GetLeagues();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<LeagueDto>>(okResult.Value);
        Assert.Empty(model);
    }
}