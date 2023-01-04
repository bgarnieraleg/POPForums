![POP Forums logo](https://avatars2.githubusercontent.com/u/8217691?s=200&v=4)

POP Forums
=========

A forum and Q&A application with real-time updating, image uploading and private message chat in multiple languages.

The main branch is now the work-in-progress for future versions running on .NET 6+. The v19.x branch is v19.x, running on .NET 6. If you're looking for the version that works on .NET Framework 4.5+ with MVC 5, check out v13.0.2.

Roadmap:
The v19 release concentrates on long awaited new feature requests, updates to the user interface and minor bug fixes.

For the latest information and documentation, and how to get started, check the pages (also in markdown in /docs of source):  
https://popworldmedia.github.io/POPForums/

Try it out and make test posts here:  
https://meta.popforums.com/Forums

CI build of main runs here:  
https://popforumsdev.azurewebsites.net/Forums

[![Build status](https://dev.azure.com/popw/POP%20Forums/_apis/build/status/popforumsdev)](https://dev.azure.com/popw/POP%20Forums/_build/latest?definitionId=13)

Latest release:  
https://github.com/POPWorldMedia/POPForums/releases/tag/v18.0.0  
Packages available on NuGet and npm.

The latest CI build packages can be found with these feeds on MyGet:  
https://www.myget.org/F/popforums/api/v3/index.json   
https://www.myget.org/F/popforums/npm/  

## Prerequisites:
* .NET v6.
* npm and Node.js to build the front-end.
* AzureKit optionally requires Redis for two-level cache, Azure Search for Search.
* AzureKit optionally requires an Azure Storage account for queues and Azure Functions.
* ElasticKit optionally requires ElasticSearch for search.
* Works great on Windows and Linux.

  Pop Forums AAPM Integration Research 

*   [Pop Forums/AAMP Integration Research](#pop-forumsaamp-integration-research)
*   [Features](#features)
*   [Setup & Configurations](#setup--configurations)
    *   [Build](#build)
    *   [Reference](#reference)
*   [Database Setup](#database-setup)
*   [Customization](#customization)


Pop Forums/AAMP Integration Research
====================================

Documentation link: [https://popworldmedia.github.io/POPForums/](https://popworldmedia.github.io/POPForums/)  
Github Repository: [https://github.com/POPWorldMedia/POPForums](https://github.com/POPWorldMedia/POPForums)  
Jira Research Ticket: [https://teamallegiance.atlassian.net/browse/AAPM-57](https://teamallegiance.atlassian.net/browse/AAPM-57)

Features
========

Most of the features described on [AAPM-57](https://teamallegiance.atlassian.net/browse/AAPM-57) are covered on PopForums.

The structure overall complies with _Category > Forum > Topics/Threads > Posts_. On PopForums a Category is created as a Parent of a Forum and inside a Forum, multiple Topics and Posts can be hosted.

**Thread Specific Features:**

*    Pin Thread to top of the list
*    Show Num Replies
*    List item
*    Show Last Post Date
*    Close thread

**Post Specific Features:**

*    Quote Post
*    Attach to Post
*    Flag Post as Bad _(didn’t find an specific feature to flag a post)_

**Search Forums**

_Note: Search capability exist but it’s needed to provision additional infrastructure to enable it, it might require to Azure Functions or Elastic Search setup, which is already present on the PopForums base code_

**Permissions**

*    Any logged in user can create a Topic
*    Any logged in user can post to Topic
*    Any logged in user can edit/delete their post

Moderation

*    Only Administrator can create Category, Forum
*    Administrator can CRUD anything in forum
*    Notified about flagged posts ()\*(There are a section called Event Definitions and Scoring Game where notifications can be created, more research is needed on this side. Also, notifications exist, but it’s needed to configure an SMPT server, and \*

Setup & Configurations
======================

As per the documentation, we have two different approaches to providing a Pop Forums instance:

Build
-----

We need to basically git clone the whole Pop Forums project and host it either in our existing .NET Core project or a new one. According to what I found, this is most likely the one to go for, since we have complete control over any customization needed at view levels.

*   Configurations: Most of the project already comes with all the configurations, except for the database configuration (connection string), which is easily configurable at the appSettings.json files

Reference
---------

*   Two NuGet packages are added to our .NET Core project and the whole MVC PopForums project (Controllers, Areas, Styles, etc) will be available. For more information about a Reference package setup, there is a Sample project on: [https://github.com/POPWorldMedia/POPForums.Sample](https://github.com/POPWorldMedia/POPForums.Sample)


Running the `gulpfile`
==============

There are some additional steps to generate the frontend bundle for PopForums to pick up the `bootstrap` and `styles` code:

1. Using your terminal, navigate to the `/src/AAPM.PopForums/src/PopForums.Mvc`
2. Install the gulp cli globally with `npm install --global gulp-cli`
3. Execute `npm install` under the `PopForums.Mvc` directory
4. Execute `gulp default` to run the `gulpfile`


Database Setup
==============

These DB setup steps work for both options mentioned before (build or reference):

1.  Configure the ConnectionString on the `appSettings.json` file on your  
    project.
	* The database needs to be created prior to running the application.
	* SQL Credentials need to be created prior to running the application which will be used on the connection string.
	* The connection string should be formatted like: 
	`server=127.0.0.1,5433;Database=[dbname];User Id=[userId];Password=[pass];TrustServerCertificate=True;`

2.  Run the project
3.  Navigate to `/Forums/Setup`
4.  Fill in the required inputs to create the database
5.  Depending on the version of the package/build, after the database was set up you might need to run a migration script to upgrade your DB to the corresponding version. For instance: `~\src\PopForums.Sql\PopForums16to19.sql` to upgrade to version 19 (build version). At the time of writing this, the latest official published version of the NuGet packages is v18.

Customization
=============

A decision needs to be made for choosing a Reference or Build approach for providing a PopForums Instance depending on the required level of customization.

*   Package: If we go for a package reference setup, the customizations are limited based on the Forums Adapter ([see here](https://popworldmedia.github.io/POPForums/customization.html)) and manually overriding CSS styles. Footers and Headers
    
*   Build: Here we have more control over the views to customize, for instance, the Headers and Footer of the application (see `~\POPForums\src\PopForums.Mvc\Areas\Forums\Views\Shared\PopForumsMaster.cshtml` from the PopForums base code).