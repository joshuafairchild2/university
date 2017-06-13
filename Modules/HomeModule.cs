using System;
using System.Collections.Generic;
using Nancy;
using University.Objects;
// using System.Data;
// using System.Data.SqlClient;

namespace University
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
    }
  }
}
