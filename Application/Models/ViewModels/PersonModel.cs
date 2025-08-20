using Application.Models.Entities;

namespace Application.Models.ViewModels;

public class PersonModel
{
    public string Email { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    
    public CertificateModel Certificate { get; set; }
    
    public List<AddressModel> Addresses { get; set; } = new();
}

public class CertificateModel
{
    public string CertificateId { get; set; }
    public string CertificateName { get; set; }
    public DateTime ExpiryDate { get; set; }
}

public class AddressModel
{
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}
