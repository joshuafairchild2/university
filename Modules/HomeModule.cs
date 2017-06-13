using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Nancy;
using University.Objects;

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
