﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;
using AccountWithdrawalActor.Interfaces;
using System.Configuration;

namespace AccountWithdrawalActor
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // This line registers an Actor Service to host your actor class with the Service Fabric runtime.
                // The contents of your ServiceManifest.xml and ApplicationManifest.xml files
                // are automatically populated when you build this project.
                // For more information, see https://aka.ms/servicefabricactorsplatform

                var repository = new AccountWithdrawalRepository(ConfigurationManager.ConnectionStrings["MongoDefault"].ConnectionString);

                ActorRuntime.RegisterActorAsync<AccountWithdrawalActor>(
                   (context, actorType) => new ActorService(context, actorType, (svc, id) => new AccountWithdrawalActor(svc, id, repository))).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}