using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Station.Helper;

namespace Station.Models
{
    public class DtoParameter
    {
        public string Fields { get; set; }

        public string OrderBy { get; set; }

        [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        public IEnumerable<string> Ids { get; set; }
    }
}