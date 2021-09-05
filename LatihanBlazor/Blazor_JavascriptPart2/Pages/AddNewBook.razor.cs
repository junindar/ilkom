using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor_JavascriptPart2.Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor_JavascriptPart2.Pages
{
    public partial class AddNewBook 
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        public Book Book { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
            Book = new Book();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("blazorInterop.focusElementById", "Judul");
            }

        }
    }
}
