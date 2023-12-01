using SellingStationery.DataProcessor.Model;

namespace SellingStationery.DataProcessor.Data
{
    public class ConsoleStationeryCountStore : IStationeryStore
    {
        private readonly TextWriter _textWriter;

        public ConsoleStationeryCountStore() : this(Console.Out) { }

        public ConsoleStationeryCountStore(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void Save(StationeryCountItem item)
        {
            var line = $"{item.StationeryType}:{item.Count}";
            _textWriter.WriteLine(line);
        }
    }
}
