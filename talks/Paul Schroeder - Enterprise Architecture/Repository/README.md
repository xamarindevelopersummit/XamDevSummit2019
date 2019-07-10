ConferenceMate
===============

[![Build Status](https://msctek.visualstudio.com/ConferenceMate/_apis/build/status/ConferenceMate?branchName=master)](https://msctek.visualstudio.com/ConferenceMate/_build/latest?definitionId=1&branchName=master)

ConferenceMate is an open-source, cross-platform Xamarin application that demonstrates a mix of new technologies combined with programming patterns commonly used in enterprise line-of-business (LOB) applications:

- Xamarin.Forms Shell
- Azure BLOB Storage
- JWT Security
- SQLite for client-side persistent storage
- MVVMLight for INotifyPropertyChanged (property setter/getter)
- Developer User Secrets for DB connection strings and Azure Key Storage
- Logging (App Insights client-side / log4net server-side)
- Use of a robust RESTful Web API (hosted in Azure)
- Lots of classes created via code generation
- Entity Framework accessing server-side SQL Server database
- Dependency Injection
- Synchronization of server-side data to mobile client
- Repository pattern
- Factory pattern
- Interfaces
- Mappers
- App configuration

Most of the projects in this solution are meant to work on many .NET platforms, such as .NET Core, .NET Framework, Xamarin, and ASP.NET Core applications.

## Get Started

1. To examine client-side Xamarin code, open the `MSC.CM.XaSh.sln` found in the \src folder.

2. Xamarin client - Configure the API endpoint in the App.xaml.cs file to match your server-side deployment of the Web API. i.e. public static string AzureBackendUrl = 

- Using Conveyor extension: "http://10.0.0.145:45455/api/");
- Using Android Emulator: "http://10.0.2.2:6677/api/";
- Deployed to Azure: "https://conferencemate.azurewebsites.net/api/");

3. To explore the server-side Web API and data access code, open the `MSC.ConferenceMate.Web.sln` found in the \src folder.

4. Web API - configure a "developer_config.json" file at the root of MSC.ConferenceMate.API.  This is for the configuration builder and should be structured like this:

- {
	"appSettings": {
		"AzureStorageConfig_AccountKey": "Your Account Key Here",
		"Token_AudienceSecret": "Your Audience Secret Here"
	},
	"connectionStrings": {
		"ConferenceMateDB": "Data Source=.;Initial Catalog=ConferenceMate;Integrated Security=True;..."
	}
}

5. Use the `100 ConferenceMate Schema.sql` and `200 ConverenceMate Data.sql` files to create your own database.  
Alternatively, use the database project that is included as part of the Web solution.

6. To test whether your API is working or not, import these files into Postman:
- src\MSC.ConferenceMate.API.Test\postman-collections\MSC.ConferenceMate.API.postman_collection.json
- src\MSC.ConferenceMate.API.Test\postman-collections\ConferenceMate (Local).postman_environment.json
 

> ## DISCLAIMER: This is SAMPLE APP and a WORK IN PROGRESS

This code is a fork of an application being built to experiment with updating some patterns we use 
when building LOB Xamarin applications.  As such, it is definitely a work in progress and 
suggestions for improvement are welcome.

