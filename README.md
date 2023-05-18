A WIP but I am beginning to collect all my disparate Unity foundational code projects into this


This project will eventually require


    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
    "com.dbrizov.naughtyattributes": "https://github.com/dbrizov/NaughtyAttributes.git#upm",
    "jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.12.0",

Add:

"com.ramensea.foundation3d": "https://github.com/RamenSea/Foundation3D.git?path=/Packages/Foundation3D",



Todo:

- Create a versioning system so projects can be locked to a tag
- Break out `Foundation` into its own repo so non Unity projects can depend on it
- Create a Foundation3D.Netcode project to house all the shared netcode i have
- Create a Foundation2D for the 2D specific code
- Add tests
- Add the rest of my shared code here
- Improve predictable random
- Flesh out random extension
- Backport some of the vector2,3 helper code to C# built in vector class so `Foundation` can utilize it
- Add and sanitize a few of my common service classes
- Add documenation explaining stuff
- finalize a spec for the context to make using it easy in a normal project
- consider having this rely on DotTween