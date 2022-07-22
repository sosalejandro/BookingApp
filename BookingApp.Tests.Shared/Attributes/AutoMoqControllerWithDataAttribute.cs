using AutoFixture.AutoMoq;
using BookingApp.Fixtures.Shared.Generators;
using BookingApp.Fixtures.Shared.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookingApp.Fixtures.Shared.Attributes;


[AttributeUsage(AttributeTargets.Method)]
public class AutoMoqControllerWithDataAttribute : AutoDataAttribute
{
    public AutoMoqControllerWithDataAttribute() : base(
       () =>
       {
           var fixture = new Fixture();
           fixture.Customizations.Add(new HotelParametersGenerator());

           //RegisterMockHotelRepository(fixture);
           //ServiceManagerHelper.RegisterMockHotelService(fixture);

           fixture.Customize(new AutoMoqCustomization());
           fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());

           return fixture;
       })
    { }
}
