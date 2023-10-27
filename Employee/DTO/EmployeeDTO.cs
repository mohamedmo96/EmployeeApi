namespace Employee.DTO
{
    public class EmployeeDTO
    {
    public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<AddressDTO>? Addresses { get; set; } = new List<AddressDTO>();
    }
}
