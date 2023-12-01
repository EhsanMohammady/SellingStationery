using SellingStationery.DataProcessor.Model;

namespace SellingStationery.DataProcessor.Data
{
    public interface IStationeryStore
    {
        void Save(StationeryCountItem item);
    }
}
