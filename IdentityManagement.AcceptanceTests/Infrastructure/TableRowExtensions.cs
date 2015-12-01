using System.Text;
using TechTalk.SpecFlow;

namespace Affecto.IdentityManagement.AcceptanceTests.Infrastructure
{
    internal static class TableRowExtensions
    {
        public static string ToTableString(this TableRow tableRow)
        {
            StringBuilder stringBuilder = new StringBuilder("| ");
            foreach (string column in tableRow.Values)
            {
                stringBuilder.Append(column);
                stringBuilder.Append(" | ");
            }
            return stringBuilder.ToString();
        }
    }
}
