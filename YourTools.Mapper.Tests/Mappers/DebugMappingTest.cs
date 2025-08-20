using Microsoft.Extensions.DependencyInjection;
using YourTools.Mapper;
using YourTools.Mapper.Generated;
using YourTools.Mapper.Tests.Models.DTOs;
using YourTools.Mapper.Tests.Models.Entities;

namespace YourTools.Mapper.Tests.Mappers;

public class DebugMappingTest
{
    [Fact]
    public void Debug_InstaPageId_Mapping()
    {
        // Arrange
        var services = new ServiceCollection();
        services.RegisterMappingHandlers();
        var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetRequiredService<IMapper>();

        // Test 1: Nullable to Non-nullable (should work with our fix)
        var testModel = new TestModel
        {
            Name = "Test",
            Age = 25,
            InstaPageId = "test123"
        };
        
        var testEntity = mapper.Map<TestEntity>(testModel);
  
        // Test 2: Non-nullable to Nullable (this is failing)
        var testEntity2 = new TestEntity
        {
            Name = "Test2",
            Age = 30,
            InstaPageId = "entity456"
        };
        
        var testModel2 = mapper.Map<TestModel>(testEntity2);
        
        // Assertions
        Assert.Equal("test123", testEntity?.InstaPageId);
        Assert.Equal("entity456", testModel2?.InstaPageId);
    }
}
