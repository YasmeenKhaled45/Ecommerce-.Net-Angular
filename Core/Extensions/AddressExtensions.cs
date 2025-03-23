using System;
using Core.Entities;

namespace Core.Extensions;

public static class AddressExtensions
{
    public static object toDto(this Address address)
    {
        if (address == null) return null;
        return new
        {
            address.Line1,
            address.Line2,
            address.City,
            address.Country,
            address.PostalCode
        };
    }
}

