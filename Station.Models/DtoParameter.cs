using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Station.Helper;
using Station.Helper.Extensions;

namespace Station.Models
{
    public abstract class DtoParameter
    {
        public string Fields { get; set; }

        public string OrderBy { get; set; }

        [ModelBinder(BinderType = typeof(ArrayModelBinder))]
        public IEnumerable<string> Ids { get; set; }
    }
}