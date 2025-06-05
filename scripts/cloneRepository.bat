@echo off

set gitBashPath=C:\Program Files\Git\git-bash.exe
set startDir=%CD%

cd git_scripts

start "" "%gitBashPath%" --cd="%CD%" "./cloneRepository.sh"

cd %startDir%