using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace AuthorizationServer
{
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service"),
                new ApiResource("productservice", "CAS Product Service"), 
                new ApiResource("agentservice", "CAS Agent Service"), 
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new[]
                    {
                        new Secret("clientsecret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] {"clientservice"}
                },
                new Client()
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new []
                    {
                        new Secret("productsecret".Sha256()), 
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                }, 
                new Client()
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new []
                    {
                        new Secret("agentsecert".Sha256()), 
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] {"agentservice", "clientservice", "productservice"}
                }, 
                new Client()
                {
                    ClientId = "cas.mvc.client.implicit",
                    ClientName = "CAS MVC Web App Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {$"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signin-oidc"},
                    PostLogoutRedirectUris = {$"http://{Configuration["Clients:MvcClient:IP"]}:{Configuration["Clients:MvcClient:Port"]}/signout-callback-oidc"},
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "agentservice", "clientservice", "productservice"
                    },
                    AllowAccessTokensViaBrowser = true
                }, 
                new Client
                {
                    ClientId = "mvc_code",
                    ClientName = "MVC Code Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = {$"http://{Configuration["Clients:MvcCodeClient:IP"]}:{Configuration["Clients:MvcCodeClient:Port"]}/signin-oidc" },
                    PostLogoutRedirectUris = {$"http://{Configuration["Clients:MvcCodeClient:IP"]}:{Configuration["Clients:MvcCodeClient:Port"]}/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "agentservice", "clientservice", "productservice"
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true
                }
            };
        }


        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword",
                    Claims = new []
                    {
                        new Claim("email", "edison@hotmail.com"), 
                    }
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword",
                    Claims = new []
                    {
                        new Claim("email", "andy@hotmail.com"), 
                    }
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword",
                    Claims = new []
                    {
                        new Claim("email", "leo@hotmail.com"), 
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
