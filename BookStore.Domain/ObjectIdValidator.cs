using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain
{
  public static class ObjectIdValidator
  {
    public static bool IsValidObjectId(this string id)
    {
      if (ObjectId.TryParse(id, out var value)) //1434234523543254 -> value=ObjectId(1434234523543254)
      {
        return true;
      }
      return false;
    }
  }
}
