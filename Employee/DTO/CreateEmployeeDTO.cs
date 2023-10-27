namespace Employee.DTO
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public AddressDTO? Address { get; set; } 
    }
}