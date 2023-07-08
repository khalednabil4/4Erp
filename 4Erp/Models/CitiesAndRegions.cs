using System.ComponentModel.DataAnnotations;

namespace _4Erp.Models
{
    public class CitiesAndRegions
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage ="برجاء ادخال الكود المدينة ")]
        public string CityCode { get; set; }

        [Required(ErrorMessage = "برجاء ادخال الوصف بالغة العربية ")]
        public string CityDescriptionAR  { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الوصف بالغة الانجلزية  ")]
        public string CityDescriptionEn { get; set; }
        [Required]
        public Boolean Status { get; set; }
        
        public string? Note { get; set; }
        public virtual ICollection<Region>? Region { get; set; }

     
    }
    public class Region
    {
        [Key]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "برجاء ادخال الكود المنطقة  ")]
        public string RegionCode { get; set; }

        [Required(ErrorMessage = "برجاء ادخال الوصف بالغة العربية ")]
        public string RegionDescriptionAR { get; set; }
        [Required(ErrorMessage = "برجاء ادخال الوصف بالغة الانجلزية  ")]
        public string RegionDescriptionEn { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual CitiesAndRegions City { get; set; }
    }
}
