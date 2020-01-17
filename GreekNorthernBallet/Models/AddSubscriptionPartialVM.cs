using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class AddSubscriptionPartialVM
  {
    public List<DanceCategory> categories { get; set; }

    public Users users { get; set; }
  }
}
