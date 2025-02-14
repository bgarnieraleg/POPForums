﻿namespace PopForums.Test.ScoringGame;

public class UserAwardServiceTests
{
	public UserAwardService GetService()
	{
		_userAwardRepo = new Mock<IUserAwardRepository>();
		_notificationTunnel = new Mock<INotificationTunnel>();
		_tenantService = new Mock<ITenantService>();
		return new UserAwardService(_userAwardRepo.Object, _notificationTunnel.Object, _tenantService.Object);
	}

	private Mock<IUserAwardRepository> _userAwardRepo;
	private Mock<INotificationTunnel> _notificationTunnel;
	private Mock<ITenantService> _tenantService;

	[Fact]
	public async Task IssueMapsFieldsToRepoCall()
	{
		var user = new User { UserID = 123 };
		var awardDef = new AwardDefinition {AwardDefinitionID = "blah", Description = "desc", Title = "title", IsSingleTimeAward = true};
		var service = GetService();
		await service.IssueAward(user, awardDef);
		_userAwardRepo.Verify(x => x.IssueAward(user.UserID, awardDef.AwardDefinitionID, awardDef.Title, awardDef.Description, It.IsAny<DateTime>()), Times.Once());
	}

	[Fact]
	public async Task IsAwardedMapsAndReturnsRightValue()
	{
		var user = new User { UserID = 123 };
		var awardDef = new AwardDefinition { AwardDefinitionID = "blah" };
		var service = GetService();
		_userAwardRepo.Setup(x => x.IsAwarded(user.UserID, awardDef.AwardDefinitionID)).ReturnsAsync(true);
		var result = await service.IsAwarded(user, awardDef);
		Assert.True(result);
	}

	[Fact]
	public async Task GetAwardsMapsUserIDAndReturnsList()
	{
		var user = new User { UserID = 123 };
		var list = new List<UserAward>();
		var service = GetService();
		_userAwardRepo.Setup(x => x.GetAwards(user.UserID)).ReturnsAsync(list);
		var result = await service.GetAwards(user);
		Assert.Same(list, result);
	}
}