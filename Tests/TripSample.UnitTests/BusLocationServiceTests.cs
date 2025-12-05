using Xunit;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Services;
using TripSample.Domain.Model;
using TripSample.Domain.DTO;
using TripSample.Infrastructure.Client;

public class BusLocationServiceTests
{
    private readonly Mock<IObiletApiClient> _apiMock;
    private readonly MemoryCache _cache;
    private readonly BusLocationService _service;
    private const string _busLocationsCacheKey = "bus_locations_all";
    public BusLocationServiceTests()
    {
        _apiMock = new Mock<IObiletApiClient>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _service = new BusLocationService(_apiMock.Object, _cache);
    }


    /// <summary>
    /// Cache MISS testi → API çağrılsın + sonuç cache’e yazılsın
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task WhenCacheMiss_ShouldCallApi_AndCacheResult()
    {

        // API sahte veri döner ama Id sabit olmak zorunda değil
        var fakeResponse = new BusLocationResponseModel
        {
            Status = "Success",
            Data = new List<BusLocationModel>
        {
            new BusLocationModel { Id = new Random().Next(), Name = "RandomCity" }
        }
        };

        _apiMock
            .Setup(x =>
                x.PostAsync<BusLocationsRequestModel, BusLocationResponseModel>(
                    It.IsAny<string>(), It.IsAny<BusLocationsRequestModel>()))
            .ReturnsAsync(fakeResponse);

        var result = await _service.GetBusLocationsAsync("ankara", new SessionModel());

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("RandomCity", result[0].Name);   // Sadece Name önemli

        // Cache’e gerçekten yazıldı mı?
        bool cacheExists = _cache.TryGetValue(_busLocationsCacheKey + "_ankara", out var cached);
        Assert.True(cacheExists);
    }

    /// <summary>
    /// API Error testi → ID bağımlı değildir zaten
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task WhenApiReturnsError_ShouldThrowException()
    {
        _apiMock
            .Setup(x =>
                x.PostAsync<BusLocationsRequestModel, BusLocationResponseModel>(
                    It.IsAny<string>(), It.IsAny<BusLocationsRequestModel>()))
            .ReturnsAsync(new BusLocationResponseModel { Status = "Error" });

        await Assert.ThrowsAsync<Exception>(() =>
            _service.GetBusLocationsAsync("test", new SessionModel()));
    }



}
