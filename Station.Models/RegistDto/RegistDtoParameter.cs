using Microsoft.AspNetCore.Mvc;
using Station.Helper;
using Station.Helper.Extensions;

namespace Station.Models.RegistDto
{
    public class RegistDtoParameter: DtoParameter
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        [ModelBinder(BinderType = typeof(DtoModelBinder<RegistSearchDto>))]
        public RegistSearchDto Search { get; set; }
    }
}