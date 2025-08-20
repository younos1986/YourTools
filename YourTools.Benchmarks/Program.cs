using System.Text.Json;
using Application.Models.Entities;
using Application.Models.ViewModels;
using AutoMapper;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using YourTools.Benchmarks;
using YourTools.Mapper;
using YourTools.Mapper.Generated;
using IMapper = YourTools.Mapper.IMapper;

//BenchmarkRunner.Run<MappingBenchmarksCustomMapper>();
BenchmarkRunner.Run<MappingBenchmarksComplexObject>();
//BenchmarkRunner.Run<MappingBenchmarksSimpleObject>();
//BenchmarkRunner.Run<MappingBenchmarksOrderEntityComplexObject>();
//BenchmarkRunner.Run<MappingBenchmarksAddressModelSimpleObject>();

return 0;

List<PersonEntity> peopleList =
[
    new()
    {
        Email = $"person@mail.com",
        Age = 38,
        Name = $"Person Name ",
        Certificate = new CertificateEntity()
        {
            CertificateId = $"CertId ",
            CertificateName = $"Certificate Name ",
            ExpiryDate = DateTime.Now
        },
        Addresses =
        [
            new()
            {
                Street = $"Street ",
                City = $"City ",
                ZipCode = $"ZipCode ",
            }
        ]
    }

];

var services = new ServiceCollection();
services.RegisterMappingHandlers();
        
var sp = services.BuildServiceProvider();
var myMapper = sp.GetRequiredService<IMapper>();

BalanceEntity BalanceSample = new()
{
    Id = Guid.NewGuid().ToString(),
    UserId = Guid.NewGuid().ToString(),
    Amount = 100,
    CreatedAt = DateTime.Now,
    UpdatedAt = DateTime.Now,
    IsDeleted = false,
};
var balanceModel = myMapper.MapSingleObject<BalanceEntity ,BalanceModel>(BalanceSample);

var result = myMapper.Map<List<PersonModel>>(peopleList);

Console.WriteLine(JsonSerializer.Serialize(result));


