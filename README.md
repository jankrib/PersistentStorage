# PersistentStorage

PersistentStorage for Xamarin .NET Standard 2.0.

To get started install the [NuGet package](https://www.nuget.org/packages/PersistentStorage/)

Inspired by https://github.com/perpetual-mobile/SimpleStorage (Use this instead for shared and PCL)

## Android Setup

Example using Xamarin.Forms.DependencyService:

    AndroidPersistentStorage.SetContext(ApplicationContext);
    Xamarin.Forms.DependencyService.Register<AndroidPersistentStorage>();

## iOS Setup

Example using Xamarin.Forms.DependencyService:

    Xamarin.Forms.DependencyService.Register<iOSPersistentStorage>();

## Retrieving the service

    var storage = Xamarin.Forms.DependencyService.Get<IPersistentStorage>();

## Basic usage

Get the right group

    var group = storage.GetGroup("group name of key/value store");

To store a value

    group.Put<string>("mykey", value); //value is serialized with BinaryFormatter

Retrieve a value

    group.Get<string>("mykey", null); //Second argument is fallback value if key does not exist or deserialization fails


## Async/Await

PutAsync, GetAsync and DeleteAsync.
