using SqlSugar;
using System;

namespace JobManagerSystem.Core.Business.Info
{
    /// <summary>
    /// 内外物料关系 表
    /// </summary>
    [SugarTable("fm_material_interandext", "内外物料关系")]
    public class MaterialInterandext
    {
        public MaterialInterandext()
        {
        }

        /// <summary>
        /// Desc:产线code
        /// </summary>
        [SugarColumn(ColumnName = "line_code", ColumnDescription = "产线code", IsNullable = true, Length = 64, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string LineCode { get; set; }

        /// <summary>
        /// Desc:产线name
        /// </summary>
        [SugarColumn(ColumnName = "line_name", ColumnDescription = "产线name", IsNullable = true, Length = 64, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string LineName { get; set; }

        /// <summary>
        /// Desc:供应商物料号
        /// </summary>
        [SugarColumn(ColumnName = "material_code_supplier", ColumnDescription = "供应商物料号", IsNullable = true, Length = 255, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string MaterialCodeSupplier { get; set; }

        /// <summary>
        /// Desc:供应商物料名称
        /// </summary>
        [SugarColumn(ColumnName = "material_name_supplier", ColumnDescription = "供应商物料名称", IsNullable = true, Length = 255, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string MaterialNameSupplier { get; set; }

        /// <summary>
        /// Desc:SAP物料号
        /// </summary>
        [SugarColumn(ColumnName = "material_code_sap", ColumnDescription = "SAP物料号", IsNullable = true, Length = 255, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string MaterialCodeSap { get; set; }

        /// <summary>
        /// Desc:SAP物料名称
        /// </summary>
        [SugarColumn(ColumnName = "material_name_sap", ColumnDescription = "SAP物料名称", IsNullable = true, Length = 255, ColumnDataType = "varchar", DecimalDigits = 0)]
        public string MaterialNameSap { get; set; }
    }
}
