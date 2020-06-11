namespace Station.Common
{
    /// <summary>
    /// 打印状态
    /// </summary>
    public enum EPrintState { 未打印, 已打印 }

    public enum EOperate { 添加, 修改, 删除, 查询, 审批, 弃审, 备用1, 备用2, 备用3 }


    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EAppState { 待审核, 审核通过, 审核不通过 }

    /// <summary>
    /// 确认状态
    /// </summary>
    public enum EConfirmState { 待确认, 同意, 有异议 }

    /// <summary>
    /// 提交状态
    /// </summary>
    public enum ESubmitState { 未提交, 已提交 }

    /// <summary>
    /// 有效状态
    /// </summary>
    public enum EValidState { 有效, 作废 }

    /// <summary>
    /// 放行状态
    /// </summary>
    public enum EReleaseState { 未放行, 已放行 }

    /// <summary>
    /// 开票状态
    /// </summary>
    public enum EBackState { 全部, 未开票, 已开票 }

    /// <summary>
    /// 保修状态
    /// </summary>
    public enum EWarrantyState { 保外, 保内 }

    /// <summary>
    /// 邮寄状态
    /// </summary>
    public enum EMailState { 待邮寄, 邮寄完成 }

    /// <summary>
    /// 备件状态
    /// </summary>
    public enum EPartState { 无, 有, 缺 }

    /// <summary>
    /// 传真发送状态
    /// </summary>
    public enum EFaxState { 未发送, 已发送 }

    /// <summary>
    /// 单据关闭状态
    /// </summary>
    public enum EClosedState { 无, 关闭, 维修报告 }

    /// <summary>
    /// 单据结案状态
    /// </summary>
    public enum EConcludedState { 未结案, 已结案 }

    /// <summary>
    /// 单据的处理流程
    /// </summary>
    public enum EReceiveState { 待领取, 领取, 退回 }

    /// <summary>
    /// 维修单状态
    /// </summary>
    public enum EProcessState { 未开始, 进行中, 已完成 }

    /// <summary>
    /// 付款确认状态
    /// </summary>
    public enum EPayConfirmState { 未确认, 已确认 }

    /// <summary>
    /// 担保状态
    /// </summary>
    public enum EGuaranteeState { 担保中, 担保完成 }

    /// <summary>
    /// 提交状态
    /// </summary>
    public enum EUseState { 未启用, 已启用, 关闭 }



    /// <summary>
    /// 申请类别
    /// </summary>
    public enum EChargeReqType { 修改, 新增, 关闭 }

    /// <summary>
    /// 条件类别
    /// </summary>
    public enum EConditionType { 开始, 结尾, 包含, 等于 }

    /// <summary>
    /// 锁定类别
    /// </summary>
    public enum ELockType { 无, 锁定, 解锁, 刷新 }

    /// <summary>
    /// 备件使用方式
    /// </summary>
    public enum EPartUseType { 报价, 内耗, 借用 }

    /// <summary>
    /// 附件类别
    /// </summary>
    public enum EAccessoryType { 图像文件, 一般文件 }

    /// <summary>
    /// 开票类别
    /// </summary>
    public enum EInvoiceType { 维修类开票, 其它类开票 }

    /// <summary>
    /// 客户约定类别
    /// </summary>
    public enum EAgreementType { 二十四小时, 四十八小时, 自定义 }

    /// <summary>
    /// 信用类别
    /// </summary>
    public enum ECreditType { 现金客户, 信用客户 }

    /// <summary>
    /// 放行类别
    /// </summary>
    public enum EReleaseType { 无, 余额充足, 信用审批, 放单审批, 收款 }

    /// <summary>
    /// 单据类别
    /// </summary>
    public enum EPartBillType { 领用, 归还 }

    /// <summary>
    /// 部品领用类别
    /// </summary>
    public enum EReceiveType { 领用, 领用后邮寄, 邮寄 }

    /// <summary>
    /// 维修类别
    /// </summary>
    public enum ERegistType { 内勤, 外勤, 维修站 }

    /// <summary>
    /// 借用归还类别
    /// </summary>
    public enum EBorrowType { 依赖票, Claim }

    /// <summary>
    /// 备件来源
    /// </summary>
    public enum EOriginType { 非代理商, 代理商 }


    /// <summary>
    /// 温度枚举
    /// </summary>
    public enum ETemperature { 无, 低温, 高温 }

    /// <summary>
    /// 维修优先级
    /// </summary>
    public enum EPriority { 高, 中, 低 }

    /// <summary>
    /// 客户性质
    /// </summary>
    public enum ECharacterType { 私营, 合资, 国营, 民营, }

    /// <summary>
    /// 树形级别
    /// </summary>
    public enum ETreeLevelType { 末级, 父级 }


    public enum EEditType { 无, 查询, 新增, 修改, 删除 }

    /// <summary>
    /// 审批的功能模块
    /// </summary>
    public enum EAppAction
    {
        无,
        检测报告,
        备件清单,
        内勤报价单,
        外勤报价单,
        内勤维修报告技术审核,
        FA维修报告技术审核,
        NC维修报告技术审核,
        EDM维修报告技术审核,
        Laser维修报告技术审核,
        信用额度放行,
        内勤结案,
        NC结案,
        EDM结案,
        FA结案,
        Laser结案,
        结案内耗审核,
        开票申请,
        保修开票申请,
        库存调拨申请,
        保内初审,
        保内终审,
        标准收费修改申请,
        保修信息修改申请
    }

    /// <summary>
    /// 外勤维修登记查询类别
    /// </summary>
    public enum EOnsiteRegistQueryType { 无, 备件清单, 报价单, 维修报告, 部件领用, 部件邮寄, 部件归还, 开票申请 }

    /// <summary>
    /// 内勤维修登记查询类别
    /// </summary>
    public enum EWorkshopRegistQueryType { 无, 检测报告, 报价单, 维修报告, 付款方确认, 装箱发货, 部件领用, 部件邮寄, 部件归还, 开票申请 }

    /// <summary>
    /// 维修站维修登记查询类别
    /// </summary>
    public enum EStationRegistQueryType { 无, 报价单, 维修报告 }

    #region Station

    /// <summary>
    /// 审核类型
    /// </summary>
    public enum EStationAppType { 初审, 终审, 审核 }

    /// <summary>
    /// 审核类型
    /// </summary>
    public enum EStationAppState { 待审核, 审核通过, 终止, 退回 }

    #endregion


    #region SAP

    /// <summary>
    /// 类别
    /// </summary>
    public enum EBillType { 内勤, 外勤, 借用, 发票 }

    /// <summary>
    /// SAP操作状态
    /// </summary>
    public enum ESapState { 无, 申请, 成功, 失败 }

    /// <summary>
    /// 操作状态
    /// </summary>
    public enum EOperateState { 可操作, 不可操作 }

    /// <summary>
    /// MASS单据类别
    /// </summary>
    public enum EServiceType { 维修, 技术服务, 培训 }

    /// <summary>
    /// 预留单申请类别
    /// </summary>
    public enum EReserveType { 新增, 修改, 领用, 释放 }

    /// <summary>
    /// 预留单状态
    /// </summary>
    public enum EReserveState { 预留, 释放, 领用 }

    /// <summary>
    /// DebitMemo申请类别
    /// </summary>
    public enum EDebitMemoType { 新增, 修改, 修改2, 释放, 结案 }

    /// <summary>
    /// 移库单申请类别
    /// </summary>
    public enum ETransferType { 新增, 修改, 预留领用 }

    #endregion
}
