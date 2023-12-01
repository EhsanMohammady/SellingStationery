using SellingStationery.DataProcessor.Data;
using SellingStationery.DataProcessor.Model;

namespace SellingStationery.DataProcessor.Processing
{
    public class MachineDataProcessor
    {
        private readonly Dictionary<string, int> _countPerStationeryType = new();
        private readonly IStationeryStore _stationeryCountStore;
        private MachineDataItem? _previousItem;

        public MachineDataProcessor(IStationeryStore stationeryCountStore)
        {
            _stationeryCountStore = stationeryCountStore;
        }

        public void ProcessItems(MachineDataItem[] dataItems)
        {
            _previousItem = null;
            _countPerStationeryType.Clear();

            foreach (var dataItem in dataItems)
            {
                ProcessItem(dataItem);
            }

            SaveCountPerStationeryCountStoreType();
        }

        private void ProcessItem(MachineDataItem dataItem)
        {
            if (!IsNewerThanPreviousItem(dataItem))
            {
                return;
            }

            if (!_countPerStationeryType.ContainsKey(dataItem.StationeryCountStoreType))
            {
                _countPerStationeryType.Add(dataItem.StationeryCountStoreType, 1);
            }
            else
            {
                _countPerStationeryType[dataItem.StationeryCountStoreType]++;
            }

            _previousItem = dataItem;
        }

        private bool IsNewerThanPreviousItem(MachineDataItem dataItem)
        {
            return _previousItem is null
                || _previousItem.CreatedAt < dataItem.CreatedAt;
        }

        private void SaveCountPerStationeryCountStoreType()
        {
            foreach (var entry in _countPerStationeryType)
            {
                _stationeryCountStore.Save(new StationeryCountItem(entry.Key, entry.Value));
            }
        }
    }
}
