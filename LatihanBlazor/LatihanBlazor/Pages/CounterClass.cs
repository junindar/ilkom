using Microsoft.AspNetCore.Components;

namespace LatihanBlazor.Pages
{
    public class CounterClass : ComponentBase
    {
        public int CurrentCount { get; set; }

        public void IncrementCount()
        {
            CurrentCount += 5;
        }
    }
}
