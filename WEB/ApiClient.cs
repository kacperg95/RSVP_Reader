using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace WEB
{
    public class ApiClient(HttpClient _httpClient, IJSRuntime _js)
    {
        private string? _cachedUserId;

        public async IAsyncEnumerable<string> GetStreamingTextAsync(string language, string topic)
        {
            var url = $"/GetAnInterestingText?language={Uri.EscapeDataString(language)}&topic={Uri.EscapeDataString(topic)}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Client-Id", await GetUserId());
            request.SetBrowserResponseStreamingEnabled(true);
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                yield return "You have exceeded your query limit. Please try again later.";
                yield break;
            }

            response.EnsureSuccessStatusCode();
            var stream = response.Content.ReadFromJsonAsAsyncEnumerable<string>();
            string leftover = "";

            //Getting data in chunks and yielding complete words as they arrive, while handling the case where a word might be split across chunks.
            await foreach (var chunk in stream)
            {
                if (string.IsNullOrEmpty(chunk)) continue;

                string currentContent = leftover + chunk;
                var parts = currentContent.Split([' ', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0) continue;

                bool endsWithSpace = char.IsWhiteSpace(chunk[^1]);
                int limit = endsWithSpace ? parts.Length : parts.Length - 1;

                for (int i = 0; i < limit; i++)
                    yield return parts[i];

                leftover = endsWithSpace ? "" : parts[^1];
            }

            if (!string.IsNullOrWhiteSpace(leftover))
                yield return leftover;
        }


        private async Task<string> GetUserId()
        {
            if (!string.IsNullOrEmpty(_cachedUserId))
                return _cachedUserId;

            var localStorgeId = await _js.InvokeAsync<string>("localStorage.getItem", "rsvp_user_id");
            if (string.IsNullOrEmpty(localStorgeId))
            {
                localStorgeId = Guid.NewGuid().ToString();
                await _js.InvokeVoidAsync("localStorage.setItem", "rsvp_user_id", localStorgeId);
            }

            _cachedUserId = localStorgeId;
            return _cachedUserId;
        }
    }
}