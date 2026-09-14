namespace greenslip_backend.DTOs;

public class SegmentDto
{
    public string SegmentName {get; set;} = string.Empty;
    public object? FilterCriteria {get; set;}
    public List<CustomerListEntryDto> CustomerList {get; set;} = new List<CustomerListEntryDto>();
    
}