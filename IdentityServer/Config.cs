// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        /// <summary>
        /// Scopes used for protecting Api Resources
        /// </summary>
        /// <value></value>
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("access-protected-api", "Access to protected api"),
            };

        /// <summary>
        /// Client applications registered with STS
        /// </summary>
        /// <value></value>
        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client{
                    ClientId = "daemon-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets = {
                        new Secret("daemon-client-secret".Sha256(),"Daemon client secret")
                    },
                    AllowedScopes = {"access-protected-api"}
                },
            };
    }
}