namespace CoreApi.Models
{
    public class DepartmentModel
    {
        public string? DepartmentGuid { get; set; }
        public string? DepartmentName { get; set; }

         
    }

    public class DepartmentModelList
    {
         public List<DepartmentModel>? _DepartmentList { get;}
    }





}
