## Overview:

Foundation is a collection of Unity and C# support code I've carried with me since I began using the product ten years
ago.

Not everything is here and its a WIP right now.

Foundation's current version is: `0.3.1`

## Installation:

Add to your "package.json" file

    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
    "com.dbrizov.naughtyattributes": "https://github.com/dbrizov/NaughtyAttributes.git#upm",
    "jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.13.2",
    "com.ramensea.foundation3d": "https://github.com/RamenSea/Foundation3D.git?path=/Packages/Foundation3D#0.3.1",
    "com.unity.addressables": "1.21.14",


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
- Create deploy script