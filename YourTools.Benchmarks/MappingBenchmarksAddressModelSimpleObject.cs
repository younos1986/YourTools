﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.Extensions.DependencyInjection;
using YourTools.Mapper.Generated;
using Application.Models.ViewModels;
using Application.Models.Entities;
using Mapster;
using IMapper = YourTools.Mapper.IMapper;

namespace YourTools.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob] // built-in fast mode
//[SimpleJob(RuntimeMoniker.Net80)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class MappingBenchmarksAddressModelSimpleObject
{
    private IMapper _myMapper = default!;
    private AddressEntity _singleAddress = default!;
    
    private AutoMapper.IMapper _mapper;
    private static AddressModel AddressModel = new() { Street = "123 Main St", City = "Sample City", ZipCode = "12345" };
    private static AddressModel[] _addressArray = [];
    private static List<AddressModel> _addressList = [];

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 25; i++)
        {
            _addressList.Add(new()
            {
                Street = $"123 Main St {i}",
                City = $"Sample City {i}",
                ZipCode = $"12345 {i}"
            });
        }
        _addressArray = _addressList.ToArray();
        Console.WriteLine("************************************************************************************");
        Console.WriteLine($"Address List Count: {_addressList.Count}");
        Console.WriteLine("************************************************************************************");
        
        var services = new ServiceCollection();
        services.RegisterYourToolsMapping();
        services.AddLogging();
        services.AddAutoMapper(cfg => { },
            typeof(AddressEntity).Assembly);
        
        var sp = services.BuildServiceProvider();
        _myMapper = sp.GetRequiredService<IMapper>();
        _mapper = sp.GetRequiredService<AutoMapper.IMapper>();
    }

    [Benchmark]
    [BenchmarkCategory("Single")]
    public AddressEntity YourTools()
    {
        return _myMapper.MapSingleObject<AddressModel, AddressEntity>(AddressModel);
    }
    
    [Benchmark]
    [BenchmarkCategory("Single")]
    public AddressEntity Mapster()
    {
        return AddressModel.Adapt<AddressEntity>();
    }
    
    [Benchmark]
    [BenchmarkCategory("Single")]
    public AddressEntity AutoMapper()
    {
        return _mapper.Map<AddressEntity>(AddressModel);
    }
    
    //***********************************************************************************

    [Benchmark]
    [BenchmarkCategory("List to List")]
    public List<AddressEntity> YourTools_List()
    {
        return _myMapper.Map<List<AddressEntity>>(_addressList);
    }
    
    [Benchmark]
    [BenchmarkCategory("List to List")]
    public List<AddressEntity> Mapster_List()
    {
        return _addressList.Adapt<List<AddressEntity>>();
    }
    
    [Benchmark]
    [BenchmarkCategory("List to List")]
    public List<AddressEntity> AutoMapper_List()
    {
        return _mapper.Map<List<AddressEntity>>(_addressList);
    }
    
    //***********************************************************************************
    
    [Benchmark]
    [BenchmarkCategory("Array To Array")]
    public AddressEntity[] YourTools_Array()
    {
        return _myMapper.Map<AddressEntity[]>(_addressArray);
    }
    
    [Benchmark]
    [BenchmarkCategory("Array To Array")]
    public AddressEntity[] Mapster_Array()
    {
        return _addressArray.Adapt<AddressEntity[]>();
    }
    
    [Benchmark]
    [BenchmarkCategory("Array To Array")]
    public AddressEntity[] AutoMapper_Array()
    {
        return _mapper.Map<AddressEntity[]>(_addressArray);
    }
}
