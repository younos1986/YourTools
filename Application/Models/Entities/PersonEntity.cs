namespace Application.Models.Entities;

public partial class PersonEntity
{
    public string Email { get; set; }
    public int Age { get; set; }
    public string Name { get; set; }
    
    public CertificateEntity Certificate { get; set; }
    
    public List<AddressEntity> Addresses { get; set; } = new();
}

public class CertificateEntity
{
    public string CertificateId { get; set; }
    public string CertificateName { get; set; }
    public DateTime ExpiryDate { get; set; }
}

public class AddressEntity
{
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}