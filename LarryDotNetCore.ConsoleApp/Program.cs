// See https://aka.ms/new-console-template for more information
using LarryDotNetCore.ConsoleApp.AdoDotNetExamples;
using LarryDotNetCore.ConsoleApp.DapperExamples;
using LarryDotNetCore.ConsoleApp.EFCoreExamples;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNet = new AdoDotNetExample();
//adoDotNet.Run();

//DapperExample dapper = new DapperExample();
//dapper.Run();

EFCoreExample eFCore = new EFCoreExample();
eFCore.Run();

Console.ReadKey();