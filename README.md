# Timeslip's Oblivion Shader Editor

This repo contains my decompiled version of Timeslip's Oblivion Shader Editor.
The original program can be found on Timeslip's website here: [http://timeslip.chorrol.com/](http://timeslip.chorrol.com/)

JetBrains' dotPeek was used to decompile the original program. 

# Changes

The following changes have been made to the source code extracted from the original EXE:
- Project files upgraded to the Visual Studios 2019 edition.
- Fixed an error where a line of code wasn't able to convert char data into a string.
- Forced program to run in x86 mode as x64 mode can't call functions in the 32-bit ShaderDisasm.dll.
