﻿namespace PopForums.Test.Email;

public class NewAccountMailerTests
{
	[Fact]
	public void SendCallsSmtpWrapper()
	{
		var wrapper = new Mock<ISmtpWrapper>();
		var resultMessage = new EmailMessage();
		wrapper.Setup(w => w.Send(It.IsAny<EmailMessage>()))
			.Returns(SmtpStatusCode.Ok)
			.Callback<EmailMessage>(msg => resultMessage = msg);
		const string mailerAddress = "a@b.com";
		const string forumTitle = "superawesome";
		var user = UserTest.GetTestUser();
		var settings = new Settings { MailerAddress = mailerAddress, ForumTitle = forumTitle};
		var settingsManager = new Mock<ISettingsManager>();
		settingsManager.Setup(s => s.Current).Returns(settings);
		var mailer = new NewAccountMailer(settingsManager.Object, wrapper.Object);

		var result = mailer.Send(user, "http://blah/");

		Assert.Equal(SmtpStatusCode.Ok, result);
		Assert.Equal(resultMessage.ToName, user.Name);
		Assert.Equal(resultMessage.ToEmail, user.Email);
		Assert.Equal(forumTitle, resultMessage.FromName);
	}
}