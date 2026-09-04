using System.Text.Json;

namespace greenslip_backend.Models;

public class CustomerSegment{
    public int Id {get; set;}
    public string SegmentName {get; set;} = string.Empty;
    public string? FilterCriteria { get; set; }
    public string CustomerList { get; set; } = "[]";
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;

}
