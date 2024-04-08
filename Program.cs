using IMDBLib.DataBase;
using IMDBLib.Services;


string titlesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.basics.data.tsv";
string namesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\name.basics.data.tsv";
string crewFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.crew.data.tsv";


try
{
    TimeZoneInfo danishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    DateTime danishNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, danishTimeZone);
    TimeOnly startTime = TimeOnly.FromDateTime(danishNow);

    Console.WriteLine("Start time: " + startTime);

    IMDBDbContext dbContext = new IMDBDbContext();
    DataImportService dataImportService = new DataImportService();
    DataDeleteService dataDeleteService = new DataDeleteService(dbContext);

    DataLoadService dataLoadService = new DataLoadService(dbContext, dataImportService, dataDeleteService);

    await dataLoadService.LoadDataAsync(titlesFilePath, namesFilePath, crewFilePath, 10000, 100000);

    DateTime danishEndTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, danishTimeZone);
    TimeOnly endTime = TimeOnly.FromDateTime(danishEndTime);

    Console.WriteLine("End time: " + endTime);

    TimeSpan totalDuration = danishEndTime - danishNow;
    Console.WriteLine("Total time taking: " + totalDuration);
}
catch (Exception e)
{
    Console.WriteLine(e);
}



