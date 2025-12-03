using UnityEngine;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class FiveTeacherFinder : MonoBehaviour
{
    private string htlLink = "https://www.htl-salzburg.ac.at/lehrerinnen.html";

    async void Start()
    {
        await FetchFiveTeachers();
    }

    async Task FetchFiveTeachers()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string html = await client.GetStringAsync(htlLink);

                // Regex: Namen sind in der Form "Nachname Vorname, Titel"
                Regex regex = new Regex(@"\b([A ][a-zA-Zäöüß]+ [A-Z][a-zA-Zäöüß]+, [^<\n\r]+)\b");
                MatchCollection matches = regex.Matches(html);

                Debug.Log("Erste 5 Lehrer:");
                for (int i = 0; i < Mathf.Min(5, matches.Count); i++)
                {
                    Debug.Log(matches[i].Groups[1].Value.Trim());
                }
            }
            catch (HttpRequestException e)
            {
                Debug.LogError("Fehler beim Abrufen der Seite: " + e.Message);
            }
        }
    }
}
