namespace MonefyClient.Application.DTOs.InputDTOs
{
    public class InputAccountDTO
    {
        public string Name { get; set; }
        = string.Empty;
        public string Currency { get; set; }
        = string.Empty;
        public IEnumerable<InputIncomeDTO> Incomes { get; set; }
            = Enumerable.Empty<InputIncomeDTO>();
        public IEnumerable<InputExpenseDTO> Expenses { get; set; }
            = Enumerable.Empty<InputExpenseDTO>();
    }
}