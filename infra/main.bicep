targetScope = 'resourceGroup'

@minLength(1)
@maxLength(64)
@description('Name of the environment which is used to generate a short unique hash used in all resources.')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

param resourceToken string = '${uniqueString(subscription().subscriptionId, resourceGroup().id)}'

// Optional parameters
param tags object = {}
param apiServiceName string = '${take(environmentName, 6)}-${resourceToken}-api'

// PostgreSQL Server parameters
param pgServerName string = '${take(environmentName, 6)}-${resourceToken}-pg'
param pgDatabaseName string = 'backenddb'
param administratorLogin string = 'postgres'
@secure()
param administratorLoginPassword string

param jwtKey string
param jwtIssuer string
param jwtAudience string

// App Service Plan
var appServicePlanName = '${take(environmentName, 6)}-${resourceToken}-plan'

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: appServicePlanName
  location: location
  tags: tags
  sku: {
    name: 'B1'
  }
  properties: {
    reserved: false
  }
}

// Log Analytics workspace for monitoring
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2024-01-01' = {
  name: '${take(environmentName, 6)}-${resourceToken}-logs'
  location: location
  tags: tags
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

// Application Insights for monitoring
resource appInsights 'Microsoft.Insights/components@2024-01-01' = {
  name: '${take(environmentName, 6)}-${resourceToken}-ai'
  location: location
  tags: tags
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalytics.id
  }
}

// Key Vault for secrets
resource keyVault 'Microsoft.KeyVault/vaults@2024-01-01' = {
  name: '${take(environmentName, 6)}-${resourceToken}-kv'
  location: location
  tags: tags
  properties: {
    enabledForDeployment: true
    enabledForTemplateDeployment: true
    enabledForDiskEncryption: true
    tenantId: subscription().tenantId
    accessPolicies: []
    sku: {
      name: 'standard'
      family: 'A'
    }
  }
}

// PostgreSQL Server
resource postgreSQLServer 'Microsoft.DBforPostgreSQL/flexibleServers@2023-12-01' = {
  name: pgServerName
  location: location
  tags: tags
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
  }
  properties: {
    version: '16'
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
    storage: {
      storageSizeGB: 32
    }
    backup: {
      backupRetentionDays: 7
      geoRedundantBackup: 'Disabled'
    }
    network: {
      delegatedSubnetResourceId: ''
      privateDnsZoneArmResourceId: ''
    }
  }
}

// PostgreSQL Database
resource postgreSQLDatabase 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2023-12-01' = {
  parent: postgreSQLServer
  name: pgDatabaseName
}

// App Service
resource apiService 'Microsoft.Web/sites@2024-04-01' = {
  name: apiServiceName
  location: location
  tags: union(tags, { 'azd-service-name': 'api' })
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      alwaysOn: true
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'Jwt__Key'
          value: jwtKey
        }
        {
          name: 'Jwt__Issuer'
          value: jwtIssuer
        }
        {
          name: 'Jwt__Audience'
          value: jwtAudience
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          type: 'SQLAzure'
          connectionString: 'Host=${postgreSQLServer.properties.fullyQualifiedDomainName};Database=${pgDatabaseName};Username=${administratorLogin}@${postgreSQLServer.name};Password=${administratorLoginPassword}'
        }
      ]
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

// Outputs
output AZURE_LOCATION string = location
output AZURE_KEY_VAULT_ENDPOINT string = keyVault.properties.vaultUri
output AZURE_KEY_VAULT_NAME string = keyVault.name
output API_URI string = 'https://${apiService.properties.defaultHostName}'
