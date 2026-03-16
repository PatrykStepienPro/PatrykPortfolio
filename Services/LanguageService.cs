using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace PatrykPortfolio.Services;

public class LanguageService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private Dictionary<string, string> _strings = new();

    public Lang Current { get; private set; } = Lang.En;
    public bool IsLoaded { get; private set; }
    public event Action? OnChange;

    public LanguageService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task InitAsync()
    {
        var saved = await _js.InvokeAsync<string?>("localStorage.getItem", "lang");
        if (Enum.TryParse<Lang>(saved, ignoreCase: true, out var lang))
            Current = lang;

        await LoadStringsAsync();
    }

    public async Task SetLanguageAsync(Lang lang)
    {
        Current = lang;
        await _js.InvokeVoidAsync("localStorage.setItem", "lang", lang.ToString());
        await LoadStringsAsync();
        OnChange?.Invoke();
    }

    public string Get(string key)
        => _strings.TryGetValue(key, out var val) ? val : $"[{key}]";

    private async Task LoadStringsAsync()
    {
        var file = Current == Lang.En ? "i18n/en.json" : "i18n/pl.json";
        _strings = await _http.GetFromJsonAsync<Dictionary<string, string>>(file) ?? new();
        IsLoaded = true;
    }
}
