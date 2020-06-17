using System;

namespace Station.Models.RegistDto
{
    public class RegistUpdateDto
    {
        public DateTime RegistDate { set; get; }

        public string MaintainNumber { set; get; }

        public string CustomName { get; set; }

        public string Address { get; set; }

        public string Linkman { get; set; }

        public string TelPhone { get; set; }

        public string Fax { get; set; }
    }
}