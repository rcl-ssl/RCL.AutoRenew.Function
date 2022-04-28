# RCL AutoRenew Function
V6.0.10

Use this Microsoft Azure Function app to automatically renew SSL/TLS certificates created with the RCL Portal.

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Frcl-ssl%2FRCL.AutoRenew.Function%2Fmaster%2Fazuredeploy.json)

## Automatically Renew SSL/TLS Certificates

You can use the function app to automatically renew SSL/TLS certificates created in the **RCL Portal** using the following creation options :

- **Azure App Services** 
- Azure Key Vault + DNS (including SAN)
- Azure DNS (including SAN)

## How to use

- Deploy the function app to your Azure account
- Register and configure an **AAD Application** in Azure Active Directory for the function app to use
- On a weekly basis, the function app will check for certificates about to expire and renew them automatically

## Read the documentation

You can read the detailed documentation to configure, install and test the function app : 

[RCL AutoRenew Function Documentation](https://docs.rclapp.com/autorenew/autorenew.html)

## Contribute to this project

If you find a bug or want to add a new feature, we welcome contributions to this project.

This is how you can contribute :

- You need a basic understanding of Git and GitHub.com

- Open an [issue](https://github.com/rcl-ssl/RCL.AutoRenew.Function/issues) describing what you want to do, such as fixing a bug or adding a new feature. Wait for approval before you invest much time

- Fork the repo of the **master** branch and create a new branch for your changes

- Submit a pull request (PR) to the **master** branch with your changes

- Respond to PR feedback
