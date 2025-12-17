using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public static class AddressablesLoader
{
    // 비동기 리소스 로드 (로드후 Addressables.Release(개체);로 해제)
    public static async Task<T> AsyncResourceLoadByLabel<T>(string label) where T : Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var location in locations)
        {
            var loadOperation = Addressables.LoadAssetAsync<T>(location);
            await loadOperation.Task;

            if (loadOperation.Status == AsyncOperationStatus.Succeeded)
            {
                // 리소스 로드에 성공한 경우 로드된 리소스를 반환
                return loadOperation.Result;
            }
        }

        // 모든 로케이션에서 로드에 실패한 경우 예외를 throw하거나 다른 처리를 수행
        throw new Exception($"Failed to load resource with label: {label}");
    }

    // 동기 리소스 로드 (로드후 Addressables.Release(개체);로 해제)
    private static T SyncResourceLoadByLabel<T>(string label) where T : Object
    {
        var op = Addressables.LoadAssetAsync<T>(label);
        op.WaitForCompletion();

        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            // 리소스 로드에 성공한 경우 로드된 리소스를 반환
            return op.Result;
        }
        else
        {
            Debug.LogError($"Failed to load asset from label: {label}");
            return null;
        }
    }
}
