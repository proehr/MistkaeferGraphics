# Mistkäfer Graphics Project
## Getting Started

* #### Game setup

To get started with developing, clone this repository and open the folder via Unity Hub.
If you decide to use any other IDE than Rider, please make sure the gitignore is setup accordingly.
More about general [Development Guidelines](#development-guidelines)

* #### Controls for player movement


## Built With
Software
* [Unity3d](https://unity3d.com/de/unity/whats-new/2020.3.2) -	Unity3d Version 2020.3.2
* [High Definition Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@10.4/manual/index.html) - Version 10.4.0

Operating system
* [Windows 10](https://www.microsoft.com/de-de/windows/) - 	Windows 10 Professional 64

## Development Guidelines

### Git Workflow

***Feature-Branch-Workflow***
1. Before starting your work on a new feature, always pull the newest version of [main](https://github.com/nimaazha/OculusQuestFitnessApp/tree/main) branch.
2. Create a new branch from main and give it a descriptive Name
    * You can use your trello card id and name for this, which can be found in the card url (for example 12-decide-on-a-git-workflow)
3. You can now work on your feature, commit and push changes inside of your Feature Branch. Make sure to always choose **descriptive** commit messages.
4. Once you have pushed all your changes for the feature, create a pull request to main branch in GitHub and assign a team member for code review.
5. If your pull request has been approved, the pull request can be merged into main.
    * If you encounter merge conflicts, merge main branch into your feature branch **locally** and push the merge commit afterwards.

### Code Review

This is a list of things you can look out for while doing code review:
* Does the code follow all naming conventions?
* Is the code too complex or can it be done simpler?
* Are all comments clear and helpful?
* Has the developer provided/updated documentation for their changes?
* Does the code function the way it is intended?
* Does the code follow our style guides?

### Other Guidelines

Always create your own scene if you want to test changes, the MainScene should be in a functioning state at all times.  
If you do make changes to a Scene you want to share, make sure to save your changes in unity before pushing to git.  
If you want to share Scene Elements with your team, create [Prefabs](https://docs.unity3d.com/Manual/Prefabs.html) and make sure they are placed properly.

## References


## Resources



## Authors (sorted alphabetically)
* **Bartholomäus Berresheim** - [Github](https://github.com/Silices)
* **Marie Lencer** - [Github](https://github.com/MarieLencer)
* **Pauline Röhr** - [Github](https://github.com/proehr)
