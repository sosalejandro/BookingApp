using AutoFixture.AutoMoq;
using BookingApp.DomainLayer.Models;
using BookingApp.Fixtures.Shared.Generators;
using BookingApp.Fixtures.Shared.Helpers;
using BookingApp.Tests.Shared.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookingApp.Fixtures.Shared.Attributes;


[AttributeUsage(AttributeTargets.Method)]
public class AutoMoqControllerEmptyAttribute : AutoDataAttribute
{
    public AutoMoqControllerEmptyAttribute() : base(
       () =>
       {
           var fixture = new Fixture();
           TestingSuiteClassMockerHelper.CreateMocks(
               fixture,
               out IEnumerable<Hotel> mockHotels,
               out IEnumerable<HotelDto> mockHotelsDto);

           Thread.Sleep(50);

           fixture.Customizations.Add(new HotelParametersGenerator());

           

           fixture.Inject(mockHotels);
           fixture.Inject(mockHotelsDto);

           fixture.Customize(new AutoMoqCustomization());
           fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

           return fixture;
       })
    { }
}
