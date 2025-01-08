namespace TransactionsAPI.Consts.Enums
{
    public class Enums
    {
        public enum OrderType
        {
            Deposit = 1,
            Withdrawal
        }

        public enum OrderStatus
        {
            Pending = 1,
            Executed,
            Failed
        }
    }
}
