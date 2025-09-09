The code depends on three environment variables. Add a launchSettings.json file to configure them, using the values available from the portal (https://ai.azure.com/)

```
{
  "profiles": {
    "YourProjectName": {
      "commandName": "Project",
      "environmentVariables": {
        "AZURE_OPENAI_ENDPOINT": "your-endpoint",
        "AZURE_OPENAI_MODEL": "your-model",
        "AZURE_OPENAI_APIKEY": "your-api-key-here"
      }
    }
  }
}
```
