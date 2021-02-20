# Grouper
Specification Program Methods subject Poject <br />
To run it localy you need .net5 sdk https://dotnet.microsoft.com/download/dotnet/5.0,
or just update Visual Studio 2019 to the latest version.
Also Microsoft Sql Server Management Studio is needed. <br />
In file ```..\Grouper.Api\src\Grouper.Api.Web\appsettings.Development.json```<br />
should change row ```"DefaultConnection": "Server=DESKTOP-2UQRN34\\SQLEXPRESS;Database=GrouperDb;Trusted_Connection=True;Integrated Security=SSPI;MultipleActiveResultSets=true"```<br />
and set ```Server={your local server}``` and that's it.
