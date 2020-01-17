using System.Collections.Generic;

namespace GreekNorthernBallet.Models
{
  public class UserSearchContextMenuVM
  {
    public PaginatedList<Users> users { get; set; }

    public List<DanceCategory> categories { get; set; }

    public UserCard card { get; set; }

  }
}
