// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Test;

namespace AuthTutorial.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("DemoApi")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "AuthWeb",
                    ClientName = "AuthWeb Demo Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44383/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:44383/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
                new Client
                {
                    ClientId = "WebApp",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new [] { new Secret("MySecret".Sha256()) },
                    AllowedScopes = new List<string> { "DemoApi" }
                },
                new Client
                {
                    ClientId = "Spa",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "DemoApi"
                    },
                    RedirectUris = { "https://localhost:44383/SignInCallback.html" },
                    PostLogoutRedirectUris = { "https://localhost:44383/SignOutCallback.html" },
                    AllowedCorsOrigins = { "https://localhost:44383" },
                    RequireConsent = false
                }
            };

        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "testuser",
                Password = "password",
                Claims = new[]
                {
                    new Claim("name", "testuser"),
                }
            }
        };
    }
}