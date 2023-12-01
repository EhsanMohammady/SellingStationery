using SellingStationery.DataProcessor.Model;

namespace SellingStationery.DataProcessor.Data;

public class ConsoleStationeryCountStoreTests
{
    [Fact]
    public void ShouldWriteOutputToConsole()
    {
        // Arrange
        var item = new StationeryCountItem("Book", 5);
        var stringWriter = new StringWriter();
        var consoleStationeryCountStore = new ConsoleStationeryCountStore(stringWriter);

        // Act
        consoleStationeryCountStore.Save(item);

        // Assert
        var result = stringWriter.ToString();
        Assert.Equal($"{item.StationeryType}:{item.Count}{Environment.NewLine}", result);
    }
}
