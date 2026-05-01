using System.Collections.Generic;
using System.Text;

public static class CSVReader
{
    public static List<string[]> ReadCSV(string csvText)
    {
        List<string[]> rows = new List<string[]>();
        List<string> currentRow = new List<string>();
        StringBuilder currentValue = new StringBuilder();

        bool insideQuotes = false;

        for (int i = 0; i < csvText.Length; i++)
        {
            char c = csvText[i];

            if (c == '"')
            {
                insideQuotes = !insideQuotes;
            }
            else if (c == ',' && !insideQuotes)
            {
                currentRow.Add(currentValue.ToString());
                currentValue.Clear();
            }
            else if ((c == '\n' || c == '\r') && !insideQuotes)
            {
                if (currentValue.Length > 0 || currentRow.Count > 0)
                {
                    currentRow.Add(currentValue.ToString());
                    rows.Add(currentRow.ToArray());

                    currentRow.Clear();
                    currentValue.Clear();
                }

                if (c == '\r' && i + 1 < csvText.Length && csvText[i + 1] == '\n')
                {
                    i++;
                }
            }
            else
            {
                currentValue.Append(c);
            }
        }

        if (currentValue.Length > 0 || currentRow.Count > 0)
        {
            currentRow.Add(currentValue.ToString());
            rows.Add(currentRow.ToArray());
        }

        return rows;
    }
}