using BurgerRaterApi.Models.Dto.Restaurant;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRaterApi.Tests.Comparers
{
    public class RestaurantResponseDtoComparer : IEqualityComparer<RestaurantResponseDto>
    {
        public static readonly IEqualityComparer<RestaurantResponseDto> Default = new RestaurantResponseDtoComparer();

        public bool Equals(RestaurantResponseDto x, RestaurantResponseDto y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Id == y.Id 
                && x.Name == y.Name
                && x.Address == y.Address
                && x.City == y.City
                && x.ZipCode == y.ZipCode
                && x.District == y.District
                && x.ClosingTime == y.ClosingTime
                && x.OpeningTime == y.OpeningTime;
        }

        public int GetHashCode([DisallowNull] RestaurantResponseDto obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.Name);
            hashCode.Add(obj.Address);
            hashCode.Add(obj.City); 
            hashCode.Add(obj.ZipCode);
            hashCode.Add(obj.ClosingTime);
            hashCode.Add(obj.OpeningTime);

            return hashCode.ToHashCode();
        }
    }
}
