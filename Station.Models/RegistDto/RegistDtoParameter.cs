using Microsoft.AspNetCore.Mvc;
using Station.Helper;

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