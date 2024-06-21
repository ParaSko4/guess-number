using System;
using System.Collections.Generic;

namespace GuessNumber
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance;

        private Dictionary<Type, IService> serviceDictionary = new();

        static ServiceLocator()
        {
            Instance = new ServiceLocator();
        }

        public void Register<T>(T service) where T : IService
        {
            serviceDictionary[typeof(T)] = service;
        }

        public T Get<T>() where T : IService
        {
            return (T)serviceDictionary[typeof(T)];
        }
    }
}