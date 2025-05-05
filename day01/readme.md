# Day 01 - May 5th, 2025

## Session Overview
- Installed SQL Server and SQL Server Management Studio
- Initialized Github repository to track training assignments
- Database design example
- Assignments

## Installing SQL Server and SQL Server Management Studio
[Instruction Video](https://youtu.be/7Cpigeoh0WI?si=z0abAkhF-enWG6nA)

Downloaded and installed [SQL Server 2022 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads?msockid=26110e4ba16e6d61115b1b9ea0c36ce6)

Downloaded and installed [SQL Server Management Studio v20.2.1](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms)

## Initializing Github repo to track training assignments
Downloaded and installed [Git for Windows](https://git-scm.com/downloads/win)

### Generating SSH key for authetication with Github
[Reference](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent)

In Git Bash, generate the SSH key
```
ssh-keygen -t ed25519 -C "your_email@example.com"
```

In admin elevated Powershell, start the ssh-agent
```
Get-Service -Name ssh-agent | Set-Service -StartupType Manual
Start-Service ssh-agent
```

In Terminal without elevated permissions, add the SSH private key to the ssh-agent
```
ssh-add c:/Users/YOU/.ssh/id_ed25519
```

### Adding SSH public key to Github
[Reference](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/adding-a-new-ssh-key-to-your-github-account)

In Terminal, copy the SSH public key to clipboard
```
cat ~/.ssh/id_ed25519.pub | clip
```

Add the copied SSH public key in the settings of your Github profile.

### First time Git configuration
[Reference](https://git-scm.com/book/en/v2/Getting-Started-First-Time-Git-Setup)
```
git config --global user.name "John Doe"
git config --global user.email johndoe@example.com
git config --global init.defaultBranch main
```

## Database design example
[Work file](database-design.txt)

**Soft Delete:** Modifying a column to indicate a record deletion instead of actually deleting the record. The deleted data can still be accessed (for auditing, compliance or analysis) or recovered if needed. It keeps track of changes over time and reduces the chance of losing important information.

## Assignments
[Questions](questions.txt)