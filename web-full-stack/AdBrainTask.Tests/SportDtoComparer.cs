using AdBrainTask.Dtos.Response;
using System;
using System.Collections.Generic;

namespace AdBrainTask.Tests
{
    class SportDtoComparer : IEqualityComparer<Sport>
    {
        public bool Equals(Sport x, Sport y)
        {
            //Check whether the objects are the same object.  
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal.  
            return x != null && y != null
                && x.Id.Equals(y.Id) && x.Author.Equals(y.Author)
                && x.Domain.Equals(y.Domain) && x.DateCreated.Equals(y.DateCreated)
                && x.Title.Equals(y.Title) && x.Url.Equals(y.Url);
        }

        public int GetHashCode(Sport obj)
        {
            //Get hash code for the Title field if it is not null.  
            int hashProductTitle = obj.Title == null ? 0 : obj.Title.GetHashCode();

            //Get hash code for the Id field.  
            int hashProductId = obj.Id.GetHashCode();

            //Calculate the hash code for the product.  
            return hashProductTitle ^ hashProductId;
        }
    }
}
