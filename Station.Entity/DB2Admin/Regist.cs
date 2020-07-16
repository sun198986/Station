using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Station.Entity.DB2Admin
{
    [Db2AdminTable("TestRegist")]
    public class Regist
    {
        [Key,Column("REGISTID")]
        public string RegistId { set; get; }

        public DateTime? RegistDate { set; get; }

        public string MaintainNumber { set; get; }

        public string CustomName { get; set; }

        public string Address { get; set; }

        public string Linkman { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        // public string OM1 { set; get; }

        // public string CustomerName { set; get; }

        // public string CustomerAddress { set; get; }

        // public string CustomerLinkman { set; get; }

        // public string CustomerPhone { set; get; }

        // public string CustomerFax { set; get; }

        // public int ProductTypeID { set; get; }

        // public string ModelCode { set; get; }

        // public string ModelName { set; get; }

        // public string SerialNumber { set; get; }

        // public string WarrantyNo { set; get; }

        // public string GlobalEncoding { set; get; }

        // public int GroupID { set; get; }

        // public string NcModelCode { set; get; }

        // public string NcSerialNumber { set; get; }

        // public string EquipmentSeries { set; get; }

        // public string ManufacturerName { set; get; }

        // public string EquipmentModel { set; get; }

        // public string EquipmentSerial { set; get; }

        // public DateTime InstallationDate { set; get; }

        // public int ReturnInstructID { set; get; }
        // public DateTime OM2 { set; get; }

        // //public EWarrantyState WarrantyState { set; get; }

        // public DateTime FaultDateTime { set; get; }

        // public string Engineer { set; get; }

        // public string PartExchange { set; get; }

        // public string FaultDescription { set; get; }

        // public string CustomerRequest { set; get; }

        // public int FrequencyID { set; get; }

        // public ETemperature TemperatureID { set; get; }

        // public bool IsCharge { set; get; }

        // public bool IsPayed { set; get; }

        // public string CreatedUser { set; get; }

        // public string CreatedUserName { set; get; }

        //// public DateTime CreatedDateTime { set; get; }

        // public string UpdatedUser { set; get; }

        // public DateTime UpdatedDateTime { set; get; }

        // public int ItemsSourceID { set; get; }
        // //
        // //public bool IsPrimary { set; get; }

        // public string PAppAdvice { set; get; }

        // public string PAppUser { set; get; }

        // public DateTime? PAppDateTime { set; get; }

        // public string PUpdatedUser { set; get; }

        // public DateTime? PUpdatedDateTime { set; get; }

        // public bool IsRepaired { set; get; }

        // public DateTime? RepairedDate { set; get; }

        // public bool IsMaintain { set; get; }

        // public DateTime? MaintainDate { set; get; }

        // public bool IsQuotation { set; get; }

        // public DateTime? QuotationDate { set; get; }

        // public bool IsShipping { set; get; }

        // public DateTime? ShippingDate { set; get; }

        // public ESubmitState SubmitState { set; get; }

        // public DateTime? WarrantyStartDate { set; get; }

        // public DateTime? WarrantyEndDate { set; get; }

        // public EStationAppState PAppState { set; get; }
        // //
        // //public bool IsFinally { set; get; }

        // public EStationAppState FAppState { set; get; }

        // public string FAppAdvice { set; get; }

        // public string FAppUser { set; get; }

        // public DateTime? FAppDateTime { set; get; }

        // public string FUpdatedUser { set; get; }

        // public DateTime? FUpdatedDateTime { set; get; }

        // public string ProductOrigin { set; get; }

        // public string Other { set; get; }

        // public DateTime? SendDate { set; get; }

        // public bool IsReturn { set; get; }

        // public bool IsClosed { set; get; }

        // public int StationID { set; get; }

        // public string StationNo { set; get; }

        //// public List<StationRegistAccessoryInfo> Accessories { set; get; }

        // public string Group { set; get; }

        // public string ProductType { set; get; }

        // public string StationName { set; get; }

        // public string SaleOrgan { set; get; }

        // public decimal TotalAmount { set; get; }

        // public string ReturnState { set; get; }

        // public bool AccessoryEdit { set; get; }
        // public bool QuotationEdit { set; get; }
        // public bool MaintainEdit { set; get; }
    }
}