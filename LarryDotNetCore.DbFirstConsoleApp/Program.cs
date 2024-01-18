// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// Scaffold DbContext Command on PM (package manager)

/*Scaffold-DbContext [-Connection] [-Provider] [-OutputDir] [-Context] [-Schemas>] [-Tables>] 
-DataAnnotations][-Force][-Project][-StartupProject][<CommonParameters>]*/

// Scaffold-DbContext "Server=.;Database=LarryDotNetCore;User ID=sa;Password=sa@123;Trusted_Connection=True;Trust Server Certificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AppDbContext -Tables Tbl_Blog