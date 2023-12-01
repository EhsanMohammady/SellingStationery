using SellingStationery.DataProcessor.Data;
using SellingStationery.DataProcessor.Model;

namespace SellingStationery.DataProcessor.Processing;

public class MachineDataProcessorTests : IDisposable
{
    private readonly FakeStationeryCountStore _stationeryCountStore;
    private readonly MachineDataProcessor _machineDataProcessor;

    public MachineDataProcessorTests()
    {
        _stationeryCountStore = new FakeStationeryCountStore();
        _machineDataProcessor = new MachineDataProcessor(_stationeryCountStore);
    }

    [Fact]
    public void ShouldSaveCountPerStationeryType()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem("Book",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Book",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Pen",new DateTime(2022,10,27,10,0,0))
        };

        // Act
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _stationeryCountStore.SavedItems.Count);

        var item = _stationeryCountStore.SavedItems[0];
        Assert.Equal("Book", item.StationeryType);
        Assert.Equal(2, item.Count);

        item = _stationeryCountStore.SavedItems[1];
        Assert.Equal("Pen", item.StationeryType);
        Assert.Equal(1, item.Count);
    }

    [Fact]
    public void ShouldClearPreviousStationeryCount()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem("Book",new DateTime(2022,10,27,8,0,0))
        };

        // Act
        _machineDataProcessor.ProcessItems(items);
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _stationeryCountStore.SavedItems.Count);
        foreach (var item in _stationeryCountStore.SavedItems)
        {
            Assert.Equal("Book", item.StationeryType);
            Assert.Equal(1, item.Count);
        }
    }

    [Fact]
    public void ShouldIgnoreItemsThatAreNotNewer()
    {
        // Arrange
        var items = new[]
        {
            new MachineDataItem("Book",new DateTime(2022,10,27,8,0,0)),
            new MachineDataItem("Book",new DateTime(2022,10,27,7,0,0)),
            new MachineDataItem("Book",new DateTime(2022,10,27,7,10,0)),
            new MachineDataItem("Book",new DateTime(2022,10,27,9,0,0)),
            new MachineDataItem("Pen",new DateTime(2022,10,27,10,0,0)),
            new MachineDataItem("Pen",new DateTime(2022,10,27,10,0,0))
        };

        // Act
        _machineDataProcessor.ProcessItems(items);

        // Assert
        Assert.Equal(2, _stationeryCountStore.SavedItems.Count);

        var item = _stationeryCountStore.SavedItems[0];
        Assert.Equal("Book", item.StationeryType);
        Assert.Equal(2, item.Count);

        item = _stationeryCountStore.SavedItems[1];
        Assert.Equal("Pen", item.StationeryType);
        Assert.Equal(1, item.Count);
    }

    public void Dispose()
    {
        // This runs after every test
    }
}

public class FakeStationeryCountStore : IStationeryStore
{
    public List<StationeryCountItem> SavedItems { get; } = new();

    public void Save(StationeryCountItem item)
    {
        SavedItems.Add(item);
    }
}
