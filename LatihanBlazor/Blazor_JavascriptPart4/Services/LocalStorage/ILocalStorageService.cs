﻿using System.Threading.Tasks;

namespace Blazor_JavascriptPart4.Services.LocalStorage
{
  public interface ILocalStorageService
  {
    Task SetItemAsync<T>(string key, T item);

    Task<T> GetItemAsync<T>(string key);
  }
}