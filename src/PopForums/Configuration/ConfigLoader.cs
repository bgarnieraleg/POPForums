﻿using System.Diagnostics;

namespace PopForums.Configuration;

public class ConfigLoader
{
	public ConfigContainer GetConfig(IConfiguration configuration)
	{
		var container = new ConfigContainer();
		container.DatabaseConnectionString = configuration["PopForums:Database:ConnectionString"];
		Debug.Write("Here");
		Debug.Write(container.DatabaseConnectionString);
		Console.WriteLine("Console....");
		Console.WriteLine(container.DatabaseConnectionString);
		var cacheSeconds = configuration["PopForums:Cache:Seconds"];
		container.CacheSeconds = cacheSeconds == null ? 90 : Convert.ToInt32(cacheSeconds);
		container.CacheConnectionString = configuration["PopForums:Cache:ConnectionString"];
		container.CacheForceLocalOnly = Convert.ToBoolean(configuration["PopForums:Cache:ForceLocalOnly"]);
		container.SearchUrl = configuration["PopForums:Search:Url"];
		container.SearchKey = configuration["PopForums:Search:Key"];
		var searchProvider = configuration["PopForums:Search:Provider"];
		container.SearchProvider = searchProvider ?? string.Empty;
		container.QueueConnectionString = configuration["PopForums:Queue:ConnectionString"];
		var logTopicViews = configuration["PopForums:LogTopicViews"];
		container.LogTopicViews = logTopicViews != null && bool.Parse(logTopicViews);
		var useReCaptcha = configuration["PopForums:ReCaptcha:UseReCaptcha"];
		container.UseReCaptcha = useReCaptcha != null && bool.Parse(useReCaptcha);
		container.ReCaptchaSiteKey = configuration["PopForums:ReCaptcha:SiteKey"];
		container.ReCaptchaSecretKey = configuration["PopForums:ReCaptcha:SecretKey"];
		container.IpLookupUrlFormat = configuration["PopForums:IpLookupUrlFormat"];
		container.WebAppUrlAndArea = configuration["PopForums:WebAppUrlAndArea"];
		container.BaseImageBlobUrl = configuration["PopForums:BaseImageBlobUrl"];
		container.StorageConnectionString = configuration["PopForums:Storage:ConnectionString"];
		var renderBootstrap = configuration["PopForums:RenderBootstrap"];
		container.RenderBootstrap = renderBootstrap != null ? bool.Parse(renderBootstrap) : true;
		return container;
	} 
}