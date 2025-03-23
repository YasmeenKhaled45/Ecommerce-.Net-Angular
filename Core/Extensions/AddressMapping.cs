using System;
using Core.DTOS;
using Core.Entities;

namespace Core.Extensions;

public static class AddressMapping
{
  public static AddressDTO? toDTo(this Address address){
     if(address == null) return null;
     return new AddressDTO
     {
        Line1 = address.Line1,
        Line2 = address.Line2,
        City = address.City,
        Country = address.Country,
        PostalCode = address.PostalCode
     };

  }
   public static Address TOEntity(this AddressDTO addressdto){
     if(addressdto == null) throw new ArgumentNullException(nameof(addressdto));
     return new Address
     {
        Line1 = addressdto.Line1,
        Line2 = addressdto.Line2,
        City = addressdto.City,
        Country = addressdto.Country,
        PostalCode = addressdto.PostalCode
     };

  }
    public static void UpdateAddress(this Address address , AddressDTO addressDTO){
     if(addressDTO == null) throw new ArgumentNullException(nameof(addressDTO));
        if(address == null) throw new ArgumentNullException(nameof(address));

         address.Line1 = addressDTO.Line1;
        address.Line2 = addressDTO.Line2;
        address.City = addressDTO.City;
        address.Country = address.Country;
        address.PostalCode = addressDTO.PostalCode;
     

  }
}
