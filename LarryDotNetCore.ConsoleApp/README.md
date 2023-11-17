A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.) https://stackoverflow.com/questions/17615260/the-certificate-chain-was-issued-by-an-authority-that-is-not-trusted-when-conn

EF Core Db Provider https://learn.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli

Visual Studio 2022 Installation https://docs.google.com/document/d/1EJUY9R0_s8BekTq_vN9N7OLmla71kSdG3AA7TTXkm6A/edit

Microsoft SQL Server 2022 Installation https://docs.google.com/document/d/1RExSyOKaXB5hTbHZAz64tGJHv4cNz8ktvPcIn6iV298/edit

```sql
CREATE TABLE [dbo].[Tbl_Blog](
	[Blog_Id] [int] IDENTITY(1,1) NOT NULL,
	[Blog_Title] [nvarchar](50) NOT NULL,
	[Blog_Author] [nvarchar](50) NOT NULL,
	[Blog_Content] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Blog] PRIMARY KEY CLUSTERED 
(
	[Blog_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```


```sh
- Console App

- Ado.Net (CRUD)
- Dapper  (CRUD)
- EF Entity Framework (Code First => Create Table, Database First => use in Code) (CRUD)
RepoDB

Asp.Net Core Web Api (Rest Api) 
	Ado.Net
	EF
	Dapper 
Postman
Api Call [Console]
	HttpClient
	RestClient
	Refit
Asp.Net Core MVC
	Ado.Net
	EF
	Dapper 
Api Call [MVC]
	HttpClient
	RestClient
	Refit
Minimal Api
Text Logging
Db Logging

Chart [ApexChart, ChartJs, HighCharts, CanvasJS]

SignalR - (Insert Data => UpdateChart, Chat Message)
UI Design
Blazor CRUD [Server, WASM]
------------------------------------------------------
Deploy WASM
Deploy on IIS

Middleware For MVC

GraphQL
gRPC
```