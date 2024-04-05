using IMDBLib.DataBase;
using IMDBLib.Services;




string titlesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.basics.data.tsv";
string namesFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\name.basics.data.tsv";
string crewFilePath = "C:\\Users\\mads-\\OneDrive\\Skrivebord\\4.Semester\\Database\\DataSet\\title.crew.data.tsv";


try
{

    YourDbContext yourDbContext = new YourDbContext();

    DataImportService dataImportService = new DataImportService(yourDbContext);

    dataImportService.ImportData(titlesFilePath, namesFilePath, crewFilePath, 1000, 50000, 10);
}
catch (Exception e)
{
    Console.WriteLine(e);
}



