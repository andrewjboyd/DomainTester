using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DomainTester
{
    public static class JSInteropExtension
    {
        public static ValueTask SetFocusAsync(this IJSRuntime jsRuntime, ElementReference elementReference)
        {
            return jsRuntime.InvokeVoidAsync("setFocusByElement", elementReference);
        }
    }
}
