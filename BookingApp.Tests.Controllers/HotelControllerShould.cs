using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.Fixtures.Shared.Attributes;
using BookingApp.Fixtures.Shared.Helpers;
using BookingApp.Presentation.Controllers;
using BookingApp.ServiceLayer.Abstractions;
using BookingApp.Tests.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Tests.Controllers;

[Collection("Hotels")]
public class HotelControllerShould
{
    //private readonly Fixture _fixture;
    //private readonly HotelsController _sut;
    //private readonly Mock<IServiceManager> _mockServiceManager;
    //private readonly HotelParameters _hotelParams;
    //private readonly CancellationToken _stoppingToken;
    //private readonly IEnumerable<Hotel> _hotels;
    //private readonly IEnumerable<HotelDto> _hotelsDto;
    public HotelControllerShould()
    {
        //_fixture = new Fixture();
        //_fixture.Customizations.Add(new HotelParametersGenerator());

        //var mockServiceManager = _fixture.Freeze<Mock<IServiceManager>>();

        //_fixture.Customize(new AutoMoqCustomization());
        //_fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

        //TestingSuiteClassMockerHelper.CreateMocks(
        //    _fixture,
        //    out IEnumerable<Hotel> mockHotels,
        //    out IEnumerable<HotelDto> mockHotelsDto);

        //_hotels = mockHotels;
        //_hotelsDto = mockHotelsDto;

        //_hotelParams = _fixture.Create<HotelParameters>();
        //_stoppingToken = _fixture.Freeze<CancellationToken>();
        //_mockServiceManager = mockServiceManager;
        //_sut = _fixture.Create<HotelsController>();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_InvokeServiceGetAllAsyncMethodOnce(
        [Frozen] HotelParameters hotelParams,
        IEnumerable<HotelDto> hotelsDto,
        CancellationToken stoppingToken,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotelsDto,
            TestingSuite.GetAllAsync,
            hotelParams);

        // Act
        _ = await sut.GetHotels(hotelParams, stoppingToken);

        // Assert
        mockServiceManager
            .Verify(
            s =>
            s.HotelService.GetAllAsync(hotelParams, stoppingToken),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_ResultHotelsMatchParameters(
        [Frozen] HotelParameters hotelParams,
        IEnumerable<HotelDto> hotelsDto,
        CancellationToken stoppingToken,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotelsDto,
            TestingSuite.GetAllAsync,
            hotelParams);

        // Act
        var result = (OkObjectResult)await sut.GetHotels(hotelParams, stoppingToken);

        // Assert
        var (hotels, metaData) =
            ((IEnumerable<HotelDto> hotels, MetaData metaData))result.Value;

        hotels.Should().HaveCountLessThanOrEqualTo(hotelParams.PageSize);
        metaData.PageSize.Should().Be(hotelParams.PageSize);
        metaData.CurrentPage.Should().Be(hotelParams.PageNumber);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_ResultNotNull(
        [Frozen] HotelParameters hotelParams,
        IEnumerable<HotelDto> hotelsDto,
        CancellationToken stoppingToken,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
              mockServiceManager,
              hotelsDto,
              TestingSuite.GetAllAsync,
              hotelParams);

        // Act
        var result = (OkObjectResult)await sut.GetHotels(hotelParams, stoppingToken);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_ReturnTupleListHotelDto_And_MetaData(
        [Frozen] HotelParameters hotelParams,
        IEnumerable<HotelDto> hotelsDto,
        CancellationToken stoppingToken,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
               mockServiceManager,
               hotelsDto,
               TestingSuite.GetAllAsync,
               hotelParams);

        // Act
        var result = (OkObjectResult)await sut.GetHotels(
            hotelParams,
            stoppingToken);

        // Assert
        var (hotels, metaData) =
            ((IEnumerable<HotelDto> hotels, MetaData metaData))result.Value;
        hotels.Should().BeOfType<List<HotelDto>>();
        metaData.Should().BeOfType<MetaData>();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels__ReturnStatus200(
        [Frozen] HotelParameters hotelParams,
        IEnumerable<HotelDto> hotelsDto,
        CancellationToken stoppingToken,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotelsDto,
            TestingSuite.GetAllAsync,
            hotelParams);

        // Act
        var result = (OkObjectResult)await sut.GetHotels(
            hotelParams,
            stoppingToken);

        // Assert
        result.StatusCode.Should().Be(200);
    }



    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotel")]
    public async Task GetHotel_ReturnStatus200(
        IEnumerable<Hotel> hotels,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.GetHotelAsync);

        int id = 5;

        // Act
        var result = (OkObjectResult)await sut.GetHotel(id);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<HotelDto>();
        (result.Value as HotelDto).Id.Should().Be(id);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotel")]
    public async Task GetHotel_ResultShouldBeTypeOfHotelDto(
    IEnumerable<Hotel> hotels,
    [Frozen] Mock<IServiceManager> mockServiceManager,
    HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.GetHotelAsync);

        int id = 5;

        // Act
        var result = (OkObjectResult)await sut.GetHotel(id);

        // Assert
        result.Value.Should().BeOfType<HotelDto>();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotel")]
    public async Task GetHotel_ReturnRequestedIdHotel(
    IEnumerable<Hotel> hotels,
    [Frozen] Mock<IServiceManager> mockServiceManager,
    HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.GetHotelAsync);

        int id = 5;

        // Act
        var result = (OkObjectResult)await sut.GetHotel(id);

        // Assert
        (result.Value as HotelDto).Id.Should().Be(id);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotel")]
    public async Task GetHotel_ReturnNotNull(
        IEnumerable<Hotel> hotels,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.GetHotelAsync);

        int id = 5;

        // Act
        var result = (OkObjectResult)await sut.GetHotel(id);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotel")]
    public async Task GetHotel_InvokeServiceGetHotelAsyncMethodOnce(
        IEnumerable<Hotel> hotels,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.GetHotelAsync);

        int id = 5;

        // Act
        var result = (OkObjectResult)await sut.GetHotel(id);

        // Assert
        mockServiceManager
            .Verify(
            s =>
            s.HotelService.GetHotelAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "DeleteHotel")]
    public async Task DeleteHotel_ReturnStatus204(
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.DeleteAsync);

        int id = 5;

        // Act
        var result = (NoContentResult)await sut.DeleteHotel(id);

        // Assert
        result.StatusCode.Should().Be(204);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "DeleteHotel")]
    public async Task DeleteHotel_InvokeServiceDeleteHotelMethodOnce(
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.DeleteAsync);

        int id = 5;

        // Act
        var result = (NoContentResult)await sut.DeleteHotel(id);

        // Assert
        mockServiceManager
            .Verify(
            s =>
            s.HotelService.DeleteAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "UpdateHotel")]
    public async Task UpdateHotel_ShouldReturnStatus204(
        HotelForUpdateDto hotel,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.UpdateAsync);

        int id = 5;

        // Act
        var result = (NoContentResult)await sut.UpdateHotel(id, hotel);

        // Assert
        result.StatusCode.Should().Be(204);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "UpdateHotel")]
    public async Task UpdateHotel_InvokeServiceUpdateHotelMethodOnce(
        HotelForUpdateDto hotel,
        [Frozen] Mock<IServiceManager> mockServiceManager,
        HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.UpdateAsync);

        int id = 5;

        // Act
        var result = (NoContentResult)await sut.UpdateHotel(id, hotel);

        // Assert
        mockServiceManager
            .Verify(
            s =>
            s.HotelService.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<HotelForUpdateDto>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "CreateHotel")]
    public async Task CreateHotel_ReturnStatus201(
         IEnumerable<Hotel> hotels,
         HotelForCreationDto hotel,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.CreateAsync);

        // Act
        var result = (CreatedAtRouteResult)await sut.CreateHotel(hotel);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<HotelDto>();
        result.StatusCode.Should().Be(201);
        result.RouteName.Should().Be("HotelById");

        mockServiceManager
            .Verify(
            s =>
            s.HotelService.CreateAsync(
                It.IsAny<HotelForCreationDto>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "CreateHotel")]
    public async Task CreateHotel_ReturnNotNull(
         IEnumerable<Hotel> hotels,
         HotelForCreationDto hotel,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.CreateAsync);

        // Act
        var result = (CreatedAtRouteResult)await sut.CreateHotel(hotel);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "CreateHotel")]
    public async Task CreateHotel_ReturnBeTypeOfHotelDto(
         IEnumerable<Hotel> hotels,
         HotelForCreationDto hotel,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.CreateAsync);

        // Act
        var result = (CreatedAtRouteResult)await sut.CreateHotel(hotel);

        // Assert
        result.Value.Should().BeOfType<HotelDto>();
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "CreateHotel")]
    public async Task CreateHotel_RouteNameEqualTo_HotelById(
         IEnumerable<Hotel> hotels,
         HotelForCreationDto hotel,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.CreateAsync);

        // Act
        var result = (CreatedAtRouteResult)await sut.CreateHotel(hotel);

        // Assert
        result.RouteName.Should().Be("HotelById");
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "CreateHotel")]
    public async Task CreateHotel_InvokeCreateHotelMethod(
         IEnumerable<Hotel> hotels,
         HotelForCreationDto hotel,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            hotels,
            TestingSuite.CreateAsync);

        // Act
        var result = (CreatedAtRouteResult)await sut.CreateHotel(hotel);

        // Assert
        mockServiceManager
            .Verify(
            s =>
            s.HotelService.CreateAsync(
                It.IsAny<HotelForCreationDto>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_OnEmptyCollection_ReturnStatus404(
         HotelParameters hotelParams,
         CancellationToken stoppingToken,
         [Frozen] Mock<IServiceManager> mockServiceManager,
         HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.GetAllEmptyAsync);

        // Act
        var result = (NotFoundObjectResult)await sut.GetHotels(hotelParams, stoppingToken);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Theory, AutoMoqControllerEmpty]
    [Trait("HotelsController", "GetHotels")]
    public async Task GetHotels_OnEmptyCollection_ReturnString(
     HotelParameters hotelParams,
     CancellationToken stoppingToken,
     [Frozen] Mock<IServiceManager> mockServiceManager,
     HotelsController sut)
    {
        // Arrange
        ServiceManagerHelper.RegisterMockHotelService(
            mockServiceManager,
            TestingSuite.GetAllEmptyAsync);

        // Act
        var result = (NotFoundObjectResult)await sut.GetHotels(hotelParams, stoppingToken);

        // Assert
        result.Value.Should().BeOfType<string>();
    }

    // TODO: Patch endpoint
}
