using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Abstraction
{
  public interface IAuthSecuredKey
  {
    string Key { get; set; }
  }
}
