using EFCore.BulkExtensions;
using IMDBLib.DataBase;
using IMDBLib.Models.Records;
using IMDBLib.Services;
using IMDBLib.Services.DatabaseServices;
using System;



string titlesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.basics.data.tsv";
string crewFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.crew.data.tsv";
string namesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\name.basics.data.tsv";


try
{
    using (var dbContext = new MyDbContext())
    {
        Console.WriteLine("deleting old data");
        //DbDeleteService.DeleteAllData();

        Console.WriteLine("TSVReader process is started");
        var TSVReader = new TSVLoadService();
        Console.WriteLine("TSVReader started");

        var tilteBasicsRecords = TSVReader.LoadCsv<TitleBasicsRecord>(titlesFilePath, 10000);
        var nameBasicsRecords = TSVReader.LoadCsv<NameBasicsRecord>(namesFilePath, 10000);
        var crewBasicsRecords = TSVReader.LoadCsv<CrewBasicsRecord>(crewFilePath, 10000);

        Console.WriteLine("TitleBasicsRecords loaded");
        Console.WriteLine("process is done");

        Console.WriteLine("TitleBasicsProcessor is starting");
        var (titles, titleTypes, genres, title_Genres) = TitleInsertService.TitleBasicsProcessor(tilteBasicsRecords);
        Console.WriteLine("TitleBasicsProcessor is done");

        Console.WriteLine("NameProcessRecords is starting");
        var (crews, crewProfessions, jobs, knownForTitlesList) = NameInsertService.NameProcessRecords(nameBasicsRecords, titles);
        Console.WriteLine("NameProcessRecords is done");

        Console.WriteLine("CrewProcessRecords is starting");
        var (title_crews, directors, writers) = CrewInsertService.CrewProcessRecords(crewBasicsRecords, titles, crews);
        Console.WriteLine("CrewProcessRecords is done");

        Console.WriteLine("BulkInsert is starting");

        Console.WriteLine("BulkInsertTitle has started");
        // Bulk insert titleTypes to save them to the database
        dbContext.BulkInsert(titleTypes);
        dbContext.BulkInsert(titles);
        dbContext.BulkInsert(genres);
        dbContext.BulkInsert(title_Genres);
        Console.WriteLine("BulkInsertTitle has finished");

        Console.WriteLine("BulkInsertName has started");
        // Bulk insert NameBasics
        dbContext.BulkInsert(jobs);
        dbContext.BulkInsert(crewProfessions);
        dbContext.BulkInsert(crews);
        dbContext.BulkInsert(knownForTitlesList);
        Console.WriteLine("BulkInsertName has finished");


        Console.WriteLine("BulkInsertCrew has started");
        // Bulk insert CrewBasics
        dbContext.BulkInsert(title_crews);
        dbContext.BulkInsert(directors);
        dbContext.BulkInsert(writers);
        Console.WriteLine("BulkInsertCrew has finished");


        Console.WriteLine("Changes are getting saved now");
        dbContext.SaveChanges();
        Console.WriteLine("Changes are save");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}



