namespace CoreApi.Models
{
    public class GenderModel
    {

        public string? GenderGuid { get; set; }
        public string? Name { get; set; }
    }


    public class GenderModellist
    {
        public string? GenderGuid { get; set; }
        public string? Name { get; set; }
    }

    public class Genderlist
    {
        public List<GenderModel> _GenderModellist { get; set; }
    }




}
