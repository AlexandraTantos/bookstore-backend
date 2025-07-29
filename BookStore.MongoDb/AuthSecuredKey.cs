using BookStore.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain
{
  public class AuthSecuredKey : IAuthSecuredKey
  {
    public string Key { get; set; }
  }
}
