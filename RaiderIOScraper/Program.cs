using HtmlAgilityPack;
using RaiderIOScraper;
using System.Web;
using CsvHelper;
using System.Globalization;


// Create a new HtmlWeb instance
var web = new HtmlWeb();
var api = new RaiderIOAPI();
var cutoff = 3637.3m;
var currentCutoff = 0m;
var characterDataList = new List<CharacterInfo>();

for (var i = 0; i < 45; i++)
{
    var url = $"https://raider.io/mythic-plus-character-rankings/season-df-3/us/all/all/{i}#content";
    var document = web.Load(url);

    var tbodyNode = document.DocumentNode.SelectNodes("//tbody")[0];
    // Process each <tbody> node and extract rows
    var rows = ExtractRowsFromTBody(tbodyNode);

    // Display the rows
    foreach (var row in rows)
    {
        try
        {
            var name = row.Item1;
            var realm = row.Item2;
            var ioScore = decimal.Parse(row.Item3);
            currentCutoff = ioScore;

            if (currentCutoff < cutoff)
            {
                break;
            }

            var characterData = api.GetCharacterInfo(realm, name);

            var cClass = characterData.Class;

            var healerRank = characterData.mythic_plus_ranks.healer?.region ?? -1;
            var tankRank = characterData.mythic_plus_ranks.tank?.region ?? -1;
            var dpsRank = characterData.mythic_plus_ranks.dps?.region ?? -1;

            if (healerRank <= 0) { healerRank = int.MaxValue; }
            if (tankRank <= 0) { tankRank = int.MaxValue; }
            if (dpsRank <= 0) { dpsRank = int.MaxValue; }

            var maxRank = Math.Min(Math.Min(healerRank, tankRank), dpsRank);

            var role = maxRank switch
            {
                var _ when maxRank == healerRank => Roles.Healer,
                var _ when maxRank == tankRank => Roles.Tank,
                _ => Roles.DPS,
            };

            var characterInfo = new CharacterInfo(name, realm, ioScore, cClass, role);
            characterDataList.Add(characterInfo);
        }
        catch { continue; }
    }

    if (currentCutoff < cutoff)
    {
        break;
    }
}

WriteToCsv(characterDataList);


static List<Tuple<string, string, string>> ExtractRowsFromTBody(HtmlNode tbodyNode)
{
    var rows = new List<Tuple<string, string, string>>();

    // Select all <tr> nodes within the <tbody>
    var rowNodes = tbodyNode.SelectNodes(".//tr");

    if (rowNodes != null)
    {
        foreach (var rowNode in rowNodes)
        {
            try
            {
                var playerNameRealm = SplitNameRealm(rowNode.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText);
                var playerIO = rowNode.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[2].InnerText;
                // Extract the inner text of each row and add it to the list
                string rowText = rowNode.InnerText.Trim();
                rows.Add(new Tuple<string, string, string>(playerNameRealm.Item1, HttpUtility.HtmlDecode(playerNameRealm.Item2), playerIO));
            }
            catch(Exception ex)
            {
                continue;
            }
        }
    }

    return rows;
}

static (string, string) SplitNameRealm(string inputText)
{
    var split = "(US)";

    var result = inputText.Split(split);
    var name = result[0];
    var realm = result[1];
    return (name, realm);
}

static void WriteToCsv(List<CharacterInfo> characterInfo)
{
    var tanks = characterInfo.Where(x => x.Role == Roles.Tank).ToList();
    var healers = characterInfo.Where(x => x.Role == Roles.Healer).ToList();
    var dps = characterInfo.Where(x => x.Role == Roles.DPS).ToList();

    // Create StreamWriter
    using (var writer = new StreamWriter("tanks.csv"))
    {
        // Create CsvWriter
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Write records to CSV
            csv.WriteRecords(tanks);
        }
    }

    // Create StreamWriter
    using (var writer = new StreamWriter("healers.csv"))
    {
        // Create CsvWriter
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Write records to CSV
            csv.WriteRecords(healers);
        }
    }

    // Create StreamWriter
    using (var writer = new StreamWriter("dps.csv"))
    {
        // Create CsvWriter
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            // Write records to CSV
            csv.WriteRecords(dps);
        }
    }
}


static List<CharacterInfo> ReadFromCsv(string filePath)
{
    // Create StreamReader
    using (var reader = new StreamReader(filePath))
    {
        // Create CsvReader
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Read records from CSV into a list of Person objects
            return csv.GetRecords<CharacterInfo>().ToList();
        }
    }
}