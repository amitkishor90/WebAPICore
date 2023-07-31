namespace CoreApi.Models
{
    public class EmployeesModels
    {
        public string? Guid { get; set; }
        public string?  FirstName { get; set; }
        public string?  LastName { get; set; }
        public string?  Emailid { get; set; }
        public string?  PenCardNo { get; set; }
        public decimal? Salary { get; set; }
       // public int?  GenderId { get; set; }
        public string ?  GenderGuid { get; set; }
        public string? DepartmentGuid { get; set; }
      //  public int?  DepartmentId { get; set; }
        public string?  Address { get; set; }

        public string? Role { get; set; }
        public DateTime? DateIns { get; set; }
    }

    public class EmployeesModelsList
    {
        public string? EmployeeGuid { get; set; }
        public string? GenderGuid { get; set; }
        public string? DepartmentGuid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Emailid { get; set; }
        public string? PenCardNo { get; set; }
        public double salary { get; set; }
        public string? GenderName { get; set; }  
        
        public string? DepartmentName { get; set; }
        public string? Address { get; set; }

         
    }


}
