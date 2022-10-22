caption XanaOS
color green black
clear
[other]
set str=/$
if str = info inf
color red black
print Not found command &&&$str&&&;
color green black
go other
[inf]
print Version Alpha 1
print Author: InnieSharp
pause
go other