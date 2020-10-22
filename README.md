# TT Games Explorer
#### *Previously named Lego Pak Explorer*
Originally programmed by Ac_K (pakexplorer@gmail.com), reversed, refactored and maintained by BRH Media.

### A Foreword
What happened to the original? To be honest, I couldn't tell you. The reality is, Ac_K has been silent about the project for over six years now, with promises to open source the tool falling into obscurity; I think it's safe to say the project's not coming back. It's due to this, that I've decided it's best the community take it into their own hands. The code (as it currently stands) contains much of the spaghetti that was the original code, and bad algorithms were present everywhere - however, it's all going to be eventually ripped out and rewritten. The plan moving forward is to become the new active maintainer of this project, and with that, the promise of hotfixes as necessary (which Ac_K was unfortunately not able to provide).

### How did you reverse it?
Ever heard of JetBrains dotPeek? It's amazing (and free)! The original author left all of his debug symbols embedded inside of the executable, which enabled me to use dotPeek's 'Export to Project' function for an entirely recompilable Visual Studio project including all code, images and resource files.

### Planned improvements?
The original purpose of this tool was actually to just browse the DAT archives, but that's not really useful is it? Especially with tools like QuickBMS, it's more a redundant feature than it is a useful one (though it's obviously still going to be present as a feature). Therefore, I've laid down what's to be added in order to provide an effective 'all-in-one' solution:
- The texture viewer needs to be revamped and allow metadata and proper exports as well as DXT3 support (it only does DXT1 and DXT5 right now; newer games don't use these)
- The DAT Extractor needs to actually interop with the rest of the progrma rather than it being an accessory; this requires a massive refactor and is already in progress.
- GHG/OBJ file model support! This is a no-brainer; all other tools can do this. I'll need to implement a new DirectX model renderer; but that isn't too hard. This should also allow you to repack the models too; I found some Python scripts that do this, but I don't want external dependencies. Instead, these will get rewritten in pure C# and thrown in a DLL for others to use (hopefully).
- PAK file extraction. Yes, I know these are backups - but you don't want to be deleting parts of the game no matter the redundancy; hence, the ability to extract TT's \*.pak format is necessary.
- Sound playback. OGG and WAV file support is on its way! This means you can also play them directly from the DAT Extractor without needing to extract first (yeah, that name needs changing).
- CBX files! What are they???!! This file format is weird, complicated and doesn't need to exist - they already use OGG and WAV, why not keep those? Anyway, the plan is to reverse these guys and chuck them in the existing sound playback framework.

...Plus a whole lot more behind-the-scenes work.
