# Incompetent Aliens

## Project Organization

- version of unity and all imported packages is locked. just to reduce headaches.
- please don't adjust project settings, lighting, rendering, or environment settings without prior approval.
- Each major folder contains folders for: Actors, Environment, UI, and Managers (where applicable)
- In Scenes:
    - **Game:** contains actual scenes that will be used in the game
    - **Devspaces:** contains folders for each dev. The idea is devs can create their own scenes for testing and development in an isolated space without messing up project organization. This is only for scenes. (i had problems with this type of thing in the past, maybe this will help, it could also be unneccessary)
- Namespace Rules:
    - use namespaces to keep from accidental code conflicts.
    - most things will live in a namespace.
    - for safety, developers should not be working on the same namespace at the same time (if we are, we gotta stop and adjust)
    - namespaces should be super clear, short, and should not conflict with any other namespace or classnames within unity. IE:
        - Player
        - SceneLoader
        - Enemy
        - AudioManager
        - etc.

## Code Commenting

### In code:
``

    // ========== big label ==========
    // the above big label uses 10 "=" symbols on each side

    // ----- some small sub-title or label -----
    // some longer note below the small label explaining a little thing
    // the above small label uses 5 "-" symbols on each side

    // sub-sub-label or note

### Class explinatons
**note:** I hate this. However, it's helpful for team clarity. It's also good for submitting code to teachers.

''

    /*
    *   Short decription of class
    *
    *   [list of methods and what they do]
    *   IsGrounded(transform ground)            => boolean
    *
    *   CallTranstionToScene(string sceneName)  => start animated transition to a scene
    */

``

### TODO

``

    // TODO: thing todo

``
    
## Git Flow

### Branching:
- Main
    - prod
        - build_v0.0.1
        - etc.
    - dev
        - qa-branch
        - wyatt
            - my-feature-1
            - my-feature-2
        - aaron
        - tristan
        - lewis
        - heather
        - etc.

### Operations:
- Work on a *new* feature branch under your name branch.
- when finished, PR to dev.
- I will branch dev to qa and test
    - either approved and PR'd into main
    - not approved and you'll update.
- periodically I'll updated dev from main, I'll notify everyone when this happens.
    - dev will only get updated from main when all current tasks are completed. (sprint or whatever)
    - you will be prompted to fetch dev and update your name branches from dev.

### Commits:
- keep them small
- keep labels small and to the point
- don't mix multiple pieces of functionality into one commit

### NOTE:

I am not perfect, I may break my own rules occasionaly. Either on purpoes or accident.

If any of the above is not followed, it is not the end of the world. almost everything is fixable.