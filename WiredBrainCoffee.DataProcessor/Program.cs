using SellingStationery.DataProcessor.Data;
using SellingStationery.DataProcessor.Model;
using SellingStationery.DataProcessor.Parsing;
using SellingStationery.DataProcessor.Processing;

Console.WriteLine("---------------------------------------");
Console.WriteLine("  Selling Stationery - Data Processor  ");
Console.WriteLine("---------------------------------------");
Console.WriteLine();

const string fileName = "StationeryMachineData.csv";
string[] csvLines = File.ReadAllLines(fileName);

MachineDataItem[] machineDataItems = CsvLineParser.Parse(csvLines);

var machineDataProcessor = new MachineDataProcessor(new ConsoleStationeryCountStore());
machineDataProcessor.ProcessItems(machineDataItems);

Console.WriteLine();
Console.WriteLine($"File {fileName} was successfully processed!");

Console.ReadLine();