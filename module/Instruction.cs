 namespace PocMarketInterface.Module
{
   public class Instruction: IData
    {
        public long DataId { get; set; }
        public DateTime CreatedTime { get; set; }

        public string DataType => EDataType.Instruction;

        public string MarketCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }   
        public decimal Amount { get; set; }  
        public bool IsCancelled { get; set; }
        public bool IsSettled { get; set; }
        public bool IsMatched { get; set; }
    }
}