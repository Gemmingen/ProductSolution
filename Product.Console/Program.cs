using System.Diagnostics;

// Erstellt eine einzige Instanz von HttpClient, um sie wiederzuverwenden.
using var client = new HttpClient();

//Ersetzen Sie XXXX durch den Port, auf dem Ihre API läuft.
string apiUrl = "https://localhost:7142/api/product";
int numberOfRequests = 100;

Console.WriteLine($"Starte {numberOfRequests} Anfragen an: {apiUrl}");

// Eine Liste, um alle Anfragen-Tasks zu speichern.
var tasks = new List<Task<HttpResponseMessage>>();

// Startet die Zeitmessung.
var stopwatch = Stopwatch.StartNew();

// Diese Schleife startet alle Anfragen, ohne auf deren Beendigung zu warten.
for (int i = 0; i < numberOfRequests; i++)
{
    // Die Anfrage wird gestartet und der Task zur Liste hinzugefügt.
    tasks.Add(client.GetAsync(apiUrl));
}

// Task.WhenAll wartet, bis ALLE Tasks in der Liste abgeschlossen sind.
// Hier laufen die 100 Anfragen parallel.
HttpResponseMessage[] responses = await Task.WhenAll(tasks);

// Stoppt die Zeitmessung.
stopwatch.Stop();

// Zählt die erfolgreichen und fehlgeschlagenen Anfragen.
int successfulRequests = responses.Count(r => r.IsSuccessStatusCode);
int failedRequests = responses.Count(r => !r.IsSuccessStatusCode);

Console.WriteLine("\n--- Ergebnisse ---");
Console.WriteLine($"Test abgeschlossen in: {stopwatch.Elapsed.TotalSeconds:F2} Sekunden");
Console.WriteLine($"Erfolgreiche Anfragen: {successfulRequests}");
Console.WriteLine($"Fehlgeschlagene Anfragen: {failedRequests}");

// Berechnet die Anfragen pro Sekunde (ein wichtiger Leistungsindikator).
double requestsPerSecond = successfulRequests / stopwatch.Elapsed.TotalSeconds;
Console.WriteLine($"Anfragen pro Sekunde (RPS): {requestsPerSecond:F2}");