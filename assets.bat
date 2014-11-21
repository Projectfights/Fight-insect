@echo off

::Cleans directory
rmdir SuperBugFighter\SuperBugFighter\Content\ /Q /S

::Copies Content folder to SuperBugFighter
xcopy SuperBugFighterContent\Assets\Assets\bin\PSM\Content\* SuperBugFighter\SuperBugFighter\Content\ /s /e