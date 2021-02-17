using System.Collections;
using System.Data;
using System.Linq;

namespace SideBySideFullStackedBarChart {
    class AgeStructureDataReader {
        static DataTable ageStructureTable;
        static DataTable AgeStructureTable {
            get {
                if (ageStructureTable == null)
                    ageStructureTable = LoadPopulationAgeStructure();
                return ageStructureTable;
            }
        }
        static DataTable LoadPopulationAgeStructure() {
            return LoadDataTableFromXml(@"..\..\Data\Population.xml", "Population");
        }
        internal static DataTable LoadDataTableFromXml(string fileName, string tableName) {
            DataSet xmlDataSet = new DataSet();
            xmlDataSet.ReadXml(fileName);
            return xmlDataSet.Tables[tableName];
        }
        internal static IList GetDataByAgeAndGender() {
            return AgeStructureTable.AsEnumerable()
                .Select(row => new {
                    GenderAge = new GenderAgeInfo(row.Field<string>("Gender"), row.Field<string>("Age")),
                    Country = row.Field<string>("Country"),
                    Population = row.Field<long>("Population")
                }).ToList();
        }
    }
}
