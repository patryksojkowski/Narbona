﻿namespace Narbona.Models
{
  public class PersonViewModel
  {
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public IList<string> Emails { get; set; } = new List<string>();
  }
}
